using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.GameObjects;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.MemoryUtils;
using GhostrunnerRNG.NonPlaceableObjects;
using GhostrunnerRNG.Windows;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GhostrunnerRNG.Maps {
    class ForbiddenZone : MapCore, IModes {

        #region Rooms
        // Note: turrets are not attached to any cp
        private Room room_1 = new Room(new Vector3f(62261, -2940, 3347), new Vector3f(67462, -10153, 490)); // 1 turret (near door)
        private Room room_2 = new Room(new Vector3f(67115, -11747, 3615), new Vector3f(66021, -17789, 1146)); // 1 turrets (above)
        private Room room_3 = new Room(new Vector3f(66841, -26139, 1352), new Vector3f(63662, -19511, 4167)); // 1 turrets (on the ledge above)
        private Room room_4 = new Room(new Vector3f(60792, -38343, 3747), new Vector3f(66751, -35353, 1490)); //  1 turrets (180 range turret)
        private Room room_5 = new Room(new Vector3f(75569, -41389, 349), new Vector3f(71659, -46911, 2439)); // 4 pistols (needed for door cp)
        private Room room_6 = new Room(new Vector3f(87488, -44137, 3881), new Vector3f(90630, -49472, 877)); // 3 turrets crossover
        private Room room_7 = new Room(new Vector3f(92640, -54591, 705), new Vector3f(82964, -50160, 3330)); // 2 froggers
        private Room room_8 = new Room(new Vector3f(89122, -57689, 4412), new Vector3f(97018, -65005, 1806)); // 2 shielders, 3 pistols, cp to door
        private Room room_9 = new Room(new Vector3f(90465, -56928, 4188), new Vector3f(104153, -54441, 6018)); // 2 weebs, 2 drones
        private Room room_10 = new Room(new Vector3f(105266, -57720, 7340), new Vector3f(116584, -53790, 3834)); // 2 turrets
        private Room room_11 = new Room(new Vector3f(124811, -57568, 6287), new Vector3f(136786, -53558, 11754)); // 2 orb, 2 weebs, 2 drones, 2 pistols, 3 shielders
        private Room room_12 = new Room(new Vector3f(144024, -60005, 10144), new Vector3f(148927, -54903, 5862)); // 4 turrets
        #endregion

        #region HC Rooms
        private Room room_HC_1 = new Room(new Vector3f(67473, -2986, 802), new Vector3f(60269, -10640, 3483));
        private Room room_HC_2 = new Room(new Vector3f(67409, -16906, 1049), new Vector3f(66018, -22243, 2079)); // no cp required
        private Room room_HC_3 = new Room(new Vector3f(64888, -18641, 833), new Vector3f(59974, -24095, 2849));
        private Room room_HC_4 = new Room(new Vector3f(61252, -38514, 1244), new Vector3f(67605, -33726, 2979));
        private Room room_HC_5 = new Room(new Vector3f(71522, -47214, 750), new Vector3f(76325, -41074, 2771));
        private Room room_HC_6 = new Room(new Vector3f(85129, -45779, 4430), new Vector3f(90979, -50034, 922));
        private Room room_HC_7 = new Room(new Vector3f(89862, -65962, 2868), new Vector3f(96512, -57857, 5028));
        private Room room_HC_8 = new Room(new Vector3f(109438, -54540, 3965), new Vector3f(117466, -59404, 6772));
        private Room room_HC_9 = new Room(new Vector3f(124274, -53720, 6952), new Vector3f(136839, -57956, 15198));
        private Room room_HC_10 = new Room(new Vector3f(142975, -54368, 10393), new Vector3f(149445, -60676, 6050));
        private Room room_HC_11 = new Room(new Vector3f(160938, -50361, 11113), new Vector3f(167928, -51283, 12353)); // lonely pair
        #endregion

        public ForbiddenZone() : base(GameUtils.MapType.ForbiddenZone) {
            ModifyCP(new DeepPointer(PtrDB.DP_ForbiddenZone_ElevatorCP), new Vector3f(73395, -3290, 1340), GameHook.game);
        }

        public void Gen_Normal() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game, 0, 26);
            AllEnemies.AddRange(GetAllEnemies(GameHook.game, 27, 13));
            Rooms = new List<RoomLayout>();
            RoomLayout layout;
            List<Enemy> enemies;

            //// Room 1 - turret ////
            enemies = room_1.ReturnEnemiesInRoom(AllEnemies);
            EnemyTurret turret = new EnemyTurret(enemies[0]);
            layout = new RoomLayout(turret);

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63893, -9617, 2148), new Angle(0.70f, 0.71f)).SetSpawnInfo(new TurretSpawnInfo())); // default

            // right crate
            TurretSpawnInfo turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = -10;
            turretSpawn.HorizontalSpeed = 60;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66503, -7017, 1982), new Angle(0.93f, 0.37f)).SetSpawnInfo(turretSpawn));

            // Above exit door
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 10;
            turretSpawn.HorizontalSpeed = 50;
            turretSpawn.RotationOffset = 30;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65144, -9365, 2400), GameUtils.CreateQuaternion(90, 45, -90)).SetSpawnInfo(turretSpawn).setDiff(1));

            // Above elevator door
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 50;
            turretSpawn.HorizontalSpeed = 75;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71816, -3283, 2599), GameUtils.CreateQuaternion(-180, 90, 0)).SetSpawnInfo(turretSpawn).setDiff(1));

            // on the wall, above default
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 35;
            turretSpawn.HorizontalSpeed = 80;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64008, -9764, 2782), new QuaternionAngle(90, 90, 0)).SetSpawnInfo(turretSpawn).setDiff(1));

            layout.Mask(SpawnPlane.Mask_Turret);
            layout.DoNotReuse(); // avoid enemies to spawn in sentry spawns(on walls and sh*t)
            Rooms.Add(layout);


            //// Room 2 - turret ////
            enemies = room_2.ReturnEnemiesInRoom(AllEnemies);
            turret = new EnemyTurret(enemies[0]);
            layout = new RoomLayout(turret);

            // change cp to avoid spawn kill, to previous platform
            ModifyCP(new DeepPointer(PtrDB.DP_ForbiddenZone_Room2_CP), new Vector3f(66641, -9836, 1602), new Angle(-0.71f, 0.71f), GameHook.game);

            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66640, -13529, 2708), new Angle(-0.72f, 0.70f)).SetSpawnInfo(new TurretSpawnInfo()));

            // on OPEN sign
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 60;
            turretSpawn.HorizontalSpeed = 60;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66988, -17736, 1600), GameUtils.CreateQuaternion(180, 20, 90)).SetSpawnInfo(turretSpawn).setDiff(1));

            // Left of OPEN, right at the path
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 0;
            turretSpawn.HorizontalAngle = 60;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66289, -18175, 1368), new Angle(0.53f, 0.85f)).SetSpawnInfo(turretSpawn));

            // Edge of platform
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 0;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67029, -15266, 1368), new Angle(0.91f, 0.42f)).SetSpawnInfo(turretSpawn));

            // Around the corner, middle section
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 10;
            turretSpawn.HorizontalSpeed = 70;
            turretSpawn.HorizontalAngle = 60;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67068, -18426, 1368), new Angle(0.97f, 0.23f)).SetSpawnInfo(turretSpawn));

            // infront of exit door
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 0;
            turretSpawn.HorizontalSpeed = 30; // slow rotation
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66594, -21430, 1368), new Angle(0.70f, 0.72f)).SetSpawnInfo(turretSpawn));

            layout.Mask(SpawnPlane.Mask_Turret);
            layout.DoNotReuse();
            Rooms.Add(layout);

            //// Room 3 - turret ////
            enemies = room_3.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyTurret(enemies[0]);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65982, -23435, 2707), new Angle(1.00f, 0.00f)).SetSpawnInfo(new TurretSpawnInfo()));

            // on corner platform
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 0;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62127, -20326, 1330), new Angle(-0.58f, 0.81f)).SetSpawnInfo(turretSpawn));

            // on wall, below default
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 0;
            turretSpawn.HorizontalSpeed = 100;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65775, -23481, 2237), GameUtils.CreateQuaternion(90, 70, 265)).SetSpawnInfo(turretSpawn));

            // before exit gate
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 0;
            turretSpawn.HorizontalAngle = 45;
            turretSpawn.HorizontalSpeed = 30;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61773, -34820, 1658), new Angle(0.70f, 0.71f)).SetSpawnInfo(turretSpawn).setDiff(1));

            layout.Mask(SpawnPlane.Mask_Turret);
            layout.DoNotReuse();
            Rooms.Add(layout);

            //// Room 4 - turret ////
            enemies = room_4.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyTurret(enemies[0]);
            layout = new RoomLayout(enemies);

            // default - fast
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.HorizontalAngle = 60;
            turretSpawn.HorizontalSpeed = 60;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64047, -36344, 2073), new Angle(-0.77f, 0.64f)).SetSpawnInfo(turretSpawn).setDiff(1));

            // middle platform
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.HorizontalAngle = 40;
            turretSpawn.HorizontalSpeed = 60;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63797, -38589, 1658), new Angle(0.89f, 0.45f)).SetSpawnInfo(turretSpawn));

            // before exit door
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.HorizontalAngle = 40;
            turretSpawn.HorizontalSpeed = 60;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65415, -36222, 2050), GameUtils.CreateQuaternion(-90, 0, -90)).SetSpawnInfo(turretSpawn));

            layout.Mask(SpawnPlane.Mask_Turret);
            layout.DoNotReuse();
            Rooms.Add(layout);

            //// Room 5 - 4 pistols ////
            // addition cp before this room
            CustomCheckPoints.Add(new CustomCP(mapType, new Vector3f(74528, -37289, 3282), new Vector3f(73654, -38304, 1862),
                new Vector3f(74049, -37700, 2319), new Angle(-0.70f, 0.71f)));

            enemies = room_5.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies);
            // default platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74556, -45218, 1048), new Vector3f(73577, -45615, 1075), new Angle(0.69f, 0.72f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(72213, -44730, 1048), new Vector3f(72194, -46653, 1048), new Angle(0.16f, 0.99f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(73073, -46306, 1463), new Vector3f(74507, -46809, 1463), new Angle(1.00f, 0.02f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74884, -41629, 1498), new Vector3f(75591, -42059, 1497), new Angle(-0.72f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74895, -42102, 1527), new Vector3f(75182, -42600, 1497), new Angle(-0.49f, 0.87f)).Mask(SpawnPlane.Mask_Flatground));

            // special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74165, -44151, 850), new Vector3f(73965, -41994, 850), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground)); // after ramp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71750, -44749, 1406), new Angle(-0.22f, 0.98f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // red neon ad
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(72680, -45908, 1615), new Angle(0.99f, 0.13f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // on pipes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(72170, -46757, 2176), new Angle(0.69f, 0.72f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(75518, -46588, 2342), new Angle(1.00f, 0.08f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74456, -45056, 2248), new Angle(-1.00f, 0.03f)).setRarity(0.2).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // hovering scaffolding
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(75265, -43796, 2421), new Angle(-0.59f, 0.81f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // edge of billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74871, -41585, 2148), new Angle(-0.56f, 0.83f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // on pipe near exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(75724, -41664, 1856), new Angle(-0.92f, 0.40f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // red neon ad near exit
            Rooms.Add(layout);


            //// Room 6 - 3 turret ////
            enemies = room_6.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyTurret(enemies[0]);
            enemies[1] = new EnemyTurret(enemies[1]);
            enemies[2] = new EnemyTurret(enemies[2]);
            layout = new RoomLayout(enemies);

            // top platform, corner near wall
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = -10;
            turretSpawn.HorizontalAngle = 30;
            turretSpawn.HorizontalSpeed = 30;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(88917, -46753, 3128), new Angle(-0.85f, 0.53f)).SetSpawnInfo(turretSpawn));

            // top platform, door frame
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 0;
            turretSpawn.HorizontalAngle = 45;
            turretSpawn.HorizontalAngle = 20;
            turretSpawn.HorizontalSpeed = 30;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(87430, -46049, 3123), new Angle(-0.91f, 0.42f)).SetSpawnInfo(turretSpawn));

            // top platform, patroling first middle section
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = -10;
            turretSpawn.HorizontalAngle = 30;
            turretSpawn.HorizontalSpeed = 30;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(86301, -47015, 3138), new Angle(-0.71f, 0.71f)).SetSpawnInfo(turretSpawn));

            // middle platform, 180 range
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 0;
            turretSpawn.HorizontalAngle = 90;
            turretSpawn.HorizontalSpeed = 40;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(87254, -49471, 2559), new Angle(0.71f, 0.70f)).SetSpawnInfo(turretSpawn));

            // middle platform, corner near default
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 0;
            turretSpawn.HorizontalAngle = 45;
            turretSpawn.HorizontalSpeed = 20;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(88818, -49336, 2557), new Angle(0.92f, 0.39f)).SetSpawnInfo(turretSpawn));

            // lower small platform
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 10;
            turretSpawn.HorizontalAngle = 45;
            turretSpawn.HorizontalSpeed = 30;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(89662, -46543, 1618), new Angle(-0.92f, 0.38f)).SetSpawnInfo(turretSpawn));

            // door platform, corner
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 10;
            turretSpawn.HorizontalAngle = 45;
            turretSpawn.HorizontalSpeed = 80;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(90310, -49206, 1618), new Angle(0.96f, 0.30f)).SetSpawnInfo(turretSpawn));

            // middle platform, near default, on wall, 360 range
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 0;
            turretSpawn.HorizontalAngle = 180;
            turretSpawn.HorizontalSpeed = 40;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(89230, -48766, 2373), GameUtils.CreateQuaternion(0, 0, -90)).SetSpawnInfo(turretSpawn));

            // ceilng
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 20;
            turretSpawn.HorizontalAngle = 90;
            turretSpawn.HorizontalSpeed = 10;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(89256, -47800, 2800), GameUtils.CreateQuaternion(180, 0, 180)).SetSpawnInfo(turretSpawn).setDiff(1));

            layout.Mask(SpawnPlane.Mask_Turret);
            layout.DoNotReuse();
            Rooms.Add(layout);


            //// Room 7 - 2 froggers ////
            enemies = room_7.ReturnEnemiesInRoom(AllEnemies);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, removeCP: false); // always take one 
            RandomPickEnemiesWithoutCP(ref enemies, force: false, removeCP: false); // random chance to take/leave it
            layout = new RoomLayout(enemies);

            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(89143, -52354, 1613), new Vector3f(87611, -51051, 1613), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground)); // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(87721, -52721, 1908), new Angle(0.60f, 0.80f)).Mask(SpawnPlane.Mask_Highground)); // red crate
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(90811, -52327, 2867), new Angle(0.99f, 0.15f)).Mask(SpawnPlane.Mask_Highground)); // right bridge ledge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(88355, -50586, 2657), new Angle(-0.71f, 0.70f)).Mask(SpawnPlane.Mask_Highground)); // above door
            Rooms.Add(layout);


            //// Room 8 - 2 shielders, 3 pistols ////
            enemies = room_8.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies);
            // default platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(92037, -63331, 3273), new Vector3f(90078, -62702, 3273), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(93417, -62897, 3273), new Vector3f(95955, -62244, 3273), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(94131, -60199, 3283), new Vector3f(94884, -59406, 3283), new Angle(0.76f, 0.65f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(91839, -62226, 2823), new Vector3f(90810, -61054, 2823), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(91739, -58284, 2821), new Vector3f(90648, -59502, 2823), new Angle(0.41f, 0.91f)).Mask(SpawnPlane.Mask_Flatground).AsVerticalPlane());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(91834, -60829, 2823), new Vector3f(90283, -59683, 2823), new Angle(0.59f, 0.81f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(92348, -56910, 2823), new Vector3f(91958, -57718, 2823), new Angle(0.92f, 0.39f)).Mask(SpawnPlane.Mask_Flatground));

            // high/special places
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(96582, -62198, 4402), new Vector3f(96590, -63328, 4400), new Angle(1.00f, 0.02f))
                .AsVerticalPlane().Mask(SpawnPlane.Mask_Highground).setDiff(1)); // high platform in the back
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(93926, -61868, 4123), new Angle(0.90f, 0.43f)).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(93927, -60461, 4123), new Angle(0.96f, 0.27f)).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // billboard 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(90608, -61187, 3738), new Angle(0.63f, 0.78f)).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // billboard 3
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(89917, -61917, 4349), new Vector3f(89930, -60581, 4349), new Angle(-0.01f, 1.00f))
                .Mask(SpawnPlane.Mask_HighgroundLimited).AsVerticalPlane().setDiff(1)); // beam above exit door

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(92349, -58152, 4250), new Angle(0.91f, 0.42f))
                .Mask(SpawnPlane.Mask_HighgroundLimited).setRarity(0.15).setDiff(1)); // hovering platform above billboard hallway
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(89973, -63984, 4349), new Angle(0.49f, 0.87f))
                .Mask(SpawnPlane.Mask_HighgroundLimited).setRarity(0.1).setDiff(1)); // beam above gate, above void
            Rooms.Add(layout);


            //// Room 9 - 2 weebs, 2 drones ////
            enemies = room_9.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyDrone(enemies[0]);
            enemies[1] = new EnemyDrone(enemies[1]);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Weeb);
            RandomPickEnemiesWithoutCP(ref enemies, force: false, removeCP: false, enemyIndex: 3); // random chance to pick last weeb
            layout = new RoomLayout(enemies);

            // drone layouts - minimal rng
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(98383, -54810, 5045), new Vector3f(98361, -56192, 5045), new Angle(-1.00f, 0.02f)).Mask(SpawnPlane.Mask_Airborne).AsVerticalPlane());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(100759, -56737, 4778), new Vector3f(100727, -54992, 4778), new Angle(-1.00f, 0.00f)).Mask(SpawnPlane.Mask_Airborne).AsVerticalPlane());

            // weeb layouts
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(92270, -56332, 5063), new Vector3f(93074, -55571, 5068), new Angle(-1.00f, 0.04f)).Mask(SpawnPlane.Mask_Flatground)); // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(105248, -55787, 4693), new Angle(-1.00f, 0.01f)).Mask(SpawnPlane.Mask_Flatground)); // after door

            Rooms.Add(layout);

            //// Room 10 - 2 turrets ////
            enemies = room_10.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyTurret(enemies[0]);
            enemies[1] = new EnemyTurret(enemies[1]);

            layout = new RoomLayout(enemies);

            // default positions (1)
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.HorizontalSpeed = 40;
            turretSpawn.HorizontalAngle = 30;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(108038, -54837, 4793), new Angle(-0.70f, 0.71f)).SetSpawnInfo(turretSpawn));

            // default positions (2)
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.HorizontalSpeed = 40;
            turretSpawn.HorizontalAngle = 30;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(113704, -56614, 4793), new Angle(0.71f, 0.70f)).SetSpawnInfo(turretSpawn));


            // second right billboard edge
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = -60;
            turretSpawn.HorizontalSpeed = 30;
            turretSpawn.HorizontalAngle = 40;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(111558, -54836, 6347), new Angle(-0.71f, 0.70f)).SetSpawnInfo(turretSpawn).setDiff(1));

            // left billboard, on board side
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 10;
            turretSpawn.HorizontalSpeed = 40;
            turretSpawn.HorizontalAngle = 45;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(110102, -56563, 5932), GameUtils.CreateQuaternion(-150, 0, 90)).SetSpawnInfo(turretSpawn));

            // middle platform, 180 range
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.HorizontalSpeed = 40;
            turretSpawn.HorizontalAngle = 90;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(110797, -55271, 4694), new Angle(-0.71f, 0.71f)).SetSpawnInfo(turretSpawn));

            // second pillar, right side 
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 10;
            turretSpawn.HorizontalSpeed = 50;
            turretSpawn.HorizontalAngle = 45;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(113431, -56626, 4542), GameUtils.CreateQuaternion(90, 0, -90)).SetSpawnInfo(turretSpawn));

            // before door 
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.HorizontalAngle = 45;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(116165, -56171, 4688), new Angle(0.95f, 0.31f)).SetSpawnInfo(turretSpawn));

            // second billboard, cover pillar edge
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = -40;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(113302, -54795, 6228), new Angle(-0.90f, 0.35f)).SetSpawnInfo(turretSpawn).setDiff(1));


            layout.Mask(SpawnPlane.Mask_Turret);
            layout.DoNotReuse();
            Rooms.Add(layout);


            //// Room 11 - 2 orb, 2 weebs, 2 drones, 2 pistols, 3 shielders ////
            enemies = room_11.ReturnEnemiesInRoom(AllEnemies);
            // shield orb: near weebs, protecting 2 pistols
            EnemyShieldOrb shieldOrb = new EnemyShieldOrb(enemies[0]);
            shieldOrb.HideBeam_Range(0, 3);
            shieldOrb.HideBeam_Range(1, 3);
            shieldOrb.LinkObject(2);
            enemies[0] = shieldOrb;
            // shield orb: above door, protecting 3 shielders
            shieldOrb = new EnemyShieldOrb(enemies[1]);
            shieldOrb.HideBeam_Range(0, 3);
            shieldOrb.HideBeam_Range(1, 3);
            shieldOrb.HideBeam_Range(2, 3);
            shieldOrb.LinkObject(3);
            enemies[1] = shieldOrb;
            // 2 - 6 ► 2 pistols, 2 shielders
            enemies[7].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[8].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[9].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies[10].SetEnemyType(Enemy.EnemyTypes.Weeb);
            layout = new RoomLayout(enemies);

            // shield orbs layout
            layout = new RoomLayout(enemies[0], enemies[1]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(132332, -57280, 10500), new Vector3f(132372, -54033, 10500)).AsVerticalPlane()); // default top platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(131585, -54673, 9264), new Vector3f(131547, -57159, 9264)).AsVerticalPlane()); // along the billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(129175, -57776, 10932))); // small fuel tank, top platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(131983, -55845, 10802))); // on 2 crates, top platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(133814, -53896, 10778)).setDiff(1)); // above billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(134342, -55305, 10896)).setRarity(0.3).setDiff(1)); // hovering above second second (top pltfrom)

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(131175, -58516, 11376))); // top platform, left wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(129502, -56842, 9598))); // above left super jump
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(129912, -57174, 8005)).setRarity(0.3)); // under left super jump
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(130664, -55847, 8818))); // hovering near double pistols
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(131069, -54021, 9050), new Vector3f(126587, -54223, 9050)).AsVerticalPlane()); // along the right billboard/wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(127604, -56831, 9598)).setRarity(0.3).setDiff(1)); // above secret room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(130213, -53902, 10748)).setRarity(0.3).setDiff(1)); // hovering ad on right wall 

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(136279, -56748, 9198), new Vector3f(135363, -56755, 9198))); // above second default orb left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(136279, -54056, 9198), new Vector3f(135310, -54091, 9198))); // above second default orb right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(133159, -54438, 8643), new Vector3f(133120, -56506, 8643)).AsVerticalPlane());  // second section, net billboard/wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(134643, -55342, 8250))); // hovering midair, near 2'nd orb default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(130811, -54883, 9198))); // red crate, first section
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(128815, -55725, 10278)).setRarity(0.2).setDiff(1)); // hovering above 1st section - rare spawn
            Rooms.Add(layout);

            // Enemies - 2 weebs, 2 drones, 2 pistols, 3 shielders 
            layout = new RoomLayout(enemies.Skip(2).ToList());

            // default platforms - flatgrounds
            var mask = SpawnPlane.Mask_Flatground;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(132709, -54246, 10500), new Vector3f(132355, -56555, 10500), new Angle(1.00f, 0.01f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(131805, -57739, 10500), new Vector3f(129602, -58279, 10500), new Angle(0.70f, 0.72f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(130337, -57462, 8898), new Vector3f(131587, -56671, 8898), new Angle(1.00f, 0.01f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(130069, -56567, 8004), new Vector3f(127601, -55922, 8004), new Angle(0.89f, 0.45f)).SetMaxEnemies(2).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(130788, -55468, 8004), new Vector3f(130235, -56242, 8004), new Angle(1.00f, 0.03f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(127912, -54229, 7451), new Vector3f(127351, -56537, 7451), new Angle(1.00f, 0.04f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(126559, -56519, 7451), new Vector3f(126044, -54123, 7451), new Angle(-1.00f, 0.03f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(128900, -56624, 7451), new Angle(-1.00f, 0.03f)).setRarity(0.3).Mask(mask)); // secret, rare spawn
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(135237, -54896, 7451), new Vector3f(133817, -55799, 7451), new Angle(-1.00f, 0.00f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(136373, -54807, 8250), new Vector3f(135533, -56046, 8252), new Angle(1.00f, 0.01f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(130788, -54946, 7457), new Angle(1.00f, 0.00f)).Mask(mask)); // hallway

            // without weebs
            mask = SpawnPlane.Mask_Highground;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(134083, -54060, 7850), new Angle(-0.98f, 0.20f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(133753, -56701, 7850), new Angle(0.70f, 0.72f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(132999, -56806, 8148), new Angle(0.52f, 0.85f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(128214, -54536, 8250), new Angle(-0.95f, 0.31f)).Mask(mask));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(125474, -55889, 8353), new Angle(0.99f, 0.11f)).Mask(mask).setDiff(1)); // first section, left fuel tank
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(125435, -54872, 8350), new Angle(-1.00f, 0.06f)).Mask(mask).setDiff(1)); // first section, right fuel tank
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(126052, -57029, 7829), new Angle(0.95f, 0.32f)).Mask(mask)); // first section, left "stairs"
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(126292, -56810, 9598), new Angle(0.94f, 0.35f)).setRarity(0.2).Mask(mask).setDiff(1)); // above secret, patrols the spawn
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(134203, -53938, 10778), new Angle(-0.97f, 0.24f)).Mask(mask).setDiff(1)); // billboard/ad hovering above 2nd section
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(129189, -53879, 9510), new Angle(-0.93f, 0.37f)).Mask(mask).setDiff(1)); // first section, right billboard

            // high places/special (without weebs, wavers)
            mask = SpawnPlane.Mask_HighgroundLimited;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(132850, -56498, 9178), new Angle(0.31f, 0.95f)).Mask(mask).setDiff(1)); // 2nd section, net wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(124745, -53992, 9048), new Angle(-0.90f, 0.43f)).setRarity(0.1).Mask(mask).setDiff(1)); // right pipes near spawn, rare spawn
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(127260, -56400, 8697), new Angle(0.16f, 0.99f)).Mask(mask).setDiff(1)); // billboard near double pistols
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(136389, -56441, 9440), new Angle(0.98f, 0.18f))
                .Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); ; // antenna, above exit door

            Rooms.Add(layout);

            //// Room 12 - 4 turrets ////
            enemies = room_12.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyTurret(enemies[0]);
            enemies[1] = new EnemyTurret(enemies[1]);
            enemies[2] = new EnemyTurret(enemies[2]);
            enemies[3] = new EnemyTurret(enemies[3]);
            layout = new RoomLayout(enemies);


            //// small platform, patroling on hallwaay
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = -10;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(148084, -59537, 8418), new Angle(0.71f, 0.71f)).SetSpawnInfo(turretSpawn));


            // default pos, small platform, moving and patroling exit 
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = -30;
            turretSpawn.HorizontalSpeed = 30;
            turretSpawn.HorizontalAngle = 30;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(148296, -55771, 8413), new Angle(-0.72f, 0.70f)).SetSpawnInfo(turretSpawn));


            // separating wall, patroling both sides
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = -40;
            turretSpawn.HorizontalAngle = 60;
            turretSpawn.HorizontalSpeed = 50;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(146119, -59352, 8418), new Angle(0.49f, 0.87f)).SetSpawnInfo(turretSpawn));


            // left section, billboard
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.HorizontalAngle = 60;
            turretSpawn.HorizontalSpeed = 30;
            turretSpawn.VerticalAngle = 40;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(144196, -59093, 8692), GameUtils.CreateQuaternion(280, 120, -90)).SetSpawnInfo(turretSpawn));

            // red crate, near exit door
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = -10;
            turretSpawn.HorizontalAngle = 45;
            turretSpawn.HorizontalSpeed = 20;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(148913, -59210, 7178), new Angle(0.91f, 0.40f)).SetSpawnInfo(turretSpawn));

            // default lower platform, but spinning
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = -10;
            turretSpawn.HorizontalAngle = 40;
            turretSpawn.HorizontalSpeed = 40;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(146810, -58436, 6978), new Angle(0.33f, 0.94f)).SetSpawnInfo(turretSpawn));

            // on left wall, before entry gate/gap, patroling hallway
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 0;
            turretSpawn.HorizontalAngle = 30;
            turretSpawn.HorizontalSpeed = 30;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(148894, -59367, 7741), GameUtils.CreateQuaternion(180, 45, 90)).setRarity(0.2).SetSpawnInfo(turretSpawn));

            layout.Mask(SpawnPlane.Mask_Turret);
            layout.DoNotReuse();
            Rooms.Add(layout);

            /////// Extra spawns for enemies without cp for extra rng ////
            layout = new RoomLayout();
            // extra spots after last room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(156599, -50852, 11515), new Angle(1.00f, 0.02f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(163353, -50842, 11594), new Angle(1.00f, 0.01f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(169168, -50866, 11594), new Angle(1.00f, 0.01f)).Mask(SpawnPlane.Mask_Highground));

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(170139, -51612, 12293), new Angle(0.98f, 0.22f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(169533, -56390, 12425), new Angle(0.65f, 0.76f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // ramp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(167286, -61311, 11594), new Vector3f(168690, -61306, 11594), new Angle(0.31f, 0.95f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(160772, -61127, 11594), new Vector3f(161891, -61521, 11594), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(156827, -85213, 13948), new Angle(0.72f, 0.70f)).Mask(SpawnPlane.Mask_Highground).setRarity(0.1)); // inside end elevator
            Rooms.Add(layout);


            #region Jump
            //first jump, before hardest room
            NonPlaceableObject uplink = new UplinkJump(0x0, 0xCD8);//6,3,6
            uplink.AddSpawnInfo(new UplinkJumpSpawnInfo().SetRarity(0.5));//default
            var jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 7.0f, JumpForwardMultiplier = 6.0f };
            uplink.AddSpawnInfo(jumpSpawn);//long jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 3.0f, JumpForwardMultiplier = -1.0f, JumpGravity = -3.0f };
            uplink.AddSpawnInfo(jumpSpawn);//weird negative gravity jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpForwardMultiplier = -3.5f };
            uplink.AddSpawnInfo(jumpSpawn);//normal backward jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 3.25f, JumpForwardMultiplier = 6.0f, JumpGravity = 3.0f };
            uplink.AddSpawnInfo(jumpSpawn);//short jump
            worldObjects.Add(uplink);

            //second jump, hardest room bottom
            uplink = new UplinkJump(0x0, 0xCE8);// 6,3,6
            uplink.AddSpawnInfo(new UplinkJumpSpawnInfo().SetRarity(0.5));//default
            jumpSpawn = new UplinkJumpSpawnInfo { JumpForwardMultiplier = -3.5f };
            uplink.AddSpawnInfo(jumpSpawn);//normal backward jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpForwardMultiplier = 7.0f };
            uplink.AddSpawnInfo(jumpSpawn);//bounce jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 7.0f, JumpForwardMultiplier = 0.5f };
            uplink.AddSpawnInfo(jumpSpawn);//high jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 2.0f, JumpForwardMultiplier = 5.0f, JumpGravity = -1.0f * Config.GetInstance().r.Next(30, 61) / 10 };
            uplink.AddSpawnInfo(jumpSpawn);//negative gravity jump
            worldObjects.Add(uplink);

            //third jump,hardest room uptop
            uplink = new UplinkJump(0x0, 0xCE0);// 4.5,3,6
            uplink.AddSpawnInfo(new UplinkJumpSpawnInfo());//default
            jumpSpawn = new UplinkJumpSpawnInfo { JumpForwardMultiplier = -3.5f };
            uplink.AddSpawnInfo(jumpSpawn);//normal backward jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpForwardMultiplier = 8.0f };
            uplink.AddSpawnInfo(jumpSpawn);//bounce
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = -4.0f }.SetRarity(0.1);
            uplink.AddSpawnInfo(jumpSpawn);//cant jump

            jumpSpawn = new UplinkJumpSpawnInfo {
                TimeToActivate = Config.GetInstance().r.Next(3, 26) / 10,
                JumpMultiplier = Config.GetInstance().r.Next(10, 81) / 10,
                JumpForwardMultiplier = Config.GetInstance().r.Next(0, 45) / 10 * (Config.GetInstance().r.Next(2) == 0 ? 1 : (-1)),
                JumpGravity = Config.GetInstance().r.Next(30, 61) / 10
            };
            uplink.AddSpawnInfo(jumpSpawn);//random jump
            worldObjects.Add(uplink);

            //fourth jump, after 4 turrets
            uplink = new UplinkJump(0x10, 0x1008);// 6,3,6
            uplink.AddSpawnInfo(new UplinkJumpSpawnInfo().SetRarity(0.5));//default
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 3.0f, JumpForwardMultiplier = 10.0f, JumpGravity = -5.0f };
            uplink.AddSpawnInfo(jumpSpawn);//weird negative gravity jump, under second platform
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 0.05f, JumpForwardMultiplier = -2.5f, JumpGravity = -3.5f };
            uplink.AddSpawnInfo(jumpSpawn);//weird negative gravity jump,
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 5.0f, JumpForwardMultiplier = 0.0f };
            uplink.AddSpawnInfo(jumpSpawn);//vertical jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 10.0f, JumpForwardMultiplier = 10.0f, JumpGravity = 3.0f };
            uplink.AddSpawnInfo(jumpSpawn);//long jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpForwardMultiplier = -3.0f };
            uplink.AddSpawnInfo(jumpSpawn);//normal backward jump
            worldObjects.Add(uplink);

            //fifth jump, second jump after 4 turrets
            uplink = new UplinkJump(0x10, 0xFF8);// 3.8,3.3,6
            uplink.AddSpawnInfo(new UplinkJumpSpawnInfo().SetRarity(0.5));//default
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 100.0f, JumpForwardMultiplier = 100.0f, JumpGravity = 4.0f };
            uplink.AddSpawnInfo(jumpSpawn);//teleport
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 2.0f, JumpForwardMultiplier = -2.0f, JumpGravity = 0.0f };
            uplink.AddSpawnInfo(jumpSpawn);//slow backward jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 1.0f, JumpForwardMultiplier = 3.0f, JumpGravity = -2.0f };
            uplink.AddSpawnInfo(jumpSpawn);//weird negative gravity jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 1000.0f, JumpForwardMultiplier = -3.0f, JumpGravity = -1000.0f };
            uplink.AddSpawnInfo(jumpSpawn);//ceiling slide
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = -4.0f }.SetRarity(0.1);
            uplink.AddSpawnInfo(jumpSpawn);//cant jump
            worldObjects.Add(uplink);
            #endregion

            #region Shurikens
            //// Hardest room
            uplink = new UplinkShurikens(0x0, 0xCB0);//5,35
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 3.0f, MaxAttacks = 30 }); // short time
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = Config.GetInstance().r.Next(3, 12), MaxAttacks = Config.GetInstance().r.Next(4, 20) }); // basic rng
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 5, MaxAttacks = Config.GetInstance().r.Next(3, 6) }); // unlucky rng
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 500, MaxAttacks = 500 }.SetRarity(0.2)); // lucky rng
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo()); // default
            worldObjects.Add(uplink);
            #endregion
        }

        public void Gen_Easy() {
            Gen_Normal();
        }

        public void Gen_Hardcore() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game);
            Rooms = new List<RoomLayout>();
            RoomLayout layout;

            //// Room 1 ////
            var enemies = room_HC_1.ReturnEnemiesInRoom(AllEnemies);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[4].SetEnemyType(Enemy.EnemyTypes.Weeb);
            RandomPickEnemiesWithoutCP(ref enemies, force: true);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66537, -5898, 1588), new Vector3f(66978, -6634, 1592), new Angle(1.00f, 0.01f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66226, -7383, 1588), new Vector3f(65375, -7995, 1588), new Angle(0.71f, 0.70f))
                .Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64630, -8072, 1588), new Vector3f(63823, -7396, 1588), new Angle(0.71f, 0.71f))
                .Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61743, -7317, 1588), new Vector3f(61361, -8050, 1588), new Angle(0.02f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63022, -9198, 1588), new Vector3f(64666, -8893, 1588), new Angle(0.71f, 0.70f))
                .Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(64351, -9488, 2148), new Vector3f(63008, -9454, 2148), new Angle(0.72f, 0.70f)).Mask(SpawnPlane.Mask_Highground));
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63455, -7665, 1981), new Angle(0.65f, 0.76f)).Mask(SpawnPlane.Mask_Highground)); // left box
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65001, -7696, 1982), new Angle(0.72f, 0.69f)).Mask(SpawnPlane.Mask_Highground)); // right box
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66388, -7146, 1982), new Angle(0.94f, 0.33f)).Mask(SpawnPlane.Mask_Highground)); // far right box
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63462, -8975, 2219), new Vector3f(64792, -8453, 2364), new Angle(0.72f, 0.70f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65858, -7279, 2364), new Angle(0.91f, 0.42f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66753, -7472, 1588), new Angle(-0.99f, 0.14f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 20, HorizontalSpeed = 30, VerticalAngle = 10 })); // behind right box

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61913, -7324, 1588), new Angle(-0.31f, 0.95f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, HorizontalSpeed = 0, VerticalAngle = 10 })); // left pathway, aim to exit

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62729, -8635, 2275), new QuaternionAngle(-0.68f, 0.18f, 0.18f, 0.68f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, HorizontalSpeed = 0, VerticalAngle = 5 })); // left beam, wall gang
            Rooms.Add(layout);


            //// Room 2 ////
            enemies = room_HC_2.ReturnEnemiesInRoom(AllEnemies);
            RandomPickEnemiesWithoutCP(ref enemies, force: true);
            // move cp to previous platform
            ModifyCP(PtrDB.DP_ForbiddenZone_HC_Room2_CP, new Vector3f(66563, -9899, 1602), new Angle(-0.71f, 0.71f), GameHook.game);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66342, -17975, 1390), new Angle(0.66f, 0.75f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66820, -21660, 1368), new Vector3f(66265, -21217, 1368), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66145, -15436, 1368), new Angle(0.57f, 0.82f)).Mask(SpawnPlane.Mask_Highground));
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66276, -18111, 1931), new Angle(0.68f, 0.73f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66608, -20915, 1391), new Angle(0.70f, 0.72f)).Mask(SpawnPlane.Mask_Turret)
                 .SetSpawnInfo(new TurretSpawnInfo { VerticalAngle = 0, HorizontalAngle = 40, HorizontalSpeed = 45})); // infront of exit door
            Rooms.Add(layout);


            //// Room 3 ////
            enemies = room_HC_3.ReturnEnemiesInRoom(AllEnemies);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Weeb);
            layout = new RoomLayout(enemies.Skip(2).ToList()); // enemies
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(60901, -20242, 1374), new Vector3f(62558, -21031, 1374), new Angle(-0.13f, 0.99f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62708, -21895, 1374), new Angle(-0.22f, 0.98f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(60904, -21437, 1374), new Angle(0.26f, 0.97f)).Mask(SpawnPlane.Mask_Flatground));
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62178, -21461, 2548), new Angle(-0.32f, 0.95f)).Mask(SpawnPlane.Mask_Highground)); // billboard
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61916, -29844, 2091), new Angle(0.71f, 0.71f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61374, -26283, 1997), new Angle(0.71f, 0.71f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61483, -33046, 2097), new Angle(0.66f, 0.75f)).Mask(SpawnPlane.Mask_Airborne));
            layout.DoNotReuse();
            Rooms.Add(layout);


            //// Room 4 ////
            enemies = room_HC_4.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyDrone(enemies[0]);
            RandomPickEnemiesWithoutCP(ref enemies, enemyIndex: 1);
            RandomPickEnemiesWithoutCP(ref enemies, enemyIndex: 0);
            layout = new RoomLayout(enemies);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(66951, -36193, 1680), new Vector3f(65906, -35683, 1670), new Angle(-0.73f, 0.69f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65856, -38034, 2109), new Angle(0.99f, 0.16f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(62519, -37973, 1950), new Angle(0.72f, 0.70f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);


            //// Room 5 ////
            enemies = room_HC_5.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyTurret(enemies[0]);
            enemies[1] = new EnemyShieldOrb(enemies[1]);
            enemies[5].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies[6].SetEnemyType(Enemy.EnemyTypes.Weeb);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, enemyIndex: 5); // weeb
            DevWindow.worldObject = enemies[0];
            RandomPickEnemiesWithoutCP(ref enemies, force: true, enemyIndex: 0); // that's the only turret in the level...
            layout = new RoomLayout(enemies[0]); // orb
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71754, -44732, 1458))); // ad, left corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(72213, -46586, 1841))); // angled billboard, left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71807, -45703, 1655))); // middle left wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(73187, -46477, 1770))); // behind laser wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(75650, -46176, 2035))); // right billboards
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(73787, -46243, 1796))); // behind laser wall, middle section
            Rooms.Add(layout);
            layout = new RoomLayout(enemies.Skip(1).ToList());
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71678, -46401, 1048), new Vector3f(72164, -45007, 1047), new Angle(0.02f, 1.00f))
                .Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(73592, -45391, 1059), new Angle(0.56f, 0.83f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74567, -45424, 1048), new Angle(0.89f, 0.47f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74951, -41653, 1498), new Vector3f(75550, -42069, 1497), new Angle(-0.68f, 0.73f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74982, -42458, 1498), new Angle(-0.44f, 0.90f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(73218, -46802, 1472), new Vector3f(74182, -46267, 1463), new Angle(0.02f, 1.00f)).Mask(SpawnPlane.Mask_Flatground)
                .SetMaxEnemies(3));
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74433, -45095, 2247), new Angle(1.00f, 0.06f)).Mask(SpawnPlane.Mask_Highground)); // upper beam
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(75741, -41648, 1856), new Angle(-0.81f, 0.59f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // ad sign
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(75498, -42242, 2068), new Angle(-0.72f, 0.69f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(75042, -43278, 1570), new Angle(0.63f, 0.78f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, VerticalAngle = 5, HorizontalSpeed = 0 })); // near exit

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74683, -45639, 2248), new Angle(0.50f, 0.87f)).Mask(SpawnPlane.Mask_Turret)
            .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, VerticalAngle = -15, HorizontalSpeed = 0 })); // beam
            Rooms.Add(layout);


            //// Room 6 ////
            enemies = room_HC_6.ReturnEnemiesInRoom(AllEnemies);
            // shift points, 3 minimum
            List<Tuple<Vector3f, Angle>> ShiftPoints_HC_Room6 = new List<Tuple<Vector3f, Angle>>() {
                new Tuple<Vector3f, Angle>(new Vector3f(89017, -49548, 2911), new Angle(0.98f, 0.22f)), // city ad
                new Tuple<Vector3f, Angle>(new Vector3f(87262, -49358, 3443), new Angle(0.60f, 0.80f)), // pipe
                new Tuple<Vector3f, Angle>(new Vector3f(85569, -48364, 3675), new Angle(-0.21f, 0.98f)), // wall fan, high
                new Tuple<Vector3f, Angle>(new Vector3f(86054, -45992, 3262), new Angle(-0.61f, 0.79f)), // default platform
                new Tuple<Vector3f, Angle>(new Vector3f(90233, -46080, 1673), new Angle(-0.88f, 0.48f)), // uplink platform
                new Tuple<Vector3f, Angle>(new Vector3f(87533, -49283, 1618), new Angle(0.52f, 0.85f)), // near exit
                new Tuple<Vector3f, Angle>(new Vector3f(90773, -49134, 2746), new Angle(1.00f, 0.06f)), // billboard to collectible
                new Tuple<Vector3f, Angle>(new Vector3f(85720, -49435, 2557), new Angle(0.22f, 0.98f)), // default platform
            };
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[1] = new EnemyShifter(enemies[1]).AddFixedSpawnInfoList(ref ShiftPoints_HC_Room6);
            layout = new RoomLayout(enemies[0]); // orb
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(88232, -45901, 3206)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(87750, -47837, 3240)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(88997, -48030, 3003))); // floating under grapple
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(90579, -46871, 2470), new Vector3f(90507, -48710, 1767)).AsVerticalPlane());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(90155, -49307, 3297))); // collectible platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(87598, -49246, 1955))); // near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(88216, -47947, 2898))); // floating below grapple
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(89623, -47727, 1561))); // floating between platform
            Rooms.Add(layout);

            layout = new RoomLayout(enemies.Skip(1).ToList()); // enemies
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(90005, -49564, 1618), new Vector3f(88109, -49017, 1618), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground)
                .SetMaxEnemies(2).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(90376, -46233, 1637), new Vector3f(89915, -46786, 1618), new Angle(-0.89f, 0.46f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(87438, -49006, 2563), new Vector3f(88933, -49385, 2593), new Angle(-1.00f, 0.00f)).Mask(SpawnPlane.Mask_Highground)
                .SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(87669, -46074, 3123), new Vector3f(88910, -46689, 3128), new Angle(-0.86f, 0.51f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(85665, -46881, 3135), new Vector3f(86880, -46250, 3128), new Angle(-0.72f, 0.69f)).Mask(SpawnPlane.Mask_Flatground));
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(85582, -47482, 2965), new Angle(-0.51f, 0.86f)).Mask(SpawnPlane.Mask_Highground)); // wallfan
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(88463, -49162, 3095), new Angle(-1.00f, 0.01f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(88353, -47111, 2143), new Angle(-0.69f, 0.72f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(88337, -45390, 3605), new Angle(-0.71f, 0.70f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(89280, -46279, 1618), new Angle(-0.78f, 0.63f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 20, VerticalAngle = 30, HorizontalSpeed = 30})); // near uplink, aim towards door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(89056, -46467, 3128), new Angle(-0.81f, 0.58f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, VerticalAngle = -5, HorizontalSpeed = 0 })); // 3rd high platform, aim towards 2nd platform
            Rooms.Add(layout);


            //// Room 7 ////
            enemies = room_HC_7.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[1] = new EnemyShieldOrb(enemies[1]);
            enemies[2] = new EnemyShieldOrb(enemies[2]);
            enemies[3] = new EnemyShieldOrb(enemies[3]);
            enemies[7].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[8].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[9].SetEnemyType(Enemy.EnemyTypes.Waver);
            layout = new RoomLayout(enemies);
            // orbs
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(95278, -64498, 3566), new Vector3f(92511, -63514, 3566))
                .Mask(SpawnPlane.Mask_ShieldOrb).AsVerticalPlane());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(90243, -62379, 3713), new Vector3f(90274, -60384, 3713))
                .Mask(SpawnPlane.Mask_ShieldOrb).AsVerticalPlane());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(92327, -60277, 2935), new Vector3f(93449, -59287, 3659))
                .Mask(SpawnPlane.Mask_ShieldOrb).AsVerticalPlane());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(94559, -60590, 3413), new Vector3f(95789, -62066, 3829))
                .Mask(SpawnPlane.Mask_ShieldOrb).AsVerticalPlane());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(93630, -61930, 3408), new Vector3f(93593, -60417, 3921))
                .Mask(SpawnPlane.Mask_ShieldOrb).AsVerticalPlane());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(90522, -63733, 3447), new Angle(0.61f, 0.79f)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(96176, -63079, 3780), new Angle(0.02f, 1.00f)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(96464, -62098, 4435)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(91555, -59803, 3295)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(89649, -62914, 3573)).Mask(SpawnPlane.Mask_ShieldOrb));
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(90119, -63383, 3273), new Vector3f(92017, -62531, 3273), new Angle(0.73f, 0.68f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(90882, -62122, 2823), new Vector3f(91953, -60711, 2833), new Angle(0.70f, 0.72f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(94736, -59520, 3283), new Vector3f(94161, -60089, 3283), new Angle(0.82f, 0.57f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(93291, -62986, 3274), new Vector3f(96196, -62180, 3274), new Angle(0.72f, 0.70f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2));
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(96539, -62411, 4398), new Vector3f(96534, -63626, 4398), new Angle(-1.00f, 0.00f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(90618, -61133, 3738), new Angle(0.70f, 0.72f)).Mask(SpawnPlane.Mask_Highground)); // billboard top 1
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(90625, -62357, 3737), new Angle(0.52f, 0.85f)).Mask(SpawnPlane.Mask_Highground)); // billboard top 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(89910, -61369, 4349), new Angle(-0.03f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // beam above door
            layout.DoNotReuse();
            Rooms.Add(layout);


            //// Room 8 ////
            enemies = room_HC_8.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[1] = new EnemyDrone(enemies[1]);
            enemies[2] = new EnemyDrone(enemies[2]);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, enemyIndex: 2);
            RandomPickEnemiesWithoutCP(ref enemies, force: false, enemyIndex: 1);
            // orb
            layout = new RoomLayout(enemies[0]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(108266, -57204, 4912))); // behind laser
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(112844, -54322, 4974))); // near second laser
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(113810, -55187, 4929))); // small billboard right of default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(113721, -54566, 5030))); // second laser
            Rooms.Add(layout);

            layout = new RoomLayout(enemies.Skip(1).ToList());
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(111633, -55839, 4690), new Vector3f(110150, -55237, 4689), new Angle(1.00f, 0.01f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(116136, -55173, 4688), new Vector3f(116559, -56011, 4688), new Angle(-1.00f, 0.06f)).Mask(SpawnPlane.Mask_Highground));
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(113788, -54879, 5418), new Angle(-1.00f, 0.10f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // top of small billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(116437, -54938, 4988), new Angle(-0.96f, 0.29f)).Mask(SpawnPlane.Mask_Highground)); // box near door
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(114958, -56711, 5005), new Angle(0.95f, 0.31f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(112892, -55617, 5349), new Angle(1.00f, 0.01f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(116179, -55378, 5370), new Angle(1.00f, 0.09f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(113684, -56596, 4793), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 40, HorizontalSpeed = 30, VerticalAngle = 0, Range = 2500}));
            Rooms.Add(layout);


            //// Room 9 ////
            enemies = room_HC_9.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[1] = new EnemyShieldOrb(enemies[1]);
            enemies[2] = new EnemyShieldOrb(enemies[2]);
            enemies[7].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[8].SetEnemyType(Enemy.EnemyTypes.Waver);
            layout = new RoomLayout(enemies.Take(3).ToList()); // orb
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(132167, -54531, 10925), new Vector3f(132167, -54531, 11847))); // right default + range
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(132115, -55423, 11847), new Vector3f(132115, -55423, 10842))); // middle default + range
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(132084, -56185, 10842), new Vector3f(132084, -56185, 11794))); // left default + range
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(132870, -53956, 10803))); // box at top platform - right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(132280, -58440, 10910))); // box at top platform - left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(132916, -56734, 10751))); // top platform, near fence
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(132936, -55831, 10618))); // top platform, fence
            Rooms.Add(layout);

            layout = new RoomLayout(enemies.Skip(3).ToList()); // enemies
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(127546, -56556, 8004), new Vector3f(129352, -55912, 8004), new Angle(0.86f, 0.51f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(128253, -53985, 8250), new Vector3f(128826, -54474, 8250), new Angle(-0.97f, 0.23f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(131394, -57405, 8898), new Vector3f(130210, -56640, 8898), new Angle(0.89f, 0.45f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(131631, -55215, 8900), new Vector3f(131611, -56429, 8900), new Angle(1.00f, 0.05f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(135449, -54742, 8257), new Vector3f(136206, -56147, 8250), new Angle(1.00f, 0.04f)).Mask(SpawnPlane.Mask_Highground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(133988, -56402, 7850), new Vector3f(133240, -56771, 7850), new Angle(0.70f, 0.72f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(133054, -54443, 7850), new Vector3f(134206, -54287, 7850), new Angle(-0.75f, 0.67f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(134265, -56006, 7451), new Vector3f(133710, -55006, 7451), new Angle(-1.00f, 0.02f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(126655, -54135, 7451), new Vector3f(126125, -56323, 7451), new Angle(-1.00f, 0.03f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(129192, -55548, 7451), new Vector3f(128618, -54912, 7451), new Angle(1.00f, 0.02f)).Mask(SpawnPlane.Mask_Flatground));
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(130819, -54848, 9198), new Angle(-0.99f, 0.10f)).Mask(SpawnPlane.Mask_Highground)); // red crate, start
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(129562, -56782, 9598), new Angle(0.91f, 0.41f)).Mask(SpawnPlane.Mask_Highground)); // seperating wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(127280, -56390, 8697), new Angle(0.21f, 0.98f)).Mask(SpawnPlane.Mask_Highground)); // orange billboard, start left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(129200, -53881, 9510), new Angle(-0.93f, 0.36f)).Mask(SpawnPlane.Mask_Highground)); // orange billboard, start right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(131021, -56036, 8298), new Angle(1.00f, 0.06f)).Mask(SpawnPlane.Mask_Highground)); // white box, middle
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(132995, -56745, 8148), new Angle(0.44f, 0.90f)).Mask(SpawnPlane.Mask_Highground)); // white box, end left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(134290, -54658, 7748), new Angle(-0.89f, 0.45f)).Mask(SpawnPlane.Mask_Highground)); // white box, end right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(135237, -56556, 9198), new Angle(0.95f, 0.31f)).Mask(SpawnPlane.Mask_Highground)); // end, top left platform ledge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(131913, -55821, 10802), new Angle(-1.00f, 0.01f)).Mask(SpawnPlane.Mask_Highground)); // top platform, middle boxes
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(135652, -55339, 9107), new Angle(1.00f, 0.01f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(130240, -55964, 8774), new Angle(0.99f, 0.14f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(133065, -54028, 8740), new Angle(-0.34f, 0.94f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(129519, -58228, 10500), new Angle(0.34f, 0.94f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, HorizontalSpeed = 0, VerticalAngle = 10 })); // ninja, top left corner 

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(133201, -55463, 7451), new Angle(0.02f, 1.00f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 40, HorizontalSpeed = 30, VerticalAngle = 13 })); // end, near laser, aim towards door

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(129708, -56909, 9061), new QuaternionAngle(0.50f, 0.50f, 0.50f, 0.50f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 12, HorizontalSpeed = 10, VerticalAngle = 0 })); // middle, wall gang, on beam, towards middle

            Rooms.Add(layout);


            //// Room 10 ////
            enemies = room_HC_10.ReturnEnemiesInRoom(AllEnemies);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Splitter);
            enemies[6].SetEnemyType(Enemy.EnemyTypes.Weeb);
            RandomPickEnemiesWithoutCP(ref enemies, force:true);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(148731, -59625, 8418), new Vector3f(146949, -59957, 8417), new Angle(0.66f, 0.75f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(146328, -59570, 8417), new Vector3f(145219, -60047, 8418), new Angle(-0.01f, 1.00f))
                .Mask(SpawnPlane.Mask_Highground).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(145435, -56208, 7660), new Angle(-0.72f, 0.70f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(145553, -55017, 7654), new Angle(-0.73f, 0.69f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(148272, -55529, 6582), new Angle(1.00f, 0.06f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(147325, -59360, 6578), new Vector3f(148628, -57641, 6578), new Angle(0.70f, 0.71f))
                .Mask(SpawnPlane.Mask_Highground).SetMaxEnemies(2).AllowSplitter());
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(149016, -59299, 9359), new Angle(0.79f, 0.61f)).Mask(SpawnPlane.Mask_Highground)); // billboard right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(146837, -59282, 8868), new Angle(0.52f, 0.86f)).Mask(SpawnPlane.Mask_Highground)); // billboard middle
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(144037, -59183, 9128), new Vector3f(144025, -57406, 9127), new Angle(-0.10f, 0.99f))
                .Mask(SpawnPlane.Mask_Highground).AsVerticalPlane()); // billboard left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(145252, -56915, 8828), new Angle(-0.72f, 0.70f)).Mask(SpawnPlane.Mask_Highground)); // edge above first door frame
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(148341, -54848, 7248), new Angle(-0.99f, 0.12f)).Mask(SpawnPlane.Mask_Highground)); // extruded part below first cp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(146979, -58448, 6978), new Angle(0.23f, 0.97f)).Mask(SpawnPlane.Mask_Highground)); // right raised platform 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(148920, -58741, 6878), new Angle(0.83f, 0.56f)).Mask(SpawnPlane.Mask_Highground)); // last part, red box right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(148910, -57483, 6878), new Angle(0.93f, 0.36f)).Mask(SpawnPlane.Mask_Highground)); // last part, white box right
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(144650, -57482, 8830), new Angle(-0.62f, 0.78f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(147171, -58858, 7493), new Angle(0.50f, 0.87f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(146899, -57459, 7378), new Angle(-0.39f, 0.92f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 40, HorizontalSpeed = 40, VerticalAngle = -20}));
            Rooms.Add(layout);


            //// Room 11 ////
            enemies = room_HC_11.ReturnEnemiesInRoom(AllEnemies);
            enemies[0].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[1].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies.ForEach(x => x.DisableAttachedCP(GameHook.game));
            EnemiesWithoutCP.AddRange(enemies);


            #region Uplinks
            // the room with a lot of shield orbs, default total-time is above 20 seconds.. too easy
            NonPlaceableObject uplink = new UplinkSlowmo(0x40, 0x190, 0xA0);
            uplink.AddSpawnInfo(new UplinkSlowmoSpawnInfo { TotalTime = Config.GetInstance().r.Next(5, 10) });
            worldObjects.Add(uplink);
            // turret platforms, before hellroom
            uplink = new UplinkShurikens(0x48, 0xD8);
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = Config.GetInstance().r.Next(1, 3), MaxAttacks = Config.GetInstance().r.Next(1, 3)});
            worldObjects.Add(uplink);
            #endregion
        }

        public void Gen_Nightmare() {
            throw new System.NotImplementedException();
        }


        protected override void Gen_PerRoom() {
            
        }
    }
}