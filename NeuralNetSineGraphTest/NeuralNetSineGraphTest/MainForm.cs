using NeuralNetLIB;
using NeuralNetLIB.ActivationFunctions;
using NeuralNetLIB.LearningAlgorithms;
using NeuralNetLIB.NetworkStructure;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace NeuralNetSineGraphTest
{
    public partial class MainForm : Form
    {
        //Set The Constants Of The Graph
        public int TotalSteps = (int)Math.Round(GraphDomain / GraphStep);
        public const double GraphDomain = 360;
        public const double GraphStep = 1;

        //Create Trainers
        long LastGeneticGenerationCount = 0;
        Genetics GeneticsTrainer;

        //Create Test Data
        public double[][] TestDataOutputs;
        public double[][] TestDataInputs;

        //Background Worker
        BackgroundWorker MainWorker = new BackgroundWorker();
        bool KeepWorking = true;

        public MainForm()
        {
            InitializeComponent();
        }

        private void SineTestTable_Load(object sender, EventArgs e)
        {
            //Init Test Data
            TestDataOutputs = new double[TotalSteps][];
            TestDataInputs = new double[TotalSteps][];

            //Create Trainers
            GeneticsTrainer = new Genetics(new NeuralNetwork(new Sigmoid(), InitializerMethod.Random, 1, 5, 5, 1), 100);

            //Create Test Data
            int TestDataCounter = -1;
            for (double i = 0; i < GraphDomain; i += GraphStep)
            {
                TestDataCounter++;
                double CorrectOutputValue = Math.Sin(Math.PI * i / 180);
                TestDataInputs[TestDataCounter] = new double[] { i };
                MainGraph.Series[2].Points.AddXY(i, CorrectOutputValue);
                TestDataOutputs[TestDataCounter] = new double[] { CorrectOutputValue };
            }

            //Start The Background Work
            MainWorker.DoWork += BackgroundWorker_DoWork;
            MainWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //Test Train Epoch
            while (KeepWorking)
            {
                GeneticsTrainer.TrainGeneration(TestDataInputs, TestDataOutputs);
            }
        }

        private void DrawPointsTimer_Tick(object sender, EventArgs e)
        {
            //Draw The Backprop
            MainGraph.Series[0].Points.Clear();
            MainGraph.Series[1].Points.Clear();
            for (double j = 0; j < GraphDomain; j += GraphStep)
            {
                MainGraph.Series[0].Points.AddXY(j, GeneticsTrainer.BestNetwork.Compute(new double[] { j })[0]);
            }

            //Make Calculations
            double GenPerSec = (GeneticsTrainer.GenerationCount - LastGeneticGenerationCount) * 0.75;

            //Display To Screen
            DebugInfoBox.Text = $"Genetics Algorithm: {Environment.NewLine}{Environment.NewLine}";
            DebugInfoBox.AppendText($"Generation Count: {GeneticsTrainer.GenerationCount}{Environment.NewLine}");
            DebugInfoBox.AppendText($"Generations Per Seccond: {GenPerSec:0.00}{Environment.NewLine}");
            DebugInfoBox.AppendText($"Best Network Error: {((GeneticNeuralNetwork)GeneticsTrainer.BestNetwork).Fitness}{Environment.NewLine}");

            //Save Last States
            LastGeneticGenerationCount = GeneticsTrainer.GenerationCount;
        }

        private void PausePlayBTN_Click(object sender, EventArgs e)
        {
            KeepWorking = !KeepWorking;
            if (!KeepWorking)
            {
                PausePlayBTN.Text = "Resume";
                DrawPointsTimer.Stop();
            }
            else
            {
                MainWorker.RunWorkerAsync();
                PausePlayBTN.Text = "Pause";
                DrawPointsTimer.Start();
            }
        }
    }
}
