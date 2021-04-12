using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using System;
using System.Diagnostics;

namespace GhostrunnerRNG.GameObjects {
    class GreenPlatform : WorldObject{

        EasyPointers EP = new EasyPointers();

        public GreenPlatform(int offset) : base(new DeepPointer(0x045A3C20, 0x30, 0xA8, offset, 0x130, 0x1D0)) {
            EP.Pointers.Add("SphereCenter", new Tuple<DeepPointer, IntPtr>(new DeepPointer(0x045A3C20, 0x30, 0xA8, offset, 0x230, 0x398, 0x150), IntPtr.Zero));
        }

        public override void SetMemoryPos(Process game, SpawnData spawnData) {
            base.SetMemoryPos(game, spawnData);

            // Update Sphere center (player detection sphere around platform) 
            game.WriteBytes(EP.Pointers["SphereCenter"].Item2, BitConverter.GetBytes((float)spawnData.pos.X));
            game.WriteBytes(EP.Pointers["SphereCenter"].Item2 + 4, BitConverter.GetBytes((float)spawnData.pos.Y));
            game.WriteBytes(EP.Pointers["SphereCenter"].Item2 + 8, BitConverter.GetBytes((float)spawnData.pos.Z));
        }

        protected override void DerefPointer(Process game) {
            base.DerefPointer(game);
            EP.DerefPointers(game);
        }
    }
}
