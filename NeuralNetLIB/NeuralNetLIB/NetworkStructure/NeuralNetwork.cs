using NeuralNetLIB.ActivationFunctions;
using System;

namespace NeuralNetLIB.NetworkStructure
{
    public class NeuralNetwork
    {
        public NeuralLayer this[int i]
        {
            get
            {
                return NeuralLayers[i];
            }
        }
        public int ExpectedInputCount { get; private set; }
        public int LayerCount { get => NeuralLayers.Length; }
        public NeuralLayer[] NeuralLayers { get; private set; }
        public ActivationFunc ActivationFunc { get; private set; }
        public NeuralLayer OutputLayer => NeuralLayers[NeuralLayers.Length - 1];

        public NeuralNetwork(ActivationFunc activationFunc, int inputCount, params int[] neuronCounts)
        {
            //Create Neural Network
            ActivationFunc = activationFunc;
            ExpectedInputCount = inputCount;
            NeuralLayers = new NeuralLayer[neuronCounts.Length];
            NeuralLayers[0] = new NeuralLayer(ActivationFunc, inputCount, neuronCounts[0]); //Not Storing The Input Layer
            for (int i = 1; i < neuronCounts.Length; i++)
            {
                NeuralLayers[i] = new NeuralLayer(ActivationFunc, NeuralLayers[i - 1].NeuronLength, neuronCounts[i]);
            }
        }

        public double[] Compute(double[] inputs)
        {
            double[] outputs = inputs;
            for (int i = 0; i < NeuralLayers.Length; i++)
            {
                outputs = NeuralLayers[i].Compute(outputs);
            }
            return outputs;
        }

        public void Randomize(Random Rand)
        {
            for (int i = 0; i < NeuralLayers.Length; i++)
            {
                NeuralLayers[i].Randomize(Rand);
            }
        }
    }
}
