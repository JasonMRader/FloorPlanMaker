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
        public frmTutorialVideos(TutorialForm tutorialType)
        {
            InitializeComponent();
            this.tutorialFormType = tutorialType;
        }
        private Image imageSelected { get; set; }
        private List<Image> currentTutorialImages = new List<Image>();
        private int currentTutorialIndex = 0;
        private TutorialForm tutorialFormType;
        public enum TutorialForm
        {
            Form1,
            EditDistribution,
            CreateShift,
            EditDiningAreas,
            Settings,
        }
        private void btnNextPic_Click(object sender, EventArgs e)
        {
            GoToNextImage();
        }
        private void GoToNextImage()
        {
            if (currentTutorialIndex < currentTutorialImages.Count - 1)
            {
                currentTutorialIndex++;
            }

            pbTutorial.Image = currentTutorialImages[currentTutorialIndex];
            UpdateImageLabelCount();
        }


        private void btnPreviousPic_Click(object sender, EventArgs e)
        {
            if (currentTutorialIndex > 0)
            {
                currentTutorialIndex--;
            }

            pbTutorial.Image = currentTutorialImages[currentTutorialIndex];
            UpdateImageLabelCount();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTutorialVideos_Load(object sender, EventArgs e)
        {
            currentTutorialImages = GetForm1Images();

            pbTutorial.Image = currentTutorialImages[currentTutorialIndex];
            UpdateImageLabelCount();

        }
        private List<Image> GetForm1Images()
        {
            List<Image> images = new List<Image>();
            images.Add(TutorialResources.Form1Overview1);
            images.Add(TutorialResources.Form1Overview2ShiftButton);
            images.Add(TutorialResources.Form1OverviewEditDiningButton);
            images.Add(TutorialResources.Form1OverviewSettingsButton);
            images.Add(TutorialResources.Form1OverviewFeedBackDescription);
            return images;
        }
        private List<Image> GetNewShiftImages()
        {
            List<Image> images = new List<Image>();

            images.Add(TutorialResources.NewShiftDateCreateShift3);
            images.Add(TutorialResources.NewShiftImportCreateShift4);
            images.Add(TutorialResources.NewShiftManualServerAddCreateShift5);
            images.Add(TutorialResources.NewShiftManualBartenderAddCreateShift6);
            images.Add(TutorialResources.NewShiftAreaHistoryStatsCreateShift7);
            images.Add(TutorialResources.NewShiftSelectAreasCreateShift8);

            return images;
        }
        private List<Image> GetEditDistributionImages()
        {
            List<Image> images = new List<Image>();

            images.Add(TutorialResources.ServerAssignStatsCreateShift9);
            images.Add(TutorialResources.ServerAssignAutoAssignCreateShift10);
            images.Add(TutorialResources.ServerAssignManualAssignCreateShift11);
            images.Add(TutorialResources.ServerAssignFloorplanStatsCreateShift12);
            return images;
        }
        private List<Image> GetShiftCreateImages()
        {
            List<Image> images = new List<Image>();
            images.Add(TutorialResources.CreateShift1);
            images.Add(TutorialResources.CreateShift2);
            images.Add(TutorialResources.NewShiftDateCreateShift3);
            images.Add(TutorialResources.NewShiftImportCreateShift4);
            images.Add(TutorialResources.NewShiftManualServerAddCreateShift5);
            images.Add(TutorialResources.NewShiftManualBartenderAddCreateShift6);
            images.Add(TutorialResources.NewShiftAreaHistoryStatsCreateShift7);
            images.Add(TutorialResources.NewShiftSelectAreasCreateShift8);
            images.Add(TutorialResources.ServerAssignStatsCreateShift9);
            images.Add(TutorialResources.ServerAssignAutoAssignCreateShift10);
            images.Add(TutorialResources.ServerAssignManualAssignCreateShift11);
            images.Add(TutorialResources.ServerAssignFloorplanStatsCreateShift12);
            return images;
        }

        private void rdoGettingStarted_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoGettingStarted.Checked)
            {
                SetCurrentTutorial();
            }
            currentTutorialIndex = 0;
            pbTutorial.Image = currentTutorialImages[currentTutorialIndex];
            UpdateImageLabelCount();

        }

        private void SetCurrentTutorial()
        {
            if (this.tutorialFormType == TutorialForm.EditDistribution)
            {
                currentTutorialImages = GetEditDistributionImages();
            }
            else if (this.tutorialFormType == TutorialForm.Form1)
            {
                currentTutorialImages = GetForm1Images();
            }
        }

        private void UpdateImageLabelCount()
        {
            lblIndex.Text = $"{currentTutorialIndex + 1}/{currentTutorialImages.Count}";
        }
        private void rdoCreatingAShiftWalkthrough_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCreatingAShiftWalkthrough.Checked)
            {
                currentTutorialImages = GetShiftCreateImages();
            }
            currentTutorialIndex = 0;
            pbTutorial.Image = currentTutorialImages[currentTutorialIndex];
            UpdateImageLabelCount();

        }

        private void pbTutorial_Click(object sender, EventArgs e)
        {
            GoToNextImage();
        }
    }
}
