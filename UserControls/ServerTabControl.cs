using System;
using System.Drawing;
using System.Windows.Forms;

namespace GAMEBR0GAMES.UserControls
{
    public class ServerTabEntry : Panel
    {
        private Button addButton;
        private bool isValid = false;

        public event EventHandler AddNewRequested;
        public event EventHandler<ModEntryValidatedEventArgs> ModValidated;

        private string modPath;
        private string modName;

        public string ModPath
        {
            get => modPath;
            set
            {
                modPath = value;
                ValidateModEntry();
            }
        }

        public string ModName
        {
            get => modName;
            set
            {
                modName = value;
                ValidateModEntry();
            }
        }

        public bool IsValid => isValid;

        public ServerTabEntry()
        {
            InitializeComponent();
            modPath = string.Empty; // Initialize ModPath
            modName = string.Empty; // Initialize ModName
        }

        private void InitializeComponent()
        {
            this.BackColor = Color.FromArgb(180, 0, 0, 0);
            this.Height = 140;
            this.Width = 400;

            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 1,
                Padding = new Padding(5)
            };

            // Add Button
            addButton = new Button
            {
                Text = "+",
                BackColor = Color.FromArgb(64, 64, 64),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Height = 30,
                Width = 30,
                Margin = new Padding(5),
                Visible = false
            };
            addButton.Click += (s, e) => AddNewRequested?.Invoke(this, EventArgs.Empty);

            // Add controls to layout
            mainLayout.Controls.Add(addButton);

            this.Controls.Add(mainLayout);
        }

        private void ValidateModEntry()
        {
            isValid = !string.IsNullOrEmpty(modPath) && !string.IsNullOrEmpty(modName);
            ModValidated?.Invoke(this, new ModEntryValidatedEventArgs(modPath, modName));
        }

        public void ShowAddButton(bool show)
        {
            addButton.Visible = show;
            addButton.Location = new Point((this.Width - addButton.Width) / 2, this.Height - addButton.Height - 10);
        }
    }
}
