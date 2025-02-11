using GAMEBR0GAMES.Config;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GAMEBR0GAMES.UserControls
{
    public class ModEntryManager
    {
        private FlowLayoutPanel modEntriesPanel;
        private List<ModEntry> modEntries = new List<ModEntry>();

        public ModEntryManager(FlowLayoutPanel panel)
        {
            modEntriesPanel = panel;
        }

        public void AddNewModEntry(string path = "", string name = "")
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

        public void SaveMods(ServerConfig config)
        {
            config.Mods.Clear();
            foreach (var entry in modEntries)
            {
                if (entry.IsValid)
                {
                    config.AddMod(entry.ModPath, entry.ModName);
                }
            }
            MessageBox.Show("Mods saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
