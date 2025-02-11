using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace GAMEBR0GAMES.UserControls
{
    public class ExeInputControl : UserControl
    {
        private TextBox exeTextBox;
        private Button browseButton;
        private string requiredExeName;

        public string ExePath
        {
            get => exeTextBox.Text;
            set => exeTextBox.Text = value;
        }

        public ExeInputControl(string labelText, string defaultValue, string requiredExeName)
        {
            this.requiredExeName = requiredExeName;
            InitializeComponent(labelText, defaultValue);
        }

        private void InitializeComponent(string labelText, string defaultValue)
        {
            var label = new Label
            {
                Text = labelText,
                Dock = DockStyle.Fill,
                ForeColor = Color.White,
                Margin = new Padding(0, 10, 0, 0)
            };

            exeTextBox = new TextBox
            {
                Text = defaultValue ?? "",
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(64, 64, 64),
                ForeColor = Color.White,
                Height = 30
            };

            browseButton = new Button
            {
                Text = "...",
                BackColor = Color.FromArgb(64, 64, 64),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Height = 30,
                AutoSize = false,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            browseButton.Click += BrowseButton_Click;

            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                Padding = new Padding(0)
            };

            panel.Controls.Add(label, 0, 0);
            panel.SetColumnSpan(label, 2);
            panel.Controls.Add(exeTextBox, 0, 1);
            panel.Controls.Add(browseButton, 1, 1);

            this.Controls.Add(panel);
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (Path.GetFileName(openFileDialog.FileName).Equals(requiredExeName, StringComparison.OrdinalIgnoreCase))
                    {
                        exeTextBox.Text = openFileDialog.FileName;
                    }
                    else
                    {
                        MessageBox.Show($"Please select the correct executable: {requiredExeName}", "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
