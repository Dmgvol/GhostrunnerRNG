using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using System;
using System.Collections.Generic;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.Maps {
    public class Awakening : MapCore {

        #region Rooms
        private Room room1 = new Room(new Vector3f(-11808, -61173, 4482), new Vector3f(-17950, -64478, 2243)); // 1 pistol
        private Room room2 = new Room(new Vector3f(-17778, -60368, 3908), new Vector3f(-14404, -58115, 2150)); // sensory boost, 1 pistol
        private Room room3 = new Room(new Vector3f(-12619, -57704, 5101), new Vector3f(-7309, -51494, 2431)); // 3 pistol, before bridge
        private Room room4 = new Room(new Vector3f(5589, -60246, 720), new Vector3f(7202, -73089, 2402)); // 3 pistol, hallway
        private Room room5 = new Room(new Vector3f(4871, -76740, 2678), new Vector3f(-3510, -67504, -1166));  // 5 pistols, dome
        #endregion

        private bool IsNewGame = false;

        public Awakening(bool isHC) : base(MapType.Awakening) {
            CheckNewGame();
            // classic level
            if(!isHC) {
                Gen_PerRoom();
            }
        }

        private void CheckNewGame() {
            DeepPointer bestTimeDP = new DeepPointer(0x04328548, 0xE0, 0xB0, 0x30, 0xF0, 0x8);
            IntPtr bestTimePtr;
            bestTimeDP.DerefOffsets(GameHook.game, out bestTimePtr);
            float time;
            GameHook.game.ReadValue<float>(bestTimePtr, out time);
            IsNewGame = time < 1;
        }

        protected override void Gen_PerRoom() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game, 0, 13); // static range, no gap
            Rooms = new List<RoomLayout>();
            RoomLayout layout;
            List<Enemy> enemies;

            //// room 1 - pistol
            enemies = room1.ReturnEnemiesInRoom(AllEnemies);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, removeCP: false); // take pistol, force, no cp/door attached


            //// room 2 - pistol, sensory boost
            if(!IsNewGame) { // skip rng if new game
                enemies = room2.ReturnEnemiesInRoom(AllEnemies);
                RandomPickEnemiesWithoutCP(ref enemies); // chance to move enemy to EnemiesWithoutCP
                if(enemies != null && enemies.Count > 0) {
                    layout = new RoomLayout(enemies); // minimal rng
                    layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-14496, -59107, 2803), new Vector3f(-15633, -60168, 2799), new Angle(-1.00f, 0.09f)));
                    layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-15826, -58998, 2889), new Angle(-0.99f, 0.17f)));
                    Rooms.Add(layout);
                }
            }

            //// room 3 - 3 pistol, before bridge
            enemies = room3.ReturnEnemiesInRoom(AllEnemies);
            TakeLastEnemyFromCP(ref enemies, force: false, attachedDoor: true, removeCP: true);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8179, -54300, 3408), new Vector3f(-9091, -52688, 3402), new Angle(-0.9f, 0.43f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8309, -55792, 3408), new Vector3f(-10221, -56789, 3408), new Angle(1.0f, 0.01f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9798, -53058, 3402), new Vector3f(-10775, -54270, 3408), new Angle(-0.96f, 0.28f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-12762, -55235, 4398), new Angle(-0.98f, 0.22f)));
            // special/high spots
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8122, -54648, 3911), new Angle(-0.99f, 0.17f))); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-10040, -55579, 4030), new Angle(-0.99f, 0.15f))); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-7336, -57117, 3408), new Angle(1.00f, 0.08f))); // SR route
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9023, -57121, 4195), new Angle(0.99f, 0.15f))); // wall lamp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9582, -57001, 3788), new Angle(0.97f, 0.26f))); // small platform
            Rooms.Add(layout);


            //// Room 4 - 3 pistol, hallway
            enemies = room4.ReturnEnemiesInRoom(AllEnemies);
            TakeLastEnemyFromCP(ref enemies, force: false, attachedDoor: true, removeCP: true);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(5740, -63956, 1468), new Vector3f(6337, -64859, 1468), new Angle(0.72f, 0.7f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(5771, -66227, 1678), new Vector3f(6538, -67229, 1678), new Angle(0.73f, 0.68f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(5656, -68518, 1868), new Vector3f(6591, -69532, 1868), new Angle(0.73f, 0.68f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6648, -70563, 1798), new Vector3f(5848, -71930, 1798), new Angle(0.69f, 0.73f)));
            // special/high spots
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(5699, -61602, 2065), new Angle(0.83f, 0.56f))); // first small wall/building
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6779, -69590, 2587), new Angle(0.83f, 0.56f))); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6404, -64078, 2241), new Angle(0.73f, 0.68f))); // pipe
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6676, -72123, 2088), new Vector3f(5889, -72129, 2088), new Angle(0.69f, 0.72f)).AsVerticalPlane()); // slide pipe
            Rooms.Add(layout);


            //// Room 5 - 5 enemies, dome
            enemies = room5.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(4603, -70321, -346), new Vector3f(3303, -68684, -350), new Angle(-0.71f, 0.71f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(5128, -71555, 307), new Vector3f(3678, -71883, 307), new Angle(-0.71f, 0.71f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(997, -70468, -41), new Vector3f(1689, -68394, -41)).RandomAngle());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(910, -72873, -160), new Vector3f(1698, -72192, -167), new Angle(-0.45f, 0.89f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(955, -76167, 362), new Vector3f(1578, -75331, 371), new Angle(0f, 1.00f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2142, -75881, -42), new Vector3f(-1468, -73341, -42), new Angle(0.12f, 0.99f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-280, -68970, -41), new Vector3f(-1275, -68147, -41), new Angle(-0.34f, 0.94f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2226, -69843, -81), new Vector3f(-1017, -70568, -81), new Angle(-0.24f, 0.97f)));

            // high/special spots
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(3256, -68387, 237), new Angle(-0.61f, 0.79f))); // neon sign platform, far bottom
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(3373, -72039, 924), new Angle(-0.64f, 0.77f))); // street light
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(1943, -75088, 1008), new Angle(-0.26f, 0.97f))); // big pipe, right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1100, -74848, 340)).RandomAngle()); // small platform, right bot
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2033, -72982, 605), new Angle(-0.18f, 0.98f))); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1339, -71031, 605), new Angle(-0.37f, 0.93f))); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2744, -71368, 473), new Angle(0.00f, 1.00f))); // fuel tank
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(891, -70099, 584), new Angle(-0.59f, 0.80f))); // street light

            Rooms.Add(layout);


            //// EXTRA spots
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-14200, -63865, 2798), new Vector3f(-14996, -62722, 2798)).RandomAngle().setRarity(0.2)); // pistol 1, default pos
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-14729, -63919, 3591), new Angle(0.03f, 1.0f))); // pistol 1, wall lamp above 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-18060, -64136, 3598), new Vector3f(-19392, -63758, 3601), new Angle(0f, 1f))); // vent area
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-21681, -63576, 2998), new Angle(-1.00f, 0.05f))); // after first vent, left corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-20723, -59412, 4298), new Vector3f(-21563, -60163, 4302), new Angle(-0.99f, 0.15f))); // before sensory boost vent
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3991, -70323, -80), new Angle(0.01f, 1.00f))); // before elevator (surprise)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-16471, -56114, 4098), new Angle(-0.78f, 0.62f))); // after sensory boost
            Rooms.Add(layout);
        }
    }
}
