using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.GameObjects;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.Windows;
using System.Collections.Generic;
using System.Linq;
using static GhostrunnerRNG.Enemies.Enemy;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.Maps {
    class TheClimb : MapCore, IModesMidCV {

        // Before CV
        #region rooms
        private Room room_1 = new Room(new Vector3f(40993, -88500, 788), new Vector3f(43638, -85855, 3355));
        private Room room_2 = new Room(new Vector3f(40356, -95096, 1319), new Vector3f(43451, -92059, 3319));
        private Room room_3 = new Room(new Vector3f(38061, -103028, 1253), new Vector3f(43340, -99754, 4518));
        private Room room_4 = new Room(new Vector3f(26250, -84961, 5063), new Vector3f(33563, -78165, 8043));
        private Room room_5 = new Room(new Vector3f(39664, -93764, 7911), new Vector3f(43955, -88808, 10769));
        private Room room_6 = new Room(new Vector3f(34210, -96651, 35617), new Vector3f(38061, -91424, 37473));

        // After CV
        private Room room_7 = new Room(new Vector3f(34349, -110338, 34550), new Vector3f(39229, -96696, 38252));
        private Room room_8 = new Room(new Vector3f(24390, -94999, 37350), new Vector3f(34033, -92643, 40013));
        private Room room_9 = new Room(new Vector3f(31865, -90089, 38183), new Vector3f(40204, -77399, 43482));
        #endregion

        #region HC Rooms
        private Room HC_room_1 = new Room(new Vector3f(109014, -25020, -19985), new Vector3f(41618, -76764, 3302)); // from start to after redsea, no cp
        private Room HC_room_2 = new Room(new Vector3f(39667, -78169, 4455), new Vector3f(45083, -97589, 847)); // drone, orb, uzi, shielder, waver
        private Room HC_room_3 = new Room(new Vector3f(44293, -97599, 4287), new Vector3f(37286, -102921, 1260)); // 4 uzi, 1 turret
        private Room HC_room_4 = new Room(new Vector3f(34028, -87236, 5417), new Vector3f(25833, -77923, 7979)); // uzi, 2 frogger, waver, pistol, splitter
        private Room HC_room_5 = new Room(new Vector3f(38800, -87989, 7522), new Vector3f(44463, -94685, 10576));  // 3 crawlers, waver, uzi
        private Room HC_room_6 = new Room(new Vector3f(49452, -88716, 11026), new Vector3f(51314, -97981, 35175)); // drone, waver, 2 weebs
        private Room HC_room_7 = new Room(new Vector3f(36446, -91188, 37157), new Vector3f(34146, -96660, 35294)); // 3 weebs, sniper
        // ---
        private Room HC_room_8 = new Room(new Vector3f(39614, -96602, 34430), new Vector3f(34319, -110458, 37523)); // waver, weeb, uzi, 2 pistols, shield orb
        private Room HC_room_9 = new Room(new Vector3f(24119, -100096, 41585), new Vector3f(33954, -91254, 36346));  // 4 shield orbs, waver, frogger, uzi
        private Room HC_room_10 = new Room(new Vector3f(30965, -90655, 42651), new Vector3f(40869, -77255, 38094)); // 4 drones, no cp
        #endregion

        public TheClimb() : base(MapType.TheClimb, BeforeCV: GameHook.xPos > 70000) {
            
        }

        public void Gen_Normal_BeforeCV() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game);
            
            Rooms = new List<RoomLayout>();
            RoomLayout layout;

            //// room 1 layout ////
            List<Enemy> enemies = room_1.ReturnEnemiesInRoom(AllEnemies);
            EnemyShieldOrb shieldOrb1 = new EnemyShieldOrb(enemies[0]);
            layout = new RoomLayout(shieldOrb1); //orb
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42793, -87934, 2868))); // top of board
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(43265, -87967, 2867))); // ""
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(43595, -87582, 2867))); // ""
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(43607, -86996, 2867))); // ""
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41064, -85903, 1553), new Vector3f(41054, -85079, 1868))); // left board
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42493, -87853, 2192), new Vector3f(43475, -86852, 2657))); // middle of 2 boards
            Rooms.Add(layout);
            

            layout = new RoomLayout(enemies[1]); //pistol 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41154, -83868, 1353), new Vector3f(41920, -85880, 1351), new Angle(0.73f, 0.69f))); // main platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41167, -86184, 1351), new Vector3f(43178, -87691, 1352), new Angle(0.70f, 0.71f))); // default platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41823, -82977, 2128), new Angle(0.73f, 0.68f))); // top of red box
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(40865, -85508, 2598), new Angle(0.60f, 0.80f))); // highplace, above left board

            Rooms.Add(layout);

            //// room 2 layout ////
            enemies = room_2.ReturnEnemiesInRoom(AllEnemies);

            shieldOrb1 = new EnemyShieldOrb(enemies[0]);
            shieldOrb1.LinkObject(1);
            shieldOrb1.HideBeam_Range(0, 2);

            // ORB
            layout = new RoomLayout(shieldOrb1);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(40763, -94801, 2352), new Vector3f(42273, -94740, 2841))); // far board
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(40669, -94750, 1974), new Vector3f(40749, -93083, 2692))); // left board
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(43342, -94759, 2702))); // top of lamp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42320, -93708, 2891))); // mid air
            Rooms.Add(layout);

            // 2 pistols
            layout = new RoomLayout(enemies[1], enemies[2]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(43152, -94586, 2203), new Vector3f(42110, -92444, 2203), new Angle(0.95f, 0.30f))); // main platform, right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41645, -92231, 1903), new Vector3f(40706, -93731, 1903), new Angle(0.67f, 0.74f))); // main platform, left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41812, -91870, 2883), new Angle(0.76f, 0.65f))); // top board, right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(40498, -93073, 2736), new Angle(0.45f, 0.89f))); // top board, left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41160, -95131, 3106), new Angle(0.68f, 0.73f))); // top board, middle
            Rooms.Add(layout);

            ///// Room 3 layout ////
            enemies = room_3.ReturnEnemiesInRoom(AllEnemies);

            shieldOrb1 = new EnemyShieldOrb(enemies[0]);
            shieldOrb1.LinkObject(1);
            shieldOrb1.HideBeam_Range(0, 2);

            //orb
            layout = new RoomLayout(shieldOrb1); 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(40630, -99888, 3348), new Vector3f(39787, -100567, 3969)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41928, -102540, 2555), new Vector3f(42390, -101542, 3340)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(38478, -102376, 3597)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(40100, -102488, 2873)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(40631, -100776, 4201)));
            Rooms.Add(layout);

            //3 pistols
            layout = new RoomLayout(enemies[1], enemies[2], enemies[3]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42538, -100185, 2099), new Vector3f(41700, -102756, 2101), new Angle(0.69f, 0.72f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41553, -102429, 2100), new Vector3f(40336, -101622, 2101), new Angle(0.72f, 0.70f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(40396, -101484, 2101), new Vector3f(39754, -100284, 2102), new Angle(0.72f, 0.69f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(38984, -101558, 2790), new Vector3f(38353, -102164, 2790), new Angle(0.45f, 0.89f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41185, -100711, 3350), new Vector3f(41149, -99466, 3348)).RandomAngle());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42378, -100928, 3507), new Angle(-0.99f, 0.13f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(39186, -102388, 3597), new Angle(0.48f, 0.87f)));
            Rooms.Add(layout);


            ///// Room 4 layout ////
            enemies = room_4.ReturnEnemiesInRoom(AllEnemies);
            shieldOrb1 = new EnemyShieldOrb(enemies[1]);
            EnemyShieldOrb shieldOrb2 = new EnemyShieldOrb(enemies[0]);
            shieldOrb2.HideBeam_Range(0, 3);
            shieldOrb2.LinkObject(1);

            //orbs
            layout = new RoomLayout(shieldOrb1, shieldOrb2);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(32801, -81293, 7200), new Vector3f(32963, -78668, 6500)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(30726, -79067, 6810)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(32963, -78668, 6858), new Vector3f(31444, -80495, 6754)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(28921, -79179, 7048)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(27212, -82286, 7035), new Angle(-0.17f, 0.99f)));
            Rooms.Add(layout);

            // 3 pistols
            layout = new RoomLayout(enemies.Skip(2).ToList()); 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(31224, -83811, 6498), new Vector3f(32833, -80718, 6500), new Angle(-0.77f, 0.64f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(30394, -79285, 6251), new Vector3f(27117, -80093, 6250), new Angle(-0.68f, 0.73f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(29692, -81998, 6247), new Vector3f(30632, -84009, 6246), new Angle(-0.67f, 0.74f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(28368, -81738, 6241), new Vector3f(27986, -82445, 6237), new Angle(0.04f, 1.00f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(33492, -84596, 8068), new Angle(0.85f, 0.53f)));   // near button
            Rooms.Add(layout);


            ///// Room 5 layout ////
            enemies = room_5.ReturnEnemiesInRoom(AllEnemies);
            // ORB objects
            shieldOrb1 = new EnemyShieldOrb(enemies[1]);
            shieldOrb1.HideBeam_Range(0, 2);
            shieldOrb1.LinkObject(2);

            shieldOrb2 = new EnemyShieldOrb(enemies[0]);
            shieldOrb2.HideBeam_Range(0, 4);
            shieldOrb2.HideBeam_Range(1, 4);
            shieldOrb2.HideBeam_Range(2, 4);
            shieldOrb2.HideBeam_Range(3, 4);
            shieldOrb2.LinkObject(4);

            // orbs
            layout = new RoomLayout(shieldOrb1, shieldOrb2);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(40772, -91647, 9705)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41699, -89639, 10120)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(39926, -91072, 10094)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(43102, -93593, 8771)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41860, -90131, 8610)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41530, -90918, 9613)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42568, -92155, 9256), new Vector3f(40759, -91801, 9497)));
            Rooms.Add(layout);

            // 4 pistols
            layout = new RoomLayout(enemies.Skip(2).ToList());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41680, -90491, 8378), new Vector3f(40199, -93334, 8378), new Angle(0.51f, 0.86f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42954, -93438, 8378), new Vector3f(42010, -92739, 8378), new Angle(0.77f, 0.64f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(43146, -92084, 8680), new Vector3f(42346, -90800, 8680), new Angle(0.75f, 0.66f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(39926, -90419, 9115), new Angle(0.06f, 1.00f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42015, -90092, 9525), new Angle(0.59f, 0.80f)));
            // billboards
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(39752, -90986, 10402), new Angle(0.31f, 0.95f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(40771, -92317, 9704), new Angle(0.49f, 0.87f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42309, -92393, 9374), new Angle(0.72f, 0.70f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42146, -93878, 9785), new Angle(0.78f, 0.63f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(43433, -92550, 9786), new Angle(0.93f, 0.37f)));
            Rooms.Add(layout);

            ///// Room 6 layout ////
            layout = new RoomLayout(room_6.ReturnEnemiesInRoom(AllEnemies)); // 7 pistols
            // first section
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(37964, -94732, 36716), new Vector3f(36792, -94069, 36716), new Angle(0.01f, 1.00f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36621, -92779, 36078), new Vector3f(37918, -91503, 36078), new Angle(-0.72f, 0.69f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(37827, -94218, 36078), new Angle(0.79f, 0.61f)));
            // room with pillar
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35441, -91389, 36078), new Vector3f(36175, -94270, 36078), new Angle(0.71f, 0.70f)).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35057, -94213, 36078), new Vector3f(34456, -91576, 36078), new Angle(0.47f, 0.88f)).SetMaxEnemies(2));
            // before elevator
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34430, -96365, 36078), new Vector3f(34759, -94577, 36078), new Angle(0.70f, 0.71f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35528, -96482, 36078), new Vector3f(35058, -95757, 36078), new Angle(1.00f, 0.06f)));
            // high areas
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35025, -94375, 36725), new Angle(0.65f, 0.76f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34338, -91380, 36415), new Angle(-0.04f, 1.00f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34283, -92406, 36785), new Angle(0.20f, 0.98f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36895, -91217, 36740), new Angle(-0.69f, 0.72f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35270, -91217, 36740), new Angle(-0.13f, 0.99f)));

            Rooms.Add(layout);
        }

        public void Gen_Normal_AfterCV() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game, 0, 22);

            Rooms = new List<RoomLayout>();
            RoomLayout layout;

            //// room 7 layout (room 1 after cv) ////
            List<Enemy> enemies = room_7.ReturnEnemiesInRoom(AllEnemies);

            EnemyShieldOrb shieldOrb1 = new EnemyShieldOrb(enemies[1]);
            shieldOrb1.HideBeam_Range(0, 4); 
            shieldOrb1.HideBeam_Range(1, 2); 
            shieldOrb1.HideBeam_Range(2, 2); 
            shieldOrb1.LinkObject(3);

            EnemyShieldOrb shieldOrb2 = new EnemyShieldOrb(enemies[0]);
            shieldOrb2.HideBeam_Range(0, 3);
            shieldOrb2.HideBeam_Range(1, 3); 
            shieldOrb2.HideBeam_Range(2, 3); 
            shieldOrb2.HideBeam_Range(3, 3);
            shieldOrb2.LinkObject(4);

            // orbs
            layout = new RoomLayout(shieldOrb1, shieldOrb2);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36199, -102430, 36315), new Vector3f(35921, -104041, 35664)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(37580, -110193, 36965), new Vector3f(35062, -110185, 36295)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35600, -105332, 36137), new Vector3f(34698, -105495, 36134)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34957, -98829, 35625)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36150, -101362, 36984), new Vector3f(36237, -104616, 37736)));
            Rooms.Add(layout);

            // pistols
            layout = new RoomLayout(enemies.Skip(2).ToList());
            // main platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36567, -98115, 34994), new Vector3f(35519, -99690, 34994), new Angle(0.65f, 0.76f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34600, -99735, 35000), new Vector3f(35195, -102877, 35002), new Angle(0.67f, 0.74f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(37940, -101879, 35005), new Vector3f(35510, -105242, 35002), new Angle(0.73f, 0.69f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34854, -109647, 35002), new Vector3f(36564, -107503, 35000), new Angle(0.74f, 0.67f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(37263, -109793, 35848), new Vector3f(34914, -110132, 35854), new Angle(0.71f, 0.70f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35063, -106732, 36597), new Angle(0.65f, 0.76f)));
            // high places
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34440, -108446, 36509), new Angle(0.19f, 0.98f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36506, -103367, 36534), new Angle(0.75f, 0.66f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34355, -105087, 36392), new Angle(0.36f, 0.93f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(38326, -100736, 35778), new Angle(0.98f, 0.18f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36539, -103047, 37688), new Angle(0.78f, 0.62f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(38325, -104677, 35298), new Angle(0.94f, 0.34f)));
            Rooms.Add(layout);

            //// room 8 layout (room 2 after cv) ////
            enemies = room_8.ReturnEnemiesInRoom(AllEnemies);

            shieldOrb1 = new EnemyShieldOrb(enemies[0]);

            shieldOrb2 = new EnemyShieldOrb(enemies[1]);
            shieldOrb2.HideBeam_Range(0, 2);
            shieldOrb2.HideBeam_Range(1, 1); ///////////
            shieldOrb2.HideBeam_Range(2, 2);
            shieldOrb2.LinkObject(3);

            // orbs
            layout = new RoomLayout(shieldOrb1, shieldOrb2);  
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(32656, -94724, 38985), new Vector3f(31683, -93829, 38580)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(30027, -93905, 38775), new Vector3f(28794, -94487, 38408)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(28027, -93197, 39472), new Vector3f(26791, -93620, 39126)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(28501, -94379, 39179), new Angle(-0.98f, 0.20f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24772, -93841, 37917), new Angle(-0.37f, 0.93f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(31008, -93832, 39241), new Angle(0.11f, 0.99f)));
            Rooms.Add(layout);

            // pistols
            layout = new RoomLayout(enemies.Skip(2).ToList()); 
            // main sections
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(33565, -94684, 37800), new Vector3f(28826, -93620, 37800), new Angle(-0.95f, 0.33f)).SetMaxEnemies(4));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(28039, -93617, 37798), new Vector3f(27433, -94574, 37801), new Angle(-0.89f, 0.45f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(26912, -93645, 37796), new Vector3f(25034, -94323, 37805), new Angle(-0.58f, 0.82f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(27188, -94754, 38697), new Vector3f(27992, -94347, 38697), new Angle(-0.77f, 0.64f)));
            // high areas
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(29996, -94089, 38991), new Angle(-1.00f, 0.01f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(31925, -93749, 39120), new Angle(-1.00f, 0.03f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(33488, -93409, 38480), new Angle(-0.99f, 0.14f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(27090, -93440, 38467), new Angle(-0.83f, 0.56f)));
            Rooms.Add(layout);

            //// room 9 layout (room 3 after cv) ////
            enemies = room_9.ReturnEnemiesInRoom(AllEnemies);

            shieldOrb1 = new EnemyShieldOrb(enemies[0]);
            shieldOrb2 = new EnemyShieldOrb(enemies[1]);
            shieldOrb2.HideBeam_Range(0, 7);
            shieldOrb2.HideBeam_Range(1, 3);
            shieldOrb2.HideBeam_Range(2, 3);
            shieldOrb2.LinkObject(3);
            EnemyShieldOrb shieldOrb3 = new EnemyShieldOrb(enemies[2]);
            shieldOrb3.HideBeam_Range(0, 5); 
            shieldOrb3.HideBeam_Range(1, 5); 
            shieldOrb3.HideBeam_Range(2, 3); 
            shieldOrb3.LinkObject(2);

            // orbs
            layout = new RoomLayout(shieldOrb1, shieldOrb2, shieldOrb3); 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(39018, -83449, 40132), new Vector3f(38939, -79619, 41707)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(33321, -79567, 41707), new Vector3f(33240, -83034, 40020)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34728, -78034, 40088), new Vector3f(33554, -79341, 41649)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(37262, -79192, 39778), new Vector3f(38264, -78247, 41943)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35220, -79432, 40065), new Vector3f(35179, -80470, 41335)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(37027, -79457, 39944), new Vector3f(37135, -80283, 41072)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(37121, -83597, 39833), new Vector3f(37209, -84392, 41374)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35125, -83705, 39833), new Vector3f(35124, -84483, 41458)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(37313, -85117, 39861), new Vector3f(38792, -85130, 41517)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35100, -85098, 41517), new Vector3f(33439, -85117, 40196)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(33694, -87285, 41472), new Vector3f(34643, -86731, 40153)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(37516, -87248, 40025), new Vector3f(38178, -87282, 41399)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(37052, -87505, 41399), new Vector3f(37202, -88617, 39971)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35170, -87558, 39971), new Vector3f(35183, -88348, 41425)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(33499, -89026, 41723), new Vector3f(34567, -89990, 40359)));
            Rooms.Add(layout);
           
            layout = new RoomLayout(enemies.Skip(3).ToList());  // pistols
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36455, -78195, 39302), new Vector3f(35669, -81768, 39302), new Angle(-0.72f, 0.69f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35810, -83480, 39302), new Vector3f(36540, -85379, 39300), new Angle(-0.70f, 0.71f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35245, -86979, 39300), new Vector3f(37026, -87850, 39300), new Angle(0.99f, 0.14f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36553, -88503, 39300), new Vector3f(37031, -90624, 39300), new Angle(0.77f, 0.64f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36085, -82561, 39628), new Angle(-0.73f, 0.69f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36109, -86216, 39628), new Angle(-0.99f, 0.13f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36250, -89913, 39628), new Vector3f(35954, -88709, 39628), new Angle(0.71f, 0.70f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35632, -89714, 40250), new Vector3f(35436, -90293, 40250), new Angle(0.69f, 0.72f)));
            // high places
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35632, -89714, 40250), new Vector3f(35436, -90293, 40250), new Angle(0.69f, 0.72f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(39348, -82164, 41688), new Angle(-1.00f, 0.10f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(39068, -85413, 42097), new Angle(-0.99f, 0.13f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(33705, -86431, 42087), new Angle(0.07f, 1.00f)));
            Rooms.Add(layout);
        }

        public void Gen_Easy_BeforeCV() {
            // Easy mode - before CV
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game);

            Rooms = new List<RoomLayout>();
            RoomLayout layout;

            //// room 1 layout ////
            List<Enemy> enemies = room_1.ReturnEnemiesInRoom(AllEnemies);
            EnemyShieldOrb shieldOrb1 = new EnemyShieldOrb(enemies[0]);
            layout = new RoomLayout(shieldOrb1); //orb
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41064, -85903, 1553), new Vector3f(41054, -85079, 1868))); // left board
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42493, -87853, 2192), new Vector3f(43475, -86852, 2657))); // middle of 2 boards
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(43303, -86029, 1352))); // behind lamp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(43083, -87627, 1514))); // floating under billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41140, -88052, 1523))); // opposite platform corner
            Rooms.Add(layout);

            layout = new RoomLayout(enemies[1]); //pistol 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41154, -83868, 1353), new Vector3f(41920, -85880, 1351), new Angle(0.73f, 0.69f))); // main platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41167, -86184, 1351), new Vector3f(43178, -87691, 1352), new Angle(0.70f, 0.71f))); // default platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41823, -82977, 2128), new Angle(0.73f, 0.68f))); // top of red box
            Rooms.Add(layout);

            //// room 2 layout ////
            enemies = room_2.ReturnEnemiesInRoom(AllEnemies);

            shieldOrb1 = new EnemyShieldOrb(enemies[0]);
            shieldOrb1.LinkObject(1);
            shieldOrb1.HideBeam_Range(0, 2);

            // ORB
            layout = new RoomLayout(shieldOrb1);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(40763, -94801, 2352), new Vector3f(42273, -94740, 2841))); // far board
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(40669, -94750, 1974), new Vector3f(40749, -93083, 2692))); // left board
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(43342, -94759, 2702))); // top of lamp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42320, -93708, 2891))); // mid air
            Rooms.Add(layout);

            // 2 pistols
            layout = new RoomLayout(enemies[1], enemies[2]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(43152, -94586, 2203), new Vector3f(42110, -92444, 2203), new Angle(0.95f, 0.30f))); // main platform, right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41645, -92231, 1903), new Vector3f(40706, -93731, 1903), new Angle(0.67f, 0.74f))); // main platform, left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(40462, -94774, 2736), new Angle(0.45f, 0.89f))); // top board, left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41736, -93814, 2197), new Angle(0.73f, 0.69f))); // lamp

            Rooms.Add(layout);

            ///// Room 3 layout ////
            enemies = room_3.ReturnEnemiesInRoom(AllEnemies);

            shieldOrb1 = new EnemyShieldOrb(enemies[0]);
            shieldOrb1.LinkObject(1);
            shieldOrb1.HideBeam_Range(0, 2);

            //orb
            layout = new RoomLayout(shieldOrb1);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(40630, -99888, 3348), new Vector3f(39787, -100567, 3969)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(39988, -101886, 2102), new Vector3f(40677, -102613, 2628)).AsVerticalPlane()); // billboard 1
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(40856, -102741, 2407), new Vector3f(41783, -102726, 2926)).AsVerticalPlane()); // billboard 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42052, -102532, 2616), new Vector3f(42655, -101695, 3086)).AsVerticalPlane()); // billboard 3
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42659, -101491, 2781), new Vector3f(41912, -100647, 3361)).AsVerticalPlane()); // billboard 4
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(38470, -102222, 3177))); // corner billboard, left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(40100, -102488, 2873))); // red box
            Rooms.Add(layout);

            //3 pistols
            layout = new RoomLayout(enemies[1], enemies[2], enemies[3]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42538, -100185, 2099), new Vector3f(41700, -102756, 2101), new Angle(0.69f, 0.72f))); // default platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41553, -102429, 2100), new Vector3f(40336, -101622, 2101), new Angle(0.72f, 0.70f))); // default platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(40396, -101484, 2101), new Vector3f(39754, -100284, 2102), new Angle(0.72f, 0.69f))); // default platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(38984, -101558, 2790), new Vector3f(38353, -102164, 2790), new Angle(0.45f, 0.89f))); // default platform, higher
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41185, -100711, 3350), new Vector3f(41149, -99466, 3348)).RandomAngle()); // default platform
            
            Rooms.Add(layout);


            ///// Room 4 layout ////
            enemies = room_4.ReturnEnemiesInRoom(AllEnemies);
            shieldOrb1 = new EnemyShieldOrb(enemies[1]);
            EnemyShieldOrb shieldOrb2 = new EnemyShieldOrb(enemies[0]);
            shieldOrb2.HideBeam_Range(0, 3);
            shieldOrb2.LinkObject(1);

            //orbs
            layout = new RoomLayout(shieldOrb1, shieldOrb2);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(32801, -81293, 7200))); // default billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(30726, -79067, 6810))); // pipes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(32963, -78668, 6858))); // platform, floating
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(27212, -82286, 7035))); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(26616, -81177, 6419))); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(28429, -79361, 6704))); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(30632, -81733, 6858))); // boxes in middle (white)
            Rooms.Add(layout);

            // 3 pistols
            layout = new RoomLayout(enemies.Skip(2).ToList());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(31224, -83811, 6498), new Vector3f(32833, -80718, 6500), new Angle(-0.77f, 0.64f))); // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(30394, -79285, 6251), new Vector3f(27117, -80093, 6250), new Angle(-0.68f, 0.73f))); // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(29692, -81998, 6247), new Vector3f(30632, -84009, 6246), new Angle(-0.67f, 0.74f))); // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(28368, -81738, 6241), new Vector3f(27986, -82445, 6237), new Angle(0.04f, 1.00f))); // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(33492, -84596, 8068), new Angle(0.85f, 0.53f)));   // near button
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(30635, -80808, 6858), new Angle(-0.77f, 0.64f))); // boxes in middle (red)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(32711, -84184, 7161), new Angle(0.77f, 0.64f))); // stairs, second floor
            Rooms.Add(layout);


            ///// Room 5 layout ////
            enemies = room_5.ReturnEnemiesInRoom(AllEnemies);
            // ORB objects
            shieldOrb1 = new EnemyShieldOrb(enemies[1]);
            shieldOrb1.HideBeam_Range(0, 2);
            shieldOrb1.LinkObject(2);

            shieldOrb2 = new EnemyShieldOrb(enemies[0]);
            shieldOrb2.HideBeam_Range(0, 4);
            shieldOrb2.HideBeam_Range(1, 4);
            shieldOrb2.HideBeam_Range(2, 4);
            shieldOrb2.HideBeam_Range(3, 4);
            shieldOrb2.LinkObject(4);

            // orbs
            layout = new RoomLayout(shieldOrb1, shieldOrb2);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(40712, -91710, 9217))); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41699, -89639, 10120))); // crate
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(39853, -91073, 9724))); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41796, -92307, 8960))); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41860, -90131, 8610))); // default platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(43406, -91557, 8770))); // platform railing
            Rooms.Add(layout);

            // 4 pistols
            layout = new RoomLayout(enemies.Skip(2).ToList());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41680, -90491, 8378), new Vector3f(40199, -93334, 8378), new Angle(0.51f, 0.86f))); // default platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42954, -93438, 8378), new Vector3f(42010, -92739, 8378), new Angle(0.77f, 0.64f))); // default platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(43146, -92084, 8680), new Vector3f(42346, -90800, 8680), new Angle(0.75f, 0.66f))); // default platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(39926, -90419, 9115), new Angle(0.06f, 1.00f))); // near button
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42015, -90092, 9525), new Angle(0.59f, 0.80f))); // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(43413, -89019, 9528), new Vector3f(42609, -89497, 9528), new Angle(0.77f, 0.64f))); // default, first enemy
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41247, -92586, 9374), new Angle(0.80f, 0.60f))); // billboard
            Rooms.Add(layout);


            ///// Room 6 layout ////
            layout = new RoomLayout(room_6.ReturnEnemiesInRoom(AllEnemies)); // 7 pistols
            // first section
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(37964, -94732, 36716), new Vector3f(36792, -94069, 36716), new Angle(0.01f, 1.00f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36621, -92779, 36078), new Vector3f(37918, -91503, 36078), new Angle(-0.72f, 0.69f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(37827, -94218, 36078), new Angle(0.79f, 0.61f)));
            // room with pillar
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35441, -91389, 36078), new Vector3f(36175, -94270, 36078), new Angle(0.71f, 0.70f)).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35057, -94213, 36078), new Vector3f(34456, -91576, 36078), new Angle(0.47f, 0.88f)).SetMaxEnemies(2));
            // before elevator
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34430, -96365, 36078), new Vector3f(34759, -94577, 36078), new Angle(0.70f, 0.71f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35528, -96482, 36078), new Vector3f(35058, -95757, 36078), new Angle(1.00f, 0.06f)));
            // high areas
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34338, -91380, 36415), new Angle(-0.04f, 1.00f))); // small platform

            Rooms.Add(layout);
        }

        public void Gen_Easy_AfterCV() {
            // Easy Mode - after CV
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game, 0, 22);

            Rooms = new List<RoomLayout>();
            RoomLayout layout;

            //// room 7 layout (room 1 after cv) ////
            List<Enemy> enemies = room_7.ReturnEnemiesInRoom(AllEnemies);

            EnemyShieldOrb shieldOrb1 = new EnemyShieldOrb(enemies[1]);
            shieldOrb1.HideBeam_Range(0, 4);
            shieldOrb1.HideBeam_Range(1, 2);
            shieldOrb1.HideBeam_Range(2, 2);
            shieldOrb1.LinkObject(3);

            EnemyShieldOrb shieldOrb2 = new EnemyShieldOrb(enemies[0]);
            shieldOrb2.HideBeam_Range(0, 3);
            shieldOrb2.HideBeam_Range(1, 3);
            shieldOrb2.HideBeam_Range(2, 3);
            shieldOrb2.HideBeam_Range(3, 3);
            shieldOrb2.LinkObject(4);

            // orbs
            layout = new RoomLayout(shieldOrb1, shieldOrb2);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36303, -103044, 35943)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(37580, -110193, 36965), new Vector3f(35062, -110185, 36295)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35600, -105332, 36137), new Vector3f(34698, -105495, 36134)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36030, -100854, 36872)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35576, -106782, 35677))); // stairs
            Rooms.Add(layout);

            // pistols
            layout = new RoomLayout(enemies.Skip(2).ToList());
            // main platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36567, -98115, 34994), new Vector3f(35519, -99690, 34994), new Angle(0.65f, 0.76f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34600, -99735, 35000), new Vector3f(35195, -102877, 35002), new Angle(0.67f, 0.74f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(37940, -101879, 35005), new Vector3f(35510, -105242, 35002), new Angle(0.73f, 0.69f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34854, -109647, 35002), new Vector3f(36564, -107503, 35000), new Angle(0.74f, 0.67f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(37263, -109793, 35848), new Vector3f(34914, -110132, 35854), new Angle(0.71f, 0.70f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35063, -106732, 36597), new Angle(0.65f, 0.76f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36699, -100592, 35398), new Vector3f(35574, -100150, 35400), new Angle(0.71f, 0.70f)));
            // high places
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34507, -105949, 36597), new Angle(0.60f, 0.80f))); // behind button
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(37445, -106272, 35402), new Angle(0.83f, 0.55f))); // small lamp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(38325, -104677, 35298), new Angle(0.94f, 0.34f))); // crates
            Rooms.Add(layout);

            //// room 8 layout (room 2 after cv) ////
            enemies = room_8.ReturnEnemiesInRoom(AllEnemies);

            shieldOrb1 = new EnemyShieldOrb(enemies[0]);

            shieldOrb2 = new EnemyShieldOrb(enemies[1]);
            shieldOrb2.HideBeam_Range(0, 2);
            shieldOrb2.HideBeam_Range(1, 1); ///////////
            shieldOrb2.HideBeam_Range(2, 2);
            shieldOrb2.LinkObject(3);

            // orbs
            layout = new RoomLayout(shieldOrb1, shieldOrb2);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(32122, -94175, 38668)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(30027, -93905, 38775), new Vector3f(28794, -94487, 38408)).AsVerticalPlane());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(28027, -93197, 39472), new Vector3f(26791, -93620, 39126)).AsVerticalPlane());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24772, -93841, 37917)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(32983, -93367, 38040)));
            Rooms.Add(layout);

            // pistols
            layout = new RoomLayout(enemies.Skip(2).ToList());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(33565, -94684, 37800), new Vector3f(28826, -93620, 37800), new Angle(-0.95f, 0.33f)).SetMaxEnemies(4));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(28039, -93617, 37798), new Vector3f(27433, -94574, 37801), new Angle(-0.89f, 0.45f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(26912, -93645, 37796), new Vector3f(25034, -94323, 37805), new Angle(-0.58f, 0.82f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(27188, -94754, 38697), new Vector3f(27992, -94347, 38697), new Angle(-0.77f, 0.64f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(28310, -94627, 38993), new Angle(1.00f, 0.03f))); // lamp near button
            Rooms.Add(layout);


            //// room 9 layout (room 3 after cv) ////
            enemies = room_9.ReturnEnemiesInRoom(AllEnemies);

            shieldOrb1 = new EnemyShieldOrb(enemies[0]);
            shieldOrb2 = new EnemyShieldOrb(enemies[1]);
            shieldOrb2.HideBeam_Range(0, 7);
            shieldOrb2.HideBeam_Range(1, 3);
            shieldOrb2.HideBeam_Range(2, 3);
            shieldOrb2.LinkObject(3);
            EnemyShieldOrb shieldOrb3 = new EnemyShieldOrb(enemies[2]);
            shieldOrb3.HideBeam_Range(0, 5);
            shieldOrb3.HideBeam_Range(1, 5);
            shieldOrb3.HideBeam_Range(2, 3);
            shieldOrb3.LinkObject(2);

            // orbs
            layout = new RoomLayout(shieldOrb1, shieldOrb2, shieldOrb3);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(39035, -83035, 40132), new Vector3f(38939, -79619, 41195)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(33321, -79567, 41228), new Vector3f(33240, -83034, 40020)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34153, -79296, 40890)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(38048, -79186, 40645)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35215, -79631, 40065), new Vector3f(35182, -80589, 41028)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(37128, -79644, 39944), new Vector3f(37115, -80414, 41072)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(37168, -83715, 40115), new Vector3f(37209, -84392, 40862)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35130, -83786, 40256), new Vector3f(35124, -84483, 40952)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(38116, -85093, 40635)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34200, -85057, 40659)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(33694, -87285, 41145), new Vector3f(34729, -87262, 40153)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(37712, -87299, 40025), new Vector3f(38204, -87324, 41244)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(37141, -88509, 40079), new Vector3f(37121, -87754, 41014)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35148, -87648, 40123), new Vector3f(35183, -88348, 41132)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(33357, -88369, 41032)));
            Rooms.Add(layout);

            layout = new RoomLayout(enemies.Skip(3).ToList());  // pistols
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36455, -78195, 39302), new Vector3f(35669, -81768, 39302), new Angle(-0.72f, 0.69f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35810, -83480, 39302), new Vector3f(36540, -85379, 39300), new Angle(-0.70f, 0.71f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35245, -86979, 39300), new Vector3f(37026, -87850, 39300), new Angle(0.99f, 0.14f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36553, -88503, 39300), new Vector3f(37031, -90624, 39300), new Angle(0.77f, 0.64f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36085, -82561, 39628), new Angle(-0.73f, 0.69f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36109, -86216, 39628), new Angle(-0.99f, 0.13f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36250, -89913, 39628), new Vector3f(35954, -88709, 39628), new Angle(0.71f, 0.70f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35632, -89714, 40250), new Vector3f(35436, -90293, 40250), new Angle(0.69f, 0.72f)));
            // high places
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35632, -89714, 40250), new Vector3f(35436, -90293, 40250), new Angle(0.69f, 0.72f)));
            Rooms.Add(layout);
        }

        public void Gen_Hardcore() {
            // HC
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game); // 61 enemies
            Rooms = new List<RoomLayout>();
            RoomLayout layout;
            List<Enemy> enemies;

            // Add all enemies to EnemiesWithoutCP
            enemies = HC_room_1.ReturnEnemiesInRoom(AllEnemies); // All up to redsea end
            enemies[5].SetEnemyType(EnemyTypes.Waver);
            enemies[6].SetEnemyType(EnemyTypes.Waver);

            // 9, 10, 11, 12 - weebs
            // remove weebs
            enemies.RemoveRange(9, 4);
            RemoveParentObjects(ref enemies);
            EnemiesWithoutCP.AddRange(enemies);

            enemies = HC_room_10.ReturnEnemiesInRoom(AllEnemies); // drones in last room, can be done without them
            enemies[0] = new EnemyDrone(enemies[0]);
            enemies[1] = new EnemyDrone(enemies[1]);
            enemies[2] = new EnemyDrone(enemies[2]);
            enemies[3] = new EnemyDrone(enemies[3]);
            RemoveParentObjects(ref enemies);
            EnemiesWithoutCP.AddRange(enemies);

            enemies = HC_room_2.ReturnEnemiesInRoom(AllEnemies);
            // Room 1 - up to end of redSea
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(86882, -36789, 301), new Angle(-0.34f, 0.94f)).Mask(SpawnPlane.Mask_Flatground)); // trailer hallway
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(83424, -36462, 270), new Angle(0, 1.00f)).Mask(SpawnPlane.Mask_Flatground)); // ""
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(70445, -35012, 2152), new Vector3f(72357, -34713, 2152), new Angle(0.02f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // catwalk, redsea start
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(59701, -39820, 488), new Vector3f(62440, -40648, 496), new Angle(-0.01f, 1.00f)).Mask(SpawnPlane.Mask_Flatground)); // catwalk, redsea
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(54122, -40654, 489), new Vector3f(57004, -39895, 496), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Flatground)); // catwalk, redsea
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41856, -56493, 1198), new Vector3f(41470, -54068, 1198), new Angle(0.71f, 0.70f))
                .Mask(SpawnPlane.Mask_Highground).SetMaxEnemies(2)); // 3nd last platform, redsea
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41604, -62651, 1335), new Angle(0.68f, 0.73f)).Mask(SpawnPlane.Mask_Highground));  // concrete slide edge

            // drones
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41982, -54232, 1625), new Vector3f(41445, -56451, 1625), new Angle(0.72f, 0.69f)).Mask(SpawnPlane.Mask_Airborne)); // above last 2nd platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(54163, -40303, 1004), new Angle(0.01f, 1.00f)).Mask(SpawnPlane.Mask_Airborne)); // above catwalk
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(70780, -34855, 2372), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Airborne));  // above first catwalk

            // turret
            var info = new TurretSpawnInfo(); // first catwalk, aiming towards SR DSJ path
            info.VerticalAngle = 45;
            info.HorizontalSpeed = 0;
            info.HorizontalAngle = 0;
            info.SetRange(5000);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(70649, -36373, 304), new Angle(1.00f, 0.01f)).Mask(SpawnPlane.Mask_Turret).SetSpawnInfo(info));

            Rooms.Add(layout);

            ////  Room 2 ////
            enemies = HC_room_2.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[1] = new EnemyDrone(enemies[1]);
            enemies[4].SetEnemyType(EnemyTypes.Waver);

            DetachEnemyFromCP(ref enemies, enemyIndex: 1); // drone

            layout = new RoomLayout(enemies[0]); // orb
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41381, -84118, 2183), new Vector3f(42895, -87248, 2703))); // floating mid air
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41373, -91436, 3316))); // floating above billboard, middle section
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(39414, -88348, 1416), new Vector3f(39386, -91131, 1416)).AsVerticalPlane()); // pipe on left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42625, -90629, 1669))); // middle wall, right of billboard
            layout.DoNotReuse();
            Rooms.Add(layout);

            layout = new RoomLayout(enemies.Skip(1).ToList());
            // high places
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41212, -90531, 2598), new Angle(0.63f, 0.78f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41817, -90891, 2883), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // billboard 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42829, -87985, 2868), new Angle(0.78f, 0.63f)).Mask(SpawnPlane.Mask_Highground)); // billboard 3 

            // default (weebs broken here, so masked as highground)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42813, -94354, 2203), new Vector3f(41995, -92787, 2203), new Angle(0.83f, 0.55f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41631, -92419, 1903), new Vector3f(40736, -93124, 1903), new Angle(0.69f, 0.73f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42587, -87831, 1355), new Vector3f(41338, -87105, 1355), new Angle(0.69f, 0.73f)).Mask(SpawnPlane.Mask_Highground));

            // drone
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42249, -94247, 2828), new Vector3f(41096, -92326, 2611), new Angle(0.70f, 0.72f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42176, -90128, 2213), new Vector3f(41479, -88677, 2213), new Angle(0.72f, 0.69f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(43541, -87803, 3251), new Angle(0.90f, 0.44f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(43194, -83977, 2268), new Vector3f(43360, -87014, 3053), new Angle(0.93f, 0.37f)).Mask(SpawnPlane.Mask_Airborne));

            // turret
            info = new TurretSpawnInfo();
            info.HorizontalAngle = 20;
            info.HorizontalSpeed = 40;
            info.VerticalAngle = 10;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42432, -92471, 2200), new Angle(-0.89f, 0.46f)).Mask(SpawnPlane.Mask_Turret).SetSpawnInfo(info));
            Rooms.Add(layout);

            //// Room 3 ////
            enemies = HC_room_3.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyTurret(enemies[0]);
            DetachEnemyFromCP(ref enemies, force:true, enemyIndex: 0);
            layout = new RoomLayout();    // TURRET
            // ceiling of middle platform
            info = new TurretSpawnInfo(); 
            info.VerticalAngle = 40;
            info.HorizontalAngle = 30;
            info.HorizontalSpeed = 40;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(40886, -100243, 2934), new QuaternionAngle(-0.66f, 0.75f, 0, 0)).SetSpawnInfo(info).Mask(SpawnPlane.Mask_Turret));

            // third orange platform
            info = new TurretSpawnInfo();
            info.VerticalAngle = 45;
            info.HorizontalAngle = 30;
            info.HorizontalSpeed = 40;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42269, -100918, 3074), new QuaternionAngle(0.70f, 0.06f, -0.50f, 0.50f)).SetSpawnInfo(info).Mask(SpawnPlane.Mask_Turret));

            // top platform corner, aiming down
            info = new TurretSpawnInfo();
            info.VerticalAngle = -45;
            info.HorizontalAngle = 45;
            info.HorizontalSpeed = 40;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41187, -100843, 3348), new Angle(-0.70f, 0.72f)).SetSpawnInfo(info).Mask(SpawnPlane.Mask_Turret));

            // triple  crates, aiming towards all platform
            info = new TurretSpawnInfo();
            info.VerticalAngle = 0;
            info.HorizontalAngle = 35;
            info.HorizontalSpeed = 40;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(39961, -102152, 2873), new Angle(0.17f, 0.99f)).Mask(SpawnPlane.Mask_Turret));

            // first floating orange platform, backside
            info = new TurretSpawnInfo();
            info.VerticalAngle = 40;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41431, -99498, 2305), new QuaternionAngle(-0.43f, 0.56f, -0.56f, 0.43f)).SetSpawnInfo(info).Mask(SpawnPlane.Mask_Turret));

            // corner platform, normal spawn
            info = new TurretSpawnInfo();
            info.VerticalAngle = 10;
            info.HorizontalAngle = 0;
            info.HorizontalSpeed = 0;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42547, -102650, 2101), new Angle(0.97f, 0.23f)).SetSpawnInfo(info).Mask(SpawnPlane.Mask_Turret));
            Rooms.Add(layout);

            // 4 UZI
            DetachEnemyFromCP(ref enemies, force: true, enemyIndex: 1); // take one uzi
            layout = new RoomLayout(enemies.ToList());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42270, -101733, 2101), new Vector3f(40523, -102552, 2100), new Angle(0.68f, 0.73f)).Mask(SpawnPlane.Mask_Highground).SetMaxEnemies(2)); // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(40404, -102666, 2873), new Angle(0.67f, 0.74f)).Mask(SpawnPlane.Mask_Highground)); // triple crates 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41365, -102874, 3057), new Angle(0.86f, 0.51f)).Mask(SpawnPlane.Mask_Highground)); // billboard top
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42474, -102240, 3318), new Angle(0.91f, 0.42f)).Mask(SpawnPlane.Mask_Highground)); // billboard top 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(38580, -101316, 2788), new Angle(0.30f, 0.95f)).Mask(SpawnPlane.Mask_Highground)); // uplink platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42501, -100868, 2101), new Angle(0.97f, 0.25f)).Mask(SpawnPlane.Mask_Highground)); // near button
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(39835, -100240, 2102), new Angle(0.37f, 0.93f)).Mask(SpawnPlane.Mask_Highground)); // platform, near light

            // drone
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(38951, -100456, 2969), new Angle(0.20f, 0.98f)).Mask(SpawnPlane.Mask_Airborne)); 
            Rooms.Add(layout);



            //// Room 4 ////
            enemies = HC_room_4.ReturnEnemiesInRoom(AllEnemies);
            enemies[1].SetEnemyType(EnemyTypes.Splitter);
            enemies[3].SetEnemyType(EnemyTypes.Waver);
            DetachEnemyFromCP(ref enemies);
            layout = new RoomLayout(enemies);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(31295, -83884, 6501), new Vector3f(32656, -79210, 6502), new Angle(-0.80f, 0.61f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2).AllowSplitter()); // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(29860, -82168, 6252), new Vector3f(30715, -83285, 6253), new Angle(-0.72f, 0.69f))
                .Mask(SpawnPlane.Mask_Flatground).AllowSplitter()); // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(30680, -80948, 6858), new Vector3f(30603, -81683, 6858), new Angle(-0.79f, 0.61f)).Mask(SpawnPlane.Mask_Highground)); // crates in middle
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(30517, -79477, 6255), new Vector3f(26891, -80370, 6251), new Angle(-0.66f, 0.75f))
                .SetMaxEnemies(2).AllowSplitter().Mask(SpawnPlane.Mask_Flatground)); // default, back

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(28310, -82292, 6237), new Angle(-0.54f, 0.84f))
                .Mask(SpawnPlane.Mask_Flatground)); // extended platform 

            // billboards
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(28003, -79224, 7048), new Vector3f(28950, -79231, 7048), new Angle(-0.68f, 0.73f)) // far back
                .AsVerticalPlane().Mask(SpawnPlane.Mask_Highground));

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(26502, -80707, 6983), new Vector3f(26482, -81611, 6983), new Angle(-0.07f, 1.00f)) // right
                 .AsVerticalPlane().Mask(SpawnPlane.Mask_Highground));

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(33123, -81005, 7297), new Vector3f(33119, -78902, 7298), new Angle(-0.92f, 0.39f)) // left, long
                .AsVerticalPlane().Mask(SpawnPlane.Mask_Highground));

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(32637, -78387, 7258), new Vector3f(31764, -78371, 7258), new Angle(-0.75f, 0.66f)) // left, corner
                 .AsVerticalPlane().Mask(SpawnPlane.Mask_Highground));

            // others
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(32145, -84477, 7158), new Angle(0.97f, 0.23f)).Mask(SpawnPlane.Mask_Highground)); // second floor, button stairs
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(33132, -84073, 8071), new Angle(0.87f, 0.49f)).Mask(SpawnPlane.Mask_Highground)); // third floor, near button 

            // drone spots
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(32318, -83572, 7223), new Vector3f(33405, -81929, 7582), new Angle(-0.97f, 0.23f)).Mask(SpawnPlane.Mask_Airborne)); // above left billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(28260, -83354, 6991), new Vector3f(28972, -81361, 6991), new Angle(-0.44f, 0.90f)) // right area
                .Mask(SpawnPlane.Mask_Airborne).AsVerticalPlane()); 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34068, -85209, 8530), new Angle(0.89f, 0.46f)).Mask(SpawnPlane.Mask_Airborne)); // above button

            // turret spots
            info = new TurretSpawnInfo();
            info.VerticalAngle = 35;
            info.HorizontalSpeed = 30;
            info.HorizontalAngle = 30;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(30939, -80843, 6548), new Angle(-0.29f, 0.96f)).SetSpawnInfo(info).Mask(SpawnPlane.Mask_Turret)); // middle crates, aiming to stairs

            info = new TurretSpawnInfo();
            info.HorizontalSpeed = 40;
            info.VerticalAngle = 0;
            info.HorizontalAngle = 45;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(33368, -85502, 8367), new Angle(0.42f, 0.91f)).SetSpawnInfo(info).Mask(SpawnPlane.Mask_Turret));

            Rooms.Add(layout);



            //// Room 5 ////
            enemies = HC_room_5.ReturnEnemiesInRoom(AllEnemies);
            enemies[4].SetEnemyType(EnemyTypes.Waver);
            layout = new RoomLayout(enemies.Skip(3).ToList());
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42281, -89482, 9528), new Vector3f(43303, -88983, 9528), new Angle(0.73f, 0.69f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(40732, -89155, 9528), new Vector3f(40073, -89592, 9528), new Angle(-0.01f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42997, -90788, 8680), new Vector3f(42334, -92051, 8680), new Angle(0.72f, 0.69f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41783, -93393, 8378), new Vector3f(40206, -92970, 8378), new Angle(0.70f, 0.72f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41800, -92231, 8378), new Vector3f(40037, -91377, 8378), new Angle(0.69f, 0.72f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());

            // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(40340, -91372, 9704), new Angle(0.37f, 0.93f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(40842, -92496, 9704), new Angle(0.50f, 0.87f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41242, -92589, 9374), new Angle(0.63f, 0.78f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42489, -92368, 9374), new Angle(0.72f, 0.69f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(39754, -89905, 10402), new Angle(0.02f, 1.00f)).Mask(SpawnPlane.Mask_HighgroundLimited));

            // high/special spots
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(43506, -92295, 9657), new Angle(0.91f, 0.41f)).setRarity(0.3).Mask(SpawnPlane.Mask_HighgroundLimited));

            // drone spot
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42785, -89812, 10026), new Angle(0.83f, 0.55f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(43167, -91480, 9331), new Angle(0.97f, 0.23f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);



            //// Room 6 ////
            enemies = HC_room_6.ReturnEnemiesInRoom(AllEnemies);
            enemies[0].SetEnemyType(EnemyTypes.Drone);
            enemies[1].SetEnemyType(EnemyTypes.Waver);
            // 2,3 - weebs (skip them)
            DetachEnemyFromCP(ref enemies, force: true, enemyIndex: 0); // drone
            DetachEnemyFromCP(ref enemies, force: true, enemyIndex: 0); // waver

            //// Room 7 ////
            enemies = HC_room_7.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemySniper(enemies[0]);
            enemies[1].SetEnemyType(EnemyTypes.Weeb);
            enemies[2].SetEnemyType(EnemyTypes.Weeb);
            enemies[3].SetEnemyType(EnemyTypes.Weeb);

            layout = new RoomLayout(enemies);
            // sniper spots
            var sniperInfo = new SniperSpawnInfo();
            sniperInfo.AddPatrolPoint(new Vector3f(34995, -91596, 36078));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34447, -95946, 36078), new Angle(0.66f, 0.75f)).Mask(SpawnPlane.Mask_Sniper).SetSpawnInfo(sniperInfo)); // default, more towards back
            sniperInfo = new SniperSpawnInfo();
            sniperInfo.AddPatrolPoint(new Vector3f(36177, -91253, 36078));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35730, -94377, 36726), new Angle(0.69f, 0.72f)).Mask(SpawnPlane.Mask_Sniper).SetSpawnInfo(sniperInfo)); // billboard
            sniperInfo = new SniperSpawnInfo();
            sniperInfo.AddPatrolPoint(new Vector3f(35938, -91704, 36078));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34300, -95346, 36660), new Angle(0.58f, 0.81f)).Mask(SpawnPlane.Mask_Sniper).setRarity(0.2).SetSpawnInfo(sniperInfo)); // above default,

            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36112, -94058, 36078), new Vector3f(35501, -91523, 36078), new Angle(0.51f, 0.86f))
                .SetMaxEnemies(2).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34435, -91765, 36078), new Vector3f(35008, -94197, 36078), new Angle(0.60f, 0.80f))
                .SetMaxEnemies(2).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35548, -96475, 36078), new Vector3f(35068, -95839, 36078), new Angle(0.96f, 0.28f))
                .Mask(SpawnPlane.Mask_Flatground));

            // high/special spots
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34411, -91374, 36411), new Angle(-0.04f, 1.00f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // scaffolding
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34283, -92297, 36785), new Angle(0.23f, 0.97f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34323, -93952, 36774), new Angle(0.45f, 0.89f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35393, -94377, 36726), new Angle(0.68f, 0.73f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34766, -94343, 37133), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_HighgroundLimited).setRarity(0.2)); // pipes
            Rooms.Add(layout);


            //// Room 8 ////
            enemies = HC_room_8.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[4].SetEnemyType(EnemyTypes.Waver);
            enemies[5].SetEnemyType(EnemyTypes.Weeb);

            // orb
            layout = new RoomLayout(enemies[0]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35788, -103164, 36261)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35957, -108801, 36729)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(37062, -105063, 36621)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36907, -105580, 35906)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36239, -102397, 37217)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36454, -106501, 37072)).Mask(SpawnPlane.Mask_ShieldOrb));
            Rooms.Add(layout);

            layout = new RoomLayout(enemies.Skip(1).ToList());
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(37305, -110041, 35848), new Vector3f(34894, -109656, 35854), new Angle(0.70f, 0.72f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35757, -101244, 35401), new Vector3f(35446, -100697, 35401), new Angle(0.70f, 0.72f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36396, -100127, 35400), new Vector3f(35603, -100602, 35401), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());

            // middle stairs platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35511, -106827, 36597), new Vector3f(34796, -106675, 36597), new Angle(0.65f, 0.76f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35553, -105410, 36137), new Vector3f(34759, -105296, 36137), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35508, -106879, 35677), new Vector3f(35080, -106589, 35677), new Angle(0.69f, 0.73f)).Mask(SpawnPlane.Mask_Highground));

            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(38422, -100694, 35778), new Angle(0.99f, 0.15f)).Mask(SpawnPlane.Mask_Highground)); // scaffolding
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(38344, -104722, 35298), new Angle(0.91f, 0.42f)).Mask(SpawnPlane.Mask_Highground)); // crates on right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36025, -97196, 35308), new Angle(-0.66f, 0.75f)).Mask(SpawnPlane.Mask_Highground)); // crate below start
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34328, -104144, 36392), new Vector3f(34314, -105440, 36392), new Angle(0.51f, 0.86f))
                .Mask(SpawnPlane.Mask_Highground).AsVerticalPlane()); // pipes near stairs
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34330, -108841, 37290), new Vector3f(34358, -105940, 37290), new Angle(0.50f, 0.87f))
                .Mask(SpawnPlane.Mask_Highground).AsVerticalPlane().SetMaxEnemies(2)); // pipes above stairs

            // turret (additional)
            info = new TurretSpawnInfo();
            info.VerticalAngle = 10;
            info.HorizontalSpeed = 0;
            info.SetRange(2500);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35582, -106662, 36597), new Angle(0.97f, 0.24f)).Mask(SpawnPlane.Mask_Turret).SetSpawnInfo(info));
            info = new TurretSpawnInfo();
            info.VerticalAngle = 10;
            info.HorizontalSpeed = 40;
            info.HorizontalSpeed = 30;
            info.SetRange(3300);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(37268, -109995, 35848), new Angle(0.99f, 0.13f)).Mask(SpawnPlane.Mask_Turret).SetSpawnInfo(info));

            // drone (additional)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34716, -104685, 36124), new Vector3f(35378, -101404, 36124), new Angle(0.63f, 0.77f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(37116, -108582, 36934), new Angle(0.83f, 0.56f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);


            //// Room 9 ////
            enemies = HC_room_9.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[1] = new EnemyShieldOrb(enemies[1]);
            enemies[2] = new EnemyShieldOrb(enemies[2]);
            enemies[3] = new EnemyShieldOrb(enemies[3]);
            enemies[6].SetEnemyType(EnemyTypes.Waver);

            // orbs
            layout = new RoomLayout(enemies.Take(4).ToList());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(31988, -97082, 38261), new Vector3f(27511, -96047, 38434)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(29181, -92928, 39394)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(31002, -93753, 39014)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(25786, -94183, 39297)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(27447, -97870, 38242), new Vector3f(32265, -97292, 38421)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(31463, -95064, 37283), new Vector3f(27613, -95074, 37283)).SetMaxEnemies(2)); // under platform
            Rooms.Add(layout);

            // enemies
            layout = new RoomLayout(enemies.Skip(4).ToList());
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(25073, -94391, 37805), new Vector3f(26968, -93670, 37796), new Angle(-0.69f, 0.73f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(27252, -93640, 37796), new Vector3f(28066, -94552, 37801), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(28498, -93676, 37800), new Vector3f(32777, -94852, 37798), new Angle(-0.71f, 0.71f)).SetMaxEnemies(3).Mask(SpawnPlane.Mask_Flatground));
            // high
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(28776, -94752, 38991), new Angle(-0.75f, 0.66f)).Mask(SpawnPlane.Mask_Highground)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(26488, -93476, 38468), new Vector3f(27678, -93464, 38468), new Angle(-0.45f, 0.89f)).AsVerticalPlane().Mask(SpawnPlane.Mask_Highground)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(31836, -93688, 39120), new Angle(-0.96f, 0.27f)).Mask(SpawnPlane.Mask_Highground)); // billboard
            // drone (additional)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(29921, -94891, 38421), new Vector3f(32153, -94458, 38704), new Angle(-0.73f, 0.68f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(28136, -93687, 39221), new Angle(-0.78f, 0.62f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(28988, -93417, 38402), new Vector3f(31400, -93530, 38402), new Angle(-0.73f, 0.69f)).Mask(SpawnPlane.Mask_Airborne));
            // turret (additional)
            info = new TurretSpawnInfo();
            info.VerticalAngle = 15;
            info.HorizontalSpeed = 30;
            info.HorizontalAngle = 20;
            info.SetRange(3000);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(29787, -94059, 38602), new QuaternionAngle(-0.18f, 0.68f, 0.68f, -0.18f)).Mask(SpawnPlane.Mask_Turret).SetSpawnInfo(info)); // billboard, aiming towards button
            Rooms.Add(layout);
        }

        public void Gen_Nightmare_BeforeCV() {
            // NM - before cv
        }

        public void Gen_Nightmare_AfterCV() {
            // NM - after CV
        }

        protected override void Gen_PerRoom() {}
    }
}
