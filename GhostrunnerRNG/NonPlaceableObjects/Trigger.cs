﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GhostrunnerRNG.NonPlaceableObjects {
    class Trigger : NonPlaceableObject {
        // first rng only, no actual rng
        private bool firstRng = true;

        // pointers
        private DeepPointer originDP;
        private IntPtr originPtr;
        private List<IntPtr> boxPtr = new List<IntPtr>();

        // data
        Vector3f pos, size;

        // signture searching
        private DeepPointer startSearch;
        private string signature;
        private int searchLength;

        //0x045A3C20 + 98 + first + 128 + A8+ second + 220 + 398 +150
        public Trigger(int firstOffset, int secondOffset, Vector3f pos, Vector3f size, DeepPointer startSearch, int searchLength, string signature) {
            originDP = new DeepPointer(0x04609420, 0x98, firstOffset, 0x128, 0xA8, secondOffset, 0x220, 0x398, 0x150);
            this.pos = pos;
            this.size = size;

            // signature searching
            this.startSearch = startSearch;
            this.signature = signature;
            this.searchLength = searchLength;
        }

        // Not real RNG, single case use, just using same structure
        public override void Randomize(Process game) {
            if(!firstRng) return;
            DerefPointers(game);
            if(boxPtr.Count == 0) return;
            firstRng = false;

            // center
            game.WriteBytes(originPtr, BitConverter.GetBytes(pos.X));
            game.WriteBytes(originPtr + 4, BitConverter.GetBytes(pos.Y));
            game.WriteBytes(originPtr + 8, BitConverter.GetBytes(pos.Z));
            // box corners
            for(int i = 0; i < boxPtr.Count; i++) {
                game.WriteBytes(boxPtr[i], BitConverter.GetBytes(pos.X - (size.X * 1.01f)));
                game.WriteBytes(boxPtr[i] + 4, BitConverter.GetBytes(pos.Y - (size.Y * 1.01f)));
                game.WriteBytes(boxPtr[i] + 8, BitConverter.GetBytes(pos.Z - (size.Z * 1.01f)));
                game.WriteBytes(boxPtr[i] + 12, BitConverter.GetBytes(pos.X + (size.X * 1.01f)));
                game.WriteBytes(boxPtr[i] + 16, BitConverter.GetBytes(pos.Y + (size.Y * 1.01f)));
                game.WriteBytes(boxPtr[i] + 20, BitConverter.GetBytes(pos.Z + (size.Z * 1.01f)));
            }
        }

        protected override void DerefPointers(Process game) {
            boxPtr.Clear();
            // normal deref
            originDP.DerefOffsets(game, out originPtr);

            // signature scan for trigger corners
            IntPtr startPtr;
            startSearch.DerefOffsets(game, out startPtr);
            SignatureScanner scanner = new SignatureScanner(game, startPtr, searchLength);
            SigScanTarget target = new SigScanTarget(signature);
            var targets = scanner.ScanAll(target).ToList();
            boxPtr.AddRange(targets);
            //if(targets != null && targets.Count > 0) {
            //    boxPtr = targets.Last();
            //}
        }

        protected override void ReadDefaultValues(Process game) {}
    }
}
