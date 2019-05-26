using MachineLearningLIB.ActivationFunctions;
using MachineLearningLIB.InitializationFunctions;
using System;

namespace MachineLearningLIB.NetworkStructure
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
        public ActivationFunc ActivationFunc { get; private set; }
        public InitializationFunction InitializationFunc { get; private set; }

        public NeuralLayer(ActivationFunc activationFunc, InitializationFunction initializationFunction, int inputCount, int neuronCount)
        {
            ActivationFunc = activationFunc;
            Neurons = new Neuron[neuronCount];
            InitializationFunc = initializationFunction;

            //Create The Neurons
            for (int i = 0; i < neuronCount; i++)
            {
                Neurons[i] = new Neuron(activationFunc, initializationFunction, inputCount);
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
        public void Initialize(Random Rand)
        {
            for (int i = 0; i < Neurons.Length; i++)
            {
                Neurons[i].InitializeDendrites(Rand);
            }
        }
    }
}
