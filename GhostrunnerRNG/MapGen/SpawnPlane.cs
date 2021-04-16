using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using System;
using System.Collections.Generic;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.MapGen {
    public class SpawnPlane {

        // 2 sides of the plane
        public Vector3f cornerA { get; private set; }
        public Vector3f cornerB { get; private set; }

        // angle
        public bool FixedAngle = true;
        public Angle angle { get; private set; } = null;

        // max/curr enemies per plane(for RoomLayout)
        public int MaxEnemies { get; private set; } = 1;
        public int CurrEnemeies { get; private set; } = 0;

        // Vertical Plane - used for spawning orbs along side of billboards
        // Settings to FALSE - will treat it as a volume object (Rectangle of diagonal points)
        public bool IsVerticalPlane = false;

        public double rarity { get; private set; } = 1;

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

        /// <summary> Bans every enemy, besides Drone, use this only for drone spawns </summary>
        public static readonly List<Enemy.EnemyTypes> Mask_Airborne = new List<Enemy.EnemyTypes>() {
            Enemy.EnemyTypes.ShieldOrb, Enemy.EnemyTypes.Weeb, Enemy.EnemyTypes.Default, Enemy.EnemyTypes.Waver, Enemy.EnemyTypes.Weeb, Enemy.EnemyTypes.Sniper, Enemy.EnemyTypes.Turret
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

        public bool IsEnemyAllowed(Enemy.EnemyTypes enemyType, double rarity = 1) {
            if(this.rarity < rarity) return false;

            // splitter? check if allowed
            if(enemyType == Enemy.EnemyTypes.Splitter) {
                if(SplitterAllowedToSpawn) return true;
                return ValidForSplitter();
            } 

            if(BannedTypes.Count == 0) return true;
            return !BannedTypes.Contains(enemyType);
        }

        public bool IsAllowed(double rarity = 1) {
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
                return new SpawnData(IsVerticalPlane ? RandomWithInVerticalPlane(cornerA, cornerB) : RandomWithinRect(cornerA, cornerB), GetAngle(), PatrolPoints ,spawnInfo);
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
        public void EnemyAdded() => CurrEnemeies++;
        
        public void ResetCurrEnemies() =>CurrEnemeies = 0;
        
        public bool CanAddEnemies() => CurrEnemeies < MaxEnemies;
    }
}

