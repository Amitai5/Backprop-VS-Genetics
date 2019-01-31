using NeuralNetLIB.ActivationFunctions;
using NeuralNetLIB.LearningAlgorithms;
using NeuralNetLIB.NetworkStructure;
using System;
using System.Collections.Generic;

namespace NeuralNetClassificationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Set Default Appearance Variables
            Console.CursorVisible = false;
            Console.SetWindowSize(100, 50);
            Console.SetBufferSize(100, 50);
            Console.Title = "Neural Net Classification Test";

            Random randy = new Random();
            Dictionary<double[], string> NewData = new Dictionary<double[], string>
            {
                //  Input Format: 
                //      # of Legs, Has Scales, Is Cold Blooded, Water Breathing
                { new double[] { 0, 1, 1, 1 }, "Tuna" },
                { new double[] { 4, 1, 0, 1 }, "Frog" },
                { new double[] { 4, 1, 1, 0 }, "Gecko" },
                { new double[] { 2, 0, 0, 0 }, "Human" },
                { new double[] { 0, 1, 1, 1 }, "Salmon" },
                { new double[] { 5, 0, 1, 1 }, "Starfish" },
                { new double[] { 4, 1, 1, 0 }, "Chameleon" },
            };
            Dictionary<double[], string> TestData = new Dictionary<double[], string>
            {
                //  Input Format: 
                //      # of Legs, Has Scales, Is Cold Blooded, Water Breathing
                { new double[] { 4, 0, 0, 0 }, "Corgi" },
                { new double[] { 2, 0, 0, 0 }, "Seagull" },
                { new double[] { 2, 1, 1, 0 }, "Sea Turtle" },
                { new double[] { 0, 0, 1, 1 }, "Jelly Fish" },
                { new double[] { 8, 0, 1, 0 }, "Black Widow" },
                { new double[] { 4, 1, 1, 0 }, "Komodo Dragon" },
                { new double[] { 0, 1, 1, 0 }, "Burmese Python" },
                { new double[] { 4, 1, 1, 0 }, "Nile Crocodile" },
            };

            double[][] TestDataInputs = new double[TestData.Keys.Count][];
            double[][] TestDataOutputs = new double[][]
            {
                new double[]{ 0 },    //Corgi
                new double[]{ 0 },    //Seagull
                new double[]{ 1 },    //Sea Turtle
                new double[]{ 0 },    //Jelly Fish
                new double[]{ 0 },    //Black Widow
                new double[]{ 1 },    //Komodo Dragon
                new double[]{ 1 },    //Burmese Python
                new double[]{ 1 },    //Nile Crocodile
            };
            TestData.Keys.CopyTo(TestDataInputs, 0);

            NeuralNetwork ModelNetwork = new NeuralNetwork(new Sigmoid(), 4, 10, 1);
            Backpropagation BackpropTrainer = new Backpropagation(ModelNetwork);
            Genetics GeneticsTrainer = new Genetics(randy, ModelNetwork, 25);
            double FinalEpochGenCount = 125;
            Console.CursorVisible = false;

            //Train Both At Least Once
            double BackpropError = BackpropTrainer.TrainEpoch(TestDataInputs, TestDataOutputs);
            GeneticsTrainer.TrainGeneration(TestDataInputs, TestDataOutputs);

            while (BackpropTrainer.EpochCount < FinalEpochGenCount || GeneticsTrainer.GenerationCount < FinalEpochGenCount)
            {
                //Train Neural Networks Only Until It Reaches The Goal Error Amount
                if (BackpropTrainer.EpochCount < FinalEpochGenCount)
                {
                    BackpropError = BackpropTrainer.TrainEpoch(TestDataInputs, TestDataOutputs);
                }
                if (GeneticsTrainer.GenerationCount < FinalEpochGenCount)
                {
                    GeneticsTrainer.TrainGeneration(TestDataInputs, TestDataOutputs);
                }

                //Write Out The Values Of The Backpropagation Neural Network
                Console.SetCursorPosition(0, 0);
                PrintBackpropHeader(BackpropTrainer, BackpropError);

                //Separator
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{Environment.NewLine}-------------------------------------{Environment.NewLine}");

                //Write Out The Values Of The Genetic Neural Network
                PrintGeneticsHeader(GeneticsTrainer);
            }

            //Show Results
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            PrintBackpropHeader(BackpropTrainer, BackpropError);
            Console.Write(Environment.NewLine);

            //Print Out The Results of Backprop
            foreach (KeyValuePair<double[], string> SingleTestData in TestData)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{SingleTestData.Value} => ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{Math.Round(BackpropTrainer.Network.Compute(SingleTestData.Key)[0], 0)}");
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"----------New Data----------");
            foreach (KeyValuePair<double[], string> SingleTestData in NewData)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{SingleTestData.Value} => ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{Math.Round(BackpropTrainer.Network.Compute(SingleTestData.Key)[0], 0)}");
            }

            //Separator
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Environment.NewLine}-------------------------------------{Environment.NewLine}");
            PrintGeneticsHeader(GeneticsTrainer);
            Console.Write(Environment.NewLine);

            //Print Out The Results of Genetics
            foreach (KeyValuePair<double[], string> SingleTestData in TestData)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{SingleTestData.Value} => ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{Math.Round(GeneticsTrainer.BestNetwork.Compute(SingleTestData.Key)[0], 0)}");
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"----------New Data----------");
            foreach (KeyValuePair<double[], string> SingleTestData in NewData)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{SingleTestData.Value} => ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{Math.Round(GeneticsTrainer.BestNetwork.Compute(SingleTestData.Key)[0], 0)}");
            }

            //Allow Me To Read The Data
            Console.ReadLine();
        }

        static void PrintGeneticsHeader(Genetics GeneticsTrainer)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Genetics Results: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Generation Count: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(GeneticsTrainer.GenerationCount);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Fitness: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{GeneticsTrainer.BestNetworkFitness:0.000000000}");
        }
        static void PrintBackpropHeader(Backpropagation BackpropTrainer, double BackpropError)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Backpropagation Results: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Epoch Count: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(BackpropTrainer.EpochCount);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Error: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{BackpropError:0.000000000}");
        }
    }
}
