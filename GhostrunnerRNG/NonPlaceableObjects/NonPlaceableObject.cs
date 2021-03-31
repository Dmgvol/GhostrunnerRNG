using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GhostrunnerRNG.NonPlaceableObjects {
    public abstract class NonPlaceableObject {

        protected DeepPointer ObjectDP;
        protected IntPtr ObjectPtr;
        protected List<SpawnInfo> spawnInfos = new List<SpawnInfo>();

        public Dictionary<string, Tuple<DeepPointer, IntPtr>> Pointers { get; private set; } = new Dictionary<string, Tuple<DeepPointer, IntPtr>>();
        protected SpawnInfo DefaultData;

        public NonPlaceableObject() { }

        public NonPlaceableObject AddSpawnInfo(SpawnInfo info) { spawnInfos.Add(info); return this; }

        public void AddNewSpawnInfo(SpawnInfo info) => spawnInfos.Add(info);

        public void ClearSpawnInfo() => spawnInfos.Clear();

        protected DeepPointer AppendBaseOffset(params int[] appendOffsets) {
            List<int> offsets = new List<int>(ObjectDP.GetOffsets());
            offsets.AddRange(appendOffsets); // add new offsets
            return new DeepPointer(ObjectDP.GetBase(), new List<int>(offsets));
        }

        protected abstract void ReadDefaultValues(Process game);

        protected bool ModifyIfChanged(Process game, IntPtr ptr, float? n, float? defaultValue) {
            // values are different? update the change
            if(n != null && defaultValue != null && (float)n != (float)defaultValue) {
                game.WriteBytes(ptr, BitConverter.GetBytes((float)n));
                return true;
                // new value is null or not set/changed? and we have default value? update to default
            } else if(defaultValue != null) {
                game.WriteBytes(ptr, BitConverter.GetBytes((float)defaultValue));
            }
            return false;
        }

        protected void ModifyIfChangedInt(Process game, IntPtr ptr, int? n, int? defaultValue) {
            // values are different? update the change
            if(n != null && defaultValue != null && (int)n != (int)defaultValue) {
                game.WriteBytes(ptr, BitConverter.GetBytes((int)n));
                // new value is null or not set/changed? and we have default value? update to default
            } else if(defaultValue != null) {
                game.WriteBytes(ptr, BitConverter.GetBytes((int)defaultValue));
            }
        }

        public abstract void Randomize(Process game);

        protected virtual void DerefPointers(Process game) {
            if(ObjectDP != null)
                ObjectDP.Deref(game, out ObjectPtr);

            // deref all pointer dictionary
            foreach(KeyValuePair<string, Tuple<DeepPointer, IntPtr>> item in new Dictionary<string, Tuple<DeepPointer, IntPtr>>(Pointers)) {
                IntPtr ptr;
                item.Value.Item1.DerefOffsets(game, out ptr);
                Pointers[item.Key] = new Tuple<DeepPointer, IntPtr>(item.Value.Item1, ptr);
            }
        }
    }
}
