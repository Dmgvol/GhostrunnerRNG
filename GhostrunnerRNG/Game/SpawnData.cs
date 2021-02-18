namespace GhostrunnerRNG.Game {

    // SpwanData Struct, contains vector3 pos and Angle(sin&cos)
    public struct SpawnData {
        public Vector3f pos { get; private set; }
        public Angle? angle{ get; private set; }

        public SpawnData(Vector3f pos, Angle? angle) {
            this.pos = pos;
            this.angle = angle;
        }

        public bool HasAngle() => angle != null;

        public SpawnData(Vector3f pos) : this(pos, null) { }
    }

    // Angle Struct
    public struct Angle {
        public static Angle Empty = new Angle(0, 0);

        public float angleSin;
        public float angleCos;

        public Angle(float angleSin, float angleCos) {
            this.angleSin = angleSin;
            this.angleCos = angleCos;
        }
    }
}
