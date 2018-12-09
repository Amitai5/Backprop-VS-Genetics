using NeuralNet.ActivationFunctions;

namespace NeuralNet.NetworkStructure
{
    public class Neuron
    {
        public double BiasValue;
        public double Output { get; private set; }
        public double[] InputDendrites { get; private set; }

        public WeightInitializer WeightInit { get; private set; }
        public IActivationFunc ActivationFunc { get; private set; }

        public Neuron(IActivationFunc activationFunc, WeightInitializer weightInitializer, int inputCount)
        {
            WeightInit = weightInitializer;
            ActivationFunc = activationFunc;
            InputDendrites = new double[inputCount];
        }

        public double Compute(double[] inputs)
        {
            double output = 0;
            for (int i = 0; i < InputDendrites.Length; i++) //Parallel For-Loop (Multi-Threadable)
            {
                output += inputs[i] * InputDendrites[i];
            }
            output = ActivationFunc.Function(output + BiasValue);
            Output = output;
            return output;
        }

        public void Randomize()
        {
            BiasValue = WeightInit.GetInitValue(ActivationFunc.Min, ActivationFunc.Max);

            //Check Before We Randomize
            if (InputDendrites != null && InputDendrites.Length > 0)
            {
                for (int i = 0; i < InputDendrites.Length; i++)
                {
                    InputDendrites[i] = WeightInit.GetInitValue(ActivationFunc.Min, ActivationFunc.Max);
                }
            }
        }
    }
}
