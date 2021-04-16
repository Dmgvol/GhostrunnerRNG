using GhostrunnerRNG.MapGen;

namespace GhostrunnerRNG.Maps {
    class SurgeCV : MapCore {

        public SurgeCV() : base(Game.GameUtils.MapType.SurgeCV, manualGen:true) {
            Gen_PerRoom();
        }
        protected override void Gen_PerRoom() {
            CPRequired = false;



        }
    }
}
