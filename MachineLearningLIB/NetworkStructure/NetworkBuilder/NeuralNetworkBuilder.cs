using MachineLearningLIB.ActivationFunctions;
using MachineLearningLIB.InitializationFunctions;
using System;

namespace MachineLearningLIB.NetworkStructure.NetworkBuilder
{
    public class NeuralNetworkBuilder : INeuralNetworkStartBuild, INeuralNetworkBuildLayers, INeuralNetworkFinalBuild
    {
        private readonly InitializationFunction InitializationFunction;
        private NeuralNetwork BuildingNet;
        private int InputCount = 0;

        public NeuralNetwork Build(Random rand)
        {
            BuildingNet.Initialize(rand);
            return BuildingNet;
        }

        public NeuralNetwork[] BuildMany(Random rand, int networkCount)
        {
            NeuralNetwork[] neuralNetworks = new NeuralNetwork[networkCount];
            for (int i = 0; i < networkCount; i++)
            {
                neuralNetworks[i] = new NeuralNetwork(BuildingNet);
            }
            return neuralNetworks;
        }

        public NeuralNetworkBuilder(InitializationFunction initializationFunc)
        {
            InitializationFunction = initializationFunc;
        }

        public INeuralNetworkBuildLayers CreateInputLayer(int inputCount)
        {
            BuildingNet = new NeuralNetwork(inputCount, InitializationFunction);
            InputCount = inputCount;
            return this;
        }

        public INeuralNetworkBuildLayers AddHiddenLayer(int neuronCount, ActivationFunc activationFunc)
        {
            int previousLayerNeuronCount = BuildingNet.NeuralLayers.Count == 0 ? InputCount : BuildingNet.NeuralLayers[BuildingNet.NeuralLayers.Count - 1].NeuronLength;
            BuildingNet.NeuralLayers.Add(new NeuralLayer(activationFunc, InitializationFunction, previousLayerNeuronCount, neuronCount));
            return this;
        }

        public INeuralNetworkFinalBuild CreateOutputLayer(int neuronCount, ActivationFunc activationFunc)
        {
            int previousLayerNeuronCount = BuildingNet.NeuralLayers.Count == 0 ? InputCount : BuildingNet.NeuralLayers[BuildingNet.NeuralLayers.Count - 1].NeuronLength;
            BuildingNet.NeuralLayers.Add(new NeuralLayer(activationFunc, InitializationFunction, previousLayerNeuronCount, neuronCount));
            return this;
        }
    }
}
