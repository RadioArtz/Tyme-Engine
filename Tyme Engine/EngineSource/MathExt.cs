﻿using System;
using System.Numerics;

namespace Tyme_Engine.Core
{
    public static class MathExt
    {
        public static double Lerp(double a,double b,float alpha)
        {
            return (a+(b-a)*alpha);
        }
        public static float Lerp(float a, float b, float alpha)
        {
            return (a+(b-a)*alpha);
        }
        public static Vector2 Vlerp(Vector2 a, Vector2 b, float alpha)
        {
            return new Vector2((a.X + (b.X - a.X) * alpha), (a.Y + (b.Y - a.Y) * alpha));
        }
        public static Vector3 Vlerp(Vector3 a, Vector3 b, float alpha)
        {
            return new Vector3((a.X + (b.X - a.X) * alpha), (a.Y + (b.Y - a.Y) * alpha), (a.Z + (b.Z - a.Z) * alpha));
        }
        public static Vector4 Vlerp(Vector4 a, Vector4 b, float alpha)
        {
            return new Vector4((a.X + (b.X - a.X) * alpha), (a.Y + (b.Y - a.Y) * alpha), (a.Z + (b.Z - a.Z) * alpha), (a.W + (b.W - a.W) * alpha));
        }
        public static double Clamp01(double input)
        {
            if (input > 1) { return 1; }
            else if (input < 0) { return 0; }
            return input;
        }
        public static float Clamp01(float input)
        {
            if (input > 1) { return 1; }
            else if (input < 0) { return 0; }
            return input;
        }
        public static int Clamp01(int input)
        {
            if (input > 1) { return 1; }
            else if (input < 0) { return 0; }
            return input;
        }

        /// <summary>
        /// Calculates a rotations Forward Vector. Expects rotation in Degrees.
        /// </summary>
        /// <param name="rotationDeg"> Rotation in Degrees</param>
        /// <returns></returns>
        public static OpenTK.Vector3 GetForwardVector(OpenTK.Vector3 rotationDeg)
        {
            OpenTK.Vector3 rotRadians = (rotationDeg * (OpenTK.MathHelper.Pi / 180f));
            OpenTK.Vector3 front;
            front.X = (float)(Math.Cos(rotRadians.X)* Math.Cos(rotRadians.Y));
            front.Y = (float)Math.Sin(rotRadians.X);
            front.Z = (float)(Math.Cos(rotRadians.X) * Math.Sin(rotRadians.Y));
            front.Normalize();
            return front;
        }

        /// <summary>
        /// Calculates a rotations Right Vector. Expects rotation in Degrees.
        /// </summary>
        /// <param name="rotationDeg"> Rotation in Degrees</param>
        /// <returns></returns>
        public static OpenTK.Vector3 GetRightVector(OpenTK.Vector3 rotationDeg)
        {
            return OpenTK.Vector3.Normalize(OpenTK.Vector3.Cross(GetForwardVector(rotationDeg), OpenTK.Vector3.UnitY));
        }

        /// <summary>
        /// Calculates a rotations Up Vector. Expects rotation in Degrees.
        /// </summary>
        /// <param name="rotationDeg"> Rotation in Degrees</param>
        /// <returns></returns>
        public static OpenTK.Vector3 GetUpVector(OpenTK.Vector3 rotationDeg)
        {
            return OpenTK.Vector3.Normalize(OpenTK.Vector3.Cross(GetRightVector(rotationDeg),GetForwardVector(rotationDeg)));
        }
    }
}
