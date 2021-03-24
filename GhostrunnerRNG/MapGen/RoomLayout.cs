using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GhostrunnerRNG.MapGen {
    public class RoomLayout {

        private List<SpawnPlane> spawnPlanes = new List<SpawnPlane>();
        private List<Enemy> roomEnemies = new List<Enemy>();

        public RoomLayout(params Enemy[] enemies) {
            for(int i = 0; i < enemies.Length; i++) {
                roomEnemies.Add(enemies[i]);
            }
        }

        public RoomLayout(List<Enemy> enemies) {
            for(int i = 0; i < enemies.Count; i++) {
                roomEnemies.Add(enemies[i]);
            }
        }

        // bans specific enemy type to spawn within all spawnplanes of this room
        public void BanRoomEnemyType(Enemy.EnemyTypes type) {
            for(int i = 0; i < spawnPlanes.Count; i++) {
                spawnPlanes[i].BanEnemyType(type);
            }
        }


        public void Mask(List<Enemy.EnemyTypes> mask) {
            for(int i = 0; i < spawnPlanes.Count; i++) {
                spawnPlanes[i].Mask(mask);
            }
        }

        /// <summary>Flags to avoid using these spawnplanes for EnemiesWithoutCP</summary>
        public void DoNotReuse() {
            for(int i = 0; i < spawnPlanes.Count; i++) {
                spawnPlanes[i].DoNotReuse();
            }
        }

        public void FixOrbBeams(Process game) {
            for(int i = 0; i < roomEnemies.Count; i++) {
                if(roomEnemies[i] is EnemyShieldOrb) {
                    ((EnemyShieldOrb)roomEnemies[i]).FixBeams(game);
                }
            }
        }

        public List<SpawnPlane> availableSpawnPlanes;

        // returns false if enemies has ShieldOrbs
        public bool IsRoomDefaultType() {
            for(int i = 0; i < roomEnemies.Count; i++) {
                if(roomEnemies[i] is EnemyShieldOrb) return false;
            }
            return true;
        }

        public void RandomizeEnemies(Process game) {
            availableSpawnPlanes = new List<SpawnPlane>(spawnPlanes);
            availableSpawnPlanes.ForEach(x => x.ResetCurrEnemies());

            for(int i = 0; i < roomEnemies.Count; i++) {
                double rarity = (double)(Config.GetInstance().r.Next(0, 100) / 100.0);

                List<SpawnPlane> availableSpawnPlanesUpdated = availableSpawnPlanes.Where(x => x.IsEnemyAllowed(roomEnemies[i].enemyType, rarity)).ToList();

                if(availableSpawnPlanesUpdated.Count == 0) break;
                int selectedPlaneIndex = Config.GetInstance().r.Next(0, availableSpawnPlanesUpdated.Count);

                // can add enemies to that plane? 
                if(availableSpawnPlanesUpdated[selectedPlaneIndex].CanAddEnemies()) {
                    roomEnemies[i].SetMemoryPos(game, availableSpawnPlanesUpdated[selectedPlaneIndex].GetRandomSpawnData());
                    availableSpawnPlanesUpdated[selectedPlaneIndex].EnemyAdded();
                }
                // can't add anymore enemies? remove plane from available list
                if(!availableSpawnPlanesUpdated[selectedPlaneIndex].CanAddEnemies()) {
                    int indexToRemove = GetSameSpawnPlaneIndex(availableSpawnPlanes, availableSpawnPlanesUpdated[selectedPlaneIndex]);
                    if(indexToRemove > -1)
                        availableSpawnPlanes.RemoveAt(indexToRemove);
                }
            }
        }

        // since we modify a copy of the list, we want to remove the actual spawnplane when we're done with the modified element,
        // so we need to find the same spawnplane in the original list by corner coords
        public static int GetSameSpawnPlaneIndex(List<SpawnPlane> planes, SpawnPlane target) {
            for(int i = 0; i < planes.Count; i++) {
                if(planes[i].cornerA.VecEquals(target.cornerA) && planes[i].cornerB.VecEquals(target.cornerB))
                    return i;
            }
            return -1;
        }

        public void AddSpawnPlane(SpawnPlane spanwPlane) {
            spawnPlanes.Add(spanwPlane);
        }
    }
}
