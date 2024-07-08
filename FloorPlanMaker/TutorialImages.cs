﻿using FloorPlanMakerUI.Properties;
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
                return $"{currentTutorialIndex + 1}/{currentTutorialImages.Count}";
            }
        }
        public enum TutorialType
        {
            Form1,
            EditDistribution,
            CreateShift,
            EditDiningAreas,
            Settings,
            UpdatingOrderHistory
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
            images.Add(TutorialResources.Form1Overview1);
            images.Add(TutorialResources.Form1Overview2ShiftButton);
            images.Add(TutorialResources.Form1OverviewEditDiningButton);
            images.Add(TutorialResources.Form1OverviewSettingsButton);
            images.Add(TutorialResources.Form1OverviewFeedBackDescription);
            images.Add(TutorialResources.Floorplans_SelectingSections_CreateShift14);
            images.Add(TutorialResources.Floorplans_SectionStats_CreateShift15);
            images.Add(TutorialResources.Floorplans_OpenTemplateForm_CreateShift16);
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
            images.Add(TutorialResources.Floorplans_SelectingSections_CreateShift14);
            images.Add(TutorialResources.Floorplans_SectionStats_CreateShift15);
            images.Add(TutorialResources.Floorplans_OpenTemplateForm_CreateShift16);
            images.Add(TutorialResources.TemplateSelectCreateShift17);
            images.Add(TutorialResources.TemplateSelectCreateShift18);
            images.Add(TutorialResources.TemplateSelectCreateShift19);
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
            imageSelected = currentTutorialImages[currentTutorialIndex];
            this.ThumbnailManager.SetPictureBoxesForImages(width, height);
            ImageSelectedChanged?.Invoke(this, EventArgs.Empty);

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
