using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.GameObjects;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.MemoryUtils;
using GhostrunnerRNG.NonPlaceableObjects;
using GhostrunnerRNG.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static GhostrunnerRNG.Enemies.Enemy;

namespace GhostrunnerRNG.Maps {
    class Echoes : MapCore, IModes {

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

        #region HC Rooms
        private Room room_HC_1 = new Room(new Vector3f(48172, -3691, 3583), new Vector3f(69575, 7262, 8623)); // 2 weeb, frogger, turret + orb, shielder, splitter
        private Room room_HC_2 = new Room(new Vector3f(45131, 3347, 6985), new Vector3f(40399, -917, 4558)); // waver, frogger, weeb
        private Room room_HC_3 = new Room(new Vector3f(16374, 3208, 5055), new Vector3f(11166, -749, 8046)); // shielder, frogger ,weeb
        private Room room_HC_4 = new Room(new Vector3f(10341, -1476, 5444), new Vector3f(4111, 4097, 8979)); // orb, 2 shielders, shifters
        private Room room_HC_5 = new Room(new Vector3f(2773, 2907, 4281), new Vector3f(-690, -4311, 6897)); // pistol, 2 shielders, splitter
        private Room room_HC_6 = new Room(new Vector3f(-3835, -5135, 4317), new Vector3f(3012, -638, 2308)); // 2 uzi, 2 weeb, turret, 2 pistol, waver 
        private Room room_HC_7 = new Room(new Vector3f(-1395, -7060, 4164), new Vector3f(-2670, -9937, 5482)); // frogger, weeb
        private Room room_HC_8 = new Room(new Vector3f(-10897, 5942, 8093), new Vector3f(-3169, -8222, 4729)); // 2 weeb, 3 uzi, shielder, orb+turret,waver, turret
        private Room room_HC_9 = new Room(new Vector3f(-10974, -2270, 6198), new Vector3f(-18966, -6475, 7858)); // 2 frogger, uzi, shielder
        private Room room_HC_10 = new Room(new Vector3f(-11077, 2122, 7231), new Vector3f(-11850, 7285, 8157)); // 3 weebs hallway
        private Room room_HC_11 = new Room(new Vector3f(-7281, 10569, 5098), new Vector3f(-571, 18102, 9518)); // 2 weeb, uzi, 2 turret, 2 uzi, frogger, pistol, shielder,
        private Room room_HC_12 = new Room(new Vector3f(-7288, 19614, 7953), new Vector3f(-15358, 25387, 10370)); // weeb, shielder, waver
        private Room room_HC_13 = new Room(new Vector3f(-11555, 21112, 7410), new Vector3f(-14568, 11367, 10045)); // sniper, frogger, 2 pistol, turret
        private Room room_HC_14 = new Room(new Vector3f(-24468, 10963, 9016), new Vector3f(-21847, 12705, 11063)); // waver
        private Room room_HC_15 = new Room(new Vector3f(-24846, 16129, 8727), new Vector3f(-31464, 10434, 11323)); // shifter, waver, 2 pistol
        private Room room_HC_16 = new Room(new Vector3f(-31253, 9748, 7996), new Vector3f(-29821, 8342, 9130)); // lonely uzi
        private Room room_HC_17 = new Room(new Vector3f(-27634, 3622, 6044), new Vector3f(-37095, -3659, 9718)); // 2 pistol, weeb
        private Room room_HC_18 = new Room(new Vector3f(-31232, -5732, 9507), new Vector3f(-27962, -18696, 11331)); // 2 pistol, 2 orb, sniper
        private Room room_HC_19 = new Room(new Vector3f(-30259, -19426, 10292), new Vector3f(-25787, -33250, 6549)); // 3 drones, 3 weebs

        #endregion

        public Echoes() : base(GameUtils.MapType.Echoes) {
            ModifyCP(PtrDB.DP_Echoes_ElevatorCP, new Vector3f(67150, -3805.012695f, 6745), GameHook.game);
        }
        public void Gen_Normal() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game, 0, 30);
            AllEnemies.AddRange(GetAllEnemies(GameHook.game, 32, 30));

            Rooms = new List<RoomLayout>();
            RoomLayout layout;
            List<Enemy> enemies;

            //// Room 1 - single Weeb ////
            //// Tutorial Weeb -skipped, to avoid broken encounters

