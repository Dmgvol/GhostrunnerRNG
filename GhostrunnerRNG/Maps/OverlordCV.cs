using GhostrunnerRNG.MapGen;

namespace GhostrunnerRNG.Maps {
    class OverlordCV : MapCore {
        public OverlordCV() : base(Game.GameUtils.MapType.OverlordCV, manualGen: true) {
            Gen_PerRoom();
        }

        protected override void Gen_PerRoom() {
            

        }
    }
}
