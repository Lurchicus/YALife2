using System;
using System.Drawing;
using System.Windows.Forms;

namespace YALife
{
    /// <summary>
    /// Yet Another Life Program (based on Conway's Game of Life) by Dan Rhea
    /// I wrote this to try out WinForms on the VS 2022 Preview. I popped this
    /// into my GitHub repository (public)
    /// 
    /// 1.0.1.0  08/12/2021 DWR Initial version
    /// 1.0.2.0  08/12/2021 DWR Code and comments cleanup pass 1
    /// 1.0.3.0  08/24/2021 DWR Code and comments cleanup pass 2 (not checked in)
    /// 1.0.4.0  08/29/2021 DWR Added a color gradient to color persistant cells. The
    ///                         gradient is pretty bland. Need something better.
    /// 1.0.5.0  09/10/2021 DWR Tweak pixel coloring, some cleanup (not checked in)
    /// 1.0.6.0  09/25/2021 DWR Implemented pixel coloring (continous cycle). This new
    ///                         method is from Davide Dolla on StackOverflow and works
    ///                         exactly the way I want.
    /// 1.0.7.0  09/28/2021 DWR Added a checkbox to control if persistance colors cycle
    ///                         only once or continously.
    /// 1.0.8.0  10/02/2021 DWR Cleaned up some cryptic comments in DoLife()
    ///                         - Expanded some of the more important comments.
    /// 1.0.9.0  10/16/2021 DWR Added comments to the ColorHeatMap class (Gradient.cs)
    /// 1.0.10.0 10/16/2021 DWR First compile using VS 2022 RC 1
    /// 1.0.11.0 10/19/2021 DWR Added a file reader form to display the license and a 
    ///                         button on the UI panel to load the reader form with the
    ///                         GPL 3 license.
    /// 1.0.12.0 10/25/2021 DWR Added a pass timer to show how ling (in seconds) it takes
    ///                         to get through both DoLife() and DrawLife().
    ///                         - Added an about/spalsh screen (preliminary).
    /// 1.0.13.0 11/05/2021 DWR Removed the reset debounce as it blocked a proper response 
    ///                         to screen size changes. With the debounce, it was seeing and
    ///                         responding to the vertical changes but not the horizontal.
    ///                         - Set frame (holds the bitmap) to a minimum of 1, 1 so we 
    ///                         never try to create a 0x0 bitmap when minimized. For now 
    ///                         coming back from a minimize forces a reset. In the future, 
    ///                         if I can detect a minimize and restore, I could try to 
    ///                         save the bitmap where the size of Frame doesn't affect it 
    ///                         and restore it when we un-minimize the form. 
    /// 1.0.14.0 11/07/2021 DWR Tweaked the log background.
    /// 1.0.15.0 11/09/2021 DWR The initial CleanPaper() in DrawLife() was a total waste of 
    ///                         time. Since we repaint the whole bitmap anyway, clearing it 
    ///                         first is not needed. Found using the performance profiler. 
    ///                         Also this version is one the release version of VS 2022.
    /// 
    /// ToDo:
    /// 
    /// 1. A faster way to draw to the screen besides individual setpixels calls. 
    ///    I'll have to research what's possible but still practical for a hobbiest 
    ///    project like this one. 
    ///    I.E. Not buying something like LeadTools for a fun project that
    ///    will never make any money, and isn't intended to... the value is in what I
    ///    learn in using the new Visual Studios 2022 RC 1 and .NET 6 RC 1.
    /// 2. Create a way to import a predefined "life" pattern. If there is a standard
    ///    for this already I'll use that, otherwise I'll create one... maybe a text
    ///    or json formatted file giving the X/Y coordinates of the starting live
    ///    cell locations... we can then single step or run them. A form to edit 
    ///    these files would be nice as well. Possibly a combination of a text editor
    ///    and a gui interface (mouse editing) of the predefined patterns.
    /// 
    /// </summary>
    public partial class YALife : Form
    {
        bool BWrap;         // Wrap around or bounded universe
        bool StopIt;        // Stop flag
        bool Stopped;       // Stop state
        bool Once;          // Cycle colors once or continiously
        int ITop;           // Tracks the top of the image frame
        int ILeft;          // Tracls the left of the image frame
        int IInitPercent;   // Initial live percentage
        int IHPixels;       // Height in pixels
        int IWPixels;       // Width in pixels
        int IHBlocks;       // Height in blocks
        int IWBlocks;       // Width in blocks
        int IBlockSize;     // Pixels in block
        int IPass;          // Pass counter
        int IBirth;         // Count of births in a pass
        int ILive;          // Count of "stay alives" in a pass
        int ILonely;        // Count of lonely deaths in a pass
        int ICrowd;         // Count of crowded deaths in a pass
        int IEmpty;         // Count of "Stay empty" cases in a pass
        int IsLiving;       // Count of all living cells
        int IsEmpty;        // Count of all empty cells
        int[,]? ILife;      // Life matrix
        int[,]? ISave;      // Save matrix
        DateTime StartMS;   // Start timer
        DateTime StopMS;    // End timer
        TimeSpan ElapsedMS; // Elapsed timer

