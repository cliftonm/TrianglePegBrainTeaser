namespace Demo
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
			this.pnlFlowSharp = new System.Windows.Forms.Panel();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.ckShowUi = new System.Windows.Forms.CheckBox();
			this.nudStartPosition = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.btnSingleStep = new System.Windows.Forms.Button();
			this.tbIterateDelay = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lbSolution = new System.Windows.Forms.ListBox();
			this.btnStart = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.tbIterations = new System.Windows.Forms.TextBox();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudStartPosition)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlFlowSharp
			// 
			this.pnlFlowSharp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlFlowSharp.Location = new System.Drawing.Point(282, 13);
			this.pnlFlowSharp.Name = "pnlFlowSharp";
			this.pnlFlowSharp.Size = new System.Drawing.Size(610, 475);
			this.pnlFlowSharp.TabIndex = 0;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.tbIterations);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Controls.Add(this.ckShowUi);
			this.groupBox3.Controls.Add(this.nudStartPosition);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.btnSingleStep);
			this.groupBox3.Controls.Add(this.tbIterateDelay);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.label1);
			this.groupBox3.Controls.Add(this.lbSolution);
			this.groupBox3.Controls.Add(this.btnStart);
			this.groupBox3.Location = new System.Drawing.Point(16, 13);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(260, 475);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Solutions:";
			// 
			// ckShowUi
			// 
			this.ckShowUi.AutoSize = true;
			this.ckShowUi.Checked = true;
			this.ckShowUi.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckShowUi.Location = new System.Drawing.Point(195, 48);
			this.ckShowUi.Name = "ckShowUi";
			this.ckShowUi.Size = new System.Drawing.Size(37, 17);
			this.ckShowUi.TabIndex = 8;
			this.ckShowUi.Text = "UI";
			this.ckShowUi.UseVisualStyleBackColor = true;
			// 
			// nudStartPosition
			// 
			this.nudStartPosition.Location = new System.Drawing.Point(83, 71);
			this.nudStartPosition.Maximum = new decimal(new int[] {
            14,
            0,
            0,
            0});
			this.nudStartPosition.Name = "nudStartPosition";
			this.nudStartPosition.Size = new System.Drawing.Size(65, 20);
			this.nudStartPosition.TabIndex = 7;
			this.nudStartPosition.ValueChanged += new System.EventHandler(this.OnStartPositionChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(7, 73);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(32, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Start:";
			// 
			// btnSingleStep
			// 
			this.btnSingleStep.Location = new System.Drawing.Point(88, 19);
			this.btnSingleStep.Name = "btnSingleStep";
			this.btnSingleStep.Size = new System.Drawing.Size(75, 23);
			this.btnSingleStep.TabIndex = 5;
			this.btnSingleStep.Text = "Single Step";
			this.btnSingleStep.UseVisualStyleBackColor = true;
			this.btnSingleStep.Click += new System.EventHandler(this.btnSingleStep_Click);
			// 
			// tbIterateDelay
			// 
			this.tbIterateDelay.Location = new System.Drawing.Point(83, 46);
			this.tbIterateDelay.Name = "tbIterateDelay";
			this.tbIterateDelay.Size = new System.Drawing.Size(65, 20);
			this.tbIterateDelay.TabIndex = 4;
			this.tbIterateDelay.Text = "10";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(153, 49);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(20, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "ms";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 49);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(70, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Iterate Delay:";
			// 
			// lbSolution
			// 
			this.lbSolution.FormattingEnabled = true;
			this.lbSolution.Location = new System.Drawing.Point(7, 133);
			this.lbSolution.Name = "lbSolution";
			this.lbSolution.Size = new System.Drawing.Size(247, 329);
			this.lbSolution.TabIndex = 1;
			this.lbSolution.SelectedIndexChanged += new System.EventHandler(this.OnSolutionStepChanged);
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(7, 20);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 23);
			this.btnStart.TabIndex = 0;
			this.btnStart.Text = "Run";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnRun_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(7, 101);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(53, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Iterations:";
			// 
			// tbIterations
			// 
			this.tbIterations.Location = new System.Drawing.Point(83, 98);
			this.tbIterations.Name = "tbIterations";
			this.tbIterations.ReadOnly = true;
			this.tbIterations.Size = new System.Drawing.Size(65, 20);
			this.tbIterations.TabIndex = 10;
			this.tbIterations.Text = "0";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(904, 500);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.pnlFlowSharp);
			this.Name = "MainForm";
			this.Text = "Brain Teaser";
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudStartPosition)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlFlowSharp;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.ListBox lbSolution;
		private System.Windows.Forms.TextBox tbIterateDelay;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnSingleStep;
		private System.Windows.Forms.NumericUpDown nudStartPosition;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox ckShowUi;
		private System.Windows.Forms.TextBox tbIterations;
		private System.Windows.Forms.Label label4;
	}
}

