using GhostrunnerRNG.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GhostrunnerRNG.NonPlaceableObjects {
    class ToggleableFan : NonPlaceableObject {

        //0x045A3C20 + 98 + <firstOffset> + 128 + A8 + <secondOffset> + XXX
        public ToggleableFan(int firstOffset, int secondOffset) {
            ObjectDP = new DeepPointer(0x045A3C20, 0x98, firstOffset, 0x128, 0xA8, secondOffset);

            // Add pointers
            Pointers.Add("FanSpeed", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x2E0), IntPtr.Zero));
            Pointers.Add("HackedFanSpeed", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x2E8), IntPtr.Zero));
            Pointers.Add("HackedDuration", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x2EC), IntPtr.Zero));
        }
        public override void Randomize(Process game) {
            DerefPointers(game);
            // read default value
            if(DefaultData == null) ReadDefaultValues(game);

            // got any spawnInfo?
            if(spawnInfos != null && spawnInfos.Count > 0 && DefaultData is ToggleableFanSpawnInfo defaultData) {
                // get list of same type
                List<ToggleableFanSpawnInfo> spawnLst = spawnInfos.OfType<ToggleableFanSpawnInfo>().ToList();

                int i = Config.GetInstance().r.Next(spawnLst.Count);
                // change modified values only
                foreach(string key in Pointers.Keys) {
                    ModifyIfChanged(game, Pointers[key].Item2, spawnLst[i].Parameters[key], defaultData.Parameters[key].Value);
                }
            }
        }

        public ToggleableFan LoadDefaultPreset() {
            AddSpawnInfo(new ToggleableFanSpawnInfo()); // default
            AddSpawnInfo(new ToggleableFanSpawnInfo { HackedDuration = Config.GetInstance().r.Next(2, 5) }); // short use
            AddSpawnInfo(new ToggleableFanSpawnInfo { HackedDuration = Config.GetInstance().r.Next(2, 6), FanSpeed = 50, HackedFanSpeed = 400 }); // slow spin, fast hacked speed
            AddSpawnInfo(new ToggleableFanSpawnInfo { HackedDuration = Config.GetInstance().r.Next(2, 6), FanSpeed = 600, HackedFanSpeed = 5 }); // fast spin, really slow hacked
            AddSpawnInfo(new ToggleableFanSpawnInfo { HackedDuration = Config.GetInstance().r.Next(5, 10), FanSpeed = 30, HackedFanSpeed = 5 }); // slow fan, and even slower hacked
            return this;
        }

        protected override void ReadDefaultValues(Process game) {
            ToggleableFanSpawnInfo info = new ToggleableFanSpawnInfo();
            // read default values [175, 10, 5]
            foreach(string key in info.Parameters.Keys.ToList()) {
                float value;
                game.ReadValue(Pointers[key].Item2, out value);
                info.Parameters[key] = value;
            }
            DefaultData = info;
        }
    }

    public class ToggleableFanSpawnInfo : SpawnInfo {
        public Dictionary<string, float?> Parameters = new Dictionary<string, float?>() {
            {"FanSpeed",  null},
            {"HackedFanSpeed",  null},
            {"HackedDuration",  null}
        };
        public float? FanSpeed { get { return Parameters["FanSpeed"]; } set { Parameters["FanSpeed"] = value; } }
        public float? HackedFanSpeed { get { return Parameters["HackedFanSpeed"]; } set { Parameters["HackedFanSpeed"] = value; } }
        public float? HackedDuration { get { return Parameters["HackedDuration"]; } set { Parameters["HackedDuration"] = value; } }
    }
}
