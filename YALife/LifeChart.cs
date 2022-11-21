using System.Windows.Forms;

namespace YALife
{
    /// <summary>
    /// 
    /// </summary>
    public partial class LifeChart : Form
    {

        /// <summary>
        /// Eventually create a graph/chart of collected stats (if any)
        /// </summary>
        public LifeChart()
        {
            InitializeComponent();

            //if (YALife.TheStats.Count> 0 )
            //{
            //    int Cnt = YALife.TheStats.Count;
            //    richTextBox1.AppendText("\n\nStats collected is ");
            //    richTextBox1.AppendText(Cnt.ToString());
            //}

            // This is on hold until some open source or community
            // licence chart software becomes available for 
            // .NET 7.0 WinForms
            // I found a prerelease conversion of DataVisualization
            // via NuGet. Looking for some docs to make line graphs.
        }
    }
}
