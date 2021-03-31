using GhostrunnerRNG.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GhostrunnerRNG.NonPlaceableObjects {
    class UplinkSlowmo : NonPlaceableObject {

        public UplinkSlowmo(int firstOffset, int secondOffset) {
            ObjectDP = new DeepPointer(0x045A3C20, 0x98, firstOffset, 0x128, 0xA8, 0x9E8, 0x270, secondOffset);

            // add pointers
            Pointers.Add("TimeMultiplier", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x8), IntPtr.Zero));
            Pointers.Add("MidCurveTime", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x20), IntPtr.Zero));
            Pointers.Add("TotalTime", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x3C), IntPtr.Zero));
        }

        public override void Randomize(Process game) {
            DerefPointers(game);
            // read default value
            if(DefaultData == null) ReadDefaultValues(game);

            // got any spawnInfo?
            if(spawnInfos != null && spawnInfos.Count > 0 && DefaultData is UplinkSlowmoSpawnInfo defaultData) {
                // get list of same type
                List<UplinkSlowmoSpawnInfo> spawnLst = spawnInfos.OfType<UplinkSlowmoSpawnInfo>().ToList();

                int i = Config.GetInstance().r.Next(spawnLst.Count);
                // change modified values only
                foreach(string key in Pointers.Keys) {
                    ModifyIfChanged(game, Pointers[key].Item2, spawnLst[i].Parameters[key], defaultData.Parameters[key].Value);
                }
            }
        }

        protected override void ReadDefaultValues(Process game) {
            UplinkSlowmoSpawnInfo info = new UplinkSlowmoSpawnInfo();
            info.TimeMultiplier = 0.1f; // game default, no need to read
            // read default TotalTime for this uplink
            float value;
            game.ReadValue(Pointers["TotalTime"].Item2, out value);
            info.Parameters["TotalTime"] = value;

            DefaultData = info;
        }
    }

    public class UplinkSlowmoSpawnInfo : SpawnInfo {
        internal Dictionary<string, float?> Parameters = new Dictionary<string, float?>() {
            { "TimeMultiplier", null},
            { "MidCurveTime", null},
            { "TotalTime", null}
        };

        public float? TimeMultiplier { get { return Parameters["TimeMultiplier"]; }  
            set { 
                Parameters["TimeMultiplier"] = value; 
                Parameters["MidCurveTime"] = value - 0.5f; // mid curve is -0.5 from TotalTime
            } }

        public float? TotalTime { get { return Parameters["TotalTime"]; } set { Parameters["TotalTime"] = value; } }
    }
}
