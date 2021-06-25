using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.MemoryUtils;
using System;
using System.Diagnostics;

namespace GhostrunnerRNG.GameObjects {
    class AmidaCVPlatform : WorldObject{
        // Box/hitbox offset
        public static Vector3f PlatformBoxOffset { get; private set; } = new Vector3f(303, 303, 5.05f);
        // pointers
        private DeepPointer PlatformDP, PlatformBoxCenterDP, BoxDP;
        private IntPtr PlatformPtr, PlatformBoxCenterPtr, BoxPtr;

        public AmidaCVPlatform(int firstOffset, int secondOffset) : base(null){
            PlatformDP = new DeepPointer(PtrDB.DP_AmidaCV_Platform).ModifyOffset(1, firstOffset); // visual center
            PlatformBoxCenterDP = new DeepPointer(PtrDB.DP_AmidaCV_PlatformBoxCenter).ModifyOffset(1, firstOffset); // box center
            BoxDP = new DeepPointer(PtrDB.DP_AmidaCV_Box).ModifyOffset(7, secondOffset); // collision/box center coords
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
