using System;
using System.Diagnostics;

namespace GhostrunnerRNG.MapGen {
    class CVPlatform {
        // Which ptr? if spawn point is 045A3C20+30+A8+138+248+1D0, then head ptr will be +045A3C20+30+A8+138
        DeepPointer spawnPointDP, endPointDP, startMoveDelayDP, timeLerpA_DP, timeLerpB_DP;
        IntPtr spawnPointPtr, endPointPtr, startMoveDelayPtr, timeLerpA_Ptr, timeLerpB_Ptr;

        public Vector3f SpawnPoint, EndPoint, LastPos;
        public float StartMoveDelay = 1, TimeLerpA = 2, TimeLerpB = 2;

        public CVPlatform(DeepPointer PlatformDP) {
            spawnPointDP = new DeepPointer(PlatformDP, 0x248, 0x1d0);
            endPointDP = new DeepPointer(PlatformDP, 0x240, 0x1d0);
            startMoveDelayDP = new DeepPointer(PlatformDP, 0x280);
            timeLerpA_DP = new DeepPointer(PlatformDP, 0x284);
            timeLerpB_DP = new DeepPointer(PlatformDP, 0x278);
            SpawnPoint = EndPoint = LastPos = Vector3f.Empty;
        }

        public void ReadMemoryData(Process game) {
            DerefPointer(game);
            // SpawnPoint
            float x, y, z;
            game.ReadValue<float>(spawnPointPtr, out x);
            game.ReadValue<float>(spawnPointPtr + 4, out y);
            game.ReadValue<float>(spawnPointPtr + 8, out z);
            SpawnPoint = new Vector3f(x, y, z);

            // End point
            game.ReadValue<float>(endPointPtr, out x);
            game.ReadValue<float>(endPointPtr + 4, out y);
            game.ReadValue<float>(endPointPtr + 8, out z);
            EndPoint = new Vector3f(x, y, z);

            // startMoveDelay, lerpA and lerpB
            game.ReadValue<float>(startMoveDelayPtr, out StartMoveDelay);
            game.ReadValue<float>(timeLerpA_Ptr, out TimeLerpA);
            game.ReadValue<float>(timeLerpB_Ptr, out TimeLerpB);
        }

        public void WriteMemory(Process game) {
            DerefPointer(game);
            // spawn point
            if(!SpawnPoint.VecEquals(Vector3f.Empty)) {
                game.WriteBytes(spawnPointPtr, BitConverter.GetBytes((float)SpawnPoint.X));
                game.WriteBytes(spawnPointPtr + 4, BitConverter.GetBytes((float)SpawnPoint.Y));
                game.WriteBytes(spawnPointPtr + 8, BitConverter.GetBytes((float)SpawnPoint.Z));
            }
            // end point
            if(!EndPoint.VecEquals(Vector3f.Empty)) {
                game.WriteBytes(endPointPtr, BitConverter.GetBytes((float)EndPoint.X));
                game.WriteBytes(endPointPtr + 4, BitConverter.GetBytes((float)EndPoint.Y));
                game.WriteBytes(endPointPtr + 8, BitConverter.GetBytes((float)EndPoint.Z));
            }
            // startMoveDelay, lerpA and lerpB
            game.WriteBytes(startMoveDelayPtr, BitConverter.GetBytes((float)StartMoveDelay));
            game.WriteBytes(timeLerpA_Ptr, BitConverter.GetBytes((float)TimeLerpA));
            game.WriteBytes(timeLerpB_Ptr, BitConverter.GetBytes((float)TimeLerpB));
        }

        public void DerefPointer(Process game) {
            spawnPointDP.DerefOffsets(game, out spawnPointPtr);
            endPointDP.DerefOffsets(game, out endPointPtr);
            startMoveDelayDP.DerefOffsets(game, out startMoveDelayPtr);
            timeLerpA_DP.DerefOffsets(game, out timeLerpA_Ptr);
            timeLerpB_DP.DerefOffsets(game, out timeLerpB_Ptr);
        }
    }
}
