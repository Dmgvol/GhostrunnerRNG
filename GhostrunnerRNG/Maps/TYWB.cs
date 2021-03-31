using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.NonPlaceableObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GhostrunnerRNG.Maps {
    class TYWB : MapCore {
        #region rooms
        private Room room_1 = new Room(new Vector3f(70795, -43279, 6772), new Vector3f(61421, -21990, 938));    // 14 spiders
        private Room room_2 = new Room(new Vector3f(61331, -20572, 6096), new Vector3f(71374, -1627, -381));    // 2 drones
        private Room room_3 = new Room(new Vector3f(69433, 4374, 6310), new Vector3f(61294, 12328, 1595));      // 2 pistols, 4 uzi
        private Room room_4 = new Room(new Vector3f(59405, 11826, 7236), new Vector3f(73486, 39942, -171));     // 5 weebs
        private Room room_5 = new Room(new Vector3f(59048, 38982, 9204), new Vector3f(73444, 49264, 1488));     // 2 balls, 1 pistol,  3 weebs, 3 geckos
        private Room room_6 = new Room(new Vector3f(75760, 47436, 9914), new Vector3f(55076, 68619, 2140));     // 1 splitter
        private Room room_7 = new Room(new Vector3f(56167, 67465, 12842), new Vector3f(74802, 82807, 1691));    // 1 frogger, 2 splitter, 1 weeb
        private Room room_8 = new Room(new Vector3f(76013, 82373, 12810), new Vector3f(55676, 94205, -1143));   // 2 froggers, 2 splitters, 2 shielders
        private Room room_9 = new Room(new Vector3f(58722, 106611, 6877), new Vector3f(72511, 116787, 1296));   // 2 shifters, 2 pistols, 4 drones
        private Room room_10 = new Room(new Vector3f(60555, 134146, 6617), new Vector3f(75583, 141394, 1517));  // 4 uzi, 2 drones
        #endregion

        public TYWB(bool isHc) : base(GameUtils.MapType.TYWB) {
            if(!isHc) {
                Gen_PerRoom();
            }
        }
        protected override void Gen_PerRoom() {
            // static enemy gap
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game, 0, 29);
            AllEnemies.AddRange(GetAllEnemies(GameHook.game, 39, 30));
            // dynamic gap: bulk search and filter by exact locations
            var lst = GetAllEnemies_Bulk(29, 40, GameHook.game, new List<Vector3f>() {
                // room 6
                new Vector3f(68825, 63495, 5021),
                // room7
                new Vector3f(67404, 75685, 3953),
                new Vector3f(63825, 75685, 3944),
                // room 8
                new Vector3f(66812, 89768, 3949),
                new Vector3f(64799, 89768, 3949),
            });
            
            AllEnemies.AddRange(lst);

            Rooms = new List<RoomLayout>();
            RoomLayout layout;
            List<Enemy> enemies;

            //// Room 1 - 14 spiders //// (minimal rng)
            enemies = room_1.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies.Take(4).ToList());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65400, -23731, 2160), new Angle(-0.70f, 0.71f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66178, -23695, 2160), new Angle(-0.70f, 0.71f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64354, -28565, 3179), new Angle(0.01f, 1.00f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64348, -28580, 2822), new Angle(0.02f, 1.00f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66274, -33032, 1952), new Angle(1.00f, 0.01f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65322, -33018, 1952), new Angle(-1.00f, 0.04f)));
            layout.DoNotReuse();
            Rooms.Add(layout);

            //// Room 2 - 2 drones ////
            enemies = room_2.ReturnEnemiesInRoom(AllEnemies);
            enemies[0].SetEnemyType(Enemy.EnemyTypes.Drone);
            enemies[1].SetEnemyType(Enemy.EnemyTypes.Drone);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, removeCP: false); // take 1 drone
            layout = new RoomLayout(enemies[0]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66522, -11587, 3549), new Vector3f(65188, -11587, 3549), new Angle(-0.71f, 0.71f)).Mask(SpawnPlane.Mask_Airborne)); // 2nd drone, default range
            Rooms.Add(layout);

            //// Room 3 - 2 pistols, 4 uzi////
            enemies = room_3.ReturnEnemiesInRoom(AllEnemies);
            TakeLastEnemyFromCP(ref enemies, force: false, removeCP: true, attachedDoor: true); // take one random
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63368, 10689, 2852), new Vector3f(63746, 8180, 2852), new Angle(-0.69f, 0.72f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67666, 10637, 2852), new Vector3f(67921, 8032, 2852), new Angle(-0.72f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66933, 10816, 2852), new Vector3f(64611, 8085, 2852), new Angle(-0.71f, 0.70f)).SetMaxEnemies(2).Mask(SpawnPlane.Mask_Flatground));
            // special/high spawns
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68146, 10111, 3163), new Angle(-0.82f, 0.58f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // small lab tube, left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68134, 9023, 3314), new Angle(-0.83f, 0.56f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // server, left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64340, 8292, 3133), new Angle(-0.49f, 0.87f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // crate, middle
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63282, 7407, 3222), new Angle(-0.33f, 0.94f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // wall pipes, right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68179, 7446, 2932), new Angle(-0.93f, 0.36f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // wall pipes, left

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67535, 9682, 3486), new Angle(-0.56f, 0.83f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // bottom ledge billboard, left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67144, 8101, 3486), new Vector3f(67144, 9953, 3486), new Angle(-0.80f, 0.60f)).AsVerticalPlane().Mask(SpawnPlane.Mask_HighgroundLimited)); //bottom ledge billboard, middle
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64285, 8137, 3486), new Vector3f(64285, 9864, 3486), new Angle(-0.53f, 0.85f)).AsVerticalPlane().Mask(SpawnPlane.Mask_HighgroundLimited)); // bottom billboard ledge, middle 2

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63335, 11003, 4851), new Angle(-0.26f, 0.97f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // corner billboard, high 1
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68102, 10978, 4851), new Angle(-0.97f, 0.26f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // corner billboard, high 2

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64466, 8805, 4981), new Vector3f(66659, 8812, 4981), new Angle(-0.70f, 0.71f)).AsVerticalPlane().setRarity(0.2).Mask(SpawnPlane.Mask_HighgroundLimited)); // hovering pipes, middle
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66072, 6919, 4604), new Angle(0.70f, 0.71f)).setRarity(0.2).Mask(SpawnPlane.Mask_HighgroundLimited)); // on top of rectangle block, front of spawn

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68001, 6568, 4230), new Angle(-1.00f, 0.09f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // left billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63400, 6666, 4230), new Angle(0.30f, 0.96f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // right billboard

            // EXTRA - DRONE
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64832, 8302, 3797), new Vector3f(66469, 10239, 4141), new Angle(-0.68f, 0.73f)).Mask(SpawnPlane.Mask_Airborne));

            Rooms.Add(layout);


            // Room 4 - 5 weebs////
            enemies = room_4.ReturnEnemiesInRoom(AllEnemies); // no room for rng, take 2 
            enemies[0].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies[1].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies[4].SetEnemyType(Enemy.EnemyTypes.Weeb);
            TakeLastEnemyFromCP(ref enemies, force: true, attachedDoor: true, removeCP:true); // take 2 weebs, by force
            TakeLastEnemyFromCP(ref enemies, force: true, attachedDoor: true, removeCP: true);

            // Room 5 - 2 balls, 1 pistol,  3 weebs, 3 geckos////
            enemies = room_5.ReturnEnemiesInRoom(AllEnemies);
            // left shield orb
            EnemyShieldOrb orb = new EnemyShieldOrb(enemies[0]);
            orb.HideBeam_Range(0, 4);
            orb.HideBeam_Range(1, 4);
            orb.HideBeam_Range(2, 5);
            orb.LinkObject(3);
            enemies[0] = orb;

            // right shield orb
            EnemyShieldOrb orb2 = new EnemyShieldOrb(enemies[1]);
            orb2.HideBeam_Range(0, 4);
            orb2.HideBeam_Range(1, 4);
            orb2.HideBeam_Range(2, 5);
            orb2.LinkObject(3);
            enemies[1] = orb2;

            // wavers
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[4].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[5].SetEnemyType(Enemy.EnemyTypes.Waver);
            // weebs
            enemies[6].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies[7].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies[8].SetEnemyType(Enemy.EnemyTypes.Weeb);

            // shield orb planes
            layout = new RoomLayout(enemies.Take(2).ToList());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67292, 45503, 4549))); // default left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63853, 45484, 4549))); // default right

            // left side of room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69258, 43143, 4354), new Vector3f(69936, 44198, 5422)).AsVerticalPlane()); //billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69660, 47512, 4527))); // wall ac unit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68420, 42521, 3373))); // lab tube
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68981, 45078, 3493), new Vector3f(65538, 45015, 4339)).AsVerticalPlane()); // hovering between platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69902, 45260, 4889))); // behind billboard

            // middle of room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66786, 46092, 4889), new Vector3f(64336, 46049, 4006)).AsVerticalPlane()); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66474, 46885, 3406))); // below platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64584, 46887, 3406))); // below platform 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65553, 47528, 4578)));  // above  exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65578, 44501, 3292))); // under bridge/arc

            // right side of room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65538, 45015, 4339), new Vector3f(62325, 44978, 3361)).AsVerticalPlane()); // hovering between platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62354, 45106, 4534), new Vector3f(61513, 44310, 5508)).AsVerticalPlane()); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62896, 42475, 3373))); // lab tube
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(60768, 41719, 3850), new Vector3f(60830, 46939, 3850)).AsVerticalPlane()); // right wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(60953, 46870, 4918))); // wall corner pipe

            // special/high places
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68879, 46487, 6407)).setRarity(0.2)); // floating lamp - rare
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65476, 44550, 6433)).setRarity(0.15)); // floating lamp, hard to reach - rare
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62862, 42841, 6790)).setRarity(0.15)); // corner tube, top of lab tube - rare

            layout.Mask(SpawnPlane.Mask_ShieldOrb);
            layout.DoNotReuse();
            Rooms.Add(layout);

            // enemies planes
            layout = new RoomLayout(enemies.Skip(2).ToList());
            // default platforms
            var mask = SpawnPlane.Mask_Flatground;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(70149, 43363, 2832), new Vector3f(69346, 47224, 2832), new Angle(-0.70f, 0.71f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61885, 47541, 2832), new Vector3f(60957, 43342, 2832), new Angle(-0.72f, 0.70f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61984, 43161, 2832), new Vector3f(63385, 43893, 2832), new Angle(-0.73f, 0.69f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67835, 43968, 2832), new Vector3f(69182, 43290, 2832), new Angle(-0.73f, 0.69f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68377, 44009, 2832), new Vector3f(66810, 44836, 2832), new Angle(-0.73f, 0.68f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63087, 43957, 2832), new Vector3f(64317, 44939, 2832), new Angle(-0.71f, 0.71f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64310, 43996, 2832), new Vector3f(66809, 43113, 2832), new Angle(-0.72f, 0.70f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66559, 44951, 2832), new Vector3f(64664, 45999, 2832), new Angle(-0.69f, 0.73f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64053, 45948, 2832), new Vector3f(63015, 46952, 2832), new Angle(-0.70f, 0.72f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67192, 46993, 2832), new Vector3f(68301, 46124, 2832), new Angle(-0.72f, 0.70f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66761, 47450, 2832), new Vector3f(64279, 47017, 2832), new Angle(-0.67f, 0.74f)).Mask(mask));
            // default raised platforms
            mask = SpawnPlane.Mask_Highground;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68913, 46484, 3952), new Angle(-0.87f, 0.49f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68907, 44492, 3952), new Angle(-0.91f, 0.42f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62471, 44472, 3952), new Angle(-0.44f, 0.90f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62479, 46489, 3952), new Angle(-0.48f, 0.88f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63389, 43624, 4452), new Vector3f(64233, 43366, 4452), new Angle(-0.43f, 0.90f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66858, 43369, 4452), new Vector3f(67733, 43607, 4452), new Angle(-0.91f, 0.42f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67750, 45587, 4452), new Vector3f(66862, 45403, 4452), new Angle(-0.74f, 0.67f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63411, 45310, 4448), new Vector3f(64316, 45591, 4452), new Angle(-0.73f, 0.69f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67391, 47318, 3922), new Vector3f(63678, 46668, 3922), new Angle(-0.70f, 0.71f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66425, 44614, 4452), new Vector3f(64723, 44461, 4452), new Angle(-0.64f, 0.77f)).Mask(mask));

            // high/special spots
            mask = SpawnPlane.Mask_HighgroundLimited;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65772, 44192, 5170), new Angle(-0.72f, 0.70f)).Mask(mask)); // middle billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67841, 42643, 5210), new Angle(-0.99f, 0.13f)).Mask(mask)); // left billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69595, 43125, 5385), new Angle(1.00f, 0.01f)).Mask(mask)); // left billboard 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69321, 45208, 5635), new Angle(-0.97f, 0.25f)).Mask(mask)); // left billboard 3
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65516, 46336, 5130), new Angle(-0.71f, 0.70f)).Mask(mask)); // middle billboard, back
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62052, 45179, 5635), new Angle(-0.27f, 0.96f)).Mask(mask)); // right billboard 3
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61336, 43937, 5385), new Angle(-0.17f, 0.99f)).Mask(mask)); // right billboard 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63608, 42657, 5200), new Angle(0.15f, 0.99f)).Mask(mask)); // right billboard 1
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61963, 47490, 4528), new Angle(-0.59f, 0.81f)).Mask(SpawnPlane.Mask_Highground)); // right wall ac unit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(60974, 46120, 4918), new Angle(-0.07f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // right tube, behind billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61029, 45522, 5717), new Angle(0.01f, 1.00f)).setRarity(0.2).Mask(mask)); // tube with cloth, right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64697, 47568, 5227), new Angle(-0.70f, 0.71f)).Mask(mask)); // wall fan above exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69172, 47488, 4528), new Angle(-0.92f, 0.39f)).Mask(mask)); // left ac unit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65533, 46932, 6452), new Angle(-0.71f, 0.71f)).setRarity(0.15).Mask(mask)); // floating lamp, above exit door (high)


            // EXTRA - for drones
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67967, 45574, 5192), new Vector3f(64162, 45583, 5192), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_Airborne));

            Rooms.Add(layout);

            //// Room 6 - 1 splitter////
            enemies = room_6.ReturnEnemiesInRoom(AllEnemies);
            enemies[0].SetEnemyType(Enemy.EnemyTypes.Splitter);
            RandomPickEnemiesWithoutCP(ref enemies, force: false, removeCP: true); // chance to take splitter, no place for rng


            //// Room 7 - 1 frogger, 2 splitter, 1 weeb////
            enemies = room_7.ReturnEnemiesInRoom(AllEnemies);
            enemies[1].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Splitter);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Splitter);
            layout = new RoomLayout(enemies);
            
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68336, 76081, 3941), new Vector3f(66643, 75026, 3940), new Angle(0.72f, 0.70f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64564, 74898, 3928), new Vector3f(62614, 75881, 3930), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62838, 76295, 3930), new Vector3f(61888, 75615, 3930), new Angle(0.51f, 0.86f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68205, 73656, 5012), new Vector3f(69461, 72554, 5012), new Angle(0.92f, 0.38f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63136, 72209, 5008), new Vector3f(61937, 73050, 5015), new Angle(0.65f, 0.76f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65002, 80869, 3930), new Vector3f(66499, 79784, 3930), new Angle(-0.72f, 0.69f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter()); // before door

            // high/special spots
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68723, 76076, 4841), new Angle(0.97f, 0.23f)).Mask(SpawnPlane.Mask_Highground)); // fuel tank
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61894, 75138, 4842), new Angle(0.20f, 0.98f)).Mask(SpawnPlane.Mask_Highground)); // fuel tank 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61694, 73571, 5887), new Angle(0.17f, 0.99f)).Mask(SpawnPlane.Mask_Highground)); // fuel tank 3
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63165, 73819, 5008), new Angle(0.40f, 0.92f)).Mask(SpawnPlane.Mask_Highground)); // platform edge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68056, 73806, 5011), new Angle(0.85f, 0.53f)).Mask(SpawnPlane.Mask_Highground)); // platform edge - default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61524, 76488, 6663), new Angle(-0.24f, 0.97f)).Mask(SpawnPlane.Mask_Highground)); // platform edge, high
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63750, 76275, 4238), new Angle(0.41f, 0.91f)).Mask(SpawnPlane.Mask_Highground)); // lab tube

            // EXTRA - for drones
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65587, 76295, 4534), new Angle(-0.72f, 0.70f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67841, 76602, 4662), new Angle(-0.83f, 0.56f)).Mask(SpawnPlane.Mask_Airborne));

            Rooms.Add(layout);

            //// Room 8 - 2 froggers, 2 splitters, 2 shielders
            enemies = room_8.ReturnEnemiesInRoom(AllEnemies);
            enemies[4].SetEnemyType(Enemy.EnemyTypes.Splitter);
            enemies[5].SetEnemyType(Enemy.EnemyTypes.Splitter);
            layout = new RoomLayout(enemies);

            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67382, 91097, 5128), new Vector3f(68406, 92444, 5128), new Angle(-0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64317, 91004, 5128), new Vector3f(63096, 92437, 5128), new Angle(-0.54f, 0.84f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63362, 87388, 5128), new Vector3f(64257, 88532, 5128), new Angle(0.28f, 0.96f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68727, 87112, 5128), new Vector3f(67393, 88503, 5128), new Angle(0.95f, 0.31f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68495, 89228, 3929), new Vector3f(62866, 90511, 3929), new Angle(-0.70f, 0.71f)).SetMaxEnemies(2).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66905, 88889, 3934), new Vector3f(64726, 87166, 3929), new Angle(-0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64782, 90808, 3929), new Vector3f(66846, 92629, 3937), new Angle(-0.71f, 0.71f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());

            // special/high spots
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66618, 86964, 6041), new Angle(-0.86f, 0.51f)).Mask(SpawnPlane.Mask_Highground)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65072, 86961, 6041), new Angle(-0.67f, 0.74f)).Mask(SpawnPlane.Mask_Highground)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62634, 89067, 6103), new Angle(-0.19f, 0.98f)).Mask(SpawnPlane.Mask_Highground)); // billboard

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61872, 89830, 6933), new Angle(-0.03f, 1.00f)).setRarity(0.2).Mask(SpawnPlane.Mask_Highground));  // fuel tank pipe
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63021, 92747, 5509), new Angle(-0.56f, 0.83f)).Mask(SpawnPlane.Mask_Highground)); // small scuffolding
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68608, 92557, 5728), new Angle(-0.88f, 0.47f)).Mask(SpawnPlane.Mask_Highground)); // crates

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(70094, 92135, 5423), new Angle(-0.96f, 0.27f)).Mask(SpawnPlane.Mask_Highground)); // wall platform holding lab tubes

            // EXTRA - for drones
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64995, 90570, 5295), new Vector3f(66529, 89228, 5295), new Angle(-0.72f, 0.70f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65589, 92250, 5865), new Angle(-0.72f, 0.70f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);

            //// Room 9 - 2 splitters, 2 pistols, 4 drones
            enemies = room_9.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyDrone(enemies[0]);
            enemies[1] = new EnemyDrone(enemies[1]);
            enemies[2] = new EnemyDrone(enemies[2]);
            enemies[3] = new EnemyDrone(enemies[3]);

            var info = new ShifterSpawnInfo();
            info.shiftPoints = new List<Tuple<Vector3f, Angle>>() {
                new Tuple<Vector3f, Angle>(new Vector3f(66087, 115592, 3762), new Angle(-0.70f, 0.71f)),
                new Tuple<Vector3f, Angle>(new Vector3f(67428, 108763, 3430), new Angle(0.90f, 0.43f)),
                new Tuple<Vector3f, Angle>(new Vector3f(63532, 110157, 2832), new Angle(0.00f, 1.00f)),
                new Tuple<Vector3f, Angle>(new Vector3f(62303, 114047, 3641), new Angle(-0.29f, 0.96f)),
                new Tuple<Vector3f, Angle>(new Vector3f(66263, 108763, 3430), new Angle(0.91f, 0.42f)),
                new Tuple<Vector3f, Angle>(new Vector3f(65091, 115597, 3762), new Angle(-0.64f, 0.77f)),
                new Tuple<Vector3f, Angle>(new Vector3f(64908, 112522, 2845), new Angle(-0.70f, 0.72f))
            };
            var info2 = new ShifterSpawnInfo();
            info2.shiftPoints = new List<Tuple<Vector3f, Angle>>() {
                new Tuple<Vector3f, Angle>(new Vector3f(63435, 115576, 3762), new Angle(-0.66f, 0.75f)),
                new Tuple<Vector3f, Angle>(new Vector3f(67707, 115576, 3762), new Angle(-0.81f, 0.59f)),
                new Tuple<Vector3f, Angle>(new Vector3f(68409, 115418, 3009), new Angle(-0.87f, 0.49f)),
                new Tuple<Vector3f, Angle>(new Vector3f(68724, 110670, 3641), new Angle(-0.96f, 0.28f)),
                new Tuple<Vector3f, Angle>(new Vector3f(64232, 108324, 4378), new Angle(0.64f, 0.77f)),
                new Tuple<Vector3f, Angle>(new Vector3f(68676, 115684, 3299), new Angle(-0.85f, 0.52f)),
                new Tuple<Vector3f, Angle>(new Vector3f(66108, 112515, 2845), new Angle(-0.71f, 0.71f))
            };

            int shifterR = Config.GetInstance().r.Next(2); // so each shifter gets a different spawnInfo and they won't collide
            enemies[6] = new EnemyShifter(enemies[6], 7).AddFixedSpawnInfo(shifterR == 0 ? info : info2); // doesn't matter which plane, shifter will use this spawninfo
            enemies[7] = new EnemyShifter(enemies[7], 7).AddFixedSpawnInfo(shifterR == 0 ? info2 : info);


            TakeLastEnemyFromCP(ref enemies, force: true, removeCP: true, attachedDoor: true, enemyIndex: 4); // take one pistol
            TakeLastEnemyFromCP(ref enemies, force: true, removeCP: true, attachedDoor: true, enemyIndex: 1); // take one drone

            layout = new RoomLayout(enemies);
            //// drones
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69928, 114787, 4200), new Vector3f(69762, 109066, 3577), new Angle(-0.96f, 0.28f)).Mask(SpawnPlane.Mask_Airborne)); // behind side billboard, left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61222, 115250, 4200), new Vector3f(61657, 108760, 3676), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Airborne)); // behind side billboard, right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62611, 115053, 3411), new Vector3f(68162, 113864, 3740), new Angle(-0.71f, 0.71f)).Mask(SpawnPlane.Mask_Airborne)); // above exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63125, 111539, 3876), new Vector3f(68166, 110286, 3484), new Angle(-0.74f, 0.67f)).Mask(SpawnPlane.Mask_Airborne)); // above entry platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68425, 108473, 3409), new Angle(1.00f, 0.01f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62536, 108404, 3409), new Angle(-0.01f, 1.00f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63025, 113812, 3370), new Vector3f(63866, 110947, 3864), new Angle(-0.47f, 0.88f)).Mask(SpawnPlane.Mask_Airborne)); // front of billboard, right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66825, 114053, 3768), new Vector3f(67744, 110845, 3246), new Angle(-0.96f, 0.30f)).Mask(SpawnPlane.Mask_Airborne)); // front of billboard, left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65533, 113368, 3941), new Angle(-0.71f, 0.71f)).Mask(SpawnPlane.Mask_Airborne)); // above jump, more towards exit door
            // below ceiling
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61333, 112138, 4874), new Angle(0.02f, 1.00f)).setRarity(0.3).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68630, 115341, 4575), new Angle(-0.84f, 0.54f)).setRarity(0.3).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65605, 108311, 4575), new Angle(0.71f, 0.71f)).setRarity(0.3).Mask(SpawnPlane.Mask_Airborne));


            //// Grounded enemies - default platforms 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68592, 109068, 2832), new Vector3f(67508, 110126, 2832), new Angle(-1.00f, 0.02f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63477, 108921, 2832), new Vector3f(62473, 109977, 2832), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62533, 115166, 2832), new Vector3f(63287, 114344, 2832), new Angle(-0.40f, 0.92f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64997, 115376, 2817), new Vector3f(66107, 114310, 2807), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67312, 114118, 2838), new Vector3f(68478, 115133, 2832), new Angle(-0.72f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68586, 109000, 2832), new Vector3f(67506, 110140, 2832), new Angle(-1.00f, 0.10f)).Mask(SpawnPlane.Mask_Flatground));
            // middle
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66175, 113033, 2847), new Vector3f(65895, 111929, 2847), new Angle(-0.72f, 0.70f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65144, 111934, 2847), new Vector3f(64879, 113034, 2847), new Angle(-0.71f, 0.70f)).Mask(SpawnPlane.Mask_Highground));

            //// high/special spots
            // billboards:
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68702, 113698, 3641), new Vector3f(68707, 112355, 3641), new Angle(-0.90f, 0.43f)).AsVerticalPlane().Mask(SpawnPlane.Mask_Highground)); // billboard 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62296, 112275, 3641), new Vector3f(62289, 113780, 3641), new Angle(-0.35f, 0.94f)).AsVerticalPlane().Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64109, 108764, 3440), new Angle(0.03f, 1.00f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67016, 108758, 3430), new Angle(1.00f, 0.05f)).Mask(SpawnPlane.Mask_Highground));

            Rooms.Add(layout);

            //// 4 uzi, 2 drones
            enemies = room_10.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyDrone(enemies[0]);
            enemies[1] = new EnemyDrone(enemies[1]);

            TakeLastEnemyFromCP(ref enemies, force: false, removeCP: true, attachedDoor: true, enemyIndex: 2); // chance to take uzi
            TakeLastEnemyFromCP(ref enemies, force: false, removeCP: true, attachedDoor: true, enemyIndex: 0); // chance to take drone

            layout = new RoomLayout(enemies);
            // drones
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71477, 138831, 5220), new Vector3f(67025, 137116, 5220), new Angle(1.00f, 0.01f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64918, 137040, 5220), new Vector3f(63375, 138479, 4245), new Angle(-0.05f, 1.00f)).Mask(SpawnPlane.Mask_Airborne));
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71638, 136803, 2840), new Vector3f(63033, 138858, 2841), new Angle(-0.71f, 0.70f)).SetMaxEnemies(4).Mask(SpawnPlane.Mask_Flatground)); // default floor
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71954, 136881, 4707), new Vector3f(71915, 138709, 4707), new Angle(1.00f, 0.00f)).AsVerticalPlane().Mask(SpawnPlane.Mask_Highground)); // slomo platform
            // special/high spots

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65584, 138994, 4785), new Angle(-0.71f, 0.71f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // generator
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63427, 138570, 5668), new Vector3f(63415, 137108, 5668), new Angle(0.04f, 1.00f)).AsVerticalPlane().Mask(SpawnPlane.Mask_HighgroundLimited)); // right floating pipe
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63376, 139192, 4448), new Vector3f(64842, 139207, 4448), new Angle(-0.62f, 0.79f)).AsVerticalPlane().Mask(SpawnPlane.Mask_HighgroundLimited)); // above exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65602, 133944, 3917), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // first wall in hallway
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65308, 139203, 5941), new Vector3f(69685, 139185, 5941), new Angle(-0.74f, 0.67f)).AsVerticalPlane().Mask(SpawnPlane.Mask_HighgroundLimited)); // billboard near ceiling
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68168, 136516, 5885), new Vector3f(69356, 136510, 5885), new Angle(0.73f, 0.68f)).AsVerticalPlane().Mask(SpawnPlane.Mask_HighgroundLimited)); // pipe near ceiling, left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71028, 139219, 4448), new Vector3f(66322, 139240, 4448), new Angle(-0.67f, 0.74f)).AsVerticalPlane().Mask(SpawnPlane.Mask_HighgroundLimited)); // along the metal wall piece

            Rooms.Add(layout);

            // EXTRA
            layout = new RoomLayout();
            // drone spots, room with 5 weebs
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64523, 34036, 3661), new Angle(-0.38f, 0.92f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66875, 38118, 3360), new Angle(-0.89f, 0.45f)).Mask(SpawnPlane.Mask_Airborne));

            // last rooms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66530, 153503, 2837), new Vector3f(64779, 155583, 2836), new Angle(-0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(60313, 151031, 8261), new Angle(-0.04f, 1.00f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64851, 173869, 2840), new Vector3f(66560, 172883, 2840), new Angle(-0.73f, 0.68f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66382, 160300, 2863), new Vector3f(64737, 158356, 2857), new Angle(-0.68f, 0.73f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65548, 175416, 2836), new Angle(-0.69f, 0.72f)).Mask(SpawnPlane.Mask_Highground).setRarity(0.2)); // elevator

            Rooms.Add(layout);


            /////////////// NonPlaceableObjects ///////////////
            #region Jumps
            //// room 8.5 - minimal rng (weird uplink spot...)
            NonPlaceableObject uplink = new UplinkJump(0x50, 0x778); 
            uplink.AddSpawnInfo(new UplinkJumpSpawnInfo ()); // default

            UplinkJumpSpawnInfo jumpSpawn = new UplinkJumpSpawnInfo { 
                TimeToActivate = Config.GetInstance().r.Next(3, 50) / 10, 
                JumpMultiplier = 5,
                JumpForwardMultiplier = Config.GetInstance().r.Next(10, 30) / 10, 
                JumpGravity = Config.GetInstance().r.Next(40, 70) / 10};
            uplink.AddSpawnInfo(jumpSpawn);
            nonPlaceableObjects.Add(uplink);

            //// room 9, 4 drones cross
            uplink = new UplinkJump(0x58, 0x6D0); 
            jumpSpawn = new UplinkJumpSpawnInfo {
                TimeToActivate = Config.GetInstance().r.Next(10, 50) / 10, //1 - 5 sec activation
                JumpMultiplier = Config.GetInstance().r.Next(25, 45) / 10, // 2.5 - 4.5 jump height
                JumpForwardMultiplier = Config.GetInstance().r.Next(0, 45) / 10 * (Config.GetInstance().r.Next(2) == 0 ? 1 : (-1)), // from -4.5 to 4.5 (surprise backwards)
                JumpGravity = Config.GetInstance().r.Next(30, 60) / 10 // slow to normal
            };
            uplink.AddSpawnInfo(jumpSpawn);

            jumpSpawn = new UplinkJumpSpawnInfo { TimeToActivate = 60 }; // one per minute, good luck
            uplink.AddSpawnInfo(jumpSpawn);
            nonPlaceableObjects.Add(uplink);


            //// room 9.5 - first jump
            uplink = new UplinkJump(0x8, 0xA78);
            uplink.AddSpawnInfo(new UplinkJumpSpawnInfo()); // default

            jumpSpawn = new UplinkJumpSpawnInfo { // low gravity (slow jump)
                JumpGravity = 3, JumpMultiplier = 4};
            uplink.AddSpawnInfo(jumpSpawn);

            jumpSpawn = new UplinkJumpSpawnInfo { // high jump, skipping platform
                JumpForwardMultiplier = 4, JumpMultiplier = 6};
            uplink.AddSpawnInfo(jumpSpawn);
            nonPlaceableObjects.Add(uplink);

            #endregion


            #region Shurikens
            //// First room with spiders
            uplink = new UplinkShurikens(0x0, 0x18C8);
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration =  4, MaxAttacks = 30}); // short time
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration =  20, MaxAttacks = 3 }); // few attacks
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 10, MaxAttacks = 1 }); // single attack
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo()); // default
            nonPlaceableObjects.Add(uplink);

            //// Room 3, closer
            uplink = new UplinkShurikens(0x20, 0xCD8);
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo {Duration = 10 , MaxAttacks = 20}); // default
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo {Duration = Config.GetInstance().r.Next(3, 12), MaxAttacks = Config.GetInstance().r.Next(2, 10)}); // basic rng
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 5, MaxAttacks = Config.GetInstance().r.Next(1, 3) }); // unlucky rng
            nonPlaceableObjects.Add(uplink);

            //// Room 3, far
            uplink = new UplinkShurikens(0x20, 0xD60);
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 10, MaxAttacks = 20 }); // default
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = Config.GetInstance().r.Next(3, 12), MaxAttacks = Config.GetInstance().r.Next(2, 10) }); // basic rng
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 5, MaxAttacks = Config.GetInstance().r.Next(1, 3) }); // unlucky rng
            nonPlaceableObjects.Add(uplink);


            //// Room 9.5
            uplink = new UplinkShurikens(0x8, 0xA80);
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo()); // default
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 5, MaxAttacks = 3}); // no mistakes
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = Config.GetInstance().r.Next(5, 10), MaxAttacks = Config.GetInstance().r.Next(5, 12)}); // general rng
            nonPlaceableObjects.Add(uplink);
            #endregion

            #region Slomo
            uplink = new UplinkSlowmo(0x10, 0xA0);
            uplink.AddSpawnInfo(new UplinkSlowmoSpawnInfo { TotalTime = Config.GetInstance().r.Next(5, 15)});
            uplink.AddSpawnInfo(new UplinkSlowmoSpawnInfo { TotalTime = 5, TimeMultiplier = 0.05f });
            #endregion

        }
    }
}
