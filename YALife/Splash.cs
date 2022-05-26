using System;
using System.Windows.Forms;

namespace YALife
{
    /// <summary>
    /// A splash screen 
    /// </summary>
    public partial class Splash : Form
    {
        /// <summary>
        /// Class vars
        /// </summary>
        private readonly int WaitTicks;
        private readonly int ParentWidth;
        private readonly int ParentHeight;
        private readonly int ParentTop;
        private readonly int ParentLeft;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="TimeToWait">int Seconds to display before closing or 0 to stay open</param>
        /// <param name="PWidth">Caller app width</param>
        /// <param name="PHeight">Caller app height</param>
        /// <param name="PTop">Caller app top</param>
        /// <param name="PLeft">Caller app left</param>
        public Splash(int TimeToWait, int PWidth, int PHeight, int PTop, int PLeft)
        {
            InitializeComponent();
            WaitTicks = TimeToWait;
            ParentWidth = PWidth;
            ParentHeight = PHeight;
            ParentTop = PTop;
            ParentLeft = PLeft;
        }

        /// <summary>
        /// Center the splash screen and if needed set the timer 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Splash_Load(object sender, EventArgs e)
        {
            // Center on the parent app 
            int TLeft = (ParentWidth - Width) / 2;
            int TTop = (ParentHeight - Height) / 2;
            Left = TLeft + ParentLeft;
            Top = TTop + ParentTop;
            // If more than 0 ms, wait and then close
            if (WaitTicks > 0) {
                ShowTimer.Interval = WaitTicks;
                ShowTimer.Enabled = true;
            }
        }

        /// <summary>
        /// Closes the spalash screen after wait
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowTimer_Tick(object sender, EventArgs e)
        {
            ShowTimer.Enabled = false;
            this.Hide();
        }
    }
}
