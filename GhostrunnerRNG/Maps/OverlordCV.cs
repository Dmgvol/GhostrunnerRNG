using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.NonPlaceableObjects;

namespace GhostrunnerRNG.Maps {
    class OverlordCV : MapCore {
        public OverlordCV() : base(Game.GameUtils.MapType.OverlordCV, manualGen: true) {
            Gen_PerRoom();

        }

        protected override void Gen_PerRoom() {
            CPRequired = false;
            #region JumpPad
            NonPlaceableObject jumppad = new JumpPad(0x4A0);//3600
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(-90.0f, -40.0f, 0.0f), Speed = 8000 }.SetRarity(0.35));//wall bounce
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(180.0f, -15.0f, 0.0f), Speed = 8000 });//forward fast
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(180.0f, -14.00f, 0.0f), Speed = 15100 }.SetRarity(0.5));//skip 2 phase
            worldObjects.Add(jumppad);
            #endregion
        }
    }
}
