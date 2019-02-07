namespace NeuralNetLIB.ActivationFunctions
{
    public class IdentityActivation : ActivationFunc
    {
        public double Derivative(double x)
        {
            return 1;
        }

        public double Function(double x)
        {
            return x;
        }
    }
}
