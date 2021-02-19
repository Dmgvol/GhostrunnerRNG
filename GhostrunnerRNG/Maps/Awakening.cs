using GhostrunnerRNG.Game;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.Maps {
    public class Awakening : MapCore {
        

        public Awakening() : base(MapType.Awakening) {

            // add enemies
            for(int i = 0; i < 13; i++) {
                Enemy pistol = new Enemy(new DeepPointer(0x045A3C20, 0x138, 0xB0, 0xB0, (0x20 * (i + 1)), 0x4F0));
                Enemies.Add(pistol);
            }

            // last room, left bot corner
            Enemies[0].AddPosPlane(new SpawnPlane(new Vector3f(4603, -70321, -346), new Vector3f(3303, -68684, -350), new Angle(-0.71f, 0.71f)));
            Enemies[0].AddPosPlane(new SpawnPlane(new Vector3f(5128, -71555, 307), new Vector3f(3678, -71883, 307), new Angle(-0.71f, 0.71f)));
            Enemies[0].AddPosPlane(new SpawnPlane(new Vector3f(3256, -68387, 237), new Angle(-0.61f, 0.79f)));
            Enemies[0].AddPosPlane(new SpawnPlane(new Vector3f(3373, -72039, 924), new Angle(-0.64f, 0.77f)));

            // last room, middle(left)
            Enemies[1].AddPosPlane(new SpawnPlane(new Vector3f(997, -70468, -41), new Vector3f(1689, -68394, -41)).RandomAngle());

            // last room, middle (right)
            Enemies[2].AddPosPlane(new SpawnPlane(new Vector3f(910, -72873, -160), new Vector3f(1698, -72192, -167), new Angle(-0.45f, 0.89f)));
            Enemies[2].AddPosPlane(new SpawnPlane(new Vector3f(955, -76167, 362), new Vector3f(1578, -75331, 371), new Angle(-0.39f, 0.92f)));
            Enemies[2].AddPosPlane(new SpawnPlane(new Vector3f(1943, -75088, 1008), new Angle(-0.26f, 0.97f)));

            // last room, right corner
            Enemies[3].AddPosPlane(new SpawnPlane(new Vector3f(-2142, -75881, -42), new Vector3f(-1468, -73341, -42), new Angle(0.12f, 0.99f)));
            Enemies[3].AddPosPlane(new SpawnPlane(new Vector3f(-1100, -74848, 340)).RandomAngle());
            Enemies[3].AddPosPlane(new SpawnPlane(new Vector3f(-2033, -72982, 605), new Angle(-0.18f, 0.98f)));

            // last room, left corner(near elevator)
            Enemies[4].AddPosPlane(new SpawnPlane(new Vector3f(-280, -68970, -41), new Vector3f(-1275, -68147, -41), new Angle(-0.34f,0.94f)));
            Enemies[4].AddPosPlane(new SpawnPlane(new Vector3f(-2226, -69843, -81), new Vector3f(-1017, -70568, -81), new Angle(-0.24f, 0.97f)));
            Enemies[4].AddPosPlane(new SpawnPlane(new Vector3f(-1339, -71031, 605), new Angle(-0.37f, 0.93f)));

            // sensory boost
            Enemies[5].AddPosPlane(new SpawnPlane(new Vector3f(-14496, -59107, 2803), new Vector3f(-15633, -60168, 2799), new Angle(-1.00f, 0.09f)));
            Enemies[5].AddPosPlane(new SpawnPlane(new Vector3f(-15826, -58998, 2889), new Angle(-0.99f, 0.17f)));

            // first enemy, second room
            Enemies[6].AddPosPlane(new SpawnPlane(new Vector3f(-14200, -63865, 2798), new Vector3f(-14996, -62722, 2798)).RandomAngle()); // default plane
            Enemies[6].AddPosPlane(new SpawnPlane(new Vector3f(-14729, -63919, 3591), new Angle(0.03f, 1.0f))); // lamp
            Enemies[6].AddPosPlane(new SpawnPlane(new Vector3f(-18060, -64136, 3598), new Vector3f(-19392, -63758, 3601), new Angle(0f, 1f))); // vent area
            Enemies[6].AddPosPlane(new SpawnPlane(new Vector3f(-21681, -63576, 2998), new Angle(-1.00f, 0.05f))); // before fan (corner)
            Enemies[6].AddPosPlane(new SpawnPlane(new Vector3f(-20723, -59412, 4298), new Vector3f(-21563, -60163, 4302), new Angle(-0.99f, 0.15f))); // before boost
            Enemies[6].AddPosPlane(new SpawnPlane(new Vector3f(-4482, -70321, -80), new Angle(-0.01f, 1.00f))); // elevator

            // 3  pistols hallway - first enemy
            Enemies[7].AddPosPlane(new SpawnPlane(new Vector3f(5740, -63956, 1468), new Vector3f(6337, -64859, 1468), new Angle(0.72f, 0.7f)));
            Enemies[7].AddPosPlane(new SpawnPlane(new Vector3f(5699, -61602, 2065), new Angle(0.83f, 0.56f)));

            // 3  pistols hallway - second enemy
            Enemies[8].AddPosPlane(new SpawnPlane(new Vector3f(5771, -66227, 1678), new Vector3f(6538, -67229, 1678), new Angle(0.73f, 0.68f)));
            Enemies[8].AddPosPlane(new SpawnPlane(new Vector3f(5656, -68518, 1868), new Vector3f(6591, -69532, 1868), new Angle(0.73f, 0.68f)));

            // 3  pistols hallway - third enemy
            Enemies[9].AddPosPlane(new SpawnPlane(new Vector3f(5656, -68518, 1868), new Vector3f(6591, -69532, 1868), new Angle(0.73f, 0.68f)));
            Enemies[9].AddPosPlane(new SpawnPlane(new Vector3f(6648, -70563, 1798), new Vector3f(5848, -71930, 1798), new Angle(0.69f, 0.73f)));

            // broken bridge part, right far enemy
            Enemies[10].AddPosPlane(new SpawnPlane(new Vector3f(-8179, -54300 , 3408), new Vector3f(-9091, -52688, 3402), new Angle(-0.9f, 0.43f)));
            Enemies[10].AddPosPlane(new SpawnPlane(new Vector3f(-8122, -54648, 3911), new Angle(-0.99f, 0.17f)));

            // broken bridge part, left far enemy
            Enemies[11].AddPosPlane(new SpawnPlane(new Vector3f(-8309, -55792, 3408), new Vector3f(-10221, -56789, 3408), new Angle(1.0f, 0.01f)));
            Enemies[11].AddPosPlane(new SpawnPlane(new Vector3f(-10040, -55579, 4030), new Angle(-0.99f, 0.15f)));
            Enemies[11].AddPosPlane(new SpawnPlane(new Vector3f(-7336, -57117, 3408), new Angle(1.00f, 0.08f)));

            // broken bridge part, right near enemy
            Enemies[12].AddPosPlane(new SpawnPlane(new Vector3f(-9798, -53058, 3402), new Vector3f(-10775, -54270, 3408), new Angle(-0.96f, 0.28f)));
            Enemies[12].AddPosPlane(new SpawnPlane(new Vector3f(-12762, -55235, 4398), new Angle(-0.98f, 0.22f)));
        }
    }
}
