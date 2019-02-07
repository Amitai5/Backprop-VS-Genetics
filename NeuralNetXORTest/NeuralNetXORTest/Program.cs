using NeuralNetLIB.ActivationFunctions;
using NeuralNetLIB.LearningAlgorithms;
using NeuralNetLIB.NetworkStructure;
using System;
using System.ComponentModel;

namespace NeuralNetXORTest
{
    class Program
    {
        //Create Neural Nets
        static readonly NeuralNetwork ModelNetwork = new NeuralNetwork(new Sigmoid(), 2, 2, 1);
        static Backpropagation BackpropTrainer = new Backpropagation(ModelNetwork);
        static Genetics GeneticsTrainer = new Genetics(randy, ModelNetwork, 25);
        static readonly Random randy = new Random();

        //Neural Net Variables
        static readonly double NeuralNetworkTargetError = 0.01;
        static double BackpropError = 0;

        static void Main(string[] args)
        {
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
            DateTime StartTime = DateTime.Now;
            Console.CursorVisible = false;

            //Train Both At Least Once
            BackpropError = BackpropTrainer.TrainEpoch(TestDataInputs, TestDataOutputs);
            GeneticsTrainer.TrainGeneration(TestDataInputs, TestDataOutputs);

            //Start Background Workers
            BackgroundWorker GeneticsWorker = new BackgroundWorker();
            BackgroundWorker BackpropWorker = new BackgroundWorker();
            GeneticsWorker.DoWork += GeneticsWorker_DoWork;
            BackpropWorker.DoWork += BackpropWorker_DoWork;
            GeneticsWorker.RunWorkerAsync();
            GeneticsWorker.RunWorkerAsync();

            while (true)
            {
                //Train Neural Networks Only Until It Reaches The Goal Error Amount
                if (BackpropError > NeuralNetworkTargetError)
                {
                    BackpropLearnTime = DateTime.Now - StartTime;
                }
                if (GeneticsTrainer.BestNetworkFitness > NeuralNetworkTargetError)
                {
                    GeneticsLearnTime = DateTime.Now - StartTime;
                }

                //Write Out The Values Of The Backpropagation Neural Network
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Backpropagation Results: ");
                Console.WriteLine($"Learning Time: {BackpropLearnTime}{Environment.NewLine}");
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
                Console.WriteLine($"Learning Time: {GeneticsLearnTime}{Environment.NewLine}");
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

        private static void BackpropWorker_DoWork(object sender, DoWorkEventArgs e)
        {
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

            //Train The Nets
            while (BackpropError > NeuralNetworkTargetError)
            {
                BackpropError = BackpropTrainer.TrainEpoch(TestDataInputs, TestDataOutputs);
            }
        }
        private static void GeneticsWorker_DoWork(object sender, DoWorkEventArgs e)
        {
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

            //Train The Nets
            while (GeneticsTrainer.BestNetworkFitness > NeuralNetworkTargetError)
            {
                GeneticsTrainer.TrainGeneration(TestDataInputs, TestDataOutputs);
            }
        }
    }
}
