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
            Random randy = new Random();
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
            Backpropagation BackpropTrainer = new Backpropagation(ModelNetwork, 0.35);
            Genetics GeneticsTrainer = new Genetics(randy, ModelNetwork, 100);
            double NeuralNetworkTargetError = 0.05;
            Console.CursorVisible = false;

            //Train Both At Least Once
            double BackpropError = BackpropTrainer.TrainEpoch(TestDataInputs, TestDataOutputs);
            GeneticsTrainer.TrainGeneration(TestDataInputs, TestDataOutputs);

            while (true)
            {
                //Train Neural Networks Only Until It Reaches The Goal Error Amount
                if (BackpropError > NeuralNetworkTargetError)
                {
                    BackpropError = BackpropTrainer.TrainEpoch(TestDataInputs, TestDataOutputs);
                }
                if (GeneticsTrainer.BestNetworkFitness > NeuralNetworkTargetError)
                {
                    GeneticsTrainer.TrainGeneration(TestDataInputs, TestDataOutputs);
                }

                //Write Out The Values Of The Backpropagation Neural Network
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Backpropagation Results: ");
                Console.WriteLine($"Epoch Count: {BackpropTrainer.EpochCount}{Environment.NewLine}");
                WriteNeuralNetSingleValue(BackpropTrainer.Network, new double[] { 1, 1 });
                WriteNeuralNetSingleValue(BackpropTrainer.Network, new double[] { 0, 1 });
                WriteNeuralNetSingleValue(BackpropTrainer.Network, new double[] { 1, 0 });
                WriteNeuralNetSingleValue(BackpropTrainer.Network, new double[] { 0, 0 });
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Error: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{BackpropError:0.000000000}");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{Environment.NewLine}-------------------------------------{Environment.NewLine}");

                //Write Out The Values Of The Genetics Neural Network
                Console.WriteLine($"Genetics Results: ");
                Console.WriteLine($"Generation Count: {GeneticsTrainer.GenerationCount}{Environment.NewLine}");
                WriteNeuralNetSingleValue(GeneticsTrainer.BestNetwork, new double[] { 1, 1 });
                WriteNeuralNetSingleValue(GeneticsTrainer.BestNetwork, new double[] { 0, 1 });
                WriteNeuralNetSingleValue(GeneticsTrainer.BestNetwork, new double[] { 1, 0 });
                WriteNeuralNetSingleValue(GeneticsTrainer.BestNetwork, new double[] { 0, 0 });
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Error: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{GeneticsTrainer.BestNetworkFitness:0.000000000}");
            }
        }

        static void WriteNeuralNetSingleValue(NeuralNetwork network, double[] SingleInputValues)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"({SingleInputValues[0]}, {SingleInputValues[1]}) => ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{network.Compute(SingleInputValues)[0]:0.00000}");
        }
    }
}
