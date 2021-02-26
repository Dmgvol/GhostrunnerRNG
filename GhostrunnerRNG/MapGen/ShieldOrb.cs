using GhostrunnerRNG.Game;
using GhostrunnerRNG.Maps;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GhostrunnerRNG.MapGen {
    class ShieldOrb : Enemy {

        // linked objects
        private List<DeepPointer> LinkedObjectsDP = new List<DeepPointer>();

        // hide beams
        private List<DeepPointer> BeamsToHide = new List<DeepPointer>();

        // shieldGlow/Cone
        private DeepPointer shieldGlowDP;
        private IntPtr shieldGlowPtr;


        public ShieldOrb(Enemy enemy, DeepPointer dp) : base(enemy.GetObjectDP()) {
            shieldGlowDP = dp;
        }

        public ShieldOrb(Enemy enemy) : base(enemy.GetObjectDP()) {}

        public void LinkObject(DeepPointer dp) {
            LinkedObjectsDP.Add(dp);
        }

        internal void FixBeams(Process game) {
            // Hide Beams
            for(int i = 0; i < BeamsToHide.Count; i++) {
                IntPtr beamPtr;
                BeamsToHide[i].DerefOffsets(game, out beamPtr); // quick deref
                // move them to 0, 0, 0
                game.WriteBytes(beamPtr, BitConverter.GetBytes(0f));
                game.WriteBytes(beamPtr + 4, BitConverter.GetBytes(0f));
                game.WriteBytes(beamPtr + 8, BitConverter.GetBytes(0f));
            }

            // Redirect linked enemies to orb xyz
            for(int i = 0; i < LinkedObjectsDP.Count; i++) {
                IntPtr beamPtr;
                LinkedObjectsDP[i].DerefOffsets(game, out beamPtr); // quick deref
                // set enemy beam to orb pos
                game.WriteBytes(beamPtr, BitConverter.GetBytes((float)Pos.X));
                game.WriteBytes(beamPtr + 4, BitConverter.GetBytes((float)Pos.Y));
                game.WriteBytes(beamPtr + 8, BitConverter.GetBytes((float)Pos.Z));
            }
        }
       

        public void HideBeam(DeepPointer dp) {
            // hide
            BeamsToHide.Add(dp);
        }

        public override void SetMemoryPos(Process game, SpawnData spawnData) {
            DerefPointer(game);
            base.SetMemoryPos(game, spawnData);

            // update shield glow/cone location to orb pos
            if(shieldGlowDP != null) {
                game.WriteBytes(shieldGlowPtr, BitConverter.GetBytes((float)spawnData.pos.X));
                game.WriteBytes(shieldGlowPtr + 4, BitConverter.GetBytes((float)spawnData.pos.Y));
                game.WriteBytes(shieldGlowPtr + 8, BitConverter.GetBytes((float)spawnData.pos.Z));
                // scale 
                game.WriteBytes(shieldGlowPtr + 16, BitConverter.GetBytes(1f));
                game.WriteBytes(shieldGlowPtr + 20, BitConverter.GetBytes(1f));
                game.WriteBytes(shieldGlowPtr + 24, BitConverter.GetBytes(1f));
            }
        }

        protected override void DerefPointer(Process game) {
            base.DerefPointer(game);

            // deref shield glow/cone if any
            if(shieldGlowDP != null) {
                shieldGlowDP.DerefOffsets(game, out shieldGlowPtr);
            }
        }
    }
}
