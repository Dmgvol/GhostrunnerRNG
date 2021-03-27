using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
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
        private Room room_8 = new Room(new Vector3f(72681, 82112, 9681), new Vector3f(58056, 94438, -845));     // 2 froggers, 2 splitters, 2 shield orbs
        private Room room_9 = new Room(new Vector3f(58722, 106611, 6877), new Vector3f(72511, 116787, 1296));   // 2 splitters, 2 pistols, 4 drones
        private Room room_10 = new Room(new Vector3f(60555, 134146, 6617), new Vector3f(75583, 141394, 1517));  // 4 uzi, 2 drones

        #endregion


        public TYWB(bool isHc) : base(GameUtils.MapType.TYWB) {
            if(!isHc) {
                Gen_PerRoom();
            }
        }
        protected override void Gen_PerRoom() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game, 0, 34);
            AllEnemies.AddRange(GetAllEnemies(GameHook.game, 38, 28));

            Rooms = new List<RoomLayout>();
            RoomLayout layout;
            List<Enemy> enemies;


            //// Room 1 - 14 spiders ////
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

            Rooms.Add(layout);



            // Room 4 - 5 weebs////
            enemies = room_4.ReturnEnemiesInRoom(AllEnemies);
            TakeLastEnemyFromCP(ref enemies, force: true, attachedDoor: true, removeCP:true);
            layout = new RoomLayout(enemies);

            //////////////////////////////////////////////////   No area to rng here, need to separate enemies from attached door

            Rooms.Add(layout);


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
            Rooms.Add(layout);

            //// Room 6 - 1 splitter////
            //enemies = room_6.ReturnEnemiesInRoom(AllEnemies);
            //enemies[0].DisableAttachedCP(GameHook.game);
            //layout = new RoomLayout(enemies);
            //////layout.AddSpawnPlane(new SpawnPlane(new Vector3f(68021, 66760, 3922), new Angle(-0.62f, 0.78f)));
            ////layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65544, 66764, 3923), new Angle(-0.68f, 0.74f)));

            //////////////////////////////////////////////////////   need RNG ideas

            //layout.DoNotReuse();
            //Rooms.Add(layout);


            ////// Room 7 - 1 splitter////
            //enemies = room_7.ReturnEnemiesInRoom(AllEnemies);
            //layout = new RoomLayout(enemies);
            //Rooms.Add(layout);


        }
    }
}
