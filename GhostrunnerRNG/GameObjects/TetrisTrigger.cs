using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using System;
using System.Diagnostics;

namespace GhostrunnerRNG.GameObjects {
    class TetrisTrigger : WorldObject {
        // Box/Hitbox offset
        private Vector3f BoxOffset = new Vector3f(101f, 101f, 40.4f);
        private EasyPointers EP = new EasyPointers();

        public TetrisTrigger(int hologramOffset, int centerOffset, int boxOffset, int particlesOffset) {
            EP.Add("Hologram", new DeepPointer(0x04609420, 0x30, 0xA8, hologramOffset, 0x230, 0x1D0));
            EP.Add("CenterBox", new DeepPointer(0x04609420, 0x30, 0xA8, centerOffset, 0x220, 0x398, 0x150));
            EP.Add("Box", new DeepPointer(0x04609420, 0x1F8, 0x60, 0xD0, 0x8C0, 0xB0, 0x5A0, 0x1A8, boxOffset));
            EP.Add("Particles", new DeepPointer(0x04609420, 0x30, 0xA8, particlesOffset, 0x130, 0x1D0));
        }

        public override void SetMemoryPos(Process game, SpawnData spawnData) {
            EP.DerefPointers(game);

            game.WriteBytes(EP.Pointers["Hologram"].Item2, BitConverter.GetBytes((float)spawnData.pos.X));
            game.WriteBytes(EP.Pointers["Hologram"].Item2 + 4, BitConverter.GetBytes((float)spawnData.pos.Y));
            game.WriteBytes(EP.Pointers["Hologram"].Item2 + 8, BitConverter.GetBytes((float)spawnData.pos.Z));

            // Particles
            game.WriteBytes(EP.Pointers["Particles"].Item2, BitConverter.GetBytes((float)spawnData.pos.X));
            game.WriteBytes(EP.Pointers["Particles"].Item2 + 4, BitConverter.GetBytes((float)spawnData.pos.Y));
            game.WriteBytes(EP.Pointers["Particles"].Item2 + 8, BitConverter.GetBytes((float)spawnData.pos.Z));

            // CenterBox
            game.WriteBytes(EP.Pointers["CenterBox"].Item2, BitConverter.GetBytes((float)spawnData.pos.X));
            game.WriteBytes(EP.Pointers["CenterBox"].Item2 + 4, BitConverter.GetBytes((float)spawnData.pos.Y));
            game.WriteBytes(EP.Pointers["CenterBox"].Item2 + 8, BitConverter.GetBytes((float)spawnData.pos.Z));
            // Hitbox
            game.WriteBytes(EP.Pointers["Box"].Item2, BitConverter.GetBytes((float)spawnData.pos.X - BoxOffset.X));
            game.WriteBytes(EP.Pointers["Box"].Item2 + 4, BitConverter.GetBytes((float)spawnData.pos.Y - BoxOffset.Y));
            game.WriteBytes(EP.Pointers["Box"].Item2 + 8, BitConverter.GetBytes((float)spawnData.pos.Z - BoxOffset.Z));
            game.WriteBytes(EP.Pointers["Box"].Item2 + 12, BitConverter.GetBytes((float)spawnData.pos.X + BoxOffset.X));
            game.WriteBytes(EP.Pointers["Box"].Item2 + 16, BitConverter.GetBytes((float)spawnData.pos.Y + BoxOffset.Y));
            game.WriteBytes(EP.Pointers["Box"].Item2 + 20, BitConverter.GetBytes((float)spawnData.pos.Z + BoxOffset.Z));
        }
    }
}
