using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace YALife
{
    class StatListRecord
    {
        public int Scount;
        public int SisLiving;
        public int SisEmpty;
        public int Sbirth;
        public int Slive;
        public int Slonely;
        public int Scrowd;
        public int SEmpty;

        public StatListRecord(int IsLiving, int IsEmpty, int birth, int Live, int Lonely, int Crowd, int Empty)
        {
            //Scount= Count;  
            SisLiving = IsLiving;
            SisEmpty = IsEmpty;
            Sbirth = birth;
            Slive = Live;
            Slonely = Lonely;
            Scrowd = Crowd;
            SEmpty = Empty;
        }

        //public static void ClearList()
        //{
        //    LifeStatsList.Clear();
        //}

    }
}
