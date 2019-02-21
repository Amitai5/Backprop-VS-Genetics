namespace NeuralNetLIB.ActivationFunctions
{
    public class IdentityActivation : ActivationFunc
    {
        public override double Derivative(double x)
        {
            return 1;
        }

        public override double Function(double x)
        {
            return x;
        }
    }
}
