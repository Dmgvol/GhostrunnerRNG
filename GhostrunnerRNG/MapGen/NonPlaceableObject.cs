using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GhostrunnerRNG.MapGen {
    public abstract class NonPlaceableObject {

        protected DeepPointer ObjectDP;
        protected IntPtr ObjectPtr;
        public NonPlaceableObject() {

        }

        protected DeepPointer AppendBaseOffset(params int[] appendOffsets) {
            List<int> offsets = new List<int>(ObjectDP.GetOffsets());
            offsets.AddRange(appendOffsets); // add new offsets
            return new DeepPointer(ObjectDP.GetBase(), new List<int>(offsets));
        }

        public abstract void Randomize(Process game);
        protected abstract void DerefPointers(Process game);
    }
}
