using System;
using System.Windows.Forms;
using System.Drawing;

namespace GAMEBR0GAMES.UserControls
{
    public class ModEntry : UserControl
    {
        private TextBox modNameTextBox;
        private TextBox modPathTextBox;
        private Button browseButton;
        private Panel containerPanel;

        public string ModPath 
        { 
            get => modPathTextBox.Text;
            set => modPathTextBox.Text = value;
        }
        
        public string ModName 
        { 
            get => modNameTextBox.Text;
            set => modNameTextBox.Text = value;
        }
        
        public bool IsValid { get; private set; }

        public event EventHandler AddNewRequested;
        public event EventHandler<ModEntryValidatedEventArgs> ModValidated;

        public ModEntry()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.AutoSize = true;
            this.Margin = new Padding(0, 0, 0, 10);
            this.Width = 500;

            containerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                Padding = new Padding(10),
                BackColor = Color.FromArgb(64, 64, 64)
            };

            modNameTextBox = new TextBox
            {
                Location = new Point(10, 10),
                Width = 300,
                BackColor = Color.FromArgb(32, 32, 32),
                ForeColor = Color.White,
                PlaceholderText = "Mod Name"
            };
            modNameTextBox.TextChanged += ValidateInputs;

            modPathTextBox = new TextBox
            {
                Location = new Point(10, 40),
                Width = 300,
                BackColor = Color.FromArgb(32, 32, 32),
                ForeColor = Color.White,
                PlaceholderText = "Mod Path"
            };
            modPathTextBox.TextChanged += ValidateInputs;

            browseButton = new Button
            {
                Text = "Browse",
                Location = new Point(320, 40),
                Width = 75,
                BackColor = Color.FromArgb(217, 119, 6),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            browseButton.Click += BrowseButton_Click;

            containerPanel.Controls.Add(modNameTextBox);
            containerPanel.Controls.Add(modPathTextBox);
            containerPanel.Controls.Add(browseButton);

            this.Controls.Add(containerPanel);
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
            {
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    ModPath = folderBrowser.SelectedPath;
                    if (string.IsNullOrEmpty(ModName))
                    {
                        ModName = System.IO.Path.GetFileName(folderBrowser.SelectedPath);
                    }
                    ValidateInputs(null, null);
                }
            }
        }

        private void ValidateInputs(object sender, EventArgs e)
        {
            bool wasValid = IsValid;
            IsValid = !string.IsNullOrWhiteSpace(ModPath) && !string.IsNullOrWhiteSpace(ModName);
            
            if (wasValid != IsValid)
            {
                ModValidated?.Invoke(this, new ModEntryValidatedEventArgs(ModPath, ModName));
            }
        }
    }

    public class ModEntryValidatedEventArgs : EventArgs
    {
        public string ModPath { get; }
        public string ModName { get; }

        public ModEntryValidatedEventArgs(string modPath, string modName)
        {
            ModPath = modPath;
            ModName = modName;
        }
    }
}
