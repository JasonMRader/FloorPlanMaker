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
        public Image imageSelected { get; private set; }
        public TutorialThumbNailManager ThumbnailManager { get; private set; } 
        public TutorialImages() 
        {
            ThumbnailManager = new TutorialThumbNailManager(this);
        }
       
        public List<Image> currentTutorialImages { get; private set; } = new List<Image>();
        public int currentTutorialIndex { get; private set; } = 0;
        public TutorialType tutorialTypeSelected;
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
        }
        public void GoToNextImage()
        {
            if (currentTutorialIndex < currentTutorialImages.Count - 1)
            {
                currentTutorialIndex++;
            }

            imageSelected = currentTutorialImages[currentTutorialIndex];
           
        }
        public void GoToPreviousImage()
        {
            if (currentTutorialIndex > 0)
            {
                currentTutorialIndex--;
            }

            imageSelected = currentTutorialImages[currentTutorialIndex];
            
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
            return images;
        }
        private List<Image> GetSettingsImages()
        {
            List<Image> images = new List<Image>();
            images.Add(TutorialResources.test);

            return images;
        }
        public void SetCurrentTutorial(int width, int height)
        {
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
            imageSelected = currentTutorialImages[currentTutorialIndex];
            this.ThumbnailManager.SetPictureBoxesForImages(width, height);

        }
        public void SetToShiftCreationWalkthough(int width, int height)
        {
            currentTutorialImages = GetShiftCreateImages();
            imageSelected = currentTutorialImages[currentTutorialIndex];
            this.ThumbnailManager.SetPictureBoxesForImages(width, height);
        }

        internal void SetSelectedImage(int index)
        {
            currentTutorialIndex = index;
            imageSelected = currentTutorialImages[currentTutorialIndex];
        }
    }
}
