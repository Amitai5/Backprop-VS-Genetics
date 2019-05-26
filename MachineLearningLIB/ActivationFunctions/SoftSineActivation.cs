using System;

namespace MachineLearningLIB.ActivationFunctions
{
    public class SoftSine : ActivationFunc
    {
        public override double Derivative(double x)
        {
            //The Derivative Value Of The Point In Which You Are AT
            return 1 / Math.Pow(1 + Math.Abs(x), 2);
        }

        public override double Function(double x)
        {
            //The SoftSine Function Itself
            return x / (1 + Math.Abs(x));
        }
    }
}
