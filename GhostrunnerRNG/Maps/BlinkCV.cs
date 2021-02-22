using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using System;
using System.Collections.Generic;

namespace GhostrunnerRNG.Maps {
    class BlinkCV : MapCore{


        public BlinkCV() : base(GameUtils.MapType.BlinkCV) {
            Gen_PerRoom();
        }

        protected override void Gen_PerRoom() {
            // Coming Soon
            List<Enemy> AllEnemies = GetAllEnemies(MainWindow.game);
            PrintEnemyPos(AllEnemies);

            //Rooms = new List<RoomLayout>();
            //RoomLayout layout;
        }
    }
}
