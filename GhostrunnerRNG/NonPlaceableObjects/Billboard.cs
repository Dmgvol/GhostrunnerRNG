using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GhostrunnerRNG.NonPlaceableObjects {
    class Billboard : NonPlaceableObject {

        // 0x045A3C20, 0x98, firstOffset, 0x128, 0xA8, secondOffset
        public Billboard(int firstOffset, int secondOffset) {
            ObjectDP = new DeepPointer(0x04609420, 0x98, firstOffset, 0x128, 0xA8, secondOffset);

            // Pointers
            Pointers.Add("Time1", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x2C8), IntPtr.Zero));
            Pointers.Add("Angle1", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x2CC), IntPtr.Zero));
            Pointers.Add("Time2", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x2C0), IntPtr.Zero));
            Pointers.Add("Angle2", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x2D4), IntPtr.Zero));
        }

        protected override void ReadDefaultValues(Process game) {
            BillboardSpawnInfo info = new BillboardSpawnInfo();
            // read default values
            foreach(string key in info.Parameters.Keys.ToList()) {
                float value;
                game.ReadValue(Pointers[key].Item2, out value);
                info.Parameters[key] = value;
            }
            DefaultData = info;
        }

        public override void Randomize(Process game) {
            DerefPointers(game);
            // read default value
            if(DefaultData == null) ReadDefaultValues(game);

            // got any spawnInfos?
            if(spawnInfos != null && spawnInfos.Count > 0 && DefaultData is BillboardSpawnInfo defaultData) {
                // pick random and check if correct type
                double rarity = (double)(Config.GetInstance().r.Next(0, 100) / 100.0);
                // get list of same type and correct rarity
                List<BillboardSpawnInfo> spawnLst = spawnInfos.OfType<BillboardSpawnInfo>().Where(x => x.rarity >= rarity).ToList();
                if(spawnLst.Count == 0) spawnLst.Add(defaultData); // no object with minimum rarity? add default

                int i = Config.GetInstance().r.Next(spawnLst.Count);
                // change modified values only
                foreach(string key in Pointers.Keys) {
                    ModifyIfChanged(game, Pointers[key].Item2, spawnLst[i].Parameters[key], defaultData.Parameters[key].Value);
                }
            }
        }
    }

    public class BillboardSpawnInfo : SpawnInfo {
        internal Dictionary<string, float?> Parameters = new Dictionary<string, float?>() {
            {"Time1",  null},
            {"Time2",  null},
            {"Angle1",  null},
            {"Angle2",  null}
        };

        public double rarity { get; private set; } = 1;
        public BillboardSpawnInfo SetRarity(double rarity) {
            if(rarity > 1) rarity = 1;
            if(rarity < 0) rarity = 0;
            this.rarity = rarity;
            return this;
        }

        public float? Time1 { get { return Parameters["Time1"]; } set { Parameters["Time1"] = value; } }
        public float? Time2 { get { return Parameters["Time2"]; } set { Parameters["Time2"] = value; } }
        public float? Angle1 { get { return Parameters["Angle1"]; } set { Parameters["Angle1"] = value; } }
        public float? Angle2 { get { return Parameters["Angle2"]; } set { Parameters["Angle2"] = value; } }
    }
}
