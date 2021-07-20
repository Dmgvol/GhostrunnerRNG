using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.GameObjects;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.MemoryUtils;
using GhostrunnerRNG.NonPlaceableObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.Maps {
    class ReignInHell : MapCore, IModesMidCV {

        #region Rooms
        // Before CV
        private Room room_1 = new Room(new Vector3f(-5910, 5931, 6901), new Vector3f(-10867, -1042, 2946));         // 2 spiders
        private Room room_2 = new Room(new Vector3f(-12207, 16820, 6743), new Vector3f(-6557, 19964, 3126));        // 4 spiders
        private Room room_3 = new Room(new Vector3f(-6853, 21748, 7930), new Vector3f(-1712, 11176, 4349));         // 2 spiders, 1 pistol
        private Room room_4 = new Room(new Vector3f(-6514, 25529, 9050), new Vector3f(3633, 42656, 4899));          // 8 spiders
        private Room room_5 = new Room(new Vector3f(5905, 36378, 9792), new Vector3f(14285, 46485, 6302));          // 4 spider+2pistol+2weeb
        private Room room_6_section1 = new Room(new Vector3f(16362, 42890, 9616), new Vector3f(23160, 35034, 5123));// 11 spiders, (2 sections)
        private Room room_6_section2 = new Room(new Vector3f(15679, 33796, 9155), new Vector3f(25563, 26058, 6008));

        // After CV
        private Room room_7 = new Room(new Vector3f(11681, 26561, 12483), new Vector3f(31432, 9054, 3603));     // 6 spider+ 3pistol+shield+weeb
        private Room room_8 = new Room(new Vector3f(32932, 9501, 12994), new Vector3f(13703, -3415, 6303));     // 2 spiders+ball+2weeb+shiled+2pistol+uzi +(shuriken+slowmo)
        private Room room_9 = new Room(new Vector3f(21662, -3316, 10454), new Vector3f(14930, -10758, 6923));   // 1 shifter
        private Room room_10 = new Room(new Vector3f(12990, -13990, 11378), new Vector3f(27936, -21298, 5840)); // 2 shifters+pistol+weeb+shield (jump)
        private Room room_11 = new Room(new Vector3f(30657, -30009, 10444), new Vector3f(38263, -41578, 7312)); // 2 shifter+weeb+frogger

        #endregion

        #region HC Rooms
        private Room room_HC_1 = new Room(new Vector3f(-9145, -12318, 1956), new Vector3f(-14681, -6095, 3821)); // shifter, weeb, pistol
        private Room room_HC_2 = new Room(new Vector3f(-7773, -4777, -99), new Vector3f(-1571, -2710, 1429)); // 2 wavers
        private Room room_HC_3 = new Room(new Vector3f(3569, -11628, -280), new Vector3f(-993, -4229, 2001)); // orb + 2 weebs
        private Room room_HC_4 = new Room(new Vector3f(-1839, 2337, 840), new Vector3f(2225, 805, 2095)); // lonely drone
        private Room room_HC_5 = new Room(new Vector3f(-5352, 3396, 3571), new Vector3f(-11095, -748, 6084)); // waver, 2 spiders
        private Room room_HC_6 = new Room(new Vector3f(-8556, 11290, 4012), new Vector3f(-11307, 19868, 5965));
        private Room room_HC_7 = new Room(new Vector3f(-5734, 16051, 4135), new Vector3f(-1782, 23757, 7574)); // shifter, spider
        private Room room_HC_8 = new Room(new Vector3f(-7533, 28812, 5733), new Vector3f(3889, 44110, 9768)); // 4 spiders, frogger, waver
        private Room room_HC_9 = new Room(new Vector3f(5510, 46946, 5670), new Vector3f(14060, 36256, 10314)); // uzi, 2 spider, 2 weeb, pistol, orb, shielder
        private Room room_HC_10 = new Room(new Vector3f(21130, 39445, 6917), new Vector3f(17063, 36853, 8125)); // waver, uzi
        private Room room_HC_11 = new Room(new Vector3f(20233, 33767, 6506), new Vector3f(25890, 29208, 8477)); // waver, 2 spiders
        private Room room_HC_12 = new Room(new Vector3f(33114, 24712, 7206), new Vector3f(24953, 22268, 8667)); // 2 drones
        private Room room_HC_13 = new Room(new Vector3f(25310, 19347, 7082), new Vector3f(17346, 10274, 9775)); // shifter, weeb, frogger, turret, waver
        private Room room_HC_14 = new Room(new Vector3f(31811, 7392, 7203), new Vector3f(18613, -2931, 10686)); // waver, turret, 2 weeb, uzi
        private Room room_HC_15 = new Room(new Vector3f(22225, -3511, 7453), new Vector3f(15518, -11483, 10304)); // shifter, 2 froggers
        private Room room_HC_16 = new Room(new Vector3f(16408, -14291, 7024), new Vector3f(24256, -20869, 10043)); // 2 shifters, waver, uzi, frogger,shielder
        private Room room_HC_17 = new Room(new Vector3f(36435, -29638, 7747), new Vector3f(31745, -41000, 9533)); // 2 shifters, uzi, 2 frogger, weeb
        #endregion

        public ReignInHell() : base(MapType.ReignInHell, BeforeCV: GameHook.yPos < 20000) {
            ModifyCP(new DeepPointer(PtrDB.DP_ReignInHell_ElevatorCP), new Vector3f(-4295, -11605, 2455), GameHook.game);

            #region DoorTrigger
            IntPtr triggerPtr;
            DeepPointer triggerDP = new DeepPointer(PtrDB.DP_RiH_DoorTrigger);
            triggerDP.DerefOffsets(GameHook.game, out triggerPtr);
            GameHook.game.WriteBytes(triggerPtr, BitConverter.GetBytes((float)0));
            #endregion

        }
        public void Gen_Normal_BeforeCV() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game, 0, 36);
            Rooms = new List<RoomLayout>();
            RoomLayout layout;
            List<Enemy> enemies;

            // Note: no room for rng for Room 1 and 2 (spiders)

            // room 3 - hallhway with 4 spiders ////
            enemies = room_3.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies.Skip(2).ToList()); // pistol spawns
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-5528, 21681, 6808), new Angle(-0.91f, 0.42f))); // exit platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-7027, 22696, 6412), new Angle(-0.87f, 0.50f)).setDiff(1)); // big fan wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9617, 18570, 4511), new Angle(-0.71f, 0.71f))); // hallway with spiders
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-5516, 21183, 5008), new Vector3f(-4875, 20702, 5008), new Angle(-0.99f, 0.16f))); // default cp platform
            // outside the default room: (around the map)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(3350, 39405, 7698), new Angle(-0.99f, 0.14f))); // after room 4, on crate
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(25425, 26961, 7317), new Angle(0.80f, 0.59f))); // before cv door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(18050, 31902, 7908), new Angle(0.74f, 0.68f))); // last room, small platform with ziplines
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(15088, 40512, 7398), new Angle(-1.00f, 0.01f))); // after door room 5
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(27825, 26910, 7308), new Angle(1.00f, 0.00f))); // before cv, right after door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(2317, -6549, 416), new Angle(0.97f, 0.23f)));  // stuck door, button area
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(1493, -9727, 409), new Angle(0.52f, 0.85f))); // stuck door, near button 
            Rooms.Add(layout);

            layout = new RoomLayout(enemies.Take(2).ToList()); // spider rng
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4720, 14969, 5308), new Vector3f(-5435, 15562, 5307), new Angle(0.73f, 0.69f))); // platform with pistol
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3806, 20964, 4999), new Angle(-1.00f, 0.03f))); // cp platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3510, 19588, 5967), new Angle(1.00f, 0.03f))); // billboard
            Rooms.Add(layout);


            //// room 4 - 8 spiders ////
            enemies = room_4.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies.Take(5).ToList());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6328, 28768, 7718), new Vector3f(-5805, 28977, 7718), new Angle(-0.02f, 1.00f)).SetMaxEnemies(2)); // default beam spawn
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4066, 33980, 6402), new Vector3f(-4045, 31588, 6402), new Angle(-0.70f, 0.71f))); // before first platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-5157, 32497, 7319), new Vector3f(-6026, 33246, 7319), new Angle(0.01f, 1.00f)).SetMaxEnemies(3)); // default spawn
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4805, 33940, 7280), new Vector3f(-3793, 33763, 7280), new Angle(-0.71f, 0.71f)).AsVerticalPlane().SetMaxEnemies(2)); // beam near default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4863, 35277, 6825), new Vector3f(-4002, 36142, 6825), new Angle(-0.69f, 0.72f))); // default platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4503, 39396, 7321), new Vector3f(-4283, 40175, 7321), new Angle(-0.71f, 0.71f))); // narrow spawn
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3892, 26911, 6440), new Angle(-0.76f, 0.65f))); // below first platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4973, 38861, 6825), new Angle(-0.01f, 1.00f))); // first turn corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6197, 36319, 7857), new Angle(0.09f, 1.00f))); // on wall parts
            Rooms.Add(layout);


            //// room 5 - 4 spider+2pistol+2weeb ////
            enemies = room_5.ReturnEnemiesInRoom(AllEnemies);
            enemies[6].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies[7].SetEnemyType(Enemy.EnemyTypes.Weeb);
            // spiders
            layout = new RoomLayout(enemies.Take(4).ToList());
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6425, 43175, 7287), new Angle(-0.61f, 0.80f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(7065, 39128, 7287), new Angle(0.73f, 0.68f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(7997, 44859, 7287), new Angle(-0.72f, 0.69f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(10073, 43019, 7287), new Angle(1.00f, 0.01f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(13313, 43330, 7287), new Angle(-1.00f, 0.08f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(13308, 39610, 7287), new Angle(0.72f, 0.69f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(11165, 38761, 7287), new Angle(0.99f, 0.11f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(11634, 40171, 8009), new Angle(-0.69f, 0.73f))); // middle zipline wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(13928, 40507, 7941), new Angle(0.71f, 0.71f))); // above exit door
            Rooms.Add(layout);

            // 2 pistols, 2 weebs
            layout = new RoomLayout(enemies.Skip(4).ToList());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(7852, 43086, 7404), new Vector3f(6627, 41163, 7404), new Angle(-1.00f, 0.02f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(7099, 40411, 7398), new Angle(0.73f, 0.68f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6908, 38394, 7398), new Angle(0.41f, 0.91f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8088, 44008, 7404), new Angle(-0.72f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8989, 43400, 7398), new Vector3f(11630, 43023, 7398), new Angle(1.00f, 0.02f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(13526, 40313, 7398), new Vector3f(12948, 41151, 7398), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(13263, 38939, 7398), new Vector3f(10617, 38331, 7398), new Angle(-1.00f, 0.01f)).Mask(SpawnPlane.Mask_Flatground));
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(7151, 40729, 8239), new Angle(0.98f, 0.22f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // left rooftop 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8336, 41078, 7698), new Angle(0.95f, 0.31f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // crates
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(7795, 43937, 8236), new Angle(-0.62f, 0.79f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // ac unit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(12231, 45511, 7158), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // pipes extending to wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(13545, 43759, 8471), new Angle(-1.00f, 0.09f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // wall lamp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(12159, 42831, 8501), new Angle(1.00f, 0.07f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // wall lamp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(13530, 37731, 8406), new Angle(0.89f, 0.46f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // white pipes left from exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(12347, 38719, 9212), new Angle(-1.00f, 0.02f)).setRarity(0.2).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // high billboard, near roof
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(9321, 37252, 8547), new Angle(0.70f, 0.71f)).setRarity(0.2).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1));// hovering pipes, high
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(10696, 40411, 8383), new Angle(0.67f, 0.75f)).Mask(SpawnPlane.Mask_HighgroundLimited).setDiff(1)); // middle zipline, wall fan
            Rooms.Add(layout);

            #region Spiders
            //// room 6 - 11 spiders ////
            //enemies = room_6_section1.ReturnEnemiesInRoom(AllEnemies);
            //layout = new RoomLayout(enemies);
            //// default platforms
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(20665, 40117, 7268), new Vector3f(19914, 40993, 7268), new Angle(-1.00f, 0.03f)));
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19815, 38376, 7268), new Vector3f(20732, 37602, 7268), new Angle(0.71f, 0.71f)));
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(17886, 38408, 7268), new Vector3f(17309, 37233, 7268), new Angle(-0.01f, 1.00f)));
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(17339, 35851, 7268), new Vector3f(18040, 36373, 7268), new Angle(0.71f, 0.70f)));

            //// default spawners (wall narrow openings)
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(16039, 38031, 7882), new Vector3f(16904, 37544, 7882), new Angle(-0.02f, 1.00f)).SetMaxEnemies(2));
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22720, 41565, 7859), new Vector3f(21866, 41063, 7859), new Angle(-1.00f, 0.00f)).SetMaxEnemies(2));
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(16120, 38014, 7859), new Vector3f(16933, 37602, 7859), new Angle(-0.02f, 1.00f)).SetMaxEnemies(2));
            //// special
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(17236, 41451, 7754), new Angle(-0.69f, 0.72f))); // near entry door
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19656, 39006, 7910), new Angle(0.70f, 0.71f))); // wall, infront of billboard
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19035, 36433, 7797), new Angle(-0.01f, 1.00f))); // behind reactor wall
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(20272, 38118, 6986), new Angle(0.64f, 0.77f))); // under platform 3
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(17703, 37510, 6986), new Angle(0.09f, 1.00f))); // under platform 2
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19978, 40896, 6986), new Angle(-1.00f, 0.04f))); // under platform 1

            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-761, 38665, 6615), new Angle(1.00f, 0.02f))); // edge platform, near zipline
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-2732, 39366, 6866), new Angle(-0.74f, 0.67f))); // last platform, around corner

            //Rooms.Add(layout);

            //// room 6 - section 2
            //enemies = room_6_section2.ReturnEnemiesInRoom(AllEnemies);
            //layout = new RoomLayout(enemies);
            //// default platforms
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(21616, 30543, 7203), new Vector3f(22446, 31541, 7203), new Angle(1.00f, 0.01f)));
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22506, 32731, 7203), new Vector3f(22914, 32141, 7203), new Angle(-0.74f, 0.67f)));
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(25081, 31441, 7203), new Vector3f(23257, 30588, 7203), new Angle(-1.00f, 0.01f)));

            //// default spaners(wall narrow openings)
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(26291, 31287, 7882), new Vector3f(25523, 30804, 7882), new Angle(-1.00f, 0.01f)).SetMaxEnemies(2));
            //// special
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(23629, 31559, 7877), new Angle(-1.00f, 0.03f))); // wall near skull decal
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(23812, 30312, 7839), new Angle(1.00f, 0.01f))); // lab tube
            //Rooms.Add(layout);
            #endregion

            #region Jump
            //first jump
            NonPlaceableObject uplink = new UplinkJump(0x28, 0x608);//4.5,3.5,6
            uplink.AddSpawnInfo(new UplinkJumpSpawnInfo().SetRarity(0.5));//default
            var jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 3.5f, JumpForwardMultiplier = 2.75f };
            uplink.AddSpawnInfo(jumpSpawn);//really short jump need to use dash
            jumpSpawn = new UplinkJumpSpawnInfo { JumpForwardMultiplier = -4.0f };
            uplink.AddSpawnInfo(jumpSpawn);//normal backward jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpGravity = 3.0f };
            uplink.AddSpawnInfo(jumpSpawn);//high jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 9.0f, JumpForwardMultiplier = 15.0f, JumpGravity = 3.0f };
            uplink.AddSpawnInfo(jumpSpawn);//bounce
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = -1.0f, JumpForwardMultiplier = -3.0f, JumpGravity = -3.0f }.SetRarity(0.15);
            uplink.AddSpawnInfo(jumpSpawn);//bounce
            worldObjects.Add(uplink);
            #endregion

            #region Shurikens
            //// First room with spiders
            uplink = new UplinkShurikens(0x0, 0xd68);//5,20
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 60, MaxAttacks = 100 }.SetRarity(0.05)); // for the spiders
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = Config.GetInstance().r.Next(6, 11), MaxAttacks = 6 }); // for 3 argets max 2 hits
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 6, MaxAttacks = Config.GetInstance().r.Next(6, 11) }); // basic
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 6, MaxAttacks = 20 }); // default
            worldObjects.Add(uplink);
            #endregion

            #region Targets
            //collectible
            var collectibleTarget = new ShurikenTarget(0x0, 0x230).AddSpawnInfo(new ShurikenTargetSpawnInfo { HitsNeeded = Config.GetInstance().r.Next(1, 4) });
            collectibleTarget.OverwritePointerOffset("HitsNeeded", 0x220);
            worldObjects.Add(collectibleTarget);

            //for the fan
            worldObjects.Add(new ShurikenTarget(0x0, 0xD70).AddSpawnInfo(new ShurikenTargetSpawnInfo { HitsNeeded = Config.GetInstance().r.Next(1, 3) }));
            worldObjects.Add(new ShurikenTarget(0x0, 0xD60).AddSpawnInfo(new ShurikenTargetSpawnInfo { HitsNeeded = Config.GetInstance().r.Next(1, 3) }));
            worldObjects.Add(new ShurikenTarget(0x0, 0xD58).AddSpawnInfo(new ShurikenTargetSpawnInfo { HitsNeeded = Config.GetInstance().r.Next(1, 3) }));
            #endregion
        }

        public void Gen_Normal_AfterCV() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game, 0, 30);
            Rooms = new List<RoomLayout>();
            RoomLayout layout;
            List<Enemy> enemies;

            // room 7 - 6 spider + 3 pistol + 1 shield + 1 weeb ////
            enemies = room_7.ReturnEnemiesInRoom(AllEnemies);
            enemies[10].SetEnemyType(Enemy.EnemyTypes.Weeb);
            // Spider spawns
            layout = new RoomLayout(enemies.Take(6).ToList());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22002, 18129, 7898), new Angle(-0.71f, 0.71f))); // small corner, left, near shielder
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(17978, 11220, 8394), new Angle(0.01f, 1.00f))); // near shurikens
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24768, 11242, 8354), new Angle(0.72f, 0.69f))); // exit door

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(25535, 15818, 7760), new Angle(0.78f, 0.62f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(23835, 16835, 7567), new Angle(0.40f, 0.91f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24408, 15578, 7697), new Angle(0.97f, 0.23f)));

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(21804, 15644, 7689), new Angle(0.44f, 0.90f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22206, 13888, 7382), new Vector3f(22216, 15290, 7382), new Angle(0.71f, 0.71f)).AsVerticalPlane().SetMaxEnemies(2)); // concrete pillar connecting platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(20785, 12537, 7827), new Vector3f(19576, 12553, 7827), new Angle(0.56f, 0.83f))); // concrete horizontal pillar
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(21120, 10672, 8842), new Vector3f(21496, 9577, 8842), new Angle(-0.70f, 0.71f)).SetMaxEnemies(3)); // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19584, 16882, 7802), new Angle(0.70f, 0.71f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19593, 17829, 7802), new Angle(0.71f, 0.71f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19195, 16020, 8127), new Angle(0.66f, 0.75f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(18372, 12041, 9173), new Angle(0.45f, 0.90f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(17800, 12842, 8388), new Angle(0.22f, 0.97f)));
            Rooms.Add(layout);

            //Pistol, Shield and weeb spawners
            layout = new RoomLayout(enemies.Skip(6).ToList());
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(25014, 18019, 8000), new Vector3f(24136, 15898, 8018), new Angle(0.70f, 0.72f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(21939, 17616, 7999), new Vector3f(19836, 16822, 7999), new Angle(0.02f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(18362, 11126, 8499), new Vector3f(19170, 12451, 8499), new Angle(0.72f, 0.69f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(25086, 12515, 8501), new Vector3f(24161, 13363, 8499), new Angle(0.73f, 0.68f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(25054, 11030, 8499), new Vector3f(24404, 11323, 8499), new Angle(0.72f, 0.70f)).Mask(SpawnPlane.Mask_Flatground)); // near door
            // special/high places
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(23740, 16535, 8648), new Angle(0.56f, 0.83f)).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24752, 17405, 8790), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // small rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(26217, 19079, 8383), new Angle(1.00f, 0.00f)).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // generator
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22450, 18110, 8008), new Angle(-0.01f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // small ledge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22030, 18360, 7999), new Angle(-0.74f, 0.67f)).Mask(SpawnPlane.Mask_Flatground)); // around the corner, near shielder
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22181, 15829, 8548), new Angle(0.58f, 0.81f)).Mask(SpawnPlane.Mask_Highground)); // on zipline pole
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(18076, 12914, 8857), new Angle(0.28f, 0.96f)).Mask(SpawnPlane.Mask_Highground)); // small ad near slowmo
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(18503, 12185, 9278), new Angle(0.67f, 0.74f)).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // small rooftop near slomo
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(21573, 14999, 8710), new Angle(0.45f, 0.89f)).Mask(SpawnPlane.Mask_Highground).setRarity(0.2).setDiff(1)); // building rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(21466, 16125, 9054), new Angle(0.22f, 0.98f)).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // building rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24744, 20905, 8001), new Angle(0.70f, 0.71f)).setRarity(0.1).Mask(SpawnPlane.Mask_Highground)); // infront of cp spawn 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24714, 10767, 9400), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // neon sign, above exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(25384, 12636, 8857), new Angle(0.81f, 0.58f)).Mask(SpawnPlane.Mask_Highground)); // small ad sign, near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24187, 12241, 9501), new Angle(0.62f, 0.79f)).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // billboard edge, near exit
            Rooms.Add(layout);


            //// room 8 - 2 spiders+ball+2weeb+shielder+2pistol+uzi ////
            enemies = room_8.ReturnEnemiesInRoom(AllEnemies);

            var orb = new EnemyShieldOrb(enemies[0]);
            orb.HideBeam_Range(0, 3);
            orb.HideBeam_Range(1, 3);
            orb.HideBeam_Range(2, 3);
            orb.LinkObject(3);

            enemies[0] = orb;
            enemies[7].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies[8].SetEnemyType(Enemy.EnemyTypes.Weeb);

            // spiders
            layout = new RoomLayout(enemies.Skip(1).Take(2).ToList());
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(25551, 291, 8389), new Angle(-0.92f, 0.38f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(29833, 6584, 8258), new Angle(-0.80f, 0.60f)));
            // additional spawns
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(25677, 6132, 8401), new Angle(0.38f, 0.93f))); // secret room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(23569, 7012, 8312), new Angle(0.63f, 0.77f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19887, -55, 8382), new Angle(0.12f, 0.99f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(20868, -2734, 9193), new Angle(0.18f, 0.98f))); // near vent
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24146, 3310, 8215), new Angle(-0.18f, 0.98f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(25508, 4518, 8403), new Angle(1.00f, 0.01f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(26456, -1117, 8487), new Angle(-1.00f, 0.04f)));
            Rooms.Add(layout);

            // orb
            layout = new RoomLayout(enemies[0]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24340, -2648, 8667), new Vector3f(25224, -163, 8667), new Angle(0.70f, 0.72f))); // default volume
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(23674, -384, 9279), new Vector3f(21917, 166, 8478), new Angle(-1.00f, 0.03f))); // hallway, double billboards
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19903, 572, 9112), new Angle(0.64f, 0.77f))); // above crates, near shielder
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19368, -2608, 9461), new Angle(0.47f, 0.88f))); // near vent/slomo
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(25499, 1175, 9026), new Vector3f(25514, 2966, 8584), new Angle(0.73f, 0.68f)).AsVerticalPlane()); // middle hallway, billboard right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(23890, 1100, 9059), new Vector3f(23895, 2952, 8506), new Angle(0.70f, 0.72f)).AsVerticalPlane()); // middle hallway, billboard, left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(26114, 4563, 9046), new Angle(-0.92f, 0.39f))); // small rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24797, 6091, 9447), new Angle(0.81f, 0.59f)).setRarity(0.2).setDiff(1)); // hovering cables/beam 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(30533, 4240, 8623), new Angle(1.00f, 0.01f))); // weeb platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(31679, 678, 9074), new Vector3f(31615, 2042, 8482), new Angle(0.72f, 0.69f))); // billboard, far right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(29840, -2359, 9155), new Vector3f(28554, -2274, 8539), new Angle(1.00f, 0.03f))); // billboard 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19330, 1465, 9221), new Angle(0.70f, 0.72f))); // near fuel tank
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(27811, 3195, 9747), new Angle(0.93f, 0.36f)).setRarity(0.2).setDiff(1)); // high pipes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(18877, -298, 9446), new Angle(0.02f, 1.00f)).setRarity(0.1).setDiff(1)); // wall lamp
            Rooms.Add(layout);

            // normal enemies
            layout = new RoomLayout(enemies.Skip(3).ToList());
            // default platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19830, 1937, 8499), new Vector3f(21056, 1194, 8500), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(21115, 1082, 8500), new Vector3f(20603, -269, 8499), new Angle(0.68f, 0.73f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24192, -2679, 8513), new Vector3f(25184, -279, 8499), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24954, 556, 8528), new Vector3f(24530, 5276, 8548), new Angle(0.70f, 0.71f)).SetMaxEnemies(2).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(30361, 4447, 8501), new Vector3f(27601, 4023, 8501), new Angle(0.99f, 0.17f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(29531, 6516, 8508), new Vector3f(27722, 7097, 8508), new Angle(1.00f, 0.01f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(20633, -2834, 9298), new Vector3f(19583, -2763, 9298), new Angle(0.68f, 0.74f)).Mask(SpawnPlane.Mask_Highground));
            // special/high spawns

            var mask = SpawnPlane.Mask_Highground;
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(20890, -1042, 9055), new Angle(0.76f, 0.65f)).Mask(mask)); // rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(20182, 852, 8858), new Angle(0.65f, 0.76f)).Mask(mask)); // crate
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(18978, 1427, 9495), new Angle(0.49f, 0.87f)).Mask(mask)); // fuel tank
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19690, 2229, 9088), new Angle(0.58f, 0.82f)).Mask(mask)); // zipline pole
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(21539, 4173, 9860), new Angle(-0.90f, 0.44f)).setRarity(0.2).Mask(mask).setDiff(1)); // high rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19006, 4428, 9496), new Angle(0.16f, 0.99f)).Mask(mask).setDiff(1)); // wall lamp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19004, 6347, 9496), new Angle(0.04f, 1.00f)).Mask(mask).setDiff(1)); // wall lamp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19106, 7221, 9962), new Angle(0.01f, 1.00f)).setRarity(0.1).setDiff(1).Mask(mask)); // wall fan
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22686, 6409, 8893), new Angle(0.21f, 0.98f)).Mask(mask).setDiff(1)); // wall pipe
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(26030, 6828, 9410), new Angle(1.00f, 0.05f)).Mask(mask).setDiff(1)); // rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(27645, 6739, 9932), new Angle(-1.00f, 0.08f)).Mask(mask).setDiff(1)); // neon sign
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(28725, 8047, 9784), new Angle(-0.72f, 0.69f)).Mask(mask).setDiff(1)); // big fuel tank
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(30047, 7320, 9544), new Angle(-0.95f, 0.32f)).Mask(mask).setDiff(1)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(32084, 4711, 9610), new Angle(-1.00f, 0.03f)).setRarity(0.3).Mask(mask).setDiff(1)); // far pipes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(31955, 2300, 9355), new Vector3f(31956, 828, 9355), new Angle(1.00f, 0.01f)).AsVerticalPlane().Mask(mask).setDiff(1)); // billboard infront of shuriken 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(26766, -1221, 9476), new Angle(-0.94f, 0.34f)).Mask(mask).setDiff(1)); // wall lamp, near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24162, -2652, 9295), new Angle(0.57f, 0.82f)).Mask(mask).setDiff(1)); // rooftop near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(23874, -1029, 9398), new Angle(0.49f, 0.87f)).Mask(mask).setDiff(1)); // wall piece near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22098, 358, 9184), new Vector3f(23647, 360, 9184), new Angle(-0.40f, 0.92f)).AsVerticalPlane().Mask(mask).setDiff(1)); // billboard top near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24741, 6221, 9447), new Angle(0.71f, 0.70f)).Mask(mask).setDiff(1)); // beam about entry
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(23944, 2053, 9388), new Angle(0.51f, 0.86f)).setRarity(0.1).Mask(mask).setDiff(1)); // beam, middle hallway
            Rooms.Add(layout);

            // room 9 - shifter ////
            enemies = room_9.ReturnEnemiesInRoom(AllEnemies);
            ShifterSpawnInfo info, info2;
            if(Config.GetInstance().Setting_Difficulty == Config.Difficulty.Normal) { // no room for easy rng, easy will get default
                EnemyShifter shifter = new EnemyShifter(enemies[0], 3);
                info = new ShifterSpawnInfo();
                info.shiftPoints = new List<Tuple<Vector3f, Angle>>() {
                    new Tuple<Vector3f, Angle>(new Vector3f(19686, -10296, 9191), new Angle(0.48f, 0.88f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(24945, -7467, 8500), new Angle(-1.00f, 0.02f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(20007, -4607, 8543), new Angle(-0.56f, 0.83f))
                };
                info2 = new ShifterSpawnInfo();
                info2.shiftPoints = new List<Tuple<Vector3f, Angle>>() {
                    new Tuple<Vector3f, Angle>(new Vector3f(18883, -10270, 9198), new Angle(0.51f, 0.86f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(17039, -6130, 8496), new Angle(-0.10f, 1.00f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(16910, -10369, 9155), new Angle(0.29f, 0.96f))
                };

                layout = new RoomLayout(shifter);
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(21105, -7502, 8503), new Angle(-0.01f, 1.00f)).SetSpawnInfo(Config.GetInstance().r.Next(0, 2) == 0 ? info : info2)); // default
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(20621, -8104, 8587), new Angle(0.23f, 0.97f)).SetSpawnInfo(Config.GetInstance().r.Next(0, 2) == 0 ? info : info2)); // platform corner
                layout.AddSpawnPlane(new SpawnPlane(new Vector3f(18389, -5446, 8496), new Angle(-0.28f, 0.96f)).SetSpawnInfo(Config.GetInstance().r.Next(0, 2) == 0 ? info : info2)); // other platform
                Rooms.Add(layout);
            }


            //// room 10 - 2 shifters+pistol+weeb+shield ////
            enemies = room_10.ReturnEnemiesInRoom(AllEnemies);

            enemies[4].SetEnemyType(Enemy.EnemyTypes.Weeb);
            int shifterR;
            if(Config.GetInstance().Setting_Difficulty == Config.Difficulty.Normal) { // no room for easy rng, easy will get default
                info = new ShifterSpawnInfo();
                info.shiftPoints = new List<Tuple<Vector3f, Angle>>() {
                    new Tuple<Vector3f, Angle>(new Vector3f(21914, -15151, 8496), new Angle(-1.00f, 0.02f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(18621, -20019, 8497), new Angle(0.73f, 0.68f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(21824, -20567, 8497), new Angle(0.86f, 0.51f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(18086, -14370, 9081), new Angle(-0.92f, 0.40f))
                };
                info2 = new ShifterSpawnInfo();
                info2.shiftPoints = new List<Tuple<Vector3f, Angle>>() {
                    new Tuple<Vector3f, Angle>(new Vector3f(20231, -16873, 8008), new Angle(0.95f, 0.31f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(18011, -20615, 9722), new Angle(0.56f, 0.83f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(17965, -20570, 8771), new Angle(0.35f, 0.94f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(16915, -14370, 9081), new Angle(-0.38f, 0.92f))
                };

                shifterR = Config.GetInstance().r.Next(2); // so each shifter gets a different spawnInfo and they won't collide
                enemies[1] = new EnemyShifter(enemies[1], 4).AddFixedSpawnInfo(shifterR == 0 ? info : info2); // doesn't matter which plane, shifter will use this spawninfo
                enemies[2] = new EnemyShifter(enemies[2], 4).AddFixedSpawnInfo(shifterR == 0 ? info2 : info);
            }
            layout = new RoomLayout(enemies);
            // default platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(17163, -14790, 8496), new Vector3f(17938, -16148, 8498), new Angle(0.72f, 0.69f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(21923, -15159, 8497), new Vector3f(23142, -14678, 8501), new Angle(1.00f, 0.01f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22677, -15339, 8501), new Vector3f(23485, -16079, 8501), new Angle(0.88f, 0.48f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22626, -19062, 8498), new Vector3f(23223, -19879, 8501), new Angle(0.73f, 0.68f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(23225, -19933, 8501), new Vector3f(21823, -20617, 8497), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(18899, -19866, 8498), new Vector3f(18396, -20681, 8498), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(18012, -20216, 8498), new Vector3f(17124, -19111, 8497), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));

            // special/high spawns
            // middle round platform (jump)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19860, -17171, 8008), new Angle(0.86f, 0.50f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(20737, -17118, 8008), new Angle(0.92f, 0.40f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(20804, -17940, 8008), new Angle(0.81f, 0.58f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19855, -18050, 8008), new Angle(0.89f, 0.46f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(17440, -20784, 9868), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // high place
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(16598, -20663, 9863), new Angle(0.57f, 0.82f)).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // pipes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24942, -16181, 8245), new Angle(0.99f, 0.14f)).setRarity(0.2).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // pipe of lab tube
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(20646, -14477, 7906), new Angle(-1.00f, 0.07f)).setRarity(0.2).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // pipe below right billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19580, -20689, 7998), new Angle(0.69f, 0.73f)).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // pipes below billboard, far back
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(17296, -20644, 9203), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // red neon lines
            Rooms.Add(layout);


            //// room 11 - 2 shifter+weeb+frogger ////
            enemies = room_11.ReturnEnemiesInRoom(AllEnemies);

            if(Config.GetInstance().Setting_Difficulty == Config.Difficulty.Normal) { // no room for easy rng, easy will get default
                info = new ShifterSpawnInfo();
                info.shiftPoints = new List<Tuple<Vector3f, Angle>>() {
                    new Tuple<Vector3f, Angle>(new Vector3f(35203, -31487, 8510), new Angle(1.00f, 0.03f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(32815, -35559, 8510), new Angle(0.69f, 0.72f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(35307, -40455, 8522), new Angle(0.71f, 0.70f))
                };
                info2 = new ShifterSpawnInfo();
                info2.shiftPoints = new List<Tuple<Vector3f, Angle>>() {
                    new Tuple<Vector3f, Angle>(new Vector3f(32971, -40167, 8510), new Angle(0.63f, 0.78f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(35615, -35426, 8518), new Angle(0.83f, 0.56f)),
                    new Tuple<Vector3f, Angle>(new Vector3f(32641, -34578, 8518), new Angle(0.60f, 0.80f))
                };

                shifterR = Config.GetInstance().r.Next(2);
                enemies[0] = new EnemyShifter(enemies[0], 3).AddFixedSpawnInfo(shifterR == 0 ? info : info2);
                enemies[1] = new EnemyShifter(enemies[1], 3).AddFixedSpawnInfo(shifterR == 0 ? info2 : info);

            }
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Weeb);

            layout = new RoomLayout(enemies);
            // default platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(33158, -31712, 8522), new Vector3f(32577, -35986, 8518), new Angle(0.71f, 0.71f)).SetMaxEnemies(2).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35066, -35548, 8518), new Vector3f(35569, -32090, 8518), new Angle(0.72f, 0.69f)).SetMaxEnemies(2).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(32682, -37943, 8522), new Vector3f(33148, -40829, 8518), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35478, -41093, 8517), new Vector3f(35010, -38585, 8518), new Angle(0.72f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));

            // special/high places
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(33336, -33327, 9243), new Angle(0.77f, 0.64f)).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // billboard edge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34823, -33340, 9243), new Angle(0.73f, 0.69f)).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // billboard edge 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34784, -34379, 9242), new Angle(-0.25f, 0.97f)).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // billboard edge 3
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(33339, -34365, 9243), new Angle(0.91f, 0.41f)).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // billboard edge 4
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34892, -41518, 9238), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Highground).setDiff(1)); // above exit door
            Rooms.Add(layout);

            #region Jump
            //4 room
            NonPlaceableObject uplink = new UplinkJump(0x8, 0x7a8);//3.6,2.8,6
            uplink.AddSpawnInfo(new UplinkJumpSpawnInfo().SetRarity(0.5));//default
            var jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 2.5f, JumpForwardMultiplier = 10.0f };
            uplink.AddSpawnInfo(jumpSpawn);//long jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpForwardMultiplier = -3.0f };
            uplink.AddSpawnInfo(jumpSpawn);//normal backward jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 1.0f, JumpForwardMultiplier = 2.0f, JumpGravity = 0.0f };
            uplink.AddSpawnInfo(jumpSpawn);//zero gravity jump
            jumpSpawn = new UplinkJumpSpawnInfo { JumpMultiplier = 25.0f, JumpForwardMultiplier = -1.0f, JumpGravity = -4.0f }.SetRarity(0.15);
            uplink.AddSpawnInfo(jumpSpawn);//ceiling slide
            worldObjects.Add(uplink);
            #endregion

            #region Shurikens
            // 2 room
            uplink = new UplinkShurikens(0x58, 0x1F8);//5,100
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 45, MaxAttacks = 50 }.SetRarity(0.05)); //for the next room
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = Config.GetInstance().r.Next(4, 11), MaxAttacks = Config.GetInstance().r.Next(7, 15) });
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 15, MaxAttacks = Config.GetInstance().r.Next(3, 7) });//unlucky rng
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo().SetRarity(0.5)); // default
            worldObjects.Add(uplink);
            #endregion

            #region Slomo
            //1 room
            uplink = new UplinkSlowmo(0x50, 0x1B0, 0xA0);
            uplink.AddSpawnInfo(new UplinkSlowmoSpawnInfo { TotalTime = 30 });
            uplink.AddSpawnInfo(new UplinkSlowmoSpawnInfo { TotalTime = Config.GetInstance().r.Next(5, 15) });
            uplink.AddSpawnInfo(new UplinkSlowmoSpawnInfo { TotalTime = 8, TimeMultiplier = 0.01f });
            //2 room
            uplink = new UplinkSlowmo(0x58, 0xA58, 0xA0);
            uplink.AddSpawnInfo(new UplinkSlowmoSpawnInfo { TotalTime = 20 });
            uplink.AddSpawnInfo(new UplinkSlowmoSpawnInfo { TotalTime = Config.GetInstance().r.Next(5, 15) });
            uplink.AddSpawnInfo(new UplinkSlowmoSpawnInfo { TotalTime = 5, TimeMultiplier = 0.01f });
            #endregion
        }

        public void Gen_Easy_BeforeCV() {Gen_Normal_BeforeCV();}

        public void Gen_Easy_AfterCV() {Gen_Normal_AfterCV();}

        public void Gen_Hardcore() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game, 0, 65);
            List<Enemy> Shifters = new List<Enemy>();
            Rooms = new List<RoomLayout>();
            RoomLayout layout;

            //// Room 1 ////
            var enemies = room_HC_1.ReturnEnemiesInRoom(AllEnemies);
            enemies[1] = new EnemyShifter(enemies[1]);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies.ToList().ForEach(x => {
                x.DisableAttachedCP(GameHook.game);
                if(x is EnemyShifter)
                    Shifters.Add(x);
                 else
                    EnemiesWithoutCP.Add(x);
            });

            

            //// Room 2 ////
            enemies = room_HC_2.ReturnEnemiesInRoom(AllEnemies); 
            enemies.ForEach(x => {
                x.SetEnemyType(Enemy.EnemyTypes.Waver);
                x.DisableAttachedCP(GameHook.game);
                EnemiesWithoutCP.Add(x);
            });


            //// Room 3 ////
            enemies = room_HC_3.ReturnEnemiesInRoom(AllEnemies); 
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[1].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Weeb);
            layout = new RoomLayout(enemies);
            // orb
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(2431, -4623, 421)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(3893, -8665, 520)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(2670, -10185, 708)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(1139, -10364, 808)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(1036, -8792, 708)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-324, -10323, 808)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(518, -6864, 370)).Mask(SpawnPlane.Mask_ShieldOrb));
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-84, -8523, 411), new Angle(-0.34f, 0.94f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(3041, -9899, 409), new Vector3f(1655, -9272, 409), new Angle(0.67f, 0.74f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-21, -10204, 409), new Angle(0.25f, 0.97f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(641, -6130, 407), new Angle(0.82f, 0.57f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(2550, -5381, 421), new Angle(-0.96f, 0.26f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(2560, -7843, 416), new Angle(0.75f, 0.66f)).Mask(SpawnPlane.Mask_Flatground));
            layout.DoNotReuse();
            Rooms.Add(layout);


            //// Room 4 ////
            enemies = room_HC_4.ReturnEnemiesInRoom(AllEnemies);
            EnemiesWithoutCP.Add(new EnemyDrone(enemies[0]));


            //// Room 5 ////
            enemies = room_HC_5.ReturnEnemiesInRoom(AllEnemies);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Waver);
            layout = new RoomLayout(enemies[0], enemies[1]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9328, 973, 4408), new Angle(0.47f, 0.88f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9499, 2401, 4401), new Angle(-0.51f, 0.86f)));

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-7547, 2125, 4123), new Angle(-0.03f, 1.00f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6769, 2128, 4123), new Angle(-0.03f, 1.00f)));

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-7707, 1086, 4156), new Angle(0.00f, 1.00f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6887, 1078, 4156), new Angle(0.01f, 1.00f)));

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4984, 647, 4542), new Angle(0.70f, 0.71f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-5014, 2653, 4542), new Angle(-0.69f, 0.72f)));
            layout.DoNotReuse();
            Rooms.Add(layout);

            layout = new RoomLayout(enemies[2]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9765, 1960, 4396), new Vector3f(-5982, 1266, 4411), new Angle(0.01f, 1.00f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(4));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8111, 2743, 4995), new Angle(-0.17f, 0.99f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8068, 537, 4998), new Angle(0.17f, 0.99f)).Mask(SpawnPlane.Mask_Highground));
            Rooms.Add(layout);


            //// Room 6 ////
            enemies = room_HC_6.ReturnEnemiesInRoom(AllEnemies); 
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Weeb);
            RandomPickEnemiesWithoutCP(ref enemies, force:true);
            layout = new RoomLayout(enemies);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-9974, 17643, 4511), new Vector3f(-9229, 12398, 4511), new Angle(-0.72f, 0.70f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(5));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8479, 14242, 5143), new Angle(-0.86f, 0.51f)).Mask(SpawnPlane.Mask_Highground)); // billboard left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-10685, 14233, 5143), new Angle(-0.57f, 0.82f)).Mask(SpawnPlane.Mask_Highground)); // billboard right
            Rooms.Add(layout);


            //// Room 7 ////
            enemies = room_HC_7.ReturnEnemiesInRoom(AllEnemies); 
            Shifters.Add(new EnemyShifter(enemies[1])); // no cp
            layout = new RoomLayout(enemies[0]); // single spider
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3044, 17983, 5225), new Angle(1.00f, 0.06f))); // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3581, 19091, 5517), new QuaternionAngle(-0.46f, -0.54f, 0.46f, 0.54f))); // orange billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4213, 17495, 4808), new QuaternionAngle(0.00f, 0.00f, 1.00f, 0.07f))); // middle beam
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3415, 20950, 6146), new QuaternionAngle(0.71f, -0.70f, 0.00f, 0.01f))); // hidding behind small billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4416, 21499, 5917), new QuaternionAngle(0.08f, -0.06f, -0.54f, 0.84f))); // small rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-5211, 20696, 4913), new QuaternionAngle(0.00f, 0.00f, -1.00f, 0.02f))); // platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3402, 19198, 4518), new QuaternionAngle(-0.50f, -0.49f, 0.50f, 0.51f))); // under orange billboard
            layout.DoNotReuse();
            Rooms.Add(layout);


            //// Room 8 ////
            enemies = room_HC_8.ReturnEnemiesInRoom(AllEnemies); 
            enemies[4].SetEnemyType(Enemy.EnemyTypes.Waver);
            layout = new RoomLayout(enemies.Take(4).ToList()); // spiders
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4541, 39700, 7350), new QuaternionAngle(0.00f, 0.00f, -0.71f, 0.71f))); // default, seg 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-5843, 32539, 7329), new QuaternionAngle(0.00f, 0.00f, 0.00f, 1.00f))); // default, seg 1
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4080, 31425, 6736), new QuaternionAngle(0.00f, 0.00f, -0.70f, 0.72f))); // first platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4071, 31417, 6521), new QuaternionAngle(0.09f, 1.00f, 0.00f, -0.01f))); // under platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3879, 33822, 7313), new QuaternionAngle(0.00f, 0.00f, -0.70f, 0.71f))); // beam
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4479, 35621, 6730), new QuaternionAngle(0.00f, 0.00f, -0.76f, 0.65f))); // 2nd platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3315, 36310, 7503), new QuaternionAngle(0.70f, 0.00f, 0.00f, 0.71f))); // green fence wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4048, 37023, 7297), new QuaternionAngle(0.27f, -0.65f, -0.27f, 0.66f))); // white billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4777, 38023, 7302), new QuaternionAngle(-0.51f, 0.39f, -0.51f, 0.57f))); // white billboard 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-1675, 38817, 6733), new QuaternionAngle(0.00f, 0.00f, 1.00f, 0.02f))); // near zipline
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3507, 38618, 6733), new QuaternionAngle(0.00f, 0.00f, -1.00f, 0.02f))); // 3rd platform
            layout.DoNotReuse();
            Rooms.Add(layout);
            enemies.Skip(4).ToList().ForEach(x => {
                x.DisableAttachedCP(GameHook.game);
                EnemiesWithoutCP.Add(x);
            });
            CustomCheckPoints.Add(new CustomCP(mapType, new Vector3f(1712, 38052, 6945), new Vector3f(3755, 42454, 8906),
                new Vector3f(2505, 40150, 7398), new Angle(0.50f, 0.87f)));


            //// Room 9 ////
            enemies = room_HC_9.ReturnEnemiesInRoom(AllEnemies); 
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[6].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies[7].SetEnemyType(Enemy.EnemyTypes.Weeb);
            RandomPickEnemiesWithoutCP(ref enemies, enemyIndex: 5, force:true);

            // shifter - ATTACHING TO CP
            List<Tuple<Vector3f, Angle>> ShifterPoints_HC_Room9 = new List<Tuple<Vector3f, Angle>>() {
                new Tuple<Vector3f, Angle>(new Vector3f(12250, 44612, 7158), new Angle(-0.73f, 0.69f)), // pipes
                new Tuple<Vector3f, Angle>(new Vector3f(13437, 38277, 7461), new Angle(0.81f, 0.58f)), // left far platform corner, trash
                new Tuple<Vector3f, Angle>(new Vector3f(12713, 40215, 7754), new Angle(0.56f, 0.83f)), // small billboard, 
                new Tuple<Vector3f, Angle>(new Vector3f(13551, 37638, 8396), new Angle(0.96f, 0.28f)), // pipes, left high
                new Tuple<Vector3f, Angle>(new Vector3f(7834, 43868, 8237), new Angle(-0.71f, 0.70f)), // ac unit
                new Tuple<Vector3f, Angle>(new Vector3f(13545, 43759, 8471), new Angle(-1.00f, 0.06f)), // wall lamp 1
                new Tuple<Vector3f, Angle>(new Vector3f(12159, 42831, 8501), new Angle(0.99f, 0.11f)), // wall lamp 2
                new Tuple<Vector3f, Angle>(new Vector3f(11629, 42354, 7230), new Angle(0.96f, 0.29f)), // billboard below platform level, middle, under zipline
                new Tuple<Vector3f, Angle>(new Vector3f(8302, 41409, 7548), new Angle(0.89f, 0.45f)), // boxes at start
            };
            AttachToGroup((Shifters[0] as EnemyShifter).AddFixedSpawnInfoList(ref ShifterPoints_HC_Room9), enemies[4]);
            enemies.Add(Shifters[0]); // that that new shifter to layout

            // orb
            layout = new RoomLayout(enemies[0]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(13285, 44951, 7639))); // default billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(10141, 44335, 7528))); // wall infront of default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(13498, 42855, 8189), new Vector3f(13491, 41309, 7565)).AsVerticalPlane()); // billboard right of exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(13327, 37416, 7640))); // left wall corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(10533, 41781, 7564))); // floating middle zipline
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(11985, 37198, 7343), new Vector3f(8089, 37175, 7654)).AsVerticalPlane()); // left whole wall range
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(10922, 39718, 7660))); // zipline left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(12840, 41515, 7983))); // wall near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(12226, 43423, 7112))); // UNDER platform, using pipes
            Rooms.Add(layout);

            // spiders
            layout = new RoomLayout(enemies[1], enemies[2]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(7426, 40342, 7751), new QuaternionAngle(-0.23f, -0.65f, 0.23f, 0.69f))); // left hallway, right wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6690, 39725, 7696), new QuaternionAngle(0.27f, 0.81f, 0.27f, 0.45f))); // left hallway, left wall 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(7095, 40037, 8111), new QuaternionAngle(-0.72f, -0.69f, 0.00f, 0.00f))); // left hallway, rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6079, 43148, 7775), new QuaternionAngle(-0.12f, 0.63f, -0.42f, 0.63f))); // gremling
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(9105, 43632, 7886), new QuaternionAngle(-0.02f, -0.71f, 0.71f, -0.02f))); // right, wall 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(13627, 42260, 8085), new QuaternionAngle(-0.49f, -0.50f, 0.49f, 0.51f))); // billboard near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(13607, 43669, 7896), new QuaternionAngle(-0.71f, -0.01f, 0.71f, 0.01f))); // right of billboard ^
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(13538, 38670, 8085), new QuaternionAngle(0.27f, -0.72f, -0.27f, 0.58f))); // red billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(11180, 38378, 7197), new QuaternionAngle(-0.35f, -0.92f, 0.16f, 0.01f))); // under left platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(7493, 38872, 7303), new QuaternionAngle(0.00f, 0.00f, 0.94f, 0.34f))); // around left corner

            layout.DoNotReuse();
            Rooms.Add(layout);

            // enemies
            layout = new RoomLayout(enemies.Skip(3).ToList());
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6801, 41138, 7404), new Vector3f(7858, 43307, 7404), new Angle(-1.00f, 0.02f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(7010, 39269, 7398), new Angle(0.72f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8373, 38558, 7398), new Angle(1.00f, 0.04f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(10171, 38852, 7398), new Vector3f(12672, 38299, 7398), new Angle(-1.00f, 0.01f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(13461, 40245, 7398), new Vector3f(12848, 39277, 7398), new Angle(-0.72f, 0.69f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(12872, 40460, 7398), new Vector3f(13417, 41091, 7398), new Angle(0.71f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(13484, 43462, 7398), new Vector3f(12430, 42965, 7398), new Angle(-1.00f, 0.01f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(11827, 43028, 7398), new Vector3f(8824, 43414, 7398), new Angle(-1.00f, 0.00f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8131, 44048, 7404), new Angle(-0.75f, 0.66f)).Mask(SpawnPlane.Mask_Flatground));
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8362, 41041, 7698), new Angle(0.99f, 0.13f)).Mask(SpawnPlane.Mask_Highground)); // red crate, first room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(13344, 44551, 8106), new Angle(-0.96f, 0.28f)).Mask(SpawnPlane.Mask_Highground)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(12216, 45423, 7158), new Angle(-0.71f, 0.70f)).Mask(SpawnPlane.Mask_Highground)); // pipes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(7151, 40729, 8239), new Angle(0.92f, 0.40f)).Mask(SpawnPlane.Mask_Highground)); // rooftop
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(12590, 37625, 7573), new Vector3f(7202, 37888, 7786), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(11522, 44249, 7744), new Angle(-0.69f, 0.72f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8429, 43189, 8146), new Angle(-0.95f, 0.32f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(13064, 43450, 8181), new Angle(-1.00f, 0.00f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(13510, 43596, 7398), new Angle(-0.91f, 0.42f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 40, HorizontalSpeed = 25, VerticalAngle = 5})); // right corner
            Rooms.Add(layout);



            //// Room 10 ////
            enemies = room_HC_10.ReturnEnemiesInRoom(AllEnemies); 
            enemies[1].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies.ForEach(x => EnemiesWithoutCP.Add(x)); // no cp required.


            //// Room 11 ////
            enemies = room_HC_11.ReturnEnemiesInRoom(AllEnemies); 
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Waver);
            // enemies
            layout = new RoomLayout(enemies[2]);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22488, 32145, 7308), new Angle(-0.61f, 0.79f)).Mask(SpawnPlane.Mask_Highground)); // right area
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22518, 29922, 7308), new Angle(0.50f, 0.87f)).Mask(SpawnPlane.Mask_Highground)); // right area
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24753, 31355, 7308), new Vector3f(22637, 30578, 7308), new Angle(-1.00f, 0.01f))
                .Mask(SpawnPlane.Mask_Highground).SetMaxEnemies(2)); // platform

            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(21625, 31378, 7862), new Angle(-0.99f, 0.11f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // zipline pole
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(25386, 26753, 7308), new Vector3f(24727, 27769, 7308), new Angle(0.72f, 0.70f))
                .Mask(SpawnPlane.Mask_Flatground));  // second platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(27294, 26881, 7308), new Angle(1.00f, 0.03f)).Mask(SpawnPlane.Mask_Highground)); // before door
            Rooms.Add(layout);

            // spiders
            layout = new RoomLayout(enemies[0], enemies[1]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(23266, 31957, 7755), new QuaternionAngle(0.50f, -0.50f, -0.50f, 0.50f))); // right wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(21766, 30276, 7756), new QuaternionAngle(0.13f, -0.75f, -0.65f, 0.03f))); // left tube
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(21483, 31750, 7948), new QuaternionAngle(0.71f, 0.00f, 0.70f, 0.00f))); // right corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24327, 30259, 7236), new QuaternionAngle(-0.03f, 0.03f, 0.64f, 0.77f))); // platform end
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(25368, 30949, 7761), new QuaternionAngle(-0.70f, -0.07f, 0.70f, 0.07f))); // back wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22587, 31805, 7214), new QuaternionAngle(0.00f, 0.00f, -0.87f, 0.50f))); // right default
            layout.DoNotReuse();
            Rooms.Add(layout);


            //// Room 12 ////
            enemies = room_HC_12.ReturnEnemiesInRoom(AllEnemies);
            enemies.ForEach(x => {
                if(x.Pos.X < 30000) EnemiesWithoutCP.Add(new EnemyDrone(x)); // only 2nd
            });


            //// Room 13 ////
            enemies = room_HC_13.ReturnEnemiesInRoom(AllEnemies); 
            enemies[0] = new EnemyTurret(enemies[0]);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[5].SetEnemyType(Enemy.EnemyTypes.Weeb);
            // shifter
            List<Tuple<Vector3f, Angle>> ShifterPoints_HC_Room13 = new List<Tuple<Vector3f, Angle>>() {
                new Tuple<Vector3f, Angle>(new Vector3f(24749, 16500, 8880), new Angle(0.78f, 0.63f)), //  rooftop
                new Tuple<Vector3f, Angle>(new Vector3f(21227, 17792, 8718), new Angle(-0.45f, 0.89f)), // pipe
                new Tuple<Vector3f, Angle>(new Vector3f(22619, 16645, 8648), new Angle(0.39f, 0.92f)), // billboard
                new Tuple<Vector3f, Angle>(new Vector3f(25000, 15753, 8548), new Angle(0.77f, 0.64f)), // zipline pole
                new Tuple<Vector3f, Angle>(new Vector3f(21462, 14958, 8710), new Angle(0.41f, 0.91f)), // building, small rooftop
                new Tuple<Vector3f, Angle>(new Vector3f(21286, 13270, 8852), new Angle(0.55f, 0.84f)), // ad
                new Tuple<Vector3f, Angle>(new Vector3f(24732, 11212, 8499), new Angle(0.70f, 0.71f)), // exit door
                new Tuple<Vector3f, Angle>(new Vector3f(18524, 12244, 9278), new Angle(0.36f, 0.93f)), // far left, clear rooftop
            };
            enemies[1] = new EnemyShifter(enemies[1]).AddFixedSpawnInfoList(ref ShifterPoints_HC_Room13); // 3 points

            RandomPickEnemiesWithoutCP(ref enemies, force: true, enemyIndex: 0); // turret
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(25013, 18029, 8000), new Vector3f(24129, 15997, 8018), new Angle(0.70f, 0.71f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(21529, 16815, 7999), new Vector3f(19801, 17586, 8008), new Angle(-0.02f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(18257, 11027, 8499), new Vector3f(18945, 13037, 8499), new Angle(0.37f, 0.93f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22472, 13371, 8500), new Vector3f(21450, 12356, 8499), new Angle(0.35f, 0.94f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(25205, 12462, 8501), new Vector3f(24142, 13400, 8508), new Angle(0.69f, 0.72f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(21962, 18304, 7999), new Angle(-0.71f, 0.70f)).Mask(SpawnPlane.Mask_Highground)); // around left corner
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24786, 17325, 8790), new Angle(0.72f, 0.70f)).Mask(SpawnPlane.Mask_Highground)); // rooftop middle
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(23933, 16546, 8648), new Angle(0.58f, 0.81f)).Mask(SpawnPlane.Mask_Highground)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22242, 17714, 8706), new Angle(0.00f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // rooftop left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19985, 17828, 8726), new Angle(-0.38f, 0.93f)).Mask(SpawnPlane.Mask_Highground)); // wall lamp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(18086, 12950, 8857), new Angle(0.50f, 0.86f)).Mask(SpawnPlane.Mask_Highground)); // fence/ad, left far
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(21543, 15562, 8721), new Angle(0.37f, 0.93f)).Mask(SpawnPlane.Mask_Highground)); // small rooftop, near zipline
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(23295, 13082, 8012), new Angle(0.66f, 0.75f)).Mask(SpawnPlane.Mask_Waver)); // beam middle
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24841, 13832, 7518), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Waver)); // beam under zipline
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19206, 10966, 8499), new Angle(0.44f, 0.90f)).Mask(SpawnPlane.Mask_Waver)); // left far corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(26170, 19039, 8378), new Angle(0.99f, 0.12f)).Mask(SpawnPlane.Mask_Highground)); // generator right of spawn
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(23326, 13040, 9019), new Angle(0.64f, 0.77f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(25374, 14328, 8597), new Angle(0.88f, 0.48f)).Mask(SpawnPlane.Mask_Airborne));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19694, 11740, 8667), new Angle(0.42f, 0.91f)).Mask(SpawnPlane.Mask_Airborne));
            // additional (turret)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(21135, 14605, 8739), new Angle(-0.28f, 0.96f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 30, HorizontalSpeed = 30, VerticalAngle = 0}));
            Rooms.Add(layout);


            //// Room 14 ////
            enemies = room_HC_14.ReturnEnemiesInRoom(AllEnemies); 
            enemies[0] = new EnemyTurret(enemies[0]);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[3].SetEnemyType(Enemy.EnemyTypes.Weeb);
            enemies[4].SetEnemyType(Enemy.EnemyTypes.Weeb);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, enemyIndex: 0); // turret
            RandomPickEnemiesWithoutCP(ref enemies, force: true); // another one

            // shifter - ATTACHING TO CP
            List<Tuple<Vector3f, Angle>> ShifterPoints_HC_Room14 = new List<Tuple<Vector3f, Angle>>() {
                new Tuple<Vector3f, Angle>(new Vector3f(30855, 3867, 8498), new Angle(-1.00f, 0.00f)), // right section, 2nd platform ledge
                new Tuple<Vector3f, Angle>(new Vector3f(26273, 3863, 8538), new Angle(0.36f, 0.93f)), // right section
                new Tuple<Vector3f, Angle>(new Vector3f(25343, -2568, 8506), new Angle(0.86f, 0.51f)), // near exit
                new Tuple<Vector3f, Angle>(new Vector3f(19193, 1987, 8950), new Angle(-0.14f, 0.99f)), // small fuel tank, left section
                new Tuple<Vector3f, Angle>(new Vector3f(20229, -1345, 9498), new Angle(0.68f, 0.74f)), // floating small platform
                new Tuple<Vector3f, Angle>(new Vector3f(21311, 1442, 8638), new Angle(0.93f, 0.36f)), // left section, merch table
                new Tuple<Vector3f, Angle>(new Vector3f(25971, 6493, 9410), new Angle(0.98f, 0.17f)), // rooftop, right of start
                new Tuple<Vector3f, Angle>(new Vector3f(25030, 6195, 9447), new Angle(0.80f, 0.60f)), // horizontal beam above start
                new Tuple<Vector3f, Angle>(new Vector3f(28504, -2511, 9251), new Angle(0.98f, 0.20f)), // shuriken billboard
                new Tuple<Vector3f, Angle>(new Vector3f(24371, 4908, 9050), new Angle(-0.27f, 0.96f)), // middle hallway, wall piece
            };
            AttachToGroup((Shifters[1] as EnemyShifter).AddFixedSpawnInfoList(ref ShifterPoints_HC_Room14), enemies[1]);
            enemies.Add(Shifters[1]); // that that new shifter to layout

            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(26114, 7154, 8528), new Vector3f(25461, 6667, 8527), new Angle(-1.00f, 0.01f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24931, 5776, 8527), new Vector3f(24602, 6349, 8527), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(29496, 7129, 8508), new Vector3f(27657, 6503, 8508), new Angle(-1.00f, 0.02f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(30262, 3900, 8501), new Vector3f(27704, 4542, 8501), new Angle(0.92f, 0.38f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24104, -2874, 8499), new Vector3f(25214, -1396, 8512), new Angle(0.41f, 0.91f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24428, 811, 8526), new Vector3f(24950, 3349, 8526), new Angle(0.70f, 0.71f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(26195, 4096, 8538), new Angle(0.31f, 0.95f)).Mask(SpawnPlane.Mask_Waver));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19775, 1192, 8500), new Vector3f(20670, 1807, 8499), new Angle(0.71f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(20679, -176, 8499), new Angle(0.38f, 0.93f)).Mask(SpawnPlane.Mask_Flatground));
            //high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(25946, 6695, 9410), new Angle(0.99f, 0.11f)).Mask(SpawnPlane.Mask_Highground)); // rooftop, right of cp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(29686, 7312, 9543), new Angle(-0.98f, 0.22f)).Mask(SpawnPlane.Mask_Highground)); // billboard, right corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(32104, 4655, 9633), new Vector3f(32113, 6704, 9610), new Angle(1.00f, 0.01f))
                .Mask(SpawnPlane.Mask_Highground).AsVerticalPlane()); // pipes, right far wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(27301, 3758, 9293), new Angle(0.70f, 0.72f)).Mask(SpawnPlane.Mask_Highground)); // orange billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(26435, 5380, 9057), new Angle(0.25f, 0.97f)).Mask(SpawnPlane.Mask_Highground)); // right section, small rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22237, 343, 9183), new Angle(-0.99f, 0.14f)).Mask(SpawnPlane.Mask_Highground)); // orange billboard, middle section
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(25549, -159, 9194), new Angle(1.00f, 0.09f)).Mask(SpawnPlane.Mask_Highground)); // pipe near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(23788, -1799, 8896), new Angle(0.11f, 0.99f)).Mask(SpawnPlane.Mask_Waver)); // wall shelf near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(28735, 7721, 9066), new Angle(-0.90f, 0.44f)).Mask(SpawnPlane.Mask_Highground)); // fuel tank right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19000, 4441, 9496), new Angle(0.08f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // wall lamp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22910, 6395, 8893), new Angle(0.28f, 0.96f)).Mask(SpawnPlane.Mask_Highground)); // pipe, right of cp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24224, -2630, 9295), new Angle(0.54f, 0.84f)).Mask(SpawnPlane.Mask_Highground)); // rooftop near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(20054, 811, 8858), new Angle(0.59f, 0.81f)).Mask(SpawnPlane.Mask_Highground)); // boxes left section
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19612, 541, 9496), new Angle(0.43f, 0.90f)).Mask(SpawnPlane.Mask_Highground)); // walllamp, left section above boxes ^
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24812, -2001, 9255), new Angle(0.63f, 0.78f)).Mask(SpawnPlane.Mask_Airborne)); // near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(20447, 3120, 9105), new Angle(0.62f, 0.78f)).Mask(SpawnPlane.Mask_Airborne)); // left zipline section
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(29759, 5194, 9105), new Angle(0.97f, 0.23f)).Mask(SpawnPlane.Mask_Airborne)); // right section
            // additional (turrets)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(25504, 318, 8508), new Angle(-0.91f, 0.41f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 40, HorizontalSpeed = 45, VerticalAngle = 5, Range = 2600})); // near exit

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(26282, 4561, 8538), new Angle(0.02f, 1.00f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, HorizontalSpeed = 0, VerticalAngle = 0}));// middle corner, aim towards right section, forcing left path
            Rooms.Add(layout);


            //// Room 15 ////
            enemies = room_HC_15.ReturnEnemiesInRoom(AllEnemies); 
            // shifter
            List<Tuple<Vector3f, Angle>> ShifterPoints_HC_Room15 = new List<Tuple<Vector3f, Angle>>() {
                new Tuple<Vector3f, Angle>(new Vector3f(19687, -10289, 9195), new Angle(0.54f, 0.84f)), // right billboard 1
                new Tuple<Vector3f, Angle>(new Vector3f(17993, -10298, 9198), new Angle(0.33f, 0.94f)), //  right billboard 2
                new Tuple<Vector3f, Angle>(new Vector3f(18085, -8900, 8587), new Angle(0.14f, 0.99f)), // right platform, trash
                new Tuple<Vector3f, Angle>(new Vector3f(17411, -5071, 8570), new Angle(-0.27f, 0.96f)), // left platform, trash
                new Tuple<Vector3f, Angle>(new Vector3f(21576, -8150, 9698), new Angle(0.98f, 0.19f)), // above default, high
                new Tuple<Vector3f, Angle>(new Vector3f(17029, -8879, 8496), new Angle(0.02f, 1.00f)), //  right platform corner
                new Tuple<Vector3f, Angle>(new Vector3f(18441, -4966, 8497), new Angle(-0.40f, 0.92f)) // left platform corner
            };
            enemies[0] = new EnemyShifter(enemies[0]).AddFixedSpawnInfoList(ref ShifterPoints_HC_Room15);
            //RandomPickEnemiesWithoutCP(ref enemies, force:true,enemyIndex: 1); // frogger
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(18215, -6014, 8496), new Vector3f(17457, -5316, 8501), new Angle(-0.27f, 0.96f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(18149, -10053, 8500), new Vector3f(17152, -9084, 8496), new Angle(0.02f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(20885, -7989, 8496), new Vector3f(21256, -7142, 8496), new Angle(-0.06f, 1.00f)).Mask(SpawnPlane.Mask_Flatground));
            Rooms.Add(layout);


            //// Room 16 ////
            enemies = room_HC_16.ReturnEnemiesInRoom(AllEnemies); 
            // shifter
            List<Tuple<Vector3f, Angle>> ShifterPoints_HC_Room16 = new List<Tuple<Vector3f, Angle>>() {
                new Tuple<Vector3f, Angle>(new Vector3f(17302, -20645, 9203), new Angle(0.69f, 0.72f)), // extruded wall part, infront of cp
                new Tuple<Vector3f, Angle>(new Vector3f(17919, -18984, 8497), new Angle(0.73f, 0.68f)), // platform ledge
                new Tuple<Vector3f, Angle>(new Vector3f(21725, -19915, 8498), new Angle(0.88f, 0.47f)), // platform ledge 2
                new Tuple<Vector3f, Angle>(new Vector3f(22578, -16154, 8498), new Angle(-1.00f, 0.04f)), // platform ledge 3
                new Tuple<Vector3f, Angle>(new Vector3f(20280, -17565, 8008), new Angle(0.91f, 0.41f)), // middle
                new Tuple<Vector3f, Angle>(new Vector3f(18050, -14370, 9081), new Angle(-0.63f, 0.77f)), // pillar near enter
                new Tuple<Vector3f, Angle>(new Vector3f(16598, -20663, 9863), new Angle(0.50f, 0.87f)), // pipes
                new Tuple<Vector3f, Angle>(new Vector3f(17440, -20784, 9868), new Angle(0.70f, 0.71f)), // high place
                new Tuple<Vector3f, Angle>(new Vector3f(19827, -14470, 7906), new Angle(-0.64f, 0.77f)), // pipes 2
                new Tuple<Vector3f, Angle>(new Vector3f(23037, -20789, 9478), new Angle(0.79f, 0.62f)), // above exit
            };

            enemies[0] = new EnemyShifter(enemies[0]).AddFixedSpawnInfoList(ref ShifterPoints_HC_Room16);
            enemies[1] = new EnemyShifter(enemies[1]).AddFixedSpawnInfoList(ref ShifterPoints_HC_Room16);
            enemies[4].SetEnemyType(Enemy.EnemyTypes.Waver);
            RandomPickEnemiesWithoutCP(ref enemies, enemyIndex: 4); // waver
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(18754, -14445, 8496), new Vector3f(18151, -14923, 8501), new Angle(1.00f, 0.03f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(21834, -15240, 8498), new Vector3f(22477, -14705, 8504), new Angle(-1.00f, 0.02f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22865, -14771, 8501), new Vector3f(23525, -15855, 8501), new Angle(1.00f, 0.01f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22731, -19198, 8497), new Vector3f(23249, -20051, 8501), new Angle(0.91f, 0.40f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22340, -19971, 8498), new Vector3f(21891, -20626, 8501), new Angle(0.97f, 0.24f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(17088, -19259, 8498), new Vector3f(17934, -20204, 8498), new Angle(0.72f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19934, -17181, 8008), new Angle(0.89f, 0.45f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(20686, -17873, 8008), new Angle(0.88f, 0.47f)).Mask(SpawnPlane.Mask_Highground));
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(23226, -14494, 9197), new Angle(-0.99f, 0.14f)).Mask(SpawnPlane.Mask_Highground)); // right server rack
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(20575, -14473, 7906), new Angle(-0.74f, 0.67f)).Mask(SpawnPlane.Mask_Highground)); // pipes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19557, -20610, 8011), new Angle(0.23f, 0.97f)).Mask(SpawnPlane.Mask_Highground)); // pipes 2
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(20966, -19124, 8757), new Vector3f(19359, -16580, 8757), new Angle(0.91f, 0.42f)).Mask(SpawnPlane.Mask_Airborne)); // middle
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(23158, -19977, 9150), new Angle(0.88f, 0.48f)).Mask(SpawnPlane.Mask_Airborne)); // near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(16714, -19604, 9150), new Angle(0.59f, 0.80f)).Mask(SpawnPlane.Mask_Airborne)); // left side of room
            // additional (turret)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22528, -14318, 9197), new Angle(-0.83f, 0.56f)).Mask(SpawnPlane.Mask_Turret)
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 20, HorizontalSpeed = 30, VerticalAngle = -5, Range = 5700})); // server rack
            Rooms.Add(layout);


            //// Room 17 ////
            enemies = room_HC_17.ReturnEnemiesInRoom(AllEnemies);
            // shifter
            List<Tuple<Vector3f, Angle>> ShifterPoints_HC_Room17 = new List<Tuple<Vector3f, Angle>>() {
                new Tuple<Vector3f, Angle>(new Vector3f(35246, -29695, 8510), new Angle(-0.99f, 0.11f)), // right platform
                new Tuple<Vector3f, Angle>(new Vector3f(32891, -35977, 8518), new Angle(0.70f, 0.71f)), // first platform, ledge
                new Tuple<Vector3f, Angle>(new Vector3f(35364, -35832, 8510), new Angle(0.70f, 0.71f)), // right platform, near zipline
                new Tuple<Vector3f, Angle>(new Vector3f(32954, -40905, 8510), new Angle(0.71f, 0.70f)), // 2nd left platform, end
                new Tuple<Vector3f, Angle>(new Vector3f(35256, -41091, 8510), new Angle(0.70f, 0.71f)), // exit
                new Tuple<Vector3f, Angle>(new Vector3f(33344, -33333, 9243), new Angle(0.76f, 0.65f)), // billboard left
                new Tuple<Vector3f, Angle>(new Vector3f(34043, -33332, 9243), new Angle(0.79f, 0.61f)), // billboard middle
                new Tuple<Vector3f, Angle>(new Vector3f(35128, -29225, 8857), new Angle(-0.91f, 0.41f)), // wall pipe, right
                new Tuple<Vector3f, Angle>(new Vector3f(34919, -41537, 9238), new Angle(0.83f, 0.56f)), // above exit left
                new Tuple<Vector3f, Angle>(new Vector3f(35727, -41521, 9238), new Angle(0.79f, 0.61f)), // above exit right
            };
            enemies[0] = new EnemyShifter(enemies[0]).AddFixedSpawnInfoList(ref ShifterPoints_HC_Room17);
            enemies[1] = new EnemyShifter(enemies[1]).AddFixedSpawnInfoList(ref ShifterPoints_HC_Room17);
            enemies[5].SetEnemyType(Enemy.EnemyTypes.Weeb);
            RandomPickEnemiesWithoutCP(ref enemies, enemyIndex: 3); // frogger
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(32622, -31840, 8522), new Vector3f(33129, -35256, 8523), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(3));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35556, -35351, 8518), new Vector3f(35087, -30538, 8518), new Angle(0.99f, 0.17f)).Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(3));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35588, -38718, 8518), new Vector3f(34970, -40281, 8518), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(33197, -38396, 8518), new Vector3f(32613, -40283, 8518), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2));
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34879, -33326, 9243), new Angle(0.86f, 0.52f)).Mask(SpawnPlane.Mask_Highground)); // billboard right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34822, -33916, 8503), new Angle(0.86f, 0.51f)).Mask(SpawnPlane.Mask_Highground)); // between billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35346, -41566, 9207), new Angle(0.74f, 0.67f)).Mask(SpawnPlane.Mask_Highground)); // above exit
            // additional (drones)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(36035, -36001, 9067), new Vector3f(35605, -31539, 9293), new Angle(0.99f, 0.15f)).Mask(SpawnPlane.Mask_Airborne)); // right section
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34136, -40422, 8941), new Angle(0.74f, 0.67f)).Mask(SpawnPlane.Mask_Airborne)); // near exit
            // additional (turret)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(32197, -35982, 8208), new Angle(-0.40f, 0.92f)).Mask(SpawnPlane.Mask_Turret)
                 .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0.5f, HorizontalSpeed = 0.5f, VerticalAngle = 10, Range = 5500, VisibleLaserLength = 0 })); // left beam under platform
            Rooms.Add(layout);



            //// EXTRA ////
            layout = new RoomLayout();
            // CV ninja behind crates (can be regular enemy or ninja turret)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(28847, 28721, 7323), new Angle(-0.11f, 0.99f))
                .Mask(new List<Enemy.EnemyTypes>() { 
                   Enemy.EnemyTypes.Weeb, Enemy.EnemyTypes.Default, Enemy.EnemyTypes.Drone, Enemy.EnemyTypes.Weeb})
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, HorizontalSpeed = 0, VerticalAngle = 5, VisibleLaserLength = 0, MaxAttackRange = 3700, MaxAttackRange2 = 3700}));

            // collectible ninja, before last room, or sneaky WAVER
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(26283, -24364, 8602), new Angle(-0.32f, 0.95f))
                .Mask(new List<Enemy.EnemyTypes>() {
                    Enemy.EnemyTypes.Default, Enemy.EnemyTypes.Drone, Enemy.EnemyTypes.Weeb, Enemy.EnemyTypes.Drone})
                .SetSpawnInfo(new TurretSpawnInfo { HorizontalAngle = 0, HorizontalSpeed = 0, VerticalAngle = 5, VisibleLaserLength = 0, MaxAttackRange = 3700, MaxAttackRange2 = 3700 }));

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(26514, -27154, 8602), new Angle(0.96f, 0.30f)).Mask(SpawnPlane.Mask_Highground)); // before last room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-3858, 33859, 7408), new Angle(-0.76f, 0.65f)).Mask(SpawnPlane.Mask_Highground)); // beam, room 8
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(20708, 40956, 7362), new Angle(-1.00f, 0.08f)).Mask(SpawnPlane.Mask_Highground)); // room 10, first platform
            Rooms.Add(layout);


            #region Uplinks
            NonPlaceableObject uplink = new UplinkShurikens(new DeepPointer(PtrDB.DP_RiH_HC_Room14_Shuriken)); // 2'nd segment of map 
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 5, MaxAttacks = Config.GetInstance().r.Next(2,4)});
            worldObjects.Add(uplink);
            #endregion
        }

        public override void RandomizeEnemies(Process game) {
            base.RandomizeEnemies(game);
            DevUtils.DEV_FindObjects("shuriken", new List<Room>() {new Room(new Vector3f(25883, -4791, 7665), new Vector3f(31187, -963, 9864)) });

        }


        public void Gen_Nightmare_BeforeCV() {
            throw new NotImplementedException();
        }
        public void Gen_Nightmare_AfterCV() {
            throw new NotImplementedException();
        }


        protected override void Gen_PerRoom() {}
    }
}