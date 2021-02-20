using System;
using System.Collections.Generic;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.Maps {
    class LookInside : MapCore{

        public LookInside(bool isHC) : base(MapType.LookInside) {
            if(!isHC) {
                



                //Gen_PerRoom();
            } else {
                // hardcore
                //TODO: remove temporary block/msg and add hc enemies and gen
            }
        }

        protected override void Gen_PerRoom() {
            Rooms = new List<RoomLayout>();
            //RoomLayout layout = new RoomLayout(Enemies[6]);
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-18060, -64136, 3598), new Vector3f(-19392, -63758, 3601), new Angle(0f, 1f)));

            //Rooms.Add(layout);



        }
    }
}
