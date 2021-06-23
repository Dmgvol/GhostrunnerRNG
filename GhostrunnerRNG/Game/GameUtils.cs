using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.MemoryUtils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GhostrunnerRNG.Game {
    public class GameUtils {
        public readonly static Dictionary<string, MapType> MapLevels = new Dictionary<string, MapType>() {
            { "/Game/Levels/MainMenu/MainMenu",                             MapType.MainMenu},
            { "/Game/Levels/Tutorial/L_Tutorial_Persistant",                MapType.AwakeningLookInside},
            { "/Game/Maps/damian_vr4",                                      MapType.LookInsideCV},
            { "/Game/Levels/01_INDUSTRIAL/01_01/01_01_World",               MapType.TheClimb},
            { "/Game/Maps/ragis_lvl_vr9_2J",                                MapType.TheClimbCV},
            { "/Game/Levels/Cyberspace/Furrashu_Tutorial/furasshu_tutorial",MapType.BlinkCV},
            { "/Game/Levels/Industrial/L_Industrial_Persistant",            MapType.JackedUp},
            { "/Game/Levels/01_INDUSTRIAL/01_03/01_03_world",               MapType.BreatheIn},
            { "/Game/Maps/ragis_lvl_vr10_6",                                MapType.BreatheInCV},
            { "/Game/Levels/01_INDUSTRIAL/01_04/01_04_World",               MapType.RoadToAmida},
            { "/Game/Levels/Test_Levels/Ld_test/01_04_Cyberspace",          MapType.RoadToAmidaCV},
            { "/Game/Maps/Force_Push_Tutorial",                             MapType.TempestCV},
            { "/Game/Levels/01_INDUSTRIAL/01_05/01_05_World",               MapType.RunUpGatekeeper},
            { "/Game/Levels/02_CYBERCITY/02_01/02_01_world",                MapType.DharmaCity},
            { "/Game/Levels/02_CYBERCITY/02_02/02_02_world",                MapType.Echoes},
            { "/Game/Maps/ragis_lvl_vr5",                                   MapType.EchoesCV},
            { "/Game/Levels/02_CYBERCITY/02_03/02_03_World",                MapType.FasterInHerOwnImage},
            { "/Game/Levels/Cyberspace/Nami_Tutorial",                      MapType.SurgeCV},
            { "/Game/Levels/03_HIGHTECH/03_01/03_01_World",                 MapType.ForbiddenZone},
            { "/Game/Levels/03_HIGHTECH/03_02/03_02_world",                 MapType.ReignInHell},
            { "/Game/Levels/Test_Levels/Ld_test/Cyberspace_Bramki",         MapType.ReignInHellCV},
            { "/Game/Levels/Test_Levels/Ld_test/Mindhacking_Tutorial",      MapType.OverlordCV},
            { "/Game/Levels/03_HIGHTECH/03_03/03_03_World",                 MapType.TYWB},
            { "/Game/Levels/03_HIGHTECH/03_04/03_04_world",                 MapType.TheSummit},
            { "/Game/Levels/03_HIGHTECH/03_04/Cyberspace_Architect",        MapType.TheMonster},
            // Killruns
            { "/Game/Levels/KILLRUN/KILLRUN_01/Killrun_01_World",           MapType.KillRun1},
            { "/Game/Levels/KILLRUN/KILLRUN_04/KILLRUN_04_WORLD",           MapType.KillRun2},
            { "/Game/Levels/KILLRUN/KILLRUN_03/KILLRUN_03_WORLD",           MapType.KillRun3},
            { "/Game/Levels/KILLRUN/KILLRUN_02/Killrun_02_world",           MapType.KillRun4},
            { "/Game/Levels/KILLRUN/KILLRUN_05/Killrun_05_World",           MapType.KillRun5}
        };

        public enum MapType {
            Unknown,
            MainMenu,
            AwakeningLookInside, // map combination
            Awakening,
            LookInside,
            LookInsideCV,
            TheClimb,
            TheClimbCV,
            BlinkCV,
            JackedUp,
            BreatheIn,
            BreatheInCV,
            RoadToAmida,
            RoadToAmidaCV,
            TempestCV,
            RunUpGatekeeper,    // map combination
            RunUp,
            Gatekeeper,
            DharmaCity,
            Echoes,
            EchoesCV,
            FasterInHerOwnImage,// map combination
            Faster,
            InHerOwnImage,
            SurgeCV,
            ForbiddenZone,
            ReignInHell,
            ReignInHellCV,
            OverlordCV,
            TYWB,
            TheSummit,
            TheMonster,

            // Killruns
            KillRun1, KillRun2, KillRun3, KillRun4, KillRun5
        }

        private static List<MapType> MapsWithoutRNG = new List<MapType>() {
            MapType.InHerOwnImage,
            MapType.TheSummit
        };

        private static List<MapType> SupportedMaps = new List<MapType>() {
            MapType.MainMenu, 
            MapType.AwakeningLookInside, 
            MapType.Awakening, 
            MapType.LookInside,
            MapType.LookInsideCV,
            MapType.TheClimb,
            MapType.TheClimbCV,
            MapType.JackedUp,
            MapType.BlinkCV,
            MapType.BreatheIn,
            MapType.BreatheInCV,
            MapType.RoadToAmida,
            MapType.RoadToAmidaCV,
            MapType.TempestCV,
            MapType.RunUp,
            MapType.Gatekeeper,
            MapType.DharmaCity,
            MapType.Echoes,
            MapType.EchoesCV,
            MapType.Faster,
            MapType.SurgeCV,
            MapType.ForbiddenZone,
            MapType.ReignInHell,
            MapType.ReignInHellCV,
            MapType.OverlordCV,
            MapType.TYWB,
            MapType.TheMonster,
        };


        private static List<MapType> SupportedHCMaps = new List<MapType>() {
                MapType.AwakeningLookInside,
            MapType.Awakening,
            MapType.LookInside,
            MapType.TheClimb,
            MapType.JackedUp,
            MapType.BreatheIn,
            MapType.RoadToAmida,
                MapType.RunUpGatekeeper,
            MapType.RunUp,
            //MapType.Gatekeeper,
            MapType.DharmaCity,
            //MapType.Echoes,
            //MapType.Faster,
            //MapType.ForbiddenZone,
            //MapType.ReignInHell,
            //MapType.TYWB,
            //MapType.TheMonster,
        };

        private static List<MapType> CVMaps = new List<MapType>() {
            MapType.LookInsideCV,
            MapType.TheClimbCV,
            MapType.BreatheInCV,
            MapType.BlinkCV,
            MapType.RoadToAmidaCV,
            MapType.TempestCV,
            MapType.EchoesCV,
            MapType.SurgeCV,
            MapType.ReignInHellCV,
            MapType.OverlordCV
        };

        public static bool IsCVMap(MapType type) => CVMaps.Contains(type);
        public static bool MapHasRng(MapType type) => !MapsWithoutRNG.Contains(type);
        public static bool MapSupported(MapType type) => SupportedMaps.Contains(type);
        public static bool HCMapSupported(MapType type) => SupportedHCMaps.Contains(type);



        public static MapType GetMapType(string fullName) {
            if(string.IsNullOrEmpty(fullName)) return MapType.Unknown;
            return MapLevels.ContainsKey(fullName) ? MapLevels[fullName] : MapType.MainMenu;
        }

        /// <summary>
        /// Checks if player within the rectangle of 2 points of the plane/box
        /// </summary>
        /// <param name="player">Player Pos</param>
        /// <param name="posA">Pos A of rec</param>
        /// <param name="posB">Pos B of rec, opposite point</param>
        /// <returns>True if player is inside rec</returns>
        public static bool PlayerWithinRectangle(Vector3f player, Vector3f posA, Vector3f posB) {
            Vector3f A = new Vector3f(Math.Min(posA.X, posB.X), Math.Min(posA.Y, posB.Y), Math.Min(posA.Z, posB.Z));
            Vector3f B = new Vector3f(Math.Max(posA.X, posB.X), Math.Max(posA.Y, posB.Y), Math.Max(posA.Z, posB.Z));
            return (player.X > A.X && player.Y > A.Y && player.Z > A.Z &&
                player.X < B.X && player.Y < B.Y && player.Z < B.Z);
        }

        /// <summary>
        /// Turret 3D Quaternion rotation calculation, for 4 axis enemy/object rotation
        /// </summary>
        /// <param name="orientation">fixed type, for turret</param>
        /// <param name="angleSin">angleSin</param>
        /// <param name="angleCos">angleCos</param>
        /// <returns>4-axis Quaternion rotations</returns>
        public static Quaternion CreateTurretQuaternion(EnemyTurret.TurretOrientation orientation, float angleSin, float angleCos) {
            Quaternion q = new Quaternion(0, 0, angleSin, angleCos);
            if(orientation == EnemyTurret.TurretOrientation.Ceiling) {
                q = new Quaternion(angleCos, angleSin, 0, 0); // angles are flipped between them
            } else if(orientation == EnemyTurret.TurretOrientation.WallLeft) {
                // get angle from Sin/Cos angles
                double angleRadian = (angleSin > 0) ? Math.Acos(angleCos) : -Math.Acos(angleCos);
                double angleDegrees = angleRadian * 180 / Math.PI;
                q = CreateQuaternion((float)angleDegrees * 2, 0, -90f); // with -90 roll as left wall
            } else if(orientation == EnemyTurret.TurretOrientation.WallRight) {
                // get angle from Sin/Cos angles
                double angleRadian = (angleSin > 0) ? Math.Acos(angleCos) : -Math.Acos(angleCos);
                double angleDegrees = angleRadian * 180 / Math.PI;
                q = CreateQuaternion((float)angleDegrees * 2, 0, 90f); // with 90 roll as right wall
            }
            return q;
        }

        /// <summary>
        /// 3D Quaternion rotation calculation, for 4 axis enemy/object rotation using YawPitchRoll
        /// </summary>
        /// <returns>4-axis Quaternion rotations</returns>
        public static Quaternion CreateQuaternion(float angleYaw, float anglePitch, float angleRoll) {
            return CreateFromYawPitchRoll((float)(angleYaw * Math.PI / 180), (float)(anglePitch * Math.PI / 180), (float)(angleRoll * Math.PI / 180));
        }

        //// Raw quaternion formula, use others to fit GR numbers
        private static Quaternion CreateFromYawPitchRoll(float yaw, float pitch, float roll) {
            float rollOver2 = roll * 0.5f;
            float sinRollOver2 = (float)Math.Sin(rollOver2);
            float cosRollOver2 = (float)Math.Cos(rollOver2);
            float pitchOver2 = pitch * 0.5f;
            float sinPitchOver2 = (float)Math.Sin(pitchOver2);
            float cosPitchOver2 = (float)Math.Cos(pitchOver2);
            float yawOver2 = yaw * 0.5f;
            float sinYawOver2 = (float)Math.Sin(yawOver2);
            float cosYawOver2 = (float)Math.Cos(yawOver2);
            float X, Y, Z, W;
            W = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2;
            X = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2;
            Y = cosYawOver2 * sinPitchOver2 * cosRollOver2 + sinYawOver2 * cosPitchOver2 * sinRollOver2;
            Z = sinYawOver2 * cosPitchOver2 * cosRollOver2 - cosYawOver2 * sinPitchOver2 * sinRollOver2;
            return new Quaternion() { x = X, y = Y, z = Z, w = W };
        }

        // 4-Axis struct, for quaternion
        public struct Quaternion {
            public float x, y, z, w;
            public Quaternion(float x, float y, float z, float w) {this.x = x; this.y = y; this.z = z; this.w = w; }
            public string ToString(bool withF = false) => $"{x.ToString("0.00")}{(withF ? $"f" : "")}, {y.ToString("0.00")}{(withF ? $"f" : "")}, {z.ToString("0.00")}{(withF ? $"f" : "")}, {w.ToString("0.00")}{(withF ? $"f" : "")}";
            public override string ToString() => $"{x.ToString("0.00")}, {y.ToString("0.00")}, {z.ToString("0.00")}, {w.ToString("0.00")}";
        }

        //[Obsolete("This method does not fit with GR numbers, Use CreateQuaternion(yaw, pitch, roll) method")]
        public static Vector3f QuaternionToEuler(Quaternion q) {
            float x, y, z;
            float unit = (q.x * q.x) + (q.y * q.y) + (q.z * q.z) + (q.w * q.w);
            float test = q.x * q.w - q.y * q.z;
            if(test > 0.4995f * unit) // singularity at north pole
            {
                x = (float)(Math.PI / 2f);
                y = (float)(2f * Math.Atan2(q.y, q.x));
                z = 0;
            } else if(test < -0.4995f * unit) // singularity at south pole
              {
                x = (float)-Math.PI / 2;
                y = (float)(-2f * Math.Atan2(q.y, q.x));
                z = 0;
            } else // no singularity - this is the majority of cases
              {
                x = (float)Math.Asin(2f * (q.w * q.x - q.y * q.z));
                y = (float)Math.Atan2(2f * q.w * q.y + 2f * q.z * q.x, 1 - 2f * (q.x * q.x + q.y * q.y));
                z = (float)Math.Atan2(2f * q.w * q.z + 2f * q.x * q.y, 1 - 2f * (q.z * q.z + q.x * q.x));
            }
            Vector3f euler = new Vector3f((float)(x * (180.0 / Math.PI)), (float)(y * (180.0 / Math.PI)), (float)(z * (180.0 / Math.PI)));
            euler.X %= 360;
            euler.Y %= 360;
            euler.Z %= 360;
            return euler;
        }


        private static EasyPointers EnemyTypeEP = new EasyPointers();
        public static void DEV_PrintEnemyTypes(Process game, List<Enemy> enemies) {
            // pointers
            if(EnemyTypeEP.Pointers.Count == 0) {
                EnemyTypeEP.Add("pistol", new DeepPointer(0x3189000 +  0x17f240));
                EnemyTypeEP.Add("turret", new DeepPointer(0x3189000 + 0x189A00));
                EnemyTypeEP.Add("drone", new DeepPointer(0x3189000 + 0x17FC58));
                EnemyTypeEP.Add("ball", new DeepPointer(0x3189000 + 0x18A6E0));
                EnemyTypeEP.Add("splitter", new DeepPointer(0x3189000 + 0x18B208));
                EnemyTypeEP.Add("meleeShifter", new DeepPointer(0x3189000 + 0x18D238));
                EnemyTypeEP.Add("shooterShifter", new DeepPointer(0x3189000 + 0x18D238));
                EnemyTypeEP.Add("shield", new DeepPointer(0x3189000 + 0x18FF70));
                EnemyTypeEP.Add("uzi", new DeepPointer(0x3189000 + 0x194810));
                EnemyTypeEP.Add("gecko", new DeepPointer(0x3189000 + 0x195B48));
                EnemyTypeEP.Add("frogger", new DeepPointer(0x3189000 + 0x185198));
                EnemyTypeEP.Add("weeb", new DeepPointer(0x3189000 + 0x1879E0));
                EnemyTypeEP.Add("spider", new DeepPointer(0x3189000 + 0x193520));
                EnemyTypeEP.Add("sniper", new DeepPointer(0x3189000 + 0x191900));
                EnemyTypeEP.Add("hel", new DeepPointer(0x3189000 + 0x1705BB));
                EnemyTypeEP.Add("mara", new DeepPointer(0x3189000 + 0x174198));
            }

            EnemyTypeEP.DerefPointers(game);

            // compare data
            for(int i = 0; i < enemies.Count; i++) {
                // create 0x0 DP of enemy
                var dp = enemies[i].GetObjectDP();
                var offsets = dp.GetOffsets();
                offsets[offsets.Count - 1] = 0x0;
                // deref DP
                DeepPointer baseDP = new DeepPointer(dp.GetBase(), offsets);
                IntPtr enemyBasePtr;
                baseDP.DerefOffsets(game, out enemyBasePtr);
                // read value
                byte[] enemyValue = new byte[8];
                game.ReadBytes(enemyBasePtr, 8, out enemyValue);
                // get IndentityAddressHEX and compare it with enemy value as HEX
                string enemyHex = BitConverter.ToString(enemyValue.Reverse().ToArray()).Replace("-", string.Empty).Remove(0, 4);
                string enemyType = EnemyTypeEP.Pointers.FirstOrDefault(x => x.Value.Item2.ToString("x8").ToUpper() == enemyHex).Key;
                // print type
                Console.WriteLine($"{i}:{enemyType}");
            }
        }

        public static void DEV_PrintEnemyTypes_Bulk(Process game, List<Enemy> AllEnemies, params Room[] rooms) {
            for(int i = 0; i < rooms.Length; i++) {
                List<Enemy> enemies = rooms[i].ReturnEnemiesInRoom(AllEnemies);
                Console.WriteLine($"\nRoom {i+1}:");
                DEV_PrintEnemyTypes(game, enemies);
            }
        }

        public static VirtualKeyCode? ConvertKeybindToKey(int keybind) {
            // A - Z
            if(keybind >= 96246 && keybind <= 96296) {
                keybind -= 96246;
                keybind /= 2;
                return VirtualKeyCode.VK_A + keybind;
            }

            // 0 - 9
            if(keybind >= 96213 && keybind <= 96244) {
                keybind -= 96213;
                keybind /= 3;
                return VirtualKeyCode.VK_0 + keybind;
            }

            // F1 - F10
            if(keybind >= 96383 && keybind <= 96401) {
                keybind -= 96383;
                keybind /= 2;
                return VirtualKeyCode.F1 + keybind;
            }

            // Numpad0 - Numpad9
            if(keybind >= 96298 && keybind <= 96355) {
                keybind -= 96298;
                keybind /= 6;
                return VirtualKeyCode.NUMPAD0 + keybind;
            }

            // Arrows
            if(keybind >= 96193 && keybind <= 96202) {
                keybind -= 96193;
                keybind /= 3;
                return VirtualKeyCode.LEFT + keybind;
            }

            // special
            switch(keybind) {
                case 96404: // F11
                    return VirtualKeyCode.F11;
                case 96407: // F12
                    return VirtualKeyCode.F12;
                case 96503:
                    return VirtualKeyCode.OEM_3; // ~ (tilda)
                case 96153:
                    return VirtualKeyCode.TAB;
                case 96164:
                    return VirtualKeyCode.CAPITAL; // Caps lock
                case 96421:
                    return VirtualKeyCode.LSHIFT;
                case 96433:
                    return VirtualKeyCode.LCONTROL;
                case 96457:
                    return VirtualKeyCode.LWIN; // I doubt some one will use win key for restart, but okay
                case 96447:
                    return VirtualKeyCode.LMENU; // LAlt
                case 96173:
                    return VirtualKeyCode.SPACE;
                case 96452:
                    return VirtualKeyCode.RMENU; // RAlt
                case 96464:
                    return VirtualKeyCode.RWIN;
                case 96440:
                    return VirtualKeyCode.RCONTROL;
                case 96427:
                    return VirtualKeyCode.RSHIFT;
                case 96156:
                    return VirtualKeyCode.RETURN; // Enter!
                case 96523:
                    return VirtualKeyCode.OEM_5; // \
                case 96147:
                    return VirtualKeyCode.BACK;

                // above arrows
                case 96415:
                    return VirtualKeyCode.SCROLL;
                case 96160:
                    return VirtualKeyCode.PAUSE;
                case 96205:
                    return VirtualKeyCode.INSERT;
                case 96190:
                    return VirtualKeyCode.HOME;
                case 96178:
                    return VirtualKeyCode.PRIOR; // Page up
                case 96182:
                    return VirtualKeyCode.NEXT; // Page down
                case 96187:
                    return VirtualKeyCode.END;
                case 96209:
                    return VirtualKeyCode.DELETE;

                // NumPad specials
                case 96410:
                    return VirtualKeyCode.NUMLOCK;
                case 96379: // numpad divide
                    return VirtualKeyCode.DIVIDE;
                case 96361: // numpad multiply 
                    return VirtualKeyCode.MULTIPLY;
                case 96369:  // numpad sub
                    return VirtualKeyCode.SUBTRACT;
                case 96366:
                    return VirtualKeyCode.ADD;
                case 96374: // numpad dot
                    return VirtualKeyCode.DECIMAL;

                // Others
                case 96471: // ;
                    return VirtualKeyCode.OEM_1;
                case 96477: // = +
                    return VirtualKeyCode.OEM_PLUS;
                case 96481: // ,
                    return VirtualKeyCode.OEM_COMMA;
                case 96485: // _ -
                    return VirtualKeyCode.OEM_MINUS;
                case 96507: // [
                    return VirtualKeyCode.OEM_4;
                case 96529: // ]
                    return VirtualKeyCode.OEM_6;
                case 96545: // '
                    return VirtualKeyCode.OEM_7;
                case 96495: // .
                    return VirtualKeyCode.OEM_PERIOD;
                case 96499: // /
                    return VirtualKeyCode.OEM_2;

                // Mouse
                case 96118: // middle mouse button
                    return VirtualKeyCode.MBUTTON;

            }
            return null;
        }

        public static bool IsNumeric(string value) => float.TryParse(value, out _);
    }
}
