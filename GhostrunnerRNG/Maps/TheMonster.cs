using GhostrunnerRNG.Game;
using GhostrunnerRNG.GameObjects;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.NonPlaceableObjects;

namespace GhostrunnerRNG.Maps {
    class TheMonster : MapCore {

        public TheMonster() : base(GameUtils.MapType.TheMonster, manualGen:true) {
            Gen_PerRoom();
        }

        protected override void Gen_PerRoom() {
            CPRequired = false;

            ////// JumpPads //////
            #region JumpPad
            var jumppad = new JumpPad(0x530);//4000
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo {Speed = 3000, Angle = new QuaternionAngle(-15, -45, 0)}); // short left
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo {Speed = 5300, Angle = new QuaternionAngle(3, -50, 0)}); // to right wall
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 7000, Angle = new QuaternionAngle(2, -45, 0)}.SetRarity(0.5)); // skip 1 jump
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 70000, Angle = new QuaternionAngle(-2, -45, 0)}.SetRarity(0.3)); // teleport
            worldObjects.Add(jumppad);

            jumppad = new JumpPad(0x68);//4200
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 6000, Angle = new QuaternionAngle(15, 0, 0) }); // to the 3 jump
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 5300, Angle = new QuaternionAngle(90, -70, 0) }); // to right wall
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 7000, Angle = new QuaternionAngle(2, -45, 0) }.SetRarity(0.25)); // works only with dash
            worldObjects.Add(jumppad);

            jumppad = new JumpPad(0x508);//5000
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 66000, Angle = new QuaternionAngle(-1, -20, 0)}.SetRarity(0.25)); // i believe i can fly
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 22000, Angle = new QuaternionAngle(-8, -25, 0)}.SetRarity(0.5)); // BALLS
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 6000, Angle = new QuaternionAngle(-60, -45, 0) }); // 
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 12000, Angle = new QuaternionAngle(-30, -45, 0) }); // fast
            worldObjects.Add(jumppad);

            CustomCheckPoints.Add(new CustomCP(mapType, new Vector3f(-5650, 24330, -3380), new Vector3f(-3326, -22780, 420),
            new Vector3f(-7260, 31000, -3350), new Angle(-0.43f, 0.9f)));
            
            jumppad = new JumpPad(0x500);//5000
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 6000, Angle = new QuaternionAngle(0, -92, 0)}); // vertical
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 15000, Angle = new QuaternionAngle(0, -92, 0)}); // troll
            worldObjects.Add(jumppad);

            jumppad = new JumpPad(0x90);//5000
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 10000, Angle = new QuaternionAngle(-5, -35, 0)}); // to the next jump
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 17000, Angle = new QuaternionAngle(-3, -35, 0)}.SetRarity(0.35)); // balls
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 7000, Angle = new QuaternionAngle(-10, -85, 0) }); // upward
            worldObjects.Add(jumppad);

            CustomCheckPoints.Add(new CustomCP(mapType, new Vector3f(15930, 11630, -2340), new Vector3f(20660, 22620, 5300),
            new Vector3f(17570, 15760, 1200), new Angle(0.0f, 1.0f)));

            jumppad = new JumpPad(0x528);//6000
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 11000, Angle = new QuaternionAngle(0, -45, 0)}); // to the next jump
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 18500, Angle = new QuaternionAngle(0, -25, 0)}.SetRarity(0.5)); // skip half balls
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 6000, Angle = new QuaternionAngle(0, -108, 0)}); // vertical
            worldObjects.Add(jumppad);

            jumppad = new JumpPad(0x520);//5000 
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 12000, Angle = new QuaternionAngle(0, -35, 0)}.SetRarity(0.5)); // skip half balls
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 18000, Angle = new QuaternionAngle(0, -35, 0)}.SetRarity(0.5)); // almost balls
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 8500, Angle = new QuaternionAngle(0, -85, 0)}); // vertical
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 20000, Angle = new QuaternionAngle(7, -55, 0)}.SetRarity(0.25)); // skip balls
            worldObjects.Add(jumppad);

            CustomCheckPoints.Add(new CustomCP(mapType, new Vector3f(48840, 11170, 6050), new Vector3f(50875, 19764, 10680),
            new Vector3f(52500, 15700, 8000), new Angle(0.0f, 1.0f)));

            jumppad = new JumpPad(0x518);//5000 balls left
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 7000, Angle = new QuaternionAngle(0, -45, 0)}); // skip next jump
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 15000, Angle = new QuaternionAngle(28, -28, 0)}.SetRarity(0.35)); // skip 4 jumps
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 4700, Angle = new QuaternionAngle(90, -35, 0)}); // to the right jump
            worldObjects.Add(jumppad);

            jumppad = new JumpPad(0x510);//5000 balls right
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 7500, Angle = new QuaternionAngle(-45, -45, 0)}); // skip next jump
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 6666, Angle = new QuaternionAngle(-18, -35, 0)}); // skip cp
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 10750, Angle = new QuaternionAngle(5, -30, 0)}.SetRarity(0.5)); // to the right jump
            worldObjects.Add(jumppad);

            CustomCheckPoints.Add(new CustomCP(mapType, new Vector3f(86900, 10200, 8075), new Vector3f(105150, 31520, 17700),
            new Vector3f(86700, 15350, 9700), new Angle(0.0f, 1.0f)));

            jumppad = new JumpPad(0x558);//6000
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 46850, Angle = new QuaternionAngle(3.19f, -25.5f, 0)}.SetRarity(0.25)); // i believe i can fly 2
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 9200, Angle = new QuaternionAngle(3.5f, -40, 0)}); // skip platforms
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 66666, Angle = new QuaternionAngle(37, -25, 0)}); // reaction jump
            worldObjects.Add(jumppad);

            jumppad = new JumpPad(0x550);//5000
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 7000, Angle = new QuaternionAngle(119, -42, 0)}); // skip 1 jump
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 10000, Angle = new QuaternionAngle(-180, -80, 0)}); // vertical
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 66666, Angle = new QuaternionAngle(100, -25, 0)}); // reaction jump
            worldObjects.Add(jumppad);

            jumppad = new JumpPad(0x98);//6000
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 5750, Angle = new QuaternionAngle(45, -45, 0)}); // skip 1 jump, short
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 100000, Angle = new QuaternionAngle(89, -23, 0)}.SetRarity(0.5)); // fast bounce
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 12500, Angle = new QuaternionAngle(15, -27, 0)}); // almost skip platforms
            worldObjects.Add(jumppad);

            jumppad = new JumpPad(0x548);//5000
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 10750, Angle = new QuaternionAngle(0, -25, 0) }); // skip half platforms
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 8000, Angle = new QuaternionAngle(0, -88, 0)}); // vertical
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 35000, Angle = new QuaternionAngle(0, -7, 0) }.SetRarity(0.5)); // straight into balls
            worldObjects.Add(jumppad);

            jumppad = new JumpPad(0x560);//5800
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 10750, Angle = new QuaternionAngle(0, -75, 0)}); // short ceiling bounce 
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 80000, Angle = new QuaternionAngle(0, -25, 0)}); // bounce and fall
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 13000, Angle = new QuaternionAngle(0, -29, 0)}); // straight into balls
            worldObjects.Add(jumppad);

            jumppad = new JumpPad(0x538);//4200
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 10750, Angle = new QuaternionAngle(0, -75, 0)}); // short ceiling bounce 
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 220000, Angle = new QuaternionAngle(0, -25, 0)}.SetRarity(0.35)); // reacion grapple
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 15000, Angle = new QuaternionAngle(0, -25, 0)}); // straight into balls
            worldObjects.Add(jumppad);
            #endregion
        }
    }
}
