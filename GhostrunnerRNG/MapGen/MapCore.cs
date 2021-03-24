using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
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

        public MapType mapType { get; private set; }
        public MapCore(MapType mapType) {
            this.mapType = mapType;
            Config.GetInstance().NewSeed();
        }

        public List<Enemy> GetAllEnemies(Process game, int startIndex = 0) {
            int index = startIndex;
            List<Enemy> enemies = new List<Enemy>();
            Enemy enemy = new Enemy(new DeepPointer(0x045A3C20, 0x138, 0xB0, 0xB0, 0x20, 0x4F0));
            while(!enemy.GetMemoryPos(game).IsEmpty()) {
                index++;
                enemies.Add(enemy);
                enemy = new Enemy(new DeepPointer(0x045A3C20, 0x138, 0xB0, 0xB0, (0x20 * (index + 1)), 0x4F0));
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
                enemy = new Enemy(new DeepPointer(0x045A3C20, 0x138, 0xB0, 0xB0, (0x20 * (index + 1)), 0x4F0));
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

                // uplinks and other nonPlaceableObjects
                for(int i = 0; i < nonPlaceableObjects.Count; i++) {
                    nonPlaceableObjects[i].Randomize(game);
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
                        var test = planes[planeIndex].GetRandomSpawnData();

                        EnemiesWithoutCP[i].SetMemoryPos(game, test);
                        // update corresponding item in spawnPlanesLeft
                        int indexToRemove = RoomLayout.GetSameSpawnPlaneIndex(spawnPlanesLeft, planes[planeIndex]);
                        if(indexToRemove > -1)
                            spawnPlanesLeft.RemoveAt(indexToRemove);
                    }
                }
            }
        }

        protected void RandomPickEnemiesWithoutCP(ref List<Enemy> enemies, bool force = false, bool removeCP = true, int enemyIndex = -1) {
            if(enemies == null || enemies.Count == 0) return;

            // 50-50 chance to even pick an enemy
            if(!force && Config.GetInstance().r.Next(2) == 0) return;

            // pick random enemy, remove cp
            int index = enemyIndex < 0 ? Config.GetInstance().r.Next(enemies.Count) : enemyIndex;
            if(removeCP) {
                enemies[index].DisableAttachedCP(GameHook.game);
            }
            // add select to list and remove it from enemies, so it won't be used in spawnplanes
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
