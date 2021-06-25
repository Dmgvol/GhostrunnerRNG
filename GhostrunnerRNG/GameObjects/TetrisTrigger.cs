using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.MemoryUtils;
using System;
using System.Diagnostics;

namespace GhostrunnerRNG.GameObjects {
    class TetrisTrigger : WorldObject {
        // Box/Hitbox offset
        private Vector3f BoxOffset = new Vector3f(101f, 101f, 40.4f);
        private EasyPointers EP = new EasyPointers();

        public TetrisTrigger(int hologramOffset, int centerOffset, int boxOffset, int particlesOffset) {
            EP.Add("Hologram", new DeepPointer(PtrDB.DP_ClimbCV_Tetris_Hologram).ModifyOffset(2, hologramOffset));
            EP.Add("CenterBox", new DeepPointer(PtrDB.DP_ClimbCV_Tetris_CenterBox).ModifyOffset(2, centerOffset));
            EP.Add("Box", new DeepPointer(PtrDB.DP_ClimbCV_Tetris_Box).ModifyOffset(7, boxOffset));
            EP.Add("Particles", new DeepPointer(PtrDB.DP_ClimbCV_Tetris_Particles).ModifyOffset(2, particlesOffset));
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
