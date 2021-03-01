using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using System.Collections.Generic;

namespace GhostrunnerRNG.Maps {
    class RoadToAmida : MapCore {

        private Room room_1 = new Room(new Vector3f(-119169, -37299, -8955), new Vector3f(-124655, -30482, -12887));
        private Room room_2 = new Room(new Vector3f(-120841, -30147, -11238), new Vector3f(-123170, -25538, -13420));
        private Room room_3 = new Room(new Vector3f(-120674, -25268, -11602), new Vector3f(-123211, -20627, -13942));
        private Room room_4 = new Room(new Vector3f(-120133, -17246, -13402), new Vector3f(-113096, -22544, -15989));
        private Room room_5 = new Room(new Vector3f(-118188, 10185, -15815), new Vector3f(-112812, 12300, -18047)); // enemies without cp, shield+protected pistol
        private Room room_6 = new Room(new Vector3f(-118732, 9722, -18329), new Vector3f(-107975, 541, -13440)); // 
        private Room room_7 = new Room(new Vector3f(-96861, 4781, -13460), new Vector3f(-91996, 11264, -16012)); // room with moving glowing platforms
        private Room room_8 = new Room(new Vector3f(-105953, 5060, -14048), new Vector3f(-99244, 8356, -16082)); // 
        private Room room_9 = new Room(new Vector3f(-101561, 15696, -13167), new Vector3f(-105575, 10802, -15290));
        private Room room_10 = new Room(new Vector3f(-91765, 14668, -13922), new Vector3f(-80907, 6970, -11828)); // first fan room
        private Room room_11 = new Room(new Vector3f(-74147, 3981, -1563), new Vector3f(-69076, 11619, -6770)); // line of uzi  - no cp needed
        private Room room_12 = new Room(new Vector3f(-70360, -1874, -3384), new Vector3f(-64374, 480, -5589)); // room before 2'nd fan


        public RoadToAmida(bool isHC) : base(GameUtils.MapType.RoadToAmida) {
            if(!isHC) {
                Gen_PerRoom();
            } else {
                // hardcore
                //TODO: remove temporary block/msg and add hc enemies and gen
            }
        }
        protected override void Gen_PerRoom() {
            List<Enemy> AllEnemies = GetAllEnemies(MainWindow.game, enemiesTarget:38);
            MainWindow.GlobalLog = "Enemies found: " + AllEnemies.Count;

            Rooms = new List<RoomLayout>();
            RoomLayout layout;

            //// Room 1 ////
            List<Enemy> enemies = room_1.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies[0]);
            // default platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121498, -36259, -11331), new Vector3f(-122633, -34975, -11328), new Angle(-0.71f, 0.70f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-122654, -32837, -11921), new Vector3f(-121363, -31014, -11914), new Angle(-0.71f, 0.71f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121262, -35626, -11331), new Angle(-0.95f, 0.31f))); // left side, between containers

