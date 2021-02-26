using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using System;
using System.Collections.Generic;

namespace GhostrunnerRNG.Maps {
    public class Enemy : WorldObject {

        // 3D virtual rectangle/volumes where enemy can spawn
        private List<SpawnPlane> planes = new List<SpawnPlane>();

        public Enemy(DeepPointer EnemyDP) : base(EnemyDP) { }

        public void AddPosPlane(SpawnPlane spanwPlane) {
            planes.Add(spanwPlane);
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
    }
}