            // Room 2 - Pistol + Weeb ////
            enemies = room_2.ReturnEnemiesInRoom(AllEnemies);
            enemies[1].SetEnemyType(EnemyTypes.Weeb);
            layout = new RoomLayout(enemies);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(60554, 5676, 4950), new Vector3f(58381, 4999, 4950), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Flatground)); // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(58283, 1196, 6048), new Angle(0.70f, 0.72f)).Mask(SpawnPlane.Mask_Highground)); // before slide
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53208, 577, 4949), new Angle(0.94f, 0.33f)).Mask(SpawnPlane.Mask_Flatground)); // double weeb room, corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50498, 521, 5248), new Angle(0.10f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // double weeb room, box
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(58031, 2414, 6177), new Angle(0.60f, 0.80f)).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // billboard
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
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41413, 1543, 6175), new Angle(-0.04f, 1.00f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // top of lamp pole, front
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(44299, -102, 6175), new Angle(0.78f, 0.63f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // top of lamp pole, right
            Rooms.Add(layout);

            //// Room 5 - drone
            enemies = room_5.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyDrone(enemies[0]);
            layout = new RoomLayout(enemies);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36430, 1597, 6691), new Vector3f(34299, 1045, 6098), new Angle(0.00f, 1.00f))); // between billboards
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(33612, 1372, 6098), new Vector3f(26799, 1372, 6098), new Angle(0.00f, 1.00f)).AsVerticalPlane()); // along side of zipline
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22119, 1816, 6915), new Angle(-0.15f, 0.99f)));    // top slide
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(16998, 1612, 6486), new Angle(-0.09f, 1.00f)));    // after slide
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(45207, 1376, 6325), new Angle(-0.01f, 1.00f)).setDiff(1));    // before Room 4
            layout.Mask(SpawnPlane.Mask_Airborne);
            Rooms.Add(layout);

            //// Room 6 ////
            enemies = room_6.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyDrone(enemies[0]);
            enemies[3].SetEnemyType(EnemyTypes.Weeb);
            enemies[4].SetEnemyType(EnemyTypes.Weeb);
            layout = new RoomLayout(enemies);

            // drone spawns
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8953, 2101, 7180), new Vector3f(7193, 638, 7180), new Angle(0.01f, 1.00f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(7149, 3363, 7191), new Angle(-0.20f, 0.98f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(9742, 1319, 7009), new Angle(-1.00f, 0.01f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(7177, -479, 7235), new Angle(0.45f, 0.89f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(4792, 1362, 6984), new Angle(0.06f, 1.00f)).Mask(SpawnPlane.Mask_Airborne)); 

            // default grounded - pistols and weebs
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8736, 3667, 6598), new Vector3f(6937, 2311, 6598), new Angle(0.04f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(5668, 2505, 6598), new Vector3f(6762, 333, 6598), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(7168, -901, 6628), new Vector3f(8672, 452, 6608), new Angle(-0.01f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(4532, 2324, 6598), new Vector3f(4913, 405, 6598), new Angle(0.02f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));

            // high places - only for pistols
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8778, -987, 6898), new Angle(0.44f, 0.90f)).Mask(SpawnPlane.Mask_Highground)); // red crates
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8145, -908, 7170), new Angle(0.81f, 0.58f)).Mask(SpawnPlane.Mask_Highground)); // rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(5902, 600, 7448), new Angle(0.11f, 0.99f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // pipes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(3474, 2483, 6953), new Angle(-0.37f, 0.93f)).Mask(SpawnPlane.Mask_HighgroundLimited)); ; // small corner concrete pillar 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(3475, 300, 6999), new Angle(0.28f, 0.96f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // small corner concrete pillar  2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(5570, 1873, 6997), new Angle(-0.07f, 1.00f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // city lights sign
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6746, 413, 7223), new Angle(0.08f, 1.00f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // light post
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(5532, 2314, 7306), new Angle(-0.01f, 1.00f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // thin ad sign
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
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(1655, -1278, 6382), new Angle(-0.50f, 0.87f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // edge of high rooftop
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
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(3234, -1710, 4325), new Angle(-1.00f, 0.09f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3949, -3447, 3567), new Angle(0.19f, 0.98f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // billboard 2
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
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4949, -7687, 5789), new Angle(-0.32f, 0.95f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // edge of billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3940, -5977, 5698), new Angle(-0.75f, 0.66f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-5700, 2669, 6083), new Angle(-0.64f, 0.77f)).Mask(SpawnPlane.Mask_Highground)); // big pipe
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3413, -1168, 5721), new Angle(1.00f, 0.01f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8844, -3760, 5823), new Angle(-0.01f, 1.00f)).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // far neon sign
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
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-10705, -88, 7446), new Angle(-0.19f, 0.98f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // AC unit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-10169, -3894, 7871), new Angle(-0.02f, 1.00f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // neon sign above door
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
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-12506, -4512, 7688), new Vector3f(-13114, -3970, 7689), new Angle(-0.15f, 0.99f))
                .AsVerticalPlane().Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1));
            Rooms.Add(layout);

            //// Room 14 ////
            enemies = room_14.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyDrone(enemies[0]);
            enemies[1] = new EnemyDrone(enemies[1]);
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
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8685, 10050, 8889), new Angle(-0.96f, 0.28f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // 2'nd billboard after hallway
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8803, 16125, 7073), new Angle(-0.71f, 0.70f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // on fence, before next room
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
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-7349, 15766, 7921), new Angle(-0.02f, 1.00f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // billboard above entrance
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6330, 16972, 7598), new Angle(-0.47f, 0.88f)).Mask(SpawnPlane.Mask_Highground)); // small platform edge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4817, 17178, 7038), new Angle(-0.87f, 0.49f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // window rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-5980, 13756, 7088), new Angle(0.18f, 0.98f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // red umbrella
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-7150, 12940, 7087), new Angle(0.13f, 0.99f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // blue umbrella
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2635, 15221, 7898), new Angle(0.98f, 0.20f)).Mask(SpawnPlane.Mask_Highground)); // red crate 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2931, 12557, 8598), new Angle(0.45f, 0.89f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // green umbrella, top
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3047, 13370, 8798), new Angle(0.52f, 0.85f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // antenna
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
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-30431, -10519, 11296), new Angle(0.64f, 0.77f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // wall fan
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
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-28866, -17637, 11459), new Angle(0.80f, 0.61f)).setDiff(1)); // wall fan, left from default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29733, -16029, 11388), new Angle(0.69f, 0.73f)).setDiff(1)); // neon sign infront of default pos
            layout.Mask(SpawnPlane.Mask_Sniper);
            Rooms.Add(layout);


            //// Room 24 ////
            enemies = room_24.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyDrone(enemies[0]);
            enemies[1] = new EnemyDrone(enemies[1]);
            enemies[2] = new EnemyDrone(enemies[2]);
            RandomPickEnemiesWithoutCP(ref enemies);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-27478, -27330, 8042), new Vector3f(-28791, -29556, 7570), new Angle(0.71f, 0.70f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-27040, -30941, 8276), new Angle(0.88f, 0.48f)));
            // special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-27997, -14745, 10578), new Vector3f(-29068, -16258, 10834), new Angle(0.77f, 0.64f))); // previous room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29545, -19211, 10767), new Angle(0.70f, 0.71f)).setDiff(1)); // above sniper

            layout.Mask(SpawnPlane.Mask_Airborne);
            Rooms.Add(layout);

            #region Jump
            //near last collectible
            NonPlaceableObject uplink = new UplinkJump(0x30, 0xBB8);
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

            worldObjects.Add(uplink);
            #endregion

            #region Shurikens //20,99 default
            //// room 16-18
            uplink = new UplinkShurikens(0x20, 0xb90);
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 5, MaxAttacks = 30 }); // short time
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 10, MaxAttacks = 5 }); // few attacks
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 10, MaxAttacks = 1 }); // single attack
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 500, MaxAttacks = 25 }.SetRarity(0.2)); //to the EoL
            worldObjects.Add(uplink);
            #endregion

            #region Billboard
            uplink = new Billboard(0x10, 0x398);
            uplink.AddSpawnInfo(new BillboardSpawnInfo { Time1 = Config.GetInstance().r.Next(1, 10), Time2 = Config.GetInstance().r.Next(1, 10) }); // basic time rng
            uplink.AddSpawnInfo(new BillboardSpawnInfo { Angle1 = -110 - (360 * Config.GetInstance().r.Next(3)), Angle2 = 110 + (360 * Config.GetInstance().r.Next(3)) }); // random additional spins
            uplink.AddSpawnInfo(new BillboardSpawnInfo { Angle1 = -220, Angle2 = 200 }); // proper angles
            uplink.AddSpawnInfo(new BillboardSpawnInfo()); // default
            worldObjects.Add(uplink);
            #endregion
        }

        public void Gen_Easy() {
            Gen_Normal();
        }


        private void FixSnipers(List<Enemy> snipers) {
            int patrolpoints;
            ulong point = 0;
            PtrDB.DP_HCEchoes_Sniperpoints1.DerefOffsets(GameHook.game, out IntPtr sniperpoints1);
            PtrDB.DP_HCEchoes_Sniperpoints2.DerefOffsets(GameHook.game, out IntPtr sniperpoints2);
            foreach(Enemy sniper in snipers) {
                IntPtr sniperptr = IntPtr.Subtract(sniper.GetObjectPtr(), 0x4f0);

                GameHook.game.ReadValue<int>(IntPtr.Add(sniperptr, 0x818), out patrolpoints);
                GameHook.game.ReadValue<IntPtr>(IntPtr.Add(sniperptr, 0x810), out IntPtr sniperpointslist1);
                GameHook.game.ReadValue<IntPtr>(IntPtr.Add(sniperptr, 0x820), out IntPtr sniperpointslist2);
                if(patrolpoints == 2) {
                    for(var i = 0; i < patrolpoints; i++) {//PatrolFocusPoints
                        GameHook.game.ReadValue<ulong>(IntPtr.Add(sniperpoints1, 0x8 * i), out point);
                        GameHook.game.WriteBytes(IntPtr.Add(sniperpointslist1, 0x8 * i), BitConverter.GetBytes((ulong)point));
                    }
                    GameHook.game.WriteBytes(IntPtr.Add(sniperptr, 0x808), BitConverter.GetBytes((ulong)point));//FirstSniperFocusPoint

                    GameHook.game.ReadValue<ulong>(IntPtr.Subtract(sniperpoints1, 0x8), out point);
                    GameHook.game.WriteBytes(sniperpointslist2, BitConverter.GetBytes((ulong)point));//NearestFocusPoints
                } else {
                    for(var i = 0; i < 2; i++) {//NearestFocusPoints
                        GameHook.game.ReadValue<ulong>(IntPtr.Add(sniperpoints2, 0x8 * i), out point);
                        GameHook.game.WriteBytes(IntPtr.Add(sniperpointslist2, 0x8 * i), BitConverter.GetBytes((ulong)point));
                    }

                    for(var i = 0; i < patrolpoints; i++) {//PatrolFocusPoints
                        GameHook.game.ReadValue<ulong>(IntPtr.Add(sniperpoints2, 0x10 + 0x8 * i), out point);
                        GameHook.game.WriteBytes(IntPtr.Add(sniperpointslist1, 0x20 - 0x8 * i), BitConverter.GetBytes((ulong)point));
                    }
                    GameHook.game.WriteBytes(IntPtr.Add(sniperptr, 0x808), BitConverter.GetBytes((ulong)point));//FirstSniperFocusPoint
                }

            }
        }

        private List<ChainedOrb> chainedOrbs = new List<ChainedOrb>();
        private List<RoomLayout> chainedOrbs_Rooms = new List<RoomLayout>();
        public void Gen_Hardcore() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game);
            FixSnipers(AllEnemies.GetRange(39, 2));
            List<Enemy> Shifters = new List<Enemy>();
            Rooms = new List<RoomLayout>();
            RoomLayout layout;

            //// Room 1 ////
            var enemies = room_HC_1.ReturnEnemiesInRoom(AllEnemies);
            enemies[2].SetEnemyType(EnemyTypes.Splitter);
            enemies[5].SetEnemyType(EnemyTypes.Weeb);
            enemies[6].SetEnemyType(EnemyTypes.Weeb);
            chainedOrbs.Add(new ChainedOrb(new EnemyShieldOrb(enemies[1]), new EnemyTurret(enemies[0])));
            enemies.RemoveRange(0, 2);
            RandomPickEnemiesWithoutCP(ref enemies, force: true);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66946, 4538, 5609), new Vector3f(67417, 3119, 5600), new Angle(-0.71f, 0.70f))
                .Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(60267, 5694, 4950), new Vector3f(58355, 5003, 4950), new Angle(-0.01f, 1.00f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(3).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(52373, 776, 4929), new Vector3f(49820, 1695, 4949), new Angle(0.00f, 1.00f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(3).AllowSplitter());
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(59620, 4707, 5306), new Angle(0.27f, 0.96f)).Mask(SpawnPlane.Mask_Highground)); // city light
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(58814, 3931, 5836), new Angle(0.41f, 0.91f)).Mask(SpawnPlane.Mask_Highground)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(58046, 2479, 6176), new Angle(0.59f, 0.81f)).Mask(SpawnPlane.Mask_Highground)); // billboard 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50468, 524, 5248), new Angle(0.13f, 0.99f)).Mask(SpawnPlane.Mask_Highground)); // ramp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49972, 2085, 5346), new Angle(-0.29f, 0.96f)).Mask(SpawnPlane.Mask_Highground)); // small rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53181, 1817, 4949), new Angle(-0.92f, 0.38f)).Mask(SpawnPlane.Mask_Highground)); // after slide, around corner
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51854, 764, 5375), new Vector3f(52340, 1618, 5548), new Angle(0.02f, 1.00f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(58337, 1977, 6146), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50271, 377, 4949), new Angle(0.96f, 0.28f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 1, HorizontalSpeed = 1, VerticalAngle = 75, VisibleLaserLength = 0 })); // ninja under billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(56477, 1299, 5949), new QuaternionAngle(-1.00f, 0.00f, 0, 0)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalSpeed = 10, HorizontalAngle = 5, VerticalAngle = 45 }));
            Rooms.Add(layout);


            //// Room 2 ////
            enemies = room_HC_2.ReturnEnemiesInRoom(AllEnemies);
            enemies[2].SetEnemyType(EnemyTypes.Weeb);
            RandomPickEnemiesWithoutCP(ref enemies);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42212, 27, 5548), new Vector3f(43956, 2792, 5548), new Angle(-0.01f, 1.00f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(4).AllowSplitter());
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42470, 3016, 5794), new Angle(-0.56f, 0.83f)).Mask(SpawnPlane.Mask_Highground)); // ac unit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41548, 2893, 6448), new Angle(-0.33f, 0.95f)).Mask(SpawnPlane.Mask_Highground)); // fuel tank
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41887, 94, 6312), new Angle(0.14f, 0.99f)).Mask(SpawnPlane.Mask_Highground)); // building rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42271, -92, 6175), new Angle(0.39f, 0.92f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // street light
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41283, 2122, 6125), new Angle(-0.04f, 1.00f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // zipline pole
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(42484, 338, 6076), new Vector3f(43436, 2514, 6461), new Angle(-0.02f, 1.00f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(40714, 1183, 5968), new Angle(0.06f, 1.00f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(41312, 344, 5835), new QuaternionAngle(-0.50f, -0.50f, 0.50f, 0.50f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 1, HorizontalSpeed = 1, VisibleLaserLength = 0, VerticalAngle = 45 })); // ninja
            Rooms.Add(layout);


            //// Room 3 ////
            enemies = room_HC_3.ReturnEnemiesInRoom(AllEnemies);
            enemies[2].SetEnemyType(EnemyTypes.Weeb);
            RandomPickEnemiesWithoutCP(ref enemies);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(12238, 2151, 6448), new Vector3f(13797, 575, 6448), new Angle(0.01f, 1.00f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(4).AllowSplitter());
            // high/special 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(14531, 645, 7063), new Angle(0.07f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(13894, 2171, 6860), new Angle(-0.15f, 0.99f)).Mask(SpawnPlane.Mask_Highground)); // umbrella
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(12030, 1526, 6803), new Angle(-0.10f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // ad
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(15166, 1265, 6458), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // ledge
            Rooms.Add(layout);

            //// Room 4 ////
            enemies = room_HC_4.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            List<Tuple<Vector3f, Angle>> ShiftPoints_HC_Room4 = new List<Tuple<Vector3f, Angle>>() {
                new Tuple<Vector3f, Angle>(new Vector3f(8885, -667, 6898), new Angle(0.77f, 0.63f)), // red crates
                new Tuple<Vector3f, Angle>(new Vector3f(8820, 4000, 6998), new Angle(-0.90f, 0.44f)), // left wall corner
                new Tuple<Vector3f, Angle>(new Vector3f(5550, 1882, 6997), new Angle(-0.36f, 0.93f)), // city light
                new Tuple<Vector3f, Angle>(new Vector3f(5912, 629, 7448), new Angle(0.17f, 0.99f)), // curved pipe
                new Tuple<Vector3f, Angle>(new Vector3f(5524, 2371, 7306), new Angle(-0.23f, 0.97f)), // thin ad
                new Tuple<Vector3f, Angle>(new Vector3f(6880, 2137, 7269), new Angle(-0.47f, 0.89f)), // street light
                new Tuple<Vector3f, Angle>(new Vector3f(6492, -452, 6644), new Angle(0.32f, 0.95f)), // under right wall fan
                new Tuple<Vector3f, Angle>(new Vector3f(8104, -860, 7170), new Angle(0.55f, 0.84f)), // small rooftop right
                new Tuple<Vector3f, Angle>(new Vector3f(4214, 396, 6598), new Angle(0.38f, 0.92f)), // near exit door
            };
            enemies[1] = new EnemyShifter(enemies[1]).AddFixedSpawnInfoList(ref ShiftPoints_HC_Room4); // 3 shift points
            // orb
            layout = new RoomLayout(enemies[0]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(5679, 1941, 6701), new Vector3f(5728, 908, 7123))); // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(3765, 360, 6640))); // left of door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6399, -320, 7130))); // right wall fan
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6445, 3119, 7121))); // left wall fan
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(5232, 1360, 6928))); // behind default wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(3942, 1400, 7102))); // above exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(7548, 1318, 6719))); // floating middle
            Rooms.Add(layout);
            // enemies
            layout = new RoomLayout(enemies.Skip(1).ToList());
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8740, 446, 6608), new Vector3f(7001, -493, 6628), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8783, 2319, 6598), new Vector3f(6857, 3348, 6598), new Angle(-0.02f, 1.00f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6709, 2516, 6598), new Vector3f(5816, 346, 6598), new Angle(-0.01f, 1.00f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(4869, 448, 6598), new Vector3f(4428, 2256, 6598), new Angle(-0.02f, 1.00f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(3675, 1424, 6598), new Angle(-0.02f, 1.00f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            // high/special 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(3475, 300, 6999), new Angle(0.16f, 0.99f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // right pillar near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(3480, 2471, 6999), new Angle(-0.30f, 0.95f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // left pillar near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(4328, 1059, 6956), new Angle(0.23f, 0.97f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // ad near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8774, -1096, 6898), new Angle(0.75f, 0.66f)).Mask(SpawnPlane.Mask_Highground)); // red crates
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(7961, -357, 7026), new Vector3f(7141, 2804, 7304), new Angle(0.02f, 1.00f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turret)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6827, 3444, 6598), new Angle(-0.84f, 0.54f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 20, HorizontalSpeed = 30, VerticalAngle = 5 })); // under left wall fan
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8270, -1194, 6598), new Angle(0.92f, 0.40f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, HorizontalSpeed = 0, VerticalAngle = 0 })); // under right rooftop
            Rooms.Add(layout);



            //// Room 5 ////
            enemies = room_HC_5.ReturnEnemiesInRoom(AllEnemies);
            enemies[1].SetEnemyType(EnemyTypes.Splitter);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, enemyIndex: 0); // lonely pistol
            RandomPickEnemiesWithoutCP(ref enemies, force: true); // take yet another one
            layout = new RoomLayout(enemies);
            // rooftops
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(821, -4213, 5612), new Vector3f(1752, -3769, 5606), new Angle(0.34f, 0.94f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-404, -3806, 5546), new Angle(0.15f, 0.99f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(113, -3053, 5546), new Angle(0.07f, 1.00f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(1348, -1630, 5548), new Angle(-0.28f, 0.96f)).Mask(SpawnPlane.Mask_Highground));
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(1915, -945, 5093), new Vector3f(2810, -1263, 5093), new Angle(-0.72f, 0.69f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(562, -3044, 5093), new Vector3f(2221, -2094, 5093), new Angle(-0.02f, 1.00f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(140, -4023, 5093), new Vector3f(532, -3499, 5093), new Angle(0.24f, 0.97f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(2894, -3201, 6598), new Angle(0.67f, 0.74f)).Mask(SpawnPlane.Mask_Highground)); // end of cooridator
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(554, -1250, 6382), new Vector3f(1591, -923, 6382), new Angle(-0.54f, 0.84f)).Mask(SpawnPlane.Mask_Highground)); // high rooftop
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(2431, -30, 5722), new Angle(-0.74f, 0.67f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-769, 760, 5425), new Angle(0.03f, 1.00f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(1406, 2461, 5000), new Angle(-0.94f, 0.33f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, HorizontalSpeed = 0, VerticalAngle = 0 }));
            Rooms.Add(layout);


            //// Room 6 ////
            enemies = room_HC_6.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyTurret(enemies[0]);
            DevWindow.worldObject = enemies[0];
            enemies[5].SetEnemyType(EnemyTypes.Waver);
            enemies[6].SetEnemyType(EnemyTypes.Weeb);
            enemies[7].SetEnemyType(EnemyTypes.Weeb);
            //RandomPickEnemiesWithoutCP(ref enemies, force: true);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-828, -4233, 4098), new Vector3f(-1256, -3619, 4098), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(341, -3030, 4098), new Vector3f(-112, -3912, 4098), new Angle(1.00f, 0.03f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(2152, -1012, 3498), new Vector3f(2941, -2869, 3498), new Angle(1.00f, 0.04f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(1947, -3034, 3498), new Vector3f(1053, -3283, 3498), new Angle(0.69f, 0.72f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(683, -1468, 3848), new Vector3f(1104, -938, 3860), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1075, -2488, 3200), new Vector3f(-1436, -1220, 3198), new Angle(0.00f, 1.00f))
                .Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3833, -2427, 2698), new Vector3f(-2773, -1184, 2698), new Angle(-0.23f, 0.97f))
                .Mask(SpawnPlane.Mask_Flatground).AsVerticalPlane());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1590, -4188, 3097), new Vector3f(-3276, -4924, 3097), new Angle(0.70f, 0.72f))
                .Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(2651, -2949, 3856), new Angle(0.96f, 0.29f)).Mask(SpawnPlane.Mask_Highground)); // small ad
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1539, -1233, 3680), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // pipe after slide
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1969, -2745, 4300), new Angle(-0.83f, 0.56f)).Mask(SpawnPlane.Mask_Highground)); // collectible ledge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3344, -1536, 4772), new Angle(-0.48f, 0.88f)).Mask(SpawnPlane.Mask_Highground)); // big wall fan
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3728, -4831, 3980), new Angle(0.48f, 0.87f)).Mask(SpawnPlane.Mask_Highground)); // fuel tank near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1998, -5236, 3676), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // exit wall light
            // turrets
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1875, -1298, 3200), new Angle(-0.86f, 0.52f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 40, HorizontalSpeed = 30, VerticalAngle = 0 })); // corner near default

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2099, -2377, 3986), new QuaternionAngle(0.58f, -0.41f, -0.58f, 0.41f)).Mask(SpawnPlane.Mask_Turret)
                 .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0.001f, HorizontalSpeed = 1, VerticalAngle = 10 })); // collectible platform side

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3180, -2533, 2698), new Angle(-0.53f, 0.85f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 20, VerticalAngle = 25 })); // behind huge pillar

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(195, -950, 3736), new QuaternionAngle(0.06f, -0.28f, -0.22f, 0.93f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 5, HorizontalSpeed = 5, VerticalAngle = -15 })); // on ramp behind laser wall

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1911, -2352, 3200), new Angle(-0.13f, 0.99f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 100, HorizontalSpeed = 60, VerticalAngle = 5 })); // default, huge range

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1030, -1979, 3816), new QuaternionAngle(0.51f, -0.86f, 0.00f, 0.00f)).Mask(SpawnPlane.Mask_Turret)
            .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 1, HorizontalSpeed = 1, VerticalAngle = 45, VisibleLaserLength = 0 })); // ceiling ninja
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2158, -4723, 3691), new Vector3f(-3197, -3507, 3852), new Angle(0.37f, 0.93f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);


            //// Room 7 ////
            enemies = room_HC_7.ReturnEnemiesInRoom(AllEnemies);
            enemies[1].SetEnemyType(EnemyTypes.Weeb);
            enemies.ForEach(x => x.DisableAttachedCP(GameHook.game));
            EnemiesWithoutCP.AddRange(enemies);
            // custom CP instead of these 2 random enemies
            CustomCheckPoints.Add(new GameObjects.CustomCP(mapType,
                new Vector3f(-2649, -7091, 4292), new Vector3f(-1274, -9926, 5474), new Vector3f(-1995, -9153, 4703), new Angle(1.00f, 0.01f)));


            //// Room 8 ////
            enemies = room_HC_8.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyTurret(enemies[0]);
            enemies[1] = new EnemyTurret(enemies[1]);
            enemies[7].SetEnemyType(EnemyTypes.Waver);
            enemies[8].SetEnemyType(EnemyTypes.Weeb);
            enemies[9].SetEnemyType(EnemyTypes.Weeb);
            enemies.Where(x => x.enemyType == EnemyTypes.Weeb && x.Pos.Y > 0).First().DisableAttachedCP(GameHook.game); // disable cp of single weeb near turret
            Enemy attachedEnemy = enemies.Take(2).Where(x => x.Pos.Y > 0).First();
            chainedOrbs.Add(new ChainedOrb(new EnemyShieldOrb(enemies[2]), attachedEnemy));
            enemies.RemoveAt(2);
            enemies.Remove(attachedEnemy);
            CustomCheckPoints.Add(new CustomCP(mapType, new Vector3f(-10892, 484, 8104), new Vector3f(-8348, 2263, 6432),
                new Vector3f(-8938, 1892, 6652), new Angle(-1.00f, 0.02f))); // CP after zipline, before 2nd room
            RandomPickEnemiesWithoutCP(ref enemies);

            // first section of the room - bottom left
            enemies.Where(x => x.Pos.Z < 6000).ToList().ForEach(x => x.DisableAttachedCP(GameHook.game));
            layout = new RoomLayout(enemies.Where(x => x.Pos.Z < 6000).ToList());
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4828, -4831, 5100), new Vector3f(-4376, -5934, 5100), new Angle(-0.70f, 0.72f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6235, -3933, 5109), new Vector3f(-4440, -3396, 5098), new Angle(-0.73f, 0.68f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4522, -1706, 5122), new Angle(-0.73f, 0.69f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3495, 1611, 5099), new Vector3f(-3561, -501, 5098), new Angle(-0.84f, 0.54f)).Mask(SpawnPlane.Mask_Flatground).AsVerticalPlane());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-5755, 4641, 5298), new Vector3f(-4692, 4179, 5298), new Angle(-0.59f, 0.81f)).Mask(SpawnPlane.Mask_Flatground).AsVerticalPlane());
            Rooms.Add(layout);

            // second section of the room - top right
            layout = new RoomLayout(enemies.Where(x => x.Pos.Z > 6000).ToList());
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9112, -2382, 6651), new Vector3f(-10008, -1402, 6648), new Angle(0.72f, 0.69f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9567, -224, 6648), new Vector3f(-8768, -1475, 6648), new Angle(1.00f, 0.05f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9897, -4378, 6648), new Vector3f(-9293, -3672, 6648), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9298, -3144, 6648), new Vector3f(-9967, -2580, 6648), new Angle(0.72f, 0.69f)).Mask(SpawnPlane.Mask_Flatground).AsVerticalPlane());
            // high
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9338, -2663, 6854), new Angle(0.58f, 0.82f)).Mask(SpawnPlane.Mask_Highground)); // crate
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9758, -3320, 6854), new Angle(0.68f, 0.74f)).Mask(SpawnPlane.Mask_Highground)); // crate
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9541, -4729, 6948), new Angle(0.69f, 0.73f)).Mask(SpawnPlane.Mask_Highground)); // crate
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-10671, -78, 7446), new Angle(-0.44f, 0.90f)).Mask(SpawnPlane.Mask_Highground)); // ac unit
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8982, -2122, 6653), new Vector3f(-8996, -3389, 6653), new Angle(-0.03f, 1.00f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 30, VerticalAngle = -20 })); // default with range

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6316, -1594, 5109), new Angle(-0.53f, 0.85f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 20, HorizontalSpeed = 30, VerticalAngle = 15 })); // lower platfrom ledge

            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9645, -5030, 7374), new Angle(0.69f, 0.73f)).Mask(SpawnPlane.Mask_Airborne)); // exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-5553, -1012, 5384), new Angle(-0.61f, 0.79f)).Mask(SpawnPlane.Mask_Airborne)); // lower area, angled pink billboard
            Rooms.Add(layout);



            //// Room 9 ////
            enemies = room_HC_9.ReturnEnemiesInRoom(AllEnemies);
            RandomPickEnemiesWithoutCP(ref enemies);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-14378, -2697, 6648), new Vector3f(-13648, -4475, 6648), new Angle(0.05f, 1.00f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-12771, -4950, 6648), new Vector3f(-14488, -5611, 6648), new Angle(-0.03f, 1.00f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11943, -5895, 6672), new Vector3f(-11280, -5160, 6648), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11024, -4816, 7119), new Angle(0.96f, 0.28f)).Mask(SpawnPlane.Mask_Highground)); // scaffolding
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-14829, -5888, 7141), new Vector3f(-13655, -4817, 7353), new Angle(0.05f, 1.00f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-12540, -5920, 6672), new Angle(0.83f, 0.55f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 30, VerticalAngle = 0 }));
            Rooms.Add(layout);



            //// Room 10 ////
            enemies = room_HC_10.ReturnEnemiesInRoom(AllEnemies);
            enemies.ForEach(x => x.SetEnemyType(EnemyTypes.Weeb));
            RandomPickEnemiesWithoutCP(ref enemies, force: true);
            layout = new RoomLayout(enemies);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11217, 2332, 7498), new Vector3f(-11742, 6154, 7498), new Angle(-0.70f, 0.72f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(4).AllowSplitter());
            Rooms.Add(layout);
            CustomCheckPoints.Add(new CustomCP(mapType, new Vector3f(-13959, 0, 8528), new Vector3f(-11319, 2284, 6720),
                new Vector3f(-12655, 1392, 7522), new Angle(0.11f, 0.99f))); // before that room


            //// Room 11 ////
            enemies = room_HC_11.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyTurret(enemies[0]);
            enemies[1] = new EnemyTurret(enemies[1]);
            enemies[8].SetEnemyType(EnemyTypes.Weeb);
            enemies[9].SetEnemyType(EnemyTypes.Weeb);
            RandomPickEnemiesWithoutCP(ref enemies, force: true);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6291, 16604, 6098), new Vector3f(-3375, 15235, 6098), new Angle(1.00f, 0.01f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6021, 13014, 6098), new Vector3f(-7017, 13931, 6110), new Angle(0.71f, 0.71f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4645, 14015, 6098), new Vector3f(-5525, 12728, 6098), new Angle(0.69f, 0.72f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4569, 13248, 6703), new Vector3f(-5586, 12809, 6703), new Angle(0.70f, 0.72f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6180, 12890, 6700), new Vector3f(-6834, 13920, 6700), new Angle(0.70f, 0.72f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2430, 16053, 7598), new Vector3f(-1799, 15069, 7598), new Angle(0.93f, 0.37f))
                .Mask(SpawnPlane.Mask_Flatground).AsVerticalPlane().AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3699, 13643, 7622), new Vector3f(-2307, 14026, 7598), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1344, 13903, 7598), new Vector3f(-1291, 12955, 7598), new Angle(0.74f, 0.68f)).Mask(SpawnPlane.Mask_Flatground).AsVerticalPlane());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1263, 13301, 8200), new Vector3f(-2507, 12882, 8200), new Angle(0.69f, 0.73f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6939, 17913, 8498), new Vector3f(-6262, 17129, 8498), new Angle(-0.69f, 0.72f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4731, 18055, 8498), new Vector3f(-5406, 17260, 8498), new Angle(-0.70f, 0.72f)).Mask(SpawnPlane.Mask_Flatground));
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6215, 16866, 7598), new Angle(-0.46f, 0.89f)).Mask(SpawnPlane.Mask_Highground)); // small platform ledge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-7078, 14565, 7091), new Angle(0.33f, 0.94f)).Mask(SpawnPlane.Mask_Highground)); // red crate
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3806, 14168, 8031), new Angle(0.93f, 0.38f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // random pole
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1204, 15296, 7898), new Angle(0.99f, 0.17f)).Mask(SpawnPlane.Mask_Highground)); // white crate
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1143, 14483, 8200), new Angle(0.88f, 0.48f)).Mask(SpawnPlane.Mask_Highground)); // highest platform ledge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3029, 13430, 8798), new Angle(0.71f, 0.71f)).Mask(SpawnPlane.Mask_Highground)); // antenna
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2950, 12506, 8598), new Angle(0.47f, 0.88f)).Mask(SpawnPlane.Mask_Highground)); // umbrella
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-5836, 14706, 8700), new Angle(-0.40f, 0.92f)).Mask(SpawnPlane.Mask_Highground)); // zipline platform
            // turrets
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4417, 13384, 6703), new Angle(0.76f, 0.65f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 25 })); // default

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-7159, 12953, 7088), new Angle(0.31f, 0.95f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, HorizontalSpeed = 0, VerticalAngle = -5 })); // blue umbrella on left

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2612, 15139, 7898), new Angle(-0.55f, 0.84f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 35, HorizontalSpeed = 45, VerticalAngle = 0 })); // default red crate but faster

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-976, 13814, 8200), new Angle(-0.98f, 0.22f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 20, HorizontalSpeed = 30, VerticalAngle = 5 })); // highest platform

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3810, 16140, 8215), new QuaternionAngle(0.96f, -0.29f, 0.00f, 0.00f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 20, HorizontalSpeed = 20, VerticalAngle = 35 })); // ceiling gang

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6150, 14693, 8207), new QuaternionAngle(-0.41f, -0.58f, 0.41f, 0.58f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0.005f, HorizontalSpeed = 1, VerticalAngle = 10 })); // zipline pillar, right side wall


            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3773, 16534, 6790), new Vector3f(-5192, 15730, 7297), new Angle(1.00f, 0.02f)).Mask(SpawnPlane.Mask_Airborne)); // entry
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2013, 13205, 8552), new Angle(0.84f, 0.55f)).Mask(SpawnPlane.Mask_Airborne)); // highest platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-5329, 16526, 8712), new Vector3f(-5422, 14016, 8712), new Angle(-0.74f, 0.68f)).Mask(SpawnPlane.Mask_Airborne)); // zipline
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6568, 16820, 8802), new Angle(-0.40f, 0.92f)).Mask(SpawnPlane.Mask_Airborne)); // near exit door
            Rooms.Add(layout);



            //// Room 12 ////
            enemies = room_HC_12.ReturnEnemiesInRoom(AllEnemies);
            enemies[1].SetEnemyType(EnemyTypes.Waver);
            enemies[3].SetEnemyType(EnemyTypes.Weeb);
            RandomPickEnemiesWithoutCP(ref enemies, force: true);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8001, 21124, 8602), new Angle(-1.00f, 0.01f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-10373, 20943, 8612), new Angle(0.22f, 0.98f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8116, 23786, 8610), new Vector3f(-9840, 22720, 8610), new Angle(-0.72f, 0.69f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11467, 23495, 9203), new Angle(-0.09f, 1.00f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-14360, 23846, 9198), new Vector3f(-13076, 24666, 9198), new Angle(-0.06f, 1.00f))
                .Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-14729, 24788, 9768), new Angle(-0.19f, 0.98f)).Mask(SpawnPlane.Mask_Highground)); // antenna
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-10098, 23831, 8898), new Angle(-0.50f, 0.87f)).Mask(SpawnPlane.Mask_Highground)); // red crate
            Rooms.Add(layout);


            //// Room 13 ////
            enemies = room_HC_13.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyTurret(enemies[0]);
            enemies[3] = new EnemySniper(enemies[3]);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-13182, 17928, 8378), new Vector3f(-14011, 17042, 8378), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-12369, 14246, 8378), new Vector3f(-13418, 14600, 8387), new Angle(0.69f, 0.72f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-12548, 12201, 8398), new Vector3f(-13076, 11810, 8398), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Highground));

            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-13461, 11830, 8803), new Angle(0.69f, 0.72f)).Mask(SpawnPlane.Mask_Highground)); // small rooftop at end
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-13467, 13750, 8831), new Angle(0.65f, 0.76f)).Mask(SpawnPlane.Mask_Highground)); // rooftop mid
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-13396, 16615, 8831), new Angle(0.77f, 0.64f)).Mask(SpawnPlane.Mask_Highground)); // rooftop start
            // turret
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-12079, 14604, 8378), new Angle(-0.98f, 0.21f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 30, VerticalAngle = 5})); // middle platform

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-12347, 13345, 8390), new Angle(-0.97f, 0.25f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0.5f, HorizontalSpeed = 0.5f, VerticalAngle = 30, VisibleLaserLength = 0, MaxAttackRange = 2770, MaxAttackRange2 = 2770 })); // ninja aiming toward exit

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11665, 16412, 8398), new Angle(-0.93f, 0.38f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 30, VerticalAngle = 5 })); // first platform, right

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-12315, 14872, 8831), new Angle(-1.00f, 0.01f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 35, HorizontalSpeed = 30, VerticalAngle = -10 })); // middle platform rooftop

            // ninja after cp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-15250, 11136, 9666), new Angle(0.99f, 0.12f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0.5f, HorizontalSpeed = 0.5f, VerticalAngle = 0, VisibleLaserLength = 0 }));

            // sniper
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// TODO
            ///////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////

            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-12784, 11529, 9246), new Angle(0.80f, 0.61f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-12681, 14364, 9246), new Vector3f(-13501, 15297, 9246), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);



            //// Room 14 ////
            enemies = room_HC_14.ReturnEnemiesInRoom(AllEnemies);
            enemies[0].SetEnemyType(EnemyTypes.Waver);
            RandomPickEnemiesWithoutCP(ref enemies, force: true); // lonely waver...
            CustomCheckPoints.Add(new GameObjects.CustomCP(mapType, new Vector3f(-22995, 12074, 8832), new Vector3f(-24837, 10824, 10582),
                new Vector3f(-23988, 11366, 9587), new Angle(-1.00f, 0.00f)));


            //// Room 15 ////
            enemies = room_HC_15.ReturnEnemiesInRoom(AllEnemies);
            List<Tuple<Vector3f, Angle>> ShiftPoints_HC_Room15 = new List<Tuple<Vector3f, Angle>>() {
                new Tuple<Vector3f, Angle>(new Vector3f(-30421, 11923, 10348), new Angle(-0.06f, 1.00f)), // antenna
                new Tuple<Vector3f, Angle>(new Vector3f(-30258, 14238, 10656), new Angle(-0.34f, 0.94f)), // high red neon sign
                new Tuple<Vector3f, Angle>(new Vector3f(-29711, 10871, 10096), new Angle(0.34f, 0.94f)), // umbrella
                new Tuple<Vector3f, Angle>(new Vector3f(-25713, 15150, 9897), new Angle(-0.83f, 0.55f)), // fuel tanks
                new Tuple<Vector3f, Angle>(new Vector3f(-28837, 12665, 9498), new Angle(-0.13f, 0.99f)), // platform corner ledge
                new Tuple<Vector3f, Angle>(new Vector3f(-30722, 13080, 9897), new Angle(0.00f, 1.00f)), // back fence
               
            };
            enemies[2] = new EnemyShifter(enemies[2]).AddFixedSpawnInfoList(ref ShiftPoints_HC_Room15); // 4 shift points
            enemies[3].SetEnemyType(EnemyTypes.Waver);
            RandomPickEnemiesWithoutCP(ref enemies, enemyIndex: 0, force:true);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-26343, 13236, 9478), new Vector3f(-25499, 12567, 9478), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-27939, 14488, 9478), new Vector3f(-26435, 14917, 9477), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29369, 15005, 9478), new Vector3f(-29758, 14602, 9486), new Angle(-0.04f, 1.00f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-30513, 13153, 9478), new Vector3f(-29500, 12815, 9478), new Angle(0.40f, 0.92f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29633, 11988, 9778), new Vector3f(-30309, 11122, 9778), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29169, 11978, 9798), new Angle(-0.01f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // default waver ledge
            Rooms.Add(layout);


            //// Room 16 ////
            enemies = room_HC_16.ReturnEnemiesInRoom(AllEnemies);
            RandomPickEnemiesWithoutCP(ref enemies, force:true); // lonely uzi


            //// Room 17 ////
            enemies = room_HC_17.ReturnEnemiesInRoom(AllEnemies);
            enemies[2].SetEnemyType(EnemyTypes.Weeb);
            enemies.ForEach(x => x.DisableAttachedCP(GameHook.game));
            EnemiesWithoutCP.AddRange(enemies); // useless area...


            //// Room 18 ////
            enemies = room_HC_18.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[1] = new EnemyShieldOrb(enemies[1]);
            enemies[4] = new EnemySniper(enemies[4]);
            // orbs
            layout = new RoomLayout(enemies.Take(2).ToList());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29086, -9916, 10777)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-28536, -14357, 10371)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29569, -16822, 10366)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29814, -13931, 10695)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29038, -12480, 10703)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-27978, -9997, 10363)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29744, -12139, 10468)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-28009, -13996, 10405)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-30422, -16503, 10320)));
            Rooms.Add(layout);
            // enemies
            layout = new RoomLayout(enemies.Skip(2).ToList());
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29314, -8254, 10208), new Vector3f(-30299, -7818, 10199), new Angle(0.72f, 0.69f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29541, -9429, 10208), new Vector3f(-30420, -10323, 10199), new Angle(0.68f, 0.73f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29869, -10720, 10199), new Vector3f(-28847, -11288, 10208), new Angle(0.72f, 0.70f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29107, -13556, 10199), new Vector3f(-30473, -13004, 10208), new Angle(0.69f, 0.73f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29560, -14503, 10207), new Vector3f(-30990, -15147, 10208), new Angle(0.69f, 0.72f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-28583, -15972, 10200), new Vector3f(-29647, -15378, 10199), new Angle(0.73f, 0.68f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-30591, -9420, 10760), new Vector3f(-30605, -8235, 10759), new Angle(0.31f, 0.95f)).Mask(SpawnPlane.Mask_Highground).AsVerticalPlane()); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-30155, -15339, 10508), new Angle(0.61f, 0.79f)).Mask(SpawnPlane.Mask_Highground)); // red crate
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-30880, -15989, 10495), new Angle(0.53f, 0.85f)).Mask(SpawnPlane.Mask_Highground)); // ramp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29031, -17855, 10300), new Angle(0.78f, 0.63f)).Mask(SpawnPlane.Mask_Highground)); // near default sniper
             
            // sniper
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// TODO
            ///////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////


            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29492, -13250, 10579), new Vector3f(-29920, -9114, 10822), new Angle(0.71f, 0.71f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29262, -17867, 10652), new Angle(0.74f, 0.67f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-31403, -16079, 10775), new Angle(0.30f, 0.95f)).Mask(SpawnPlane.Mask_Airborne));

            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-30707, -14634, 10199), new Angle(0.24f, 0.97f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0.005f, HorizontalSpeed = 1 , VerticalAngle = 0})); // last platform, behind laser

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-28862, -13146, 10200), new Angle(0.98f, 0.19f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 40, HorizontalSpeed = 30, VerticalAngle = 0 })); // last platform, ledge

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-31062, -9679, 10208), new Angle(-0.28f, 0.96f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0.05f, HorizontalSpeed = 1, VerticalAngle = 0, VisibleLaserLength = 0})); // ninja behind left billboard
            Rooms.Add(layout);



            //// Room 19 ////
            enemies = room_HC_19.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyDrone(enemies[0]);
            enemies[1] = new EnemyDrone(enemies[1]);
            enemies[2] = new EnemyDrone(enemies[2]);
            enemies[3].SetEnemyType(EnemyTypes.Weeb);
            enemies[4].SetEnemyType(EnemyTypes.Weeb);
            enemies[5].SetEnemyType(EnemyTypes.Weeb);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, enemyIndex: 5);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, enemyIndex: 1);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, enemyIndex: 0);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-27856, -31588, 7299), new Vector3f(-29323, -30200, 7299), new Angle(0.69f, 0.72f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(4).AllowSplitter());
            // drones
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-29472, -24766, 8503), new Angle(0.74f, 0.67f)).Mask(SpawnPlane.Mask_Airborne)); // only one...
            // high
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-27221, -31428, 8180), new Angle(0.97f, 0.26f)).Mask(SpawnPlane.Mask_Highground)); // fuel tank
            Rooms.Add(layout);


            //// ChainedOrbs ////
            // room 2, alternative default 
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(57688, 4213, 5319)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(58203, 4093, 5476), new QuaternionAngle(-0.50f, 0.50f, -0.50f, 0.50f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0.005f, HorizontalSpeed = 1, VerticalAngle = 0 }));
            chainedOrbs_Rooms.Add(layout);

            // before room 6
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(452, -1332, 6381)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(1435, 2466, 5000), new Angle(-0.88f, 0.48f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, HorizontalSpeed = 0, VerticalAngle = 10 }));
            chainedOrbs_Rooms.Add(layout);

            // room 8
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8842, -792, 7283)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9761, -1059, 7462), new QuaternionAngle(0.50f, 0.50f, -0.50f, 0.50f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0.005f, HorizontalSpeed = 1, VerticalAngle = 80 }));
            chainedOrbs_Rooms.Add(layout);

            // room 12
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-10464, 20394, 8784)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-10606, 21143, 8612), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, HorizontalSpeed = 0, VerticalAngle = 15 }));
            chainedOrbs_Rooms.Add(layout);

            // room 13
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-14879, 14297, 8527)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-12450, 13512, 8831), new Angle(-0.92f, 0.39f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, HorizontalSpeed = 0, VerticalAngle = 25 }));
            chainedOrbs_Rooms.Add(layout);

            // room 15
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-34008, 10238, 8729)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-32137, 7993, 8520), new Angle(0.58f, 0.81f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, HorizontalSpeed = 0, VerticalAngle = 27, Range = 3500}));
            chainedOrbs_Rooms.Add(layout);
        }

        public override void RandomizeEnemies(Process game) {
            base.RandomizeEnemies(game);
            ChainedOrb.Randomize(ref chainedOrbs, ref chainedOrbs_Rooms);
        }


        public void Gen_Nightmare() {
            throw new System.NotImplementedException();
        }


        protected override void Gen_PerRoom() {}

    }
}