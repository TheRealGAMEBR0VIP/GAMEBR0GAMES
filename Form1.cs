using System;
using System.Drawing;
using System.Windows.Forms;
using GAMEBR0GAMES.UserControls;
using GAMEBR0GAMES.Config;

namespace GAMEBR0GAMES
{
    public partial class Form1 : Form
    {
        private Panel topBar = new();
        private Panel leftBar = new();
        private Panel rightBar = new();
        private Panel bottomBar = new();
        private SlantedPanel hudPanel = new();
        private SlantedPanel mainPanel = new();
        private DayZAddonBuilder dayZBuilder;
        private const int PANEL_GAP = 20;
        private const int SIDE_MARGIN = 50;

        // Store the validated paths
        private string diagExePath = string.Empty;
        private string serverExePath = string.Empty;
        private string workbenchExePath = string.Empty;

        public Form1()
        {
            InitializeComponent();
            dayZBuilder = new DayZAddonBuilder(new ServerConfig());
            dayZBuilder.PathsValidated += DayZBuilder_PathsValidated;

            SetupBars();
            SetupPanels();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Resize += Form1_Resize;
        }

        private void SetupBars()
        {
            var barColor = Color.FromArgb(200, 0, 0, 0);
            const int barThickness = 30;

            topBar.BackColor = barColor;
            topBar.Dock = DockStyle.Top;
            topBar.Height = barThickness;
            bottomBar.BackColor = barColor;
            bottomBar.Dock = DockStyle.Bottom;
            bottomBar.Height = barThickness;
            leftBar.BackColor = barColor;
            leftBar.Dock = DockStyle.Left;
            leftBar.Width = barThickness;
            rightBar.BackColor = barColor;
            rightBar.Dock = DockStyle.Right;
            rightBar.Width = barThickness;

            Controls.Add(topBar);
            Controls.Add(bottomBar);
            Controls.Add(leftBar);
            Controls.Add(rightBar);
        }

        private void SetupPanels()
        {
            // Calculate available width
            int availableWidth = ClientSize.Width - (2 * SIDE_MARGIN) - PANEL_GAP;
            
            // Left HUD panel (1/4 of available width)
            hudPanel.BackColor = Color.FromArgb(200, 0, 0, 0);
            hudPanel.Width = availableWidth / 4;
            hudPanel.Height = (int)(ClientSize.Height * 0.75);

            // Main panel (3/4 of available width)
            mainPanel.BackColor = Color.FromArgb(200, 0, 0, 0);
            mainPanel.Width = (availableWidth * 3) / 4;
            mainPanel.Height = (int)(ClientSize.Height * 0.75);
            mainPanel.Visible = false; // Hidden by default

            // Position panels
            int verticalPosition = (ClientSize.Height - hudPanel.Height) / 2;
            hudPanel.Location = new Point(SIDE_MARGIN, verticalPosition);
            mainPanel.Location = new Point(hudPanel.Right + PANEL_GAP, verticalPosition);

            Controls.Add(hudPanel);
            Controls.Add(mainPanel);
            mainPanel.BringToFront();
            hudPanel.BringToFront();

            SetupControls();
        }

        private void SetupControls()
        {
            // Add Server Profile button to hudPanel
            var serverProfileBtn = new MenuButton("Server Profile");
            serverProfileBtn.ButtonClicked += (s, e) => 
            {
                if (!mainPanel.Visible)
                {
                    mainPanel.Visible = true;
                }
                else
                {
                    mainPanel.Visible = false;
                }
            };
            hudPanel.Controls.Add(serverProfileBtn);
        }

        private void DayZBuilder_PathsValidated(object? sender, PathsValidatedEventArgs e)
        {
            // Store the validated paths
            diagExePath = e.DiagPath;
            serverExePath = e.ServerPath;
            workbenchExePath = e.WorkbenchPath;

            MessageBox.Show("Paths validated and saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_Resize(object? sender, EventArgs e)
        {
            if (hudPanel != null && mainPanel != null)
            {
                // Calculate available width
                int availableWidth = ClientSize.Width - (2 * SIDE_MARGIN) - PANEL_GAP;
                
                // Update panel sizes
                hudPanel.Width = availableWidth / 4;
                hudPanel.Height = (int)(ClientSize.Height * 0.75);
                mainPanel.Width = (availableWidth * 3) / 4;
                mainPanel.Height = (int)(ClientSize.Height * 0.75);

                // Update panel positions
                int verticalPosition = (ClientSize.Height - hudPanel.Height) / 2;
                hudPanel.Location = new Point(SIDE_MARGIN, verticalPosition);
                mainPanel.Location = new Point(hudPanel.Right + PANEL_GAP, verticalPosition);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
        }
    }
}
