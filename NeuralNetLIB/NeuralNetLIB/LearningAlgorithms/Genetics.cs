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
        public InitializerMethod InitMethod { get; private set; }
        public NeuralNetwork BestNetwork { get; private set; }
        private GeneticNeuralNetwork[] NeuralNets;

        //Store The Training Data
        public long GenerationCount { get; private set; }
        public double LearningRate { get; private set; }

        public Genetics(NeuralNetwork modelNetwork, int totalNetCount, double learningRate = 0.5)
        {
            //Store Neural Network Data
            LearningRate = learningRate;
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
                NeuralNets[j] = new GeneticNeuralNetwork(ActivationFunc, InitMethod, 1, neuronCounts);
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

            //Cross Over 33% Of Nets & Randomize The Other 33%
            int OneThirdPopulation = NeuralNets.Length / 3;
            Parallel.For(OneThirdPopulation, NeuralNets.Length, j =>
            {
                GeneticNeuralNetwork CurrentNet = NeuralNets[j];
                if (j < 2 * OneThirdPopulation)
                {
                    CurrentNet.CrossOver(NeuralNets[j - NeuralNets.Length / 3], LearningRate);
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
                MeanAbsoluteError += Math.Pow(desiredOutputs[i][0] - Output[0], 2);
            }
            neuralNetwork.Fitness = MeanAbsoluteError /= inputs.Length;
        }
    }
}
