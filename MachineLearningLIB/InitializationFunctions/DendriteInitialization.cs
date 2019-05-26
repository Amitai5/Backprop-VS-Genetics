using MachineLearningLIB.ActivationFunctions;
using System;

namespace MachineLearningLIB.InitializationFunctions
{
    public class DendriteInitialization
    {
        public static Func<ActivationFunc, Random, double> GetInitFunction(InitializationFunction initializationFunction)
        {
            switch (initializationFunction)
            {
                default:
                case InitializationFunction.Random:
                    return RandomInitialization;
            }
        }

        public static double RandomInitialization(ActivationFunc activationFunc, Random rand)
        {
            return rand.NextDouble(activationFunc.DendriteMinGen, activationFunc.DendriteMaxGen);
        }
    }
}
