namespace NeuralNetSineGraphTest
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.DrawPointsTimer = new System.Windows.Forms.Timer(this.components);
            this.MainSplitter = new System.Windows.Forms.SplitContainer();
            this.MainGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.PredictBTN = new System.Windows.Forms.Button();
            this.DebugInfoBox = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitter)).BeginInit();
            this.MainSplitter.Panel1.SuspendLayout();
            this.MainSplitter.Panel2.SuspendLayout();
            this.MainSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // DrawPointsTimer
            // 
            this.DrawPointsTimer.Enabled = true;
            this.DrawPointsTimer.Interval = 250;
            this.DrawPointsTimer.Tick += new System.EventHandler(this.DrawPointsTimer_Tick);
            // 
            // MainSplitter
            // 
            this.MainSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitter.Location = new System.Drawing.Point(0, 0);
            this.MainSplitter.Name = "MainSplitter";
            // 
            // MainSplitter.Panel1
            // 
            this.MainSplitter.Panel1.Controls.Add(this.MainGraph);
            // 
            // MainSplitter.Panel2
            // 
            this.MainSplitter.Panel2.Controls.Add(this.PredictBTN);
            this.MainSplitter.Panel2.Controls.Add(this.DebugInfoBox);
            this.MainSplitter.Size = new System.Drawing.Size(2350, 1247);
            this.MainSplitter.SplitterDistance = 1805;
            this.MainSplitter.TabIndex = 0;
            // 
            // MainGraph
            // 
            chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisX.IsMarginVisible = false;
            chartArea1.AxisX.MajorGrid.Interval = 0D;
            chartArea1.AxisX.MajorGrid.IntervalOffset = 0D;
            chartArea1.AxisX.MajorGrid.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea1.AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisX2.TitleFont = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY2.TitleFont = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.BorderWidth = 5;
            chartArea1.Name = "MainArea";
            this.MainGraph.ChartAreas.Add(chartArea1);
            this.MainGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.AutoFitMinFontSize = 12;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend1.IsTextAutoFit = false;
            legend1.Name = "Algorithms";
            this.MainGraph.Legends.Add(legend1);
            this.MainGraph.Location = new System.Drawing.Point(0, 0);
            this.MainGraph.Name = "MainGraph";
            series1.BorderWidth = 5;
            series1.ChartArea = "MainArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
            series1.Color = System.Drawing.Color.Blue;
            series1.EmptyPointStyle.MarkerSize = 10;
            series1.EmptyPointStyle.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Font = new System.Drawing.Font("Times New Roman", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series1.Legend = "Algorithms";
            series1.LegendText = "Genetics Algorithm";
            series1.MarkerBorderColor = System.Drawing.Color.Blue;
            series1.MarkerSize = 8;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "Genetics";
            series2.BorderWidth = 5;
            series2.ChartArea = "MainArea";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
            series2.Color = System.Drawing.Color.Green;
            series2.EmptyPointStyle.MarkerSize = 10;
            series2.EmptyPointStyle.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series2.Legend = "Algorithms";
            series2.LegendText = "Backpropagation Algorithm";
            series2.MarkerSize = 8;
            series2.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series2.Name = "Backprop";
            series3.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series3.BorderWidth = 3;
            series3.ChartArea = "MainArea";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            series3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series3.Legend = "Algorithms";
            series3.LegendText = "Sine Graph";
            series3.MarkerSize = 8;
            series3.Name = "SineGraph";
            series4.ChartArea = "MainArea";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.Black;
            series4.IsVisibleInLegend = false;
            series4.Legend = "Algorithms";
            series4.Name = "PredictionLine";
            this.MainGraph.Series.Add(series1);
            this.MainGraph.Series.Add(series2);
            this.MainGraph.Series.Add(series3);
            this.MainGraph.Series.Add(series4);
            this.MainGraph.Size = new System.Drawing.Size(1805, 1247);
            this.MainGraph.TabIndex = 2;
            title1.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            title1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "GraphTitle";
            title1.Text = "Backpropagation VS. Genetics Learning Algorithm: Sine Wave";
            this.MainGraph.Titles.Add(title1);
            this.MainGraph.CustomizeLegend += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.CustomizeLegendEventArgs>(this.MainGraph_CustomizeLegend);
            // 
            // PredictBTN
            // 
            this.PredictBTN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PredictBTN.Font = new System.Drawing.Font("Arial Rounded MT Bold", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PredictBTN.Location = new System.Drawing.Point(0, 738);
            this.PredictBTN.Name = "PredictBTN";
            this.PredictBTN.Size = new System.Drawing.Size(541, 509);
            this.PredictBTN.TabIndex = 1;
            this.PredictBTN.Text = "Predict Values";
            this.PredictBTN.UseVisualStyleBackColor = true;
            this.PredictBTN.Click += new System.EventHandler(this.PredictBTN_Click);
            // 
            // DebugInfoBox
            // 
            this.DebugInfoBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DebugInfoBox.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DebugInfoBox.Location = new System.Drawing.Point(0, 0);
            this.DebugInfoBox.Name = "DebugInfoBox";
            this.DebugInfoBox.ReadOnly = true;
            this.DebugInfoBox.Size = new System.Drawing.Size(541, 800);
            this.DebugInfoBox.TabIndex = 0;
            this.DebugInfoBox.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(18F, 36F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2350, 1247);
            this.Controls.Add(this.MainSplitter);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Backpropagation VS. Genetics: Sine Wave";
            this.Load += new System.EventHandler(this.SineTestTable_Load);
            this.MainSplitter.Panel1.ResumeLayout(false);
            this.MainSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitter)).EndInit();
            this.MainSplitter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainGraph)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer MainSplitter;
        private System.Windows.Forms.DataVisualization.Charting.Chart MainGraph;
        private System.Windows.Forms.Button PredictBTN;
        private System.Windows.Forms.RichTextBox DebugInfoBox;
        private System.Windows.Forms.Timer DrawPointsTimer;
    }
}
