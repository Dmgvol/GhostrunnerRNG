using GhostrunnerRNG.MapGen;
using System;
using System.Diagnostics;

namespace GhostrunnerRNG.Maps {
    class RoadToAmidaCV : MapCore {
        public RoadToAmidaCV() : base(Game.GameUtils.MapType.RoadToAmidaCV) {
            Gen_PerRoom();
        }

        AmidaCVPlatform platform;


        protected override void Gen_PerRoom() {
            platform = new AmidaCVPlatform(0x268, 0x4230);
        }

        public override void RandomizeEnemies(Process game) {
            platform.SetMemoryPos(game, null);
        }

    }
}
