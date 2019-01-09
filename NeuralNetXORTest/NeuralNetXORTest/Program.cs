using NeuralNetLIB.ActivationFunctions;
using NeuralNetLIB.LearningAlgorithms;
using NeuralNetLIB.NetworkStructure;
using System;

namespace NeuralNetXORTest
{
    class Program
    {
        static void Main(string[] args)
        {
            double[][] TestDataInputs = new double[][]
            {
                new double[]{ 0, 0 },
                new double[]{ 1, 0 },
                new double[]{ 0, 1 },
                new double[]{ 1, 1 }
            };

            double[][] TestDataOutputs = new double[][]
            {
                new double[]{ 0 },
                new double[]{ 1 },
                new double[]{ 1 },
                new double[]{ 0 }
            };

            NeuralNetwork ModelNetwork = new NeuralNetwork(new Sigmoid(), 2, 2, 1);
            Backpropagation BackpropTrainer = new Backpropagation(ModelNetwork, 1);
            Genetics GeneticsTrainer = new Genetics(ModelNetwork, 100, 0.05);
            double NeuralNetworkTargetError = 0.05;
            Console.CursorVisible = false;
            double BackpropError = 1;

            do
            {
                //Train Neural Networks
                BackpropError = BackpropTrainer.TrainEpoch(TestDataInputs, TestDataOutputs);
                GeneticsTrainer.TrainGeneration(TestDataInputs, TestDataOutputs);

                //Write Out The Values Of The Backpropagation Neural Network
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Backpropagation Results: {Environment.NewLine}");
                WriteNeuralNetSingleValue(BackpropTrainer.Network, new double[] { 1, 1 });
                WriteNeuralNetSingleValue(BackpropTrainer.Network, new double[] { 0, 1 });
                WriteNeuralNetSingleValue(BackpropTrainer.Network, new double[] { 1, 0 });
                WriteNeuralNetSingleValue(BackpropTrainer.Network, new double[] { 0, 0 });
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Error: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{BackpropError}{Environment.NewLine}");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"-------------------------------------{Environment.NewLine}");

                //Write Out The Values Of The Genetics Neural Network
                Console.WriteLine($"Genetics Results: {Environment.NewLine}");
                WriteNeuralNetSingleValue(GeneticsTrainer.BestNetwork, new double[] { 1, 1 });
                WriteNeuralNetSingleValue(GeneticsTrainer.BestNetwork, new double[] { 0, 1 });
                WriteNeuralNetSingleValue(GeneticsTrainer.BestNetwork, new double[] { 1, 0 });
                WriteNeuralNetSingleValue(GeneticsTrainer.BestNetwork, new double[] { 0, 0 });
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Error: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(GeneticsTrainer.BestNetwork.Fitness);
            } while (BackpropError > NeuralNetworkTargetError && GeneticsTrainer.BestNetwork.Fitness > NeuralNetworkTargetError);

            Console.ReadKey();
        }

        static void WriteNeuralNetSingleValue(NeuralNetwork network, double[] SingleInputValues)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"({SingleInputValues[0]}, {SingleInputValues[1]}): ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(network.Compute(SingleInputValues)[0]);
        }
    }
}
