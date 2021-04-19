using GhostrunnerRNG.Game;
using GhostrunnerRNG.GameObjects;
using GhostrunnerRNG.MapGen;
using System;
using System.Collections.Generic;
using GhostrunnerRNG.NonPlaceableObjects;
using System.Diagnostics;

namespace GhostrunnerRNG.Maps {
    class LookInsideCV : MapCore {

        // Platforms
        private List<GreenPlatform> platforms = new List<GreenPlatform>();
        private List<SpawnData> platformLayouts = new List<SpawnData>();

        public LookInsideCV() : base(GameUtils.MapType.LookInsideCV, manualGen:true) {
            Gen_PerRoom();
            CPRequired = false;
        }

        protected override void Gen_PerRoom() {

            ////// Green Platforms //////
            platforms.Add(new GreenPlatform(0x9E0, 0xE88));
            platforms.Add(new GreenPlatform(0x9F8, 0xDB0));
            platforms.Add(new GreenPlatform(0xA00, 0xD68));
            platforms.Add(new GreenPlatform(0xA08, 0xD20));
            platforms.Add(new GreenPlatform(0xA30, 0xBB8));
            platforms.Add(new GreenPlatform(0xA20, 0xC48));
            platforms.Add(new GreenPlatform(0xA18, 0xC90));
            platforms.Add(new GreenPlatform(0xA28, 0xC00));
            platforms.Add(new GreenPlatform(0xA10, 0xCD8));
            platforms.Add(new GreenPlatform(0x9F0, 0xDF8));
            platforms.Add(new GreenPlatform(0x9E8, 0xE40));
            platforms.Add(new GreenPlatform(0x9D8, 0xED0));
            platforms.Add(new GreenPlatform(0x9D0, 0xF18));
            platforms.Add(new GreenPlatform(0x9C8, 0xF60));
            platforms.Add(new GreenPlatform(0x9C0, 0xFA8));
            platforms.Add(new GreenPlatform(0x9B8, 0xFF0));
            platforms.Add(new GreenPlatform(0x9B0, 0x1038));
            platforms.Add(new GreenPlatform(0x9A8, 0x1080));

            // Platforms layouts
            //platformLayouts.Add(new SpawnData(new Vector3f(50232.93f, -10371.06f, 2988.9f), new Angle(1.00f, 0.00f)));
            //platformLayouts.Add(new SpawnData(new Vector3f(50746.19f, -10623.07f, 3123.783f), new Angle(-0.45f, 0.89f)));
            //platformLayouts.Add(new SpawnData(new Vector3f(51239.04f, -11337.36f, 3209.209f), new Angle(-0.72f, 0.70f)));
            //platformLayouts.Add(new SpawnData(new Vector3f(51176.75f, -12164.8f, 3406.602f), new Angle(-0.71f, 0.71f)));
            //platformLayouts.Add(new SpawnData(new Vector3f(51293.42f, -12759.18f, 3573.017f), new Angle(-0.48f, 0.87f)));
            //platformLayouts.Add(new SpawnData(new Vector3f(51762.2f, -13277.17f, 3737.203f), new Angle(-0.28f, 0.96f)));
            //platformLayouts.Add(new SpawnData(new Vector3f(52215.43f, -13650.36f, 3941.672f), new Angle(-0.65f, 0.76f)));
            //platformLayouts.Add(new SpawnData(new Vector3f(52200.01f, -14317.66f, 4140.634f), new Angle(-0.72f, 0.70f)));
            //platformLayouts.Add(new SpawnData(new Vector3f(52099.57f, -14987.48f, 4318.261f), new Angle(-0.76f, 0.65f)));
            //platformLayouts.Add(new SpawnData(new Vector3f(51784.11f, -15663.53f, 4455.74f), new Angle(-0.88f, 0.48f)));
            //platformLayouts.Add(new SpawnData(new Vector3f(51317.89f, -16294.11f, 4584.072f), new Angle(-0.89f, 0.45f)));
            //platformLayouts.Add(new SpawnData(new Vector3f(50883.04f, -16784.74f, 4716.091f), new Angle(-0.85f, 0.52f)));
            //platformLayouts.Add(new SpawnData(new Vector3f(50659.76f, -17336.82f, 4764.241f), new Angle(-0.76f, 0.65f)));
            //platformLayouts.Add(new SpawnData(new Vector3f(50223.77f, -18020.39f, 4812.391f), new Angle(-0.82f, 0.57f)));

            //platformLayouts.Add(new SpawnData(new Vector3f(50428.26f, -12785.69f, 3529.526f), new Angle(-1.00f, 0.02f)));
            //platformLayouts.Add(new SpawnData(new Vector3f(49754.46f, -13154.93f, 3900.068f), new Angle(-0.96f, 0.28f)));
            //platformLayouts.Add(new SpawnData(new Vector3f(49187.71f, -13739.8f, 4251.918f), new Angle(-0.91f, 0.42f)));
            //platformLayouts.Add(new SpawnData(new Vector3f(49155.66f, -14196.82f, 4731.175f), new Angle(-0.51f, 0.86f)));

            platformLayouts.Add(new SpawnData(new Vector3f(49928.3f, -10312.03f, 3170.154f), new Angle(-0.26f, 0.97f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50252.32f, -10700.2f, 3345.558f), new Angle(-0.59f, 0.81f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50486.54f, -11389.49f, 3660.798f), new Angle(-0.61f, 0.79f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50709.39f, -12114.19f, 3992.159f), new Angle(-0.59f, 0.80f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50921.46f, -12905.07f, 4346.134f), new Angle(-0.62f, 0.79f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51068.15f, -13386.38f, 4590.304f), new Angle(-0.57f, 0.82f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51005.89f, -14147.21f, 4745.639f), new Angle(-0.79f, 0.62f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50599.54f, -14875.6f, 4823.447f), new Angle(-0.86f, 0.50f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50122.34f, -15624.99f, 4883.574f), new Angle(-0.89f, 0.45f)));

            platformLayouts.Add(new SpawnData(new Vector3f(48998.49f, -10505.32f, 3185.244f), new Angle(-0.94f, 0.35f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48649.5f, -10795.1f, 3203.475f), new Angle(0.35f, 0.94f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48388.04f, -11375.94f, 3390.9f), new Angle(-0.74f, 0.67f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48372.87f, -12026.14f, 3607.499f), new Angle(-0.67f, 0.74f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48418.58f, -12612.28f, 3793.165f), new Angle(-0.68f, 0.74f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48496.28f, -13244.21f, 4021.173f), new Angle(-0.65f, 0.76f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48564.23f, -13815.14f, 4264.257f), new Angle(-0.62f, 0.78f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48692.07f, -14364.02f, 4559.024f), new Angle(-0.29f, 0.96f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49049.59f, -14572.7f, 4826.806f), new Angle(-0.25f, 0.97f)));
            //



            ////// keys //////
            #region Keys
            Rooms = new List<RoomLayout>();
            RoomLayout layout;
            List<WorldObject> keys = new List<WorldObject>();
            keys.Add(new CVKey(0xA98, 0xCF8, 0x318));
            keys.Add(new CVKey(0xAB0, 0xCD8, 0x408));
            keys.Add(new CVKey(0xAA8, 0xCB8, 0x510));
            keys.Add(new CVKey(0xAA0, 0xCA8, 0x570));
            keys.Add(new CVKey(0xA90, 0xC88, 0x5D0));

            // First Room
            layout = new RoomLayout(keys[0]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(45823, -37582, 8593)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53754, -37374, 8593)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53697, -34409, 8593)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49089, -33463, 8593)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(47570, -39136, 8621)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(52143, -38772, 8643)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49757, -42331, 8593)));
            Rooms.Add(layout);

            // Second Room - 2 keys
            layout = new RoomLayout(keys[1], keys[2]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(47537, -34205, 11478)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(45532, -35307, 11478)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(45611, -37278, 11478)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(45647, -38806, 11337)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(45537, -40240, 11470)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49587, -36790, 11478)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53261, -38783, 11478)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53310, -41370, 12917)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53327, -37553, 12878)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53371, -35344, 12826)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51016, -34844, 12878)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51075, -33337, 12878)));
            Rooms.Add(layout);

            // Third Room - architect/mara scene
            layout = new RoomLayout(keys[3]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(54034, -33631, 14314), new Vector3f(45390, -36957, 14303)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50890, -42501, 14314), new Vector3f(54199, -37696, 14303)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(45326, -37232, 14303), new Vector3f(48101, -42259, 14314)));
            Rooms.Add(layout);

            // 4'th Room, long platform
            layout = new RoomLayout(keys[4]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(52483, -39739, 17498), new Vector3f(51113, -38894, 17498)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49968, -38949, 17498), new Vector3f(45421, -39878, 17498)));
            Rooms.Add(layout);
            #endregion


            for(int i = 0; i < platformLayouts.Count; i++) {
                platformLayouts[i].angle = ToQuaternion(platformLayouts[i].angle);
            }


            ////// JumpPads //////
            #region JumpPad
            NonPlaceableObject jumppad =  new JumpPad(0xB78);//5000
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(90.0f, -60.0f, 0.0f), Speed = 6000});//backwards 1 platfrom
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(120.0f, -60.0f, 0.0f), Speed = 8000}.SetRarity(0.5));//backwards 2 platform
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(0.0f, -92.0f, 0.0f), Speed = 8500 }.SetRarity(0.35));//3 platfrom
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(0.0f, -92.0f, 0.0f), Speed = 9500 }.SetRarity(0.2));//to the last jumppad
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(0.0f, -95.0f, 0.0f), Speed = 14000 }.SetRarity(0.2));//to the EOL
            worldObjects.Add(jumppad);

            CustomCheckPoints.Add(new CustomCP(mapType, new Vector3f(48575, -35900, 8230), new Vector3f(50500, -34860, 9450),
            new Vector3f(49740, -40875, 8650), new Angle(0.71f, 0.71f)));

            jumppad = new JumpPad(0xB70);//5000
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(90.0f, -50.0f, 0.0f), Speed = 6000 });//front without cp
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(-11.0f, -40.0f, 0.0f), Speed = 8200 });//3 green platforms
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(-135.0f, -40.0f, 0.0f), Speed = 6500 });//left side
            //jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(-90.0f, -37.0f, 0.0f), Speed = 8000 }.SetRarity(0.2));//for hardcore
            worldObjects.Add(jumppad);

            CustomCheckPoints.Add(new CustomCP(mapType, new Vector3f(45000, -32900, 11000), new Vector3f(46350, -33875, 12000),
            new Vector3f(49655, -34680, 11550), new Angle(-0.71f, 0.71f)));
            CustomCheckPoints.Add(new CustomCP(mapType, new Vector3f(45000, -40780, 11130), new Vector3f(46180, -39500, 11775),
            new Vector3f(49655, -34680, 11550), new Angle(-0.71f, 0.71f)));
            CustomCheckPoints.Add(new CustomCP(mapType, new Vector3f(52345, -37325, 12145), new Vector3f(54945, -33385, 13470),
            new Vector3f(49655, -34680, 11550), new Angle(-0.71f, 0.71f)));

            jumppad = new JumpPad(0xB68);//5000
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(-90.0f, -50.0f, 0.0f), Speed = 6000 });//front without cp
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(0.0f, -90.0f, 0.0f), Speed = 10200 }.SetRarity(0.5));// last jumppad
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(0.0f, -90.0f, 0.0f), Speed = 13200 }.SetRarity(0.25));//to the EOL
            worldObjects.Add(jumppad);

            jumppad = new JumpPad(0xB60);//5000
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(0.0f, -90.0f, 0.0f), Speed = 6000 });//1 up
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(-125.0f, -30.0f, 0.0f), Speed = 12200 }.SetRarity(0.25));// hard jump, easy with sideway jump
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(25.0f, -60.0f, 0.0f), Speed = 7500 }.SetRarity(0.35));//2 up
            worldObjects.Add(jumppad);

            jumppad = new JumpPad(0xB88);//5000
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(65.0f, -60.0f, 0.0f), Speed = 5500 });//1 up
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(135.0f, -45.0f, 0.0f), Speed = 8200 });// right side
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(25.0f, -40.0f, 0.0f), Speed = 6500 });//left side up
            worldObjects.Add(jumppad);

            jumppad = new JumpPad(0x6a8);//3500
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(-36.0f, -20.0f, 0.0f), Speed = 8500 });//climb on other side
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(-33.0f, -20.0f, 0.0f), Speed = 13200 }.SetRarity(0.35));// bounce jump,sideways(can make to the last jumppad)
            worldObjects.Add(jumppad);

            CustomCheckPoints.Add(new CustomCP(mapType, new Vector3f(53067, -35725, 19143), new Vector3f(54065, -36610, 20550),
            new Vector3f(50660, -34400, 19600), new Angle(0.0f, 1.0f)));

            jumppad = new JumpPad(0xB80);//10000
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(0.0f, -90.0f, 0.0f), Speed = 9280 });//short
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(0.0f, -90.0f, 0.0f), Speed = 150000 });//teleport
            worldObjects.Add(jumppad);
            #endregion
        }

        private QuaternionAngle ToQuaternion(Angle angle) {
            double angleRadian = (angle.angleSin > 0) ? Math.Acos(angle.angleCos) : -Math.Acos(angle.angleCos);
            return new QuaternionAngle((float)(angleRadian * 180 / Math.PI) * 2, 0, 0);
        }

        public override void RandomizeEnemies(Process game) {
            base.RandomizeEnemies(game);
            // Platforms RNG
            if(platforms.Count == 0 || platformLayouts.Count == 0 || platformLayouts.Count < platforms.Count) return;

            int r = Config.GetInstance().r.Next(platformLayouts.Count / platforms.Count);
            for(int i = 0; i < platforms.Count; i++) {
                platforms[i].SetMemoryPos(game, platformLayouts[platforms.Count * r + i]);
            }
        }
    }
}
