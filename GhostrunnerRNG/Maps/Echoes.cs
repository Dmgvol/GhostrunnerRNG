using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.NonPlaceableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using static GhostrunnerRNG.Enemies.Enemy;

namespace GhostrunnerRNG.Maps {
    class Echoes : MapCore {

        #region Rooms
        private Room room_1 = new Room(new Vector3f(68082, 1155, 6559), new Vector3f(66580, 6304, 5099)); // first weeb, block tutorial (needed for blocking)
        private Room room_2 = new Room(new Vector3f(61147, 6086, 6481), new Vector3f(57778, 4347, 4472)); // pistol + weeb (no cp)
        private Room room_3 = new Room(new Vector3f(53423, -427, 4668), new Vector3f(49174, 2368, 6411)); // 2 weebs room
        private Room room_4 = new Room(new Vector3f(44735, 3411, 4682), new Vector3f(41207, -464, 7571)); // 1 pistol, 2 weebs
        private Room room_5 = new Room(new Vector3f(32198, 2556, 4984), new Vector3f(34345, 583, 7347)); // drone
        private Room room_6 = new Room(new Vector3f(8962, 4035, 6483), new Vector3f(5371, -793, 8861));  // 1drone + 2 weebs + 2pistol
        private Room room_7 = new Room(new Vector3f(3330, -631, 4637), new Vector3f(-651, -4311, 6539)); // 2 froggers+shield
        private Room room_8 = new Room(new Vector3f(-1563, -4335, 3719), new Vector3f(526, -2748, 4636)); // 2 pistols, not needed for cp 
        private Room room_9 = new Room(new Vector3f(3109, -3003, 3498), new Vector3f(2001, -790, 3018)); // 2 pistols, needed for cp  
        private Room room_10 = new Room(new Vector3f(-742, -920, 3610), new Vector3f(-3990, -4840, 2474)); // gecko+2pistol, needed for cp(door)
        private Room room_11 = new Room(new Vector3f(-3056, 5857, 4605), new Vector3f(-6611, -7436, 5667)); // 3pistols+2geckos, needed for cp 
        private Room room_12 = new Room(new Vector3f(-10797, -70, 6324), new Vector3f(-8481, -5638, 7258)); // needed for cp(door)  pistol+uzi+weeb
        private Room room_13 = new Room(new Vector3f(-11031, -2223, 6397), new Vector3f(-15296, -6373, 7321)); // pistol+weeb+gecko, needed for cp
        private Room room_14 = new Room(new Vector3f(-18894, 1069, 7240), new Vector3f(-17870, -3686, 5812)); // not needed for cp, 2 drones
        private Room room_15 = new Room(new Vector3f(-11119, 1771, 7208), new Vector3f(-11776, 6684, 7825)); // not needed for cp  	pisotol+weeb

        //private Room room_16 = new Room(new Vector3f(-7290, 16929, 5782), new Vector3f(-4002, 12391, 7278));    // not needed for cp  	2pistol+gecko
        //private Room room_17 = new Room(new Vector3f(-4102, 12484, 7235), new Vector3f(-364, 16537, 8548));     //  needed for cp       pistol+uzi+weeb 
        //private Room room_18 = new Room(new Vector3f(-3863, 18435, 8153), new Vector3f(-7383, 16880, 9168));    // needed for door	    shield+uzi
        private Room room_16To18 = new Room(new Vector3f(56, 11109, 4833), new Vector3f(-7312, 18149, 9400));   //needed for door	    shield+uzi

        private Room room_19 = new Room(new Vector3f(-7863, 19199, 7880), new Vector3f(-11297, 24985, 9795));   // not needed for cp    frogger+shield+weeb
        private Room room_20 = new Room(new Vector3f(-14634, 12318, 9027), new Vector3f(-13233, 10939, 9992));  // needed for cp  		sniper
        private Room room_21 = new Room(new Vector3f(-28495, 12336, 9331), new Vector3f(-29925, 11110, 10558)); // needed for cp  		sniper
        private Room room_22 = new Room(new Vector3f(-31159, -6124, 9887), new Vector3f(-27606, -15533, 11226));// not needed for cp 	4 pistols
        private Room room_23 = new Room(new Vector3f(-30257, -16613, 9826), new Vector3f(-28760, -19291, 10756)); //needed for cp 		sniper
        private Room room_24 = new Room(new Vector3f(-26688, -29409, 6924), new Vector3f(-31551, -19573, 10826)); //3 drones
        #endregion

