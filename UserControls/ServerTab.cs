using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using GAMEBR0GAMES.Config;

using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using GAMEBR0GAMES.Config;

using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using GAMEBR0GAMES.Config;

namespace GAMEBR0GAMES.UserControls
{
    public partial class DayZAddonBuilder : UserControl
    {
        private List<ModEntry> modEntries = new List<ModEntry>(); // Added to hold mod entries

        private void CreateServerTab()
        {
            if (serverTab != null) return; // Prevent creating multiple tabs

            serverTab = new TabPage("Server");
            serverTab.BackColor = Color.FromArgb(255, 0, 0, 0);
            
            // Create scrollable panel for mod entries
            var scrollPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            modEntriesPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(10)
            };

            // Add initial mod entry or load existing mods
            if (config.Mods.Count == 0)
            {
                AddNewModEntry();
            }
            else
            {
                foreach (var mod in config.Mods)
                {
                    AddNewModEntry(mod.ModPath, mod.ModName);
                }
            }

            // Add a button to add new mod entries
            var addModButton = new Button
            {
                Text = "Add Mod",
                Dock = DockStyle.Top, // Changed to DockStyle.Top for better positioning
                BackColor = Color.FromArgb(255, 217, 119),
                ForeColor = Color.Black,
                Height = 40
            };
            addModButton.Click += (s, e) => AddNewModEntry();

            // Add a button to remove the last mod entry
            var removeModButton = new Button
            {
                Text = "Remove Last Mod",
                Dock = DockStyle.Top, // Changed to DockStyle.Top for better positioning
                BackColor = Color.FromArgb(255, 217, 119),
                ForeColor = Color.Black,
                Height = 40
            };
            removeModButton.Click += (s, e) =>
            {
                if (modEntries.Count > 0)
                {
                    var lastEntry = modEntries[modEntries.Count - 1];
                    modEntries.Remove(lastEntry);
                    modEntriesPanel.Controls.Remove(lastEntry);
                    UpdateAddButtons();
                }
            };

            // Add a button to browse for mod folder
            var browseModButton = new Button
            {
                Text = "Browse Mod Folder",
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(255, 217, 119),
                ForeColor = Color.Black,
                Height = 40
            };
            browseModButton.Click += BrowseModButton_Click;

            scrollPanel.Controls.Add(modEntriesPanel);
            scrollPanel.Controls.Add(addModButton);
            scrollPanel.Controls.Add(removeModButton);
            scrollPanel.Controls.Add(browseModButton);
            serverTab.Controls.Add(scrollPanel);

            // Only add the launch button to the Server tab
            tabControl.Controls.Add(serverTab);
            tabControl.SelectedTab = serverTab;
        }

