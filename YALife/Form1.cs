using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace YALife
{
    /// <summary>
    /// Yet Another Life Program (based on Conway's Game of Life) by Dan Rhea
    /// I wrote this to try out WinForms on the VS 2022 Preview. I popped this
    /// into my GitHub repository (public)
    /// 
    /// 1.0.6.0 09/25/2021 DWR Implemented pixel coloring (continous cycle)
    /// 
    /// </summary>
    public partial class YALife : Form
    {
        bool BWrap;         // Wrap around or bounded universe
        bool StopIt;        // Stop flag
        bool Stopped;       // Stop state
        bool Resetting;     // Reseting flag (used for debounce)
        int IHPixels;       // Height in pixels
        int IWPixels;       // Width in pixels
        int IHBlocks;       // Height in blocks
        int IWBlocks;       // Width in blocks
        int IBlockSize;     // Pixels in block
        int IBirth;         // Count of births in pass
        int ILive;          // Count of "stay alives" in pass
        int ILonely;        // Count of lonely deaths in pass
        int ICrowd;         // Count of crowded deaths in pass
        int IEmpty;         // Count of "Stay empty" cases in a pass
        int IsLiving;       // Count of all living cells
        int IsEmpty;        // Count of all empty cells
        int IPass;          // Pass counter
        int IInitPercent;   // Initial live percentage
        int ITop;           // Tracks the top of the image frame
        int ILeft;          // Tracls the left of the image frame
        int[,]? ILife;      // Life matrix
        int[,]? ISave;      // Save matrix
        Bitmap? Paper;      // Bitmap to draw on
        readonly Random RNG = new();
        ColorHeatMap CMap = new ColorHeatMap(); 

        /// <summary>
        /// YALife constructor
        /// </summary>
        public YALife()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Start loading the UI and kick off a timer to finish initializing things
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // Give the form a bit of time to draw
            ITop = Frame.Top;
            ILeft = Frame.Left;
            Timer.Enabled = true;
        }

        /// <summary>
        /// Timer event finishes initialization
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            Timer.Enabled = false;

            // Initialize the gradient 
            //int A = 255;
            //int R = 64 + 32;
            //int G = 96 + 32;
            //int B = 128 + 32;

            //for (int Shade=0; Shade<256; Shade++)
            //{
            //    Color C = Color.FromArgb(A, R, G, B);
            //    Gradient CMap = new Gradient { Alpha = A, aRGB = C, Red = R, Green = G, Blue = B };
            //    ArrGB.Add(CMap);
            //    R++; //RNG.Next(1, 2);
            //    if (R > 255) { R = 96; }
            //    G++; //RNG.Next(1, 2);
            //    if (G > 255) { G = 96; }
            //    B++; //RNG.Next(1, 2);
            //    if (B > 255) { B = 96; }
            //}

            Reset();
        }

        /// <summary>
        /// When we change our block size, reset everything
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NBlockSize_ValueChanged(object sender, EventArgs e)
        {
            Reset();
        }

        /// <summary>
        /// When we resize the image, reset everything
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_SizeChanged(object sender, EventArgs e)
        {
            Reset();
        }

        /// <summary>
        /// If we reset, yes, reset everything
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        /// <summary>
        /// If we switch between a wrapping or bounded universe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CkWrap_CheckedChanged(object sender, EventArgs e)
        {
            if (CkWrap.Checked)
            {
                BWrap = true;
            }
            else
            {
                BWrap = false;
            }
        }

        /// <summary>
        /// If we change our initial percentage, reset everything
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxPercent_ValueChanged(object sender, EventArgs e)
        {
            Reset();
        }

        /// <summary>
        /// Start running until stopped
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BRun_Click(object sender, EventArgs e)
        {
            Stopped = false;
            StopIt = false;
            BStop.Focus();
            while (!Stopped)
            {
                IPass++;
                DoLife();
                if (StopIt) { Stopped = true; }
            }
        }

        /// <summary>
        /// If we are running, stop the run
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BStop_Click(object sender, EventArgs e)
        {
            if (!Stopped && !StopIt)
            {
                StopIt = true;
            }
            BReset.Focus();
        }

        /// <summary>
        /// Exit the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Single step each pass
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BStep_Click(object sender, EventArgs e)
        {
            BStep.Focus();
            IPass++;
            DoLife();
        }

        /// <summary>
        /// Reset and reinitialize the variables and the working "life" array
        /// </summary>
        private void Reset()
        {
            // Debounce reset
            if (Resetting) { return; }
            Resetting = true;

            // Get and report pixels we can draw in
            IHPixels = Frame.Height;
            IWPixels = Frame.Width;
            TxHPixels.Text = IHPixels.ToString();
            TxWPixels.Text = IWPixels.ToString();

            // Get the requested block size
            IBlockSize = (int)NBlockSize.Value;

            // Calculate and report block size
            IHBlocks = IHPixels / IBlockSize;
            IWBlocks = IWPixels / IBlockSize;
            TxHBlocks.Text = IHBlocks.ToString();
            TxWBlocks.Text = IWBlocks.ToString();

            // Manage "Frame" size
            Frame.Top = ITop;
            Frame.Left = ILeft;
            Frame.Width = this.ClientSize.Width - ILeft;
            Frame.Height = this.ClientSize.Height;
            Application.DoEvents();

            // Clear and recreate bitmap
            if (Frame.Image != null)
            {
                Frame.Image.Dispose();
            }
            Paper = MakePaper();
            CleanPaper(Paper);
            Frame.Image = Paper;

            // Clear and recreate life array (init to 0)
            IInitPercent = (int)TxPercent.Value;
            ILife = new int[IWBlocks, IHBlocks];
            ISave = new int[IWBlocks, IHBlocks];
            
            // Initialize the life array randomly based on a desired starting
            // percentage of live cells
            for (int IW = 0; IW < IWBlocks; IW++)
            {
                for (int IH = 0; IH < IHBlocks; IH++)
                {
                    ISave[IW, IH] = 0;
                    int IRNG = RNG.Next(1, 101);
                    if (IRNG <= IInitPercent)
                    {
                        ILife[IW, IH] = 1;
                    }
                    else
                    {
                        ILife[IW, IH] = 0;
                    }
                }
            }
            DrawLife();

            //TxLog.Visible = false;
            //TxLog.AppendText("ILife: " + IWBlocks.ToString() + "x" + IHBlocks.ToString() + "\r\n");
            StopIt = false;
            Stopped = true;
            IPass = 1;
            BWrap = (CkWrap.Checked);
            BRun.Focus();

            Application.DoEvents();
            Resetting = false;
        }

        /// <summary>
        /// Creates and returns a freash new bitmap
        /// </summary>
        /// <returns>Bitmap</returns>
        private Bitmap MakePaper()
        {
            return new Bitmap(IWPixels, IHPixels, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
        }

        /// <summary>
        /// Sets all the pixels in the supplied bitmap to white
        /// </summary>
        /// <param name="RefPaper">A bitmap</param>
        private static void CleanPaper(Bitmap RefPaper)
        {
            //TxLog.AppendText("Paper: " + RefPaper.Width.ToString() + "x" + RefPaper.Height.ToString()+"\r\n");
            for (int Wid = 0; Wid < RefPaper.Width; Wid++)
            {
                for (int Hei = 0; Hei < RefPaper.Height; Hei++)
                {
                    RefPaper.SetPixel(Wid, Hei, Color.White);
                }
            }
        }


        /// <summary>
        /// Apply Conway's Life rules to the "life" array. Supports a 
        /// bounded and wrap around universe.
        /// </summary>
        private void DoLife()
        {
            int W;
            int H;

            if (ILife == null) return;
            if (ISave == null) return;

            // Update the pass counter in the UI
            TxPass.Text = IPass.ToString();

            // Zero out the detail counters
            IBirth = 0;
            ILive = 0;
            ILonely = 0;
            ICrowd = 0;
            IEmpty = 0;

            // Clear out the save array
            for (int CurW = 0; CurW < IWBlocks; CurW++)
            {
                for (int CurH = 0; CurH < IHBlocks; CurH++)
                {
                    ISave[CurW, CurH] = 0;
                }
            }

            // Scan through the "life" array
            for (int CurW = 0; CurW < IWBlocks; CurW++)
            {
                for (int CurH = 0; CurH < IHBlocks; CurH++)
                {
                    int Friends = 0;

                    if (BWrap)
                    {
                        // Wrap around universe
                        // Look around the current array element to determine
                        // our fate

                        // North:   W       H-1
                        W = CurW;
                        H = CurH - 1;
                        if (H < 0) { H = IHBlocks - 1; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // Northeast:  W+1     H-1
                        W = CurW + 1;
                        H = CurH - 1;
                        if (W == IWBlocks) { W = 0; }
                        if (H < 0) { H = IHBlocks - 1; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // East:   W+1     H
                        W = CurW + 1;
                        H = CurH;
                        if (W == IWBlocks) { W = 0; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // Southeast:  W+1     H+1
                        W = CurW + 1;
                        H = CurH + 1;
                        if (W == IWBlocks) { W = 0; }
                        if (H == IHBlocks) { H = 0; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // South:   W       H+1
                        W = CurW;
                        H = CurH + 1;
                        if (H == IHBlocks) { H = 0; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // Southwest:  W-1     H+1
                        W = CurW - 1;
                        H = CurH + 1;
                        if (W < 0) { W = IWBlocks - 1; }
                        if (H == IHBlocks) { H = 0; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // West:   W-1     H
                        W = CurW - 1;
                        H = CurH;
                        if (W < 0) { W = IWBlocks - 1; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // Northwest:  W-1     H-1
                        W = CurW - 1;
                        H = CurH - 1;
                        if (W < 0) { W = IWBlocks - 1; }
                        if (H < 0) { H = IHBlocks - 1; }
                        if (ILife[W, H] >= 1) { Friends++; }
                    }
                    else
                    {
                        // Bounded universe
                        // Look around the current array element to determine
                        // our fate

                        // North:   W       H-1
                        W = CurW;
                        H = CurH - 1;
                        if (H < 0) { H = 0; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // Northeast:  W+1     H-1
                        W = CurW + 1;
                        H = CurH - 1;
                        if (W == IWBlocks) { W = IWBlocks - 1; }
                        if (H < 0) { H = 0; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // East:   W+1     H
                        W = CurW + 1;
                        H = CurH;
                        if (W == IWBlocks) { W = IWBlocks - 1; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // Southeast:  W+1     H+1
                        W = CurW + 1;
                        H = CurH + 1;
                        if (W == IWBlocks) { W = IWBlocks - 1; }
                        if (H == IHBlocks) { H = IHBlocks - 1; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // South:   W       H+1
                        W = CurW;
                        H = CurH + 1;
                        if (H == IHBlocks) { H = IHBlocks - 1; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // Southwest:  W-1     H+1
                        W = CurW - 1;
                        H = CurH + 1;
                        if (W < 0) { W = 0; }
                        if (H == IHBlocks) { H = IHBlocks - 1; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // West:   W-1     H
                        W = CurW - 1;
                        H = CurH;
                        if (W < 0) { W = 0; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // Northwest:  W-1     H-1
                        W = CurW - 1;
                        H = CurH - 1;
                        if (W < 0) { W = 0; }
                        if (H < 0) { H = 0; }
                        if (ILife[W, H] >= 1) { Friends++; }
                    }

                    if (ILife[CurW, CurH] >= 1)
                    {
                        // Live cell rules (current cell is alive)
                        switch (Friends)
                        {
                            case < 2:
                                // We have one or less neibours so we are too loney to live
                                ISave[CurW, CurH] = 0;
                                ILonely++;
                                break;
                            case 2:
                                // We have two neibours, happy, we live on
                                ISave[CurW, CurH] = (ILife[CurW, CurH] + 1);  // Ha ha ha ha, stay'n alive, stay'n alive...
                                if (ILife[CurW, CurH] >= 255)
                                {
                                    ISave[CurW, CurH] = 1;
                                    ILife[CurW, CurH] = 1;
                                }
                                ILive++;
                                break;
                            case 3:
                                // We have three neibours, happy, we survive another pass
                                ISave[CurW, CurH] = (ILife[CurW, CurH] + 1);  // Ha ha ha ha, stay'n alive, stay'n alive...
                                if (ILife[CurW, CurH] >= 255)
                                {
                                    ISave[CurW, CurH] = 1;
                                    ILife[CurW, CurH] = 1;
                                }
                                ILive++;
                                break;
                            case > 3:
                                // We have more than three neibours, too many, we die
                                ISave[CurW, CurH] = 0; 
                                ICrowd++;
                                break;
                        }
                    }
                    else
                    {
                        // Empty cell rules (current cell is empty)
                        switch (Friends)
                        {
                            case < 3:
                                // Less than three neibours, we stay empty
                                ISave[CurW, CurH] = 0; 
                                IEmpty++;
                                break;
                            case 3:
                                // Three neibours! Birth!
                                ISave[CurW, CurH] = 1;
                                if (ILife[CurW, CurH] >= 255)
                                {
                                    ISave[CurW, CurH] = 1;
                                    ILife[CurW, CurH] = 1;
                                }
                                IBirth++;
                                break;
                            case > 3:
                                // More than three neibours, stay empty
                                ISave[CurW, CurH] = 0;
                                IEmpty++;
                                break;
                        }
                    }
                }
            }

            // Copy the cell info from the "save" array to the "life" array
            // and collect raw alive/empty counts
            IsLiving = 0;
            IsEmpty = 0;
            for (int CurW = 0; CurW < IWBlocks; CurW++)
            {
                for (int CurH = 0; CurH < IHBlocks; CurH++)
                {
                    ILife[CurW, CurH] = ISave[CurW, CurH];
                    if (ILife[CurW, CurH] >= 1)
                    {
                        IsLiving++;
                    }
                    else
                    {
                        IsEmpty++;
                    }
                }
            }

            // Show living and empty cells
            TxIsLiving.Text = IsLiving.ToString();
            TxIsEmpty.Text = IsEmpty.ToString();
            
            // Show details
            TxBirth.Text = IBirth.ToString();
            TxLive.Text = ILive.ToString();
            TxLonely.Text = ILonely.ToString();
            TxCrowd.Text = ICrowd.ToString();
            TxEmpty.Text = IEmpty.ToString();

            // Draw the new bitmap
            DrawLife();
        }

        /// <summary>
        /// Scan the "life" array and use it to generate a bitmap. An array element 
        /// can be scaled from 1 pixel per array element to 16 pixels per array
        /// element. This gives us an ersatz zoom (and it was a fun challange)
        /// </summary>
        private void DrawLife()
        {
            int IWOffSet;
            int IHOffset;
            int IW;
            int IH;

            if (ILife == null) return;
            if (Paper == null) return;

            CleanPaper(Paper);

            // Step through the array
            for (int Wid = 0; Wid < IWBlocks; Wid++)
            {
                for (int Hei = 0; Hei < IHBlocks; Hei++)
                {
                    IWOffSet = Wid * IBlockSize;
                    IHOffset = Hei * IBlockSize;

                    // Create a block of 1 to 16 pixels for each array element
                    for (int W = 0; W < IBlockSize; W++)
                    {
                        for (int H = 0; H < IBlockSize; H++)
                        {
                            IW = W + IWOffSet;
                            IH = H + IHOffset;

                            if (IW < Paper.Width && IH < Paper.Height)
                            {
                                if (ILife[Wid, Hei] >= 1)
                                {
                                    Double Cndx = (double)ILife[Wid, Hei];
                                    if (Cndx > 255) { Cndx = 255; }
                                    Color Clr = CMap.GetColorForValue(Cndx, (double)256);
                                    Paper.SetPixel(IW, IH, Clr);
                                    //Paper.SetPixel(IW, IH, ArrGB[ILife[Wid, Hei]].aRGB);
                                }
                                else
                                {
                                    Paper.SetPixel(IW, IH, Color.Black);
                                }
                            }
                            else
                            {
                                TxLog.AppendText("Ovfl Err: IW:" + IW.ToString() + " IH:" + IH.ToString() + "\r\n");
                            }
                        }
                    }
                }
            }

            // Show the new bitmap
            Frame.Image = Paper;
            Application.DoEvents();
        }
    }
}
