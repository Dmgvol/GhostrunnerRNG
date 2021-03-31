using GhostrunnerRNG.Game;
using System;
using System.Diagnostics;

namespace GhostrunnerRNG.MapGen {
    class AmidaCVPlatform : WorldObject{
        // Box/hitbox offset
        public static Vector3f PlatformBoxOffset { get; private set; } = new Vector3f(303, 303, 5.05f);
        // pointers
        private DeepPointer PlatformDP, PlatformBoxCenterDP, BoxDP;
        private IntPtr PlatformPtr, PlatformBoxCenterPtr, BoxPtr;

        public AmidaCVPlatform(int firstOffset, int secondOffset) : base(null){
            PlatformDP = new DeepPointer(0x045A3C20, 0x210, firstOffset, 0x1D0); // visual center
            PlatformBoxCenterDP = new DeepPointer(0x045A3C20, 0x210, firstOffset, 0x398 , 0xA0);// box center
            BoxDP = new DeepPointer(0x045A3C20, 0x1F8, 0x60, 0xD0, 0x8C0, 0xB0, 0x570, 0x1A8, secondOffset); // collision/box center coords
        }

        public override void SetMemoryPos(Process game, SpawnData spawnData) {
            DerefPointer(game);

            // visual center
            game.WriteBytes(PlatformPtr, BitConverter.GetBytes(spawnData.pos.X));
            game.WriteBytes(PlatformPtr + 4, BitConverter.GetBytes(spawnData.pos.Y));
            game.WriteBytes(PlatformPtr + 8, BitConverter.GetBytes(spawnData.pos.Z));

            // box center
            game.WriteBytes(PlatformBoxCenterPtr, BitConverter.GetBytes(spawnData.pos.X));
            game.WriteBytes(PlatformBoxCenterPtr + 4, BitConverter.GetBytes(spawnData.pos.Y));
            game.WriteBytes(PlatformBoxCenterPtr + 8, BitConverter.GetBytes(spawnData.pos.Z));

            // Corners of hitbox
            game.WriteBytes(BoxPtr, BitConverter.GetBytes(spawnData.pos.X - PlatformBoxOffset.X));
            game.WriteBytes(BoxPtr + 4, BitConverter.GetBytes(spawnData.pos.Y - PlatformBoxOffset.Y));
            game.WriteBytes(BoxPtr + 8, BitConverter.GetBytes(spawnData.pos.Z - PlatformBoxOffset.Z));
            game.WriteBytes(BoxPtr + 12, BitConverter.GetBytes(spawnData.pos.X + PlatformBoxOffset.X));
            game.WriteBytes(BoxPtr + 16, BitConverter.GetBytes(spawnData.pos.Y + PlatformBoxOffset.Y));
            game.WriteBytes(BoxPtr + 20, BitConverter.GetBytes(spawnData.pos.Z + PlatformBoxOffset.Z));
        }

        protected override void DerefPointer(Process game) {
            PlatformDP.DerefOffsets(game, out PlatformPtr);
            PlatformBoxCenterDP.DerefOffsets(game, out PlatformBoxCenterPtr);
            BoxDP.DerefOffsets(game, out BoxPtr);
        }
    }
}
