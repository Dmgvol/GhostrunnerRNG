using GhostrunnerRNG.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GhostrunnerRNG.MapGen {
    public class Enemy : WorldObject {

        // 3D virtual rectangle/volumes where enemy can spawn
        private List<SpawnPlane> planes = new List<SpawnPlane>();

        public Enemy(DeepPointer EnemyDP) : base(EnemyDP) { }

        public enum EnemyTypes { Default, Waver, Drone, ShieldOrb }
        public EnemyTypes enemyType { get; protected set; } = EnemyTypes.Default;

        public void AddPosPlane(SpawnPlane spanwPlane) {
            planes.Add(spanwPlane);
        }

        public void SetEnemyType(EnemyTypes enemyType) {
            this.enemyType = enemyType;
        }

        // get random spawndata
        public SpawnData GetSpawnData() {
            if(planes == null || planes.Count == 0) throw new IndexOutOfRangeException();

            int plane = SpawnPlane.r.Next(0, planes.Count);
            return planes[plane].GetRandomSpawnData();
        }

        // clear all planes
        internal void ClearAllPlanes() {
            planes.Clear();
        }

        public void DisableAttachedCP(Process game) {
            List<int> offsets = new List<int>(ObjectDP.GetOffsets());
            offsets[offsets.Count - 1] = 0x5D0;
            DeepPointer parentDP = new DeepPointer(ObjectDP.GetBase(), offsets);
            IntPtr parentPtr;
            parentDP.DerefOffsets(game, out parentPtr);
            game.WriteBytes(parentPtr, new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 });
        }
    }
}
