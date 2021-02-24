using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using System.Collections.Generic;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.Maps {
    class BreatheIn : MapCore {

        // Note: Enemies are not in the same order in this map, so we group them by their RoomRectangle

        // not needed for cp
        private Room room_1 = new Room(new Vector3f(184259, -53861, 2461), new Vector3f(184944, -52937, 3032));//first pitol guy    used
        private Room room_2 = new Room(new Vector3f(169261, -78666, 2431), new Vector3f(171584, -77872, 3432));//pistol guy near the fan    used
        private Room room_3 = new Room(new Vector3f(137944, -69982, 3014), new Vector3f(142419, -61581, 5690));//3 guys in room with 2 vertical fans used
        private Room room_4 = new Room(new Vector3f(132223, -59023, 4643), new Vector3f(133541, -57744, 5478));//1 guy on top big fan near slowmo   used
        private Room room_5 = new Room(new Vector3f(107633, -42942, 2799), new Vector3f(116956, -34844, 5556));//3 froggers after fan skip     used
        private Room room_6 = new Room(new Vector3f(99196, -41572, 3792), new Vector3f(102451, -39761, 4934));//2 pistols on the bridge used
        private Room room_7 = new Room(new Vector3f(86180, -50246, 3741), new Vector3f(87136, -49211, 4913));//2 guys in press room     used
        private Room room_8 = new Room(new Vector3f(59991, -59272, 4063), new Vector3f(61472, -57880, 5546));//2guy in the hall with laser and crusher used

        //enemies needed for cp

        private Room room_11 = new Room(new Vector3f(173538, -62396, 2199), new Vector3f(182884, -57998, 3646));//slowmo room
        private Room room_12 = new Room(new Vector3f(166384, -78760, 2379), new Vector3f(167845, -75668, 3411));//2 guys on the left of big room before first bridge
        private Room room_13 = new Room(new Vector3f(163974, -81962, 2244), new Vector3f(172641, -80093, 4042));//4 guysin the hallway of big room before first bridge
        private Room room_14 = new Room(new Vector3f(144970, -89551, 1353), new Vector3f(155147, -80872, 4692));//room before 2 vertical fans and collectible
        private Room room_15 = new Room(new Vector3f(134060, -57933, 3511), new Vector3f(135978, -54139, 4482));//3 guys under big fan
        private Room room_16 = new Room(new Vector3f(88745, -44180, 3777), new Vector3f(89966, -42858, 4723));//2 guys after bridge
        private Room room_17 = new Room(new Vector3f(76326, -56166, 3427), new Vector3f(84585, -50042, 6040));//fight before crane room
        private Room room_18 = new Room(new Vector3f(60506, -76635, 2338), new Vector3f(66520, -69131, 5550));//second to last fight before cv
        private Room room_19 = new Room(new Vector3f(66303, -102064, 2941), new Vector3f(72570, -94987, 5496));//last fight before cv

        // cv flag
        private bool BeforeCV = true;

        public BreatheIn(bool isHC) : base(MapType.BreatheIn) {

            BeforeCV = MainWindow.xPos > 150000.0f;

            if(!isHC) {
                if(BeforeCV)
                    Gen_PerRoom();
            }
        }

        protected override void Gen_PerRoom() {
            List<Enemy> AllEnemies = GetAllEnemies(MainWindow.game);

            Rooms = new List<RoomLayout>();
            RoomLayout layout;

            ////// room 11 layout ////
            List<Enemy> enemies1 = room_7.ReturnEnemiesInRoom(AllEnemies);
            List<Enemy> enemies = room_11.ReturnEnemiesInRoom(AllEnemies);

            layout = new RoomLayout(enemies[0], enemies[1], enemies[2], enemies[3], enemies1[0], enemies1[1]); //4 uzi + pistol+frogger
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(183228, -55397, 3343), new Angle(-1.00f, 0.05f)));//box near slowmo
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(183041, -61969, 4053), new Angle(0.74f, 0.67f)));//crane near collectible
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(176772, -61946, 4053), new Angle(0.34f, 0.94f)));//crane near the fan
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(173433, -60851, 3648), new Angle(0.06f, 1.00f)));//boxes near button
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(180000, -60642, 3508), new Vector3f(179026, -60642, 3508), new Angle(0.69f, 0.72f)));//billboard

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(182734, -60312, 2753), new Vector3f(182258, -56845, 2748), new Angle(0.73f, 0.69f)));//1 guy spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(180395, -60415, 2754), new Vector3f(182179, -59969, 2748), new Angle(0.56f, 0.83f)).SetMaxEnemies(2));//before bilboard 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(178348, -58916, 2748), new Vector3f(177916, -60438, 2748), new Angle(0.37f, 0.93f)));//2 guy spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(176822, -60415, 2754), new Vector3f(177780, -60026, 2748), new Angle(-0.01f, 1.00f)));//platform on the back of the 2 guy 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(175956, -59962, 2748), new Vector3f(173904, -60443, 2754), new Angle(0.00f, 1.00f)));// last guys spawn plane

            Rooms.Add(layout);


            //// rooms 12 and 13 layout ////
            List<Enemy> enemies2 = room_1.ReturnEnemiesInRoom(AllEnemies);
            List<Enemy> enemies3 = room_2.ReturnEnemiesInRoom(AllEnemies);
            enemies1 = room_12.ReturnEnemiesInRoom(AllEnemies);
            enemies = room_13.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies[0], enemies[1], enemies[2], enemies[3], enemies1[0], enemies1[1], enemies2[0], enemies3[0]);//3 pistol+3 uzi+2pistols
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(166489, -77468, 3748), new Angle(-0.28f, 0.96f)));//billboard on the left near 2 dudes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(169128, -79603, 4393), new Angle(-1.00f, 0.02f)));//near crane on top on the fan
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(166682, -78727, 3336), new Angle(0.35f, 0.94f)));//citylight
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(169284, -77099, 3417), new Angle(0.38f, 0.93f)));//billboard on the left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(166966, -80164, 3451), new Vector3f(166966, -79067, 3451), new Angle(-0.07f, 1.00f)));//billboard near citylight
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(168834, -79287, 3150), new Angle(0.50f, 0.87f)));//tube
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(161077, -80543, 3557), new Angle(-0.08f, 1.00f)));//cargo near bridge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(170340, -79438, 3585), new Angle(0.68f, 0.74f)));//on top of the rack
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(164953, -80456, 3481), new Vector3f(166682, -80488, 3472), new Angle(-0.29f, 0.96f)));//billboard near last guy
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(161924, -83446, 3070), new Vector3f(162501, -83088, 3070), new Angle(0.51f, 0.86f)).AsVerticalPlane());//tube in bridge room

            //default spawn planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(167618, -78529, 2798), new Vector3f(166810, -75917, 2798), new Angle(0.09f, 1.00f)).SetMaxEnemies(2));//2 dudes on the left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(171528, -78657, 2793), new Vector3f(169243, -77932, 2798), new Angle(0.58f, 0.81f)).SetMaxEnemies(2));//guy on the middle before fan
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(169395, -81891, 2810), new Vector3f(171394, -80640, 2802), new Angle(1.00f, 0.05f)).SetMaxEnemies(2));//2 guys behind fan
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(166685, -81841, 2798), new Vector3f(167942, -80457, 2798), new Angle(0.71f, 0.70f)));//second to last guy
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(164302, -81687, 2798), new Vector3f(165036, -80621, 2798), new Angle(-0.05f, 1.00f)));//last guy

            Rooms.Add(layout);


            //// Room 14 layout ////
            enemies1 = room_4.ReturnEnemiesInRoom(AllEnemies);
            enemies2 = room_5.ReturnEnemiesInRoom(AllEnemies);
            enemies = room_14.ReturnEnemiesInRoom(AllEnemies);
            if(Config.GetInstance().Gen_RngOrbs) {
                layout = new RoomLayout(enemies[0]); //orb
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(148695, -88911, 3261)));//behind billboard near middle platform
                //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(152815, -87759, 2230)));//under right platform
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(150835, -87631, 2750), new Vector3f(148114, -85596, 3600)));//middle platform
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(147351, -82958, 3417)));//billboard near exit
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(144450, -85503, 2921), new Vector3f(144450, -84265, 3523)));//wall near slowmo platform
                Rooms.Add(layout);
            }

            layout = new RoomLayout(enemies[1], enemies[2], enemies[3], enemies[4], enemies[5], enemies1[0], enemies2[0]); //2 pistols + 3 uzi+frogger+pistol
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(154986, -82205, 3398), new Angle(-0.91f, 0.40f)));//boxes on the left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153961, -84904, 3417), new Angle(0.66f, 0.75f)));//billboard on the right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153893, -88219, 3683), new Angle(0.75f, 0.66f)));//boxes behind right guy
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(150018, -88641, 3613), new Vector3f(149291, -87780, 3613), new Angle(0.36f, 0.93f)).AsVerticalPlane());//on top of the billboard near middle platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(150831, -85799, 4188), new Angle(1.00f, 0.04f)));//crane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(149435, -81215, 3923), new Angle(-0.70f, 0.71f)));//lamp on top of the door

            //default planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153858, -87925, 2798), new Vector3f(153128, -86642, 2801), new Angle(0.71f, 0.71f)));//right platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(150835, -87631, 2798), new Vector3f(148114, -85596, 2798), new Angle(0.30f, 0.95f)).SetMaxEnemies(2));//middle platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(148278, -81101, 2800), new Vector3f(149485, -82704, 2801), new Angle(0.03f, 1.00f)));//pistol near exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(150620, -81098, 2798), new Vector3f(150184, -82798, 2800), new Angle(-0.71f, 0.70f)));//uzi near exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(145340, -82203, 2798), new Vector3f(146515, -84270, 2798), new Angle(-0.03f, 1.00f)));//slowmo platform
            Rooms.Add(layout);


            //// Room 15+3 layout //// 
            ModifyCP(new DeepPointer(0x045A3C20, 0x98, 0x80, 0x128, 0xA8, 0xD0, 0x248, 0x1D0), new Vector3f(140756, -63564, 3948), MainWindow.game);
            enemies = room_15.ReturnEnemiesInRoom(AllEnemies);
            enemies1 = room_3.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies[0], enemies[1], enemies[2], enemies1[0], enemies1[1], enemies1[2]); // 2 pistols+ 4 uzi
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(139294, -69248, 5284), new Vector3f(139218, -70234, 5284), new Angle(-0.49f, 0.87f)).AsVerticalPlane());//crane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(141380, -64042, 4248), new Angle(-0.70f, 0.72f)));//third platform boxes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(140372, -60942, 4697), new Vector3f(141236, -61817, 4696), new Angle(-0.71f, 0.70f)).AsVerticalPlane());//third platform billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(138060, -64747, 4698), new Vector3f(138060, -64288, 4698), new Angle(-0.53f, 0.85f)));//second platform billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(141809, -66441, 5283), new Angle(-0.94f, 0.35f)));//billboard on the right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(138628, -65949, 5093), new Angle(-0.71f, 0.70f)));//tube spot
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(140801, -72750, 3944), new Angle(0.71f, 0.70f)));//under the entry spot
            //default planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(140279, -69332, 3995), new Vector3f(141367, -68616, 3968), new Angle(-0.72f, 0.69f)));//first guy
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(139698, -66304, 3948), new Vector3f(139110, -66729, 3948), new Angle(-0.72f, 0.70f)));//near second guy
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(138857, -66488, 3948), new Vector3f(138315, -64921, 3948), new Angle(-0.48f, 0.88f)));//second guy
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(141307, -63868, 3947), new Vector3f(140323, -63089, 3948), new Angle(-0.69f, 0.72f)));//third guy
            Rooms.Add(layout);


            //// Room 16 layout ////
            enemies = room_16.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies[0], enemies[1]); //froggers+pistol
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(86751, -43562, 5228), new Angle(0.00f, 1.00f)));//tube uptop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(86919, -43999, 4835), new Angle(0.07f, 1.00f)));//lamp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(87762, -42340, 4205), new Angle(-0.70f, 0.71f)));//side corridor
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(88571, -43878, 4998), new Angle(0.11f, 0.99f)));//uptop laser generator
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(91343, -43069, 4198), new Vector3f(91775, -43777, 4198), new Angle(0.00f, 1.00f)));//second to last crusher
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(95114, -43821, 4205), new Angle(0.68f, 0.73f)));//corner
            //default planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(89755, -43757, 4198), new Vector3f(85820, -43000, 4205), new Angle(0.00f, 1.00f)));//default area
            Rooms.Add(layout);


            //// Room 17+8 layout ////
            enemies = room_17.ReturnEnemiesInRoom(AllEnemies);
            enemies1 = room_8.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies[0], enemies[1], enemies[2], enemies[3], enemies1[0], enemies1[1]); //2uzi+2froggers+room8
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(83347, -50062, 4501), new Angle(-0.34f, 0.94f)));//boxes on the entry platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(81834, -50521, 4843), new Angle(-0.14f, 0.99f)));//white billboard on the left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(76350, -53602, 5481), new Vector3f(76350, -54813, 5481), new Angle(-0.04f, 1.00f)));//billboard on the far right platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(79057, -55309, 5428), new Angle(0.35f, 0.94f)));//near weird mechanism
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(79629, -52605, 4498), new Angle(-0.17f, 0.99f)));//boxes on left platform
            //default planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(83397, -55454, 4198), new Vector3f(82449, -54108, 4198), new Angle(0.68f, 0.74f)));//closer right platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(80811, -54361, 4199), new Vector3f(79918, -55854, 4198), new Angle(0.41f, 0.91f)).SetMaxEnemies(2));//middle right platform 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(77439, -53458, 4202), new Vector3f(76697, -54952, 4228), new Angle(0.00f, 1.00f)));//far right platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(79703, -50569, 4198), new Vector3f(81139, -52288, 4213), new Angle(0.00f, 1.00f)));//left platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(83054, -50595, 4198), new Vector3f(83671, -52264, 4198), new Angle(-0.08f, 1.00f)));//entry platform
            Rooms.Add(layout);


            //// Room 18+5 layout ////
            enemies = room_18.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies[0], enemies[1], enemies[2], enemies[3], enemies[4], enemies2[1], enemies2[2]); // 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61119, -74764, 4653), new Vector3f(62311, -74764, 4653), new Angle(0.46f, 0.89f)));//crane near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64718, -73300, 4653), new Vector3f(63436, -73300, 4653), new Angle(0.95f, 0.33f)));//main crane in the center
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65709, -74946, 4732), new Angle(0.90f, 0.44f)));//far left corner uptop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65100, -72946, 3855), new Vector3f(65100, -72398, 3855), new Angle(1.00f, 0.00f)));//below the crane in the center
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66452, -70657, 4731), new Angle(1.00f, 0.00f)));//uptop first guy
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63334, -73072, 3497), new Angle(0.70f, 0.71f)));//laser pillar
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61342, -73916, 4499), new Angle(0.70f, 0.72f)));//billboard near last guy

            //deafult planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65177, -70196, 3801), new Vector3f(66267, -69282, 3798), new Angle(1.00f, 0.00f)));//first guy near laser
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66244, -72056, 3100), new Vector3f(62494, -70242, 3098), new Angle(1.00f, 0.00f)).SetMaxEnemies(2));//seconf guy on main platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65559, -75832, 3098), new Vector3f(60985, -74310, 3098), new Angle(0.91f, 0.41f)).SetMaxEnemies(2));//main platform near exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62331, -74022, 3698), new Vector3f(61553, -72774, 3698), new Angle(0.00f, 1.00f)));//last guy platform

            Rooms.Add(layout);


            //// Room 19+6 layout ////
            enemies = room_19.ReturnEnemiesInRoom(AllEnemies);
            enemies1 = room_6.ReturnEnemiesInRoom(AllEnemies);

            if(Config.GetInstance().Gen_RngOrbs) {
                layout = new RoomLayout(enemies[0]); //orb
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(72728, -100985, 4608)));//colectible spot
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69685, -98975, 5239), new Vector3f(68705, -98975, 5239)));//
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67944, -101616, 4558)));//cv spot
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64775, -93641, 5027), new Vector3f(64775, -95084, 5427)));//billboard on the left
                Rooms.Add(layout);
            }

            layout = new RoomLayout(enemies[1], enemies[2], enemies[3], enemies[4], enemies1[0], enemies1[1]); // lastroom +room6
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71824, -94329, 5433), new Angle(0.82f, 0.58f)));//billboard next to the cp point
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66624, -98895, 4533), new Vector3f(66616, -97808, 4533), new Angle(0.00f, 1.00f)));//billboard on the right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65849, -95950, 4798), new Angle(0.29f, 0.96f)));//platform over 2 shielded enemies
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65911, -91934, 4498), new Vector3f(66945, -92838, 4506), new Angle(-0.40f, 0.92f)));//platform on the left of cp point
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(73171, -94823, 3859), new Angle(-0.95f, 0.30f)));//hidden tube spot on the right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66066, -101488, 4098), new Angle(0.43f, 0.90f)));//boxes near cv spot
            //deafult planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71214, -97432, 3798), new Vector3f(70300, -96350, 3798), new Angle(0.73f, 0.69f)).SetMaxEnemies(2));//closer right platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68260, -97342, 3808), new Vector3f(66431, -96136, 3798), new Angle(0.44f, 0.90f)).SetMaxEnemies(2));//closer left platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67295, -101437, 3798), new Vector3f(68310, -99170, 3798), new Angle(0.37f, 0.93f)).SetMaxEnemies(2));//far left platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71589, -100298, 3798), new Vector3f(70074, -99196, 3808), new Angle(0.69f, 0.72f)).SetMaxEnemies(2));//far left platform

            Rooms.Add(layout);
        }
    }
}
