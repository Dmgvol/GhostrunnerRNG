using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using System;
using System.Diagnostics;

namespace GhostrunnerRNG.GameObjects {
    class GreenPlatform : WorldObject{

        EasyPointers EP = new EasyPointers();

        private const float SphereR = 1579.342407f;

        public GreenPlatform(int offset, int boxOffset) : base(new DeepPointer(0x04609420, 0x30, 0xA8, offset, 0x130, 0x1D0)) {
            EP.Add("SphereCenter",new DeepPointer(0x04609420, 0x30, 0xA8, offset, 0x230, 0x398, 0x150));
            EP.Add("SphereBox", new DeepPointer(0x04609420, 0x1F8, 0x60, 0xD0, 0x8C0, 0xB0, 0x5A0, 0x1A8, boxOffset));
            EP.Add("OldCollision", new DeepPointer(0x04609420, 0x30, 0xA8, offset, 0x238, 0x398, 0x150));
        }

        public override void SetMemoryPos(Process game, SpawnData spawnData) {
            base.SetMemoryPos(game, spawnData);

            // Update Sphere center (player detection sphere around platform) 
            game.WriteBytes(EP.Pointers["SphereCenter"].Item2, BitConverter.GetBytes((float)spawnData.pos.X));
            game.WriteBytes(EP.Pointers["SphereCenter"].Item2 + 4, BitConverter.GetBytes((float)spawnData.pos.Y));
            game.WriteBytes(EP.Pointers["SphereCenter"].Item2 + 8, BitConverter.GetBytes((float)spawnData.pos.Z));

            // SphereBox
            game.WriteBytes(EP.Pointers["SphereBox"].Item2, BitConverter.GetBytes((float)spawnData.pos.X - (SphereR * 1.01f)));
            game.WriteBytes(EP.Pointers["SphereBox"].Item2 + 4, BitConverter.GetBytes((float)spawnData.pos.Y - (SphereR * 1.01f)));
            game.WriteBytes(EP.Pointers["SphereBox"].Item2 + 8, BitConverter.GetBytes((float)spawnData.pos.Z - (SphereR * 1.01f)));
            game.WriteBytes(EP.Pointers["SphereBox"].Item2 + 12, BitConverter.GetBytes((float)spawnData.pos.X + (SphereR * 1.01f)));
            game.WriteBytes(EP.Pointers["SphereBox"].Item2 + 16, BitConverter.GetBytes((float)spawnData.pos.Y + (SphereR * 1.01f)));
            game.WriteBytes(EP.Pointers["SphereBox"].Item2 + 20, BitConverter.GetBytes((float)spawnData.pos.Z + (SphereR * 1.01f)));

            // Old collision  - remove
            game.WriteBytes(EP.Pointers["OldCollision"].Item2, BitConverter.GetBytes((float)0));

        }

        protected override void DerefPointer(Process game) {
            base.DerefPointer(game);
            EP.DerefPointers(game);
        }
    }
}
