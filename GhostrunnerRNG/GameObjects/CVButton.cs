using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using System;
using System.Diagnostics;

namespace GhostrunnerRNG.GameObjects {
    class CVButton : WorldObject {

        private EasyPointers EP = new EasyPointers();
        private Vector3f BoxSize1 = new Vector3f(116.352f, 116.352f, 38.784f);
        private Vector3f BoxSize2 = new Vector3f(119.937988f, 126.250576f, 60.6f);
        private bool IsBoxSizeOne;

        /// <summary>
        /// CV Button, for BreatheIn CV
        /// </summary>
        public CVButton(int offset, int boxOffset, bool IsBoxSizeOne) : base(new DeepPointer(0x04609420, 0x30, 0xA8, offset, 0x248, 0x398, 0x150)){
            // pointers
            EP.Add("Box", new DeepPointer(0x04609420, 0x1F8, 0x60, 0xD0, 0x8C0, 0xB0, 0x5A0, 0x1A8, boxOffset));
            this.IsBoxSizeOne = IsBoxSizeOne;
        }

        public override void SetMemoryPos(Process game, SpawnData spawnData) {
            base.SetMemoryPos(game, spawnData);
            EP.DerefPointers(game);
            // box boundaries
            game.WriteBytes(EP.Pointers["Box"].Item2, BitConverter.GetBytes(spawnData.pos.X - (IsBoxSizeOne ? BoxSize1.X : BoxSize2.X)));
            game.WriteBytes(EP.Pointers["Box"].Item2 + 4, BitConverter.GetBytes(spawnData.pos.Y - (IsBoxSizeOne ? BoxSize1.Y : BoxSize2.Y)));
            game.WriteBytes(EP.Pointers["Box"].Item2 + 8, BitConverter.GetBytes(spawnData.pos.Z - (IsBoxSizeOne ? BoxSize1.Z : BoxSize2.Z)));
            game.WriteBytes(EP.Pointers["Box"].Item2 + 12, BitConverter.GetBytes(spawnData.pos.X + (IsBoxSizeOne ? BoxSize1.X : BoxSize2.X)));
            game.WriteBytes(EP.Pointers["Box"].Item2 + 16, BitConverter.GetBytes(spawnData.pos.Y + (IsBoxSizeOne ? BoxSize1.Y : BoxSize2.Y)));
            game.WriteBytes(EP.Pointers["Box"].Item2 + 20, BitConverter.GetBytes(spawnData.pos.Z + (IsBoxSizeOne ? BoxSize1.Z : BoxSize2.Z)));
        }
    }
}
