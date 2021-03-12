using GhostrunnerRNG.Enemies;
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

        public MapType mapType { get; private set; }
        public MapCore(MapType mapType) {
            this.mapType = mapType;
            SpawnPlane.r = new Random();
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
                        int planeIndex = SpawnPlane.r.Next(0, planes.Count);
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

        protected void RandomPickEnemiesWithoutCP(ref List<Enemy> enemies, bool force = false, bool removeCP = true) {
            if(enemies == null || enemies.Count == 0) return;

            // 50-50 chance to even pick an enemy
            if(!force && SpawnPlane.r.Next(2) == 0) return;

            // pick random enemy, remove cp
            int index = SpawnPlane.r.Next(enemies.Count);
            if(removeCP) {
                enemies[index].DisableAttachedCP(MainWindow.game);
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