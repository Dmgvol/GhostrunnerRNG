using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.GameObjects;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.MemoryUtils;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.Maps {
    class RunUp : MapCore, IModes {


        #region Rooms
        // not needed for cp
        private Room room_1 = new Room(new Vector3f(58593, -67346, 2158), new Vector3f(69793, -64940, 4337));// 4 pistols guys in first room 
        private Room room_2 = new Room(new Vector3f(55639, -14697, 1718), new Vector3f(63942, -12552, 3351));// 2 geckos after sliding room  

        //enemies needed for cp
        private Room room_11 = new Room(new Vector3f(70912, -67435, 2393), new Vector3f(72590, -65153, 3760)); // 2 last guy in first hallway
        private Room room_12 = new Room(new Vector3f(71127, -62511, 2964), new Vector3f(75440, -59777, 4564)); // 4 pistols in second room
        private Room room_13 = new Room(new Vector3f(49154, -16915, 2023), new Vector3f(52701, -11231, 4658)); // 2 geckos, kunai room
        private Room room_14 = new Room(new Vector3f(49128, -2705, 5097), new Vector3f(55849, 4367, 8069)); // 3 geckos room
        private Room room_15 = new Room(new Vector3f(48342, 4783, 4664), new Vector3f(56801, 14851, 7506)); // final fight room
        #endregion

        #region HC Rooms
        private Room HC_room1 = new Room(new Vector3f(49981, -67257, 2784), new Vector3f(63555, -65205, 5709)); // 2 pistol, shielder, waver, drone
        private Room HC_room2 = new Room(new Vector3f(67196, -62609, 2217), new Vector3f(73451, -67433, 4138)); // orb with 2 attached enemies
        private Room HC_room3 = new Room(new Vector3f(75728, -62710, 2806), new Vector3f(70933, -59667, 4860)); // 3 weebs, pistol
        private Room HC_room4 = new Room(new Vector3f(72202, -54985, 1829), new Vector3f(67911, -48106, 4156)); // pistol, frogger, waver, orb+turret(chainedOrb)
        private Room HC_room5 = new Room(new Vector3f(69666, -28961, -395), new Vector3f(63952, -18193, 4788)); // 3 turret, waver
        private Room HC_room6 = new Room(new Vector3f(66374, -16906, 1763), new Vector3f(54535, -12501, 3999)); // uzi, frogger, shielder, 3 wavers
        private Room HC_room7 = new Room(new Vector3f(52757, -16813, 2286), new Vector3f(47086, -11028, 4229)); // 2 turrets, waver, splitter
        private Room HC_room8 = new Room(new Vector3f(48455, -3811, 4692), new Vector3f(56118, 4237, 7678)); //  shifter, 3 wavers, frogger,   
        private Room HC_room9 = new Room(new Vector3f(56457, 6839, 4885), new Vector3f(48532, 14553, 7822)); // orb+shield(chainedOrb), 2 wavers

        private Room HC_room10_TOM1 = new Room(new Vector3f(44924, 23953, -20976), new Vector3f(48814, 16539, -19150)); // orb + turret 1
        private Room HC_room10_TOM2 = new Room(new Vector3f(55196, 25641, -21105), new Vector3f(59788, 18364, -19175)); // orb + turret 2
        #endregion

        public RunUp() : base(MapType.RunUp) {}

        public void Gen_Normal() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game);
            AllEnemies.AddRange(GetAllEnemies(GameHook.game, 10));
            Rooms = new List<RoomLayout>();
            RoomLayout layout;

            // First section, move all to EnemiesWithoutCP
            List<Enemy> enemies = room_1.ReturnEnemiesInRoom(AllEnemies);
            enemies.AddRange(room_11.ReturnEnemiesInRoom(AllEnemies));
            for(int i = 0; i < 6; i++) 
                RandomPickEnemiesWithoutCP(ref enemies, force: true, removeCP: true, enemyIndex: 0);
            

            //// room 12 layout ////
            enemies = room_12.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies[0], enemies[1], enemies[2], enemies[3]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74554, -63128, 4334), new Angle(-0.38f, 0.93f)));// thing on the wall near entry door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(73100, -62328, 4435), new Vector3f(73100, -59976, 4435), new Angle(0.00f, 1.00f)));//shorter line
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71980, -61100, 4435), new Vector3f(74056, -61100, 4435), new Angle(-0.31f, 0.95f)));//other line
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74625, -59867, 4658), new Angle(-0.71f, 0.70f)));//air conditioner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71721, -59849, 4335), new Angle(-0.32f, 0.95f)));//lamp on top of the exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71218, -61708, 3956), new Angle(0.00f, 1.00f)));//citylight
            //defafult planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(72730, -60695, 3598), new Vector3f(71414, -60001, 3597), new Angle(-0.26f, 0.96f)));//closest to the exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(75255, -62267, 3598), new Vector3f(73738, -61609, 3604), new Angle(-0.54f, 0.84f)));//closest to the entry
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71589, -62234, 3598), new Vector3f(72761, -61553, 3598), new Angle(0.00f, 1.00f)));//platform on the right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(73675, -59999, 3597), new Vector3f(75104, -60602, 3597), new Angle(-0.72f, 0.69f)));//platform on the left
            Rooms.Add(layout);

            //// room 13 layout ////
            List<Enemy> enemies2 = room_2.ReturnEnemiesInRoom(AllEnemies);
            enemies = room_13.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies[0], enemies[1], enemies2[0]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53548, -13563, 3048), new Angle(-0.44f, 0.90f)));//boxes near entry spot
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(52217, -11717, 3198), new Angle(-0.77f, 0.64f)));//kunai boxes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50642, -11379, 3198), new Angle(-0.41f, 0.91f)));//boxes near leftgecko
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49175, -13289, 3188), new Angle(-0.05f, 1.00f)));// middle back boxes 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49132, -14662, 2898), new Angle(0.06f, 1.00f))); // middle back boxes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49656, -15830, 2898), new Angle(0.24f, 0.97f)));//boxes at the back of right gecko
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49650, -16260, 3198), new Angle(0.31f, 0.95f))); // boxes at the back of right gecko 2
            //default planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49962, -14304, 2598), new Vector3f(51544, -16156, 2598), new Angle(0.29f, 0.96f)));//right gecko
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50023, -13466, 2598), new Vector3f(51993, -11847, 2598), new Angle(-0.28f, 0.96f)));//left gecko
            Rooms.Add(layout);

            //// room 14 layout ////
            enemies = room_14.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies[0], enemies[1], enemies[2], enemies2[1]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53751, -2797, 5798), new Angle(-0.71f, 0.70f)));//left corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50960, -2810, 5798), new Angle(-0.72f, 0.70f)));//right corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53403, 850, 6388), new Angle(-0.88f, 0.48f)));// boxes near center on the left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51190, 466, 6098), new Angle(-0.72f, 0.69f)));//boxes near center on the left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49516, -1148, 6403), new Angle(-0.60f, 0.80f)));//boxes near center on the left

            //default planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(55354, 1687, 5798), new Vector3f(54043, -631, 5798), new Angle(-0.84f, 0.54f)));//left gecko
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49521, -670, 5798), new Vector3f(50701, 1905, 5798), new Angle(-0.69f, 0.72f)));//right gecko
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53666, 3250, 5798), new Vector3f(50942, 3250, 5798), new Angle(-0.71f, 0.71f)).AsVerticalPlane());//center gecko
            Rooms.Add(layout);

            //// room 15 layout ////
            enemies = room_15.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies[0], enemies[1], enemies[2]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(54329, 9010, 6776), new Vector3f(52839, 7535, 6776), new Angle(-0.83f, 0.56f)).AsVerticalPlane());//first billboard on the left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(54682, 8373, 7058), new Vector3f(53563, 7285, 7056), new Angle(-0.96f, 0.27f)).AsVerticalPlane());//second billboard on the left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51564, 7811, 6811), new Vector3f(50386, 8987, 6811), new Angle(-0.45f, 0.89f)).AsVerticalPlane());//first billboard on the right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50995, 7395, 7058), new Vector3f(50109, 8306, 7058), new Angle(-0.31f, 0.95f)).AsVerticalPlane());//second billboard on the right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(48417, 9813, 6598), new Vector3f(51489, 12848, 6598), new Angle(-0.44f, 0.90f)).AsVerticalPlane());//wall on the right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53292, 12883, 6598), new Vector3f(55813, 10359, 6598), new Angle(-0.88f, 0.48f)).AsVerticalPlane());//wall on the left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(52840, 13065, 6254), new Vector3f(52840, 13764, 6254), new Angle(-0.72f, 0.69f)));//box near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51679, 6367, 6428), new Angle(-0.55f, 0.84f)));//box near entry
            //default planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(55217, 9194, 5786), new Vector3f(55234, 10840, 5786), new Angle(-0.90f, 0.43f)));//left guy
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51513, 12678, 5786), new Vector3f(53308, 11056, 5786), new Angle(-0.72f, 0.69f)));//gecko platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(48797, 9504, 5786), new Vector3f(50704, 9906, 5786), new Angle(-0.42f, 0.91f)));//right platform
            Rooms.Add(layout);


            //// EXTRA ////
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67967, -43516, 2458), new Angle(-0.71f, 0.70f)));//after sliding before crusher
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65752, -18076, 2298), new Angle(-0.68f, 0.73f)));//after sliding, under collectible
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53694, -67174, 6018), new Angle(0.99f, 0.16f)));//near spawn room on the left wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(75144, -65817, 3869), new Angle(1.00f, 0.00f)));//billboard near 4 pistols room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(52543, -10639, 3718), new Angle(0.97f, 0.23f)));//platform near last ollectible
            // room 13
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51911, -13582, 3405), new Angle(-0.06f, 1.00f)));//thing on the left of the pillar
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51068, -13883, 3363), new Angle(1.00f, 0.05f)));//back of the pillar
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(47997, -11227, 3918), new Angle(-0.24f, 0.97f)));//first billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(48251, -9843, 4528), new Angle(-0.69f, 0.72f)));//second billboard
             
            // Room with 2 geckos,  with train
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(59758, -13541, 2218), new Vector3f(59317, -13955, 2218), new Angle(-0.56f, 0.83f)));//behind middle wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63587, -13130, 2498), new Angle(-0.69f, 0.73f)));//citylight
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61651, -12993, 3008), new Angle(-0.13f, 0.99f)));//net
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61694, -14505, 2639), new Angle(0.11f, 0.99f)));//thing on right wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(56229, -13321, 2837), new Vector3f(55332, -13320, 2837), new Angle(-0.09f, 1.00f)));//left billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(59047, -13118, 2693), new Angle(-0.68f, 0.73f)));//dumpster
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(60769, -14395, 2218), new Vector3f(63548, -13278, 2218), new Angle(-0.02f, 1.00f)));//1 gecko spawn plane (pistol)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(58478, -14392, 2225), new Vector3f(63548, -13278, 2218), new Angle(-0.06f, 1.00f)));//2 gecko spawn plane (pistol)

            Rooms.Add(layout);

        }

        public void Gen_Easy() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game);
            AllEnemies.AddRange(GetAllEnemies(GameHook.game, 10));

            Rooms = new List<RoomLayout>();
            RoomLayout layout;
            List<Enemy> enemies1 = room_1.ReturnEnemiesInRoom(AllEnemies);//random spots on the map for 1 pistol
            layout = new RoomLayout(enemies1[0]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67967, -43516, 2458), new Angle(-0.71f, 0.70f)));//after sliding before crusher
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65752, -18076, 2298), new Angle(-0.68f, 0.73f)));//after sliding, under collectible
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53694, -67174, 6018), new Angle(0.99f, 0.16f)));//near spawn room on the left wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(75099, -64406, 3590), new Angle(-0.90f, 0.44f))); // near door of first room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(52543, -10639, 3718), new Angle(0.97f, 0.23f)));//platform near last ollectible
            Rooms.Add(layout);

            //// room 11 layout //// 2 dudes 
            ModifyCP(new DeepPointer(PtrDB.DP_RunUp_ElevatorCP), new Vector3f(55595, -13929, 2228), new Angle(1,0), GameHook.game);
            List<Enemy> enemies = room_11.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies[0], enemies[1]);//room with train on the left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(59758, -13541, 2218), new Vector3f(59317, -13955, 2218), new Angle(-0.56f, 0.83f)));//behind middle wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63587, -13130, 2498), new Angle(-0.69f, 0.73f)));//citylight
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61694, -14505, 2639), new Angle(0.11f, 0.99f)));//thing on right wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(55218, -13326, 2837), new Angle(-0.28f, 0.96f)));//left billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(59047, -13118, 2693), new Angle(-0.68f, 0.73f)));//dumpster
            //deafult planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(60769, -14395, 2218), new Vector3f(63548, -13278, 2218), new Angle(-0.02f, 1.00f)));//1 gecko spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(58478, -14392, 2225), new Vector3f(63548, -13278, 2218), new Angle(-0.06f, 1.00f)));//2 gecko spawn plane
            Rooms.Add(layout);

            //// room 12 layout ////
            enemies = room_12.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies[0], enemies[1], enemies[2], enemies[3], enemies1[1]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(72435, -59898, 3896), new Angle(-0.38f, 0.92f))); ;//lamp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71218, -61708, 3956), new Angle(0.00f, 1.00f)));//citylight
            //defafult planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(72730, -60695, 3598), new Vector3f(71414, -60001, 3597), new Angle(-0.26f, 0.96f)));//closest to the exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(75255, -62267, 3598), new Vector3f(73738, -61609, 3604), new Angle(-0.54f, 0.84f)));//closest to the entry
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71589, -62234, 3598), new Vector3f(72761, -61553, 3598), new Angle(0.00f, 1.00f)));//platform on the right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(73675, -59999, 3597), new Vector3f(75104, -60602, 3597), new Angle(-0.72f, 0.69f)));//platform on the left
            Rooms.Add(layout);

            //// room 13 layout ////
            List<Enemy> enemies2 = room_2.ReturnEnemiesInRoom(AllEnemies);
            enemies = room_13.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies[0], enemies[1], enemies2[0]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53548, -13563, 3048), new Angle(-0.44f, 0.90f)));//boxes near entry spot
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(52217, -11717, 3198), new Angle(-0.77f, 0.64f)));//kunai boxes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50642, -11379, 3198), new Angle(-0.41f, 0.91f)));//boxes near leftgecko
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49175, -13289, 3188), new Angle(-0.05f, 1.00f)));// middle back boxes 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49132, -14662, 2898), new Angle(0.06f, 1.00f))); // middle back boxes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49656, -15830, 2898), new Angle(0.24f, 0.97f)));//boxes at the back of right gecko
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49650, -16260, 3198), new Angle(0.31f, 0.95f))); // boxes at the back of right gecko 2

            //default planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49962, -14304, 2598), new Vector3f(51544, -16156, 2598), new Angle(0.29f, 0.96f)));//right gecko
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50023, -13466, 2598), new Vector3f(51993, -11847, 2598), new Angle(-0.28f, 0.96f)));//left gecko
            Rooms.Add(layout);

            //1 pistol additional
            EnemiesWithoutCP.Add(enemies1[2]);

            Rooms.Add(layout);

            //// room 14 layout ////
            enemies = room_14.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies[0], enemies[1], enemies[2], enemies2[1]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53751, -2797, 5798), new Angle(-0.71f, 0.70f)));//left corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50960, -2810, 5798), new Angle(-0.72f, 0.70f)));//right corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53403, 850, 6388), new Angle(-0.88f, 0.48f)));// boxes near center on the left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51190, 466, 6098), new Angle(-0.72f, 0.69f)));//boxes near center on the left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49516, -1148, 6403), new Angle(-0.60f, 0.80f)));//boxes near center on the left

            //default planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(55354, 1687, 5798), new Vector3f(54043, -631, 5798), new Angle(-0.84f, 0.54f)));//left gecko
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49521, -670, 5798), new Vector3f(50701, 1905, 5798), new Angle(-0.69f, 0.72f)));//right gecko
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53666, 3250, 5798), new Vector3f(50942, 3250, 5798), new Angle(-0.71f, 0.71f)).AsVerticalPlane());//center gecko
            Rooms.Add(layout);

            //// room 15 layout ////
            enemies = room_15.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies[0], enemies[1], enemies[2], enemies1[3]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(52840, 13065, 6254), new Vector3f(52840, 13764, 6254), new Angle(-0.72f, 0.69f)));//box near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51679, 6367, 6428), new Angle(-0.55f, 0.84f)));//box near entry
            //default planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(55217, 9194, 5786), new Vector3f(55234, 10840, 5786), new Angle(-0.90f, 0.43f)));//left guy
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51513, 12678, 5786), new Vector3f(53308, 11056, 5786), new Angle(-0.72f, 0.69f)));//gecko platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(48797, 9504, 5786), new Vector3f(50704, 9906, 5786), new Angle(-0.42f, 0.91f)));//right platform
            Rooms.Add(layout);
        }


        private List<ChainedOrb> chainedOrbs = new List<ChainedOrb>();
        private List<RoomLayout> chainedOrbs_Rooms = new List<RoomLayout>();

        public void Gen_Hardcore() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game);
            List<Enemy> Shifters = new List<Enemy>();
            Rooms = new List<RoomLayout>();
            RoomLayout layout;

            //// Room 1 ////
            var enemies = HC_room1.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyDrone(enemies[0]);
            enemies[4].SetEnemyType(Enemy.EnemyTypes.Waver);
            RandomPickEnemiesWithoutCP(ref enemies, force: true);
            enemies.ForEach(x => x.DisableAttachedCP(GameHook.game)); // 2 last pistols are cp attached
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53831, -66708, 4187), new Vector3f(56093, -65584, 4188), new Angle(-1.00f, 0.01f)).Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(57964, -66906, 4189), new Vector3f(57433, -65875, 4188), new Angle(1.00f, 0.01f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61230, -65524, 4193), new Vector3f(62007, -66784, 4188), new Angle(-1.00f, 0.01f)).Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68989, -66781, 2888), new Vector3f(68231, -65614, 2888), new Angle(1.00f, 0.01f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74907, -64128, 3578), new Angle(-0.83f, 0.55f)).Mask(SpawnPlane.Mask_Highground)); // exit door
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64407, -66518, 3997), new Angle(-1.00f, 0.05f)).Mask(SpawnPlane.Mask_Highground)); // slide ledge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(55078, -67024, 4808), new Angle(0.99f, 0.14f)).Mask(SpawnPlane.Mask_Highground)); // 1st platform crates
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(57250, -65543, 4499), new Angle(-0.98f, 0.19f)).Mask(SpawnPlane.Mask_Highground)); // 2nd platform crates
            // drones
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62801, -66592, 4540), new Angle(0.99f, 0.13f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(59817, -66537, 4697), new Angle(1.00f, 0.02f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74819, -65315, 3620), new Angle(-0.99f, 0.14f)).Mask(SpawnPlane.Mask_Airborne)); // curved billboard
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(60058, -65298, 4193), new Angle(-0.40f, 0.91f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalSpeed = 30, HorizontalAngle = 30, VisibleLaserLength = 0})); // hidden behind crates
            Rooms.Add(layout);


            //// Room 2 ////
            enemies = HC_room2.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            // orb
            layout = new RoomLayout(enemies[0]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69970, -64286, 2961), new Vector3f(69065, -63287, 3365))); // default 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(72941, -67200, 3459))); // pipes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(72858, -65310, 3453))); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(72443, -66299, 2931))); // enemy platform
            Rooms.Add(layout);
            // enemies
            layout = new RoomLayout(enemies.Skip(1).ToList());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(72324, -66940, 2893), new Vector3f(71311, -65454, 2893), new Angle(-1.00f, 0.02f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(3)); // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(72241, -67095, 3188), new Angle(0.97f, 0.25f)).Mask(SpawnPlane.Mask_Highground)); // lamp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(73606, -65308, 3453), new Angle(-0.97f, 0.24f)).Mask(SpawnPlane.Mask_Highground)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(75217, -65870, 3875), new Angle(1.00f, 0.02f)).Mask(SpawnPlane.Mask_Highground)); // curved billboard
            Rooms.Add(layout);


            //// Room 3 ////
            enemies = HC_room3.ReturnEnemiesInRoom(AllEnemies);
            enemies[1].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Weeb);
            RandomPickEnemiesWithoutCP(ref enemies);
            layout = new RoomLayout(enemies);
            // default planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71461, -60099, 3597), new Vector3f(72487, -60573, 3597), new Angle(-0.01f, 1.00f)).Mask(SpawnPlane.Mask_Flatground)); // closest to the exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(73731, -61708, 3604), new Vector3f(74815, -62185, 3598), new Angle(-0.48f, 0.88f)).Mask(SpawnPlane.Mask_Flatground)); // closest to the entry
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(72560, -61653, 3598), new Vector3f(71520, -62198, 3598), new Angle(0.08f, 1.00f)).Mask(SpawnPlane.Mask_Flatground)); // platform on the right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(75104, -60602, 3597), new Vector3f(73753, -60032, 3597), new Angle(-0.71f, 0.71f)).Mask(SpawnPlane.Mask_Flatground)); // platform on the left
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74554, -63128, 4334), new Angle(-0.38f, 0.93f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // thing on the wall near entry door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(73100, -62328, 4435), new Vector3f(73100, -59976, 4435), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // shorter line
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71980, -61100, 4435), new Vector3f(74056, -61100, 4435), new Angle(-0.31f, 0.95f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // other line
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74625, -59867, 4658), new Angle(-0.71f, 0.70f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // air conditioner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71721, -59849, 4335), new Angle(-0.32f, 0.95f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // lamp on top of the exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71218, -61708, 3956), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // citylight
            Rooms.Add(layout);


            //// Room 4 ////
            enemies = HC_room4.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyTurret(enemies[0]);
            enemies[1] = new EnemyShieldOrb(enemies[1]);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Waver);
            // orb
            layout = new RoomLayout(enemies[1]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71212, -48226, 3698))); // corner crate stack
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71053, -50066, 3615))); // left wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69988, -50812, 3068))); // behind middle crates
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69785, -50599, 2958), new Vector3f(70221, -49114, 3394))); // default range
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67891, -55139, 2913))); // main slide
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(70325, -53016, 3556))); // floating - grapple needed
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(70814, -48519, 2608)).setRarity(0.5)); // hiding pipes behind platform
            Rooms.Add(layout);
            // enemies
            layout = new RoomLayout(enemies.Except(new List<Enemy>{enemies[1]}).ToList());
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(70520, -49222, 2953), new Vector3f(71060, -49900, 2952), new Angle(-0.71f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71116, -50234, 2948), new Vector3f(70704, -51340, 2952), new Angle(-0.74f, 0.67f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71102, -51496, 2958), new Vector3f(71541, -52363, 2952), new Angle(-0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            // special/high
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(70200, -50947, 3538), new Angle(-0.67f, 0.74f)).Mask(SpawnPlane.Mask_Highground)); // crates 1
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71831, -53335, 3828), new Angle(-0.93f, 0.36f)).Mask(SpawnPlane.Mask_Highground)); // crates 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(70516, -50314, 3248), new Angle(-0.63f, 0.78f)).Mask(SpawnPlane.Mask_Highground)); // crates 3
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68276, -56908, 3479), new Angle(-0.37f, 0.93f)).Mask(SpawnPlane.Mask_Highground)); // slide start
            // turrets
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69128, -50821, 3163), new Angle(0.98f, 0.22f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { VerticalAngle = -35, HorizontalAngle = 30, HorizontalSpeed = 30 })); // default, aim main path

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69101, -50581, 2706), new Angle(1, 0)).Mask(SpawnPlane.Mask_Turret)
                  .SetSpawnInfo(new TurretSpawnInfo { VerticalAngle = -20, HorizontalAngle = 30, HorizontalSpeed = 15})); // default, lower platform

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69657, -49668, 2546), new QuaternionAngle(-0.71f, 0.00f, 0.71f, 0.00f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { VerticalAngle = 60, HorizontalAngle = 60, HorizontalSpeed = 45 })); // wall side

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69177, -50972, 2475), new QuaternionAngle(0.33f, 0.94f, 0.00f, 0.00f)).Mask(SpawnPlane.Mask_Turret)
                 .SetSpawnInfo(new TurretSpawnInfo { VerticalAngle = 45, HorizontalAngle = 50, HorizontalSpeed = 40 })); // ceiling gang, under default
            // additional(drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69396, -51349, 3331), new Angle(-0.66f, 0.75f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67929, -44152, 3044), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);


            //// Room 5 ////
            enemies = HC_room5.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyTurret(enemies[0]);
            enemies[1] = new EnemyTurret(enemies[1]);
            enemies[2] = new EnemyTurret(enemies[2]);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[3].DisableAttachedCP(GameHook.game);
            CustomCheckPoints.Add(new GameObjects.CustomCP(mapType, new Vector3f(65791, -26173, 1691), new Vector3f(64018, -23172, 315),
                new Vector3f(65163, -25601, 568), new Angle(0.33f, 0.94f))); // replace waver cp with custom
            RandomPickEnemiesWithoutCP(ref enemies, force: true);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65567, -18929, 2301), new Vector3f(67014, -18536, 2301), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65766, -18384, 2995), new Angle(-0.49f, 0.87f)).Mask(SpawnPlane.Mask_Highground)); // door lamp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67325, -21311, 2777), new Vector3f(68178, -21284, 2768), new Angle(-0.72f, 0.70f))
                .AsVerticalPlane().Mask(SpawnPlane.Mask_Highground)); // slide end
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65001, -19092, 2300), new Angle(-0.44f, 0.90f)).Mask(SpawnPlane.Mask_Highground)); //  platform corner
            // turrets
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68279, -20947, 2645), new Vector3f(67292, -20930, 2639), new QuaternionAngle(-0.08f, 0.21f, 0.83f, 0.51f))
                .Mask(SpawnPlane.Mask_Turret).AsVerticalPlane().SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 20, VerticalAngle = 20})); // slide ledge (Q: 120 20 17)

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68145, -18618, 2878), new Angle(-0.99f, 0.11f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 30, VerticalAngle = -10, Range = 5000})); // left crate, default but moving

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65168, -18975, 2301), new Vector3f(65286, -18542, 2301), new Angle(-0.04f, 1.00f)).Mask(SpawnPlane.Mask_Turret)
                  .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 20, HorizontalSpeed = 30, VerticalAngle = 15 })); // right, default but moving

            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66413, -20033, 2962), new Angle(-0.62f, 0.78f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68585, -19298, 2958), new Angle(-0.78f, 0.62f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);


            //// Room 6 ////
            enemies = HC_room6.ReturnEnemiesInRoom(AllEnemies);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[4].SetEnemyType(Enemy.EnemyTypes.Waver);
            RandomPickEnemiesWithoutCP(ref enemies, force: true);
            // entry cp in case you skip turrets
            CustomCheckPoints.Add(new GameObjects.CustomCP(mapType, new Vector3f(66277, -18047, 3062), new Vector3f(65218, -18160, 2129), new Vector3f(66194, -18075, 2298), new Angle(0.86f, 0.51f)));
            // exit cp, enemies have mixed checkpoints
            CustomCheckPoints.Add(new GameObjects.CustomCP(mapType, new Vector3f(54266, -14496, 2522), new Vector3f(53695, -13283, 3344), new Vector3f(54230, -14004, 2703), new Angle(-1.00f, 0.00f)));
            // remove cp from first section
            enemies.Where(x => x.Pos.X > 60000).ToList().ForEach(x => x.DisableAttachedCP(GameHook.game));

            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66072, -13673, 2217), new Vector3f(65493, -14364, 2217), new Angle(-0.71f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62863, -14310, 2217), new Vector3f(60722, -13471, 2217), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(55266, -14291, 2218), new Vector3f(57926, -13533, 2218), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(54726, -13683, 2696), new Vector3f(55007, -14334, 2708), new Angle(0.02f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // door
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(56072, -14489, 2837), new Angle(0.12f, 0.99f)).Mask(SpawnPlane.Mask_Highground)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(56076, -13357, 2837), new Angle(-0.15f, 0.99f)).Mask(SpawnPlane.Mask_Highground)); // billboard 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61659, -14488, 2639), new Angle(0.13f, 0.99f)).Mask(SpawnPlane.Mask_Highground)); // fuse box
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63640, -13091, 2497), new Angle(-0.69f, 0.72f)).Mask(SpawnPlane.Mask_Highground)); // citylight
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(54943, -13400, 3012), new Angle(-0.15f, 0.99f)).Mask(SpawnPlane.Mask_Highground)); // lamp near door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64318, -14291, 2218), new Angle(0.99f, 0.17f)).Mask(SpawnPlane.Mask_Flatground)); // around corner

            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65515, -17778, 3548), new Angle(0.70f, 0.72f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 2, HorizontalSpeed = 2, VerticalAngle = -45, VisibleLaserLength = 0})); // hidden at collectible platform

            bool hiddenTurrentFlag = Config.GetInstance().r.Next(2) == 0; // make turret visible or hidden
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(59661, -13616, 2218), new Angle(-0.97f, 0.24f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = hiddenTurrentFlag ? 2 : 45, HorizontalSpeed = 30, VerticalAngle = 0, Range = (hiddenTurrentFlag ? 0 : 2000)})); // around corner

            // addtional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61374, -13471, 2804), new Angle(-0.07f, 1.00f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(57527, -13337, 2804), new Angle(-0.19f, 0.98f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);


            //// Room 7 ////
            enemies = HC_room7.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyTurret(enemies[0]);
            enemies[1] = new EnemyTurret(enemies[1]);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Splitter);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Waver);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, enemyIndexBesides:2); // without splitter
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(52194, -14499, 2598), new Vector3f(50270, -16037, 2598), new Angle(-0.02f, 1.00f)).Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51846, -13395, 2598), new Vector3f(50420, -11830, 2598), new Angle(-0.03f, 1.00f)).Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2).AllowSplitter());
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49616, -15846, 2898), new Angle(0.29f, 0.96f)).Mask(SpawnPlane.Mask_Highground)); // right crates behind laser
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49618, -11662, 2896), new Angle(-0.36f, 0.93f)).Mask(SpawnPlane.Mask_Highground)); // lamp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50532, -11346, 3198), new Angle(-0.45f, 0.90f)).Mask(SpawnPlane.Mask_Highground)); // left red crate
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(52179, -11568, 3198), new Angle(-0.83f, 0.56f)).Mask(SpawnPlane.Mask_Highground)); // red left crate 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51042, -13916, 3359), new Angle(0.95f, 0.31f)).Mask(SpawnPlane.Mask_Highground)); // generator behind wall
            // turrets
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53335, -13407, 2852), new Angle(-0.94f, 0.35f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 2, HorizontalSpeed = 2, VerticalAngle = 0, VisibleLaserLength = 0})); // hidden behind crates (next to cp)

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49010, -13902, 3188), new Angle(0.02f, 1.00f)).Mask(SpawnPlane.Mask_Turret)
                 .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 45, HorizontalSpeed = 30, VerticalAngle = 0})); // default, aiming to middle

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(52162, -16258, 3198), new Angle(0.91f, 0.41f)).Mask(SpawnPlane.Mask_Turret)
                 .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 20, HorizontalSpeed = 25, VerticalAngle = -10 })); // right corner crates

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50295, -11254, 2898), new Angle(-0.87f, 0.49f)).Mask(SpawnPlane.Mask_Turret)
                 .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, HorizontalSpeed = 0, VerticalAngle = 10 })); // left far corner

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49106, -13312, 3188), new Angle(0.81f, 0.59f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 20, HorizontalSpeed = 20, VerticalAngle = 20 })); // default to side

            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50459, -12489, 3352), new Angle(-0.24f, 0.97f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50355, -14509, 3352), new Vector3f(51403, -15764, 3352), new Angle(0.22f, 0.98f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49142, -12331, 3313), new Angle(-0.21f, 0.98f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);


            //// Room 8 ////
            enemies = HC_room8.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShifter(enemies[0]);
            enemies[1].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Waver);
            // detach shifter
            enemies[0].DisableAttachedCP(GameHook.game);
            Shifters.Add(enemies[0]);
            enemies.RemoveAt(0);

            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49762, 2504, 5798), new Vector3f(50659, -408, 5798), new Angle(-0.72f, 0.69f)).Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(3));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(55152, 2351, 5798), new Vector3f(54084, -986, 5798), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(3));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53582, 3504, 5798), new Vector3f(51285, 2897, 5798), new Angle(-0.73f, 0.68f)).Mask(SpawnPlane.Mask_Flatground));
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51031, -2794, 5798), new Vector3f(53503, -2765, 5798), new Angle(-0.72f, 0.69f)).Mask(SpawnPlane.Mask_Highground).SetMaxEnemies(3)); // platform ledge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53402, 916, 6388), new Angle(-0.65f, 0.76f)).Mask(SpawnPlane.Mask_Highground)); // crates left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51203, 938, 6388), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_Highground)); // crates right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49511, -1104, 6403), new Angle(-0.37f, 0.93f)).Mask(SpawnPlane.Mask_Highground)); // crates far right
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50588, -494, 6391), new Vector3f(49562, 1850, 6391), new Angle(-0.67f, 0.74f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(54066, 2420, 6294), new Vector3f(55083, -789, 6405), new Angle(-0.71f, 0.71f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49424, -591, 5798), new Angle(0.46f, 0.89f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 40, HorizontalSpeed = 30, VerticalAngle = 0 })); // around corner of right crate
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(52304, 2583, 6978), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, HorizontalSpeed = 0, VerticalAngle = -45 })); // above exit door
            Rooms.Add(layout);


            //// Room 9 ////
            enemies = HC_room9.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyTurret(enemies[0]);
            enemies[1] = new EnemyShieldOrb(enemies[1]);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Waver);
            RandomPickEnemiesWithoutCP(ref enemies, enemyIndex:3);
            // orb
            layout = new RoomLayout(enemies[1]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(54521, 8901, 6590), new Vector3f(53928, 7901, 6038)).AsVerticalPlane()); // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50839, 8860, 6051), new Vector3f(50593, 9442, 6666)).AsVerticalPlane()); // behind right laser
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(52646, 10728, 6207))); // lamp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50742, 11366, 5957))); // floating right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(54140, 11492, 6156))); // floating left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53275, 13552, 6278))); // behind fence (near exit)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(55191, 8572, 6020), new Vector3f(54273, 7633, 6731))); // behind billboard
            Rooms.Add(layout);

            // enemies
            enemies.RemoveAt(1); // remove orb
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49109, 9178, 5786), new Vector3f(49915, 10457, 5786), new Angle(-0.46f, 0.89f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(55518, 9382, 5786), new Vector3f(54843, 10247, 5787), new Angle(-1.00f, 0.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51686, 12740, 5786), new Vector3f(53042, 10973, 5786), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(54090, 10222, 5786), new Vector3f(53566, 10794, 5786), new Angle(-0.81f, 0.59f)).Mask(SpawnPlane.Mask_Highground).AsVerticalPlane());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51354, 10825, 5786), new Vector3f(50662, 10209, 5786), new Angle(-0.54f, 0.84f)).Mask(SpawnPlane.Mask_Highground).AsVerticalPlane());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(52153, 13828, 5786), new Angle(-0.67f, 0.74f)).Mask(SpawnPlane.Mask_Flatground)); // near exit door
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(52834, 13596, 6254), new Angle(-0.77f, 0.64f)).Mask(SpawnPlane.Mask_Highground)); // generator near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(55933, 10240, 6598), new Vector3f(53441, 12736, 6598), new Angle(-0.82f, 0.57f)).Mask(SpawnPlane.Mask_HighgroundLimited).AsVerticalPlane()); // long billboard left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51423, 12797, 6598), new Vector3f(48986, 10382, 6598), new Angle(-0.39f, 0.92f)).Mask(SpawnPlane.Mask_HighgroundLimited).AsVerticalPlane()); // long billboard right
            // turrets
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(52234, 10731, 5786), new Angle(-0.99f, 0.14f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 30, VerticalAngle = 0})); // default but moving

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53263, 11322, 5788), new Angle(-0.28f, 0.96f)).Mask(SpawnPlane.Mask_Turret)
             .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 30, VerticalAngle = 0 })); // left far

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(54787, 10635, 5786), new Angle(0.94f, 0.33f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 30, VerticalAngle = 0 })); // left near

            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53202, 10950, 6362), new Vector3f(51712, 12412, 6739), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(55101, 9951, 6549), new Angle(-0.98f, 0.18f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49321, 9636, 6549), new Angle(-0.38f, 0.92f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);

            //// Shifters ////
            layout = new RoomLayout(Shifters);
            // room 7
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51909, -13488, 3401), new Angle(-0.09f, 1.00f))
                .SetSpawnInfo(new ShifterSpawnInfo {
                    shiftPoints = new List<System.Tuple<Vector3f, Angle>>(){
                    new System.Tuple<Vector3f, Angle>(new Vector3f(51606, -11312, 3048), new Angle(-0.58f, 0.81f)),
                    new System.Tuple<Vector3f, Angle>(new Vector3f(49611, -16293, 3198), new Angle(0.34f, 0.94f)),
                    new System.Tuple<Vector3f, Angle>(new Vector3f(49214, -13646, 2898), new Angle(0.08f, 1.00f))
                }}));

            // room 8
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49509, -1510, 6098), new Angle(-0.41f, 0.91f))
                .SetSpawnInfo(new ShifterSpawnInfo {
                    shiftPoints = new List<System.Tuple<Vector3f, Angle>>(){
                    new System.Tuple<Vector3f, Angle>(new Vector3f(51156, 460, 6052), new Angle(-0.73f, 0.68f)),
                    new System.Tuple<Vector3f, Angle>(new Vector3f(53426, 495, 6068), new Angle(-0.75f, 0.66f)),
                    new System.Tuple<Vector3f, Angle>(new Vector3f(52307, 1240, 7032), new Angle(-0.71f, 0.71f))
                }
                }));

            // room 9
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51669, 7762, 6809), new Angle(-0.61f, 0.79f))
                .SetSpawnInfo(new ShifterSpawnInfo {
                    shiftPoints = new List<System.Tuple<Vector3f, Angle>>(){
                    new System.Tuple<Vector3f, Angle>(new Vector3f(49812, 10792, 5786), new Angle(-0.56f, 0.83f)),
                    new System.Tuple<Vector3f, Angle>(new Vector3f(54388, 9076, 6776), new Angle(1.00f, 0.06f)),
                    new System.Tuple<Vector3f, Angle>(new Vector3f(51664, 13091, 6553), new Angle(-0.59f, 0.80f))
                }
                }));
            layout.DoNotReuse();
            Rooms.Add(layout);

            //// ChainedOrbs ////
            enemies = HC_room10_TOM1.ReturnEnemiesInRoom(AllEnemies);
            chainedOrbs.Add(new ChainedOrb(new EnemyShieldOrb(enemies[1]), new EnemyTurret(enemies[0])));
            enemies = HC_room10_TOM2.ReturnEnemiesInRoom(AllEnemies);
            chainedOrbs.Add(new ChainedOrb(new EnemyShieldOrb(enemies[1]), new EnemyTurret(enemies[0])));

            // Room 5, under bridge
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67787, -22721, 2196)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67699, -25881, 1952), new QuaternionAngle(-0.71f, 0.00f, 0.00f, 0.71f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 1, HorizontalSpeed = 1, VerticalAngle = 30 }));
            chainedOrbs_Rooms.Add(layout);

            // Room 8
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53445, 1526, 5968)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53165, -143, 5798), new Angle(0.98f, 0.20f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 30, VerticalAngle = 20 }));
            chainedOrbs_Rooms.Add(layout);


            // Room 9
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(56267, 9722, 6186)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51498, 11712, 5786), new Angle(0.54f, 0.84f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 1, HorizontalSpeed = 1, VerticalAngle = 20 }));
            chainedOrbs_Rooms.Add(layout);

            // Room 1
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(58095, -65397, 4488), new Angle(-0.68f, 0.73f)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62041, -65397, 4193), new Angle(-0.42f, 0.91f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 1, HorizontalSpeed = 1, VerticalAngle = 35 }));
            chainedOrbs_Rooms.Add(layout);

        }

        public override void RandomizeEnemies(Process game) {
            base.RandomizeEnemies(game);

            //// chainedOrbs ////
            if(chainedOrbs_Rooms?.Count == 0 || chainedOrbs?.Count == 0) return;
            // create available indexes, for random room pick
            List<int> availableIndexes = new List<int>();
            for(int i = 0; i < chainedOrbs_Rooms.Count; i++) {
                chainedOrbs_Rooms[i].ClearRoomObjects();
                availableIndexes.Add(i);
            }

            // chainedOb loop
            for(int i = 0; i < chainedOrbs.Count; i++) {
                // pick random room
                int randomRoom = Config.GetInstance().r.Next(availableIndexes.Count);
                // swap enemies to random room
                List<Enemy> roomEnemies = new List<Enemy>();
                roomEnemies.Add(chainedOrbs[i].orb);
                roomEnemies.AddRange(chainedOrbs[i].attachedEnemies);
                chainedOrbs_Rooms[availableIndexes[randomRoom]].SwapEnemies(roomEnemies.ToArray());
                availableIndexes.RemoveAt(randomRoom);
            }
            // rng chained rooms
            chainedOrbs_Rooms.ForEach(x => x.RandomizeEnemies(GameHook.game));
        }

        public void Gen_Nightmare() {
            throw new System.NotImplementedException();
        }

        protected override void Gen_PerRoom() { }
    }
}
