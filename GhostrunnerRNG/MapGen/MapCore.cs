using System;
using System.Collections.Generic;
using System.Diagnostics;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.Maps {
    public abstract class MapCore {

        // List of all enemies(spawndata per enemy)
        public List<Enemy> Enemies = new List<Enemy>();

        // room layouts (different gen)
        protected List<RoomLayout> Rooms;

        public MapType mapType { get; private set; }
        public MapCore(MapType mapType) {
            this.mapType = mapType;
        }

        // FOR DEBUG
        public string GetAllEnemyPositions(Process game) {
            string str = "";
            List<Enemy> enemies = GetAllEnemies(game);
            for(int i = 0; i < enemies.Count; i++)
                str += $"enemy[{i}]: {enemies[i].GetMemoryPos(game)}\n";
            return str;
        }

        //public List<Enemy> GetAllEnemies(Process game) {
        //    int totalEnemies = 0;
        //    List<Enemy> enemies = new List<Enemy>();
        //    Enemy enemy = new Enemy(new DeepPointer(0x045A3C20, 0x138, 0xB0, 0xB0, 0x20, 0x4F0));
        //    while(!enemy.GetMemoryPos(game).IsEmpty()) {
        //        totalEnemies++;
        //        enemies.Add(enemy);
        //        enemy = new Enemy(new DeepPointer(0x045A3C20, 0x138, 0xB0, 0xB0, (0x20 * (totalEnemies + 1)), 0x4F0));
        //    }
        //    return enemies;
        //}

        public List<Enemy> GetAllEnemies(Process game, int startIndex = 0, int enemiesTarget = 100) {
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
            } 
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