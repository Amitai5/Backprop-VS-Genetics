using MachineLearningLIB.ActivationFunctions;
using MachineLearningLIB.InitializationFunctions;
using System;
using System.Collections.Generic;

namespace MachineLearningLIB.NetworkStructure
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
        public int LayerCount { get => NeuralLayers.Count + 1; }
        public List<NeuralLayer> NeuralLayers { get; private set; }
        public InitializationFunction InitializationFunc { get; private set; }
        public NeuralLayer OutputLayer => NeuralLayers[NeuralLayers.Count - 1];

        public NeuralNetwork(NeuralNetwork modelNetwork)
        {
            InitializationFunc = modelNetwork.InitializationFunc;
            ExpectedInputCount = modelNetwork.ExpectedInputCount;

            //Copy Layers
            NeuralLayers = new List<NeuralLayer>();
            ActivationFunc activationFunc = modelNetwork.NeuralLayers[0].ActivationFunc;
            NeuralLayers.Add(new NeuralLayer(activationFunc, InitializationFunc, ExpectedInputCount, modelNetwork.NeuralLayers[0].NeuronLength));
            for (int i = 1; i < modelNetwork.NeuralLayers.Count; i++)
            {
                activationFunc = modelNetwork.NeuralLayers[i].ActivationFunc;
                NeuralLayers.Add(new NeuralLayer(activationFunc, InitializationFunc, modelNetwork.NeuralLayers[i - 1].NeuronLength, modelNetwork.NeuralLayers[i].NeuronLength));
            }
        }

        public NeuralNetwork(int inputCount, InitializationFunction initializationFunction)
        {
            ExpectedInputCount = inputCount;
            NeuralLayers = new List<NeuralLayer>();
            InitializationFunc = initializationFunction;
        }

        public NeuralNetwork(ActivationFunc activationFunc, InitializationFunction initializationFunction, int inputCount, params int[] neuronCounts)
        {
            //Create Neural Network
            ExpectedInputCount = inputCount;
            NeuralLayers = new List<NeuralLayer>
            {
                [0] = new NeuralLayer(activationFunc, initializationFunction, inputCount, neuronCounts[0]) //Not Storing The Input Layer
            };
            for (int i = 1; i < neuronCounts.Length; i++)
            {
                NeuralLayers.Add(new NeuralLayer(activationFunc, initializationFunction, NeuralLayers[i - 1].NeuronLength, neuronCounts[i]));
            }
        }

        public double[] Compute(double[] inputs)
        {
            double[] outputs = inputs;
            for (int i = 0; i < NeuralLayers.Count; i++)
            {
                outputs = NeuralLayers[i].Compute(outputs);
            }
            return outputs;
        }

        public void Initialize(Random Rand)
        {
            for (int i = 0; i < NeuralLayers.Count; i++)
            {
                NeuralLayers[i].Initialize(Rand);
            }
        }
    }
}
