using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    using FloorPlanMaker;
    using FloorPlanMakerUI;
    using FloorplanUserControlLibrary;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public class FloorplanPrinter
    {
        private Panel _floorplanPanel;
        private List<Edge> _edges;
        private SectionLineDrawer _lineDrawer;

        public FloorplanPrinter(Panel floorplanPanel)
        {
            _floorplanPanel = floorplanPanel;
        }
        private List<SectionLine> _sectionLines;

        public FloorplanPrinter(Panel floorplanPanel, SectionLineDrawer lineDrawer, List<Edge> edges)
        {
            _floorplanPanel = floorplanPanel;
            //_sectionLines = sectionLines;
           _edges = edges;
           _lineDrawer = lineDrawer;
            
        }

        private void HandlePrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            // Calculate the scaling factor based on the entire page
            float scaleWidth = e.PageBounds.Width / (float)_floorplanPanel.Width;
            float scaleHeight = e.PageBounds.Height / (float)_floorplanPanel.Height;
            float scale = Math.Min(scaleWidth, scaleHeight);  // Use the smaller scale factor to ensure fit
            scale = scale - (float).07;
            // Adjust the origin if necessary. For example, if you want to center the scaled content:
            float offsetX = (e.PageBounds.Width - (_floorplanPanel.Width * scale)) / 2;
            float offsetY = (e.PageBounds.Height - (_floorplanPanel.Height * scale)) / 2;

            g.TranslateTransform(offsetX, offsetY);  // Move the origin
            g.ScaleTransform(scale, scale);           // Apply the scaling transformation

            // Draw TableControls first
            foreach (Control control in _floorplanPanel.Controls)
            {
                if (control is TableControl tableControl)
                {
                    // Draw the table in black and white
                    TableControl.DrawTableOnGraphics(g, tableControl, true);
                }
            }

            // Draw SectionControls next
            foreach (Control control in _floorplanPanel.Controls)
            {
                if (control is SectionLabelControl sectionControl)
                {
                    SectionLabelControl.DrawSectionLabelForPrinting(g, sectionControl);
                }
            }
            if (_lineDrawer != null && _edges != null)
            {
                _lineDrawer.DrawEdges(g, _edges);
            }
           
        }

      

        public void ShowPrintPreview()
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += HandlePrintPage;

            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog
            {
                Document = printDocument
            };
            printPreviewDialog.ShowDialog();
        }

        public void Print()
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += HandlePrintPage;

            PrintDialog printDialog = new PrintDialog
            {
                Document = printDocument
            };

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDialog.Document.Print();
            }
        }
    }

}
