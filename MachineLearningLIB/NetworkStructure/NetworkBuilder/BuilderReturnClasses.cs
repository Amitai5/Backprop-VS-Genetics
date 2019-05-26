using MachineLearningLIB.ActivationFunctions;
using System;

namespace MachineLearningLIB.NetworkStructure.NetworkBuilder
{
    public interface INeuralNetworkStartBuild
    {
        INeuralNetworkBuildLayers CreateInputLayer(int inputCount);
    }

    public interface INeuralNetworkBuildLayers
    {
        INeuralNetworkFinalBuild CreateOutputLayer(int neuronCount, ActivationFunc activationFunc);
        INeuralNetworkBuildLayers AddHiddenLayer(int neuronCount, ActivationFunc activationFunc);
    }

    public interface INeuralNetworkFinalBuild
    {
        NeuralNetwork[] BuildMany(Random rand, int networkCount);
        NeuralNetwork Build(Random rand);
    }
}
