using NeuralNetLIB.ActivationFunctions;
using NeuralNetLIB.NetworkStructure;
using System;
using System.Threading.Tasks;

namespace NeuralNetLIB.LearningAlgorithms
{
    public class Genetics
    {
        //Store The Neural Net Data
        public ActivationFunc ActivationFunc { get; private set; }
        public NeuralNetwork BestNetwork { get; private set; }
        private GeneticNeuralNetwork[] NeuralNets;
        public double BestNetworkFitness
        {
            get
            {
                return NeuralNets[0].Fitness;
            }
        }

        //Store The Training Data
        public Random Rand { get; private set; }
        public double MutationRate { get; private set; }
        public long GenerationCount { get; private set; }

        public Genetics(Random rand, NeuralNetwork modelNetwork, int totalNetCount, double mutationRate = 0.05)
        {
            //Store Neural Network Data
            Rand = rand;
            MutationRate = mutationRate;
            ActivationFunc = modelNetwork.ActivationFunc;

            //Get Network Layer Neuron Counts
            int[] neuronCounts = new int[modelNetwork.NeuralLayers.Length];
            for (int i = 0; i < modelNetwork.NeuralLayers.Length; i++)
            {
                neuronCounts[i] = modelNetwork.NeuralLayers[i].NeuronLength;
            }

            //Create Neural Nets
            NeuralNets = new GeneticNeuralNetwork[totalNetCount];
            for (int j = 0; j < totalNetCount; j++)
            {
                NeuralNets[j] = new GeneticNeuralNetwork(ActivationFunc, modelNetwork.ExpectedInputCount, neuronCounts);
                NeuralNets[j].Randomize(rand);
            }
        }

        public void TrainGeneration(double[][] inputs, double[][] outputs)
        {
            //Cross Over 80% Of Nets & Randomize 10%
            int OneTenthPopulation = NeuralNets.Length / 10;
            for (int i = OneTenthPopulation; i < NeuralNets.Length; i++)
            {
                GeneticNeuralNetwork CurrentNet = NeuralNets[i];
                if (i < 9 * OneTenthPopulation)
                {
                    CurrentNet.CrossOverAndMutate(NeuralNets[i % OneTenthPopulation], MutationRate, Rand);
                }
                else
                {
                    CurrentNet.Randomize(Rand);
                }
            }

            //Calculate Fitnesses & Sort
            Parallel.For(0, NeuralNets.Length, j =>
            {
                CalculateFitness(NeuralNets[j], inputs, outputs);
            });
            Array.Sort(NeuralNets, (a, b) => a.Fitness.CompareTo(b.Fitness));
            BestNetwork = NeuralNets[0];
            GenerationCount++;
        }

        private void CalculateFitness(GeneticNeuralNetwork neuralNetwork, double[][] inputs, double[][] desiredOutputs)
        {
            double MeanAbsoluteError = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                double Output = neuralNetwork.Compute(inputs[i])[0];
                MeanAbsoluteError += Math.Pow(desiredOutputs[i][0] - Output, 2);
            }
            neuralNetwork.Fitness = MeanAbsoluteError / inputs.Length;
        }
    }
}
