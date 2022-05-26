using System;
using System.IO;
using System.Windows.Forms;

namespace YALife
{
    /// <summary>
    /// A form to read a supplied file
    /// </summary>
    public partial class FileReader : Form
    {
        /// <summary>
        /// This string will hold the name and path of the file we will show
        /// to the user (set via the constructor)
        /// </summary>
        private readonly string ReaderFile;
        private readonly int ParentWidth;
        private readonly int ParentHeight;
        private readonly int ParentTop;
        private readonly int ParentLeft;
        private string[] GoodExt = new string[] {".txt", ".TXT", ".md", ".MD", ".json", ".JSON"};

        /// <summary>
        /// Constructor to initialize the form and save the file to display
        /// </summary>
        /// <param name="FileToRead">string with the file and path of the file to display</param>
        /// <param name="PWidth">int Parent form width</param>
        /// <param name="PHeight">int Parent form height</param>
        /// <param name="PTop">int Top of parent form</param>
        /// <param name="PLeft">int Left side of parent form</param>
        public FileReader(string FileToRead, int PWidth, int PHeight, int PTop, int PLeft)
        {
            InitializeComponent();
            ReaderFile = FileToRead;
            ParentWidth = PWidth;
            ParentHeight = PHeight;
            ParentTop = PTop;
            ParentLeft = PLeft;
        }

        /// <summary>
        /// Loads the file to display into a text box with a stream reader
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void FileReader_Load(object sender, EventArgs e)
        {
            // Limit the file extentions we will allow to be viewed and
            // throw an exception if we see one we don't allow.
            if (!ValidateExtension(ReaderFile))
            {
                throw new Exception("Error! Invalid file format");
            }
            // Center the text reader on parent app
            int TLeft = (ParentWidth - Width) / 2;
            int TTop = (ParentHeight - Height) / 2;
            Left = TLeft + ParentLeft;
            Top = TTop + ParentTop;
            // Show the requested file
            TBReader.Clear();
            using StreamReader r = new(ReaderFile);
            TBReader.Text = await r.ReadToEndAsync();
            TBReader.Select(0, 0);
            r.Close();
        }

        /// <summary>
        /// Limit the file extentions we will allow to be viewed and
        /// return false if we don't find the extension
        /// </summary>
        /// <param name="ThePath">A full pathname</param>
        /// <returns>bool</returns>
        private bool ValidateExtension(string ThePath)
        {
            string ExtIs = Path.GetExtension(ThePath).ToLower();
            // Validate file extension
            foreach (string Ext in GoodExt)
            {
                if (Ext == ExtIs) { return true; }
            }
            return false;
        }
    }
}
