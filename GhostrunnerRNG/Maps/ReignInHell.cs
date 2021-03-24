using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using System.Collections.Generic;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.Maps {
    class ReignInHell : MapCore {

        #region Rooms
        // Before CV
        //private Room room_1 = new Room());
        //private Room room_2 = new Room());
        //private Room room_3 = new Room());
        //private Room room_4 = new Room());
        //private Room room_5 = new Room());
        //private Room room_6 = new Room());
        //private Room room_7 = new Room());
        //// After CV
        //private Room room_8 = new Room());
        //private Room room_9 = new Room());
        //private Room room_10 = new Room());
        //private Room room_11 = new Room());
        //private Room room_12 = new Room());

        #endregion

        // cv flag
        private bool BeforeCV = true;

        public ReignInHell(bool isHC) : base(MapType.ReignInHell) {
            BeforeCV = GameHook.yPos < 20000;
            if(!isHC) {
                if(BeforeCV)
                    Gen_PerRoom();
                else
                    Gen_PerRoom_AfterCV();
            }
        }

        protected override void Gen_PerRoom() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game, 0, 37);
            Rooms = new List<RoomLayout>();
            RoomLayout layout;
            List<Enemy> enemies;




        }

        private void Gen_PerRoom_AfterCV() {


        }
    }
}
