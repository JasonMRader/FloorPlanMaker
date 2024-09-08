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
            SetIncludedListBindings();

        }
        private void SetIncludedListBindings()
        {
            tablesCountedNotInCurrent = tablesCounted
               .Where(tc => !tablesInArea.Any(tia => tia.TableNumber == tc.TableNumber))
               .ToList();
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

        

        private void btnAddAllToCounted_Click(object sender, EventArgs e)
        {
            var newTablesToAdd = tablesInArea.Where(t => !tablesCounted.Any(tc => tc.TableNumber == t.TableNumber)).ToList();
            tablesCounted.AddRange(newTablesToAdd);
            foreach (var table in tablesCounted) {
                table.IsIncluded = true;
            }
            SetIncludedListBindings();
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            tablesCounted.AddRange(tablesExcluded.ToList());
            SqliteDataAccess.SaveTablesCounted(tablesCounted);
        }
        private void lbTablesInArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbTablesInArea.SelectedIndex != -1) {
                Table table = lbTablesInArea.SelectedItem as Table;
                if (table != null) {
                    txtTableToAdd.Text = table.TableNumber;
                    txtExcludedTable.Text = table.TableNumber;
                }
               
            }

        }
        private void btnAddSelected_Click(object sender, EventArgs e)
        {
            if (lbTablesInArea.SelectedItems.Count > 0) {
                foreach (var table in lbTablesInArea.SelectedItems) {
                    if (table is Table tableInArea) {
                        if (!tablesCounted.Contains(tableInArea)) {
                            tablesCounted.Add(tableInArea);
                        }


                    }
                }
                SetIncludedListBindings();
            }
        }
        private void btnAddTablesToCountedManual_Click(object sender, EventArgs e)
        {
            Table table = new Table() {
                TableNumber = txtTableToAdd.Text,
                DiningAreaId = diningArea.ID,
                IsIncluded = true
            };
            AddToTablesCounted(table);
            

        }
        private void AddToTablesCounted(Table table)
        {
            if (!tablesCounted.Any(t => t.TableNumber == table.TableNumber)) {
                tablesCounted.Add(table);
            }
            if (tablesExcluded.Any(t => t.TableNumber == table.TableNumber)) {
                var tableToRemove = tablesExcluded.FirstOrDefault(t => t.TableNumber == table.TableNumber);
                tablesExcluded.Remove(tableToRemove);
            }
            SetIncludedListBindings();
        }
        private void AddToTablesExcluded(Table table)
        {
            if (tablesCounted.Any(t => t.TableNumber == table.TableNumber)) {
                var tableToRemove = tablesCounted.FirstOrDefault(t => t.TableNumber == table.TableNumber);
                tablesCounted.Remove(tableToRemove);
            }
            if (!tablesExcluded.Any(t => t.TableNumber == table.TableNumber)) {
                tablesExcluded.Add(table);
            }
            SetIncludedListBindings();
        }
        private void btnAddRange_Click(object sender, EventArgs e)
        {
            int tableStart = (int)nudFirstTable.Value;
            int tableEnd = (int)nudLastTable.Value;
            for(int i = tableStart; i <= tableEnd; i++) {
                string tableNumber = i.ToString();
                bool isNotExcluded = !tablesExcluded.Any(t => t.TableNumber == tableNumber);
                bool isNotIncluded = !tablesCounted.Any(t => t.TableNumber == tableNumber);
                if(isNotExcluded && isNotIncluded) {
                    Table table = new Table() {
                        TableNumber = tableNumber,
                        DiningAreaId = diningArea.ID,
                        IsIncluded = true
                    };
                    tablesCounted.Add(table);
                }
            }
            SetIncludedListBindings();

        }
       
        private void btnAddExcluded_Click(object sender, EventArgs e)
        {
            Table table = new Table() {
                TableNumber = txtExcludedTable.Text,
                DiningAreaId = diningArea.ID,
                IsIncluded = false
            };
            AddToTablesExcluded(table);
            SetIncludedListBindings();
        }
    }
}
