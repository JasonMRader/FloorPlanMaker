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
        string messageType = "**Bug**";
        public frmReporting()
        {
            InitializeComponent();
        }

        private void rdoBug_CheckedChanged(object sender, EventArgs e)
        {
            lblBugDescription.Text = "When reporting a bug, it is VERY helpful to be as detailed as possible." +
                " What did you do before it happened, what were the effects, so on.";
            if (rdoBug.Checked)
            {
                messageType = "**Bug**";
            }
        }

        private void rdoFeature_CheckedChanged(object sender, EventArgs e)
        {
            lblBugDescription.Text = "What would you like this application to do that it doesn't?" +
                " What would make it easier to use?";
            if (rdoFeature.Checked)
            {
                messageType = "**FeatureRequest**";
            }
        }

        private void rdoOther_CheckedChanged(object sender, EventArgs e)
        {
            lblBugDescription.Text = "Anything else I should know?";
            if (rdoOther.Checked)
            {
                messageType = "**Other**";
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("jasonmrader@outlook.com");
            mail.To.Add("jasonmrader@outlook.com");
            mail.Subject = messageType + " " + txtTitle.Text;
            mail.Body = txtMessageContents.Text;

            SmtpClient smtpClient = new SmtpClient("smtp.office365.com");
            smtpClient.Port = 587; // or 465 if using SSL
            smtpClient.EnableSsl = true; // SSL/TLS
            smtpClient.Credentials = new System.Net.NetworkCredential("jasonmrader@outlook.com", "123Pacman");


            try
            {
                smtpClient.Send(mail);
                MessageBox.Show("Email Sent Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending email. " + ex.Message);
            }

            mail.Dispose();
            smtpClient.Dispose();
            this.Close();

        }

        private void frmReporting_Load(object sender, EventArgs e)
        {

        }
    }
}
