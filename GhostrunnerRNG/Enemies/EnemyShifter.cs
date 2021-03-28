using GhostrunnerRNG.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GhostrunnerRNG.Enemies {
    class EnemyShifter : Enemy {

        List<Tuple<DeepPointer, IntPtr>> ShiftPointers = new List<Tuple<DeepPointer, IntPtr>>();

        private List<ShifterSpawnInfo> spawnInfos;
      
        public EnemyShifter(Enemy enemy, int shiftPoints) : base(enemy.GetObjectDP()) {
            Pos = enemy.Pos;

            // add pointers
            for(int i = 0; i < shiftPoints; i++) {
                ShiftPointers.Add(new Tuple<DeepPointer, IntPtr>(AppendBaseLastOffset(0x7d0, 0x8 * i, 0x130, 0x1D0) ,IntPtr.Zero));
            }
        }

        /// <summary>
        /// Fixed list of shiftpoints, doesn't matter which plane shifter spawns - it will use one of the fixed spawn info(list)
        /// </summary>
        /// <param name="info">ShifterSpawnInfo, all n ShiftPoints</param>
        public Enemy AddFixedSpawnInfo(ShifterSpawnInfo info) {
            if(spawnInfos == null) spawnInfos = new List<ShifterSpawnInfo>();
            spawnInfos.Add(info);
            return this;
        }

        public override void SetMemoryPos(Process game, SpawnData spawnData) {
            base.SetMemoryPos(game, spawnData);

            // Update shift positions with spawndata/spawninfo, if any
            if(spawnInfos != null && spawnInfos.Count > 0) {
                int i = Config.GetInstance().r.Next(spawnInfos.Count);
                for(int j = 0; j < ShiftPointers.Count; j++) {
                    game.WriteBytes(ShiftPointers[j].Item2, BitConverter.GetBytes((float)spawnInfos[i].shiftPoints[j].Item1.X));
                    game.WriteBytes(ShiftPointers[j].Item2 + 4, BitConverter.GetBytes((float)spawnInfos[i].shiftPoints[j].Item1.Y));
                    game.WriteBytes(ShiftPointers[j].Item2 + 8, BitConverter.GetBytes((float)spawnInfos[i].shiftPoints[j].Item1.Z));
                    game.WriteBytes(ShiftPointers[j].Item2 - 8, BitConverter.GetBytes((float)spawnInfos[i].shiftPoints[j].Item2.angleSin));
                    game.WriteBytes(ShiftPointers[j].Item2 - 4, BitConverter.GetBytes((float)spawnInfos[i].shiftPoints[j].Item2.angleCos));
                }

            } else if(spawnData.spawnInfo != null && spawnData.spawnInfo is ShifterSpawnInfo info && info.shiftPoints != null && ShiftPointers.Count == info.shiftPoints.Count) {
                for(int i = 0; i < info.shiftPoints.Count; i++) {
                    game.WriteBytes(ShiftPointers[i].Item2, BitConverter.GetBytes((float)info.shiftPoints[i].Item1.X));
                    game.WriteBytes(ShiftPointers[i].Item2 + 4, BitConverter.GetBytes((float)info.shiftPoints[i].Item1.Y));
                    game.WriteBytes(ShiftPointers[i].Item2 + 8, BitConverter.GetBytes((float)info.shiftPoints[i].Item1.Z));
                    game.WriteBytes(ShiftPointers[i].Item2 - 8, BitConverter.GetBytes((float)info.shiftPoints[i].Item2.angleSin));
                    game.WriteBytes(ShiftPointers[i].Item2 - 4, BitConverter.GetBytes((float)info.shiftPoints[i].Item2.angleCos));
                }
            }
        }

        protected override void DerefPointer(Process game) {
            base.DerefPointer(game);

            // Deref ShiftPointers
            var lst = new List<Tuple<DeepPointer, IntPtr>>();
            for(int i = 0; i < ShiftPointers.Count; i++) {
                IntPtr ptr;
                ShiftPointers[i].Item1.DerefOffsets(game, out ptr);
                lst.Add(new Tuple<DeepPointer, IntPtr>(ShiftPointers[i].Item1, ptr));
            }
            ShiftPointers = lst;
        }
    }

    public class ShifterSpawnInfo : SpawnInfo {
        public List<Tuple<Vector3f, Angle>> shiftPoints;
    }
}
