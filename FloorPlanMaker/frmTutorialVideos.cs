using FloorPlanMakerUI.Properties;
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
    public partial class frmTutorialVideos : Form
    {
        public TutorialImages tutorialImages = new TutorialImages();
        public frmTutorialVideos(TutorialImages.TutorialType tutorialType)
        {
            InitializeComponent();
            this.tutorialImages.tutorialTypeSelected = tutorialType;

        }
        private void btnNextPic_Click(object sender, EventArgs e)
        {
            tutorialImages.GoToNextImage();
            UpdateUIElements();
        }
        private void btnPreviousPic_Click(object sender, EventArgs e)
        {
            tutorialImages.GoToPreviousImage();
            UpdateUIElements();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmTutorialVideos_Load(object sender, EventArgs e)
        {
            tutorialImages.SetCurrentTutorial(143, 112);
            UpdateUIElements();
        }

        private void rdoGettingStarted_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoGettingStarted.Checked)
            {
                tutorialImages.SetCurrentTutorial(143, 112);
            }
            UpdateUIElements();
        }

        private void rdoCreatingAShiftWalkthrough_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCreatingAShiftWalkthrough.Checked)
            {
                tutorialImages.SetToShiftCreationWalkthough(143, 112);
            }
            UpdateUIElements();
        }
        private void pbTutorial_Click(object sender, EventArgs e)
        {
            tutorialImages.GoToNextImage();
            UpdateUIElements();

        }
        private void UpdateUIElements()
        {
            pbTutorial.Image = tutorialImages.imageSelected;
            lblIndex.Text = tutorialImages.imageLabelCountString;
            flowThumbnails.Controls.Clear();
            foreach(PictureBox pb in tutorialImages.ThumbnailManager.PictureBoxes)
            {
                flowThumbnails.Controls.Add(pb);
            }
        }
    }
}
