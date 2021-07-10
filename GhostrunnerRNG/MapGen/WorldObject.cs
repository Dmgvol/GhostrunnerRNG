using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.GameObjects;
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
        public Angle angle { get; private set; }

        public WorldObject() {}

        public WorldObject(DeepPointer ObjectDP) {
            this.ObjectDP = ObjectDP;
        }

        //// Memory Actions ////
        public virtual Vector3f GetMemoryPos(Process game) {
            DerefPointer(game);
            float x, y, z;
            bool flag1 = game.ReadValue<float>(ObjectPtr, out x);
            bool flag2 = game.ReadValue<float>(ObjectPtr + 4, out y);
            bool flag3 = game.ReadValue<float>(ObjectPtr + 8, out z);
            if(flag1 && flag2 && flag3 && !float.IsNaN(x) && !float.IsNaN(y) && !float.IsNaN(z)) // check if there's no NaN floats
                Pos = new Vector3f(x,y,z);

            return Pos;
        }


        public Tuple<Vector3f, QuaternionAngle> LastUpdatedCoords;
        public virtual Tuple<Vector3f, QuaternionAngle> GetMemoryCurrCoords(Process game) {
            var offsets = ObjectDP.GetOffsets();
            offsets[offsets.Count - 1] = 0x130;
            offsets.Add(0x1D0);
            DeepPointer DP_CurrentPos = new DeepPointer(ObjectDP.GetBase(), offsets);
            IntPtr PTR_CurrentPos;
            DP_CurrentPos.DerefOffsets(GameHook.game, out PTR_CurrentPos);
            float q1, q2, q3, q4, x, y, z;

            game.ReadValue<float>(PTR_CurrentPos, out x);
            game.ReadValue<float>(PTR_CurrentPos + 4, out y);
            game.ReadValue<float>(PTR_CurrentPos + 8, out z);
            game.ReadValue<float>(PTR_CurrentPos - 16, out q1);
            game.ReadValue<float>(PTR_CurrentPos - 12, out q2);
            game.ReadValue<float>(PTR_CurrentPos - 8, out q3);
            game.ReadValue<float>(PTR_CurrentPos - 4, out q4);
            LastUpdatedCoords = new Tuple<Vector3f, QuaternionAngle>(new Vector3f(x,y,z), new QuaternionAngle(q1, q2, q3, q4));
            return LastUpdatedCoords;
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

            // waver special case: raise Z by 60 units
            if(this is Enemy e && e.enemyType == Enemy.EnemyTypes.Waver) {
                spawnData.pos.Z += 60;
            }

            // CV GreenPlatform case: reduce height by 500
            if(this is GreenPlatform) {
                spawnData.pos.Z -= 200;
            }

            DerefPointer(game);
            // XYZ coords
            game.WriteBytes(ObjectPtr, BitConverter.GetBytes((float)spawnData.pos.X));
            game.WriteBytes(ObjectPtr + 4, BitConverter.GetBytes((float)spawnData.pos.Y));
            game.WriteBytes(ObjectPtr + 8, BitConverter.GetBytes((float)spawnData.pos.Z));
            Pos = spawnData.pos; // update last pos

            // angle orientation
            if(spawnData.angle is QuaternionAngle) {
                game.WriteBytes(ObjectPtr - 16, BitConverter.GetBytes((float)((QuaternionAngle)spawnData.angle).quaternion.x));
                game.WriteBytes(ObjectPtr - 12, BitConverter.GetBytes((float)((QuaternionAngle)spawnData.angle).quaternion.y));
                game.WriteBytes(ObjectPtr - 8, BitConverter.GetBytes((float)((QuaternionAngle)spawnData.angle).quaternion.z));
                game.WriteBytes(ObjectPtr - 4, BitConverter.GetBytes((float)((QuaternionAngle)spawnData.angle).quaternion.w));
            } else if(spawnData.HasAngle()) {
                game.WriteBytes(ObjectPtr - 8, BitConverter.GetBytes((float)spawnData.angle.angleSin));
                game.WriteBytes(ObjectPtr - 4, BitConverter.GetBytes((float)spawnData.angle.angleCos));

                // default angle - make sure X,Y are  default
                game.WriteBytes(ObjectPtr - 12, BitConverter.GetBytes(0));
                game.WriteBytes(ObjectPtr - 16, BitConverter.GetBytes(0));

                angle = spawnData.angle;
            }
        }

        // Write last pos to memory
        public virtual void SetMemoryPos(Process game) {
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
        public IntPtr GetObjectPtr() => ObjectPtr;

        public void Deref(Process game) => DerefPointer(game);
    }
}
