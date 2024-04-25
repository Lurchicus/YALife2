
namespace YALife
{
    partial class YALife
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.Windows.Forms.Label LBirth;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YALife));
            Frame = new System.Windows.Forms.PictureBox();
            panel1 = new System.Windows.Forms.Panel();
            Predef = new System.Windows.Forms.Button();
            BtnChart = new System.Windows.Forms.Button();
            label3 = new System.Windows.Forms.Label();
            CbxCollectStats = new System.Windows.Forms.CheckBox();
            DDMode = new System.Windows.Forms.ComboBox();
            BAbout = new System.Windows.Forms.Button();
            txtPassTimer = new System.Windows.Forms.TextBox();
            LabPassTimer = new System.Windows.Forms.Label();
            BLicense = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            LEmpty = new System.Windows.Forms.Label();
            LCrowd = new System.Windows.Forms.Label();
            LLonely = new System.Windows.Forms.Label();
            LAlive = new System.Windows.Forms.Label();
            TxEmpty = new System.Windows.Forms.TextBox();
            TxCrowd = new System.Windows.Forms.TextBox();
            TxLonely = new System.Windows.Forms.TextBox();
            TxLive = new System.Windows.Forms.TextBox();
            TxBirth = new System.Windows.Forms.TextBox();
            LIsEmpty = new System.Windows.Forms.Label();
            TxIsEmpty = new System.Windows.Forms.TextBox();
            LIsLiving = new System.Windows.Forms.Label();
            TxIsLiving = new System.Windows.Forms.TextBox();
            LPass = new System.Windows.Forms.Label();
            TxPass = new System.Windows.Forms.TextBox();
            BStep = new System.Windows.Forms.Button();
            BExit = new System.Windows.Forms.Button();
            TxLog = new System.Windows.Forms.TextBox();
            BStop = new System.Windows.Forms.Button();
            BRun = new System.Windows.Forms.Button();
            TxPercent = new System.Windows.Forms.NumericUpDown();
            BReset = new System.Windows.Forms.Button();
            LInitPercent = new System.Windows.Forms.Label();
            LWBlocks = new System.Windows.Forms.Label();
            TxWBlocks = new System.Windows.Forms.TextBox();
            LHBlocks = new System.Windows.Forms.Label();
            TxHBlocks = new System.Windows.Forms.TextBox();
            CkWrap = new System.Windows.Forms.CheckBox();
            LWPixels = new System.Windows.Forms.Label();
            TxWPixels = new System.Windows.Forms.TextBox();
            LHPixels = new System.Windows.Forms.Label();
            TxHPixels = new System.Windows.Forms.TextBox();
            LBlockSize = new System.Windows.Forms.Label();
            NBlockSize = new System.Windows.Forms.NumericUpDown();
            Timer = new System.Windows.Forms.Timer(components);
            LBirth = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)Frame).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TxPercent).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NBlockSize).BeginInit();
            SuspendLayout();
            // 
            // LBirth
            // 
            LBirth.AutoSize = true;
            LBirth.Location = new System.Drawing.Point(5, 335);
            LBirth.Name = "LBirth";
            LBirth.Size = new System.Drawing.Size(85, 15);
            LBirth.TabIndex = 31;
            LBirth.Text = "Births this pass";
            // 
            // Frame
            // 
            Frame.BackColor = System.Drawing.SystemColors.Control;
            Frame.Dock = System.Windows.Forms.DockStyle.Right;
            Frame.Location = new System.Drawing.Point(222, 0);
            Frame.MinimumSize = new System.Drawing.Size(1, 1);
            Frame.Name = "Frame";
            Frame.Size = new System.Drawing.Size(805, 670);
            Frame.TabIndex = 0;
            Frame.TabStop = false;
            Frame.SizeChanged += Image_SizeChanged;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            panel1.Controls.Add(Predef);
            panel1.Controls.Add(BtnChart);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(CbxCollectStats);
            panel1.Controls.Add(DDMode);
            panel1.Controls.Add(BAbout);
            panel1.Controls.Add(txtPassTimer);
            panel1.Controls.Add(LabPassTimer);
            panel1.Controls.Add(BLicense);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(LEmpty);
            panel1.Controls.Add(LCrowd);
            panel1.Controls.Add(LLonely);
            panel1.Controls.Add(LAlive);
            panel1.Controls.Add(LBirth);
            panel1.Controls.Add(TxEmpty);
            panel1.Controls.Add(TxCrowd);
            panel1.Controls.Add(TxLonely);
            panel1.Controls.Add(TxLive);
            panel1.Controls.Add(TxBirth);
            panel1.Controls.Add(LIsEmpty);
            panel1.Controls.Add(TxIsEmpty);
            panel1.Controls.Add(LIsLiving);
            panel1.Controls.Add(TxIsLiving);
            panel1.Controls.Add(LPass);
            panel1.Controls.Add(TxPass);
            panel1.Controls.Add(BStep);
            panel1.Controls.Add(BExit);
            panel1.Controls.Add(TxLog);
            panel1.Controls.Add(BStop);
            panel1.Controls.Add(BRun);
            panel1.Controls.Add(TxPercent);
            panel1.Controls.Add(BReset);
            panel1.Controls.Add(LInitPercent);
            panel1.Controls.Add(LWBlocks);
            panel1.Controls.Add(TxWBlocks);
            panel1.Controls.Add(LHBlocks);
            panel1.Controls.Add(TxHBlocks);
            panel1.Controls.Add(CkWrap);
            panel1.Controls.Add(LWPixels);
            panel1.Controls.Add(TxWPixels);
            panel1.Controls.Add(LHPixels);
            panel1.Controls.Add(TxHPixels);
            panel1.Controls.Add(LBlockSize);
            panel1.Controls.Add(NBlockSize);
            panel1.Dock = System.Windows.Forms.DockStyle.Left;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(216, 670);
            panel1.TabIndex = 1;
            // 
            // Predef
            // 
            Predef.BackColor = System.Drawing.Color.Gold;
            Predef.ForeColor = System.Drawing.Color.Black;
            Predef.Location = new System.Drawing.Point(144, 592);
            Predef.Name = "Predef";
            Predef.Size = new System.Drawing.Size(55, 23);
            Predef.TabIndex = 46;
            Predef.Text = "Predef";
            Predef.UseVisualStyleBackColor = false;
            Predef.Click += Predef_Click;
            // 
            // BtnChart
            // 
            BtnChart.BackColor = System.Drawing.Color.LightSkyBlue;
            BtnChart.Location = new System.Drawing.Point(83, 592);
            BtnChart.Name = "BtnChart";
            BtnChart.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            BtnChart.Size = new System.Drawing.Size(55, 23);
            BtnChart.TabIndex = 45;
            BtnChart.Text = "Chart";
            BtnChart.UseVisualStyleBackColor = false;
            BtnChart.Click += BtnChart_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(5, 507);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(71, 15);
            label3.TabIndex = 44;
            label3.Text = "Collect stats";
            // 
            // CbxCollectStats
            // 
            CbxCollectStats.AutoSize = true;
            CbxCollectStats.Location = new System.Drawing.Point(133, 506);
            CbxCollectStats.Name = "CbxCollectStats";
            CbxCollectStats.Size = new System.Drawing.Size(63, 19);
            CbxCollectStats.TabIndex = 43;
            CbxCollectStats.Text = "Collect";
            CbxCollectStats.UseVisualStyleBackColor = true;
            CbxCollectStats.CheckedChanged += CbxCollectStats_CheckedChanged;
            // 
            // DDMode
            // 
            DDMode.FormattingEnabled = true;
            DDMode.Location = new System.Drawing.Point(133, 211);
            DDMode.Name = "DDMode";
            DDMode.Size = new System.Drawing.Size(83, 23);
            DDMode.TabIndex = 2;
            DDMode.SelectedIndexChanged += DDMode_SelectedIndexChanged;
            // 
            // BAbout
            // 
            BAbout.BackColor = System.Drawing.Color.LightSkyBlue;
            BAbout.Location = new System.Drawing.Point(144, 563);
            BAbout.Name = "BAbout";
            BAbout.Size = new System.Drawing.Size(55, 23);
            BAbout.TabIndex = 42;
            BAbout.Text = "About";
            BAbout.UseVisualStyleBackColor = false;
            BAbout.Click += BAbout_Click;
            // 
            // txtPassTimer
            // 
            txtPassTimer.BackColor = System.Drawing.Color.LightSkyBlue;
            txtPassTimer.Location = new System.Drawing.Point(133, 477);
            txtPassTimer.Name = "txtPassTimer";
            txtPassTimer.ReadOnly = true;
            txtPassTimer.Size = new System.Drawing.Size(80, 23);
            txtPassTimer.TabIndex = 41;
            txtPassTimer.Text = "0";
            // 
            // LabPassTimer
            // 
            LabPassTimer.AutoSize = true;
            LabPassTimer.Location = new System.Drawing.Point(5, 480);
            LabPassTimer.Name = "LabPassTimer";
            LabPassTimer.Size = new System.Drawing.Size(92, 15);
            LabPassTimer.TabIndex = 40;
            LabPassTimer.Text = "Pass timer (sec.)";
            // 
            // BLicense
            // 
            BLicense.BackColor = System.Drawing.Color.LightSkyBlue;
            BLicense.Location = new System.Drawing.Point(83, 563);
            BLicense.Name = "BLicense";
            BLicense.Size = new System.Drawing.Size(55, 23);
            BLicense.TabIndex = 39;
            BLicense.Text = "License";
            BLicense.UseVisualStyleBackColor = false;
            BLicense.Click += BLicense_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(5, 214);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(100, 15);
            label2.TabIndex = 38;
            label2.Text = "Color cycle mode";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(5, 189);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(101, 15);
            label1.TabIndex = 37;
            label1.Text = "Universe: Open or";
            // 
            // LEmpty
            // 
            LEmpty.AutoSize = true;
            LEmpty.Location = new System.Drawing.Point(5, 451);
            LEmpty.Name = "LEmpty";
            LEmpty.Size = new System.Drawing.Size(127, 15);
            LEmpty.TabIndex = 35;
            LEmpty.Text = "Stayed empty this pass";
            // 
            // LCrowd
            // 
            LCrowd.AutoSize = true;
            LCrowd.Location = new System.Drawing.Point(5, 422);
            LCrowd.Name = "LCrowd";
            LCrowd.Size = new System.Drawing.Size(128, 15);
            LCrowd.TabIndex = 34;
            LCrowd.Text = "Died crowded this pass";
            // 
            // LLonely
            // 
            LLonely.AutoSize = true;
            LLonely.Location = new System.Drawing.Point(5, 393);
            LLonely.Name = "LLonely";
            LLonely.Size = new System.Drawing.Size(114, 15);
            LLonely.TabIndex = 33;
            LLonely.Text = "Died lonely this pass";
            // 
            // LAlive
            // 
            LAlive.AutoSize = true;
            LAlive.Location = new System.Drawing.Point(5, 364);
            LAlive.Name = "LAlive";
            LAlive.Size = new System.Drawing.Size(117, 15);
            LAlive.TabIndex = 32;
            LAlive.Text = "Stayed alive this pass";
            // 
            // TxEmpty
            // 
            TxEmpty.BackColor = System.Drawing.Color.Gold;
            TxEmpty.Location = new System.Drawing.Point(133, 448);
            TxEmpty.Name = "TxEmpty";
            TxEmpty.ReadOnly = true;
            TxEmpty.Size = new System.Drawing.Size(80, 23);
            TxEmpty.TabIndex = 30;
            TxEmpty.Text = "0";
            // 
            // TxCrowd
            // 
            TxCrowd.BackColor = System.Drawing.Color.LightSalmon;
            TxCrowd.Location = new System.Drawing.Point(133, 419);
            TxCrowd.Name = "TxCrowd";
            TxCrowd.ReadOnly = true;
            TxCrowd.Size = new System.Drawing.Size(80, 23);
            TxCrowd.TabIndex = 29;
            TxCrowd.Text = "0";
            // 
            // TxLonely
            // 
            TxLonely.BackColor = System.Drawing.Color.LightSalmon;
            TxLonely.Location = new System.Drawing.Point(133, 390);
            TxLonely.Name = "TxLonely";
            TxLonely.ReadOnly = true;
            TxLonely.Size = new System.Drawing.Size(80, 23);
            TxLonely.TabIndex = 28;
            TxLonely.Text = "0";
            // 
            // TxLive
            // 
            TxLive.BackColor = System.Drawing.Color.PaleGreen;
            TxLive.Location = new System.Drawing.Point(133, 361);
            TxLive.Name = "TxLive";
            TxLive.ReadOnly = true;
            TxLive.Size = new System.Drawing.Size(80, 23);
            TxLive.TabIndex = 27;
            TxLive.Text = "0";
            // 
            // TxBirth
            // 
            TxBirth.BackColor = System.Drawing.Color.PaleGreen;
            TxBirth.Location = new System.Drawing.Point(133, 332);
            TxBirth.Name = "TxBirth";
            TxBirth.ReadOnly = true;
            TxBirth.Size = new System.Drawing.Size(80, 23);
            TxBirth.TabIndex = 26;
            TxBirth.Text = "0";
            // 
            // LIsEmpty
            // 
            LIsEmpty.AutoSize = true;
            LIsEmpty.Location = new System.Drawing.Point(5, 306);
            LIsEmpty.Name = "LIsEmpty";
            LIsEmpty.Size = new System.Drawing.Size(97, 15);
            LIsEmpty.TabIndex = 25;
            LIsEmpty.Text = "Total Empty Cells";
            // 
            // TxIsEmpty
            // 
            TxIsEmpty.BackColor = System.Drawing.Color.LightSalmon;
            TxIsEmpty.Location = new System.Drawing.Point(133, 303);
            TxIsEmpty.Name = "TxIsEmpty";
            TxIsEmpty.ReadOnly = true;
            TxIsEmpty.Size = new System.Drawing.Size(80, 23);
            TxIsEmpty.TabIndex = 24;
            TxIsEmpty.Text = "0";
            // 
            // LIsLiving
            // 
            LIsLiving.AutoSize = true;
            LIsLiving.Location = new System.Drawing.Point(5, 277);
            LIsLiving.Name = "LIsLiving";
            LIsLiving.Size = new System.Drawing.Size(95, 15);
            LIsLiving.TabIndex = 23;
            LIsLiving.Text = "Total Living Cells";
            // 
            // TxIsLiving
            // 
            TxIsLiving.BackColor = System.Drawing.Color.PaleGreen;
            TxIsLiving.Location = new System.Drawing.Point(133, 274);
            TxIsLiving.Name = "TxIsLiving";
            TxIsLiving.ReadOnly = true;
            TxIsLiving.Size = new System.Drawing.Size(80, 23);
            TxIsLiving.TabIndex = 22;
            TxIsLiving.Text = "0";
            // 
            // LPass
            // 
            LPass.AutoSize = true;
            LPass.Location = new System.Drawing.Point(5, 248);
            LPass.Name = "LPass";
            LPass.Size = new System.Drawing.Size(30, 15);
            LPass.TabIndex = 21;
            LPass.Text = "Pass";
            // 
            // TxPass
            // 
            TxPass.BackColor = System.Drawing.Color.Gold;
            TxPass.Location = new System.Drawing.Point(133, 245);
            TxPass.Name = "TxPass";
            TxPass.ReadOnly = true;
            TxPass.Size = new System.Drawing.Size(80, 23);
            TxPass.TabIndex = 20;
            TxPass.Text = "0";
            // 
            // BStep
            // 
            BStep.BackColor = System.Drawing.Color.SpringGreen;
            BStep.Location = new System.Drawing.Point(144, 534);
            BStep.Name = "BStep";
            BStep.Size = new System.Drawing.Size(55, 23);
            BStep.TabIndex = 19;
            BStep.Text = "Step";
            BStep.UseVisualStyleBackColor = false;
            BStep.Click += BStep_Click;
            // 
            // BExit
            // 
            BExit.BackColor = System.Drawing.Color.Coral;
            BExit.Location = new System.Drawing.Point(22, 592);
            BExit.Name = "BExit";
            BExit.Size = new System.Drawing.Size(55, 23);
            BExit.TabIndex = 18;
            BExit.Text = "Exit";
            BExit.UseVisualStyleBackColor = false;
            BExit.Click += BExit_Click;
            // 
            // TxLog
            // 
            TxLog.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            TxLog.BackColor = System.Drawing.Color.Bisque;
            TxLog.Location = new System.Drawing.Point(3, 621);
            TxLog.Multiline = true;
            TxLog.Name = "TxLog";
            TxLog.ReadOnly = true;
            TxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            TxLog.Size = new System.Drawing.Size(210, 46);
            TxLog.TabIndex = 15;
            // 
            // BStop
            // 
            BStop.BackColor = System.Drawing.Color.Coral;
            BStop.Location = new System.Drawing.Point(22, 563);
            BStop.Name = "BStop";
            BStop.Size = new System.Drawing.Size(55, 23);
            BStop.TabIndex = 17;
            BStop.Text = "Stop";
            BStop.UseVisualStyleBackColor = false;
            BStop.Click += BStop_Click;
            // 
            // BRun
            // 
            BRun.BackColor = System.Drawing.Color.SpringGreen;
            BRun.Location = new System.Drawing.Point(83, 534);
            BRun.Name = "BRun";
            BRun.Size = new System.Drawing.Size(55, 23);
            BRun.TabIndex = 16;
            BRun.Text = "Run";
            BRun.UseVisualStyleBackColor = false;
            BRun.Click += BRun_Click;
            // 
            // TxPercent
            // 
            TxPercent.Location = new System.Drawing.Point(133, 32);
            TxPercent.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            TxPercent.Name = "TxPercent";
            TxPercent.Size = new System.Drawing.Size(80, 23);
            TxPercent.TabIndex = 14;
            TxPercent.Value = new decimal(new int[] { 25, 0, 0, 0 });
            TxPercent.ValueChanged += TxPercent_ValueChanged;
            // 
            // BReset
            // 
            BReset.BackColor = System.Drawing.Color.Gold;
            BReset.Location = new System.Drawing.Point(21, 534);
            BReset.Name = "BReset";
            BReset.Size = new System.Drawing.Size(55, 23);
            BReset.TabIndex = 13;
            BReset.Text = "Reset";
            BReset.UseVisualStyleBackColor = false;
            BReset.Click += BReset_Click;
            // 
            // LInitPercent
            // 
            LInitPercent.AutoSize = true;
            LInitPercent.Location = new System.Drawing.Point(3, 35);
            LInitPercent.Name = "LInitPercent";
            LInitPercent.Size = new System.Drawing.Size(98, 15);
            LInitPercent.TabIndex = 12;
            LInitPercent.Text = "Initial Percentage";
            // 
            // LWBlocks
            // 
            LWBlocks.AutoSize = true;
            LWBlocks.Location = new System.Drawing.Point(3, 127);
            LWBlocks.Name = "LWBlocks";
            LWBlocks.Size = new System.Drawing.Size(89, 15);
            LWBlocks.TabIndex = 10;
            LWBlocks.Text = "Width in Blocks";
            // 
            // TxWBlocks
            // 
            TxWBlocks.BackColor = System.Drawing.Color.Gold;
            TxWBlocks.Location = new System.Drawing.Point(133, 124);
            TxWBlocks.Name = "TxWBlocks";
            TxWBlocks.ReadOnly = true;
            TxWBlocks.Size = new System.Drawing.Size(80, 23);
            TxWBlocks.TabIndex = 9;
            TxWBlocks.Text = "0";
            // 
            // LHBlocks
            // 
            LHBlocks.AutoSize = true;
            LHBlocks.Location = new System.Drawing.Point(3, 156);
            LHBlocks.Name = "LHBlocks";
            LHBlocks.Size = new System.Drawing.Size(93, 15);
            LHBlocks.TabIndex = 8;
            LHBlocks.Text = "Height in Blocks";
            // 
            // TxHBlocks
            // 
            TxHBlocks.BackColor = System.Drawing.Color.Gold;
            TxHBlocks.Location = new System.Drawing.Point(133, 153);
            TxHBlocks.Name = "TxHBlocks";
            TxHBlocks.ReadOnly = true;
            TxHBlocks.Size = new System.Drawing.Size(80, 23);
            TxHBlocks.TabIndex = 7;
            TxHBlocks.Text = "0";
            // 
            // CkWrap
            // 
            CkWrap.AutoSize = true;
            CkWrap.Checked = true;
            CkWrap.CheckState = System.Windows.Forms.CheckState.Checked;
            CkWrap.Location = new System.Drawing.Point(133, 188);
            CkWrap.Name = "CkWrap";
            CkWrap.Size = new System.Drawing.Size(54, 19);
            CkWrap.TabIndex = 6;
            CkWrap.Text = "Wrap";
            CkWrap.UseVisualStyleBackColor = true;
            CkWrap.CheckedChanged += CkWrap_CheckedChanged;
            // 
            // LWPixels
            // 
            LWPixels.AutoSize = true;
            LWPixels.Location = new System.Drawing.Point(3, 69);
            LWPixels.Name = "LWPixels";
            LWPixels.Size = new System.Drawing.Size(85, 15);
            LWPixels.TabIndex = 5;
            LWPixels.Text = "Width in Pixels";
            // 
            // TxWPixels
            // 
            TxWPixels.BackColor = System.Drawing.Color.Yellow;
            TxWPixels.Location = new System.Drawing.Point(133, 66);
            TxWPixels.Name = "TxWPixels";
            TxWPixels.ReadOnly = true;
            TxWPixels.Size = new System.Drawing.Size(80, 23);
            TxWPixels.TabIndex = 4;
            TxWPixels.Text = "0";
            // 
            // LHPixels
            // 
            LHPixels.AutoSize = true;
            LHPixels.Location = new System.Drawing.Point(3, 98);
            LHPixels.Name = "LHPixels";
            LHPixels.Size = new System.Drawing.Size(89, 15);
            LHPixels.TabIndex = 3;
            LHPixels.Text = "Height in Pixels";
            // 
            // TxHPixels
            // 
            TxHPixels.BackColor = System.Drawing.Color.Yellow;
            TxHPixels.Location = new System.Drawing.Point(133, 95);
            TxHPixels.Name = "TxHPixels";
            TxHPixels.ReadOnly = true;
            TxHPixels.Size = new System.Drawing.Size(80, 23);
            TxHPixels.TabIndex = 2;
            TxHPixels.Text = "0";
            // 
            // LBlockSize
            // 
            LBlockSize.AutoSize = true;
            LBlockSize.Location = new System.Drawing.Point(3, 5);
            LBlockSize.Name = "LBlockSize";
            LBlockSize.Size = new System.Drawing.Size(99, 15);
            LBlockSize.TabIndex = 1;
            LBlockSize.Text = "Block size (zoom)";
            // 
            // NBlockSize
            // 
            NBlockSize.Location = new System.Drawing.Point(133, 3);
            NBlockSize.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
            NBlockSize.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NBlockSize.Name = "NBlockSize";
            NBlockSize.Size = new System.Drawing.Size(80, 23);
            NBlockSize.TabIndex = 0;
            NBlockSize.Value = new decimal(new int[] { 1, 0, 0, 0 });
            NBlockSize.ValueChanged += NBlockSize_ValueChanged;
            // 
            // Timer
            // 
            Timer.Interval = 600;
            Timer.Tick += Timer_Tick;
            // 
            // YALife
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            ClientSize = new System.Drawing.Size(1027, 670);
            Controls.Add(panel1);
            Controls.Add(Frame);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "YALife";
            Text = "YALife";
            Load += Form1_Load;
            SizeChanged += YALife_SizeChanged;
            ((System.ComponentModel.ISupportInitialize)Frame).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TxPercent).EndInit();
            ((System.ComponentModel.ISupportInitialize)NBlockSize).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.PictureBox Frame;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox CkWrap;
        private System.Windows.Forms.Label LWPixels;
        private System.Windows.Forms.TextBox TxWPixels;
        private System.Windows.Forms.Label LHPixels;
        private System.Windows.Forms.TextBox TxHPixels;
        private System.Windows.Forms.Label LBlockSize;
        private System.Windows.Forms.NumericUpDown NBlockSize;
        private System.Windows.Forms.Timer Timer;
        private System.Windows.Forms.Label LWBlocks;
        private System.Windows.Forms.TextBox TxWBlocks;
        private System.Windows.Forms.Label LHBlocks;
        private System.Windows.Forms.TextBox TxHBlocks;
        private System.Windows.Forms.Label LInitPercent;
        private System.Windows.Forms.Button BReset;
        private System.Windows.Forms.NumericUpDown TxPercent;
        private System.Windows.Forms.TextBox TxLog;
        private System.Windows.Forms.Button BExit;
        private System.Windows.Forms.Button BStop;
        private System.Windows.Forms.Button BRun;
        private System.Windows.Forms.Button BStep;
        private System.Windows.Forms.TextBox TxPass;
        private System.Windows.Forms.Label LPass;
        private System.Windows.Forms.Label LIsEmpty;
        private System.Windows.Forms.TextBox TxIsEmpty;
        private System.Windows.Forms.Label LIsLiving;
        private System.Windows.Forms.TextBox TxIsLiving;
        private System.Windows.Forms.TextBox TxEmpty;
        private System.Windows.Forms.TextBox TxCrowd;
        private System.Windows.Forms.TextBox TxLonely;
        private System.Windows.Forms.TextBox TxLive;
        private System.Windows.Forms.TextBox TxBirth;
        private System.Windows.Forms.Label LEmpty;
        private System.Windows.Forms.Label LCrowd;
        private System.Windows.Forms.Label LLonely;
        private System.Windows.Forms.Label LAlive;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BLicense;
        private System.Windows.Forms.Button BAbout;
        private System.Windows.Forms.TextBox txtPassTimer;
        private System.Windows.Forms.Label LabPassTimer;
        private System.Windows.Forms.ComboBox DDMode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox CbxCollectStats;
        private System.Windows.Forms.Button BtnChart;
        private System.Windows.Forms.Button Predef;
    }
}

