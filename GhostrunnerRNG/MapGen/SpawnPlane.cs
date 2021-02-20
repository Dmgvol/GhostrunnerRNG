using GhostrunnerRNG.Game;
using System;

namespace GhostrunnerRNG.Maps {
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

        public SpawnPlane(Vector3f a, Vector3f b, Angle? angle) {
            // fix oriantation
            cornerA = new Vector3f(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y), Math.Min(a.Z, b.Z));
            cornerB = new Vector3f(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y), Math.Max(a.Z, b.Z));
            this.angle = angle;
        }

        public SpawnPlane(Vector3f a, Vector3f b) : this(a, b, null) {}

        public SpawnPlane RandomAngle() {
            FixedAngle = false;
            return this;
        }

        public SpawnPlane SetMaxEnemies(int MaxEnemies) {
            this.MaxEnemies = MaxEnemies;
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

        public SpawnData GetRandomSpawnData() {
            // only 1 point, send cornerA as single pos
            if(cornerB.IsEmpty()) {
                return new SpawnData(cornerA, GetAngle());
            } else {
                // generate random spot within 2 points
                Vector3f newPos = new Vector3f(
                    cornerA.X + r.Next((int)Math.Abs(cornerB.X - cornerA.X)),
                    cornerA.Y + r.Next((int)Math.Abs(cornerB.Y - cornerA.Y)),
                    cornerA.Z + r.Next((int)Math.Abs(cornerB.Z - cornerA.Z))
                    );

                return new SpawnData(newPos, GetAngle());
            }
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
