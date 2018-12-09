using NeuralNetLIB.ActivationFunctions;
using NeuralNetLIB.NetworkStructure;
using System;
using System.Threading.Tasks;

namespace NeuralNetLIB.LearningAlgorithms
{
    public class GeneticNeuralNetwork : NeuralNetwork
    {
        //Save The Fitness
        public double Fitness { get; set; }
        private readonly Random Rand = new Random();

        public GeneticNeuralNetwork(IActivationFunc activationFunc, InitializerMethod weightInitializer, int inputCount, params int[] neuronCounts)
            : base(activationFunc, weightInitializer, inputCount, neuronCounts)
        {
            Fitness = 0;
        }

        public void CrossOver(GeneticNeuralNetwork BestNetwork, GeneticNeuralNetwork GoodNetwork)
        {
            Parallel.For(0, NeuralLayers.Length, i =>
             {
                 Parallel.For(0, NeuralLayers[i].NeuronLength, j =>
                 {
                     //Get The Neurons
                     Neuron CurrentNeuron = NeuralLayers[i][j];
                     Neuron BestNetworkNeuron = BestNetwork.NeuralLayers[i][j];
                     Neuron GoodNetworkNeuron = GoodNetwork.NeuralLayers[i][j];

                     for (int h = 0; h < CurrentNeuron.InputDendrites.Length / 2; h++)
                     {
                         CurrentNeuron.InputDendrites[h] = BestNetworkNeuron.InputDendrites[h];
                         CurrentNeuron.InputDendrites[h] += WeightInit.GetInitValue(-6, 6);
                     }

                     for (int k = CurrentNeuron.InputDendrites.Length / 2; k < CurrentNeuron.InputDendrites.Length; k++)
                     {
                         CurrentNeuron.InputDendrites[k] = GoodNetworkNeuron.InputDendrites[k];
                         CurrentNeuron.InputDendrites[k] += WeightInit.GetInitValue(-6, 6);
                     }
                     CurrentNeuron.BiasValue = (BestNetworkNeuron.BiasValue + GoodNetworkNeuron.BiasValue) / 2;
                     CurrentNeuron.BiasValue += WeightInit.GetInitValue(-6, 6);
                 });
             });
        }
    }
}
