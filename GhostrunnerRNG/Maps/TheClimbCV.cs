using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.NonPlaceableObjects;
using System.Collections.Generic;
using System.Diagnostics;

namespace GhostrunnerRNG.Maps {
    class TheClimbCV : MapCore {

        private readonly List<Vector3f> Triggers = new List<Vector3f>() { 
            new Vector3f(-2200,  9000, 7693.49f),   // I Block
            new Vector3f(-2200, 7000, 7693.49f),    // T Block
            new Vector3f(770, 9700, 7693.49f)       // third Block
        };

        public TheClimbCV() : base(GameUtils.MapType.TheClimbCV, manualGen:true) {
            Gen_PerRoom();
            CPRequired = false;
        }

        protected override void Gen_PerRoom() {
            // Tetris Hologram Triggers
            nonPlaceableObjects.Add(new TetrisTrigger(0x158, 0x278, 0x330));
            nonPlaceableObjects.Add(new TetrisTrigger(0x150, 0x288, 0x318));
            nonPlaceableObjects.Add(new TetrisTrigger(0x18, 0x40, 0x5B8));
        }

        public override void RandomizeEnemies(Process game) {
            // shuffle spots
            List<Vector3f> availableSpots = new List<Vector3f>(Triggers);
            for(int i = 0; i < nonPlaceableObjects.Count; i++) {
                if(nonPlaceableObjects[i] is TetrisTrigger tetris) {
                    // pick random from available
                    int r = Config.GetInstance().r.Next(availableSpots.Count);
                    // add new spawninfo
                    tetris.ClearSpawnInfo();
                    tetris.AddNewSpawnInfo(new SpawnInfo { Pos = availableSpots[r]});
                    availableSpots.RemoveAt(r);
                }
            }

            // apply spawnInfo
            nonPlaceableObjects.ForEach(x => x.Randomize(game));
        }
    }
}
