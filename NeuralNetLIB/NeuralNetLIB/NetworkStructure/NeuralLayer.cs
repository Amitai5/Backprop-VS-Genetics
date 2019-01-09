using NeuralNetLIB.ActivationFunctions;
using System;

namespace NeuralNetLIB.NetworkStructure
{
    public class NeuralLayer
    {
        public double[] Outputs
        {
            get
            {
                double[] outputs = new double[Neurons.Length];
                for (int i = 0; i < Neurons.Length; i++)
                {
                    outputs[i] = Neurons[i].Output;
                }
                return outputs;
            }
        }
        public Neuron this[int i]
        {
            get
            {
                return Neurons[i];
            }
        }
        public Neuron[] Neurons { get; private set; }
        public int NeuronLength { get => Neurons.Length; }
        public IActivationFunc ActivationFunc { get; private set; }

        public NeuralLayer(IActivationFunc activationFunc, int inputCount, int neuronCount)
        {
            ActivationFunc = activationFunc;

            //Create The Neurons
            Neurons = new Neuron[neuronCount];
            for (int i = 0; i < neuronCount; i++)
            {
                Neurons[i] = new Neuron(ActivationFunc, inputCount);
            }
        }

        public double[] Compute(double[] inputs)
        {
            for (int i = 0; i < Neurons.Length; i++)
            {
                Neurons[i].Compute(inputs);
            }
            return Outputs;
        }

        public void Randomize(Random Rand)
        {
            for (int i = 0; i < Neurons.Length; i++)
            {
                Neurons[i].Randomize(Rand);
            }
        }
    }
}
