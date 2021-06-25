using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.MemoryUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.Maps {
    class JackedUp : MapCore, IModes {

        #region Classic Rooms
        private Room room_1 = new Room(new Vector3f(-9297, -23929, 546), new Vector3f(-5180, -17091, 2281));
        private Room room_2 = new Room(new Vector3f(-30424, -22624, 155), new Vector3f(-21734, -16856, 3089));
        private Room room_3 = new Room(new Vector3f(-40820, -11223, 5152), new Vector3f(-32991, -4121, 10285));
        private Room room_4 = new Room(new Vector3f(-68632, -26742, 8423), new Vector3f(-63143, -19223, 11190)); 
        private Room room_5 = new Room(new Vector3f(-61959, -27794, 9791), new Vector3f(-60096, -25898, 10758)); // lonely enemy in last fight
        #endregion

        #region HC Room
        private Room HC_room_1 = new Room(new Vector3f(-10418, -19911, -558), new Vector3f(-19289, -15061, 2642)); // shielder, pistol, 2 froggers
        private Room HC_room_2 = new Room(new Vector3f(-22026, -16718, 611), new Vector3f(-31601, -22657, 4125)); // shifter(3 spots), waver, uzi (no cp)
        private Room HC_room_3 = new Room(new Vector3f(-32842, -11689, 8893), new Vector3f(-40909, -3982, 4093)); // 1 shield orb(4 targets), 2froggers, uzi, pistol
        private Room HC_room_4 = new Room(new Vector3f(-46559, -8604, 5904), new Vector3f(-53913, 3282, 9809)); // uzi, 2 drones, waver, weebs (no cp)
        private Room HC_room_5 = new Room(new Vector3f(-48568, -15142, 6046), new Vector3f(-56215, -24350, 11065)); // 4 orbs, weeb, pistol
        private Room HC_room_6 = new Room(new Vector3f(-59287, -19367, 8290), new Vector3f(-70990, -28324, 12126)); // orb, 2 uzi, 2 wavers, pistol, shielder, frogger
        private Room HC_room_7 = new Room(new Vector3f(-71723, -24489, 8586), new Vector3f(-79006, -13803, 14690)); // 3 pistols, frogger, weeb, waver
        #endregion

        public JackedUp() : base(MapType.JackedUp) {
            ModifyCP(new DeepPointer(PtrDB.DP_JackedUp_ElevatorCP), new Vector3f(-6140, -25230, 1645), GameHook.game);
        }

        public void Gen_Normal() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game);
            Rooms = new List<RoomLayout>();
            RoomLayout layout;

            List<Enemy> enemies = room_1.ReturnEnemiesInRoom(AllEnemies);
            enemies.AddRange(room_5.ReturnEnemiesInRoom(AllEnemies));
            RandomPickEnemiesWithoutCP(ref enemies, force: true, removeCP: false);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, removeCP: false);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, removeCP: false);

            //// room 2 layout ////
            layout = new RoomLayout(room_2.ReturnEnemiesInRoom(AllEnemies)); //pistols
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-21196, -18213, 2188), new Angle(0.32f, 0.95f)));//box near cp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-24745, -17806, 2078), new Angle(-0.18f, 0.98f)));//box on the left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-25774, -20884, 3251), new Angle(0.25f, 0.97f)).setDiff(1));//on right grapple line
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-25041, -19397, 2438), new Angle(0.07f, 1.00f)));//box near middle guy
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-24383, -17169, 1583), new Vector3f(-23624, -17782, 1598), new Angle(-0.24f, 0.97f)));//guy on the left spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-24817, -20152, 1958), new Vector3f(-26861, -19575, 1958), new Angle(-0.01f, 1.00f)));//middle guy spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-27271, -21702, 2508), new Vector3f(-26087, -22419, 2523), new Angle(0.37f, 0.93f))); //guy on the right spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-28177, -17552, 2254), new Vector3f(-27686, -18289, 2205), new Angle(-0.02f, 1.00f))); //far guy spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-28915, -20519, 2568), new Vector3f(-27719, -19575, 2568), new Angle(-0.02f, 1.00f)));//near button
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-26610, -20559, 3383), new Angle(0.16f, 0.99f)).setDiff(1));//on crane near button
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-23195, -20494, 2638), new Vector3f(-24397, -20477, 2638), new Angle(0.25f, 0.97f)).SetMaxEnemies(2).setDiff(1));//billboard on the right
            Rooms.Add(layout);

            //// room 3 layout ////
            layout = new RoomLayout(room_3.ReturnEnemiesInRoom(AllEnemies)); // 5 pistols
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-33411, -10404, 6202), new Vector3f(-35089, -9769, 6202), new Angle(0.68f, 0.73f)));//closer right platformspawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-37399, -10206, 6198), new Vector3f(-40051, -9827, 6203), new Angle(0.23f, 0.97f))); //far right platform spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-38782, -8209, 6203), new Vector3f(-40321, -7547, 6203), new Angle(0.01f, 1.00f))); //far middle platform spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-33776, -8237, 6194), new Vector3f(-36841, -7593, 6194), new Angle(0.0f, 1.00f)));//closer middle platform spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-36922, -5423, 6198), new Vector3f(-34115, -5995, 6202), new Angle(-0.68f, 0.74f)));//closer left platformspawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-40545, -5525, 6195), new Vector3f(-38839, -6006, 6202), new Angle(-0.03f, 1.00f))); //far left platform spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-37883, -7182, 6881), new Angle(-0.09f, 1.00f)));//billboard in the middle
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-36108, -10755, 6917), new Angle(0.38f, 0.93f)));//billboard on the right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-37630, -5086, 6880), new Angle(-0.26f, 0.96f)));//billboard on the left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-35764, -9000, 7410), new Vector3f(-38934, -9000, 7410), new Angle(0.09f, 1.00f)).SetMaxEnemies(2).setDiff(1));//grapples line on the right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-35077, -6800, 7410), new Vector3f(-36823, -6800, 7410), new Angle(-0.11f, 0.99f)).setDiff(1));//grapples line on the left
            Rooms.Add(layout);

            //// room 4 layout ////
            enemies = room_4.ReturnEnemiesInRoom(AllEnemies);
            EnemyShieldOrb shieldOrb1 = new EnemyShieldOrb(enemies[0]);
            shieldOrb1.HideBeam_Range(0, 2);
            shieldOrb1.HideBeam_Range(1, 2);
            shieldOrb1.LinkObject(2);

            layout = new RoomLayout(shieldOrb1); //orb
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-65251, -20095, 10823), new Vector3f(-66206, -19589, 11027)));//near most left enemy
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-62300, -26600, 10131), new Vector3f(-62300, -25600, 10564)).setDiff(1));//back side of the billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-63103, -27335, 10716)).setDiff(1));//top of the second billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-65438, -25916, 12028)));//uptop moving platform on the right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67475, -23533, 9396), new Vector3f(-67475, -22526, 9750)));//billboard near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-68975, -23056, 9508))); // near door, on moving platform

            Rooms.Add(layout);

            layout = new RoomLayout(enemies.Skip(1).ToList()); //4 pistols + 1 uzi
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67825, -21555, 11403), new Vector3f(-66226, -21555, 11403), new Angle(-0.08f, 1.00f)).setDiff(1));//uptop left grapple point
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67865, -24555, 11413), new Vector3f(-66417, -24555, 11413), new Angle(0.11f, 0.99f)).setDiff(1));//uptop right grapple point
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-62400, -23965, 10563), new Vector3f(-62400, -21730, 10563), new Angle(0.00f, 1.00f)).SetMaxEnemies(2).setDiff(1));//grapple line 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-60731, -24760, 10355), new Angle(0.72f, 0.70f)).setDiff(1)); //billboard on the way to that lost pistol
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-64572, -23000, 9968), new Vector3f(-63646, -23000, 9968), new Angle(0.02f, 1.00f)));//left cargo
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-64530, -24300, 9968), new Vector3f(-63576, -24300, 9968), new Angle(0.29f, 0.96f)));//right cargo

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-68054, -19559, 10152), new Vector3f(-67168, -20614, 10152)).RandomAngle());//upper left  enemy spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67327, -21076, 9465), new Vector3f(-66308, -22052, 9472), new Angle(-0.37f, 0.93f)));//bottom left enemy spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-68105, -25048, 9452), new Vector3f(-66263, -24006, 9452), new Angle(0.00f, 1.00f)));//bottom right enemy spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67064, -26495, 10153), new Vector3f(-66309, -25499, 10154), new Angle(0.20f, 0.98f)));//upper right  enemy spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-64454, -26275, 10162), new Vector3f(-63856, -25658, 10163), new Angle(0.46f, 0.89f)));//closer right enemy spawn plane
            Rooms.Add(layout);

            //// EXTRA //// 
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-17322, -17173, 888), new Angle(0.04f, 1.00f))); // Room 1
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-27473, -10452, 5892), new Angle(-0.99f, 0.12f))); // Between Room 2 & 3
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-30844, -7872, 6513), new Angle(0.02f, 1.00f))); // Between Room 2 & 3
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-46950, -8139, 7306), new Angle(0.02f, 1.00f))); // before button room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-46447, -19445, 8274), new Angle(0.45f, 0.89f))); // lasers room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-54132, -19711, 9600), new Angle(0.15f, 0.99f))); // after lasers room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-58270, -21755, 9548), new Angle(0.59f, 0.81f))); // hallway before last fight room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-75493, -18342, 11185), new Angle(-0.23f, 0.97f))); // before elevator
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-78881, -14708, 13053), new Angle(-0.06f, 1.00f))); // elevator hallway
            Rooms.Add(layout);
        }

        public void Gen_Easy() {
            Gen_Normal();
        }

        public void Gen_Hardcore() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game);
            Rooms = new List<RoomLayout>();
            RoomLayout layout;

            
            //// Room 1 ////
            var enemies = HC_room_1.ReturnEnemiesInRoom(AllEnemies);
            EnemiesWithoutCP.AddRange(enemies);


            //// Room 2 ////
            enemies = HC_room_2.ReturnEnemiesInRoom(AllEnemies);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Waver);
            RemoveParentObjects(ref enemies);

            // SHIFTER LAYOUT
            var Shifter = new EnemyShifter(enemies[0], 3);
            layout = new RoomLayout(Shifter);

            // ROOM 6
            var info = new ShifterSpawnInfo();
            info.shiftPoints = new List<Tuple<Vector3f, Angle>>() { 
                 new Tuple<Vector3f, Angle>(new Vector3f(-65105, -19758, 10419), new Angle(-0.48f, 0.88f)),
                 new Tuple<Vector3f, Angle>(new Vector3f(-66265, -26261, 10153), new Angle(0.50f, 0.87f)),
                 new Tuple<Vector3f, Angle>(new Vector3f(-63713, -26258, 10283), new Angle(0.56f, 0.83f))};
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-60848, -21594, 9558), new Angle(0.07f, 1.00f)).SetSpawnInfo(info));

            // LAST ROOM
            info = new ShifterSpawnInfo();
            info.shiftPoints = new List<Tuple<Vector3f, Angle>>() {
                 new Tuple<Vector3f, Angle>(new Vector3f(-73999, -18201, 9448), new Angle(-0.69f, 0.72f)),
                 new Tuple<Vector3f, Angle>(new Vector3f(-75569, -18357, 10182), new Angle(-0.07f, 1.00f)),
                 new Tuple<Vector3f, Angle>(new Vector3f(-73826, -17115, 10835), new Angle(-0.70f, 0.71f))};
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-72762, -23132, 9445), new Angle(0.04f, 1.00f)).SetSpawnInfo(info));

            // ROOM 5
            info = new ShifterSpawnInfo();
            info.shiftPoints = new List<Tuple<Vector3f, Angle>>() {
                 new Tuple<Vector3f, Angle>(new Vector3f(-54514, -22173, 9148), new Angle(0.36f, 0.93f)),
                 new Tuple<Vector3f, Angle>(new Vector3f(-55268, -19687, 9600), new Angle(0.34f, 0.94f)),
                 new Tuple<Vector3f, Angle>(new Vector3f(-51200, -16905, 8348), new Angle(0.00f, 1.00f))};
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-51610, -15989, 6948), new Angle(-0.13f, 0.99f)).SetSpawnInfo(info));

            // ROOM 4
            info = new ShifterSpawnInfo();
            info.shiftPoints = new List<Tuple<Vector3f, Angle>>() {
                 new Tuple<Vector3f, Angle>(new Vector3f(-51710, -5461, 7298), new Angle(-0.27f, 0.96f)),
                 new Tuple<Vector3f, Angle>(new Vector3f(-47920, -3635, 7299), new Angle(-0.74f, 0.67f)),
                 new Tuple<Vector3f, Angle>(new Vector3f(-52382, 728, 7598), new Angle(-0.53f, 0.85f))};
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-47052, -7710, 7303), new Angle(0.04f, 1.00f)).SetSpawnInfo(info));

            layout.DoNotReuse();
            Rooms.Add(layout);

            enemies.RemoveAt(0); // remove shifter from list.
            EnemiesWithoutCP.AddRange(enemies); // to dynamic enemy list


            //// Room 3 ////
            enemies = HC_room_3.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            // orb
            layout = new RoomLayout(enemies[0]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-37977, -5307, 6943))); // floating between billboards, left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-36410, -6743, 5877))); // below platform level, left near (grapple above)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-39168, -8963, 5925))); // below platform level, right far
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-36917, -7967, 6198))); // middle platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-40562, -8001, 6758))); // near exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-38650, -6785, 6268))); // behind pillar
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-38129, -10303, 6557))); // above platform light
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-38005, -4924, 5729))); // below billboard, left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-36112, -10877, 5729))); // below billboard, right
            Rooms.Add(layout);

            // enemies
            layout = new RoomLayout(enemies.Skip(1).ToList());
            // flatground, default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-39945, -10210, 6198), new Vector3f(-36948, -9821, 6203), new Angle(0.31f, 0.95f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-34518, -10335, 6198), new Vector3f(-33647, -9851, 6195), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-35445, -7664, 6194), new Vector3f(-36837, -8129, 6194), new Angle(-0.01f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-36922, -5967, 6198), new Vector3f(-35200, -5469, 6198), new Angle(-0.57f, 0.82f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-39038, -6021, 6202), new Vector3f(-40427, -5554, 6195), new Angle(-0.03f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-40329, -8199, 6203), new Vector3f(-38930, -7652, 6198), new Angle(0.04f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            // high grounds
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-40727, -5961, 6378), new Angle(0.04f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // red crate
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-40519, -10220, 6702), new Angle(0.07f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // scaffolding
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-33227, -10274, 6584), new Angle(0.92f, 0.39f)).Mask(SpawnPlane.Mask_Highground)); // scaffolding 2
            // billboards
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-37450, -7193, 6881), new Vector3f(-38250, -7194, 6881), new Angle(-0.34f, 0.94f)).Mask(SpawnPlane.Mask_Highground).AsVerticalPlane());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-36578, -10740, 6917), new Vector3f(-35658, -10743, 6917), new Angle(0.75f, 0.66f)).Mask(SpawnPlane.Mask_Highground).AsVerticalPlane());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-38474, -5058, 6881), new Vector3f(-37492, -5067, 6881), new Angle(-0.39f, 0.92f)).Mask(SpawnPlane.Mask_Highground).AsVerticalPlane());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-37336, -6406, 6881), new Vector3f(-38446, -6403, 6881), new Angle(0.32f, 0.95f)).Mask(SpawnPlane.Mask_Highground).AsVerticalPlane());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-38269, -4831, 7048), new Vector3f(-37686, -4838, 7048), new Angle(-0.66f, 0.75f)).Mask(SpawnPlane.Mask_Highground).AsVerticalPlane()); // beam behind billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-35827, -10920, 7049), new Vector3f(-36396, -10932, 7049), new Angle(0.70f, 0.72f)).Mask(SpawnPlane.Mask_Highground).AsVerticalPlane()); // beam behind billboard
            // high places
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-40492, -8742, 7518), new Angle(0.24f, 0.97f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // grappling hook spawner block
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-40477, -7042, 7518), new Angle(-0.30f, 0.95f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // grappling hook spawner block
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-37388, -4945, 7660), new Angle(-0.44f, 0.90f)).Mask(SpawnPlane.Mask_HighgroundLimited).setRarity(0.3)); // beam top
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-38476, -4916, 7660), new Angle(-0.36f, 0.93f)).Mask(SpawnPlane.Mask_HighgroundLimited).setRarity(0.3)); // beam top
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-36627, -10897, 7660), new Angle(0.52f, 0.85f)).Mask(SpawnPlane.Mask_HighgroundLimited).setRarity(0.3)); // beam top
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-35544, -10856, 7660), new Angle(0.45f, 0.89f)).Mask(SpawnPlane.Mask_HighgroundLimited).setRarity(0.3)); // beam top
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-39581, -10103, 6802), new Vector3f(-37440, -10169, 6802), new Angle(0.26f, 0.96f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-37845, -7930, 6886), new Angle(0.01f, 1.00f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-36212, -5727, 6723), new Angle(-0.41f, 0.91f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);



            //// Room 4 ////
            enemies = HC_room_4.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyDrone(enemies[0]);
            enemies[1] = new EnemyDrone(enemies[1]);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[4].SetEnemyType(Enemy.EnemyTypes.Weeb);
            RemoveParentObjects(ref enemies);
            EnemiesWithoutCP.AddRange(enemies);


            //// Room 5 ////
            enemies = HC_room_5.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[1] = new EnemyShieldOrb(enemies[1]);
            enemies[2] = new EnemyShieldOrb(enemies[2]);
            enemies[3] = new EnemyShieldOrb(enemies[3]);
            enemies[5].SetEnemyType(Enemy.EnemyTypes.Weeb);
            // orbs
            layout = new RoomLayout(enemies.Take(4).ToList());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-51746, -21124, 9288))); // moving platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-52927, -18843, 8129), new Vector3f(-55267, -18846, 8129)).AsVerticalPlane()); // moving platform range
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-51488, -15531, 9107))); // vertical moving platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-48998, -15981, 8648), new Vector3f(-49007, -17450, 8648)).AsVerticalPlane()); // pipes above entrance
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-50208, -19290, 9186), new Vector3f(-50201, -18359, 9186))); // platform spawner rectangle
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-55360, -13667, 8210), new Vector3f(-51692, -13733, 8210)).AsVerticalPlane()); // moving platform range
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-55843, -17560, 9897))); // near exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-52566, -19788, 9394))); // wall light
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-55443, -20536, 8276), new Vector3f(-54148, -20544, 8276)).AsVerticalPlane()); // moving platform beam
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-55623, -16391, 5849))); // under moving platform (cp)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-54675, -14645, 7727), new Vector3f(-54675, -14645, 9758)).AsVerticalPlane()); // vertical moving platform range
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-55708, -16032, 9143))); // wall pipes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-54025, -15095, 9780))); // high between vertical beams
            Rooms.Add(layout);

            // enemies
            layout = new RoomLayout(enemies.Skip(4).ToList());
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-51417, -17609, 6798), new Vector3f(-51657, -16889, 6798), new Angle(0.04f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-52321, -17229, 6798), new Angle(0.04f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-51883, -17690, 8348), new Vector3f(-51376, -17114, 8348), new Angle(0.48f, 0.88f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-51189, -17989, 8358), new Vector3f(-50553, -17744, 8348), new Angle(0.83f, 0.55f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-50417, -22478, 8348), new Vector3f(-52324, -22892, 8348), new Angle(0.70f, 0.72f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-54479, -22928, 9148), new Vector3f(-54797, -22455, 9148), new Angle(0.32f, 0.95f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-54325, -20263, 9500), new Vector3f(-55745, -19865, 9498), new Angle(0.28f, 0.96f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-55416, -19718, 9498), new Vector3f(-55733, -17936, 9498), new Angle(-0.06f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-53904, -14532, 8098), new Angle(-0.32f, 0.95f)).Mask(SpawnPlane.Mask_Flatground));
            // high
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-55864, -18080, 10195), new Angle(-0.32f, 0.95f)).Mask(SpawnPlane.Mask_Highground)); // exit sign
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-54601, -23262, 9298), new Angle(0.36f, 0.93f)).Mask(SpawnPlane.Mask_Highground)); // crate
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-53393, -19821, 8648), new Angle(0.58f, 0.81f)).Mask(SpawnPlane.Mask_Highground)); // crates
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-51852, -17776, 9633), new Angle(0.34f, 0.94f)).Mask(SpawnPlane.Mask_Highground)); // extruded wall piece
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-49005, -16982, 8998), new Vector3f(-48984, -17680, 8998), new Angle(-1.00f, 0.00f)).Mask(SpawnPlane.Mask_Highground).AsVerticalPlane()); // pipe above entrance
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-51420, -14610, 9398), new Angle(-0.82f, 0.57f)).Mask(SpawnPlane.Mask_Highground));
            // special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-52560, -18860, 7748), new Angle(0.24f, 0.97f)).Mask(SpawnPlane.Mask_HighgroundLimited).setRarity(0.25));  // below moving platform, on small ledge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-52721, -12971, 8209), new Angle(-0.60f, 0.80f)).Mask(SpawnPlane.Mask_HighgroundLimited).setRarity(0.25)); // far left, small pipe
            // additional(drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-53769, -17060, 7764), new Angle(-0.03f, 1.00f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-53335, -23387, 9054), new Angle(0.40f, 0.91f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-55338, -19881, 9996), new Angle(0.42f, 0.91f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-51617, -16141, 7470), new Angle(-0.16f, 0.99f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);


            //// Room 6 ////
            enemies = HC_room_6.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[5].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[6].SetEnemyType(Enemy.EnemyTypes.Waver);
            layout = new RoomLayout(enemies[0]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-68920, -27801, 9476))); // moving platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-66788, -25615, 9643))); // under platform (easy)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-70038, -20384, 13023)).setRarity(0.3)); // highest platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67754, -21519, 11403))); // floating beam 1
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-66263, -24560, 11413))); // floating beam 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-63118, -27884, 10840))); // on billboard, right fan
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-71038, -22705, 9847))); // light near exit, left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-71072, -23471, 9847))); // light near exit, right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-65927, -21556, 11403))); // floating beam 1 right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-68176, -22354, 10408), new Vector3f(-68174, -23776, 10408)).AsVerticalPlane()); // slowmo ledge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-65458, -24902, 13437)).setRarity(0.3)); // very high, (need to use vertical elevator)
            Rooms.Add(layout);

            // enemies
            layout = new RoomLayout(enemies.Skip(1).ToList());
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-60772, -26787, 10163), new Vector3f(-61630, -27405, 10162), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-62280, -27505, 10162), new Angle(0.43f, 0.90f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-63088, -25627, 10161), new Vector3f(-64585, -26063, 10162), new Angle(0.73f, 0.68f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-66337, -26449, 10153), new Vector3f(-66962, -25605, 10154), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-65372, -20007, 10140), new Vector3f(-66435, -19762, 10140), new Angle(-0.39f, 0.92f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-68005, -20481, 10143), new Vector3f(-67462, -19705, 10143), new Angle(-0.64f, 0.77f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67275, -21134, 9465), new Vector3f(-66383, -21965, 9468), new Angle(-0.24f, 0.97f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-68059, -25051, 9450), new Vector3f(-66412, -24286, 9445), new Angle(0.14f, 0.99f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-70833, -23329, 9448), new Vector3f(-69862, -22777, 9448), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-66184, -22484, 10408), new Vector3f(-66731, -23676, 10408), new Angle(0.08f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            // high
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-63099, -26938, 10716), new Angle(0.61f, 0.79f)).Mask(SpawnPlane.Mask_Highground)); // billboard, near fan
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-64558, -24322, 9968), new Angle(0.27f, 0.96f)).Mask(SpawnPlane.Mask_Highground)); // middle floating block 1
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-64551, -22973, 9968), new Angle(0.11f, 0.99f)).Mask(SpawnPlane.Mask_Highground)); // middle flopating block 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-71067, -23034, 10286), new Angle(0.01f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // light above exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67662, -24594, 11413), new Vector3f(-66446, -24611, 11413), new Angle(-0.65f, 0.76f)).AsVerticalPlane().Mask(SpawnPlane.Mask_Highground)); // floating beam
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-66481, -21551, 11403), new Vector3f(-67585, -21543, 11403), new Angle(-0.70f, 0.72f)).AsVerticalPlane().Mask(SpawnPlane.Mask_Highground)); // floating beam
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-60293, -23460, 10285), new Angle(0.95f, 0.30f)).Mask(SpawnPlane.Mask_Highground)); // extruded wall part, near start
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-68011, -21274, 9765), new Angle(-0.22f, 0.98f)).Mask(SpawnPlane.Mask_Highground)); // crate
            // additional (drone)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-66118, -21743, 10275), new Angle(-0.18f, 0.98f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-68175, -24704, 10690), new Angle(0.30f, 0.95f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67049, -26723, 10797), new Angle(0.45f, 0.89f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-65458, -25058, 10815), new Vector3f(-65466, -21297, 10815), new Angle(0.01f, 1.00f)).AsVerticalPlane().Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);


            //// Room 7 ////
            enemies = HC_room_7.ReturnEnemiesInRoom(AllEnemies);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[5].SetEnemyType(Enemy.EnemyTypes.Weeb);
            EnemiesWithoutCP.AddRange(enemies);


            //// EXTRA ////
            layout = new RoomLayout();
            // room 1 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-17625, -16367, 888), new Angle(-0.12f, 0.99f)).Mask(SpawnPlane.Mask_Flatground)); // near exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-17287, -17283, 1374), new Angle(0.12f, 0.99f)).Mask(SpawnPlane.Mask_Airborne)); // near exit door
            // room 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-21877, -19096, 1608), new Angle(0.18f, 0.98f)).Mask(SpawnPlane.Mask_Highground)); // after cp, on ledge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-27779, -19847, 2568), new Angle(0.06f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // middle platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-31017, -20938, 2998), new Angle(0.20f, 0.98f))); // verical lasers room
            // between room 2 and 3 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-32163, -12508, 5903), new Angle(-0.65f, 0.76f)).Mask(SpawnPlane.Mask_Highground)); // aiming down the laser walls hallway
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-31085, -7885, 6513), new Angle(-0.01f, 1.00f)).Mask(SpawnPlane.Mask_Flatground)); // hallway
            // between room 3 and 4 (button)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-49218, -7660, 7689), new Angle(-0.07f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // scaffolding
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-52185, 533, 7298), new Angle(-0.51f, 0.86f)).Mask(SpawnPlane.Mask_Highground)); // near button
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-48468, -3795, 7298), new Angle(-0.65f, 0.76f)).Mask(SpawnPlane.Mask_Highground)); // middle platform
            // moving laser coridor
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-48055, -11608, 8202), new Angle(0.82f, 0.57f)).Mask(SpawnPlane.Mask_Highground)); 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-48690, -13023, 8193), new Angle(0.66f, 0.75f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-45700, -19190, 8580), new Angle(0.59f, 0.81f)).Mask(SpawnPlane.Mask_Airborne));
            // before room 6
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-58091, -21274, 9548), new Angle(0.63f, 0.77f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-57595, -22183, 10293), new Angle(0.73f, 0.69f)).Mask(SpawnPlane.Mask_HighgroundLimited));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-58237, -21765, 10166), new Angle(0.60f, 0.80f)).Mask(SpawnPlane.Mask_Airborne));
            // room 7 (before elevator)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-74640, -20592, 9947), new Angle(-0.33f, 0.94f)).Mask(SpawnPlane.Mask_Highground)); // pipe
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-83129, -14731, 13038), new Angle(0.01f, 1.00f)).Mask(SpawnPlane.Mask_Flatground)); // before elevator
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-75398, -15344, 12748), new Angle(-0.61f, 0.80f)).Mask(SpawnPlane.Mask_Highground)); // last platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-78044, -14506, 13386), new Angle(-0.28f, 0.96f)).Mask(SpawnPlane.Mask_Airborne)); // above vertical laser
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-74187, -22740, 9948), new Angle(-0.12f, 0.99f)).Mask(SpawnPlane.Mask_Airborne)); // infront of door
            Rooms.Add(layout);
        }

        public void Gen_Nightmare() {throw new NotImplementedException();}

        protected override void Gen_PerRoom() { }
    }
}


