﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace FloorplanClassLibrary
{
    using FloorPlanMaker;
    using FloorPlanMakerUI;
    using FloorplanUserControlLibrary;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public class FloorplanPrinter
    {
        private Panel _floorplanPanel;
        private List<FloorplanLine> _lines;
       // private List<Edge> _edges;
        //private SectionLineDrawer _lineDrawer;

        public FloorplanPrinter(Panel floorplanPanel)
        {
            _floorplanPanel = floorplanPanel;
        }
        public FloorplanPrinter(Panel floorplanPanel, List<FloorplanLine> lines)

        {
            _floorplanPanel = floorplanPanel;
            _lines = lines;
        }
        private List<SectionLine> _sectionLines;

        //public FloorplanPrinter(Panel floorplanPanel, SectionLineDrawer lineDrawer, List<Edge> edges)
        //{
        //    _floorplanPanel = floorplanPanel;
        //    //_sectionLines = sectionLines;
        //   //_edges = edges;
        //  // _lineDrawer = lineDrawer;

        //}

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
            foreach (var line in _lines)
            {
                using (Pen pen = new Pen(line.LineColor, line.LineThickness))
                {
                    e.Graphics.DrawLine(pen, line.StartPoint, line.EndPoint);
                }
            }
            //if (_lineDrawer != null && _edges != null)
            //{
            //    _lineDrawer.DrawEdges(g, _edges);
            //}

        }

        public void CreatePdf(string filePath, bool isBlackAndWhite)
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Calculate the scaling factor based on the entire page
            float scaleWidth = (float)page.Width / _floorplanPanel.Width;
            float scaleHeight = (float)page.Height / _floorplanPanel.Height;
            float scale = Math.Min(scaleWidth, scaleHeight);  // Use the smaller scale factor to ensure fit

            // Adjust the origin to center the content
            float offsetX = ((float)page.Width - (_floorplanPanel.Width * scale)) / 2;
            float offsetY = ((float)page.Height - (_floorplanPanel.Height * scale)) / 2;

            gfx.TranslateTransform(offsetX, offsetY);  // Move the origin
            gfx.ScaleTransform(scale, scale);          // Apply the scaling transformation

            // Draw TableControls first
            foreach (Control control in _floorplanPanel.Controls)
            {
                if (control is TableControl tableControl)
                {
                    // Draw the table in black and white
                    TableControl.DrawTableOnGraphics(gfx, tableControl, isBlackAndWhite);
                }
            }

            // Draw SectionControls next
            foreach (Control control in _floorplanPanel.Controls)
            {
                if (control is SectionLabelControl sectionControl)
                {
                    SectionLabelControl.DrawSectionLabelForPrinting(gfx, sectionControl, isBlackAndWhite);
                }
            }

            // Draw the lines
            foreach (var line in _lines)
            {
                XPen pen = new XPen(line.LineColor.ToXColor(), line.LineThickness);
                gfx.DrawLine(pen, line.StartPoint.X, line.StartPoint.Y, line.EndPoint.X, line.EndPoint.Y); // Use the transformed graphics context
            }

            document.Save(filePath);
            Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true }); // Open the PDF file with the default PDF viewer
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
