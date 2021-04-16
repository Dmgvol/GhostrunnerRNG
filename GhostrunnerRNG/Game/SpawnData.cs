using System;
using System.Collections.Generic;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.Game {

    // SpwanData Struct, contains vector3 pos and Angle(sin&cos)
    public class SpawnData {
        public Vector3f pos;
        public Angle angle;

        // Patrol Points - for drones, list of pairs<pointer and vector>
        public List<Tuple<DeepPointer, Vector3f>> patrolPoints { get; private set; }

        public SpawnInfo spawnInfo { get; private set; }

        public SpawnData(Vector3f pos, Angle angle) {
            this.pos = pos;
            this.angle = angle;
            patrolPoints = null;
            spawnInfo = null;
        }

        public bool HasAngle() => angle != null;

        public SpawnData(Vector3f pos) : this(pos, null) { }

        public SpawnData(Vector3f pos, Angle angle, List<Tuple<DeepPointer, Vector3f>> patrolPoints, SpawnInfo spawnInfo) : this(pos, angle) {
            this.patrolPoints = patrolPoints; // transfer patrol points from SpawnPlane to SpawnData (for drones)
            this.spawnInfo = spawnInfo;
        }
    }

    // Angle Struct
    public class Angle {
        public static Angle Empty = new Angle(0, 0);
        public float angleSin, angleCos;
        public Angle() {

        }

        public Angle(float angleSin, float angleCos) { this.angleSin = angleSin; this.angleCos = angleCos; }
    }

    public class QuaternionAngle : Angle {
        public Quaternion quaternion { get; private set; }
        public float angleYaw { get; private set; }
        public float anglePitch { get; private set; }
        public float angleRoll { get; private set; }

        public QuaternionAngle(float angleYaw, float anglePitch, float angleRoll) {
            this.angleYaw = angleYaw;
            this.anglePitch = anglePitch;
            this.angleRoll = angleRoll;
            quaternion = CreateQuaternion(angleYaw, anglePitch, angleRoll);
        }

        public QuaternionAngle(float x, float y, float z, float w) {
            quaternion = new Quaternion(x, y, z, w);
        }

        public QuaternionAngle(Quaternion q) => quaternion = q;

        public override string ToString() => quaternion.ToString();

    }

    public class SpawnInfo{
        public Vector3f Pos;
    }
}
