using GhostrunnerRNG.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GhostrunnerRNG.Maps {
    public class Enemy {

        // pointers
        private DeepPointer EnemyDP;
        public IntPtr EnemyPtr;
        // 3D virtual planes when enemy can spawn
        private List<SpawnPlane> planes = new List<SpawnPlane>();
        // default pos
        private Vector3f defaultPos = Vector3f.Empty;
        

        public Enemy(DeepPointer DP) {
            EnemyDP = DP;
        }

        public Enemy(DeepPointer DP, Vector3f defaultPos) : this(DP) {
            this.defaultPos = defaultPos;
        }

        public Vector3f GetMemoryPos(Process game) {
            DerefPointer(game);
            float x;
            game.ReadValue<float>(EnemyPtr, out x);
            float y;
            game.ReadValue<float>(EnemyPtr + 4, out y);
            float z;
            game.ReadValue<float>(EnemyPtr + 8, out z);
            return new Vector3f(x, y, z);
        }

        public void SetMemoryPos(Process game, SpawnData spawn) {
            if(spawn.pos.IsEmpty()) return;

            DerefPointer(game);
            // XYZ coords
            game.WriteBytes(EnemyPtr, BitConverter.GetBytes((float)spawn.pos.X));
            game.WriteBytes(EnemyPtr + 4, BitConverter.GetBytes((float)spawn.pos.Y));
            game.WriteBytes(EnemyPtr + 8, BitConverter.GetBytes((float)spawn.pos.Z));

            // angle orientation
            if(spawn.HasAngle()) {
                game.WriteBytes(EnemyPtr - 8, BitConverter.GetBytes((float)spawn.angle.Value.angleSin));
                game.WriteBytes(EnemyPtr - 4, BitConverter.GetBytes((float)spawn.angle.Value.angleCos));
            }
        }

        public void DerefPointer(Process game) {
            EnemyDP.DerefOffsets(game, out EnemyPtr);
        }

        public void AddPosPlane(SpawnPlane spanwPlane) {
            planes.Add(spanwPlane);
        }

        // single pos, default angle
        public void AddPosPlane(Vector3f pointA) {
            planes.Add(new SpawnPlane(pointA));
        }

        // get random spawndata
        public SpawnData GetSpawnData() {
            if(planes.Count > 0) {
                int plane = SpawnPlane.r.Next(0, planes.Count);
                return planes[plane].GetRandomSpawnData();
            }
            return new SpawnData(defaultPos);
        }
    }
}
