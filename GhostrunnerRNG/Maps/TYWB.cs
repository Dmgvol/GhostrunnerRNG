using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.GameObjects;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.MemoryUtils;
using GhostrunnerRNG.NonPlaceableObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GhostrunnerRNG.Maps {
    class TYWB : MapCore, IModes {
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

        #region HC Room
        private Room room_hc_1 = new Room(new Vector3f(38840, -54113, 1761), new Vector3f(45415, -57841, 2948));
        private Room room_hc_2 = new Room(new Vector3f(47785, -61672, 1656), new Vector3f(52812, -54379, 3474));
        private Room room_hc_3 = new Room(new Vector3f(67064, -43736, 1513), new Vector3f(64797, -23339, 3174)); // chained orb (shielder)
        private Room room_hc_4 = new Room(new Vector3f(67601, -17501, 2078), new Vector3f(64263, -4968, 4316)); // drones + chained orb (waver)
        private Room room_hc_5 = new Room(new Vector3f(67806, -5893, 1732), new Vector3f(63712, 3755, 5884)); // 2 shifters, chained orb(waver)
        private Room room_hc_6 = new Room(new Vector3f(68437, 5229, 2386), new Vector3f(63038, 11990, 5186)); 
        private Room room_hc_7 = new Room(new Vector3f(67537, 14570, 2125), new Vector3f(70303, 17408, 4455)); // lonely shielder...
        private Room room_hc_8 = new Room(new Vector3f(68419, 32088, 1718), new Vector3f(62104, 37748, 4095)); // 2 shifters
        private Room room_hc_9 = new Room(new Vector3f(60588, 41833, 2121), new Vector3f(71222, 48466, 6680)); // hell room
        private Room room_hc_10 = new Room(new Vector3f(61330, 50942, 5113), new Vector3f(71553, 56104, 7390)); // 2 random froggers
        private Room room_hc_11 = new Room(new Vector3f(58229, 70351, 2125), new Vector3f(71859, 80363, 8374));
        private Room room_hc_12 = new Room(new Vector3f(60847, 84923, 2392), new Vector3f(70475, 93269, 7095)); 
        private Room room_hc_13 = new Room(new Vector3f(61594, 108303, 1887), new Vector3f(70202, 116291, 4931));
        private Room room_hc_14 = new Room(new Vector3f(70931, 118162, 3010), new Vector3f(61454, 127542, 10608));
        private Room room_hc_15 = new Room(new Vector3f(58791, 142521, 1189), new Vector3f(70975, 156481, 9948));
        private Room room_hc_16 = new Room(new Vector3f(60869, 160076, 715), new Vector3f(69750, 173096, 11563)); // 2 chained orbs
        #endregion 

        public TYWB() : base(GameUtils.MapType.TYWB) {
            ModifyCP(new DeepPointer(PtrDB.DP_TYWB_ElevatorCP), new Vector3f(33409, -55474, 2309), GameHook.game);
        }

        public void Gen_Normal() {
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

            // set their type and add to AllEnemies list
            lst.ForEach(x => x.SetEnemyType(Enemy.EnemyTypes.Splitter));
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
            DetachEnemyFromCP(ref enemies, force: false); // take one random
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

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63335, 11003, 4851), new Angle(-0.26f, 0.97f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // corner billboard, high 1
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68102, 10978, 4851), new Angle(-0.97f, 0.26f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // corner billboard, high 2

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64466, 8805, 4981), new Vector3f(66659, 8812, 4981), new Angle(-0.70f, 0.71f)).AsVerticalPlane().setRarity(0.2).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // hovering pipes, middle

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68001, 6568, 4230), new Angle(-1.00f, 0.09f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // left billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63400, 6666, 4230), new Angle(0.30f, 0.96f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // right billboard

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
            DetachEnemyFromCP(ref enemies, force: true); // take 2 weebs, by force
            DetachEnemyFromCP(ref enemies, force: true);

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
            orb2.HideBeam_Range(0, 5);
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
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69660, 47512, 4527)).setDiff(1)); // wall ac unit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68420, 42521, 3373)).setDiff(1)); // lab tube
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68981, 45078, 3493), new Vector3f(65538, 45015, 4339)).AsVerticalPlane()); // hovering between platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69902, 45260, 4889)).setDiff(1)); // behind billboard

            // middle of room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66786, 46092, 4889), new Vector3f(64336, 46049, 4006)).AsVerticalPlane()); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66474, 46885, 3406))); // below platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64584, 46887, 3406))); // below platform 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65553, 47528, 4578)));  // above  exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65578, 44501, 3292))); // under bridge/arc

            // right side of room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65538, 45015, 4339), new Vector3f(62325, 44978, 3361)).AsVerticalPlane()); // hovering between platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62354, 45106, 4534), new Vector3f(61513, 44310, 5508)).AsVerticalPlane()); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62896, 42475, 3373)).setDiff(1)); // lab tube
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(60768, 41719, 3850), new Vector3f(60830, 46939, 3850)).AsVerticalPlane().setDiff(1)); // right wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(60953, 46870, 4918)).setDiff(1)); // wall corner pipe

            // special/high places
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68879, 46487, 6407)).setRarity(0.25).setDiff(1)); // floating lamp - rare
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65476, 44550, 6433)).setRarity(0.25).setDiff(1)); // floating lamp, hard to reach - rare
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62862, 42841, 6790)).setRarity(0.25).setDiff(1)); // corner tube, top of lab tube - rare

            layout.Mask(SpawnPlane.Mask_ShieldOrb);
            layout.DoNotReuse();
            Rooms.Add(layout);

            // enemies planes
            layout = new RoomLayout(enemies.Skip(2).ToList());
            // default platforms
            var mask = SpawnPlane.Mask_Highground;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(70149, 43363, 2832), new Vector3f(69346, 47224, 2832), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground)); // allow for weebs
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61885, 47541, 2832), new Vector3f(60957, 43342, 2832), new Angle(-0.72f, 0.70f)).Mask(SpawnPlane.Mask_Flatground)); // allow for weebs
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61984, 43161, 2832), new Vector3f(63385, 43893, 2832), new Angle(-0.73f, 0.69f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67835, 43968, 2832), new Vector3f(69182, 43290, 2832), new Angle(-0.73f, 0.69f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68377, 44009, 2832), new Vector3f(66810, 44836, 2832), new Angle(-0.73f, 0.68f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63087, 43957, 2832), new Vector3f(64317, 44939, 2832), new Angle(-0.71f, 0.71f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64310, 43996, 2832), new Vector3f(66809, 43113, 2832), new Angle(-0.72f, 0.70f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66559, 44951, 2832), new Vector3f(64664, 45999, 2832), new Angle(-0.69f, 0.73f)).Mask(SpawnPlane.Mask_Flatground)); // allow for weebs
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64053, 45948, 2832), new Vector3f(63015, 46952, 2832), new Angle(-0.70f, 0.72f)).Mask(SpawnPlane.Mask_Flatground)); // allow for weebs
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67192, 46993, 2832), new Vector3f(68301, 46124, 2832), new Angle(-0.72f, 0.70f)).Mask(SpawnPlane.Mask_Flatground)); // allow for weebs
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66761, 47450, 2832), new Vector3f(64279, 47017, 2832), new Angle(-0.67f, 0.74f)).Mask(mask));
            // default raised platforms
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
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65772, 44192, 5170), new Angle(-0.72f, 0.70f)).Mask(mask).setDiff(1)); // middle billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67841, 42643, 5210), new Angle(-0.99f, 0.13f)).Mask(mask).setDiff(1)); // left billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69595, 43125, 5385), new Angle(1.00f, 0.01f)).Mask(mask).setDiff(1)); // left billboard 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69321, 45208, 5635), new Angle(-0.97f, 0.25f)).Mask(mask).setDiff(1)); // left billboard 3
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65516, 46336, 5130), new Angle(-0.71f, 0.70f)).Mask(mask).setDiff(1)); // middle billboard, back
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62052, 45179, 5635), new Angle(-0.27f, 0.96f)).Mask(mask).setDiff(1)); // right billboard 3
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61336, 43937, 5385), new Angle(-0.17f, 0.99f)).Mask(mask).setDiff(1)); // right billboard 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63608, 42657, 5200), new Angle(0.15f, 0.99f)).Mask(mask).setDiff(1)); // right billboard 1
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61963, 47490, 4528), new Angle(-0.59f, 0.81f)).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // right wall ac unit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(60974, 46120, 4918), new Angle(-0.07f, 1.00f)).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // right tube, behind billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61029, 45522, 5717), new Angle(0.01f, 1.00f)).setRarity(0.25).Mask(mask).setDiff(1)); // tube with cloth, right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64697, 47568, 5227), new Angle(-0.70f, 0.71f)).Mask(mask).setDiff(1)); // wall fan above exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69172, 47488, 4528), new Angle(-0.92f, 0.39f)).Mask(mask).setDiff(1)); // left ac unit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65533, 46932, 6452), new Angle(-0.71f, 0.71f)).setRarity(0.25).Mask(mask).setDiff(1)); // floating lamp, above exit door (high)

            // EXTRA - for drones
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67967, 45574, 5192), new Vector3f(64162, 45583, 5192), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_Airborne).setDiff(1));

            //layout.DoNotReuse();
            Rooms.Add(layout);

            //// Room 6 - 1 splitter////
            enemies = room_6.ReturnEnemiesInRoom(AllEnemies);
            RandomPickEnemiesWithoutCP(ref enemies, force: false, removeCP: true); // chance to take splitter, no place for rng


            //// Room 7 - 1 frogger, 2 splitter, 1 weeb////
            enemies = room_7.ReturnEnemiesInRoom(AllEnemies);
            enemies[1].SetEnemyType(Enemy.EnemyTypes.Weeb);
            layout = new RoomLayout(enemies);

            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68336, 76081, 3941), new Vector3f(66643, 75026, 3940), new Angle(0.72f, 0.70f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64564, 74898, 3928), new Vector3f(62614, 75881, 3930), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62838, 76295, 3930), new Vector3f(61888, 75615, 3930), new Angle(0.51f, 0.86f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68205, 73656, 5012), new Vector3f(69461, 72554, 5012), new Angle(-0.95f, 0.31f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63136, 72209, 5008), new Vector3f(61937, 73050, 5015), new Angle(-0.18f, 0.98f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
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
            //enemies[4].SetEnemyType(Enemy.EnemyTypes.Splitter);
            //enemies[5].SetEnemyType(Enemy.EnemyTypes.Splitter);
            layout = new RoomLayout(enemies);

            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67382, 91097, 5128), new Vector3f(68406, 92444, 5128), new Angle(-0.84f, 0.54f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64317, 91004, 5128), new Vector3f(63096, 92437, 5128), new Angle(-0.54f, 0.84f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63362, 87388, 5128), new Vector3f(64257, 88532, 5128), new Angle(-0.20f, 0.98f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68727, 87112, 5128), new Vector3f(67393, 88503, 5128), new Angle(-0.91f, 0.41f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68495, 89228, 3929), new Vector3f(62866, 90511, 3929), new Angle(-0.70f, 0.71f)).SetMaxEnemies(2).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66905, 88889, 3934), new Vector3f(64726, 87166, 3929), new Angle(-0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64782, 90808, 3929), new Vector3f(66846, 92629, 3937), new Angle(-0.71f, 0.71f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());

            // special/high spots
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66618, 86964, 6041), new Angle(-0.86f, 0.51f)).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65072, 86961, 6041), new Angle(-0.67f, 0.74f)).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62634, 89067, 6103), new Angle(-0.19f, 0.98f)).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // billboard

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61872, 89830, 6933), new Angle(-0.03f, 1.00f)).setRarity(0.2).Mask(SpawnPlane.Mask_Highground).setDiff(1));  // fuel tank pipe
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63021, 92747, 5509), new Angle(-0.56f, 0.83f)).Mask(SpawnPlane.Mask_Highground)); // small scuffolding
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68608, 92557, 5728), new Angle(-0.88f, 0.47f)).Mask(SpawnPlane.Mask_Highground)); // crates

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(70094, 92135, 5423), new Angle(-0.96f, 0.27f)).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // wall platform holding lab tubes

            // EXTRA - for drones
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64995, 90570, 5295), new Vector3f(66529, 89228, 5295), new Angle(-0.72f, 0.70f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65589, 92250, 5865), new Angle(-0.72f, 0.70f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);

            //// Room 9 - 2 shifters, 2 pistols, 4 drones
            enemies = room_9.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyDrone(enemies[0]);
            enemies[1] = new EnemyDrone(enemies[1]);
            enemies[2] = new EnemyDrone(enemies[2]);
            enemies[3] = new EnemyDrone(enemies[3]);

            if(Config.GetInstance().Setting_Difficulty == Config.Difficulty.Normal) { // no room for easy rng, easy get default
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
            }


            DetachEnemyFromCP(ref enemies, force: true, enemyIndex: 4); // take one pistol
            DetachEnemyFromCP(ref enemies, force: true, enemyIndex: 1); // take one drone

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
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61333, 112138, 4874), new Angle(0.02f, 1.00f)).setRarity(0.3).Mask(SpawnPlane.Mask_Airborne).setDiff(1));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68630, 115341, 4575), new Angle(-0.84f, 0.54f)).setRarity(0.3).Mask(SpawnPlane.Mask_Airborne).setDiff(1));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65605, 108311, 4575), new Angle(0.71f, 0.71f)).setRarity(0.3).Mask(SpawnPlane.Mask_Airborne).setDiff(1));


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
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68702, 113698, 3641), new Vector3f(68707, 112355, 3641), new Angle(-0.90f, 0.43f)).AsVerticalPlane().Mask(SpawnPlane.Mask_Highground).setDiff(1)); // billboard 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62296, 112275, 3641), new Vector3f(62289, 113780, 3641), new Angle(-0.35f, 0.94f)).AsVerticalPlane().Mask(SpawnPlane.Mask_Highground).setDiff(1));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64109, 108764, 3440), new Angle(0.03f, 1.00f)).Mask(SpawnPlane.Mask_Highground).setDiff(1));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67016, 108758, 3430), new Angle(1.00f, 0.05f)).Mask(SpawnPlane.Mask_Highground).setDiff(1));

            Rooms.Add(layout);

            //// 4 uzi, 2 drones
            enemies = room_10.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyDrone(enemies[0]);
            enemies[1] = new EnemyDrone(enemies[1]);

            DetachEnemyFromCP(ref enemies, force: false, enemyIndex: 2); // chance to take uzi
            DetachEnemyFromCP(ref enemies, force: false, enemyIndex: 0); // chance to take drone

            layout = new RoomLayout(enemies);
            // drones
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71477, 138831, 5220), new Vector3f(67025, 137116, 5220), new Angle(1.00f, 0.01f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64918, 137040, 5220), new Vector3f(63375, 138479, 4245), new Angle(-0.05f, 1.00f)).Mask(SpawnPlane.Mask_Airborne));
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71638, 136803, 2840), new Vector3f(63033, 138858, 2841), new Angle(-0.71f, 0.70f)).SetMaxEnemies(4).Mask(SpawnPlane.Mask_Flatground)); // default floor
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71954, 136881, 4707), new Vector3f(71915, 138709, 4707), new Angle(1.00f, 0.00f)).AsVerticalPlane().SetMaxEnemies(2).Mask(SpawnPlane.Mask_Highground)); // slomo platform
            // special/high spots
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65584, 138994, 4785), new Angle(-0.71f, 0.71f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // generator
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63427, 138570, 5668), new Vector3f(63415, 137108, 5668), new Angle(0.04f, 1.00f)).AsVerticalPlane().Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // right floating pipe
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63376, 139192, 4448), new Vector3f(64842, 139207, 4448), new Angle(-0.62f, 0.79f)).AsVerticalPlane().Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // above exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65602, 133944, 3917), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // first wall in hallway
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65308, 139203, 5941), new Vector3f(69685, 139185, 5941), new Angle(-0.74f, 0.67f)).AsVerticalPlane().Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // billboard near ceiling
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68168, 136516, 5885), new Vector3f(69356, 136510, 5885), new Angle(0.73f, 0.68f)).AsVerticalPlane().Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // pipe near ceiling, left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71028, 139219, 4448), new Vector3f(66322, 139240, 4448), new Angle(-0.67f, 0.74f)).AsVerticalPlane().Mask(SpawnPlane.Mask_HighgroundLimited)); // along the metal wall piece
            layout.DoNotReuse();
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
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65548, 175416, 2836), new Angle(-0.69f, 0.72f)).Mask(SpawnPlane.Mask_Highground).setRarity(0.3)); // elevator

            Rooms.Add(layout);


            /////////////// NonPlaceableObjects ///////////////
            #region Jumps
            //// room 8.5 - minimal rng (weird uplink spot...)
            NonPlaceableObject uplink = new UplinkJump(0x50, 0x778);
            uplink.AddSpawnInfo(new UplinkJumpSpawnInfo()); // default

            UplinkJumpSpawnInfo jumpSpawn = new UplinkJumpSpawnInfo {
                TimeToActivate = Config.GetInstance().r.Next(3, 50) / 10,
                JumpMultiplier = 5,
                JumpForwardMultiplier = Config.GetInstance().r.Next(10, 30) / 10,
                JumpGravity = Config.GetInstance().r.Next(40, 70) / 10
            };
            uplink.AddSpawnInfo(jumpSpawn);
            worldObjects.Add(uplink);

            //// room 9, 4 drones cross
            uplink = new UplinkJump(0x58, 0x6D0);
            jumpSpawn = new UplinkJumpSpawnInfo {
                TimeToActivate = Config.GetInstance().r.Next(10, 50) / 10, //1 - 5 sec activation
                JumpMultiplier = Config.GetInstance().r.Next(25, 45) / 10, // 2.5 - 4.5 jump height
                JumpForwardMultiplier = Config.GetInstance().r.Next(0, 45) / 10 * (Config.GetInstance().r.Next(2) == 0 ? 1 : (-1)), // from -4.5 to 4.5 (surprise backwards)
                JumpGravity = Config.GetInstance().r.Next(30, 60) / 10 // slow to normal
            };
            uplink.AddSpawnInfo(jumpSpawn);
            worldObjects.Add(uplink);


            //// room 9.5 - first jump
            uplink = new UplinkJump(0x8, 0xA78);
            uplink.AddSpawnInfo(new UplinkJumpSpawnInfo().SetRarity(0.5)); // default

            jumpSpawn = new UplinkJumpSpawnInfo { // low gravity (slow jump)
                JumpGravity = 3, JumpMultiplier = 4
            };
            uplink.AddSpawnInfo(jumpSpawn);

            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = -5.0f, JumpForwardMultiplier = -2.0f, JumpGravity = -5.0f }.SetRarity(0.2);
            uplink.AddSpawnInfo(jumpSpawn);//very weird negative gravity jump

            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 4.25f, JumpForwardMultiplier = 0.0f, JumpGravity = 0.0f };
            uplink.AddSpawnInfo(jumpSpawn);//vertical jump

            jumpSpawn = new UplinkJumpSpawnInfo { // high jump, skipping platform
                JumpForwardMultiplier = 4, JumpMultiplier = 6
            };
            uplink.AddSpawnInfo(jumpSpawn);
            worldObjects.Add(uplink);

            //// room 9.5 - second jump
            uplink = new UplinkJump(0x8, 0xA70);//4.5,3,6
            uplink.AddSpawnInfo(new UplinkJumpSpawnInfo().SetRarity(0.5)); // default
            jumpSpawn = new UplinkJumpSpawnInfo { JumpForwardMultiplier = -3.5f };
            uplink.AddSpawnInfo(jumpSpawn);//normal backward jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 10.0f, JumpForwardMultiplier = 10.0f, JumpGravity = 3.0f };
            uplink.AddSpawnInfo(jumpSpawn);//almost oob
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 1.0f, JumpForwardMultiplier = 6.0f, JumpGravity = -5.0f };
            uplink.AddSpawnInfo(jumpSpawn);//right into the hole

            //// room 9.5 - fourth jump
            uplink = new UplinkJump(0x8, 0x48);//7,3,6
            uplink.AddSpawnInfo(new UplinkJumpSpawnInfo().SetRarity(0.5)); // default
            jumpSpawn = new UplinkJumpSpawnInfo { JumpForwardMultiplier = -3.5f };
            uplink.AddSpawnInfo(jumpSpawn);//normal backward jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 9.0f, JumpForwardMultiplier = 3.0f, JumpGravity = 13.0f };
            uplink.AddSpawnInfo(jumpSpawn);//short jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 0.5f, JumpForwardMultiplier = 4.0f, JumpGravity = -6.0f };
            uplink.AddSpawnInfo(jumpSpawn);//negative gravity

            //// room 10.5 - first jump
            uplink = new UplinkJump(0x18, 0x1318);//5,3,6
            uplink.AddSpawnInfo(new UplinkJumpSpawnInfo().SetRarity(0.5)); // default
            jumpSpawn = new UplinkJumpSpawnInfo { JumpForwardMultiplier = 2.5f, JumpGravity = 8.0f };
            uplink.AddSpawnInfo(jumpSpawn);//really short jump need to use dash
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 8.0f, JumpForwardMultiplier = 8.0f, JumpGravity = 4.0f };
            uplink.AddSpawnInfo(jumpSpawn);//negative gravity
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 0.5f, JumpForwardMultiplier = -3.0f, JumpGravity = -3.5f };
            uplink.AddSpawnInfo(jumpSpawn);//negative gravity backward
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 20.0f, JumpForwardMultiplier = -20.0f, JumpGravity = 6.0f };
            uplink.AddSpawnInfo(jumpSpawn);//backward bounce
            worldObjects.Add(uplink);

            //// room 10.5 - second jump
            uplink = new UplinkJump(0x18, 0x13b8);//6,3,6
            uplink.AddSpawnInfo(new UplinkJumpSpawnInfo().SetRarity(0.5)); // default
            jumpSpawn = new UplinkJumpSpawnInfo { JumpGravity = 8.0f };
            uplink.AddSpawnInfo(jumpSpawn);//really short jump need to use dash
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 6.0f, JumpForwardMultiplier = 0.0f, JumpGravity = -2.5f };
            uplink.AddSpawnInfo(jumpSpawn);//vertical jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpForwardMultiplier = -3.0f, JumpGravity = -3.5f };
            uplink.AddSpawnInfo(jumpSpawn);//negative gravity backward
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 3.0f, JumpForwardMultiplier = -2.0f, JumpGravity = -5.0f };
            uplink.AddSpawnInfo(jumpSpawn);//backwards
            worldObjects.Add(uplink);

            //// room 10.5 - recovery jump
            uplink = new UplinkJump(0x18, 0x548);//7,3,6
            uplink.AddSpawnInfo(new UplinkJumpSpawnInfo()); // default
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 3.0f, JumpForwardMultiplier = 16.0f, JumpGravity = 10.0f };
            uplink.AddSpawnInfo(jumpSpawn);//to the first platform
            jumpSpawn = new UplinkJumpSpawnInfo { JumpForwardMultiplier = -4.0f };
            uplink.AddSpawnInfo(jumpSpawn);//normal backward jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 6.0f, JumpForwardMultiplier = 6.1f, JumpGravity = 8.0f };
            uplink.AddSpawnInfo(jumpSpawn);//to the second platform

            #endregion

            #region Shurikens
            //// First room with spiders
            uplink = new UplinkShurikens(0x0, 0x18C8);
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 4, MaxAttacks = 30 }); // short time
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 20, MaxAttacks = 3 }); // few attacks
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 10, MaxAttacks = 1 }); // single attack
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo()); // default
            worldObjects.Add(uplink);

            //// Room 3, closer
            uplink = new UplinkShurikens(0x20, 0xCD8);
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 10, MaxAttacks = 20 }); // default
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = Config.GetInstance().r.Next(3, 12), MaxAttacks = Config.GetInstance().r.Next(2, 10) }); // basic rng
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 5, MaxAttacks = Config.GetInstance().r.Next(1, 3) }); // unlucky rng
            worldObjects.Add(uplink);

            //// Room 3, far
            uplink = new UplinkShurikens(0x20, 0xD60);
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 10, MaxAttacks = 20 }); // default
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = Config.GetInstance().r.Next(3, 12), MaxAttacks = Config.GetInstance().r.Next(2, 10) }); // basic rng
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 5, MaxAttacks = Config.GetInstance().r.Next(1, 3) }); // unlucky rng
            worldObjects.Add(uplink);


            //// Room 9.5
            uplink = new UplinkShurikens(0x8, 0xA80);
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo()); // default
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 5, MaxAttacks = 3 }); // no mistakes
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = Config.GetInstance().r.Next(5, 10), MaxAttacks = Config.GetInstance().r.Next(5, 12) }); // general rng
            worldObjects.Add(uplink);
            #endregion

            #region Slomo
            uplink = new UplinkSlowmo(0x10, 0x9E8, 0xA0);
            uplink.AddSpawnInfo(new UplinkSlowmoSpawnInfo { TotalTime = Config.GetInstance().r.Next(5, 15) });
            uplink.AddSpawnInfo(new UplinkSlowmoSpawnInfo { TotalTime = 5, TimeMultiplier = 0.05f });
            #endregion

        }

        public void Gen_Easy() { Gen_Normal(); }


        private List<ChainedOrb> chainedOrbs = new List<ChainedOrb>();
        private List<RoomLayout> chainedOrbs_Rooms = new List<RoomLayout>();

        public void Gen_Hardcore() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game);
            List<Enemy> Shifters = new List<Enemy>();
            Rooms = new List<RoomLayout>();
            RoomLayout layout;


            //// Room 1 ////
            var enemies = room_hc_1.ReturnEnemiesInRoom(AllEnemies);
            enemies[5].SetEnemyType(Enemy.EnemyTypes.Waver);
            RandomPickEnemiesWithoutCP(ref enemies, force:true);
            RandomPickEnemiesWithoutCP(ref enemies);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41016, -56050, 2272), new Vector3f(40099, -55066, 2277), new Angle(-1.00f, 0.03f)).Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41549, -54957, 2272), new Vector3f(42292, -55646, 2272), new Angle(1.00f, 0.01f)).Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42908, -55052, 2274), new Vector3f(43579, -55984, 2272), new Angle(1.00f, 0.01f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(45394, -55016, 2272), new Vector3f(44225, -56123, 2272), new Angle(-1.00f, 0.01f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(45145, -58524, 2272), new Vector3f(44381, -56552, 2272), new Angle(0.73f, 0.68f)).Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2).AllowSplitter());
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41911, -56099, 2578), new Angle(0.98f, 0.17f)).Mask(SpawnPlane.Mask_Highground)); // tube
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42854, -54898, 2577), new Angle(-0.99f, 0.12f)).Mask(SpawnPlane.Mask_Highground)); // tube 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(44192, -57584, 2577), new Angle(0.63f, 0.77f)).Mask(SpawnPlane.Mask_Highground)); // tube
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(45312, -58047, 2578), new Angle(0.79f, 0.61f)).Mask(SpawnPlane.Mask_Highground)); // tube
            // drones
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(44899, -55486, 2855), new Angle(1.00f, 0.00f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);


            //// Room 2 ////
            enemies = room_hc_2.ReturnEnemiesInRoom(AllEnemies);
            enemies[0].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[1].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[4].SetEnemyType(Enemy.EnemyTypes.Weeb);
            RandomPickEnemiesWithoutCP(ref enemies, force: true);
            RandomPickEnemiesWithoutCP(ref enemies);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(48595, -55384, 2272), new Vector3f(49363, -58137, 2272), new Angle(-0.70f, 0.71f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(52166, -55429, 2272), new Angle(-1.00f, 0.01f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51702, -55038, 2938), new Angle(-0.98f, 0.19f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50801, -55899, 2939), new Angle(-1.00f, 0.06f)).Mask(SpawnPlane.Mask_Highground));
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(48421, -56301, 2578), new Angle(-0.65f, 0.76f)).Mask(SpawnPlane.Mask_Highground)); // tube
            // drones
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(54527, -55508, 3214), new Angle(-1.00f, 0.00f)).Mask(SpawnPlane.Mask_Airborne));
            // turret
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53146, -56883, 2596), new Angle(0.56f, 0.83f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, HorizontalSpeed = 0, VerticalAngle = 10, VisibleLaserLength = 0})); // ninja, on left tube, ziplines
            Rooms.Add(layout);


            //// Room 3 ////
            enemies = room_hc_3.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[1].SetEnemyType(Enemy.EnemyTypes.Splitter);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Splitter);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Splitter);
            enemies[5].SetEnemyType(Enemy.EnemyTypes.Weeb);
            // chained orb
            chainedOrbs.Add(new ChainedOrb(new EnemyShieldOrb(enemies[0]), enemies[4])); // orb + shielder
            enemies.RemoveAt(4); enemies.RemoveAt(0);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, enemyIndex: 1); // splitter
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66377, -39041, 2272), new Vector3f(65388, -39628, 2272), new Angle(-0.70f, 0.72f))
                .Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65176, -38240, 2272), new Vector3f(66332, -33192, 2272), new Angle(-0.70f, 0.72f))
                .Mask(SpawnPlane.Mask_Flatground).AllowSplitter().SetMaxEnemies(4));
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65092, -33618, 2631), new Angle(-0.64f, 0.77f)).Mask(SpawnPlane.Mask_Highground)); // ad   
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65738, -23741, 2275), new Angle(-0.71f, 0.71f)).Mask(SpawnPlane.Mask_Highground)); // exit door
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65514, -38684, 2942), new Vector3f(66116, -33920, 3099), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);


            //// Room 4 ////
            enemies = room_hc_4.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[1] = new EnemyDrone(enemies[1]);
            enemies[2] = new EnemyDrone(enemies[2]);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Waver);
            chainedOrbs.Add(new ChainedOrb(new EnemyShieldOrb(enemies[0]), enemies[3])); // orb + waver
            enemies.RemoveAt(3); enemies.RemoveAt(0);
            EnemiesWithoutCP.AddRange(enemies); // 2 drones



            //// Room 5 ////
            enemies = room_hc_5.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[1] = new EnemyShifter(enemies[1]);
            enemies[2] = new EnemyShifter(enemies[2]);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[4].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[6].SetEnemyType(Enemy.EnemyTypes.Weeb);
            // chained orb
            chainedOrbs.Add(new ChainedOrb(new EnemyShieldOrb(enemies[0]), 
                enemies.Where(x=>x.enemyType == Enemy.EnemyTypes.Waver && x.Pos.Y > 0).First()));
            enemies.RemoveAll(x => x.Pos.Y > 2000);
            // shifters
            enemies[1].DisableAttachedCP(GameHook.game);
            Shifters.Add(enemies[1]); // shifter
            enemies.RemoveAt(1); // remove shifter from room list

            // shifter
            List<Tuple<Vector3f, Angle>> ShifterPoints_HC_Room5 = new List<Tuple<Vector3f, Angle>>() {
                new Tuple<Vector3f, Angle>(new Vector3f(67157, 2392, 3863), new Angle(-0.91f, 0.42f)), // left tube, middle platform
                new Tuple<Vector3f, Angle>(new Vector3f(66701, 2712, 3552), new Angle(-0.96f, 0.30f)), // middle platform
                new Tuple<Vector3f, Angle>(new Vector3f(64204, 1309, 4952), new Angle(-0.09f, 1.00f)), // above 2nd stairs
                new Tuple<Vector3f, Angle>(new Vector3f(64255, -2529, 4343), new Angle(0.58f, 0.82f)), // table
                new Tuple<Vector3f, Angle>(new Vector3f(67152, -2554, 4262), new Angle(0.94f, 0.34f)), // default, last platform
                new Tuple<Vector3f, Angle>(new Vector3f(66903, 710, 4378), new Angle(-0.97f, 0.26f)), // barrels near ramp
                new Tuple<Vector3f, Angle>(new Vector3f(66981, 2875, 4939), new Angle(-0.81f, 0.58f)), // 2nd round pillar, before exit
                new Tuple<Vector3f, Angle>(new Vector3f(64268, 556, 4329), new Angle(0.04f, 1.00f)), // 3rd platform, after stairs
                new Tuple<Vector3f, Angle>(new Vector3f(66565, 3364, 4672), new Angle(-0.72f, 0.70f)), // pipe, front wall
            };
            (enemies[0] as EnemyShifter)?.AddFixedSpawnInfoList(ref ShifterPoints_HC_Room5);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66641, 309, 2853), new Vector3f(66310, -1260, 2853), new Angle(-0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64141, 2349, 3552), new Vector3f(65798, 1988, 3552), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66490, 1901, 3552), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64894, -113, 4262), new Vector3f(64524, -2670, 4262), new Angle(0.70f, 0.72f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66669, -294, 4262), new Vector3f(67305, -1543, 4262), new Angle(-0.94f, 0.35f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67247, -1852, 4500), new Angle(-0.99f, 0.12f)).Mask(SpawnPlane.Mask_Highground)); // boxes last platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67162, 622, 4618), new Angle(-0.84f, 0.54f)).Mask(SpawnPlane.Mask_Highground)); // ramp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67109, 1500, 4839), new Angle(-0.79f, 0.62f)).Mask(SpawnPlane.Mask_Highground)); // round pillar top
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66376, 2351, 4293), new Angle(-0.73f, 0.68f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65407, 3305, 3552), new Angle(-0.83f, 0.55f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 10, HorizontalSpeed = 5, VerticalAngle = 5}));
            Rooms.Add(layout);


            //// Room 6 ////
            enemies = room_hc_6.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[1] = new EnemyShieldOrb(enemies[1]);
            enemies[2] = new EnemyShieldOrb(enemies[2]);
            enemies[5].SetEnemyType(Enemy.EnemyTypes.Splitter);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, enemyIndex: 4); // pistol
            RandomPickEnemiesWithoutCP(ref enemies, force: true, enemyIndex: 3); // pistol 
            layout = new RoomLayout(enemies.Take(3).ToList()); // orbs
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67749, 10923, 3696), new Vector3f(64137, 10953, 4466)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67035, 10018, 3641), new Vector3f(66887, 7913, 4497)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64442, 8012, 3722), new Vector3f(64456, 9762, 4522)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63756, 9317, 3445), new Vector3f(63744, 10093, 4141)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66090, 7229, 3599), new Vector3f(65257, 7139, 4082)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68090, 8245, 3749), new Vector3f(68145, 10005, 4252)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67653, 9273, 3610), new Vector3f(67689, 9955, 4091)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67378, 10469, 3860)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64069, 10417, 3860)));
            Rooms.Add(layout);
            layout = new RoomLayout(enemies.Skip(3).ToList()); // enemies
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67038, 10885, 2852), new Vector3f(64673, 8048, 2852), new Angle(-0.71f, 0.70f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(4).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63367, 10501, 2852), new Vector3f(63761, 8180, 2852), new Angle(-0.68f, 0.74f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67651, 7970, 2852), new Vector3f(67976, 10726, 2852), new Angle(-0.73f, 0.69f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2).AllowSplitter());
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67486, 10963, 3050), new Angle(-0.71f, 0.70f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // boxes left of exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68209, 9516, 3314), new Angle(-0.84f, 0.54f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63307, 10990, 4851), new Angle(-0.38f, 0.93f)).Mask(SpawnPlane.Mask_Highground)); // top right billboard corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68147, 11001, 4851), new Angle(-0.95f, 0.31f)).Mask(SpawnPlane.Mask_Highground)); // top left billboard corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67991, 6586, 4230), new Angle(-0.99f, 0.17f)).Mask(SpawnPlane.Mask_Highground)); // first left billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63417, 6662, 4230), new Angle(-0.13f, 0.99f)).Mask(SpawnPlane.Mask_Highground)); // first right billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64356, 8337, 3133), new Angle(-0.53f, 0.85f)).Mask(SpawnPlane.Mask_Highground)); // boxes middle
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66774, 10511, 4079), new Vector3f(64636, 8228, 4268), new Angle(-0.72f, 0.69f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63695, 7225, 4268), new Angle(-0.49f, 0.87f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68052, 9890, 3928), new Angle(-0.75f, 0.66f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67483, 10718, 2852), new Angle(-0.98f, 0.21f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 30, VerticalAngle = 5}));
            Rooms.Add(layout);


            //// Room 7 ////
            enemies = room_hc_7.ReturnEnemiesInRoom(AllEnemies);
            RandomPickEnemiesWithoutCP(ref enemies, force: true); // lonely shielder
           

            //// Room 8 ////
            enemies = room_hc_8.ReturnEnemiesInRoom(AllEnemies);
            enemies[1].SetEnemyType(Enemy.EnemyTypes.Splitter);
            enemies[2] = new EnemyShifter(enemies[2]);
            enemies[3] = new EnemyShifter(enemies[3]);
            // shifters
            Shifters.Add(enemies[3]);
            enemies.RemoveAt(3);
            (enemies[2] as EnemyShifter)?.AddFixedSpawnInfo(new ShifterSpawnInfo { shiftPoints = new List<Tuple<Vector3f, Angle>>() {
                new Tuple<Vector3f, Angle>(new Vector3f(67180, 33990, 3294), new Angle(0.82f, 0.58f)), // server rack, left 
                new Tuple<Vector3f, Angle>(new Vector3f(64527, 34128, 3294), new Angle(0.57f, 0.82f)), // server rack, right
                new Tuple<Vector3f, Angle>(new Vector3f(67085, 37917, 3075), new Angle(-0.90f, 0.44f)), // boxes
                new Tuple<Vector3f, Angle>(new Vector3f(64487, 38079, 2836), new Angle(-0.44f, 0.90f)) // platform, right corner
            }});

            RandomPickEnemiesWithoutCP(ref enemies, enemyIndex: 0); // pistol
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64487, 37288, 2836), new Vector3f(67116, 35030, 2836), new Angle(-0.70f, 0.72f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(5).AllowSplitter());
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65334, 38369, 3334), new Angle(-0.64f, 0.77f)).Mask(SpawnPlane.Mask_Highground)); // wall light
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64949, 35572, 3298), new Vector3f(66834, 37263, 3514), new Angle(-0.70f, 0.72f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65798, 34015, 3178), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Turret).SetSpawnInfo(
                new TurretSpawnInfo { HorizontalAngle = 40, HorizontalSpeed = 40, VerticalAngle = -5, Range = 3300 })); // middle of server rack, aim towards door
            Rooms.Add(layout);



            //// Room 9 ////
            enemies = room_hc_9.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[1] = new EnemyShieldOrb(enemies[1]);
            // shifter (3 for each, 6 total min)
            List<Tuple<Vector3f, Angle>> ShifterPoints_HC_Room9 = new List<Tuple<Vector3f, Angle>>() {
                // right
                new Tuple<Vector3f, Angle>(new Vector3f(62496, 44394, 3952), new Angle(-0.56f, 0.83f)), // near lower platform
                new Tuple<Vector3f, Angle>(new Vector3f(62516, 46486, 3952), new Angle(-0.65f, 0.76f)), // lower platform
                new Tuple<Vector3f, Angle>(new Vector3f(63451, 43388, 4452), new Angle(-0.43f, 0.90f)), // high platform, right
                new Tuple<Vector3f, Angle>(new Vector3f(64726, 44350, 4451), new Angle(-0.63f, 0.77f)), // middle-front, right
                new Tuple<Vector3f, Angle>(new Vector3f(63432, 45489, 4452), new Angle(-0.58f, 0.82f)), // middle-back, right
                // left
                new Tuple<Vector3f, Angle>(new Vector3f(68890, 44489, 3952), new Angle(-0.90f, 0.44f)), // near lower platform
                new Tuple<Vector3f, Angle>(new Vector3f(68978, 46510, 3952), new Angle(-0.89f, 0.46f)), // far lower platform
                new Tuple<Vector3f, Angle>(new Vector3f(67668, 43388, 4452), new Angle(-0.84f, 0.54f)), // high platform, left
                new Tuple<Vector3f, Angle>(new Vector3f(66447, 44354, 4452), new Angle(-0.76f, 0.65f)), // middle-front, left
                new Tuple<Vector3f, Angle>(new Vector3f(67724, 45443, 4452), new Angle(-0.75f, 0.66f))  // middle-back, left
            };

            enemies[2] = new EnemyShifter(enemies[2]).AddFixedSpawnInfoList(ref ShifterPoints_HC_Room9);
            enemies[3] = new EnemyShifter(enemies[3]).AddFixedSpawnInfoList(ref ShifterPoints_HC_Room9);
            enemies[6].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[7].SetEnemyType(Enemy.EnemyTypes.Waver);

            layout = new RoomLayout(enemies.Skip(2).ToList());
            // high
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66925, 45507, 4452), new Angle(-0.72f, 0.69f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66847, 43485, 4452), new Angle(-0.81f, 0.58f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64233, 45523, 4452), new Angle(-0.68f, 0.73f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64265, 43405, 4452), new Angle(-0.57f, 0.82f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63931, 47569, 3922), new Angle(-0.73f, 0.69f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67223, 47566, 3958), new Angle(-0.70f, 0.72f)).Mask(SpawnPlane.Mask_Highground));
            // special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69670, 47600, 4528), new Angle(-0.91f, 0.41f)).Mask(SpawnPlane.Mask_Highground)); // left, wall ac
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65527, 47554, 4578), new Angle(-0.74f, 0.68f)).Mask(SpawnPlane.Mask_Highground)); // above exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62147, 47596, 4528), new Angle(-0.42f, 0.91f)).Mask(SpawnPlane.Mask_Highground)); // right, wall ac
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62521, 46371, 4813), new Angle(-0.45f, 0.89f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68173, 46395, 4813), new Angle(-0.87f, 0.48f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63687, 46332, 3919), new Angle(-0.19f, 0.98f)).Mask(SpawnPlane.Mask_Turret).SetSpawnInfo(
                new TurretSpawnInfo { HorizontalAngle = 0, HorizontalSpeed = 0, VerticalAngle = 13})); // exit platform, right edge, aiming towards middle
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67223, 47611, 3958), new Angle(-0.61f, 0.79f)).Mask(SpawnPlane.Mask_Turret).SetSpawnInfo(
                new TurretSpawnInfo { HorizontalAngle = 20, HorizontalSpeed = 20, VerticalAngle = 10})); // exit platform, left corner, aiming towards middle

            // convert leftovers from shifter points to spawns
            for(int i = 0; i < ShifterPoints_HC_Room9.Count; i++) 
                layout.AddSpawnPlane(new SpawnPlane(ShifterPoints_HC_Room9[i].Item1, ShifterPoints_HC_Room9[i].Item2).Mask(SpawnPlane.Mask_Highground));

            Rooms.Add(layout);
            // ORBS
            layout = new RoomLayout(enemies[0], enemies[1]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64191, 46455, 3975), new Vector3f(66877, 46661, 4971))); // platform near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66823, 46051, 4839), new Vector3f(64457, 45929, 4077))); // otherside of, platform near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66070, 44313, 4916), new Vector3f(65038, 44384, 4494))); // middle platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63718, 42877, 5017), new Vector3f(62457, 42872, 4210))); // right platform 1
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62131, 43059, 5252), new Vector3f(61539, 44061, 4346)).AsVerticalPlane()); // right angled
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62722, 45579, 5004))); // floating after angled billboard, right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67927, 43001, 5004), new Vector3f(68918, 43014, 4288))); // left platform 1
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69213, 43149, 4208), new Vector3f(69708, 43896, 5195)).AsVerticalPlane()); // left angled billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68532, 45440, 5043))); // floating after angled billboard, left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65540, 46293, 5173))); // above exit billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65737, 45430, 4637))); // floating middle
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65562, 44941, 4033))); // behind+below middle platform
            layout.DoNotReuse();
            Rooms.Add(layout);

            //// Room 10 ////
            enemies = room_hc_10.ReturnEnemiesInRoom(AllEnemies); // 2 random froggers without cp
            EnemiesWithoutCP.AddRange(enemies);


            //// Room 11 ////
            enemies = room_hc_11.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[1] = new EnemyDrone(enemies[1]);
            enemies[2] = new EnemyDrone(enemies[2]);
            // shifters
            List<Tuple<Vector3f, Angle>> ShifterPoints_HC_Room11 = new List<Tuple<Vector3f, Angle>>() {
                new Tuple<Vector3f, Angle>(new Vector3f(69315, 75120, 4248), new Angle(-0.99f, 0.14f)), // left platform, tube
                new Tuple<Vector3f, Angle>(new Vector3f(68722, 76074, 4841), new Angle(-0.96f, 0.29f)), // left platform, right fuel tank
                new Tuple<Vector3f, Angle>(new Vector3f(68034, 73794, 5009), new Angle(0.95f, 0.31f)), // left platform, corner
                new Tuple<Vector3f, Angle>(new Vector3f(63124, 73797, 5008), new Angle(0.27f, 0.96f)), // right platform, corner
                new Tuple<Vector3f, Angle>(new Vector3f(61710, 73621, 5884), new Angle(-0.15f, 0.99f)), // higher right platform, fuel tank
                new Tuple<Vector3f, Angle>(new Vector3f(61813, 75136, 4830), new Angle(-0.10f, 0.99f)), // middle right platform, fuel tank
                new Tuple<Vector3f, Angle>(new Vector3f(63678, 76272, 4238), new Angle(-0.60f, 0.80f)), // middle lab tube
                new Tuple<Vector3f, Angle>(new Vector3f(64902, 79940, 3930), new Angle(-0.58f, 0.81f)), // last platform, near light
                new Tuple<Vector3f, Angle>(new Vector3f(63342, 72972, 4139), new Angle(0.37f, 0.93f)), // curved pipes, right platform
                new Tuple<Vector3f, Angle>(new Vector3f(68506, 81078, 4405), new Angle(-0.81f, 0.59f)) // big wall fan, left of exit
            };

            enemies[3].SetEnemyType(Enemy.EnemyTypes.Splitter);
            enemies[4].SetEnemyType(Enemy.EnemyTypes.Splitter);
            enemies[5] = new EnemyShifter(enemies[5]).AddFixedSpawnInfoList(ref ShifterPoints_HC_Room11);
            enemies[6] = new EnemyShifter(enemies[6]).AddFixedSpawnInfoList(ref ShifterPoints_HC_Room11);
            enemies[7].SetEnemyType(Enemy.EnemyTypes.Waver);
            RandomPickEnemiesWithoutCP(ref enemies, enemyIndex: 8); // frogger
            RandomPickEnemiesWithoutCP(ref enemies, enemyIndex: 1); // drone
            // orb
            layout = new RoomLayout(enemies[0]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61380, 77024, 7042))); // high platform, corner lamp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63603, 78283, 6039))); // red billboard thing
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65527, 76519, 4661))); // floating near grapple 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64737, 77600, 7040))); // high angled billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61472, 76436, 4792))); // first billboard edge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61157, 73594, 6778))); // floating high (uplink required)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69729, 73848, 5008))); // left platform, corner near light
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69018, 76433, 3933))); // behind 2 fuel tanks
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63555, 78600, 6978))); // top of red billboard thing
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(58133, 74635, 5803))); // curved wall pipes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68090, 75570, 6830))); // floating lamp (uplink required)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61619, 75551, 3923))); // right platform near light
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62432, 73150, 7645))); // really high floating lamp (uplink required)
            Rooms.Add(layout);

            layout = new RoomLayout(enemies.Skip(1).ToList());
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68463, 72559, 5012), new Vector3f(69263, 73507, 5012), new Angle(-0.95f, 0.32f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62936, 72327, 5008), new Vector3f(62146, 73376, 5015), new Angle(-0.45f, 0.89f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64402, 75111, 3930), new Vector3f(63668, 76000, 3931), new Angle(-0.42f, 0.91f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61719, 76278, 3928), new Vector3f(62557, 75539, 3930), new Angle(-0.02f, 1.00f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67319, 76263, 3940), new Vector3f(68268, 75065, 3940), new Angle(-0.90f, 0.44f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61193, 77061, 6665), new Vector3f(60328, 77915, 6662), new Angle(0.48f, 0.88f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64988, 79901, 3930), new Vector3f(65816, 80537, 3930), new Angle(-0.68f, 0.74f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64764, 80373, 4195), new Angle(-0.65f, 0.76f)).Mask(SpawnPlane.Mask_Highground)); // boxes near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69220, 76076, 4837), new Angle(-0.97f, 0.26f)).Mask(SpawnPlane.Mask_Highground)); // middle left platform, left fuel tank
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68928, 75024, 3940), new Angle(-1.00f, 0.07f)).Mask(SpawnPlane.Mask_Waver)); // left middle platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66102, 81100, 4474), new Angle(-0.67f, 0.75f)).Mask(SpawnPlane.Mask_Waver)); // exit, big lab tube
            // drones
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68848, 78762, 5808), new Vector3f(65102, 76976, 4648), new Angle(-0.71f, 0.70f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(59728, 75353, 5972), new Vector3f(60722, 73567, 5751), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65586, 72986, 5780), new Angle(-0.72f, 0.69f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68912, 73841, 5008), new Angle(0.88f, 0.48f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 30, VerticalAngle = -15})); // left platform, aim to mid
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69542, 76448, 3938), new Angle(0.99f, 0.13f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { 
                    HorizontalAngle = 0, HorizontalSpeed = 0, VerticalAngle = 5, MaxAttackRange = 5000, 
                    MaxAttackRange2 = 5000, VisibleLaserLength = 0 })); // ninja, left platform, behind fuel tanks
            Rooms.Add(layout);




            //// Room 12 ////
            enemies = room_hc_12.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[1] = new EnemyShieldOrb(enemies[1]);
            enemies[2] = new EnemyShieldOrb(enemies[2]);
            enemies.RemoveRange(3, 2); // phantom splitters
            // merge shifter
            var uziList = enemies.Where(x => x.Pos.Z < 4500 && x.enemyType != Enemy.EnemyTypes.ShieldOrb).ToList();
            RandomPickEnemiesWithoutCP(ref enemies, force: true, enemyIndex: enemies.IndexOf(uziList[0]));
            // shifters
            List<Tuple<Vector3f, Angle>> ShifterPoints_HC_Room12 = new List<Tuple<Vector3f, Angle>>() {
                new Tuple<Vector3f, Angle>(new Vector3f(68570, 92463, 5728), new Angle(-0.91f, 0.42f)), // left boxes
                new Tuple<Vector3f, Angle>(new Vector3f(62929, 92504, 5438), new Angle(-0.56f, 0.83f)), // right lab tube far
                new Tuple<Vector3f, Angle>(new Vector3f(63443, 87191, 5438), new Angle(-0.05f, 1.00f)), // right lab tube near
                new Tuple<Vector3f, Angle>(new Vector3f(65849, 92112, 3929), new Angle(-0.71f, 0.70f)), // near exit door
                //new Tuple<Vector3f, Angle>(new Vector3f(70095, 92133, 5423), new Angle(-0.99f, 0.13f)), // left wall, vertical lab tubes
                new Tuple<Vector3f, Angle>(new Vector3f(68972, 89562, 6102), new Angle(-0.91f, 0.41f)), // left billboard
                new Tuple<Vector3f, Angle>(new Vector3f(62620, 89512, 6102), new Angle(-0.45f, 0.89f)), // right billboard
                new Tuple<Vector3f, Angle>(new Vector3f(66666, 86979, 6041), new Angle(0.52f, 0.85f)), // first billboard
                new Tuple<Vector3f, Angle>(new Vector3f(64991, 86946, 6041), new Angle(0.59f, 0.80f)), // first billboard
                new Tuple<Vector3f, Angle>(new Vector3f(66932, 92922, 4238), new Angle(-0.87f, 0.49f)), // left of door, lab tube
                new Tuple<Vector3f, Angle>(new Vector3f(64686, 92935, 4238), new Angle(-0.62f, 0.79f)) // right of door, lab tube
            };
            AttachToGroup((Shifters[0] as EnemyShifter).AddFixedSpawnInfoList(ref ShifterPoints_HC_Room12), enemies[3]);
            enemies.Add(Shifters[0]); // that that new shifter to layout
            // orbs
            layout = new RoomLayout(enemies.Take(3).ToList());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64962, 87240, 5401), new Vector3f(66723, 87228, 5793)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68700, 89054, 5203), new Vector3f(68621, 90505, 5932)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62875, 89031, 5071), new Vector3f(62849, 90559, 5839)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64740, 92629, 5015), new Vector3f(66894, 92725, 5779)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66593, 90774, 4890), new Vector3f(64757, 92405, 5041)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62035, 90652, 5543))); // behind billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69411, 88848, 5385))); // behind billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65761, 86952, 6041))); // top of middle billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67168, 89726, 6669))); // floating left side (grapple)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64016, 89865, 6788))); // floating right side (grapple)
            Rooms.Add(layout);
            // enemies
            layout = new RoomLayout(enemies.Skip(3).ToList());
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63380, 88611, 5128), new Vector3f(64373, 87322, 5127), new Angle(-0.59f, 0.81f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67351, 88574, 5128), new Vector3f(68716, 87183, 5128), new Angle(-0.93f, 0.36f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68524, 91076, 5128), new Vector3f(67246, 92134, 5128), new Angle(-0.92f, 0.39f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64295, 91056, 5128), new Vector3f(62940, 92279, 5128), new Angle(-0.53f, 0.85f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66878, 92798, 3937), new Vector3f(64797, 87166, 3929), new Angle(-0.72f, 0.69f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(4).AllowSplitter());
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62782, 89057, 3929), new Angle(0.25f, 0.97f)).Mask(SpawnPlane.Mask_Waver)); // right corner
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62921, 91339, 5872), new Angle(-0.48f, 0.88f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68466, 91370, 5721), new Angle(-0.92f, 0.39f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68519, 88990, 3929), new Angle(0.97f, 0.25f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 20, VerticalAngle = 10})); // left corner
            Rooms.Add(layout);



            //// Room 13 ////
            enemies = room_hc_13.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[1] = new EnemyShieldOrb(enemies[1]);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[4].SetEnemyType(Enemy.EnemyTypes.Waver);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, enemyIndex: 2); // shielder
            // shifter points
            List<Tuple<Vector3f, Angle>> ShifterPoints_HC_Room13 = new List<Tuple<Vector3f, Angle>>() {
                new Tuple<Vector3f, Angle>(new Vector3f(68724, 110636, 3641), new Angle(-0.98f, 0.19f)), // left billboard, near
                new Tuple<Vector3f, Angle>(new Vector3f(68722, 113230, 3641), new Angle(-0.99f, 0.16f)), // left billboard, far
                new Tuple<Vector3f, Angle>(new Vector3f(62289, 111253, 3641), new Angle(-0.30f, 0.95f)), // right billboard, near
                new Tuple<Vector3f, Angle>(new Vector3f(62321, 113313, 3641), new Angle(-0.46f, 0.89f)), // right billboard, far
                new Tuple<Vector3f, Angle>(new Vector3f(65105, 115581, 3762), new Angle(-0.73f, 0.68f)), // above exit door
            };
            AttachToGroup((Shifters[1] as EnemyShifter).AddFixedSpawnInfoList(ref ShifterPoints_HC_Room13), enemies[3]);
            enemies.Add(Shifters[1]); // that that new shifter to layout

            // orbs
            layout = new RoomLayout(enemies[0], enemies[1]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62647, 110674, 3477), new Vector3f(62515, 113659, 2825))); // right billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68391, 113922, 3412), new Vector3f(68407, 110799, 2910))); // left billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63526, 115181, 3361), new Vector3f(67327, 115363, 2956))); // across back wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69069, 113995, 3360), new Vector3f(69021, 111018, 2846))); // behind left billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62030, 111042, 2846), new Vector3f(62012, 113709, 3286))); // behind right billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65546, 112250, 4103))); // floating middle
            Rooms.Add(layout);

            // enemies
            layout = new RoomLayout(enemies.Skip(2).ToList());
            // reuse leftovers from shifter
            for(int i = 0; i < ShifterPoints_HC_Room13.Count; i++) 
                layout.AddSpawnPlane(new SpawnPlane(ShifterPoints_HC_Room13[i].Item1, ShifterPoints_HC_Room13[i].Item2).Mask(SpawnPlane.Mask_Highground));
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68435, 115284, 2832), new Vector3f(67608, 114445, 2832), new Angle(-0.85f, 0.53f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65073, 114635, 2818), new Vector3f(65994, 115471, 2818), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62489, 115320, 2868), new Vector3f(63353, 114402, 2832), new Angle(-0.64f, 0.77f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63261, 109166, 2832), new Vector3f(62570, 109915, 2832), new Angle(0.64f, 0.77f)).Mask(SpawnPlane.Mask_Waver));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68398, 109915, 2832), new Vector3f(67742, 109052, 2847), new Angle(-1.00f, 0.01f)).Mask(SpawnPlane.Mask_Waver));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65169, 112771, 2847), new Vector3f(65869, 112132, 2847), new Angle(-0.69f, 0.72f)).Mask(SpawnPlane.Mask_Waver));
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66911, 108747, 3430), new Angle(0.92f, 0.39f)).Mask(SpawnPlane.Mask_Highground)); // left near billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64149, 108794, 3440), new Angle(0.24f, 0.97f)).Mask(SpawnPlane.Mask_Highground)); // right near billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67704, 115583, 3762), new Angle(-0.73f, 0.68f)).Mask(SpawnPlane.Mask_Highground)); // left far platform, high
            //// additional (drones)
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68214, 114355, 3922), new Vector3f(66615, 110987, 3772), new Angle(-0.75f, 0.66f)).Mask(SpawnPlane.Mask_Airborne));
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62804, 114232, 3772), new Vector3f(64265, 111098, 3960), new Angle(-0.68f, 0.73f)).Mask(SpawnPlane.Mask_Airborne));
            //// additional (turrets)
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67709, 110027, 2832), new Angle(0.84f, 0.55f)).Mask(SpawnPlane.Mask_Turret)
            //    .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 30, VerticalAngle = 5})); // left platform, aim to middle
            layout.DoNotReuse(); // prevent froggers from spawning 
            Rooms.Add(layout);



            //// Room 14 ////
            enemies = room_hc_14.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyDrone(enemies[0]);
            enemies[1].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies.ForEach(x => {
                x.DisableAttachedCP(GameHook.game);
                EnemiesWithoutCP.Add(x);
            });


            //// Room 15 ////
            enemies = room_hc_15.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyTurret(enemies[0]);
            enemies[1] = new EnemyTurret(enemies[1]);
            enemies[2] = new EnemyShieldOrb(enemies[2]);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[4].SetEnemyType(Enemy.EnemyTypes.Weeb);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, enemyIndex: 4); // useless weeb
            // orbs
            layout = new RoomLayout(enemies[2]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66107, 151942, 7163), new Vector3f(63333, 150353, 6512)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66670, 149275, 8280), new Vector3f(64200, 152641, 8777)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69796, 151359, 6427)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61019, 154629, 8741)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67395, 149269, 5323)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62828, 152264, 6850)));
            Rooms.Add(layout);
            // enemies
            enemies.RemoveAt(2);// orb
            layout = new RoomLayout(enemies);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63753, 145688, 5013), new Angle(-0.46f, 0.89f)).Mask(SpawnPlane.Mask_Waver));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(60657, 151158, 7432), new Angle(-0.54f, 0.84f)).Mask(SpawnPlane.Mask_Waver));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62539, 152708, 7422), new Angle(-0.97f, 0.26f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64839, 155444, 2836), new Vector3f(66558, 154010, 2834), new Angle(-0.72f, 0.70f)).Mask(SpawnPlane.Mask_Highground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66762, 153434, 2838), new Angle(0.88f, 0.48f)).Mask(SpawnPlane.Mask_Turret)
                 .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 10, HorizontalSpeed = 10, VerticalAngle = 10 }));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63521, 147269, 5010), new Angle(0.59f, 0.81f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 20, HorizontalSpeed = 10, VerticalAngle = 0 }));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62790, 151282, 7433), new Angle(0.22f, 0.97f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, HorizontalSpeed = 1, VerticalAngle = -10 }));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64178, 153454, 2839), new Angle(-0.07f, 1.00f)).Mask(SpawnPlane.Mask_Turret)
                 .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, HorizontalSpeed = 1, VerticalAngle = 50 }));
            Rooms.Add(layout);







            ////// Room 16 ////
            enemies = room_hc_16.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyTurret(enemies[0]);
            enemies[1] = new EnemyTurret(enemies[1]);
            enemies[2] = new EnemyTurret(enemies[2]);
            enemies[3] = new EnemyShieldOrb(enemies[3]);
            enemies[4] = new EnemyShieldOrb(enemies[4]);
            enemies[6].SetEnemyType(Enemy.EnemyTypes.Waver);
            // orbs
            layout = new RoomLayout(enemies[3], enemies[4]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61688, 162175, 10110), new Vector3f(66052, 164058, 10747)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67979, 163064, 8119)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(69546, 165872, 5281), new Vector3f(67205, 164727, 5842)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64362, 166665, 4947), new Vector3f(66265, 167603, 5283)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62885, 164813, 4350)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68057, 168284, 5283)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61692, 166376, 5954)));
            Rooms.Add(layout);

            // enemies
            enemies.RemoveRange(3, 2); // remove orbs
            // the only turret
            RandomPickEnemiesWithoutCP(ref enemies, force: true, enemyIndex:
                enemies.IndexOf(enemies.Where(x => EnemyInApproximetry(x.Pos, new Vector3f(63444, 166405, 4954))).First()));

            layout = new RoomLayout(enemies);
            // high/special (no weeb nor splitters)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62449, 167604, 5012), new Angle(-0.56f, 0.83f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67878, 167396, 5423), new Angle(-0.99f, 0.17f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67330, 167921, 5021), new Angle(-0.98f, 0.21f)).Mask(SpawnPlane.Mask_Waver));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67131, 163834, 6928), new Angle(0.87f, 0.49f)).Mask(SpawnPlane.Mask_Waver));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64473, 162535, 8033), new Angle(0.05f, 1.00f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62500, 163566, 8007), new Angle(-0.22f, 0.97f)).Mask(SpawnPlane.Mask_Highground));
            // turrets
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62739, 167811, 5012), new Angle(0.50f, 0.87f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 30, VerticalAngle = 10}));

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68200, 167899, 5021), new Angle(0.96f, 0.30f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 40, HorizontalSpeed = 20, VerticalAngle = 0 }));

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68785, 166799, 5021), new Angle(-0.84f, 0.55f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 10, HorizontalSpeed = 20, VerticalAngle = 30 }));

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68011, 166930, 5423), new Angle(-0.72f, 0.70f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 20, HorizontalSpeed = 10, VerticalAngle = 20 }));

            Rooms.Add(layout);

            //// ChainedOrb Rooms ////
            chainedOrbs_Rooms.Add(new RoomLayout() { });
            // first door
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65362, -28001, 3209), new Vector3f(65341, -24767, 2489)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65388, -23651, 2272), new Vector3f(66080, -23899, 2272), new Angle(-0.71f, 0.70f)).Mask(SpawnPlane.Mask_Highground));
            chainedOrbs_Rooms.Add(layout);

            // double drones room, middle/start
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65585, -20526, 3592)).Mask(SpawnPlane.Mask_ShieldOrb)); // above spawn
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65553, -14464, 3402), new Angle(-0.65f, 0.76f)).Mask(SpawnPlane.Mask_Highground)); // after laser
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66400, -14340, 3635), new Angle(-0.81f, 0.58f)).Mask(SpawnPlane.Mask_Highground)); // after laser box
            chainedOrbs_Rooms.Add(layout);

            // double drones room, before laser
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66920, -5390, 3264)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64880, -4216, 2933), new Angle(-0.60f, 0.80f)).Mask(SpawnPlane.Mask_Highground)); // ledge
            chainedOrbs_Rooms.Add(layout);

            // room 10
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65409, 65037, 6014)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66415, 66857, 3930), new Angle(-0.79f, 0.62f)).Mask(SpawnPlane.Mask_Highground));
            chainedOrbs_Rooms.Add(layout);

            // before room 13
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68032, 105679, 4197)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65546, 105917, 2862), new Angle(-0.73f, 0.69f)).Mask(SpawnPlane.Mask_Highground));
            chainedOrbs_Rooms.Add(layout);

            // lazer maze
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63073, 137794, 3382)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64077, 138496, 2839), new Angle(-0.22f, 0.98f)).Mask(SpawnPlane.Mask_Highground));
            chainedOrbs_Rooms.Add(layout);

            // before elevator
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65569, 173222, 3959)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65534, 174054, 2841), new Angle(-0.74f, 0.68f)).Mask(SpawnPlane.Mask_Highground));
            chainedOrbs_Rooms.Add(layout);


            //// EXTRA
            layout = new RoomLayout();
            // laser maze
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(72230, 138533, 4707), new Angle(1.00f, 0.00f)).Mask(SpawnPlane.Mask_Highground));
            Rooms.Add(layout);

        }

        public override void RandomizeEnemies(Process game) {
            base.RandomizeEnemies(game);
            ChainedOrb.Randomize(ref chainedOrbs, ref chainedOrbs_Rooms);
        }

        public void Gen_Nightmare() {
            throw new NotImplementedException();
        }


        protected override void Gen_PerRoom() {}
    }
}
