using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace YALife
{
    class StatListRecord
    {
        public int Counter;
        public int Living;
        public int Emptys;
        public int Births;
        public int Lives;
        public int Lonelys;
        public int Crowded;
        public int EmptyCount;

        public StatListRecord(int IsLiving, int IsEmpty, int birth, int Live, int Lonely, int Crowd, int Empty)
        {
            //Scount= Count;  
            Living = IsLiving;
            Emptys = IsEmpty;
            Births = birth;
            Lives = Live;
            Lonelys = Lonely;
            Crowded = Crowd;
            EmptyCount = Empty;
        }

        //public static void ClearList()
        //{
        //    LifeStatsList.Clear();
        //}

    }
}
