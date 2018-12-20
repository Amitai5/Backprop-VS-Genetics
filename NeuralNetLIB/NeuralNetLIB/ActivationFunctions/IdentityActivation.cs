namespace NeuralNetLIB.ActivationFunctions
{
    public class IdentityActivation : IActivationFunc
    {
        public bool DerrivativeRequiresOutput => false;
        public double Min => double.MinValue;
        public double Max => double.MaxValue;

        public double Derivative(double x)
        {
            return 1;
        }

        public double Derivative2(double y)
        {
            return 1;
        }

        public double Function(double x)
        {
            return x;
        }
    }
}
