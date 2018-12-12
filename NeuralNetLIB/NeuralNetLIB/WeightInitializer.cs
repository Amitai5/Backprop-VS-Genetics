using System;

namespace NeuralNetLIB
{
    public class WeightInitializer
    {
        private readonly Random RandGen;
        public InitializerMethod InitMethod { get; private set; }

        public WeightInitializer(InitializerMethod method)
        {
            InitMethod = method;
            RandGen = new Random();
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
            return RandGen.NextDouble() * (max - min) + min;
        }
    }
}