        public Echoes(bool isHc) : base(GameUtils.MapType.Echoes) {
            if(!isHc) {
                Gen_PerRoom();
            }
        }
        protected override void Gen_PerRoom() {
            //indexes from 0 to 61 without 30 - 31

            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game, 0, 30);
            AllEnemies.AddRange(GetAllEnemies(GameHook.game, 32, 30));

            Rooms = new List<RoomLayout>();
            RoomLayout layout;
            List<Enemy> enemies;

            //// Room 1 - single Weeb ////
            enemies = room_1.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67382, 2918, 5600), new Vector3f(66919, 4177, 5600), new Angle(-0.70f, 0.72f)).DoNotReuse());
            Rooms.Add(layout);

            //// Room 2 - Pistol + Weeb ////
            enemies = room_2.ReturnEnemiesInRoom(AllEnemies);
            enemies[1].SetEnemyType(EnemyTypes.Weeb);
            layout = new RoomLayout(enemies);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(60554, 5676, 4950), new Vector3f(58381, 4999, 4950), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Flatground)); // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(58283, 1196, 6048), new Angle(0.70f, 0.72f)).Mask(SpawnPlane.Mask_Highground)); // before slide
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53208, 577, 4949), new Angle(0.94f, 0.33f)).Mask(SpawnPlane.Mask_Flatground)); // double weeb room, corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50498, 521, 5248), new Angle(0.10f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // double weeb room, box
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(58031, 2414, 6177), new Angle(0.60f, 0.80f)).Mask(SpawnPlane.Mask_Highground)); // billboard
            Rooms.Add(layout);

            //// Room 3 - 2 Weebs
            enemies = room_3.ReturnEnemiesInRoom(AllEnemies);
            enemies[0].SetEnemyType(EnemyTypes.Weeb);
            enemies[1].SetEnemyType(EnemyTypes.Weeb);

            // use 2'nd weeb for whole map rng?
            if(Config.GetInstance().r.Next(2) == 1) {
                layout = new RoomLayout(enemies[0]);
                enemies[1].DisableAttachedCP(GameHook.game);
                EnemiesWithoutCP.Add(enemies[1]);
            } else {
                layout = new RoomLayout(enemies);
            }

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49854, 769, 4949), new Vector3f(51388, 2058, 4929), new Angle(0.04f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51553, 882, 4929), new Vector3f(52773, 1672, 4950), new Angle(-0.01f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            Rooms.Add(layout);

            //// Room 4 - 2 weebs 1 pistol
            enemies = room_4.ReturnEnemiesInRoom(AllEnemies);
            enemies[1].SetEnemyType(EnemyTypes.Weeb);
            enemies[2].SetEnemyType(EnemyTypes.Weeb);
            layout = new RoomLayout(enemies);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(43626, 2189, 5548), new Vector3f(42124, 154, 5548), new Angle(-0.02f, 1.00f)).SetMaxEnemies(3).Mask(SpawnPlane.Mask_Flatground)); // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42388, 2951, 5794), new Angle(-0.47f, 0.88f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // top of AC units, left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41413, 1543, 6175), new Angle(-0.04f, 1.00f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // top of lamp pole, front
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(44299, -102, 6175), new Angle(0.78f, 0.63f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // top of lamp pole, right
            Rooms.Add(layout);

            //// Room 5 - drone
            enemies = room_5.ReturnEnemiesInRoom(AllEnemies);
            enemies[0].SetEnemyType(EnemyTypes.Drone);
            layout = new RoomLayout(enemies);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36430, 1597, 6691), new Vector3f(34299, 1045, 6098), new Angle(0.00f, 1.00f))); // between billboards
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(33612, 1372, 6098), new Vector3f(26799, 1372, 6098), new Angle(0.00f, 1.00f)).AsVerticalPlane()); // along side of zipline
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22119, 1816, 6915), new Angle(-0.15f, 0.99f)));    // top slide
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(16998, 1612, 6486), new Angle(-0.09f, 1.00f)));    // after slide
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(45207, 1376, 6325), new Angle(-0.01f, 1.00f)));    // before Room 4
            layout.Mask(SpawnPlane.Mask_Airborne);
            Rooms.Add(layout);

            //// Room 6 ////
            enemies = room_6.ReturnEnemiesInRoom(AllEnemies);
            enemies[0].SetEnemyType(EnemyTypes.Drone);
            enemies[3].SetEnemyType(EnemyTypes.Weeb);
            enemies[4].SetEnemyType(EnemyTypes.Weeb);

            layout = new RoomLayout(enemies);

            // drone spawns
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8953, 2101, 7180), new Vector3f(7193, 638, 7180), new Angle(0.01f, 1.00f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(7149, 3363, 7191), new Angle(-0.20f, 0.98f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(9742, 1319, 7009), new Angle(-1.00f, 0.01f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(7177, -479, 7235), new Angle(0.45f, 0.89f)).Mask(SpawnPlane.Mask_Airborne));
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(4792, 1362, 6984), new Angle(0.06f, 1.00f)).Mask(SpawnPlane.Mask_Airborne)); // Drone Lerp fix needed

            // default grounded - pistols and weebs
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8736, 3667, 6598), new Vector3f(6937, 2311, 6598), new Angle(0.04f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(5668, 2505, 6598), new Vector3f(6762, 333, 6598), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(7168, -901, 6628), new Vector3f(8672, 452, 6608), new Angle(-0.01f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(4532, 2324, 6598), new Vector3f(4913, 405, 6598), new Angle(0.02f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));

            // high places - only for pistols
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8778, -987, 6898), new Angle(0.44f, 0.90f)).Mask(SpawnPlane.Mask_Highground)); // red crates
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8145, -908, 7170), new Angle(0.81f, 0.58f)).Mask(SpawnPlane.Mask_Highground)); // rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(5902, 600, 7448), new Angle(0.11f, 0.99f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // pipes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(3474, 2483, 6953), new Angle(-0.37f, 0.93f)).Mask(SpawnPlane.Mask_HighgroundLimited)); ; // small corner concrete pillar 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(3475, 300, 6999), new Angle(0.28f, 0.96f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // small corner concrete pillar  2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(5570, 1873, 6997), new Angle(-0.07f, 1.00f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // city lights sign
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6746, 413, 7223), new Angle(0.08f, 1.00f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // light post
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(5532, 2314, 7306), new Angle(-0.01f, 1.00f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // thin ad sign
            Rooms.Add(layout);


            //// Room 7 ////
            enemies = room_7.ReturnEnemiesInRoom(AllEnemies);
            enemies.ForEach(x => x.DisableAttachedCP(GameHook.game)); // remove all CP

            // take random enemy to EnemiesWithoutCP?
            if(Config.GetInstance().r.Next(2) == 1) {
                int index = Config.GetInstance().r.Next(enemies.Count); // pick random
                enemies[index].DisableAttachedCP(GameHook.game);
                EnemiesWithoutCP.Add(enemies[index]); // add to list
                enemies.RemoveAt(index); // remove from enemy list
            }

            layout = new RoomLayout(enemies);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(3069, -2386, 6598), new Angle(0.71f, 0.71f)).Mask(SpawnPlane.Mask_Flatground)); // blocking hallway
            // rooftops
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(2671, -1952, 5493), new Angle(1.00f, 0.00f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(1634, -1430, 5546), new Vector3f(1082, -1853, 5548), new Angle(-0.10f, 0.99f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-188, -3220, 5546), new Vector3f(215, -2778, 5546), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(767, -4188, 5612), new Vector3f(1676, -3777, 5606), new Angle(0.57f, 0.82f)).Mask(SpawnPlane.Mask_Highground));
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(2897, -2398, 5093), new Vector3f(504, -3009, 5093), new Angle(0.02f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(1792, -1641, 5093), new Vector3f(3054, -734, 5103), new Angle(-0.74f, 0.68f)).Mask(SpawnPlane.Mask_Flatground));
            // special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1252, -573, 4898), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground)); // blocking next hallway (after rotating billboard)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(1655, -1278, 6382), new Angle(-0.50f, 0.87f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // edge of high rooftop
            Rooms.Add(layout);

            //// Room 8 (8, 9, 10) ////
            enemies = room_8.ReturnEnemiesInRoom(AllEnemies);
            enemies.AddRange(room_9.ReturnEnemiesInRoom(AllEnemies));
            enemies.AddRange(room_10.ReturnEnemiesInRoom(AllEnemies));
            enemies[6].SetEnemyType(EnemyTypes.Waver);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-838, -3826, 4097), new Vector3f(-1459, -3003, 4098), new Angle(0.71f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(369, -3017, 4098), new Vector3f(-160, -4230, 4098), new Angle(0.68f, 0.74f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(1195, -844, 3847), new Vector3f(596, -1470, 3848), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(2142, -2856, 3498), new Vector3f(2823, -992, 3507), new Angle(-1.00f, 0.01f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1027, -2474, 3200), new Vector3f(-1940, -1659, 3200), new Angle(0.02f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3302, -4189, 3097), new Vector3f(-1518, -5038, 3098), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3228, -2732, 2697), new Vector3f(-3792, -1945, 2697), new Angle(-0.68f, 0.73f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(1672, -1207, 2898), new Vector3f(569, -2008, 2898), new Angle(-0.31f, 0.95f)).Mask(SpawnPlane.Mask_Flatground));
            // special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3699, -4854, 3981), new Angle(0.41f, 0.91f)).Mask(SpawnPlane.Mask_Highground)); // fuel tank
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1979, -2771, 4300), new Angle(-0.91f, 0.42f)).Mask(SpawnPlane.Mask_Highground)); // secret ledge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(3234, -1710, 4325), new Angle(-1.00f, 0.09f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3949, -3447, 3567), new Angle(0.19f, 0.98f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // billboard 2
            Rooms.Add(layout);

            //// Room 11 ////
            enemies = room_11.ReturnEnemiesInRoom(AllEnemies);
            enemies[4].SetEnemyType(EnemyTypes.Waver);
            enemies[3].SetEnemyType(EnemyTypes.Waver);
            RandomPickEnemiesWithoutCP(ref enemies);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4335, -6861, 5100), new Vector3f(-4846, -4837, 5099), new Angle(-0.71f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6273, -3461, 5109), new Vector3f(-4781, -3895, 5109), new Angle(-0.70f, 0.72f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6269, -1634, 5109), new Vector3f(-5078, -2030, 5098), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3849, 2017, 5134), new Vector3f(-3241, -2, 5103), new Angle(-0.68f, 0.73f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-5722, 4486, 5298), new Vector3f(-4441, 3998, 5314), new Angle(-0.47f, 0.88f)).Mask(SpawnPlane.Mask_Flatground));
            // high places / special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4949, -7687, 5789), new Angle(-0.32f, 0.95f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // edge of billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3940, -5977, 5698), new Angle(-0.75f, 0.66f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-5700, 2669, 6083), new Angle(-0.64f, 0.77f)).Mask(SpawnPlane.Mask_Highground)); // big pipe
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3413, -1168, 5721), new Angle(1.00f, 0.01f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8844, -3760, 5823), new Angle(-0.01f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // far neon sign
            Rooms.Add(layout);


            //// Room 12 ////
            enemies = room_12.ReturnEnemiesInRoom(AllEnemies);
            enemies[2].SetEnemyType(EnemyTypes.Weeb);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-10621, 39, 6648), new Vector3f(-10038, -1546, 6648), new Angle(0.48f, 0.88f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8852, -126, 6657), new Vector3f(-9479, -1487, 6648), new Angle(-1.00f, 0.10f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-10052, -1672, 6648), new Vector3f(-9537, -2974, 6648), new Angle(0.50f, 0.87f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9537, -2974, 6648), new Vector3f(-9156, -4447, 6648), new Angle(0.73f, 0.69f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9877, -5416, 6648), new Vector3f(-9100, -4948, 6652), new Angle(0.35f, 0.94f)).Mask(SpawnPlane.Mask_Flatground));
            // high places/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9561, -4678, 6948), new Angle(0.53f, 0.85f)).Mask(SpawnPlane.Mask_Highground)); // crates
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9739, -3340, 6854), new Angle(0.02f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // crates
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9238, -2636, 6854), new Angle(0.75f, 0.67f)).Mask(SpawnPlane.Mask_Highground)); // creates
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-10705, -88, 7446), new Angle(-0.19f, 0.98f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // AC unit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-10169, -3894, 7871), new Angle(-0.02f, 1.00f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // neon sign above door
            Rooms.Add(layout);

            //// Room 13 ////
            enemies = room_13.ReturnEnemiesInRoom(AllEnemies);
            enemies[1].SetEnemyType(EnemyTypes.Waver);
            enemies[2].SetEnemyType(EnemyTypes.Weeb);
            RandomPickEnemiesWithoutCP(ref enemies);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11384, -2504, 6648), new Vector3f(-11749, -3547, 6648), new Angle(-0.08f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11240, -5003, 6648), new Vector3f(-15168, -5517, 6672), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-13519, -2605, 6648), new Vector3f(-14403, -4879, 6648), new Angle(-0.01f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            // high places
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11039, -4770, 7119), new Angle(1.00f, 0.03f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-12506, -4512, 7688), new Vector3f(-13114, -3970, 7689), new Angle(-0.15f, 0.99f)).AsVerticalPlane().Mask(SpawnPlane.Mask_HighgroundLimited));
            Rooms.Add(layout);

            //// Room 14 ////
            enemies = room_14.ReturnEnemiesInRoom(AllEnemies);
            enemies[0].SetEnemyType(EnemyTypes.Drone);
            enemies[1].SetEnemyType(EnemyTypes.Drone);
            RandomPickEnemiesWithoutCP(ref enemies, force: true); // force take 1
            layout = new RoomLayout(enemies);
            // a few around that section
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-14290, -5436, 7671), new Angle(0.32f, 0.95f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11828, 1427, 8034), new Angle(-1.00f, 0.10f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-16808, -5584, 7055), new Angle(0.07f, 1.00f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);

            //// Room 15 ////
            enemies = room_15.ReturnEnemiesInRoom(AllEnemies);
            enemies[1].SetEnemyType(EnemyTypes.Weeb);
            RandomPickEnemiesWithoutCP(ref enemies);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11757, 6250, 7498), new Vector3f(-11286, 2389, 7498), new Angle(-0.71f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11667, 7366, 7498), new Vector3f(-11356, 8202, 7498), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_Highground));
            // special/high places
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11371, 853, 7946), new Angle(0.96f, 0.28f)).Mask(SpawnPlane.Mask_Highground)); // on big pipe, prior to hallway
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8685, 10050, 8889), new Angle(-0.96f, 0.28f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // 2'nd billboard after hallway
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8803, 16125, 7073), new Angle(-0.71f, 0.70f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // on fence, before next room
            Rooms.Add(layout);


            //// Room 16, 17, 18 ////
            enemies = room_16To18.ReturnEnemiesInRoom(AllEnemies);
            enemies[6].SetEnemyType(EnemyTypes.Waver);
            enemies[7].SetEnemyType(EnemyTypes.Weeb);

            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6207, 16759, 6098), new Vector3f(-3631, 15172, 6098), new Angle(1.00f, 0.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6119, 14141, 6110), new Vector3f(-6836, 12901, 6098), new Angle(0.70f, 0.72f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-5603, 12698, 6098), new Vector3f(-4698, 13824, 6098), new Angle(0.74f, 0.67f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4123, 13252, 6703), new Vector3f(-5478, 12870, 6703), new Angle(0.70f, 0.72f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6282, 12780, 6700), new Vector3f(-6977, 13972, 6700), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2962, 16111, 7607), new Vector3f(-1974, 16001, 7598), new Angle(-0.32f, 0.95f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2319, 15705, 7598), new Vector3f(-1475, 14755, 7598), new Angle(-0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1123, 14418, 7598), new Vector3f(-1528, 12962, 7598), new Angle(-1.00f, 0.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1717, 13111, 7598), new Vector3f(-3423, 12623, 7598), new Angle(1.00f, 0.02f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2152, 14061, 7598), new Vector3f(-3748, 13630, 7622), new Angle(0.68f, 0.73f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1620, 14276, 8201), new Vector3f(-1135, 12959, 8199), new Angle(0.94f, 0.35f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4764, 18093, 8498), new Vector3f(-5684, 17119, 8498), new Angle(-0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6147, 17993, 8498), new Vector3f(-7086, 16994, 8503), new Angle(-0.69f, 0.73f)).Mask(SpawnPlane.Mask_Flatground));
            // special/high places
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-7164, 14514, 7092), new Angle(0.29f, 0.96f)).Mask(SpawnPlane.Mask_Highground)); // red crate
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-7218, 15793, 7246), new Angle(-0.05f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // entrance arc
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-7349, 15766, 7921), new Angle(-0.02f, 1.00f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // billboard above entrance
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6330, 16972, 7598), new Angle(-0.47f, 0.88f)).Mask(SpawnPlane.Mask_Highground)); // small platform edge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4817, 17178, 7038), new Angle(-0.87f, 0.49f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // window rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-5980, 13756, 7088), new Angle(0.18f, 0.98f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // red umbrella
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-7150, 12940, 7087), new Angle(0.13f, 0.99f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // blue umbrella
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2635, 15221, 7898), new Angle(0.98f, 0.20f)).Mask(SpawnPlane.Mask_Highground)); // red crate 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2931, 12557, 8598), new Angle(0.45f, 0.89f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // green umbrella, top
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3047, 13370, 8798), new Angle(0.52f, 0.85f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // antenna
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-5822, 14728, 8700), new Angle(-0.68f, 0.73f)).Mask(SpawnPlane.Mask_Highground));// small platform between ziplines
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4243, 15959, 9107), new Angle(-0.96f, 0.29f)).Mask(SpawnPlane.Mask_HighgroundLimited));// billboard near door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4689, 16448, 8538), new Angle(-0.95f, 0.31f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // small ledge near door
            Rooms.Add(layout);

            //// Room 19 ////
            enemies = room_19.ReturnEnemiesInRoom(AllEnemies);
            enemies[2].SetEnemyType(EnemyTypes.Weeb);
            RandomPickEnemiesWithoutCP(ref enemies, force: true);
            RandomPickEnemiesWithoutCP(ref enemies, force: true); // take 2 enemies to enemiesWithoutCP, forced, leave 1 random around that area
            layout = new RoomLayout(enemies);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-10420, 22654, 8598), new Vector3f(-8583, 23715, 8610), new Angle(-0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground)); // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-12376, 23274, 9198), new Vector3f(-11729, 23604, 9198), new Angle(0.03f, 1.00f)).Mask(SpawnPlane.Mask_Flatground)); // default hallway
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-13138, 23800, 9202), new Vector3f(-14412, 24394, 9198), new Angle(-0.01f, 1.00f)).Mask(SpawnPlane.Mask_Flatground)); // default before door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-10310, 23901, 8898), new Angle(-0.60f, 0.80f)).Mask(SpawnPlane.Mask_Highground)); // red crate
            Rooms.Add(layout);

            //// Room 20 /////
            enemies = room_20.ReturnEnemiesInRoom(AllEnemies);
            EnemySniper sniper = new EnemySniper(enemies[0]);

            layout = new RoomLayout(sniper);

            // Top of umbrella
            SniperSpawnInfo sniperData = new SniperSpawnInfo();
            sniperData.AddPatrolPoint(new Vector3f(-14519, 17741, 8773));
            sniperData.AddPatrolPoint(new Vector3f(-11731, 19523, 8974));
            sniperData.AddFocusPoint(new Vector3f(-13395, 13365, 8870));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-12323, 13465, 9253), new Angle(0.80f, 0.60f)).SetSpawnInfo(sniperData));

            // small rooftop
            sniperData = new SniperSpawnInfo();
            sniperData.AddPatrolPoint(new Vector3f(-14569, 17777, 8773));
            sniperData.AddPatrolPoint(new Vector3f(-11832, 17789, 8861));
            sniperData.AddFocusPoint(new Vector3f(-13831, 13365, 8857));

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-13434, 13865, 8831), new Angle(0.72f, 0.69f)).SetSpawnInfo(sniperData));

            layout.Mask(SpawnPlane.Mask_Sniper);
            Rooms.Add(layout);

            //// Room 21 ////
            enemies = room_21.ReturnEnemiesInRoom(AllEnemies);
            sniper = new EnemySniper(enemies[0]);
            layout = new RoomLayout(sniper);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29210, 12732, 10046), new Angle(-0.17f, 0.98f))); // fuel tank - default patrols
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29693, 10888, 10096), new Angle(0.07f, 1.00f))); // umbrella - default patrols


            sniperData = new SniperSpawnInfo();
            sniperData.AddPatrolPoint(new Vector3f(-25061, 11181, 9578));
            sniperData.AddPatrolPoint(new Vector3f(-26419, 11548, 9598));
            sniperData.AddPatrolPoint(new Vector3f(-29088, 10582, 9797));
            sniperData.AddPatrolPoint(new Vector3f(-30166, 11598, 9792));
            sniperData.AddPatrolPoint(new Vector3f(-30609, 13791, 9603));
            sniperData.AddPatrolPoint(new Vector3f(-30373, 14214, 9603));
            sniperData.AddPatrolPoint(new Vector3f(-29920, 14970, 9478));
            sniperData.AddFocusPoint(new Vector3f(-24699, 14429, 9493));
            sniperData.AddFocusPoint(new Vector3f(-26361, 15220, 9493));

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-25717, 15099, 9911), new Angle(-0.78f, 0.62f)).SetSpawnInfo(sniperData)); // left triple fuel tanks

            sniperData = new SniperSpawnInfo();
            sniperData.AddPatrolPoint(new Vector3f(-32438, 11495, 9942));
            sniperData.AddPatrolPoint(new Vector3f(-30343, 11435, 9942));
            sniperData.AddPatrolPoint(new Vector3f(-29053, 11187, 9942));
            sniperData.AddPatrolPoint(new Vector3f(-28797, 12725, 9499));
            sniperData.AddPatrolPoint(new Vector3f(-25069, 15178, 9898));
            sniperData.AddPatrolPoint(new Vector3f(-26007, 14337, 9478));
            sniperData.AddPatrolPoint(new Vector3f(-26205, 11289, 9635));
            sniperData.AddFocusPoint(new Vector3f(-29235, 15046, 9498));
            sniperData.AddFocusPoint(new Vector3f(-30660, 13235, 9498));

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-30273, 14357, 10656), new Angle(-0.53f, 0.85f)).SetSpawnInfo(sniperData)); // red neon sign, left top

            layout.Mask(SpawnPlane.Mask_Sniper);
            Rooms.Add(layout);


            //// Room 22 ////
            enemies = room_22.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-30913, -6403, 10208), new Vector3f(-29212, -6721, 10199), new Angle(0.69f, 0.72f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-30411, -7787, 10199), new Vector3f(-29304, -8318, 10208), new Angle(0.72f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-30489, -9407, 10208), new Vector3f(-29473, -10538, 10199), new Angle(0.74f, 0.67f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-28451, -10606, 10208), new Vector3f(-30077, -11394, 10208), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-30537, -12889, 10208), new Vector3f(-29147, -13636, 10199), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29903, -14167, 10199), new Vector3f(-29545, -15971, 10199), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-28527, -12991, 10208), new Angle(0.72f, 0.69f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-31018, -15560, 10208), new Angle(0.48f, 0.88f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-31047, -9967, 10208), new Angle(-0.05f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            // special/high places
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29704, -7090, 10675), new Vector3f(-30292, -7311, 10675), new Angle(0.69f, 0.72f)).Mask(SpawnPlane.Mask_Highground)); // 1st roof top
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-30800, -9369, 10760), new Vector3f(-30591, -8445, 10790), new Angle(0.16f, 0.99f)).Mask(SpawnPlane.Mask_Highground)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29261, -10211, 10675), new Vector3f(-28844, -9812, 10675), new Angle(0.74f, 0.67f)).Mask(SpawnPlane.Mask_Highground)); // 2nd roof top
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-30179, -15706, 10508), new Angle(0.65f, 0.76f)).Mask(SpawnPlane.Mask_Highground)); // red crate 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-30431, -10519, 11296), new Angle(0.64f, 0.77f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // wall fan
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-30983, -16916, 10598), new Angle(0.65f, 0.76f)).Mask(SpawnPlane.Mask_Highground)); // wall entrance
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-28844, -18245, 10300), new Vector3f(-29288, -17828, 10300), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Highground)); // left of sniper
            Rooms.Add(layout);

            //// Room 23 ////
            enemies = room_23.ReturnEnemiesInRoom(AllEnemies);
            sniper = new EnemySniper(enemies[0]);
            layout = new RoomLayout(sniper);
            // DEFAULT PATROLS FOR SNIPER, as it's the best optimal area for a sniper
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29704, -18005, 10308), new Angle(0.71f, 0.71f))); // default pos
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29602, -18631, 10178), new Angle(0.71f, 0.71f))); // ramp behind default pos
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-28866, -17637, 11459), new Angle(0.80f, 0.61f))); // wall fan, left from default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29733, -16029, 11388), new Angle(0.69f, 0.73f))); // neon sign infront of default pos
            layout.Mask(SpawnPlane.Mask_Sniper);
            Rooms.Add(layout);


            //// Room 24 ////
            enemies = room_24.ReturnEnemiesInRoom(AllEnemies);
            enemies[0].SetEnemyType(EnemyTypes.Drone);
            enemies[1].SetEnemyType(EnemyTypes.Drone);
            enemies[2].SetEnemyType(EnemyTypes.Drone);
            RandomPickEnemiesWithoutCP(ref enemies);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-27478, -27330, 8042), new Vector3f(-28791, -29556, 7570), new Angle(0.71f, 0.70f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-27040, -30941, 8276), new Angle(0.88f, 0.48f)));
            // special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-27997, -14745, 10578), new Vector3f(-29068, -16258, 10834), new Angle(0.77f, 0.64f))); // previous room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29545, -19211, 10767), new Angle(0.70f, 0.71f))); // above sniper

            layout.Mask(SpawnPlane.Mask_Airborne);
            Rooms.Add(layout);

            #region Jump
            //near last collectible
            NonPlaceableObject uplink = new UplinkJump(0x30, 0xBA8);
            uplink.AddSpawnInfo(new UplinkJumpSpawnInfo().SetRarity(0.5));//default

            var jumpSpawn = new UplinkJumpSpawnInfo { TimeToActivate = 10, JumpMultiplier = 15, JumpForwardMultiplier = 15, JumpGravity = 3 };
            uplink.AddSpawnInfo(jumpSpawn);//high jump

            jumpSpawn = new UplinkJumpSpawnInfo { JumpForwardMultiplier = -3 };
            uplink.AddSpawnInfo(jumpSpawn);//normal backward jump

            jumpSpawn = new UplinkJumpSpawnInfo();//full rng
            jumpSpawn.TimeToActivate = Config.GetInstance().r.Next(10, 71) / 10;
            jumpSpawn.JumpMultiplier = Config.GetInstance().r.Next(1, 11);
            jumpSpawn.JumpForwardMultiplier = Config.GetInstance().r.Next(0, 16) - 8;
            jumpSpawn.JumpGravity = Config.GetInstance().r.Next(3, 9);
            uplink.AddSpawnInfo(jumpSpawn);

            nonPlaceableObjects.Add(uplink);
            #endregion

            #region Shurikens //20,99 default
            //// room 16-18
            uplink = new UplinkShurikens(0x20, 0xb90);
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 5, MaxAttacks = 30 }); // short time
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 10, MaxAttacks = 5 }); // few attacks
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 10, MaxAttacks = 1 }); // single attack
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 500, MaxAttacks = 25 }.SetRarity(0.2)); //to the EoL
            nonPlaceableObjects.Add(uplink);
            #endregion
        }
    }
}