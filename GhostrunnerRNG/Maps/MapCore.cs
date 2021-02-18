using System.Collections.Generic;
using System.Diagnostics;

namespace GhostrunnerRNG.Maps {
    public abstract class MapCore {

        // List of all enemies(spawndata per enemy)
        public List<Enemy> Enemies = new List<Enemy>();
        public string mapCodeName;


        public MapCore(string mapCodeName) {
            this.mapCodeName = mapCodeName;
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
    }
}