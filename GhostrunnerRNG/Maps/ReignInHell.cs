using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.Maps {
    class ReignInHell : MapCore {

        #region Rooms


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

        }

        public void Gen_PerRoom_AfterCV() {


        }
    }
}
