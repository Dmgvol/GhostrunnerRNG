using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using System.Collections.Generic;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.Maps {
    class RunUp : MapCore {

        // Note: Enemies are not in the same order in this map, so we group them by their RoomRectangle

        // not needed for cp
        private Room room_1 = new Room(new Vector3f(58593, -67346, 2158), new Vector3f(69793, -64940, 4337));//4 pistols guys in first room  //used 2 in last room
        private Room room_2 = new Room(new Vector3f(55639, -14697, 1718), new Vector3f(63942, -12552, 3351));//2 geckos after sliding room   //used 

        //enemies needed for cp
        private Room room_11 = new Room(new Vector3f(70912, -67435, 2393), new Vector3f(72590, -65153, 3760));//2 last guy in first hallway
        private Room room_12 = new Room(new Vector3f(71127, -62511, 2964), new Vector3f(75440, -59777, 4564));//4 pistols in second room
        private Room room_13 = new Room(new Vector3f(49154, -16915, 2023), new Vector3f(52701, -11231, 4658));//2 geckos, kunai room
        private Room room_14 = new Room(new Vector3f(49128, -2705, 5097), new Vector3f(55849, 4367, 8069));//3 geckos room
        private Room room_15 = new Room(new Vector3f(48342, 4783, 4664), new Vector3f(56801, 14851, 7506));//final fight room

        public RunUp(bool isHC) : base(MapType.RunUp) {
            if(!isHC) {
                Gen_PerRoom();
            } else {
                // hardcore
                //TODO: remove temporary block/msg and add hc enemies and gen
            }
        }

        protected override void Gen_PerRoom() {
            List<Enemy> AllEnemies = GetAllEnemies(MainWindow.game);
            AllEnemies.AddRange(GetAllEnemies(MainWindow.game, 10));

            Rooms = new List<RoomLayout>();
            RoomLayout layout;
            List<Enemy> enemies1 = room_1.ReturnEnemiesInRoom(AllEnemies);//random spots on the map for 1 pistol
            layout = new RoomLayout(enemies1[0]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(67967, -43516, 2458), new Angle(-0.71f, 0.70f)));//after sliding before crusher
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(65752, -18076, 2298), new Angle(-0.68f, 0.73f)));//after sliding, under collectible
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53694, -67174, 6018), new Angle(0.99f, 0.16f)));//near spawn room on the left wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(75144, -65817, 3869), new Angle(1.00f, 0.00f)));//billboard near 4 pistols room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(52543, -10639, 3718), new Angle(0.97f, 0.23f)));//platform near last ollectible
            Rooms.Add(layout);

            //// room 11 layout //// 2 dudes 
            ModifyCP(new DeepPointer(0x045A3C20, 0x98, 0x0, 0x128, 0xA8, 0xA98, 0x248, 0x1D0), new Vector3f(55595, -13929, 2228), MainWindow.game);
            List<Enemy> enemies = room_11.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies[0], enemies[1]);//room with train on the left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(59758, -13541, 2218), new Vector3f(59317, -13955, 2218), new Angle(-0.56f, 0.83f)));//behind middle wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(63587, -13130, 2498), new Angle(-0.69f, 0.73f)));//citylight
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61651, -12993, 3008), new Angle(-0.13f, 0.99f)));//net
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(61694, -14505, 2639), new Angle(0.11f, 0.99f)));//thing on right wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(56229, -13321, 2837), new Vector3f(55332, -13320, 2837), new Angle(-0.09f, 1.00f)));//left billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(59047, -13118, 2693), new Angle(-0.68f, 0.73f)));//dumpster
            //deafult planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(60769, -14395, 2218), new Vector3f(63548, -13278, 2218), new Angle(-0.02f, 1.00f)));//1 gecko spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(58478, -14392, 2225), new Vector3f(63548, -13278, 2218), new Angle(-0.06f, 1.00f)));//2 gecko spawn plane
            Rooms.Add(layout);

            //// room 12 layout ////
            enemies = room_12.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies[0], enemies[1], enemies[2], enemies[3], enemies1[1]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74554, -63128, 4334), new Angle(-0.38f, 0.93f)));// thing on the wall near entry door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(73100, -62328, 4435), new Vector3f(73100, -59976, 4435), new Angle(0.00f, 1.00f)));//shorter line
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71980, -61100, 4435), new Vector3f(74056, -61100, 4435), new Angle(-0.31f, 0.95f)));//other line
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(74625, -59867, 4658), new Angle(-0.71f, 0.70f)));//air conditioner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71721, -59849, 4335), new Angle(-0.32f, 0.95f)));//lamp on top of the exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71218, -61708, 3956), new Angle(0.00f, 1.00f)));//citylight
            //defafult planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(72730, -60695, 3598), new Vector3f(71414, -60001, 3597), new Angle(-0.26f, 0.96f)));//closest to the exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(75255, -62267, 3598), new Vector3f(73738, -61609, 3604), new Angle(-0.54f, 0.84f)));//closest to the entry
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(71589, -62234, 3598), new Vector3f(72761, -61553, 3598), new Angle(0.00f, 1.00f)));//platform on the right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(73675, -59999, 3597), new Vector3f(75104, -60602, 3597), new Angle(-0.72f, 0.69f)));//platform on the left
            Rooms.Add(layout);

            //// room 13 layout ////
            List<Enemy> enemies2 = room_2.ReturnEnemiesInRoom(AllEnemies);
            enemies = room_13.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies[0], enemies[1], enemies2[0]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53548, -13563, 3048), new Angle(-0.44f, 0.90f)));//boxes near entry spot
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(52217, -11717, 3198), new Angle(-0.77f, 0.64f)));//kunai boxes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50642, -11379, 3198), new Angle(-0.41f, 0.91f)));//boxes near leftgecko
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49175, -13289, 3188), new Angle(-0.05f, 1.00f)));//turrets boxes 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49656, -15830, 2898), new Angle(0.24f, 0.97f)));//boxes at the back of right gecko
            //default planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49962, -14304, 2598), new Vector3f(51544, -16156, 2598), new Angle(0.29f, 0.96f)));//right gecko
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50023, -13466, 2598), new Vector3f(51993, -11847, 2598), new Angle(-0.28f, 0.96f)));//left gecko
            Rooms.Add(layout);

            //1 pistol additional layout for room 13
            layout = new RoomLayout(enemies1[2]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51911, -13582, 3405), new Angle(-0.06f, 1.00f)));//thing on the left of the pillar
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51068, -13883, 3363), new Angle(1.00f, 0.05f)));//back of the pillar
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(47997, -11227, 3918), new Angle(-0.24f, 0.97f)));//first billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(48251, -9843, 4528), new Angle(-0.69f, 0.72f)));//second billboard
            Rooms.Add(layout);

            //// room 14 layout ////
            enemies = room_14.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies[0], enemies[1], enemies[2], enemies2[1]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53751, -2797, 5798), new Angle(-0.71f, 0.70f)));//left corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50960, -2810, 5798), new Angle(-0.72f, 0.70f)));//right corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53403, 850, 6388), new Angle(-0.88f, 0.48f)));// boxes near center on the left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51190, 466, 6098), new Angle(-0.72f, 0.69f)));//boxes near center on the left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49516, -1148, 6403), new Angle(-0.60f, 0.80f)));//boxes near center on the left

            //default planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(55354, 1687, 5798), new Vector3f(54043, -631, 5798), new Angle(-0.84f, 0.54f)));//left gecko
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(49521, -670, 5798), new Vector3f(50701, 1905, 5798), new Angle(-0.69f, 0.72f)));//right gecko
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53666, 3250, 5798), new Vector3f(50942, 3250, 5798), new Angle(-0.71f, 0.71f)).AsVerticalPlane());//center gecko
            Rooms.Add(layout);

            //// room 15 layout ////
            enemies = room_15.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies[0], enemies[1], enemies[2], enemies1[3]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(54329, 9010, 6776), new Vector3f(52839, 7535, 6776), new Angle(-0.83f, 0.56f)).AsVerticalPlane());//first billboard on the left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(54682, 8373, 7058), new Vector3f(53563, 7285, 7056), new Angle(-0.96f, 0.27f)).AsVerticalPlane());//second billboard on the left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51564, 7811, 6811), new Vector3f(50386, 8987, 6811), new Angle(-0.45f, 0.89f)).AsVerticalPlane());//first billboard on the right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(50995, 7395, 7058), new Vector3f(50109, 8306, 7058), new Angle(-0.31f, 0.95f)).AsVerticalPlane());//second billboard on the right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(48417, 9813, 6598), new Vector3f(51489, 12848, 6598), new Angle(-0.44f, 0.90f)).AsVerticalPlane());//wall on the right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(53292, 12883, 6598), new Vector3f(55813, 10359, 6598), new Angle(-0.88f, 0.48f)).AsVerticalPlane());//wall on the left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(52840, 13065, 6254), new Vector3f(52840, 13764, 6254), new Angle(-0.72f, 0.69f)));//box near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51679, 6367, 6428), new Angle(-0.55f, 0.84f)));//box near entry
            //default planes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(55217, 9194, 5786), new Vector3f(55234, 10840, 5786), new Angle(-0.90f, 0.43f)));//left guy
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(51513, 12678, 5786), new Vector3f(53308, 11056, 5786), new Angle(-0.72f, 0.69f)));//gecko platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(48797, 9504, 5786), new Vector3f(50704, 9906, 5786), new Angle(-0.42f, 0.91f)));//right platform
            Rooms.Add(layout);
        }
    }
}
