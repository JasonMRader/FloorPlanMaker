﻿using FloorplanClassLibrary;
using FloorPlanMaker;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMakerUI
{
    internal static class ImageSetter
    {
       
        public static void SetShiftImages(ShiftControl shiftControl, Shift shift)
        {
            if (shift.IsOutside)
            {
                shiftControl.PicOutside.Image = Resource1.Outside;
            }
            else
            {
                shiftControl.PicOutside.Image = Resource1.Inside;
            }
            if (shift.IsCloser)
            {
                shiftControl.PicClose.Image = Resource1.Closer;
            }
            else
            {
                shiftControl.PicClose.Image = Resource1.Cut;
            }
            if (shift.IsTeamWait)
            {
                shiftControl.PicTeam.Image = Resource1.TeamWait;
            }
            else
            {
                shiftControl.PicTeam.Image = Resource1.Solo;
            }
        }
        //public Image OutsideImage { get; set; }
        //public Image CloserImage { get; set;}
        //public Image TeamWaitImage { get; set;}
    }
}
