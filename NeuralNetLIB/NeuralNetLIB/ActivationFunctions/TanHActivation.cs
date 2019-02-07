using System;

namespace NeuralNetLIB.ActivationFunctions
{
    public class TanH : ActivationFunc
    {
        public double Derivative(double x)
        {
            return 1 - Math.Pow(Math.Tanh(x), 2);
        }

        public double Function(double x)
        {
            return Math.Tanh(x);
        }
    }
}
