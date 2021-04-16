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
            List<int> offsets = new List<int>(ObjectDP.GetOffsets());
            offsets[offsets.Count - 1] = 0x5D0;
            DeepPointer parentDP = new DeepPointer(ObjectDP.GetBase(), offsets);
            IntPtr parentPtr;
            parentDP.DerefOffsets(game, out parentPtr);
            game.WriteBytes(parentPtr, new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 });
        }
    }
}
