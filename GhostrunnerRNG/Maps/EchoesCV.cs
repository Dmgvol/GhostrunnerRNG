using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.GameObjects;
using GhostrunnerRNG.MapGen;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GhostrunnerRNG.Maps {
    class EchoesCV : MapCore {

        private readonly Vector3f Core = new Vector3f(-14500f, -4900f, -700f);

        // Rotation
        private DeepPointer RotationDP = new DeepPointer(0x045A3C20, 0x30, 0xE8, 0x10, 0x298, 0x60, 0x2C0);
        private IntPtr RotationPtr;
        private int rotationTimeOffset = 0x5A;
        private int rotationAngleOffset = 0x5F;

           
        public EchoesCV() : base(Game.GameUtils.MapType.EchoesCV, manualGen:true) {
            Gen_PerRoom();
            RotationDP.DerefOffsets(GameHook.game, out RotationPtr);
            CPRequired = false;
        }
        protected override void Gen_PerRoom() {
            Rooms = new List<RoomLayout>();
            RoomLayout layout;
            List<WorldObject> orbs = new List<WorldObject>();
            for(int i = 1; i <= 13; i++) 
                orbs.Add(new CVOrb(i));
            layout = new RoomLayout(orbs);

            // first platform
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-15460, -4642, -807))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-14811, -4782, -1087))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-15138, -4506, -1114))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-15118, -4659, -1441))));

            ////// 2nd platform (big piece) //////
            // up facing
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-13211, -4677, 98))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-13773, -4652, 89))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-12932, -5260, 8))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-12251, -5712, 22))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-13727, -4486, -413))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-13321, -4470, -414))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-13706, -4929, -800))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-13362, -5387, -775))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-12495, -5318, -801))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-12088, -4903, -1102))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-12906, -4480, -811))));
            // bot facing
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-12520, -5746, -653))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-12898, -5262, -653))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-12528, -5327, -1477))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-13262, -5315, -1456))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-13746, -4691, -1456))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-13174, -4920, -1832))));
            // side facing
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-13406, -4215, -115))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-11807, -5734, -311))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-12782, -5704, -305))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-13206, -5230, -305))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-12807, -4565, -305))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-12901, -4195, -1081))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-13684, -4164, -1081))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-13331, -4207, -694))));

            ////// 3nd platform (tunnel) //////
            // up facing
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-11319, -4916, -301))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-10838, -4647, -414))));
            // bot facing
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-10878, -4667, -1119))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-11365, -4986, -1472))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-11237, -4978, -604))));
            // side facing
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-11254, -4525, -755))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-10855, -4460, -755))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-11273, -5316, -755))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-11374, -5257, -1053))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-11580, -4872, -1133))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-10960, -4934, -1133))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-10625, -4649, -736))));

            ////// 4nd platform (U-shaped with floating platform) //////
            // up facing
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-10501, -4702, -109))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-10086, -4234, -448))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-10113, -5073, -401))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-10055, -4632, -803))));
            // bot facing
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-10046, -4960, -1201))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-10093, -4332, -1201))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-10534, -4617, -420))));
            // side facing
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-10102, -4111, -776))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-10097, -5144, -778))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-9803, -4673, -1041))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-9803, -4673, -1041))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-10509, -4192, -308))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-10480, -5166, -297))));

            ////// 5nd platform (last one, biggest piece) //////
            // up facing
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-9336, -5617, 135))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-8486, -5325, 98))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-8089, -5295, -395))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-7694, -4890, -401))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-8916, -5304, -384))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-9292, -5346, -732))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-9299, -4780, -751))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-8863, -4915, -400))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-8902, -5351, -401))));
            // bot facing
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-9266, -4867, -1106))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-9339, -5339, -1106))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-8839, -4955, -1106))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-8107, -5289, -1106))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-7691, -4951, -1483))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-9316, -5667, -1483))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-8903, -5293, -1483))));
            // side facing
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-9352, -5997, -1004))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-9352, -5997, -348))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-9701, -5736, -265))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-9599, -4629, -1110))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-8547, -4943, -838))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-8092, -5598, -709))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-8872, -5679, -709))));
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-8638, -5601, -37))));

            ////// Extra //////
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-16949, -4916, -401)))); // behind spawn
            layout.AddSpawnPlane(new SpawnPlane(ToRelative(new Vector3f(-6261, -4865, -355)))); // end tower/platform
            Rooms.Add(layout);
        }

        public override void RandomizeEnemies(Process game) {
            base.RandomizeEnemies(game);

            // Core rotation rng
            if(RotationPtr != IntPtr.Zero) {
                game.WriteBytes(RotationPtr, BitConverter.GetBytes((float)Config.GetInstance().r.Next(2, 5)));
                game.WriteBytes(RotationPtr + rotationTimeOffset, BitConverter.GetBytes((float)Config.GetInstance().r.Next(1, 5)));
                game.WriteBytes(RotationPtr + rotationAngleOffset, BitConverter.GetBytes((float)Config.GetInstance().r.Next(2) == 0 ? 90f : -90f));
            }
        }

        private Vector3f ToRelative(Vector3f pos) {
            return new Vector3f(pos.X - Core.X, pos.Z - Core.Z, (pos.Y - Core.Y) * (-1));
        }
    }
}
