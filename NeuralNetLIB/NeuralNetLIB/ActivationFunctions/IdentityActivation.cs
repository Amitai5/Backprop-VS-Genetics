namespace NeuralNetLIB.ActivationFunctions
{
    public class IdentityActivation : IActivationFunc
    {
        public bool DerrivativeRequiresOutput => false;
        public double DendriteMinGen => -0.5;
        public double DendriteMaxGen => 0.5;

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
