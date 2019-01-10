namespace NeuralNetLIB.ActivationFunctions
{
    public interface IActivationFunc
    {
        double DendriteMinGen { get; }
        double DendriteMaxGen { get; }

        double Function(double x);
        double Derivative(double x);
        double Derivative2(double y); //Expects The Output Of A Neuron
        bool DerrivativeRequiresOutput { get; }
    }
}
