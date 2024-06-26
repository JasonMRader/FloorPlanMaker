﻿using FloorplanClassLibrary;
using FloorPlanMakerUI;
using FloorplanUserControlLibrary.Properties;
using System.Security.Cryptography.X509Certificates;

namespace FloorPlanMaker
{
    public class ShiftControl : FlowLayoutPanel
    {
        private PictureBox _picOutside;
        private PictureBox _picClose;
        private PictureBox _picTeam;
        private PictureBox _picWeekDay;
        public EmployeeShift Shift { get; set; }

        public ShiftControl(EmployeeShift shift, int width, int height)
        {
            this.Height = height;
            this.Width = width;
            this.BackColor = Color.LightGray;
            this.Tag = shift;
            this.Shift = shift;
            this.AutoSize = true;
            int picBoxWidth = (int)(width / 1.25);
            this.MaximumSize = new Size(width, height*2);
            this.Margin = new Padding(0,0,0,0);
            this.Padding = new Padding(2, 0, 0, 0);
            ShiftDayOfWeek = new Label
            {
                Text = shift.Date.ToString("ddd"),
                Width = width,
                AutoSize = false,
                Height = 17,
                Font = UITheme.SmallerFont,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(1,0,0,0)
            };
            _picWeekDay = new PictureBox
            {
                Image = GetDayOfWeekImage(),
                Width = (int)(width - 2),
                Height = (height / 4) - (ShiftDayOfWeek.Height / 3),
                Padding = new Padding(0, 0, 0, 0),
                Margin = new Padding(1, 0, 0, 0),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            // Set images based on shift properties
            _picOutside = new PictureBox
            {
                Width = (int)(width-2),
                Height = (height / 4) - (ShiftDayOfWeek.Height / 3),
                Padding = new Padding(0, 0, 0, 0),
                Margin = new Padding(1,0,0,0),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            _picClose = new PictureBox
            {
                Width = (int)(width-2),
                Height = (height / 4) - (ShiftDayOfWeek.Height / 3),
                Padding = new Padding(0, 0, 0, 0),
                Margin = new Padding(3, 0, 0, 0),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            _picTeam = new PictureBox
            {
                Width = (int)(width),
                Height = (height / 4) - (ShiftDayOfWeek.Height / 3),
                Padding = new Padding(0, 0, 0, 0),
                Margin = new Padding(1, 0, 0, 0),
                SizeMode = PictureBoxSizeMode.Zoom
            };

            // Add to Controls
            //this.Controls.Add(ShiftDayOfWeek);
            this.Controls.Add(_picWeekDay);
            this.Controls.Add(_picOutside);
            //this.Controls.Add(_picClose);
            //this.Controls.Add(_picTeam);
        }
        public void HideOutside()
        {
            this._picOutside.Dispose();
        }
        private Image GetDayOfWeekImage()
        {
            Image image = null;
            DayOfWeek dayOfWeek = this.Shift.Date.DayOfWeek;
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    image = Resources.MondayText;
                    break;
                case DayOfWeek.Tuesday:
                    image = Resources.TuesdayText;
                    break;
                case DayOfWeek.Wednesday:
                    image = Resources.WednesdayText;
                    break;
                case DayOfWeek.Thursday:
                    image = Resources.ThursdayText;
                    break;
                case DayOfWeek.Friday:
                    image = Resources.FridayText;
                    break;
                case DayOfWeek.Saturday:
                    image = Resources.SaturdayText;
                    break;
                case DayOfWeek.Sunday:
                    image = Resources.SundayText;
                    break;
                default:
                    throw new InvalidOperationException("Unknown day of the week");
                   
            }
            return image;
        }
        public void ShowClose()
        {
            _picClose = new PictureBox
            {
                Width = (int)(this.Width),
                Height = (int)(this.Width / 2), 
                Margin = new Padding(0, 0, 0, 0),
                SizeMode = PictureBoxSizeMode.StretchImage,

            };
            if (this.Shift.IsCloser)
            {
                _picClose.Image = Resources.Close;
            }
            else if (this.Shift.IsPre)
            {
                _picClose.Image = Resources.Pre2;
            }
            else
            {
                _picClose.Image = Resources.ScissorsOval;
            }
            this.Controls.Add(_picClose);
            //this.Invalidate();
            
        }
        public void ShowTeam()
        {
            _picTeam = new PictureBox
            {
                Width = (int)(this.Width),
                Height = (int)(this.Width / 2),
                Margin = new Padding(0, 0, 0, 0),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            if (this.Shift.IsTeamWait)
            {
                _picTeam.Image = Resources.teamOval;
            }
            else
            {
                _picTeam.Image= Resources.personOval;
            }
            this.Controls.Add(_picTeam);
            //this.Invalidate();
        }
        public Label ShiftDayOfWeek { get ; set; }
        public PictureBox PicOutside => _picOutside;
        public PictureBox PicClose => _picClose;
        public PictureBox PicTeam => _picTeam;
    }

}