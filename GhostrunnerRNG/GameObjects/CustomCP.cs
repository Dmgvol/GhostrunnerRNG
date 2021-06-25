using GhostrunnerRNG.Game;
using GhostrunnerRNG.MemoryUtils;
using System;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.GameObjects {
    public class CustomCP {

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
            DeepPointer cpDP = PtrDB.MapCPPointers[mapType];
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
