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
            Pointers.Add("AngleToDestoryFromFace", new Tuple<DeepPointer, IntPtr>(AppendBaseLastOffset(0x88C), IntPtr.Zero));
            Pointers.Add("VerticalAngle", new Tuple<DeepPointer, IntPtr>(AppendBaseLastOffset(0x6A4), IntPtr.Zero));
            Pointers.Add("HorizontalAngle", new Tuple<DeepPointer, IntPtr>(AppendBaseLastOffset(0x6A0), IntPtr.Zero));
            Pointers.Add("HorizontalSpeed", new Tuple<DeepPointer, IntPtr>(AppendBaseLastOffset(0x69C), IntPtr.Zero));
            Pointers.Add("HorizontalDetectionAngle", new Tuple<DeepPointer, IntPtr>(AppendBaseLastOffset(0x6B0), IntPtr.Zero));
            Pointers.Add("VerticalDetectionAngle", new Tuple<DeepPointer, IntPtr>(AppendBaseLastOffset(0x6B4), IntPtr.Zero));
            Pointers.Add("VisibleLaserLength", new Tuple<DeepPointer, IntPtr>(AppendBaseLastOffset(0x6B8), IntPtr.Zero));
            Pointers.Add("RotationOffset", new Tuple<DeepPointer, IntPtr>(AppendBaseLastOffset(0x6A8), IntPtr.Zero));
            Pointers.Add("HorizontalAngleSmooth", new Tuple<DeepPointer, IntPtr>(AppendBaseLastOffset(0x6EC), IntPtr.Zero));
            Pointers.Add("MaxAttackRange", new Tuple<DeepPointer, IntPtr>(AppendBaseLastOffset(0xE0, 0x2E8, 0xB0, 0x0, 0x50), IntPtr.Zero));
            Pointers.Add("MaxAttackRange2", new Tuple<DeepPointer, IntPtr>(AppendBaseLastOffset(0xE0, 0x2E8, 0xB0, 0x0, 0x54), IntPtr.Zero));
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
            game.ReadValue(Pointers["MaxAttackRange"].Item2, out value);
            DefaultData.MaxAttackRange = value;
            game.ReadValue(Pointers["MaxAttackRange2"].Item2, out value);
            DefaultData.MaxAttackRange2 = value;
        }

        public override void SetMemoryPos(Process game, SpawnData spawnData) {
            DerefPointer(game);

            // first write? read default values
            if(DefaultData == null) {
                ReadDefaultValues(game);
            }

            base.SetMemoryPos(game, spawnData);

            // SpawnData contains SpawnInfo of type Turret?
            if(spawnData.spawnInfo != null && spawnData.spawnInfo is TurretSpawnInfo info) {
                ModifyIfChanged(game, Pointers["AngleToDestoryFromFace"].Item2, ref info.AngleToDestoryFromFace, DefaultData.AngleToDestoryFromFace);
                ModifyIfChanged(game, Pointers["HorizontalAngle"].Item2, ref info.HorizontalAngle, DefaultData.HorizontalAngle);
                ModifyIfChanged(game, Pointers["HorizontalSpeed"].Item2, ref info.HorizontalSpeed, DefaultData.HorizontalSpeed);
                ModifyIfChanged(game, Pointers["HorizontalDetectionAngle"].Item2, ref info.HorizontalDetectionAngle, DefaultData.HorizontalDetectionAngle);
                ModifyIfChanged(game, Pointers["VerticalAngle"].Item2, ref info.VerticalAngle, DefaultData.VerticalAngle);
                ModifyIfChanged(game, Pointers["VerticalDetectionAngle"].Item2, ref info.VerticalDetectionAngle, DefaultData.VerticalDetectionAngle);
                ModifyIfChanged(game, Pointers["VisibleLaserLength"].Item2, ref info.VisibleLaserLength, DefaultData.VisibleLaserLength);
                ModifyIfChanged(game, Pointers["RotationOffset"].Item2, ref info.RotationOffset, DefaultData.RotationOffset);

                if(info.SetRangeAsVisible) {
                    game.WriteBytes(Pointers["MaxAttackRange"].Item2, BitConverter.GetBytes((float)DefaultData.VisibleLaserLength));
                    game.WriteBytes(Pointers["MaxAttackRange2"].Item2, BitConverter.GetBytes((float)DefaultData.VisibleLaserLength));
                } else {
                    ModifyIfChanged(game, Pointers["MaxAttackRange"].Item2, ref info.MaxAttackRange, DefaultData.MaxAttackRange);
                    ModifyIfChanged(game, Pointers["MaxAttackRange2"].Item2, ref info.MaxAttackRange2, DefaultData.MaxAttackRange2);
                }

                if(info.HorizontalAngle is float angle && angle != 0) {
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
    }

    public class TurretSpawnInfo : SpawnInfo {
        public float? VerticalAngle;
        public float? AngleToDestoryFromFace; // 270 default
        public float? HorizontalAngle; //+- of head move
        public float? HorizontalSpeed;
        public float? HorizontalDetectionAngle; // 15 default
        public float? VerticalDetectionAngle; // 15 default
        public float? VisibleLaserLength; // 8000 default
        public float? RotationOffset; // 
        public float? HorizontalAngleSmooth; // corner smoothing value
        public float? MaxAttackRange; // max attack/detection range
        public float? MaxAttackRange2; // max attack/detection range

        public const float DefaultRange = 8000;

        public void SetRange(float range) {
            VisibleLaserLength = range;
            MaxAttackRange = range;
            MaxAttackRange2 = range;
            if(HorizontalAngle == null || (HorizontalAngle != null & HorizontalAngle == 0))
            HorizontalAngle = 0.001f; // fix for static turrets
        }

        public bool SetRangeAsVisible = false;
    }
}