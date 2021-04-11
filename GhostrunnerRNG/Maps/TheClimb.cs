using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using System.Collections.Generic;
using System.Linq;
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
        }

        public void Gen_Easy_AfterCV() {
            // Easy Mode - after CV
        }

        public void Gen_SR_BeforeCV() {
            // SR Mode - before CV
        }

        public void Gen_SR_AfterCV() {
            // SR Mode - after CV
        }

        public void Gen_Hardcore_BeforeCV() {
            // HC - before CV
        }

        public void Gen_Hardcore_AfterCV() {
            // HC - after CV
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
