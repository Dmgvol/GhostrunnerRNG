using GhostrunnerRNG.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GhostrunnerRNG.Enemies {
    class EnemyDrone : Enemy{

        private DeepPointer dronePosDP;
        private IntPtr dronePosPtr;

        public EnemyDrone(Enemy enemy) : base(enemy.GetObjectDP()) {
            enemyType = EnemyTypes.Drone;
            Pos = enemy.Pos;


            // extra pos pointer
            List<int> offsets = new List<int>(ObjectDP.GetOffsets());
            offsets.RemoveAt(offsets.Count - 1);
            offsets.Add(0x280);
            offsets.Add(0x650);
            offsets.Add(0x340);
            dronePosDP = new DeepPointer(ObjectDP.GetBase(), offsets);
        }

        public void ChangePatrolPoint(Process game, DeepPointer patrolDP, Vector3f pos) {
            IntPtr patrolPtr;
            patrolDP.DerefOffsets(game, out patrolPtr);
            game.WriteBytes(patrolPtr, BitConverter.GetBytes((float)pos.X));
            game.WriteBytes(patrolPtr + 4, BitConverter.GetBytes((float)pos.Y));
            game.WriteBytes(patrolPtr + 8, BitConverter.GetBytes((float)pos.Z));
        }

        public override void SetMemoryPos(Process game, SpawnData spawnData) {
            base.SetMemoryPos(game, spawnData);

            DerefPointer(game);
            // update drone's second pos
            game.WriteBytes(dronePosPtr, BitConverter.GetBytes((float)spawnData.pos.X));
            game.WriteBytes(dronePosPtr + 4, BitConverter.GetBytes((float)spawnData.pos.Y));
            game.WriteBytes(dronePosPtr + 8, BitConverter.GetBytes((float)spawnData.pos.Z));
            // patrol points?
            if(spawnData.patrolPoints != null && spawnData.patrolPoints.Count > 0) {
                for(int i = 0; i < spawnData.patrolPoints.Count; i++) {
                    ChangePatrolPoint(game, spawnData.patrolPoints[i].Item1, spawnData.patrolPoints[i].Item2);
                }
            }
        }

        protected override void DerefPointer(Process game) {
            base.DerefPointer(game);
            // drone pos
            dronePosDP.DerefOffsets(game, out dronePosPtr);
        }
    }
}
