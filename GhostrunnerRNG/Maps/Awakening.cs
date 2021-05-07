using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using System;
using System.Collections.Generic;
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

        private bool IsNewGame = false;

        public Awakening() : base(MapType.Awakening) {
            CheckNewGame();
        }

        private void CheckNewGame() {
            DeepPointer bestTimeDP = new DeepPointer(0x0438BB40, 0xE0, 0xC8, 0x30, 0xF0, 0x8);
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
            TakeLastEnemyFromCP(ref enemies, force: false, attachedDoor: true, removeCP: true);
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
            TakeLastEnemyFromCP(ref enemies, force: false, attachedDoor: true, removeCP: true);
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
            TakeLastEnemyFromCP(ref enemies, force: false, attachedDoor: true, removeCP: true);
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
            TakeLastEnemyFromCP(ref enemies, force: false, attachedDoor: true, removeCP: true);
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
            TakeLastEnemyFromCP(ref enemies, force: false, attachedDoor: true, removeCP: true);
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
        }

        protected override void Gen_PerRoom() {} // don't use if IModes or IModesMidCV interfaces is used.
    }
}
