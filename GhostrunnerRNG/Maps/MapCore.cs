using System.Collections.Generic;
using System.Diagnostics;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.Maps {
    public abstract class MapCore {

        // List of all enemies(spawndata per enemy)
        public List<Enemy> Enemies = new List<Enemy>();
        public MapType mapType { get; private set; }

        public MapCore(MapType mapType) {
            this.mapType = mapType;
        }

        // new RNG
        public void RandomizeEnemies(Process game) {
            for(int i = 0; i < Enemies.Count; i++) {
                Enemies[i].SetMemoryPos(game, Enemies[i].GetSpawnData());
            }
        }

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