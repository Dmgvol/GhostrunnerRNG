using GhostrunnerRNG.Game;
using GhostrunnerRNG.GameObjects;
using GhostrunnerRNG.MapGen;
using System.Collections.Generic;
using System.Diagnostics;

namespace GhostrunnerRNG.Maps {
    class BreatheInCV : MapCore {

        private List<Vector3f> Section1Positions = new List<Vector3f>();
        private List<Vector3f> Section2Positions = new List<Vector3f>();
        private bool FirstRNG = true;

        public BreatheInCV() : base(GameUtils.MapType.BreatheInCV, manualGen:true) {Gen_PerRoom();}

        protected override void Gen_PerRoom() {
            // section  1
            worldObjects.Add(new CVButton(0xF0, 0x1158, true)); // left button
            worldObjects.Add(new CVButton(0x468, 0x48, false)); // right button
            // section  2
            worldObjects.Add(new CVButton(0x450, 0x210, false)); // left button
            worldObjects.Add(new CVButton(0x458, 0x198, false)); // middle button
            worldObjects.Add(new CVButton(0x448, 0x288, false)); // right button
        }

        public override void RandomizeEnemies(Process game) {
            // First RNG? gather default positions
            if(FirstRNG) {
                FirstRNG = false;
                for(int i = 0; i < worldObjects.Count; i++) {
                    if(i < 2)
                        Section1Positions.Add(worldObjects[i].GetMemoryPos(game));
                    else
                        Section2Positions.Add(worldObjects[i].GetMemoryPos(game));
                }
            }

            // rng button positions
            var available = new List<Vector3f>(Section1Positions); // section 1
            for(int i = 0; i < 2; i++) {
                int r = Config.GetInstance().r.Next(available.Count);
                worldObjects[i].SetMemoryPos(game, new SpawnData(available[r]));
                available.RemoveAt(r);
            }
            available = new List<Vector3f>(Section2Positions); // section 2
            for(int i = 2; i < 5; i++) {
                int r = Config.GetInstance().r.Next(available.Count);
                worldObjects[i].SetMemoryPos(game, new SpawnData(available[r]));
                available.RemoveAt(r);
            }
        }
    }
}
