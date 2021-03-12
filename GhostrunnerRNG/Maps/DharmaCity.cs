using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using System.Collections.Generic;
using System.Linq;

namespace GhostrunnerRNG.Maps {
    class DharmaCity : MapCore{

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

        public DharmaCity(bool isHC) : base(GameUtils.MapType.DharmaCity) {
            if(!isHC) {
                Gen_PerRoom();
            } else {
                // hardcore
            }
        }
        
        protected override void Gen_PerRoom() {
            List<Enemy> AllEnemies = GetAllEnemies(MainWindow.game, 0, 27);
            AllEnemies.AddRange(GetAllEnemies(MainWindow.game, 32, 7));

            Rooms = new List<RoomLayout>();
            RoomLayout layout;
            List<Enemy> enemies;
            List<Enemy> drones = new List<Enemy>();


            ///// First room - enemies /////
            enemies = room_1.ReturnEnemiesInRoom(AllEnemies);
            enemies.ForEach(x => x.DisableAttachedCP(MainWindow.game)); // disable cp for all enemies in room1
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
            enemies.ForEach(x => x.DisableAttachedCP(MainWindow.game)); // disable enemy checkpoint
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
            EnemyShieldOrb shieldOrb1 = new EnemyShieldOrb(enemies[0], new DeepPointer(0x045A3C20, 0x98, 0x18, 0x128, 0xA8, 0x860, 0x130, 0x1D0));
            shieldOrb1.HideBeam_Range(0x18, 0x200, 0x8, 0x20);
            shieldOrb1.HideBeam_Range(0x18, 0x208, 0x8, 0x20);
            shieldOrb1.LinkObject(new DeepPointer(0x045A3C20, 0x98, 0x18, 0x128, 0xA8, 0x200, 0x220));
            shieldOrb1.LinkObject(new DeepPointer(0x045A3C20, 0x98, 0x18, 0x128, 0xA8, 0x208, 0x220));

            // RNG orb?
            if(Config.GetInstance().Gen_RngOrbs) {
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
            }

            // Enemies in that area
            enemies = room_6_protectedEnemies.ReturnEnemiesInRoom(AllEnemies);
            enemies.AddRange(room_6_restEnemies.ReturnEnemiesInRoom(AllEnemies));
            layout = new RoomLayout(enemies);
            // default platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(90420, -58150, 4103), new Vector3f(88460, -57080, 4093), new Angle(0.71f, 0.71f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(96110, -53358, 5288), new Vector3f(94905, -52082, 5310), new Angle(0.89f, 0.45f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(96155, -50082, 5288), new Vector3f(94760, -48905, 5289), new Angle(-0.88f, 0.48f)));
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
                .AddPatrolPoint(new DeepPointer(0x045A3C20, 0x98, 0x28, 0x128, 0xA8, 0x238, 0x130, 0x1D0), new Vector3f(78996, -81310, 5364)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74338, -85272, 5570))
                .AddPatrolPoint(new DeepPointer(0x045A3C20, 0x98, 0x28, 0x128, 0xA8, 0x238, 0x130, 0x1D0), new Vector3f(74371, -80265, 5570)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74749, -81993, 5293))
               .AddPatrolPoint(new DeepPointer(0x045A3C20, 0x98, 0x28, 0x128, 0xA8, 0x238, 0x130, 0x1D0), new Vector3f(79130, -81986, 5293)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(77323, -85030, 5763))
               .AddPatrolPoint(new DeepPointer(0x045A3C20, 0x98, 0x28, 0x128, 0xA8, 0x238, 0x130, 0x1D0), new Vector3f(77368, -81590, 5763)));
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
            enemies.ForEach(e => e.DisableAttachedCP(MainWindow.game));
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
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53422, -78650, 3702), new Angle(0.76f, 0.65f))); // pipes under slide
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
        }
    }
}