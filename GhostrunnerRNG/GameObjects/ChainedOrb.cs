using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using System.Collections.Generic;
using System.Linq;

namespace GhostrunnerRNG.GameObjects {
    public class ChainedOrb {
        public EnemyShieldOrb orb { get; private set; }
        public List<Enemy> attachedEnemies { get; private set; }

        public ChainedOrb(EnemyShieldOrb orb, params Enemy[] attachedEnemies) {
            this.orb = orb;
            this.attachedEnemies = attachedEnemies.ToList();
            this.attachedEnemies.ForEach(x => x.DisableAttachedCP(GameHook.game));
        }

        public static void Randomize(ref List<ChainedOrb> chainedOrbs, ref List<RoomLayout> chainedOrbs_Rooms) {
            //// chainedOrbs ////
            if(chainedOrbs_Rooms?.Count == 0 || chainedOrbs?.Count == 0) return;
            // create available indexes, for random room pick
            List<int> availableIndexes = new List<int>();
            for(int i = 0; i < chainedOrbs_Rooms.Count; i++) {
                chainedOrbs_Rooms[i].ClearRoomObjects();
                availableIndexes.Add(i);
            }

            // chainedOb loop
            for(int i = 0; i < chainedOrbs.Count; i++) {
                // pick random room
                if(availableIndexes.Count == 0) break;
                int randomRoom = Config.GetInstance().r.Next(availableIndexes.Count);
                // swap enemies to random room
                List<Enemy> roomEnemies = new List<Enemy>();
                roomEnemies.Add(chainedOrbs[i].orb);
                roomEnemies.AddRange(chainedOrbs[i].attachedEnemies);
                chainedOrbs_Rooms[availableIndexes[randomRoom]].SwapEnemies(roomEnemies.ToArray());
                availableIndexes.RemoveAt(randomRoom);
            }
            // rng chained rooms
            chainedOrbs_Rooms.ForEach(x => x.RandomizeEnemies(GameHook.game));
        }
    }
}
