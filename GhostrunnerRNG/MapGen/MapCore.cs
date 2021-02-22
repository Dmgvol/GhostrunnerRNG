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
            int totalEnemies = 0;
            Enemy enemy = new Enemy(new DeepPointer(0x045A3C20, 0x138, 0xB0, 0xB0, 0x20, 0x4F0));
            while(!enemy.GetMemoryPos(game).IsEmpty()) {
                str += $"enemy[{totalEnemies}]: {enemy.GetMemoryPos(game)}\n";

                totalEnemies++;

                enemy = new Enemy(new DeepPointer(0x045A3C20, 0x138, 0xB0, 0xB0, (0x20 * (totalEnemies + 1)), 0x4F0));
            }
            return str;
        }

        public List<Enemy> GetAllEnemies(Process game) {
            string str = "";
            int totalEnemies = 0;
            List<Enemy> enemies = new List<Enemy>();
            Enemy enemy = new Enemy(new DeepPointer(0x045A3C20, 0x138, 0xB0, 0xB0, 0x20, 0x4F0));
            while(!enemy.GetMemoryPos(game).IsEmpty()) {
                //str += $"enemy[{totalEnemies}]: {enemy.GetMemoryPos(game)}\n";
                totalEnemies++;
                enemies.Add(enemy);
                enemy = new Enemy(new DeepPointer(0x045A3C20, 0x138, 0xB0, 0xB0, (0x20 * (totalEnemies + 1)), 0x4F0));
            }
            return enemies;
        }

        protected abstract void Gen_PerRoom();

        // new RNG
        public void RandomizeEnemies(Process game) {
            if(Rooms != null && Rooms.Count > 0) {
                // RoomLayout Gen
                for(int i = 0; i < Rooms.Count; i++) {
                    Rooms[i].RandomizeEnemies(game);
                }
            } else {
                // PerEnemy Gen
                for(int i = 0; i < Enemies.Count; i++) {
                    Enemies[i].SetMemoryPos(game, Enemies[i].GetSpawnData());
                }
            }
        }

        [Obsolete("No need, it derefs per enemy once needed to change memory")]
        public void DerefPointers(Process game) {
            for(int i = 0; i < Enemies.Count; i++) {
                Enemies[i].DerefPointer(game);
            }
        }

         ~MapCore() {
            foreach(Enemy e in Enemies) {
                e.ClearAllPlanes();
            }
            Enemies.Clear();
        }
    }
}