﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class Shift
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public bool isLunch { get; set; }
        public int FloorplanID { get; set; }
        public int SectionID { get; set; }
        public int ServerID { get; set; }
        public int DiningAreaID { get; set; }
        public bool IsCloser { get; set; }
        public bool IsOutside { get; set; }
        public bool IsTeamWait { get; set; }
        
        //... Other relevant properties
    }

}
