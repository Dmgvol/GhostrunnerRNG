using GhostrunnerRNG.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GhostrunnerRNG.Enemies {
    class EnemySniper : Enemy {
        // Sniper Aim patrol points
        private List<SniperPatrol> patrolPoints = new List<SniperPatrol>();

        // Focus point, usually behind/around the sniper
        private List<SniperPatrol> focusPoints = new List<SniperPatrol>();

        public EnemySniper(Enemy enemy) : base(enemy.GetObjectDP()) {
            enemyType = EnemyTypes.Sniper;
            Pos = enemy.Pos;
        }

        protected override void DerefPointer(Process game) {
            base.DerefPointer(game);

            // Deref patrol pointers
            for(int i = 0; i < patrolPoints.Count; i++) {
                IntPtr posPtr, delayPtr;
                patrolPoints[i].DP_Pos.DerefOffsets(game, out posPtr);
                patrolPoints[i].DP_Delay.DerefOffsets(game, out delayPtr);
                // update struct
                patrolPoints[i] = new SniperPatrol().UpdatePointers(patrolPoints[i], posPtr, delayPtr);
            }

            // Deref focus pointers
            for(int i = 0; i < focusPoints.Count; i++) {
                IntPtr posPtr, delayPtr;
                focusPoints[i].DP_Pos.DerefOffsets(game, out posPtr);
                focusPoints[i].DP_Delay.DerefOffsets(game, out delayPtr);
                // update struct
                focusPoints[i] = new SniperPatrol().UpdatePointers(focusPoints[i], posPtr, delayPtr);
            }
        }

        public override void SetMemoryPos(Process game, SpawnData spawnData) {
            // SpawnData contain sniper spawn data?
            if(spawnData.spawnInfo != null && spawnData.spawnInfo is SniperSpawnInfo) {
                patrolPoints.Clear();
                focusPoints.Clear();
                for(int i = 0; i < ((SniperSpawnInfo)spawnData.spawnInfo).patrolPoints.Count; i++) {
                    AddPatrolPoint(((SniperSpawnInfo)spawnData.spawnInfo).patrolPoints[i].Item1, ((SniperSpawnInfo)spawnData.spawnInfo).patrolPoints[i].Item2);
                }

                for(int i = 0; i < ((SniperSpawnInfo)spawnData.spawnInfo).focusPoints.Count; i++) {
                    AddFocusPoint(((SniperSpawnInfo)spawnData.spawnInfo).focusPoints[i].Item1, ((SniperSpawnInfo)spawnData.spawnInfo).focusPoints[i].Item2);
                }
            }

            DerefPointer(game);
            base.SetMemoryPos(game, spawnData);

            for(int i = 0; i < patrolPoints.Count; i++) {
                // patrol pos
                game.WriteBytes(patrolPoints[i].PosPtr, BitConverter.GetBytes((float)patrolPoints[i].Pos.X));
                game.WriteBytes(patrolPoints[i].PosPtr + 4, BitConverter.GetBytes((float)patrolPoints[i].Pos.Y));
                game.WriteBytes(patrolPoints[i].PosPtr + 8, BitConverter.GetBytes((float)patrolPoints[i].Pos.Z));
                // patrol delay
                game.WriteBytes(patrolPoints[i].DelayPtr, BitConverter.GetBytes((float)patrolPoints[i].Delay));
            }

            for(int i = 0; i < focusPoints.Count; i++) {
                // read original first
                float x, y, z;
                game.ReadValue<float>(focusPoints[i].PosPtr, out x);
                game.ReadValue<float>(focusPoints[i].PosPtr + 4, out y);
                game.ReadValue<float>(focusPoints[i].PosPtr + 8, out z);
                Console.WriteLine(new Vector3f(x,y,z));


                // patrol pos
                game.WriteBytes(focusPoints[i].PosPtr, BitConverter.GetBytes((float)focusPoints[i].Pos.X));
                game.WriteBytes(focusPoints[i].PosPtr + 4, BitConverter.GetBytes((float)focusPoints[i].Pos.Y));
                game.WriteBytes(focusPoints[i].PosPtr + 8, BitConverter.GetBytes((float)focusPoints[i].Pos.Z));
                // patrol delay
                game.WriteBytes(focusPoints[i].DelayPtr, BitConverter.GetBytes((float)focusPoints[i].Delay));
            }
        }

        private void AddPatrolPoint(Vector3f pos, float delay=.2f) {
            List<int> offsets = new List<int>(ObjectDP.GetOffsets());
            SniperPatrol patrol;

            // patrol DP
            offsets.RemoveAt(offsets.Count - 1);
            offsets.Add(0x808);
            if(patrolPoints.Count == 0) {
                offsets.Add(0x0);
            } else {
                offsets.Add(0x8 * patrolPoints.Count);
            }

            offsets.Add(0x130);
            offsets.Add(0x1D0);
            patrol.DP_Pos = new DeepPointer(ObjectDP.GetBase(), new List<int>(offsets));

            // delay DP
            offsets = new List<int>(ObjectDP.GetOffsets());
            offsets.RemoveAt(offsets.Count - 1);
            offsets.Add(0x808);
            if(patrolPoints.Count == 0) {
                offsets.Add(0x0);
            } else {
                offsets.Add(0x8 * patrolPoints.Count);
            }
            offsets.Add(0x248);
            patrol.DP_Delay = new DeepPointer(ObjectDP.GetBase(), new List<int>(offsets));


            // pos and delay values
            patrol.Delay = delay;
            patrol.Pos = pos;
            patrol.PosPtr = IntPtr.Zero;
            patrol.DelayPtr = IntPtr.Zero;

            patrolPoints.Add(patrol);
        }

        private void AddFocusPoint(Vector3f pos, float delay = .2f) {
            SniperPatrol patrol;
            List<int> offsets;
            // focus DP
            offsets = new List<int>(ObjectDP.GetOffsets());
            offsets.RemoveAt(offsets.Count - 1);
            offsets.Add(0x818);
            if(focusPoints.Count == 0) {
                offsets.Add(0x0);
            } else {
                offsets.Add(0x8 * focusPoints.Count);
            }
            offsets.Add(0x130);
            offsets.Add(0x1D0);
            patrol.DP_Pos = new DeepPointer(ObjectDP.GetBase(), new List<int>(offsets));

            // focus delay DP
            offsets = new List<int>(ObjectDP.GetOffsets());
            offsets.RemoveAt(offsets.Count - 1);
            offsets.Add(0x818);
            if(focusPoints.Count == 0) {
                offsets.Add(0x0);
            } else {
                offsets.Add(0x8 * focusPoints.Count);
            }
            offsets.Add(0x248);
            patrol.DP_Delay = new DeepPointer(ObjectDP.GetBase(), new List<int>(offsets));

            // pos and delay values
            patrol.Delay = delay;
            patrol.Pos = pos;
            patrol.PosPtr = IntPtr.Zero;
            patrol.DelayPtr = IntPtr.Zero;

            focusPoints.Add(patrol);
        }

        private struct SniperPatrol {
            public DeepPointer DP_Pos, DP_Delay;
            public IntPtr PosPtr, DelayPtr;
            public Vector3f Pos;
            public float Delay;

            // Update Pointers
           public SniperPatrol UpdatePointers(SniperPatrol patrol, IntPtr PosPtr, IntPtr DelayPtr) {
                SniperPatrol newPatrol = new SniperPatrol();
                // Copy deep pointers
                newPatrol.DP_Pos = patrol.DP_Pos;
                newPatrol.DP_Delay = patrol.DP_Delay;
                // asign updated int pointers
                newPatrol.PosPtr = PosPtr;
                newPatrol.DelayPtr = DelayPtr;
                // copy values
                newPatrol.Pos = patrol.Pos;
                newPatrol.Delay = patrol.Delay;
                return newPatrol;
            }
        }
    }
}
