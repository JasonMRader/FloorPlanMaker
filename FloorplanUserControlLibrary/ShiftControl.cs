using FloorplanClassLibrary;
using FloorPlanMakerUI;
using FloorplanUserControlLibrary.Properties;
using System.Security.Cryptography.X509Certificates;

namespace FloorPlanMaker
{
    public class ShiftControl : FlowLayoutPanel
    {
       
        private PictureBox _picClose;
        private PictureBox _picTeam;
        private PictureBox _picWeekDay;
        private ToolTip _toolTip = new ToolTip();
        public EmployeeShift Shift { get; set; }
        public ShiftControl(EmployeeShift shift, int width, int height)
        {
            this.Height = height;
            this.Width = width;
            this.BackColor = Color.LightGray;
            this.Tag = shift;
            this.Shift = shift;
            this.AutoSize = false;
            this.MaximumSize = new Size(width, height*2);
            this.Margin = new Padding(0,0,0,0);
            this.Padding = new Padding(0, 0, 0, 0);
            
            _picWeekDay = new PictureBox
            {
                Image = GetDayOfWeekImage(),
                Width = (int)(width - 2),
                Height = (height / 3) -2,
                Padding = new Padding(0, 0, 0, 0),
                Margin = new Padding(1, 0, 0, 0),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            string isLunch = "AM";
            if (!shift.isLunch)
            {
                isLunch = "PM";
            }
            _toolTip.SetToolTip(_picWeekDay, Shift.Date.ToString("d") + " " + isLunch);
            
            //_picClose = new PictureBox
            //{
            //    Width = (int)(width-2),
            //    Height = (height / 3) - 2,
            //    Padding = new Padding(0, 0, 0, 0),
            //    Margin = new Padding(3, 0, 0, 0),
            //    SizeMode = PictureBoxSizeMode.Zoom
            //};
            //_picTeam = new PictureBox
            //{
            //    Width = (int)(width),
            //    Height = (height / 3) - 2,
            //    Padding = new Padding(0, 0, 0, 0),
            //    Margin = new Padding(1, 0, 0, 0),
            //    SizeMode = PictureBoxSizeMode.Zoom
            //};

            
            this.Controls.Add(_picWeekDay);
            
            ShowClose();
            ShowTeam();
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
                Height = (int)(this.Height / 3), 
                Margin = new Padding(0, 0, 0, 0),
                SizeMode = PictureBoxSizeMode.Zoom,

            };
            if (this.Shift.IsCloser)
            {
                _picClose.Image = Resources.CloseBlack;
                _picClose.BackColor = UITheme.NoColor;
            }
            else if (this.Shift.IsPre)
            {
                _picClose.Image = Resources.PrecloseBlack;
                _picClose.BackColor = UITheme.WarningColor;
            }
            else
            {
                _picClose.Image = Resources.ScissorsCircle;
                _picClose.BackColor= UITheme.YesColor;
            }
            this.Controls.Add(_picClose);
            //this.Invalidate();
            
        }
        public void ShowTeam()
        {
            _picTeam = new PictureBox
            {
                Width = (int)(this.Width),
                Height = (int)(this.Height / 3),
                Margin = new Padding(0, 0, 0, 0),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            if (this.Shift.IsTeamWait)
            {
                _picTeam.Image = Resources.waiters_28;
                _picTeam.BackColor = UITheme.NoColor;
            }
            else
            {
                _picTeam.Image= Resources.waiter;
                _picTeam.BackColor= UITheme.YesColor;
            }
            this.Controls.Add(_picTeam);
            //this.Invalidate();
        }
      
        public PictureBox PicClose => _picClose;
        public PictureBox PicTeam => _picTeam;
    }

}