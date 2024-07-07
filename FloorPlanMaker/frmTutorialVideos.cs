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
            this.tutorialImages.ImageSelectedChanged += TutorialImages_ImageSelectedChanged;

        }

        private void TutorialImages_ImageSelectedChanged(object? sender, EventArgs e)
        {
            UpdateUIElements();
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
            tutorialImages.SetCurrentTutorial(165, 129);
            UpdateUIElements();
        }

        private void rdoGettingStarted_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoGettingStarted.Checked)
            {
                tutorialImages.SetCurrentTutorial(165, 129);
            }
           UpdateUIElements();
        }

        private void rdoCreatingAShiftWalkthrough_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCreatingAShiftWalkthrough.Checked)
            {
                tutorialImages.SetToShiftCreationWalkthough(165, 129);
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
            foreach (PictureBox pb in tutorialImages.ThumbnailManager.ViewedPictureBoxes)
            {
                flowThumbnails.Controls.Add(pb);
            }
            pnlHighlight.Location = new Point(tutorialImages.ThumbnailManager.HighlightPanelLocation, 0);
            //UpdateHighlightPanelLocation();
        }
        private void UpdateHighlightPanelLocation()
        {
            int x = 143 * (tutorialImages.currentTutorialIndex);
            pnlHighlight.Location = new Point(x, 0);
        }
    }
}
