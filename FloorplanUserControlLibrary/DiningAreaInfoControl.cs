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

namespace FloorplanUserControlLibrary
{
    public partial class DiningAreaInfoControl : UserControl
    {
        private DiningArea diningArea { get; set; }
        public event EventHandler OpenTableManagerClicked;
        public Table tableSelected { get; set; }
        public DiningAreaInfoControl()
        {
            InitializeComponent();

        }
        public void SetDiningArea(DiningArea diningArea)
        {
            this.diningArea = diningArea;
            setControlsForDiningArea();
            SetTableSelectedToNone();
        }
        public void SetTableSelected(Table tableSelected)
        {
            this.tableSelected = tableSelected;
            SetControlForTableSelected();
        }
        public void SetTableSelectedToNone()
        {
            lblTableSelected.Visible = false;
        }

        private void SetControlForTableSelected()
        {
            lblTableSelected.Visible = true;
            lblTableSelected.Text = tableSelected.TableNumber;
            lbLegacyTables.Items.Clear();
            foreach (string tableNumber in tableSelected.InheritedTables) {
                lbLegacyTables.Items.Add(tableNumber);
            }
            if (tableSelected.IsIncluded) {
                rdoIncludeTable.Checked = true;
                rdoIncludeTable.Text = "Table Included In Stats";
                rdoExcludeTable.Text = "Exclude Table";
            }
            else {
                rdoExcludeTable.Checked = true;                
                rdoIncludeTable.Text = "Included Table";
                rdoExcludeTable.Text = "Table Excluded From Stats";
            }
        }

        private void setControlsForDiningArea()
        {
            lblDiningAreaName.Text = diningArea.Name;
        }



        private void btnOpenManageTablesForm_Click(object sender, EventArgs e)
        {
            OpenTableManagerClicked?.Invoke(this, EventArgs.Empty);
        }

        private void DiningAreaInfoControl_Load(object sender, EventArgs e)
        {

        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if (txtLegacyTable.Text.Length > 0
                && !tableSelected.InheritedTables.Contains(txtLegacyTable.Text)) {
                tableSelected.InheritedTables.Add(txtLegacyTable.Text);
                lbLegacyTables.Items.Add(txtLegacyTable.Text);
                SqliteDataAccess.SaveInheritedTablePairs(tableSelected);
            }
        }

        private void btnRemoveSelected_Click(object sender, EventArgs e)
        {
            if (lbLegacyTables.SelectedIndex != -1) {
                string tablePair = lbLegacyTables.SelectedItem.ToString();
                tableSelected.InheritedTables.Remove(tablePair);
                lbLegacyTables.Items.Remove(lbLegacyTables.SelectedIndex);
                SqliteDataAccess.SaveInheritedTablePairs(tableSelected);
            }
        }

        private void lbLegacyTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbLegacyTables.SelectedIndex != -1) {
                btnRemoveSelected.Enabled = true;
                btnRemoveSelected.Text = $"Remove {lbLegacyTables.SelectedItem} Pair";
            }
            else {
                btnRemoveSelected.Enabled = false;
                btnRemoveSelected.Text = "Remove Selected";
            }
        }

        private void rdoIncludeTable_Clicked(object sender, EventArgs e)
        {
            if (rdoExcludeTable.Checked) {
                tableSelected.IsIncluded = true;
                SqliteDataAccess.UpdateTable(tableSelected);
                rdoIncludeTable.Text = "Table Included In Stats";
                rdoExcludeTable.Text = "Exclude Table";
            }
            else {
                tableSelected.IsIncluded = false;
                SqliteDataAccess.UpdateTable(tableSelected);
                rdoIncludeTable.Text = "Included Table";
                rdoExcludeTable.Text = "Table Excluded From Stats";
            }
        }

    }
}
