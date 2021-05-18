﻿using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.GameObjects;
using GhostrunnerRNG.Maps;
using GhostrunnerRNG.MemoryUtils;
using GhostrunnerRNG.NonPlaceableObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.MapGen {
    public abstract class MapCore {

        // List of all enemies(spawndata per enemy)
        public List<Enemy> Enemies = new List<Enemy>();

        // room layouts (different gen)
        protected List<RoomLayout> Rooms = new List<RoomLayout>();
        protected List<DynamicRoomLayout> DynamicRooms = new List<DynamicRoomLayout>();

        // enemies without cp
        protected List<Enemy> EnemiesWithoutCP = new List<Enemy>();

        // list of WorldObject
        protected List<WorldObject> worldObjects = new List<WorldObject>();

        // custom checkpoints
        protected List<CustomCP> CustomCheckPoints = new List<CustomCP>();

        private EasyPointers CoreEP = new EasyPointers();

        public MapType mapType { get; private set; }

        // general
        public bool HasRng = true;
        public bool CPRequired = true;

        // forced cp
        private Thread Thread_Restart;
        private bool FirstUpdate = true;
        public bool ForcedCPFlag = false;

        // player move
        private Vector3f _playerPos;
        protected bool PlayerMoved = false;

        public MapCore(MapType mapType, bool BeforeCV = true, bool manualGen = false) {
            this.mapType = mapType;
            Config.GetInstance().NewSeed();
            if(Config.GetInstance().Settings_ForcedRestart) {
                CoreEP.Add("RestartFlag", new DeepPointer(0x0438D7F8, 0x58, 0xB0));
                CoreEP.Add("RestartBind", new DeepPointer(0x0438D7F8, 0x70, 0x60, 0x188));
                CoreEP.Add("CutsceneTimer", new DeepPointer(0x04609420, 0x128, 0x38C));
                CoreEP.Add("CanRestart", new DeepPointer(0x04609420, 0x188, 0x2EB));
                CheckPlayerMove(true);
            }


            if(manualGen) return;

            // Level has IModes Interface
            if(this is IModes mode) {
                if(GameHook.IsHC) {
                    mode.Gen_Hardcore();
                    return;
                }

                switch(Config.GetInstance().Setting_Difficulty) {
                    case Config.Difficulty.Easy:
                        mode.Gen_Easy();
                        break;
                    case Config.Difficulty.Normal:
                        mode.Gen_Normal();
                        break;
                    case Config.Difficulty.Nightmare:
                        mode.Gen_Nightmare();
                        break;
                }
                return;
            }

            //  Modes Interface
            if(this is IModesMidCV modeCV) {
                if(GameHook.IsHC) {
                    modeCV.Gen_Hardcore();
                    return;
                }

                if(BeforeCV) {
                    switch(Config.GetInstance().Setting_Difficulty) {
                        case Config.Difficulty.Easy:
                            modeCV.Gen_Easy_BeforeCV();
                            break;
                        case Config.Difficulty.Normal:
                            modeCV.Gen_Normal_BeforeCV();
                            break;
                        case Config.Difficulty.Nightmare:
                            modeCV.Gen_Nightmare_BeforeCV();
                            break;
                    }
                    return;
                } else {
                    switch(Config.GetInstance().Setting_Difficulty) {
                        case Config.Difficulty.Easy:
                            modeCV.Gen_Easy_AfterCV();
                            break;
                        case Config.Difficulty.Normal:
                            modeCV.Gen_Normal_AfterCV();
                            break;
                        case Config.Difficulty.Nightmare:
                            modeCV.Gen_Nightmare_AfterCV();
                            break;
                    }
                    return;
                }
            }

            // Not manual? no interface? load default
            Gen_PerRoom();

        }

        public List<Enemy> GetAllEnemies(Process game, int startIndex = 0) {
            int index = startIndex;
            List<Enemy> enemies = new List<Enemy>();
            Enemy enemy = new Enemy(new DeepPointer(0x04609420, 0x138, 0xB0, 0xB0, 0x20, 0x4F0));
            while(!enemy.GetMemoryPos(game).IsEmpty()) {
                index++;
                enemies.Add(enemy);
                enemy = new Enemy(new DeepPointer(0x04609420, 0x138, 0xB0, 0xB0, (0x20 * (index + 1)), 0x4F0));
            }
            return enemies;
        }

        public List<Enemy> GetAllEnemies(Process game, int startIndex, int enemiesTarget) {
            int index = startIndex;
            List<Enemy> enemies = new List<Enemy>();
            int threshold = 5;
            Enemy enemy;
            bool ValidEnemy = true;
            index--;

            while(ValidEnemy || threshold > 0) {
                index++;
                enemy = new Enemy(new DeepPointer(0x04609420, 0x138, 0xB0, 0xB0, (0x20 * (index + 1)), 0x4F0));
                ValidEnemy = !enemy.GetMemoryPos(game).IsEmpty();
                if(!ValidEnemy) {
                    threshold--;
                } else {
                    threshold = 5;
                    enemies.Add(enemy);

                    // N enemies reached? return list
                    if(enemies.Count == enemiesTarget)
                        return enemies;
                }
            }
            return enemies;
        }

        protected abstract void Gen_PerRoom();

        // new RNG
        public virtual void RandomizeEnemies(Process game) {
            // Dynamic Rooms
            if(DynamicRooms?.Count > 0) {
                DynamicRooms.ForEach(x => x.RandomizeEnemies(game));
            }

            // Generic Rooms
            if(Rooms != null && Rooms.Count > 0) {
               
                // Regular Rooms
                for(int i = 0; i < Rooms.Count; i++) {
                    Rooms[i].RandomizeEnemies(game);
                }

                // fix orb beams
                for(int i = 0; i < Rooms.Count; i++) {
                    Rooms[i].FixOrbBeams(game);
                }
                // enemies without cp
                if(EnemiesWithoutCP.Count > 0) {
                    List<SpawnPlane> spawnPlanesLeft = new List<SpawnPlane>();
                    var roomsList = Rooms.Where(x => x.IsRoomDefaultType()).ToList(); // to avoid orb planes
                    // add all remaining spawn planes from all rooms into one list
                    for(int i = 0; i < roomsList.Count; i++) {
                        spawnPlanesLeft.AddRange(roomsList[i].availableSpawnPlanes);
                    }

                    if(spawnPlanesLeft.Count == 0) return;

                    // pick random room, and plane with in
                    for(int i = 0; i < EnemiesWithoutCP.Count; i++) {
                        // list of left planes which are suitable for current enemy
                        var planes = spawnPlanesLeft.Where(x => x.IsEnemyAllowed(EnemiesWithoutCP[i].enemyType) && x.CanAddEnemies() && x.ReuseFlag).ToList();
                        if(planes.Count == 0) continue;
                        int planeIndex = Config.GetInstance().r.Next(0, planes.Count);
                        var spawnData = planes[planeIndex].GetRandomSpawnData();

                        EnemiesWithoutCP[i].SetMemoryPos(game, spawnData);
                        // update corresponding item in spawnPlanesLeft
                        int indexToRemove = RoomLayout.GetSameSpawnPlaneIndex(spawnPlanesLeft, planes[planeIndex]);
                        if(indexToRemove > -1)
                            spawnPlanesLeft.RemoveAt(indexToRemove);
                    }
                }
            }
            

            // uplinks and other nonPlaceableObjects
            for(int i = 0; i < worldObjects.Count; i++) {
                if(worldObjects[i] is NonPlaceableObject npo) {
                    // easy mode? skip Uplinks, billboards and AmidaFans RNG
                    if(Config.GetInstance().Setting_Difficulty == Config.Difficulty.Easy &&
                        Config.GetInstance().Settings_DisableUplinksOnEasy &&
                        (npo is UplinkJump || npo is UplinkShurikens || npo is UplinkSlowmo ||
                        npo is Billboard || npo is ToggleableFan || npo is ShurikenTarget)) return;


                    npo.Randomize(game);
                }else {
                    worldObjects[i].SetMemoryPos(game);
                }
            }

        }
        
        // check if player moved from spawn pos
        public void CheckPlayerMove(bool first = false) {
            if(PlayerMoved) return;
            if(first) {
                _playerPos = new Vector3f(GameHook.xPos, GameHook.yPos, GameHook.zPos);
                return;
            }
            // check if played moved
            if(_playerPos.X != GameHook.xPos || _playerPos.Y != GameHook.yPos || _playerPos.Z != GameHook.zPos)
                PlayerMoved = true;
        }

        public virtual void UpdateMap(Vector3f Player) {
            // check custom checkpoints
            if(CustomCheckPoints != null && CustomCheckPoints.Count > 0) {
                for(int i = 0; i < CustomCheckPoints.Count; i++) {
                    CustomCheckPoints[i].Update(Player);
                }
                CustomCheckPoints.RemoveAll(x => x.CPTriggered);
            }

            // Deref EasyPointers - if needed
            if(Config.GetInstance().Settings_ForcedRestart && FirstUpdate) {
                FirstUpdate = false;
                CoreEP.DerefPointers(GameHook.game);

                // update player pos
                CheckPlayerMove(true);
            }

            // player moved?
            if(!PlayerMoved)
                CheckPlayerMove();

            // forced restart?
            if(Config.GetInstance().Settings_ForcedRestart && !ForcedCPFlag && CPRequired && GameHook.CP_COUNTER == 0) {
                float value = 0;
                bool canRestart;
                GameHook.game.ReadValue(CoreEP.Pointers["CutsceneTimer"].Item2, out value);
                GameHook.game.ReadValue(CoreEP.Pointers["CanRestart"].Item2, out canRestart);
                
                if((PlayerMoved && canRestart && mapType != MapType.Faster) || value > 0) {
                    // Note: Faster; restart only after cutscene to avoid NG new upgrade being stuck
                    ForcedCPFlag = true;
                    ForceRestart();
                }
            }
        }

        public void DEV_FindAllCP(List<Room> rooms) {
            int c = 0;
            for(var i = 1; i < 1000; i++) {
                for(var j = 1; j < 20; j++) {
                    DeepPointer hitDP = new DeepPointer(0x04609420, 0x98, 0x8 * (j - 1), 0x128, 0xA8, 0x8 * (i - 1), 0x248, 0x1D0);
                    IntPtr hitPtr;
                    hitDP.DerefOffsets(GameHook.game, out hitPtr);
                    float x,y,z;
                    GameHook.game.ReadValue(hitPtr, out x);
                    GameHook.game.ReadValue(hitPtr + 4, out y);
                    GameHook.game.ReadValue(hitPtr + 8, out z);

                    if(rooms.Where(r => PlayerWithinRectangle(new Vector3f(x,y,z), r.pointA, r.pointB)).Count() > 0) {
                        c++;
                        Console.WriteLine($"CP:\nPos: {x} {y} {z}");
                        Console.WriteLine($"(0x04609420, 0x98, 0x{0x8 * (j - 1):X}, 0x128, 0xA8, 0x{0x8 * (i - 1):X}, 0x248, 0x1D0)");
                    }
                }
            }
            Console.WriteLine("found: " + c);
        }

        public void DEV_GetEnemyTypes(List<Enemy> enemies) {
            for(int i = 0; i < enemies.Count; i++) {
                List<int> offsets = new List<int>(enemies[i].GetObjectDP().GetOffsets());
                offsets[offsets.Count - 1] = 0x0;
                DeepPointer parentDP = new DeepPointer(enemies[i].GetObjectDP().GetBase(), offsets);
                IntPtr parentPtr;
                parentDP.DerefOffsets(GameHook.game, out parentPtr);
                //  pos
                int value;
                GameHook.game.ReadValue(parentPtr, out value);
                Console.WriteLine($"Enemy: {i}: {value}");
            }
        }

        public void ForceRestart() {
            // Doing it on a separated thread to avoid game/rng getting stuck
            Thread_Restart = new Thread(new ThreadStart(TRestart));
            Thread_Restart.Start();
        }

        private void TRestart() {
            // read player settings
            byte[] restartFlag = new byte[1];
            int restartBind;
            GameHook.game.ReadBytes(CoreEP.Pointers["RestartFlag"].Item2, restartFlag.Length, out restartFlag);
            GameHook.game.ReadValue(CoreEP.Pointers["RestartBind"].Item2, out restartBind);

            var key = ConvertKeybindToKey(restartBind); // get current keybinding
            if(key == null) { // failed to get key?
                MainWindow.GlobalLog = "Error:\n Failed to read Restart Keybinding";
                return;
            }

            // restart disabled? enable it!
            if(restartFlag[0] == 0) GameHook.game.WriteBytes(CoreEP.Pointers["RestartFlag"].Item2, new byte[] {1 });

            if(mapType == MapType.Awakening) Thread.Sleep(500);
            // simulate restart key
            if(key is VirtualKeyCode k) globalKeyboardHook.KeyPress(k);

            // restart was disabled?
            if(restartFlag[0] == 0) {
                Thread.Sleep(100);
                GameHook.game.WriteBytes(CoreEP.Pointers["RestartFlag"].Item2, restartFlag);
            }
        }


        protected void RandomPickEnemiesWithoutCP(ref List<Enemy> enemies, bool force = false, bool removeCP = true, int enemyIndex = -1, int enemyIndexBesides = -1) {
            if(enemies == null || enemies.Count == 0) return;

            // 50-50 chance to even pick an enemy
            if(!force && Config.GetInstance().r.Next(2) == 0) return;
            int index;
            if(enemyIndexBesides < 0) {
                index = enemyIndex < 0 ? Config.GetInstance().r.Next(enemies.Count) : enemyIndex;
            } else {
                List<int> indexes = new List<int>();
                for(int i = 0; i < enemies.Count; i++) indexes.Add(i);
                indexes.RemoveAt(enemyIndexBesides);
                index = Config.GetInstance().r.Next(indexes.Count);
            }

            // pick random enemy, remove cp
            if(removeCP) {
                enemies[index].DisableAttachedCP(GameHook.game);
            }

            // add select to list and remove it from enemies, so it won't be used in spawnplanes
            EnemiesWithoutCP.Add(enemies[index]);
            enemies.RemoveAt(index);
        }

        protected List<Enemy> GetAllEnemies_Bulk(int index, int target, Process game, List<Vector3f> positions) {
            List<Enemy> enemiesBulk = new List<Enemy>();
            List<Enemy> enemies = new List<Enemy>();
            Enemy enemy;
            bool ValidEnemy = true;
            index--;
            // get bulk enemies
            while(ValidEnemy || index < target) {
                index++;
                enemy = new Enemy(new DeepPointer(0x04609420, 0x138, 0xB0, 0xB0, (0x20 * (index + 1)), 0x4F0));
                ValidEnemy = !enemy.GetMemoryPos(game).IsEmpty();
                if(ValidEnemy) {
                    enemiesBulk.Add(enemy);
                }
            }

            // get needed only
            for(int i = 0; i < enemiesBulk.Count; i++) {
                for(int j = 0; j < positions.Count; j++) {
                    if(EnemyInApproximetry(enemiesBulk[i].Pos, positions[j])) { 
                        enemies.Add(enemiesBulk[i]);
                    }
                }
            }

            return enemies;
        }

        private bool EnemyInApproximetry(Vector3f enemy, Vector3f target, float threshold = 50) {
            Vector3f cornerA = target - new Vector3f(threshold, threshold, threshold);
            Vector3f cornerB = target + new Vector3f(threshold, threshold, threshold);

            return (enemy.X >= Math.Min(cornerA.X, cornerB.X) && enemy.X <= Math.Max(cornerA.X, cornerB.X) &&
               enemy.Y >= Math.Min(cornerA.Y, cornerB.Y) && enemy.Y <= Math.Max(cornerA.Y, cornerB.Y) &&
               enemy.Z >= Math.Min(cornerA.Z, cornerB.Z) && enemy.Z <= Math.Max(cornerA.Z, cornerB.Z));
        }

        protected void RemoveParentObjects(ref List<Enemy> enemies) {
            enemies.ForEach(x => x.DisableAttachedCP(GameHook.game));
        }

        protected List<Enemy> RemoveParentObjects(List<Enemy> enemies) {
            RemoveParentObjects(ref enemies);
            return enemies;
        }

        /// <summary>
        /// Removes enemy from CP/Door and reduces enemy count if found, removes from enemiesList and moves to EnemiesWithoutCP
        /// </summary>
        /// <param name="enemies">Referance to enemies list</param>
        /// <param name="force">false has a 50% chance</param>
        /// <param name="enemyIndex">Specific enemy index, leave empty for random(default)</param>
        /// <param name="moveToEnemiesWithoutCP">To move enemy to EnemiesWithoutCP? true by default</param>
        /// <param name="modifyList">To remove enemy from referenced enemies list? true by default</param>
        protected void DetachEnemyFromCP(ref List<Enemy> enemies, bool force = false, int enemyIndex = -1, bool moveToEnemiesWithoutCP = true, bool modifyList = true) {
            if(enemies == null || enemies.Count == 0) return;
            if(!force && Config.GetInstance().r.Next(2) == 0) return;
            if(enemyIndex < 0) enemyIndex = Config.GetInstance().r.Next(enemies.Count);
            enemies[enemyIndex].DisableAttachedCP(GameHook.game);

            if(moveToEnemiesWithoutCP) EnemiesWithoutCP.Add(enemies[enemyIndex]);
            if(modifyList) enemies.RemoveAt(enemyIndex);
        }

        public static void ModifyCP(DeepPointer dp, Vector3f pos, Process game) {
            IntPtr cpPtr;
            dp.DerefOffsets(game, out cpPtr);
            game.WriteBytes(cpPtr, BitConverter.GetBytes(pos.X));
            game.WriteBytes(cpPtr + 4, BitConverter.GetBytes(pos.Y));
            game.WriteBytes(cpPtr + 8, BitConverter.GetBytes(pos.Z));
        }

        public static void ModifyCP(DeepPointer dp, Vector3f pos, Angle angle, Process game) {
            IntPtr cpPtr;
            dp.DerefOffsets(game, out cpPtr);
            // pos
            game.WriteBytes(cpPtr, BitConverter.GetBytes(pos.X));
            game.WriteBytes(cpPtr + 4, BitConverter.GetBytes(pos.Y));
            game.WriteBytes(cpPtr + 8, BitConverter.GetBytes(pos.Z));
            // angle
            game.WriteBytes(cpPtr - 8, BitConverter.GetBytes(angle.angleSin));
            game.WriteBytes(cpPtr - 4, BitConverter.GetBytes(angle.angleCos));
        }

        protected void ModifyCP(Process game, SpawnData sp, DeepPointer dp, int[] posAppendOffsets, int[] angleAppendOffsets) {
            DeepPointer posDP = AppendBaseOffset(dp, posAppendOffsets);
            DeepPointer angleDP = AppendBaseOffset(dp, angleAppendOffsets);

            // Deref
            IntPtr posPtr, anglePtr;
            posDP.DerefOffsets(game, out posPtr);
            angleDP.DerefOffsets(game, out anglePtr);
            // Update Pos
            game.WriteBytes(posPtr, BitConverter.GetBytes(sp.pos.X));
            game.WriteBytes(posPtr + 4, BitConverter.GetBytes(sp.pos.Y));
            game.WriteBytes(posPtr + 8, BitConverter.GetBytes(sp.pos.Z));
            // Update Angle
            game.WriteBytes(anglePtr, BitConverter.GetBytes(sp.angle.angleSin));
            game.WriteBytes(anglePtr + 4, BitConverter.GetBytes(sp.angle.angleCos));
        }

        private DeepPointer AppendBaseOffset(DeepPointer dp, int[] appendOffsets) {
            List<int> offsets = new List<int>(dp.GetOffsets());
            offsets.AddRange(appendOffsets); // add new offsets
            return new DeepPointer(dp.GetBase(), new List<int>(offsets));
        }

        ~MapCore() {
            foreach(Enemy e in Enemies) {
                e.ClearAllPlanes();
            }
            Enemies.Clear();
        }
    }
}
