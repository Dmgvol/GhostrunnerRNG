using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.GameObjects;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.MemoryUtils;
using GhostrunnerRNG.NonPlaceableObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GhostrunnerRNG.Maps {
    class DharmaCity : MapCore, IModes {

        #region Rooms
        private Room room_1 = new Room(new Vector3f(108857, -14366, 1374), new Vector3f(113721, -6211, 4384)); // first fight (lonely pistol + 3 enemies on rooftop)
        private Room room_2 = new Room(new Vector3f(107860, -17332, 4396), new Vector3f(110422, -25432, 404));  // first drone
        private Room room_3 = new Room(new Vector3f(105776, -29794, 2859), new Vector3f(100694, -30811, 1373)); // 2'nd drone in hallway 
        private Room room_4 = new Room(new Vector3f(98878, -29044, 1923), new Vector3f(92131, -33496, -156)); // 2 drones hovering
        private Room room_5 = new Room(new Vector3f(92700, -33935, 458), new Vector3f(93928, -37374, -262)); // 2 pistols after drones ^
        private Room room_6_orb = new Room(new Vector3f(94823, -49431, 5048), new Vector3f(96319, -48272, 5862)); // big fight room, orb
        private Room room_6_protectedEnemies = new Room(new Vector3f(88150, -55767, 3499), new Vector3f(90652, -58286, 4888)); // big fight room, orb linked enemies
        private Room room_6_restEnemies = new Room(new Vector3f(84366, -54855, 1029), new Vector3f(95398, -42171, 5801)); // big fight room, 3+2 rest enemies
        private Room room_7 = new Room(new Vector3f(73332, -77553, 7225), new Vector3f(81767, -87619, 3708)); // 4 enemies, fight room
        private Room room_8 = new Room(new Vector3f(70556, -83913, 6867), new Vector3f(61959, -85951, 3881)); // 3 drones hallway
        private Room room_9 = new Room(new Vector3f(61821, -86056, 4380), new Vector3f(59259, -82421, 3509)); // 2 pistols after drones ^
        private Room room_10 = new Room(new Vector3f(56072, -79532, 5195), new Vector3f(47186, -73947, 2215)); // last fight before elevator
        #endregion

        #region HC Rooms
        private Room HC_room1 = new Room(new Vector3f(115779, -14563, 213), new Vector3f(108291, -5009, 5626)); 
        private Room HC_room2 = new Room(new Vector3f(107465, -19761, 2970), new Vector3f(110765, -31658, 6463));
        private Room HC_room3 = new Room(new Vector3f(109060, -33042, 3061), new Vector3f(106175, -36530, 5441)); 
        private Room HC_room4 = new Room(new Vector3f(91954, -39134, -1141), new Vector3f(102135, -28243, 3476)); 
        private Room HC_room5 = new Room(new Vector3f(96094, -40060, 1046), new Vector3f(90865, -43118, 3066));
        private Room HC_room6 = new Room(new Vector3f(86915, -59286, 3314), new Vector3f(96869, -47264, 6561));
        private Room HC_room7 = new Room(new Vector3f(91210, -70406, 3929), new Vector3f(82735, -73599, 6097));
        private Room HC_room8 = new Room(new Vector3f(72689, -87970, 2846), new Vector3f(81210, -76819, 7248));
        private Room HC_room9 = new Room(new Vector3f(54590, -80484, 1703), new Vector3f(47986, -73592, 5440));
        #endregion

        public DharmaCity() : base(GameUtils.MapType.DharmaCity) {
            // first cp, move slightly forward
            ModifyCP(new DeepPointer(PtrDB.DP_Dharma_ElevatorCP), new Vector3f(104390, -13350, 1745), GameHook.game);

            if(!GameHook.IsHC) {
                // Custom CP before fighting room, (before 2nd uplink)
                CustomCheckPoints.Add(new CustomCP(mapType, new Vector3f(92117, -40419, 2783), new Vector3f(94247, -41923, 1413),
                    new Vector3f(94133, -41282, 1798), new Angle(-1f, 0.03f)));
            }

        }
        public void Gen_Normal() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game, 0, 27);
            AllEnemies.AddRange(GetAllEnemies(GameHook.game, 32, 7));

            Rooms = new List<RoomLayout>();
            RoomLayout layout;
            List<Enemy> enemies;
            List<Enemy> drones = new List<Enemy>();

            ///// First room - enemies /////
            enemies = room_1.ReturnEnemiesInRoom(AllEnemies);
            enemies.ForEach(x => x.DisableAttachedCP(GameHook.game)); // disable cp for all enemies in room1
            EnemiesWithoutCP.AddRange(enemies.Skip(1)); // skip first 2 

            layout = new RoomLayout(enemies.Take(1).ToList()); // take 2, leave them in that area
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(110973, -16164, 3807), new Angle(0.83f, 0.56f))); // umbrella on that platform ^^
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(109706, -12909, 3110), new Angle(-1.00f, 0.07f))); // neon sign, in front of spawn
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(104915, -12566, 1698), new Angle(-0.17f, 0.99f))); // around the corner of spawn
            Rooms.Add(layout);

            /////// Drone 1 - First drone /////
            enemies = room_2.ReturnEnemiesInRoom(AllEnemies);
            EnemyDrone drone1 = new EnemyDrone(enemies[0]);
            drones.Add(drone1);

            ///// Drone 2 - hallway /////
            enemies = room_3.ReturnEnemiesInRoom(AllEnemies);
            EnemyDrone drone2 = new EnemyDrone(enemies[0]);
            drones.Add(drone2);

            ///// 2 Drone hovering /////
            enemies = room_4.ReturnEnemiesInRoom(AllEnemies);
            EnemyDrone drone3 = new EnemyDrone(enemies[0]);
            EnemyDrone drone4 = new EnemyDrone(enemies[1]);
            drones.Add(drone4); // second drone to drone list

            layout = new RoomLayout(drone3); // leaving only one
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(97846, -32198, 1339), new Vector3f(95746, -29965, 700), new Angle(0.10f, 1.00f))); // light rng
            layout.BanRoomEnemyType(Enemy.EnemyTypes.Default);
            Rooms.Add(layout);

            // Double pistols after drones (with cp)
            enemies = room_5.ReturnEnemiesInRoom(AllEnemies);
            enemies.ForEach(x => x.DisableAttachedCP(GameHook.game)); // disable enemy checkpoint
            EnemiesWithoutCP.Add(enemies[1]);
            layout = new RoomLayout(enemies[0]); // leave one
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(92463, -36036, -1), new Angle(0.41f, 0.91f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(92038, -44474, 1799), new Angle(0.50f, 0.87f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(93998, -36579, 498), new Angle(0.79f, 0.61f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(87360, -46419, 4297), new Angle(0.35f, 0.94f)));
            Rooms.Add(layout);

            ///// Fighting room with shield orb /////
            // Shield Orb
            enemies = room_6_orb.ReturnEnemiesInRoom(AllEnemies);
            EnemyShieldOrb shieldOrb1 = new EnemyShieldOrb(enemies[0]);
            shieldOrb1.HideBeam_Range(0, 4);
            shieldOrb1.HideBeam_Range(1, 4);
            shieldOrb1.LinkObject(2);

            layout = new RoomLayout(shieldOrb1);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(90731, -52504, 5438))); // on umbrella, middle tower
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(94547, -52794, 5792))); // neon sign, right platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(92456, -55706, 5501), new Vector3f(92411, -54205, 6441)).AsVerticalPlane()); // tower billboard, near slide
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(86821, -54536, 4067), new Vector3f(88001, -54510, 4669)).AsVerticalPlane()); // "CHICK" billboard, middle
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(84911, -57269, 3969), new Vector3f(86543, -58867, 4866)).AsVerticalPlane()); // far left billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(91182, -57538, 4686))); // floating after slide
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(89422, -52134, 4421), new Vector3f(90102, -51630, 4642))); // floating range in middle platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(96011, -51791, 5347), new Vector3f(96001, -50334, 5901))); // billboard near default pos
            layout.BanRoomEnemyType(Enemy.EnemyTypes.Default);
            Rooms.Add(layout);
        

            // Enemies in that area
            enemies = room_6_protectedEnemies.ReturnEnemiesInRoom(AllEnemies);
            enemies.AddRange(room_6_restEnemies.ReturnEnemiesInRoom(AllEnemies));
            layout = new RoomLayout(enemies);
            // default platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(90420, -58150, 4103), new Vector3f(88460, -57080, 4093), new Angle(0.71f, 0.71f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(96110, -53358, 5288), new Vector3f(94905, -52082, 5310), new Angle(0.89f, 0.45f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(94760, -48905, 5289), new Vector3f(95817, -50134, 5300), new Angle(-0.93f, 0.37f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(90852, -51435, 4090), new Vector3f(89324, -52013, 4090), new Angle(0.71f, 0.71f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(86243, -53613, 4120), new Vector3f(85263, -54622, 4090), new Angle(0.06f, 1.00f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(88126, -44594, 3412), new Vector3f(87629, -46636, 3398), new Angle(0.22f, 0.97f)));

            // high/special places
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(86330, -52879, 4995), new Angle(0.14f, 0.99f))); // boiler, left platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(89522, -58908, 4488), new Angle(0.69f, 0.72f))); // neon sign, near door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(88137, -55193, 4915), new Vector3f(88115, -56674, 4915), new Angle(0.30f, 0.95f)).AsVerticalPlane()); // billboard, before door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(92382, -57508, 5123), new Angle(0.94f, 0.34f))); // slide
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(96210, -51649, 6015), new Vector3f(96203, -50467, 6061), new Angle(1.00f, 0.03f))); // right platform, billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(91456, -58278, 5744), new Angle(0.77f, 0.64f))); // ac fan, near door
            Rooms.Add(layout);

            ///// Room 7 - Main closed fight room /////
            enemies = room_7.ReturnEnemiesInRoom(AllEnemies);
            enemies[0].SetEnemyType(Enemy.EnemyTypes.Drone);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Waver);

            layout = new RoomLayout(enemies.Skip(1).ToList()); // skip drone

            // default platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74027, -83519, 5132), new Vector3f(74967, -85267, 5109), new Angle(-0.01f, 1.00f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74970, -81132, 5069), new Vector3f(73955, -79938, 5070), new Angle(-0.60f, 0.80f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(80068, -81804, 5098), new Vector3f(80895, -78602, 5098), new Angle(0.71f, 0.70f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(78989, -80509, 4049), new Vector3f(77483, -81269, 4039), new Angle(0.02f, 1.00f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(75800, -85689, 4039), new Vector3f(78038, -84749, 4062), new Angle(0.02f, 1.00f)));

            // high/special 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(73742, -82355, 6029), new Angle(-0.21f, 0.98f)).BanEnemyType(Enemy.EnemyTypes.Waver)); // billboard near door // no waver
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(73547, -83933, 6176), new Angle(0.01f, 1.00f)).BanEnemyType(Enemy.EnemyTypes.Waver)); // led sign, near door // no waver
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(76603, -83007, 4914), new Angle(0.11f, 0.99f)).BanEnemyType(Enemy.EnemyTypes.Waver)); // middle of rotating platform // no waver
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(80923, -80223, 5553), new Angle(0.84f, 0.54f))); // first platform, rooftop on right, near neon sign
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(81134, -80838, 6165), new Angle(0.87f, 0.49f))); // first platform, rooftop above neon sign
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(75606, -77620, 5583), new Angle(-0.39f, 0.92f))); // flashing neon sign, left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(79404, -81194, 4948), new Angle(0.52f, 0.86f))); // fuel tank

            Rooms.Add(layout);

            // Drone - with custom patrol points
            layout = new RoomLayout(new EnemyDrone(enemies[0]));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(79028, -85769, 5364))
                .AddPatrolPoint(new DeepPointer(PtrDB.DP_Drone_Dharma_Patrol), new Vector3f(78996, -81310, 5364)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74338, -85272, 5570))
                .AddPatrolPoint(new DeepPointer(PtrDB.DP_Drone_Dharma_Patrol), new Vector3f(74371, -80265, 5570)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74749, -81993, 5293))
               .AddPatrolPoint(new DeepPointer(PtrDB.DP_Drone_Dharma_Patrol), new Vector3f(79130, -81986, 5293)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(77323, -85030, 5763))
               .AddPatrolPoint(new DeepPointer(PtrDB.DP_Drone_Dharma_Patrol), new Vector3f(77368, -81590, 5763)));
            layout.BanRoomEnemyType(Enemy.EnemyTypes.Default); // to avoid grounded enemies spawning in mid-air
            Rooms.Add(layout);

            ///// Triple Drones /////
            enemies = room_8.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyDrone(enemies[0]);
            enemies[1] = new EnemyDrone(enemies[1]);
            enemies[2] = new EnemyDrone(enemies[2]);

            drones.AddRange(enemies.Take(2)); // take 2 first, leave 3rd without rng

            ///// Double pistols /////
            enemies = room_9.ReturnEnemiesInRoom(AllEnemies);
            enemies.ForEach(e => e.DisableAttachedCP(GameHook.game));
            EnemiesWithoutCP.AddRange(enemies); // add both pistols to enemies without cp

            ///// Last fight before elevator /////
            enemies = room_10.ReturnEnemiesInRoom(AllEnemies);

            // Drones //
            layout = new RoomLayout(new EnemyDrone(enemies[0]), new EnemyDrone(enemies[1]));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(55463, -77624, 4302), new Angle(-0.22f, 0.98f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51655, -79074, 4230), new Angle(0.69f, 0.73f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51703, -77020, 3810), new Angle(0.03f, 1.00f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50039, -75045, 4652), new Angle(-0.13f, 0.99f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49819, -79016, 4220), new Angle(0.01f, 1.00f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53863, -80096, 4875), new Angle(0.32f, 0.95f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53255, -74233, 4408), new Angle(-0.48f, 0.88f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50336, -80752, 3725), new Angle(0.45f, 0.89f)));
            layout.BanRoomEnemyType(Enemy.EnemyTypes.Default); // to avoid grounded enemies to spawn mid air
            Rooms.Add(layout);

            // Grounded enemies
            layout = new RoomLayout(enemies.Skip(2).ToList()); // skip drones
            // default platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(52799, -74275, 3714), new Vector3f(53924, -74971, 3718), new Angle(-0.43f, 0.90f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49284, -74915, 3698), new Vector3f(50409, -74271, 3702), new Angle(-0.31f, 0.95f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51195, -76319, 2844), new Vector3f(51991, -74330, 2818), new Angle(-0.24f, 0.97f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51281, -79935, 2818), new Vector3f(51842, -77874, 2818), new Angle(0.29f, 0.96f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51023, -79436, 3709), new Vector3f(49561, -78538, 3707), new Angle(-0.04f, 1.00f)));

            // special/high places
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50961, -77065, 4426), new Angle(0.07f, 1.00f))); // billboard center
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51541, -74056, 4806), new Angle(-0.70f, 0.71f))); // billboard left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49524, -74234, 4276), new Angle(-0.21f, 0.98f))); // small rooftop, far left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(48271, -79192, 4605), new Angle(0.16f, 0.99f))); // fuel tank, far back
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53581, -79020, 4442), new Angle(0.01f, 1.00f))); // slide
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53704, -78044, 4441), new Angle(0.26f, 0.97f))); // edge of neon sign near slide
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51829, -80100, 3317), new Angle(0.72f, 0.69f))); // zipline pole
            Rooms.Add(layout);

            ///// Extra rooms for enemies without cp for extra rng ////
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(109548, -27708, 873), new Angle(0.71f, 0.70f))); // after first drone, small rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(99260, -30750, 1488), new Vector3f(99757, -29767, 1488), new Angle(-0.01f, 1.00f))); // platform after 2n'd drone
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(93756, -35108, -1), new Vector3f(92893, -35726, -1), new Angle(0.65f, 0.76f))); // platform on first super jump 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(92666, -40935, 1798), new Vector3f(93171, -42021, 1798), new Angle(0.71f, 0.70f))); // platform on second super jump
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(92834, -45708, 2516), new Angle(0.73f, 0.68f))); // small ad near second super jump
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(89674, -67624, 4700), new Vector3f(88688, -68732, 4700), new Angle(0.70f, 0.71f))); // platform after metro
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(81043, -73044, 4700), new Vector3f(79913, -72398, 4715), new Angle(0.67f, 0.74f))); // platform after metro 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(72415, -84887, 5108), new Angle(-0.01f, 1.00f))); // right after locked fight room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61223, -84295, 4003), new Vector3f(60056, -85335, 3998), new Angle(0.00f, 1.00f))); // platform after triple drones
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(57302, -78823, 4107), new Vector3f(56324, -79587, 4098), new Angle(-0.02f, 1.00f))); // platform before last fight
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53821, -72708, 3698), new Angle(-0.76f, 0.65f))); // right before elevator
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(90793, -60401, 3577), new Angle(0.06f, 1.00f))); // metro
            Rooms.Add(layout);

            //// available DRONES for rng ////
            layout = new RoomLayout(drones);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53227, -79133, 5076), new Angle(0.39f, 0.92f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(59742, -83256, 4324), new Angle(-0.37f, 0.93f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(80435, -77091, 5250), new Angle(0.69f, 0.72f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(90858, -67060, 5315), new Angle(0.74f, 0.68f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(92234, -43054, 2113), new Angle(0.54f, 0.84f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(88855, -55886, 4571), new Angle(0.72f, 0.69f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(106894, -30698, 2581), new Angle(0.54f, 0.84f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(99067, -29556, 2224), new Angle(-0.12f, 0.99f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(87373, -54178, 4520), new Angle(0.48f, 0.88f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(80767, -72953, 5578), new Angle(0.32f, 0.95f)));
            layout.BanRoomEnemyType(Enemy.EnemyTypes.Default);
            Rooms.Add(layout);

            #region Jump
            //first jump
            NonPlaceableObject uplink = new UplinkJump(0x10, 0x8F8);//5,3,6
            uplink.AddSpawnInfo(new UplinkJumpSpawnInfo().SetRarity(0.5));//default
            var jumpSpawn = new UplinkJumpSpawnInfo { JumpForwardMultiplier = 1.5f };
            uplink.AddSpawnInfo(jumpSpawn);//really short jump need to use dash
            jumpSpawn = new UplinkJumpSpawnInfo { JumpForwardMultiplier = -3.5f };
            uplink.AddSpawnInfo(jumpSpawn);//normal backward jump
            //jumpSpawn = new UplinkJumpSpawnInfo { JumpForwardMultiplier = 0.0f, JumpGravity = 7.5f };
            //uplink.AddSpawnInfo(jumpSpawn);//ultra short jump, can make it with dsj or from left platform   // NOTE: leave it for SR options
            jumpSpawn = new UplinkJumpSpawnInfo { JumpGravity = 3.0f };
            uplink.AddSpawnInfo(jumpSpawn);//high jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpForwardMultiplier = 8.0f, JumpGravity = 2.0f }.SetRarity(0.15);
            uplink.AddSpawnInfo(jumpSpawn);//ultra long jump, to the door
            worldObjects.Add(uplink);

            //second jump
            uplink = new UplinkJump(0x10, 0x8F0);// 4.7,3,6
            uplink.AddSpawnInfo(new UplinkJumpSpawnInfo().SetRarity(0.5));//default
            jumpSpawn = new UplinkJumpSpawnInfo { JumpForwardMultiplier = 1.0f };
            uplink.AddSpawnInfo(jumpSpawn);//really short jump need to use dash
            jumpSpawn = new UplinkJumpSpawnInfo { JumpForwardMultiplier = -3.5f };
            uplink.AddSpawnInfo(jumpSpawn);//normal backward jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpGravity = Config.GetInstance().r.Next(30, 71) / 10 };
            uplink.AddSpawnInfo(jumpSpawn);//high jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = Config.GetInstance().r.Next(30, 61) / 10, JumpForwardMultiplier = 3.5f, JumpGravity = Config.GetInstance().r.Next(40, 61) / 10 }.SetRarity(0.25);
            uplink.AddSpawnInfo(jumpSpawn);//random jump
            worldObjects.Add(uplink);

            //third jump
            uplink = new UplinkJump(0x18, 0x7D8);// 4.3,3.6,6
            uplink.AddSpawnInfo(new UplinkJumpSpawnInfo().SetRarity(0.5));//default
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 1.0f, JumpForwardMultiplier = 1.0f, JumpGravity = -5.0f };
            uplink.AddSpawnInfo(jumpSpawn);//weird negative gravity jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpForwardMultiplier = -4.0f };
            uplink.AddSpawnInfo(jumpSpawn);//normal backward jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = -4.0f }.SetRarity(0.1);
            uplink.AddSpawnInfo(jumpSpawn);//cant jump

            jumpSpawn = new UplinkJumpSpawnInfo {
                TimeToActivate = Config.GetInstance().r.Next(3, 26) / 10,
                JumpMultiplier = Config.GetInstance().r.Next(10, 81) / 10,
                JumpForwardMultiplier = Config.GetInstance().r.Next(0, 45) / 10 * (Config.GetInstance().r.Next(2) == 0 ? 1 : (-1)),
                JumpGravity = Config.GetInstance().r.Next(10, 101) / 10
            };
            uplink.AddSpawnInfo(jumpSpawn);//random jump
            worldObjects.Add(uplink);

            //fourth jump, patrol drone room
            uplink = new UplinkJump(0x28, 0x240);// 4.0,2.5,6
            uplink.AddSpawnInfo(new UplinkJumpSpawnInfo());//default
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 5.0f, JumpForwardMultiplier = 0.0f };
            uplink.AddSpawnInfo(jumpSpawn);//vertical jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpForwardMultiplier = -3.0f };
            uplink.AddSpawnInfo(jumpSpawn);//normal backward jump

            jumpSpawn = new UplinkJumpSpawnInfo {
                TimeToActivate = Config.GetInstance().r.Next(3, 46) / 10,
                JumpMultiplier = Config.GetInstance().r.Next(10, 81) / 10,
                JumpForwardMultiplier = Config.GetInstance().r.Next(0, 45) / 10 * (Config.GetInstance().r.Next(2) == 0 ? 1 : (-1)),
                JumpGravity = Config.GetInstance().r.Next(10, 101) / 10
            };
            uplink.AddSpawnInfo(jumpSpawn);//random jump
            worldObjects.Add(uplink);

            //fifth jump, 3 drone hallway
            uplink = new UplinkJump(0x30, 0x5d0);// 3.8,3.3,6
            uplink.AddSpawnInfo(new UplinkJumpSpawnInfo().SetRarity(0.5));//default
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 1.0f, JumpForwardMultiplier = 8.0f, JumpGravity = 3.5f };
            uplink.AddSpawnInfo(jumpSpawn);//long jump to the pistols
            jumpSpawn = new UplinkJumpSpawnInfo { JumpForwardMultiplier = -4.0f };
            uplink.AddSpawnInfo(jumpSpawn);//normal backward jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 1.0f, JumpForwardMultiplier = 3.0f, JumpGravity = -2.0f };
            uplink.AddSpawnInfo(jumpSpawn);//weird negative gravity jump
            jumpSpawn = new UplinkJumpSpawnInfo {
                TimeToActivate = Config.GetInstance().r.Next(3, 46) / 10,
                JumpMultiplier = Config.GetInstance().r.Next(10, 81) / 10,
                JumpForwardMultiplier = Config.GetInstance().r.Next(0, 45) / 10 * (Config.GetInstance().r.Next(2) == 0 ? 1 : (-1)),
                JumpGravity = Config.GetInstance().r.Next(10, 101) / 10
            };
            uplink.AddSpawnInfo(jumpSpawn);//random jump
            worldObjects.Add(uplink);
            #endregion

            #region Billboards
            // first billboard
            uplink = new Billboard(0x20, 0x100);
            uplink.AddSpawnInfo(new BillboardSpawnInfo { Angle1 = -450 }); // extra spins
            uplink.AddSpawnInfo(new BillboardSpawnInfo { Time1 = Config.GetInstance().r.Next(3, 6), Time2 = Config.GetInstance().r.Next(3, 6) }); // very slow
            uplink.AddSpawnInfo(new BillboardSpawnInfo { Time1 = 1, Angle1 = 270, Time2 = 3, Angle2 = -45 });
            uplink.AddSpawnInfo(new BillboardSpawnInfo { Time1 = 3, Angle1 = -30, Time2 = 3, Angle2 = -50 });
            uplink.AddSpawnInfo(new BillboardSpawnInfo()); // default
            worldObjects.Add(uplink);

            uplink = new Billboard(0x20, 0x108);
            uplink.AddSpawnInfo(new BillboardSpawnInfo { Angle1 = -350 }); // extra spins
            uplink.AddSpawnInfo(new BillboardSpawnInfo { Time1 = Config.GetInstance().r.Next(1, 6), Time2 = Config.GetInstance().r.Next(1, 6) }); // speed rng
            uplink.AddSpawnInfo(new BillboardSpawnInfo { Time1 = 1, Angle1 = -270, Time2 = 2, Angle2 = 45 });
            uplink.AddSpawnInfo(new BillboardSpawnInfo { Time1 = 3, Angle1 = -60, Time2 = 2, Angle2 = 20 });
            uplink.AddSpawnInfo(new BillboardSpawnInfo()); // default
            worldObjects.Add(uplink);

            uplink = new Billboard(0x28, 0x1A8);
            uplink.AddSpawnInfo(new BillboardSpawnInfo { Angle1 = -810 }); // extra spins
            uplink.AddSpawnInfo(new BillboardSpawnInfo { Time1 = Config.GetInstance().r.Next(1, 6), Time2 = Config.GetInstance().r.Next(1, 6) }); // speed rng
            uplink.AddSpawnInfo(new BillboardSpawnInfo { Time1 = 1, Angle1 = -220, Time2 = 2, Angle2 = 35 });
            uplink.AddSpawnInfo(new BillboardSpawnInfo { Time1 = 3, Angle1 = -90, Time2 = 2, Angle2 = 30 });
            uplink.AddSpawnInfo(new BillboardSpawnInfo()); // default
            worldObjects.Add(uplink);

            uplink = new Billboard(0x30, 0x258);
            uplink.AddSpawnInfo(new BillboardSpawnInfo { Angle1 = 450 }); // extra spins
            uplink.AddSpawnInfo(new BillboardSpawnInfo { Time1 = Config.GetInstance().r.Next(1, 6), Time2 = Config.GetInstance().r.Next(1, 6) }); // speed rng
            uplink.AddSpawnInfo(new BillboardSpawnInfo { Time1 = 1, Angle1 = -220, Time2 = 2, Angle2 = 35 });
            uplink.AddSpawnInfo(new BillboardSpawnInfo { Time1 = 3, Angle1 = -90, Time2 = 2, Angle2 = -180 });
            uplink.AddSpawnInfo(new BillboardSpawnInfo { Time1 = 0, Time2 = 0 }.SetRarity(0.05)); // no time
            uplink.AddSpawnInfo(new BillboardSpawnInfo()); // default
            worldObjects.Add(uplink);
            #endregion
        }

        public void Gen_Easy() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game, 0, 27);
            AllEnemies.AddRange(GetAllEnemies(GameHook.game, 32, 7));

            Rooms = new List<RoomLayout>();
            RoomLayout layout;
            List<Enemy> enemies;
            List<Enemy> drones = new List<Enemy>();


            ///// First room - enemies /////
            enemies = room_1.ReturnEnemiesInRoom(AllEnemies);
            enemies.ForEach(x => x.DisableAttachedCP(GameHook.game)); // disable cp for all enemies in room1
            EnemiesWithoutCP.AddRange(enemies.Skip(1)); // skip first 2 

            layout = new RoomLayout(enemies.Take(1).ToList()); // take 2, leave them in that area
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(110973, -16164, 3807), new Angle(0.83f, 0.56f))); // umbrella on that platform ^^
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(104915, -12566, 1698), new Angle(-0.17f, 0.99f))); // around the corner of spawn
            Rooms.Add(layout);

            /////// Drone 1 - First drone /////
            enemies = room_2.ReturnEnemiesInRoom(AllEnemies);
            EnemyDrone drone1 = new EnemyDrone(enemies[0]);
            drones.Add(drone1);

            ///// Drone 2 - hallway /////
            enemies = room_3.ReturnEnemiesInRoom(AllEnemies);
            EnemyDrone drone2 = new EnemyDrone(enemies[0]);
            drones.Add(drone2);

            ///// 2 Drone hovering /////
            enemies = room_4.ReturnEnemiesInRoom(AllEnemies);
            EnemyDrone drone3 = new EnemyDrone(enemies[0]);
            EnemyDrone drone4 = new EnemyDrone(enemies[1]);
            drones.Add(drone4); // second drone to drone list

            layout = new RoomLayout(drone3); // leaving only one
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(97846, -32198, 1339), new Vector3f(95746, -29965, 700), new Angle(0.10f, 1.00f))); // light rng
            layout.BanRoomEnemyType(Enemy.EnemyTypes.Default);
            Rooms.Add(layout);

            // Double pistols after drones (with cp)
            enemies = room_5.ReturnEnemiesInRoom(AllEnemies);
            enemies.ForEach(x => x.DisableAttachedCP(GameHook.game)); // disable enemy checkpoint
            EnemiesWithoutCP.Add(enemies[1]);
            layout = new RoomLayout(enemies[0]); // leave one
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(92463, -36036, -1), new Angle(0.41f, 0.91f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(92038, -44474, 1799), new Angle(0.50f, 0.87f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(92688, -35070, 308), new Angle(0.49f, 0.87f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(87423, -46252, 3757), new Angle(0.29f, 0.96f)));
            Rooms.Add(layout);

            ///// Fighting room with shield orb /////
            // Shield Orb
            enemies = room_6_orb.ReturnEnemiesInRoom(AllEnemies);
            EnemyShieldOrb shieldOrb1 = new EnemyShieldOrb(enemies[0]);
            shieldOrb1.HideBeam_Range(0, 4);
            shieldOrb1.HideBeam_Range(1, 4);
            shieldOrb1.LinkObject(2);

            // orb
            layout = new RoomLayout(shieldOrb1);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(94547, -52794, 5792))); // neon sign, right platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(86821, -54536, 4067), new Vector3f(88001, -54510, 4669)).AsVerticalPlane()); // "CHICK" billboard, middle
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(84911, -57269, 3969), new Vector3f(86543, -58867, 4866)).AsVerticalPlane()); // far left billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(91182, -57538, 4686))); // floating after slide
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(92944, -57467, 5348))); // middle of slide
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(89422, -52134, 4421), new Vector3f(90102, -51630, 4642))); // floating range in middle platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(96011, -51791, 5347), new Vector3f(96001, -50334, 5901))); // billboard near default pos
            layout.BanRoomEnemyType(Enemy.EnemyTypes.Default);
            Rooms.Add(layout);
            

            // Enemies in that area
            enemies = room_6_protectedEnemies.ReturnEnemiesInRoom(AllEnemies);
            enemies.AddRange(room_6_restEnemies.ReturnEnemiesInRoom(AllEnemies));
            layout = new RoomLayout(enemies);
            // default platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(90420, -58150, 4103), new Vector3f(88460, -57080, 4093), new Angle(0.71f, 0.71f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(96110, -53358, 5288), new Vector3f(94905, -52082, 5310), new Angle(0.89f, 0.45f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(94760, -48905, 5289), new Vector3f(95817, -50134, 5300), new Angle(-0.93f, 0.37f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(90852, -51435, 4090), new Vector3f(89324, -52013, 4090), new Angle(0.71f, 0.71f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(86243, -53613, 4120), new Vector3f(85263, -54622, 4090), new Angle(0.06f, 1.00f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(88126, -44594, 3412), new Vector3f(87629, -46636, 3398), new Angle(0.22f, 0.97f)));

            // high/special places
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(89522, -58908, 4488), new Angle(0.69f, 0.72f))); // neon sign, near door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(92382, -57508, 5123), new Angle(0.94f, 0.34f))); // slide
            Rooms.Add(layout);

            ///// Room 7 - Main closed fight room /////
            enemies = room_7.ReturnEnemiesInRoom(AllEnemies);
            enemies[0].SetEnemyType(Enemy.EnemyTypes.Drone);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Waver);

            layout = new RoomLayout(enemies.Skip(1).ToList()); // skip drone

            // default platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74027, -83519, 5132), new Vector3f(74967, -85267, 5109), new Angle(-0.01f, 1.00f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74970, -81132, 5069), new Vector3f(73955, -79938, 5070), new Angle(-0.60f, 0.80f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(80068, -81804, 5098), new Vector3f(80895, -78602, 5098), new Angle(0.71f, 0.70f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(78989, -80509, 4049), new Vector3f(77483, -81269, 4039), new Angle(0.02f, 1.00f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(75800, -85689, 4039), new Vector3f(78038, -84749, 4062), new Angle(0.02f, 1.00f)));
            // high/special 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(76603, -83007, 4914), new Angle(0.11f, 0.99f)).BanEnemyType(Enemy.EnemyTypes.Waver)); // middle of rotating platform // no waver
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(80923, -80223, 5553), new Angle(0.84f, 0.54f))); // first platform, rooftop on right, near neon sign
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(79404, -81194, 4948), new Angle(0.52f, 0.86f))); // fuel tank

            Rooms.Add(layout);

            // Drone - with custom patrol points
            layout = new RoomLayout(new EnemyDrone(enemies[0]));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(79028, -85769, 5364))
                .AddPatrolPoint(new DeepPointer(PtrDB.DP_Drone_Dharma_Patrol), new Vector3f(78996, -81310, 5364)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74338, -85272, 5570))
                .AddPatrolPoint(new DeepPointer(PtrDB.DP_Drone_Dharma_Patrol), new Vector3f(74371, -80265, 5570)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74749, -81993, 5293))
               .AddPatrolPoint(new DeepPointer(PtrDB.DP_Drone_Dharma_Patrol), new Vector3f(79130, -81986, 5293)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(77323, -85030, 5763))
               .AddPatrolPoint(new DeepPointer(PtrDB.DP_Drone_Dharma_Patrol), new Vector3f(77368, -81590, 5763)));
            layout.BanRoomEnemyType(Enemy.EnemyTypes.Default); // to avoid grounded enemies spawning in mid-air
            Rooms.Add(layout);

            ///// Triple Drones /////
            enemies = room_8.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyDrone(enemies[0]);
            enemies[1] = new EnemyDrone(enemies[1]);
            enemies[2] = new EnemyDrone(enemies[2]);

            drones.AddRange(enemies.Take(2)); // take 2 first, leave 3rd without rng

            ///// Double pistols /////
            enemies = room_9.ReturnEnemiesInRoom(AllEnemies);
            enemies.ForEach(e => e.DisableAttachedCP(GameHook.game));
            EnemiesWithoutCP.AddRange(enemies); // add both pistols to enemies without cp

            ///// Last fight before elevator /////
            enemies = room_10.ReturnEnemiesInRoom(AllEnemies);

            // Drones //
            layout = new RoomLayout(new EnemyDrone(enemies[0]), new EnemyDrone(enemies[1]));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51655, -79074, 4230), new Angle(0.69f, 0.73f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51703, -77020, 3440), new Angle(0.03f, 1.00f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49819, -79016, 4220), new Angle(0.01f, 1.00f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51790, -74419, 4266), new Angle(-0.34f, 0.94f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50336, -80752, 3725), new Angle(0.45f, 0.89f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(56152, -79055, 4478), new Angle(0.02f, 1.00f)));
            layout.BanRoomEnemyType(Enemy.EnemyTypes.Default); // to avoid grounded enemies to spawn mid air
            Rooms.Add(layout);

            // Grounded enemies
            layout = new RoomLayout(enemies.Skip(2).ToList()); // skip drones
            // default platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(52799, -74275, 3714), new Vector3f(53924, -74971, 3718), new Angle(-0.43f, 0.90f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49284, -74915, 3698), new Vector3f(50409, -74271, 3702), new Angle(-0.31f, 0.95f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51195, -76319, 2844), new Vector3f(51991, -74330, 2818), new Angle(-0.24f, 0.97f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51281, -79935, 2818), new Vector3f(51842, -77874, 2818), new Angle(0.29f, 0.96f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51023, -79436, 3709), new Vector3f(49561, -78538, 3707), new Angle(-0.04f, 1.00f)));

            // special/high places
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(48271, -79192, 4605), new Angle(0.16f, 0.99f))); // fuel tank, far back
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53581, -79020, 4442), new Angle(0.01f, 1.00f))); // slide
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51829, -80100, 3317), new Angle(0.72f, 0.69f))); // zipline pole
            Rooms.Add(layout);

            ///// Extra rooms for enemies without cp for extra rng ////
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(109548, -27708, 873), new Angle(0.71f, 0.70f))); // after first drone, small rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(99260, -30750, 1488), new Vector3f(99757, -29767, 1488), new Angle(-0.01f, 1.00f))); // platform after 2n'd drone
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(93756, -35108, -1), new Vector3f(92893, -35726, -1), new Angle(0.65f, 0.76f))); // platform on first super jump 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(92666, -40935, 1798), new Vector3f(93171, -42021, 1798), new Angle(0.71f, 0.70f))); // platform on second super jump
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(92834, -45708, 2516), new Angle(0.73f, 0.68f))); // small ad near second super jump
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(89674, -67624, 4700), new Vector3f(88688, -68732, 4700), new Angle(0.70f, 0.71f))); // platform after metro
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(81043, -73044, 4700), new Vector3f(79913, -72398, 4715), new Angle(0.67f, 0.74f))); // platform after metro 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(72415, -84887, 5108), new Angle(-0.01f, 1.00f))); // right after locked fight room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61223, -84295, 4003), new Vector3f(60056, -85335, 3998), new Angle(0.00f, 1.00f))); // platform after triple drones
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(57302, -78823, 4107), new Vector3f(56324, -79587, 4098), new Angle(-0.02f, 1.00f))); // platform before last fight
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53821, -72708, 3698), new Angle(-0.76f, 0.65f))); // right before elevator
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(90793, -60401, 3577), new Angle(0.06f, 1.00f))); // metro
            Rooms.Add(layout);

            //// available DRONES for rng ////
            layout = new RoomLayout(drones);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53227, -79133, 5076), new Angle(0.39f, 0.92f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(59742, -83256, 4324), new Angle(-0.37f, 0.93f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(80435, -77091, 5250), new Angle(0.69f, 0.72f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(92234, -43054, 2113), new Angle(0.54f, 0.84f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(88855, -55886, 4571), new Angle(0.72f, 0.69f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(87373, -54178, 4520), new Angle(0.48f, 0.88f)));
            layout.BanRoomEnemyType(Enemy.EnemyTypes.Default);
            Rooms.Add(layout);
        }

        public void Gen_Hardcore() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game);
            List<Enemy> Shifters = new List<Enemy>();
            Rooms = new List<RoomLayout>();
            RoomLayout layout;

            //// Room 1 ////
            var enemies = HC_room1.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyTurret(enemies[0]);
            enemies[1] = new EnemyDrone(enemies[1]);
            enemies[2] = new EnemyDrone(enemies[2]);
            enemies[3] = new EnemyDrone(enemies[3]);
            enemies[4].SetEnemyType(Enemy.EnemyTypes.Splitter);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, enemyIndex: 3); // drone
            RandomPickEnemiesWithoutCP(ref enemies, force: true, enemyIndex: 0); // turret
            RandomPickEnemiesWithoutCP(ref enemies, force: true);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(112082, -8229, 3400), new Vector3f(110913, -9410, 3400), new Angle(1.00f, 0.02f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(111274, -11973, 3400), new Vector3f(110171, -10872, 3400), new Angle(0.69f, 0.73f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(113727, -13435, 1198), new Vector3f(112995, -12791, 1198), new Angle(-1.00f, 0.02f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(110273, -8499, 1898), new Vector3f(109289, -9214, 1903), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(109694, -12839, 3110), new Angle(-0.99f, 0.12f)).Mask(SpawnPlane.Mask_Highground)); // side wall neon sign
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(110656, -11683, 4640), new Angle(0.91f, 0.42f)).Mask(SpawnPlane.Mask_Highground)); // neon sign above main platform
            // drones
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(112099, -10310, 3694), new Angle(0.98f, 0.19f)).Mask(SpawnPlane.Mask_Airborne)); // between main platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(110613, -13545, 3823), new Angle(0.86f, 0.50f)).Mask(SpawnPlane.Mask_Airborne)); // before grapple
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(111939, -13163, 2148), new Angle(-1.00f, 0.05f)).Mask(SpawnPlane.Mask_Airborne)); // above first zipline
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(108940, -8915, 2544), new Angle(-0.63f, 0.78f)).Mask(SpawnPlane.Mask_Airborne)); // above 2nd small platform
            // turrets
            Rooms.Add(layout);


            //// Room 2 ////
            enemies = HC_room2.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[1] = new EnemyDrone(enemies[1]);
            // orb
            layout = new RoomLayout(enemies[0]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(110577, -27017, 4966), new Vector3f(107789, -27023, 4953))); // 2nd platform ledge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(107455, -22903, 4957), new Vector3f(110378, -22923, 4901))); // 1st platform ledge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(107743, -22455, 5386), new Vector3f(107952, -24103, 6063))); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(108940, -23923, 5327))); // floating mid platforms
            Rooms.Add(layout);

            layout = new RoomLayout(enemies.Skip(1).ToList());
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(110649, -26824, 4768), new Vector3f(107757, -26444, 4768), new Angle(0.71f, 0.71f)).Mask(SpawnPlane.Mask_Highground).SetMaxEnemies(3));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(110380, -22682, 4768), new Vector3f(107863, -22286, 4768), new Angle(0.68f, 0.73f)).Mask(SpawnPlane.Mask_Highground).SetMaxEnemies(3));
            // high special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(109846, -21016, 5311), new Angle(0.80f, 0.61f)).Mask(SpawnPlane.Mask_Highground)); // right wall sign
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(107643, -26949, 5315), new Angle(0.55f, 0.84f)).Mask(SpawnPlane.Mask_Highground)); // small sign, left 2nd platform
            // drones
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(108281, -25578, 5007), new Vector3f(110159, -23762, 5157), new Angle(0.72f, 0.70f)).Mask(SpawnPlane.Mask_Airborne)); // between platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(109795, -31721, 4694), new Vector3f(108079, -29630, 5179), new Angle(0.72f, 0.69f)).Mask(SpawnPlane.Mask_Airborne)); // default rng
            Rooms.Add(layout);


            //// Room 3 ////
            enemies = HC_room3.ReturnEnemiesInRoom(AllEnemies);
            enemies[1].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies.ForEach(x => x.DisableAttachedCP(GameHook.game));
            EnemiesWithoutCP.AddRange(enemies);
           

            //// Room 4 ////
            enemies = HC_room4.ReturnEnemiesInRoom(AllEnemies);
            enemies[1].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Waver);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(99751, -30417, 1488), new Vector3f(100967, -29858, 1488), new Angle(-0.02f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(93718, -36504, -1), new Vector3f(92862, -35428, -1), new Angle(0.70f, 0.72f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(100963, -30706, 1488), new Angle(0.71f, 0.71f)).Mask(SpawnPlane.Mask_Highground)); // corner, 1st platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(92310, -35883, -1), new Angle(0.28f, 0.96f)).Mask(SpawnPlane.Mask_Highground)); // corner, 2nd platform
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(98921, -30891, 2108), new Angle(0.13f, 0.99f)).Mask(SpawnPlane.Mask_Highground)); // antenna
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(99046, -29570, 1888), new Angle(-0.08f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // city light
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(98199, -30451, 1552), new Angle(0.02f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // jumping ledge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(93978, -36147, 498), new Angle(0.70f, 0.72f)).Mask(SpawnPlane.Mask_Highground)); // fence 2nd platform

            Rooms.Add(layout);


            //// Room 5 ////
            enemies = HC_room5.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[1].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Weeb);
            DynamicRoomLayout dynamicLayout = new DynamicRoomLayout();
            // dynamic room! between default and next/higher platform on left
            layout = new RoomLayout(enemies);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(94362, -40892, 1798), new Vector3f(93520, -41433, 1798), new Angle(-1.00f, 0.03f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(93189, -41033, 1798), new Vector3f(92634, -42140, 1813), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(93227, -44485, 1798), new Vector3f(92392, -44854, 1798), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(92883, -45711, 2516), new Angle(0.71f, 0.71f)).Mask(SpawnPlane.Mask_Highground)); // city light
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(93169, -43305, 1997)).Mask(SpawnPlane.Mask_ShieldOrb)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(95295, -41172, 1997)).Mask(SpawnPlane.Mask_ShieldOrb)); // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(91886, -44391, 2055)).Mask(SpawnPlane.Mask_ShieldOrb)); // platform corner
            // default CP
            layout.ModifyAttachedCP(new DeepPointer(PtrDB.DP_Dharma_HC_Room5_CP), new Vector3f(92920, -44920, 1798), new Angle(1.00f, 0.00f));
            dynamicLayout.AddRoom(layout);

            layout = new RoomLayout(enemies);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(88122, -46530, 3413), new Vector3f(87623, -44371, 3403), new Angle(0.01f, 1.00f)).Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(3));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(87437, -44642, 3757), new Angle(-0.24f, 0.97f)).Mask(SpawnPlane.Mask_Highground)); // city light
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(88123, -47329, 3518), new Angle(0.72f, 0.69f)).Mask(SpawnPlane.Mask_Highground)); // barrels in corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(87562, -45403, 3828)).Mask(SpawnPlane.Mask_ShieldOrb)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(86776, -46961, 3512)).Mask(SpawnPlane.Mask_ShieldOrb)); // around corner
            // new platform cp
            layout.ModifyAttachedCP(new DeepPointer(PtrDB.DP_Dharma_HC_Room5_CP), new Vector3f(87810, -46453, 3398), new Angle(-0.72f, 0.70f));
            dynamicLayout.AddRoom(layout);
            DynamicRooms.Add(dynamicLayout);



            //// Room 6 ////
            enemies = HC_room6.ReturnEnemiesInRoom(AllEnemies);
            enemies[0].SetEnemyType(Enemy.EnemyTypes.Splitter);
            enemies[1].SetEnemyType(Enemy.EnemyTypes.Splitter);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(90366, -57118, 4090), new Vector3f(89771, -58066, 4088), new Angle(0.70f, 0.72f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(89014, -58594, 4089), new Vector3f(88358, -57206, 4103), new Angle(0.70f, 0.72f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(86225, -54473, 4090), new Vector3f(85078, -53472, 4090), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Flatground)
                .AllowSplitter().SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(96041, -50125, 5296), new Vector3f(94924, -49297, 5288), new Angle(-1.00f, 0.03f)).Mask(SpawnPlane.Mask_Flatground)
                .AllowSplitter().SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(96215, -53245, 5288), new Vector3f(94859, -51972, 5322), new Angle(0.72f, 0.69f)).Mask(SpawnPlane.Mask_Flatground)
                .AllowSplitter().SetMaxEnemies(2));
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(93694, -57068, 5591), new Vector3f(93719, -57976, 5600), new Angle(0.22f, 0.97f)).Mask(SpawnPlane.Mask_Highground)
                .AsVerticalPlane());  // slide ledge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(90521, -58219, 5020), new Angle(0.81f, 0.59f)).Mask(SpawnPlane.Mask_Highground)); // wall beam, above exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(89782, -59492, 4398), new Angle(0.69f, 0.72f)).Mask(SpawnPlane.Mask_Highground)); // small rooftop near exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(88162, -56676, 4915), new Vector3f(88169, -55258, 4915), new Angle(0.60f, 0.80f)).Mask(SpawnPlane.Mask_Highground)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(90826, -50910, 4090), new Angle(-0.62f, 0.79f)).Mask(SpawnPlane.Mask_Highground)); // near shuriken uplink, around corner

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(94556, -48929, 5647), new Angle(-0.95f, 0.31f)).Mask(SpawnPlane.Mask_Highground)); // higher platform city ad light
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(96381, -49392, 5688), new Angle(-0.98f, 0.22f)).Mask(SpawnPlane.Mask_Highground)); // higher platform, fence
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(95929, -53557, 5843), new Angle(0.88f, 0.48f)).Mask(SpawnPlane.Mask_Highground)); // zipline pole
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(92537, -55209, 5662), new Angle(1.00f, 0.04f)).Mask(SpawnPlane.Mask_Highground)); // billboard in middle, high spot
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(90602, -52405, 5219), new Angle(0.92f, 0.39f)).Mask(SpawnPlane.Mask_Highground)); // umbrella, middle
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(86237, -52867, 4994), new Angle(-0.03f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // fuel tank, left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(86291, -59066, 5003), new Angle(0.25f, 0.97f)).Mask(SpawnPlane.Mask_Highground)); // billboard far left
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(88822, -58116, 4763), new Angle(0.71f, 0.71f)).Mask(SpawnPlane.Mask_Airborne)); // exit platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(95799, -49429, 5820), new Vector3f(94921, -52419, 5820), new Angle(1.00f, 0.03f)).Mask(SpawnPlane.Mask_Airborne)); // right platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(88898, -55988, 4347), new Angle(0.73f, 0.68f)).Mask(SpawnPlane.Mask_Airborne)); // middle red billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(87463, -57836, 4354), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Airborne)); // exit platform, around corner
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(90014, -57014, 4103), new Angle(-0.88f, 0.48f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 40, VerticalAngle = 0 })); // exit door aiming towards exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(94694, -48879, 5288), new Angle(-0.48f, 0.88f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 40, HorizontalSpeed = 35, VerticalAngle = 0 })); // right platform, behind ads
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(91104, -52280, 4090), new Angle(0.15f, 0.99f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 1, HorizontalSpeed = 1, VerticalAngle = 10 , VisibleLaserLength = 0})); // ninja, middle platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(86877, -57226, 4292), new QuaternionAngle(0.71f, 0.00f, 0.00f, 0.71f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 5, HorizontalSpeed = 10, VerticalAngle = 20 })); // on 4 pillars, aim towards exit
            Rooms.Add(layout);


            //// Room 7 ////
            enemies = HC_room7.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            // orb
            layout = new RoomLayout(enemies[0]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(88276, -71153, 4824), new Vector3f(86435, -71262, 5579))); // right side of billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(84882, -72858, 5558))); // above small building
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(84388, -71418, 5404))); // antenna
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(84406, -72603, 4700))); // platform corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(85109, -68849, 5242))); // left billboard
            Rooms.Add(layout);
            // enemies
            layout = new RoomLayout(enemies.Skip(1).ToList());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(89888, -72549, 4700), new Vector3f(88517, -71676, 4700), new Angle(0.70f, 0.72f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter().SetMaxEnemies(3));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(85899, -71317, 4700), new Vector3f(84497, -72412, 4700), new Angle(0.02f, 1.00f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter().SetMaxEnemies(3));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(85238, -73015, 5154), new Angle(0.15f, 0.99f)).Mask(SpawnPlane.Mask_Highground)); // small building side
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(89947, -71458, 5607), new Angle(0.80f, 0.60f)).Mask(SpawnPlane.Mask_Highground)); // fuel tank
            Rooms.Add(layout);


            //// Room 8 ////
            enemies = HC_room8.ReturnEnemiesInRoom(AllEnemies);

            var info = new ShifterSpawnInfo() {
                shiftPoints = new List<Tuple<Vector3f, Angle>>() {
                    new Tuple<Vector3f, Angle>(new Vector3f(80313, -86092, 6295), new Angle(0.77f, 0.64f)), // right red neon sign
                    new Tuple<Vector3f, Angle>(new Vector3f(81045, -80849, 6118), new Angle(-0.94f, 0.34f)), // BBW rooftop
                    new Tuple<Vector3f, Angle>(new Vector3f(77000, -86333, 4572), new Angle(0.52f, 0.85f)), // fence
                    new Tuple<Vector3f, Angle>(new Vector3f(75501, -77647, 5590), new Angle(-0.50f, 0.86f)), // flashing white neon sign
                    new Tuple<Vector3f, Angle>(new Vector3f(79641, -82095, 5101), new Angle(-1.00f, 0.04f)), // first platform
                    new Tuple<Vector3f, Angle>(new Vector3f(75583, -84654, 4024), new Angle(0.38f, 0.92f)), // lower platfrom near exit
                }
            };

            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Splitter);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Splitter);
            enemies[4] = new EnemyShifter(enemies[4]).AddFixedSpawnInfo(info); // 6 shift points
            enemies[6].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies[7].SetEnemyType(Enemy.EnemyTypes.Weeb);

            // orb
            layout = new RoomLayout(enemies[0]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74391, -79574, 5147))); // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74616, -82170, 5157))); // near uplink billboard, floating
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(77408, -84891, 5970), new Vector3f(77372, -81325, 5970))); // along the zipline
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(79751, -86266, 5223))); // near antenna, red sign
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(75822, -85459, 4532))); // lower far platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(79460, -83317, 4151))); // floating low
            Rooms.Add(layout);

            // enemies
            layout = new RoomLayout(enemies.Skip(1).ToList());
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(80967, -81845, 5098), new Vector3f(80017, -80446, 5105), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(76906, -81331, 4039), new Vector3f(78004, -80235, 4039), new Angle(0.01f, 1.00f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74964, -81166, 5069), new Vector3f(73962, -79924, 5070), new Angle(-0.26f, 0.97f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74949, -83452, 5109), new Vector3f(74018, -85437, 5113), new Angle(0.39f, 0.92f)).Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(76385, -84782, 4039), new Vector3f(77956, -85824, 4042), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(79939, -85034, 5066), new Vector3f(80819, -85734, 5066), new Angle(0.74f, 0.68f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter().SetMaxEnemies(2));
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(77836, -86067, 4633), new Angle(0.53f, 0.85f)).Mask(SpawnPlane.Mask_Highground)); // antenna
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74953, -85773, 5660), new Angle(0.14f, 0.99f)).Mask(SpawnPlane.Mask_Highground)); // zipline pole
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(73613, -80140, 5301), new Angle(-0.05f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // ac stack
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(73718, -79682, 5974), new Angle(-0.27f, 0.96f)).Mask(SpawnPlane.Mask_Highground)); // fuel tank
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(75121, -79702, 5490), new Angle(-0.19f, 0.98f)).Mask(SpawnPlane.Mask_Highground)); // neon sign
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(73528, -84008, 6176), new Angle(0.20f, 0.98f)).Mask(SpawnPlane.Mask_Highground)); // big neon sign, near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(79370, -81200, 4945), new Angle(0.55f, 0.83f)).Mask(SpawnPlane.Mask_Highground)); // fuel tank at start
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(80836, -81138, 5553), new Angle(0.81f, 0.59f)).Mask(SpawnPlane.Mask_Highground)); // BBQ rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(76601, -83012, 4914), new Angle(0.31f, 0.95f)).Mask(SpawnPlane.Mask_Highground)); // spinning sign middle
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(75633, -79519, 5475), new Vector3f(78813, -80030, 5700), new Angle(-0.02f, 1.00f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(79281, -81404, 5420), new Vector3f(78190, -85287, 5868), new Angle(0.69f, 0.73f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(75470, -84883, 5378), new Vector3f(76913, -81560, 5759), new Angle(0.26f, 0.97f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(81321, -81120, 5548), new Angle(-0.90f, 0.45f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 1, HorizontalSpeed = 1, VerticalAngle = 0, VisibleLaserLength = 0})); // ninja around BBQ

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(81164, -85467, 5066), new Angle(0.99f, 0.17f)).Mask(SpawnPlane.Mask_Turret)
                 .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 30, VerticalAngle = 0 })); // right platform

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(78911, -80429, 4039), new Angle(-0.98f, 0.18f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 25, VerticalAngle = 30 })); // left bot platform, aiming towards shurikens or middle
            Rooms.Add(layout);


            //// Room 9 ////
            enemies = HC_room9.ReturnEnemiesInRoom(AllEnemies);

            List<Tuple<Vector3f, Angle>> ShiftPoints = new List<Tuple<Vector3f, Angle>>() {
                new Tuple<Vector3f, Angle>(new Vector3f(53700, -78048, 4440), new Angle(0.71f, 0.71f)), // extended neon sign
                new Tuple<Vector3f, Angle>(new Vector3f(53688, -79038, 4481), new Angle(-0.04f, 1.00f)), // slide ledge
                new Tuple<Vector3f, Angle>(new Vector3f(48226, -79190, 4606), new Angle(0.11f, 0.99f)), // fuel tank
                new Tuple<Vector3f, Angle>(new Vector3f(51168, -78370, 3708), new Angle(0.49f, 0.87f)), // platform ledge
                new Tuple<Vector3f, Angle>(new Vector3f(50958, -76976, 4426), new Angle(0.07f, 1.00f)), // billboard

                new Tuple<Vector3f, Angle>(new Vector3f(52173, -77973, 3618), new Angle(0.45f, 0.89f)), // fence
                new Tuple<Vector3f, Angle>(new Vector3f(51575, -76201, 2831), new Angle(-0.15f, 0.99f)), // middle platform ledge
                new Tuple<Vector3f, Angle>(new Vector3f(52624, -75131, 3718), new Angle(-0.75f, 0.66f)), // platform ledge exit door
                new Tuple<Vector3f, Angle>(new Vector3f(53984, -75047, 3718), new Angle(-0.84f, 0.55f)), // platform ledge near exit
                new Tuple<Vector3f, Angle>(new Vector3f(52021, -77679, 2828), new Angle(0.29f, 0.96f)), // middle platform ledge

                new Tuple<Vector3f, Angle>(new Vector3f(49566, -74254, 4276), new Angle(-0.19f, 0.98f)), // small rooftop
                new Tuple<Vector3f, Angle>(new Vector3f(50526, -75059, 3717), new Angle(-0.39f, 0.92f)), // platform ledge
                new Tuple<Vector3f, Angle>(new Vector3f(50938, -79492, 3708), new Angle(0.42f, 0.91f)), // plaatform ledge right
                new Tuple<Vector3f, Angle>(new Vector3f(52642, -78703, 4100), new Angle(0.65f, 0.76f)), // slide middle
                new Tuple<Vector3f, Angle>(new Vector3f(51371, -80035, 2818), new Angle(0.61f, 0.79f)), // neaar limbs

                new Tuple<Vector3f, Angle>(new Vector3f(48846, -78949, 3698), new Angle(0.02f, 1.00f)), // near fuel tank
                new Tuple<Vector3f, Angle>(new Vector3f(49256, -74274, 3698), new Angle(-0.28f, 0.96f)), // under small rooftop
                new Tuple<Vector3f, Angle>(new Vector3f(49895, -75166, 3718), new Angle(-0.25f, 0.97f)), // near rooftop
                new Tuple<Vector3f, Angle>(new Vector3f(48896, -74494, 4519), new Angle(-0.20f, 0.98f)), // fence near small rooftop
                new Tuple<Vector3f, Angle>(new Vector3f(52153, -80020, 3617), new Angle(0.85f, 0.52f)), // fence near limbs

                new Tuple<Vector3f, Angle>(new Vector3f(51850, -80091, 3317), new Angle(0.72f, 0.69f)), // zipline pole
                new Tuple<Vector3f, Angle>(new Vector3f(50277, -75277, 3431), new Angle(-0.23f, 0.97f)), // generator
                new Tuple<Vector3f, Angle>(new Vector3f(49596, -76675, 4171), new Angle(0.82f, 0.57f)), // generator at the back
                new Tuple<Vector3f, Angle>(new Vector3f(53396, -78614, 3702), new Angle(0.80f, 0.61f)), // pipes under slide
                new Tuple<Vector3f, Angle>(new Vector3f(51052, -78130, 2828), new Angle(0.36f, 0.93f)), // middle platform
            };

            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[1] = new EnemyShifter(enemies[1]).AddFixedSpawnInfo(GetRandomShiftPoints(ref ShiftPoints)); // 5 shift points
            enemies[2] = new EnemyShifter(enemies[2]).AddFixedSpawnInfo(GetRandomShiftPoints(ref ShiftPoints)); // 5 shift points
            enemies[3] = new EnemyShifter(enemies[3]).AddFixedSpawnInfo(GetRandomShiftPoints(ref ShiftPoints)); // 5 shift points
            enemies[4] = new EnemyShifter(enemies[4]).AddFixedSpawnInfo(GetRandomShiftPoints(ref ShiftPoints)); // 5 shift points

            layout = new RoomLayout(enemies[0]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51138, -77767, 2921), new Vector3f(51071, -76515, 3611))); // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(52454, -74306, 3753), new Vector3f(50708, -74302, 4585))); // billboard near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50063, -80712, 3458))); // floating near zipline
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53725, -77836, 4565))); // slide neon sign, edge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51600, -75132, 3704))); // floating in middle
            Rooms.Add(layout);

            layout = new RoomLayout(enemies.Skip(1).ToList());
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50937, -79400, 3709), new Vector3f(49501, -78526, 3708), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51963, -78577, 2818), new Vector3f(51124, -77759, 2828), new Angle(0.27f, 0.96f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51173, -75775, 2828), new Vector3f(52079, -74609, 2818), new Angle(-0.33f, 0.94f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(52887, -74949, 3717), new Vector3f(53943, -74500, 3706), new Angle(-0.49f, 0.87f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49308, -75064, 3718), new Vector3f(49980, -74138, 3698), new Angle(-0.19f, 0.98f)).Mask(SpawnPlane.Mask_Flatground));
            Rooms.Add(layout);


            //// EXTRA ////
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(91464, -61235, 4116), new Angle(-0.82f, 0.57f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 1, HorizontalSpeed = 1, VisibleLaserLength = 0 })); // ninja after metro

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(72249, -84881, 5108), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // after door of room 8

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61077, -85506, 3998), new Vector3f(59678, -84137, 3998), new Angle(-0.03f, 1.00f))
                .Mask(SpawnPlane.Mask_Highground).SetMaxEnemies(3)); // after laser maze
            Rooms.Add(layout);

        }

        private ShifterSpawnInfo GetRandomShiftPoints(ref List<Tuple<Vector3f, Angle>> list) {
            List<Tuple<Vector3f, Angle>> points = new List<Tuple<Vector3f, Angle>>();
            for(int i = 0; i < 5; i++) {
                int r = Config.GetInstance().r.Next(list.Count);
                points.Add(list[r]);
                list.RemoveAt(r);
            }
            return new ShifterSpawnInfo() {shiftPoints = points };
        }

        public void Gen_Nightmare() {
            throw new NotImplementedException();
        }

        protected override void Gen_PerRoom() {}
    }
}