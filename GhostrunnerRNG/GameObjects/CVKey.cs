using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using System;
using System.Diagnostics;

namespace GhostrunnerRNG.GameObjects {
    public class CVKey : WorldObject {

        private Vector3f BoxOffset = new Vector3f(327.5f, 335f, 247.5f);
        private EasyPointers DP = new EasyPointers();
        private const float ZOffset = 120;

        public CVKey(int hologramOffset, int centerOffset, int boxOffset) : base(null){
            DP.Pointers.Add("Hologram", new Tuple<DeepPointer, IntPtr>(new DeepPointer(0x04609420, 0x30, 0xA8, hologramOffset, 0x130, 0x1D0), IntPtr.Zero));
            DP.Pointers.Add("Center", new Tuple<DeepPointer, IntPtr>(new DeepPointer(0x04609420, 0x30, 0xA8, centerOffset, 0x220, 0x398, 0x150), IntPtr.Zero));
            DP.Pointers.Add("Box", new Tuple<DeepPointer, IntPtr>(new DeepPointer(0x04609420, 0x1F8, 0x60, 0xD0, 0x8C0, 0xB0, 0x5A0, 0x1A8, boxOffset), IntPtr.Zero));
        }

        public override void SetMemoryPos(Process game, SpawnData spawnData) {
            DerefPointer(game);

            game.WriteBytes(DP.Pointers["Hologram"].Item2, BitConverter.GetBytes((float)spawnData.pos.X));
            game.WriteBytes(DP.Pointers["Hologram"].Item2 + 4, BitConverter.GetBytes((float)spawnData.pos.Y));
            game.WriteBytes(DP.Pointers["Hologram"].Item2 + 8, BitConverter.GetBytes((float)spawnData.pos.Z + ZOffset));

            // Center
            game.WriteBytes(DP.Pointers["Center"].Item2, BitConverter.GetBytes((float)spawnData.pos.X));
            game.WriteBytes(DP.Pointers["Center"].Item2 + 4, BitConverter.GetBytes((float)spawnData.pos.Y));
            game.WriteBytes(DP.Pointers["Center"].Item2 + 8, BitConverter.GetBytes((float)spawnData.pos.Z + ZOffset));

            // Hitbox
            game.WriteBytes(DP.Pointers["Box"].Item2, BitConverter.GetBytes((float)spawnData.pos.X - BoxOffset.X * 1.01f));
            game.WriteBytes(DP.Pointers["Box"].Item2 + 4, BitConverter.GetBytes((float)spawnData.pos.Y - BoxOffset.Y * 1.01f));
            game.WriteBytes(DP.Pointers["Box"].Item2 + 8, BitConverter.GetBytes((float)(spawnData.pos.Z + ZOffset) - BoxOffset.Z * 1.01f));
            game.WriteBytes(DP.Pointers["Box"].Item2 + 12, BitConverter.GetBytes((float)spawnData.pos.X + BoxOffset.X * 1.01f));
            game.WriteBytes(DP.Pointers["Box"].Item2 + 16, BitConverter.GetBytes((float)spawnData.pos.Y + BoxOffset.Y * 1.01f));
            game.WriteBytes(DP.Pointers["Box"].Item2 + 20, BitConverter.GetBytes((float)(spawnData.pos.Z + ZOffset) + BoxOffset.Z * 1.01f));
        }

        protected override void DerefPointer(Process game) {
            DP.DerefPointers(game);
        }
    }
}
