﻿using System;

namespace NeuralNetLIB.ActivationFunctions
{
    public class LeakyReLU : IActivationFunc
    {
        public bool UseSecondDerivative => false;
        public double Min => -6;
        public double Max => 6;

        public double Derivative(double x)
        {
            //The Derrivative Value Of The Point In Which You Are AT
            if (x >= 1)
            {
                return 1;
            }
            else
            {
                return 0.01;
            }
        }

        public double Derivative2(double y)
        {
            throw new NotImplementedException();
        }

        public double Function(double x)
        {
            //The ReLU Function Itself
            if (x < 0)
            {
                return 0.01 * x;
            }
            return x;
        }
    }
}
