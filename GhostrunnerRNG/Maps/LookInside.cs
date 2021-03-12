using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using System.Collections.Generic;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.Maps {
    class LookInside : MapCore{

        public LookInside(bool isHC) : base(MapType.LookInside) {
            if(!isHC) {
                for(int i = 0; i < 9; i++) {
                    Enemy enemy = new Enemy(new DeepPointer(0x045A3C20, 0x138, 0xB0, 0xB0, (0x20 * (i + 1)), 0x4F0));
                    Enemies.Add(enemy);
                }
                Gen_PerRoom();
            } else {
                // hardcore
                //TODO: remove temporary block/msg and add hc enemies and gen
            }
        }

        protected override void Gen_PerRoom() {
            Rooms = new List<RoomLayout>();

            ////// Last room //////
            RoomLayout layout = new RoomLayout(Enemies[0], Enemies[1], Enemies[2], Enemies[3], Enemies[8]);

            // last platform,
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(158225, -43241, 3198), new Vector3f(156081, -42827, 3198), new Angle(-0.89f, 0.46f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(156846, -40737, 3198), new Vector3f(157130, -41736, 3198), new Angle(-0.94f, 0.35f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(158792, -41972, 3198), new Vector3f(158922, -43343, 3198), new Angle(-0.96f, 0.27f)));


            // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(151930, -47267, 3948), new Angle(-0.52f, 0.85f)));

            // slide edge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153667, -50388, 3298), new Angle(-0.64f, 0.77f)));

            // side generators
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(155839, -42364, 4052), new Angle(-0.53f, 0.85f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(158129, -44004, 4052), new Angle(-1.00f, 0.01f)));

            // middle wall platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(155591, -45151, 3618), new Angle(0.47f, 0.89f)));

            // first wide generator
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153328, -47607, 3638), new Angle(-0.88f, 0.47f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153988, -48037, 3640), new Angle(-0.96f, 0.27f)));

            // main platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(152012, -48136, 3198), new Vector3f(152209, -47198, 3198), new Angle(-0.59f, 0.81f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153685, -47214, 3198), new Vector3f(155030, -47501, 3198), new Angle(-0.89f, 0.46f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(156721, -46427, 3198), new Vector3f(156351, -45285, 3198), new Angle(-0.89f, 0.46f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(154520, -43688, 3198), new Vector3f(154647, -45111, 3198), new Angle(-0.87f, 0.49f)));

            Rooms.Add(layout);

            ////// 4 platforms //////
            layout = new RoomLayout(Enemies[4], Enemies[5], Enemies[6], Enemies[7]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(152687, -56046, 3598), new Vector3f(155578, -54924, 3598)).RandomAngle().SetMaxEnemies(2)); // collectible platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153858, -52961, 3803), new Angle(-0.82f, 0.57f))); // start of slide to last room
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153274, -56737, 4301), new Angle(-0.82f, 0.58f))); // on wall 1
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(155214, -56562, 4301), new Angle(-0.75f, 0.66f))); // on wall 2

            // main platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(156600, -56551, 2833), new Vector3f(155800, -58005, 2833), new Angle(-0.74f, 0.67f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(157807, -58805, 2832), new Vector3f(158380, -60445, 2832), new Angle(-0.92f, 0.39f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(155776, -61614, 2832), new Vector3f(154050, -61968, 2832), new Angle(0.68f, 0.73f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(152743, -59166, 2832), new Vector3f(154546, -58908, 2832), new Angle(-0.96f, 0.29f)));

            // billboards
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(154456, -62203, 4234), new Angle(0.77f, 0.64f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(158635, -59604, 4248), new Angle(-1.00f, 0.08f)));

            Rooms.Add(layout);
        }
    }
}
