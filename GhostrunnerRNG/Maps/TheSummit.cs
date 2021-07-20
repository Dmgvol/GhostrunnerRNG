using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.MapGen;
using GhostrunnerRNG.MemoryUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GhostrunnerRNG.Maps {
    class TheSummit : MapCore, IModes {

        #region HC Rooms
        private Room room_hc_1 = new Room(new Vector3f(-10729, -3809, 465), new Vector3f(-2107, -12186, 4230)); //
        private Room room_hc_2 = new Room(new Vector3f(-146, 3190, -1233), new Vector3f(8430, -10079, 5120)); //
        private Room room_hc_3 = new Room(new Vector3f(9783, -4882, 933), new Vector3f(18870, -14568, 5148)); //
        #endregion


        public TheSummit() : base(GameUtils.MapType.TheSummit) {
            ModifyCP(new DeepPointer(PtrDB.DP_Summit_ElevatorCP), new Vector3f(-15136, -11263, 1303), GameHook.game);
        }

        public void Gen_Easy() {
            
        }

        public void Gen_Hardcore() {
            List<Enemy> AllEnemies = GetAllEnemies(GameHook.game);
            Rooms = new List<RoomLayout>();
            RoomLayout layout;

            //// Room 1 ////
            var enemies = room_hc_1.ReturnEnemiesInRoom(AllEnemies);
            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[1].SetEnemyType(Enemy.EnemyTypes.Waver);
            enemies[2].SetEnemyType(Enemy.EnemyTypes.Waver);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, enemyIndex: 3); // frogger
            layout = new RoomLayout(enemies);
            // orb
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6768, -7316, 1952)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-5607, -7288, 1845)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-7108, -6459, 1845)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6823, -4901, 1563)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-5895, -5014, 1565)).Mask(SpawnPlane.Mask_ShieldOrb));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8377, -6113, 1714)).Mask(SpawnPlane.Mask_ShieldOrb));
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-4284, -5851, 1301), new Vector3f(-5621, -6300, 1302), new Angle(-1.00f, 0.05f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-6459, -6382, 1305), new Vector3f(-5936, -7750, 1302), new Angle(-0.85f, 0.53f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8312, -6684, 1302), new Vector3f(-7833, -7901, 1302), new Angle(-0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            // high
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-8403, -5657, 1498), new Vector3f(-5265, -5590, 1502), new Angle(-0.74f, 0.67f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-5666, -6600, 1502), new Angle(-0.92f, 0.38f)).Mask(SpawnPlane.Mask_Highground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(-7522, -6687, 1498), new Angle(-0.78f, 0.63f)).Mask(SpawnPlane.Mask_Highground));
            layout.DoNotReuse();
            Rooms.Add(layout);
            

            //// Room 2 ////
            enemies = room_hc_2.ReturnEnemiesInRoom(AllEnemies);
            List<Tuple<Vector3f, Angle>> Shifters_Room2 = new List<Tuple<Vector3f, Angle>>() {
                new Tuple<Vector3f, Angle>(new Vector3f(4864, -798, 2797), new Angle(-0.74f, 0.68f)), // lamp post
                new Tuple<Vector3f, Angle>(new Vector3f(4215, 905, 3376), new Angle(-0.66f, 0.75f)), // far big lab tube structure
                new Tuple<Vector3f, Angle>(new Vector3f(5616, -2995, 3472), new Angle(-0.87f, 0.50f)), // billboard
                new Tuple<Vector3f, Angle>(new Vector3f(3880, -5415, 3478), new Angle(0.45f, 0.89f)), // billboard 2
                new Tuple<Vector3f, Angle>(new Vector3f(3905, -8080, 2745), new Angle(0.52f, 0.86f)), // boxes near exit
                new Tuple<Vector3f, Angle>(new Vector3f(6315, -8011, 2818), new Angle(0.94f, 0.35f)), // lab tube near exit
                new Tuple<Vector3f, Angle>(new Vector3f(6222, -4975, 1497), new Angle(0.95f, 0.33f)), // computers, left
                new Tuple<Vector3f, Angle>(new Vector3f(5435, -1169, 1367), new Angle(-0.80f, 0.60f)), // near red window
                new Tuple<Vector3f, Angle>(new Vector3f(4508, -606, 1608), new Angle(-0.79f, 0.61f)), // lab tube under stairs
                new Tuple<Vector3f, Angle>(new Vector3f(3047, 153, 2238), new Angle(-0.48f, 0.88f)), // big ad monitor
                new Tuple<Vector3f, Angle>(new Vector3f(4284, -6800, 2502), new Angle(0.68f, 0.73f)) // last platform ledge
            }; 

            enemies[0] = new EnemyDrone(enemies[0]);
            enemies[1] = new EnemyDrone(enemies[1]);
            enemies[2] = new EnemyShifter(enemies[2]).AddFixedSpawnInfoList(ref Shifters_Room2);
            enemies[3] = new EnemyShifter(enemies[3]).AddFixedSpawnInfoList(ref Shifters_Room2);
            enemies[4].SetEnemyType(Enemy.EnemyTypes.Waver);
            RandomPickEnemiesWithoutCP(ref enemies, enemyIndex: 4); // waver
            RandomPickEnemiesWithoutCP(ref enemies, enemyIndex: 1, force:true); // drone
            layout = new RoomLayout(enemies);
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(3982, -5237, 1302), new Vector3f(5344, -1710, 1305), new Angle(-0.72f, 0.69f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(5));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(3411, -92, 1902), new Angle(-0.60f, 0.80f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(4931, 433, 2502), new Vector3f(5637, -498, 2502), new Angle(-0.92f, 0.40f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(6112, -6931, 2502), new Vector3f(5091, -7638, 2502), new Angle(1.00f, 0.03f)).Mask(SpawnPlane.Mask_Flatground));
            // high/special
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(3126, -6791, 2740), new Angle(0.38f, 0.92f)).Mask(SpawnPlane.Mask_Highground)); // boxes above entry
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(5765, -6146, 1302), new Angle(0.78f, 0.62f)).Mask(SpawnPlane.Mask_Highground)); // around left lab tubes
            // reuse shifter points
            for(int i = 0; i < Shifters_Room2.Count; i++) 
                layout.AddSpawnPlane(new SpawnPlane(Shifters_Room2[i].Item1, Shifters_Room2[i].Item2).Mask(SpawnPlane.Mask_Highground));
            // drones
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(5077, -7649, 2953), new Angle(0.84f, 0.55f)).Mask(SpawnPlane.Mask_Airborne)); // above exit
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(3497, 399, 2953), new Angle(-0.59f, 0.80f)).Mask(SpawnPlane.Mask_Airborne)); // above stairs, right corner
            Rooms.Add(layout);


            //// Room 3 ////
            enemies = room_hc_3.ReturnEnemiesInRoom(AllEnemies);

            List<Tuple<Vector3f, Angle>> Shifters_Room3 = new List<Tuple<Vector3f, Angle>>() {
                new Tuple<Vector3f, Angle>(new Vector3f(17071, -11951, 2502), new Angle(0.88f, 0.48f)), // right hallway corner
                new Tuple<Vector3f, Angle>(new Vector3f(17159, -7749, 2502), new Angle(0.99f, 0.14f)), // end of 1st platform
                new Tuple<Vector3f, Angle>(new Vector3f(15093, -7805, 2496), new Angle(1.00f, 0.04f)), // platform ledge
                new Tuple<Vector3f, Angle>(new Vector3f(13055, -6635, 3390), new Angle(-0.98f, 0.18f)), // right billboard
                new Tuple<Vector3f, Angle>(new Vector3f(14355, -6668, 3390), new Angle(-0.97f, 0.26f)), // right billboard 2
                new Tuple<Vector3f, Angle>(new Vector3f(10977, -12438, 2740), new Angle(0.52f, 0.86f)), // boxes near end
                new Tuple<Vector3f, Angle>(new Vector3f(14630, -12098, 2740), new Angle(0.68f, 0.74f)), // boxes middle
                new Tuple<Vector3f, Angle>(new Vector3f(15924, -12793, 3429), new Angle(0.76f, 0.65f)), // high billboard/ad  
                new Tuple<Vector3f, Angle>(new Vector3f(10489, -11790, 2578), new Angle(0.01f, 1.00f)), // hallway corner near exit
            };

            enemies[0] = new EnemyShieldOrb(enemies[0]);
            enemies[1] = new EnemyDrone(enemies[1]);
            enemies[2] = new EnemyShifter(enemies[2]).AddFixedSpawnInfoList(ref Shifters_Room3);
            enemies[3] = new EnemyShifter(enemies[3]).AddFixedSpawnInfoList(ref Shifters_Room3);
            enemies[4].SetEnemyType(Enemy.EnemyTypes.Waver);
            RandomPickEnemiesWithoutCP(ref enemies, force: true, enemyIndex: 1); // drone
            // orb
            layout = new RoomLayout(enemies[0]);
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(16280, -12539, 2691)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(16310, -10714, 2959)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(15039, -9824, 2582)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(13861, -6837, 2815)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(11599, -10551, 2785)));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(15652, -12369, 2586), new Vector3f(12159, -12671, 2846)));
            Rooms.Add(layout);
            // enemies
            layout = new RoomLayout(enemies.Skip(1).ToList());
            // default
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(12147, -11543, 2504), new Vector3f(14354, -12122, 2504), new Angle(0.69f, 0.73f))
                .Mask(SpawnPlane.Mask_Flatground).SetMaxEnemies(2));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(15686, -11658, 2504), new Vector3f(17032, -12005, 2502), new Angle(0.76f, 0.66f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(16578, -7197, 2497), new Vector3f(15434, -7737, 2504), new Angle(1.00f, 0.00f)).Mask(SpawnPlane.Mask_Flatground));
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(11363, -14395, 2504), new Vector3f(10988, -13133, 2504), new Angle(0.70f, 0.71f)).Mask(SpawnPlane.Mask_Flatground));
            // high - reuse shifter points
            for(int i = 0; i < Shifters_Room3.Count; i++)
                layout.AddSpawnPlane(new SpawnPlane(Shifters_Room3[i].Item1, Shifters_Room3[i].Item2).Mask(SpawnPlane.Mask_Highground));
            // drones
            layout.AddSpawnPlane(new SpawnPlane(new Vector3f(11527, -12313, 3111), new Angle(0.47f, 0.88f)).Mask(SpawnPlane.Mask_Airborne));
            Rooms.Add(layout);

            //// EXTRA ////
            //layout = new RoomLayout();
            // above mara
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(18352, -29019, 3338), new Vector3f(18233, -27237, 3338), new Angle(0.82f, 0.58f)).Mask(SpawnPlane.Mask_Airborne).AsVerticalPlane());
            //layout.AddSpawnPlane(new SpawnPlane(new Vector3f(16222, -29200, 3134), new Vector3f(16132, -27307, 3134), new Angle(0.54f, 0.84f)).Mask(SpawnPlane.Mask_Airborne).AsVerticalPlane());
            //Rooms.Add(layout);

        }

        public void Gen_Nightmare() {
           
        }

        public void Gen_Normal() {
            
        }

        protected override void Gen_PerRoom() {}
    }
}
