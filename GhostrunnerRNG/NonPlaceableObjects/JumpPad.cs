using GhostrunnerRNG.Game;
using GhostrunnerRNG.MemoryUtils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GhostrunnerRNG.NonPlaceableObjects {
    class JumpPad : NonPlaceableObject {

        public JumpPad(int offset) {
            ObjectDP = new DeepPointer(PtrDB.DP_JumpPad).ModifyOffset(2, offset);

            // Pointers
            Pointers.Add("Speed", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x248), IntPtr.Zero));
            Pointers.Add("Rotation", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x230, 0x1C0), IntPtr.Zero));
        }

        public override void Randomize(Process game) {
            DerefPointers(game);
            // read default value
            if(DefaultData == null) ReadDefaultValues(game);

            // got any spawninfos?
            if(spawnInfos != null && spawnInfos.Count > 0 && DefaultData is JumpPadSpawnInfo defaultInfo) {
                // pick random and check if correct type
                double rarity = (double)(Config.GetInstance().r.Next(0, 100) / 100.0);

                List<JumpPadSpawnInfo> spawnLst = spawnInfos.OfType<JumpPadSpawnInfo>().Where(x => x.rarity >= rarity).ToList();
                if(spawnLst.Count == 0) spawnLst.Add(defaultInfo); // no object with minimum rarity? add default

                // pick random and check if correct type
                int i = Config.GetInstance().r.Next(spawnLst.Count);
                // change modified values only
                ModifyIfChanged(game, Pointers["Speed"].Item2, spawnLst[i].Speed, defaultInfo.Speed);
                ModifyIfChanged(game, Pointers["Rotation"].Item2, spawnLst[i].Angle?.quaternion.x, defaultInfo.Angle.quaternion.x);
                ModifyIfChanged(game, Pointers["Rotation"].Item2 + 4, spawnLst[i].Angle?.quaternion.y, defaultInfo.Angle.quaternion.y);
                ModifyIfChanged(game, Pointers["Rotation"].Item2 + 8, spawnLst[i].Angle?.quaternion.z, defaultInfo.Angle.quaternion.z);
                ModifyIfChanged(game, Pointers["Rotation"].Item2 + 12, spawnLst[i].Angle?.quaternion.w, defaultInfo.Angle.quaternion.w);
            }
        }

        protected override void ReadDefaultValues(Process game) {
            JumpPadSpawnInfo info = new JumpPadSpawnInfo();
            // speed
            float value;
            game.ReadValue(Pointers["Speed"].Item2, out value);
            info.Speed = value;
            // rotation
            float x, y, z, w;
            game.ReadValue(Pointers["Rotation"].Item2, out x);
            game.ReadValue(Pointers["Rotation"].Item2 + 4, out y);
            game.ReadValue(Pointers["Rotation"].Item2 + 8, out z);
            game.ReadValue(Pointers["Rotation"].Item2 + 12, out w);
            info.Angle = new QuaternionAngle(x, y, z, w);
            // set data
            DefaultData = info;
        }
    }

    public class JumpPadSpawnInfo : SpawnInfo {
        public float? Speed;
        public QuaternionAngle Angle;
        public double rarity { get; private set; } = 1;
        public JumpPadSpawnInfo SetRarity(double rarity) {
            if(rarity > 1) rarity = 1;
            if(rarity < 0) rarity = 0;
            this.rarity = rarity;
            return this;
        }
    }
}
