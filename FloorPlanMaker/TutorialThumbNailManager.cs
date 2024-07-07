using FloorPlanMakerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanUserControlLibrary
{
    public class TutorialThumbNailManager
    {
        public TutorialImages TutorialImages { get; set; }
        public TutorialThumbNailManager(TutorialImages tutorialImages)
        {
            TutorialImages = tutorialImages;
        }
        public int HighlightPanelLocation = 0;
        public List<PictureBox> PictureBoxes { get; set; } = new List<PictureBox>();
        public List<PictureBox> ViewedPictureBoxes { get; set; } = new List<PictureBox>();
        public void SetPictureBoxesForImages(int width, int height)
        {
            PictureBoxes.Clear();
            foreach(Image img in TutorialImages.currentTutorialImages)
            {
                PictureBox pictureBox = new PictureBox
                {
                    Size = new Size(width, height),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Image = img,
                    Margin = new Padding(0),
                    
                };
                pictureBox.Click += PictureBox_Click;
                PictureBoxes.Add(pictureBox);
                SetViewedPictureBoxes();
            }
        }
        public void SetViewedPictureBoxes()
        {
            int index = TutorialImages.currentTutorialIndex;
            int imgCount = TutorialImages.currentTutorialImages.Count;
            int panelWidth = PictureBoxes[0].Width * 7;
            if(imgCount <= 7)
            {
                ViewedPictureBoxes = PictureBoxes;
                HighlightPanelLocation = index * ViewedPictureBoxes[0].Width;
            }
            else if(imgCount > 7 && index <= 3)
            {
                ViewedPictureBoxes = PictureBoxes.Take(7).ToList();
                HighlightPanelLocation = index * ViewedPictureBoxes[0].Width;
            }
            else if(imgCount > 7 && 
                index >= imgCount - 4)
            {
                ViewedPictureBoxes = PictureBoxes.TakeLast(7).ToList();
                HighlightPanelLocation = panelWidth - ((imgCount - index) * ViewedPictureBoxes[0].Width);
            }
            else if(imgCount > 7 
                && index > 3
                && index < imgCount - 4)
            {
                ViewedPictureBoxes.Clear();
                for(int i = index - 3; i <= index + 3; i++)
                {
                    ViewedPictureBoxes.Add(PictureBoxes[i]);
                }
                HighlightPanelLocation = 3 * ViewedPictureBoxes[0].Width;
            }
        }


        private void PictureBox_Click(object? sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            int index = PictureBoxes.IndexOf(pictureBox);
            TutorialImages.SetSelectedImage(index);
        }
    }
}
