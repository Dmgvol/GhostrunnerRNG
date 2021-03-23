using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostrunnerRNG.MapGen {
    class UplinkJump : NonPlaceableObject {

        private Dictionary<string, Tuple<DeepPointer, IntPtr>> Pointers = new Dictionary<string, Tuple<DeepPointer, IntPtr>>();


        public UplinkJump(int firstOffset, int secondOffset) {
            ObjectDP = new DeepPointer(0x045A3C20, 0x98, firstOffset, 0x128, 0xA8, secondOffset);



            // add pointers
            Pointers.Add("TimeToActivate", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x234), IntPtr.Zero));
            Pointers.Add("JumpMultiplier", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x280), IntPtr.Zero));
            Pointers.Add("JumpForwardMultiplier", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x284), IntPtr.Zero));
            Pointers.Add("JumpGravity", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x288), IntPtr.Zero)); // 6 by default, 2x from basic value ingame
        }

        public override void Randomize(Process game) {
            DerefPointers(game);
        }

        protected override void DerefPointers(Process game) {
            
        }
    }
}
