using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.NonPlaceableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.Maps {
    class ReignInHell : MapCore {

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

        // cv flag
        private bool BeforeCV = true;

        public ReignInHell(bool isHC) : base(MapType.ReignInHell) {
            BeforeCV = GameHook.yPos < 20000;
            if(!isHC) {
                if(BeforeCV)
                    Gen_PerRoom();
                else
                    Gen_PerRoom_AfterCV();
            }
        }

        protected override void Gen_PerRoom() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game, 0, 37);
            Rooms = new List<RoomLayout>();
            RoomLayout layout;
            List<Enemy> enemies;

            // Note: no room for rng for Room 1 and 2 (spiders)

            // room 3 - hallhway with 4 spiders ////
            enemies = room_3.ReturnEnemiesInRoom(AllEnemies);
            layout = new RoomLayout(enemies.Skip(2).ToList()); // pistol spawns
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-5528, 21681, 6808), new Angle(-0.91f, 0.42f))); // exit platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-7027, 22696, 6412), new Angle(-0.87f, 0.50f))); // big fan wall
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
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(7151, 40729, 8239), new Angle(0.98f, 0.22f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // left rooftop 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8336, 41078, 7698), new Angle(0.95f, 0.31f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // crates
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(7795, 43937, 8236), new Angle(-0.62f, 0.79f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // ac unit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(12231, 45511, 7158), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // pipes extending to wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(13545, 43759, 8471), new Angle(-1.00f, 0.09f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // wall lamp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(12159, 42831, 8501), new Angle(1.00f, 0.07f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // wall lamp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(13530, 37731, 8406), new Angle(0.89f, 0.46f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // white pipes left from exit door
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(12347, 38719, 9212), new Angle(-1.00f, 0.02f)).setRarity(0.2).Mask(SpawnPlane.Mask_HighgroundLimited)); // high billboard, near roof
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(9321, 37252, 8547), new Angle(0.70f, 0.71f)).setRarity(0.2).Mask(SpawnPlane.Mask_HighgroundLimited));// hovering pipes, high
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(10696, 40411, 8383), new Angle(0.67f, 0.75f)).Mask(SpawnPlane.Mask_HighgroundLimited)); // middle zipline, wall fan
            Rooms.Add(layout);


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
            nonPlaceableObjects.Add(uplink);
            #endregion

            #region Shurikens
            //// First room with spiders
            uplink = new UplinkShurikens(0x0, 0xd68);//5,20
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 60, MaxAttacks = 100 }.SetRarity(0.05)); // for the spiders
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = Config.GetInstance().r.Next(6, 11), MaxAttacks = 6 }); // for 3 argets max 2 hits
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 6, MaxAttacks = Config.GetInstance().r.Next(6, 11) }); // basic
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 6, MaxAttacks = 20 }); // default
            nonPlaceableObjects.Add(uplink);
            #endregion

            #region Targets
            //collectible
            var collectibleTarget = new ShurikenTarget(0x0, 0x230).AddSpawnInfo(new ShurikenTargetSpawnInfo { HitsNeeded = Config.GetInstance().r.Next(1, 4) });
            collectibleTarget.OverwritePointerOffset("HitsNeeded", 0x220);
            nonPlaceableObjects.Add(collectibleTarget);

            //for the fan
            nonPlaceableObjects.Add(new ShurikenTarget(0x0, 0xD70).AddSpawnInfo(new ShurikenTargetSpawnInfo { HitsNeeded = Config.GetInstance().r.Next(1, 3) }));
            nonPlaceableObjects.Add(new ShurikenTarget(0x0, 0xD60).AddSpawnInfo(new ShurikenTargetSpawnInfo { HitsNeeded = Config.GetInstance().r.Next(1, 3) }));
            nonPlaceableObjects.Add(new ShurikenTarget(0x0, 0xD58).AddSpawnInfo(new ShurikenTargetSpawnInfo { HitsNeeded = Config.GetInstance().r.Next(1, 3) }));
            #endregion
        }

        private void Gen_PerRoom_AfterCV() {
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
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(23740, 16535, 8648), new Angle(0.56f, 0.83f)).Mask(SpawnPlane.Mask_Highground)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24752, 17405, 8790), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Highground)); // small rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(26217, 19079, 8383), new Angle(1.00f, 0.00f)).Mask(SpawnPlane.Mask_Highground)); // generator
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22450, 18110, 8008), new Angle(-0.01f, 1.00f)).Mask(SpawnPlane.Mask_Highground)); // small ledge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22030, 18360, 7999), new Angle(-0.74f, 0.67f)).Mask(SpawnPlane.Mask_Flatground)); // around the corner, near shielder
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22181, 15829, 8548), new Angle(0.58f, 0.81f)).Mask(SpawnPlane.Mask_Highground)); // on zipline pole
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(18076, 12914, 8857), new Angle(0.28f, 0.96f)).Mask(SpawnPlane.Mask_Highground)); // small ad near slowmo
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(18503, 12185, 9278), new Angle(0.67f, 0.74f)).Mask(SpawnPlane.Mask_Highground)); // small rooftop near slomo
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(21292, 10666, 9268), new Angle(0.69f, 0.72f)).Mask(SpawnPlane.Mask_Highground)); // above spider spawner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(21573, 14999, 8710), new Angle(0.45f, 0.89f)).Mask(SpawnPlane.Mask_Highground).setRarity(0.2)); // building rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(21466, 16125, 9054), new Angle(0.22f, 0.98f)).Mask(SpawnPlane.Mask_Highground)); // building rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24744, 20905, 8001), new Angle(0.70f, 0.71f)).setRarity(0.1).Mask(SpawnPlane.Mask_Highground)); // infront of cp spawn 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24714, 10767, 9400), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Highground)); // neon sign, above exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(25384, 12636, 8857), new Angle(0.81f, 0.58f)).Mask(SpawnPlane.Mask_Highground)); // small ad sign, near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24187, 12241, 9501), new Angle(0.62f, 0.79f)).Mask(SpawnPlane.Mask_Highground)); // billboard edge, near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24187, 12241, 9501), new Angle(0.62f, 0.79f)).Mask(SpawnPlane.Mask_Highground)); // wall pipes
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
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(23674, -384, 9279), new Vector3f(21917, 166, 8478), new Angle(-1.00f, 0.03f))); // whole hallway, 2 billboards
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19903, 572, 9112), new Angle(0.64f, 0.77f))); // above crates, near shielder
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19368, -2608, 9461), new Angle(0.47f, 0.88f))); // near vent/slomo
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(25499, 1175, 9026), new Vector3f(25514, 2966, 8584), new Angle(0.73f, 0.68f)).AsVerticalPlane()); // middle hallway, billboard right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(23890, 1100, 9059), new Vector3f(23895, 2952, 8506), new Angle(0.70f, 0.72f)).AsVerticalPlane()); // middle hallway, billboard, left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(26114, 4563, 9046), new Angle(-0.92f, 0.39f))); // small rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24797, 6091, 9447), new Angle(0.81f, 0.59f)).setRarity(0.2)); // hovering cables/beam 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(30533, 4240, 8623), new Angle(1.00f, 0.01f))); // weeb platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(31679, 678, 9074), new Vector3f(31615, 2042, 8482), new Angle(0.72f, 0.69f))); // billboard, far right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(29840, -2359, 9155), new Vector3f(28554, -2274, 8539), new Angle(1.00f, 0.03f))); // billboard 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19330, 1465, 9221), new Angle(0.70f, 0.72f))); // near fuel tank
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(27811, 3195, 9747), new Angle(0.93f, 0.36f)).setRarity(0.2)); // high pipes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(18877, -298, 9446), new Angle(0.02f, 1.00f)).setRarity(0.1)); // wall lamp
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
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(21539, 4173, 9860), new Angle(-0.90f, 0.44f)).setRarity(0.2).Mask(mask)); // high rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19006, 4428, 9496), new Angle(0.16f, 0.99f)).Mask(mask)); // wall lamp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19004, 6347, 9496), new Angle(0.04f, 1.00f)).Mask(mask)); // wall lamp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19106, 7221, 9962), new Angle(0.01f, 1.00f)).setRarity(0.1).Mask(mask)); // wall fan
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22686, 6409, 8893), new Angle(0.21f, 0.98f)).Mask(mask)); // wall pipe
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(26030, 6828, 9410), new Angle(1.00f, 0.05f)).Mask(mask)); // rooftop
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(27645, 6739, 9932), new Angle(-1.00f, 0.08f)).Mask(mask)); // neon sign
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(28725, 8047, 9784), new Angle(-0.72f, 0.69f)).Mask(mask)); // big fuel tank
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(30047, 7320, 9544), new Angle(-0.95f, 0.32f)).Mask(mask)); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(32084, 4711, 9610), new Angle(-1.00f, 0.03f)).setRarity(0.2).Mask(mask)); // far pipes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(31955, 2300, 9355), new Vector3f(31956, 828, 9355), new Angle(1.00f, 0.01f)).AsVerticalPlane().Mask(mask)); // billboard infront of shuriken 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(26766, -1221, 9476), new Angle(-0.94f, 0.34f)).Mask(mask)); // wall lamp, near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24162, -2652, 9295), new Angle(0.57f, 0.82f)).Mask(mask)); // rooftop near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(23874, -1029, 9398), new Angle(0.49f, 0.87f)).Mask(mask)); // wall piece near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(22098, 358, 9184), new Vector3f(23647, 360, 9184), new Angle(-0.40f, 0.92f)).AsVerticalPlane().Mask(mask)); // billboard top near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24741, 6221, 9447), new Angle(0.71f, 0.70f)).Mask(mask)); // beam about entry
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(23944, 2053, 9388), new Angle(0.51f, 0.86f)).setRarity(0.1).Mask(mask)); // beam, middle hallway
            Rooms.Add(layout);


            // room 9 - shifter ////
            enemies = room_9.ReturnEnemiesInRoom(AllEnemies);

            EnemyShifter shifter = new EnemyShifter(enemies[0], 3);
            ShifterSpawnInfo info = new ShifterSpawnInfo();
            info.shiftPoints = new List<Tuple<Vector3f, Angle>>() {
                new Tuple<Vector3f, Angle>(new Vector3f(19686, -10296, 9191), new Angle(0.48f, 0.88f)),
                new Tuple<Vector3f, Angle>(new Vector3f(24945, -7467, 8500), new Angle(-1.00f, 0.02f)),
                new Tuple<Vector3f, Angle>(new Vector3f(20007, -4607, 8543), new Angle(-0.56f, 0.83f))
            };
            ShifterSpawnInfo info2 = new ShifterSpawnInfo();
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



            //// room 10 - 2 shifters+pistol+weeb+shield ////
            enemies = room_10.ReturnEnemiesInRoom(AllEnemies);

            enemies[4].SetEnemyType(Enemy.EnemyTypes.Weeb);

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

            int shifterR = Config.GetInstance().r.Next(2); // so each shifter gets a different spawnInfo and they won't collide
            enemies[1] = new EnemyShifter(enemies[1], 4).AddFixedSpawnInfo(shifterR == 0 ? info : info2); // doesn't matter which plane, shifter will use this spawninfo
            enemies[2] = new EnemyShifter(enemies[2], 4).AddFixedSpawnInfo(shifterR == 0 ? info2 : info);

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
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(17440, -20784, 9868), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Highground)); // 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(16598, -20663, 9863), new Angle(0.57f, 0.82f)).Mask(SpawnPlane.Mask_Highground)); // pipes
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(24942, -16181, 8245), new Angle(0.99f, 0.14f)).setRarity(0.1).Mask(SpawnPlane.Mask_Highground)); // pipe of lab tube
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(20646, -14477, 7906), new Angle(-1.00f, 0.07f)).setRarity(0.2).Mask(SpawnPlane.Mask_Highground)); // pipe below right billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(19580, -20689, 7998), new Angle(0.69f, 0.73f)).Mask(SpawnPlane.Mask_Highground)); // pipes below billboard, far back
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(17296, -20644, 9203), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Highground)); // red neon lines
            Rooms.Add(layout);


            //// room 11 - 2 shifter+weeb+frogger ////
            enemies = room_11.ReturnEnemiesInRoom(AllEnemies);

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


            enemies[3].SetEnemyType(Enemy.EnemyTypes.Weeb);

            layout = new RoomLayout(enemies);
            // default platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(33158, -31712, 8522), new Vector3f(32577, -35986, 8518), new Angle(0.71f, 0.71f)).SetMaxEnemies(2).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35066, -35548, 8518), new Vector3f(35569, -32090, 8518), new Angle(0.72f, 0.69f)).SetMaxEnemies(2).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(32682, -37943, 8522), new Vector3f(33148, -40829, 8518), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(35478, -41093, 8517), new Vector3f(35010, -38585, 8518), new Angle(0.72f, 0.70f)).Mask(SpawnPlane.Mask_Flatground));

            // special/high places
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(33336, -33327, 9243), new Angle(0.77f, 0.64f)).Mask(SpawnPlane.Mask_Highground)); // billboard edge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34823, -33340, 9243), new Angle(0.73f, 0.69f)).Mask(SpawnPlane.Mask_Highground)); // billboard edge 2
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34784, -34379, 9242), new Angle(-0.25f, 0.97f)).Mask(SpawnPlane.Mask_Highground)); // billboard edge 3
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(33339, -34365, 9243), new Angle(0.91f, 0.41f)).Mask(SpawnPlane.Mask_Highground)); // billboard edge 4
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(34892, -41518, 9238), new Angle(0.71f, 0.70f)).Mask(SpawnPlane.Mask_Highground)); // above exit door
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
            nonPlaceableObjects.Add(uplink);
            #endregion

            #region Shurikens
            // 2 room
            uplink = new UplinkShurikens(0x58, 0x1F8);//5,100
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 45, MaxAttacks = 50 }.SetRarity(0.05)); //for the next room
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = Config.GetInstance().r.Next(4, 11), MaxAttacks = Config.GetInstance().r.Next(7, 15) });
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo { Duration = 15, MaxAttacks = Config.GetInstance().r.Next(3, 7) });//unlucky rng
            uplink.AddSpawnInfo(new UplinkShurikensSpawnInfo().SetRarity(0.5)); // default
            nonPlaceableObjects.Add(uplink);
            #endregion

            #region Slomo
            //1 room
            uplink = new UplinkSlowmo(0x50, 0xB0, 0xA0);
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
    }
}