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

        public GeneticNeuralNetwork(IActivationFunc activationFunc, int inputCount, params int[] neuronCounts)
            : base(activationFunc, inputCount, neuronCounts)
        {
            Fitness = 0;
        }

        public void CrossOverAndMutate(GeneticNeuralNetwork BetterNetwork, double MutationRate)
        {
            Parallel.For(0, NeuralLayers.Length, i =>
            {
                //Cross Over The Neurons From Each Layer At Given Cut Off Point
                int Flip = Rand.Next(2);
                int CutPoint = Rand.Next(NeuralLayers[i].NeuronLength);
                for (int j = Flip == 0 ? 0 : CutPoint; j < (Flip == 0 ? CutPoint : NeuralLayers[i].NeuronLength); j++)
                {
                    //Get The Neurons
                    Neuron CurrentNeuron = NeuralLayers[i][j];
                    Neuron BetterNetworkNeuron = BetterNetwork.NeuralLayers[i][j];

                    for (int h = 0; h < CurrentNeuron.InputDendrites.Length; h++)
                    {
                        CurrentNeuron.InputDendrites[h] = BetterNetworkNeuron.InputDendrites[h];
                    }
                    CurrentNeuron.BiasValue = (BetterNetworkNeuron.BiasValue + CurrentNeuron.BiasValue) / 2;
                }

                //Mutate The Crossed Over Neurons
                for (int j = 0; j < NeuralLayers[i].NeuronLength; j++)
                {
                    Neuron CurrentNeuron = NeuralLayers[i][j];
                    for (int h = 0; h < CurrentNeuron.InputDendrites.Length; h++)
                    {
                        if (Rand.NextDouble() < MutationRate)
                        {
                            CurrentNeuron.InputDendrites[h] *= Rand.NextDouble(0.5, 1.5);

                            //"Flip A Coin" To See If We Should Flip Sign
                            if (Rand.NextDouble() < MutationRate)
                            {
                                CurrentNeuron.InputDendrites[h] *= -1;
                            }
                        }
                    }

                    //Mutate The Bias
                    if (Rand.NextDouble() < MutationRate)
                    {
                        CurrentNeuron.BiasValue *= Rand.NextDouble(0.5, 1.5);

                        //"Flip A Coin" To See If We Should Flip Sign
                        if (Rand.NextDouble() < MutationRate)
                        {
                            CurrentNeuron.BiasValue *= -1;
                        }
                    }
                }
            });
        }
    }
}
