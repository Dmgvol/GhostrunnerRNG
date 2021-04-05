using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using System;
using System.Diagnostics;

namespace GhostrunnerRNG.Enemies {
    class CVOrb : Enemy{
        // Box/Hitbox offset
        private Vector3f BoxOffset = new Vector3f(151.5f, 151.5f, 101f);
        // pointers
        private DeepPointer boxOriginDP, boxDP;
        private IntPtr boxOriginPtr, boxPtr;

        /// <summary>
        /// RiH CV orb constructor
        /// </summary>
        public CVOrb(int orbOffset, int boxSecondOffset) : base(new DeepPointer(0x045A3C20, 0x30, 0xA8, orbOffset, 0x248, 0x1D0)) {
            boxOriginDP = new DeepPointer(0x045A3C20, 0x30, 0xA8, orbOffset, 0x238, 0x398, 0x150);
            boxDP = new DeepPointer(0x045A3C20, 0x1F8, 0x60, 0xD0, 0x8C0, 0xB0, 0x5A0, 0x1A8, boxSecondOffset);
        }

        /// <summary>
        /// Tempest CV orb constructor
        /// </summary>
        public CVOrb(int orbOffset, int boxFirstOffset, int boxSecondOffset) : base(new DeepPointer(0x045A3C20, 0x30, 0xA8, orbOffset, 0x248, 0x1D0)) {
            
            boxOriginDP = new DeepPointer(0x045A3C20, 0x30, 0xA8, orbOffset, 0x238, 0x398, 0x150);
            boxDP = new DeepPointer(0x045A3C20, 0x1F8, 0x60, 0xD0, 0x298, boxFirstOffset, 0xB0, 0x5A0, 0x1A8, boxSecondOffset);
        }

        public override void SetMemoryPos(Process game, SpawnData spawnData) {
            base.SetMemoryPos(game, spawnData);
            // hitbox origin
            game.WriteBytes(boxOriginPtr, BitConverter.GetBytes(spawnData.pos.X));
            game.WriteBytes(boxOriginPtr + 4 , BitConverter.GetBytes(spawnData.pos.Y));
            game.WriteBytes(boxOriginPtr + 8 , BitConverter.GetBytes(spawnData.pos.Z));
            // Corners of hitbox
            game.WriteBytes(boxPtr, BitConverter.GetBytes(spawnData.pos.X - BoxOffset.X));
            game.WriteBytes(boxPtr + 4, BitConverter.GetBytes(spawnData.pos.Y - BoxOffset.Y));
            game.WriteBytes(boxPtr + 8, BitConverter.GetBytes(spawnData.pos.Z - BoxOffset.Z));
            game.WriteBytes(boxPtr + 12, BitConverter.GetBytes(spawnData.pos.X + BoxOffset.X));
            game.WriteBytes(boxPtr + 16, BitConverter.GetBytes(spawnData.pos.Y + BoxOffset.Y));
            game.WriteBytes(boxPtr + 20, BitConverter.GetBytes(spawnData.pos.Z + BoxOffset.Z));
        }
        protected override void DerefPointer(Process game) {
            base.DerefPointer(game);
            boxDP.DerefOffsets(game, out boxPtr);
            boxOriginDP.DerefOffsets(game, out boxOriginPtr);
        }
    }
}
