using System;
using System.Drawing;
using System.Windows.Forms;
using static YALife.YALife;

namespace YALife
{
    /// <summary>
    /// Yet Another Life Program (based on Conway's Game of Life) by Dan Rhea
    /// I wrote this to try out WinForms on the VS 2022 Preview. I popped this
    /// into my GitHub repository (public)
    /// </summary>
    public partial class YALife : Form
    {
        bool BWrap;
        bool StopIt;
        bool Stopped;
        bool Resetting;
        int IHPixels;
        int IWPixels;
        int IHBlocks;
        int IWBlocks;
        int IBlockSize;
        int IBirth;
        int ILive;
        int ILonely;
        int ICrowd;
        int IEmpty;
        int IsLiving;
        int IsEmpty;
        int IPass;
        int IInitPercent;
        int ITop;
        int ILeft;
        int[,]? ILife;
        int[,]? ISave;
        Bitmap? Paper;
        List<Gradient> ArrGB = new List<Gradient>();
        readonly Random RNG = new();

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
            int A = 255;
            int R = 64+32;
            int G = 96+32;
            int B = 128+32;
            //Color C = Color.FromArgb(A, R, G, B);
            for (int Shade=0; Shade<256; Shade++)
            {
                Color C = Color.FromArgb(A, R, G, B);
                Gradient CMap = new Gradient { Alpha = A, aRGB = C, Red = R, Green = G, Blue = B };
                ArrGB.Add(CMap);
                if (++R > 255) { R = 64 + 32; }
                if (++G > 255) { G = 64 + 32; }
                if (++B > 255) { B = 64 + 32; }
            }
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

                        // N:   W       H-1
                        W = CurW;
                        H = CurH - 1;
                        if (H < 0) { H = IHBlocks - 1; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // NE:  W+1     H-1
                        W = CurW + 1;
                        H = CurH - 1;
                        if (W == IWBlocks) { W = 0; }
                        if (H < 0) { H = IHBlocks - 1; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // E:   W+1     H
                        W = CurW + 1;
                        H = CurH;
                        if (W == IWBlocks) { W = 0; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // SE:  W+1     H+1
                        W = CurW + 1;
                        H = CurH + 1;
                        if (W == IWBlocks) { W = 0; }
                        if (H == IHBlocks) { H = 0; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // S:   W       H+1
                        W = CurW;
                        H = CurH + 1;
                        if (H == IHBlocks) { H = 0; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // SW:  W-1     H+1
                        W = CurW - 1;
                        H = CurH + 1;
                        if (W < 0) { W = IWBlocks - 1; }
                        if (H == IHBlocks) { H = 0; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // W:   W-1     H
                        W = CurW - 1;
                        H = CurH;
                        if (W < 0) { W = IWBlocks - 1; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // NW:  W-1     H-1
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

                        // N:   W       H-1
                        W = CurW;
                        H = CurH - 1;
                        if (H < 0) { H = 0; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // NE:  W+1     H-1
                        W = CurW + 1;
                        H = CurH - 1;
                        if (W == IWBlocks) { W = IWBlocks - 1; }
                        if (H < 0) { H = 0; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // E:   W+1     H
                        W = CurW + 1;
                        H = CurH;
                        if (W == IWBlocks) { W = IWBlocks - 1; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // SE:  W+1     H+1
                        W = CurW + 1;
                        H = CurH + 1;
                        if (W == IWBlocks) { W = IWBlocks - 1; }
                        if (H == IHBlocks) { H = IHBlocks - 1; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // S:   W       H+1
                        W = CurW;
                        H = CurH + 1;
                        if (H == IHBlocks) { H = IHBlocks - 1; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // SW:  W-1     H+1
                        W = CurW - 1;
                        H = CurH + 1;
                        if (W < 0) { W = 0; }
                        if (H == IHBlocks) { H = IHBlocks - 1; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // W:   W-1     H
                        W = CurW - 1;
                        H = CurH;
                        if (W < 0) { W = 0; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // NW:  W-1     H-1
                        W = CurW - 1;
                        H = CurH - 1;
                        if (W < 0) { W = 0; }
                        if (H < 0) { H = 0; }
                        if (ILife[W, H] >= 1) { Friends++; }
                    }

                    if (ILife[CurW, CurH] >= 1)
                    {
                        // Live cell rules
                        switch (Friends)
                        {
                            case < 2:
                                ISave[CurW, CurH] = 0;  // Lonely death
                                ILonely++;
                                break;
                            case 2:
                                ISave[CurW, CurH] = (ILife[CurW, CurH] + 1);  // Ha ha ha ha, stay'n alive, stay'n alive...
                                if (ILife[CurW, CurH] >= 255)
                                {
                                    ISave[CurW, CurH] = 1;
                                    ILife[CurW, CurH] = 1;
                                }
                                ILive++;
                                break;
                            case 3:
                                ISave[CurW, CurH] = (ILife[CurW, CurH] + 1);  // Ha ha ha ha, stay'n alive, stay'n alive...
                                if (ILife[CurW, CurH] >= 255)
                                {
                                    ISave[CurW, CurH] = 1;
                                    ILife[CurW, CurH] = 1;
                                }
                                ILive++;
                                break;
                            case > 3:
                                ISave[CurW, CurH] = 0;  // Crowded death
                                ICrowd++;
                                break;
                        }
                    }
                    else
                    {
                        // Empty cell rules
                        switch (Friends)
                        {
                            case < 3:
                                ISave[CurW, CurH] = 0;  // Stay empty
                                IEmpty++;
                                break;
                            case 3:
                                ISave[CurW, CurH] = 1;  // Birth!
                                if (ILife[CurW, CurH] >= 255)
                                {
                                    ISave[CurW, CurH] = 255;
                                    ILife[CurW, CurH] = 255;
                                }
                                IBirth++;
                                break;
                            case > 3:
                                ISave[CurW, CurH] = 0;  // Stay empty
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
            int Floof = 0;
            for (int CurW = 0; CurW < IWBlocks; CurW++)
            {
                for (int CurH = 0; CurH < IHBlocks; CurH++)
                {
                    ILife[CurW, CurH] = ISave[CurW, CurH];
                    //if (ILife[CurW, CurH] > 1)
                    //{
                    //    Floof++;
                    //}
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
            int G1 = 0;

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
                                    int Cndx = ILife[Wid, Hei];
                                    //if (Cndx > 1)
                                    //{
                                    //    G1++;
                                    //}
                                    Color Clr = ArrGB[Cndx].aRGB;
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

        /// <summary>
        /// Class to hold color definitions and make up a gradient Color Map in a List objecy
        /// </summary>
        public class Gradient
        {
            public int Alpha;   // Alpha channel value
            public int Red;     // Red channel
            public int Green;   // Green channel
            public int Blue;    // Blue channel
            public Color aRGB;  // ARGB color
        }
    }
}
