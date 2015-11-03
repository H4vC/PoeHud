using System;
using System.Linq;
using SharpDX;

namespace PoeHUD.Framework.Helpers
{
    public static class MathHepler
    {
        private const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        static MathHepler()
        {
            Randomizer = new Random();
        }

        public static Random Randomizer { get; }

        public static double GetPolarCoordinates(this Vector2 vector, out double phi)
        {
            double distance = vector.Length();
            phi = Math.Acos(vector.X / distance);
            if (vector.Y < 0)
            {
                phi = MathUtil.TwoPi - phi;
            }
            return distance;
        }

        public static string GetRandomWord(int length)
        {
            var array = new char[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = CHARS[Randomizer.Next(CHARS.Length)];
            }
            return new string(array);
        }

        public static float Max(params float[] values)
        {
            float max = values.First();
            for (int i = 1; i < values.Length; i++)
            {
                max = Math.Max(max, values[i]);
            }
            return max;
        }

        public static Vector2 Translate(this Vector2 vector, float dx, float dy)
        {
            return new Vector2(vector.X + dx, vector.Y + dy);
        }

        public static Vector3 Translate(this Vector3 vector, float dx, float dy, float dz)
        {
            return new Vector3(vector.X + dx, vector.Y + dy, vector.Z + dz);
        }
    }
}