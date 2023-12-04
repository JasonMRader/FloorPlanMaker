using FloorplanClassLibrary;
using FloorPlanMaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanUserControlLibrary
{
    public class MiniTableControl : TableControl
    {
        public TemplateTable TemplateTable { get; set; }
        public Section templateSection { get; set; }
        public MiniTableControl(TemplateTable table, float factor, int yAdjustment)
        {
            TemplateTable = table;
            //Width = (int)(table.Width * factor);
            //Height = (int)(table.Height * factor);
            //Left = (int)(table.XCoordinate * factor);
            //Top = (int)(table.YCoordinate * factor) + yAdjustment;
            Width = table.Width;
            Height = table.Height;
            Left = table.XCoordinate;
            Top = table.YCoordinate;
            Moveable = false;
            Shape = table.Shape;
            Location = new Point(table.XCoordinate, table.YCoordinate);
            //Location = new Point((int)(table.XCoordinate * factor), (int)(table.YCoordinate * factor) + yAdjustment);
            Tag = table;
            templateSection = table.Section;
            TableNumberFontSize = 10;
        }
        public string _tableNumber { get { return this.TemplateTable.TableNumber; } }
        protected override void OnPaint(PaintEventArgs pe)
        {
            DrawTableOnGraphics(pe.Graphics, this);
        }

        public static void DrawTableOnGraphics(Graphics g, MiniTableControl control, bool isForPrint = false)
        {
            control.BackColor = control.templateSection.Color;
            control.ForeColor = control.templateSection.FontColor;
            int xOffset = isForPrint ? control.Left : 0;
            int yOffset = isForPrint ? control.Top : 0;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using (Pen pen = new Pen(control.BorderColor, control.BorderThickness))
            {
                switch (control.Shape)
                {
                    case Table.TableShape.Circle:
                        g.DrawEllipse(pen, xOffset, yOffset, control.Width - control.BorderThickness, control.Height - control.BorderThickness);
                        break;
                    case Table.TableShape.Square:
                        g.DrawRectangle(pen, xOffset, yOffset, control.Width - control.BorderThickness, control.Height - control.BorderThickness);
                        break;
                    case Table.TableShape.Diamond:
                        Point[] diamondPoints = {
                    new Point(xOffset + control.Width / 2, yOffset),
                    new Point(xOffset + control.Width, yOffset + control.Height / 2),
                    new Point(xOffset + control.Width / 2, yOffset + control.Height),
                    new Point(xOffset, yOffset + control.Height / 2)
                };
                        g.DrawPolygon(pen, diamondPoints);
                        break;
                }
            }


            string textToDisplay = control._tableNumber.ToString();
           

            if (!string.IsNullOrEmpty(textToDisplay))
            {
                using (Font font = new Font(control.Font.FontFamily, control.TableNumberFontSize))
                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;


                    Rectangle tableBounds = new Rectangle(xOffset, yOffset, control.Width, control.Height);
                    using (Brush textBrush = new SolidBrush(control.templateSection.FontColor)) // Use the TextColor property
                    {
                        g.DrawString(textToDisplay, font, textBrush, tableBounds, sf);
                    }
                }
            }
        }
    }
}
