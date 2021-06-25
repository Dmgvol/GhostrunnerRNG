using GhostrunnerRNG.Game;
using GhostrunnerRNG.MemoryUtils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GhostrunnerRNG.NonPlaceableObjects {
    class UplinkSlowmo : NonPlaceableObject {

        public UplinkSlowmo(int firstOffset, int secondOffset, int thirdOffet) {
            ObjectDP = new DeepPointer(PtrDB.DP_UplinkSlowmo).ModifyOffset(1, firstOffset).ModifyOffset(4, secondOffset).ModifyOffset(6, thirdOffet);

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
            game.ReadValue(Pointers["MidCurveTime"].Item2, out value);
            info.Parameters["MidCurveTime"] = value;
            DefaultData = info;
        }
    }

    public class UplinkSlowmoSpawnInfo : SpawnInfo {
        internal Dictionary<string, float?> Parameters = new Dictionary<string, float?>() {
            { "TimeMultiplier", null},
            { "MidCurveTime", null},
            { "TotalTime", null}
        };

        public float? TimeMultiplier { get { return Parameters["TimeMultiplier"]; } set { Parameters["TimeMultiplier"] = value;  } }

        public float? TotalTime { get { return Parameters["TotalTime"]; } set { 
                Parameters["TotalTime"] = value;
                Parameters["MidCurveTime"] = (value - 0.4f);
            } 
        }
    }
}
