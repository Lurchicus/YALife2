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

            if (LifeStat.LifeStatsList != null)
            {
                int Cnt = LifeStat.LifeStatsList.Count;
                richTextBox1.AppendText("\n\nStats collected is ");
                richTextBox1.AppendText(Cnt.ToString());
            } 
            // This is on hold until some open source or community
            // licence chart software becomes available for 
            // .NET 7.0 WinForms
            // I found a prerelease conversion of DataVisualization
            // via NuGet. Looking for some docs to make line graphs.
        }
    }
}
