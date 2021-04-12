using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GhostrunnerRNG.Game {
    public class EasyPointers {
        public Dictionary<string, Tuple<DeepPointer, IntPtr>> Pointers { get; private set; } = new Dictionary<string, Tuple<DeepPointer, IntPtr>>();

        public virtual void DerefPointers(Process game) {
            // deref all pointer dictionary
            foreach(KeyValuePair<string, Tuple<DeepPointer, IntPtr>> item in new Dictionary<string, Tuple<DeepPointer, IntPtr>>(Pointers)) {
                IntPtr ptr;
                item.Value.Item1.DerefOffsets(game, out ptr);
                Pointers[item.Key] = new Tuple<DeepPointer, IntPtr>(item.Value.Item1, ptr);
            }
        }
    }
}
