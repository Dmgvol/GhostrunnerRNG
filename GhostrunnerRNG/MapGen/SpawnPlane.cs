using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using System;
using System.Collections.Generic;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.MapGen {
    public class SpawnPlane {

        public const double MIN_SPAWN_SPACING = 400;
        public const int ADDITIONAL_SPAWNS = 5;

        // 2 sides of the plane
        public Vector3f cornerA { get; private set; }
        public Vector3f cornerB { get; private set; }

        // angle
        public bool FixedAngle = true;
        public Angle angle { get; private set; } = null;

        // max/curr enemies per plane(for RoomLayout)
        public int MaxEnemies { get; private set; } = 1;
        public int CurrEnemeies { get; private set; } = 0;
        public List<Vector3f> RelativeSpawnedPositions { get; private set; } = new List<Vector3f>();

        // Vertical Plane - used for spawning orbs along side of billboards
        // Settings to FALSE - will treat it as a volume object (Rectangle of diagonal points)
        public bool IsVerticalPlane = false;

        public double rarity { get; private set; } = 1;


        /// <summary>
        /// -1: valid for all(default), 0: easy only , 0.5: easy + normal, 1: normal only
        /// </summary>
        public double difficulty { get; private set; } = -1;

        public bool SplitterAllowedToSpawn = false;


        #region Plane Masks
        // enemy types
        public List<Enemy.EnemyTypes> BannedTypes = new List<Enemy.EnemyTypes>();

        // Presets
        /// <summary> Bans ShieldOrb, Drone</summary>
        public static readonly List<Enemy.EnemyTypes> Mask_Flatground = new List<Enemy.EnemyTypes>() {
            Enemy.EnemyTypes.ShieldOrb, Enemy.EnemyTypes.Drone, Enemy.EnemyTypes.Sniper, Enemy.EnemyTypes.Turret
        };

        /// <summary> Bans ShieldOrb, Drone, Weeb</summary>
        public static readonly List<Enemy.EnemyTypes> Mask_Highground = new List<Enemy.EnemyTypes>() {
            Enemy.EnemyTypes.ShieldOrb, Enemy.EnemyTypes.Drone, Enemy.EnemyTypes.Weeb, Enemy.EnemyTypes.Sniper, Enemy.EnemyTypes.Turret
        };

        /// <summary> Bans ShieldOrb, Drone, Weeb and Waver </summary>
        public static readonly List<Enemy.EnemyTypes> Mask_HighgroundLimited = new List<Enemy.EnemyTypes>() {
            Enemy.EnemyTypes.ShieldOrb, Enemy.EnemyTypes.Drone, Enemy.EnemyTypes.Weeb, Enemy.EnemyTypes.Waver, Enemy.EnemyTypes.Sniper, Enemy.EnemyTypes.Turret
        };

        /// <summary> Bans every enemy, besides Waver - use this only for Waver/gecko spawns </summary>
        public static readonly List<Enemy.EnemyTypes> Mask_Waver = new List<Enemy.EnemyTypes>() {
            Enemy.EnemyTypes.ShieldOrb, Enemy.EnemyTypes.Weeb, Enemy.EnemyTypes.Default, Enemy.EnemyTypes.Drone, Enemy.EnemyTypes.Sniper, Enemy.EnemyTypes.Turret
        };

        /// <summary> Bans every enemy, besides Drone - use this only for drone spawns </summary>
        public static readonly List<Enemy.EnemyTypes> Mask_Airborne = new List<Enemy.EnemyTypes>() {
            Enemy.EnemyTypes.ShieldOrb, Enemy.EnemyTypes.Weeb, Enemy.EnemyTypes.Default, Enemy.EnemyTypes.Waver, Enemy.EnemyTypes.Sniper, Enemy.EnemyTypes.Turret
        };

        /// <summary> Bans every enemy, besides Sniper - use this only for sniper spawns </summary>
        public static readonly List<Enemy.EnemyTypes> Mask_Sniper = new List<Enemy.EnemyTypes>() {
            Enemy.EnemyTypes.Default, Enemy.EnemyTypes.Waver, Enemy.EnemyTypes.Drone, Enemy.EnemyTypes.ShieldOrb, Enemy.EnemyTypes.Weeb, Enemy.EnemyTypes.Turret
        };

        /// <summary> Bans every enemy, besides Turret - use this only for turret spawns </summary>
        public static readonly List<Enemy.EnemyTypes> Mask_Turret = new List<Enemy.EnemyTypes>() {
           Enemy.EnemyTypes.Default, Enemy.EnemyTypes.Waver, Enemy.EnemyTypes.Drone, Enemy.EnemyTypes.ShieldOrb, Enemy.EnemyTypes.Weeb, Enemy.EnemyTypes.Sniper
        };

        /// <summary> Bans every enemy, besides ShieldOrb - use this only for ShieldOrb spawns </summary>
        public static readonly List<Enemy.EnemyTypes> Mask_ShieldOrb = new List<Enemy.EnemyTypes>() {
           Enemy.EnemyTypes.Default, Enemy.EnemyTypes.Waver, Enemy.EnemyTypes.Drone, Enemy.EnemyTypes.Weeb, Enemy.EnemyTypes.Sniper, Enemy.EnemyTypes.Turret
        };

        #endregion

        // patrol points - for drones
        public List<Tuple<DeepPointer, Vector3f>> PatrolPoints = new List<Tuple<DeepPointer, Vector3f>>();

        public SpawnInfo spawnInfo;

        //// By default use all unused planes for EnemiesWithoutCP
        public bool ReuseFlag = true;


        public SpawnPlane(Vector3f a, Vector3f b, Angle angle) {
            cornerA = a;
            cornerB = b;
            this.angle = angle;
        }

        public SpawnPlane(Vector3f a, Vector3f b) : this(a, b, null) {}

        public SpawnPlane RandomAngle() {
            FixedAngle = false;
            return this;
        }

        /// <summary>Use plane as vertical plane, like a billboard</summary>
        public SpawnPlane AsVerticalPlane() {
            IsVerticalPlane = true;
            return this;
        }

        /// <summary>Adds patrol point, for Drones</summary>
        public SpawnPlane AddPatrolPoint(DeepPointer dp, Vector3f pos) {
            PatrolPoints.Add(new Tuple<DeepPointer, Vector3f>(dp, pos));
            return this;
        }

        /// <summary>Sets custom spawnInfo; Turrets, Snipers...</summary>
        public SpawnPlane SetSpawnInfo(SpawnInfo spawnInfo) {
            this.spawnInfo = spawnInfo;
            return this;
        }

        /// <summary>Setting max enemies for plane</summary>
        public SpawnPlane SetMaxEnemies(int MaxEnemies) {
            this.MaxEnemies = MaxEnemies;
            return this;
        }

        /// <summary>Flags to avoid using in EnemiesWithoutCP</summary>
        public SpawnPlane DoNotReuse() {
            ReuseFlag = false;
            return this;
        }

        /// <summary>Spawn rarity, 0.2 is kinda rare, 0.1 is rare, 0.05 is super rare, 0.01 is legandary spawn(1%)</summary>
        public SpawnPlane setRarity(double value) {
            if(value > 1) value = 1;
            if(value < 0) value = 0;
            rarity = value;
            return this;
        }

        /// <summary> Sets plane's validation for specific difficulties </summary>
        public SpawnPlane setDiff(double diff) {
            if(diff % 0.5 > 0) diff -= diff % 0.5f;
            if(diff < -1) diff = -1;
            if(diff > 1) diff = 1;

            difficulty = diff;
            return this;
        }

        // ban enemy type from using this spawnPlane
        public SpawnPlane BanEnemyType(Enemy.EnemyTypes enemyType) {
            if(BannedTypes.Contains(enemyType)) return this;
            BannedTypes.Add(enemyType);
            return this;
        }


        public SpawnPlane Mask(List<Enemy.EnemyTypes> mask) {
            BannedTypes = new List<Enemy.EnemyTypes>(mask);
            return this;
        }

        private bool ValidDiff() {
            if(difficulty < 0) return true; // all

            if(Config.GetInstance().Setting_Difficulty == Config.Difficulty.Easy)
                    return difficulty <= 0.5;

            if(Config.GetInstance().Setting_Difficulty == Config.Difficulty.Normal)
                return difficulty >= 0.5 && difficulty <= 1.5;
            
            // return true otherwise, because we care only for easy and normal
            return true;
        }

        public bool IsEnemyAllowed(Enemy.EnemyTypes enemyType, double rarity = 1) {
            // spawn plane valid for diff?
            if(!ValidDiff()) return false;

            // rarity
            // Easy - no rarity feature(no rare spawn), Nightmare - all included
            if(Config.GetInstance().Setting_Difficulty == Config.Difficulty.Easy) rarity = 1;
            else if(Config.GetInstance().Setting_Difficulty == Config.Difficulty.Nightmare) rarity = 0;
            if(this.rarity < rarity) return false;

            // splitter? check if allowed
            if(enemyType == Enemy.EnemyTypes.Splitter) {
                return SplitterAllowedToSpawn;
            } 

            if(BannedTypes.Count == 0) return true;
            return !BannedTypes.Contains(enemyType);
        }

        public bool IsAllowed(double rarity = 1) {
            if(!ValidDiff()) return false;

            // Easy - no rarity feature, Nightmare - all included
            if(Config.GetInstance().Setting_Difficulty == Config.Difficulty.Easy) rarity = 1;
            else if(Config.GetInstance().Setting_Difficulty == Config.Difficulty.Nightmare) rarity = 0;

            if(this.rarity < rarity) return false;
            return true;
        }

        public SpawnPlane AllowSplitter() {
            SplitterAllowedToSpawn = true;
            return this;
        }


        // single pos
        public SpawnPlane(Vector3f a) {
            cornerA = a;
            SetMaxEnemies(1);
        }

        public SpawnPlane(Vector3f a, Angle angle) : this(a) {
            this.angle = angle;
        }

        public SpawnPlane(Vector3f a, Quaternion q) : this(a) {
            angle = new QuaternionAngle(q);
        }

        public SpawnData GetRandomSpawnData() {
            // only 1 point, send cornerA as single pos
            if(cornerB.IsEmpty()) {
                return new SpawnData(cornerA, GetAngle(), PatrolPoints, spawnInfo);
            } else {
                return new SpawnData(IsVerticalPlane ? RandomWithInVerticalPlane(cornerA, cornerB) : RandomWithinRect_Spaced(cornerA, cornerB), GetAngle(), PatrolPoints ,spawnInfo);
            }
        }

        private Vector3f RandomWithinRect(Vector3f a, Vector3f b) {
            float xDelta = Math.Max(a.X, b.X) - Math.Min(a.X, b.X);
            float newX = Math.Min(a.X, b.X) + Config.GetInstance().r.Next((int)xDelta);

            float yDelta = Math.Max(a.Y, b.Y) - Math.Min(a.Y, b.Y);
            float newY = Math.Min(a.Y, b.Y) + Config.GetInstance().r.Next((int)yDelta);

            float zDelta = Math.Max(a.Z, b.Z) - Math.Min(a.Z, b.Z);
            float newZ = Math.Min(a.Z, b.Z) + Config.GetInstance().r.Next((int)zDelta);

            return new Vector3f(newX, newY, newZ);
        }

        private Vector3f RandomWithinRect_Spaced(Vector3f a, Vector3f b) {
            if(MaxEnemies == 1) return RandomWithinRect(a, b); // single enemy -> default
            if(CurrEnemeies == 0) { // multiple enemies but it's the first -> default
                Vector3f vec = RandomWithinRect(a, b);
                return vec;
            } else {
                // create n random spawns
                List<Vector3f> NewSpawns = new List<Vector3f>();
                for(int i = 0; i < ADDITIONAL_SPAWNS; i++) {
                    NewSpawns.Add(RandomWithinRect(a, b));
                }

                // compare distance of new spawns versus existing spawns, and remove below min spawn spacing
                List<Vector3f> ValidNewSpawns = new List<Vector3f>(NewSpawns);
                for(int i = 0; i < NewSpawns.Count; i++) {
                    for(int j = 0; j < RelativeSpawnedPositions.Count; j++) {
                        if(Vector3f.Distance(NewSpawns[i], RelativeSpawnedPositions[j]) < MIN_SPAWN_SPACING) {
                            ValidNewSpawns.Remove(NewSpawns[i]);
                            break;
                        }
                    }
                }

                // Any valid spawns left? pick any!
                if(ValidNewSpawns.Count > 0) {
                    return ValidNewSpawns[Config.GetInstance().r.Next(ValidNewSpawns.Count)];
                } else { // failed to find spaced spot -> default rng
                    return NewSpawns[Config.GetInstance().r.Next(NewSpawns.Count)];
                }
            }
        }

        public bool ValidForSplitter() {
            if(cornerB.IsEmpty()) return false;

            // check if enough space (default clone radius, 500 * 2 from both sides)
            float xDelta = Math.Max(cornerA.X, cornerB.X) - Math.Min(cornerA.X, cornerB.X);
            float yDelta = Math.Max(cornerA.Y, cornerB.Y) - Math.Min(cornerA.Y, cornerB.Y);
            return (Math.Abs(xDelta)-1000 > 0 && Math.Abs(yDelta)-1000 > 0);
        }

        private Vector3f RandomWithInVerticalPlane(Vector3f a, Vector3f b) {
            float x = Config.GetInstance().r.Next(Math.Min((int)a.X, (int)b.X), Math.Max((int)a.X, (int)b.X));
            float y = a.Y + (b.Y - a.Y) / (b.X - a.X) * (x - a.X);
            float z = Config.GetInstance().r.Next(Math.Min((int)a.Z, (int)b.Z), Math.Max((int)a.Z, (int)b.Z));
            return new Vector3f(x, y, z);
        }

        private Angle GetAngle() {
            // no defined angle + fixedAngle tag = return null(won't change angle)
            if(angle == null && FixedAngle == true) return null;

            // fixed angle? return it
            if(FixedAngle || angle is QuaternionAngle) {
                return angle;
            } else {
                // random angle? generate one
                Random r = new Random();
                int angle = r.Next(0, 360);
                return new Angle((float)Math.Sin(angle), (float)Math.Cos(angle));
            }
        }

        // for RoomLayout, to track curr enemies 
        public void EnemyAdded(Vector3f relativePos) {
            CurrEnemeies++;
            RelativeSpawnedPositions.Add(relativePos);
        }

        public void ResetCurrEnemies() {
            CurrEnemeies = 0;
            RelativeSpawnedPositions.Clear();
        }
        
        public bool CanAddEnemies() => CurrEnemeies < MaxEnemies;
    }
}

