using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using GAMEBR0GAMES.Config;

namespace GAMEBR0GAMES.UserControls
{
    public partial class DayZAddonBuilder : UserControl
    {
        private TextBox diagExeTextBox;
        private TextBox serverExeTextBox;
        private TextBox workbenchExeTextBox;
        private Button mainButton;
        private TabControl tabControl;
        private ServerTabPage serverTab;

        public event EventHandler<PathsValidatedEventArgs> PathsValidated;

        private ServerConfig config;

        public DayZAddonBuilder(ServerConfig configInstance)
        {
            this.config = configInstance;
            InitializeComponent();
            CheckServerTabVisibility();
        }

        private void InitializeComponent()
        {
            this.BackColor = Color.FromArgb(255, 0, 0, 0);
            this.ForeColor = Color.White;
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(20);

            // Tab Control
            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;
            tabControl.Selected += TabControl_Selected;

            // Create Profile Tab
            var createProfileTab = new TabPage("CREATE SERVER PROFILE");
            createProfileTab.BackColor = Color.FromArgb(255, 0, 0, 0);
            createProfileTab.Font = new Font("Arial", 12, FontStyle.Bold);

            var createProfilePanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 8,
                Padding = new Padding(20),
                ColumnStyles = {
                    new ColumnStyle(SizeType.Percent, 80),
                    new ColumnStyle(SizeType.Percent, 20)
                }
            };

            // Add row styles
            for (int i = 0; i < 8; i++)
            {
                createProfilePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            }

            // Title Label
            var titleLabel = new Label
            {
                Text = "NEW PROFILE",
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Height = 40
            };
            createProfilePanel.Controls.Add(titleLabel, 0, 0);
            createProfilePanel.SetColumnSpan(titleLabel, 2);

            // Add exe inputs
            AddExeInput(createProfilePanel, "DayZDiag.exe", config.DiagExePath, 1, "DayZDiag_x64.exe");
            AddExeInput(createProfilePanel, "DayZServer_x64.exe", config.ServerExePath, 3, "DayZServer_x64.exe");
            AddExeInput(createProfilePanel, "Workbench.exe", config.WorkbenchExePath, 5, "workbenchApp.exe");

            // Main Button
            mainButton = new Button
            {
                Text = "Create",
                BackColor = Color.FromArgb(255, 217, 119),
                ForeColor = Color.Black,
                Dock = DockStyle.Bottom,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(5)
            };
            mainButton.Click += MainButton_Click;

            createProfileTab.Controls.Add(createProfilePanel);
            tabControl.Controls.Add(createProfileTab);

            this.Controls.Add(mainButton);
            this.Controls.Add(tabControl);
        }

        private void CheckServerTabVisibility()
        {
            string batchFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GAMEBR0GAMES", "Server.bat");
            if (File.Exists(batchFilePath) && serverTab == null)
            {
                CreateServerTab();
            }
        }

        private void CreateServerTab()
        {
            serverTab = new ServerTabPage(config);
            tabControl.Controls.Add(serverTab);
            tabControl.SelectedTab = serverTab;
        }

        private void TabControl_Selected(object sender, TabControlEventArgs e)
        {
            mainButton.Text = e.TabPage.Text == "Server" ? "Launch Server" : "Create";
        }

        private void AddExeInput(TableLayoutPanel panel, string labelText, string defaultValue, int row, string requiredExeName)
        {
            var label = new Label
            {
                Text = labelText,
                Dock = DockStyle.Fill,
                ForeColor = Color.White,
                Margin = new Padding(0, 10, 0, 0)
            };

            var textBox = new TextBox
            {
                Text = defaultValue ?? "",
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(64, 64, 64),
                ForeColor = Color.White,
                Height = 30
            };

            var browseButton = new Button
            {
                Text = "...",
                BackColor = Color.FromArgb(64, 64, 64),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Height = 30,
                AutoSize = false,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            browseButton.Click += (s, e) =>
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 1;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (Path.GetFileName(openFileDialog.FileName).Equals(requiredExeName, StringComparison.OrdinalIgnoreCase))
                        {
                            textBox.Text = openFileDialog.FileName;
                        }
                        else
                        {
                            MessageBox.Show($"Please select the correct executable: {requiredExeName}", "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            };

            panel.Controls.Add(label, 0, row);
            panel.SetColumnSpan(label, 2);
            panel.Controls.Add(textBox, 0, row + 1);
            panel.Controls.Add(browseButton, 1, row + 1);

            switch (row)
            {
                case 1: diagExeTextBox = textBox; break;
                case 3: serverExeTextBox = textBox; break;
                case 5: workbenchExeTextBox = textBox; break;
            }
        }

        private void MainButton_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab.Text == "Server")
            {
                if (serverTab != null)
                {
                    serverTab.LaunchServer();
                }
            }
            else if (ValidatePaths())
            {
                config.DiagExePath = diagExeTextBox.Text;
                config.ServerExePath = serverExeTextBox.Text;
                config.WorkbenchExePath = workbenchExeTextBox.Text;
                config.Save();

                DebugProfileGenerator.GenerateDebugProfile("Debugprofile.cfg");
                BatchFileGenerator.GenerateBatchFile("Server.bat", diagExeTextBox.Text, serverExeTextBox.Text);

                CreateServerTab();
                
                PathsValidated?.Invoke(this, new PathsValidatedEventArgs(
                    diagExeTextBox.Text,
                    serverExeTextBox.Text,
                    workbenchExeTextBox.Text
                ));
            }
        }

        private bool ValidatePaths()
        {
            if (!ValidateExePath(diagExeTextBox.Text, "DayZDiag_x64.exe")) return false;
            if (!ValidateExePath(serverExeTextBox.Text, "DayZServer_x64.exe")) return false;
            if (!ValidateExePath(workbenchExeTextBox.Text, "workbenchApp.exe")) return false;
            return true;
        }

        private bool ValidateExePath(string path, string requiredExeName)
        {
            if (!File.Exists(path) || 
                !Path.GetFileName(path).Equals(requiredExeName, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show($"Please select a valid {requiredExeName} file.", "Invalid Path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }

    public class PathsValidatedEventArgs : EventArgs
    {
        public string DiagPath { get; }
        public string ServerPath { get; }
        public string WorkbenchPath { get; }

        public PathsValidatedEventArgs(string diagPath, string serverPath, string workbenchPath)
        {
            DiagPath = diagPath;
            ServerPath = serverPath;
            WorkbenchPath = workbenchPath;
        }
    }
}
