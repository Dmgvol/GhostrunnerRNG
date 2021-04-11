using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using System.Collections.Generic;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.Maps {
    class LookInside : MapCore{

        #region Rooms
        private Room room1 = new Room(new Vector3f(159175, -52611, 1562), new Vector3f(152993, -66967, 5886)); // 4 pistols, floating platforms 
        private Room room2 = new Room(new Vector3f(165231, -42333, 8077), new Vector3f(148296, -48473, 2321)); // 5 pistols ,last room
        #endregion

        public LookInside() : base(MapType.LookInside) {
            if(GameHook.IsHC) return;
            Gen_PerRoom();
        }

        protected override void Gen_PerRoom() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game, 0, 9); // static range, no gap
            Rooms = new List<RoomLayout>();
            RoomLayout layout;

            //// Room 1 - 4 pistols, floating platforms
            layout = new RoomLayout(room1.ReturnEnemiesInRoom(AllEnemies));
            // to avoid wrong pos spawns, I use vertical planes in the middle of the  platforms
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153148, -59471, 2832), new Vector3f(154124, -58691, 2832), new Angle(-0.75f, 0.66f)).AsVerticalPlane());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(155664, -57651, 2833), new Vector3f(156740, -56869, 2833), new Angle(-0.53f, 0.85f)).AsVerticalPlane());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(158191, -59078, 2832), new Vector3f(157983, -60312, 2832), new Angle(-0.87f, 0.50f)).AsVerticalPlane());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(155531, -61958, 2832), new Vector3f(154254, -61625, 2832), new Angle(0.39f, 0.92f)).AsVerticalPlane());
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(152882, -55794, 3598), new Vector3f(155365, -54986, 3598), new Angle(-0.68f, 0.74f))); // collectible platform
            // special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153746, -62014, 4234), new Angle(0.76f, 0.65f))); // billboard near
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(158799, -58624, 4248), new Angle(0.96f, 0.28f))); // billboard far
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153237, -56789, 4301), new Angle(-0.53f, 0.85f))); // collectible wall
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153961, -52953, 3803), new Angle(-0.70f, 0.71f))); // slide ledge, to next room
            Rooms.Add(layout);

            //// Room 2 - 5 pistols, last room
            layout = new RoomLayout(room2.ReturnEnemiesInRoom(AllEnemies));
            // main platforms
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(158225, -43241, 3198), new Vector3f(156081, -42827, 3198), new Angle(-0.89f, 0.46f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(156846, -40737, 3198), new Vector3f(157130, -41736, 3198), new Angle(-0.94f, 0.35f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(158792, -41972, 3198), new Vector3f(158922, -43343, 3198), new Angle(-0.96f, 0.27f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(152012, -48136, 3198), new Vector3f(152209, -47198, 3198), new Angle(-0.59f, 0.81f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153685, -47214, 3198), new Vector3f(155030, -47501, 3198), new Angle(-0.89f, 0.46f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(156721, -46427, 3198), new Vector3f(156351, -45285, 3198), new Angle(-0.89f, 0.46f)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(154520, -43688, 3198), new Vector3f(154647, -45111, 3198), new Angle(-0.87f, 0.49f)));
            // special/high spots
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(151930, -47267, 3948), new Angle(-0.52f, 0.85f))); // billboard
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153667, -50388, 3298), new Angle(-0.64f, 0.77f))); // slide edge
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(155839, -42364, 4052), new Angle(-0.53f, 0.85f))); // generator/server
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(158129, -44004, 4052), new Angle(-1.00f, 0.01f))); // generator/server 2 
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(155591, -45151, 3618), new Angle(0.47f, 0.89f)));  // middle wall platform
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153328, -47607, 3638), new Angle(-0.88f, 0.47f))); // wide generator 1
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(153988, -48037, 3640), new Angle(-0.96f, 0.27f))); // wide generator 2

            Rooms.Add(layout);
        }
    }
}
