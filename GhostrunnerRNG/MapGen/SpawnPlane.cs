using GhostrunnerRNG.Enemies;
using GhostrunnerRNG.Game;
using System;
using System.Collections.Generic;

namespace GhostrunnerRNG.MapGen {
    public class SpawnPlane {

        // 2 sides of the plane
        public Vector3f cornerA { get; private set; }
        public Vector3f cornerB { get; private set; }

        // angle
        public bool FixedAngle = true;
        public Angle? angle { get; private set; } = null;

        public static Random r; // randomizer

        // max/curr enemies per plane(for RoomLayout)
        public int MaxEnemies { get; private set; } = 1;
        public int CurrEnemeies { get; private set; } = 0;

        // Vertical Plane - used for spawning orbs along side of billboards
        // Settings to FALSE - will treat it as a volume object (Rectangle of diagonal points)
        public bool IsVerticalPlane = false;


        #region Plane Masks
        // enemy types
        public List<Enemy.EnemyTypes> BannedTypes = new List<Enemy.EnemyTypes>();

        // Presets
        /// <summary> Bans ShieldOrb, Drone</summary>
        public static readonly List<Enemy.EnemyTypes> Mask_Flatground = new List<Enemy.EnemyTypes>() {
            Enemy.EnemyTypes.ShieldOrb, Enemy.EnemyTypes.Drone, Enemy.EnemyTypes.Sniper
        };

        /// <summary> Bans ShieldOrb, Drone, Weeb</summary>
        public static readonly List<Enemy.EnemyTypes> Mask_Highground = new List<Enemy.EnemyTypes>() {
            Enemy.EnemyTypes.ShieldOrb, Enemy.EnemyTypes.Drone, Enemy.EnemyTypes.Weeb, Enemy.EnemyTypes.Sniper
        };

        /// <summary> Bans ShieldOrb, Drone, Weeb and Waver </summary>
        public static readonly List<Enemy.EnemyTypes> Mask_HighgroundLimited = new List<Enemy.EnemyTypes>() {
            Enemy.EnemyTypes.ShieldOrb, Enemy.EnemyTypes.Drone, Enemy.EnemyTypes.Weeb, Enemy.EnemyTypes.Waver, Enemy.EnemyTypes.Sniper
        };
        /// <summary> Bans ShieldOrb, Weeb, Default, Waver, Weeb </summary>
        public static readonly List<Enemy.EnemyTypes> Mask_Airborne = new List<Enemy.EnemyTypes>() {
            Enemy.EnemyTypes.ShieldOrb, Enemy.EnemyTypes.Weeb, Enemy.EnemyTypes.Default, Enemy.EnemyTypes.Waver, Enemy.EnemyTypes.Weeb, Enemy.EnemyTypes.Sniper
        };
        /// <summary> Bans every enemy, besides Sniper - use this only for sniper spawns </summary>
        public static readonly List<Enemy.EnemyTypes> Mask_Sniper = new List<Enemy.EnemyTypes>() {
            Enemy.EnemyTypes.Default, Enemy.EnemyTypes.Waver, Enemy.EnemyTypes.Drone, Enemy.EnemyTypes.ShieldOrb, Enemy.EnemyTypes.Weeb,
        };

        #endregion

        // patrol points - for drones
        public List<Tuple<DeepPointer, Vector3f>> PatrolPoints = new List<Tuple<DeepPointer, Vector3f>>();

        // SniperSpawn data - for snipers
        public SniperSpawnData sniperData;

        //// By default use all unused planes for EnemiesWithoutCP
        public bool ReuseFlag = true;


        public SpawnPlane(Vector3f a, Vector3f b, Angle? angle) {
            cornerA = a;
            cornerB = b;
            this.angle = angle;
        }

        public SpawnPlane(Vector3f a, Vector3f b) : this(a, b, null) {}

        public SpawnPlane RandomAngle() {
            FixedAngle = false;
            return this;
        }

        public SpawnPlane AsVerticalPlane() {
            IsVerticalPlane = true;
            return this;
        }

        public SpawnPlane AddPatrolPoint(DeepPointer dp, Vector3f pos) {
            PatrolPoints.Add(new Tuple<DeepPointer, Vector3f>(dp, pos));
            return this;
        }

        public SpawnPlane SetSniperData(SniperSpawnData data) {
            sniperData = data;
            return this;
        }

        public SpawnPlane SetMaxEnemies(int MaxEnemies) {
            this.MaxEnemies = MaxEnemies;
            return this;
        }

        public SpawnPlane DoNotReuse() {
            ReuseFlag = false;
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
        public bool IsEnemyAllowed(Enemy.EnemyTypes enemyType) {
            if(BannedTypes.Count == 0) return true;
            return !BannedTypes.Contains(enemyType);
        }

        // single pos
        public SpawnPlane(Vector3f a) {
            cornerA = a;
            SetMaxEnemies(1);
        }

        public SpawnPlane(Vector3f a, Angle angle) : this(a) {
            this.angle = angle;
        }

        public SpawnData GetRandomSpawnData() {
            // only 1 point, send cornerA as single pos
            if(cornerB.IsEmpty()) {
                return new SpawnData(cornerA, GetAngle(), PatrolPoints, sniperData);
            } else {
                if(IsVerticalPlane) {
                    return new SpawnData(RandomWithInVerticalPlane(cornerA, cornerB), GetAngle(), PatrolPoints, sniperData);
                } else {
                    return new SpawnData(RandomWithinRect(cornerA, cornerB), GetAngle(), PatrolPoints, sniperData);
                }
            }
        }

        private Vector3f RandomWithinRect(Vector3f a, Vector3f b) {
            float xDelta = Math.Max(a.X, b.X) - Math.Min(a.X, b.X);
            float newX = Math.Min(a.X, b.X) + r.Next((int)xDelta);

            float yDelta = Math.Max(a.Y, b.Y) - Math.Min(a.Y, b.Y);
            float newY = Math.Min(a.Y, b.Y) + r.Next((int)yDelta);

            float zDelta = Math.Max(a.Z, b.Z) - Math.Min(a.Z, b.Z);
            float newZ = Math.Min(a.Z, b.Z) + r.Next((int)zDelta);

            return new Vector3f(newX, newY, newZ);
        }

        private Vector3f RandomWithInVerticalPlane(Vector3f a, Vector3f b) {
            float x = r.Next(Math.Min((int)a.X, (int)b.X), Math.Max((int)a.X, (int)b.X));
            float y = a.Y + (b.Y - a.Y) / (b.X - a.X) * (x - a.X);
            float z = r.Next(Math.Min((int)a.Z, (int)b.Z), Math.Max((int)a.Z, (int)b.Z));
            return new Vector3f(x, y, z);
        }

        private Angle? GetAngle() {
            // no defined angle + fixedAngle tag = return null(won't change angle)
            if(angle == null && FixedAngle == true) return null;

            // fixed angle? return it
            if(FixedAngle) {
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

