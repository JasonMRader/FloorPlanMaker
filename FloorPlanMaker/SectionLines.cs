using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMaker
{
    public class SectionLines : Control
    {
        private Point MouseDownLocation;
        private bool isDraggingTop = false;
        private bool isDraggingBottom = false;
        private bool isDraggingLeft = false;
        private bool isDraggingRight = false;
        public const int LineOffset = 5; // Distance from TableControl
        private const int DragTolerance = 3; // Tolerance for grabbing the line

        public bool TopVisible { get; set; } = true;
        public bool BottomVisible { get; set; } = true;
        public bool LeftVisible { get; set; } = true;
        public bool RightVisible { get; set; } = true;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (Pen pen = new Pen(Color.Black) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
            {
                if (TopVisible) e.Graphics.DrawLine(pen, 0, LineOffset, Width, LineOffset);
                if (BottomVisible) e.Graphics.DrawLine(pen, 0, Height - LineOffset, Width, Height - LineOffset);
                if (LeftVisible) e.Graphics.DrawLine(pen, LineOffset, 0, LineOffset, Height);
                if (RightVisible) e.Graphics.DrawLine(pen, Width - LineOffset, 0, Width - LineOffset, Height);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            MouseDownLocation = e.Location;

            if (Math.Abs(e.Y - LineOffset) <= DragTolerance) isDraggingTop = true;
            if (Math.Abs(e.Y - (Height - LineOffset)) <= DragTolerance) isDraggingBottom = true;
            if (Math.Abs(e.X - LineOffset) <= DragTolerance) isDraggingLeft = true;
            if (Math.Abs(e.X - (Width - LineOffset)) <= DragTolerance) isDraggingRight = true;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            ResetDragFlags();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (isDraggingTop)
            {
                int difference = MouseDownLocation.Y - e.Y;
                Height += difference;
                Top -= difference;
            }
            else if (isDraggingBottom)
            {
                Height = e.Y;
            }
            else if (isDraggingLeft)
            {
                int difference = MouseDownLocation.X - e.X;
                Width += difference;
                Left -= difference;
            }
            else if (isDraggingRight)
            {
                Width = e.X;
            }

            Invalidate();
        }

        private void ResetDragFlags()
        {
            isDraggingTop = false;
            isDraggingBottom = false;
            isDraggingLeft = false;
            isDraggingRight = false;
        }
    }

}
