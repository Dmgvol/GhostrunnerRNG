using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.MemoryUtils;
using GhostrunnerRNG.NonPlaceableObjects;
using System.Collections.Generic;
using System.Diagnostics;

namespace GhostrunnerRNG.Maps {
    class SurgeCV : MapCore {

        private List<SpawnData> Section1Spawns = new List<SpawnData>();
        private List<SpawnData> Section2Spawns = new List<SpawnData>();
        private List<SpawnData> Section3Spawns = new List<SpawnData>();

        private List<Enemy> enemies = new List<Enemy>();

        public SurgeCV() : base(GameUtils.MapType.SurgeCV, manualGen:true) {
            CPRequired = false;
            Gen_PerRoom();
        }
        protected override void Gen_PerRoom() {
            // section 1 - single enemy
            enemies.Add(new Enemy(new DeepPointer(PtrDB.DP_SurgeCV_EnemyEntity).Format(0x10)));
            // section 2 - double enemies
            enemies.Add(new Enemy(new DeepPointer(PtrDB.DP_SurgeCV_EnemyEntity).Format(0x30)));
            enemies.Add(new Enemy(new DeepPointer(PtrDB.DP_SurgeCV_EnemyEntity).Format(0x38)));
            // section 3 - triple enemies
            enemies.Add(new Enemy(new DeepPointer(PtrDB.DP_SurgeCV_EnemyEntity).Format(0x28)));
            enemies.Add(new Enemy(new DeepPointer(PtrDB.DP_SurgeCV_EnemyEntity).Format(0x20)));
            enemies.Add(new Enemy(new DeepPointer(PtrDB.DP_SurgeCV_EnemyEntity).Format(0x18)));

            //// Layouts ////
            // section 1
            Section1Spawns.Add(new SpawnData(new Vector3f(800, 9200, 477), new Angle(0.71f, 0.71f))); // default
            Section1Spawns.Add(new SpawnData(new Vector3f(-2386, 6176, 536), new Angle(0.0f, 1.0f))); // exit ledge
            Section1Spawns.Add(new SpawnData(new Vector3f(-1714, 9701, 690), new Angle(0.71f, 0.71f))); // right rock
            Section1Spawns.Add(new SpawnData(new Vector3f(-2189, 9689, 1655), new Angle(0.71f, 0.71f))); // palm
            Section1Spawns.Add(new SpawnData(new Vector3f(-1260, 9682, 1514), new Angle(0.71f, 0.71f))); // palm 2
            Section1Spawns.Add(new SpawnData(new Vector3f(-4973, 9148, 1499), new Angle(-0.71f, 0.71f))); // arc behind spawn
            Section1Spawns.Add(new SpawnData(new Vector3f(-4201, 11227, 2186), new Angle(1.0f, 0.0f)));// highest palm
            Section1Spawns.Add(new SpawnData(new Vector3f(-2203, 6098, 1910), new Angle(0.0f, 1.0f)));// palm near next phase 

            // section 2
            Section2Spawns.Add(new SpawnData(new Vector3f(-2600, 1400, 490), new Angle(0.0f, 1.0f)));//default
            Section2Spawns.Add(new SpawnData(new Vector3f(-1450, 400, 390), new Angle(0.0f, 1.0f)));

            Section2Spawns.Add(new SpawnData(new Vector3f(-2591, 1355, 498), new Angle(0.0f, 1.0f)));
            Section2Spawns.Add(new SpawnData(new Vector3f(-2037, 2441, 497), new Angle(0.0f, 1.0f)));

            Section2Spawns.Add(new SpawnData(new Vector3f(-1493, 2764, 1608), new Angle(0.0f, 1.0f)));
            Section2Spawns.Add(new SpawnData(new Vector3f(-2780, 2936, 498), new Angle(-0.71f, 0.71f)));

            Section2Spawns.Add(new SpawnData(new Vector3f(-3699, -2199, 1678), new Angle(0.0f, 1.0f)));
            Section2Spawns.Add(new SpawnData(new Vector3f(-4133, -1901, 1690), new Angle(0.0f, 1.0f)));

            Section2Spawns.Add(new SpawnData(new Vector3f(-3827, 3575, 2450), new Angle(-1.0f, 0.0f)));
            Section2Spawns.Add(new SpawnData(new Vector3f(-4513, 3332, 2692), new Angle(-1.0f, 0.0f)));

            // section 3
            Section3Spawns.Add(new SpawnData(new Vector3f(-5162, -8694, 2898), new Angle(-0.89f, 0.46f))); // default
            Section3Spawns.Add(new SpawnData(new Vector3f(-5981, -8421, 3089), new Angle(-0.82f, 0.57f)));
            Section3Spawns.Add(new SpawnData(new Vector3f(-5993, -9006, 3091), new Angle(-0.82f, 0.57f)));

            Section3Spawns.Add(new SpawnData(new Vector3f(-2629, -10411, 3043), new Angle(0.0f, 1.0f)));
            Section3Spawns.Add(new SpawnData(new Vector3f(-2874, -9929, 2965), new Angle(0.0f, 1.0f)));
            Section3Spawns.Add(new SpawnData(new Vector3f(-2898, -10215, 2847), new Angle(0.0f, 1.0f)));

            Section3Spawns.Add(new SpawnData(new Vector3f(-4042, -12560, 2464), new Angle(0.0f, 1.0f)));
            Section3Spawns.Add(new SpawnData(new Vector3f(-3884, -12385, 2418), new Angle(0.0f, 1.0f)));
            Section3Spawns.Add(new SpawnData(new Vector3f(-3603, -12379, 2369), new Angle(0.0f, 1.0f)));


            #region JumpPad
            var jumppad = new JumpPad(0x1D0);//3500
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 9000, Angle = new QuaternionAngle(-93, -30, 0) }); // short 
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 9300, Angle = new QuaternionAngle(-93, -38, 0) }); // long
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 7500, Angle = new QuaternionAngle(-90, -75, 0) }.SetRarity(0.35)); // oob on top
            worldObjects.Add(jumppad);

            jumppad = new JumpPad(0x1C8);//3500
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 6000, Angle = new QuaternionAngle(-147, -45, 0) }.SetRarity(0.5)); // to the exit 
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 4750, Angle = new QuaternionAngle(-120, -45, 0) }); // long
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 4230, Angle = new QuaternionAngle(-98, -60, 0) }); // next jumppad
            jumppad.AddSpawnInfo(new JumpPadSpawnInfo { Speed = 6600, Angle = new QuaternionAngle(0, -90, 0) }.SetRarity(0.5)); // high
            worldObjects.Add(jumppad);

            //jumppad = new JumpPad(0x148);//3500
            //jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            //worldObjects.Add(jumppad);
            #endregion

        }

        public override void RandomizeEnemies(Process game) { 
            base.RandomizeEnemies(game);
            // section 1
            int r = Config.GetInstance().r.Next(Section1Spawns.Count);
            enemies[0].SetMemoryPos(game, Section1Spawns[r]);

            // section 2
            r = Config.GetInstance().r.Next(Section2Spawns.Count / 2);
            enemies[1].SetMemoryPos(game, Section2Spawns[r * 2]);
            enemies[2].SetMemoryPos(game, Section2Spawns[r * 2 + 1]);
            // section 3
            r = Config.GetInstance().r.Next(Section3Spawns.Count /  3);
            enemies[3].SetMemoryPos(game, Section3Spawns[r * 3]);
            enemies[4].SetMemoryPos(game, Section3Spawns[r * 3 + 1]);
            enemies[5].SetMemoryPos(game, Section3Spawns[r * 3 + 2]);
        }
    }
}
