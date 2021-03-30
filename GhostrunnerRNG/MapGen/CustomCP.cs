using GhostrunnerRNG.Game;
using System;
using System.Collections.Generic;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.MapGen {
    public class CustomCP {

        Dictionary<MapType, DeepPointer> MapCPPointers = new Dictionary<MapType, DeepPointer>() {
            { MapType.Awakening, new DeepPointer(0x04326CF8, 0x28, 0x28, 0x48)},
            { MapType.LookInside, new DeepPointer(0x04326CF8, 0x28, 0x28, 0x48)},
            { MapType.LookInsideCV, new DeepPointer(0x04326CF8, 0x28, 0x28, 0x48)},
            { MapType.Faster, new DeepPointer(0x04326CF8, 0x28, 0x28, 0x48)},
            { MapType.InHerOwnImage, new DeepPointer(0x04326CF8, 0x28, 0x28, 0x48)},
            { MapType.ReignInHell, new DeepPointer(0x04326CF8, 0x28, 0x28, 0x48)},
            { MapType.ReignInHellCV, new DeepPointer(0x04326CF8, 0x28, 0x28, 0x48)},

            { MapType.TheClimb, new DeepPointer(0x04326CF8, 0x28, 0xA8, 0x48)},
            { MapType.TheClimbCV, new DeepPointer(0x04326CF8, 0x28, 0x28, 0x48)},

            { MapType.BlinkCV, new DeepPointer(0x04326CF8, 0x28, 0x0, 0x48)},
            { MapType.TempestCV, new DeepPointer(0x04326CF8, 0x28, 0x0, 0x48)},
            { MapType.EchoesCV, new DeepPointer(0x04326CF8, 0x28, 0x0, 0x48)},
            { MapType.OverlordCV, new DeepPointer(0x04326CF8, 0x28, 0x0, 0x48)},
            { MapType.TheMonster, new DeepPointer(0x04326CF8, 0x28, 0x0, 0x48)},

            { MapType.JackedUp, new DeepPointer(0x04326CF8, 0x28, 0x48, 0x48)},

            { MapType.BreatheIn, new DeepPointer(0x04326CF8, 0x28, 0x80, 0x48)},
            { MapType.BreatheInCV, new DeepPointer(0x04326CF8, 0x28, 0x80, 0x48)},

            { MapType.RoadToAmida, new DeepPointer(0x04326CF8, 0x28, 0x50, 0x48)},

            { MapType.RunUp, new DeepPointer(0x04326CF8, 0x28, 0x60, 0x48)},
            { MapType.Gatekeeper, new DeepPointer(0x04326CF8, 0x28, 0x60, 0x48)},

            { MapType.DharmaCity, new DeepPointer(0x04326CF8, 0x28, 0x10, 0x48)},
            { MapType.TYWB, new DeepPointer(0x04326CF8, 0x28, 0x10, 0x48)},

            { MapType.Echoes, new DeepPointer(0x04326CF8, 0x28, 0x138, 0x48)},

            { MapType.ForbiddenZone, new DeepPointer(0x04326CF8, 0x28, 0x18, 0x48)},

            { MapType.TheSummit, new DeepPointer(0x04326CF8, 0x28, 0x8, 0x48)},
        };

        // stats
        private Vector3f cornerA, cornerB, spawnPos;
        private Angle spawnAngle;
        public bool CPTriggered { get; private set; } = false;
        private MapType mapType;
        private float verticalAngle = 0;

        // warp volume
        private Vector3f LastPlayerPos;
        private bool IsWarpVolume = false;

        public CustomCP(MapType mapType, Vector3f cornerA, Vector3f cornerB, Vector3f spawnPos, Angle spawnAngle, float verticalAngle = 0) {
            this.cornerA = cornerA;
            this.cornerB = cornerB;
            this.spawnPos = spawnPos;
            this.spawnAngle = spawnAngle;
            this.mapType = mapType;
            this.verticalAngle = verticalAngle;
        }

        public CustomCP AsWarpVolume() {
            IsWarpVolume = true;
            return this;
        }

        public void Update(Vector3f Player) {
            if(!IsWarpVolume) {
                if(!CPTriggered && PlayerInVolume(Player)) {
                    CPTriggered = true;
                    SetLastCP();
                }
            } else {
                if(!LastPlayerPos.VecEquals(Player))
                    // player moved inside the volume?
                    if(!PlayerInVolume(LastPlayerPos) && PlayerInVolume(Player)) {
                        MovePlayer();
                    }
                    LastPlayerPos = Player;
            }
        }

        private bool PlayerInVolume(Vector3f player) {
            return (player.X >= Math.Min(cornerA.X, cornerB.X) && player.X <= Math.Max(cornerA.X, cornerB.X) &&
               player.Y >= Math.Min(cornerA.Y, cornerB.Y) && player.Y <= Math.Max(cornerA.Y, cornerB.Y) &&
               player.Z >= Math.Min(cornerA.Z, cornerB.Z) && player.Z <= Math.Max(cornerA.Z, cornerB.Z));
        }

        private void MovePlayer() {
            // Warp player to spawnPos/Angle
            GameHook.game.WriteBytes(GameHook.xPosPtr, BitConverter.GetBytes(spawnPos.X));
            GameHook.game.WriteBytes(GameHook.yPosPtr, BitConverter.GetBytes(spawnPos.Y));
            GameHook.game.WriteBytes(GameHook.zPosPtr, BitConverter.GetBytes(spawnPos.Z));
            GameHook.game.WriteBytes(GameHook.angleSinPtr, BitConverter.GetBytes(spawnAngle.angleSin));
            GameHook.game.WriteBytes(GameHook.angleCosPtr, BitConverter.GetBytes(spawnAngle.angleCos));
        }

        private void SetLastCP() {
            IntPtr cpPtr;
            DeepPointer cpDP = MapCPPointers[mapType];
            cpDP.DerefOffsets(GameHook.game, out cpPtr);
            // pos
            GameHook.game.WriteBytes(cpPtr, BitConverter.GetBytes(spawnPos.X));
            GameHook.game.WriteBytes(cpPtr + 4, BitConverter.GetBytes(spawnPos.Y));
            GameHook.game.WriteBytes(cpPtr + 8, BitConverter.GetBytes(spawnPos.Z));
            // angle
            double horizontalAngle = (((spawnAngle.angleSin > 0) ? Math.Acos(spawnAngle.angleCos) : -Math.Acos(spawnAngle.angleCos)) * 180 / Math.PI) * 2;
            GameHook.game.WriteBytes(cpPtr + 20, BitConverter.GetBytes((float)horizontalAngle)); // horizontal angle
            GameHook.game.WriteBytes(cpPtr + 16, BitConverter.GetBytes(verticalAngle)); // vertical angle
        }
    }
}
