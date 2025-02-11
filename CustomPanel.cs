using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GAMEBR0GAMES
{
    public class SlantedPanel : Panel
    {
        public SlantedPanel()
        {
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (var path = new GraphicsPath())
            {
                int slantAmount = 20; // Amount of slant for top and bottom edges

                Point[] points = new Point[]
                {
                    new Point(0, slantAmount),                    // Top-left
                    new Point(Width, 0),                          // Top-right
                    new Point(Width, Height - slantAmount),       // Bottom-right
                    new Point(0, Height)                          // Bottom-left
                };

                path.AddPolygon(points);

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(this.BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }
    }
}
