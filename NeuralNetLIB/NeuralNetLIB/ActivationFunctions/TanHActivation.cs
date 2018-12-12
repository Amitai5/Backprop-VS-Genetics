using System;

namespace NeuralNetLIB.ActivationFunctions
{
    public class TanH : IActivationFunc
    {
        public bool UseSecondDerivative => true;
        public double Min => -6;
        public double Max => 6;

        public double Derivative(double x)
        {
            throw new NotImplementedException();
        }

        public double Derivative2(double y)
        {
            return 1 - Math.Pow(y, 2);
        }

        public double Function(double x)
        {
            //The ReLU Function Itself
            return Math.Tanh(x);
        }
    }
}
