using GhostrunnerRNG.Game;
using System;
using System.Diagnostics;

namespace GhostrunnerRNG.NonPlaceableObjects {
    class TetrisTrigger : NonPlaceableObject {
        // Box/Hitbox offset
        private Vector3f BoxOffset = new Vector3f(101f, 101f, 40.4f);

        public TetrisTrigger(int hologramOffset, int centerOffset, int boxOffset) {
            Pointers.Add("Hologram", new Tuple<DeepPointer, IntPtr>(new DeepPointer(0x045A3C20, 0x30, 0xA8, hologramOffset, 0x230, 0x1D0), IntPtr.Zero));
            Pointers.Add("CenterBox", new Tuple<DeepPointer, IntPtr>(new DeepPointer(0x045A3C20, 0x30, 0xA8, centerOffset, 0x220, 0x398, 0x150), IntPtr.Zero));
            Pointers.Add("Box", new Tuple<DeepPointer, IntPtr>(new DeepPointer(0x045A3C20, 0x1F8, 0x60, 0xD0, 0x8C0, 0xB0, 0x5A0, 0x1A8, boxOffset), IntPtr.Zero));
        }

        public override void Randomize(Process game) {
            DerefPointers(game);

            if(spawnInfos != null && spawnInfos.Count > 0 && spawnInfos[0] is SpawnInfo info) {    
                // Hologram
                game.WriteBytes(Pointers["Hologram"].Item2, BitConverter.GetBytes((float)info.Pos.X));
                game.WriteBytes(Pointers["Hologram"].Item2 + 4, BitConverter.GetBytes((float)info.Pos.Y));
                game.WriteBytes(Pointers["Hologram"].Item2 + 8, BitConverter.GetBytes((float)info.Pos.Z));
                // CenterBox
                game.WriteBytes(Pointers["CenterBox"].Item2, BitConverter.GetBytes((float)info.Pos.X));
                game.WriteBytes(Pointers["CenterBox"].Item2 + 4, BitConverter.GetBytes((float)info.Pos.Y));
                game.WriteBytes(Pointers["CenterBox"].Item2 + 8, BitConverter.GetBytes((float)info.Pos.Z));
                // Hitbox
                game.WriteBytes(Pointers["Box"].Item2, BitConverter.GetBytes((float)info.Pos.X - BoxOffset.X));
                game.WriteBytes(Pointers["Box"].Item2 + 4, BitConverter.GetBytes((float)info.Pos.Y - BoxOffset.Y));
                game.WriteBytes(Pointers["Box"].Item2 + 8, BitConverter.GetBytes((float)info.Pos.Z - BoxOffset.Z));
                game.WriteBytes(Pointers["Box"].Item2 + 12, BitConverter.GetBytes((float)info.Pos.X + BoxOffset.X));
                game.WriteBytes(Pointers["Box"].Item2 + 16, BitConverter.GetBytes((float)info.Pos.Y + BoxOffset.Y));
                game.WriteBytes(Pointers["Box"].Item2 + 20, BitConverter.GetBytes((float)info.Pos.Z + BoxOffset.Z));
            }
        }
        protected override void ReadDefaultValues(Process game) {}
    }
}
