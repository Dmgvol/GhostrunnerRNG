using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.MemoryUtils;
using System;
using System.Diagnostics;

namespace GhostrunnerRNG.GameObjects {
    class CVOrb : WorldObject{
        // Box/Hitbox offset
        private Vector3f BoxOffset = new Vector3f(151.5f, 151.5f, 101f);
        // pointers
        private DeepPointer boxOriginDP, boxDP;
        private IntPtr boxOriginPtr, boxPtr;

        /// <summary>
        /// RiH CV orb constructor
        /// </summary>
        public CVOrb(int orbOffset, int boxSecondOffset) : base(new DeepPointer(PtrDB.DP_CVOrb).Format(orbOffset)) {
            boxOriginDP = new DeepPointer(PtrDB.DP_CVOrb_BoxOrigin).Format(orbOffset);
            boxDP = new DeepPointer(PtrDB.DP_CVOrb_Box).Format(boxSecondOffset);
        }

        /// <summary>
        /// Tempest CV orb constructor
        /// </summary>
        public CVOrb(int orbOffset, int boxFirstOffset, int boxSecondOffset) : base(new DeepPointer(PtrDB.DP_CVOrb).Format(orbOffset)) {
            boxOriginDP = new DeepPointer(PtrDB.DP_CVOrb_BoxOrigin).Format(orbOffset);
            boxDP = new DeepPointer(PtrDB.DP_CVOrb_Box_RiH).Format(boxFirstOffset, boxSecondOffset);
        }

        /// <summary>
        /// Echoes CV
        /// </summary>
        /// <param name="n">orb index</param>
        public CVOrb(int n) : base(new DeepPointer(PtrDB.DP_CVOrb_Echoes).ModifyOffset(2, 0x10 + 0x8 * (n - 1))) {}

        public override void SetMemoryPos(Process game, SpawnData spawnData) {
            DerefPointer(game);
            base.SetMemoryPos(game, spawnData);
            // hitbox origin
            if(boxOriginPtr != IntPtr.Zero) {
                game.WriteBytes(boxOriginPtr, BitConverter.GetBytes(spawnData.pos.X));
                game.WriteBytes(boxOriginPtr + 4 , BitConverter.GetBytes(spawnData.pos.Y));
                game.WriteBytes(boxOriginPtr + 8 , BitConverter.GetBytes(spawnData.pos.Z));
            }
            // Corners of hitbox
            if(boxPtr != IntPtr.Zero) {
                game.WriteBytes(boxPtr, BitConverter.GetBytes(spawnData.pos.X - BoxOffset.X));
                game.WriteBytes(boxPtr + 4, BitConverter.GetBytes(spawnData.pos.Y - BoxOffset.Y));
                game.WriteBytes(boxPtr + 8, BitConverter.GetBytes(spawnData.pos.Z - BoxOffset.Z));
                game.WriteBytes(boxPtr + 12, BitConverter.GetBytes(spawnData.pos.X + BoxOffset.X));
                game.WriteBytes(boxPtr + 16, BitConverter.GetBytes(spawnData.pos.Y + BoxOffset.Y));
                game.WriteBytes(boxPtr + 20, BitConverter.GetBytes(spawnData.pos.Z + BoxOffset.Z));
            }
        }
        protected override void DerefPointer(Process game) {
            base.DerefPointer(game);
            if(boxDP != null) boxDP.DerefOffsets(game, out boxPtr);
            if(boxOriginDP != null) boxOriginDP.DerefOffsets(game, out boxOriginPtr);
        }
    }
}
