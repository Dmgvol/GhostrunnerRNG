using GhostrunnerRNG.Maps;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GhostrunnerRNG.MapGen {
    // Room is the same as a geometrical rectangle class, just different name.

    class Room {
        Vector3f pointA, pointB;

        public Room(Vector3f a, Vector3f b) {
            // makes PointA the smallest and PointB the biggest for easy math,
            // they might be flipped but work the same
            pointA = new Vector3f(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y), Math.Min(a.Z, b.Z));
            pointB = new Vector3f(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y), Math.Max(a.Z, b.Z));
        }

        private bool IsEnemyInRoom(Enemy enemy) {
            return (enemy.Pos.X > pointA.X && enemy.Pos.Y > pointA.Y && enemy.Pos.Z > pointA.Z &&
                enemy.Pos.X < pointB.X && enemy.Pos.Y < pointB.Y && enemy.Pos.Z < pointB.Z);
        }

        public List<Enemy> ReturnEnemiesInRoom(List<Enemy> allEnemies) => allEnemies.Where(x => IsEnemyInRoom(x)).ToList();
    }
}
