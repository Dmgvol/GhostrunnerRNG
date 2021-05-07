using GhostrunnerRNG.Game;
using System.Collections.Generic;
using System.Diagnostics;

namespace GhostrunnerRNG.MapGen {
    public class DynamicRoomLayout : IRandomize {

        // List of roomLayouts, each one contains its own enemies, spawnplanes and checkpoints
        public List<List<RoomLayout>> Rooms = new List<List<RoomLayout>>();

        public DynamicRoomLayout() {}

        // single
        public void AddRoom(RoomLayout room) {
            Rooms.Add(new List<RoomLayout>() { room });
        }

        public void AddRoom(List<RoomLayout> newRooms) {
            Rooms.Add(newRooms);
        }

        public void RandomizeEnemies(Process game) {
            if(Rooms.Count == 0) return;
            int r = Config.GetInstance().r.Next(Rooms.Count);
            Rooms[r].ForEach(x=>x.RandomizeEnemies(game));
            Rooms[r].ForEach(x => x.FixOrbBeams(game));
        }
    }
}
