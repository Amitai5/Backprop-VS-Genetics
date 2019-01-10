using System;

namespace NeuralNetLIB.ActivationFunctions
{
    public class TanH : IActivationFunc
    {
        public bool DerrivativeRequiresOutput => true;
        public double DendriteMinGen => -5;
        public double DendriteMaxGen => 5;

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
