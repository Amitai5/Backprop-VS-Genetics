using NeuralNetLIB.ActivationFunctions;
using System;
using System.Diagnostics;

namespace NeuralNetLIB.NetworkStructure
{
    public class Neuron
    {
        public double BiasValue;
        public double Output { get; private set; }
        public double Input { get; private set; }
        public double[] InputDendrites { get; private set; }
        public ActivationFunc ActivationFunc { get; private set; }

        public Neuron(ActivationFunc activationFunc, int inputCount)
        {
            ActivationFunc = activationFunc;
            InputDendrites = new double[inputCount];
        }

        [Conditional("DEBUG")]
        private void CheckInputLength(double[] inputs)
        {
            if (inputs.Length != InputDendrites.Length)
            {
                throw new ArgumentException();
            }
        }
        public double Compute(double[] inputs)
        {
            //Will Only Run When In Debug Mode
            CheckInputLength(inputs);

            double output = 0;
            for (int i = 0; i < InputDendrites.Length; i++)
            {
                output += inputs[i] * InputDendrites[i];
            }
            Input = output + BiasValue;

            //Run It Through The Activation Function
            Output = ActivationFunc.Function(Input);
            return Output;
        }

        public void Randomize(Random Rand)
        {
            BiasValue = Rand.NextDouble(ActivationFunc.DendriteMinGen, ActivationFunc.DendriteMaxGen);

            //Check Before We Randomize
            if (InputDendrites != null && InputDendrites.Length > 0)
            {
                for (int i = 0; i < InputDendrites.Length; i++)
                {
                    InputDendrites[i] = Rand.NextDouble(ActivationFunc.DendriteMinGen, ActivationFunc.DendriteMaxGen);
                }
            }
        }
    }
}
