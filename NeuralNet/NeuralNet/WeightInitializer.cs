using System;

namespace NeuralNet
{
    public class WeightInitializer
    {
        private static readonly Random Rand = new Random();
        public InitializerMethod InitMethod { get; private set; }

        public WeightInitializer(InitializerMethod method)
        {
            InitMethod = method;
        }

        public double GetInitValue(double min, double max)
        {
            switch (InitMethod)
            {
                default:
                case InitializerMethod.Random:
                    return RandomInit(min, max);
            }
        }

        private double RandomInit(double min, double max)
        {
            return Rand.NextDouble() * (max - min) + min;
        }
    }
}
