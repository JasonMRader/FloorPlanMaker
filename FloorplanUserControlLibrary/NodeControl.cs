using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorplanUserControlLibrary
{
    public partial class NodeControl : UserControl
    {
        public Node Node { get; set; }
        private ToolTip toolTip1;
        public string displayString { get; private set; }
        public void SetDisplayString(string name)
        {
            this.displayString += name;
            this.lblName.Text = name;
        }

        public NodeControl(Node node, NodePosition position)
        {
            InitializeComponent();
            Node = node;
            this.BackColor = Node.Section.Color;
            this.lblNodeNumber.Text = node.HierarchyNumber.ToString();

            GetPosition( position );

            toolTip1 = new ToolTip();
            // Set up the delays for the tooltip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the tooltip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            // Set up the Tooltip text for the NodeControl
            toolTip1.SetToolTip(this.lblNodeNumber, $"Node Number: {Node.HierarchyNumber}\n" +
                $"Node X: {Node.X}  Node Y: {Node.Y}\n" +
                $"{position}");
        }

        private void GetPosition(NodePosition position)
        {
            if( position == NodePosition.Left )
            {
               
            }
            if( position == NodePosition.Right )
            {

            }
            if( position == NodePosition.Top )
            {
                this.Location = new Point(this.Node.X - this.Width, Node.Y - this.Height);
            }
            if ( position == NodePosition.Bottom )
            {
                this.Location = new Point(this.Node.X - this.Width, Node.Y);
            }

        }
        public enum NodePosition
        {
            Top,
            Left,
            Right,
            Bottom,
        }


    }
}
