using System;
using System.Drawing;
using System.Collections.Generic;

namespace YALife
{
    // This was written by Davide Dolla and was shared on StackOverflow 
    // (https://stackoverflow.com/questions/17821828/calculating-heat-map-colours)
    // Added my own comments to make sure I understand how this class works.

    // In effect, we define two or more key colors (ColorsOfMap) then we
    // calculate and return ARGB values between these key colors.

    public class ColorHeatMap
    {
        /// <summary>
        /// Byte that defines the Alpha (transparency) part of an ARGB color
        /// </summary>
        public byte Alpha = 0xff;

        /// <summary>
        /// Defines key ARGB points in what can be seen as a large virtual gradient
        /// </summary>
        public List<Color> ColorsOfMap = new List<Color>();

        /// <summary>
        /// Class constructor
        /// </summary>
        public ColorHeatMap()
        {
            initColorsBlocks();
        }

        /// <summary>
        /// Class constructor with an alpha other than 0xFF
        /// </summary>
        /// <param name="alpha">byte value used to define our ARGB alpha (transparency)</param>
        public ColorHeatMap(byte alpha)
        {
            this.Alpha = alpha;
            initColorsBlocks();
        }

        /// <summary>
        /// Define the color steps that define our virtual color gradient/color map
        /// </summary>
        private void initColorsBlocks()
        {
            ColorsOfMap.AddRange(new Color[] {
                Color.FromArgb(Alpha, 0xFF, 0, 0xFF) ,  //Purple
                Color.FromArgb(Alpha, 0, 0, 0xFF) ,     //Blue
                Color.FromArgb(Alpha, 0, 0xFF, 0xFF) ,  //Cyan
                Color.FromArgb(Alpha, 0, 0xFF, 0) ,     //Green
                Color.FromArgb(Alpha, 0xFF, 0xFF, 0) ,  //Yellow
                Color.FromArgb(Alpha, 0xFF, 0, 0) ,     //Red
                Color.FromArgb(Alpha, 0xFF, 0xFF, 0xFF) // White
            });
        }

        /// <summary>
        /// Calculate a single ARGB color from a virtual gradient/color map
        /// </summary>
        /// <param name="val">a double from 1 to maxVal-1 that can be thought of as an index into
        /// a large (maxVal) array of colors that make up a virtual gradient</param>
        /// <param name="maxVal">a double that defines how many colors our virtual gradient will contain</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Color GetColorForValue(double val, double maxVal)
        {
            double valPerc = val / maxVal;                              // value%
            double colorPerc = 1d / (ColorsOfMap.Count - 1);            // % of each block of color. the last is the "100% Color"
            double blockOfColor = valPerc / colorPerc;                  // the integer part repersents how many block to skip
            int blockIdx = (int)Math.Truncate(blockOfColor);         // Idx of 
            double valPercResidual = valPerc - (blockIdx * colorPerc);  // remove the part represented of block 
            double percOfColor = valPercResidual / colorPerc;           // % of color of this block that will be filled

            Color cTarget = ColorsOfMap[blockIdx];
            Color cNext = cNext = ColorsOfMap[blockIdx + 1];

            var deltaR = cNext.R - cTarget.R;
            var deltaG = cNext.G - cTarget.G;
            var deltaB = cNext.B - cTarget.B;

            var R = cTarget.R + (deltaR * percOfColor);
            var G = cTarget.G + (deltaG * percOfColor);
            var B = cTarget.B + (deltaB * percOfColor);

            Color c = ColorsOfMap[0];
            try
            {
                c = Color.FromArgb(Alpha, (byte)R, (byte)G, (byte)B);
            }
            catch (Exception ex)
            {
                throw new Exception("Failboat exception: " + ex.Message + ", " + ex.StackTrace);
            }
            return c;
        }
    }
}