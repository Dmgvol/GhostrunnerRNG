using GhostrunnerRNG.Game;
using GhostrunnerRNG.GameObjects;
using GhostrunnerRNG.MapGen;
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
            worldObjects.Add(new TetrisTrigger(0x158, 0x278, 0x330, 0x98));
            worldObjects.Add(new TetrisTrigger(0x150, 0x288, 0x318, 0x90));
            worldObjects.Add(new TetrisTrigger(0x18, 0x40, 0x5B8, 0x10));
        }

        public override void RandomizeEnemies(Process game) {
            // shuffle spots
            List<Vector3f> availableSpots = new List<Vector3f>(Triggers);
            for(int i = 0; i < worldObjects.Count; i++) {
                if(worldObjects[i] is TetrisTrigger tetris) {
                    // pick random from available
                    int r = Config.GetInstance().r.Next(availableSpots.Count);
                    // add new spawninfo
                    tetris.SetMemoryPos(game, new SpawnData(availableSpots[r]));
                    availableSpots.RemoveAt(r);
                }
            }
        }
    }
}
