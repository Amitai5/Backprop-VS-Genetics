﻿using System;

namespace NeuralNetLIB.ActivationFunctions
{
    public class Sigmoid : ActivationFunc
    {
        public double Derivative(double x)
        {
            //The Derrivative Value Of The Point In Which You Are AT
            return Function(x) * (1 - Function(x));
        }

        public double Function(double x)
        {
            //The Sigmoid Function Itself
            return 1.0 / (1.0 + Math.Exp(-x));
        }
    }
}
