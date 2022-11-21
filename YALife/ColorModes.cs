using System.Collections.Generic;

namespace YALife
{
    internal class ColorModes
    {
    }
    /// <summary>
    /// Create a list of color modes to bind to a drop down list
    /// </summary>
    internal class ColorMode
    {
        /// <summary>
        /// Unique value for each mode
        /// </summary>
        public string? ModeValue { get; set; }
        /// <summary>
        /// A description of the mode for the drop down
        /// </summary>
        public string? ModeInfo { get; set; }

        /// <summary>
        /// Create our list of color modes
        /// </summary>
        /// <returns>List of ColorMode</returns>
        public static List<ColorMode> ModeList()
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
