using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorPlanMaker
{
    public partial class frmTemplateSelection : Form
    {
        ShiftManager ShiftManager;
        public frmTemplateSelection(ShiftManager shiftManager)
        {
            InitializeComponent();
            this.ShiftManager = shiftManager;
        }

        private void frmTemplateSelection_Load(object sender, EventArgs e)
        {
            ShiftManager.Templates = SqliteDataAccess.LoadTemplatesByDiningArea(ShiftManager.SelectedDiningArea);
            Panel[] panels = { panel1, panel2, panel3, panel4 };  // Assuming you have named your panels like this

            for (int i = 0; i < 4 && i < ShiftManager.Templates.Count; i++)
            {
                SetupPanelWithTemplate(panels[i], ShiftManager.Templates[i]);
            }
        }
        private void SetupPanelWithTemplate(Panel pnl, FloorplanTemplate template)
        {
            // Clear the current controls
            
            //pnl.Controls.Clear();

            foreach (Table table in ShiftManager.SelectedDiningArea.Tables)  // Assuming FloorplanTemplate has a Tables property
            {
                table.DiningArea = ShiftManager.SelectedDiningArea;
                TableControl tableControl = TableControlFactory.CreateMiniTableControl(table, (float).4, 27);
                //tableControl.TableClicked += Table_TableClicked;  // Uncomment if you want to attach event handler

                pnl.Controls.Add(tableControl);
            }
            ShiftManager.SetSectionsToTemplate(template);
            ShiftManager.AssignSectionNumbers();
            foreach (Control ctrl in pnl.Controls)
            {
                if (ctrl is TableControl tableControl)
                {
                    foreach (Section section in ShiftManager.Sections)
                    {

                        foreach (Table table in section.Tables)
                        {
                            if (tableControl.Table.TableNumber == table.TableNumber)
                            {
                                tableControl.Section = section;
                                tableControl.BackColor = section.Color;
                                tableControl.Invalidate();
                                break; // Once found, no need to check other tables in this section
                            }
                        }
                    }
                }
            }

            // Any other setup logic specific to the template can be added here
        }

    }
}
