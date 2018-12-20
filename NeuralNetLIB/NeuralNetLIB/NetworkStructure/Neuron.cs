using NeuralNetLIB.ActivationFunctions;
using System;

namespace NeuralNetLIB.NetworkStructure
{
    public class Neuron
    {
        public double BiasValue;
        public double Output { get; private set; }
        public double[] InputDendrites { get; private set; }

        private readonly Random Rand = new Random();
        public IActivationFunc ActivationFunc { get; private set; }

        public Neuron(IActivationFunc activationFunc, int inputCount)
        {
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
            BiasValue = Rand.NextDouble(ActivationFunc.Min, ActivationFunc.Max);

            //Check Before We Randomize
            if (InputDendrites != null && InputDendrites.Length > 0)
            {
                for (int i = 0; i < InputDendrites.Length; i++)
                {
                    InputDendrites[i] = Rand.NextDouble(ActivationFunc.Min, ActivationFunc.Max);
                }
            }
        }
    }
}
