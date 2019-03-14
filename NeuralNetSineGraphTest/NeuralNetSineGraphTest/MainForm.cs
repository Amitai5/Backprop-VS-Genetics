using MachineLearningLIB.ActivationFunctions;
using MachineLearningLIB.InitializationFunctions;
using MachineLearningLIB.LearningAlgorithms;
using MachineLearningLIB.NetworkStructure;
using MachineLearningLIB.NetworkStructure.NetworkBuilder;
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
        public const int TestDataCount = 64;
        public const double GraphDomain = 4 * Math.PI;

        //Neural Network Objects
        double NetworkError = 0;
        Genetics GeneticsTrainer;
        Backpropagation BackpropTrainer;
        public const double TargetNetworkError = 0.01;

        //Training Timers
        TimeSpan GeneticsTrainingTime = new TimeSpan();
        TimeSpan BackpropTrainingTime = new TimeSpan();

        //Create Test Data
        public double[][] TestDataInputs;
        public double[][] TestDataOutputs;

        //Background Worker
        bool KeepWorking = true;
        BackgroundWorker GeneticsWorker = new BackgroundWorker();
        BackgroundWorker BackpropWorker = new BackgroundWorker();

        public MainForm()
        {
            InitializeComponent();
        }

        private void SineTestTable_Load(object sender, EventArgs e)
        {
            //Set Graph Min And Max
            MainGraph.ChartAreas[0].AxisY.Maximum = 1.25;
            MainGraph.ChartAreas[0].AxisY.Minimum = -1.25;

            //Create Model Network & Random
            Random Rand = new Random(26);
            NeuralNetwork BackpropNeuralNetwork = new NeuralNetworkBuilder(InitializationFunction.Random)
                                            .CreateInputLayer(5)
                                            .AddHiddenLayer(20, new SoftSine())
                                            .CreateOutputLayer(1, new SoftSine())
                                            .Build(Rand);

            GeneticNeuralNetwork[] GeneticsNetworkPopulation = new NeuralNetworkBuilder(InitializationFunction.Random)
                                           .CreateInputLayer(5)
                                           .AddHiddenLayer(20, new SoftSine())
                                           .CreateOutputLayer(1, new SoftSine())
                                           .BuildMany(Rand, 250);

            //Create Trainers
            GeneticsTrainer = new Genetics(Rand, GeneticsNetworkPopulation, 0.025);
            BackpropTrainer = new Backpropagation(BackpropNeuralNetwork, 25E-5, 25E-16);

            //Create Test Data & Draw The Model Sine Wave
            TestDataInputs = new double[TestDataCount][];
            TestDataOutputs = new double[TestDataCount][];
            for (int i = 0; i < TestDataCount; i++)
            {
                //Calculate The Point X-Values
                double pointXValue = GraphDomain / TestDataCount * i;
                double previousXValue = GraphDomain / TestDataCount * (i - 1);
                double previousXValue2 = GraphDomain / TestDataCount * (i - 2);
                double previousXValue3 = GraphDomain / TestDataCount * (i - 3);
                double previousXValue4 = GraphDomain / TestDataCount * (i - 4);

                TestDataInputs[i] = new double[] { previousXValue4, previousXValue3, previousXValue2, previousXValue, pointXValue };
                TestDataOutputs[i] = new double[] { Math.Sin(previousXValue), Math.Sin(pointXValue) };
                MainGraph.Series[2].Points.AddXY(pointXValue, TestDataOutputs[i][0]);
            }

            //Start Training
            KeepWorking = true;

            //Start The Background Workers
            BackpropWorker.DoWork += BackpropWorker_DoWork;
            GeneticsWorker.DoWork += GeneticsWorker_DoWork;
            BackpropWorker.RunWorkerAsync();
            GeneticsWorker.RunWorkerAsync();
        }

        #region Background Workers
        private void GeneticsWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (KeepWorking)
            {
                DateTime GeneticsStartTime = DateTime.Now;
                GeneticsTrainer.TrainGeneration(TestDataInputs, TestDataOutputs);
                GeneticsTrainingTime += DateTime.Now - GeneticsStartTime;
            }
        }
        private void BackpropWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (KeepWorking)
            {
                DateTime BackpropStartTime = DateTime.Now;
                NetworkError = BackpropTrainer.TrainEpoch(TestDataInputs, TestDataOutputs);
                BackpropTrainingTime += DateTime.Now - BackpropStartTime;
            }
        }
        #endregion Background Workers

        public void UpdateDisplay()
        {
            //Draw The Neural Network's Graph
            MainGraph.Series[0].Points.Clear();
            MainGraph.Series[1].Points.Clear();
            for (int i = 0; i < TestDataCount; i++)
            {
                double pointXValue = TestDataInputs[i][TestDataInputs[i].Length - 1];
                MainGraph.Series[1].Points.AddXY(pointXValue, BackpropTrainer.Network.Compute(TestDataInputs[i])[0]);
                MainGraph.Series[0].Points.AddXY(pointXValue, GeneticsTrainer.BestNetwork.Compute(TestDataInputs[i])[0]);
            }

            //Get Genetics Fitness
            double BestNetworkFitness = ((GeneticNeuralNetwork)GeneticsTrainer.BestNetwork).Fitness;

            //Start Debug Info
            DebugInfoBox.Clear();

            //Write Genetics Info
            DebugInfoBox.AppendText($"Genetics Algorithm: {Environment.NewLine}");
            DebugInfoBox.Select(0, DebugInfoBox.TextLength);
            DebugInfoBox.SelectionColor = Color.Red;

            DebugInfoBox.AppendText($"Best Network Error: {BestNetworkFitness: 0.00000}{Environment.NewLine}");
            DebugInfoBox.AppendText($"Training Time: {GeneticsTrainingTime}");
            DebugInfoBox.AppendText($"{Environment.NewLine}{Environment.NewLine}");

            //Write Backpropagation Info
            int LastLineIndex = DebugInfoBox.TextLength;
            DebugInfoBox.AppendText($"Backpropagation Algorithm: {Environment.NewLine}");
            DebugInfoBox.Select(LastLineIndex, DebugInfoBox.TextLength);
            DebugInfoBox.SelectionColor = Color.Blue;

            DebugInfoBox.AppendText($"Network Error: {NetworkError: 0.00000}{Environment.NewLine}");
            DebugInfoBox.AppendText($"Training Time: {BackpropTrainingTime}");

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
        private void PredictBTN_Click(object sender, EventArgs e)
        {
            //Set Up For Prediction
            WindowState = FormWindowState.Maximized;
            MainSplitter.Panel2Collapsed = true;
            DrawPointsTimer.Stop();
            KeepWorking = false;
            UpdateDisplay();

            //Clear Graph
            MainGraph.Series[0].Points.Clear();
            MainGraph.Series[1].Points.Clear();
            MainGraph.Series[2].Points.Clear();

            //Create Prediction Line
            MainGraph.Series[3].BorderDashStyle = ChartDashStyle.Dash;
            MainGraph.Series[3].Points.AddXY(GraphDomain, -5);
            MainGraph.Series[3].Points.AddXY(GraphDomain, 5);
            MainGraph.Series[3].BorderWidth = 8;

            //Draw The Second Sine Period
            int pedictionDataCount = TestDataCount * 2;
            for (int i = 0; i < pedictionDataCount; i++)
            {
                //Calculate The Point X-Values
                double pointXValue = GraphDomain / TestDataCount * i;
                double previousXValue = GraphDomain / TestDataCount * (i - 1);
                double previousXValue2 = GraphDomain / TestDataCount * (i - 2);
                double previousXValue3 = GraphDomain / TestDataCount * (i - 3);
                double previousXValue4 = GraphDomain / TestDataCount * (i - 4);
                double[] pointNeuralNetworkInput = new double[] { previousXValue4, previousXValue3, previousXValue2, previousXValue, pointXValue };

                //Re-Draw The Main Sine Graph
                MainGraph.Series[2].Points.AddXY(pointXValue, Math.Sin(pointXValue));

                //Draw Predicted Values
                MainGraph.Series[0].Points.AddXY(pointXValue, GeneticsTrainer.BestNetwork.Compute(pointNeuralNetworkInput)[0]);
                MainGraph.Series[1].Points.AddXY(pointXValue, BackpropTrainer.Network.Compute(pointNeuralNetworkInput)[0]);
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
                legendItem.MarkerSize = 256;
                legendItem.BorderWidth = 256;
            }
        }
    }
}
