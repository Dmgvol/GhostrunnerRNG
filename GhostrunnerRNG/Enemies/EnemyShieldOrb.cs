using GhostrunnerRNG.Game;
using GhostrunnerRNG.Maps;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GhostrunnerRNG.Enemies {
    class EnemyShieldOrb : Enemy {

        // linked objects
        private List<DeepPointer> LinkedObjectsDP = new List<DeepPointer>();

        // hide beams
        private List<DeepPointer> BeamsToHide = new List<DeepPointer>();

        // shieldGlow/Cone
        private DeepPointer shieldGlowDP;
        private IntPtr shieldGlowPtr;

        public EnemyShieldOrb(Enemy enemy) : base(enemy.GetObjectDP()) {
            Pos = enemy.Pos;
            enemyType = EnemyTypes.ShieldOrb;

            // glow pointer
            List<int> offsets = new List<int>(ObjectDP.GetOffsets());
            offsets.RemoveAt(offsets.Count - 1);
            offsets.Add(0x6c8);
            offsets.Add(0x130);
            offsets.Add(0x1d0);
            shieldGlowDP = new DeepPointer(ObjectDP.GetBase(), offsets);
        }

        public void HideBeam_Range(int enemyIndex, int beams) {
            if(enemyIndex < 0 || beams < 0) return;

            for(int j = 0;  j < beams; j++) {
                List<int> offsets = new List<int>(ObjectDP.GetOffsets());
                offsets.RemoveAt(offsets.Count - 1);
                offsets.Add(0x6f0);
                offsets.Add(0x8 * enemyIndex);
                offsets.Add(0x200);
                offsets.Add(0x8 * (j + 1));
                offsets.Add(0x1D0);
                BeamsToHide.Add(new DeepPointer(ObjectDP.GetBase(), offsets));
            }
            
        }

        public void LinkObject(int enemies) {
            if(enemies < 0) return;

            for(int i = 0; i < enemies; i++) {
                List<int> offsets = new List<int>(ObjectDP.GetOffsets());
                offsets.RemoveAt(offsets.Count - 1);
                offsets.Add(0x6f0);
                offsets.Add(0x8 * LinkedObjectsDP.Count);
                offsets.Add(0x220);
                LinkedObjectsDP.Add(new DeepPointer(ObjectDP.GetBase(), offsets));
            }
        }

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

        public override void SetMemoryPos(Process game, SpawnData spawnData) {
            DerefPointer(game);
            if(Config.GetInstance().Gen_RngOrbs) {
                base.SetMemoryPos(game, spawnData);
            }

            // update shield glow/cone location to orb pos - even RngOrbs disabled
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
