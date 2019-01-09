using NeuralNetLIB.ActivationFunctions;
using NeuralNetLIB.NetworkStructure;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NeuralNetLIB.LearningAlgorithms
{
    public class Genetics
    {
        //Store The Neural Net Data
        public IActivationFunc ActivationFunc { get; private set; }
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
        public double MutationRate { get; private set; }
        public long GenerationCount { get; private set; }

        public Genetics(NeuralNetwork modelNetwork, int totalNetCount, double mutationRate = 0.05)
        {
            //Store Neural Network Data
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
                NeuralNets[j] = new GeneticNeuralNetwork(ActivationFunc, 1, neuronCounts);
                NeuralNets[j].Randomize();
            }
        }

        public void TrainGeneration(double[][] inputs, double[][] outputs)
        {
            //Calculate Fitnesses
            Parallel.For(0, NeuralNets.Length, i =>
            {
                CalculateFitness(NeuralNets[i], inputs, outputs);
            });
            NeuralNets = NeuralNets.OrderBy(x => x.Fitness).ToArray();

            //Cross Over 80% Of Nets & Randomize 10%
            int OneTenthPopulation = NeuralNets.Length / 10;
            Parallel.For(OneTenthPopulation, NeuralNets.Length, j =>
            {
                GeneticNeuralNetwork CurrentNet = NeuralNets[j];
                if (j < 9 * OneTenthPopulation)
                {
                    CurrentNet.CrossOverAndMutate(NeuralNets[j % 10], MutationRate);
                }
                else
                {
                    CurrentNet.Randomize();
                }
            });

            //Calculate Fitnesses & Sort
            Parallel.For(0, NeuralNets.Length, i =>
            {
                CalculateFitness(NeuralNets[i], inputs, outputs);
            });
            NeuralNets = NeuralNets.OrderBy(x => x.Fitness).ToArray();
            BestNetwork = NeuralNets[0];
            GenerationCount++;
        }

        private void CalculateFitness(GeneticNeuralNetwork neuralNetwork, double[][] inputs, double[][] desiredOutputs)
        {
            double MeanAbsoluteError = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                double[] Output = neuralNetwork.Compute(inputs[i]);
                MeanAbsoluteError += Math.Abs(desiredOutputs[i][0] - Output[0]);
            }
            neuralNetwork.Fitness = MeanAbsoluteError /= inputs.Length;
        }
    }
}
