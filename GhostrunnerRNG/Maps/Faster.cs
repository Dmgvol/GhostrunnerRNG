using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using System.Collections.Generic;
using static GhostrunnerRNG.Enemies.Enemy;

namespace GhostrunnerRNG.Maps {
    class Faster : MapCore {

        #region Rooms
        // City
        private Room room_1 = new Room(new Vector3f(12973, -167437, -2550), new Vector3f(20962, -156159, -132)); // 2 drones
        private Room room_2 = new Room(new Vector3f(6608, -173850, -1893), new Vector3f(8679, -170686, -61)); // 1 drones
        private Room room_3 = new Room(new Vector3f(136, -168617, -4263), new Vector3f(-7818, -160200, 770)); // 1 weeb, 1 drone, 2 uzi, 1 pistol

        // Train
        private Room room_4 = new Room(new Vector3f(-11027, 143531, 1833), new Vector3f(-12681, 149757, -538)); // 2 weebs, 1 pistol
        private Room room_5 = new Room(new Vector3f(-10979, 172915, 1868), new Vector3f(-12791, 178916, -171)); // 1 weeb, 1 waver
        private Room room_6 = new Room(new Vector3f(-10888, 193792, 2282), new Vector3f(-12856, 204304, -232)); // 3 weebs, 1 frogger
        #endregion

        public Faster(bool isHC) : base(GameUtils.MapType.Faster) {
            if(!isHC) {
                Gen_PerRoom();
            } else {
                // hc
            }
        }

        protected override void Gen_PerRoom() {
            //indexes ?
            List<Enemy> AllEnemies = GetAllEnemies(MainWindow.game, 0, 6);
            AllEnemies.AddRange(GetAllEnemies(MainWindow.game, 8, 11));
            Rooms = new List<RoomLayout>();
            RoomLayout layout;
            List<Enemy> enemies;

            enemies = room_1.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyDrone(enemies[0]);
            enemies[1] = new EnemyDrone(enemies[1]);
            EnemiesWithoutCP.AddRange(enemies);

            enemies = room_2.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyDrone(enemies[0]);
            layout = new RoomLayout(enemies[0]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6299, -173453, -1379), new Vector3f(7884, -171472, -787)));
            layout.Mask(SpawnPlane.Mask_Airborne);
            Rooms.Add(layout);

            enemies = room_3.ReturnEnemiesInRoom(AllEnemies);

            enemies[0] = new EnemyDrone(enemies[0]);
            enemies[4].SetEnemyType(EnemyTypes.Weeb);
            layout = new RoomLayout(enemies);

