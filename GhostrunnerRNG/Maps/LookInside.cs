using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.GameObjects;
using GhostrunnerRNG.MapGen;
using System.Collections.Generic;
using System.Linq;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.Maps {
    class LookInside : MapCore, IModes{

        #region Rooms
        private Room room1 = new Room(new Vector3f(159175, -52611, 1562), new Vector3f(152993, -66967, 5886)); // 4 pistols, floating platforms 
        private Room room2 = new Room(new Vector3f(165231, -42333, 8077), new Vector3f(148296, -48473, 2321)); // 5 pistols ,last room
        #endregion

        #region HC Rooms
        private Room hc_room1 = new Room(new Vector3f(143382, -71435, -88), new Vector3f(158600, -51257, 6221)); // 2 drones, 3 turrets, 2 shielders, froggers
        private Room hc_room2 = new Room(new Vector3f(149989, -49756, 2730), new Vector3f(155288, -47062, 4413)); // 2 shifters   // default shifters
        private Room hc_room3 = new Room(new Vector3f(152013, -46561, 2610), new Vector3f(160768, -40716, 5004)); // orb, shielder, frogger

        private Room Awakening_hc_room1 = new Room(new Vector3f(-17620, -62069, 4280), new Vector3f(-6683, -68959, 1248)); //pistol, 2 uzi,  weeb, frogger - no cp
        private Room Awakening_hc_room2 = new Room(new Vector3f(-21402, -65468, 2247), new Vector3f(-32819, -58079, 5706)); // frogger,  pistol,  waver - no cp
        private Room Awakening_hc_room4 = new Room(new Vector3f(-3039, -53776, 5166), new Vector3f(7115, -62748, 788)); // drone, uzi, pistol - no cp
        private Room Awakening_hc_room5 = new Room(new Vector3f(4844, -62911, 3386), new Vector3f(7626, -72804, 1401)); // pistol, waver, shielder, frogger
        #endregion

        public LookInside() : base(MapType.LookInside) {
            CustomCheckPoints.Add(new CustomCP(mapType, new Vector3f(145063, -64312, 4484), new Vector3f(148462, -66128, 2459),
               new Vector3f(147344, -64958, 3353), new Angle(0.22f, 0.97f))); // SR route, wall extruded corner
        }

        public void Gen_Normal() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game, 0, 9); // static range, no gap
            Rooms = new List<RoomLayout>();
            RoomLayout layout;

            //// Room 1 - 4 pistols, floating platforms
            layout = new RoomLayout(room1.ReturnEnemiesInRoom(AllEnemies));
            // to avoid wrong pos spawns, I use vertical planes in the middle of the  platforms
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153148, -59471, 2832), new Vector3f(154124, -58691, 2832), new Angle(-0.75f, 0.66f)).AsVerticalPlane());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(155664, -57651, 2833), new Vector3f(156740, -56869, 2833), new Angle(-0.53f, 0.85f)).AsVerticalPlane());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(158191, -59078, 2832), new Vector3f(157983, -60312, 2832), new Angle(-0.87f, 0.50f)).AsVerticalPlane());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(155531, -61958, 2832), new Vector3f(154254, -61625, 2832), new Angle(0.39f, 0.92f)).AsVerticalPlane());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(152882, -55794, 3598), new Vector3f(155365, -54986, 3598), new Angle(-0.68f, 0.74f))); // collectible platform
            // special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153746, -62014, 4234), new Angle(0.76f, 0.65f)).setDiff(1)); // billboard near
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(158799, -58624, 4248), new Angle(0.96f, 0.28f)).setDiff(1)); // billboard far
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153237, -56789, 4301), new Angle(-0.53f, 0.85f)).setDiff(1)); // collectible wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153961, -52953, 3803), new Angle(-0.70f, 0.71f))); // slide ledge, to next room
            Rooms.Add(layout);

            //// Room 2 - 5 pistols, last room
            layout = new RoomLayout(room2.ReturnEnemiesInRoom(AllEnemies));
            // main platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(158225, -43241, 3198), new Vector3f(156081, -42827, 3198), new Angle(-0.89f, 0.46f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(156846, -40737, 3198), new Vector3f(157130, -41736, 3198), new Angle(-0.94f, 0.35f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(158792, -41972, 3198), new Vector3f(158922, -43343, 3198), new Angle(-0.96f, 0.27f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(152012, -48136, 3198), new Vector3f(152209, -47198, 3198), new Angle(-0.59f, 0.81f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153685, -47214, 3198), new Vector3f(155030, -47501, 3198), new Angle(-0.89f, 0.46f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(156721, -46427, 3198), new Vector3f(156351, -45285, 3198), new Angle(-0.89f, 0.46f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(154520, -43688, 3198), new Vector3f(154647, -45111, 3198), new Angle(-0.87f, 0.49f)));
            // special/high spots
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(151930, -47267, 3948), new Angle(-0.52f, 0.85f)).setDiff(1)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153667, -50388, 3298), new Angle(-0.64f, 0.77f))); // slide edge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(155839, -42364, 4052), new Angle(-0.53f, 0.85f))); // generator/server
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(158129, -44004, 4052), new Angle(-1.00f, 0.01f))); // generator/server 2 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(155591, -45151, 3618), new Angle(0.47f, 0.89f)));  // middle wall platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153328, -47607, 3638), new Angle(-0.88f, 0.47f))); // wide generator 1
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153988, -48037, 3640), new Angle(-0.96f, 0.27f))); // wide generator 2
            Rooms.Add(layout);
        }

        public void Gen_Easy() {Gen_Normal();}

        public void Gen_Hardcore() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game, 0, 39);
            Rooms = new List<RoomLayout>();
            RoomLayout layout;
            List<Enemy> enemies;

            // Room 1
            enemies = hc_room1.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyTurret(enemies[0]);
            enemies[1] = new EnemyTurret(enemies[1]);
            enemies[2] = new EnemyTurret(enemies[2]);
            enemies[3] = new EnemyDrone(enemies[3]);
            enemies[4] = new EnemyDrone(enemies[4]);

            // turrets
            layout = new RoomLayout(enemies.Take(3).ToList());

            // slide ledge
            TurretSpawnInfo turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 10;
            turretSpawn.HorizontalSpeed = 50;
            turretSpawn.HorizontalAngle = 60;
            turretSpawn.SetRange(3250);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153916, -52963, 3803), new Angle(-0.68f, 0.73f)).SetSpawnInfo(turretSpawn));

            // platform near uplink
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 10;
            turretSpawn.HorizontalSpeed = 50;
            turretSpawn.HorizontalAngle = 90;
            turretSpawn.SetRange(TurretSpawnInfo.DefaultRange);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153093, -59742, 2832), new Angle(-0.35f, 0.94f)).SetSpawnInfo(turretSpawn));

            // left billboard, aiming toward last raised platform
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = -30;
            turretSpawn.HorizontalSpeed = 0;
            turretSpawn.HorizontalAngle = 0;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(158809, -58789, 4248), new Angle(0.94f, 0.34f)).SetSpawnInfo(turretSpawn));

            // last room, middle ledge platform
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = -10;
            turretSpawn.HorizontalSpeed = 50;
            turretSpawn.HorizontalAngle = 45;
            turretSpawn.SetRange(TurretSpawnInfo.DefaultRange);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(155608, -45100, 3618), new Angle(0.46f, 0.89f)).SetSpawnInfo(turretSpawn));

            // last room, around left serverrack
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 10;
            turretSpawn.HorizontalSpeed = 0;
            turretSpawn.HorizontalAngle = 0;
            turretSpawn.SetRange(3100);
            turretSpawn.SetRange(TurretSpawnInfo.DefaultRange);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(159454, -42630, 3198), new Angle(-0.97f, 0.24f)).SetSpawnInfo(turretSpawn));

            // last room, middle platform, aiming towards main entry
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 20;
            turretSpawn.HorizontalSpeed = 0;
            turretSpawn.HorizontalAngle = 0;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(154429, -48413, 3198), new Angle(-0.99f, 0.13f)).SetSpawnInfo(turretSpawn));

            // right platform billboard, aiming to uplink
            turretSpawn = new TurretSpawnInfo();
            turretSpawn.VerticalAngle = 45;
            turretSpawn.SetRange(TurretSpawnInfo.DefaultRange);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(155018, -62253, 3356), new QuaternionAngle(0.09f, 0.70f, 0.70f, 0.09f)).SetSpawnInfo(turretSpawn));

            layout.DoNotReuse();
            Rooms.Add(layout);


            // Floating platforms
            layout = new RoomLayout(RemoveParentObjects(enemies.Skip(3).ToList()));
            // ground spawns
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(155878, -57543, 2833), new Vector3f(156530, -57130, 2833), new Angle(-0.72f, 0.70f)).AsVerticalPlane().Mask(SpawnPlane.Mask_Flatground)); // platform 1
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(158035, -59066, 2832), new Vector3f(158265, -60201, 2832), new Angle(-0.77f, 0.64f)).AsVerticalPlane().Mask(SpawnPlane.Mask_Flatground)); // platform 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(154542, -61364, 2832), new Vector3f(155412, -62000, 2832), new Angle(-0.18f, 0.98f)).AsVerticalPlane().Mask(SpawnPlane.Mask_Flatground)); // platform 3
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(154006, -58364, 2832), new Vector3f(153574, -59231, 2832), new Angle(-0.44f, 0.90f)).AsVerticalPlane().Mask(SpawnPlane.Mask_Flatground)); // platform 4
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(154862, -62325, 4234), new Angle(0.22f, 0.97f)).Mask(SpawnPlane.Mask_Highground)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(158603, -59749, 4248), new Angle(-0.98f, 0.18f)).Mask(SpawnPlane.Mask_Highground)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(155258, -56568, 4301), new Angle(-0.63f, 0.77f)).Mask(SpawnPlane.Mask_Highground)); // raised platform wall, left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153344, -56715, 4301), new Angle(-0.58f, 0.82f)).Mask(SpawnPlane.Mask_Highground)); // raised platform wall, right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153884, -51710, 4517), new Angle(-0.71f, 0.71f)).Mask(SpawnPlane.Mask_Highground));// above pipes, slide

            // drones
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153066, -60119, 3365), new Vector3f(157084, -59017, 3365), new Angle(-0.57f, 0.82f)).Mask(SpawnPlane.Mask_Airborne)); // middle floating platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(157441, -56301, 3838), new Angle(-0.96f, 0.27f)).Mask(SpawnPlane.Mask_Airborne)); // ledge climb
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153447, -53383, 4042), new Vector3f(154392, -53333, 4042), new Angle(-0.72f, 0.70f)).Mask(SpawnPlane.Mask_Airborne)); // near slide frame
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(152182, -58956, 3312), new Vector3f(154446, -57679, 3312), new Angle(-0.52f, 0.85f)).Mask(SpawnPlane.Mask_Airborne)); // back, floating platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153277, -46094, 3719), new Vector3f(155130, -47623, 3719), new Angle(-0.88f, 0.47f)).Mask(SpawnPlane.Mask_Airborne)); // last room, first section
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(156323, -42670, 4252), new Vector3f(157676, -43712, 4252), new Angle(-0.90f, 0.44f)).Mask(SpawnPlane.Mask_Airborne)); // last room, second section
            Rooms.Add(layout);

            // Last Room 
            enemies = hc_room3.ReturnEnemiesInRoom(AllEnemies); // orb + 2 enemies
            enemies.AddRange(hc_room2.ReturnEnemiesInRoom(AllEnemies)); // shifters
            layout = new RoomLayout(new EnemyShieldOrb(enemies[0])); // orb
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(158199, -43323, 3363), new Vector3f(156247, -42835, 3363))); // default orb
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(158340, -44165, 4139))); // left serverrack
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(156293, -44027, 3288))); // floating in middle, last section
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(157757, -42117, 3517))); // near door
            layout.DoNotReuse();
            Rooms.Add(layout);

            layout = new RoomLayout(enemies.Skip(1).ToList());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(154503, -43834, 3198), new Vector3f(154684, -45125, 3198), new Angle(-0.89f, 0.45f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153952, -46586, 3198), new Vector3f(154822, -47930, 3198), new Angle(-0.87f, 0.49f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(152584, -47444, 3198), new Vector3f(151648, -47889, 3198), new Angle(-0.38f, 0.92f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(156773, -46517, 3198), new Vector3f(156409, -45096, 3198), new Angle(-0.88f, 0.48f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(157709, -43922, 3198), new Angle(-0.88f, 0.47f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(155972, -42788, 3198), new Angle(-0.86f, 0.52f)).Mask(SpawnPlane.Mask_Flatground));
            Rooms.Add(layout);



            // EXTRA //
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(158458, -42995, 3198), new Angle(-0.98f, 0.19f)).Mask(SpawnPlane.Mask_Flatground)); // last room spawns
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(156573, -41959, 3198), new Angle(-0.72f, 0.70f)).Mask(SpawnPlane.Mask_Flatground)); // last room spawns
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(132165, -69982, 2398), new Angle(0.83f, 0.56f)).Mask(SpawnPlane.Mask_Flatground)); // infront of elevator

            // first part of map
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(133134, -75252, 3298), new Vector3f(133859, -74498, 3298), new Angle(0.78f, 0.62f)).Mask(SpawnPlane.Mask_Flatground)); // first platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(137251, -73872, 4042), new Vector3f(138452, -74746, 4052), new Angle(-0.94f, 0.35f)).Mask(SpawnPlane.Mask_Highground).AsVerticalPlane()); // slide ledge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(144243, -69072, 3887), new Vector3f(144299, -69784, 3887), new Angle(-0.98f, 0.20f)).Mask(SpawnPlane.Mask_Highground).AsVerticalPlane()); // ledge before grinder
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(147668, -69379, 4493), new Angle(0.99f, 0.10f)).Mask(SpawnPlane.Mask_Highground)); // billboard after grinder
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(150356, -67284, 4804), new Angle(-0.96f, 0.28f)).Mask(SpawnPlane.Mask_Highground)); // billboard between default drones
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(154819, -68148, 4848), new Angle(1.00f, 0.04f)).Mask(SpawnPlane.Mask_Highground)); // last billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(154675, -66714, 3938), new Vector3f(155200, -67550, 3937), new Angle(-0.99f, 0.12f)).Mask(SpawnPlane.Mask_Highground).AsVerticalPlane());  // last slide ledge
            Rooms.Add(layout);

            //// Awakening Enemies
            List<Enemy> AwakeningEnemies = new List<Enemy>();
            enemies = Awakening_hc_room1.ReturnEnemiesInRoom(AllEnemies); //pistol, 2 uzi,  weeb, frogger
            AwakeningEnemies.AddRange(enemies.Take(4).ToList());
            enemies = Awakening_hc_room2.ReturnEnemiesInRoom(AllEnemies); // frogger,  pistol,  waver
            enemies[1].SetEnemyType(Enemy.EnemyTypes.Waver);
            AwakeningEnemies.AddRange(enemies);
            enemies = Awakening_hc_room4.ReturnEnemiesInRoom(AllEnemies); // drone, uzi, pistol 
            enemies[0] = new EnemyDrone(enemies[0]);
            AwakeningEnemies.AddRange(enemies);
            enemies = Awakening_hc_room5.ReturnEnemiesInRoom(AllEnemies); // pistol, waver, shielder, frogger
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Waver);
            AwakeningEnemies.AddRange(enemies);
            RemoveParentObjects(ref AwakeningEnemies); // remove parent objects
            EnemiesWithoutCP.AddRange(AwakeningEnemies); // just add them to enemy pool

            // custom checkpoints
            CustomCheckPoints.Add(new CustomCP(mapType ,new Vector3f(152500, -56671, 5228), new Vector3f(155863, -53898, 2256),
                new Vector3f(153243, -56343, 3598), new Angle(0.57f, 0.82f))); // before last room
        }

        public void Gen_Nightmare() {
            throw new System.NotImplementedException();
        }

        protected override void Gen_PerRoom() {}
    }
}
