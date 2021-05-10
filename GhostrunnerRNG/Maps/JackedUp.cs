using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using System.Collections.Generic;
using System.Linq;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.Maps {
    class JackedUp : MapCore, IModes {

        private Room room_1 = new Room(new Vector3f(-9297, -23929, 546), new Vector3f(-5180, -17091, 2281));
        private Room room_2 = new Room(new Vector3f(-30424, -22624, 155), new Vector3f(-21734, -16856, 3089));
        private Room room_3 = new Room(new Vector3f(-40820, -11223, 5152), new Vector3f(-32991, -4121, 10285));
        private Room room_4 = new Room(new Vector3f(-68632, -26742, 8423), new Vector3f(-63143, -19223, 11190)); 
        private Room room_5 = new Room(new Vector3f(-61959, -27794, 9791), new Vector3f(-60096, -25898, 10758)); // lonely enemy in last fight

           
        public JackedUp() : base(MapType.JackedUp) {
            ModifyCP(new DeepPointer(0x04609420, 0x98, 0x8, 0x128, 0xA8, 0x30, 0x248, 0x1D0), new Vector3f(-6140, -25230, 1645), GameHook.game);
        }

        public void Gen_Normal() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game);
            Rooms = new List<RoomLayout>();
            RoomLayout layout;

            List<Enemy> enemies = room_1.ReturnEnemiesInRoom(AllEnemies);
            enemies.AddRange(room_5.ReturnEnemiesInRoom(AllEnemies));
            RandomPickEnemiesWithoutCP(ref enemies, force: true, removeCP: false);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, removeCP: false);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, removeCP: false);

            //// room 2 layout ////
            layout = new RoomLayout(room_2.ReturnEnemiesInRoom(AllEnemies)); //pistols
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-21196, -18213, 2188), new Angle(0.32f, 0.95f)));//box near cp
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-24745, -17806, 2078), new Angle(-0.18f, 0.98f)));//box on the left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-25774, -20884, 3251), new Angle(0.25f, 0.97f)).setDiff(1));//on right grapple line
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-25041, -19397, 2438), new Angle(0.07f, 1.00f)));//box near middle guy
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-24383, -17169, 1583), new Vector3f(-23624, -17782, 1598), new Angle(-0.24f, 0.97f)));//guy on the left spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-24817, -20152, 1958), new Vector3f(-26861, -19575, 1958), new Angle(-0.01f, 1.00f)));//middle guy spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-27271, -21702, 2508), new Vector3f(-26087, -22419, 2523), new Angle(0.37f, 0.93f))); //guy on the right spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-28177, -17552, 2254), new Vector3f(-27686, -18289, 2205), new Angle(-0.02f, 1.00f))); //far guy spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-28915, -20519, 2568), new Vector3f(-27719, -19575, 2568), new Angle(-0.02f, 1.00f)));//near button
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-26610, -20559, 3383), new Angle(0.16f, 0.99f)).setDiff(1));//on crane near button
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-23195, -20494, 2638), new Vector3f(-24397, -20477, 2638), new Angle(0.25f, 0.97f)).SetMaxEnemies(2).setDiff(1));//billboard on the right
            Rooms.Add(layout);

            //// room 3 layout ////
            layout = new RoomLayout(room_3.ReturnEnemiesInRoom(AllEnemies)); // 5 pistols
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-33411, -10404, 6202), new Vector3f(-35089, -9769, 6202), new Angle(0.68f, 0.73f)));//closer right platformspawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-37399, -10206, 6198), new Vector3f(-40051, -9827, 6203), new Angle(0.23f, 0.97f))); //far right platform spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-38782, -8209, 6203), new Vector3f(-40321, -7547, 6203), new Angle(0.01f, 1.00f))); //far middle platform spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-33776, -8237, 6194), new Vector3f(-36841, -7593, 6194), new Angle(0.0f, 1.00f)));//closer middle platform spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-36922, -5423, 6198), new Vector3f(-34115, -5995, 6202), new Angle(-0.68f, 0.74f)));//closer left platformspawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-40545, -5525, 6195), new Vector3f(-38839, -6006, 6202), new Angle(-0.03f, 1.00f))); //far left platform spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-37883, -7182, 6881), new Angle(-0.09f, 1.00f)));//billboard in the middle
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-36108, -10755, 6917), new Angle(0.38f, 0.93f)));//billboard on the right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-37630, -5086, 6880), new Angle(-0.26f, 0.96f)));//billboard on the left
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-35764, -9000, 7410), new Vector3f(-38934, -9000, 7410), new Angle(0.09f, 1.00f)).SetMaxEnemies(2).setDiff(1));//grapples line on the right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-35077, -6800, 7410), new Vector3f(-36823, -6800, 7410), new Angle(-0.11f, 0.99f)).setDiff(1));//grapples line on the left
            Rooms.Add(layout);

            //// room 4 layout ////
            enemies = room_4.ReturnEnemiesInRoom(AllEnemies);
            EnemyShieldOrb shieldOrb1 = new EnemyShieldOrb(enemies[0]);
            shieldOrb1.HideBeam_Range(0, 2);
            shieldOrb1.HideBeam_Range(1, 2);
            shieldOrb1.LinkObject(2);

            layout = new RoomLayout(shieldOrb1); //orb
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-65251, -20095, 10823), new Vector3f(-66206, -19589, 11027)));//near most left enemy
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-62300, -26600, 10131), new Vector3f(-62300, -25600, 10564)).setDiff(1));//back side of the billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-63103, -27335, 10716)).setDiff(1));//top of the second billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-65438, -25916, 12028)));//uptop moving platform on the right
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67475, -23533, 9396), new Vector3f(-67475, -22526, 9750)));//billboard near exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-68975, -23056, 9508))); // near door, on moving platform

            Rooms.Add(layout);

            layout = new RoomLayout(enemies.Skip(1).ToList()); //4 pistols + 1 uzi
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67825, -21555, 11403), new Vector3f(-66226, -21555, 11403), new Angle(-0.08f, 1.00f)).setDiff(1));//uptop left grapple point
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67865, -24555, 11413), new Vector3f(-66417, -24555, 11413), new Angle(0.11f, 0.99f)).setDiff(1));//uptop right grapple point
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-62400, -23965, 10563), new Vector3f(-62400, -21730, 10563), new Angle(0.00f, 1.00f)).SetMaxEnemies(2).setDiff(1));//grapple line 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-60731, -24760, 10355), new Angle(0.72f, 0.70f)).setDiff(1)); //billboard on the way to that lost pistol
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-64572, -23000, 9968), new Vector3f(-63646, -23000, 9968), new Angle(0.02f, 1.00f)));//left cargo
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-64530, -24300, 9968), new Vector3f(-63576, -24300, 9968), new Angle(0.29f, 0.96f)));//right cargo

            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-68054, -19559, 10152), new Vector3f(-67168, -20614, 10152)).RandomAngle());//upper left  enemy spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67327, -21076, 9465), new Vector3f(-66308, -22052, 9472), new Angle(-0.37f, 0.93f)));//bottom left enemy spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-68105, -25048, 9452), new Vector3f(-66263, -24006, 9452), new Angle(0.00f, 1.00f)));//bottom right enemy spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-67064, -26495, 10153), new Vector3f(-66309, -25499, 10154), new Angle(0.20f, 0.98f)));//upper right  enemy spawn plane
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-64454, -26275, 10162), new Vector3f(-63856, -25658, 10163), new Angle(0.46f, 0.89f)));//closer right enemy spawn plane
            Rooms.Add(layout);

            //// EXTRA //// 
            layout = new RoomLayout();
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-17322, -17173, 888), new Angle(0.04f, 1.00f))); // Room 1
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-27473, -10452, 5892), new Angle(-0.99f, 0.12f))); // Between Room 2 & 3
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-30844, -7872, 6513), new Angle(0.02f, 1.00f))); // Between Room 2 & 3
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-46950, -8139, 7306), new Angle(0.02f, 1.00f))); // before button room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-46447, -19445, 8274), new Angle(0.45f, 0.89f))); // lasers room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-54132, -19711, 9600), new Angle(0.15f, 0.99f))); // after lasers room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-58270, -21755, 9548), new Angle(0.59f, 0.81f))); // hallway before last fight room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-75493, -18342, 11185), new Angle(-0.23f, 0.97f))); // before elevator
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-78881, -14708, 13053), new Angle(-0.06f, 1.00f))); // elevator hallway
            Rooms.Add(layout);
        }

        public void Gen_Easy() {
            Gen_Normal();
        }

        public void Gen_Hardcore() {
            throw new System.NotImplementedException();
        }

        public void Gen_Nightmare() {
            throw new System.NotImplementedException();
        }

        protected override void Gen_PerRoom() { }
    }
}

