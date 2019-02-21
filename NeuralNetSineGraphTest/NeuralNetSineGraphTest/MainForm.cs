using NeuralNetLIB.ActivationFunctions;
using NeuralNetLIB.LearningAlgorithms;
using NeuralNetLIB.NetworkStructure;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace NeuralNetSineGraphTest
{
    public partial class MainForm : Form
    {
        //Set The Constants Of The Graph
        public const int TestDataCount = 1250;
        public const double GraphDomain = 4 * Math.PI;
        public double StepValue = GraphDomain / TestDataCount;

        //Neural Network Objects
        double NetworkError = 0;
        Genetics GeneticsTrainer;
        Backpropagation BackpropTrainer;
        public const double TargetNetworkError = 0.005;

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

        private void PredictBTN_Click(object sender, EventArgs e)
        {
            //Stop The Background Worker & Timer
            DrawPointsTimer.Stop();
            KeepWorking = false;

            //Update Display Again
            UpdateDisplay();

            //Clear Graph
            MainGraph.Series[0].Points.Clear();
            MainGraph.Series[1].Points.Clear();
            MainGraph.Series[2].Points.Clear();

            //Change Graph Display
            for (int i = 0; i < MainGraph.Series.Count; i++)
            {
                MainGraph.Series[i].BorderWidth = 10;
                MainGraph.Series[i].MarkerStyle = MarkerStyle.None;
                MainGraph.Series[i].ChartType = SeriesChartType.Line;
                MainGraph.Series[i].BorderDashStyle = ChartDashStyle.Solid;
            }

            //Draw The Second Sine Period
            for (double i = 0; i < GraphDomain * 2; i += StepValue)
            {
                //Re-Draw The Main Sine Graph
                MainGraph.Series[2].Points.AddXY(i, Math.Sin(i));

                //Draw Predicted Values
                MainGraph.Series[1].Points.AddXY(i, BackpropTrainer.Network.Compute(new double[] { i })[0]);
                MainGraph.Series[0].Points.AddXY(i, GeneticsTrainer.BestNetwork.Compute(new double[] { i })[0]);
            }

            //Create Prediction Line
            MainGraph.Series[3].Points.AddXY(GraphDomain, 5);
            MainGraph.Series[3].Points.AddXY(GraphDomain, -5);
            MainGraph.Series[3].BorderDashStyle = ChartDashStyle.Dash;
        }
        private void SineTestTable_Load(object sender, EventArgs e)
        {
            //Set Graph Min And Max
            MainGraph.ChartAreas[0].AxisY.Maximum = 1.25;
            MainGraph.ChartAreas[0].AxisY.Minimum = -1.25;

            //Create Model Network & Random
            Random Rand = new Random();
            NeuralNetwork ModelNetwork = new NeuralNetwork(new SoftSine(), 1, 20, 1);

            //Create Trainers
            GeneticsTrainer = new Genetics(Rand, ModelNetwork, totalNetCount: 10, mutationRate: 0.05);
            BackpropTrainer = new Backpropagation(ModelNetwork, 25E-6, 25E-10);

            //Init Test Data & Store Time Per Update
            TestDataInputs = new double[TestDataCount][];
            TestDataOutputs = new double[TestDataCount][];

            //Create Test Data & Draw The Model Sine Wave
            int indexCounter = 0;
            for (double i = 0; i < GraphDomain; i += StepValue)
            {
                TestDataInputs[indexCounter] = new double[] { i };
                TestDataOutputs[indexCounter] = new double[] { Math.Sin(i) };
                MainGraph.Series[2].Points.AddXY(i, TestDataOutputs[indexCounter][0]);
                indexCounter++;
            }

            //Start Training
            BackpropTrainer.TrainEpoch(TestDataInputs, TestDataOutputs);
            GeneticsTrainer.TrainGeneration(TestDataInputs, TestDataOutputs);
            KeepWorking = true;

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
                NetworkError = BackpropTrainer.TrainEpoch(TestDataInputs, TestDataOutputs);
            }
        }

        public void UpdateDisplay()
        {
            //Draw The Neural Network's Graph
            MainGraph.Series[0].Points.Clear();
            MainGraph.Series[1].Points.Clear();
            for (double i = 0; i < GraphDomain; i += StepValue)
            {
                MainGraph.Series[1].Points.AddXY(i, BackpropTrainer.Network.Compute(new double[] { i })[0]);
                MainGraph.Series[0].Points.AddXY(i, GeneticsTrainer.BestNetwork.Compute(new double[] { i })[0]);
            }

            //Get Fitness
            double BestNetworkFitness = ((GeneticNeuralNetwork)GeneticsTrainer.BestNetwork).Fitness;

            //Start Debug Info
            DebugInfoBox.Clear();

            //Write Genetics Info
            DebugInfoBox.AppendText($"Genetics Algorithm: {Environment.NewLine}");
            DebugInfoBox.Select(0, DebugInfoBox.TextLength);
            DebugInfoBox.SelectionColor = Color.Blue;

            DebugInfoBox.AppendText($"Best Network Error: {BestNetworkFitness: 0.00000}{Environment.NewLine}");
            DebugInfoBox.AppendText($"Current Gen: {GeneticsTrainer.GenerationCount}");
            DebugInfoBox.AppendText($"{Environment.NewLine}{Environment.NewLine}");

            //Write Backpropagation Info
            int LastLineIndex = DebugInfoBox.TextLength;
            DebugInfoBox.AppendText($"Backpropagation Algorithm: {Environment.NewLine}");
            DebugInfoBox.Select(LastLineIndex, DebugInfoBox.TextLength);
            DebugInfoBox.SelectionColor = Color.Green;

            DebugInfoBox.AppendText($"Network Error: {NetworkError: 0.00000}{Environment.NewLine}");
            DebugInfoBox.AppendText($"Current Epoch: {BackpropTrainer.EpochCount}");

            //Display To Screen
            DebugInfoBox.Update();

            //Check The Error
            if (BackpropTrainer.EpochCount > 10 &&
                (NetworkError < TargetNetworkError || BestNetworkFitness < TargetNetworkError))
            {
                //Automatically Predict Values
                PredictBTN_Click(this, new EventArgs());
            }
        }
        private void DrawPointsTimer_Tick(object sender, EventArgs e)
        {
            UpdateDisplay();
        }
        private void MainGraph_CustomizeLegend(object sender, CustomizeLegendEventArgs e)
        {
            foreach (LegendItem legendItem in e.LegendItems)
            {
                legendItem.MarkerSize = 64;
            }
        }
    }
}
