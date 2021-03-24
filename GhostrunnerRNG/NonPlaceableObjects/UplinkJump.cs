using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GhostrunnerRNG.NonPlaceableObjects {
    class UplinkJump : NonPlaceableObject {

        //0x045A3C20, 0x98, <firstOffset>, 0x128, 0xA8, <secondOffset>
        public UplinkJump(int firstOffset, int secondOffset) {
            ObjectDP = new DeepPointer(0x045A3C20, 0x98, firstOffset, 0x128, 0xA8, secondOffset);
            
            // add pointers
            Pointers.Add("TimeToActivate", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x234), IntPtr.Zero));
            Pointers.Add("JumpMultiplier", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x280), IntPtr.Zero));
            Pointers.Add("JumpForwardMultiplier", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x284), IntPtr.Zero));
            Pointers.Add("JumpGravity", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x288), IntPtr.Zero)); // 6 by default, 2x from basic value ingame
        }

        protected override void ReadDefaultValues(Process game) {
            UplinkJumpSpawnInfo info = new UplinkJumpSpawnInfo();
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
            if(spawnInfos != null && spawnInfos.Count > 0 && DefaultData is UplinkJumpSpawnInfo defaultData) {
                // pick random and check if correct type
                double rarity = (double)(SpawnPlane.r.Next(0, 100) / 100.0);
                // get list of same type and correct rarity
                List<UplinkJumpSpawnInfo> spawnLst = spawnInfos.OfType<UplinkJumpSpawnInfo>().Where(x => x.rarity >= rarity).ToList();
                if(spawnLst.Count == 0)  spawnLst.Add(defaultData); // no object with minimum rarity? add default
                
                int i = SpawnPlane.r.Next(spawnLst.Count);
                // change modified values only
                foreach(string key in Pointers.Keys) {
                    ModifyIfChanged(game, Pointers[key].Item2, spawnLst[i].Parameters[key], defaultData.Parameters[key].Value);
                }
            }
        }
    }

    public class UplinkJumpSpawnInfo : SpawnInfo {
        internal Dictionary<string, float?> Parameters = new Dictionary<string, float?>() {
            {"TimeToActivate",  null},
            {"JumpMultiplier",  null},
            {"JumpForwardMultiplier",  null},
            {"JumpGravity",  null}
        };

        public double rarity { get; private set; } = 1;
        public void SetRarity(double rarity) {
            if(rarity > 1) rarity = 1;
            if(rarity < 0) rarity = 0;
            this.rarity = rarity;
        }

        public float? TimeToActivate { get { return Parameters["TimeToActivate"]; } set { Parameters["TimeToActivate"] = value; } }
        public float? JumpMultiplier { get { return Parameters["JumpMultiplier"]; } set { Parameters["JumpMultiplier"] = value; } }
        public float? JumpForwardMultiplier { get { return Parameters["JumpForwardMultiplier"]; } set { Parameters["JumpForwardMultiplier"] = value; } }
        public float? JumpGravity { get { return Parameters["JumpGravity"]; } set { Parameters["JumpGravity"] = value; } }
    }
}