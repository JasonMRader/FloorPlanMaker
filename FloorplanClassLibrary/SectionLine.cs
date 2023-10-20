using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMaker
{
    public class SectionLine : Control
    {
        public int ID { get; set; }
        private Point _startPoint;
        private Point _endPoint;
        public SectionLine()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            this.Invalidate();
        }
        public Point StartPoint
        {
            get { return _startPoint; }
            set { _startPoint = value; SetBounds(); }
        }
        public Point EndPoint
        {
            get { return _endPoint; }
            set { _endPoint = value; SetBounds(); }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            using (Pen pen = new Pen(Color.Black, 15))
            {
                // Convert absolute points to relative points
                var relativeStart = new Point(_startPoint.X - this.Left, _startPoint.Y - this.Top);
                var relativeEnd = new Point(_endPoint.X - this.Left, _endPoint.Y - this.Top);

                pe.Graphics.DrawLine(pen, relativeStart, relativeEnd);
            }
        }

        // Determine and set the bounding box for the line
        private void SetBounds()
        {
            int x = Math.Min(_startPoint.X, _endPoint.X);
            int y = Math.Min(_startPoint.Y, _endPoint.Y);
            int width = Math.Abs(_startPoint.X - _endPoint.X);
            int height = Math.Abs(_startPoint.Y - _endPoint.Y);

            if (width == 0) width = 1;  // Ensure it has at least a pixel to display in the case of vertical line
            if (height == 0) height = 1; // Ensure it has at least a pixel to display in the case of horizontal line

            this.SetBounds(x, y, width, height);
        }
    }


}


