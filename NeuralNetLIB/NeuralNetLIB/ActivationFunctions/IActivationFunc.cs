namespace NeuralNetLIB.ActivationFunctions
{
    public interface IActivationFunc
    {
        double DendriteMinGen { get; }
        double DendriteMaxGen { get; }

        double Function(double x);
        double Derivative(double x);
    }
}
