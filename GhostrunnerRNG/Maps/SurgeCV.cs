using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.NonPlaceableObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GhostrunnerRNG.Maps {
    class SurgeCV : MapCore {

        private List<SpawnData> Section1Spawns = new List<SpawnData>();
        private List<SpawnData> Section2Spawns = new List<SpawnData>();
        private List<SpawnData> Section3Spawns = new List<SpawnData>();

        private List<Enemy> enemies = new List<Enemy>();

        public SurgeCV() : base(GameUtils.MapType.SurgeCV, manualGen:true) {
            Gen_PerRoom();
        }
        protected override void Gen_PerRoom() {
            // section 1 - single enemy
            enemies.Add(new Enemy(new DeepPointer(0x04609420, 0x30, 0xA8, 0x10, 0x130, 0x1D0)));
            // section 2 - double enemies
            enemies.Add(new Enemy(new DeepPointer(0x04609420, 0x30, 0xA8, 0x30, 0x130, 0x1D0)));
            enemies.Add(new Enemy(new DeepPointer(0x04609420, 0x30, 0xA8, 0x38, 0x130, 0x1D0)));
            // section 3 - triple enemies
            enemies.Add(new Enemy(new DeepPointer(0x04609420, 0x30, 0xA8, 0x28, 0x130, 0x1D0)));
            enemies.Add(new Enemy(new DeepPointer(0x04609420, 0x30, 0xA8, 0x20, 0x130, 0x1D0)));
            enemies.Add(new Enemy(new DeepPointer(0x04609420, 0x30, 0xA8, 0x18, 0x130, 0x1D0)));

            //// Layouts ////
            // section 1
            Section1Spawns.Add(new SpawnData(new Vector3f(792, 9229, 482), new Angle(1.00f, 0.01f))); // default
            Section1Spawns.Add(new SpawnData(new Vector3f(-2386, 6176, 536), new Angle(0.89f, 0.45f))); // exit ledge
            Section1Spawns.Add(new SpawnData(new Vector3f(-1714, 9701, 690), new Angle(-0.98f, 0.20f))); // right rock
            Section1Spawns.Add(new SpawnData(new Vector3f(-2189, 9689, 1655), new Angle(-0.99f, 0.16f))); // pam
            Section1Spawns.Add(new SpawnData(new Vector3f(-1260, 9682, 1514), new Angle(-1.00f, 0.07f))); // pam 2
            Section1Spawns.Add(new SpawnData(new Vector3f(-4973, 9148, 1499), new Angle(0.01f, 1.00f))); // arc behind spawn
            Section1Spawns.Add(new SpawnData(new Vector3f(-4201, 11227, 2186), new Angle(-0.42f, 0.91f)));// highest palm
            Section1Spawns.Add(new SpawnData(new Vector3f(-2203, 6098, 1910), new Angle(0.71f, 0.71f)));// palm near next pahse 

            // section 2
            Section2Spawns.Add(new SpawnData(new Vector3f(-2591, 1355, 498), new Angle(0.84f, 0.53f)));
            Section2Spawns.Add(new SpawnData(new Vector3f(-1530, 429, 398), new Angle(0.88f, 0.47f)));

            Section2Spawns.Add(new SpawnData(new Vector3f(-2591, 1355, 498), new Angle(0.84f, 0.53f)));
            Section2Spawns.Add(new SpawnData(new Vector3f(-3928, 15, 505), new Angle(0.67f, 0.74f)));

            Section2Spawns.Add(new SpawnData(new Vector3f(-2591, 1355, 498), new Angle(0.84f, 0.53f)));
            Section2Spawns.Add(new SpawnData(new Vector3f(-2037, 2441, 497), new Angle(0.56f, 0.83f)));

            Section2Spawns.Add(new SpawnData(new Vector3f(-4268, 2725, 497), new Angle(0.04f, 1.00f)));
            Section2Spawns.Add(new SpawnData(new Vector3f(-3021, 2831, 498), new Angle(0.04f, 1.00f)));

            Section2Spawns.Add(new SpawnData(new Vector3f(-1493, 2764, 1608), new Angle(0.99f, 0.11f)));
            Section2Spawns.Add(new SpawnData(new Vector3f(-2780, 2936, 498), new Angle(0.98f, 0.20f)));

            Section2Spawns.Add(new SpawnData(new Vector3f(-3699, -2199, 1678), new Angle(0.58f, 0.82f)));
            Section2Spawns.Add(new SpawnData(new Vector3f(-4133, -1901, 1690), new Angle(0.52f, 0.86f)));

            Section2Spawns.Add(new SpawnData(new Vector3f(-3827, 3575, 2450), new Angle(-0.62f, 0.78f)));
            Section2Spawns.Add(new SpawnData(new Vector3f(-4513, 3332, 2692), new Angle(-0.41f, 0.91f)));


            // section 3
            Section3Spawns.Add(new SpawnData(new Vector3f(-5141, -8693, 2898), new Angle(-0.02f, 1.00f))); // default
            Section3Spawns.Add(new SpawnData(new Vector3f(-5971, -8434, 3096), new Angle(-0.12f, 0.99f)));
            Section3Spawns.Add(new SpawnData(new Vector3f(-6017, -8988, 3100), new Angle(0.08f, 1.00f)));

            Section3Spawns.Add(new SpawnData(new Vector3f(-2629, -10411, 3043), new Angle(0.78f, 0.63f)));
            Section3Spawns.Add(new SpawnData(new Vector3f(-2874, -9929, 2965), new Angle(0.78f, 0.63f)));
            Section3Spawns.Add(new SpawnData(new Vector3f(-2898.7f, -10215.11f, 2847.67f), new Angle(0.78f, 0.63f)));

            Section3Spawns.Add(new SpawnData(new Vector3f(-4042, -12560, 2464), new Angle(0.70f, 0.71f)));
            Section3Spawns.Add(new SpawnData(new Vector3f(-3884, -12385, 2418), new Angle(0.70f, 0.71f)));
            Section3Spawns.Add(new SpawnData(new Vector3f(-3603, -12379, 2369), new Angle(0.70f, 0.72f)));


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
            worldObjects.Add(jumppad);

            //jumppad = new JumpPad(0x148);//3500
            //jumppad.AddSpawnInfo(new JumpPadSpawnInfo());//default
            //worldObjects.Add(jumppad);
            #endregion

        }

        public override void RandomizeEnemies(Process game) {
            // section 1
            int r = Config.GetInstance().r.Next(Section1Spawns.Count);
            //enemies[0].SetMemoryPos(game, Section1Spawns[r]);
            SetCVPlayerPos(game, enemies[0], Section1Spawns[r]);

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

        private void SetCVPlayerPos(Process game, Enemy enemy, SpawnData spawnData) {
            enemy.Deref(game);

            game.WriteBytes(enemy.GetObjectPtr() - 8, BitConverter.GetBytes((float)spawnData.angle.angleSin));
            game.WriteBytes(enemy.GetObjectPtr() - 4, BitConverter.GetBytes((float)spawnData.angle.angleCos));
            
            game.WriteBytes(enemy.GetObjectPtr(), BitConverter.GetBytes((float)spawnData.pos.X));
            game.WriteBytes(enemy.GetObjectPtr() + 4, BitConverter.GetBytes((float)spawnData.pos.Y));
            game.WriteBytes(enemy.GetObjectPtr() + 8, BitConverter.GetBytes((float)spawnData.pos.Z));
        }
    }
}
