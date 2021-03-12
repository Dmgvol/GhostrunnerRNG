using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using System.Collections.Generic;
using static GhostrunnerRNG.Enemies.Enemy;

namespace GhostrunnerRNG.Maps {
    class Faster :MapCore {

        #region Rooms
        // City
        private Room room_1 = new Room(new Vector3f(20269, -156684, 464), new Vector3f(4145, -176088, -3302)); // 3 drones
        private Room room_2 = new Room(new Vector3f(136, -168617, -4263), new Vector3f(-7818, -160200, 770)); // 1 weeb, 1 drone, 2 uzi

        // Train
        private Room room_3 = new Room(new Vector3f(-11027, 143531, 1833), new Vector3f(-12681, 149757, -538)); // 2 weebs, 1 pistol
        private Room room_4 = new Room(new Vector3f(-10979, 172915, 1868), new Vector3f(-12791, 178916, -171)); // 1 weeb, 1 waver
        private Room room_5 = new Room(new Vector3f(-10888, 193792, 2282), new Vector3f(-12856, 204304, -232)); // 3 weebs, 1 frogger
        #endregion


        public Faster(bool isHC) : base(GameUtils.MapType.Faster) {
            if(!isHC) {
                Gen_PerRoom();
            } else {
                // hc
            }
        }

        protected override void Gen_PerRoom() {
            //indexes ?
            List<Enemy> AllEnemies = GetAllEnemies(MainWindow.game, 0, 26);
            AllEnemies.AddRange(GetAllEnemies(MainWindow.game, 27, 14));

            Rooms = new List<RoomLayout>();
            RoomLayout layout;
            List<Enemy> enemies;



            

        }
    }
}
