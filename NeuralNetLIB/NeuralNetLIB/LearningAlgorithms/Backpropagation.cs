using NeuralNetLIB.NetworkStructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetLIB.LearningAlgorithms
{
    public class Backpropagation
    {
        private readonly Dictionary<Neuron, BackpropagationDelta> Deltas;
        public NeuralNetwork Network { get; private set; }
        public double LearningRate { get; private set; }
        public long EpochCount { get; private set; }

        public Backpropagation(NeuralNetwork neuralNetwork, double learningRate = 1.0)
        {
            EpochCount = 0;
            Network = neuralNetwork;
            LearningRate = learningRate.Clamp(0, 1);
            Deltas = new Dictionary<Neuron, BackpropagationDelta>();

            Network.Randomize(new Random());
            for (int l = 0; l < Network.NeuralLayers.Length; l++)
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
                    for (int i = 0; i < neuron.InputDendrites.Length; i++)
                    {
                        neuron.InputDendrites[i] += Deltas[neuron].WeightUpdates[i];
                    }
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

        public void CalculateError(double[] desiredOutput)
        {
            //Output Layer
            NeuralLayer OutputLayer = Network.OutputLayer;
            for (int i = 0; i < OutputLayer.Neurons.Length; i++)
            {
                Neuron neuron = OutputLayer.Neurons[i];
                double Error = desiredOutput[i] - neuron.Output;
                Deltas[neuron].PartialDerivative = Error * neuron.ActivationFunc.Derivative(neuron.Output);
            }

            //Hidden Layers
            for (int i = Network.NeuralLayers.Length - 2; i >= 0; i--)
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

                    Deltas[neuron].PartialDerivative = Error * neuron.ActivationFunc.Derivative(neuron.Output);
                }
            }
        }
        private void CalculateUpdates(double[] input, double learningRate)
        {
            //Input Layer
            NeuralLayer InputLayer = Network.NeuralLayers[0];
            for (int i = 0; i < InputLayer.Neurons.Length; i++)
            {
                Neuron neuron = InputLayer.Neurons[i];
                for (int j = 0; j < neuron.InputDendrites.Length; j++)
                {
                    Deltas[neuron].WeightUpdates[j] += learningRate * Deltas[neuron].PartialDerivative * input[j];
                }
                Deltas[neuron].BiasUpdate += learningRate * Deltas[neuron].PartialDerivative;
            }

            //Hidden Layers
            for (int i = 1; i < Network.NeuralLayers.Length; i++)
            {
                NeuralLayer currLayer = Network.NeuralLayers[i];
                NeuralLayer prevLayer = Network.NeuralLayers[i - 1];

                for (int j = 0; j < currLayer.Neurons.Length; j++)
                {
                    Neuron neuron = currLayer.Neurons[j];
                    for (int k = 0; k < neuron.InputDendrites.Length; k++)
                    {
                        Deltas[neuron].WeightUpdates[k] += learningRate * Deltas[neuron].PartialDerivative * prevLayer.Outputs[k];
                    }
                    Deltas[neuron].BiasUpdate += learningRate * Deltas[neuron].PartialDerivative;
                }
            }
        }

        protected void Train(double[] input, double[] desiredOutput)
        {
            Network.Compute(input);
            CalculateError(desiredOutput);
            CalculateUpdates(input, LearningRate);
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
            double MeanAbsoluteError = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                double[] Output = Network.Compute(inputs[i]);
                MeanAbsoluteError += desiredOutputs[i].Zip(Output, (e, a) => Math.Pow(e - a, 2)).Average();
            }
            MeanAbsoluteError /= inputs.Length;
            return MeanAbsoluteError;
        }
    }
}
