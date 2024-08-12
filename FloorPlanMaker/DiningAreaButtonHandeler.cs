using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMakerUI
{
    public class DiningAreaButtonHandeler
    {
        private List<DiningArea> diningAreas = new List<DiningArea>();
        private FlowLayoutPanel flowLayoutPanel { get; set; }
        public event EventHandler DiningAreaChanged;
        private List<RadioButton> radioButtons = new List<RadioButton>();
        public DiningAreaButtonHandeler(List<DiningArea> allDiningAreas, FlowLayoutPanel flow)
        {
            this.diningAreas = allDiningAreas;
            this.flowLayoutPanel = flow;
            CreateDiningAreaButtons();
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

        private RadioButton CreateDiningAreaButton(DiningArea area)
        {
            int margin = 5;
            int totalMarginSpace = margin * (diningAreas.Count - 1);
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
        public void UpdateForShift(Shift shift)
        {
            if(shift == null) {  return; }
            if(shift.Floorplans.Count == 0) 
            {
                foreach(RadioButton radio in radioButtons)
                {
                    radio.BackColor = UITheme.CTAColor;
                }
            }
            else
            {
                foreach(RadioButton radio in radioButtons)
                {
                    if(shift.DiningAreasUsed.Contains(radio.Tag))
                    {
                        DiningArea area = radio.Tag as DiningArea;
                        if(SqliteDataAccess.CheckIfFloorplanExistsForDate(shift.DateOnly, shift.IsAM, area.ID))
                        {
                            radio.BackColor = UITheme.YesColor;
                        }
                        else
                        {
                            radio.BackColor = UITheme.WarningColor;
                        }
                    }
                    else
                    {
                        radio.BackColor = UITheme.ButtonColor;
                    }
                }
            }
        }

        private void areaButtonClicked(object? sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            DiningArea area = (DiningArea)rdo.Tag;
            if (rdo.Checked)
            {
                DiningAreaChanged?.Invoke(rdo, EventArgs.Empty);
            }
           
        }
    }
}
