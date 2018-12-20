using NeuralNetLIB.ActivationFunctions;
using NeuralNetLIB.LearningAlgorithms;
using NeuralNetLIB.NetworkStructure;
using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace NeuralNetSineGraphTest
{
    public partial class MainForm : Form
    {
        //Set The Constants Of The Graph
        public double SecondsPerUpdate = 0;
        public const long GraphDomain = 360;
        public double[] SineOfDegree(int i)
        {
            double radians = (i * Math.PI) / 180;
            return new double[] { Math.Sin(radians) };
        }

        //Genetics Neural Network Objects
        double NetworkError = 0;
        Backpropagation BackpropTrainer;
        double LastBackpropEpochCount = 0;

        //Genetics Neural Network Objects
        Genetics GeneticsTrainer;
        double LastGeneticGenerationCount = 0;

        //Create Test Data
        public double[][] TestDataInputs;
        public double[][] TestDataOutputs;

        //Background Worker
        bool KeepWorking = true;
        BackgroundWorker MainWorker = new BackgroundWorker();

        public MainForm()
        {
            InitializeComponent();
        }

        private void SineTestTable_Load(object sender, EventArgs e)
        {
            //Create Model Network
            NeuralNetwork ModelNetwork = new NeuralNetwork(new Sigmoid(), 1, 20, 1);

            //Create Trainers
            GeneticsTrainer = new Genetics(ModelNetwork, totalNetCount: 100, mutationRate: 0.05);
            BackpropTrainer = new Backpropagation(ModelNetwork, 0.0005);

            //Init Test Data & Store Time Per Update
            TestDataInputs = new double[GraphDomain][];
            TestDataOutputs = new double[GraphDomain][];
            SecondsPerUpdate = (double)DrawPointsTimer.Interval / 1000;

            //Create Test Data & Draw The Model Sine Wave
            for (int i = 0; i < GraphDomain; i++)
            {
                TestDataOutputs[i] = SineOfDegree(i);
                TestDataInputs[i] = new double[] { i };

                //Draw Model Sine Wave & Create Other Graph Points
                MainGraph.Series[2].Points.AddXY(i, TestDataOutputs[i][0]);
            }

            //Start Training
            BackpropTrainer.TrainEpoch(TestDataInputs, TestDataOutputs);
            GeneticsTrainer.TrainGeneration(TestDataInputs, TestDataOutputs);
            KeepWorking = true;

            //Start The Background Work
            MainWorker.DoWork += BackgroundWorker_DoWork;
            MainWorker.RunWorkerAsync();
        }

        public void UpdateDisplay()
        {
            //Draw The Neural Network's Graph
            MainGraph.Series[0].Points.Clear();
            MainGraph.Series[1].Points.Clear();
            for (int i = 0; i < GraphDomain; i += 1)
            {
                MainGraph.Series[1].Points.AddXY(i, BackpropTrainer.Network.Compute(new double[] { i })[0]);
                MainGraph.Series[0].Points.AddXY(i, GeneticsTrainer.BestNetwork.Compute(new double[] { i })[0]);
            }

            //Make Calculations
            double BestNetworkFitness = ((GeneticNeuralNetwork)GeneticsTrainer.BestNetwork).Fitness;
            double EpochPerSec = (BackpropTrainer.EpochCount - LastBackpropEpochCount) / SecondsPerUpdate;
            double GenPerSec = (GeneticsTrainer.GenerationCount - LastGeneticGenerationCount) / SecondsPerUpdate;

            //Create Debug String
            StringBuilder DisplayTextBuilder = new StringBuilder($"Genetics Algorithm: {Environment.NewLine}{Environment.NewLine}");
            DisplayTextBuilder.AppendLine($"Best Network Error: {BestNetworkFitness: 0.00000}");
            DisplayTextBuilder.AppendLine($"Current Gen: {GeneticsTrainer.GenerationCount}");
            DisplayTextBuilder.AppendLine($"G/S: {GenPerSec:0.00}{Environment.NewLine}{Environment.NewLine}");

            DisplayTextBuilder.AppendLine($"Backpropagation Algorithm: {Environment.NewLine}");
            DisplayTextBuilder.AppendLine($"Network Error: {NetworkError: 0.00000}");
            DisplayTextBuilder.AppendLine($"Current Epoch: {BackpropTrainer.EpochCount}");
            DisplayTextBuilder.AppendLine($"E/S: {EpochPerSec:0.00}");

            //Display To Screen
            DebugInfoBox.Text = DisplayTextBuilder.ToString();

            //Save Last States
            LastBackpropEpochCount = BackpropTrainer.EpochCount;
            LastGeneticGenerationCount = GeneticsTrainer.GenerationCount;
        }
        private void DrawPointsTimer_Tick(object sender, EventArgs e)
        {
            UpdateDisplay();
        }
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //Test Train Epoch
            while (KeepWorking)
            {
                GeneticsTrainer.TrainGeneration(TestDataInputs, TestDataOutputs);
                NetworkError = BackpropTrainer.TrainEpoch(TestDataInputs, TestDataOutputs);
            }
        }

        private void PausePlayBTN_Click(object sender, EventArgs e)
        {
            //Stop The Background Worker
            KeepWorking = !KeepWorking;

            if (!KeepWorking)
            {
                //Update The Screen Again
                UpdateDisplay();

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
