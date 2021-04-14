using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.Maps;
using GhostrunnerRNG.NonPlaceableObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.MapGen {
    public abstract class MapCore {

        // List of all enemies(spawndata per enemy)
        public List<Enemy> Enemies = new List<Enemy>();

        // room layouts (different gen)
        protected List<RoomLayout> Rooms;

        // enemies without cp
        protected List<Enemy> EnemiesWithoutCP = new List<Enemy>();

        // list of NonPlaceableObjects
        protected List<NonPlaceableObject> nonPlaceableObjects = new List<NonPlaceableObject>();

        // custom checkpoints
        protected List<CustomCP> CustomCheckPoints = new List<CustomCP>();

        public MapType mapType { get; private set; }

        // general
        public bool HasRng = true;
        public bool CPRequired = true;

        public MapCore(MapType mapType, bool BeforeCV = true, bool manualGen = false) {
            this.mapType = mapType;
            Config.GetInstance().NewSeed();

            if(manualGen) return;

            // Level has IModes Interface
            if(this is IModes mode) {
                switch(Config.GetInstance().Setting_Difficulty) {
                    case Config.Difficulty.Easy:
                        mode.Gen_Easy();
                        break;
                    case Config.Difficulty.Normal:
                        mode.Gen_Normal();
                        break;
                    case Config.Difficulty.SR:
                        mode.Gen_SR();
                        break;
                    case Config.Difficulty.Nightmare:
                        mode.Gen_Nightmare();
                        break;
                }

                if(GameHook.IsHC) mode.Gen_Hardcore();
                return;
            }

            //  Modes Interface
            if(this is IModesMidCV modeCV) {
                if(BeforeCV) {
                    switch(Config.GetInstance().Setting_Difficulty) {
                        case Config.Difficulty.Easy:
                            modeCV.Gen_Easy_BeforeCV();
                            break;
                        case Config.Difficulty.Normal:
                            modeCV.Gen_Normal_BeforeCV();
                            break;
                        case Config.Difficulty.SR:
                            modeCV.Gen_SR_BeforeCV();
                            break;
                        case Config.Difficulty.Nightmare:
                            modeCV.Gen_Nightmare_BeforeCV();
                            break;
                    }

                    if(GameHook.IsHC) modeCV.Gen_Hardcore_BeforeCV();
                    return;
                } else {
                    switch(Config.GetInstance().Setting_Difficulty) {
                        case Config.Difficulty.Easy:
                            modeCV.Gen_Easy_AfterCV();
                            break;
                        case Config.Difficulty.Normal:
                            modeCV.Gen_Normal_AfterCV();
                            break;
                        case Config.Difficulty.SR:
                            modeCV.Gen_SR_AfterCV();
                            break;
                        case Config.Difficulty.Nightmare:
                            modeCV.Gen_Nightmare_AfterCV();
                            break;
                    }

                    if(GameHook.IsHC) modeCV.Gen_Hardcore_AfterCV();
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
            if(Rooms != null && Rooms.Count > 0) {
                // RoomLayout Gen
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
            for(int i = 0; i < nonPlaceableObjects.Count; i++) {
                nonPlaceableObjects[i].Randomize(game);
            }

        }

        public virtual void UpdateMap(Vector3f Player) {
            // check custom checkpoints
            if(CustomCheckPoints != null && CustomCheckPoints.Count > 0) {
                for(int i = 0; i < CustomCheckPoints.Count; i++) {
                    CustomCheckPoints[i].Update(Player);
                }
                CustomCheckPoints.RemoveAll(x => x.CPTriggered);
            }
        }

        protected void RandomPickEnemiesWithoutCP(ref List<Enemy> enemies, bool force = false, bool removeCP = true, int enemyIndex = -1) {
            if(enemies == null || enemies.Count == 0) return;

            // 50-50 chance to even pick an enemy
            if(!force && Config.GetInstance().r.Next(2) == 0) return;
            int index = enemyIndex < 0 ? Config.GetInstance().r.Next(enemies.Count) : enemyIndex;

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
                    if((int)enemiesBulk[i].Pos.X == (int)positions[j].X && (int)enemiesBulk[i].Pos.Y == (int)positions[j].Y && (int)enemiesBulk[i].Pos.Z == (int)positions[j].Z) {
                        enemies.Add(enemiesBulk[i]);
                    }
                }
            }

            return enemies;
        }

        protected void TakeLastEnemyFromCP(ref List<Enemy> enemies, bool force = false, bool removeCP = true, bool attachedDoor = false, int enemyIndex = 0) {
            if(enemies == null || enemies.Count == 0) return;

            // 50-50 chance to even pick an enemy
            if(!force && Config.GetInstance().r.Next(2) == 0) return;

            int index = -1;

            // create DP to the last enemy in the list
            List<int> offsets = new List<int>(enemies[enemyIndex].GetObjectDP().GetOffsets());
            offsets.RemoveAt(offsets.Count - 1); // removes last offset
            offsets.Add(0x5D0);
            offsets.Add(0x228);
            offsets.Add(0x8 * (enemies.Count - 1));
            offsets.Add(0x0);
            DeepPointer lastenemyDP = new DeepPointer(enemies[enemyIndex].GetObjectDP().GetBase(), new List<int>(offsets));
            // get enemy pointer
            IntPtr lastenemyPtr;
            lastenemyDP.DerefOffsets(GameHook.game, out lastenemyPtr);
            //find last enemy index
            DeepPointer enemyDP;
            IntPtr enemyPtr;
            for(var i = 0; i < enemies.Count; i++) {
                offsets = new List<int>(enemies[i].GetObjectDP().GetOffsets());
                offsets[offsets.Count - 1] = 0x0; // set last offset to 0
                enemyDP = new DeepPointer(enemies[i].GetObjectDP().GetBase(), new List<int>(offsets));
                enemyDP.DerefOffsets(GameHook.game, out enemyPtr);
                if(enemyPtr == lastenemyPtr) {
                    index = i;
                    break;
                }
            }
            //return if didn't find last enemy index
            if(index == -1) {
                return;
            }

            // attached to a door? find last enemy in the list and reduce door needed enemies count
            if(attachedDoor) {
                offsets = new List<int>(enemies[index].GetObjectDP().GetOffsets());
                offsets.RemoveAt(offsets.Count - 1); // removes last offset
                offsets.Add(0x5D0);
                offsets.Add(0x230);
                DeepPointer doorDP = new DeepPointer(enemies[index].GetObjectDP().GetBase(), new List<int>(offsets));
                // get value, reduce by one and write back
                IntPtr doorPtr;
                int doorCount;
                doorDP.DerefOffsets(GameHook.game, out doorPtr);
                // read default value
                GameHook.game.ReadValue<int>(doorPtr, out doorCount);
                doorCount -= 1;
                // update decreased value
                GameHook.game.WriteBytes(doorPtr, BitConverter.GetBytes(doorCount));
            }

            // remove cp for last enemy in the list
            if(removeCP) {
                enemies[index].DisableAttachedCP(GameHook.game);
            }

            // add last enmy to list and remove it from enemies, so it won't be used in spawnplanes
            EnemiesWithoutCP.Add(enemies[index]);
            enemies.RemoveAt(index);
        }

        protected void ModifyCP(DeepPointer dp, Vector3f pos, Process game) {
            IntPtr cpPtr;
            dp.DerefOffsets(game, out cpPtr);
            game.WriteBytes(cpPtr, BitConverter.GetBytes(pos.X));
            game.WriteBytes(cpPtr + 4, BitConverter.GetBytes(pos.Y));
            game.WriteBytes(cpPtr + 8, BitConverter.GetBytes(pos.Z));
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

        protected void PrintEnemyPos(List<Enemy> enemies) {
            string str = "";
            foreach(Enemy e in enemies) str += e.Pos + "\n";
            Console.WriteLine(str);
        }

         ~MapCore() {
            foreach(Enemy e in Enemies) {
                e.ClearAllPlanes();
            }
            Enemies.Clear();
        }
    }
}
