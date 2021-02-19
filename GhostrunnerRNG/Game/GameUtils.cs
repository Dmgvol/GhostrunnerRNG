﻿using System;
using System.Collections.Generic;

namespace GhostrunnerRNG.Game {
    public class GameUtils {
        public readonly static Dictionary<string, MapType> MapLevels = new Dictionary<string, MapType>() {
            { "/Game/Levels/MainMenu/MainMenu",                             MapType.MainMenu},
            { "/Game/Levels/Tutorial/L_Tutorial_Persistant",                MapType.AwakeningLookInside},
            { "/Game/Maps/damian_vr4",                                      MapType.LookInsideCV},
            { "/Game/Levels/01_INDUSTRIAL/01_01/01_01_World",               MapType.TheClimb},
            { "/Game/Maps/ragis_lvl_vr9_2J",                                MapType.ThEClimbCV},
            { "/Game/Levels/Cyberspace/Furrashu_Tutorial/furasshu_tutorial",MapType.BlinkCV},
            { "/Game/Levels/Industrial/L_Industrial_Persistant",            MapType.JackedUp},
            { "/Game/Levels/01_INDUSTRIAL/01_03/01_03_world",               MapType.BreatheIn},
            { "/Game/Maps/ragis_lvl_vr10_6",                                MapType.BreatheInCV},
            { "/Game/Levels/01_INDUSTRIAL/01_04/01_04_World",               MapType.RoadToAmida},
            { "/Game/Levels/Test_levels/Ld_test/01_04_Cyberspace",          MapType.RoadToAmidaCV},
            { "/Game/Maps/Force_Push_Tutorial",                             MapType.TempestCV},
            { "/Game/Levels/01_INDUSTRIAL/01_05/01_05_World",               MapType.RunUp},
            { "/Game/Levels/01_INDUSTRIAL/01_05/01_05_world ",              MapType.Gatekeeper},
            { "/Game/Levels/02_CYBERCITY/02_01/02_01_world",                MapType.DharmaCity},
            { "/Game/Levels/02_CYBERCITY/02_02/02_02_world",                MapType.Echoes},
            { "/Game/Maps/ragis_lvl_vr5",                                   MapType.EchoesCV},
            { "/Game/Levels/02_CYBERCITY/02_03/02_03_World",                MapType.Faster},
            { "Game/Levels/02_CYBERCITY/02_03/02_03_world",                 MapType.InHerOwnImage},
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
            AwakeningLookInside,
            Awakening,
            LookInside,
            LookInsideCV,
            TheClimb,
            ThEClimbCV,
            BlinkCV,
            JackedUp,
            BreatheIn,
            BreatheInCV,
            RoadToAmida,
            RoadToAmidaCV,
            TempestCV,
            RunUp,
            Gatekeeper,
            DharmaCity,
            Echoes,
            EchoesCV,
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

        // Spawn Rectangle: Awakening
        public static readonly Vector3f SpawnRect_Awakening_PointA = new Vector3f(-20280, -67178, 2099);
        public static readonly Vector3f SpawnRect_Awakening_PointB = new Vector3f(-20084, -66971, 2100);
        // Spawn Rectangle: LookInside
        public static readonly Vector3f SpawnRect_LookInside_PointA = new Vector3f(130553, -68616, 2400);
        public static readonly Vector3f SpawnRect_LookInside_PointB = new Vector3f(130053, -67783, 1050);

        public static MapType GetMapName(string fullName) {
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
    }
}
