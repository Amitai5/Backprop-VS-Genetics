﻿using NeuralNet.ActivationFunctions;

namespace NeuralNet.NetworkStructure
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
        public int LayerCount { get => NeuralLayers.Length; }
        public NeuralLayer[] NeuralLayers { get; private set; }

        public WeightInitializer WeightInit { get; private set; }
        public IActivationFunc ActivationFunc { get; private set; }

        public NeuralLayer OutputLayer => NeuralLayers[NeuralLayers.Length - 1];
        public double[] OutputValues => NeuralLayers[NeuralLayers.Length - 1].Outputs;

        public NeuralNetwork(IActivationFunc activationFunc, InitializerMethod weightInitializer, int inputCount, params int[] neuronCounts)
        {
            ActivationFunc = activationFunc;
            WeightInit = new WeightInitializer(weightInitializer);

            //Create Neural Network
            NeuralLayers = new NeuralLayer[neuronCounts.Length];
            NeuralLayers[0] = new NeuralLayer(ActivationFunc, WeightInit, inputCount, neuronCounts[0]); //Not Storing The Input Layer
            for (int i = 1; i < neuronCounts.Length; i++)
            {
                NeuralLayers[i] = new NeuralLayer(ActivationFunc, WeightInit, NeuralLayers[i - 1].NeuronLength, neuronCounts[i]);
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

        public void Randomize()
        {
            for (int i = 0; i < NeuralLayers.Length; i++)
            {
                NeuralLayers[i].Randomize();
            }
        }
    }
}
