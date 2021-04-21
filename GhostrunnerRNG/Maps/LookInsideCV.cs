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

            ////// Platforms layouts //////
            #region GreenPlatforms
            //// going around right, splitting in middle
            platformLayouts.Add(new SpawnData(new Vector3f(50232.93f, -10371.06f, 2988.9f), new Angle(1.00f, 0.00f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50746.19f, -10623.07f, 3123.783f), new Angle(-0.45f, 0.89f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51239.04f, -11337.36f, 3209.209f), new Angle(-0.72f, 0.70f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51176.75f, -12164.8f, 3406.602f), new Angle(-0.71f, 0.71f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51293.42f, -12759.18f, 3573.017f), new Angle(-0.48f, 0.87f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51762.2f, -13277.17f, 3737.203f), new Angle(-0.28f, 0.96f)));
            platformLayouts.Add(new SpawnData(new Vector3f(52215.43f, -13650.36f, 3941.672f), new Angle(-0.65f, 0.76f)));
            platformLayouts.Add(new SpawnData(new Vector3f(52200.01f, -14317.66f, 4140.634f), new Angle(-0.72f, 0.70f)));
            platformLayouts.Add(new SpawnData(new Vector3f(52099.57f, -14987.48f, 4318.261f), new Angle(-0.76f, 0.65f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51784.11f, -15663.53f, 4455.74f), new Angle(-0.88f, 0.48f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51317.89f, -16294.11f, 4584.072f), new Angle(-0.89f, 0.45f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50883.04f, -16784.74f, 4716.091f), new Angle(-0.85f, 0.52f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50659.76f, -17336.82f, 4764.241f), new Angle(-0.76f, 0.65f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50223.77f, -18020.39f, 4812.391f), new Angle(-0.82f, 0.57f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50428.26f, -12785.69f, 3529.526f), new Angle(-1.00f, 0.02f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49754.46f, -13154.93f, 3900.068f), new Angle(-0.96f, 0.28f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49187.71f, -13739.8f, 4251.918f), new Angle(-0.91f, 0.42f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49155.66f, -14196.82f, 4731.175f), new Angle(-0.51f, 0.86f)));

            //// splits at start, 2 paths around
            platformLayouts.Add(new SpawnData(new Vector3f(49928, -10312, 3170), new Angle(-0.26f, 0.97f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50252, -10700, 3345), new Angle(-0.59f, 0.81f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50486, -11389, 3660), new Angle(-0.61f, 0.79f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50709, -12114, 3992), new Angle(-0.59f, 0.80f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50921, -12905, 4346), new Angle(-0.62f, 0.79f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51068, -13386, 4590), new Angle(-0.57f, 0.82f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51005, -14147, 4745), new Angle(-0.79f, 0.62f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50599, -14875, 4823), new Angle(-0.86f, 0.50f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50122, -15624, 4883), new Angle(-0.89f, 0.45f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48998, -10505, 3185), new Angle(-0.94f, 0.35f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48649, -10795, 3203), new Angle(0.35f, 0.94f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48388, -11375, 3390), new Angle(-0.74f, 0.67f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48372, -12026, 3607), new Angle(-0.67f, 0.74f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48418, -12612, 3793), new Angle(-0.68f, 0.74f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48496, -13244, 4021), new Angle(-0.65f, 0.76f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48564, -13815, 4264), new Angle(-0.62f, 0.78f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48692, -14364, 4559), new Angle(-0.29f, 0.96f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49049, -14572, 4826), new Angle(-0.25f, 0.97f)));

            //// stair-like on main platform
            platformLayouts.Add(new SpawnData(new Vector3f(49656f, -10212f, 3430f), new Angle(1.00f, 0.01f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49294f, -10239f, 3790f), new Angle(0.68f, 0.73f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49335f, -9567f, 3732f), new Angle(0.70f, 0.72f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49368f, -8924f, 3829f), new Angle(0.32f, 0.95f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49854f, -8735f, 4073f), new Angle(0.06f, 1.00f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50363f, -8889f, 4317f), new Angle(-0.34f, 0.94f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50665f, -9431f, 4532f), new Angle(-0.66f, 0.75f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50815f, -10256f, 4561f), new Angle(-0.64f, 0.77f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50789f, -11138f, 4572f), new Angle(-0.80f, 0.60f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50577f, -11889f, 4677f), new Angle(-0.80f, 0.60f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50588f, -12653f, 4775f), new Angle(-0.70f, 0.72f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50730f, -13350f, 4952f), new Angle(-0.62f, 0.79f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50693f, -14090f, 5075f), new Angle(-0.83f, 0.55f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50364f, -14597f, 4911f), new Angle(-0.88f, 0.48f)));
            //  hide rest
            platformLayouts.Add(new SpawnData(new Vector3f(45468f, -10094f, 4457f), new Angle(-0.03f, 1.00f)));
            platformLayouts.Add(new SpawnData(new Vector3f(45468f, -10094f, 4457f), new Angle(-0.03f, 1.00f)));
            platformLayouts.Add(new SpawnData(new Vector3f(45468f, -10094f, 4457f), new Angle(-0.03f, 1.00f)));
            platformLayouts.Add(new SpawnData(new Vector3f(45468f, -10094f, 4457f), new Angle(-0.03f, 1.00f)));

            //// early stairs, around left side
            platformLayouts.Add(new SpawnData(new Vector3f(49422f, -8528f, 3183f), new Angle(-0.71f, 0.70f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49422f, -8894f, 3388f), new Angle(-0.70f, 0.71f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49405f, -9388f, 3622f), new Angle(-0.71f, 0.70f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49411f, -9903f, 3865f), new Angle(-0.70f, 0.71f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49411f, -10469f, 4102f), new Angle(-0.71f, 0.71f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49411f, -10982f, 4353f), new Angle(-0.71f, 0.71f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49350f, -11762f, 4421f), new Angle(-0.71f, 0.71f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49346f, -12423f, 4632f), new Angle(-0.71f, 0.71f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48628f, -12405f, 4777f), new Angle(0.02f, 1.00f)));
            platformLayouts.Add(new SpawnData(new Vector3f(47640f, -12444f, 4825f), new Angle(0.00f, 1.00f)));
            platformLayouts.Add(new SpawnData(new Vector3f(47637f, -13124f, 4873f), new Angle(0.00f, 1.00f)));
            platformLayouts.Add(new SpawnData(new Vector3f(47666f, -13921f, 4921f), new Angle(0.71f, 0.70f)));
            platformLayouts.Add(new SpawnData(new Vector3f(47668f, -14622f, 4969f), new Angle(0.71f, 0.70f)));
            platformLayouts.Add(new SpawnData(new Vector3f(47692f, -15471f, 5017f), new Angle(0.71f, 0.70f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48138f, -15964f, 5065f), new Angle(-0.36f, 0.93f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48829f, -16554f, 4893f), new Angle(-0.34f, 0.94f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49241f, -16973f, 4857f), new Angle(-0.39f, 0.92f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49531f, -17340f, 4829f), new Angle(-0.60f, 0.80f)));

            //// Inverted spiral
            platformLayouts.Add(new SpawnData(new Vector3f(49701f, -11128f, 3153f), new Angle(-0.42f, 0.91f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50386f, -11533f, 3242f), new Angle(-0.18f, 0.98f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51231f, -11638f, 3295f), new Angle(0.09f, 1.00f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51873f, -11280f, 3376f), new Angle(0.30f, 0.95f)));
            platformLayouts.Add(new SpawnData(new Vector3f(52293f, -10676f, 3505f), new Angle(0.56f, 0.83f)));
            platformLayouts.Add(new SpawnData(new Vector3f(52207f, -9967f, 3622f), new Angle(0.84f, 0.55f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51557f, -9450f, 3694f), new Angle(0.98f, 0.21f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50811f, -9420f, 3771f), new Angle(-0.99f, 0.13f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50087f, -9632f, 3868f), new Angle(-0.99f, 0.17f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49424f, -10042f, 3995f), new Angle(-0.94f, 0.33f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48780f, -10568f, 4038f), new Angle(-0.92f, 0.39f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48397f, -11260f, 4129f), new Angle(-0.81f, 0.59f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48361f, -11940f, 4291f), new Angle(-0.68f, 0.73f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48172f, -12515f, 4507f), new Angle(-0.84f, 0.54f)));
            platformLayouts.Add(new SpawnData(new Vector3f(47852f, -13104f, 4717f), new Angle(-0.79f, 0.62f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48050f, -13880f, 5012f), new Angle(-0.59f, 0.80f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48421f, -14681f, 4868f), new Angle(-0.42f, 0.91f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49052f, -15132f, 4823f), new Angle(-0.22f, 0.97f)));

            //// high step stairs
            platformLayouts.Add(new SpawnData(new Vector3f(49863f, -10249f, 3185f), new Angle(-0.01f, 1.00f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50313f, -10302f, 3641f), new Angle(-0.04f, 1.00f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50613f, -10331f, 4008f), new Angle(-0.03f, 1.00f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51077f, -10372f, 4508f), new Angle(0.00f, 1.00f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51078f, -10772f, 4969f), new Angle(-0.71f, 0.70f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51072f, -11467f, 4203f), new Angle(-0.69f, 0.73f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51206f, -12384f, 4507f), new Angle(-0.74f, 0.67f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51601f, -13060f, 4675f), new Angle(-0.49f, 0.87f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51767f, -13788f, 4824f), new Angle(-0.79f, 0.61f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51307f, -14580f, 4906f), new Angle(-0.90f, 0.43f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50667f, -15140f, 4863f), new Angle(-0.95f, 0.32f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50198f, -15678f, 4783f), new Angle(-0.88f, 0.47f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51720f, -10804f, 5462f), new Angle(-0.01f, 1.00f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51666f, -12065f, 5532f), new Angle(-0.71f, 0.70f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51501f, -12952f, 5554f), new Angle(-0.76f, 0.65f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51309f, -13913f, 5376f), new Angle(-0.79f, 0.62f)));
            // hide
            platformLayouts.Add(new SpawnData(new Vector3f(45468f, -10094f, 4457f), new Angle(-0.03f, 1.00f)));
            platformLayouts.Add(new SpawnData(new Vector3f(45468f, -10094f, 4457f), new Angle(-0.03f, 1.00f)));


            //// down drop
            platformLayouts.Add(new SpawnData(new Vector3f(49582f, -10629f, 3170f), new Angle(-0.71f, 0.71f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49576f, -10914f, 3218f), new Angle(-0.72f, 0.69f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49562f, -11194f, 2615f), new Angle(0.68f, 0.73f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49589f, -11915f, 2283f), new Angle(0.72f, 0.70f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49300f, -12588f, 2331f), new Angle(0.50f, 0.86f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48933f, -13232f, 2380f), new Angle(0.50f, 0.86f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48533f, -13917f, 2674f), new Angle(0.50f, 0.87f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48192f, -14490f, 2877f), new Angle(0.49f, 0.87f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48006f, -15053f, 3024f), new Angle(0.62f, 0.79f)));
            platformLayouts.Add(new SpawnData(new Vector3f(47854f, -15695f, 3302f), new Angle(0.62f, 0.78f)));
            platformLayouts.Add(new SpawnData(new Vector3f(47739f, -16225f, 3643f), new Angle(0.67f, 0.74f)));
            platformLayouts.Add(new SpawnData(new Vector3f(47716f, -16740f, 3866f), new Angle(0.69f, 0.72f)));
            platformLayouts.Add(new SpawnData(new Vector3f(47880f, -17336f, 4067f), new Angle(0.79f, 0.61f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48062f, -17837f, 4349f), new Angle(0.84f, 0.55f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48393f, -18410f, 4501f), new Angle(0.88f, 0.47f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48788f, -18990f, 4681f), new Angle(0.90f, 0.44f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49206f, -19520f, 4820f), new Angle(-0.46f, 0.89f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49448f, -19729f, 4828f), new Angle(-0.58f, 0.82f)));


            //// left, to middle and around the right
            platformLayouts.Add(new SpawnData(new Vector3f(48944f, -10468f, 3179f), new Angle(-0.97f, 0.23f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48584f, -10659f, 3344f), new Angle(-0.95f, 0.30f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48016f, -11026f, 3478f), new Angle(-0.95f, 0.31f)));
            platformLayouts.Add(new SpawnData(new Vector3f(47618f, -11548f, 3628f), new Angle(-0.81f, 0.59f)));
            platformLayouts.Add(new SpawnData(new Vector3f(47525f, -12080f, 3718f), new Angle(0.70f, 0.71f)));
            platformLayouts.Add(new SpawnData(new Vector3f(48329f, -12072f, 3755f), new Angle(-1.00f, 0.00f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49029f, -12035f, 3794f), new Angle(-1.00f, 0.00f)));
            platformLayouts.Add(new SpawnData(new Vector3f(49699f, -12038f, 3832f), new Angle(1.00f, 0.01f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50417f, -12055f, 3850f), new Angle(1.00f, 0.02f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51136f, -12137f, 3819f), new Angle(1.00f, 0.03f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51832f, -12443f, 3965f), new Angle(0.96f, 0.29f)));
            platformLayouts.Add(new SpawnData(new Vector3f(52026f, -12993f, 4105f), new Angle(0.77f, 0.63f)));
            platformLayouts.Add(new SpawnData(new Vector3f(52078f, -13592f, 4237f), new Angle(0.70f, 0.71f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51743f, -14260f, 4361f), new Angle(0.54f, 0.84f)));
            platformLayouts.Add(new SpawnData(new Vector3f(51316f, -14702f, 4477f), new Angle(0.40f, 0.92f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50878f, -15056f, 4650f), new Angle(0.31f, 0.95f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50412f, -15582f, 4868f), new Angle(-0.93f, 0.37f)));
            platformLayouts.Add(new SpawnData(new Vector3f(50042f, -15943f, 4774f), new Angle(-0.92f, 0.38f)));

            #endregion

            // Fix green platform rotations by converting it to quaternion rotation
            for(int i = 0; i < platformLayouts.Count; i++) {
                platformLayouts[i].angle = ToQuaternion(platformLayouts[i].angle);
            }

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
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Angle = new QuaternionAngle(0.0f, -90.0f, 0.0f), Speed = 9350 });//short
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
            Console.WriteLine(r);
            for(int i = 0; i < platforms.Count; i++) {
                platforms[i].SetMemoryPos(game, new SpawnData(platformLayouts[platforms.Count * r  + i].pos, platformLayouts[platforms.Count * r + i].angle));
            }
        }
    }
}
