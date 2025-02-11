using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using GAMEBR0GAMES.Config;

namespace GAMEBR0GAMES.UserControls
{
    public class ServerTabPage : TabPage
    {
        private FlowLayoutPanel modEntriesPanel;
        private Button addNewModButton;
        private List<ModEntry> modEntries = new List<ModEntry>();
        private ServerConfig config;

        public ServerTabPage(ServerConfig config)
        {
            this.config = config;
            this.Text = "Server";
            this.BackColor = Color.FromArgb(255, 0, 0, 0);
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            var mainContainer = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            // Create the mod entries container
            modEntriesPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(10)
            };

            // Add "Launch Server" button
            var launchServerButton = new Button
            {
                Text = "Launch Server",
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = Color.FromArgb(255, 217, 119),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat
            };
            launchServerButton.Click += LaunchServer_Click;

            // Add "Add New Mod" button
            addNewModButton = new Button
            {
                Text = "Add New Mod",
                Width = modEntriesPanel.Width,
                Height = 30,
                BackColor = Color.FromArgb(217, 119, 6),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(10)
            };
            addNewModButton.Click += (s, e) => AddNewModEntry();

            // Load existing mods
            if (config.Mods.Count > 0)
            {
                foreach (var mod in config.Mods)
                {
                    AddNewModEntry(mod.ModPath, mod.ModName);
                }
            }
            else
            {
                AddNewModEntry();
            }

            mainContainer.Controls.Add(modEntriesPanel);
            mainContainer.Controls.Add(addNewModButton);
            mainContainer.Controls.Add(launchServerButton);
            
            this.Controls.Add(mainContainer);
        }

        public void LaunchServer()
        {
            LaunchServer_Click(null, EventArgs.Empty);
        }

        private void LaunchServer_Click(object? sender, EventArgs e)
        {
            string batchFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GAMEBR0GAMES", "Server.bat");
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = batchFilePath,
                    WorkingDirectory = Path.GetDirectoryName(batchFilePath),
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to launch server: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddNewModEntry(string path = "", string name = "")
        {
            var modEntry = new ModEntry();
            if (!string.IsNullOrEmpty(path)) modEntry.ModPath = path;
            if (!string.IsNullOrEmpty(name)) modEntry.ModName = name;

            modEntry.ModValidated += ModEntry_ModValidated;
            modEntries.Add(modEntry);
            modEntriesPanel.Controls.Add(modEntry);
            
            // Update the position of the Add New Mod button
            if (addNewModButton != null)
            {
                modEntriesPanel.Controls.SetChildIndex(addNewModButton, modEntriesPanel.Controls.Count - 1);
            }

            UpdateServerBatchFile();
        }

        private void ModEntry_ModValidated(object sender, ModEntryValidatedEventArgs e)
        {
            UpdateServerBatchFile();
        }

        private void UpdateServerBatchFile()
        {
            string batchFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GAMEBR0GAMES", "Server.bat");
            if (File.Exists(batchFilePath))
            {
                var validMods = modEntries.Where(m => m.IsValid).Select(m => m.ModPath).ToList();
                string modsParameter = validMods.Any() ? $"-mod=\"{string.Join(";", validMods)}\"" : "";

                // Read existing content and update mods parameter
                string[] lines = File.ReadAllLines(batchFilePath);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains("-mod="))
                    {
                        lines[i] = modsParameter;
                        break;
                    }
                }
                File.WriteAllLines(batchFilePath, lines);
            }
        }
    }
}
