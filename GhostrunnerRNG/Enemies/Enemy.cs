using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using System;
using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// Base object for any enemy in the game
/// </summary>
namespace GhostrunnerRNG.Enemies {
    public class Enemy : WorldObject {

        // 3D virtual rectangle/volumes where enemy can spawn
        private List<SpawnPlane> planes = new List<SpawnPlane>();

        public Enemy(DeepPointer EnemyDP) : base(EnemyDP) { }

        public enum EnemyTypes { Default, Waver, Drone, ShieldOrb, Weeb, Sniper, Turret, Splitter }
        public EnemyTypes enemyType { get; protected set; } = EnemyTypes.Default;

        public void SetEnemyType(EnemyTypes enemyType) {
            this.enemyType = enemyType;
        }

        // clear all planes
        public void ClearAllPlanes() => planes.Clear();

        protected DeepPointer AppendBaseLastOffset(params int[] appendOffsets) {
            List<int> offsets = new List<int>(ObjectDP.GetOffsets());
            offsets.RemoveAt(offsets.Count - 1); // removes last offset
            offsets.AddRange(appendOffsets); // add new offsets
            return new DeepPointer(ObjectDP.GetBase(), new List<int>(offsets));
        }
        public void DisableAttachedCP(Process game) {
            IntPtr parentPtr, killcountPtr, enemyPtr, killlistPtr, basePtr;
            ulong parent;
            int killcount;
            //check if there is a prent object
            List<int> offsets = new List<int>(ObjectDP.GetOffsets());
            offsets[offsets.Count - 1] = 0x5D0;
            DeepPointer parentDP = new DeepPointer(ObjectDP.GetBase(), offsets);
            parentDP.DerefOffsets(GameHook.game, out parentPtr);
            GameHook.game.ReadValue<ulong>(parentPtr, out parent);
            //return if there is no parent object 
            if(parent == 0) return;

            //read kill count
            offsets.Add(0x230);
            DeepPointer killcountDP = new DeepPointer(ObjectDP.GetBase(), offsets);
            killcountDP.DerefOffsets(GameHook.game, out killcountPtr);
            GameHook.game.ReadValue<int>(killcountPtr, out killcount);
            //remove parent object if kill count = 0
            if(killcount == 0) {
                game.WriteBytes(parentPtr, new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 });
                return;
            }
            //this enemy start address
            basePtr = parentPtr - 0x5d0;

            //find killlist address
            bool foundPtr = false;
            offsets[offsets.Count - 1] = 0x228;
            offsets.Add(0x0);
            DeepPointer killlistDP = new DeepPointer(ObjectDP.GetBase(), offsets);
            killlistDP.DerefOffsets(GameHook.game, out killlistPtr);
            for(var i = 0; i < killcount; i++) {
                //find base pointer in kill list          
                GameHook.game.ReadValue<IntPtr>(killlistPtr + 0x8 * i, out enemyPtr);
                if(enemyPtr == basePtr) {
                    //switch pointers
                    foundPtr = true;
                    if(i != killcount - 1) {
                        GameHook.game.ReadValue<IntPtr>(killlistPtr + 0x8 * (killcount - 1), out enemyPtr);
                        GameHook.game.WriteBytes(killlistPtr + 0x8 * (killcount - 1), BitConverter.GetBytes((ulong)basePtr));
                        GameHook.game.WriteBytes(killlistPtr + 0x8 * i, BitConverter.GetBytes((ulong)enemyPtr));
                    }
                    break;
                }
            }

            if(!foundPtr) return;

            killcount -= 1;
            // update decreased value
            GameHook.game.WriteBytes(killcountPtr, BitConverter.GetBytes(killcount));
            //remove parent object
            game.WriteBytes(parentPtr, new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 });
        }
    }
}
