using System;

namespace MachineLearningLIB.ActivationFunctions
{
    public class Sigmoid : ActivationFunc
    {
        public override double Derivative(double x)
        {
            //The Derivative Value Of The Point In Which You Are AT
            return Function(x) * (1 - Function(x));
        }

        public override double Function(double x)
        {
            //The Sigmoid Function Itself
            return 1.0 / (1.0 + Math.Exp(-x));
        }
    }
}
