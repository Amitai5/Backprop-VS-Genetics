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
            //Create Neural Nets
            Random randy = new Random();
            NeuralNetwork ModelNetwork = new NeuralNetwork(new Sigmoid(), 2, 2, 1);
            Backpropagation BackpropTrainer = new Backpropagation(randy, ModelNetwork);
            Genetics GeneticsTrainer = new Genetics(randy, ModelNetwork, 500);

            //Neural Net Variables
            double NeuralNetworkTargetError = 0.05;
            double BackpropError = 0;

            //Set Test Data
            double[][] TestDataOutputs = new double[][]
            {
                new double[]{ 0 },
                new double[]{ 1 },
                new double[]{ 1 },
                new double[]{ 0 }
            };
            double[][] TestDataInputs = new double[][]
            {
                new double[]{ 0, 0 },
                new double[]{ 1, 0 },
                new double[]{ 0, 1 },
                new double[]{ 1, 1 }
            };

            //Create Timers
            TimeSpan GeneticsLearnTime = new TimeSpan();
            TimeSpan BackpropLearnTime = new TimeSpan();
            Console.CursorVisible = false;

            //Train Both At Least Once
            BackpropError = BackpropTrainer.TrainEpoch(TestDataInputs, TestDataOutputs);
            GeneticsTrainer.TrainGeneration(TestDataInputs, TestDataOutputs);

            while (true)
            {
                //Train Backprop Neural Network
                if (BackpropError > NeuralNetworkTargetError)
                {
                    DateTime StartTime = DateTime.Now;
                    BackpropError = BackpropTrainer.TrainEpoch(TestDataInputs, TestDataOutputs);
                    BackpropLearnTime += DateTime.Now - StartTime;
                }

                //Train Genetics Neural Network
                if (GeneticsTrainer.BestNetworkFitness > NeuralNetworkTargetError)
                {
                    DateTime StartTime = DateTime.Now;
                    GeneticsTrainer.TrainGeneration(TestDataInputs, TestDataOutputs);
                    GeneticsLearnTime += DateTime.Now - StartTime;
                }

                //Write Out The Values Of The Backpropagation Neural Network
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Backpropagation Results: ");
                Console.WriteLine($"Epoch Count: {BackpropTrainer.EpochCount}");
                Console.WriteLine($"Learning Time: {BackpropLearnTime.TotalMilliseconds}{Environment.NewLine}");
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
                Console.WriteLine($"Generation Count: {GeneticsTrainer.GenerationCount}");
                Console.WriteLine($"Learning Time: {GeneticsLearnTime.TotalMilliseconds}{Environment.NewLine}");
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
