using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GhostrunnerRNG.Maps {
    class RoadToAmidaCV : MapCore {

        private List<AmidaCVPlatform> platforms = new List<AmidaCVPlatform>();
        private List<int[,]> layouts = new List<int[,]>();
        private readonly Vector3f Anchor = new Vector3f(-19156, -14750, 21800);

        public RoadToAmidaCV() : base(GameUtils.MapType.RoadToAmidaCV, manualGen: true) {
            Gen_PerRoom();
            CPRequired = false;
        }

        protected override void Gen_PerRoom() {
            // Platforms
            platforms.Add(new AmidaCVPlatform(0x268, 0x4230));
            platforms.Add(new AmidaCVPlatform(0x260, 0x4200));
            platforms.Add(new AmidaCVPlatform(0x258, 0x41d0));
            platforms.Add(new AmidaCVPlatform(0x250, 0x41A0));
            platforms.Add(new AmidaCVPlatform(0x248, 0x44A0));
            platforms.Add(new AmidaCVPlatform(0x240, 0x4170));
            platforms.Add(new AmidaCVPlatform(0x238, 0x4140));

            // Layouts
            layouts.Add(new int[5, 5] {
                { 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0},
                { 0, 0, 0, 1, 1},
                { 0, 1, 1, 1, 0},
                { 1, 1, 0, 0, 0}});

            layouts.Add(new int[5, 5] {
                { 0, 0, 0, 0, 0},
                { 0, 0, 0, 1, 1},
                { 0, 0, 1, 0, 0},
                { 1, 1, 0, 0, 0},
                { 0, 0, 0, 0, 0}});

            layouts.Add(new int[5, 5] {
                { 1, 0, 0, 0, 0},
                { 1, 1, 1, 0, 0},
                { 0, 0, 1, 1, 1},
                { 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0}});

            layouts.Add(new int[5, 5] {
                { 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0},
                { 0, 1, 1, 1, 0},
                { 1, 1, 0, 1, 1}});

            layouts.Add(new int[5, 5] {
                { 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0},
                { 1, 1, 0, 0, 0},
                { 0, 0, 1, 0, 0},
                { 0, 0, 1, 1, 1}});

            layouts.Add(new int[5, 5] {
                { 0, 1, 0, 1, 0},
                { 0, 0, 0, 0, 0},
                { 1, 0, 1, 0, 1},
                { 0, 1, 0, 1, 0},
                { 0, 0, 0, 0, 0}});

            layouts.Add(new int[5, 5] {
                { 1, 0, 1, 0, 1},
                { 0, 1, 0, 1, 0},
                { 0, 0, 1, 0, 1},
                { 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0}});

            layouts.Add(new int[5, 5] {
                { 0, 0, 0, 0, 0},
                { 0, 0, 1, 1, 0},
                { 1, 1, 0, 0, 1},
                { 0, 0, 1, 1, 0},
                { 0, 0, 0, 0, 0}});

            layouts.Add(new int[5, 5] {
                { 1, 1, 0, 0, 0},
                { 0, 1, 0, 1, 1},
                { 0, 1, 1, 0, 0},
                { 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0}});

            layouts.Add(new int[5, 5] {
                { 0, 0, 0, 0, 0},
                { 0, 1, 1, 1, 1},
                { 0, 1, 0, 0, 0},
                { 0, 1, 0, 0, 0},
                { 1, 0, 0, 0, 0}});

            layouts.Add(new int[5, 5] {
                { 1, 0, 1, 0, 0},
                { 0, 0, 1, 0, 0},
                { 0, 0, 1, 0, 0},
                { 0, 0, 1, 0, 0},
                { 0, 0, 1, 0, 1}});

            layouts.Add(new int[5, 5] {
                { 0, 0, 0, 0, 0},
                { 0, 0, 0, 1, 0},
                { 0, 0, 1, 0, 0},
                { 1, 0, 1, 1, 1},
                { 0, 1, 0, 0, 0}});

            layouts.Add(new int[5, 5] {
                { 0, 0, 0, 0, 0},
                { 0, 0, 1, 0, 0},
                { 1, 1, 1, 1, 1},
                { 0, 0, 1, 0, 0},
                { 0, 0, 0, 0, 0}});

            layouts.Add(new int[5, 5] {
                { 1, 0, 0, 0, 1},
                { 0, 1, 1, 1, 0},
                { 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0},
                { 1, 0, 0, 0, 1}});

            layouts.Add(new int[5, 5] {
                { 0, 0, 1, 0, 0},
                { 1, 1, 1, 0, 0},
                { 0, 0, 0, 1, 0},
                { 0, 0, 0, 0, 1},
                { 0, 0, 0, 0, 0}});

            layouts.Add(new int[5, 5] {
                { 0, 0, 0, 0, 0},
                { 0, 0, 1, 0, 1},
                { 0, 1, 0, 1, 0},
                { 1, 0, 0, 0, 1},
                { 0, 0, 0, 0, 1}});

            layouts.Add(new int[5, 5] {
                { 0, 0, 0, 0, 0},
                { 1, 1, 0, 0, 0},
                { 0, 1, 0, 0, 0},
                { 0, 0, 1, 0, 1},
                { 0, 0, 1, 0, 1}});

            layouts.Add(new int[5, 5] {
                { 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0},
                { 0, 1, 1, 1, 0},
                { 1, 0, 0, 0, 1},
                { 1, 0, 0, 0, 1}});
        }

        private void RandomizePlatforms(Process game) {
            int[] arr = new int[5 * 5];
            for(int i = 0; i < arr.Length; i++) arr[i] = i;

            var lst = arr.OrderBy(x => Config.GetInstance().r.Next()).Take(platforms.Count).ToList();
            for(int i = 0; i < platforms.Count; i++) {
                int row = lst[i] / 5;
                int colmn = lst[i] % 5;
                Vector3f vec = new Vector3f(
                    Anchor.X + (AmidaCVPlatform.PlatformBoxOffset.X * row * 2),
                    Anchor.Y - (AmidaCVPlatform.PlatformBoxOffset.Y * colmn * 2),
                    Anchor.Z);
                platforms[i].SetMemoryPos(game, new SpawnData(vec));
            }
        }

        public override void RandomizeEnemies(Process game) {
            int selectedLayoutIndex = Config.GetInstance().r.Next(layouts.Count);
            int currPlatform = 0;
            for(int i = 0; i < 5; i++) {
                for(int j = 0; j < 5; j++) {
                    if(currPlatform >= 7) break;
                    if(layouts[selectedLayoutIndex][i, j] == 1) {
                        Vector3f vec = new Vector3f(
                            Anchor.X + (AmidaCVPlatform.PlatformBoxOffset.X * i * 2),
                            Anchor.Y - (AmidaCVPlatform.PlatformBoxOffset.Y * j * 2),
                            Anchor.Z);
                        platforms[currPlatform].SetMemoryPos(game, new SpawnData(vec));
                        currPlatform++;
                    }
                }
            }
        }
    }
}
