using FloorPlanMakerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanUserControlLibrary
{
    public static class NotificationHandler
    {
        public static void ShowNotificationLabel(Control parent, string text, Color backColor, Color foreColor, 
            Point location, int width, int height, TimeSpan timeShown)
        {
            
            Label label = new Label
            {
                Text = text,
                BackColor = backColor,
                ForeColor = foreColor,
                Location = location,
                AutoSize = false,
                Size = new Size(width, height),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = UITheme.MainFont,
                Visible = true
            };

            // Add the label to the parent control (e.g., a form or panel)
            parent.Controls.Add(label);
            label.BringToFront();

            // Initialize the timer
            System.Timers.Timer timer = new System.Timers.Timer(timeShown.TotalMilliseconds);
            timer.Elapsed += (sender, e) =>
            {
                // Remove the label and dispose of the timer
                label.Invoke((MethodInvoker)(() =>
                {
                    parent.Controls.Remove(label);
                    label.Dispose();
                }));
                timer.Dispose();
            };
            timer.AutoReset = false; // Ensure the timer only fires once
            timer.Start();
        }
        public static void ShowNotificationOverControl(Control parent, string text, Color backColor, Color foreColor,
             TimeSpan timeShown)
        {

            Label label = new Label
            {
                Text = text,
                BackColor = backColor,
                ForeColor = foreColor,
                Location = parent.Location,
                AutoSize = false,
                Size = new Size(parent.Width, parent.Height),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = UITheme.MainFont,
                Visible = true
            };

            // Add the label to the parent control (e.g., a form or panel)
            parent.Controls.Add(label);
            label.BringToFront();

            // Initialize the timer
            System.Timers.Timer timer = new System.Timers.Timer(timeShown.TotalMilliseconds);
            timer.Elapsed += (sender, e) =>
            {
                // Remove the label and dispose of the timer
                label.Invoke((MethodInvoker)(() =>
                {
                    parent.Controls.Remove(label);
                    label.Dispose();
                }));
                timer.Dispose();
            };
            timer.AutoReset = false; // Ensure the timer only fires once
            timer.Start();
        }
    }
}
