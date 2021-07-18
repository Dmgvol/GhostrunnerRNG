using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.MemoryUtils;
using GhostrunnerRNG.NonPlaceableObjects;
using System;
using System.Diagnostics;

namespace GhostrunnerRNG.Maps {
    class GateKeeper : MapCore {
        // DeepPointer WhiteRing1DP = 0x04609420, 0x98, 0x30, 0x128, 0xA8, 0x708, 0x9E8, 0x134);

        EasyPointers EP = new EasyPointers();

        public GateKeeper() : base(GameUtils.MapType.Gatekeeper, manualGen: true) {
            EP.Add("Rotation", new DeepPointer(PtrDB.DP_Tom_Rotation));
            if(GameHook.IsHC) return;
            Gen_PerRoom();
        }

        protected override void Gen_PerRoom() {
            // declare lasers
            worldObjects.Add(new TomLaser(0xb88).AddLaserRange(-1, 4, new QuaternionAngle(0, 0, 0)));

            worldObjects.Add(new TomLaser(0xb88 + 0x8).AddLaserRange(0, 4, new QuaternionAngle(0, 180, 0), true));

            worldObjects.Add(new TomLaser(0xb88 + (0x8 * 2)).AddLaserRange(-4, 1, new QuaternionAngle(0, 0, 0), false));

            worldObjects.Add(new TomLaser(0xb88 + (0x8 * 3)).AddLaserRange(-4, 0, new QuaternionAngle(0, 180, 0), true));


            // NOTE: Lasers with broken hitboxes (disabled until we find a way to move those hitboxes)
            #region DisabledLasers
            /*
            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 4)).AddLaserRange(-1, 1, new QuaternionAngle(0, 0, 0), false));

            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 5)).AddLaserRange(-1, 1, new QuaternionAngle(0, 180, 0), true));

            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 6)).AddLaserRange(-1, 1, new QuaternionAngle(0, 0, 0), false));

            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 7)).AddLaserRange(-1, 1, new QuaternionAngle(0, 180, 0), true));

            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 8)).AddLaserRange(-1, 2, new QuaternionAngle(0, 0, 0), false));
            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 9)).AddLaserRange(-2, 1, new QuaternionAngle(0, 180, 0), true));

            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 10)).AddLaserRange(-3, 1, new QuaternionAngle(0, 0, 0), false));
            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 11)).AddLaserRange(-4, 1, new QuaternionAngle(0, 180, 0), true));
            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 12)).AddLaserRange(-1, 3, new QuaternionAngle(0, 0, 0), false));
            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 13)).AddLaserRange(-1, 4, new QuaternionAngle(0, 180, 0), true));

            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 14)).AddLaserRange(-3, 0, new QuaternionAngle(0, 0, 0), false));
            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 15)).AddLaserRange(-3, 0, new QuaternionAngle(0, 180, 0), true));

            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 16)).AddLaserRange(-1, 1, new QuaternionAngle(0, 0, 0), false));
            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 17)).AddLaserRange(-1, 1, new QuaternionAngle(0, 180, 0), true));

            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 18)).AddLaserRange(-1, 3, new QuaternionAngle(0, 0, 0), false));
            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 19)).AddLaserRange(-1, 3, new QuaternionAngle(0, 180, 0), true));

            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 20)).AddLaserRange(-5, 6, new QuaternionAngle(0, 0, 0), false));
            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 21)).AddLaserRange(-6, 5, new QuaternionAngle(0, 180, 0), true));

            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 22)).AddLaserRange(-3, -1, new QuaternionAngle(0, 0, 0), false));
            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 24)).AddLaserRange(-1, 1, new QuaternionAngle(0, 0, 0), false));
            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 26)).AddLaserRange(1, 4, new QuaternionAngle(0, 0, 0), false));

            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 23)).AddLaserRange(-5, -1, new QuaternionAngle(0, 180, 0), true));
            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 25)).AddLaserRange(-1, 1, new QuaternionAngle(0, 180, 0), true));
            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 27)).AddLaserRange(1, 5, new QuaternionAngle(0, 180, 0), true));

            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 28)).AddLaserRange(-3, 3, new QuaternionAngle(0, 0, 0), false));
            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 29)).AddLaserRange(-3, 3, new QuaternionAngle(0, 180, 0), true));

            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 30)).AddLaserRange(-2, 2, new QuaternionAngle(0, 0, 0), false));
            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 31)).AddLaserRange(-2, 2, new QuaternionAngle(0, 180, 0), true));

            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 32)).AddLaserRange(-2, 2, new QuaternionAngle(0, 0, 0), false));
            nonPlaceableObjects.Add(new TomLaser(0xb88 + (0x8 * 33)).AddLaserRange(-2, 2, new QuaternionAngle(0, 180, 0), true));
            */

            //// hide white ring (Disabled for now)
            //WhiteRing1DP.DerefOffsets(GameHook.game, out WhiteRing1Ptr);
            //GameHook.game.WriteBytes(WhiteRing1Ptr, BitConverter.GetBytes(0));
            //GameHook.game.WriteBytes(WhiteRing1Ptr + 4, BitConverter.GetBytes(0));

            #endregion

            ////// SECTION 3 ////
            worldObjects.Add(new TomLaser(0xD78).SectionThree().AddLaserRange(-18, -6, new QuaternionAngle(90, 0, 0, 0), false)
                .AddLaserRange(18, 6, new QuaternionAngle(90, 0, 0, 0), false)
                .AddPosOffset(new Vector3f(30, 0, -1000)));

            worldObjects.Add(new TomLaser(0xD80).SectionThree().AddLaserRange(-5, 5, new QuaternionAngle(90, 0, 0, 0), false).AddPosOffset(new Vector3f(30, 20, -1000)));

            worldObjects.Add(new TomLaser(0xD58).SectionThree().AddLaserRange(-3, 3, new QuaternionAngle(90, 0, 0, 0), false).AddPosOffset(new Vector3f(30, 20, 0)));

            worldObjects.Add(new TomLaser(0xD60).SectionThree().AddLaserRange(-12, -9, new QuaternionAngle(90, 0, 0, 0), false)
                .AddLaserRange(9, 12, new QuaternionAngle(90, 0, 0, 0), false)
                .AddPosOffset(new Vector3f(30, 0, 0)));

            worldObjects.Add(new TomLaser(0xD68).SectionThree().AddLaserRange(-8, -5, new QuaternionAngle(90, 0, 0, 0), false)
                .AddPosOffset(new Vector3f(30, 0, 0)));

            worldObjects.Add(new TomLaser(0xD70).SectionThree().AddLaserRange(5, 8, new QuaternionAngle(90, 0, 0, 0), false)
               .AddPosOffset(new Vector3f(30, 0, 0)));
        }

        public override void RandomizeEnemies(Process game) {
            base.RandomizeEnemies(game);
            EP.DerefPointers(game);

            // rotation
            game.WriteBytes(EP.Pointers["Rotation"].Item2, BitConverter.GetBytes((float)Config.GetInstance().r.Next(18, 23)));
        }
    }
}