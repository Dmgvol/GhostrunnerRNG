using GhostrunnerRNG.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GhostrunnerRNG.NonPlaceableObjects {
    class ChainedSignSpawners : NonPlaceableObject {

        private List<SignSpawner> spawners = new List<SignSpawner>();
        private List<List<SignSpawnerSpawnInfo>> chainedSpawnInfos = new List<List<SignSpawnerSpawnInfo>>();

        public ChainedSignSpawners(params int[] spawnerOffests) {
            for(int i = 0; i < spawnerOffests.Length; i++) {
                spawners.Add(new SignSpawner(spawnerOffests[i]));
            }
        }

        public void AddChainedInfo(List<SignSpawnerSpawnInfo> info) {
            if(info.Count == spawners.Count) {
                chainedSpawnInfos.Add(info);
            }
        }

        public override void Randomize(Process game) {
            int selectedIndex = Config.GetInstance().r.Next(chainedSpawnInfos.Count);
            // as a trick, we add only one SpawnFnfo to corresponding spawners, so it won't "double-rng" it (consistent results)
            spawners.ForEach(x => x.ClearSpawnInfo());
            for(int i = 0; i < spawners.Count; i++) {
                var info = chainedSpawnInfos[selectedIndex][i];
                spawners[i].AddSpawnInfo(info);
            }
            spawners.ForEach(x => x.Randomize(game));
        }

        protected override void ReadDefaultValues(Process game) {throw new NotImplementedException(); }
    }
}
