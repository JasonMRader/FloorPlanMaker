using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMakerUI
{
    public class DiningAreaButtonHandeler: IShiftObserver
    {
        private List<DiningArea> diningAreas = new List<DiningArea>();
        private FlowLayoutPanel flowLayoutPanel { get; set; }
        public event EventHandler DiningAreaChanged;
        private List<RadioButton> radioButtons = new List<RadioButton>();
        private Panel pnlIndicatorContainer = new Panel();
        private Panel pnlIndicator = new Panel();
        private Panel pnlIndicator2 = new Panel();
        private Shift shift = new Shift();
        private int diningAreaSelectedIndex
        {
            get
            {
                return diningAreas.IndexOf(shift.SelectedDiningArea);
            }
        }
        public DiningAreaButtonHandeler(List<DiningArea> allDiningAreas, FlowLayoutPanel flow, Panel pnlIndicatorContainer,
            Panel pnlIndicator, Panel pnlIndicator2)
        {
            this.diningAreas = allDiningAreas;
            this.flowLayoutPanel = flow;
            CreateDiningAreaButtons();
            this.pnlIndicatorContainer = pnlIndicatorContainer;
            this.pnlIndicator = pnlIndicator;
            this.pnlIndicator2 = pnlIndicator2;
            UpdateRadioButtons();
        }
        private void CreateDiningAreaButtons()
        {
            foreach (DiningArea area in diningAreas)
            {
                RadioButton button = CreateDiningAreaButton(area);
                radioButtons.Add(button);
                flowLayoutPanel.Controls.Add(button);

            }
        }

        private RadioButton CreateDiningAreaButtonHORIZONTAL(DiningArea area)
        {
            int margin = 5;
            int totalMarginSpace = margin * (diningAreas.Count);
            int adjustedWidth = flowLayoutPanel.Width - totalMarginSpace;
            int width = adjustedWidth / diningAreas.Count;
            //int width = (int)((float)(flowLayoutPanel.Width - (margin * (diningAreas.Count - 1)) / (float)(diningAreas.Count)));
            //((flowLayoutPanel.Width + (diningAreas.Count * 10)) / (diningAreas.Count + 1))
            RadioButton btn = new RadioButton() 
            {
               
                Image = UITheme.GetDiningAreaImage(area),
                Size = new Size(width, flowLayoutPanel.Height),
                Margin = new Padding(0, 0, margin, 0),
                Tag = area
            };
            btn.Click += areaButtonClicked;
            UITheme.FormatCTAButton(btn);
            return btn;
        }
        private RadioButton CreateDiningAreaButton(DiningArea area)
        {
            int margin = 10;
            int totalMarginSpace = margin * (diningAreas.Count + 1);
            int adjustedHeight = flowLayoutPanel.Height - totalMarginSpace;
            int height = adjustedHeight / diningAreas.Count;
            //int width = (int)((float)(flowLayoutPanel.Width - (margin * (diningAreas.Count - 1)) / (float)(diningAreas.Count)));
            //((flowLayoutPanel.Width + (diningAreas.Count * 10)) / (diningAreas.Count + 1))
            RadioButton btn = new RadioButton()
            {

                Image = UITheme.GetDiningAreaImage(area),
                Size = new Size(flowLayoutPanel.Width, flowLayoutPanel.Height / diningAreas.Count),
                Margin = new Padding(0,0,0,0),
                Tag = area
               
                
            };
            btn.Click += areaButtonClicked;
            UITheme.FormatCTAButton(btn);
            btn.BackColor = UITheme.ButtonColor;
            btn.FlatAppearance.CheckedBackColor = btn.BackColor;
            return btn;
        }
        public void UpdateForShift(Shift shift)
        {
            this.shift.RemoveObserver(this);
            this.shift = shift;
            this.shift.SubscribeObserver(this);

            //if (shift != this.shift)
            //{
            //    this.shift.RemoveObserver(this);
            //    this.shift = shift;               
                
            //}
            if (shift.Floorplans.Count == 0) 
            {
                foreach(RadioButton radio in radioButtons)
                {
                    radio.BackColor = UITheme.ButtonColor;
                    radio.FlatAppearance.CheckedBackColor = UITheme.ButtonColor;
                    pnlIndicator.BackColor = radio.BackColor;
                    //pnlIndicator.Invalidate();
                }
            }
            else
            {
                foreach(RadioButton radio in radioButtons)
                {
                    if(this.shift.DiningAreasUsed.Contains(radio.Tag))
                    {
                        DiningArea area = radio.Tag as DiningArea;
                        if(SqliteDataAccess.CheckIfFloorplanExistsForDate(shift.DateOnly, shift.IsAM, area.ID))
                        {
                            radio.BackColor = UITheme.YesColor;
                            radio.FlatAppearance.CheckedBackColor = UITheme.YesColor;
                        }
                        else
                        {
                            radio.BackColor = UITheme.WarningColor;
                            radio.FlatAppearance.CheckedBackColor = UITheme.WarningColor;
                        }
                    }
                    else
                    {
                        radio.BackColor = UITheme.ButtonColor;
                        radio.FlatAppearance.CheckedBackColor = UITheme.ButtonColor;
                    }
                    if (radio.Checked)
                    {
                        pnlIndicator.BackColor = radio.BackColor;
                        //pnlIndicator.Invalidate();
                    }
                }
            }
           
            //UpdateRadioButtons();
        }
       

        private void areaButtonClicked(object? sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            DiningArea area = (DiningArea)rdo.Tag;
            if (rdo.Checked)
            {
                DiningAreaChanged?.Invoke(rdo, EventArgs.Empty);
            }
            //UpdateRadioButtons();
           
        }
        private void UpdateRadioButtons()
        {
            if(this.shift.SelectedDiningArea == null) {  return; }
            foreach (RadioButton rdo in radioButtons)
            {
                DiningArea area = rdo.Tag as DiningArea;

                if (area.ID == shift.SelectedDiningArea.ID)
                {
                    rdo.Checked = true;
                    pnlIndicator.Height = rdo.Height;
                    pnlIndicator.Location = new Point(0, rdo.Location.Y);
                    pnlIndicator.BackColor = rdo.BackColor;
                    pnlIndicator2.Height = rdo.Height;
                    pnlIndicator2.Location = new Point(0, rdo.Location.Y);
                    pnlIndicator2.BackColor = rdo.BackColor;
                    pnlIndicator2.BackColor = Color.FromArgb(255, 103, 0);
                    //rdo.Width = rdo.Width + 5;

                }
                else
                {
                    rdo.Width = flowLayoutPanel.Width;
                }

            }
        }
        public void UpdateShift(Shift shift)
        {
            if(this.shift != shift)
            {
                UpdateForShift(shift);
            }
            UpdateRadioButtons();
            
        }
        public void ChangeDiningAreaUp()
        {
            if (diningAreaSelectedIndex > diningAreas.IndexOf(diningAreas.Last()))
            {
                shift.SetSelectedDiningArea(diningAreas[diningAreaSelectedIndex + 1]);
            }
            else
            {
                shift.SetSelectedDiningArea(diningAreas.Last());
            }
        }

        public void ChangeDiningAreaDown()
        {
            if (diningAreaSelectedIndex < 0)
            {
                shift.SetSelectedDiningArea(diningAreas[diningAreaSelectedIndex - 1]);
            }
            else
            {
                shift.SetSelectedDiningArea(diningAreas.First());
            }
        }
    }
}
