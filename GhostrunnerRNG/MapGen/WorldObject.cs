using GhostrunnerRNG.Game;
using System;
using System.Diagnostics;

namespace GhostrunnerRNG.MapGen {
    /// <summary>
    /// Base Object for any object that has xyz coords and can be written/readable from memory.
    /// HeadDP is the pointer for X position of the object.
    /// </summary>
    public class WorldObject {
        // pointers
        protected DeepPointer ObjectDP; // pointer to x coord of the object
        protected IntPtr ObjectPtr;

        // last known pos and angle(if any)
        public Vector3f Pos = Vector3f.Empty;
        public Angle? angle { get; private set; }

        public WorldObject(DeepPointer ObjectDP) {
            this.ObjectDP = ObjectDP;
        }

        //// Memory Actions ////
        public virtual Vector3f GetMemoryPos(Process game) {
            DerefPointer(game);
            float x, y, z;
            game.ReadValue<float>(ObjectPtr, out x);
            game.ReadValue<float>(ObjectPtr + 4, out y);
            game.ReadValue<float>(ObjectPtr + 8, out z);
            Pos = new Vector3f(x,y,z);
            return Pos;
        }

        public virtual void SetMemoryPos(Process game, Vector3f pos) {
            if(pos.IsEmpty()) return;

            DerefPointer(game);
            // XYZ coords
            game.WriteBytes(ObjectPtr, BitConverter.GetBytes((float)pos.X));
            game.WriteBytes(ObjectPtr + 4, BitConverter.GetBytes((float)pos.Y));
            game.WriteBytes(ObjectPtr + 8, BitConverter.GetBytes((float)pos.Z));
            Pos = pos; // update last pos
        }

        public virtual void SetMemoryPos(Process game, SpawnData spawnData) {
            if(spawnData.pos.IsEmpty()) return;

            DerefPointer(game);
            // XYZ coords
            game.WriteBytes(ObjectPtr, BitConverter.GetBytes((float)spawnData.pos.X));
            game.WriteBytes(ObjectPtr + 4, BitConverter.GetBytes((float)spawnData.pos.Y));
            game.WriteBytes(ObjectPtr + 8, BitConverter.GetBytes((float)spawnData.pos.Z));
            Pos = spawnData.pos; // update last pos

            // angle orientation
            if(spawnData.HasAngle()) {
                game.WriteBytes(ObjectPtr - 8, BitConverter.GetBytes((float)spawnData.angle.Value.angleSin));
                game.WriteBytes(ObjectPtr - 4, BitConverter.GetBytes((float)spawnData.angle.Value.angleCos));
                angle = spawnData.angle;
            }
        }

        // Write last pos to memory
        public void SetMemoryPos(Process game) {
            if(Pos.IsEmpty()) return;

            DerefPointer(game);
            // XYZ coords
            game.WriteBytes(ObjectPtr, BitConverter.GetBytes((float)Pos.X));
            game.WriteBytes(ObjectPtr + 4, BitConverter.GetBytes((float)Pos.Y));
            game.WriteBytes(ObjectPtr + 8, BitConverter.GetBytes((float)Pos.Z));
        }

        /// <summary>
        /// Deref ObjectDP, overwrite if needed for more pointers
        /// </summary>
        /// <param name="game">hooked game process</param>
        protected virtual void DerefPointer(Process game) {
            if(ObjectDP == null) return;
            ObjectDP.DerefOffsets(game, out ObjectPtr);
        }

        public DeepPointer GetObjectDP() => ObjectDP;
    }
}