            //drone planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3380, -162846, -1032), new Vector3f(-1529, -164636, -532), new Angle(-0.34f, 0.94f)).Mask(SpawnPlane.Mask_Airborne));//near left billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6211, -163236, -1457), new Vector3f(-4321, -164490, -618), new Angle(-0.01f, 1.00f)).Mask(SpawnPlane.Mask_Airborne));//between grapple point and door 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(2870, -164751, -1396), new Vector3f(841, -166047, -595), new Angle(-0.52f, 0.85f)).Mask(SpawnPlane.Mask_Airborne));//left of the cp point
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-5800, -166436, -1140), new Vector3f(-3738, -168281, -544), new Angle(-0.08f, 1.00f)).Mask(SpawnPlane.Mask_Airborne));//above pistol+uzi plane
            //pistols +uzi+weeb
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2262, -165921, -1065), new Angle(-0.14f, 0.99f)).Mask(SpawnPlane.Mask_HighgroundLimited));//pole between billboards
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6285, -161101, -799), new Angle(-0.55f, 0.84f)).Mask(SpawnPlane.Mask_HighgroundLimited));// above shuriken
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6987, -162593, -1043), new Angle(0.02f, 1.00f)).Mask(SpawnPlane.Mask_HighgroundLimited));//zipline pole near door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1224, -165351, -1141), new Vector3f(-3209, -165362, -1141), new Angle(-0.17f, 0.99f)).AsVerticalPlane().Mask(SpawnPlane.Mask_HighgroundLimited));//left billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2422, -161895, -1043), new Angle(-0.70f, 0.72f)).Mask(SpawnPlane.Mask_HighgroundLimited));//end of zipline on weeb platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(232, -165490, -2201), new Vector3f(-392, -167449, -2201), new Angle(-0.41f, 0.91f)).Mask(SpawnPlane.Mask_Highground));//platform near cp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(2087, -166558, -971), new Angle(-0.24f, 0.97f)).Mask(SpawnPlane.Mask_HighgroundLimited));//pole near cp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4725, -165273, -611), new Angle(0.03f, 1.00f)).Mask(SpawnPlane.Mask_HighgroundLimited));//sigh in the middle
            //defualt planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6153, -167230, -2191), new Vector3f(-3729, -166006, -2192), new Angle(0.24f, 0.97f)).SetMaxEnemies(2).Mask(SpawnPlane.Mask_Flatground));//pistol+uzi
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4328, -164639, -2191), new Vector3f(-3766, -165860, -2191), new Angle(0.01f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));//uzi near drone
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6809, -161689, -1611), new Vector3f(-4012, -162393, -1611), new Angle(-0.31f, 0.95f)).Mask(SpawnPlane.Mask_Flatground));//weeb plane

            Rooms.Add(layout);


            //delete additional cp in that section???
            ModifyCP(new DeepPointer(0x045A3C20, 0x98, 0x18, 0x128, 0xA8, 0x1760, 0x240, 0x398, 0x150), new Vector3f(0, 0, 0), MainWindow.game);
            enemies = room_4.ReturnEnemiesInRoom(AllEnemies);
            enemies[1].SetEnemyType(EnemyTypes.Weeb);
            enemies[2].SetEnemyType(EnemyTypes.Weeb);
            layout = new RoomLayout(enemies);
            //air spawns
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11564, 149494, 1110), new Vector3f(-12251, 140205, 1110), new Angle(-0.73f, 0.68f)).SetMaxEnemies(2).Mask(SpawnPlane.Mask_Airborne));//air spawn for drones

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11798, 138623, 340), new Angle(-0.71f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));//in front of the spawn
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11814, 135849, 1018), new Angle(0.70f, 0.72f)).Mask(SpawnPlane.Mask_HighgroundLimited));//behind the spawn
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11686, 141284, 893), new Angle(-0.72f, 0.69f)).Mask(SpawnPlane.Mask_HighgroundLimited));//first boxes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11804, 142467, 299), new Angle(-0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));//between the boxes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11797, 150242, 1243), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_HighgroundLimited));//above door
            //default planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-12363, 145626, 318), new Vector3f(-11238, 148925, 318), new Angle(-0.70f, 0.71f)).SetMaxEnemies(2).Mask(SpawnPlane.Mask_Flatground));

            Rooms.Add(layout);

            enemies = room_5.ReturnEnemiesInRoom(AllEnemies);
            enemies[0].SetEnemyType(EnemyTypes.Waver);
            enemies[1].SetEnemyType(EnemyTypes.Weeb);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, removeCP: false);
            layout = new RoomLayout(enemies);
            //air spawns
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11008, 177873, 1109), new Vector3f(-12534, 175725, 1109), new Angle(-0.68f, 0.73f)).SetMaxEnemies(2).Mask(SpawnPlane.Mask_Airborne));//air spawn

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-10924, 178352, 336), new Angle(-0.75f, 0.66f)).Mask(SpawnPlane.Mask_Flatground));//nearzipline
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11280, 175423, 318), new Vector3f(-11537, 175892, 318), new Angle(-0.87f, 0.49f)).Mask(SpawnPlane.Mask_Flatground));//between pipes
            //deafult planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11903, 177370, 335), new Vector3f(-12342, 175403, 318), new Angle(-0.71f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));//weeb plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-12157, 178006, 365), new Vector3f(-11460, 177654, 361), new Angle(-0.69f, 0.72f)).Mask(SpawnPlane.Mask_Flatground));//waver plane
            Rooms.Add(layout);

            //Delete cp before EoL
            ModifyCP(new DeepPointer(0x045A3C20, 0x98, 0x18, 0x128, 0xA8, 0x1720, 0x240, 0x398, 0x150), new Vector3f(0, 0, 0), MainWindow.game);
            enemies = room_6.ReturnEnemiesInRoom(AllEnemies);
            enemies[1].SetEnemyType(EnemyTypes.Weeb);
            enemies[2].SetEnemyType(EnemyTypes.Weeb);
            enemies[3].SetEnemyType(EnemyTypes.Weeb);
            layout = new RoomLayout(enemies);
            //air spawns
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11114, 198101, 1245), new Vector3f(-12437, 196885, 1245), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_Airborne));//first plafrom air
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11120, 203207, 1245), new Vector3f(-12367, 199525, 1245), new Angle(-0.71f, 0.70f)).Mask(SpawnPlane.Mask_Airborne));//second platform air 

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11753, 201524, 893), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_HighgroundLimited));//boxes neardoor
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-12289, 201724, 317), new Angle(-0.69f, 0.72f)).Mask(SpawnPlane.Mask_Flatground));//right side of boxes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11436, 203800, 359), new Vector3f(-12135, 203572, 359), new Angle(-0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));//before EoL
            //deafult planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11265, 198217, 317), new Vector3f(-12322, 196118, 317), new Angle(-0.72f, 0.70f)).SetMaxEnemies(2).Mask(SpawnPlane.Mask_Flatground));//2weebs
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11236, 201290, 318), new Vector3f(-12436, 199282, 317), new Angle(-0.72f, 0.70f)).SetMaxEnemies(2).Mask(SpawnPlane.Mask_Flatground));//weeb+frogger
            Rooms.Add(layout);

            //random spots
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-17479, -142371, -2700), new Angle(-0.72f, 0.70f)).Mask(SpawnPlane.Mask_Airborne));//slide section before second shuriken
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-10730, -161776, -1647), new Vector3f(-9819, -162463, -1647), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));//after 1 fight
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(12121, -171111, -763), new Angle(0.20f, 0.98f)).Mask(SpawnPlane.Mask_Airborne));//near second billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(27927, -152787, 605), new Angle(0.70f, 0.72f)).Mask(SpawnPlane.Mask_Highground));//hel platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11928, 151164, 340), new Angle(-0.66f, 0.75f)).Mask(SpawnPlane.Mask_Flatground));//collectible room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11749, 179582, 1307), new Angle(0.72f, 0.70f)).Mask(SpawnPlane.Mask_Airborne));//after second train fight
            Rooms.Add(layout);
        }
    }
}
