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
    public partial class ServerHistoryControl : UserControl
    {
        public ServerHistoryControl()
        {
            InitializeComponent();
        }

        private void cbTimeSpan_CheckedChanged(object sender, EventArgs e)
        {
            //switch (cbTimeSpan.CheckState)
            //{

            //    case CheckState.Unchecked:
            //        cbTimeSpan.CheckState = CheckState.Checked;
            //        cbTimeSpan.Text = "1 Month";

            //        break;
            //    case CheckState.Checked:
            //        cbTimeSpan.CheckState = CheckState.Indeterminate;
            //        cbTimeSpan.Text = "3 Months";
            //        break;
            //    case CheckState.Indeterminate:
            //        cbTimeSpan.CheckState = CheckState.Unchecked;
            //        cbTimeSpan.Text = "1 Week";
            //        break;
            //}
            //if (cbTimeSpan.Checked)
            //{

            //}

        }

        private void cbTimeSpan_Click(object sender, EventArgs e)
        {
            switch (cbTimeSpan.CheckState)
            {

                case CheckState.Unchecked:
                    cbTimeSpan.CheckState = CheckState.Checked;
                    cbTimeSpan.Text = "3 Months";

                    break;
                case CheckState.Checked:
                    cbTimeSpan.CheckState = CheckState.Indeterminate;
                    cbTimeSpan.Text = "1 Month";
                    break;
                case CheckState.Indeterminate:
                    cbTimeSpan.CheckState = CheckState.Unchecked;
                    cbTimeSpan.Text = "1 Week";
                    break;
            }
            if (cbTimeSpan.Checked)
            {

            }
        }
    }
}
