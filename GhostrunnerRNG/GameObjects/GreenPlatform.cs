using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.MemoryUtils;
using System;
using System.Diagnostics;

namespace GhostrunnerRNG.GameObjects {
    class GreenPlatform : WorldObject{

        EasyPointers EP = new EasyPointers();

        private const float SphereR = 1579.342407f;

        public GreenPlatform(int offset, int boxOffset) : base(new DeepPointer(PtrDB.DP_LookInsideCV_GreenPlatform).ModifyOffset(2, offset)) {
            EP.Add("SphereCenter", new DeepPointer(PtrDB.DP_LookInsideCV_GreenPlatform_SphereCenter).ModifyOffset(2, offset));
            EP.Add("SphereBox", new DeepPointer(PtrDB.DP_LookInsideCV_GreenPlatform_SphereBox).ModifyOffset(7, boxOffset));
            EP.Add("OldCollision", new DeepPointer(PtrDB.DP_LookInsideCV_GreenPlatform_OldCollision).ModifyOffset(2, offset));

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
