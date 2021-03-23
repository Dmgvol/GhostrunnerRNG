using GhostrunnerRNG.Enemies;
using System;
using System.Collections.Generic;

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
            { "/Game/Levels/Test_levels/Ld_test/Cyberspace_Bramki",         MapType.ReignInHellCV},
            { "/Game/Levels/Test_levels/Ld_test/Mindhacking_Tutorial",      MapType.OverlordCV},
            { "/Game/Levels/03_HIGHTECH/03_03/03_03_World",                 MapType.TYWB},
            { "/Game/Levels/03_HIGHTECH/03_04/03_04_world",                 MapType.TheSummit},
            { "/Game/Levels/03_HIGHTECH/03_04/Cyberspace_Architect",        MapType.TheMonster}
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
            TheMonster
        }

        private static List<MapType> MapsWithoutRNG = new List<MapType>() {
            MapType.LookInsideCV,
            MapType.TheClimbCV,
            MapType.TempestCV,
            MapType.RoadToAmidaCV,
            MapType.TempestCV,
            MapType.EchoesCV,
            MapType.Gatekeeper,
            MapType.InHerOwnImage,
            MapType.SurgeCV,
            MapType.ReignInHellCV,
            MapType.TheSummit,
        };

        private static List<MapType> SupportedMaps = new List<MapType>() {
            MapType.MainMenu, 
            MapType.AwakeningLookInside, 
            MapType.Awakening, 
            MapType.LookInside,
            MapType.TheClimb, 
            MapType.JackedUp,
            MapType.BlinkCV,
            MapType.BreatheIn,
            MapType.RoadToAmida,
            MapType.RunUp,
            MapType.DharmaCity,
            MapType.Echoes,
            MapType.Faster,
            MapType.ForbiddenZone

            /*TODO:
            ReignInHell
            OverlordCV
            TYWB
            TheMonster
            */
        };

        public static bool MapHasRng(MapType type) => !MapsWithoutRNG.Contains(type);
        public static bool MapSupported(MapType type) => SupportedMaps.Contains(type);


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

        [Obsolete("This method does not fit with GR numbers, Use CreateQuaternion(yaw, pitch, roll) method")]
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

        public static bool IsNumeric(string value) => float.TryParse(value, out _);
    }
}
