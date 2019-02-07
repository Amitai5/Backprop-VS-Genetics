﻿namespace NeuralNetLIB.ActivationFunctions
{
    public class ReLU : ActivationFunc
    {
        public double Derivative(double x)
        {
            //The Derrivative Value Of The Point In Which You Are AT
            if (x > 1)
            {
                return 1;
            }
            else if (x < 0)
            {
                return 0;
            }
            return x;
        }

        public double Function(double x)
        {
            //The ReLU Function Itself
            if (x < 0)
            {
                return 0;
            }
            return x;
        }
    }
}
