using GhostrunnerRNG.Game;
using GhostrunnerRNG.GameObjects;
using GhostrunnerRNG.MapGen;
using System.Collections.Generic;
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
            platforms.Add(new GreenPlatform(0x9F8, 0xDB));
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
