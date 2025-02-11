using System;
using System.Windows.Forms;

namespace GAMEBR0GAMES.UserControls
{
    public class ModEntry : UserControl
    {
        public string ModPath { get; set; }
        public string ModName { get; set; }
        public bool IsValid { get; private set; }

        public event EventHandler AddNewRequested;
        public event EventHandler<ModEntryValidatedEventArgs> ModValidated;

        public ModEntry()
        {
            // Initialize components and layout for ModEntry
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Setup UI components for ModEntry
            // This can include TextBoxes, Labels, and Buttons for mod entry
        }

        private void ValidateMod()
        {
            // Logic to validate the mod entry
            IsValid = true; // Set based on validation logic
            ModValidated?.Invoke(this, new ModEntryValidatedEventArgs(ModPath, ModName));
        }

        private void RequestAddNew()
        {
            AddNewRequested?.Invoke(this, EventArgs.Empty);
        }

        public void ShowAddButton(bool show)
        {
            // Logic to show or hide the add button
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
