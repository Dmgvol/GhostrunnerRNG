using GhostrunnerRNG.Maps;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GhostrunnerRNG.MapGen {
    // Room is the same as a geometrical rectangle class, just different name.

    class Room {
        Vector3f pointA, pointB;

        public Room(Vector3f a, Vector3f b) {
            pointA = a;
            pointB = b;
        }

        private bool IsEnemyInRoom(Enemy enemy) {
            return (enemy.Pos.X >= Math.Min(pointA.X, pointB.X) && enemy.Pos.X <= Math.Max(pointA.X, pointB.X) &&
               enemy.Pos.Y >= Math.Min(pointA.Y, pointB.Y) && enemy.Pos.Y <= Math.Max(pointA.Y, pointB.Y) &&
               enemy.Pos.Z >= Math.Min(pointA.Z, pointB.Z) && enemy.Pos.Z <= Math.Max(pointA.Z, pointB.Z));
        }

        public List<Enemy> ReturnEnemiesInRoom(List<Enemy> allEnemies) => allEnemies.Where(x => IsEnemyInRoom(x)).ToList();
    }
}
