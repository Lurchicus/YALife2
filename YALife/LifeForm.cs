﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
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
    /// Change history and to do is at the end of this file.
    /// 
    /// To anyone that sees this, I realize this is overkill for a game of life
    /// program that uses random data, but I wrote it specifily to try out 
    /// VS 2022 and .NET 6 WinForms. Not to mention, I find this sort of thing 
    /// fun and I believe it helps keep my 66 year old brain plastic and able to 
    /// learn new things.
    /// </summary>
    public partial class YALife : Form
    {
        bool BWrap;         // Wrap around or bounded universe
        bool StopIt;        // Stop flag
        bool Stopped;       // Stop state
        int ITop;           // Tracks the top of the image frame
        int ILeft;          // Tracks the left of the image frame
        int IInitPercent;   // Initial live percentage
        int IHPixels;       // Height in pixels
        int IWPixels;       // Width in pixels
        int IHBlocks;       // Height in blocks (blocks are made up of multiple pixels)
        int IWBlocks;       // Width in blocks
        int IBlockSize;     // Pixels in block
        int IPass;          // Pass counter

        int IBirth;         // Count of births in a pass
        int ILive;          // Count of "stay alives" in a pass
        int ILonely;        // Count of lonely deaths in a pass
        int ICrowd;         // Count of crowded deaths in a pass
        int InEmpty;        // Count of "Stay empty" cases in a pass
        int IsLiving;       // Count of all living cells
        int IsEmpty;        // Count of all empty cells

        int Mode;           // Color cycle mode
        int FriendCount;    // Nearby friends

        bool UsingPredefined = false;
        string? PredefinedFile;
        bool LoadedPredefined = false;

        int[,]? ILife;      // Life matrix
        int[,]? ISave;      // Save matrix

        DateTime StartMS;   // Start timer
        DateTime StopMS;    // End timer
        TimeSpan ElapsedMS; // Elapsed timer

        DirectBitmap Paper = new(1, 1);
        readonly Random RNG = new();    // Object to pull RNG values
        readonly ColorHeatMap CMap = new(); // Color map for ColorHeatMap class
        readonly string LicenseFile = "MIT_License.txt";   // MIT license file
        readonly string NumSpec = "N0";
        readonly CultureInfo Culture = CultureInfo.CurrentCulture;
        bool CollectStats = false; // Hooked to a checkbox.
        readonly List<StatListRecord> TheStats = [];

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
            ITop = Frame.Top;
            ILeft = Frame.Left;
            // Init and load and bind the color mode drop down list
            ColorMode colorMode = new();
            DDMode.DataSource = ColorMode.ModeList();
            DDMode.DisplayMember = "ModeInfo";
            DDMode.ValueMember = "ModeValue";
            // Give the form a bit of time to draw and display
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
            // Pop up the splash screen for 6 seconds
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
        /// Controll if we will be saving pass stats into a list or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxCollectStats_CheckedChanged(object sender, EventArgs e)
        {
            if (CbxCollectStats.Checked)
            {
                // If we turn on collection, always start with a clean slate
                TheStats.Clear();
                CollectStats = true;
            }
            else
            {
                CollectStats = false;
                // Don't keep old stats either
                if (TheStats.Count > 0)
                {
                    TheStats.Clear();
                }
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
        /// Will eventually show a chart/graph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnChart_Click(object sender, EventArgs e)
        {
            LifeChart ChartForm = new();
            ChartForm.ShowDialog();
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
        /// If we are running, stop the run (can be continued with
        /// step or run).
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
        /// Detect a minimize. If detected, stop and reset as well
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
            // Stop and reset first to make sure we exit cleanly
            if (!Stopped && !StopIt)
            {
                StopIt = true;
            }
            Reset();
            // Dispose of our "fast" bitmap
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
            // in effect a zoom, as it allows the cells/locations displayed to be
            // larger than a single pixel. I'm allowing a block size of up to 32
            // (that can be increased by changing the maximum size set in 
            // NBlockSize). If block size too big you will eventually get 
            // exceptions in DrawLife(). And yes, 32 is very silly.
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
            Frame.Image?.Dispose();

            // Manage stats collection and reset collected stats
            CollectStats = CbxCollectStats.Checked;
            TheStats.Clear();

            // Only recreate the "fast" bitmap on a reset
            Paper.Dispose();
            Paper = new DirectBitmap(IWPixels, IHPixels);

            // Recreate life array (init to 0) and get the 
            // initial population percentage.
            IInitPercent = (int)TxPercent.Value;
            ILife = new int[IWBlocks, IHBlocks];
            ISave = new int[IWBlocks, IHBlocks];

            // Initialize the life array randomly based on a desired starting
            // percentage of live cells
            if (UsingPredefined)
            {
                {
                    // Load and parse .gol file and initialize predefined object
                    if (PredefinedFile is not null)
                    {
                        LoadPredefined(PredefinedFile);
                        if (!LoadedPredefined)
                        {
                            UsingPredefined = false;
                        }
                        UsingPredefined = true;
                    }
                    else
                    {
                        UsingPredefined |= false;
                    }
                }
            }
            else
            {
                // Initialize the life array randomly based on a desired starting
                // percentage of live cells
                Array.Clear(ISave, 0, ISave.Length);
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
        /// Open a file dialog and locate a .gol file
        /// </summary>
        private void GetGolFile()
        {
            OpenFileDialog GetGolFile = new()
            {
                Filter = "GOL File|*.gol|All files|*.*",
                FilterIndex = 0,
                InitialDirectory = Application.ExecutablePath
            };
            if (GetGolFile.ShowDialog() == DialogResult.OK)
            {
                PredefinedFile = GetGolFile.FileName;
                UsingPredefined = true;
            }
            else
            {
                UsingPredefined = false;
            }
            GetGolFile.Dispose();
            GC.Collect();
        }

        /// <summary>
        /// Load and parse the GOL predefined file
        /// *The initial version only supports a single object
        /// </summary>
        /// <param name="PredefinedFile">Predefined GOL file name and path</param>
        private async void LoadPredefined(string PredefinedFile)
        {
            string? Line;
            string? Line2;
            string[] Got;
            char[] Chunk;

            StreamReader Gol = new(PredefinedFile);

            string? GolName;
            int XOffset = 0;
            int YOffset = 0;
            int KeepIt = 0;
            int Rows = 0;
            int MaxLen = 0;
            List<string> GolList = [];

            Line = await Gol.ReadLineAsync();
            while (Line is not null)
            {
                Line = Line.Trim();
                if (!Line.StartsWith('#'))
                {
                    Chunk = Line.Trim()[..1].ToLower().ToCharArray();
                    if (Chunk is not null)
                    {
                        // GOL Object name
                        if (Chunk[0] >= 'a' && Chunk[0] <= 'z')
                        {
                            GolName = Line.Trim();
                            TxLog.Text = "Predefine: \n" + GolName;
                        }
                    }
                    if (Line.StartsWith('+'))
                    {
                        // Offset header
                        Line2 = Line.Trim()[1..];
                        Got = Line2.Split(",");
                        // This is a real weak point for the parser... I'm going to add a "+" prefix to the line
                        try
                        {
                            XOffset = Int32.Parse(Got[0]);
                            YOffset = Int32.Parse(Got[1]);
                            KeepIt = Int32.Parse(Got[2]);
                        }
                        catch (Exception e)
                        {
                            LoadedPredefined = false;
                            UsingPredefined = false;
                            throw (new Exception("LoadPredefined: Integer conversion error in offset header. Err:" + e.Message));
                        }
                    }
                    if (Line.StartsWith("-1"))
                    {
                        // End of object
                        break;
                    }
                    if (Line.StartsWith('0') || Line.StartsWith('1'))
                    {
                        // One or more object rows
                        Rows++;
                        GolList.Add(Line.Trim()[..(Line.Length - 1)]);
                        if (Line.Length > MaxLen) { MaxLen = Line.Length; }
                    }
                }
                else
                {
                    // Comment, ignore it
                }
                // Next line
                Line = await Gol.ReadLineAsync();
            }
            Gol.Close();

            // Draw the predefined object
            if (KeepIt == 0)
            {
                // Wipe everything first
                if (ISave is not null) { Array.Clear(ISave, 0, ISave.Length); }
                if (ILife is not null) { Array.Clear(ILife, 0, ILife.Length); }
            }
            // "Draw" the object (Go Row then Column to keep things simpler)
            int GRow = 0;
            for (int IH = YOffset; IH < IHBlocks; IH++)
            {
                if (GRow < GolList.Count)
                {
                    int GLen = GolList[GRow].ToString().Length;
                    string GStr = GolList[GRow].ToString();
                    int GIdx = 0;
                    int GVal;
                    for (int IW = XOffset; IW < IWBlocks; IW++)
                    {
                        if (GIdx < GLen)
                        {
                            GVal = Int32.Parse(GStr.Substring(GIdx, 1));
                            if (ILife is not null)
                            {
                                if (GVal > 0)
                                {
                                    ILife[IW, IH] = GVal;
                                }
                            }
                            GIdx++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    GRow++;
                }
            }
            GolList.Clear();
            LoadedPredefined = true;
            DrawLife();
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
            InEmpty = 0;

            // Scan through the "life" array (taking the block size into
            // account as well)
            for (int CurW = 0; CurW < IWBlocks; CurW++)
            {
                for (int CurH = 0; CurH < IHBlocks; CurH++)
                {
                    FriendCount = 0;

                    // Look around the current array element to determine
                    // the future of the current location. Updates 
                    // FriendCount. Tried running the tasks below in parallel 
                    // but it really hurt performance, backed it out.
                    North(CurW, CurH, BWrap);
                    NorthEast(CurW, CurH, BWrap);
                    East(CurW, CurH, BWrap);
                    SouthEast(CurW, CurH, BWrap);
                    South(CurW, CurH, BWrap);
                    SouthWest(CurW, CurH, BWrap);
                    West(CurW, CurH, BWrap);
                    NorthWest(CurW, CurH, BWrap);

                    if (ILife[CurW, CurH] >= 1)
                    {
                        // Live cell rules (current cell is alive)
                        switch (FriendCount)
                        {
                            case < 2:
                                // We have one or less neibours so we are too loney to live (/sadface)
                                ISave[CurW, CurH] = 0;
                                ILonely++;
                                break;
                            case 2:
                            case 3:
                                // We have two or three neibours, happy, we live on
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
                        switch (FriendCount)
                        {
                            case < 3:
                            // Less than three neibours, we stay empty
                            case > 3:
                                // More than three neibours, stay empty
                                ISave[CurW, CurH] = 0;
                                InEmpty++;
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
            TxEmpty.Text = InEmpty.ToString(NumSpec, Culture);
            if (CollectStats)
            {
                StatListRecord StatList = new(IsLiving, IsEmpty, IBirth, ILive, ILonely, ICrowd, InEmpty);

                TheStats.Add(StatList);
                int Got = TheStats.Count;
                if (Got > 0)
                {
                    TheStats[Got - 1].Scount = Got;
                }
            }

            // Draw the new bitmap
            DrawLife();
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
            if (H < 0) { if (Wrap) { H = IHBlocks - 1; } else { H = 0; } }
            if (ILife[W, H] >= 1) { FriendCount++; }
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
            if (ILife[W, H] >= 1) { FriendCount++; }
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
            if (ILife[W, H] >= 1) { FriendCount++; }
        }

        /// <summary>
        /// Check SouthEast
        /// </summary>
        /// <param name="CurW">Current width position (X)</param>
        /// <param name="CurH">Current height position (Y)</param>
        /// <param name="Wrap">Edge wrap mode</param>
        private void SouthEast(int CurW, int CurH, bool Wrap)
        {
            if (ILife == null) { return; }

            // Southeast: Width+1, Height+1
            int W = CurW + 1;
            int H = CurH + 1;
            if (W == IWBlocks) { if (Wrap) { W = 0; } else { W = IWBlocks - 1; } }
            if (H == IHBlocks) { if (Wrap) { H = 0; } else { H = IHBlocks - 1; } }
            if (ILife[W, H] >= 1) { FriendCount++; }
        }

        /// <summary>
        /// Check South
        /// </summary>
        /// <param name="CurW">Current width position (X)</param>
        /// <param name="CurH">Current height position (Y)</param>
        /// <param name="Wrap">Edge wrap mode</param>
        private void South(int CurW, int CurH, bool Wrap)
        {
            if (ILife == null) { return; }
            // South: Width, Height+1
            int W = CurW;
            int H = CurH + 1;
            if (H == IHBlocks) { if (Wrap) { H = 0; } else { H = IHBlocks - 1; } }
            if (ILife[W, H] >= 1) { FriendCount++; }
        }

        /// <summary>
        /// Check SouthWest
        /// </summary>
        /// <param name="CurW">Current width position (X)</param>
        /// <param name="CurH">Current height position (Y)</param>
        /// <param name="Wrap">Edge wrap mode</param>
        private void SouthWest(int CurW, int CurH, bool Wrap)
        {
            if (ILife == null) { return; }
            // Southwest: Width-1, Height+1
            int W = CurW - 1;
            int H = CurH + 1;
            if (W < 0) { if (Wrap) { W = IWBlocks - 1; } else { W = 0; } }
            if (H == IHBlocks) { if (Wrap) { H = 0; } else { H = IHBlocks - 1; } }
            if (ILife[W, H] >= 1) { FriendCount++; }
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
            if (ILife[W, H] >= 1) { FriendCount++; }
        }

        /// <summary>
        /// Check NorthWest
        /// </summary>
        /// <param name="CurW">Current width position (X)</param>
        /// <param name="CurH">Current height position (Y)</param>
        /// <param name="Wrap">Edge wrap mode</param>
        private void NorthWest(int CurW, int CurH, bool Wrap)
        {
            if (ILife == null) { return; }
            // Northwest: Width-1, Height-1
            int W = CurW - 1;
            int H = CurH - 1;
            if (W < 0) { if (Wrap) { W = IWBlocks - 1; } else { W = 0; } }
            if (H < 0) { if (Wrap) { H = IHBlocks - 1; } else { H = 0; } }
            if (ILife[W, H] >= 1) { FriendCount++; }
        }

        /// <summary>
        /// Scan the "life" array and use it to generate a bitmap. An array element 
        /// can be scaled from 1 pixel per array element to 32 pixels per array
        /// element. This gives us an ersatz zoom (and it was a fun challange)
        /// </summary>
        private void DrawLife()
        {
            int IWOffSet;
            int IHOffset;
            int IW;
            int IH;
            Color Clr;
            Double ColorIndex;

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
                                    // ColorIndex contains a value from 1 to 255 that is actually the number
                                    // of passes this cell/location has survived. We take this value
                                    // and use it to map into a gradient/color map and we use this
                                    // 'color' as the color we paint the cell/location. It's not really
                                    // usefull for the program but it takes what is effectively a 
                                    // binary display (alive or empty) and adds some interest by letting
                                    // us know which cells are persistant and which are not.
                                    // Note that the "rules" part of DoLife() sets and resets (or clamps)
                                    // the value in the cell.
                                    if (Mode == 1 || Mode == 2)
                                    {
                                        ColorIndex = (double)ILife[Wid, Hei];
                                        if (ColorIndex > 255) { ColorIndex = 255; }
                                        Clr = CMap.GetColorForValue(ColorIndex, (double)256);
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
                                // I honestly don't recall the last time I actually saw this error.
                                TxLog.AppendText("Ovfl Err: IW:" + IW.ToString() + " IH:" + IH.ToString() + "\r\n");
                            }
                        }
                    }
                }
            }

            // Show the updated bitmap and update the UI
            Frame.Image = Paper.Bitmap;
            // Also calculate and show our pass timing
            StopMS = DateTime.Now;
            ElapsedMS = StopMS - StartMS;
            txtPassTimer.Text = ElapsedMS.TotalSeconds.ToString("N4", Culture);
            Application.DoEvents();
        }

        private void Predef_Click(object sender, EventArgs e)
        {
            GetGolFile();
            Reset();
        }
    }

    // ToDo:
    //
    // 1. Collect pass statistics into a list that we could then use to create a chart
    //    This is mostly done. Just no good .NET 6 chart options so far for WinForms.
    //    WIP.
    // 2. Create a way to import a predefined "life" pattern. If there is a standard
    //    for this already I'll use that, otherwise I'll create one... maybe a text
    //    or json formatted file giving the X/Y coordinates of the starting live
    //    cell locations... we can then single step or run them. A form to edit 
    //    these files would be nice as well. Possibly a combination of a text editor
    //    and a gui interface (mouse editing) of the predefined patterns. WIP.
    //
    //  .gol file format (preliminary design)
    //  
    //  # Comment
    //  Name of GOL pattern,        # Patten name, no quotes, terminated by a comma
    //  0,0,0,                      # X,Y origin, clear flag: 1-clear life array, 0-leave as is
    //  11100000101011110000111,    # One row 1-Alive 0-empty
    //  00111101011111010101101,    # Another row
    //  10000100001000010000111,    # this is all random gibberish btw
    //  11100010111110001,          # Line length can vary (implies trailing zeros)
    //  -1,                         # Pattern end (comma indicates another pattern follows)
    //
    // Program change history:
    // 
    // 1.0.1.0  08/12/2021 DWR Initial version
    // 1.0.2.0  08/12/2021 DWR Code and comments cleanup pass 1
    // 1.0.3.0  08/24/2021 DWR Code and comments cleanup pass 2 (not checked in)
    // 1.0.4.0  08/29/2021 DWR Added a color gradient to color persistant cells. The
    //                         gradient is pretty bland. Need something better.
    // 1.0.5.0  09/10/2021 DWR Tweak pixel coloring, some cleanup (not checked in)
    // 1.0.6.0  09/25/2021 DWR Implemented pixel coloring (continous cycle). This new
    //                         method is from Davide Dolla on StackOverflow and works
    //                         exactly the way I want.
    // 1.0.7.0  09/28/2021 DWR Added a checkbox to control if persistance colors cycle
    //                         only once or continously.
    // 1.0.8.0  10/02/2021 DWR Cleaned up some cryptic comments in DoLife()
    //                         - Expanded some of the more important comments.
    // 1.0.9.0  10/16/2021 DWR Added comments to the ColorHeatMap class (Gradient.cs)
    // 1.0.10.0 10/16/2021 DWR First compile using VS 2022 RC 1
    // 1.0.11.0 10/19/2021 DWR Added a file reader form to display the license and a 
    //                         button on the UI panel to load the reader form with the
    //                         GPL 3 license.
    // 1.0.12.0 10/25/2021 DWR Added a pass timer to show how ling (in seconds) it takes
    //                         to get through both DoLife() and DrawLife().
    //                         - Added an about/spalsh screen (preliminary).
    // 1.0.13.0 11/05/2021 DWR Removed the reset debounce as it blocked a proper response 
    //                         to screen size changes. With the debounce, it was seeing and
    //                         responding to the vertical changes but not the horizontal.
    //                         - Set frame (holds the bitmap) to a minimum of 1, 1 so we 
    //                         never try to create a 0x0 bitmap when minimized. For now 
    //                         coming back from a minimize forces a reset. In the future, 
    //                         if I can detect a minimize and restore, I could try to 
    //                         save the bitmap where the size of Frame doesn't affect it 
    //                         and restore it when we un-minimize the form. 
    // 1.0.14.0 11/07/2021 DWR Tweaked the log background.
    // 1.0.15.0 11/09/2021 DWR The initial CleanPaper() in DrawLife() was a total waste of 
    //                         time. Since we repaint the whole bitmap anyway, clearing it 
    //                         first is not needed. Found using the performance profiler. 
    //                         Also this version is on the release version of VS 2022.
    // 1.0.16.0 11/09/2021 DWR Added a "N0" format to add a thousands seperator for most
    //                         of the text boxes. Also added the culture parameter (I
    //                         used currrent, so it's based on how the current windows
    //                         config is set).
    //                         - Removed minimize and maximize from the splash screen.
    //                         - Implemented DirectBitmap which greatly improved my draw
    //                         times. More optimizations by moving the bitmap create and
    //                         dispose into the reset logic so we only recreate the 
    //                         bitmap if we change size or the blocksize (or click Stop
    //                         or Reset).
    // 1.0.17.0 11/20/2021 DWR Added "(zoom)" to block size label text.
    // 1.0.18.0 11/26/2021 DWR Added a "mode" dropdown that will control color cycling
    //                         or a single non cycling color.
    //                         - Removed no longer used functions, methods and events.
    // 1.0.19.0 11/26/2021 DWR More optmizations (don't clear the new life array first
    //                         since we are setting every element anyway). Found a 
    //                         couple case statements that could be optimized too.
    // 1.0.20.0 12/08/2021 DWR Did some refactoring in DoLife with the code that 
    //                         checks for any friends living around us. It's simplified
    //                         in DoLife but the new 8 functions are a bit jank for now.
    // 1.0.21.0 12/12/2021 DWR More comment and code tweaking.\
    // 1.0.22.0 01/27/2022 DWR Added attributions to comments, readme and splash screen 
    //                         for the code used in Paper.cs and Gradient.cs that came
    //                         from StackOverflow.com.
    //                         - Added a file extention checker for FileReader to limit
    //                         what files the reader can display (experimenting on some
    //                         code to use at work)
    // 1.0.23.0 02/02/2022 DWR Minor tweaks and comments
    // 1.0.24.0 03/13/2022 DWR Minor tweaks and moved cardnal direction checkers after
    //                         DoLife() but before DrawLife(). It just seemed to make 
    //                         more sense that way.
    // 1.0.25.0 03/28/2022 DWR Reworked the readme.md file (a work in progress).
    // 1.0.26.0 03/31/2022 DWR More general optimizations. 
    //                         - Switched the ReadToEnd in FileReader load event to
    //                         ReadToEndAsync()... because I can. :)
    //                         - Switched out license from GPL3 to MIT (seems a lot
    //                         simpler and it matches the two classes from 
    //                         StackOverflow.com (attributions are in the splash 
    //                         screen and readme.md file).
    //                         - Removed the GPL3 license text file from the project.
    //                         - Tried to add a screenshot to Readme.md but it isn't
    //                         working. 
    //                         - Started work on a way to format "GOL" predefined 
    //                         patterns in a file format (which will have the file
    //                         extension .gol of course). These could be loaded as 
    //                         sort of batch, or into a drop-down for individual 
    //                         selections. It's a work in progress.
    // 1.0.27.0 05/14/2022 DWR Added a stats class and list so we can collect stats 
    //                         each pass. The intent is to use the list as a data
    //                         set for a chart of some sort (probably just a line
    //                         graph. Work in progress!
    //                         - Fixed dumb bug where I was calling a bunch of
    //                         functions in an if/else block where the if was not
    //                         needed.
    //                         - Added a try/catch where we add stats to the stats 
    //                         list as this could cause a memory exception. For now
    //                         we simply stop collecting stats and let the user know 
    //                         they can use [Stop] and [Reset] to clear out the stats
    //                         we have collected so far. No additional stats are 
    //                         collected for the current run.
    // 1.0.28.0 05/26/2022 DWR Continuing to try and find a chart/data visualization 
    //                         pack for WinForms .NET 6. It doesn't look like
    //                         Microsoft wants to migrate the .NET Framework 4.x
    //                         version to .NET 6 (Core). Boo! Hiss. I could use 
    //                         somethink like Syncfusion, but I'm not sure about
    //                         the licensing needed (also not sure it actually 
    //                         supports .NET 6.0 WinForms properly). Well see. 
    // 1.0.29.0 10/11/2022 DWR Renamed "Friends" to "FriendCount" and it's much
    //                         more descriptive.
    // 1.0.30.0 11/08/2022 DWR Renamed "Cndx" to "ColorIndex" and cleaned up some
    //                         comments and typos.
    // 1.0.31.0 11/09/2022 DWR Renamed Form1 to LifeForm because it's kinda funny. 
    //                         Also switched to .NET 7. No changes needed. Updated
    //                         the Splash "screen" and readme file.
    // 1.0.32.0 11/21/2022 DWR Some refactoring, move the colormode class to its 
    //                         own internal class file (ColorModes.cs).
    //                         - Moved the LifeStat class into the internal class
    //                         file LifeStats.cs. Eventually figured out how to 
    //                         reference the list in the class in the LifeChart form.
    // 1.0.33.0 11/21/2022 DWR Had to rewrite LifeStat class as LifeStats and move 
    //                         the list to the main form. Still can't access the list
    //                         in LifeChart as I can't make the list static. WIP.
}
