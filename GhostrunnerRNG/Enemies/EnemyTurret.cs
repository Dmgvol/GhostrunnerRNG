using GhostrunnerRNG.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GhostrunnerRNG.Enemies {
    public class EnemyTurret : Enemy {

        private Dictionary<string, Tuple<DeepPointer, IntPtr>> Pointers = new Dictionary<string, Tuple<DeepPointer, IntPtr>>();

        private TurretSpawnInfo DefaultData;

        public enum TurretOrientation { Normal, WallLeft, WallRight, Ceiling }

        public EnemyTurret(Enemy enemy) : base(enemy.GetObjectDP()) {
            enemyType = EnemyTypes.Turret;
            Pos = enemy.Pos;
            // add pointers
            Pointers.Add("AngleToDestoryFromFace", new Tuple<DeepPointer, IntPtr>(AppendBaseLastOffset(0x884), IntPtr.Zero));
            Pointers.Add("VerticalAngle", new Tuple<DeepPointer, IntPtr>(AppendBaseLastOffset(0x69C), IntPtr.Zero));
            Pointers.Add("HorizontalAngle", new Tuple<DeepPointer, IntPtr>(AppendBaseLastOffset(0x698), IntPtr.Zero));
            Pointers.Add("HorizontalSpeed", new Tuple<DeepPointer, IntPtr>(AppendBaseLastOffset(0x694), IntPtr.Zero));
            Pointers.Add("HorizontalDetectionAngle", new Tuple<DeepPointer, IntPtr>(AppendBaseLastOffset(0x6A8), IntPtr.Zero));
            Pointers.Add("VerticalDetectionAngle", new Tuple<DeepPointer, IntPtr>(AppendBaseLastOffset(0x6AC), IntPtr.Zero));
            Pointers.Add("VisibleLaserLength", new Tuple<DeepPointer, IntPtr>(AppendBaseLastOffset(0x6B0), IntPtr.Zero));
            Pointers.Add("RotationOffset", new Tuple<DeepPointer, IntPtr>(AppendBaseLastOffset(0x6A0), IntPtr.Zero));
            Pointers.Add("HorizontalAngleSmooth", new Tuple<DeepPointer, IntPtr>(AppendBaseLastOffset(0x6E4), IntPtr.Zero));
        }

        private DeepPointer AppendBaseLastOffset(params int[] appendOffsets) {
            List<int> offsets = new List<int>(ObjectDP.GetOffsets());
            offsets.RemoveAt(offsets.Count - 1); // removes last offset
            offsets.AddRange(appendOffsets); // add new offsets
            return new DeepPointer(ObjectDP.GetBase(), new List<int>(offsets));
        }

        protected override void DerefPointer(Process game) {
            base.DerefPointer(game);

            // deref all pointer dictionary
            foreach(KeyValuePair<string, Tuple<DeepPointer, IntPtr>> item in new Dictionary<string, Tuple<DeepPointer, IntPtr>>(Pointers)) {
                IntPtr ptr;
                item.Value.Item1.DerefOffsets(game, out ptr);
                Pointers[item.Key] = new Tuple<DeepPointer, IntPtr>(item.Value.Item1, ptr);
            }
        }

        public void ReadDefaultValues(Process game) {
            DefaultData = new TurretSpawnInfo();

            float value;
            game.ReadValue(Pointers["AngleToDestoryFromFace"].Item2, out value);
            DefaultData.AngleToDestoryFromFace = value;
            game.ReadValue(Pointers["HorizontalAngle"].Item2, out value);
            DefaultData.HorizontalAngle = value;
            game.ReadValue(Pointers["HorizontalSpeed"].Item2, out value);
            DefaultData.HorizontalSpeed = value;
            game.ReadValue(Pointers["HorizontalDetectionAngle"].Item2, out value);
            DefaultData.HorizontalDetectionAngle = value;
            game.ReadValue(Pointers["VerticalAngle"].Item2, out value);
            DefaultData.VerticalAngle = value;
            game.ReadValue(Pointers["VisibleLaserLength"].Item2, out value);
            DefaultData.VisibleLaserLength = value;
            game.ReadValue(Pointers["RotationOffset"].Item2, out value);
            DefaultData.RotationOffset = value;
            game.ReadValue(Pointers["HorizontalAngleSmooth"].Item2, out value);
            DefaultData.HorizontalAngleSmooth = value;
        }

        public override void SetMemoryPos(Process game, SpawnData spawnData) {
            DerefPointer(game);

            // first write? read default values
            if(DefaultData == null) {
                ReadDefaultValues(game);
            }

            base.SetMemoryPos(game, spawnData);

            // SpawnData contains SpawnInfo of type Turret?
            if(spawnData.spawnInfo != null && spawnData.spawnInfo is TurretSpawnInfo) {
                ModifyIfChanged(game, Pointers["AngleToDestoryFromFace"].Item2, ref ((TurretSpawnInfo)spawnData.spawnInfo).AngleToDestoryFromFace, DefaultData.AngleToDestoryFromFace);
                ModifyIfChanged(game, Pointers["HorizontalAngle"].Item2, ref ((TurretSpawnInfo)spawnData.spawnInfo).HorizontalAngle, DefaultData.HorizontalAngle);
                ModifyIfChanged(game, Pointers["HorizontalSpeed"].Item2, ref ((TurretSpawnInfo)spawnData.spawnInfo).HorizontalSpeed, DefaultData.HorizontalSpeed);
                ModifyIfChanged(game, Pointers["HorizontalDetectionAngle"].Item2, ref ((TurretSpawnInfo)spawnData.spawnInfo).HorizontalDetectionAngle, DefaultData.HorizontalDetectionAngle);
                ModifyIfChanged(game, Pointers["VerticalAngle"].Item2, ref ((TurretSpawnInfo)spawnData.spawnInfo).VerticalAngle, DefaultData.VerticalAngle);
                ModifyIfChanged(game, Pointers["VerticalDetectionAngle"].Item2, ref ((TurretSpawnInfo)spawnData.spawnInfo).VerticalDetectionAngle, DefaultData.VerticalDetectionAngle);
                ModifyIfChanged(game, Pointers["VisibleLaserLength"].Item2, ref ((TurretSpawnInfo)spawnData.spawnInfo).VisibleLaserLength, DefaultData.VisibleLaserLength);
                ModifyIfChanged(game, Pointers["RotationOffset"].Item2, ref ((TurretSpawnInfo)spawnData.spawnInfo).RotationOffset, DefaultData.RotationOffset);

                if(((TurretSpawnInfo)spawnData.spawnInfo).HorizontalAngle is float angle && angle != 0) {
                    game.WriteBytes(Pointers["HorizontalAngleSmooth"].Item2, BitConverter.GetBytes((float)1.0f / angle));
                }
            }
        }

        private void ModifyIfChanged(Process game, IntPtr ptr, ref float? n, float? defaultValue) {
            // values are different? update the change
            if(n != null && defaultValue != null && (float)n != (float)defaultValue) {
                game.WriteBytes(ptr, BitConverter.GetBytes((float)n));

                // new value is null or not set/changed? and we have default value? update to default
            } else if(defaultValue != null) {
                game.WriteBytes(ptr, BitConverter.GetBytes((float)defaultValue));
            }
        }

        // force update (if not null)
        private void ModifyIfChanged(Process game, IntPtr ptr, ref float? n) {
            if(n != null) game.WriteBytes(ptr, BitConverter.GetBytes((float)n));
        }
    }
}