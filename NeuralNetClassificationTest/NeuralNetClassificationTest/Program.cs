﻿using NeuralNetLIB.ActivationFunctions;
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
            Console.SetBufferSize(100, 60);
            Console.Title = "Neural Net Classification Test";

            while (true)
            {
                //Ask For Amount Of Epocs/Generations
                Random randy = new Random(26);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Train Time (in milliseconds): ");
                Console.ForegroundColor = ConsoleColor.White;
                int Milliseconds = int.Parse(Console.ReadLine());

                Dictionary<double[], string> NewData = new Dictionary<double[], string>
                {
                    //  Input Format: 
                    //      # of Legs, Has Scales, Is Cold Blooded, Water Breathing, Has Fins
                    { new double[] { 4, 0, 1, 1, 0 }, "Frog" },
                    { new double[] { 4, 0, 0, 0, 0 }, "Corgi" },
                    { new double[] { 4, 0, 0, 0, 0 }, "Tiger" },
                    { new double[] { 4, 0, 0, 0, 0 }, "Stoat" },
                    { new double[] { 4, 1, 1, 0, 0 }, "Gecko" },
                    { new double[] { 2, 0, 0, 0, 0 }, "Human" },
                    { new double[] { 0, 1, 1, 1, 1 }, "Salmon" },
                    { new double[] { 0, 0, 0, 0, 1 }, "Dolphin" },
                    { new double[] { 5, 0, 1, 1, 0 }, "Starfish" },
                    { new double[] { 4, 1, 1, 0, 0 }, "Chameleon" },
                    { new double[] { 2, 1, 1, 0, 0 }, "Sea Turtle" },
                    { new double[] { 8, 0, 1, 0, 0 }, "Black Widow" },
                    { new double[] { 4, 1, 1, 0, 0 }, "Komodo Dragon" }
                };
                double[] NewDataCorrectOutputs = new double[]
                {
                    0,    //Frog
                    0,    //Corgi
                    0,    //Tiger
                    0,    //Stoat
                    1,    //Gecko
                    0,    //Human
                    0,    //Salmon
                    0,    //Dolphin
                    0,    //Starfish
                    1,    //Chameleon
                    1,    //Sea Turtle
                    0,    //Black Widow
                    1     //Komodo Dragon
                };

                Dictionary<double[], string> TestData = new Dictionary<double[], string>
                {
                    //  Input Format: 
                    //      # of Legs, Has Scales, Is Cold Blooded, Water Breathing, Has Fins
                    { new double[] { 0, 1, 1, 1, 1 }, "Tuna" },
                    { new double[] { 2, 0, 0, 0, 0 }, "Seagull" },
                    { new double[] { 0, 0, 1, 1, 0 }, "Jelly Fish" },
                    { new double[] { 0, 1, 1, 0, 0 }, "Burmese Python" },
                    { new double[] { 4, 1, 1, 0, 0 }, "Nile Crocodile" },
                };
                double[][] TestDataInputs = new double[TestData.Keys.Count][];
                double[][] TestDataOutputs = new double[][]
                {
                    new double[]{ 0 },    //Tuna
                    new double[]{ 0 },    //Seagull
                    new double[]{ 0 },    //Jelly Fish
                    new double[]{ 1 },    //Burmese Python
                    new double[]{ 1 },    //Nile Crocodile
                };
                TestData.Keys.CopyTo(TestDataInputs, 0);

                NeuralNetwork ModelNetwork = new NeuralNetwork(new Sigmoid(), 5, 1);
                Backpropagation BackpropTrainer = new Backpropagation(randy, ModelNetwork, 0.035);
                Genetics GeneticsTrainer = new Genetics(randy, ModelNetwork, 250);
                Console.CursorVisible = false;
                Console.Clear();

                //Create Timers
                TimeSpan GeneticsLearnTime = new TimeSpan();
                TimeSpan BackpropLearnTime = new TimeSpan();

                //Train Both At Least Once
                double BackpropError = BackpropTrainer.TrainEpoch(TestDataInputs, TestDataOutputs);
                GeneticsTrainer.TrainGeneration(TestDataInputs, TestDataOutputs);

                while (BackpropLearnTime.TotalMilliseconds < Milliseconds || GeneticsLearnTime.TotalMilliseconds < Milliseconds)
                {
                    //Train Neural Networks Only Until It Reaches The Time Limit
                    if (BackpropLearnTime.TotalMilliseconds < Milliseconds)
                    {
                        DateTime StartTime = DateTime.Now;
                        BackpropError = BackpropTrainer.TrainEpoch(TestDataInputs, TestDataOutputs);
                        BackpropLearnTime += DateTime.Now - StartTime;
                    }
                    if (GeneticsLearnTime.TotalMilliseconds < Milliseconds)
                    {
                        DateTime StartTime = DateTime.Now;
                        GeneticsTrainer.TrainGeneration(TestDataInputs, TestDataOutputs);
                        GeneticsLearnTime += DateTime.Now - StartTime;
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

                //Print Backprop New Data
                int BackpropQuestionIndex = 0;
                int BackpropCorrectQuestions = 0;
                foreach (KeyValuePair<double[], string> SingleTestData in NewData)
                {
                    int NeuralNetComputedValue = (int)Math.Round(BackpropTrainer.Network.Compute(SingleTestData.Key)[0], 0);
                    bool CorrectAnswer = NeuralNetComputedValue == NewDataCorrectOutputs[BackpropQuestionIndex];
                    Console.ForegroundColor = CorrectAnswer ? ConsoleColor.White : ConsoleColor.Red;
                    BackpropCorrectQuestions += CorrectAnswer ? 1 : 0;
                    Console.Write($"{SingleTestData.Value} => ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(NeuralNetComputedValue);
                    BackpropQuestionIndex++;
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Backprop Prediction (% Correct): ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Math.Round(BackpropCorrectQuestions / (double)NewDataCorrectOutputs.Length * 100, 2));

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

                //Print The New Data
                int GeneticQuestionIndex = 0;
                int GeneticCorrectQuestions = 0;
                foreach (KeyValuePair<double[], string> SingleTestData in NewData)
                {
                    int NeuralNetComputedValue = (int)Math.Round(GeneticsTrainer.BestNetwork.Compute(SingleTestData.Key)[0], 0);
                    bool CorrectAnswer = NeuralNetComputedValue == NewDataCorrectOutputs[GeneticQuestionIndex];
                    Console.ForegroundColor = CorrectAnswer ? ConsoleColor.White : ConsoleColor.Red;
                    GeneticCorrectQuestions += CorrectAnswer ? 1 : 0;
                    Console.Write($"{SingleTestData.Value} => ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(NeuralNetComputedValue);
                    GeneticQuestionIndex++;
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Genetics Prediction (% Correct): ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Math.Round(GeneticCorrectQuestions / (double)NewDataCorrectOutputs.Length * 100, 2));

                //Allow Me To Read The Data
                Console.ReadLine();
                Console.Clear();
            }
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
