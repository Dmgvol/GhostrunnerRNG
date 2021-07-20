using GhostrunnerRNG.Game;
using GhostrunnerRNG.MemoryUtils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GhostrunnerRNG.NonPlaceableObjects {
    public class UplinkShurikens : NonPlaceableObject {

        // 0x045A3C20, 0x98, firstOffset, 0x128, 0xA8, secondOffset
        public UplinkShurikens(int firstOffset, int secondOffset) {
            ObjectDP = new DeepPointer(PtrDB.DP_UplinkShurikens).ModifyOffset(1, firstOffset).ModifyOffset(4, secondOffset);
            AddPointers();
        }

        public UplinkShurikens(DeepPointer customDP) {
            ObjectDP = customDP;
            AddPointers();
        }

        private void AddPointers() {
            // Add points
            Pointers.Add("Duration", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x238), IntPtr.Zero));
            Pointers.Add("MaxAttacks", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x270), IntPtr.Zero));
        }

        protected override void ReadDefaultValues(Process game) {
            UplinkShurikensSpawnInfo info = new UplinkShurikensSpawnInfo();
            // read default values
            
            float duration;
            game.ReadValue(Pointers["Duration"].Item2, out duration);
            info.Duration = duration;

            int maxAttacks;
            game.ReadValue(Pointers["MaxAttacks"].Item2, out maxAttacks);
            info.MaxAttacks = maxAttacks;

            DefaultData = info;
        }

        public override void Randomize(Process game) {
            DerefPointers(game);
            // read default value
            if(DefaultData == null) ReadDefaultValues(game);

            // got any spawnInfos?
            if(spawnInfos != null && spawnInfos.Count > 0 && DefaultData is UplinkShurikensSpawnInfo defaultInfo) {
                // pick random and check if correct type
                double rarity = (double)(Config.GetInstance().r.Next(0, 100) / 100.0);

                List<UplinkShurikensSpawnInfo> spawnLst = spawnInfos.OfType<UplinkShurikensSpawnInfo>().Where(x => x.rarity >= rarity).ToList();
                if(spawnLst.Count == 0) spawnLst.Add(defaultInfo); // no object with minimum rarity? add default

                // pick random and check if correct type
                int i = Config.GetInstance().r.Next(spawnLst.Count);
                // change modified values only
                ModifyIfChanged(game, Pointers["Duration"].Item2, spawnLst[i].Duration, defaultInfo.Duration);
                ModifyIfChangedInt(game, Pointers["MaxAttacks"].Item2, spawnLst[i].MaxAttacks, defaultInfo.MaxAttacks);
            }
        }
    }
    public class UplinkShurikensSpawnInfo : SpawnInfo {
        public float? Duration;
        public int? MaxAttacks;
        public double rarity { get; private set; } = 1;
        public UplinkShurikensSpawnInfo SetRarity(double rarity) {
            if(rarity > 1) rarity = 1;
            if(rarity < 0) rarity = 0;
            this.rarity = rarity;
            return this;
        }
    }
}
