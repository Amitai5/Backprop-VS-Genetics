namespace NeuralNetLIB.ActivationFunctions
{
    public class LeakyReLU : ActivationFunc
    {
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
