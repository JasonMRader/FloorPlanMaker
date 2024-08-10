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

namespace FloorPlanMakerUI
{
    public partial class frmManageDiningAreaTables : Form
    {
        private DiningArea diningArea { get; set; }
        private List<Table> tablesInArea = new List<Table>();
        private List<Table> tablesCounted = new List<Table>();
        private List<Table> tablesCountedNotInCurrent = new List<Table>();
        private List<Table> tablesExcluded = new List<Table>();

        public frmManageDiningAreaTables(DiningArea diningArea)
        {
            InitializeComponent();
            this.diningArea = diningArea;

            tablesInArea = diningArea.Tables;
            tablesCounted = SqliteDataAccess.GetCountedTablesForDiningArea(diningArea);
            tablesCountedNotInCurrent = tablesCounted
                .Where(tc => !tablesInArea.Any(tia => tia.TableNumber == tc.TableNumber))
                .ToList();
            tablesExcluded = SqliteDataAccess.GetExcludedTablesForDiningArea(diningArea);

            BindListBox(lbTablesInArea, tablesInArea);
            BindListBox(lbTablesCountedInStats, tablesCounted);
            BindListBox(lbCountedNotInCurrent, tablesCountedNotInCurrent);
            BindListBox(lbTablesExcludedFromStats, tablesExcluded);

        }
        private void BindListBox(ListBox listBox1, List<Table> tables)
        {
            BindingSource tableBindingSource = new BindingSource();

            tableBindingSource.DataSource = tables;
            listBox1.DataSource = tableBindingSource;
            listBox1.DisplayMember = "TableNumber";
            listBox1.ValueMember = "ID";
        }

        private void frmManageDiningAreaTables_Load(object sender, EventArgs e)
        {
            lblDiningAreaName.Text = diningArea.Name;
        }

        private void lblDiningAreaName_Click(object sender, EventArgs e)
        {

        }

        private void btnAddSelected_Click(object sender, EventArgs e)
        {
            if (lbTablesInArea.SelectedItems.Count > 0)
            {
                foreach (var table in lbTablesInArea.SelectedItems)
                {
                    if (table is Table tableInArea)
                    {
                        if (!tablesCounted.Contains(tableInArea))
                        {
                            tablesCounted.Add(tableInArea);
                        }


                    }
                }
                BindListBox(lbTablesCountedInStats, tablesCounted);
            }
        }

        private void btnAddAllToCounted_Click(object sender, EventArgs e)
        {
            var newTablesToAdd = tablesInArea.Where(t => !tablesCounted.Any(tc => tc.TableNumber == t.TableNumber)).ToList();
            tablesCounted.AddRange(newTablesToAdd);
            foreach(var table in tablesCounted)
            {
                table.IsIncluded = true;
            }
            BindListBox(lbTablesCountedInStats, tablesCounted);
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            SqliteDataAccess.SaveTablesCounted(tablesCounted);
        }
    }
}
