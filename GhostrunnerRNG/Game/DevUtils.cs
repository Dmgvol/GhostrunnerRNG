using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.MemoryUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GhostrunnerRNG.Game {
    public class DevUtils {

        public static void DEV_FindAllCP(List<Room> rooms = default, ExtBool overlap = ExtBool.Both, Mode mode = Mode.Both) {
            int c = 0;
            ReadPtrValue(PtrDB.ObjectsTypes["checkpoint"], out IntPtr CheckpointVFT);

            ReadDPValue(PtrDB.DP_SublevelsCount, out int Sublevels);

            for(var j = 1; j < Sublevels; j++) {

                ReadDPValue(new DeepPointer(PtrDB.DP_ObjectsCount).Format(0x8 * (j - 1)), out int Objects);

                for(var i = 1; i < Objects; i++) {

                    DeepPointer VFTableDP = new DeepPointer(PtrDB.DP_VFTablePattern).Format(0x8 * (j - 1), 0x8 * (i - 1), 0x0);
                    VFTableDP.DerefOffsets(GameHook.game, out IntPtr VFTablePtr);
                    GameHook.game.ReadValue(VFTablePtr, out IntPtr VFTable);
                    if(VFTable == CheckpointVFT) {

                        GameHook.game.ReadValue(IntPtr.Add(VFTablePtr, 0x2A0), out bool triggerbyoverlap);
                        GameHook.game.ReadValue(IntPtr.Add(VFTablePtr, 0x2A1), out bool workinHC);
                        GameHook.game.ReadValue(IntPtr.Add(VFTablePtr, 0x2A2), out bool workinCL);


                        List<int> offsets = new List<int>(VFTableDP.GetOffsets());
                        offsets[offsets.Count - 1] = 0x248;
                        offsets.Add(0x1d0);

                        DeepPointer PosDP = new DeepPointer(VFTableDP.GetBase(), offsets);
                        PosDP.DerefOffsets(GameHook.game, out IntPtr PosPtr);

                        float x, y, z;
                        GameHook.game.ReadValue(PosPtr, out x);
                        GameHook.game.ReadValue(PosPtr + 4, out y);
                        GameHook.game.ReadValue(PosPtr + 8, out z);
                        if(rooms == default(List<Room>) || rooms.Where(r => GameUtils.PlayerWithinRectangle(new Vector3f(x, y, z), r.pointA, r.pointB)).Count() > 0) {

                            if((overlap == ExtBool.Both || Convert.ToBoolean(overlap) == triggerbyoverlap) && (mode == Mode.Both || (mode == Mode.Classic ? (Convert.ToBoolean(mode) == !workinCL) : (Convert.ToBoolean(mode) == workinHC)))) {
                                c++;
                                Console.WriteLine($"CP:\nPos: {x} {y} {z}");
                                Console.WriteLine($"TriggerbyOverlap: {triggerbyoverlap}, WorkinHC: {workinHC}, WorkinCl: {workinCL}");
                                Console.WriteLine($"(0x04609420, 0x98, 0x{0x8 * (j - 1):X}, 0x128, 0xA8, 0x{0x8 * (i - 1):X}, 0x248, 0x1D0)\n");
                            }
                        }
                    }

                }
            }
            Console.WriteLine("found: " + c);
        }


        public enum Uplinks { All, Shuriken, Jump, BulletTimeDevice };
        public enum Mode { Both = -1, Classic = 0, Hardcore = 1 };
        public enum ExtBool { Both = -1, False = 0, True = 1 };
        public static void DEV_FindUplinks(List<Room> rooms = default, Uplinks uplink = Uplinks.All, Mode mode = Mode.Both) {
            int c = 0;

            ReadPtrValue(PtrDB.ObjectsTypes["triggerbase"], out IntPtr TriggerBaseVFT);
            ReadDPValue(PtrDB.DP_SublevelsCount, out int Sublevels);

            for(var j = 1; j <= Sublevels; j++) {

                ReadDPValue(new DeepPointer(PtrDB.DP_ObjectsCount).Format(0x8 * (j - 1)), out int Objects);

                for(var i = 1; i <= Objects; i++) {

                    DeepPointer VFTableDP = new DeepPointer(PtrDB.DP_VFTablePattern).Format(0x8 * (j - 1), 0x8 * (i - 1), 0x0);
                    VFTableDP.DerefOffsets(GameHook.game, out IntPtr VFTablePtr);
                    GameHook.game.ReadValue(VFTablePtr, out IntPtr VFTable);

                    if(VFTable == TriggerBaseVFT) {
                        GameHook.game.ReadValue(IntPtr.Add(VFTablePtr, 0x230), out byte Type);

                        List<int> offsets = new List<int>(VFTableDP.GetOffsets());
                        offsets[offsets.Count - 1] = 0x130;
                        offsets.Add(0x11c);

                        DeepPointer PosDP = new DeepPointer(VFTableDP.GetBase(), offsets);
                        PosDP.DerefOffsets(GameHook.game, out IntPtr PosPtr);

                        float x, y, z;
                        GameHook.game.ReadValue(PosPtr, out x);
                        GameHook.game.ReadValue(PosPtr + 4, out y);
                        GameHook.game.ReadValue(PosPtr + 8, out z);

                        if(rooms == default(List<Room>) || rooms.Where(r => GameUtils.PlayerWithinRectangle(new Vector3f(x, y, z), r.pointA, r.pointB)).Count() > 0) {

                            if((uplink == Uplinks.All || uplink == Uplinks.Shuriken) && Type == 3) {

                                GameHook.game.ReadValue(IntPtr.Add(VFTablePtr, 0x238), out float duration);
                                GameHook.game.ReadValue(IntPtr.Add(VFTablePtr, 0x270), out int attacks);
                                GameHook.game.ReadValue(IntPtr.Add(VFTablePtr, 0x260), out bool HC);

                                if(duration > 0.5f && duration < 100.0f && attacks >= 1 && attacks < 500 && Type == 3 && (mode == Mode.Both || Convert.ToBoolean(mode) == HC)) {
                                    c++;
                                    Console.WriteLine($"Shuriken: duration: {duration}, attacks: {attacks} , HC: {HC}");
                                    Console.WriteLine($"Pos: {x} {y} {z}");
                                    Console.WriteLine($"(0x{PtrDB.BASE_KR_Update:X}, 0x98, 0x{0x8 * (j - 1):X}, 0x128, 0xA8, 0x{0x8 * (i - 1):X}, 0x238)\n");

                                }
                            }

                            if((uplink == Uplinks.All || uplink == Uplinks.Jump) && Type == 10) {
                                GameHook.game.ReadValue(IntPtr.Add(VFTablePtr, 0x234), out float timetoactivate);
                                GameHook.game.ReadValue(IntPtr.Add(VFTablePtr, 0x238), out float duration);
                                GameHook.game.ReadValue(IntPtr.Add(VFTablePtr, 0x280), out float jumpmultiplier);
                                GameHook.game.ReadValue(IntPtr.Add(VFTablePtr, 0x284), out float jumpforward);
                                GameHook.game.ReadValue(IntPtr.Add(VFTablePtr, 0x288), out float jumpgravity);
                                GameHook.game.ReadValue(IntPtr.Add(VFTablePtr, 0x260), out bool HC);
                                List<float> parameters = new List<float> { timetoactivate, duration, jumpmultiplier, jumpforward, jumpgravity };

                                if(parameters.All(parameter => parameter > 0.5f && parameter < 100.0f) && (mode == Mode.Both || Convert.ToBoolean(mode) == HC)) {
                                    c++;
                                    Console.WriteLine($"Jumps: timetoactivate: {timetoactivate}, jumpmultiplier: {jumpmultiplier}, jumpforward: {jumpforward}, jumpgravity: {jumpgravity} , HC: {HC}");
                                    Console.WriteLine($"Pos: {x} {y} {z}");
                                    Console.WriteLine($"(0x{PtrDB.BASE_KR_Update:X}, 0x98, 0x{0x8 * (j - 1):X}, 0x128, 0xA8, 0x{0x8 * (i - 1):X}, 0x234)\n");
                                }
                            }

                            if((uplink == Uplinks.All || uplink == Uplinks.BulletTimeDevice) && Type == 4) {
                                DeepPointer CurveFloatDP = PtrDB.ObjectsTypes["curvefloat"];
                                CurveFloatDP.DerefOffsets(GameHook.game, out IntPtr CurveFloatVFT);

                                GameHook.game.ReadValue(IntPtr.Add(VFTablePtr, 0x270), out IntPtr CurveFloat);
                                if(CurveFloat != IntPtr.Zero) {
                                    GameHook.game.ReadValue(CurveFloat, out IntPtr CurveFloatVFTable);
                                    GameHook.game.ReadValue(IntPtr.Add(VFTablePtr, 0x260), out bool HC);

                                    if(CurveFloatVFT == CurveFloatVFTable && (mode == Mode.Both || Convert.ToBoolean(mode) == HC)) {
                                        c++;
                                        Console.WriteLine($"Slowmo: HC: {HC}");
                                        Console.WriteLine($"Pos: {x} {y} {z}");
                                        Console.WriteLine($"(0x{PtrDB.BASE_KR_Update:X}, 0x98, 0x{0x8 * (j - 1):X}, 0x128, 0xA8, 0x{0x8 * (i - 1):X}, 0x270, 0xA0, 0x0)\n");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine("found: " + c);
        }

        private static void ReadPtrValue(DeepPointer DP, out IntPtr ptr) {
            DP.DerefOffsets(GameHook.game, out IntPtr ptr0);
            ptr = ptr0;
        }

        private static void ReadDPValue(DeepPointer DP, out int result) {
            DP.DerefOffsets(GameHook.game, out IntPtr ptr);
            GameHook.game.ReadValue(ptr, out result);
        }

        public static void DEV_GetEnemyTypes(List<Enemy> enemies) {
            for(int i = 0; i < enemies.Count; i++) {
                List<int> offsets = new List<int>(enemies[i].GetObjectDP().GetOffsets());
                offsets[offsets.Count - 1] = 0x0;
                DeepPointer parentDP = new DeepPointer(enemies[i].GetObjectDP().GetBase(), offsets);
                IntPtr parentPtr;
                parentDP.DerefOffsets(GameHook.game, out parentPtr);
                //  pos
                int value;
                GameHook.game.ReadValue(parentPtr, out value);
                Console.WriteLine($"Enemy: {i}: {value}");
            }
        }


        public static void DEV_FindObjects(string Name, List<Room> rooms = default) {
            int c = 0;
            ReadDPValue(PtrDB.DP_SublevelsCount, out int Sublevels);

            for(var j = 1; j < Sublevels; j++) {
                ReadDPValue(new DeepPointer(PtrDB.DP_ObjectsCount).Format(0x8 * (j - 1)), out int Objects);
                for(var i = 1; i < Objects; i++) {
                    DeepPointer DP = new DeepPointer(PtrDB.DP_VFTablePattern).Format(0x8 * (j - 1), 0x8 * (i - 1), 0x18);
                    ReadDPValue(DP, out int fname);

                    string objectname = GetNameFromFName(fname);
                    if(objectname.Contains(Name)) {
                        ReadLocation(DP, 0x1d0, out Vector3f location);
                        if(rooms == default(List<Room>) || rooms.Where(r => GameUtils.PlayerWithinRectangle(location, r.pointA, r.pointB)).Count() > 0) {
                            c++;
                            Console.WriteLine($"{objectname}:\nPos: {location.X} {location.Y} {location.Z}");
                            Console.WriteLine($"Pointer:(0x04609420, 0x98, 0x{0x8 * (j - 1):X}, 0x128, 0xA8, 0x{0x8 * (i - 1):X}, 0x0)\n");
                        }
                    }
                }
            }
            Console.WriteLine("found: " + c);
        }

        private static IntPtr fNamePool;
        private static string GetNameFromFName(int key) {
            int chunkOffset = key >> 16;
            int nameOffset = (ushort)key;

            if(fNamePool == IntPtr.Zero) {
                ReadPtrValue(new DeepPointer(PtrDB.FNamePool), out fNamePool);
            }

            GameHook.game.ReadValue(IntPtr.Add(fNamePool, (chunkOffset + 2) * 0x8), out IntPtr namePoolChunk);
            IntPtr entryOffset = IntPtr.Add(namePoolChunk, 2 * nameOffset);
            GameHook.game.ReadValue(entryOffset, out Int16 nameEntry);
            int nameLength = nameEntry >> 6;

            GameHook.game.ReadString(IntPtr.Add(entryOffset, 2), nameLength, out string result);
            return result;
        }


        private static void ReadLocation(DeepPointer DP, int offset, out Vector3f location, int rootoffset = 0x130) {

            List<int> offsets = new List<int>(DP.GetOffsets());
            offsets[offsets.Count - 1] = rootoffset;
            offsets.Add(offset);

            DeepPointer PosDP = new DeepPointer(DP.GetBase(), offsets);
            PosDP.DerefOffsets(GameHook.game, out IntPtr PosPtr);

            float x, y, z;
            GameHook.game.ReadValue(PosPtr, out x);
            GameHook.game.ReadValue(PosPtr + 4, out y);
            GameHook.game.ReadValue(PosPtr + 8, out z);
            location = new Vector3f(x, y, z);
        }
    }
}
