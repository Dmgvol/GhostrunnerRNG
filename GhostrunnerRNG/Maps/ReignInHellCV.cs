using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.GameObjects;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.NonPlaceableObjects;
using System.Collections.Generic;

namespace GhostrunnerRNG.Maps {
    class ReignInHellCV : MapCore {
        public ReignInHellCV() : base(Game.GameUtils.MapType.ReignInHellCV, manualGen: true) {
            if(GameHook.IsHC) return;

            Gen_PerRoom();
            CPRequired = false;
        }
        protected override void Gen_PerRoom() {
            Rooms = new List<RoomLayout>();
            RoomLayout layout;

            ///// SECTION 1 /////
            List<WorldObject> orbs = new List<WorldObject>();
            orbs.Add(new CVOrb(0x120, 0x1938));
            orbs.Add(new CVOrb(0x128, 0x1920));
            orbs.Add(new CVOrb(0x138, 0x18f0));
            orbs.Add(new CVOrb(0x130, 0x1908));
            layout = new RoomLayout(orbs);

            // railings
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(102651, -25257, 298), new Vector3f(102641, -26964, 298))); // middle platform, right railing
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(101649, -27187, 289), new Vector3f(101651, -25313, 298))); // middle platform, left railing
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(103249, -27136, 298), new Vector3f(103245, -28100, 298))); // middle, right edge, corner railing
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(101022, -28150, 295), new Vector3f(101048, -27123, 295))); // middle, left edge, corner railing
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(101024, -29145, 296))); // door platform, corner railing left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(103296, -29156, 280))); // door platform, corner railing, right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(104737, -28165, 399))); // right platform, corner jump
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(104751, -27124, 457))); // right platform, corner jump 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(99551, -27137, 433))); // left platform, corner jump 1
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(99553, -28157, 408))); // left platform, corner jump 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(103243, -23228, 298))); // behind spawn, railing right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(101055, -23226, 298))); // behind spawn, railing left
            // Inside hallways
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(103969, -30261, 198))); // door platform, right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(100271, -30254, 198))); // door platform, left
            // default platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(101177, -27334, 198), new Vector3f(101601, -27971, 198)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(102688, -27293, 198), new Vector3f(103122, -28041, 198)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(103032, -29533, 198), new Vector3f(101283, -32102, 198)).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(101852, -29773, 198), new Vector3f(102517, -25240, 198)).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(102209, -29355, 310))); // arc
            Rooms.Add(layout);

            ///// SECTION 2 /////
            orbs = new List<WorldObject>();
            orbs.Add(new CVOrb(0x168, 0x1860));
            orbs.Add(new CVOrb(0x170, 0x1848));
            orbs.Add(new CVOrb(0x178, 0x1830));
            orbs.Add(new CVOrb(0x180, 0x1818));
            orbs.Add(new CVOrb(0x188, 0x1800));
            orbs.Add(new CVOrb(0x190, 0x17E8));

            layout = new RoomLayout(orbs);
            // default platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(102944, -45880, 648)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(101431, -42426, 648)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(104838, -43916, 648)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(104773, -45813, 648)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(102707, -47428, 1398), new Vector3f(102693, -47409, 1398)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(103029, -46097, 1398), new Vector3f(101401, -45763, 1398)));
            // railings
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(103166, -45600, 1612), new Vector3f(101448, -45596, 1572)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(103145, -45798, 1686), new Vector3f(103142, -47288, 1646)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(101566, -44100, 758), new Vector3f(102732, -44109, 758)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(103150, -43817, 758), new Vector3f(103146, -42426, 758)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(104649, -42295, 758), new Vector3f(104635, -43831, 757)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(104879, -44106, 758), new Vector3f(106369, -44109, 758)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(106419, -45598, 758), new Vector3f(104913, -45599, 758)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(104668, -45878, 757), new Vector3f(104641, -47298, 758)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(101374, -45583, 757), new Vector3f(102736, -45599, 757)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(103141, -45879, 758), new Vector3f(103142, -47345, 758)));
            // railing corners, hovering
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(103155, -44120, 871)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(104643, -44088, 924)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(104627, -45610, 858)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(103149, -45604, 1652)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(102550, -46196, 1642)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(106668, -45599, 927)));
            //// others
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(104870, -42133, 939))); // right-bot, railing between buildings
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(104726, -48032, 648))); // between trees
            // inside hallways
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(100616, -43116, 648)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(107224, -43101, 648)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(107059, -46584, 648)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(100693, -45905, 1398)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(100747, -46601, 648)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(102836, -48000, 1398)));
            Rooms.Add(layout);

            ///// SECTION 3 /////

            orbs = new List<WorldObject>();
            orbs.Add(new CVOrb(0x140, 0x18D8));
            orbs.Add(new CVOrb(0x148, 0x18C0));
            orbs.Add(new CVOrb(0x150, 0x18A8));
            orbs.Add(new CVOrb(0x158, 0x1890));
            orbs.Add(new CVOrb(0x160, 0x1878));

            layout = new RoomLayout(orbs);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(106184, -65970, -2501), new Vector3f(107959, -68040, -2501)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(109818, -68014, -2501), new Vector3f(111236, -66392, -2501)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(110215, -71496, -1901), new Vector3f(111502, -72057, -1901)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(107927, -69955, -2501), new Vector3f(106114, -70584, -2501)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(106112, -72492, -2501), new Vector3f(107912, -72994, -2501)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(104237, -73035, -2501), new Vector3f(102644, -71272, -2501)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(102571, -67943, -2501), new Vector3f(104293, -66198, -2501)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(102586, -68564, -1501), new Vector3f(103486, -68935, -1501)));
            // railings corners
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(103624, -68462, -1338)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(104448, -71025, -2223)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(105938, -72257, -2211)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(108167, -72230, -2243)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(108161, -70738, -2201)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(108130, -69738, -2307)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(105927, -69755, -2214)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(105943, -70756, -2205)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(104435, -68252, -2201)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(104444, -66024, -2266)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(105930, -68267, -2214)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(108162, -68228, -2275), new Vector3f(108145, -66148, -2265)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(109664, -66031, -2216), new Vector3f(109658, -68267, -2303)));
            // others
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(102314, -67150, -1788))); // door frame (left)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(103336, -66157, -1789))); // door frame 2 (left)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(103349, -70371, -1501))); // bridge (after button press)
            // in hallways
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(102253, -68760, -598)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(110841, -72220, -798)));
            Rooms.Add(layout);

            ////// JumpPads //////
            #region JumpPad
            NonPlaceableObject jumppad = new JumpPad(0x4e8);//4500
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(-90.0f, -40.0f, 0.0f), Speed = 8000 });//forward high
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(-90.0f, -20.0f, 0.0f), Speed = 8000 });//forward short
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(-90.0f, -80.00f, 0.0f), Speed = 8100 });//3 platfrom
            worldObjects.Add(jumppad);

            jumppad = new JumpPad(0x508);//4700
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(-85.0f, -40.0f, 0.0f), Speed = 8000 }.SetRarity(0.035));//skip all jumppads
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(-60.0f, -73.0f, 0.0f), Speed = 12750 }.SetRarity(0.25));//oob to the exit
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(0.0f, -90.00f, 0.0f), Speed = 100000 }.SetRarity(0.35));//troll
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(-57.0f, -45.00f, 0.0f), Speed = 7200 }.SetRarity(0.5));//skip 2
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(-35.0f, -70.00f, 0.0f), Speed = 7200 }.SetRarity(1.35));//skip 1
            worldObjects.Add(jumppad);

            jumppad = new JumpPad(0x500);//4800
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(-75.0f, -60.0f, 0.0f), Speed = 5500 }.SetRarity(0.35));//skip all jumppads
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(-85.0f, -70.0f, 0.0f), Speed = 8000 }.SetRarity(0.5));//oob to the next section
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(-42.5f, -40.0f, 0.0f), Speed = 5800 });//skip 1
            worldObjects.Add(jumppad);

            jumppad = new JumpPad(0x4F0);//4800
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(-127.0f, -60.0f, 0.0f), Speed = 6500 });//skip all jumppads
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(0.0f, -90.00f, 0.0f), Speed = 100000 }.SetRarity(0.35));//troll
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(-75.0f, -70.0f, 0.0f), Speed = 8050 }.SetRarity(1.35));//oob to the exit
            worldObjects.Add(jumppad);

            //jumppad = new JumpPad(0x4F8);//4600
            //jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            //worldObjects.Add(jumppad);
            #endregion

        }
    }
}
