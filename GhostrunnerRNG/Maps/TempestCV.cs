using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using System.Collections.Generic;

namespace GhostrunnerRNG.Maps {
    class TempestCV : MapCore{

        public TempestCV() : base(Game.GameUtils.MapType.TempestCV, manualGen: true) {
            if(GameHook.IsHC) return;

            Gen_PerRoom();
            CPRequired = false;
        }

        protected override void Gen_PerRoom() {
            Rooms = new List<RoomLayout>();
            RoomLayout layout;

            ///// SECTION 1 /////
            List<Enemy> orbs = new List<Enemy>();
            orbs.Add(new CVOrb(0x3D0, 0x830, 0xA8));
            orbs.Add(new CVOrb(0x3C8, 0x830, 0xC0));
            orbs.Add(new CVOrb(0x3B8, 0x830, 0xF0));
            orbs.Add(new CVOrb(0x3C0, 0x830, 0xD8));
            layout = new RoomLayout(orbs);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8544, 31522, 398), new Vector3f(-6934, 30261, 398)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6883, 27798, 398), new Vector3f(-8493, 26694, 398)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-10379, 26737, 1648), new Vector3f(-11499, 27674, 1648)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8092, 34847, 398), new Vector3f(-7551, 35312, 398)));
            // railing corners
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8808, 32014, 641)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8400, 34694, 685)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8410, 35506, 627)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-7201, 35500, 617)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-7196, 34715, 702)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8810, 29683, 676)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8795, 28098, 591)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6542, 28075, 722)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6557, 26365, 692)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8789, 26353, 696)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-10053, 26384, 1917)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11795, 26379, 1970)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-11790, 28088, 1925)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-10049, 28084, 1912)));
            // special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6776, 29796, 858))); // main platform, left corner rock
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6529, 32504, 315))); // main platform, right corner, over the railing
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-7673, 27619, 2628))); // left rooftop (using moving platform)
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8735, 28053, 1858))); // left rooftop corner
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-12949, 31738, 398))); // around the corner, entry door
            Rooms.Add(layout);


            ///// SECTION 2 /////
            orbs = new List<Enemy>();
            orbs.Add(new CVOrb(0x338, 0x830, 0x360));
            orbs.Add(new CVOrb(0x330, 0x830, 0x378));
            orbs.Add(new CVOrb(0x320, 0x830, 0x3A8));
            orbs.Add(new CVOrb(0x328, 0x830, 0x390));
            layout = new RoomLayout(orbs);

            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(10509, 23741, 398), new Vector3f(11257, 22069, 398)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8046, 13386, 398), new Vector3f(8811, 14803, 398)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(14307, 19884, 398), new Vector3f(11906, 19406, 398)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(10320, 19296, 398), new Vector3f(6429, 19931, 398)));

            // special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(10320, 19296, 398), new Vector3f(6429, 19931, 398))); // floating platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(8413, 18273, 428))); // right block
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(14999, 19233, 395))); // entry door, around the corner
            Rooms.Add(layout);

        }
    }
}
