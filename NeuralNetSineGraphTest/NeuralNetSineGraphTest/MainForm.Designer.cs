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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.DrawPointsTimer = new System.Windows.Forms.Timer(this.components);
            this.MainSplitter = new System.Windows.Forms.SplitContainer();
            this.MainGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.PausePlayBTN = new System.Windows.Forms.Button();
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
            this.DrawPointsTimer.Interval = 750;
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
            this.MainSplitter.Panel2.Controls.Add(this.PausePlayBTN);
            this.MainSplitter.Panel2.Controls.Add(this.DebugInfoBox);
            this.MainSplitter.Size = new System.Drawing.Size(2350, 1247);
            this.MainSplitter.SplitterDistance = 1805;
            this.MainSplitter.TabIndex = 0;
            // 
            // MainGraph
            // 
            chartArea2.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea2.AxisX.IsMarginVisible = false;
            chartArea2.AxisX.MajorGrid.Interval = 0D;
            chartArea2.AxisX.MajorGrid.IntervalOffset = 0D;
            chartArea2.AxisX.MajorGrid.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea2.AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea2.AxisX.TitleFont = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisX2.TitleFont = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisY.TitleFont = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisY2.TitleFont = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.BorderWidth = 5;
            chartArea2.Name = "MainArea";
            this.MainGraph.ChartAreas.Add(chartArea2);
            this.MainGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.AutoFitMinFontSize = 12;
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend2.IsTextAutoFit = false;
            legend2.Name = "Algorithms";
            this.MainGraph.Legends.Add(legend2);
            this.MainGraph.Location = new System.Drawing.Point(0, 0);
            this.MainGraph.Name = "MainGraph";
            series4.BorderWidth = 5;
            series4.ChartArea = "MainArea";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.Blue;
            series4.Font = new System.Drawing.Font("Times New Roman", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series4.Legend = "Algorithms";
            series4.LegendText = "Genetics Algorithm";
            series4.MarkerBorderColor = System.Drawing.Color.Red;
            series4.MarkerSize = 8;
            series4.Name = "Genetics";
            series5.BorderWidth = 5;
            series5.ChartArea = "MainArea";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Color = System.Drawing.Color.Green;
            series5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series5.Legend = "Algorithms";
            series5.LegendText = "Backpropagation Algorithm";
            series5.Name = "Backprop";
            series6.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series6.BorderWidth = 3;
            series6.ChartArea = "MainArea";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            series6.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series6.Legend = "Algorithms";
            series6.LegendText = "Sine Graph";
            series6.Name = "SineGraph";
            this.MainGraph.Series.Add(series4);
            this.MainGraph.Series.Add(series5);
            this.MainGraph.Series.Add(series6);
            this.MainGraph.Size = new System.Drawing.Size(1805, 1247);
            this.MainGraph.TabIndex = 2;
            title2.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            title2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title2.Name = "GraphTitle";
            title2.Text = "Backpropagation VS. Genetics Learning Algorithm: Sine Wave";
            this.MainGraph.Titles.Add(title2);
            // 
            // PausePlayBTN
            // 
            this.PausePlayBTN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PausePlayBTN.Font = new System.Drawing.Font("Arial Rounded MT Bold", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PausePlayBTN.Location = new System.Drawing.Point(0, 800);
            this.PausePlayBTN.Name = "PausePlayBTN";
            this.PausePlayBTN.Size = new System.Drawing.Size(541, 447);
            this.PausePlayBTN.TabIndex = 1;
            this.PausePlayBTN.Text = "Pause";
            this.PausePlayBTN.UseVisualStyleBackColor = true;
            this.PausePlayBTN.Click += new System.EventHandler(this.PausePlayBTN_Click);
            // 
            // DebugInfoBox
            // 
            this.DebugInfoBox.Dock = System.Windows.Forms.DockStyle.Top;
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
        private System.Windows.Forms.Timer DrawPointsTimer;
        private System.Windows.Forms.SplitContainer MainSplitter;
        private System.Windows.Forms.DataVisualization.Charting.Chart MainGraph;
        private System.Windows.Forms.Button PausePlayBTN;
        private System.Windows.Forms.RichTextBox DebugInfoBox;
    }
}