        Bitmap? Paper;                        // Bitmap to draw on
        readonly Random RNG = new();    // Object to pull RNG values
        ColorHeatMap CMap = new(); // Color map for ColorHeatMap class
        readonly string LicenseFile = "gnu_gpl3.txt";   // GPL 3 license file

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
            // Stop the load delay timer and finish init (Reset())
            Timer.Enabled = false;
            Splash SplashScreen = new(6000, Width, Height, Top, Left);
            SplashScreen.ShowDialog();
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
        /// Switch the once flag if needed... lets us change it while the program
        /// is running
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CkOnce_CheckedChanged(object sender, EventArgs e)
        {
            if (CkOnce.Checked)
            {
                Once = true;
            }
            else
            {
                Once = false;
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
        /// Display the license to the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BLicense_Click(object sender, EventArgs e)
        {
            FileReader ReaderForm = new(LicenseFile, Width, Height, Top, Left);
            ReaderForm.ShowDialog();
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
            // Focus the reset button but don't actually reset
            BReset.Focus();
        }

        /// <summary>
        /// Detect a minimize
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YALife_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                if (!Stopped && !StopIt)
                {
                    StopIt = true;
                }
                Reset();
            }
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
        /// Show the about/splash screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BAbout_Click(object sender, EventArgs e)
        {
            Splash SplashScreen = new(0, Width, Height, Top, Left);
            SplashScreen.ShowDialog();
        }

        /// <summary>
        /// Reset and reinitialize the variables and the working "life" array
        /// </summary>
        private void Reset()
        {
            // Get and report pixels we can draw in
            IHPixels = Frame.Height;
            IWPixels = Frame.Width;
            TxHPixels.Text = IHPixels.ToString();
            TxWPixels.Text = IWPixels.ToString();

            // BlockSize is very important to how I designed this program. It is
            // in effect a zoom, as it allow the cells/locations displayed to be
            // larger than a single pixel. I'm allowing a block size of up to 16
            // (that can be increased by changing the maximum size set in 
            // NBlockSize). If block size too big you will eventually get 
            // exceptions in DrawLife(). 
            //
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

            // Clear and recreate bitmap (I just call it paper
            // because I can)
            if (Frame.Image != null)
            {
                Frame.Image.Dispose();
            }
            //if (Mini) { return; }
            Paper = MakePaper();
            CleanPaper(Paper);
            Frame.Image = Paper;

            // Recreate life array (init to 0) and get the 
            // initial population percentage.
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
            Once = (CkOnce.Checked);
            BRun.Focus();

            // Update the UI
            Application.DoEvents();
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
            if (RefPaper == null) { return; }  
            //TxLog.AppendText("Paper: " + RefPaper.Width.ToString() + "x" + RefPaper.Height.ToString()+"\r\n");
            for (int Wid = 0; Wid < RefPaper.Width; Wid++)
            {
                for (int Hei = 0; Hei < RefPaper.Height; Hei++)
                {
                    RefPaper.SetPixel(Wid, Hei, Color.Black);
                }
            }
        }

        /// <summary>
        /// Apply Conway's Life rules to the "life" array. Supports an 
        /// unbounded and wrap-around universe.
        /// </summary>
        private void DoLife()
        {
            StartMS = DateTime.Now;
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

            // Scan through the "life" array (taking the block size into
            // account as well)
            for (int CurW = 0; CurW < IWBlocks; CurW++)
            {
                for (int CurH = 0; CurH < IHBlocks; CurH++)
                {
                    int Friends = 0;

                    if (BWrap)
                    {
                        // Wrap around universe: Things that hit an edge reappear
                        // on the oppisite side.
                        // Look around the current array element to determine
                        // the future of the current location.

                        // North: Width, Height-1
                        W = CurW;
                        H = CurH - 1;
                        if (H < 0) { H = IHBlocks - 1; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // Northeast: Width+1, Height-1
                        W = CurW + 1;
                        H = CurH - 1;
                        if (W == IWBlocks) { W = 0; }
                        if (H < 0) { H = IHBlocks - 1; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // East: Width+1, Height
                        W = CurW + 1;
                        H = CurH;
                        if (W == IWBlocks) { W = 0; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // Southeast: Width+1, Height+1
                        W = CurW + 1;
                        H = CurH + 1;
                        if (W == IWBlocks) { W = 0; }
                        if (H == IHBlocks) { H = 0; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // South: Width, Height+1
                        W = CurW;
                        H = CurH + 1;
                        if (H == IHBlocks) { H = 0; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // Southwest: Width-1, Height+1
                        W = CurW - 1;
                        H = CurH + 1;
                        if (W < 0) { W = IWBlocks - 1; }
                        if (H == IHBlocks) { H = 0; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // West: Width-1, Height
                        W = CurW - 1;
                        H = CurH;
                        if (W < 0) { W = IWBlocks - 1; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // Northwest: Width-1, Height-1
                        W = CurW - 1;
                        H = CurH - 1;
                        if (W < 0) { W = IWBlocks - 1; }
                        if (H < 0) { H = IHBlocks - 1; }
                        if (ILife[W, H] >= 1) { Friends++; }
                    }
                    else
                    {
                        // Unbound universe: Things that hit the edge, keep going 
                        // (well actually they stop at the edge, but in theory
                        // they could keep going).
                        // Look around the current array element to determine
                        // what happens to this location.

                        // North: Width, Height-1
                        W = CurW;
                        H = CurH - 1;
                        if (H < 0) { H = 0; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // Northeast: Width+1, Height-1
                        W = CurW + 1;
                        H = CurH - 1;
                        if (W == IWBlocks) { W = IWBlocks - 1; }
                        if (H < 0) { H = 0; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // East: Width+1, Height
                        W = CurW + 1;
                        H = CurH;
                        if (W == IWBlocks) { W = IWBlocks - 1; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // Southeast: Width+1, Height+1
                        W = CurW + 1;
                        H = CurH + 1;
                        if (W == IWBlocks) { W = IWBlocks - 1; }
                        if (H == IHBlocks) { H = IHBlocks - 1; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // South: Width, Height+1
                        W = CurW;
                        H = CurH + 1;
                        if (H == IHBlocks) { H = IHBlocks - 1; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // Southwest: Width-1, Height+1
                        W = CurW - 1;
                        H = CurH + 1;
                        if (W < 0) { W = 0; }
                        if (H == IHBlocks) { H = IHBlocks - 1; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // West: Width-1, Height
                        W = CurW - 1;
                        H = CurH;
                        if (W < 0) { W = 0; }
                        if (ILife[W, H] >= 1) { Friends++; }

                        // Northwest: Width-1, Height-1
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
                                // We have one or less neibours so we are too loney to live (/sadface)
                                ISave[CurW, CurH] = 0;
                                ILonely++;
                                break;
                            case 2:
                                // We have two neibours, happy, we live on
                                ISave[CurW, CurH] = (ILife[CurW, CurH] + 1);  // Ha ha ha ha, stay'n alive, stay'n alive...
                                if (ILife[CurW, CurH] >= 255)
                                {
                                    ISave[CurW, CurH] = (Once) ? 255 : 1;
                                    ILife[CurW, CurH] = (Once) ? 255 : 1;
                                }
                                ILive++;
                                break;
                            case 3:
                                // We have three neibours, happy, we survive another pass
                                ISave[CurW, CurH] = (ILife[CurW, CurH] + 1);  // Ha ha ha ha, stay'n alive, stay'n alive...
                                if (ILife[CurW, CurH] >= 255)
                                {
                                    ISave[CurW, CurH] = (Once) ? 255 : 1;
                                    ILife[CurW, CurH] = (Once) ? 255 : 1;
                                }
                                ILive++;
                                break;
                            case > 3:
                                // We have more than three neibours, too many, we die (overpopulation)
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
                                    ISave[CurW, CurH] = (Once) ? 255 : 1;
                                    ILife[CurW, CurH] = (Once) ? 255 : 1;
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

            // This clear was a total waste of time. Since we repaint the whole
            // bitmap anyway, clearing it first is not needed. 
            //CleanPaper(Paper);

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
                                    // Cndx contains a value from 1 to 255 that is actually the number
                                    // of passes this cell/location has survived. We take this value
                                    // and use it to map into a gradient/color map and we use this
                                    // 'color' as the color we paint the cell/location. It's not really
                                    // usefull for the program but it takes what is effectively a 
                                    // binary display (alive or empty) and adds some interest by letting
                                    // us know which cells are persistant and which are not.
                                    // Note that the "rules" part of DoLife() sets and resets (or clamps)
                                    // the value in the cell (FYI: Cndx is color index).
                                    Double Cndx = (double)ILife[Wid, Hei];
                                    if (Cndx > 255) { Cndx = 255; }
                                    Color Clr = CMap.GetColorForValue(Cndx, (double)256);
                                    Paper.SetPixel(IW, IH, Clr);
                                }
                                else
                                {
                                    // Empty is always black
                                    Paper.SetPixel(IW, IH, Color.Black);
                                }
                            }
                            else
                            {
                                // Log if we overflow our bitmap. I may change this to just throw an exception later.
                                TxLog.AppendText("Ovfl Err: IW:" + IW.ToString() + " IH:" + IH.ToString() + "\r\n");
                            }
                        }
                    }
                }
            }

            // Show the new bitmap and update the UI
            Frame.Image = Paper;
            StopMS = DateTime.Now;
            ElapsedMS = StopMS - StartMS;
            //TxLog.AppendText("Pass: " + IPass.ToString() + " | " + ElapsedMS.TotalSeconds + "s\r\n");
            txtPassTimer.Text = ElapsedMS.TotalSeconds.ToString(); 
            Application.DoEvents();
        }
    }
}
