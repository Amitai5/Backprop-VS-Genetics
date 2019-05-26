using MachineLearningLIB.NetworkStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineLearningLIB.LearningAlgorithms
{
    public class Backpropagation
    {
        private readonly Dictionary<Neuron, BackpropagationDelta> Deltas;
        public NeuralNetwork Network { get; private set; }
        public double LearningRate { get; private set; }
        public double MomentumRate { get; private set; }
        public long EpochCount { get; private set; }

        public Backpropagation(NeuralNetwork neuralNetwork, double learningRate = 0.2, double momentumRate = 0.0125)
        {
            EpochCount = 0;
            Network = neuralNetwork;
            MomentumRate = momentumRate;
            LearningRate = learningRate.Clamp(0, 1);
            Deltas = new Dictionary<Neuron, BackpropagationDelta>();

            //Create Deltas
            for (int l = 0; l < Network.NeuralLayers.Count; l++)
            {
                NeuralLayer Layer = Network.NeuralLayers[l];
                for (int n = 0; n < Layer.Neurons.Length; n++)
                {
                    Neuron neuron = Layer.Neurons[n];
                    Deltas[neuron] = new BackpropagationDelta(neuron.InputDendrites.Length);
                }
            }
        }

        private void ApplyUpdates()
        {
            foreach (NeuralLayer layer in Network.NeuralLayers)
            {
                foreach (Neuron neuron in layer.Neurons)
                {
                    Parallel.For(0, neuron.InputDendrites.Length, i =>
                    {
                        //Calculate Momentum
                        Deltas[neuron].Momentums[i] = MomentumRate * Deltas[neuron].LastWeightUpdates[i];
                        Deltas[neuron].LastWeightUpdates[i] = Deltas[neuron].WeightUpdates[i];

                        neuron.InputDendrites[i] += Deltas[neuron].WeightUpdates[i] + Deltas[neuron].Momentums[i];
                    });
                    neuron.BiasValue += Deltas[neuron].BiasUpdate;
                }
            }
        }
        private void ClearUpdates()
        {
            foreach (BackpropagationDelta Delta in Deltas.Values)
            {
                Delta.Initialize();
            }
        }

        private void CalculateUpdates(double[] input)
        {
            //Input Layer
            NeuralLayer InputLayer = Network.NeuralLayers[0];
            for (int i = 0; i < InputLayer.Neurons.Length; i++)
            {
                Neuron neuron = InputLayer.Neurons[i];
                for (int j = 0; j < neuron.InputDendrites.Length; j++)
                {
                    Deltas[neuron].WeightUpdates[j] += LearningRate * Deltas[neuron].PartialDerivative * input[j];
                }
                Deltas[neuron].BiasUpdate += LearningRate * Deltas[neuron].PartialDerivative;
            }

            //Hidden Layers
            for (int i = 1; i < Network.NeuralLayers.Count; i++)
            {
                NeuralLayer currLayer = Network.NeuralLayers[i];
                NeuralLayer prevLayer = Network.NeuralLayers[i - 1];

                for (int j = 0; j < currLayer.Neurons.Length; j++)
                {
                    Neuron neuron = currLayer.Neurons[j];
                    for (int k = 0; k < neuron.InputDendrites.Length; k++)
                    {
                        Deltas[neuron].WeightUpdates[k] += LearningRate * Deltas[neuron].PartialDerivative * prevLayer.Outputs[k];
                    }
                    Deltas[neuron].BiasUpdate += LearningRate * Deltas[neuron].PartialDerivative;
                }
            }
        }
        public void CalculateError(double[] desiredOutput)
        {
            //Output Layer
            NeuralLayer OutputLayer = Network.OutputLayer;
            for (int i = 0; i < OutputLayer.Neurons.Length; i++)
            {
                Neuron neuron = OutputLayer.Neurons[i];
                double Error = desiredOutput[i] - neuron.Output;
                Deltas[neuron].PartialDerivative = Error * neuron.ActivationFunc.Derivative(neuron.Input);
            }

            //Hidden Layers
            for (int i = Network.NeuralLayers.Count - 2; i >= 0; i--)
            {
                NeuralLayer CurrentLayer = Network.NeuralLayers[i];
                NeuralLayer NextLayer = Network.NeuralLayers[i + 1];

                for (int j = 0; j < CurrentLayer.Neurons.Length; j++)
                {
                    Neuron neuron = CurrentLayer.Neurons[j];
                    double Error = 0.0;

                    foreach (Neuron nextNeuron in NextLayer.Neurons)
                    {
                        Error += Deltas[nextNeuron].PartialDerivative * nextNeuron.InputDendrites[j];
                    }

                    Deltas[neuron].PartialDerivative = Error * neuron.ActivationFunc.Derivative(neuron.Input);
                }
            }
        }

        protected void Train(double[] input, double[] desiredOutput)
        {
            Network.Compute(input);
            CalculateError(desiredOutput);
            CalculateUpdates(input);
        }
        public double TrainEpoch(double[][] inputs, double[][] desiredOutputs)
        {
            //Increase Epoch Count
            EpochCount++;

            ClearUpdates();
            for (int i = 0; i < inputs.Length; i++)
            {
                Train(inputs[i], desiredOutputs[i]);
            }
            ApplyUpdates();

            //Calculate And Return The Error
            double MeanSquaredError = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                double[] Output = Network.Compute(inputs[i]);
                MeanSquaredError += desiredOutputs[i].Zip(Output, (e, a) => Math.Pow(e - a, 2)).Average();
            }
            MeanSquaredError /= inputs.Length;
            return MeanSquaredError;
        }
    }
}
