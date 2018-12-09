using NeuralNet.ActivationFunctions;
using NeuralNet.NetworkStructure;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NeuralNet.LearningAlgorithms
{
    public class Genetics
    {
        //Store The Neural Net Data
        public IActivationFunc ActivationFunc { get; private set; }
        public InitializerMethod InitMethod { get; private set; }
        public NeuralNetwork BestNetwork { get; private set; }
        private GeneticNeuralNetwork[] NeuralNets;

        //Store The Training Data
        public long GenerationCount { get; private set; }
        public double LearningRate { get; private set; }

        public Genetics(NeuralNetwork modelNetwork, int totalNetCount)
        {
            //Store Neural Network Data
            ActivationFunc = modelNetwork.ActivationFunc;
            InitMethod = modelNetwork.WeightInit.InitMethod;

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
                GeneticNeuralNetwork NewNetwork = new GeneticNeuralNetwork(ActivationFunc, InitMethod, 1, neuronCounts);
                NewNetwork.Randomize();
                NeuralNets[j] = NewNetwork;
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

            //Create 75% New Networks
            int QuarterMark = NeuralNets.Length / 4;
            Parallel.For(QuarterMark, NeuralNets.Length - QuarterMark, j =>
            {
                GeneticNeuralNetwork NextBestNetwork = NeuralNets[j - QuarterMark + 1];
                GeneticNeuralNetwork BetterNetwork = NeuralNets[j - QuarterMark];
                GeneticNeuralNetwork CurrentNet = NeuralNets[j];

                CurrentNet.CrossOver(BetterNetwork, NextBestNetwork);
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
                MeanAbsoluteError += Math.Pow(desiredOutputs[i][0] - Output[0], 2);
            }
            neuralNetwork.Fitness = MeanAbsoluteError /= inputs.Length;
        }
    }
}
