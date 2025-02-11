using System;
using System.Drawing;
using System.Windows.Forms;

namespace GAMEBR0GAMES.UserControls
{
    public class MenuButton : Button
    {
        public event EventHandler<EventArgs> ButtonClicked;

        public MenuButton(string text)
        {
            Text = text;
            ForeColor = Color.White;
            BackColor = Color.FromArgb(200, 0, 0, 0);
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 1;
            FlatAppearance.BorderColor = Color.FromArgb(100, 255, 255, 255);
            Font = new Font("Arial", 11, FontStyle.Regular); // Reduced font size
            Dock = DockStyle.Top;
            Height = 60; // Further increased height
            Margin = new Padding(10);
            Padding = new Padding(20, 10, 20, 10); // Adjusted padding
            TextAlign = ContentAlignment.MiddleLeft;
            Cursor = Cursors.Hand;
            AutoSize = false;

            // Events
            MouseEnter += (s, e) => BackColor = Color.FromArgb(150, 0, 0, 0);
            MouseLeave += (s, e) => BackColor = Color.FromArgb(200, 0, 0, 0);
            Click += (s, e) => ButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
