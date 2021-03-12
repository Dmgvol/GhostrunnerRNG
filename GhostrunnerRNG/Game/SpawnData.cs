using System;
using System.Collections.Generic;

namespace GhostrunnerRNG.Game {

    // SpwanData Struct, contains vector3 pos and Angle(sin&cos)
    public struct SpawnData {
        public Vector3f pos { get; private set; }
        public Angle? angle{ get; private set; }

        // Patrol Points - for drones, list of pairs<pointer and vector>
        public List<Tuple<DeepPointer, Vector3f>> patrolPoints { get; private set; }
        // Patrol points - for snipers
        public SniperSpawnData sniperSpawnData { get; private set; }

        public SpawnData(Vector3f pos, Angle? angle) {
            this.pos = pos;
            this.angle = angle;
            patrolPoints = null;
            sniperSpawnData = null;
        }

        public bool HasAngle() => angle != null;

        public void AddPatrolPoint(DeepPointer dp, Vector3f pos) {
            if(patrolPoints.Contains(new Tuple<DeepPointer, Vector3f>(dp, pos))) return;
            if(patrolPoints == null) patrolPoints = new List<Tuple<DeepPointer, Vector3f>>();
            patrolPoints.Add(new Tuple<DeepPointer, Vector3f>(dp, pos));
        }

        public SpawnData(Vector3f pos) : this(pos, null) { }

        public SpawnData(Vector3f pos, Angle? angle, List<Tuple<DeepPointer, Vector3f>> patrolPoints, SniperSpawnData sniperSpawnData) : this(pos, angle) {
            this.patrolPoints = patrolPoints; // transfer patrol points from SpawnPlane to SpawnData (for drones)
            this.sniperSpawnData = sniperSpawnData; // transfer patrol points from SpawnPlane to SpawnData (for snipers)
        }
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

    // Sniper Data
    public class SniperSpawnData {
        // Sniper Aim patrol points
        public List<Tuple<Vector3f, float>> patrolPoints { get; private set; } = new List<Tuple<Vector3f, float>>();

        // Focus point, usually behind/around the sniper
        public List<Tuple<Vector3f, float>> focusPoints { get; private set; } = new List<Tuple<Vector3f, float>>();

        public void AddPatrolPoint(Vector3f pos, float delay = 0.2f) => patrolPoints.Add(new Tuple<Vector3f, float>(pos, delay));

        public void AddFocusPoint(Vector3f pos, float delay = 0.2f) => focusPoints.Add(new Tuple<Vector3f, float>(pos, delay));
            
        
    }
}
