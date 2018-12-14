using System;

namespace NeuralNetLIB.ActivationFunctions
{
    public class SoftSine : IActivationFunc
    {
        public bool DerrivativeRequiresOutput => false;
        public double Min => -6;
        public double Max => 6;

        public double Derivative(double x)
        {
            //The Derrivative Value Of The Point In Which You Are AT
            return 1 / Math.Pow(1 + Math.Abs(x), 2);
        }

        public double Derivative2(double y)
        {
            throw new NotImplementedException();
        }

        public double Function(double x)
        {
            //The SoftSine Function Itself
            return x / (1 + Math.Abs(x));
        }
    }
}
