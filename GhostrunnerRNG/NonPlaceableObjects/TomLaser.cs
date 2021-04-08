using GhostrunnerRNG.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static GhostrunnerRNG.Game.GameUtils;

namespace GhostrunnerRNG.NonPlaceableObjects {
    class TomLaser : NonPlaceableObject {

        private List<Tuple<Vector3f, QuaternionAngle>> LaserSpots = new List<Tuple<Vector3f, QuaternionAngle>>();
        private Vector3f TomCore = new Vector3f(51881.16f, 20805.03f, 0);
        private bool IsFlipped = false;
        private bool AsVertical = false;
        private Vector3f posOffset = Vector3f.Empty;
        public TomLaser(int offset) {
            ObjectDP = new DeepPointer(0x045A3C20, 0x98, 0x30, 0x128, 0xA8, 0x708, offset);

            // pointers
            Pointers.Add("Pos", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x11C), IntPtr.Zero));
            Pointers.Add("VerticalAngle", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x128), IntPtr.Zero));
            Pointers.Add("Rotation", new Tuple<DeepPointer, IntPtr>(AppendBaseOffset(0x1A0), IntPtr.Zero));
        }

        public TomLaser AddPosOffset(Vector3f posOffset) {
            this.posOffset = posOffset;
            return this;
        }

        public override void Randomize(Process game) {
            // deref
            DerefPointers(game);

            // pick random laser spot
            int r = Config.GetInstance().r.Next(LaserSpots.Count);
            // position
            game.WriteBytes(Pointers["Pos"].Item2, BitConverter.GetBytes(LaserSpots[r].Item1.X + posOffset.X));
            game.WriteBytes(Pointers["Pos"].Item2 + 4, BitConverter.GetBytes(LaserSpots[r].Item1.Y + posOffset.Y));
            game.WriteBytes(Pointers["Pos"].Item2 + 8, BitConverter.GetBytes(LaserSpots[r].Item1.Z + posOffset.Z));

            // vertical angle must be -90 for lasers on opposite side, for custom angle (weird logic but okay)
            if(AsVertical) game.WriteBytes(Pointers["VerticalAngle"].Item2, BitConverter.GetBytes((float)0f));
            else if(IsFlipped) game.WriteBytes(Pointers["VerticalAngle"].Item2, BitConverter.GetBytes(-90f));

            if(AsAngles) {
                game.WriteBytes(Pointers["VerticalAngle"].Item2 + 4, BitConverter.GetBytes(LaserSpots[r].Item2.quaternion.x  + (IsFlipped ? 180 : 0)));
                return;
            }


            // rotation
            if(!Section3Flag) {
                game.WriteBytes(Pointers["Rotation"].Item2, BitConverter.GetBytes(LaserSpots[r].Item2.quaternion.x));
                game.WriteBytes(Pointers["Rotation"].Item2 + 4, BitConverter.GetBytes(LaserSpots[r].Item2.quaternion.y));
                game.WriteBytes(Pointers["Rotation"].Item2 + 8, BitConverter.GetBytes(LaserSpots[r].Item2.quaternion.z));
                game.WriteBytes(Pointers["Rotation"].Item2 + 12, BitConverter.GetBytes(LaserSpots[r].Item2.quaternion.w));
            } else {
                game.WriteBytes(Pointers["VerticalAngle"].Item2 + 4, BitConverter.GetBytes(LaserSpots[r].Item2.quaternion.x));
            }
        }

        public void AddLaserSpot(Vector3f pos, QuaternionAngle angle) {
            LaserSpots.Add(new Tuple<Vector3f, QuaternionAngle>(pos, angle));
        }

        public TomLaser AddLaserRange(int spotsToLeft, int spotsToRight, QuaternionAngle offset, bool flipped = false) {
            AddLaserSpotRange(spotsToLeft, spotsToRight, offset, flipped);
            return this;
        }



        private bool AsAngles = false;
        public TomLaser AddLaserRangeAngles(int spotsToLeft, int spotsToRight) {
            AsAngles = true;
            AddLaserSpotRangeAngles(spotsToLeft, spotsToRight);
            return this;
        }

        public void AddLaserSpotRangeAngles(int spotsToLeft, int spotsToRight) {
            QuaternionAngle originAngle = new QuaternionAngle(0, 0 , 0);
           
            for(int i = spotsToLeft; i <= spotsToRight; i++) {
                Tuple<Vector3f, QuaternionAngle> laserSpot = MoveLaser(originAngle, TomCore, 560, i);
                AddLaserSpot(laserSpot.Item1, new QuaternionAngle((15 * i), 0, 0, 0));
            }
           
        }



        public TomLaser SetVertical() {
            AsVertical = true;
            return this;
        }

        public void AddLaserSpotRange(int spotsToLeft, int spotsToRight, QuaternionAngle offset, bool flipped = false) {
            QuaternionAngle originAngle = new QuaternionAngle(-90 + offset.angleYaw, -90 + offset.anglePitch, offset.angleRoll);
            if(!Section3Flag) {
                IsFlipped = flipped;
                for(int i = spotsToLeft; i <= spotsToRight; i++) {
                    Tuple<Vector3f, QuaternionAngle> laserSpot = MoveLaser(originAngle, TomCore, 560 * (flipped ? (-1) : 1), i);
                    AddLaserSpot(laserSpot.Item1, laserSpot.Item2);
                }
            } else {
                for(int i = spotsToLeft; i <= spotsToRight; i++) {
                    Tuple<Vector3f, QuaternionAngle> laserSpot = MoveLaser(originAngle, TomCore, 560 * (flipped ? (-1) : 1), i);
                    AddLaserSpot(laserSpot.Item1, new QuaternionAngle(offset.quaternion.x + (15 * i * (offset.quaternion.x > 0 ? (-1) : 1)), 0, 0, 0));
                }
            }
        }

        protected override void ReadDefaultValues(Process game) {}

        private Vector3f PosOnCircleRelative(float centerX, float centerY, float r, float steps) {
            float y = (float)(r * Math.Sin((Math.PI / 180) * (15 * steps)) + centerX);
            float z = (float)(r * Math.Cos((Math.PI / 180) * (15 * steps)) + centerY);
            if(!Section3Flag)
                return new Vector3f(0, y - centerX, z - centerY);
            else
                return new Vector3f(y - centerX, z - centerY, 0);
        }

        private QuaternionAngle GetCirclePointAngle(QuaternionAngle currAngle, int steps) {
            return new QuaternionAngle(CreateQuaternion(currAngle.angleYaw, currAngle.anglePitch + (-steps * 15), currAngle.angleRoll));
        }

        private Tuple<Vector3f, QuaternionAngle> MoveLaser(QuaternionAngle currAngle, Vector3f center, float r, int steps) {
            return new Tuple<Vector3f, QuaternionAngle>(PosOnCircleRelative(center.X, center.Y, r, steps), GetCirclePointAngle(currAngle, steps));
        }

        private bool Section3Flag;
        public TomLaser SectionThree() {
            Section3Flag = true;
            return this;
        }
    }
}
