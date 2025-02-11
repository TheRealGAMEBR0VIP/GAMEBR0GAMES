using System;
using System.IO;
using System.Windows.Forms;

namespace GAMEBR0GAMES.UserControls
{
    public static class BatchFileGenerator
    {
        public static void GenerateBatchFile(string batchFileName, string diagExePath, string serverExePath)
        {
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GAMEBR0GAMES");
            string batchFilePath = Path.Combine(folderPath, batchFileName);
            
            // Get the path to the DayZ folder from the selected DayZDiag_x64.exe path
            string dayZFolderPath = Path.GetDirectoryName(diagExePath);
            string modPaths = "";

            // Check for the !Workshop folder and the required mods
            string workshopPath = Path.Combine(dayZFolderPath, "!Workshop");
            if (Directory.Exists(workshopPath))
            {
                if (Directory.Exists(Path.Combine(workshopPath, "@Community-Online-Tools")))
                {
                    modPaths += $"{Path.Combine(dayZFolderPath, "!Workshop", "@Community-Online-Tools")};";
                }
                if (Directory.Exists(Path.Combine(workshopPath, "@CF")))
                {
                    modPaths += $"{Path.Combine(dayZFolderPath, "!Workshop", "@CF")};";
                }
            }

            string batchContent = $"@echo off\n" +
                                  $"echo Launching DayZ Server (Diagnostic)\n" +
                                  $"start /D \"{Path.GetDirectoryName(serverExePath)}\" DayZServer_x64.exe -port=2302 -noPause -forcedebugger -scriptDebug=true -dologs -adminlogs -server \"-mod={modPaths}\" \"-config=%CD%\\Debugprofile.cfg\" \"-profiles=%CD%\\Debugprofile\" -scrAllowFileWrite -filePatching \"-mission={Path.Combine(Path.GetDirectoryName(serverExePath), "mpmissions", "dayzOffline.enoch")}\" -newErrorsAreWarnings=1\n" +
                                  $"echo Server launch complete.\n" +
                                  $"pause";

            File.WriteAllText(batchFilePath, batchContent);
            MessageBox.Show($"Batch file created at: {batchFilePath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
