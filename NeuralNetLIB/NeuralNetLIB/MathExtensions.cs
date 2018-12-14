using System;

namespace NeuralNetLIB
{
    public static class MathExtensions
    {
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            return val.CompareTo(min) < 0 ? min : val.CompareTo(max) > 0 ? max : val;
        }

        public static double NextDouble(this Random RandGen, double min, double max)
        {
            return RandGen.NextDouble() * (max - min) + min;
        }
    }
}
