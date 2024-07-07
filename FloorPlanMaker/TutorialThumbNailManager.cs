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
        public List<PictureBox> PictureBoxes { get; set; } = new List<PictureBox>();
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
