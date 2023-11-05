using FloorplanClassLibrary;
using FloorPlanMaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMakerUI
{
    public class TableControlEditor : Panel
    {
        // UI components
        private TextBox txtTableNumber;
        private TextBox txtMaxCovers;
        private TextBox txtAverageCovers;
        private Button btnWidthIncrease;
        private Button btnWidthDecrease;
        private Button btnHeightIncrease;
        private Button btnHeightDecrease;
        private Button btnLockMove;
        public Table Table { get; set; }

        public TableControlEditor(Table table) 
        {
            this.Height = 200;
            this.Width = 200;
            this.Table = table;
            // Assume default sizes for buttons and text boxes
            Size buttonSize = new Size(50, 20); // width: 80px, height: 30px
            Size textBoxSize = new Size(30, 20); // width: 100px, height: 20px

            int spacing = 5; // Spacing between controls
            int controlStartY = spacing; // Start position on the Y axis

            // Initialize buttons with size and location
            btnHeightIncrease = new Button { Text = "Height +", Size = buttonSize };
            btnHeightIncrease.Location = new Point(this.Width / 2 - btnHeightIncrease.Width / 2, controlStartY);

            controlStartY += btnHeightIncrease.Height + spacing;

            txtTableNumber = new TextBox { Text = this.Table.TableNumber, Size = textBoxSize };
            txtTableNumber.Location = new Point(this.Width / 2 - txtTableNumber.Width / 2, controlStartY);

            controlStartY += txtTableNumber.Height + spacing;

            txtMaxCovers = new TextBox { Text = this.Table.MaxCovers.ToString(), Size = textBoxSize };
            txtMaxCovers.Location = new Point(this.Width / 2 - txtMaxCovers.Width / 2, controlStartY);

            controlStartY += txtMaxCovers.Height + spacing;

            txtAverageCovers = new TextBox { Text = this.Table.AverageCovers.ToString(), Size = textBoxSize };
            txtAverageCovers.Location = new Point(this.Width / 2 - txtAverageCovers.Width / 2, controlStartY);

            controlStartY += txtAverageCovers.Height + spacing;

            btnHeightDecrease = new Button { Text = "Height -", Size = buttonSize };
            btnHeightDecrease.Location = new Point(this.Width / 2 - btnHeightDecrease.Width / 2, controlStartY);

            // Buttons for width are placed to the left and right of the control
            btnWidthDecrease = new Button { Text = "Width -", Size = buttonSize };
            btnWidthDecrease.Location = new Point(spacing, this.Height / 2 - btnWidthDecrease.Height / 2);

            btnWidthIncrease = new Button { Text = "Width +", Size = buttonSize };
            btnWidthIncrease.Location = new Point(this.Width - btnWidthIncrease.Width - spacing, this.Height / 2 - btnWidthIncrease.Height / 2);

            btnLockMove = new Button { Text = "Lock Move", Size = buttonSize };
            btnLockMove.Location = new Point(this.Width / 2 - btnLockMove.Width / 2, this.Height - btnLockMove.Height - spacing);

            // Subscribe to events and add the event handlers as before

            // Add controls to the TableControlEditor
            this.Controls.Add(btnHeightIncrease);
            this.Controls.Add(txtTableNumber);
            this.Controls.Add(txtMaxCovers);
            this.Controls.Add(txtAverageCovers);
            this.Controls.Add(btnHeightDecrease);
            this.Controls.Add(btnWidthDecrease);
            this.Controls.Add(btnWidthIncrease);
            this.Controls.Add(btnLockMove);
            this.Location = new Point(table.XCoordinate, table.YCoordinate);
            this.Tag = table;
        }


        // Add any additional functionality required for the editor here
    }
}
