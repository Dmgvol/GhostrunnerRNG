using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.NonPlaceableObjects;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GhostrunnerRNG.Maps {
    class RoadToAmida : MapCore, IModes {

        #region Rooms
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
        #endregion

        #region HC Rooms
        private Room HC_room1 = new Room(new Vector3f(-119241, -37492, -12910), new Vector3f(-124752, -30493, -10020)); // orb, 3 turret
        private Room HC_room2 = new Room(new Vector3f(-120864, -29674, -13645), new Vector3f(-123118, -25438, -11365)); // splitter, shielder
        private Room HC_room3 = new Room(new Vector3f(-123149, -24585, -14338), new Vector3f(-120635, -19175, -12061)); // 2 shielders, waver
        private Room HC_room4 = new Room(new Vector3f(-120264, -17637, -16170), new Vector3f(-112617, -22685, -12746)); // 2 orbs, 2 shielders, pistol
        private Room HC_room5 = new Room(new Vector3f(-122792, 529, -15259), new Vector3f(-116313, -3614, -13298)); // orb, turret, drone
        private Room HC_room6 = new Room(new Vector3f(-120064, 5343, -18663), new Vector3f(-122418, 9848, -16829)); // splitter, weeb - no cp
        private Room HC_room7 = new Room(new Vector3f(-121665, 19921, -16751), new Vector3f(-124897, 17820, -18561)); // shielder, frogger (linked to door, but no cp...weird)
        private Room HC_room8 = new Room(new Vector3f(-121356, 19674, -18436), new Vector3f(-113232, 10218, -16231)); // 2 wavers, frogger, weeb - no cp
        private Room HC_room9 = new Room(new Vector3f(-118434, 9613, -18346), new Vector3f(-107895, 437, -13285)); // orb, turret, pistol, uzi - no cp
        private Room HC_room10 = new Room(new Vector3f(-99455, 1181, -14346), new Vector3f(-97176, 5908, -12726)); // 2 weebs in hallway - no cp
        private Room HC_room11 = new Room(new Vector3f(-96922, 4637, -15751), new Vector3f(-92014, 11349, -13039)); // 2 shifters, frogger
        private Room HC_room12 = new Room(new Vector3f(-105947, 4982, -15998), new Vector3f(-99210, 8740, -14318)); // waver, turret, pistol, uzi - no cp
        private Room HC_room13 = new Room(new Vector3f(-101641, 15662, -13045), new Vector3f(-105406, 10741, -14899)); // shielder, 2 wavers
        private Room HC_room14 = new Room(new Vector3f(-107527, 12445, -14674), new Vector3f(-109077, 15272, -13255)); // lonely turret
        private Room HC_room15 = new Room(new Vector3f(-81169, 9777, -14768), new Vector3f(-91802, 14252, -11862)); // shield, turret, uzi, forgger, waver, weeb, splitter
        private Room HC_room16 = new Room(new Vector3f(-82513, 6761, -7130), new Vector3f(-73380, 10837, -3781)); // 2 turrets, frogger, shielder, orb
        private Room HC_room17 = new Room(new Vector3f(-69170, 5669, -6860), new Vector3f(-70700, 11144, -5259)); // splitter, waver, weeb
        private Room HC_room18 = new Room(new Vector3f(-70047, 2337, -6554), new Vector3f(-73999, 302, -4819)); // 2 weebs, frogger 
        private Room HC_room19 = new Room(new Vector3f(-69094, 421, -5230), new Vector3f(-64413, -1853, -3150)); // orb, waver, pistol, uzi
        private Room HC_room20 = new Room(new Vector3f(-57181, -2072, 1997), new Vector3f(-61588, 1029, 4336)); // 2 pistols, frogger
        #endregion

        public RoadToAmida() : base(GameUtils.MapType.RoadToAmida) { 
            ModifyCP(new DeepPointer(0x04609420, 0x98, 0x0, 0x128, 0xA8, 0xD0, 0x248, 0x1D0), new Vector3f(-122000, -39020, -11285), GameHook.game);
        }

        public void Gen_Normal() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game, 0, 16);
            AllEnemies.AddRange(GetAllEnemies(GameHook.game, 20, 22));

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
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-119614, -37179, -10849), new Angle(1.00f, 0.03f)).setDiff(1));  // far wall corner, on pipe
            Rooms.Add(layout);

            //// Room 2 ////
            enemies = room_2.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121604, -27290, -12628), new Vector3f(-122402, -25842, -12624), new Angle(-0.70f, 0.72f))); // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-123001, -26294, -11638), new Angle(-0.54f, 0.84f)).setDiff(1)); // left billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121974, -29579, -12881), new Angle(-0.81f, 0.58f))); // under the slide
            Rooms.Add(layout);

            //// Room 3 ////
            enemies = room_3.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies[0], enemies[1]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121568, -22633, -13328), new Vector3f(-120958, -21043, -13328), new Angle(-0.72f, 0.70f))); // default left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-122357, -22693, -13331), new Vector3f(-123071, -20975, -13324), new Angle(-0.71f, 0.71f))); // default right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-122267, -20256, -13327), new Vector3f(-121763, -19680, -13327), new Angle(-0.71f, 0.71f))); // near exit door

            // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-122993, -20828, -12108), new Angle(-0.55f, 0.84f)).setDiff(1));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-120937, -20797, -12108), new Angle(-0.92f, 0.40f)).setDiff(1));

            Rooms.Add(layout);

            //// Room 4 ////
            enemies = room_4.ReturnEnemiesInRoom(AllEnemies);
            EnemyShieldOrb shieldOrb = new EnemyShieldOrb(enemies[0]);

            layout = new RoomLayout(shieldOrb);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-116806, -22430, -14855), new Vector3f(-114075, -22327, -14188)).AsVerticalPlane()); // left billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-118367, -20124, -14822))); // under slide
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-116063, -19212, -14813), new Vector3f(-116171, -18640, -13262)).AsVerticalPlane()); // front face of box stack
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-115419, -21463, -13790))); // left cage
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-114441, -19968, -14101))); // floating above platform
            Rooms.Add(layout);


            layout = new RoomLayout(enemies[1], enemies[2], enemies[3]);
            // default platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-119333, -18499, -14931), new Vector3f(-117388, -17672, -14931), new Angle(-0.98f, 0.22f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-116236, -17567, -14528), new Vector3f(-115386, -18330, -14528), new Angle(1.00f, 0.05f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-115821, -22419, -15281), new Vector3f(-116839, -21277, -15281), new Angle(0.92f, 0.39f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-116015, -20477, -15031), new Vector3f(-113295, -19381, -15031), new Angle(-1.00f, 0.00f)));

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-115612, -18915, -13091), new Angle(-0.98f, 0.18f))); // highest box
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-115791, -17533, -13814), new Angle(-0.90f, 0.44f))); // light above exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-114749, -17826, -13772), new Angle(-0.99f, 0.11f)).setDiff(1)); //billboard, near exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-118393, -21081, -13317), new Angle(0.66f, 0.75f)).setDiff(1));  // billboard, left of slide
            Rooms.Add(layout);

            //// Room 5+6 //// 
            enemies = room_5.ReturnEnemiesInRoom(AllEnemies); // orb, pistol, shield, no cp
            enemies.AddRange(room_6.ReturnEnemiesInRoom(AllEnemies));
            // 5 enemies, 0 index is orb
            shieldOrb = new EnemyShieldOrb(enemies[0]);

            layout = new RoomLayout(shieldOrb);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-112894, 7020, -16380), new Vector3f(-112984, 7805, -14904)).AsVerticalPlane()); // vertical cage side
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-117073, 4758, -15140)).setDiff(1)); // top of the high railing
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-109666, 3614, -15146)).setDiff(1)); // side of wall, SR route
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-111463, 528, -14527)).setDiff(1));  // on top of lamp, last platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-111639, 5686, -15616))); // right mid platform, on red crate
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-114393, 5044, -15699))); // middle platform, blue crate
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-113495, 3271, -15681))); // on top of toggleable fan, 1
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-111747, 3230, -15681))); // on top of toggleable fan, 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-110488, 1935, -15851))); // last platform, corner
            Rooms.Add(layout);


            layout = new RoomLayout(enemies[1], enemies[2], enemies[3], enemies[4]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-111614, 5942, -15997), new Angle(0.86f, 0.50f))); // right broken platform, right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-108101, 2165, -14369), new Vector3f(-108633, 1473, -14369), new Angle(-1.00f, 0.01f))); // exit door platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-112149, 917, -15253), new Angle(0.80f, 0.59f))); // last platform, top of box
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-110696, 1754, -15849), new Vector3f(-111631, 938, -15848), new Angle(0.70f, 0.71f))); // last platform, default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-111831, 5042, -16000), new Vector3f(-112503, 5527, -15998), new Angle(0.89f, 0.46f))); // default platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-113481, 10328, -16348), new Angle(0.99f, 0.16f)).setDiff(1)); // billboard of the entrance
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-109031, 5221, -15180), new Angle(1.00f, 0.00f)).setDiff(1)); // right wall ledge
            Rooms.Add(layout);

            //// Room 7 //// 
            enemies = room_7.ReturnEnemiesInRoom(AllEnemies);
            shieldOrb = new EnemyShieldOrb(enemies[0]);
            shieldOrb.HideBeam_Range(0, 1);
            shieldOrb.LinkObject(1);

            layout = new RoomLayout(shieldOrb);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-94185, 6306, -14368), new Vector3f(-93859, 8862, -13461))); // in between billboards
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-93058, 5750, -13834)).setDiff(1)); // top of billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-95601, 10149, -13364))); // floating, after glowing blocks
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-94240, 8786, -14550))); // middle of the room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-92674, 10943, -14561))); // red crate at the back
            Rooms.Add(layout);

            layout = new RoomLayout(enemies[1], enemies[2], enemies[3]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-95601, 10149, -13364), new Vector3f(-95592, 8268, -13364), new Angle(0.01f, 1.00f)).setDiff(1)); // top of beam 1
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-95577, 8105, -13364), new Vector3f(-95593, 6496, -13364), new Angle(0.02f, 1.00f)).setDiff(1));  // top of beam 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-94285, 7130, -13378), new Angle(-0.87f, 0.49f)).setDiff(1)); // top of billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-93058, 4834, -13763), new Angle(0.99f, 0.14f)).setDiff(1)); // left side wall generator
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-91965, 7690, -15067), new Angle(0.97f, 0.25f))); // underplatform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-92071, 6029, -14421), new Angle(1.00f, 0.04f))); // left red crate
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
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-100972, 7806, -14086), new Angle(-0.57f, 0.82f)).setDiff(1)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-102236, 5205, -15491), new Angle(0.01f, 1.00f))); // end of slide
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-103141, 6677, -15369), new Angle(-0.47f, 0.88f))); // on barrels, behind arrow

            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-99556, 7985, -15489), new Vector3f(-101228, 6538, -15489), new Angle(-0.99f, 0.15f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-104240, 5218, -15491), new Vector3f(-102682, 6463, -15491), new Angle(-0.05f, 1.00f)));
            Rooms.Add(layout);

            //// Room 9 ////
            enemies = room_9.ReturnEnemiesInRoom(AllEnemies);
            shieldOrb = new EnemyShieldOrb(enemies[0]);
            layout = new RoomLayout(shieldOrb);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-101963, 14350, -14361))); // under stairs
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-104783, 13319, -13301))); // stacked boxes in the corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-102633, 10828, -12974))); // right top billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-103380, 11993, -13474))); // floating above exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-103292, 13457, -14339))); // floating above void
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-105032, 15512, -13885), new Vector3f(-105099, 13642, -13209)).AsVerticalPlane()); // default billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-101763, 15493, -14003), new Vector3f(-103769, 15534, -13281)).AsVerticalPlane()); // front billboard
            Rooms.Add(layout);
           

            layout = new RoomLayout(enemies[1], enemies[2], enemies[3]);
            // default platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-103879, 11041, -14382), new Vector3f(-102798, 12629, -14382), new Angle(0.70f, 0.72f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-104350, 13669, -13885), new Vector3f(-104969, 15345, -13885), new Angle(-0.01f, 1.00f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-103579, 14441, -14361), new Vector3f(-102168, 15417, -14357), new Angle(-0.56f, 0.83f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-102337, 13979, -13875), new Vector3f(-101678, 13558, -13875), new Angle(-0.71f, 0.70f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-104025, 12575, -13674), new Angle(0.44f, 0.90f)).setDiff(1)); // lamp above exit gate
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-102449, 12879, -12419), new Angle(0.91f, 0.42f)).setDiff(1));    // top of concrete wall/pillar
            Rooms.Add(layout);

            //// Rooms 10 ////
            enemies = room_10.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies);

            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-91390, 11132, -13168), new Vector3f(-90763, 12708, -13169), new Angle(0.36f, 0.93f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-85818, 11297, -13169), new Vector3f(-87700, 10952, -13169), new Angle(-1.00f, 0.00f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-85205, 9900, -12863), new Vector3f(-84574, 8194, -12871), new Angle(0.98f, 0.19f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-84426, 11696, -12482), new Vector3f(-85005, 10602, -12482), new Angle(-0.96f, 0.28f)));

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-85976, 9689, -12141), new Angle(0.96f, 0.28f)));  // billboard 1
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-88304, 10633, -12508), new Angle(0.97f, 0.22f))); // billboard 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-85509, 10719, -12321), new Angle(0.99f, 0.10f))); // billboard 3
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-85055, 6791, -12136), new Angle(0.71f, 0.71f)));  // left side wall pipe
            Rooms.Add(layout);

            //// Room 11 ////
            enemies = room_11.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies.Take(2).ToList()); // just the first 2

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-68144, 11533, -5931), new Angle(-0.96f, 0.27f))); // corner concrete block
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67995, 10868, -5821), new Angle(1.00f, 0.00f))); // concerete door frame
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-69155, 11633, -5194), new Angle(-0.96f, 0.30f)).setDiff(1)); // red billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-70231, 11599, -4721), new Vector3f(-68040, 11618, -4721), new Angle(-0.93f, 0.37f))
                .setDiff(1).AsVerticalPlane().SetMaxEnemies(2)); // right livingblock ledge

            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-68382, 10604, -6226), new Vector3f(-69206, 11151, -6226), new Angle(-1.00f, 0.03f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-69488, 11052, -6226), new Vector3f(-69904, 9679, -6226), new Angle(-1.00f, 0.03f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-70111, 8409, -6211), new Vector3f(-69343, 6820, -6226), new Angle(1.00f, 0.00f)));
            Rooms.Add(layout);

            // The rest to EnemiesWithoutCP
            EnemiesWithoutCP.AddRange(enemies.Skip(2));

            //// Room 12 ////
            enemies = room_12.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-68210, -874, -4446), new Vector3f(-69181, -1530, -4451), new Angle(-1.00f, 0.01f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67124, -1629, -4451), new Vector3f(-64557, -861, -4451), new Angle(-1.00f, 0.01f)).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-64548, 320, -4651), new Vector3f(-65881, -557, -4651), new Angle(-1.00f, 0.03f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67032, -100, -5161), new Vector3f(-68004, -743, -5161), new Angle(-1.00f, 0.01f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-69590, -618, -4452), new Angle(-0.79f, 0.61f))); // right corner

            // high places
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-70106, -695, -3644), new Angle(-0.93f, 0.37f)).setDiff(1)); // pillar
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-69710, -141, -4041), new Angle(-0.62f, 0.78f)).setDiff(1)); // umbrella
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67664, -1676, -4756), new Angle(0.71f, 0.70f))); // concrete door frame (middle left)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-70276, 1, -3851), new Angle(-0.96f, 0.29f)).setDiff(1)); // wall generator

            Rooms.Add(layout);


            ///// EXTRA
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-48853, -100, 3107), new Angle(-1.00f, 0.05f))); // on slide, before rotating part
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-20869, -550, 1575), new Angle(1.00f, 0.03f))); // last exit door
            Rooms.Add(layout);




            /////////////// NonPlaceableObjects ///////////////
            #region Shurikens
            // 1 - 2 targets, last walls
            NonPlaceableObject uplink = new UplinkShurikens(0x30, 0xF28);
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = Config.GetInstance().r.Next(40, 80) / 10.0f, MaxAttacks = 12 }); // normal rng
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 3, MaxAttacks = 2 }); // no mistakes
            worldObjects.Add(uplink);

            // 2 - 3 targets after fan
            uplink = new UplinkShurikens(0x48, 0x1B68);
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = Config.GetInstance().r.Next(50, 120) / 10.0f, MaxAttacks = 12 }); // normal rng
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 5, MaxAttacks = 3 }); // no mistakes
            worldObjects.Add(uplink);

            // 3 - 5 smgs
            uplink = new UplinkShurikens(0x40, 0xF20);
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = Config.GetInstance().r.Next(60, 120) / 10.0f, MaxAttacks = 20 }); // normal rng
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 6, MaxAttacks = 5 }); // no mistakes
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 3, MaxAttacks = 1 }); // just one, good luck
            worldObjects.Add(uplink);

            // 4 - last room
            uplink = new UplinkShurikens(0x8, 0x19A0);
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = Config.GetInstance().r.Next(50, 120) / 10.0f, MaxAttacks = 20 }); // normal rng
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 4, MaxAttacks = 3 }); // no mistakes
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 5, MaxAttacks = 2 }); // challenge: 3rd target with sword
            worldObjects.Add(uplink);
            #endregion

            #region ToggleableFans
            worldObjects.Add(new ToggleableFan(0x18, 0x68).LoadDefaultPreset());
            worldObjects.Add(new ToggleableFan(0x20, 0x128).LoadDefaultPreset());
            worldObjects.Add(new ToggleableFan(0x20, 0x140).LoadDefaultPreset());
            worldObjects.Add(new ToggleableFan(0x20, 0x138).LoadDefaultPreset());
            worldObjects.Add(new ToggleableFan(0x20, 0x130).LoadDefaultPreset());
            worldObjects.Add(new ToggleableFan(0x28, 0x1A0).LoadDefaultPreset());
            worldObjects.Add(new ToggleableFan(0x28, 0x1A8).LoadDefaultPreset());
            worldObjects.Add(new ToggleableFan(0x28, 0x1B0).LoadDefaultPreset());
            worldObjects.Add(new ToggleableFan(0x28, 0x198).LoadDefaultPreset());
            worldObjects.Add(new ToggleableFan(0x28, 0x1B8).LoadDefaultPreset());
            worldObjects.Add(new ToggleableFan(0x48, 0x298).LoadDefaultPreset());
            worldObjects.Add(new ToggleableFan(0x48, 0x2B0).LoadDefaultPreset());
            worldObjects.Add(new ToggleableFan(0x48, 0x2A8).LoadDefaultPreset());
            worldObjects.Add(new ToggleableFan(0x48, 0x2A0).LoadDefaultPreset());
            worldObjects.Add(new ToggleableFan(0x40, 0x238).LoadDefaultPreset());
            worldObjects.Add(new ToggleableFan(0x8, 0x240).LoadDefaultPreset());
            worldObjects.Add(new ToggleableFan(0x8, 0x248).LoadDefaultPreset());
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
            var enemies = HC_room1.ReturnEnemiesInRoom(AllEnemies);
            chainedOrbs.Add(new ChainedOrb(new EnemyShieldOrb(enemies[3]), new EnemyTurret(enemies[2])));
            enemies.RemoveAt(3); // remove ball
            enemies.RemoveAt(2); // remove turret

            enemies[0] = new EnemyTurret(enemies[0]);
            enemies[1] = new EnemyTurret(enemies[1]);

            layout = new RoomLayout(enemies);
            // first platform, right section
            var turretInfo = new TurretSpawnInfo();
            turretInfo.HorizontalAngle = 30;
            turretInfo.HorizontalSpeed = 40;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-122820, -35520, -11329), new Angle(0.22f, 0.98f)).Mask(SpawnPlane.Mask_Turret).SetSpawnInfo(turretInfo));

            // first platform, left far crate
            turretInfo = new TurretSpawnInfo();
            turretInfo.HorizontalAngle = 0;
            turretInfo.HorizontalSpeed = 0;
            turretInfo.VerticalAngle = -15;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121322, -34635, -11024), new Angle(-1.00f, 0.06f)).Mask(SpawnPlane.Mask_Turret).SetSpawnInfo(turretInfo));

            // first platform, top right white crate
            turretInfo = new TurretSpawnInfo();
            turretInfo.HorizontalAngle = 30;
            turretInfo.HorizontalSpeed = 40;
            turretInfo.VerticalAngle = -30;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-122738, -34740, -10731), new Angle(-0.15f, 0.99f)).Mask(SpawnPlane.Mask_Turret).SetSpawnInfo(turretInfo));

            // right cargo-elevator, aiming towards door
            turretInfo = new TurretSpawnInfo();
            turretInfo.HorizontalAngle = 30;
            turretInfo.HorizontalSpeed = 40;
            turretInfo.VerticalAngle = 0;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-122301, -33967, -11017), new QuaternionAngle(-0.30f, -0.64f, -0.30f, -0.64f)).Mask(SpawnPlane.Mask_Turret).SetSpawnInfo(turretInfo));

            // near door, left crate
            turretInfo = new TurretSpawnInfo();
            turretInfo.HorizontalAngle = 0;
            turretInfo.HorizontalSpeed = 0;
            turretInfo.VerticalAngle = -20;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121266, -30599, -11314), new Angle(-1.00f, 0.01f)).Mask(SpawnPlane.Mask_Turret).SetSpawnInfo(turretInfo));

            // 2nd platform, aiming toward laser path
            turretInfo = new TurretSpawnInfo();
            turretInfo.HorizontalAngle = 40;
            turretInfo.HorizontalSpeed = 30;
            turretInfo.VerticalAngle = 0;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-122613, -32775, -11921), new Angle(1.00f, 0.00f)).Mask(SpawnPlane.Mask_Turret).SetSpawnInfo(turretInfo));

            // 2nd platform, default
            turretInfo = new TurretSpawnInfo();
            turretInfo.HorizontalAngle = 30;
            turretInfo.HorizontalSpeed = 40;
            turretInfo.VerticalAngle = 10;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121086, -33065, -11914), new Angle(0.91f, 0.41f)).Mask(SpawnPlane.Mask_Turret).SetSpawnInfo(turretInfo));

            // right-cargo elevator, aiming toward platform
            turretInfo = new TurretSpawnInfo();
            turretInfo.HorizontalAngle = 0;
            turretInfo.HorizontalSpeed = 0;
            turretInfo.VerticalAngle = 40;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-122807, -33383, -11113), new QuaternionAngle(-0.68f, 0.18f, 0.18f, 0.68f)).Mask(SpawnPlane.Mask_Turret).SetSpawnInfo(turretInfo));

            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-123116, -31226, -11007), new Angle(-0.35f, 0.94f)).Mask(SpawnPlane.Mask_Airborne)); // near door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-123639, -33731, -11763), new Angle(-0.56f, 0.83f)).Mask(SpawnPlane.Mask_Airborne)); // laser path
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-122038, -33909, -11613), new Angle(0.68f, 0.73f)).Mask(SpawnPlane.Mask_Airborne)); // middle
            // addtional (flatground)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-122715, -32443, -11921), new Vector3f(-121335, -31754, -11921), new Angle(-0.72f, 0.69f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter()); // 2nd platform
            // additional (highground enemies)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121981, -30441, -11204), new Angle(-0.71f, 0.71f)).Mask(SpawnPlane.Mask_Highground)); // door light
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-120976, -33180, -11625), new Angle(0.94f, 0.34f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // floor lamp
            Rooms.Add(layout);


            //// Room 2 ////
            enemies = HC_room2.ReturnEnemiesInRoom(AllEnemies);
            enemies[0].SetEnemyType(Enemy.EnemyTypes.Splitter);
            RandomPickEnemiesWithoutCP(ref enemies);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121710, -26091, -12624), new Vector3f(-122246, -27054, -12628), new Angle(-0.71f, 0.71f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter()); // default
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-122341, -28859, -12262), new Vector3f(-121652, -28842, -12266), new Angle(-0.77f, 0.64f)).Mask(SpawnPlane.Mask_Highground)); // slope
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-122819, -30120, -11454), new Angle(0.37f, 0.93f)).Mask(SpawnPlane.Mask_Highground)); // wall piece on right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121082, -26209, -11638), new Angle(-0.84f, 0.54f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // billboard left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-122888, -26191, -11638), new Angle(-0.57f, 0.82f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // billboard right
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121297, -26532, -12096), new Vector3f(-122712, -27195, -11881), new Angle(-0.66f, 0.75f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turrets)
            //  - 
            Rooms.Add(layout);



            //// Room 3 ////
            enemies = HC_room3.ReturnEnemiesInRoom(AllEnemies);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Waver);
            RandomPickEnemiesWithoutCP(ref enemies);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121500, -22647, -13328), new Vector3f(-120943, -20913, -13328), new Angle(-0.72f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-122300, -22759, -13331), new Vector3f(-123051, -21018, -13324), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-122200, -20978, -13328), new Vector3f(-121728, -19603, -13328), new Angle(-0.72f, 0.70f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            // high/special
            // -
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121170, -22592, -12956), new Vector3f(-122836, -23245, -12506), new Angle(-0.67f, 0.74f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121962, -21126, -12475), new Angle(-0.71f, 0.70f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121615, -22618, -13328), new Angle(0.91f, 0.42f)).Mask(SpawnPlane.Mask_Turret).SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, VerticalAngle = 10}));
            Rooms.Add(layout);



            //// Room 4 ////
            enemies = HC_room4.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[1] = new EnemyShieldOrb(enemies[1]);
            RandomPickEnemiesWithoutCP(ref enemies, enemyIndex: 4); // pistol
            // orbs
            layout = new RoomLayout(enemies.Take(2).ToList());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-118426, -19200, -14786), new Vector3f(-117506, -20982, -15207))); // floating around default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-114166, -20996, -15154), new Vector3f(-113341, -22107, -14256))); // floating around default, back
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-115602, -22228, -14843), new Vector3f(-116643, -21380, -14293))); // floating left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-116312, -18772, -13572))); // crate stack
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-115111, -20827, -14885), new Vector3f(-114408, -20827, -14012))); // left cargo-elevator side
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-115029, -18108, -13745))); // near door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-118999, -17632, -14550), new Vector3f(-117363, -17618, -14253)).AsVerticalPlane()); // right billboard/wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-117618, -21417, -13730))); // first billboard, left
            Rooms.Add(layout);
            // enemies
            layout = new RoomLayout(enemies.Skip(2).ToList());
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-119447, -18607, -14931), new Vector3f(-117391, -17666, -14931), new Angle(-0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-116126, -17672, -14528), new Vector3f(-115463, -18196, -14528), new Angle(-0.92f, 0.39f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-115836, -19438, -15031), new Vector3f(-113218, -20428, -15031), new Angle(1.00f, 0.04f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-116664, -22337, -15281), new Vector3f(-115964, -21465, -15281), new Angle(0.94f, 0.34f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            // special/high
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-118127, -20071, -14331), new Angle(-1.00f, 0.02f)).Mask(SpawnPlane.Mask_Highground)); // end slope
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-113720, -19188, -13991), new Angle(-0.97f, 0.23f)).Mask(SpawnPlane.Mask_Highground)); // crate stack
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-114952, -17689, -13772), new Angle(-0.98f, 0.22f)).Mask(SpawnPlane.Mask_Highground)); // billboard near exit
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-113514, -20208, -14236), new Angle(1.00f, 0.03f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-117212, -18102, -13974), new Angle(-0.95f, 0.30f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-114811, -18696, -13691), new Angle(0.90f, 0.43f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, VerticalAngle = -20})); // on crates aiming on exit

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-116719, -21346, -15281), new Angle(0.40f, 0.92f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 20, HorizontalSpeed = 30, VerticalAngle = 20 })); // left platform, aiming to middle platform
            Rooms.Add(layout);



            //// Room 5 ////
            enemies = HC_room5.ReturnEnemiesInRoom(AllEnemies);
            chainedOrbs.Add(new ChainedOrb(new EnemyShieldOrb(enemies[1]), new EnemyTurret(enemies[0]))); // to chainedOrbs list
            enemies[2] = new EnemyDrone(enemies[2]);
            enemies.RemoveRange(0, 2);
            RandomPickEnemiesWithoutCP(ref enemies, force: true); // drone, by force


            //// Room 6 ////
            enemies = HC_room6.ReturnEnemiesInRoom(AllEnemies);
            enemies[0].SetEnemyType(Enemy.EnemyTypes.Splitter);
            enemies[1].SetEnemyType(Enemy.EnemyTypes.Weeb);
            RandomPickEnemiesWithoutCP(ref enemies, force: true);
            RandomPickEnemiesWithoutCP(ref enemies, force: true);


            //// Room 7 ////
            // CP before room, on slide
            //CustomCheckPoints.Add(new GameObjects.CustomCP(mapType, new Vector3f(-124227, 16966, -18933), new Vector3f(-122169, 17164, -16726), new Vector3f(-122803, 15575, -16978), new Angle(0.71f, 0.71f)));

            enemies = HC_room7.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-124323, 19749, -17644), new Vector3f(-123269, 18847, -17648), new Angle(-0.73f, 0.69f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121972, 19830, -17647), new Vector3f(-122826, 18929, -17648), new Angle(-0.76f, 0.65f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-122193, 18187, -17051), new Angle(1.00f, 0.05f)).Mask(SpawnPlane.Mask_Highground)); // white crate
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121909, 19894, -16751), new Vector3f(-124243, 19899, -16751), new Angle(-0.61f, 0.79f))
                .Mask(SpawnPlane.Mask_Highground).AsVerticalPlane().SetMaxEnemies(2)); // concrete beam
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121680, 19527, -16914), new Angle(-0.98f, 0.22f)).Mask(SpawnPlane.Mask_Highground)); // exit light
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-124382, 18826, -17240), new Angle(-0.42f, 0.91f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-122052, 18661, -17351), new Angle(0.68f, 0.73f)).Mask(SpawnPlane.Mask_Turret).SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, VerticalAngle = -20}));
            Rooms.Add(layout);


            //// Room 8 ////
            enemies = HC_room8.ReturnEnemiesInRoom(AllEnemies);
            enemies[0].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[1].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Weeb);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-120104, 16246, -17653), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-120585, 12109, -17648), new Vector3f(-118747, 10885, -17647), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter().SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-113808, 11992, -17644), new Vector3f(-114798, 10469, -17644), new Angle(-1.00f, 0.01f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter().SetMaxEnemies(2));
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-120050, 10435, -17051), new Angle(0.66f, 0.75f)).Mask(SpawnPlane.Mask_Highground)); // crates
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121265, 13266, -16516), new Vector3f(-121278, 14195, -16516), new Angle(-0.01f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // left horizontal beam, near laser
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-117617, 10359, -16638), new Angle(0.94f, 0.35f)).Mask(SpawnPlane.Mask_Highground)); // laser pillar left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-117606, 11891, -16638), new Angle(-0.98f, 0.19f)).Mask(SpawnPlane.Mask_Highground)); // laser pillar right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-113128, 11214, -16347), new Angle(-1.00f, 0.02f)).Mask(SpawnPlane.Mask_Highground)); // billboard
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-119113, 12514, -16937), new Vector3f(-120590, 13997, -16937), new Angle(0.72f, 0.69f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-119323, 11512, -17019), new Vector3f(-115089, 10818, -16658), new Angle(1.00f, 0.03f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-118663, 10401, -17648), new Angle(0.15f, 0.99f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 20, VerticalAngle = 0}));
            Rooms.Add(layout);


            //// Room 9 ////
            enemies = HC_room9.ReturnEnemiesInRoom(AllEnemies);
            // add turret and shield to chained orb &  remove from list
            chainedOrbs.Add(new ChainedOrb(new EnemyShieldOrb(enemies[1]), new EnemyTurret(enemies[0])));
            enemies.RemoveRange(0, 2);
            RandomPickEnemiesWithoutCP(ref enemies, force: true); // pistol
            RandomPickEnemiesWithoutCP(ref enemies, force: true); // uzi


            //// Room 10 ////
            enemies = HC_room10.ReturnEnemiesInRoom(AllEnemies);
            enemies[0].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies[1].SetEnemyType(Enemy.EnemyTypes.Weeb);
            RandomPickEnemiesWithoutCP(ref enemies, force: true); // weeb 
            RandomPickEnemiesWithoutCP(ref enemies, force: true); // weeb 


            //// Room 11 ////
            DisableCP(new DeepPointer(0x04609420, 0x98, 0x30, 0x128, 0xA8, 0xF68, 0x248, 0x1D0)); // disable small platform
            enemies = HC_room11.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShifter(enemies[0]);
            enemies[1] = new EnemyShifter(enemies[1]);
            Shifters.AddRange(enemies.Take(2).ToList());
            RandomPickEnemiesWithoutCP(ref enemies, force: true, enemyIndex: 2);


            //// Room 12 ////
            enemies = HC_room12.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyTurret(enemies[0]);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Waver);
            RandomPickEnemiesWithoutCP(ref enemies, enemyIndexBesides: 0);

            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-103508, 5204, -15491), new Vector3f(-105382, 6391, -15491), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Flatground)
                .SetMaxEnemies(2).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-99478, 7884, -15489), new Vector3f(-100650, 6612, -15485), new Angle(-1.00f, 0.02f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-101035, 6601, -15489), new Vector3f(-101906, 5902, -15491), new Angle(1.00f, 0.02f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-100739, 6251, -15207), new Angle(1.00f, 0.01f)).Mask(SpawnPlane.Mask_Highground)); // scaffolding
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-101133, 5792, -15086), new Angle(0.99f, 0.15f)).Mask(SpawnPlane.Mask_Highground)); // concrete frame
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-103589, 5076, -15086), new Angle(0.24f, 0.97f)).Mask(SpawnPlane.Mask_Highground)); // concrete frame 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-104551, 6572, -15020), new Angle(-0.21f, 0.98f)).Mask(SpawnPlane.Mask_Highground)); // pipes
            // turret
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-102544, 6575, -15491), new Angle(-0.72f, 0.70f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 30, VerticalAngle = 10})); // default

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-101338, 7177, -15489), new Angle(-0.27f, 0.96f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 30, VerticalAngle = 10 })); // around corner

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-99611, 6426, -15489), new Angle(0.68f, 0.73f)).Mask(SpawnPlane.Mask_Turret)
                 .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, HorizontalSpeed = 0, VerticalAngle = 20 })); // in left dark corner

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-100620, 6061, -14885), new QuaternionAngle(-0.61f, -0.35f, 0.61f, 0.35f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 20, HorizontalSpeed = 20, VerticalAngle = 0 })); // on the wall, aiming to main path
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-99574, 8381, -14848), new Angle(-0.82f, 0.57f)).Mask(SpawnPlane.Mask_Airborne)); 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-101198, 6369, -14919), new Angle(-1.00f, 0.08f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);


            //// Room 13 ////
            enemies = HC_room13.ReturnEnemiesInRoom(AllEnemies);
            enemies[1].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Waver);
            RandomPickEnemiesWithoutCP(ref enemies);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-104874, 14325, -13885), new Vector3f(-104306, 15294, -13885), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Highground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-103457, 15275, -14371), new Vector3f(-102796, 14444, -14361), new Angle(-0.23f, 0.97f)).Mask(SpawnPlane.Mask_Highground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-102621, 11104, -14382), new Vector3f(-103896, 12637, -14382), new Angle(0.71f, 0.71f)).Mask(SpawnPlane.Mask_Highground).AllowSplitter());
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-103725, 15166, -13901), new Angle(-0.38f, 0.92f)).Mask(SpawnPlane.Mask_Highground)); // platform corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-104069, 11624, -14048), new Angle(0.52f, 0.85f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // ac unit
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-103316, 11539, -13420), new Angle(0.71f, 0.71f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turret)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-104168, 13344, -13885), new Angle(-0.50f, 0.86f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { VerticalAngle = -20, HorizontalAngle = 0 })); // crates corners, aiming towards 3'rd platform
            Rooms.Add(layout);


            //// Room 14 ////
            enemies = HC_room14.ReturnEnemiesInRoom(AllEnemies);  // lonely turret
            enemies[0] = new EnemyTurret(enemies[0]);
            RandomPickEnemiesWithoutCP(ref enemies, force: true);


            //// Room 15 ////
            enemies = HC_room15.ReturnEnemiesInRoom(AllEnemies);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Splitter);
            enemies[4].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[6].SetEnemyType(Enemy.EnemyTypes.Weeb);
            chainedOrbs.Add(new ChainedOrb(new EnemyShieldOrb(enemies[1]), new EnemyTurret(enemies[0])));
            enemies.RemoveRange(0, 2); // remove ball and attached turret
            RandomPickEnemiesWithoutCP(ref enemies);

            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-90700, 11059, -13168), new Vector3f(-91350, 11767, -13169), new Angle(0.39f, 0.92f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-87770, 10772, -13169), new Vector3f(-86885, 11305, -13169), new Angle(1.00f, 0.01f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-85666, 11423, -13169), new Vector3f(-85913, 10848, -13169), new Angle(-1.00f, 0.00f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-85231, 8061, -12871), new Vector3f(-84619, 9804, -12871), new Angle(1.00f, 0.04f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-85481, 11109, -12292), new Angle(1.00f, 0.03f)).Mask(SpawnPlane.Mask_Highground)); // platform edge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-87991, 10626, -12508), new Angle(0.98f, 0.19f)).Mask(SpawnPlane.Mask_Highground)); // billboard
            // additional (turret)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-84070, 8895, -9972), new QuaternionAngle(0.00f, 0.71f, 0.00f, 0.71f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { VerticalAngle = 45}));
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-85744, 11170, -11887), new Vector3f(-84715, 9048, -11887), new Angle(1.00f, 0.06f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-87813, 11038, -12626), new Angle(1.00f, 0.02f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);


            //// Room 16 ////
            enemies = HC_room16.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyTurret(enemies[0]);
            enemies[1] = new EnemyTurret(enemies[1]);
            enemies[2] = new EnemyShieldOrb(enemies[2]);
            RandomPickEnemiesWithoutCP(ref enemies, enemyIndex:4); // frogger
            RandomPickEnemiesWithoutCP(ref enemies, enemyIndex: 0); // turret
            // orb
            int orbIndex = enemies.IndexOf(enemies.Where(x => x is EnemyShieldOrb).First());
            layout = new RoomLayout(enemies[orbIndex]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-80051, 8999, -5712)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-78190, 8935, -4928)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-78959, 8129, -4454), new Vector3f(-76213, 9409, -4454)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-79306, 8848, -4132)));
            Rooms.Add(layout);
            enemies.RemoveAt(orbIndex);

            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-76209, 9275, -5749), new Vector3f(-78792, 8334, -5752), new Angle(1.00f, 0.00f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2));
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-76846, 7995, -5344), new Angle(0.95f, 0.32f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-76980, 9590, -5344), new Angle(-0.99f, 0.13f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-78532, 9620, -4770), new Angle(-0.99f, 0.10f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-75976, 9058, -5226), new Angle(-1.00f, 0.03f)).Mask(SpawnPlane.Mask_Highground)); // door gate
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-78995, 7557, -5481), new Angle(0.94f, 0.35f)).Mask(SpawnPlane.Mask_Highground)); // extruded wall piece, left
            // turrets
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-80228, 9563, -5281), new Angle(-0.43f, 0.90f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 35, HorizontalSpeed = 30, VerticalAngle = -10})); // 1st platform, default area

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-78770, 9589, -5752), new Angle(-0.51f, 0.86f)).Mask(SpawnPlane.Mask_Turret)
                   .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 35, HorizontalSpeed = 30, VerticalAngle = 0 })); // 2nd platform, aiming towards middle

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-80205, 7436, -5274), new Angle(0.32f, 0.95f)).Mask(SpawnPlane.Mask_Turret)
                 .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 35, HorizontalSpeed = 30, VerticalAngle = -10 })); // 1st platform, default area 2
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-76608, 9326, -5241), new Vector3f(-78688, 8486, -4955), new Angle(-1.00f, 0.01f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-79580, 7864, -4999), new Angle(0.94f, 0.33f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);


            //// Room 17 ////
            enemies = HC_room17.ReturnEnemiesInRoom(AllEnemies);
            enemies[0].SetEnemyType(Enemy.EnemyTypes.Splitter);
            enemies[1].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies.ForEach(e => e.DisableAttachedCP(GameHook.game));
            EnemiesWithoutCP.AddRange(enemies);


            //// Room 18 ////
            enemies = HC_room18.ReturnEnemiesInRoom(AllEnemies);
            enemies[1].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies.ForEach(e => e.DisableAttachedCP(GameHook.game));
            EnemiesWithoutCP.AddRange(enemies);

            
            //// Room 19 ////
            enemies = HC_room19.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Waver);
            // orb
            layout = new RoomLayout(enemies[0]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-68867, 325, -4526), new Vector3f(-67340, 321, -3984)).AsVerticalPlane()); // right billboard 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-64547, -169, -3899), new Vector3f(-64510, -1371, -4417)).AsVerticalPlane()); // default billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67721, -1192, -4734))); // floating in middle
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-66231, -444, -3721))); // grapple floating
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-66474, -517, -4904))); // middle billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-66233, 391, -4912), new Vector3f(-68788, 369, -4912))); // below right billboard
            Rooms.Add(layout);
            // enemies

            layout = new RoomLayout(enemies.Skip(1).ToList());
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-69628, -1574, -4451), new Vector3f(-68195, -925, -4451), new Angle(-1.00f, 0.01f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-64731, -847, -4452), new Vector3f(-66477, -1564, -4452), new Angle(1.00f, 0.00f)).Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-64553, 359, -4651), new Vector3f(-65913, -552, -4651), new Angle(-1.00f, 0.01f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67011, -108, -5161), new Vector3f(-67977, -709, -5161), new Angle(-1.00f, 0.03f)).Mask(SpawnPlane.Mask_Highground).AllowSplitter());
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-69784, -426, -4451), new Angle(-0.53f, 0.84f)).Mask(SpawnPlane.Mask_Highground)); // near umbrella
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67618, -1640, -4757), new Angle(0.91f, 0.42f)).Mask(SpawnPlane.Mask_Highground)); // concrete frame, middle
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67261, -762, -4431), new Angle(0.99f, 0.14f)).Mask(SpawnPlane.Mask_Highground)); // platform ledge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-64964, -1668, -3131), new Vector3f(-68399, -1634, -3131), new Angle(0.83f, 0.56f)).Mask(SpawnPlane.Mask_Highground)
                .SetMaxEnemies(3).AsVerticalPlane()); // left high beam (easy reachable using grapple)
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-64832, 145, -4024), new Vector3f(-66502, -1428, -3735), new Angle(1.00f, 0.04f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67757, -30, -4473), new Angle(-0.96f, 0.27f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67704, -1120, -4083), new Angle(1.00f, 0.02f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-65950, -597, -4651), new Angle(0.23f, 0.97f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 30, VerticalAngle = 10})); // last platform, aim to door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67728, -1136, -5161), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalSpeed = 0, VerticalAngle = 45})); // middle, aiming up
            Rooms.Add(layout);


            //// Room 20 ////
            enemies = HC_room20.ReturnEnemiesInRoom(AllEnemies);
            enemies.ForEach(e => e.DisableAttachedCP(GameHook.game));
            EnemiesWithoutCP.AddRange(enemies);


            //// EXTRA ////
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-115802, -4236, -13817), new Angle(-0.70f, 0.72f)).Mask(SpawnPlane.Mask_Airborne)); // before room 5, near door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-119088, -272, -14429), new Angle(-0.26f, 0.97f)).Mask(SpawnPlane.Mask_Highground)); // platform at room 5
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121455, -1096, -14431), new Vector3f(-120800, -162, -14431), new Angle(-0.73f, 0.69f)).Mask(SpawnPlane.Mask_Highground)); // room 5 default platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121513, 7785, -17883), new Vector3f(-120914, 6789, -17875), new Angle(-0.68f, 0.74f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter()); // room 6
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-123529, 14047, -17114), new Angle(-0.64f, 0.76f)).Mask(SpawnPlane.Mask_Airborne)); // before room 7, between billboards
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-120196, 19398, -17653), new Angle(-1.00f, 0.00f)).Mask(SpawnPlane.Mask_Flatground).AllowSplitter()); // after room 7, infront of cp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-108790, 2111, -14371), new Angle(0.98f, 0.22f)).Mask(SpawnPlane.Mask_Highground)); // shuriken/billboards room, exit ledge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-98223, 5329, -13786), new Angle(-0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground)); // end of hallway, room 10
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-98251, 4701, -14599), new Vector3f(-97511, 5816, -14593), new Angle(0.70f, 0.71f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2)); // room 11, exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-101231, 11076, -14372), new Angle(-0.06f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // small ramp, right before room 13
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-102201, 10869, -13714), new Angle(0.05f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // billboard, before room 13

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-108144, 13856, -14091), new Angle(0.39f, 0.92f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 30, VerticalAngle = 0})); // room 14, default but moving

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-108628, 15767, -14089), new Angle(0.18f, 0.98f)).Mask(SpawnPlane.Mask_Turret) 
                 .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 40, HorizontalSpeed = 45, VerticalAngle = 10 }));// room 14

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-104764, 17196, -13881), new QuaternionAngle(0.26f, 0.00f, 0.97f, 0.00f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 50, HorizontalSpeed = 40, VerticalAngle = 0})); // room 14, exit ramp  (Q:180 -45 0)

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-91066, 17216, -12528), new Angle(-1.00f, 0.04f)).Mask(SpawnPlane.Mask_Highground)); // ramp, before room 15
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-87730, 17350, -12505), new Vector3f(-87233, 15963, -12215), new Angle(1.00f, 0.02f)).Mask(SpawnPlane.Mask_Airborne)); // before room 15
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-75773, 8946, -5749), new Angle(-1.00f, 0.00f)).Mask(SpawnPlane.Mask_Highground)); // right after door room 16
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-68012, 11281, -5821), new Angle(-0.97f, 0.23f)).Mask(SpawnPlane.Mask_Highground)); // room 17, concrete frame
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-68212, 11543, -5931), new Angle(-0.97f, 0.23f)).Mask(SpawnPlane.Mask_Highground)); // room 17, concrete wall parts
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-68351, 8279, -6226), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Highground)); // room 17, end of platform, infront of cp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-68379, 10696, -6227), new Vector3f(-69849, 11094, -6226), new Angle(-1.00f, 0.08f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(3).AllowSplitter());  // default ground
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-68384, 4120, -4777), new Angle(0.70f, 0.72f)).Mask(SpawnPlane.Mask_Airborne)); // after room 17, between grapples
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-73667, 1431, -5099), new Angle(-0.05f, 1.00f)).Mask(SpawnPlane.Mask_Airborne)); // room 18
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-73562, 1176, -5625), new Vector3f(-72260, 1615, -5625), new Angle(0.00f, 1.00f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(3).AllowSplitter()); // room 18 - default platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-73807, -1405, -5251), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Highground)); // before room 19, ledge of fan
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-71445, -1494, -4875), new Angle(1.00f, 0.08f)).Mask(SpawnPlane.Mask_Highground)); // ramp before room 19, behind laser
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-60743, -340, -3093), new QuaternionAngle(0.00f, 1.00f, 0.00f, 0.00f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 40, VerticalAngle = -30}));// second fan, ceiling gang
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-61699, 366, 578), new QuaternionAngle(0.42f, -0.91f, 0.00f, 0.00f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 20, HorizontalSpeed = 30, VerticalAngle = 20})); // second fan, mid way ceiling gang
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-61728, -1508, 3115), new Angle(0.71f, 0.71f)).Mask(SpawnPlane.Mask_Turret) 
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalSpeed = 30, HorizontalAngle = 50, VerticalAngle = 10})); // second fan, top of fan patroling exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-52503, -269, 3048), new Vector3f(-54770, -543, 3658), new Angle(-1.00f, 0.00f)).Mask(SpawnPlane.Mask_Airborne)); // after 2nd fan
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-48892, -443, 3125), new Angle(-1.00f, 0.06f)).Mask(SpawnPlane.Mask_Highground)); // ramp ledge, after room 20
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-20882, -929, 1575), new Angle(0.98f, 0.19f)).Mask(SpawnPlane.Mask_Highground)); // level exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-21464, -842, 2578), new Angle(0.99f, 0.14f)).Mask(SpawnPlane.Mask_Airborne)); // last room, blocking shuriken target
            // hidden turrets
            // room 11 exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-97227, 5950, -14602), new Angle(-0.97f, 0.25f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 5, VerticalAngle = 5, VisibleLaserLength = 0}));

            // room 17 corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-68359, 11707, -5931), new Angle(-0.71f, 0.71f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 5, VerticalAngle = 5, VisibleLaserLength = 0 }));

            // room 19
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-63913, 936, -4641), new Angle(-0.26f, 0.97f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 5, VerticalAngle = 5, VisibleLaserLength = 0 }));

            // hallway with 2 weebs, hidden ceiling
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-98252, 2133, -13405), new QuaternionAngle(0.71f, 0.71f, 0, 0)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 5, VerticalAngle = 5, VisibleLaserLength = 0 }));

            Rooms.Add(layout);


            //// Shifters ////
            RemoveParentObjects(ref Shifters);
            layout = new RoomLayout(Shifters);
            // room 4
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-115890, -19110, -13091), new Angle(-0.98f, 0.18f))
                .SetSpawnInfo(new ShifterSpawnInfo { shiftPoints = new List<System.Tuple<Vector3f, Angle>>() {
                    new System.Tuple<Vector3f, Angle>(new Vector3f(-117307, -17767, -14931), new Angle(-0.93f, 0.37f)),
                    new System.Tuple<Vector3f, Angle>(new Vector3f(-116001, -20458, -15031), new Angle(1.00f, 0.01f)),
                    new System.Tuple<Vector3f, Angle>(new Vector3f(-115876, -22425, -15281), new Angle(0.90f, 0.44f)),
                }}));
            // room 9
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-111763, 6212, -16022), new Angle(0.87f, 0.50f))
                 .SetSpawnInfo(new ShifterSpawnInfo {
                     shiftPoints = new List<System.Tuple<Vector3f, Angle>>() {
                    new System.Tuple<Vector3f, Angle>(new Vector3f(-111671, 5674, -15706), new Angle(1.00f, 0.05f)),
                    new System.Tuple<Vector3f, Angle>(new Vector3f(-114483, 5034, -15761), new Angle(0.64f, 0.77f)),
                    new System.Tuple<Vector3f, Angle>(new Vector3f(-112176, 1892, -15849), new Angle(0.74f, 0.68f)),
                }}));
            // room 11
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-94720, 5650, -14903), new Angle(-1.00f, 0.06f))
                .SetSpawnInfo(new ShifterSpawnInfo {
                    shiftPoints = new List<System.Tuple<Vector3f, Angle>>() {
                    new System.Tuple<Vector3f, Angle>(new Vector3f(-94955, 9478, -14499), new Angle(-0.75f, 0.67f)),
                    new System.Tuple<Vector3f, Angle>(new Vector3f(-97064, 9761, -14302), new Angle(-0.17f, 0.98f)),
                    new System.Tuple<Vector3f, Angle>(new Vector3f(-94208, 8194, -14600), new Angle(-0.95f, 0.32f)),
                }}));
            // room 13
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-103943, 15494, -13901), new Angle(-0.44f, 0.90f))
                .SetSpawnInfo(new ShifterSpawnInfo {
                    shiftPoints = new List<System.Tuple<Vector3f, Angle>>() {
                    new System.Tuple<Vector3f, Angle>(new Vector3f(-103904, 14418, -14362), new Angle(-0.25f, 0.97f)),
                    new System.Tuple<Vector3f, Angle>(new Vector3f(-103304, 10861, -14382), new Angle(0.71f, 0.71f)),
                    new System.Tuple<Vector3f, Angle>(new Vector3f(-104747, 13389, -13305), new Angle(0.20f, 0.98f)),
                }}));
            // room 15
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-91596, 11581, -12991), new Angle(0.32f, 0.95f))
                .SetSpawnInfo(new ShifterSpawnInfo {
                    shiftPoints = new List<System.Tuple<Vector3f, Angle>>() {
                    new System.Tuple<Vector3f, Angle>(new Vector3f(-88068, 10629, -12508), new Angle(0.98f, 0.20f)),
                    new System.Tuple<Vector3f, Angle>(new Vector3f(-86000, 9202, -12141), new Angle(0.92f, 0.39f)),
                    new System.Tuple<Vector3f, Angle>(new Vector3f(-85505, 11608, -12291), new Angle(-0.95f, 0.32f)),
                }}));
            layout.DoNotReuse();
            Rooms.Add(layout);


            //// Chained Orbs////
            // room 2
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-123054, -25802, -12175)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-122468, -26193, -12624), new Angle(0.46f, 0.89f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, HorizontalSpeed = 0, VerticalAngle = 10 }));
            chainedOrbs_Rooms.Add(layout);

            // room 3
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121778, -19900, -13328)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-121651, -20578, -12586), new QuaternionAngle(-0.50f, 0.50f, 0.50f, 0.50f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 20, HorizontalSpeed = 30, VerticalAngle = 45 }));
            chainedOrbs_Rooms.Add(layout);

            // slide door
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-115852, -4590, -12562)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-115320, -4692, -14401), new Angle(0.87f, 0.49f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, HorizontalSpeed = 0, VerticalAngle = 10 }));
            chainedOrbs_Rooms.Add(layout);

            // room 6
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-122254, 14627, -17142)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-122642, 14314, -17142), new QuaternionAngle(-0.50f, -0.50f, 0.50f, 0.50f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, HorizontalSpeed = 0, VerticalAngle = 10 }));
            chainedOrbs_Rooms.Add(layout);

            // room 9
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-109444, 1570, -14584)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-112153, 869, -15253), new Angle(0.12f, 0.99f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 30, VerticalAngle = 20, Range = 2800}));
            chainedOrbs_Rooms.Add(layout);

            // room 15 
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-93226, 17458, -12972)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-91242, 16274, -12432), new QuaternionAngle(-0.71f, 0.00f, 0.00f, 0.71f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, HorizontalSpeed = 0, VerticalAngle = 20 }));
            chainedOrbs_Rooms.Add(layout);

        }

        public override void RandomizeEnemies(Process game) {
            base.RandomizeEnemies(game);

            //// chainedOrbs ////
            if(chainedOrbs_Rooms?.Count == 0 || chainedOrbs?.Count == 0) return;
            // create available indexes, for random room pick
            List<int> availableIndexes = new List<int>();
            for(int i = 0; i < chainedOrbs_Rooms.Count; i++) {
                chainedOrbs_Rooms[i].ClearRoomObjects();
                availableIndexes.Add(i);
            }

            // chainedOb loop
            for(int i = 0; i < chainedOrbs.Count; i++) {
                // pick random room
                int randomRoom = Config.GetInstance().r.Next(availableIndexes.Count);
                // swap enemies to random room
                List<Enemy> roomEnemies = new List<Enemy>();
                roomEnemies.Add(chainedOrbs[i].orb);
                roomEnemies.AddRange(chainedOrbs[i].attachedEnemies);
                chainedOrbs_Rooms[availableIndexes[randomRoom]].SwapEnemies(roomEnemies.ToArray());
                availableIndexes.RemoveAt(randomRoom);
            }
            // rng chained rooms
            chainedOrbs_Rooms.ForEach(x => x.RandomizeEnemies(GameHook.game));
        }

        public void Gen_Nightmare() {
            throw new System.NotImplementedException();
        }

        protected override void Gen_PerRoom() {}

    }

    public class ChainedOrb {
        public EnemyShieldOrb orb { get; private set; }
        public List<Enemy> attachedEnemies { get; private set; }

        public ChainedOrb(EnemyShieldOrb orb, params Enemy[] attachedEnemies) {
            this.orb = orb;
            this.attachedEnemies = attachedEnemies.ToList();
            this.attachedEnemies.ForEach(x => x.DisableAttachedCP(GameHook.game));
        }
    }
}
