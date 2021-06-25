using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.GameObjects;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.MemoryUtils;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GhostrunnerRNG.Maps {
    class BlinkCV : MapCore {
        // Enemies
        List<Enemy> Enemies_Room1, Enemies_Room2, Enemies_Room3;

        // Rooms
        Room room1 = new Room(new Vector3f(-8529, 25234, 213), new Vector3f(-6636, 20403, 1491));
        Room room2 = new Room(new Vector3f(-9458, 18142, -135), new Vector3f(-5490, 14078, 1942));
        Room room_3platforms = new Room(new Vector3f(-9025, 10537, -563), new Vector3f(-6563, 7547, 1574));

        List<Vector3f> platformDefaultPositions = new List<Vector3f>();

        // predefined spawns for platforms
        List<PlatformSpawner> platformSpawns = new List<PlatformSpawner>();
        List<CVPlatform> platforms = new List<CVPlatform>();

        public BlinkCV() : base(GameUtils.MapType.BlinkCV, manualGen: true) {
            if(GameHook.IsHC) return;
            Gen_PerRoom();
        }

        protected override void Gen_PerRoom() {
            // get all enemies and create layout & rooms
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game);

            // Sort enemies per room
            Enemies_Room1 = room1.ReturnEnemiesInRoom(AllEnemies);
            Enemies_Room2 = room2.ReturnEnemiesInRoom(AllEnemies);
            Enemies_Room3 = room_3platforms.ReturnEnemiesInRoom(AllEnemies);

            // spawn planes for 3 platforms
            platformSpawns.Add(new PlatformSpawner() {
                p1_start = new Vector3f(-7331, 9542, 803),
                p1_end = new Vector3f(-8105, 9551, 10),
                p2_start = new Vector3f(-7700, 9100, 300),
                p3_start = new Vector3f(-7308, 8648, 10),
                p3_end = new Vector3f(-8101, 8644, 803)
            });

            platformSpawns.Add(new PlatformSpawner() {
                p1_start = new Vector3f(-7703, 9519, 600),
                p1_end = new Vector3f(-7703, 9519, 22),
                p2_start = new Vector3f(-7700, 9100, 300),
                p3_start = new Vector3f(-7699, 8632, 22),
                p3_end = new Vector3f(-7699, 8632, 600)
            });

            platformSpawns.Add(new PlatformSpawner() {
                p1_start = new Vector3f(-7699, 9554, -151),
                p1_end = new Vector3f(-7699, 9554, 567),
                p2_start = new Vector3f(-7700, 9100, 700),
                p3_start = new Vector3f(-8127, 8666, 892),
                p3_end = new Vector3f(-7254, 8680, 892)
            });

            platformSpawns.Add(new PlatformSpawner() {
                p1_start = new Vector3f(-7135, 9279, 314),
                p1_end = new Vector3f(-7996, 9547, 314),
                p2_start = new Vector3f(-7700, 9100, 700),
                p3_start = new Vector3f(-7202, 8610, 463),
                p3_end = new Vector3f(-8097, 8889, 463)
            });

            // platforms 
            platforms.Add(new CVPlatform(new DeepPointer(PtrDB.DP_BlinkCV_Platform1)));
            platforms.Add(new CVPlatform(new DeepPointer(PtrDB.DP_BlinkCV_Platform2)));
            platforms.Add(new CVPlatform(new DeepPointer(PtrDB.DP_BlinkCV_Platform3)));

            // default pos
            platformDefaultPositions.Add(platforms[0].GetMemoryPos(GameHook.game));
            platformDefaultPositions.Add(platforms[1].GetMemoryPos(GameHook.game));
            platformDefaultPositions.Add(platforms[2].GetMemoryPos(GameHook.game));

            // set second patrol to same pos (stop platforms)
            for (int i = 0; i < platforms.Count; i++) {
                platforms[i].Pos = platformDefaultPositions[i];
                platforms[i].EndPoint = platformDefaultPositions[i];
                platforms[i].WriteMemory(GameHook.game);
            }
        }

        public override void RandomizeEnemies(Process game) {
            //// Room 1 ////
            Vector3f pos1 = new Vector3f(-7550, 23550, 390);
            Vector3f pos2 = new Vector3f(-7800, 23040, 390);
            Vector3f pos3 = new Vector3f(-7650, 22555, 390);

            pos1.X = -7950 + Config.GetInstance().r.Next(0, 500);
            pos3.X = -7950 + Config.GetInstance().r.Next(0, 500);
            pos2.X = (pos1.X + pos3.X) / 2 + Config.GetInstance().r.Next(-100, 100);

            Enemies_Room1[0].SetMemoryPos(game, new SpawnData(pos1));
            Enemies_Room1[1].SetMemoryPos(game, new SpawnData(pos2));
            Enemies_Room1[2].SetMemoryPos(game, new SpawnData(pos3));

            //// room 2 ////
            SpawnPlane middlePlane = new SpawnPlane(new Vector3f(-8176, 16164, 407), new Vector3f(-7138, 15286, 398));
            Vector3f pos5 = middlePlane.GetRandomSpawnData().pos;

            float phi = (float)(Config.GetInstance().r.Next(0, 180) / Math.PI * 180);
            Vector3f pos4 = new Vector3f(pos5.X - 560 * (float)Math.Cos(phi), pos5.Y + 560 * (float)Math.Sin(phi), 408);
            Vector3f pos6 = new Vector3f(pos5.X + 560 * (float)Math.Cos(phi), pos5.Y - 560 * (float)Math.Sin(phi), 408);

            Enemies_Room2[0].SetMemoryPos(game, new SpawnData(pos4));
            Enemies_Room2[1].SetMemoryPos(game, new SpawnData(pos5));
            Enemies_Room2[2].SetMemoryPos(game, new SpawnData(pos6));

            //// room 3 - platforms ////
            if (FirstPlatformsRngFlag) {
                int spawnIndex = Config.GetInstance().r.Next(platformSpawns.Count);

                //asign platform values
                platforms[0].Pos = platformSpawns[spawnIndex].p1_start;
                platforms[0].EndPoint = platformSpawns[spawnIndex].p1_end;
                platforms[0].WriteMemory(GameHook.game);

                platforms[1].Pos = platformSpawns[spawnIndex].p2_start;
                platforms[1].WriteMemory(GameHook.game);

                platforms[2].Pos = platformSpawns[spawnIndex].p3_start;
                platforms[2].EndPoint = platformSpawns[spawnIndex].p3_end;
                platforms[2].WriteMemory(GameHook.game);

                // asign enemies based on platform rng
                Enemies_Room3[0].SetMemoryPos(game, new SpawnData(platformSpawns[spawnIndex].p1_start + new Vector3f(0, -25, 110)));
                Enemies_Room3[1].SetMemoryPos(game, new SpawnData(platformSpawns[spawnIndex].p2_start + new Vector3f(0, -25, 110)));
                Enemies_Room3[2].SetMemoryPos(game, new SpawnData(platformSpawns[spawnIndex].p3_start + new Vector3f(0, -25, 110)));
            }
        }

        private bool FirstPlatformsRngFlag = false;
        // Trigger Volume for Platforms
        private Vector3f cornerA = new Vector3f(-10817, 19596, 1975);
        private Vector3f cornerB = new Vector3f(-3712, 21937, -1790);

        public override void UpdateMap(Vector3f Player) {
            base.UpdateMap(Player);

            if (!FirstPlatformsRngFlag && PlayerInVolume(Player)) {
                FirstPlatformsRngFlag = true;

                // rng
                int spawnIndex = Config.GetInstance().r.Next(platformSpawns.Count);

                //asign platform values
                platforms[0].Pos = platformSpawns[spawnIndex].p1_start;
                platforms[0].EndPoint = platformSpawns[spawnIndex].p1_end;
                platforms[0].WriteMemory(GameHook.game);

                platforms[1].Pos = platformSpawns[spawnIndex].p2_start;
                platforms[1].WriteMemory(GameHook.game);

                platforms[2].Pos = platformSpawns[spawnIndex].p3_start;
                platforms[2].EndPoint = platformSpawns[spawnIndex].p3_end;
                platforms[2].WriteMemory(GameHook.game);

                // asign enemies based on platform rng
                Enemies_Room3[0].SetMemoryPos(GameHook.game, new SpawnData(platformSpawns[spawnIndex].p1_start + new Vector3f(0, -25, 110)));
                Enemies_Room3[1].SetMemoryPos(GameHook.game, new SpawnData(platformSpawns[spawnIndex].p2_start + new Vector3f(0, -25, 110)));
                Enemies_Room3[2].SetMemoryPos(GameHook.game, new SpawnData(platformSpawns[spawnIndex].p3_start + new Vector3f(0, -25, 110)));
            }
        }

        private bool PlayerInVolume(Vector3f player) {
            return (player.X >= Math.Min(cornerA.X, cornerB.X) && player.X <= Math.Max(cornerA.X, cornerB.X) &&
               player.Y >= Math.Min(cornerA.Y, cornerB.Y) && player.Y <= Math.Max(cornerA.Y, cornerB.Y) &&
               player.Z >= Math.Min(cornerA.Z, cornerB.Z) && player.Z <= Math.Max(cornerA.Z, cornerB.Z));
        }

        private struct PlatformSpawner {
            public Vector3f p1_start, p1_end;
            public Vector3f p2_start;
            public Vector3f p3_start, p3_end;
        }
    }
}
