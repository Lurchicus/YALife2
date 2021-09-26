using System;
using System.Drawing;
using System.Collections.Generic;

namespace YALife
{
    // This was written by Davide Dolla and was shared on StackOverflow 
    // (https://stackoverflow.com/questions/17821828/calculating-heat-map-colours)

    public class ColorHeatMap
    {
        public ColorHeatMap()
        {
            initColorsBlocks();
        }

        public ColorHeatMap(byte alpha)
        {
            this.Alpha = alpha;
            initColorsBlocks();
        }

        private void initColorsBlocks()
        {
            ColorsOfMap.AddRange(new Color[]
            {
                Color.FromArgb(Alpha, 0xFF, 0, 0xFF) ,//Purple
                Color.FromArgb(Alpha, 0, 0, 0xFF) ,//Blue
                Color.FromArgb(Alpha, 0, 0xFF, 0xFF) ,//Cyan
                Color.FromArgb(Alpha, 0, 0xFF, 0) ,//Green
                Color.FromArgb(Alpha, 0xFF, 0xFF, 0) ,//Yellow
                Color.FromArgb(Alpha, 0xFF, 0, 0) ,//Red
                Color.FromArgb(Alpha, 0xFF, 0xFF, 0xFF) // White
            });
        }

        // maxVal needs to be 1 larger than the largest value val can attain. Otherwise this will throw
        // an out of range exception.

        public Color GetColorForValue(double val, double maxVal)
        {
            double valPerc = val / maxVal;// value%
            double colorPerc = 1d / (ColorsOfMap.Count - 1); // % of each block of color. the last is the "100% Color"
            double blockOfColor = valPerc / colorPerc; // the integer part repersents how many block to skip
            int blockIdx = (int)Math.Truncate(blockOfColor);// Idx of 
            double valPercResidual = valPerc - (blockIdx * colorPerc); //remove the part represented of block 
            double percOfColor = valPercResidual / colorPerc; // % of color of this block that will be filled

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
            }
            return c;
        }

        public byte Alpha = 0xff;
        public List<Color> ColorsOfMap = new List<Color>();

    }
}