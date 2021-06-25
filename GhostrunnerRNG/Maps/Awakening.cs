using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.MemoryUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.Maps {
    public class Awakening : MapCore, IModes{

        #region Rooms
        private Room room1 = new Room(new Vector3f(-11808, -61173, 4482), new Vector3f(-17950, -64478, 2243)); // 1 pistol
        private Room room2 = new Room(new Vector3f(-17778, -60368, 3908), new Vector3f(-14404, -58115, 2150)); // sensory boost, 1 pistol
        private Room room3 = new Room(new Vector3f(-12619, -57704, 5101), new Vector3f(-7309, -51494, 2431)); // 3 pistol, before bridge
        private Room room4 = new Room(new Vector3f(5589, -60246, 720), new Vector3f(7202, -73089, 2402)); // 3 pistol, hallway
        private Room room5 = new Room(new Vector3f(4871, -76740, 2678), new Vector3f(-3510, -67504, -1166));  // 5 pistols, dome
        #endregion

        #region HC Rooms
        // HC Rooms
        private Room hc_room1 = new Room(new Vector3f(-17620, -62069, 4280), new Vector3f(-6683, -68959, 1248)); //pistol, 2 uzi,  weeb, frogger - no cp
        private Room hc_room2 = new Room(new Vector3f(-21402, -65468, 2247), new Vector3f(-32819, -58079, 5706)); // frogger,  pistol,  waver - no cp
        private Room hc_room3 = new Room(new Vector3f(-12335, -57229, 5578), new Vector3f(-5492, -48695, 2535)); // pistol,  uzi,  frogger, shielder, shifter - no cp
        private Room hc_room4 = new Room(new Vector3f(-3039, -53776, 5166), new Vector3f(7115, -62748, 788)); // drone, uzi, pistol - no cp
        private Room hc_room5 = new Room(new Vector3f(4844, -62911, 3386), new Vector3f(7626, -72804, 1401)); // pistol, waver, shielder, frogger
        private Room hc_room6 = new Room(new Vector3f(4659, -76660, 3230), new Vector3f(-3545, -67205, -1210)); // orb, drone, uzi, shielder, frogger, shifter
        #endregion


        #region HC LookInside Rooms
        private Room LookInside = new Room(new Vector3f(130592, -76528, -6355), new Vector3f(175651, -27455, 24290));
        private Room LookInisde_room1 = new Room(new Vector3f(143382, -71435, -88), new Vector3f(158600, -51257, 6221)); // 2 drones, 3 turrets, 2 shielders, froggers
        private Room LookInisde_room2 = new Room(new Vector3f(149989, -49756, 2730), new Vector3f(155288, -47062, 4413)); // 2 shifters
        private Room LookInisde_room3 = new Room(new Vector3f(152013, -46561, 2610), new Vector3f(160768, -40716, 5004)); // orb, shielder, frogger
        #endregion


        private bool IsNewGame = false;

        public Awakening() : base(MapType.Awakening) {
            CheckNewGame();
        }

        private void CheckNewGame() {
            DeepPointer bestTimeDP = new DeepPointer(PtrDB.DP_Awakening_BestTime);
            IntPtr bestTimePtr;
            bestTimeDP.DerefOffsets(GameHook.game, out bestTimePtr);
            float time;
            GameHook.game.ReadValue<float>(bestTimePtr, out time);
            IsNewGame = time < 1;
        }

        public void Gen_Normal() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game, 0, 13); // static range, no gap
            Rooms = new List<RoomLayout>();
            RoomLayout layout;
            List<Enemy> enemies;

            //// room 1 - pistol
            enemies = room1.ReturnEnemiesInRoom(AllEnemies);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, removeCP: false); // take pistol, force, no cp/door attached


            //// room 2 - pistol, sensory boost
            if(!IsNewGame) { // skip rng if new game
                enemies = room2.ReturnEnemiesInRoom(AllEnemies);
                RandomPickEnemiesWithoutCP(ref enemies); // chance to move enemy to EnemiesWithoutCP
                if(enemies != null && enemies.Count > 0) {
                    layout = new RoomLayout(enemies); // minimal rng
                    layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-14496, -59107, 2803), new Vector3f(-15633, -60168, 2799), new Angle(-1.00f, 0.09f)));
                    layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-15826, -58998, 2889), new Angle(-0.99f, 0.17f)));
                    Rooms.Add(layout);
                }
            }

            //// room 3 - 3 pistol, before bridge
            enemies = room3.ReturnEnemiesInRoom(AllEnemies);
            DetachEnemyFromCP(ref enemies, force: false);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8179, -54300, 3408), new Vector3f(-9091, -52688, 3402), new Angle(-0.9f, 0.43f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8309, -55792, 3408), new Vector3f(-10221, -56789, 3408), new Angle(1.0f, 0.01f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9798, -53058, 3402), new Vector3f(-10775, -54270, 3408), new Angle(-0.96f, 0.28f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-12762, -55235, 4398), new Angle(-0.98f, 0.22f)));
            // special/high spots
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8122, -54648, 3911), new Angle(-0.99f, 0.17f)).setDiff(1)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-10040, -55579, 4030), new Angle(-0.99f, 0.15f)).setDiff(1)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-7336, -57117, 3408), new Angle(1.00f, 0.08f))); // SR route
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9023, -57121, 4195), new Angle(0.99f, 0.15f)).setDiff(1)); // wall lamp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9582, -57001, 3788), new Angle(0.97f, 0.26f))); // small platform
            Rooms.Add(layout);


            //// Room 4 - 3 pistol, hallway
            enemies = room4.ReturnEnemiesInRoom(AllEnemies);
            DetachEnemyFromCP(ref enemies, force: false);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(5740, -63956, 1468), new Vector3f(6337, -64859, 1468), new Angle(0.72f, 0.7f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(5771, -66227, 1678), new Vector3f(6538, -67229, 1678), new Angle(0.73f, 0.68f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(5656, -68518, 1868), new Vector3f(6591, -69532, 1868), new Angle(0.73f, 0.68f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6648, -70563, 1798), new Vector3f(5848, -71930, 1798), new Angle(0.69f, 0.73f)));
            // special/high spots
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(5699, -61602, 2065), new Angle(0.83f, 0.56f))); // first small wall/building
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6764, -62525, 2116), new Angle(0.89f, 0.46f)).setDiff(1)); // first billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6779, -69590, 2587), new Angle(0.83f, 0.56f)).setDiff(1)); // last billboard 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6404, -64078, 2241), new Angle(0.73f, 0.68f)).setDiff(1)); // pipe
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6676, -72123, 2088), new Vector3f(5889, -72129, 2088), new Angle(0.69f, 0.72f)).AsVerticalPlane()); // slide pipe
            Rooms.Add(layout);


            //// Room 5 - 5 enemies, dome
            enemies = room5.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(4603, -70321, -346), new Vector3f(3303, -68684, -350), new Angle(-0.71f, 0.71f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(5128, -71555, 307), new Vector3f(3678, -71883, 307), new Angle(-0.71f, 0.71f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(997, -70468, -41), new Vector3f(1689, -68394, -41)).RandomAngle());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(910, -72873, -160), new Vector3f(1698, -72192, -167), new Angle(-0.45f, 0.89f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(955, -76167, 362), new Vector3f(1578, -75331, 371), new Angle(0f, 1.00f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2142, -75881, -42), new Vector3f(-1468, -73341, -42), new Angle(0.12f, 0.99f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-280, -68970, -41), new Vector3f(-1275, -68147, -41), new Angle(-0.34f, 0.94f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2226, -69843, -81), new Vector3f(-1017, -70568, -81), new Angle(-0.24f, 0.97f)));

            // high/special spots
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(3256, -68387, 237), new Angle(-0.61f, 0.79f))); // neon sign platform, far bottom
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(3373, -72039, 924), new Angle(-0.64f, 0.77f))); // street light
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(1943, -75088, 1008), new Angle(-0.26f, 0.97f))); // big pipe, right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1100, -74848, 340)).RandomAngle()); // small platform, right bot
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2033, -72982, 605), new Angle(-0.18f, 0.98f)).setDiff(1)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1339, -71031, 605), new Angle(-0.37f, 0.93f)).setDiff(1)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2744, -71368, 473), new Angle(0.00f, 1.00f))); // fuel tank
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(891, -70099, 584), new Angle(-0.59f, 0.80f))); // street light
            Rooms.Add(layout);

            //// EXTRA spots
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-14200, -63865, 2798), new Vector3f(-14996, -62722, 2798)).RandomAngle().setRarity(0.2)); // pistol 1, default pos
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-14729, -63919, 3591), new Angle(0.03f, 1.0f)).setDiff(1)); // pistol 1, wall lamp above 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-18060, -64136, 3598), new Vector3f(-19392, -63758, 3601), new Angle(0f, 1f))); // vent area
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-21681, -63576, 2998), new Angle(-1.00f, 0.05f))); // after first vent, left corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-20723, -59412, 4298), new Vector3f(-21563, -60163, 4302), new Angle(-0.99f, 0.15f))); // before sensory boost vent
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3991, -70323, -80), new Angle(0.01f, 1.00f))); // before elevator (surprise)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-16471, -56114, 4098), new Angle(-0.78f, 0.62f))); // after sensory boost
            Rooms.Add(layout);
        }

        public void Gen_Easy() {Gen_Normal();}

        public void Gen_Nightmare() {
            // GEN - Nightmare (classic levels)

            // GEN - SR routes
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game, 0, 13); // static range, no gap
            Rooms = new List<RoomLayout>();
            RoomLayout layout;
            List<Enemy> enemies;

            //// room 1 - pistol
            enemies = room1.ReturnEnemiesInRoom(AllEnemies);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, removeCP: false); // take pistol, force, no cp/door attached

            //// room 3 - 3 pistol, before bridge
            enemies = room3.ReturnEnemiesInRoom(AllEnemies);
            DetachEnemyFromCP(ref enemies, force: false);
            layout = new RoomLayout(enemies);
            // special/high spots
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-12850, -56073, 4908), new Angle(-1.00f, 0.03f))); // door frame
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11519, -54938, 5039), new Angle(-0.98f, 0.18f))); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-10337, -54556, 4030), new Angle(-0.97f, 0.23f))); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9410, -54661, 5294), new Angle(-0.92f, 0.38f))); // high spot ad
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8988, -57174, 4195), new Angle(0.98f, 0.22f))); // wall lamp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8100, -55539, 3971), new Angle(-0.99f, 0.11f))); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-7492, -50434, 4798), new Angle(-0.89f, 0.45f))); // above bridge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-12441, -54370, 5670), new Angle(-0.33f, 0.95f))); // generator above entrance
            Rooms.Add(layout);

            //// Room 4 - 3 pistol, hallway
            enemies = room4.ReturnEnemiesInRoom(AllEnemies);
            DetachEnemyFromCP(ref enemies, force: false);
            layout = new RoomLayout(enemies);
            // special/high spots
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(5839, -61841, 2457), new Angle(0.76f, 0.65f))); // neon sign, first building/platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(5591, -65940, 2138), new Angle(0.71f, 0.71f))); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6417, -64113, 2239), new Angle(0.76f, 0.65f))); // pipes on right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6412, -72908, 1798), new Angle(0.72f, 0.70f))); // after pipes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6676, -72123, 2088), new Vector3f(5889, -72129, 2088), new Angle(0.69f, 0.72f)).AsVerticalPlane()); // slide pipe
            Rooms.Add(layout);

            //// Room 5 - 5 enemies, dome
            enemies = room5.ReturnEnemiesInRoom(AllEnemies);
            DetachEnemyFromCP(ref enemies, force: false);
            layout = new RoomLayout(enemies);
            // high/special spots
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(3256, -68387, 237), new Angle(-0.61f, 0.79f))); // neon sign platform, far bottom
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(3373, -72039, 924), new Angle(-0.64f, 0.77f))); // street light
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(1943, -75088, 1008), new Angle(-0.26f, 0.97f))); // big pipe, right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1100, -74848, 340)).RandomAngle()); // small platform, right bot
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2744, -71368, 473), new Angle(0.00f, 1.00f))); // fuel tank
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(891, -70099, 584), new Angle(-0.59f, 0.80f))); // street light
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(319, -69641, 1223), new Angle(-0.51f, 0.86f))); // above middle pos, between beams (double fuel tanks)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(4606, -75429, 2298), new Angle(-1.00f, 0.03f))); // above spawn
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(1630, -71496, 2080), new Angle(-0.70f, 0.71f))); // middle of dome, high generator
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2451, -68522, 1842), new Angle(-0.39f, 0.92f))); // high pipes, above exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-964, -67398, 1411), new Angle(-0.83f, 0.56f))); // generator
            Rooms.Add(layout);

            //// EXTRA spots
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-17894, -64016, 4128), new Angle(0.08f, 1.00f))); // before first vent, on small beam, above entrance
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-27278, -64081, 3021), new Angle(0.01f, 1.00f))); // on big fan
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-20563, -58060, 4732), new Angle(-0.84f, 0.54f))); // before sensory boost, on right wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8906, -76819, 2291), new Vector3f(7531, -76705, 2291), new Angle(0.74f, 0.67f)).AsVerticalPlane().SetMaxEnemies(2)); // before dome, dsj beam
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4309, -70324, -80), new Angle(0.02f, 1.00f)));  // before elevator
            Rooms.Add(layout);
        }

        public void Gen_Hardcore() {
            // Gen - Hardcore(for in-game Hardcore mode, requires special attention)
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game, 0, 39); 
            Rooms = new List<RoomLayout>();
            RoomLayout layout;
            List<Enemy> enemies;

            // room 1
            enemies = hc_room1.ReturnEnemiesInRoom(AllEnemies);
            enemies[4].SetEnemyType(Enemy.EnemyTypes.Weeb);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, removeCP: false);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-19224, -64141, 3598), new Vector3f(-18365, -63753, 3598), new Angle(0.02f, 1.00f)).Mask(SpawnPlane.Mask_Flatground)); // area before vent
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-12260, -64194, 3099), new Vector3f(-11507, -63194, 3099), new Angle(-0.28f, 0.96f)).Mask(SpawnPlane.Mask_Flatground)); // fakeMantle platform

            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9969, -67666, 2778), new Vector3f(-10971, -67635, 2778), new Angle(0.97f, 0.23f))
                .Mask(SpawnPlane.Mask_Highground).AsVerticalPlane()); // billboard left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9704, -63406, 3296), new Vector3f(-10746, -63403, 3296), new Angle(-0.66f, 0.75f))
                .Mask(SpawnPlane.Mask_Highground).AsVerticalPlane()); // bilboard right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-13559, -64348, 3908), new Angle(0.21f, 0.98f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // billboard before vent 1 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-14738, -63855, 3590), new Angle(0.03f, 1.00f)).Mask(SpawnPlane.Mask_HighgroundLimited));  // billboard before vent 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9238, -63303, 3193), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_Highground)); // near 'billboard right'
            Rooms.Add(layout);


            // room 2
            enemies = hc_room2.ReturnEnemiesInRoom(AllEnemies);
            enemies[1].SetEnemyType(Enemy.EnemyTypes.Waver);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, removeCP: false);
            layout = new RoomLayout(enemies);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-26815, -64787, 2998), new Vector3f(-26243, -63621, 2998), new Angle(-0.03f, 1.00f)).Mask(SpawnPlane.Mask_Flatground)); // before fan
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29552, -61256, 3798), new Angle(-0.62f, 0.79f)).Mask(SpawnPlane.Mask_Highground)); // after fan, platform ledge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-20779, -60233, 4298), new Vector3f(-21916, -59283, 4298), new Angle(-1.00f, 0.07f)).Mask(SpawnPlane.Mask_Flatground)); // before vent
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-20364, -59342, 4708), new Angle(-0.96f, 0.27f)).Mask(SpawnPlane.Mask_Highground )); // vent frame
            Rooms.Add(layout);


            // room 3 - before big slide
            enemies = hc_room3.ReturnEnemiesInRoom(AllEnemies);
            //RandomPickEnemiesWithoutCP(ref enemies, force: true, removeCP: false, enemyIndexBesides: 1); // besides shifter
            var info = new ShifterSpawnInfo();
            info.shiftPoints = new List<Tuple<Vector3f, Angle>>() {
                new Tuple<Vector3f, Angle>(new Vector3f(-11876, -56306, 4513), new Angle(0.03f, 1.00f)),
                new Tuple<Vector3f, Angle>(new Vector3f(-11181, -53500, 3772), new Angle(-0.56f, 0.83f)),
                new Tuple<Vector3f, Angle>(new Vector3f(-9615, -56984, 3788), new Angle(0.90f, 0.43f)),
            };

            enemies[1] = new EnemyShifter(enemies[1], 3).AddFixedSpawnInfo(info);
            layout = new RoomLayout(enemies);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8414, -56026, 3402), new Vector3f(-9987, -56690, 3402), new Angle(-1.00f, 0.02f)).Mask(SpawnPlane.Mask_Flatground)); // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11531, -54906, 5040), new Angle(-0.99f, 0.17f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-10315, -54501, 4030), new Angle(-0.99f, 0.17f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9994, -55723, 4030), new Angle(-1.00f, 0.09f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8131, -54614, 3970), new Angle(-1.00f, 0.08f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8112, -55647, 3970), new Angle(-1.00f, 0.06f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9004, -57100, 4195), new Angle(0.94f, 0.34f)).Mask(SpawnPlane.Mask_Highground)); // wall lamp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-10356, -52803, 4591), new Angle(-0.78f, 0.63f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // right wall ledge
            Rooms.Add(layout);


            // room 4
            enemies = hc_room4.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyDrone(enemies[0]);
            RemoveParentObjects(ref enemies);
            EnemiesWithoutCP.AddRange(enemies);

            // room 5
            enemies = hc_room5.ReturnEnemiesInRoom(AllEnemies);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Waver);
            DetachEnemyFromCP(ref enemies, force: true);
            layout = new RoomLayout(enemies);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6758, -62646, 2116), new Angle(0.87f, 0.50f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6421, -64019, 2238), new Angle(0.77f, 0.64f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // pipes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6814, -67090, 2383), new Angle(0.86f, 0.51f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6779, -69609, 2587), new Angle(0.85f, 0.53f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(5795, -68741, 2498), new Angle(0.68f, 0.73f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(7050, -66729, 1982), new Angle(0.96f, 0.28f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // roofotop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(5255, -67813, 2123), new Angle(0.47f, 0.88f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // small thin ad/sign
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6485, -69383, 1868), new Vector3f(5827, -68615, 1868), new Angle(0.71f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6450, -66512, 1681), new Vector3f(5834, -67095, 1678), new Angle(0.72f, 0.69f)).Mask(SpawnPlane.Mask_Flatground));
            Rooms.Add(layout);


            // room 6
            enemies = hc_room6.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[1] = new EnemyDrone(enemies[1]);

            // shifter points
            info = new ShifterSpawnInfo();
            info.shiftPoints = new List<Tuple<Vector3f, Angle>>() { // 3 street lights + billboard
                 new Tuple<Vector3f, Angle>(new Vector3f(3311, -71903, 969), new Angle(0.57f, 0.82f)),
                 new Tuple<Vector3f, Angle>(new Vector3f(906, -70209, 630), new Angle(-0.30f, 0.95f)),
                 new Tuple<Vector3f, Angle>(new Vector3f(-1086, -70504, 545), new Angle(-0.27f, 0.96f)),
                 new Tuple<Vector3f, Angle>(new Vector3f(-2056, -73103, 604), new Angle(-0.19f, 0.98f))
            };
            enemies[2] = new EnemyShifter(enemies[2], 4).AddFixedSpawnInfo(info);

            // orb
            layout = new RoomLayout(enemies.Take(1).Single());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-95, -74846, 966))); // top of billboard right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1265, -75711, 2797), new Vector3f(-1224, -75844, 1457)).AsVerticalPlane()); // top-up curved billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(895, -70257, 811))); // street light, middle
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2705, -68480, 1051))); // top of curve billboard, near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(4773, -71128, 1276))); // left billboard, high
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(2348, -70853, -2), new Vector3f(2340, -71890, 1275)).AsVerticalPlane()); // middle left wall/net
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(4054, -71784, -200))); // under slide
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2544, -69779, 289))); // near exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(673, -75451, 565), new Vector3f(-583, -75460, 912)).AsVerticalPlane()); // right small billboard
            layout.DoNotReuse();
            Rooms.Add(layout);

            // enemies
            enemies = enemies.Skip(1).ToList();
            layout = new RoomLayout(enemies);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1352, -70928, 605), new Angle(-0.32f, 0.95f)).Mask(SpawnPlane.Mask_Highground)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1349, -72207, 605), new Angle(-0.31f, 0.95f)).Mask(SpawnPlane.Mask_Highground)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2038, -71712, 605), new Angle(-0.36f, 0.93f)).Mask(SpawnPlane.Mask_Highground)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(863, -75103, 1018), new Vector3f(2206, -75094, 1008), new Angle(-0.03f, 1.00f)).AsVerticalPlane().Mask(SpawnPlane.Mask_Highground)); // pipe right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(198, -69741, 913), new Angle(-0.59f, 0.81f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // wall lamp, far back
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-391, -69741, 913), new Angle(-0.66f, 0.75f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // wall lamp, far back
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2805, -68600, 1051), new Angle(-0.47f, 0.88f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // curved billboard near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(744, -71932, 1408), new Angle(-0.45f, 0.89f))); // middle high billboard/fence
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2606, -74004, 630), new Angle(-0.07f, 1.00f))); // street light, far right corner

            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2230, -70553, -80), new Vector3f(-1202, -69921, -80), new Angle(-0.02f, 1.00f)).Mask(SpawnPlane.Mask_Flatground)); // near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(1130, -68547, -37), new Vector3f(1613, -70412, -40), new Angle(-0.31f, 0.95f)).Mask(SpawnPlane.Mask_Flatground)); // middle
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(4524, -71740, 307), new Angle(-0.92f, 0.39f)).Mask(SpawnPlane.Mask_Highground)); // slide middle platform

            // drone
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(3054, -72544, 787), new Vector3f(-1907, -73969, 787), new Angle(-0.22f, 0.97f)).Mask(SpawnPlane.Mask_Airborne)); // middle horizontal section
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(3838, -70256, 787), new Vector3f(-2210, -70176, 787), new Angle(-0.53f, 0.85f)).Mask(SpawnPlane.Mask_Airborne)); // back horizontal section
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(1456, -75623, 1655), new Angle(-0.02f, 1.00f)).Mask(SpawnPlane.Mask_Airborne)); // above right slide

            Rooms.Add(layout);


            //// EXTRA DRONES ////
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-13297, -63905, 3287), new Angle(0.01f, 1.00f))); // first section, after fakeMantle platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-16909, -63726, 3334), new Angle(-0.01f, 1.00f))); // before first vent
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-15556, -55873, 4360), new Angle(-0.91f, 0.41f))); // after sensory boost
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8338, -56423, 4049), new Vector3f(-9328, -55028, 4049), new Angle(1.00f, 0.02f))); // before big slide
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6013, -68097, 2075), new Vector3f(6087, -63280, 2075), new Angle(0.70f, 0.71f)).AsVerticalPlane()); // across "3pistol hallway"
            layout.Mask(SpawnPlane.Mask_Airborne);
            Rooms.Add(layout);

            //// EXTRA ////
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4540, -70299, -80), new Angle(-0.02f, 1.00f)).Mask(SpawnPlane.Mask_Flatground)); // before elevator door

            Rooms.Add(layout);




            ////////////////// Combine enemies from LookInside to Awakening //////////////////
            var LIenemiesAll = LookInside.ReturnEnemiesInRoom(AllEnemies);
            // remove any cp and attached doors 
            RemoveParentObjects(ref LIenemiesAll);


            // LookInside Room1
            var LIenemies = LookInisde_room1.ReturnEnemiesInRoom(LIenemiesAll);
            LIenemies[0] = new EnemyTurret(LIenemies[0]);
            LIenemies[1] = new EnemyTurret(LIenemies[1]);
            LIenemies[2] = new EnemyTurret(LIenemies[2]);
            LIenemies[3] = new EnemyDrone(LIenemies[3]);
            LIenemies[4] = new EnemyDrone(LIenemies[4]);
            EnemiesWithoutCP.AddRange(LIenemies.Skip(3).Take(2).ToList()); // both drones to EnemiesWithoutCP
            EnemiesWithoutCP.AddRange(LIenemies.Skip(5).Take(3).ToList()); // 3 normals

            LIenemies = LIenemies.Take(3).ToList(); // leave 3 turrets
            layout = new RoomLayout(LIenemies);

            // Last room, bottom right, scaffolding, aiming to door
            TurretSpawnInfo turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 0;
            turretSpawn.HorizontalSpeed = 50;
            turretSpawn.HorizontalAngle = 45;
            turretSpawn.SetRange(TurretSpawnInfo.DefaultRange);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1082, -74877, 339), new Angle(0.76f, 0.65f)).SetSpawnInfo(turretSpawn));

            // Last room, exit door platform, aiming to middle
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 10;
            turretSpawn.HorizontalSpeed = 30;
            turretSpawn.HorizontalAngle = 35;
            turretSpawn.SetRange(TurretSpawnInfo.DefaultRange);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-975, -70054, -80), new Angle(-0.02f, 1.00f)).SetSpawnInfo(turretSpawn));

            // Last room, left bottom, aiming to middle/slide
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 30;
            turretSpawn.HorizontalSpeed = 30;
            turretSpawn.HorizontalAngle = 40;
            turretSpawn.SetRange(TurretSpawnInfo.DefaultRange);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(4739, -70409, -350), new Angle(1.00f, 0.02f)).SetSpawnInfo(turretSpawn));

            // Last room, middle metal wall, aiming to door
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 20;
            turretSpawn.HorizontalSpeed = 40;
            turretSpawn.HorizontalAngle = 30;
            turretSpawn.SetRange(TurretSpawnInfo.DefaultRange);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-66, -69760, 256), new QuaternionAngle(0.00f, -0.71f, 0.71f, 0.00f)).SetSpawnInfo(turretSpawn));


            // Before big slide, right far platform edge, aiming to middile platform
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 10;
            turretSpawn.HorizontalSpeed = 30;
            turretSpawn.HorizontalAngle = 30;
            turretSpawn.SetRange(TurretSpawnInfo.DefaultRange);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8836, -54267, 3403), new Angle(-0.69f, 0.72f)).SetSpawnInfo(turretSpawn));


            // Before big slide, on top of metal slide, aiming at slide
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 0;
            turretSpawn.HorizontalSpeed = 0;
            turretSpawn.HorizontalAngle = 0;
            turretSpawn.SetRange(TurretSpawnInfo.DefaultRange);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11887, -56304, 4500), new QuaternionAngle(0.00f, 0.22f, 0.00f, 0.98f)).SetSpawnInfo(turretSpawn));


            // 3DudeHallway, middle platform left corner, aiming at platform 3
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 30;
            turretSpawn.HorizontalSpeed = 30;
            turretSpawn.HorizontalAngle = 30;
            turretSpawn.SetRange(TurretSpawnInfo.DefaultRange);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(5282, -66960, 1678), new Angle(-0.47f, 0.88f)).SetSpawnInfo(turretSpawn));


            // After fan, first platform
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 10;
            turretSpawn.HorizontalSpeed = 50;
            turretSpawn.HorizontalAngle = 45;
            turretSpawn.SetRange(TurretSpawnInfo.DefaultRange);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-30803, -63598, 3198), new Angle(0.39f, 0.92f)).SetSpawnInfo(turretSpawn));

            // First classic pistol room, on billboard side, aiming towards main path
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 0;
            turretSpawn.HorizontalSpeed = 0;
            turretSpawn.HorizontalAngle = 0;
            turretSpawn.SetRange(3220);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-14269, -61957, 3503), new QuaternionAngle(-0.35f, 0.61f, -0.61f, 0.35f)).SetSpawnInfo(turretSpawn));

            layout.DoNotReuse();
            Rooms.Add(layout);



            enemies = LookInisde_room2.ReturnEnemiesInRoom(LIenemiesAll); // 2 shifters
            // SHIFTER 1 : Slide area, before last room
            info = new ShifterSpawnInfo() {
                shiftPoints = new List<Tuple<Vector3f, Angle>>() {
                    new Tuple<Vector3f, Angle>(new Vector3f(8584, -79887, 878), new Angle(0.82f, 0.58f)), // platform
                    new Tuple<Vector3f, Angle>(new Vector3f(7975, -80338, 1711), new Angle(0.70f, 0.71f)), // billboard 
                    new Tuple<Vector3f, Angle>(new Vector3f(6778, -79664, 1997), new Angle(0.27f, 0.96f)), // billboard mounted 
                    new Tuple<Vector3f, Angle>(new Vector3f(6363, -77943, 1348), new Angle(-0.40f, 0.91f)) // in wall, SR route(kinda)
                }
            };
            enemies[0] = new EnemyShifter(enemies[0], 4).AddFixedSpawnInfo(info);
            layout = new RoomLayout(enemies[0]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8295, -78141, 1093), new Angle(0.71f, 0.71f)).DoNotReuse()); // slide (before last room)
            Rooms.Add(layout);


            // SHIFTER 2: sensory boost
            info = new ShifterSpawnInfo() {
                shiftPoints = new List<Tuple<Vector3f, Angle>>() {
                    new Tuple<Vector3f, Angle>(new Vector3f(-17375, -58267, 3909), new Angle(-0.47f, 0.88f)),  // middle platform
                    new Tuple<Vector3f, Angle>(new Vector3f(-17139, -56103, 4100), new Angle(-0.61f, 0.79f)), // 3rd platform
                    new Tuple<Vector3f, Angle>(new Vector3f(-15815, -57200, 4266), new Angle(-0.89f, 0.45f)), // wall pipes (above SR route)
                    new Tuple<Vector3f, Angle>(new Vector3f(-17518, -57571, 4325), new Angle(-0.34f, 0.94f))  // middle platform, street light
                }
            };
            enemies[1] = new EnemyShifter(enemies[1], 4).AddFixedSpawnInfo(info);
            layout = new RoomLayout(enemies[1]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-15517, -59683, 2798), new Angle(1.00f, 0.01f)).DoNotReuse()); // sensory boost
            Rooms.Add(layout);


            // last slide, after 3dude hallway
            LIenemies = LookInisde_room3.ReturnEnemiesInRoom(LIenemiesAll);
            LIenemies[0] = new EnemyShieldOrb(LIenemies[0]);
            // 0 - orb
            layout = new RoomLayout(LIenemies.Take(1).Single());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8990, -73925, 2302), new Vector3f(9006, -72959, 1917))); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8149, -74709, 1975))); // middle platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8988, -72344, 2383))); // collectible platform, right
            layout.DoNotReuse();
            Rooms.Add(layout);

            layout = new RoomLayout(LIenemies.Skip(1).ToList());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(7479, -74629, 1948), new Angle(0.76f, 0.65f)).Mask(SpawnPlane.Mask_Flatground)); // middle platform, left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(9033, -74727, 1948), new Angle(0.88f, 0.47f)).Mask(SpawnPlane.Mask_Flatground)); // middle platform, right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(9811, -74741, 1948), new Angle(-1.00f, 0.02f)).Mask(SpawnPlane.Mask_Flatground)); // middle platform, right corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8281, -75940, 1682), new Angle(0.72f, 0.70f)).Mask(SpawnPlane.Mask_Highground)); // slide start
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(7000, -74212, 1807), new Angle(0.40f, 0.92f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // left, small wall ledge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(7389, -76764, 2291), new Vector3f(8757, -76806, 2291), new Angle(0.81f, 0.58f)).AsVerticalPlane().SetMaxEnemies(2).Mask(SpawnPlane.Mask_Highground)); // SR beam

            // drone extra
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8851, -75820, 2415), new Vector3f(7486, -75685, 2415), new Angle(0.69f, 0.72f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);



            // Custom Checkpoints
            // after big slide, (just bigger/additional for speedrunners)
            CustomCheckPoints.Add(new GameObjects.CustomCP(mapType, new Vector3f(2546, -60309, 640), new Vector3f(5995, -58083, 3287),
                new Vector3f(4099, -58986, 1396), new Angle(-0.22f, 0.98f)));

            // after "3dudehallway"
            CustomCheckPoints.Add(new GameObjects.CustomCP(mapType, new Vector3f(5554, -73284, 2487), new Vector3f(7084, -72234, 1285), 
                new Vector3f(6099, -72832, 1800), new Angle(-0.03f, 1.00f)));


        }

        protected override void Gen_PerRoom() {} // don't use if IModes or IModesMidCV interfaces is used.
    }
}