            // high places
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-122835, -30770, -11316), new Angle(-0.61f, 0.79f))); // on box, near door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121166, -36880, -10731), new Angle(0.97f, 0.22f)));  // on box, near elevator
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-119614, -37179, -10849), new Angle(1.00f, 0.03f)));  // far wall corner, on pipe
            Rooms.Add(layout);

            //// Room 2 ////
            enemies = room_2.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121604, -27290, -12628), new Vector3f(-122402, -25842, -12624), new Angle(-0.70f, 0.72f))); // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-123001, -26294, -11638), new Angle(-0.54f, 0.84f))); // left billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121974, -29579, -12881), new Angle(-0.81f, 0.58f))); // under the slide
            Rooms.Add(layout);

            //// Room 3 ////
            enemies = room_3.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies[0], enemies[1]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121568, -22633, -13328), new Vector3f(-120958, -21043, -13328), new Angle(-0.72f, 0.70f))); // default left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-122357, -22693, -13331), new Vector3f(-123071, -20975, -13324), new Angle(-0.71f, 0.71f))); // default right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-122267, -20256, -13327), new Vector3f(-121763, -19680, -13327), new Angle(-0.71f, 0.71f))); // near exit door

            // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-122993, -20828, -12108), new Angle(-0.55f, 0.84f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-120937, -20797, -12108), new Angle(-0.92f, 0.40f)));

            Rooms.Add(layout);

            //// Room 4 ////
            enemies = room_4.ReturnEnemiesInRoom(AllEnemies);
            ShieldOrb shieldOrb = new ShieldOrb(enemies[0], new DeepPointer(0x045A3C20, 0x98, 0x10, 0x128, 0xA8, 0x688, 0x130, 0x1D0));

            if(Config.GetInstance().Gen_RngOrbs) {
                layout = new RoomLayout(shieldOrb);
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-116806, -22430, -14855), new Vector3f(-114075, -22327, -14188)).AsVerticalPlane()); // left billboard
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-118367, -20124, -14822))); // under slide
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-116063, -19212, -14813), new Vector3f(-116171, -18640, -13262)).AsVerticalPlane()); // front face of box stack
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-115419, -21463, -13790))); // left cage
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-114441, -19968, -14101))); // floating above platform
                Rooms.Add(layout);
            }

            layout = new RoomLayout(enemies[1], enemies[2], enemies[3]);
            // default platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-119333, -18499, -14931), new Vector3f(-117388, -17672, -14931), new Angle(-0.98f, 0.22f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-116236, -17567, -14528), new Vector3f(-115386, -18330, -14528), new Angle(1.00f, 0.05f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-115821, -22419, -15281), new Vector3f(-116839, -21277, -15281), new Angle(0.92f, 0.39f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-116015, -20477, -15031), new Vector3f(-113295, -19381, -15031), new Angle(-1.00f, 0.00f)));

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-115612, -18915, -13091), new Angle(-0.98f, 0.18f))); // highest box
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-115791, -17533, -13814), new Angle(-0.90f, 0.44f))); // light above exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-114749, -17826, -13772), new Angle(-0.99f, 0.11f))); //billboard, near exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-118393, -21081, -13317), new Angle(0.66f, 0.75f)));  // billboard, left of slide
            Rooms.Add(layout);

            //// Room 5+6 //// 
            enemies = room_5.ReturnEnemiesInRoom(AllEnemies); // orb, pistol, shield, no cp
            enemies.AddRange(room_6.ReturnEnemiesInRoom(AllEnemies));
            // 5 enemies, 0 index is orb
            shieldOrb = new ShieldOrb(enemies[0], new DeepPointer(0x045A3C20, 0x98, 0x28, 0x128, 0xA8, 0xE08, 0x130, 0x1D0));
            if(Config.GetInstance().Gen_RngOrbs) {
                layout = new RoomLayout(shieldOrb);
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-112894, 7020, -16380), new Vector3f(-112984, 7805, -14904)).AsVerticalPlane()); // vertical cage side
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-117073, 4758, -15140))); // top of the railing
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-109666, 3614, -15146))); // side of wall, SR route
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-111463, 528, -14527)));  // on top of lamp, last platform
                Rooms.Add(layout);
            }

            layout = new RoomLayout(enemies[1], enemies[2], enemies[3] , enemies[4]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-111614, 5942, -15997), new Angle(0.86f, 0.50f))); // right broken platform, sr route
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-108101, 2165, -14369), new Vector3f(-108633, 1473, -14369), new Angle(-1.00f, 0.01f))); // exit door platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-112149, 917, -15253), new Angle(0.80f, 0.59f))); // last platform, top of box
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-110696, 1754, -15849), new Vector3f(-111631, 938, -15848), new Angle(0.70f, 0.71f))); // last platform, default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-111831, 5042, -16000), new Vector3f(-112503, 5527, -15998), new Angle(0.89f, 0.46f))); // default platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-113481, 10328, -16348), new Angle(0.99f, 0.16f))); // billboard of the entrance
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-109031, 5221, -15180), new Angle(1.00f, 0.00f))); // right wall ledge
            Rooms.Add(layout);

            //// Room 7 //// 
            enemies = room_7.ReturnEnemiesInRoom(AllEnemies);
            shieldOrb = new ShieldOrb(enemies[0], new DeepPointer(0x045A3C20, 0x98, 0x30, 0x128, 0xA8, 0xFD0, 0x130, 0x1D0));
            shieldOrb.HideBeam(new DeepPointer(0x045A3C20, 0x98, 0x30, 0x128, 0xA8, 0x370, 0x200, 0x8, 0x1D0));
            shieldOrb.LinkObject(new DeepPointer(0x045A3C20, 0x98, 0x30, 0x128, 0xA8, 0x370, 0x220));
            if(Config.GetInstance().Gen_RngOrbs) {
                layout = new RoomLayout(shieldOrb);
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-94185, 6306, -14368), new Vector3f(-93859, 8862, -13461))); // in between billboards
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-93058, 5750, -13834))); // top of billboard
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-95601, 10149, -13364))); // floating, after glowing blocks
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-94240, 8786, -14550))); // middle of the room
                Rooms.Add(layout);
            }

            layout = new RoomLayout(enemies[1], enemies[2], enemies[3]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-95601, 10149, -13364), new Vector3f(-95592, 8268, -13364), new Angle(0.01f, 1.00f))); // top of beam 1
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-95577, 8105, -13364), new Vector3f(-95593, 6496, -13364), new Angle(0.02f, 1.00f)));  // top of beam 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-94285, 7130, -13378), new Angle(-0.87f, 0.49f))); // top of billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-93058, 4834, -13763), new Angle(0.99f, 0.14f))); // left side wall generator
            // default platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-97469, 9508, -14303), new Vector3f(-98174, 10223, -14299), new Angle(-0.14f, 0.99f))); 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-92157, 7589, -14599), new Vector3f(-92551, 6403, -14599), new Angle(-1.00f, 0.04f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-94334, 7919, -14896), new Vector3f(-95019, 6411, -14894), new Angle(-0.75f, 0.67f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-94352, 9462, -14499), new Vector3f(-94938, 10351, -14499), new Angle(-0.69f, 0.72f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-93888, 8890, -15200), new Vector3f(-92167, 7983, -15199), new Angle(-1.00f, 0.03f)));
            Rooms.Add(layout);

            //// Room 8 ////
            enemies = room_8.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-100666, 5989, -15110), new Angle(0.98f, 0.18f))); // on scaffolding
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-103580, 5046, -15086), new Angle(0.08f, 1.00f))); // on concrete entrance
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-101314, 5796, -15086), new Angle(0.95f, 0.32f))); // on concrete entrance, around corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-104842, 6613, -15020), new Angle(-0.20f, 0.98f))); // on pipes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-100972, 7806, -14086), new Angle(-0.57f, 0.82f))); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-102236, 5205, -15491), new Angle(0.01f, 1.00f))); // end of slide
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-103141, 6677, -15369), new Angle(-0.47f, 0.88f))); // on barrels, behind arrow

            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-99556, 7985, -15489), new Vector3f(-101228, 6538, -15489), new Angle(-0.99f, 0.15f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-104240, 5218, -15491), new Vector3f(-102682, 6463, -15491), new Angle(-0.05f, 1.00f)));
            Rooms.Add(layout);

            //// Room 9 ////
            enemies = room_9.ReturnEnemiesInRoom(AllEnemies);
            shieldOrb = new ShieldOrb(enemies[0], new DeepPointer(0x045A3C20, 0x98, 0x38, 0x128, 0xA8, 0xDD0, 0x130, 0x1D0));
            if(Config.GetInstance().Gen_RngOrbs) {
                layout = new RoomLayout(shieldOrb);
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-101963, 14350, -14361))); // under stairs
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-104783, 13319, -13301))); // stacked boxes in the corner
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-102633, 10828, -12974))); // right top billboard
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-103380, 11993, -13474))); // floating above exit door
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-103292, 13457, -14339))); // floating above void
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-105032, 15512, -13885), new Vector3f(-105099, 13642, -13209)).AsVerticalPlane()); // default billboard
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-101763, 15493, -14003), new Vector3f(-103769, 15534, -13281)).AsVerticalPlane()); // front billboard
                Rooms.Add(layout);
            }

            layout = new RoomLayout(enemies[1], enemies[2], enemies[3]);
            // default platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-103879, 11041, -14382), new Vector3f(-102798, 12629, -14382), new Angle(0.70f, 0.72f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-104350, 13669, -13885), new Vector3f(-104969, 15345, -13885), new Angle(-0.01f, 1.00f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-103579, 14441, -14361), new Vector3f(-102168, 15417, -14357), new Angle(-0.56f, 0.83f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-102337, 13979, -13875), new Vector3f(-101678, 13558, -13875), new Angle(-0.71f, 0.70f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-104025, 12575, -13674), new Angle(0.44f, 0.90f))); // lamp above exit gate
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-102449, 12879, -12419), new Angle(0.91f, 0.42f)));    // top of concrete wall/pillar
            Rooms.Add(layout);

            //// Rooms 10 ////
            enemies = room_10.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-91390, 11132, -13168), new Vector3f(-90763, 12708, -13169), new Angle(0.36f, 0.93f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-85818, 11297, -13169), new Vector3f(-87700, 10952, -13169), new Angle(-1.00f, 0.00f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-85205, 9900, -12863), new Vector3f(-84574, 8194, -12871), new Angle(0.98f, 0.19f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-84030, 11493, -12481), new Vector3f(-85112, 10590, -12481), new Angle(-1.00f, 0.00f)));

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-85976, 9689, -12141), new Angle(0.96f, 0.28f)));  // billboard 1
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-88304, 10633, -12508), new Angle(0.97f, 0.22f))); // billboard 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-85509, 10719, -12321), new Angle(0.99f, 0.10f))); // billboard 3
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-85055, 6791, -12136), new Angle(0.71f, 0.71f)));  // left side wall pipe
            Rooms.Add(layout);

            //// Room 11 ////
            enemies = room_11.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies);

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-68144, 11533, -5931), new Angle(-0.96f, 0.27f))); // corner concrete block
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67995, 10868, -5821), new Angle(1.00f, 0.00f))); // concerete door frame
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-69155, 11633, -5194), new Angle(-0.96f, 0.30f))); // red billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-70231, 11599, -4721), new Vector3f(-68040, 11618, -4721), new Angle(-0.93f, 0.37f)).AsVerticalPlane().SetMaxEnemies(2)); // right livingblock ledge

            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-68382, 10604, -6226), new Vector3f(-69206, 11151, -6226), new Angle(-1.00f, 0.03f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-69488, 11052, -6226), new Vector3f(-69904, 9679, -6226), new Angle(-1.00f, 0.03f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-70111, 8409, -6211), new Vector3f(-69343, 6820, -6226), new Angle(1.00f, 0.00f)));
            Rooms.Add(layout);

            //// Room 12 ////
            enemies = room_12.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-68210, -874, -4446), new Vector3f(-69181, -1530, -4451), new Angle(-1.00f, 0.01f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67124, -1629, -4451), new Vector3f(-64557, -861, -4451), new Angle(-1.00f, 0.01f)).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-64548, 320, -4651), new Vector3f(-65881, -557, -4651), new Angle(-1.00f, 0.03f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67032, -100, -5161), new Vector3f(-68004, -743, -5161), new Angle(-1.00f, 0.01f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-69590, -618, -4452), new Angle(-0.79f, 0.61f))); // right corner
            Rooms.Add(layout);

        }
    }
}
