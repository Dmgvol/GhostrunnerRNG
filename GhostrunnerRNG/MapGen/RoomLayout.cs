using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GhostrunnerRNG.Maps {
    public class RoomLayout {

        private List<SpawnPlane> spawnPlanes = new List<SpawnPlane>();
        private List<Enemy> roomEnemies = new List<Enemy>();

        

        public RoomLayout(List<Enemy> enemies) {
            roomEnemies = enemies;
        }

        public RoomLayout(Enemy enemy) {
            AddEnemy(enemy);
        }

        public void AddEnemy(Enemy enemy) {
            roomEnemies.Add(enemy);
        }

        public void RandomizeEnemies(Process game) {
            List<SpawnPlane> availableSpawnPlanes = new List<SpawnPlane>(spawnPlanes);
            availableSpawnPlanes.ForEach(x => x.ResetCurrEnemies());

            

            for(int i = 0; i < roomEnemies.Count; i++) {
                if(availableSpawnPlanes.Count == 0) break;

                int selectedPlaneIndex = SpawnPlane.r.Next(0, availableSpawnPlanes.Count);

                // can add enemies to that plane? 
                if(availableSpawnPlanes[selectedPlaneIndex].CanAddEnemies()) {
                    roomEnemies[i].SetMemoryPos(game, availableSpawnPlanes[selectedPlaneIndex].GetRandomSpawnData());
                    availableSpawnPlanes[selectedPlaneIndex].EnemyAdded();
                }

                // can't add anymore enemies? remove plane from available list
                if(!availableSpawnPlanes[selectedPlaneIndex].CanAddEnemies())
                    availableSpawnPlanes.RemoveAt(selectedPlaneIndex);
            }
        }

        public void AddSpawnPlane(SpawnPlane spanwPlane) {
            spawnPlanes.Add(spanwPlane);
        }
    }
}
