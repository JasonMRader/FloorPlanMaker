using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    using FloorPlanMaker;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public class FloorplanPrinter
    {
        private Panel _floorplanPanel;

        public FloorplanPrinter(Panel floorplanPanel)
        {
            _floorplanPanel = floorplanPanel;
        }
        private List<SectionLine> _sectionLines;

        public FloorplanPrinter(Panel floorplanPanel, List<SectionLine> sectionLines)
        {
            _floorplanPanel = floorplanPanel;
            _sectionLines = sectionLines;
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

            // Draw SectionLines
            foreach (SectionLine sectionLine in _sectionLines)
            {
                Point adjustedStart = new Point(sectionLine.StartPoint.X, sectionLine.StartPoint.Y);
                Point adjustedEnd = new Point(sectionLine.EndPoint.X, sectionLine.EndPoint.Y);
                DrawingHandler.Printer_Paint(g, sectionLine, adjustedStart, adjustedEnd);
            }
        }

        //private void HandlePrintPage(object sender, PrintPageEventArgs e)
        //{
        //    Graphics g = e.Graphics;

        //    // Calculate the scaling factor
        //    float scaleWidth = e.MarginBounds.Width / (float)_floorplanPanel.Width;
        //    float scaleHeight = e.MarginBounds.Height / (float)_floorplanPanel.Height;
        //    float scale = Math.Min(scaleWidth, scaleHeight);  // Use the smaller scale factor to ensure fit

        //    // Apply the scaling transformation
        //    g.ScaleTransform(scale, scale);

        //    foreach (Control control in _floorplanPanel.Controls)
        //    {
        //        if (control is TableControl tableControl)
        //        {
        //            // Draw the table in black and white
        //            TableControl.DrawTableOnGraphics(g, tableControl, true);
        //            // Add any other specific drawing logic for TableControl
        //        }
        //        else if (control is SectionControl sectionControl)
        //        {
        //            SectionControl.DrawSectionLabelForPrinting(g, sectionControl);
        //            sectionControl.BringToFront();
        //        }
        //        foreach (SectionLine sectionLine in _sectionLines)
        //        {
        //            Point adjustedStart = new Point(sectionLine.StartPoint.X, sectionLine.StartPoint.Y);
        //            Point adjustedEnd = new Point(sectionLine.EndPoint.X, sectionLine.EndPoint.Y);
        //            DrawingHandler.Printer_Paint(g, sectionLine, adjustedStart, adjustedEnd);
        //        }
        //    }
        //}


        //private void HandlePrintPage(object sender, PrintPageEventArgs e)
        //{
        //    Graphics g = e.Graphics;

        //    // First, draw all the TableControl instances
        //    foreach (Control control in _floorplanPanel.Controls)
        //    {
        //        if (control is TableControl tableControl)
        //        {
        //            // Draw the table in black and white
        //            TableControl.DrawTableOnGraphics(g, tableControl, true);
        //            // Add any other specific drawing logic for TableControl
        //        }
        //    }

        //    // Then, draw all the SectionControl instances so they appear on top
        //    foreach (Control control in _floorplanPanel.Controls)
        //    {
        //        if (control is SectionControl sectionControl)
        //        {
        //            SectionControl.DrawSectionLabelForPrinting(g, sectionControl);
        //            sectionControl.BringToFront(); // Note: this may not have an effect on the Graphics object
        //        }
        //    }

        //    foreach (SectionLine sectionLine in _sectionLines)
        //    {
        //        Point adjustedStart = new Point(sectionLine.StartPoint.X, sectionLine.StartPoint.Y);
        //        Point adjustedEnd = new Point(sectionLine.EndPoint.X, sectionLine.EndPoint.Y);
        //        DrawingHandler.Printer_Paint(g, sectionLine, adjustedStart, adjustedEnd);
        //    }
        //}


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
