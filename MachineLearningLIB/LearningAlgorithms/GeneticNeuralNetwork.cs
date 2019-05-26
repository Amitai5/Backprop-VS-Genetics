using MachineLearningLIB.ActivationFunctions;
using MachineLearningLIB.NetworkStructure;
using System;
using System.Threading.Tasks;

namespace MachineLearningLIB.LearningAlgorithms
{
    public class GeneticNeuralNetwork : NeuralNetwork
    {
        //Save The Fitness
        public double Fitness { get; set; }

        public GeneticNeuralNetwork(NeuralNetwork neuralNetwork)
            : base(neuralNetwork)
        {
            Fitness = Double.PositiveInfinity;
        }

        public void CrossOverAndMutate(GeneticNeuralNetwork BetterNetwork, double MutationRate, Random Rand)
        {
            Parallel.For(0, NeuralLayers.Count, i =>
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
                    ActivationFunc activationFunc = NeuralLayers[i].ActivationFunc;
                    Neuron CurrentNeuron = NeuralLayers[i][j];
                    for (int h = 0; h < CurrentNeuron.InputDendrites.Length; h++)
                    {
                        if (Rand.NextDouble() < MutationRate)
                        {
                            Mutate(activationFunc, ref CurrentNeuron.InputDendrites[h], Rand);
                        }
                    }

                    //Mutate The Bias
                    if (Rand.NextDouble() < MutationRate)
                    {
                        Mutate(activationFunc, ref CurrentNeuron.BiasValue, Rand);
                    }
                }
            });
        }

        private void Mutate(ActivationFunc activationFunc, ref double weight, Random Rand)
        {
            switch (Rand.Next(4))
            {
                case 0: // randomize
                    weight = Rand.NextDouble(activationFunc.DendriteMinGen, activationFunc.DendriteMaxGen);
                    break;
                case 1: // add/subtract
                    weight += Rand.NextDouble(-1, 1);
                    break;
                case 2: // flip sign
                    weight *= -1;
                    break;
                default:
                case 3: // scale
                    weight *= Rand.NextDouble(0.5, 1.5);
                    break;
            }
        }
    }
}
