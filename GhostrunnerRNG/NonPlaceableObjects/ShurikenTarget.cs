using GhostrunnerRNG.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GhostrunnerRNG.NonPlaceableObjects {
    class ShurikenTarget : NonPlaceableObject {

        //0x045A3C20, 0x98, firstOffset, 0x128, 0xA8, secondOffset,
        public ShurikenTarget(int firstOffset, int secondOffset) {
            ObjectDP = new DeepPointer(0x045A3C20, 0x98, firstOffset, 0x128, 0xA8, secondOffset);
            // Add pointers
            Pointers.Add("HitsNeeded", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x224), IntPtr.Zero));
        }

        public override void Randomize(Process game) {
            DerefPointers(game);
            // read default value
            if(DefaultData == null) ReadDefaultValues(game);

            // got any spawnInfos?
            if(spawnInfos != null && spawnInfos.Count > 0 && DefaultData is ShurikenTargetSpawnInfo defaultInfo) {
                // pick random and check if correct type

                List<ShurikenTargetSpawnInfo> spawnLst = spawnInfos.OfType<ShurikenTargetSpawnInfo>().ToList();
                if(spawnLst.Count == 0) spawnLst.Add(defaultInfo); // no object with minimum rarity? add default

                // pick random and check if correct type
                int i = Config.GetInstance().r.Next(spawnLst.Count);
                // change modified values only
                ModifyIfChangedInt(game, Pointers["HitsNeeded"].Item2, spawnLst[i].HitsNeeded, defaultInfo.HitsNeeded);
            }
        }

        protected override void ReadDefaultValues(Process game) {
            ShurikenTargetSpawnInfo info = new ShurikenTargetSpawnInfo();
            // read default values
            int HitsNeeded;
            game.ReadValue(Pointers["HitsNeeded"].Item2, out HitsNeeded);
            info.HitsNeeded = HitsNeeded;
            DefaultData = info;
        }
    }

    public class ShurikenTargetSpawnInfo : SpawnInfo {
        private int? hitsNeeded;
        public int? HitsNeeded {
            get { return hitsNeeded; }
            set {
                if(value < 1) 
                    hitsNeeded = 1;
                else
                    hitsNeeded = value;
            }
        }
    }
}