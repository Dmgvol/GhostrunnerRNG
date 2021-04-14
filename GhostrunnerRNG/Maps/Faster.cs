using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.NonPlaceableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Faster() : base(GameUtils.MapType.Faster, manualGen: true) {
            if(GameHook.IsHC) return;

            Gen_PerRoom();
           
        }

        protected override void Gen_PerRoom() {
            //indexes ?
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game, 0, 6);
            AllEnemies.AddRange(GetAllEnemies(GameHook.game, 8, 11));
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
            ModifyCP(new DeepPointer(0x04609420, 0x98, 0x18, 0x128, 0xA8, 0x1760, 0x240, 0x398, 0x150), new Vector3f(0, 0, 0), GameHook.game);
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
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11792, 178094, 1243), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_HighgroundLimited));//above door 2
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
            ModifyCP(new DeepPointer(0x04609420, 0x98, 0x18, 0x128, 0xA8, 0x1720, 0x240, 0x398, 0x150), new Vector3f(0, 0, 0), GameHook.game);
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
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11265, 198217, 318), new Vector3f(-12308, 197556, 318), new Angle(-0.70f, 0.72f)).Mask(SpawnPlane.Mask_Flatground));//1 weeb
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

            #region Billboards
            Billboard board1 = new Billboard(0x0, 0x320);
            board1.AddSpawnInfo(new BillboardSpawnInfo { Time1 = 3, Angle1 = 270, Time2 = 1, Angle2 = 90 });
            board1.AddSpawnInfo(new BillboardSpawnInfo { Time1 = 3, Angle1 = -45, Time2 = 1, Angle2 = 45 });
            board1.AddSpawnInfo(new BillboardSpawnInfo { Time1 = 2, Angle1 = 970, Time2 = 1, Angle2 = 110 });
            board1.AddSpawnInfo(new BillboardSpawnInfo { Time1 = 2.25f, Angle1 = -1140, Time2 = 1, Angle2 = 60 });
            board1.AddSpawnInfo(new BillboardSpawnInfo { Time1 = 1, Angle1 = 0, Time2 = 1, Angle2 = 0 }.SetRarity(0.05));
            board1.AddSpawnInfo(new BillboardSpawnInfo { Time1 = 500, Angle1 = 200000, Time2 = 1, Angle2 = 90 }.SetRarity(0.05));
            board1.AddSpawnInfo(new BillboardSpawnInfo());
            nonPlaceableObjects.Add(board1);

            Billboard board2 = new Billboard(0x0, 0x138);
            board2.AddSpawnInfo(new BillboardSpawnInfo { Time1 = 4, Angle1 = -40, Time2 = 0.5f, Angle2 = 40 });
            board2.AddSpawnInfo(new BillboardSpawnInfo { Time1 = 4, Angle1 = 320, Time2 = 0.5f, Angle2 = 40 });
            board2.AddSpawnInfo(new BillboardSpawnInfo { Time1 = 1, Angle1 = 700, Time2 = 0.5f, Angle2 = 20 });
            board2.AddSpawnInfo(new BillboardSpawnInfo { Time1 = 1.25f, Angle1 = -1160, Time2 = 0.5f, Angle2 = 80 });
            board2.AddSpawnInfo(new BillboardSpawnInfo { Time1 = 0.25f, Angle1 = 60, Time2 = 500, Angle2 = 0 });
            board2.AddSpawnInfo(new BillboardSpawnInfo { Time1 = 500, Angle1 = 200000, Time2 = 1, Angle2 = 90 }.SetRarity(0.05));
            board2.AddSpawnInfo(new BillboardSpawnInfo());
            nonPlaceableObjects.Add(board2);
            #endregion

            #region Jump
            NonPlaceableObject uplink = new UplinkJump(0x0, 0xFD8);

            var jumpSpawn = new UplinkJumpSpawnInfo { TimeToActivate = 3, JumpMultiplier = 7 };
            uplink.AddSpawnInfo(jumpSpawn);

            jumpSpawn = new UplinkJumpSpawnInfo { TimeToActivate = Config.GetInstance().r.Next(3, 51) / 10, JumpMultiplier = 6, JumpForwardMultiplier = 1, JumpGravity = 15 };
            uplink.AddSpawnInfo(jumpSpawn);

            jumpSpawn = new UplinkJumpSpawnInfo { TimeToActivate = Config.GetInstance().r.Next(3, 6) / 10, JumpMultiplier = 2, JumpForwardMultiplier = 8 };
            uplink.AddSpawnInfo(jumpSpawn);

            jumpSpawn = new UplinkJumpSpawnInfo();
            jumpSpawn.TimeToActivate = Config.GetInstance().r.Next(3, 51) / 10;
            jumpSpawn.JumpMultiplier = Config.GetInstance().r.Next(1, 11);
            jumpSpawn.JumpForwardMultiplier = Config.GetInstance().r.Next(0, 16) - 8;
            jumpSpawn.JumpGravity = Config.GetInstance().r.Next(2, 13);
            jumpSpawn.SetRarity(0.1);
            uplink.AddSpawnInfo(jumpSpawn);

            //default
            uplink.AddSpawnInfo(new UplinkJumpSpawnInfo());

            nonPlaceableObjects.Add(uplink);
            #endregion

            #region shuriken1
            uplink = new UplinkShurikens(0x8, 0x498);

            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 50, MaxAttacks = 3 });
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 2, MaxAttacks = 10 });
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = Config.GetInstance().r.Next(1, 11), MaxAttacks = Config.GetInstance().r.Next(1, 21) });
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 3600, MaxAttacks = 3600 }.SetRarity(0.05));
            //default
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo());
            nonPlaceableObjects.Add(uplink);
            #endregion

            #region Targets
            nonPlaceableObjects.Add(new ShurikenTarget(0x10, 0xc98).AddSpawnInfo(new ShurikenTargetSpawnInfo { HitsNeeded = Config.GetInstance().r.Next(1, 4) }));
            nonPlaceableObjects.Add(new ShurikenTarget(0x10, 0xd00).AddSpawnInfo(new ShurikenTargetSpawnInfo { HitsNeeded = Config.GetInstance().r.Next(1, 4) }));
            nonPlaceableObjects.Add(new ShurikenTarget(0x10, 0xcf8).AddSpawnInfo(new ShurikenTargetSpawnInfo { HitsNeeded = Config.GetInstance().r.Next(1, 4) }));
            nonPlaceableObjects.Add(new ShurikenTarget(0x10, 0xcf0).AddSpawnInfo(new ShurikenTargetSpawnInfo { HitsNeeded = Config.GetInstance().r.Next(1, 4) }));
            nonPlaceableObjects.Add(new ShurikenTarget(0x10, 0xce8).AddSpawnInfo(new ShurikenTargetSpawnInfo { HitsNeeded = Config.GetInstance().r.Next(1, 4) }));
            nonPlaceableObjects.Add(new ShurikenTarget(0x10, 0xce0).AddSpawnInfo(new ShurikenTargetSpawnInfo { HitsNeeded = Config.GetInstance().r.Next(1, 4) }));
            #endregion

            #region Signs1
            ChainedSignSpawners chained = new ChainedSignSpawners(0x3c0, 0x3c8, 0x3b8);
            //mixed
            chained.AddChainedInfo(new List<SignSpawnerSpawnInfo>() {
                new SignSpawnerSpawnInfo { DelayOnStart = 1, SpawnDelay = 3},
                new SignSpawnerSpawnInfo { DelayOnStart = 2, SpawnDelay = 3},
                new SignSpawnerSpawnInfo { DelayOnStart = 0, SpawnDelay = 3},
            });

            chained.AddChainedInfo(new List<SignSpawnerSpawnInfo>() {
                new SignSpawnerSpawnInfo { DelayOnStart = 2, SpawnDelay = 3},
                new SignSpawnerSpawnInfo { DelayOnStart = 0, SpawnDelay = 3},
                new SignSpawnerSpawnInfo { DelayOnStart = 1, SpawnDelay = 3},
            });

            //some double sign))
            chained.AddChainedInfo(new List<SignSpawnerSpawnInfo>() {
                new SignSpawnerSpawnInfo { DelayOnStart = 0, SpawnDelay = 3},
                new SignSpawnerSpawnInfo { DelayOnStart = 2, SpawnDelay = 2},
                new SignSpawnerSpawnInfo { DelayOnStart = 1, SpawnDelay = 3},
            });

            chained.AddChainedInfo(new List<SignSpawnerSpawnInfo>() {
                new SignSpawnerSpawnInfo { DelayOnStart = 0, SpawnDelay = 3},
                new SignSpawnerSpawnInfo { DelayOnStart = 0, SpawnDelay = 2},
                new SignSpawnerSpawnInfo { DelayOnStart = 2, SpawnDelay = 3},
            });

            //should be rare
            chained.AddChainedInfo(new List<SignSpawnerSpawnInfo>() {
                new SignSpawnerSpawnInfo { DelayOnStart = 0, SpawnDelay = 2},
                new SignSpawnerSpawnInfo { DelayOnStart = 0, SpawnDelay = 1},
                new SignSpawnerSpawnInfo { DelayOnStart = 3, SpawnDelay = 4},
            });

            //default
            chained.AddChainedInfo(new List<SignSpawnerSpawnInfo>() {
                new SignSpawnerSpawnInfo { DelayOnStart = 0, SpawnDelay = 3},
                new SignSpawnerSpawnInfo { DelayOnStart = 1, SpawnDelay = 3},
                new SignSpawnerSpawnInfo { DelayOnStart = 2, SpawnDelay = 3},
            });

            nonPlaceableObjects.Add(chained);
            #endregion

            #region Signs2
            chained = new ChainedSignSpawners(0x3b0, 0x3a8, 0x3a0, 0x398, 0x390, 0x388, 0x3D0);
            //default
            var delays = new List<float>() { 0.0f, 1.1f, 2.2f, 3.3f, 4.0f, 4.8f, 5.8f };
            var result = delays.Select(x => new { value = x, order = Config.GetInstance().r.Next() }).OrderBy(x => x.order).Select(x => x.value).ToList();
            chained.AddChainedInfo(new List<SignSpawnerSpawnInfo>() {
                new SignSpawnerSpawnInfo { DelayOnStart = result[0], SpawnDelay = 6.8f},
                new SignSpawnerSpawnInfo { DelayOnStart = result[1], SpawnDelay = 6.8f},
                new SignSpawnerSpawnInfo { DelayOnStart = result[2], SpawnDelay = 6.8f},
                new SignSpawnerSpawnInfo { DelayOnStart = result[3], SpawnDelay = 6.8f},
                new SignSpawnerSpawnInfo { DelayOnStart = result[4], SpawnDelay = 6.8f},
                new SignSpawnerSpawnInfo { DelayOnStart = result[5], SpawnDelay = 6.8f},
                new SignSpawnerSpawnInfo { DelayOnStart = result[6], SpawnDelay = 6.8f},
            });
            nonPlaceableObjects.Add(chained);
            #endregion

            #region ForceSlideTrigger
            if(Config.GetInstance().Setting_RemoveForceSlideTrigger) {
                IntPtr triggerPtr;
                DeepPointer triggerDP = new DeepPointer(0x04609420, 0x98, 0x10, 0x128, 0xA8, 0x3C0, 0x230, 0x398, 0x150);
                triggerDP.DerefOffsets(GameHook.game, out triggerPtr);
                GameHook.game.WriteBytes(triggerPtr, BitConverter.GetBytes((float)0));
            }
            #endregion


            #region Signs Triggers
            // force signs
            // Default: -11830, 160550, 2040 | 650, 1025, 540
            nonPlaceableObjects.Add(new Trigger(0x18, 0x380, new Vector3f(-11815, 139661, 350), new Vector3f(650, 1025, 540), // first wagon doorframe
                new DeepPointer(0x04609420, 0x1F8, 0x60, 0xD0, 0x298, 0x830, 0xB0, 0x5A0, 0x1A8, 0x0), 0x19000,
                "9A F1 42 C6 B0 C6 1B 48 33 D3 BA 44 66 BE 2E C6 50 CC 1D 48 66 96 21 45"));
            #endregion
        }
    }
}
