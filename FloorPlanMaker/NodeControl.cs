using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMaker
{
    public class NodeControl : Control
    {
        private const int NodeDiameter = 10;

        public bool IsLocked { get; set; }
        public bool IsActive { get; set; }

        public Point Center => new Point(Left + Width / 2, Top + Height / 2);

        private static NodeControl activeNode;

        public NodeControl()
        {
            this.Width = NodeDiameter;
            this.Height = NodeDiameter;
            this.MouseDown += NodeControl_MouseDown;
            this.MouseMove += NodeControl_MouseMove;
        }

        private void NodeControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsLocked && e.Button == MouseButtons.Left)
            {
                Left += e.X - (Width / 2);
                Top += e.Y - (Height / 2);
            }
        }

        private void NodeControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsLocked)
            {
                if (IsActive)
                {
                    IsActive = false;
                    activeNode = null;
                    this.Invalidate();
                }
                else if (activeNode == null)
                {
                    IsActive = true;
                    activeNode = this;
                    this.Invalidate();
                }
                else if (activeNode != null)
                {
                    using (Graphics g = Parent.CreateGraphics())
                    {
                        g.DrawLine(Pens.Black, Center, activeNode.Center);
                    }
                    IsActive = true;
                    activeNode.IsActive = false;
                    this.Invalidate();
                    activeNode = null;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            using (Brush brush = new SolidBrush(IsActive ? Color.Red : Color.Black))
            {
                pe.Graphics.FillEllipse(brush, 0, 0, NodeDiameter, NodeDiameter);
            }
        }
    }

}