        private void BrowseModButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
            {
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = folderBrowser.SelectedPath;
                    string modName = Path.GetFileName(selectedPath);
                    AddNewModEntry(selectedPath, modName);
                    UpdateServerBatchFile(selectedPath);
                }
            }
        }

        private void UpdateServerBatchFile(string modPath)
        {
            string batchFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GAMEBR0GAMES", "Server.bat");
            if (File.Exists(batchFilePath))
            {
                string existingContent = File.ReadAllText(batchFilePath);
                string newModEntry = $"-mod={modPath};";
                if (!existingContent.Contains(newModEntry))
                {
                    existingContent += newModEntry;
                    File.WriteAllText(batchFilePath, existingContent);
                }
            }
        }

        private void AddNewModEntry(string path = "", string name = "")
        {
            var modEntry = new ModEntry();
            if (!string.IsNullOrEmpty(path)) modEntry.ModPath = path;
            if (!string.IsNullOrEmpty(name)) modEntry.ModName = name;

            modEntry.AddNewRequested += ModEntry_AddNewRequested;
            modEntry.ModValidated += ModEntry_ModValidated;
            modEntries.Add(modEntry);
            modEntriesPanel.Controls.Add(modEntry);

            UpdateAddButtons();
        }

        private void ModEntry_AddNewRequested(object sender, EventArgs e)
        {
            AddNewModEntry();
        }

        private void ModEntry_ModValidated(object sender, ModEntryValidatedEventArgs e)
        {
            UpdateAddButtons();
        }

        private void UpdateAddButtons()
        {
            // Hide all add buttons
            foreach (var entry in modEntries)
            {
                entry.ShowAddButton(false);
            }

            // Show add button on the last valid entry
            for (int i = modEntries.Count - 1; i >= 0; i--)
            {
                if (modEntries[i].IsValid)
                {
                    modEntries[i].ShowAddButton(true);
                    break;
                }
            }
        }
    }
}
{
    public partial class DayZAddonBuilder : UserControl
    {
        private List<ModEntry> modEntries = new List<ModEntry>(); // Added to hold mod entries

        private void CreateServerTab()
        {
            if (serverTab != null) return; // Prevent creating multiple tabs

            serverTab = new TabPage("Server");
            serverTab.BackColor = Color.FromArgb(255, 0, 0, 0);
            
            // Create scrollable panel for mod entries
            var scrollPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            modEntriesPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(10)
            };

            // Add initial mod entry or load existing mods
            if (config.Mods.Count == 0)
            {
                AddNewModEntry();
            }
            else
            {
                foreach (var mod in config.Mods)
                {
                    AddNewModEntry(mod.ModPath, mod.ModName);
                }
            }

            // Add a button to add new mod entries
            var addModButton = new Button
            {
                Text = "Add Mod",
                Dock = DockStyle.Top, // Changed to DockStyle.Top for better positioning
                BackColor = Color.FromArgb(255, 217, 119),
                ForeColor = Color.Black,
                Height = 40
            };
            addModButton.Click += (s, e) => AddNewModEntry();

            // Add a button to remove the last mod entry
            var removeModButton = new Button
            {
                Text = "Remove Last Mod",
                Dock = DockStyle.Top, // Changed to DockStyle.Top for better positioning
                BackColor = Color.FromArgb(255, 217, 119),
                ForeColor = Color.Black,
                Height = 40
            };
            removeModButton.Click += (s, e) =>
            {
                if (modEntries.Count > 0)
                {
                    var lastEntry = modEntries[modEntries.Count - 1];
                    modEntries.Remove(lastEntry);
                    modEntriesPanel.Controls.Remove(lastEntry);
                    UpdateAddButtons();
                }
            };

            // Add a button to browse for mod folder
            var browseModButton = new Button
            {
                Text = "Browse Mod Folder",
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(255, 217, 119),
                ForeColor = Color.Black,
                Height = 40
            };
            browseModButton.Click += BrowseModButton_Click;

            scrollPanel.Controls.Add(modEntriesPanel);
            scrollPanel.Controls.Add(addModButton);
            scrollPanel.Controls.Add(removeModButton);
            scrollPanel.Controls.Add(browseModButton);
            serverTab.Controls.Add(scrollPanel);

            // Only add the launch button to the Server tab
            tabControl.Controls.Add(serverTab);
            tabControl.SelectedTab = serverTab;
        }
=======
    }
}
{
    public partial class DayZAddonBuilder : UserControl
    {
        private List<ModEntry> modEntries = new List<ModEntry>(); // Added to hold mod entries
{
    public partial class DayZAddonBuilder : UserControl
    {
        private void CreateServerTab()
        {
            if (serverTab != null) return; // Prevent creating multiple tabs

            serverTab = new TabPage("Server");
            serverTab.BackColor = Color.FromArgb(255, 0, 0, 0);

            // Create scrollable panel for mod entries
            var scrollPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            modEntriesPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(10)
            };

            // Add initial mod entry or load existing mods
            if (config.Mods.Count == 0)
            {
                AddNewModEntry();
            }
            else
            {
                foreach (var mod in config.Mods)
                {
                    AddNewModEntry(mod.ModPath, mod.ModName);
                }
            }

            // Add a button to add new mod entries
            var addModButton = new Button
            {
                Text = "Add Mod",
                Dock = DockStyle.Top, // Changed to DockStyle.Top for better positioning
                BackColor = Color.FromArgb(255, 217, 119),
                ForeColor = Color.Black,
                Height = 40
            };
            addModButton.Click += (s, e) => AddNewModEntry();

            // Add a button to remove the last mod entry
            var removeModButton = new Button
            {
                Text = "Remove Last Mod",
                Dock = DockStyle.Top, // Changed to DockStyle.Top for better positioning
                BackColor = Color.FromArgb(255, 217, 119),
                ForeColor = Color.Black,
                Height = 40
            };
            removeModButton.Click += (s, e) =>
            {
                if (modEntries.Count > 0)
                {
                    var lastEntry = modEntries[modEntries.Count - 1];
                    modEntries.Remove(lastEntry);
                    modEntriesPanel.Controls.Remove(lastEntry);
                    UpdateAddButtons();
                }
            };

            // Add a button to browse for mod folder
            var browseModButton = new Button
            {
                Text = "Browse Mod Folder",
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(255, 217, 119),
                ForeColor = Color.Black,
                Height = 40
            };
            browseModButton.Click += BrowseModButton_Click;

            scrollPanel.Controls.Add(modEntriesPanel);
            scrollPanel.Controls.Add(addModButton);
            scrollPanel.Controls.Add(removeModButton);
            scrollPanel.Controls.Add(browseModButton);
            serverTab.Controls.Add(scrollPanel);

            // Only add the launch button to the Server tab
            tabControl.Controls.Add(serverTab);
            tabControl.SelectedTab = serverTab;
        }

        private void BrowseModButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
            {
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = folderBrowser.SelectedPath;
                    string modName = Path.GetFileName(selectedPath);
                    AddNewModEntry(selectedPath, modName);
                    UpdateServerBatchFile(selectedPath);
                }
            }
        }

        private void UpdateServerBatchFile(string modPath)
        {
            string batchFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GAMEBR0GAMES", "Server.bat");
            if (File.Exists(batchFilePath))
            {
                string existingContent = File.ReadAllText(batchFilePath);
                string newModEntry = $"-mod={modPath};";
                if (!existingContent.Contains(newModEntry))
                {
                    existingContent += newModEntry;
                    File.WriteAllText(batchFilePath, existingContent);
                }
            }
        }

        private void AddNewModEntry(string path = "", string name = "")
        {
            var modEntry = new ModEntry();
            if (!string.IsNullOrEmpty(path)) modEntry.ModPath = path;
            if (!string.IsNullOrEmpty(name)) modEntry.ModName = name;

            modEntry.AddNewRequested += ModEntry_AddNewRequested;
            modEntry.ModValidated += ModEntry_ModValidated;
            modEntries.Add(modEntry);
            modEntriesPanel.Controls.Add(modEntry);

            UpdateAddButtons();
        }

        private void ModEntry_AddNewRequested(object sender, EventArgs e)
        {
            AddNewModEntry();
        }

        private void ModEntry_ModValidated(object sender, ModEntryValidatedEventArgs e)
        {
            UpdateAddButtons();
        }

        private void UpdateAddButtons()
        {
            // Hide all add buttons
            foreach (var entry in modEntries)
            {
                entry.ShowAddButton(false);
            }

            // Show add button on the last valid entry
            for (int i = modEntries.Count - 1; i >= 0; i--)
            {
                if (modEntries[i].IsValid)
                {
                    modEntries[i].ShowAddButton(true);
                    break;
                }
            }
        }
    }
}
