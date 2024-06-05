using System.Collections.Generic;

namespace YALife
{
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
            return
            [
                new() { ModeValue = "", ModeInfo = "Select" },
                new() { ModeValue = "1", ModeInfo = "Cycle once" },
                new() { ModeValue = "2", ModeInfo = "Cycle many" },
                new() { ModeValue = "3", ModeInfo = "Color: Yellow" }
            ];
        }
    }
}
