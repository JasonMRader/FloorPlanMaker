using FloorPlanMakerUI.Properties;
using FloorplanUserControlLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FloorPlanMakerUI.frmTutorialVideos;

namespace FloorPlanMakerUI
{
    public class TutorialImages
    {
        public TutorialImages()
        {
            ThumbnailManager = new TutorialThumbNailManager(this);
        }
        public event EventHandler ImageSelectedChanged;

        private Image _imageSelected;
        public Image imageSelected
        {
            get { return _imageSelected; }
            private set
            {
                if (_imageSelected != value)
                {
                    _imageSelected = value;
                   
                }
            }
        }
        public TutorialThumbNailManager ThumbnailManager { get; private set; }        
        public List<Image> currentTutorialImages { get; private set; } = new List<Image>();
        public int currentTutorialIndex { get; private set; } = 0;
        public TutorialType tutorialTypeSelected;
        public event EventHandler<EventArgs> UpdateRequired;
        public string imageLabelCountString
        {
            get
            {
                return $"{currentTutorialIndex + 1} / {currentTutorialImages.Count}";
            }
        }
        public enum TutorialType
        {
            Form1,
            EditDistribution,
            CreateShift,
            EditDiningAreas,
            Settings,
            UpdatingOrderHistory,
            Servers,
            Sections,
            AssigningSections
        }
        public void GoToNextImage()
        {
            if (currentTutorialIndex < currentTutorialImages.Count - 1)
            {
                currentTutorialIndex++;
            }

            imageSelected = currentTutorialImages[currentTutorialIndex];
            this.ThumbnailManager.SetViewedPictureBoxes();
            ImageSelectedChanged?.Invoke(this, EventArgs.Empty);

        }
        public void GoToPreviousImage()
        {
            if (currentTutorialIndex > 0)
            {
                currentTutorialIndex--;
            }

            imageSelected = currentTutorialImages[currentTutorialIndex];
            this.ThumbnailManager.SetViewedPictureBoxes();
            ImageSelectedChanged?.Invoke(this, EventArgs.Empty);

        }
        private List<Image> GetForm1Images()
        {
            List<Image> images = new List<Image>();
            images.Add(TutorialResources.MainForm_1);
            images.Add(TutorialResources.MainForm_2);
            images.Add(TutorialResources.MainForm_3);
            images.Add(TutorialResources.MainForm_4);
            images.Add(TutorialResources.MainForm_5);
            images.Add(TutorialResources.MainForm_6);
            images.Add(TutorialResources.MainForm_7);
            images.Add(TutorialResources.MainForm_8);
            images.Add(TutorialResources.MainForm_9);
            images.Add(TutorialResources.MainForm_10);
            images.Add(TutorialResources.MainForm_11);
            images.Add(TutorialResources.MainForm_12);
            images.Add(TutorialResources.MainForm_13);
            images.Add(TutorialResources.MainForm_14);
            images.Add(TutorialResources.MainForm_15);
            images.Add(TutorialResources.MainForm_16);
            images.Add(TutorialResources.MainForm_17);
            images.Add(TutorialResources.MainForm_18);
            images.Add(TutorialResources.MainForm_19);
            images.Add(TutorialResources.MainForm_20);
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
            images.Add(TutorialResources.ServerAssignBalanceSalesCreateShift13);
            return images;
        }
        private List<Image> GetShiftCreateImages()
        {
            List<Image> images = new List<Image>();
            
            images.Add(TutorialResources.MainForm_10);
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
            images.Add(TutorialResources.ServerAssignBalanceSalesCreateShift13);
            images.Add(TutorialResources.Floorplans_SectionStats_CreateShift15);
            images.Add(TutorialResources.CreatingFloorplansAuto_1);
            
            //
            images.Add(TutorialResources.CreatingFloorplansAuto_2);
            images.Add(TutorialResources.CreatingFloorplansAuto_3);
            images.Add(TutorialResources.CreatingFloorplansAuto_4);
            images.Add(TutorialResources.CreatingFloorplansAuto_5);
            images.Add(TutorialResources.ManualSections_1);
            images.Add(TutorialResources.ManualSections_2);
            images.Add(TutorialResources.ManualSections_3);
            images.Add(TutorialResources.ManualSections_4);
            images.Add(TutorialResources.ManualSections_5);
            images.Add(TutorialResources.ManualSections_6);
            images.Add(TutorialResources.ManualSections_7);
            images.Add(TutorialResources.ManualSections_8);
            images.Add(TutorialResources.ManualSections_9);
            images.Add(TutorialResources.ManualSections_10);
            //images.Add(TutorialResources.Floorplans_SelectingSections_CreateShift14);

            //images.Add(TutorialResources.Floorplans_OpenTemplateForm_CreateShift16);
            //images.Add(TutorialResources.TemplateSelectCreateShift17);
            //images.Add(TutorialResources.TemplateSelectCreateShift18);
            //images.Add(TutorialResources.TemplateSelectCreateShift19);
            return images;
        }
        private List<Image> GetSectionCreationImages()
        {
            List<Image> images = new List<Image>();
            images.Add(TutorialResources.ManualSections_1);
            images.Add(TutorialResources.ManualSections_2);
            images.Add(TutorialResources.ManualSections_3);
            images.Add(TutorialResources.ManualSections_4);
            images.Add(TutorialResources.ManualSections_5);
            return images;
        }
        private List<Image> GetSettingsImages()
        {
            List<Image> images = new List<Image>();
            images.Add(TutorialResources.SettingsForm1);
            images.Add(TutorialResources.SettingsForm2);
            images.Add(TutorialResources.SettingsForm3);
            images.Add(TutorialResources.SettingsForm4);
            return images;
        }
        private List<Image> GetUpdatingOrderHistoryImages()
        {
            List<Image> images = new List<Image>();
            images.Add(TutorialResources.ImportOrders1);
            images.Add(TutorialResources.ImportOrders2);
            images.Add(TutorialResources.ImportOrders3);
            images.Add(TutorialResources.ImportOrders4);
            return images;
        }
        private List<Image> GetServerImages()
        {
            List<Image> images = new List<Image>();
            images.Add(TutorialResources.ServerForm_1);
            images.Add(TutorialResources.ServerForm_2);
            images.Add(TutorialResources.ServerForm_3);
            images.Add(TutorialResources.ServerForm_4);
            images.Add(TutorialResources.ServerForm_5);
            images.Add(TutorialResources.ServerForm_6);
            images.Add(TutorialResources.ServerForm_7);
            images.Add(TutorialResources.ServerForm_8);
            images.Add(TutorialResources.ServerForm_9);
            return images;
        }
        public void SetCurrentTutorial(int width, int height)
        {
            currentTutorialIndex = 0;
            if (this.tutorialTypeSelected == TutorialType.EditDistribution)
            {
                currentTutorialImages = GetEditDistributionImages();
            }
            else if (this.tutorialTypeSelected == TutorialType.Form1)
            {
                currentTutorialImages = GetForm1Images();
            }
            else if (this.tutorialTypeSelected == TutorialType.CreateShift)
            {
                currentTutorialImages = GetNewShiftImages();
            }
            else if (this.tutorialTypeSelected == TutorialType.Settings)
            {
                currentTutorialImages = GetSettingsImages();
            }
            else if (this.tutorialTypeSelected == TutorialType.UpdatingOrderHistory)
            {
                currentTutorialImages = GetUpdatingOrderHistoryImages();
            }
            else if (this.tutorialTypeSelected == TutorialType.Servers)
            {
                currentTutorialImages = GetServerImages();
            }
           
            imageSelected = currentTutorialImages[currentTutorialIndex];
            this.ThumbnailManager.SetPictureBoxesForImages(width, height);
            ImageSelectedChanged?.Invoke(this, EventArgs.Empty);

        }
        public void SetToTutorialSelected(int width, int height, TutorialType tutorialType)
        {
            currentTutorialIndex = 0;
            if (tutorialType == TutorialType.EditDistribution)
            {
                currentTutorialImages = GetEditDistributionImages();
            }
            else if (tutorialType == TutorialType.Form1)
            {
                currentTutorialImages = GetForm1Images();
            }
            else if (tutorialType == TutorialType.CreateShift)
            {
                currentTutorialImages = GetNewShiftImages();
            }
            else if (tutorialType == TutorialType.Settings)
            {
                currentTutorialImages = GetSettingsImages();
            }
            else if (tutorialType == TutorialType.UpdatingOrderHistory)
            {
                currentTutorialImages = GetUpdatingOrderHistoryImages();
            }
            else if (tutorialType == TutorialType.Servers)
            {
                currentTutorialImages = GetServerImages();
            }
            else if (tutorialType == TutorialType.Sections)
            {
                currentTutorialImages = GetSectionCreationImages();
            }
            else if (tutorialType == TutorialType.AssigningSections)
            {
                currentTutorialImages = GetAssignSectionImages();
            }
            imageSelected = currentTutorialImages[currentTutorialIndex];
            this.ThumbnailManager.SetPictureBoxesForImages(width, height);
            ImageSelectedChanged?.Invoke(this, EventArgs.Empty);
        }

        private List<Image> GetAssignSectionImages()
        {
            List<Image> images = new List<Image>();
            images.Add(TutorialResources.ManualSections_6);
            images.Add(TutorialResources.ManualSections_7);
            images.Add(TutorialResources.ManualSections_8);
            images.Add(TutorialResources.ManualSections_9);
            images.Add(TutorialResources.ManualSections_10);
            return images;
        }

        public void SetToShiftCreationWalkthough(int width, int height)
        {
            currentTutorialIndex = 0;
            currentTutorialImages = GetShiftCreateImages();
            imageSelected = currentTutorialImages[currentTutorialIndex];
            this.ThumbnailManager.SetPictureBoxesForImages(width, height);
            ImageSelectedChanged?.Invoke(this, EventArgs.Empty);
        }

        internal void SetSelectedImage(int index)
        {
            currentTutorialIndex = index;
            imageSelected = currentTutorialImages[currentTutorialIndex];
            this.ThumbnailManager.SetViewedPictureBoxes();
            ImageSelectedChanged?.Invoke(this, EventArgs.Empty);

        }
       

    }
}
