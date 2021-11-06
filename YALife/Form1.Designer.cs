
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label LBirth;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YALife));
            this.Frame = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BAbout = new System.Windows.Forms.Button();
            this.txtPassTimer = new System.Windows.Forms.TextBox();
            this.LabPassTimer = new System.Windows.Forms.Label();
            this.BLicense = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CkOnce = new System.Windows.Forms.CheckBox();
            this.LEmpty = new System.Windows.Forms.Label();
            this.LCrowd = new System.Windows.Forms.Label();
            this.LLonely = new System.Windows.Forms.Label();
            this.LAlive = new System.Windows.Forms.Label();
            this.TxEmpty = new System.Windows.Forms.TextBox();
            this.TxCrowd = new System.Windows.Forms.TextBox();
            this.TxLonely = new System.Windows.Forms.TextBox();
            this.TxLive = new System.Windows.Forms.TextBox();
            this.TxBirth = new System.Windows.Forms.TextBox();
            this.LIsEmpty = new System.Windows.Forms.Label();
            this.TxIsEmpty = new System.Windows.Forms.TextBox();
            this.LIsLiving = new System.Windows.Forms.Label();
            this.TxIsLiving = new System.Windows.Forms.TextBox();
            this.LPass = new System.Windows.Forms.Label();
            this.TxPass = new System.Windows.Forms.TextBox();
            this.BStep = new System.Windows.Forms.Button();
            this.BExit = new System.Windows.Forms.Button();
            this.TxLog = new System.Windows.Forms.TextBox();
            this.BStop = new System.Windows.Forms.Button();
            this.BRun = new System.Windows.Forms.Button();
            this.TxPercent = new System.Windows.Forms.NumericUpDown();
            this.BReset = new System.Windows.Forms.Button();
            this.LInitPercent = new System.Windows.Forms.Label();
            this.LWBlocks = new System.Windows.Forms.Label();
            this.TxWBlocks = new System.Windows.Forms.TextBox();
            this.LHBlocks = new System.Windows.Forms.Label();
            this.TxHBlocks = new System.Windows.Forms.TextBox();
            this.CkWrap = new System.Windows.Forms.CheckBox();
            this.LWPixels = new System.Windows.Forms.Label();
            this.TxWPixels = new System.Windows.Forms.TextBox();
            this.LHPixels = new System.Windows.Forms.Label();
            this.TxHPixels = new System.Windows.Forms.TextBox();
            this.LBlockSize = new System.Windows.Forms.Label();
            this.NBlockSize = new System.Windows.Forms.NumericUpDown();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            LBirth = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Frame)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NBlockSize)).BeginInit();
            this.SuspendLayout();
            // 
            // LBirth
            // 
            LBirth.AutoSize = true;
            LBirth.Location = new System.Drawing.Point(5, 315);
            LBirth.Name = "LBirth";
            LBirth.Size = new System.Drawing.Size(85, 15);
            LBirth.TabIndex = 31;
            LBirth.Text = "Births this pass";
            // 
            // Frame
            // 
            this.Frame.BackColor = System.Drawing.SystemColors.Control;
            this.Frame.Dock = System.Windows.Forms.DockStyle.Right;
            this.Frame.Location = new System.Drawing.Point(222, 0);
            this.Frame.MinimumSize = new System.Drawing.Size(1, 1);
            this.Frame.Name = "Frame";
            this.Frame.Size = new System.Drawing.Size(805, 636);
            this.Frame.TabIndex = 0;
            this.Frame.TabStop = false;
            this.Frame.SizeChanged += new System.EventHandler(this.Image_SizeChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel1.Controls.Add(this.BAbout);
            this.panel1.Controls.Add(this.txtPassTimer);
            this.panel1.Controls.Add(this.LabPassTimer);
            this.panel1.Controls.Add(this.BLicense);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.CkOnce);
            this.panel1.Controls.Add(this.LEmpty);
            this.panel1.Controls.Add(this.LCrowd);
            this.panel1.Controls.Add(this.LLonely);
            this.panel1.Controls.Add(this.LAlive);
            this.panel1.Controls.Add(LBirth);
            this.panel1.Controls.Add(this.TxEmpty);
            this.panel1.Controls.Add(this.TxCrowd);
            this.panel1.Controls.Add(this.TxLonely);
            this.panel1.Controls.Add(this.TxLive);
            this.panel1.Controls.Add(this.TxBirth);
            this.panel1.Controls.Add(this.LIsEmpty);
            this.panel1.Controls.Add(this.TxIsEmpty);
            this.panel1.Controls.Add(this.LIsLiving);
            this.panel1.Controls.Add(this.TxIsLiving);
            this.panel1.Controls.Add(this.LPass);
            this.panel1.Controls.Add(this.TxPass);
            this.panel1.Controls.Add(this.BStep);
            this.panel1.Controls.Add(this.BExit);
            this.panel1.Controls.Add(this.TxLog);
            this.panel1.Controls.Add(this.BStop);
            this.panel1.Controls.Add(this.BRun);
            this.panel1.Controls.Add(this.TxPercent);
            this.panel1.Controls.Add(this.BReset);
            this.panel1.Controls.Add(this.LInitPercent);
            this.panel1.Controls.Add(this.LWBlocks);
            this.panel1.Controls.Add(this.TxWBlocks);
            this.panel1.Controls.Add(this.LHBlocks);
            this.panel1.Controls.Add(this.TxHBlocks);
            this.panel1.Controls.Add(this.CkWrap);
            this.panel1.Controls.Add(this.LWPixels);
            this.panel1.Controls.Add(this.TxWPixels);
            this.panel1.Controls.Add(this.LHPixels);
            this.panel1.Controls.Add(this.TxHPixels);
            this.panel1.Controls.Add(this.LBlockSize);
            this.panel1.Controls.Add(this.NBlockSize);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(216, 636);
            this.panel1.TabIndex = 1;
            // 
            // BAbout
            // 
            this.BAbout.BackColor = System.Drawing.Color.LightSkyBlue;
            this.BAbout.Location = new System.Drawing.Point(142, 515);
            this.BAbout.Name = "BAbout";
            this.BAbout.Size = new System.Drawing.Size(55, 23);
            this.BAbout.TabIndex = 42;
            this.BAbout.Text = "About";
            this.BAbout.UseVisualStyleBackColor = false;
            this.BAbout.Click += new System.EventHandler(this.BAbout_Click);
            // 
            // txtPassTimer
            // 
            this.txtPassTimer.BackColor = System.Drawing.Color.LightSkyBlue;
            this.txtPassTimer.Location = new System.Drawing.Point(133, 457);
            this.txtPassTimer.Name = "txtPassTimer";
            this.txtPassTimer.ReadOnly = true;
            this.txtPassTimer.Size = new System.Drawing.Size(80, 23);
            this.txtPassTimer.TabIndex = 41;
            this.txtPassTimer.Text = "0";
            // 
            // LabPassTimer
            // 
            this.LabPassTimer.AutoSize = true;
            this.LabPassTimer.Location = new System.Drawing.Point(5, 460);
            this.LabPassTimer.Name = "LabPassTimer";
            this.LabPassTimer.Size = new System.Drawing.Size(92, 15);
            this.LabPassTimer.TabIndex = 40;
            this.LabPassTimer.Text = "Pass timer (sec.)";
            // 
            // BLicense
            // 
            this.BLicense.BackColor = System.Drawing.Color.LightSkyBlue;
            this.BLicense.Location = new System.Drawing.Point(81, 515);
            this.BLicense.Name = "BLicense";
            this.BLicense.Size = new System.Drawing.Size(55, 23);
            this.BLicense.TabIndex = 39;
            this.BLicense.Text = "License";
            this.BLicense.UseVisualStyleBackColor = false;
            this.BLicense.Click += new System.EventHandler(this.BLicense_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 203);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 15);
            this.label2.TabIndex = 38;
            this.label2.Text = "Cycle colors";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 178);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 15);
            this.label1.TabIndex = 37;
            this.label1.Text = "Universe: Open or";
            // 
            // CkOnce
            // 
            this.CkOnce.AutoSize = true;
            this.CkOnce.Checked = true;
            this.CkOnce.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CkOnce.Location = new System.Drawing.Point(133, 202);
            this.CkOnce.Name = "CkOnce";
            this.CkOnce.Size = new System.Drawing.Size(54, 19);
            this.CkOnce.TabIndex = 36;
            this.CkOnce.Text = "Once";
            this.CkOnce.UseVisualStyleBackColor = true;
            this.CkOnce.CheckedChanged += new System.EventHandler(this.CkOnce_CheckedChanged);
            // 
            // LEmpty
            // 
            this.LEmpty.AutoSize = true;
            this.LEmpty.Location = new System.Drawing.Point(5, 431);
            this.LEmpty.Name = "LEmpty";
            this.LEmpty.Size = new System.Drawing.Size(127, 15);
            this.LEmpty.TabIndex = 35;
            this.LEmpty.Text = "Stayed empty this pass";
            // 
            // LCrowd
            // 
            this.LCrowd.AutoSize = true;
            this.LCrowd.Location = new System.Drawing.Point(5, 402);
            this.LCrowd.Name = "LCrowd";
            this.LCrowd.Size = new System.Drawing.Size(128, 15);
            this.LCrowd.TabIndex = 34;
            this.LCrowd.Text = "Died crowded this pass";
            // 
            // LLonely
            // 
            this.LLonely.AutoSize = true;
            this.LLonely.Location = new System.Drawing.Point(5, 373);
            this.LLonely.Name = "LLonely";
            this.LLonely.Size = new System.Drawing.Size(114, 15);
            this.LLonely.TabIndex = 33;
            this.LLonely.Text = "Died lonely this pass";
            // 
            // LAlive
            // 
            this.LAlive.AutoSize = true;
            this.LAlive.Location = new System.Drawing.Point(5, 344);
            this.LAlive.Name = "LAlive";
            this.LAlive.Size = new System.Drawing.Size(117, 15);
            this.LAlive.TabIndex = 32;
            this.LAlive.Text = "Stayed alive this pass";
            // 
            // TxEmpty
            // 
            this.TxEmpty.BackColor = System.Drawing.Color.Gold;
            this.TxEmpty.Location = new System.Drawing.Point(133, 428);
            this.TxEmpty.Name = "TxEmpty";
            this.TxEmpty.ReadOnly = true;
            this.TxEmpty.Size = new System.Drawing.Size(80, 23);
            this.TxEmpty.TabIndex = 30;
            this.TxEmpty.Text = "0";
            // 
            // TxCrowd
            // 
            this.TxCrowd.BackColor = System.Drawing.Color.LightSalmon;
            this.TxCrowd.Location = new System.Drawing.Point(133, 399);
            this.TxCrowd.Name = "TxCrowd";
            this.TxCrowd.ReadOnly = true;
            this.TxCrowd.Size = new System.Drawing.Size(80, 23);
            this.TxCrowd.TabIndex = 29;
            this.TxCrowd.Text = "0";
            // 
            // TxLonely
            // 
            this.TxLonely.BackColor = System.Drawing.Color.LightSalmon;
            this.TxLonely.Location = new System.Drawing.Point(133, 370);
            this.TxLonely.Name = "TxLonely";
            this.TxLonely.ReadOnly = true;
            this.TxLonely.Size = new System.Drawing.Size(80, 23);
            this.TxLonely.TabIndex = 28;
            this.TxLonely.Text = "0";
            // 
            // TxLive
            // 
            this.TxLive.BackColor = System.Drawing.Color.PaleGreen;
            this.TxLive.Location = new System.Drawing.Point(133, 341);
            this.TxLive.Name = "TxLive";
            this.TxLive.ReadOnly = true;
            this.TxLive.Size = new System.Drawing.Size(80, 23);
            this.TxLive.TabIndex = 27;
            this.TxLive.Text = "0";
            // 
            // TxBirth
            // 
            this.TxBirth.BackColor = System.Drawing.Color.PaleGreen;
            this.TxBirth.Location = new System.Drawing.Point(133, 312);
            this.TxBirth.Name = "TxBirth";
            this.TxBirth.ReadOnly = true;
            this.TxBirth.Size = new System.Drawing.Size(80, 23);
            this.TxBirth.TabIndex = 26;
            this.TxBirth.Text = "0";
            // 
            // LIsEmpty
            // 
            this.LIsEmpty.AutoSize = true;
            this.LIsEmpty.Location = new System.Drawing.Point(5, 286);
            this.LIsEmpty.Name = "LIsEmpty";
            this.LIsEmpty.Size = new System.Drawing.Size(97, 15);
            this.LIsEmpty.TabIndex = 25;
            this.LIsEmpty.Text = "Total Empty Cells";
            // 
            // TxIsEmpty
            // 
            this.TxIsEmpty.BackColor = System.Drawing.Color.LightSalmon;
            this.TxIsEmpty.Location = new System.Drawing.Point(133, 283);
            this.TxIsEmpty.Name = "TxIsEmpty";
            this.TxIsEmpty.ReadOnly = true;
            this.TxIsEmpty.Size = new System.Drawing.Size(80, 23);
            this.TxIsEmpty.TabIndex = 24;
            this.TxIsEmpty.Text = "0";
            // 
            // LIsLiving
            // 
            this.LIsLiving.AutoSize = true;
            this.LIsLiving.Location = new System.Drawing.Point(5, 257);
            this.LIsLiving.Name = "LIsLiving";
            this.LIsLiving.Size = new System.Drawing.Size(95, 15);
            this.LIsLiving.TabIndex = 23;
            this.LIsLiving.Text = "Total Living Cells";
            // 
            // TxIsLiving
            // 
            this.TxIsLiving.BackColor = System.Drawing.Color.PaleGreen;
            this.TxIsLiving.Location = new System.Drawing.Point(133, 254);
            this.TxIsLiving.Name = "TxIsLiving";
            this.TxIsLiving.ReadOnly = true;
            this.TxIsLiving.Size = new System.Drawing.Size(80, 23);
            this.TxIsLiving.TabIndex = 22;
            this.TxIsLiving.Text = "0";
            // 
            // LPass
            // 
            this.LPass.AutoSize = true;
            this.LPass.Location = new System.Drawing.Point(5, 228);
            this.LPass.Name = "LPass";
            this.LPass.Size = new System.Drawing.Size(30, 15);
            this.LPass.TabIndex = 21;
            this.LPass.Text = "Pass";
            // 
            // TxPass
            // 
            this.TxPass.BackColor = System.Drawing.Color.Gold;
            this.TxPass.Location = new System.Drawing.Point(133, 225);
            this.TxPass.Name = "TxPass";
            this.TxPass.ReadOnly = true;
            this.TxPass.Size = new System.Drawing.Size(80, 23);
            this.TxPass.TabIndex = 20;
            this.TxPass.Text = "0";
            // 
            // BStep
            // 
            this.BStep.BackColor = System.Drawing.Color.SpringGreen;
            this.BStep.Location = new System.Drawing.Point(142, 486);
            this.BStep.Name = "BStep";
            this.BStep.Size = new System.Drawing.Size(55, 23);
            this.BStep.TabIndex = 19;
            this.BStep.Text = "Step";
            this.BStep.UseVisualStyleBackColor = false;
            this.BStep.Click += new System.EventHandler(this.BStep_Click);
            // 
            // BExit
            // 
            this.BExit.BackColor = System.Drawing.Color.Coral;
            this.BExit.Location = new System.Drawing.Point(20, 544);
            this.BExit.Name = "BExit";
            this.BExit.Size = new System.Drawing.Size(55, 23);
            this.BExit.TabIndex = 18;
            this.BExit.Text = "Exit";
            this.BExit.UseVisualStyleBackColor = false;
            this.BExit.Click += new System.EventHandler(this.BExit_Click);
            // 
            // TxLog
            // 
            this.TxLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TxLog.Location = new System.Drawing.Point(3, 573);
            this.TxLog.Multiline = true;
            this.TxLog.Name = "TxLog";
            this.TxLog.ReadOnly = true;
            this.TxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxLog.Size = new System.Drawing.Size(210, 60);
            this.TxLog.TabIndex = 15;
            // 
            // BStop
            // 
            this.BStop.BackColor = System.Drawing.Color.Coral;
            this.BStop.Location = new System.Drawing.Point(20, 515);
            this.BStop.Name = "BStop";
            this.BStop.Size = new System.Drawing.Size(55, 23);
            this.BStop.TabIndex = 17;
            this.BStop.Text = "Stop";
            this.BStop.UseVisualStyleBackColor = false;
            this.BStop.Click += new System.EventHandler(this.BStop_Click);
            // 
            // BRun
            // 
            this.BRun.BackColor = System.Drawing.Color.SpringGreen;
            this.BRun.Location = new System.Drawing.Point(81, 486);
            this.BRun.Name = "BRun";
            this.BRun.Size = new System.Drawing.Size(55, 23);
            this.BRun.TabIndex = 16;
            this.BRun.Text = "Run";
            this.BRun.UseVisualStyleBackColor = false;
            this.BRun.Click += new System.EventHandler(this.BRun_Click);
            // 
            // TxPercent
            // 
            this.TxPercent.Location = new System.Drawing.Point(133, 32);
            this.TxPercent.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TxPercent.Name = "TxPercent";
            this.TxPercent.Size = new System.Drawing.Size(80, 23);
            this.TxPercent.TabIndex = 14;
            this.TxPercent.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.TxPercent.ValueChanged += new System.EventHandler(this.TxPercent_ValueChanged);
            // 
            // BReset
            // 
            this.BReset.BackColor = System.Drawing.Color.Gold;
            this.BReset.Location = new System.Drawing.Point(19, 486);
            this.BReset.Name = "BReset";
            this.BReset.Size = new System.Drawing.Size(55, 23);
            this.BReset.TabIndex = 13;
            this.BReset.Text = "Reset";
            this.BReset.UseVisualStyleBackColor = false;
            this.BReset.Click += new System.EventHandler(this.BReset_Click);
            // 
            // LInitPercent
            // 
            this.LInitPercent.AutoSize = true;
            this.LInitPercent.Location = new System.Drawing.Point(3, 35);
            this.LInitPercent.Name = "LInitPercent";
            this.LInitPercent.Size = new System.Drawing.Size(98, 15);
            this.LInitPercent.TabIndex = 12;
            this.LInitPercent.Text = "Initial Percentage";
            // 
            // LWBlocks
            // 
            this.LWBlocks.AutoSize = true;
            this.LWBlocks.Location = new System.Drawing.Point(3, 122);
            this.LWBlocks.Name = "LWBlocks";
            this.LWBlocks.Size = new System.Drawing.Size(89, 15);
            this.LWBlocks.TabIndex = 10;
            this.LWBlocks.Text = "Width in Blocks";
            // 
            // TxWBlocks
            // 
            this.TxWBlocks.BackColor = System.Drawing.Color.Gold;
            this.TxWBlocks.Location = new System.Drawing.Point(133, 119);
            this.TxWBlocks.Name = "TxWBlocks";
            this.TxWBlocks.ReadOnly = true;
            this.TxWBlocks.Size = new System.Drawing.Size(80, 23);
            this.TxWBlocks.TabIndex = 9;
            this.TxWBlocks.Text = "0";
            // 
            // LHBlocks
            // 
            this.LHBlocks.AutoSize = true;
            this.LHBlocks.Location = new System.Drawing.Point(3, 151);
            this.LHBlocks.Name = "LHBlocks";
            this.LHBlocks.Size = new System.Drawing.Size(93, 15);
            this.LHBlocks.TabIndex = 8;
            this.LHBlocks.Text = "Height in Blocks";
            // 
            // TxHBlocks
            // 
            this.TxHBlocks.BackColor = System.Drawing.Color.Gold;
            this.TxHBlocks.Location = new System.Drawing.Point(133, 148);
            this.TxHBlocks.Name = "TxHBlocks";
            this.TxHBlocks.ReadOnly = true;
            this.TxHBlocks.Size = new System.Drawing.Size(80, 23);
            this.TxHBlocks.TabIndex = 7;
            this.TxHBlocks.Text = "0";
            // 
            // CkWrap
            // 
            this.CkWrap.AutoSize = true;
            this.CkWrap.Checked = true;
            this.CkWrap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CkWrap.Location = new System.Drawing.Point(133, 177);
            this.CkWrap.Name = "CkWrap";
            this.CkWrap.Size = new System.Drawing.Size(54, 19);
            this.CkWrap.TabIndex = 6;
            this.CkWrap.Text = "Wrap";
            this.CkWrap.UseVisualStyleBackColor = true;
            this.CkWrap.CheckedChanged += new System.EventHandler(this.CkWrap_CheckedChanged);
            // 
            // LWPixels
            // 
            this.LWPixels.AutoSize = true;
            this.LWPixels.Location = new System.Drawing.Point(3, 64);
            this.LWPixels.Name = "LWPixels";
            this.LWPixels.Size = new System.Drawing.Size(85, 15);
            this.LWPixels.TabIndex = 5;
            this.LWPixels.Text = "Width in Pixels";
            // 
            // TxWPixels
            // 
            this.TxWPixels.BackColor = System.Drawing.Color.Yellow;
            this.TxWPixels.Location = new System.Drawing.Point(133, 61);
            this.TxWPixels.Name = "TxWPixels";
            this.TxWPixels.ReadOnly = true;
            this.TxWPixels.Size = new System.Drawing.Size(80, 23);
            this.TxWPixels.TabIndex = 4;
            this.TxWPixels.Text = "0";
            // 
            // LHPixels
            // 
            this.LHPixels.AutoSize = true;
            this.LHPixels.Location = new System.Drawing.Point(3, 93);
            this.LHPixels.Name = "LHPixels";
            this.LHPixels.Size = new System.Drawing.Size(89, 15);
            this.LHPixels.TabIndex = 3;
            this.LHPixels.Text = "Height in Pixels";
            // 
            // TxHPixels
            // 
            this.TxHPixels.BackColor = System.Drawing.Color.Yellow;
            this.TxHPixels.Location = new System.Drawing.Point(133, 90);
            this.TxHPixels.Name = "TxHPixels";
            this.TxHPixels.ReadOnly = true;
            this.TxHPixels.Size = new System.Drawing.Size(80, 23);
            this.TxHPixels.TabIndex = 2;
            this.TxHPixels.Text = "0";
            // 
            // LBlockSize
            // 
            this.LBlockSize.AutoSize = true;
            this.LBlockSize.Location = new System.Drawing.Point(3, 5);
            this.LBlockSize.Name = "LBlockSize";
            this.LBlockSize.Size = new System.Drawing.Size(58, 15);
            this.LBlockSize.TabIndex = 1;
            this.LBlockSize.Text = "Block size";
            // 
            // NBlockSize
            // 
            this.NBlockSize.Location = new System.Drawing.Point(133, 3);
            this.NBlockSize.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.NBlockSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NBlockSize.Name = "NBlockSize";
            this.NBlockSize.Size = new System.Drawing.Size(80, 23);
            this.NBlockSize.TabIndex = 0;
            this.NBlockSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NBlockSize.ValueChanged += new System.EventHandler(this.NBlockSize_ValueChanged);
            // 
            // Timer
            // 
            this.Timer.Interval = 600;
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // YALife
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(1027, 636);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Frame);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "YALife";
            this.Text = "YALife";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.YALife_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.Frame)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NBlockSize)).EndInit();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.CheckBox CkOnce;
        private System.Windows.Forms.Button BLicense;
        private System.Windows.Forms.Button BAbout;
        private System.Windows.Forms.TextBox txtPassTimer;
        private System.Windows.Forms.Label LabPassTimer;
    }
}

