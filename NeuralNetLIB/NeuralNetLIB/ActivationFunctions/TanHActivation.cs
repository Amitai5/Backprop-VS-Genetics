using System;

namespace NeuralNetLIB.ActivationFunctions
{
    public class TanH : ActivationFunc
    {
        public override double Derivative(double x)
        {
            return 1 - Math.Pow(Math.Tanh(x), 2);
        }

        public override double Function(double x)
        {
            return Math.Tanh(x);
        }
    }
}
