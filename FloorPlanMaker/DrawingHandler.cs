using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMaker
{
    public class DrawingHandler
    {
        private bool lastPointWasRightClick = false;
        private Panel targetPanel;
        private List<Point> points;

        public bool DrawSectionLinesMode { get; set; }

        public DrawingHandler(Panel panel)
        {
            this.targetPanel = panel;
            this.points = new List<Point>();

            this.targetPanel.MouseDown += Panel_MouseDown;
            this.targetPanel.Paint += Panel_Paint;
        }

        //private void Panel_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (DrawSectionLinesMode && e.Button == MouseButtons.Left)
        //    {
        //        points.Add(e.Location);
        //        targetPanel.Invalidate();
        //    }
        //}
        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (DrawSectionLinesMode)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (lastPointWasRightClick)
                    {
                        // If last point was a right-click, clear the last point added
                        points.RemoveAt(points.Count - 1);
                        lastPointWasRightClick = false;
                    }
                    points.Add(e.Location);
                    targetPanel.Invalidate();
                }
                else if (e.Button == MouseButtons.Right)
                {
                    lastPointWasRightClick = true;
                    points.Add(e.Location); // This is a breakpoint, and might be removed later
                    targetPanel.Invalidate();
                }
            }
        }


        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            if (!DrawSectionLinesMode || points.Count == 0)
                return;

            using (Pen pen = new Pen(Color.Black))
            using (Brush brush = new SolidBrush(Color.Black))
            {
                for (int i = 0; i < points.Count - 1; i++)
                {
                    e.Graphics.DrawLine(pen, points[i], points[i + 1]);
                }

                foreach (Point point in points)
                {
                    e.Graphics.FillEllipse(brush, point.X - 2, point.Y - 2, 5, 5);
                }
            }
        }

        public void ClearLines()
        {
            points.Clear();
            targetPanel.Invalidate();
        }
        public void UndoLastPoint()
        {
            if (points.Count > 0)
            {
                points.RemoveAt(points.Count - 1);
                targetPanel.Invalidate();
            }
        }

    }

}
