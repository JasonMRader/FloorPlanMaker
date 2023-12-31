using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;

namespace FloorPlanMakerUI
{
    public partial class frmReporting : Form
    {
        string messageType = string.Empty;
        public frmReporting()
        {
            InitializeComponent();
        }

        private void rdoBug_CheckedChanged(object sender, EventArgs e)
        {
            lblBugDescription.Text = "When reporting a bug, it is VERY helpful to be as detailed as possible." +
                " What did you do before it happened, what were the effects, so on.";
            if (rdoBug.Checked )
            {
                messageType = "**Bug**"; 
            }
        }

        private void rdoFeature_CheckedChanged(object sender, EventArgs e)
        {
            lblBugDescription.Text = "What would you like this application to do that it doesn't?" +
                " What would make it easier to use?";
            if (rdoFeature.Checked )
            {
                messageType = "**FeatureRequest**";
            }
        }

        private void rdoOther_CheckedChanged(object sender, EventArgs e)
        {
            lblBugDescription.Text = "Anything else I should know?";
            if (rdoOther.Checked )
            {
                messageType = "**Other**";
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("your_email@example.com");
            mail.To.Add("recipient_email@example.com");
           
            mail.Body = txtMessageContents.Text;

        }
    }
}
