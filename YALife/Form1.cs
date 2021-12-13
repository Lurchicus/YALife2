using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace YALife
{
    /// <summary>
    /// Yet Another Life Program (based on Conway's Game of Life) by Dan Rhea
    /// I wrote this to try out WinForms on the VS 2022 Preview. I popped this
    /// into my GitHub repository (public)
    /// 
    /// I was saddened to learn that we lost Mr. Conway to Covid-19 complications
    /// April 11th of 2020.
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
    ///                         Also this version is on the release version of VS 2022.
    /// 1.0.16.0 11/09/2021 DWR Added a "N0" format to add a thousands seperator for most
    ///                         of the text boxes. Also added the culture parameter (I
    ///                         used currrent, so it's based on how the current windows
    ///                         config is set).
    ///                         - Removed minimize and maximize from the splash screen.
    ///                         - Implemented DirectBitmap which greatly improved my draw
    ///                         times. More optimizations by moving the bitmap create and
    ///                         dispose into the reset logic so we only recreate the 
    ///                         bitmap if we change size or the blocksize (or click Stop
    ///                         or Reset).
    /// 1.0.17.0 11/20/2021 DWR Added "(zoom)" to block size label text.
    /// 1.0.18.0 11/26/2021 DWR Added a "mode" dropdown that will control color cycling
    ///                         or a single non cycling color.
    ///                         - Removed no longer used functions, methods and events.
    /// 1.0.19.0 11/26/2021 DWR More optmizations (don't clear the new life array first
    ///                         since we are setting every element anyway). Found a 
    ///                         couple case statements that could be optimized too.
    /// 1.0.20.0 12/08/2021 DWR Did some refactoring in DoLife with the code that 
    ///                         checks for any friends living around us. It's simplified
    ///                         in DoLife but the new 8 functions are a bit jank for now.
    /// 1.0.21.0 12/12/2021 DWR More comment and code tweaking.
    /// 
    /// ToDo:
    /// 1. Create a way to import a predefined "life" pattern. If there is a standard
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
        int Mode;           // Color cycle mode
        int Friends;        // Nearby friends
        int[,]? ILife;      // Life matrix
        int[,]? ISave;      // Save matrix
        DateTime StartMS;   // Start timer
        DateTime StopMS;    // End timer
        TimeSpan ElapsedMS; // Elapsed timer

        DirectBitmap Paper = new DirectBitmap(1, 1);
        readonly Random RNG = new();    // Object to pull RNG values
        ColorHeatMap CMap = new(); // Color map for ColorHeatMap class
        readonly string LicenseFile = "gnu_gpl3.txt";   // GPL 3 license file
        readonly string NumSpec = "N0";
        CultureInfo Culture = CultureInfo.CurrentCulture;

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
            // Init and load and bind the color mode drop down list
            ColorMode colorMode = new ColorMode();
            DDMode.DataSource = colorMode.ModeList();
            DDMode.DisplayMember = "ModeInfo";
            DDMode.ValueMember = "ModeValue";
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
            Paper.Dispose();
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
        /// Grab the selected color mode and force a reset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DDMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Mode = DDMode.SelectedIndex;
            Reset();
        }

        /// <summary>
        /// Reset and reinitialize the variables and the working "life" array
        /// </summary>
        private void Reset()
        {
            // Get and report pixels we can draw in
            IHPixels = Frame.Height;
            IWPixels = Frame.Width;
            TxHPixels.Text = IHPixels.ToString(NumSpec, Culture);
            TxWPixels.Text = IWPixels.ToString(NumSpec, Culture);

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
            TxHBlocks.Text = IHBlocks.ToString(NumSpec, Culture);
            TxWBlocks.Text = IWBlocks.ToString(NumSpec, Culture);

            // Manage "Frame" size
            Frame.Top = ITop;
            Frame.Left = ILeft;
            Frame.Width = this.ClientSize.Width - ILeft;
            Frame.Height = this.ClientSize.Height;

            // Clear and prepare for a fresh bitmap
            if (Frame.Image != null)
            {
                Frame.Image.Dispose();
            }

            // Only recreate the bitmap on a reset
            Paper.Dispose();
            Paper = new DirectBitmap(IWPixels, IHPixels);

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

            StopIt = false;
            Stopped = true;
            IPass = 1;
            BWrap = (CkWrap.Checked);
            BRun.Focus();

            // Update the UI
            Application.DoEvents();
        }

        /// <summary>
        /// Check North
        /// </summary>
        /// <param name="CurW">Current width position (X)</param>
        /// <param name="CurH">Current height position (Y)</param>
        /// <param name="Wrap">Edge wrap mode</param>
        private void North(int CurW, int CurH, bool Wrap)
        {
            if (ILife == null) { return; }

            // North: Width, Height-1
            int W = CurW;
            int H = CurH - 1;
            if (H < 0) { if (Wrap) { H = IHBlocks - 1; } else { H = 0; }}
            if (ILife[W, H] >= 1) { Friends++; }
        }

        /// <summary>
        /// Check NorthEast
        /// </summary>
        /// <param name="CurW">Current width position (X)</param>
        /// <param name="CurH">Current height position (Y)</param>
        /// <param name="Wrap">Edge wrap mode</param>
        private void NorthEast(int CurW, int CurH, bool Wrap)
        {
            if (ILife == null) { return; }

            // Northeast: Width+1, Height-1
            int W = CurW + 1;
            int H = CurH - 1;
            if (W == IWBlocks) { if (Wrap) { W = 0; } else { W = IWBlocks - 1; } }
            if (H < 0) { if (Wrap) { H = IHBlocks - 1; } else { H = 0; } }
            if (ILife[W, H] >= 1) { Friends++; }
        }

        /// <summary>
        /// Check East
        /// </summary>
        /// <param name="CurW">Current width position (X)</param>
        /// <param name="CurH">Current height position (Y)</param>
        /// <param name="Wrap">Edge wrap mode</param>
        private void East(int CurW, int CurH, bool Wrap)
        {
            if (ILife == null) { return; }

            // East: Width+1, Height
            int W = CurW + 1;
            int H = CurH;
            if (W == IWBlocks) { if (Wrap) { W = 0; } else { W = IWBlocks - 1; } }
            if (ILife[W, H] >= 1) { Friends++; }
        }

        /// <summary>
        /// Check SouthEast
        /// </summary>
        /// <param name="CurW">Current width position (X)</param>
        /// <param name="CurH">Current height position (Y)</param>
        /// <param name="Wrap">Edge wrap mode</param>
        private void SouthEast(int CurW, int CurH, bool Wrap)
        {
            if (ILife==null) { return; } 
            
            // Southeast: Width+1, Height+1
            int W = CurW + 1;
            int H = CurH + 1;
            if (W == IWBlocks) { if (Wrap) { W = 0; } else { W = IWBlocks - 1; } }
            if (H == IHBlocks) { if (Wrap) { H = 0; } else { H = IHBlocks - 1; } }
            if (ILife[W, H] >= 1) { Friends++; }
        }

        /// <summary>
        /// Check South
        /// </summary>
        /// <param name="CurW">Current width position (X)</param>
        /// <param name="CurH">Current height position (Y)</param>
        /// <param name="Wrap">Edge wrap mode</param>
        private void South(int CurW, int CurH, bool Wrap)
        {
            if (ILife==null) { return; }    
            // South: Width, Height+1
            int W = CurW;
            int H = CurH + 1;
            if (H == IHBlocks) { if (Wrap) { H = 0; } else { H = IHBlocks - 1; } }
            if (ILife[W, H] >= 1) { Friends++; }
        }

        /// <summary>
        /// Check SouthWest
        /// </summary>
        /// <param name="CurW">Current width position (X)</param>
        /// <param name="CurH">Current height position (Y)</param>
        /// <param name="Wrap">Edge wrap mode</param>
        private void SouthWest(int CurW, int CurH, bool Wrap)
        {
            if (ILife==null) { return; }    
            // Southwest: Width-1, Height+1
            int W = CurW - 1;
            int H = CurH + 1;
            if (W < 0) { if (Wrap) { W = IWBlocks - 1; } else { W = 0; } }
            if (H == IHBlocks) { if (Wrap) { H = 0; } else { H = IHBlocks - 1; } }
            if (ILife[W, H] >= 1) { Friends++; }
        }

        /// <summary>
        /// Check West
        /// </summary>
        /// <param name="CurW">Current width position (X)</param>
        /// <param name="CurH">Current height position (Y)</param>
        /// <param name="Wrap">Edge wrap mode</param>
        private void West(int CurW, int CurH, bool Wrap)
        {
            if (ILife == null) { return; }
            // West: Width-1, Height
            int W = CurW - 1;
            int H = CurH;
            if (W < 0) { if (Wrap) { W = IWBlocks - 1; } else { W = 0; } }
            if (ILife[W, H] >= 1) { Friends++; }
        }

        /// <summary>
        /// Check NorthWest
        /// </summary>
        /// <param name="CurW">Current width position (X)</param>
        /// <param name="CurH">Cuyrrent height position (Y)</param>
        /// <param name="Wrap">Edge wrap mode</param>
        private void NorthWest(int CurW, int CurH, bool Wrap)
        {
            if (ILife == null) { return; }
            // Northwest: Width-1, Height-1
            int W = CurW - 1;
            int H = CurH - 1;
            if (W < 0) { if (Wrap) { W = IWBlocks - 1; } else { W = 0; } }
            if (H < 0) { if (Wrap) { H = IHBlocks - 1; } else { H = 0; } }
            if (ILife[W, H] >= 1) { Friends++; }
        }

        /// <summary>
        /// Apply Conway's Life rules to the "life" array. Supports an 
        /// unbounded and wrap-around universe.
        /// </summary>
        private void DoLife()
        {
            StartMS = DateTime.Now;

            if (ILife == null) return;
            if (ISave == null) return;

            // Update the pass counter in the UI
            TxPass.Text = IPass.ToString(NumSpec, Culture);

            // Zero out the detail counters
            IBirth = 0;
            ILive = 0;
            ILonely = 0;
            ICrowd = 0;
            IEmpty = 0;

            // Scan through the "life" array (taking the block size into
            // account as well)
            for (int CurW = 0; CurW < IWBlocks; CurW++)
            {
                for (int CurH = 0; CurH < IHBlocks; CurH++)
                {
                    Friends = 0;

                    if (BWrap)
                    {
                        // Wrap around universe: Things that hit an edge reappear
                        // on the oppisite side.
                        // Look around the current array element to determine
                        // the future of the current location.
                        North(CurW, CurH, BWrap);
                        NorthEast(CurW, CurH, BWrap);
                        East(CurW, CurH, BWrap);
                        SouthEast(CurW, CurH, BWrap); 
                        South(CurW, CurH, BWrap); 
                        SouthWest(CurW, CurH, BWrap);   
                        West(CurW, CurH, BWrap);
                        NorthWest(CurW, CurH, BWrap);
                    }
                    else
                    {
                        // Unbound universe: Things that hit the edge, keep going 
                        // (well actually they stop at the edge, but in theory
                        // they could keep going).
                        // Look around the current array element to determine
                        // what happens to this location.
                        North(CurW, CurH, BWrap);
                        NorthEast(CurW, CurH, BWrap);   
                        East(CurW, CurH, BWrap);    
                        SouthEast(CurW, CurH, BWrap);   
                        South(CurW, CurH, BWrap);  
                        SouthWest(CurW, CurH, BWrap);   
                        West(CurW, CurH, BWrap);
                        NorthWest(CurW, CurH, BWrap); 
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
                            case 3:
                                // We have two neibours, happy, we live on
                                ISave[CurW, CurH] = (ILife[CurW, CurH] + 1);  // Ha ha ha ha, stay'n alive, stay'n alive...
                                if (ILife[CurW, CurH] >= 255)
                                {
                                    ISave[CurW, CurH] = (Mode == 1) ? 255 : 1;
                                    ILife[CurW, CurH] = (Mode == 1) ? 255 : 1;
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
                            case > 3:
                                // More than three neibours, stay empty
                                ISave[CurW, CurH] = 0;
                                IEmpty++;
                                break;
                            case 3:
                                // Three neibours! Birth!
                                ISave[CurW, CurH] = 1;
                                if (ILife[CurW, CurH] >= 255)
                                {
                                    ISave[CurW, CurH] = (Mode == 1) ? 255 : 1;
                                    ILife[CurW, CurH] = (Mode == 1) ? 255 : 1;
                                }
                                IBirth++;
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
            TxIsLiving.Text = IsLiving.ToString(NumSpec, Culture);
            TxIsEmpty.Text = IsEmpty.ToString(NumSpec, Culture);

            // Show detail stats
            TxBirth.Text = IBirth.ToString(NumSpec, Culture);
            TxLive.Text = ILive.ToString(NumSpec, Culture);
            TxLonely.Text = ILonely.ToString(NumSpec, Culture);
            TxCrowd.Text = ICrowd.ToString(NumSpec, Culture);
            TxEmpty.Text = IEmpty.ToString(NumSpec, Culture);

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
            Color Clr;
            Double Cndx;

            if (ILife == null) return;

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
                                    if (Mode == 1 || Mode == 2)
                                    {
                                        Cndx = (double)ILife[Wid, Hei];
                                        if (Cndx > 255) { Cndx = 255; }
                                        Clr = CMap.GetColorForValue(Cndx, (double)256);
                                    } 
                                    else
                                    {
                                        // A cool side effect of making this a simple else, this will
                                        // be the default if a mode is not yet selected. I like it!
                                        Clr = Color.Yellow;
                                    }
                                }
                                else
                                {
                                    // Empty is always black
                                    Clr = Color.Black;
                                }
                                Paper.SetPixel(IW, IH, Clr);
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

            // Show the updated bitmap and update the UI
            Frame.Image = Paper.Bitmap;
            StopMS = DateTime.Now;
            ElapsedMS = StopMS - StartMS;
            txtPassTimer.Text = ElapsedMS.TotalSeconds.ToString("N4", Culture);
            Application.DoEvents();
        }
    }

    /// <summary>
    /// Create a list of color modes to bind to a drop down list
    /// </summary>
    public class ColorMode
    {
        /// <summary>
        /// Unique value for each mode
        /// </summary>
        public string ?ModeValue { get; set; } 
        /// <summary>
        /// A description of the mode for the drop down
        /// </summary>
        public string ?ModeInfo { get; set; }

        /// <summary>
        /// Create our list of color modes
        /// </summary>
        /// <returns>List of ColorMode</returns>
        public List<ColorMode> ModeList()
        {
            return new List<ColorMode>
            {
                new ColorMode{ ModeValue = "", ModeInfo = "Select" },
                new ColorMode{ ModeValue = "1", ModeInfo = "Cycle once" },
                new ColorMode{ ModeValue = "2", ModeInfo = "Cycle many" },
                new ColorMode{ ModeValue = "3", ModeInfo = "Color: Yellow" }
            };
        }
    }
}
