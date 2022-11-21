using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace YALife
{
    /// <summary>
    /// Used to create a snapshot of stats at the end of doLife. Collecting the stats
    /// is an option as this data will keep growing everytime we make a doLife pass.
    /// The intent is to have a dataset we could render into a chart if we wanted to.
    /// </summary>
    internal class LifeStat
    {
        public static List<LifeStat> LifeStatsList { get; set; } = new();

        ///<summary>The ID number of the stat cell</summary>
        public static int ID;
        /// <summary>The number of living cells</summary>
        public static int LivingCount;
        /// <summary>The number of empty cells</summary>
        public static int EmptyCount;
        /// <summary>The number of newly born cells</summary>
        public static int NewbornCount;
        /// <summary>The number of cells that stayed alive</summary>
        public static int LivedOnCount;
        /// <summary>The number of cells that died of lonelyness</summary>
        public static int DiedLonelyCount;
        /// <summary>The number of cells that died of overcrowding</summary>
        public static int DiedCrowededCount;
        /// <summary>The number of cells that stayed empty</summary>
        public static int StayedEmptyCount;

        /// <summary>
        /// Collect and save a frame of statistics
        /// </summary>
        public static string StatLife(int ID, int IsLiving, int IsEmpty, int IBirth, int ILive, int ILonely, int ICrowd, int IEmpty,
            LifeStat LStat) 
        {
            // Create a stat object and load it with stats
            int IDis = LifeStatsList.Count;
            ID = IDis;
            LivingCount = IsLiving;
            EmptyCount = IsEmpty;
            NewbornCount = IBirth;
            LivedOnCount = ILive;
            DiedLonelyCount = ILonely;
            DiedCrowededCount = ICrowd;
            StayedEmptyCount = IEmpty;
            try
            {
                // Add the stat object to the list of stats
                LifeStatsList.Add(LStat);
                return "";
            }
            catch (Exception ex)
            {
                // If we have an error, stop collecting stats (but don't clear
                // what we have so far (wait for a reset to do the clear).
                return ex.Message +
                    "\r\nError encountered, statistics collection stopped." +
                    "\r\nTo clear, click [Stop] and [Reset].";
            }
        }

        public static void ClearList()
        {
            LifeStatsList.Clear();
        }
    }
}
