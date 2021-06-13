using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GhostrunnerRNG.MapGen {
    public class RoomLayout : IRandomize  {

        public List<SpawnPlane> spawnPlanes { get; private set; } = new List<SpawnPlane>();
        private List<WorldObject> roomObjects = new List<WorldObject>();
        private Tuple<Vector3f, Angle> AttachedCPData;

        public RoomLayout() {}

        public RoomLayout(params Enemy[] enemies) {
            for(int i = 0; i < enemies.Length; i++) {
                roomObjects.Add(enemies[i]);
            }
        }

        public void SwapEnemies(params Enemy[] enemies) {
            ClearRoomObjects();

            for(int i = 0; i < enemies.Length; i++) {
                roomObjects.Add(enemies[i]);
            }
        }
        
        public RoomLayout(params WorldObject[] objects) {
            for(int i = 0; i < objects.Length; i++) {
                roomObjects.Add(objects[i]);
            }
        }

        public RoomLayout(List<Enemy> enemies) {
            for(int i = 0; i < enemies.Count; i++) {
                roomObjects.Add(enemies[i]);
            }
        }

        public RoomLayout(List<WorldObject> objects) {
            for(int i = 0; i < objects.Count; i++) {
                roomObjects.Add(objects[i]);
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
            for(int i = 0; i < roomObjects.Count; i++) {
                if(roomObjects[i] is EnemyShieldOrb) {
                    ((EnemyShieldOrb)roomObjects[i]).FixBeams(game);
                }
            }
        }

        public List<SpawnPlane> availableSpawnPlanes;

        // returns false if enemies has ShieldOrbs
        public bool IsRoomDefaultType() {
            for(int i = 0; i < roomObjects.Count; i++) {
                if(roomObjects[i] is EnemyShieldOrb) return false;
            }
            return true;
        }

        public void RandomizeEnemies(Process game) {
            availableSpawnPlanes = new List<SpawnPlane>(spawnPlanes);
            availableSpawnPlanes.ForEach(x => x.ResetCurrEnemies());

            for(int i = 0; i < roomObjects.Count; i++) {
                
                double rarity = (double)(Config.GetInstance().r.Next(0, 100) / 100.0);
                List<SpawnPlane> availableSpawnPlanesUpdated;
                if(roomObjects[i] is Enemy enemy) {
                    availableSpawnPlanesUpdated = availableSpawnPlanes.Where(x => x.IsEnemyAllowed(enemy.enemyType, rarity)).ToList();
                } else {
                    availableSpawnPlanesUpdated = availableSpawnPlanes.Where(x => x.IsAllowed(rarity)).ToList();
                }
                if(availableSpawnPlanesUpdated.Count == 0) break;
                int selectedPlaneIndex = Config.GetInstance().r.Next(0, availableSpawnPlanesUpdated.Count);

                // can add enemies to that plane? 
                if(availableSpawnPlanesUpdated[selectedPlaneIndex].CanAddEnemies()) {
                    roomObjects[i].SetMemoryPos(game, availableSpawnPlanesUpdated[selectedPlaneIndex].GetRandomSpawnData());
                    availableSpawnPlanesUpdated[selectedPlaneIndex].EnemyAdded(roomObjects[i].Pos);
                }
                // can't add anymore enemies? remove plane from available list
                if(!availableSpawnPlanesUpdated[selectedPlaneIndex].CanAddEnemies()) {
                    int indexToRemove = GetSameSpawnPlaneIndex(availableSpawnPlanes, availableSpawnPlanesUpdated[selectedPlaneIndex]);
                    if(indexToRemove > -1)
                        availableSpawnPlanes.RemoveAt(indexToRemove);
                }
            }

            // Need to modify cp? find parent and change
            if(AttachedCPData != null) {
                var dp = roomObjects.OfType<Enemy>()?.Last()?.GetObjectDP();
                List<int> offsets = new List<int>(dp.GetOffsets());
                offsets[offsets.Count - 1] = 0x5D0;
                offsets.Add(0x280);
                offsets.Add(0x248);
                offsets.Add(0x1D0);
                DeepPointer parentDP = new DeepPointer(dp.GetBase(), offsets);
                IntPtr parentPtr;
                parentDP.DerefOffsets(game, out parentPtr);
                //  pos
                game.WriteValue(parentPtr, AttachedCPData.Item1.X);
                game.WriteValue(parentPtr + 4, AttachedCPData.Item1.Y);
                game.WriteValue(parentPtr + 8, AttachedCPData.Item1.Z);
                // angle
                game.WriteValue(parentPtr - 8, AttachedCPData.Item2.angleSin);
                game.WriteValue(parentPtr - 4, AttachedCPData.Item2.angleCos);
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

        public void ModifyAttachedCP(Vector3f vec, Angle angle) {
            AttachedCPData = new Tuple<Vector3f, Angle>(vec, angle);
        }

        public void ClearRoomObjects() => roomObjects.Clear();
    }
}
