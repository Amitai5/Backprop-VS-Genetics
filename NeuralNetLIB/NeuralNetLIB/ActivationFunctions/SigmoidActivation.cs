using System;

namespace NeuralNetLIB.ActivationFunctions
{
    public class Sigmoid : IActivationFunc
    {
        public bool DerrivativeRequiresOutput => true;
        public double Min => -6;
        public double Max => 6;

        public double Derivative(double x)
        {
            //The Derrivative Value Of The Point In Which You Are AT
            return Function(x) * (1 - Function(x));
        }

        public double Derivative2(double y)
        {
            //Only Run The Value The Neuron Outputted Once Through The Derrivative Function
            return y * (1 - y);
        }

        public double Function(double x)
        {
            //The Sigmoid Function Itself
            return 1.0 / (1.0 + Math.Exp(-x));
        }
    }
}
