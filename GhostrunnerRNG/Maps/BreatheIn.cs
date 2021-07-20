using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.MemoryUtils;
using GhostrunnerRNG.NonPlaceableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.Maps {
    class BreatheIn : MapCore, IModesMidCV {

        // Note: Enemies are not in the same order in this map, so we group them by their RoomRectangle

        // not needed for cp
        #region Rooms
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
        #endregion

        #region HC Room
        private Room HC_room_1 = new Room(new Vector3f(193602, -54335, 11578), new Vector3f(180515, -43233, 8588));// 2 weeb, 2 uzi, waver
        private Room HC_room_2 = new Room(new Vector3f(183152, -57138, 1900), new Vector3f(173987, -63205, 4505)); // uzi, shifter, waver ,weeb
        private Room HC_room_3 = new Room(new Vector3f(173686, -74851, 1668), new Vector3f(163944, -82504, 5197)); // 3 sniper, uzi
        private Room HC_room_4 = new Room(new Vector3f(155071, -81084, 1965), new Vector3f(143803, -90245, 5050)); // 3 shifter, 2 uzi, 2 pistol
        private Room HC_room_5 = new Room(new Vector3f(145271, -74182, 5292), new Vector3f(143784, -75451, 4255)); // lonely pistol, no cp
        private Room HC_room_6 = new Room(new Vector3f(142555, -72770, 3310), new Vector3f(128559, -52191, 7839)); // orb, drone, 2 wavers, 2 weebs
        private Room HC_room_7 = new Room(new Vector3f(133832, -44761, 6392), new Vector3f(125435, -40676, 4102)); // shielder ,waver
        private Room HC_room_8 = new Room(new Vector3f(121849, -44452, 2872), new Vector3f(106051, -37952, 6359)); // 2 pistol, waver
        private Room HC_room_9 = new Room(new Vector3f(100661, -39725, 3384), new Vector3f(85882, -51944, 6100)); // shielder, uzi, sniper
        private Room HC_room_10 = new Room(new Vector3f(85083, -55831, 3714), new Vector3f(76216, -50113, 6553)); // orb, 2 turret, 2 shielders, 2 pistols
        private Room HC_room_11 = new Room(new Vector3f(63012, -59427, 5150), new Vector3f(58665, -57461, 3476)); // shielder, 3 uzi
        private Room HC_room_12 = new Room(new Vector3f(59985, -76681, 2043), new Vector3f(67923, -66899, 6993)); // 5 orbs, waver, shielder, pistol, uzi
        private Room HC_room_13 = new Room(new Vector3f(72471, -92630, 2530), new Vector3f(66087, -101995, 5566)); // shifter, splitter, 2 uzi, waver
        private Room HC_Room14 = new Room(new Vector3f(54296, -64937, 7209), new Vector3f(48524, -56378, 4768)); // 2 drones, uzi
        #endregion

        public BreatheIn() : base(MapType.BreatheIn, BeforeCV: GameHook.xPos > 150000.0f) {
            ModifyCP(new DeepPointer(PtrDB.DP_BreatheIn_ElevatorCP), new Vector3f(197550, -49400, 9645), GameHook.game);
        }

        public void Gen_Easy_BeforeCV() {Gen_Normal_BeforeCV();}

        public void Gen_Easy_AfterCV() {HasRng = false;}

        public void Gen_Normal_BeforeCV() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game);
            Rooms = new List<RoomLayout>();
            RoomLayout layout;

            // Enemies without CP
            EnemiesWithoutCP.AddRange(room_1.ReturnEnemiesInRoom(AllEnemies));
            EnemiesWithoutCP.AddRange(room_2.ReturnEnemiesInRoom(AllEnemies));
            EnemiesWithoutCP.AddRange(room_3.ReturnEnemiesInRoom(AllEnemies));
            EnemiesWithoutCP.AddRange(room_4.ReturnEnemiesInRoom(AllEnemies));
            EnemiesWithoutCP.AddRange(room_5.ReturnEnemiesInRoom(AllEnemies));
            EnemiesWithoutCP.AddRange(room_6.ReturnEnemiesInRoom(AllEnemies));
            EnemiesWithoutCP.AddRange(room_7.ReturnEnemiesInRoom(AllEnemies));
            EnemiesWithoutCP.AddRange(room_8.ReturnEnemiesInRoom(AllEnemies));

            ////// room 11 layout ////
            layout = new RoomLayout(room_11.ReturnEnemiesInRoom(AllEnemies)); //4 uzi + pistol+frogger
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(183228, -55397, 3343), new Angle(-1.00f, 0.05f)));//box near slowmo
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(183041, -61969, 4053), new Angle(0.74f, 0.67f)).setDiff(1));//crane near collectible
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(176772, -61946, 4053), new Angle(0.34f, 0.94f)).setDiff(1));//crane near the fan
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(173433, -60851, 3648), new Angle(0.06f, 1.00f)));//boxes near button
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(180000, -60642, 3508), new Vector3f(179026, -60642, 3508), new Angle(0.69f, 0.72f)).setDiff(1));//billboard

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(182734, -60312, 2753), new Vector3f(182258, -56845, 2748), new Angle(0.73f, 0.69f)));//1 guy spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(180395, -60415, 2754), new Vector3f(182179, -59969, 2748), new Angle(0.56f, 0.83f)).SetMaxEnemies(2));//before bilboard 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(178348, -58916, 2748), new Vector3f(177916, -60438, 2748), new Angle(0.37f, 0.93f)));//2 guy spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(176822, -60415, 2754), new Vector3f(177780, -60026, 2748), new Angle(-0.01f, 1.00f)));//platform on the back of the 2 guy 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(175956, -59962, 2748), new Vector3f(173904, -60443, 2754), new Angle(0.00f, 1.00f)));// last guys spawn plane

            Rooms.Add(layout);


            //// rooms 12 and 13 layout ////
            var enemies = room_13.ReturnEnemiesInRoom(AllEnemies);
            enemies.AddRange(room_13.ReturnEnemiesInRoom(AllEnemies));
            layout = new RoomLayout(enemies);//3 pistol+3 uzi+2pistols
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(166489, -77468, 3748), new Angle(-0.28f, 0.96f)).setDiff(1));//billboard on the left near 2 dudes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(169128, -79603, 4393), new Angle(-1.00f, 0.02f)).setDiff(1));//near crane on top on the fan
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(166682, -78727, 3336), new Angle(0.35f, 0.94f)));//citylight
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(169284, -77099, 3417), new Angle(0.38f, 0.93f)).setDiff(1));//billboard on the left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(166966, -80164, 3451), new Vector3f(166966, -79067, 3451), new Angle(-0.07f, 1.00f)).setDiff(1));//billboard near citylight
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(168834, -79287, 3150), new Angle(0.50f, 0.87f)).setDiff(1));//tube
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(161077, -80543, 3557), new Angle(-0.08f, 1.00f)));//cargo near bridge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(170340, -79438, 3585), new Angle(0.68f, 0.74f)));//on top of the rack
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(164953, -80456, 3481), new Vector3f(166682, -80488, 3472), new Angle(-0.29f, 0.96f)).setDiff(1));//billboard near last guy
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(161924, -83446, 3070), new Vector3f(162501, -83088, 3070), new Angle(0.51f, 0.86f)).AsVerticalPlane().setDiff(1));//tube in bridge room

            //default spawn planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(167618, -78529, 2798), new Vector3f(166810, -75917, 2798), new Angle(0.09f, 1.00f)).SetMaxEnemies(2));//2 dudes on the left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(171528, -78657, 2793), new Vector3f(169243, -77932, 2798), new Angle(0.58f, 0.81f)).SetMaxEnemies(2));//guy on the middle before fan
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(169395, -81891, 2810), new Vector3f(171394, -80640, 2802), new Angle(1.00f, 0.05f)).SetMaxEnemies(2));//2 guys behind fan
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(166685, -81841, 2798), new Vector3f(167942, -80457, 2798), new Angle(0.71f, 0.70f)));//second to last guy
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(164302, -81687, 2798), new Vector3f(165036, -80621, 2798), new Angle(-0.05f, 1.00f)));//last guy

            Rooms.Add(layout);


            //// Room 14 layout ////
            enemies = room_14.ReturnEnemiesInRoom(AllEnemies);

            EnemyShieldOrb shieldOrb1 = new EnemyShieldOrb(enemies[0]);
            shieldOrb1.HideBeam_Range(0, 2);
            shieldOrb1.LinkObject(2);
            shieldOrb1.HideBeam_Range(1, 2);

            layout = new RoomLayout(shieldOrb1); //orb
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(148695, -88911, 3261)));//behind billboard near middle platform
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(152815, -87759, 2230)).setDiff(1).setRarity(0.2));//under right platform     - nightmare
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(150835, -87631, 2750), new Vector3f(148114, -85596, 3600)));//middle platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(147351, -82958, 3417)));//billboard near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(144450, -85503, 2921), new Vector3f(144450, -84265, 3523)));//wall near slowmo platform
            Rooms.Add(layout);
        

            layout = new RoomLayout(enemies.Skip(1).ToList()); //2 pistols + 3 uzi+frogger+pistol
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(154986, -82205, 3398), new Angle(-0.91f, 0.40f)));//boxes on the left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153961, -84904, 3417), new Angle(0.66f, 0.75f)).setDiff(1));//billboard on the right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153893, -88219, 3683), new Angle(0.75f, 0.66f)));//boxes behind right guy
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(149375, -87871, 3613), new Angle(0.38f, 0.92f)).setDiff(1)); //on top of the billboard near middle platform, left corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(150037, -88679, 3613), new Angle(0.62f, 0.78f)).setDiff(1)); //on top of the billboard near middle platform, right corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(150831, -85799, 4188), new Angle(1.00f, 0.04f)).setDiff(1));//crane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(149435, -81215, 3923), new Angle(-0.70f, 0.71f)).setDiff(1));//lamp on top of the door

            //default planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153858, -87925, 2798), new Vector3f(153128, -86642, 2801), new Angle(0.71f, 0.71f)));//right platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(150835, -87631, 2798), new Vector3f(148114, -85596, 2798), new Angle(0.30f, 0.95f)).SetMaxEnemies(2));//middle platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(148278, -81101, 2800), new Vector3f(149485, -82704, 2801), new Angle(0.03f, 1.00f)));//pistol near exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(150620, -81098, 2798), new Vector3f(150184, -82798, 2800), new Angle(-0.71f, 0.70f)));//uzi near exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(145340, -82203, 2798), new Vector3f(146515, -84270, 2798), new Angle(-0.03f, 1.00f)));//slowmo platform
            Rooms.Add(layout);


            //// Room 15 layout //// 
            ModifyCP(new DeepPointer(PtrDB.DP_BreatheIn_Room15_CP), new Vector3f(140756, -63564, 3948), new Angle(0.83f, 0.56f), GameHook.game);

            layout = new RoomLayout(room_15.ReturnEnemiesInRoom(AllEnemies)); // 2 pistols+ 4 uzi
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(139294, -69248, 5284), new Vector3f(139218, -70234, 5284), new Angle(-0.49f, 0.87f)).AsVerticalPlane().setDiff(1));//crane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(141380, -64042, 4248), new Angle(-0.70f, 0.72f)));//third platform boxes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(140372, -60942, 4697), new Vector3f(141236, -61817, 4696), new Angle(-0.71f, 0.70f)).AsVerticalPlane().setDiff(1));//third platform billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(138060, -64747, 4698), new Vector3f(138060, -64288, 4698), new Angle(-0.53f, 0.85f)).setDiff(1));//second platform billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(141809, -66441, 5283), new Angle(-0.94f, 0.35f)).setDiff(1));//billboard on the right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(138628, -65949, 5093), new Angle(-0.71f, 0.70f)).setDiff(1));//tube spot
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(140801, -72750, 3944), new Angle(0.71f, 0.70f)));//under the entry spot
            //default planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(140279, -69332, 3995), new Vector3f(141367, -68616, 3968), new Angle(-0.72f, 0.69f)));//first guy
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(139698, -66304, 3948), new Vector3f(139110, -66729, 3948), new Angle(-0.72f, 0.70f)));//near second guy
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(138857, -66488, 3948), new Vector3f(138315, -64921, 3948), new Angle(-0.48f, 0.88f)));//second guy
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(141307, -63868, 3947), new Vector3f(140323, -63089, 3948), new Angle(-0.69f, 0.72f)));//third guy
            Rooms.Add(layout);


            //// Room 16 layout ////
            layout = new RoomLayout(room_16.ReturnEnemiesInRoom(AllEnemies)); //froggers+pistol
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(86751, -43562, 5228), new Angle(0.00f, 1.00f)).setDiff(1));//tube uptop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(86919, -43999, 4835), new Angle(0.07f, 1.00f)));//lamp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(87762, -42340, 4205), new Angle(-0.70f, 0.71f)));//side corridor
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(88571, -43878, 4998), new Angle(0.11f, 0.99f)));//uptop laser generator
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(91343, -43069, 4198), new Vector3f(91775, -43777, 4198), new Angle(0.00f, 1.00f)));//second to last crusher
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(95114, -43821, 4205), new Angle(0.68f, 0.73f)));//corner
            //default planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(89755, -43757, 4198), new Vector3f(85820, -43000, 4205), new Angle(0.00f, 1.00f)));//default area
            Rooms.Add(layout);


            //// Room 17 layout ////
            layout = new RoomLayout(room_17.ReturnEnemiesInRoom(AllEnemies)); //2uzi+2froggers+room8
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(83347, -50062, 4501), new Angle(-0.34f, 0.94f)));//boxes on the entry platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(81834, -50521, 4843), new Angle(-0.14f, 0.99f)).setDiff(1));//white billboard on the left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(76350, -53602, 5481), new Vector3f(76350, -54813, 5481), new Angle(-0.04f, 1.00f)).setDiff(1));//billboard on the far right platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(79057, -55309, 5428), new Angle(0.35f, 0.94f)).setDiff(1));//near weird mechanism
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(79629, -52605, 4498), new Angle(-0.17f, 0.99f)));//boxes on left platform
            //default planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(83397, -55454, 4198), new Vector3f(82449, -54108, 4198), new Angle(0.68f, 0.74f)));//closer right platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(80811, -54361, 4199), new Vector3f(79918, -55854, 4198), new Angle(0.41f, 0.91f)).SetMaxEnemies(2));//middle right platform 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(77439, -53458, 4202), new Vector3f(76697, -54952, 4228), new Angle(0.00f, 1.00f)));//far right platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(79703, -50569, 4198), new Vector3f(81139, -52288, 4213), new Angle(0.00f, 1.00f)));//left platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(83054, -50595, 4198), new Vector3f(83671, -52264, 4198), new Angle(-0.08f, 1.00f)));//entry platform
            Rooms.Add(layout);


            //// Room 18+5 layout ////
            layout = new RoomLayout(room_18.ReturnEnemiesInRoom(AllEnemies)); // 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61119, -74764, 4653), new Vector3f(62311, -74764, 4653), new Angle(0.46f, 0.89f)).setDiff(1));//crane near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64718, -73300, 4653), new Vector3f(63436, -73300, 4653), new Angle(0.95f, 0.33f)));//main crane in the center
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65709, -74946, 4732), new Angle(0.90f, 0.44f)));//far left corner uptop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65100, -72946, 3855), new Vector3f(65100, -72398, 3855), new Angle(1.00f, 0.00f)));//below the crane in the center
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66452, -70657, 4731), new Angle(1.00f, 0.00f)));//uptop first guy
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63334, -73072, 3497), new Angle(0.70f, 0.71f)));//laser pillar
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61342, -73916, 4499), new Angle(0.70f, 0.72f)).setDiff(1));//billboard near last guy
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65132, -67435, 5143), new Angle(-0.80f, 0.60f)).setDiff(1)); // extruded wall piece, on right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67342, -70789, 5600), new Vector3f(67365, -73867, 5600), new Angle(1.00f, 0.05f)).setDiff(1)); // double airvent wall, behind lasers

            //deafult planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65177, -70196, 3801), new Vector3f(66267, -69282, 3798), new Angle(1.00f, 0.00f)));//first guy near laser
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66244, -72056, 3100), new Vector3f(62485, -70638, 3098), new Angle(1.00f, 0.00f)).SetMaxEnemies(2));//second guy on main platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65559, -75832, 3098), new Vector3f(60985, -74310, 3098), new Angle(0.91f, 0.41f)).SetMaxEnemies(2));//main platform near exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62331, -74022, 3698), new Vector3f(61553, -72774, 3698), new Angle(0.00f, 1.00f)));//last guy platform

            Rooms.Add(layout);

            //// Room 19+6 layout ////
            enemies = room_19.ReturnEnemiesInRoom(AllEnemies);

            shieldOrb1 = new EnemyShieldOrb(enemies[0]);
            layout = new RoomLayout(shieldOrb1); //orb
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(72728, -100985, 4608)));   // colectible spot
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69685, -98975, 5239), new Vector3f(68705, -98975, 5239)));//
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67944, -101616, 4558)));   // cv spot
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64775, -93641, 5027), new Vector3f(64775, -95084, 5427)));//billboard on the left
            Rooms.Add(layout);

            layout = new RoomLayout(enemies.Skip(1).ToList()); // lastroom +room6
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71824, -94329, 5433), new Angle(0.82f, 0.58f)).setDiff(1));//billboard next to the cp point
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66624, -98895, 4533), new Vector3f(66616, -97808, 4533), new Angle(0.00f, 1.00f)).setDiff(1));//billboard on the right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65849, -95950, 4798), new Angle(0.29f, 0.96f)));//platform over 2 shielded enemies
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65911, -91934, 4498), new Vector3f(66945, -92838, 4506), new Angle(-0.40f, 0.92f)));//platform on the left of cp point
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(73171, -94823, 3859), new Angle(-0.95f, 0.30f)).setDiff(1));//hidden tube spot on the right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66066, -101488, 4098), new Angle(0.43f, 0.90f)));//boxes near cv spot
            //deafult planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71214, -97432, 3798), new Vector3f(70300, -96350, 3798), new Angle(0.73f, 0.69f)).SetMaxEnemies(2));//closer right platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68260, -97342, 3808), new Vector3f(66431, -96136, 3798), new Angle(0.44f, 0.90f)).SetMaxEnemies(2));//closer left platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67295, -101437, 3798), new Vector3f(68310, -99170, 3798), new Angle(0.37f, 0.93f)).SetMaxEnemies(2));//far left platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71589, -100298, 3798), new Vector3f(70074, -99196, 3808), new Angle(0.69f, 0.72f)).SetMaxEnemies(2));//far left platform
            Rooms.Add(layout);

            //// EXTRA ////
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(185741, -47495, 10232), new Angle(0.04f, 1.00f))); // after first fan
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(177562, -67381, 2286), new Angle(0.67f, 0.74f))); // after first button fan
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(170602, -73548, 2800), new Angle(0.06f, 1.00f))); // before room 12
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(156057, -83425, 2802), new Angle(0.57f, 0.82f))); // before room 14
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(141457, -74909, 4752), new Angle(0.00f, 1.00f))); // before room 15
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(136536, -54627, 3955), new Angle(-0.63f, 0.78f))); // "purple" door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(135528, -45165, 4601), new Angle(-0.50f, 0.87f))); // pivoting platform room ,start
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(126515, -43144, 4601), new Angle(-0.01f, 1.00f))); // pivoting platform room, before fan
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(131125, -42819, 4601), new Angle(-0.17f, 0.99f))); // pivoting platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(115476, -39912, 3798), new Angle(-0.62f, 0.78f))); // single frogger room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65856, -58652, 5047), new Angle(0.00f, 1.00f))); // on top of shredder
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(57173, -66129, 4498), new Angle(0.75f, 0.66f))); // before room 18
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69932, -84002, 4592), new Angle(1.00f, 0.02f))); // before final room
            Rooms.Add(layout);

            /////////////// NonPlaceableObjects ///////////////
            #region Slowmo
            //// Sub 1
            // 1
            NonPlaceableObject uplink = new UplinkSlowmo(0x8, 0x188, 0xA0);
            uplink.AddSpawnInfo(new UplinkSlowmoSpawnInfo { TotalTime = Config.GetInstance().r.Next(3, 7) });
            worldObjects.Add(uplink);

            // 2
            uplink = new UplinkSlowmo(0x8, 0x180, 0xA0);
            uplink.AddSpawnInfo(new UplinkSlowmoSpawnInfo { TotalTime = Config.GetInstance().r.Next(3, 7) });
            worldObjects.Add(uplink);
            // 3
            uplink = new UplinkSlowmo(0x8, 0x178, 0xA0);  // same as sub 2, first slowmo
            uplink.AddSpawnInfo(new UplinkSlowmoSpawnInfo { TotalTime = Config.GetInstance().r.Next(6, 12) });
            worldObjects.Add(uplink);

            //// Sub 2
            // 4 - skipped, same as previous one (3)

            // 5
            uplink = new UplinkSlowmo(0x58, 0x100, 0xA0);  // same as 7
            uplink.AddSpawnInfo(new UplinkSlowmoSpawnInfo { TotalTime = Config.GetInstance().r.Next(6, 12) });
            uplink.AddSpawnInfo(new UplinkSlowmoSpawnInfo { TotalTime = 1 }); // fake slowmo
            worldObjects.Add(uplink);

            //// Sub 3
            // 6
            uplink = new UplinkSlowmo(0x60, 0x140, 0xA0);
            uplink.AddSpawnInfo(new UplinkSlowmoSpawnInfo { TotalTime = Config.GetInstance().r.Next(10, 16) });
            // no fake slowmo here, same values used in sub 8, needed to pass fans
            worldObjects.Add(uplink);

            //// Sub 4
            // 7 - skipped, same as (5)

            //// Sub 7
            // 8
            uplink = new UplinkSlowmo(0x80, 0xD8, 0xA0);
            uplink.AddSpawnInfo(new UplinkSlowmoSpawnInfo { TotalTime = Config.GetInstance().r.Next(6, 12) });
            uplink.AddSpawnInfo(new UplinkSlowmoSpawnInfo { TotalTime = 1 }); // fake slowmo
            worldObjects.Add(uplink);

            //// Sub 8
            // 9 - skipped, same as (6)

            //// Sub 11
            // 10
            uplink = new UplinkSlowmo(0x18, 0xC0, 0xA0);
            uplink.AddSpawnInfo(new UplinkSlowmoSpawnInfo { TotalTime = Config.GetInstance().r.Next(10, 20) });
            worldObjects.Add(uplink);

            // 11
            uplink = new UplinkSlowmo(0x18, 0xC0, 0xA0);
            uplink.AddSpawnInfo(new UplinkSlowmoSpawnInfo { TotalTime = Config.GetInstance().r.Next(10, 25) });
            worldObjects.Add(uplink);

            //// Sub 14
            // 12 - skipped, same as (3) and (4)
            //// Sub 17
            // 13 - skipped, same as (11)
            #endregion

        }

        public void Gen_Normal_AfterCV() { HasRng = false; }

        public void Gen_Hardcore() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game);
            List<Enemy> Shifters = new List<Enemy>();
            List<Enemy> Snipers = new List<Enemy>();
            Rooms = new List<RoomLayout>();
            RoomLayout layout;
 
            // Room 6, corner CP modified to avoid spawn kill
            ModifyCP(new DeepPointer(PtrDB.DP_BreatheIn_HC_Room6_CP), new Vector3f(137166, -61553, 3948), new Angle(-0.95f, 0.31f), GameHook.game);

            //// Room 1 ////
            var enemies = HC_room_1.ReturnEnemiesInRoom(AllEnemies);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies[4].SetEnemyType(Enemy.EnemyTypes.Weeb);
            RemoveParentObjects(ref enemies);
            EnemiesWithoutCP.AddRange(enemies);


            //// Room 2 ////
            enemies = HC_room_2.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShifter(enemies[0]);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Weeb);
            // remove shifter to its own list
            enemies[0].DisableAttachedCP(GameHook.game);
            Shifters.Add(enemies[0]);
            enemies.RemoveAt(0);
            RandomPickEnemiesWithoutCP(ref enemies);

            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(182342, -57186, 2748), new Vector3f(182715, -60229, 2753), new Angle(0.71f, 0.70f)).SetMaxEnemies(2).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(182224, -60017, 2748), new Vector3f(180543, -60405, 2754), new Angle(0.15f, 0.99f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(178269, -60444, 2748), new Vector3f(178025, -59024, 2754), new Angle(-0.01f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(177635, -60078, 2748), new Vector3f(176671, -60392, 2754), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(175849, -60336, 2748), new Vector3f(174750, -60020, 2753), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(174624, -61116, 2748), new Vector3f(173910, -60063, 2754), new Angle(-0.01f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(183216, -63600, 2748), new Vector3f(182349, -63256, 2748), new Angle(0.72f, 0.69f)).Mask(SpawnPlane.Mask_Highground)); // collectible platform
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(180283, -57180, 2238), new Angle(-0.60f, 0.80f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // pipes, left below entrance
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(179971, -60642, 3508), new Vector3f(178968, -60638, 3508), new Angle(0.39f, 0.92f)).AsVerticalPlane().Mask(SpawnPlane.Mask_HighgroundLimited)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(176898, -58584, 3833), new Angle(-0.77f, 0.64f)).Mask(SpawnPlane.Mask_Highground)); // left wall, extruded piece (near button)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(173454, -60884, 3648), new Angle(0.03f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // crates
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(173902, -59718, 3129), new Angle(-0.06f, 1.00f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // scaffolding
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(177091, -61963, 3982), new Vector3f(178467, -61983, 3982), new Angle(0.74f, 0.67f)).Mask(SpawnPlane.Mask_Highground).AsVerticalPlane()); // crane
            // addtional(drone)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(175157, -61433, 3325), new Angle(0.14f, 0.99f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(179463, -59805, 3132), new Angle(0.35f, 0.94f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);



            //// Room 3 ////
            enemies = HC_room_3.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemySniper(enemies[0]);
            enemies[1] = new EnemySniper(enemies[1]);
            enemies[2] = new EnemySniper(enemies[2]);
            // snipers  - disable cp and move snipers to their own list
            enemies[0].DisableAttachedCP(GameHook.game);
            enemies[1].DisableAttachedCP(GameHook.game);
            enemies[2].DisableAttachedCP(GameHook.game);
            Snipers.AddRange(enemies.Take(3).ToList());
            enemies.RemoveRange(0, 3);

            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(172447, -79687, 2800), new Vector3f(170806, -79033, 2801), new Angle(0.69f, 0.72f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(167450, -78614, 2798), new Vector3f(166779, -76036, 2798), new Angle(-0.01f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(166773, -81693, 2798), new Vector3f(167731, -80674, 2798), new Angle(0.63f, 0.78f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(172255, -81705, 2802), new Vector3f(169461, -80991, 2802), new Angle(0.91f, 0.41f)).SetMaxEnemies(2).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(164483, -81639, 2798), new Vector3f(164941, -80648, 2798), new Angle(-0.03f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(166746, -75645, 3093), new Angle(-0.06f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // crate
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(169313, -76699, 3417), new Angle(0.35f, 0.94f)).Mask(SpawnPlane.Mask_Highground)); // billboard edge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(172974, -80615, 3420), new Vector3f(172819, -81705, 3420), new Angle(1.00f, 0.03f)).Mask(SpawnPlane.Mask_Highground)); // net cage
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(172311, -80462, 3185), new Angle(0.93f, 0.36f)).Mask(SpawnPlane.Mask_Highground)); // scaffolding near fan
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(168884, -79208, 3137), new Angle(0.93f, 0.36f)).Mask(SpawnPlane.Mask_Highground)); // pipe
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(167638, -82512, 3288), new Vector3f(166854, -82516, 3288), new Angle(0.65f, 0.76f)).Mask(SpawnPlane.Mask_Highground).AsVerticalPlane()); // horizontal pipe/beam
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(169953, -82446, 3403), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Highground)); // crates

            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(167484, -79898, 3840), new Angle(0.45f, 0.89f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);



            //// Room 4 ////
            enemies = HC_room_4.ReturnEnemiesInRoom(AllEnemies);
            enemies[3] = new EnemyShifter(enemies[3]);
            enemies[4] = new EnemyShifter(enemies[4]);
            enemies[5] = new EnemyShifter(enemies[5]);            
            // shifters - disable cp and move shifter to their own list
            enemies[3].DisableAttachedCP(GameHook.game);
            enemies[4].DisableAttachedCP(GameHook.game);
            enemies[5].DisableAttachedCP(GameHook.game);
            Shifters.AddRange(enemies.Skip(3).Take(3).ToList());
            enemies.RemoveRange(3, 3);

            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(150519, -82752, 2800), new Vector3f(150081, -81999, 2801), new Angle(0.01f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(148304, -82781, 2800), new Vector3f(148823, -81793, 2798), new Angle(-0.02f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(145271, -82321, 2798), new Vector3f(145632, -84274, 2798), new Angle(-0.03f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(148116, -85672, 2798), new Vector3f(150763, -87837, 2798), new Angle(0.01f, 1.00f)).SetMaxEnemies(2).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(152310, -86647, 2800), new Vector3f(153944, -87085, 2798), new Angle(0.72f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153120, -88319, 2798), new Vector3f(152405, -87749, 2798), new Angle(0.68f, 0.73f)).Mask(SpawnPlane.Mask_Flatground));
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(154945, -82203, 3398), new Angle(-0.87f, 0.49f)).Mask(SpawnPlane.Mask_Highground)); // crate
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(149344, -87822, 3613), new Angle(0.29f, 0.96f)).Mask(SpawnPlane.Mask_Highground)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(154004, -88024, 3683), new Angle(0.82f, 0.57f)).Mask(SpawnPlane.Mask_Highground)); // box billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(150902, -85811, 4188), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Highground)); // crane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(148722, -88433, 3758), new Angle(0.58f, 0.82f)).Mask(SpawnPlane.Mask_Highground)); // double-pipe box
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(147906, -82817, 3417), new Angle(0.02f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // billboard near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(149829, -81807, 3098), new Angle(-0.31f, 0.95f)).Mask(SpawnPlane.Mask_Highground)); // red crate
            // addtional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(147834, -86964, 3453), new Angle(0.15f, 0.99f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(151106, -87979, 3788), new Angle(0.21f, 0.98f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);


            //// Room 5 ////
            enemies = HC_room_5.ReturnEnemiesInRoom(AllEnemies); // lonely pistol
            EnemiesWithoutCP.AddRange(enemies);


            //// Room 6 ////
            enemies = HC_room_6.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[1] = new EnemyDrone(enemies[1]);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[4].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies[5].SetEnemyType(Enemy.EnemyTypes.Weeb);
            RandomPickEnemiesWithoutCP(ref enemies, enemyIndex:1);
            // orb
            layout = new RoomLayout(enemies[0]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(141652, -67348, 4574), new Vector3f(141708, -65555, 5116)).AsVerticalPlane()); // left billboard (default)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(138403, -64230, 3912), new Vector3f(138310, -65419, 4446))); // right billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(139286, -69244, 5284))); // crane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(141727, -68848, 4912))); // left off laser-wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(138598, -61753, 4405))); // floating
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(141048, -61909, 3976), new Vector3f(140261, -61108, 4412)).AsVerticalPlane()); // last billboard
            layout.DoNotReuse();
            Rooms.Add(layout);
            // enemies
            layout = new RoomLayout(enemies.Skip(1).ToList());
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(140320, -63955, 3948), new Vector3f(140853, -63316, 3948), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(138326, -65209, 3928), new Vector3f(138808, -66199, 3948), new Angle(-0.53f, 0.85f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(135357, -60941, 3955), new Vector3f(134565, -59272, 3948), new Angle(-0.70f, 0.72f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(137309, -61056, 3948), new Vector3f(138615, -60681, 3948), new Angle(-0.70f, 0.72f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(135752, -56673, 3952), new Vector3f(134291, -57245, 3948), new Angle(-0.71f, 0.71f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(134294, -55575, 3955), new Vector3f(135725, -54204, 3948), new Angle(-0.71f, 0.71f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(136817, -56218, 3948), new Angle(-0.70f, 0.72f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(136578, -54340, 3957), new Angle(-0.86f, 0.50f)).Mask(SpawnPlane.Mask_Flatground));
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(138087, -64246, 4697), new Angle(-0.51f, 0.86f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // right billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(140331, -60830, 4698), new Angle(-0.72f, 0.69f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // angled billboard
            // additional (drone)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(135421, -58822, 4672), new Angle(-0.36f, 0.93f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);


            //// Room 7 ////
            enemies = HC_room_7.ReturnEnemiesInRoom(AllEnemies);
            enemies[1].SetEnemyType(Enemy.EnemyTypes.Waver);
            RemoveParentObjects(ref enemies);
            EnemiesWithoutCP.AddRange(enemies);


            //// Room 8 ////
            enemies = HC_room_8.ReturnEnemiesInRoom(AllEnemies);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Waver);
            RemoveParentObjects(ref enemies);
            EnemiesWithoutCP.AddRange(enemies);


            //// Room 9 ////
            enemies = HC_room_9.ReturnEnemiesInRoom(AllEnemies);
            // remove sniper, and move to sniprs list
            enemies[1] = new EnemySniper(enemies[1]);
            enemies[1].DisableAttachedCP(GameHook.game);
            Snipers.Add(enemies[1]);
            enemies.RemoveAt(1);
            RemoveParentObjects(ref enemies);
            EnemiesWithoutCP.AddRange(enemies);


            //// Room 10 ////
            enemies = HC_room_10.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyTurret(enemies[0]);
            enemies[1] = new EnemyTurret(enemies[1]);
            enemies[2] = new EnemyShieldOrb(enemies[2]);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, enemyIndex: 0); // take both turrets by force
            RandomPickEnemiesWithoutCP(ref enemies, force: true, enemyIndex: 0);
            // orbs
            layout = new RoomLayout(enemies[0]);// orb  will be 0 after both turrets are gone
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(77338, -53366, 4498))); // light
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(79134, -55172, 4319), new Vector3f(77556, -55192, 4720)).AsVerticalPlane()); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(80673, -52830, 4261), new Vector3f(80739, -54345, 4652)).AsVerticalPlane()); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(82620, -55727, 4317), new Vector3f(81358, -55678, 4636)).AsVerticalPlane()); // billboard
            Rooms.Add(layout);

            // enemies
            layout = new RoomLayout(enemies.Skip(1).ToList());
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(81172, -52040, 4218), new Vector3f(79580, -50704, 4198), new Angle(0.00f, 1.00f)).SetMaxEnemies(2).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(80945, -54727, 4198), new Vector3f(79438, -55203, 4198), new Angle(-0.02f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(83681, -54219, 4198), new Vector3f(82579, -54900, 4198), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(77316, -54877, 4202), new Vector3f(76667, -53503, 4202), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(79508, -55349, 4893), new Angle(0.47f, 0.89f)).Mask(SpawnPlane.Mask_Highground)); // billboard edge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(80849, -54440, 4891), new Angle(-0.93f, 0.36f)).Mask(SpawnPlane.Mask_Highground)); // billboard edge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(83723, -55035, 4498), new Angle(0.78f, 0.62f)).Mask(SpawnPlane.Mask_Highground)); // crate
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(80751, -50240, 4498), new Angle(-0.87f, 0.49f)).Mask(SpawnPlane.Mask_Highground)); // floor lamp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(79540, -50204, 4348), new Angle(-0.36f, 0.93f)).Mask(SpawnPlane.Mask_Highground)); // blue crate
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(79611, -52646, 4498), new Angle(0.19f, 0.98f)).Mask(SpawnPlane.Mask_Highground)); // red crates
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(78035, -55012, 4424), new Vector3f(78749, -53274, 4802), new Angle(0.26f, 0.96f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);


            //// Room 11 ////
            enemies = HC_room_11.ReturnEnemiesInRoom(AllEnemies);
            RemoveParentObjects(ref enemies);
            EnemiesWithoutCP.AddRange(enemies);


            //// Room 12 ////
            enemies = HC_room_12.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[1] = new EnemyShieldOrb(enemies[1]);
            enemies[2] = new EnemyShieldOrb(enemies[2]);
            enemies[3] = new EnemyShieldOrb(enemies[3]);
            enemies[4] = new EnemyShieldOrb(enemies[4]);
            enemies[8].SetEnemyType(Enemy.EnemyTypes.Waver);

            // orbs
            layout = new RoomLayout(enemies.Take(5).ToList());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67114, -71038, 5371), new Vector3f(67097, -73587, 4709))); // double fan wall/billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65565, -73290, 4696))); // crane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62277, -72696, 4008), new Vector3f(61547, -72772, 5048)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64941, -68827, 3170), new Vector3f(64894, -70143, 3598)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64810, -72401, 3568), new Vector3f(64796, -74104, 3179)));

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65477, -74153, 4511)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67252, -73402, 3919), new Vector3f(67277, -71066, 3160)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63581, -73078, 3162)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61934, -74225, 5538)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67150, -75085, 5093)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65850, -68934, 4318)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62425, -71939, 4481), new Vector3f(61532, -71943, 5153)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61765, -74406, 3270)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64059, -76228, 3681)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65781, -69788, 4739))); // air vent
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64131, -69447, 3410)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62084, -72198, 3737)).setRarity(0.3)); // INSIDE metal net structure, between pipes
            Rooms.Add(layout);

            // enemies
            enemies.RemoveRange(0, 5); // get rid of orbs
            RandomPickEnemiesWithoutCP(ref enemies, force: true, enemyIndex: 2); // UZI
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63108, -68457, 3598), new Vector3f(64752, -67860, 3598), new Angle(-0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66587, -69428, 4697), new Vector3f(67122, -70462, 4699), new Angle(-0.97f, 0.26f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67271, -74119, 4696), new Vector3f(66632, -74876, 4697), new Angle(0.95f, 0.30f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62204, -73902, 3698), new Vector3f(61646, -72954, 3698), new Angle(-0.01f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62477, -74608, 3098), new Vector3f(64948, -75664, 3100), new Angle(0.76f, 0.65f)).Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66153, -73877, 3100), new Vector3f(65740, -72207, 3099), new Angle(0.86f, 0.51f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66008, -72010, 3099), new Vector3f(63078, -70471, 3098), new Angle(-1.00f, 0.04f)).Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2));
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65167, -70238, 3801), new Angle(-0.98f, 0.21f)).Mask(SpawnPlane.Mask_Highground)); // platform ledge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64342, -67553, 3979), new Angle(-0.75f, 0.66f)).Mask(SpawnPlane.Mask_Highground)); // scaffolding right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66458, -70646, 4727), new Angle(-0.98f, 0.17f)).Mask(SpawnPlane.Mask_Highground)); // platform ledge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65804, -75021, 4701), new Angle(0.92f, 0.38f)).Mask(SpawnPlane.Mask_Highground)); // platform ledge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67335, -73859, 5600), new Vector3f(67322, -70740, 5600), new Angle(1.00f, 0.00f)).Mask(SpawnPlane.Mask_Highground)); // double-fan wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62318, -73094, 5672), new Angle(0.16f, 0.99f)).Mask(SpawnPlane.Mask_Highground)); // high platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63744, -73273, 4653), new Angle(0.85f, 0.53f)).Mask(SpawnPlane.Mask_Highground)); // crane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61376, -73917, 4499), new Angle(0.04f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // billboard wall, left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63297, -73104, 3498), new Angle(0.82f, 0.57f)).Mask(SpawnPlane.Mask_Highground)); // laser pillar
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65091, -72464, 3864), new Angle(0.93f, 0.36f)).Mask(SpawnPlane.Mask_Highground)); // net billboard
            // addtional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65003, -75511, 4037), new Angle(0.86f, 0.51f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66053, -71012, 3661), new Vector3f(65241, -71705, 4240), new Angle(1.00f, 0.01f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62998, -74137, 3864), new Angle(0.74f, 0.67f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);



            //// Room 13 ////
            enemies = HC_room_13.ReturnEnemiesInRoom(AllEnemies);
            enemies[0].SetEnemyType(Enemy.EnemyTypes.Splitter);
            enemies[1] = new EnemyShifter(enemies[1]);
            enemies[4].SetEnemyType(Enemy.EnemyTypes.Waver);
            RandomPickEnemiesWithoutCP(ref enemies, enemyIndex: 4); // waver
            // move shifter
            enemies[1].DisableAttachedCP(GameHook.game);
            Shifters.Add(enemies[1]);
            enemies.RemoveAt(1);

            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68151, -96340, 3805), new Vector3f(66625, -97334, 3808)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71241, -97252, 3802), new Vector3f(70308, -96633, 3798)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(70297, -100348, 3805), new Vector3f(71417, -99349, 3808)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68194, -99294, 3798), new Vector3f(67292, -100355, 3798)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            // high
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66996, -100265, 4098), new Angle(0.43f, 0.90f)).Mask(SpawnPlane.Mask_Highground)); // crate
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65812, -96558, 4798), new Angle(0.34f, 0.94f)).Mask(SpawnPlane.Mask_Highground)); // platform ledge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(70221, -96160, 4103), new Angle(0.54f, 0.84f)).Mask(SpawnPlane.Mask_Highground)); // red crate
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(72830, -100713, 4608), new Angle(0.95f, 0.30f)).Mask(SpawnPlane.Mask_Highground)); // collectible spot
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66588, -98812, 4533), new Vector3f(66608, -97790, 4533), new Angle(0.33f, 0.95f)).Mask(SpawnPlane.Mask_Highground)); // left billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65847, -96948, 5698), new Angle(0.35f, 0.94f)).Mask(SpawnPlane.Mask_Highground)); // ledge of huge ac unit
            // additional (drone)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68009, -98755, 3937), new Vector3f(67121, -97940, 4416), new Angle(0.72f, 0.69f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(70187, -97737, 3856), new Vector3f(71420, -98678, 4400), new Angle(0.72f, 0.69f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71360, -100445, 4430), new Vector3f(67806, -99579, 4430), new Angle(0.72f, 0.69f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);


            //// Room 14 ////
            // useless room, move all to EnemiesWithoutCP
            enemies = HC_Room14.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyDrone(enemies[0]);
            enemies[1] = new EnemyDrone(enemies[1]);
            RandomPickEnemiesWithoutCP(ref enemies, force: true);
            RandomPickEnemiesWithoutCP(ref enemies, force: true);
            RandomPickEnemiesWithoutCP(ref enemies, force: true);



            ///// SNIPERS
            layout = new RoomLayout(Snipers);
            // room 3 - top, aiming to middle
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(169174, -79376, 4398), new Angle(0.95f, 0.30f))
                .SetSpawnInfo(new SniperSpawnInfo().SinglePatrol(new Vector3f(167191, -78244, 2800))));

            // room 3 - metal net frame (blink through fan or from above)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(173062, -80693, 3420), new Vector3f(172815, -81538, 3420), new Angle(1.00f, 0.01f))
                .SetSpawnInfo(new SniperSpawnInfo().SinglePatrol(new Vector3f(166864, -81152, 2798))));

            // room 3 - exit beam
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(164017, -82022, 3938), new Vector3f(164010, -80926, 3938), new Angle(0.03f, 1.00f)).AsVerticalPlane()
                .SetSpawnInfo(new SniperSpawnInfo().SinglePatrol(new Vector3f(167610, -80744, 2798))));

            // room 3 - scaffolding aiming to shuriken spawn
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(165796, -78046, 3190), new Angle(-0.05f, 1.00f))
                .SetSpawnInfo(new SniperSpawnInfo().SinglePatrol(new Vector3f(169462, -78293, 2801))));

            // room 4 - left corner, hiding behind crates
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(155026, -81898, 3098), new Angle(-1.00f, 0.03f))
                 .SetSpawnInfo(new SniperSpawnInfo().SinglePatrol(new Vector3f(150503, -82213, 2798))));

            // room 4 - near exit door, hiding in corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(150484, -81112, 2798), new Angle(-0.72f, 0.70f))
                 .SetSpawnInfo(new SniperSpawnInfo().SinglePatrol(new Vector3f(149745, -86608, 2798))));

            // after room 4, vertical fans room with lasers, on top of roof/platform (collectible)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(149429, -78810, 4248), new Angle(0.85f, 0.53f))
                .SetSpawnInfo(new SniperSpawnInfo().SinglePatrol(new Vector3f(149366, -78710, 4059))));

            // room 6
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(134905, -62510, 3948), new Angle(0.28f, 0.96f))
                .SetSpawnInfo(new SniperSpawnInfo().SinglePatrol(new Vector3f(135923, -61825, 4048))));

            // room 10, near exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(80648, -56235, 4199), new Angle(0.73f, 0.68f))
                .SetSpawnInfo(new SniperSpawnInfo().SinglePatrol(new Vector3f(80597, -50309, 4198))));

            // spinning platform/fan room, right pipe
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(104399, -42112, 4470), new Angle(0.98f, 0.21f))
                .SetSpawnInfo(new SniperSpawnInfo().SinglePatrol(new Vector3f(104460, -41924, 4256))));

            // before grinder, right of CP
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(70038, -59302, 4198), new Angle(0.99f, 0.11f))
                 .SetSpawnInfo(new SniperSpawnInfo().SinglePatrol(new Vector3f(69968, -59288, 4043))));

            // after grinder, hiding behind left wall/corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61344, -57913, 4498), new Angle(-0.99f, 0.11f))
                 .SetSpawnInfo(new SniperSpawnInfo().SinglePatrol(new Vector3f(61227, -57935, 4343))));

            // after grinder room, around the corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(56797, -57717, 4501), new Angle(-0.69f, 0.72f))
                 .SetSpawnInfo(new SniperSpawnInfo().SinglePatrol(new Vector3f(56803, -57859, 4345))));

            layout.DoNotReuse();
            layout.Mask(SpawnPlane.Mask_Sniper);
            Rooms.Add(layout);


            //// SHIFTERS
            layout = new RoomLayout(Shifters);

            // first room
            var info = new ShifterSpawnInfo();
            info.shiftPoints = new List<Tuple<Vector3f, Angle>>() {
                    new Tuple<Vector3f, Angle>(new Vector3f(182680, -60400, 2868), new Angle(0.69f, 0.73f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(173503, -61381, 3048), new Angle(0.04f, 1.00f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(174694, -61741, 2867), new Angle(0.22f, 0.98f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(180083, -60641, 3508), new Angle(0.47f, 0.88f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(181336, -57091, 3739), new Angle(-0.71f, 0.70f))
             };
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(183243, -56600, 2748), new Angle(0.92f, 0.40f)).SetSpawnInfo(info));

            // room 3
            info = new ShifterSpawnInfo();
            info.shiftPoints = new List<Tuple<Vector3f, Angle>>() {
                    new Tuple<Vector3f, Angle>(new Vector3f(166893, -76139, 2798), new Angle(0.04f, 1.00f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(166459, -77442, 3748), new Angle(-0.19f, 0.98f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(168864, -79833, 3148), new Angle(0.96f, 0.28f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(166747, -81759, 2798), new Angle(0.56f, 0.83f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(171852, -81589, 2802), new Angle(0.99f, 0.12f))
             };
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(169396, -76223, 2798), new Angle(0.23f, 0.97f)).SetSpawnInfo(info));

            // room 4
            info = new ShifterSpawnInfo();
            info.shiftPoints = new List<Tuple<Vector3f, Angle>>() {
                    new Tuple<Vector3f, Angle>(new Vector3f(152777, -88466, 3098), new Angle(0.51f, 0.86f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(148080, -87801, 2918), new Angle(0.37f, 0.93f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(147963, -82774, 3417), new Angle(-0.10f, 1.00f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(154006, -87894, 3683), new Angle(0.80f, 0.60f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(150444, -82766, 2800), new Angle(-0.15f, 0.99f))
             };
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(152854, -83814, 2798), new Angle(0.14f, 0.99f)).SetSpawnInfo(info));

            // room 10
            info = new ShifterSpawnInfo();
            info.shiftPoints = new List<Tuple<Vector3f, Angle>>() {
                    new Tuple<Vector3f, Angle>(new Vector3f(79493, -51328, 4152), new Angle(-0.07f, 1.00f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(81838, -50513, 4797), new Angle(-0.34f, 0.94f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(79793, -55579, 4449), new Angle(0.24f, 0.97f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(76867, -55329, 5008), new Angle(0.33f, 0.94f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(76328, -53709, 5434), new Angle(0.09f, 1.00f))
             };
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(82670, -52140, 4198), new Angle(0.12f, 0.99f)).SetSpawnInfo(info));

            // room 12
            info = new ShifterSpawnInfo();
            info.shiftPoints = new List<Tuple<Vector3f, Angle>>() {
                    new Tuple<Vector3f, Angle>(new Vector3f(64648, -68321, 3598), new Angle(-0.89f, 0.45f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(66452, -69272, 4714), new Angle(-0.98f, 0.21f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(65408, -73251, 4696), new Angle(0.88f, 0.47f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(62399, -72982, 5667), new Angle(0.25f, 0.97f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(62083, -76873, 3498), new Angle(0.64f, 0.77f))
             };
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65136, -69490, 3798), new Angle(-0.98f, 0.20f)).SetSpawnInfo(info));

            // last room
            info = new ShifterSpawnInfo();
            info.shiftPoints = new List<Tuple<Vector3f, Angle>>() {
                    new Tuple<Vector3f, Angle>(new Vector3f(72746, -100720, 4607), new Angle(0.90f, 0.43f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(66184, -101375, 4098), new Angle(0.43f, 0.90f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(68180, -96153, 3808), new Angle(0.41f, 0.91f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(67910, -101696, 4158), new Angle(0.61f, 0.79f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(65359, -95448, 4798), new Angle(0.12f, 0.99f))
             };
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71403, -96622, 3953), new Angle(0.78f, 0.63f)).SetSpawnInfo(info));

            layout.DoNotReuse();
            Rooms.Add(layout);



            //// TURRETS
            layout = new RoomLayout();
            // first room aiming towards fan
            var turrInfo = new TurretSpawnInfo();
            turrInfo.VerticalAngle = 10;
            turrInfo.HorizontalAngle = 20;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(178097, -59380, 2748), new Angle(-0.71f, 0.70f)).SetSpawnInfo(turrInfo));

            // room 3
            turrInfo = new TurretSpawnInfo();
            turrInfo.VerticalAngle = 10;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(167714, -77371, 2800), new Angle(-0.94f, 0.34f)).SetSpawnInfo(turrInfo));

            // room 4
            turrInfo = new TurretSpawnInfo();
            turrInfo.HorizontalSpeed = 0;
            turrInfo.VerticalAngle = 15;
            // 20 0 -90 quaternion
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(147202, -82883, 3021), new QuaternionAngle(-0.70f, -0.12f, 0.12f, 0.70f)).SetSpawnInfo(turrInfo));

            // room 6 - aiming toward exit door
            turrInfo = new TurretSpawnInfo();
            turrInfo.HorizontalSpeed = 0;
            turrInfo.VerticalAngle = 10;
            turrInfo.SetRange(2100);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(135457, -54551, 3948), new Angle(-0.01f, 1.00f)).SetSpawnInfo(turrInfo));

            // tunnel with lasers (forces the player to come down versus jumping over)
            turrInfo = new TurretSpawnInfo();
            turrInfo.VerticalAngle = 25;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(121811, -42430, 3801), new Angle(-0.99f, 0.14f)).SetSpawnInfo(turrInfo));

            // crashers tunnel/room, aiming from dead-end tunnel
            turrInfo = new TurretSpawnInfo();
            turrInfo.HorizontalAngle = 90;
            turrInfo.VerticalAngle = 10;
            turrInfo.HorizontalSpeed = 60;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(87778, -41859, 4205), new Angle(-0.71f, 0.70f)).SetSpawnInfo(turrInfo));

            // crashers tunnel/room, blocking path near default pistol
            turrInfo = new TurretSpawnInfo();
            turrInfo.HorizontalSpeed = 0;
            turrInfo.VerticalAngle = 35;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(85944, -48563, 4206), new Angle(-0.21f, 0.98f)).SetSpawnInfo(turrInfo));

            // default turret
            turrInfo = new TurretSpawnInfo();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(80510, -52614, 4198), new Angle(-0.73f, 0.68f)).SetSpawnInfo(turrInfo));

            layout.Mask(SpawnPlane.Mask_Turret);
            Rooms.Add(layout);


            //// EXTRA
            layout = new RoomLayout();
            // first default pistol
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(184573, -54236, 2749), new Angle(0.71f, 0.71f)).Mask(SpawnPlane.Mask_Highground));
            // before room 3
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(174065, -74236, 2802), new Angle(0.68f, 0.73f)).Mask(SpawnPlane.Mask_Flatground));
            // vertical fans rooms and laser walls
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(148234, -75157, 4098), new Angle(-0.60f, 0.80f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(144809, -75065, 4750), new Angle(-0.02f, 1.00f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(148552, -76274, 4234), new Angle(-0.42f, 0.91f)).Mask(SpawnPlane.Mask_Airborne));
            // billboard after vertical air  (before room 7)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(137553, -50468, 5265), new Angle(-0.50f, 0.87f)).Mask(SpawnPlane.Mask_Highground));
            // after room 7, in tunnel
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(123978, -43368, 4606), new Angle(0.19f, 0.98f)).Mask(SpawnPlane.Mask_Highground));
            // tunnel with a lot of lasers room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(118115, -43376, 3801), new Vector3f(118345, -42584, 3800), new Angle(0.02f, 1.00f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(118775, -42438, 3801), new Vector3f(118989, -43287, 3801), new Angle(0.05f, 1.00f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(115409, -42432, 3801), new Vector3f(116778, -43241, 3805), new Angle(0.02f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(118007, -43337, 4347), new Angle(0.09f, 1.00f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(116343, -42282, 4234), new Angle(-0.06f, 1.00f)).Mask(SpawnPlane.Mask_Airborne));
            // after that section ^
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(115506, -40151, 3801), new Angle(-0.60f, 0.80f)).Mask(SpawnPlane.Mask_Highground)); // edge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(108417, -40979, 3748), new Angle(0.13f, 0.99f)).Mask(SpawnPlane.Mask_Highground)); // waver default spot
            // grapple between pipes room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(97274, -40876, 4196), new Angle(-0.01f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            // crashers with laser walls
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(91679, -43643, 4198), new Angle(0.07f, 1.00f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(93119, -43241, 4198), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(86543, -52429, 4898), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(85938, -51008, 4835), new Angle(0.61f, 0.79f)).Mask(SpawnPlane.Mask_Highground));
            // between rooms, on crate (before room 12)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(58005, -58213, 4798), new Angle(-0.52f, 0.85f)).Mask(SpawnPlane.Mask_Highground));
            // right after door of room 12
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62030, -78179, 3498), new Angle(0.69f, 0.72f)).Mask(SpawnPlane.Mask_Highground));
            // room 11 (after grinder)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(59216, -58961, 4498), new Vector3f(60895, -58264, 4501), new Angle(-0.02f, 1.00f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(4));
            // right after room 10
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(80545, -57743, 4503), new Angle(0.79f, 0.61f)).Mask(SpawnPlane.Mask_Highground));
            Rooms.Add(layout);

            #region Uplinks
            // mid-room shuriken
            NonPlaceableObject uplink = new UplinkShurikens(0x30, 0xA8);
            uplink.AddNewSpawnInfo(new UplinkShurikensSpawnInfo { MaxAttacks = 1});
            worldObjects.Add(uplink);

            // first kill room
            uplink = new UplinkSlowmo(0x60, 0xF8, 0xA0);
            uplink.AddNewSpawnInfo(new UplinkSlowmoSpawnInfo { TotalTime = Config.GetInstance().r.Next(2, 5)});
            worldObjects.Add(uplink);

            // 2nd room
            uplink = new UplinkShurikens(0x60, 0x120);
            uplink.AddNewSpawnInfo(new UplinkShurikensSpawnInfo { Duration = Config.GetInstance().r.Next(2, 5), MaxAttacks = Config.GetInstance().r.Next(1, 4)});
            worldObjects.Add(uplink);
            #endregion

        }

        public void Gen_Nightmare_BeforeCV() {}

        public void Gen_Nightmare_AfterCV() { HasRng = false; }

        protected override void Gen_PerRoom() {}
    }
}
