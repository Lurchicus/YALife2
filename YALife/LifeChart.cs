using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization;

namespace YALife
{
    /// <summary>
    /// 
    /// </summary>
    public partial class LifeChart : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public LifeChart()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="StatsOfLife"></param>
        public LifeChart(List<LifeStat> StatsOfLife)
        {
            InitializeComponent();
            
            if (StatsOfLife != null)
            {
                // This is on hold until some open source or community
                // licence chart software becomes available for 
                // .NET 6.0 WinForms
                // I found a prerelease conversion of DataVisualization
                // via NuGet. Looking for some docs to make line graphs.

                int c = StatsOfLife.Count;
            }
        }

    }
}
