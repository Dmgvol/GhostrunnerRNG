using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.NonPlaceableObjects;

namespace GhostrunnerRNG.Maps {
    class TheMonster : MapCore {

        public TheMonster() : base(GameUtils.MapType.TheMonster, manualGen:true) {
            Gen_PerRoom();
        }

        protected override void Gen_PerRoom() {
            CPRequired = false;

            var jumppad = new JumpPad(0x530);
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo {Speed = 5000, Angle = new QuaternionAngle(90, -45, 0)}); // right, 45 angle
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 5000, Angle = new QuaternionAngle(-90, -45, 0)}); // left, 45 angle
            worldObjects.Add(jumppad);
        }
    }
}
