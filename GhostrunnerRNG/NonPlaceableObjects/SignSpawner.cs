using GhostrunnerRNG.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GhostrunnerRNG.NonPlaceableObjects {
    class SignSpawner : NonPlaceableObject {

        // 0x045A3C20, 0x98, 0x18, 0x128, 0xA8, offset
        public SignSpawner(int offset) {
            ObjectDP = new DeepPointer(0x04609420, 0x98, 0x18, 0x128, 0xA8, offset);

            Pointers.Add("Speed", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x248), IntPtr.Zero));
            Pointers.Add("SpawnDelay", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x24C), IntPtr.Zero));
            Pointers.Add("DelayOnStart", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x250), IntPtr.Zero));
        }

        protected override void ReadDefaultValues(Process game) {
            SignSpawnerSpawnInfo info = new SignSpawnerSpawnInfo();
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
            if(spawnInfos != null && spawnInfos.Count > 0 && DefaultData is SignSpawnerSpawnInfo defaultData) {
                // get list of same type and correct rarity
                List<SignSpawnerSpawnInfo> spawnLst = spawnInfos.OfType<SignSpawnerSpawnInfo>().ToList();
                if(spawnLst.Count == 0) spawnLst.Add(defaultData); // no object for some reason? add default

                int i = Config.GetInstance().r.Next(spawnLst.Count);
                // change modified values only
                foreach(string key in Pointers.Keys) {
                    ModifyIfChanged(game, Pointers[key].Item2, spawnLst[i].Parameters[key], defaultData.Parameters[key].Value);
                }
            }
        }
    }

    public class SignSpawnerSpawnInfo : SpawnInfo {
        internal Dictionary<string, float?> Parameters = new Dictionary<string, float?>() {
            {"Speed",  null},
            {"SpawnDelay",  null},
            {"DelayOnStart",  null}
        };

        public float? Speed { get { return Parameters["Speed"]; } set { Parameters["Speed"] = value; } }
        public float? SpawnDelay { get { return Parameters["SpawnDelay"]; } set { Parameters["SpawnDelay"] = value; } }
        public float? DelayOnStart { get { return Parameters["DelayOnStart"]; } set { Parameters["DelayOnStart"] = value; } }
    }
}
