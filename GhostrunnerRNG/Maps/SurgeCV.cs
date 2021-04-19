using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.NonPlaceableObjects;

namespace GhostrunnerRNG.Maps {
    class SurgeCV : MapCore {

        public SurgeCV() : base(Game.GameUtils.MapType.SurgeCV, manualGen:true) {
            Gen_PerRoom();
        }
        protected override void Gen_PerRoom() {
            CPRequired = false;

            #region JumpPad
            var jumppad = new JumpPad(0x1D0);//3500
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 9000, Angle = new QuaternionAngle(-93, -30, 0) }); // short 
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 9300, Angle = new QuaternionAngle(-93, -38, 0) }); // long
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 7500, Angle = new QuaternionAngle(-90, -75, 0) }.SetRarity(0.35)); // oob on top
            worldObjects.Add(jumppad);

            jumppad = new JumpPad(0x1C8);//3500
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 6000, Angle = new QuaternionAngle(-147, -45, 0) }.SetRarity(0.5)); // to the exit 
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 4750, Angle = new QuaternionAngle(-120, -45, 0) }); // long
            worldObjects.Add(jumppad);

            //jumppad = new JumpPad(0x148);//3500
            //jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            //worldObjects.Add(jumppad);
            #endregion

        }
    }
}
