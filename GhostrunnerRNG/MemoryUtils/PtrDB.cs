using System;
using System.Collections.Generic;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.MemoryUtils {
    public static class PtrDB {
        // BASE ADDRESSES
        public const Int32 BASE_KR_Update = 0x04609420;

        //// Main Module ////
        public static readonly DeepPointer DP_Module_Capsule = new DeepPointer(0x0438BB50, 0x30, 0x130, 0x0);
        public static readonly DeepPointer DP_Module_MapName = new DeepPointer(0x0438BB40, 0x30, 0xF8, 0x0);
        public static readonly DeepPointer DP_Module_PreciseTime = new DeepPointer(BASE_KR_Update, 0x138, 0xB0, 0x128);
        public static readonly DeepPointer DP_Module_HC = new DeepPointer(0x0438BB40, 0x330, 0x30);
        public static readonly DeepPointer DP_Module_Loading = new DeepPointer(0x044C4478, 0x1E8);
        public static readonly DeepPointer DP_Module_ReloadCounter = new DeepPointer(BASE_KR_Update, 0x128, 0x388);

        //// Localization ////
        public static readonly DeepPointer DP_Settings_Lang = new DeepPointer(0x0438D7F8, 0x58, 0x60);
        public static readonly DeepPointer DP_MenuTitle = new DeepPointer(0x044629B0, 0x3E8, 0x70, 0x2F0, 0x20, 0x0);
        public static readonly DeepPointer DP_MenuTitleLength = new DeepPointer(0x044629B0, 0x3E8, 0x70, 0x2F0, 0x28);
        public static readonly DeepPointer DP_MenuDes = new DeepPointer(0x044629B0, 0x3E8, 0x70, 0x2F0, 0x0, 0x0);
        public static readonly DeepPointer DP_MenuDescLength = new DeepPointer(0x044629B0, 0x3E8, 0x70, 0x2F0, 0x8);

        // Enemies - Genral
        public static readonly DeepPointer DP_EnemyListFirstEntity = new DeepPointer(BASE_KR_Update, 0x138, 0xB0, 0xB0, 0x20, 0x4F0);
        public static readonly DeepPointer DP_SurgeCV_EnemyEntity = new DeepPointer(BASE_KR_Update, 0x30, 0xA8, 0x0, 0x130, 0x1D0);
        public static readonly DeepPointer DP_Drone_Dharma_Patrol = new DeepPointer(BASE_KR_Update, 0x98, 0x28, 0x128, 0xA8, 0x238, 0x130, 0x1D0);

        // Interactive Objects
        public static readonly DeepPointer DP_InteractiveObjPattern = new DeepPointer(BASE_KR_Update, 0x98, 0x0, 0x128, 0xA8, 0x0, 0x248, 0x1D0);

        // Loading Indicators
        public static readonly DeepPointer DP_RestartFlag = new DeepPointer(0x0438D7F8, 0x58, 0xB0);
        public static readonly DeepPointer DP_RestartBind = new DeepPointer(0x0438D7F8, 0x70, 0x60, 0x188);
        public static readonly DeepPointer DP_CutsceneTimer = new DeepPointer(BASE_KR_Update, 0x128, 0x38C);
        public static readonly DeepPointer DP_CanRestart = new DeepPointer(BASE_KR_Update, 0x188, 0x2EB);

        ////// Cybervoid //////
        // Amida CV
        public static readonly DeepPointer DP_AmidaCV_Platform = new DeepPointer(BASE_KR_Update, 0x210, 0x0, 0x1D0); // visual center
        public static readonly DeepPointer DP_AmidaCV_PlatformBoxCenter = new DeepPointer(BASE_KR_Update, 0x210, 0x0, 0x398, 0xA0);// box center
        public static readonly DeepPointer DP_AmidaCV_Box = new DeepPointer(BASE_KR_Update, 0x1F8, 0x60, 0xD0, 0x8C0, 0xB0, 0x570, 0x1A8, 0x0); // collision/box center coords
        // Echoes CV
        public static readonly DeepPointer DP_EchoesCV_Rotation = new DeepPointer(BASE_KR_Update, 0x30, 0xE8, 0x10, 0x298, 0x60, 0x2C0);
        // Blink CV
        public static readonly DeepPointer DP_BlinkCV_Platform1 = new DeepPointer(BASE_KR_Update, 0x30, 0xA8, 0x138);
        public static readonly DeepPointer DP_BlinkCV_Platform2 = new DeepPointer(BASE_KR_Update, 0x30, 0xA8, 0x128);
        public static readonly DeepPointer DP_BlinkCV_Platform3 = new DeepPointer(BASE_KR_Update, 0x30, 0xA8, 0x130);
        // Climb CV
        public static readonly DeepPointer DP_ClimbCV_Tetris_Hologram = new DeepPointer(BASE_KR_Update, 0x30, 0xA8, 0x0, 0x230, 0x1D0);
        public static readonly DeepPointer DP_ClimbCV_Tetris_CenterBox = new DeepPointer(BASE_KR_Update, 0x30, 0xA8, 0x0, 0x220, 0x398, 0x150);
        public static readonly DeepPointer DP_ClimbCV_Tetris_Box = new DeepPointer(BASE_KR_Update, 0x1F8, 0x60, 0xD0, 0x8C0, 0xB0, 0x5A0, 0x1A8, 0x0);
        public static readonly DeepPointer DP_ClimbCV_Tetris_Particles = new DeepPointer(BASE_KR_Update, 0x30, 0xA8, 0x0, 0x130, 0x1D0);
        // LookInside CV
        public static readonly DeepPointer DP_LookInsideCV_GreenPlatform = new DeepPointer(BASE_KR_Update, 0x30, 0xA8, 0x0, 0x130, 0x1D0);
        public static readonly DeepPointer DP_LookInsideCV_GreenPlatform_SphereCenter = new DeepPointer(BASE_KR_Update, 0x30, 0xA8, 0x0, 0x230, 0x398, 0x150);
        public static readonly DeepPointer DP_LookInsideCV_GreenPlatform_SphereBox = new DeepPointer(BASE_KR_Update, 0x1F8, 0x60, 0xD0, 0x8C0, 0xB0, 0x5A0, 0x1A8, 0x0);
        public static readonly DeepPointer DP_LookInsideCV_GreenPlatform_OldCollision = new DeepPointer(BASE_KR_Update, 0x30, 0xA8, 0x0, 0x238, 0x398, 0x150);
        // BreatheIn CV
        public static readonly DeepPointer DP_BreatheInCV_CVButton = new DeepPointer(BASE_KR_Update, 0x30, 0xA8, 0x0, 0x248, 0x398, 0x150);
        public static readonly DeepPointer DP_BreatheInCV_CVButtonBox = new DeepPointer(BASE_KR_Update, 0x1F8, 0x60, 0xD0, 0x8C0, 0xB0, 0x5A0, 0x1A8, 0x0);
        // Keys
        public static readonly DeepPointer DP_CVKey_Hologram = new DeepPointer(BASE_KR_Update, 0x30, 0xA8, 0x0, 0x130, 0x1D0);
        public static readonly DeepPointer DP_CVKey_Center = new DeepPointer(BASE_KR_Update, 0x30, 0xA8, 0x0, 0x220, 0x398, 0x150);
        public static readonly DeepPointer DP_CVKey_Box = new DeepPointer(BASE_KR_Update, 0x1F8, 0x60, 0xD0, 0x8C0, 0xB0, 0x5A0, 0x1A8, 0x0);
        // Orbs
        public static readonly DeepPointer DP_CVOrb = new DeepPointer(BASE_KR_Update, 0x30, 0xA8, 0x0, 0x248, 0x1D0);
        public static readonly DeepPointer DP_CVOrb_BoxOrigin = new DeepPointer(BASE_KR_Update, 0x30, 0xA8, 0x0, 0x238, 0x398, 0x150);
        public static readonly DeepPointer DP_CVOrb_Box = new DeepPointer(BASE_KR_Update, 0x1F8, 0x60, 0xD0, 0x8C0, 0xB0, 0x5A0, 0x1A8, 0x0);
        public static readonly DeepPointer DP_CVOrb_Box_RiH = new DeepPointer(BASE_KR_Update, 0x1F8, 0x60, 0xD0, 0x298, 0x0, 0xB0, 0x5A0, 0x1A8, 0x0);
        public static readonly DeepPointer DP_CVOrb_Echoes = new DeepPointer(BASE_KR_Update, 0x30, 0xA8, 0x0, 0x220, 0x11c);


        ////// Level-Specific //////
        public static readonly DeepPointer DP_Awakening_BestTime = new DeepPointer(0x0438BB40, 0xE0, 0xC8, 0x30, 0xF0, 0x8);
        public static readonly DeepPointer DP_Tom_Laser = new DeepPointer(BASE_KR_Update, 0x98, 0x30, 0x128, 0xA8, 0x708, 0x0);
        public static readonly DeepPointer DP_Tom_Rotation = new DeepPointer(BASE_KR_Update, 0x98, 0x30, 0x128, 0xA8, 0x708, 0x840, 0xA0, 0x20);
        public static readonly DeepPointer DP_Faster_ForcedSliderTrigger = new DeepPointer(BASE_KR_Update, 0x98, 0x10, 0x128, 0xA8, 0x3C0, 0x230, 0x398, 0x150);
        public static readonly DeepPointer DP_SignTrigger_Scan = new DeepPointer(BASE_KR_Update, 0x1F8, 0x60, 0xD0, 0x298, 0x830, 0xB0, 0x5A0, 0x1A8, 0x0);
        public static readonly DeepPointer DP_RiH_DoorTrigger = new DeepPointer(BASE_KR_Update, 0x98, 0x0, 0x128, 0xA8, 0xD00, 0x5C);
        public static readonly DeepPointer DP_RiH_HC_Room14_Shuriken = new DeepPointer(BASE_KR_Update, 0x30, 0xA8, 0x90);

        public static readonly DeepPointer DP_HCEchoes_Sniperpoints1 = new DeepPointer(BASE_KR_Update, 0x98, 0x28, 0x128, 0xA8, 0xB68);
        public static readonly DeepPointer DP_HCEchoes_Sniperpoints2 = new DeepPointer(BASE_KR_Update, 0x98, 0x30, 0x128, 0xA8, 0xBD8);
        public static readonly int SignTrigger_ScanLength = 0x19000;
        public static readonly string SignTrigger1_Signature = "9A F1 42 C6 B0 C6 1B 48 33 D3 BA 44 66 BE 2E C6 50 CC 1D 48 66 96 21 45";
        public static readonly string SignTrigger2_Signature = "EE C3 41 C6 66 BA 30 48 6A F6 27 43 54 FC 2E C6 9A CE 30 48 33 81 9A 44";


        ////// Checkpoints //////
        public static readonly DeepPointer DP_TYWB_ElevatorCP = new DeepPointer(BASE_KR_Update, 0x98, 0x0, 0x128, 0xA8, 0xD0, 0x248, 0x1D0);
        public static readonly DeepPointer DP_RunUp_ElevatorCP = new DeepPointer(BASE_KR_Update, 0x98, 0x0, 0x128, 0xA8, 0xA98, 0x248, 0x1D0);
        public static readonly DeepPointer DP_RoadToAmida_ElevatorCP = new DeepPointer(BASE_KR_Update, 0x98, 0x0, 0x128, 0xA8, 0xD0, 0x248, 0x1D0);
        public static readonly DeepPointer DP_ReignInHell_ElevatorCP = new DeepPointer(BASE_KR_Update, 0x98, 0x0, 0x128, 0xA8, 0x58, 0x248, 0x1D0);
        public static readonly DeepPointer DP_JackedUp_ElevatorCP = new DeepPointer(BASE_KR_Update, 0x98, 0x8, 0x128, 0xA8, 0x30, 0x248, 0x1D0);
        public static readonly DeepPointer DP_ForbiddenZone_ElevatorCP = new DeepPointer(BASE_KR_Update, 0x98, 0x8, 0x128, 0xA8, 0x78, 0x248, 0x1D0);
        public static readonly DeepPointer DP_Echoes_ElevatorCP = new DeepPointer(BASE_KR_Update, 0x98, 0x30, 0x128, 0xA8, 0x1280, 0x248, 0x1D0);
        public static readonly DeepPointer DP_BreatheIn_ElevatorCP = new DeepPointer(BASE_KR_Update, 0x98, 0x8, 0x128, 0xA8, 0x40, 0x248, 0x1D0);
        public static readonly DeepPointer DP_Dharma_ElevatorCP = new DeepPointer(BASE_KR_Update, 0x98, 0x0, 0x128, 0xA8, 0xE8, 0x248, 0x1D0);
        public static readonly DeepPointer DP_Summit_ElevatorCP = new DeepPointer(BASE_KR_Update, 0x98, 0x0, 0x128, 0xA8, 0x70, 0x248, 0x1D0);
        // specific checkpoints
        public static readonly DeepPointer DP_BreatheIn_Room15_CP = new DeepPointer(BASE_KR_Update, 0x98, 0x80, 0x128, 0xA8, 0xD0, 0x248, 0x1D0);
        public static readonly DeepPointer DP_BreatheIn_HC_Room6_CP = new DeepPointer(BASE_KR_Update, 0x98, 0x80, 0x128, 0xA8, 0xC0, 0x248, 0x1D0);
        public static readonly DeepPointer DP_Dharma_HC_Room5_CP = new DeepPointer(BASE_KR_Update, 0x98, 0x10, 0x128, 0xA8, 0x168, 0x248, 0x1D0);
        public static readonly DeepPointer DP_Faster_Train1_CP = new DeepPointer(BASE_KR_Update, 0x98, 0x18, 0x128, 0xA8, 0x1760, 0x240, 0x398, 0x150); // train spawn
        public static readonly DeepPointer DP_Faster_TrainLast_CP = new DeepPointer(BASE_KR_Update, 0x98, 0x18, 0x128, 0xA8, 0x1720, 0x240, 0x398, 0x150); // before EoL
        public static readonly DeepPointer DP_ForbiddenZone_Room2_CP = new DeepPointer(BASE_KR_Update, 0x98, 0x18, 0x128, 0xA8, 0x210, 0x248, 0x1D0);
        public static readonly DeepPointer DP_RoadToAmida_HC_Room11_CP = new DeepPointer(BASE_KR_Update, 0x98, 0x30, 0x128, 0xA8, 0xF68, 0x248, 0x1D0);
        public static readonly DeepPointer DP_ForbiddenZone_HC_Room2_CP = new DeepPointer(BASE_KR_Update, 0x98, 0x18, 0x128, 0xA8, 0x210, 0x248, 0x1D0);

        ////// WorldObjects //////
        public static readonly DeepPointer DP_WorldTrigger = new DeepPointer(BASE_KR_Update, 0x98, 0x0, 0x128, 0xA8, 0x0, 0x220, 0x398, 0x150); // Faster, sign triggers

        ////// NonPlaceableObjects //////
        public static readonly DeepPointer DP_Billboard = new DeepPointer(BASE_KR_Update, 0x98, 0x0, 0x128, 0xA8, 0x0);
        public static readonly DeepPointer DP_UplinkSlowmo = new DeepPointer(BASE_KR_Update, 0x98, 0x0, 0x128, 0xA8, 0x0, 0x270, 0x0);
        public static readonly DeepPointer DP_UplinkShurikens = new DeepPointer(BASE_KR_Update, 0x98, 0x0, 0x128, 0xA8, 0x0);
        public static readonly DeepPointer DP_UplinkJump = new DeepPointer(BASE_KR_Update, 0x98, 0x0, 0x128, 0xA8, 0x0);
        public static readonly DeepPointer DP_ToggleableFan = new DeepPointer(BASE_KR_Update, 0x98, 0x0, 0x128, 0xA8, 0x0);
        public static readonly DeepPointer DP_SignSpawner = new DeepPointer(BASE_KR_Update, 0x98, 0x18, 0x128, 0xA8, 0x0);
        public static readonly DeepPointer DP_ShurikenTarget = new DeepPointer(BASE_KR_Update, 0x98, 0x0, 0x128, 0xA8, 0x0);
        public static readonly DeepPointer DP_JumpPad = new DeepPointer(BASE_KR_Update, 0x30, 0xA8, 0x0);


        ////// Abilities //////
        public static readonly DeepPointer DP_Tempest_refund = new DeepPointer(0x0438D7F0, 0x20, 0x118, 0xD38, 0x9c); //PlayerInputOML->BlueprintGeneratedClass->BP_PlayerCharacter_C->UpgradesValueDatabase->Tempest_refund

        ////// DEV //////
        public static readonly DeepPointer DP_SublevelsCount = new DeepPointer(BASE_KR_Update, 0xA0);
        public static readonly DeepPointer DP_ObjectsCount = new DeepPointer(BASE_KR_Update, 0x98, 0x0, 0x128, 0xB0);
        public static readonly DeepPointer DP_VFTablePattern = new DeepPointer(BASE_KR_Update, 0x98, 0x0, 0x128, 0xA8, 0x0, 0x0);
        public const int FNamePool = 0x044ADE80; //FNamePool -> PtrDB
        // ObjectsTypes
        public static Dictionary<string, DeepPointer> ObjectsTypes = new Dictionary<string, DeepPointer>() {
            {"triggerbase", new DeepPointer(0x3189000 +  0x80578)},
            {"curvefloat", new DeepPointer(0x3189000 +  0x6b26a8)},
            {"checkpoint", new DeepPointer(0x3189000 +  0x1ADBC8)}
        };


        //// CustomCP Pointers ////
        public static Dictionary<MapType, DeepPointer> MapCPPointers = new Dictionary<MapType, DeepPointer>() {
            { MapType.Awakening, new DeepPointer(0x0438BB18, 0x28, 0x28, 0x48)},
            { MapType.LookInside, new DeepPointer(0x0438BB18, 0x28, 0x28, 0x48)},
            { MapType.LookInsideCV, new DeepPointer(0x0438BB18, 0x28, 0x0, 0x48)},
            { MapType.Faster, new DeepPointer(0x0438BB18, 0x28, 0x28, 0x48)},
            { MapType.InHerOwnImage, new DeepPointer(0x0438BB18, 0x28, 0x28, 0x48)},
            { MapType.ReignInHell, new DeepPointer(0x0438BB18, 0x28, 0x28, 0x48)},
            { MapType.ReignInHellCV, new DeepPointer(0x0438BB18, 0x28, 0x0, 0x48)},
            { MapType.TheClimb, new DeepPointer(0x0438BB18, 0x28, 0xA8, 0x48)},
            { MapType.TheClimbCV, new DeepPointer(0x0438BB18, 0x28, 0x28, 0x48)},
            { MapType.BlinkCV, new DeepPointer(0x0438BB18, 0x28, 0x0, 0x48)},
            { MapType.TempestCV, new DeepPointer(0x0438BB18, 0x28, 0x0, 0x48)},
            { MapType.EchoesCV, new DeepPointer(0x0438BB18, 0x28, 0x0, 0x48)},
            { MapType.OverlordCV, new DeepPointer(0x0438BB18, 0x28, 0x0, 0x48)},
            { MapType.TheMonster, new DeepPointer(0x0438BB18, 0x28, 0x0, 0x48)},
            { MapType.JackedUp, new DeepPointer(0x0438BB18, 0x28, 0x48, 0x48)},
            { MapType.BreatheIn, new DeepPointer(0x0438BB18, 0x28, 0x80, 0x48)},
            { MapType.BreatheInCV, new DeepPointer(0x0438BB18, 0x28, 0x80, 0x48)},
            { MapType.RoadToAmida, new DeepPointer(0x0438BB18, 0x28, 0x50, 0x48)},
            { MapType.RunUp, new DeepPointer(0x0438BB18, 0x28, 0x60, 0x48)},
            { MapType.Gatekeeper, new DeepPointer(0x0438BB18, 0x28, 0x60, 0x48)},
            { MapType.DharmaCity, new DeepPointer(0x0438BB18, 0x28, 0x10, 0x48)},
            { MapType.TYWB, new DeepPointer(0x0438BB18, 0x28, 0x10, 0x48)},
            { MapType.Echoes, new DeepPointer(0x0438BB18, 0x28, 0x138, 0x48)},
            { MapType.ForbiddenZone, new DeepPointer(0x0438BB18, 0x28, 0x18, 0x48)},
            { MapType.TheSummit, new DeepPointer(0x0438BB18, 0x28, 0x8, 0x48)},
        };

        // EnemyTypes
        public static Dictionary<string, DeepPointer> EnemyTypes = new Dictionary<string, DeepPointer>() {
            {"pistol", new DeepPointer(0x3189000 +  0x17f240) },
            {"turret", new DeepPointer(0x3189000 + 0x189A00)},
            {"drone", new DeepPointer(0x3189000 + 0x17FC58)},
            {"ball", new DeepPointer(0x3189000 + 0x18A6E0)},
            {"splitter", new DeepPointer(0x3189000 + 0x18B208)},
            {"meleeShifter", new DeepPointer(0x3189000 + 0x18D238)},
            {"shooterShifter", new DeepPointer(0x3189000 + 0x18D238)},
            {"shield", new DeepPointer(0x3189000 + 0x18FF70)},
            {"uzi", new DeepPointer(0x3189000 + 0x194810)},
            {"gecko", new DeepPointer(0x3189000 + 0x195B48)},
            {"frogger", new DeepPointer(0x3189000 + 0x185198)},
            {"weeb", new DeepPointer(0x3189000 + 0x1879E0)},
            {"spider", new DeepPointer(0x3189000 + 0x193520)},
            {"sniper", new DeepPointer(0x3189000 + 0x191900)},
            {"hel", new DeepPointer(0x3189000 + 0x1705BB)},
            {"mara", new DeepPointer(0x3189000 + 0x174198)},
        };

    }
}
