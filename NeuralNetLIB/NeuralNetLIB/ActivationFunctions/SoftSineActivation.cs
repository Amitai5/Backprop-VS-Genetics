using System;

namespace NeuralNetLIB.ActivationFunctions
{
    public class SoftSine : ActivationFunc
    {
        public double Derivative(double x)
        {
            //The Derrivative Value Of The Point In Which You Are AT
            return 1 / Math.Pow(1 + Math.Abs(x), 2);
        }

        public double Function(double x)
        {
            //The SoftSine Function Itself
            return x / (1 + Math.Abs(x));
        }
    }
}
