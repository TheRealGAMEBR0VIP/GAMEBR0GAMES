using System;
using System.IO;
using System.Windows.Forms;

namespace GAMEBR0GAMES.UserControls
{
    public static class DebugProfileGenerator
    {
        public static void GenerateDebugProfile(string profileName)
        {
            string profilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GAMEBR0GAMES", profileName);
            string profileContent = 
@"hostname = ""Test""; // Server name
password = """";                  // Password to connect to the server
passwordAdmin = """";             // Password to become a server admin

enableWhitelist = 0;            // Enable/disable whitelist (value 0-1)
logAverageFps = 60;			    // Logs the average server FPS (value in seconds), needs to have ''-doLogs'' launch parameter active
logMemory = 60;				    // Logs the server memory usage (value in seconds), needs to have the ''-doLogs'' launch parameter active
maxPlayers = 5;                 // Maximum amount of players
BattlEye = 0;                   // turn off BE since diag exe does not run with it
verifySignatures = 0;           // if testing mods which aren't properly signed 0
maxPing = 200;					// Max ping value until server kick the user (value in milliseconds)
forceSameBuild = 0;             // When enabled, the server will allow the connection only to clients with same the .exe revision as the server (value 0-1)
disableVoN = 0;                 // Enable/disable voice over network (value 0-1)
vonCodecQuality = 30;           // Voice over network codec quality, the higher the better (values 0-30)
disable3rdPerson = 0;             // Toggles the 3rd person view for players (value 0-1)
disableCrosshair = 1;             // Toggles the cross-hair (value 0-1)
disablePersonalLight = 1;       // Disables personal light for all clients connected to server
lightingConfig = 1;             // 0 for brighter night setup, 1 for darker night setup
serverTime = ""2021/7/1/12/00"";     // Initial in-game time of the server. ""SystemTime"" means the local time of the machine. Another possibility is to set the time to some value in ""YYYY/MM/DD/HH/MM"" format, f.e. ""2015/4/8/17/23"" .
serverTimeAcceleration = 12;      // Accelerated Time (value 0-24)// This is a time multiplier for in-game time. In this case, the time would move 24 times faster than normal, so an entire day would pass in one hour.
serverNightTimeAcceleration = 10; // Accelerated Nigh Time - The numerical value being a multiplier (0.1-64) and also multiplied by serverTimeAcceleration value. Thus, in case it is set to 4 and serverTimeAcceleration is set to 2, night time would move 8 times faster than normal. An entire night would pass in 3 hours.
serverTimePersistent = 0;         // Persistent Time (value 0-1)// The actual server time is saved to storage, so when active, the nextserver start will use the saved time value.
guaranteedUpdates = 1;            // Communication protocol used with game server (use only number 1)
loginQueueConcurrentPlayers = 5;  // The number of players concurrently processed during the login process. Should prevent massive performance drop during connection when a lot of people are connecting at the same time.
loginQueueMaxPlayers = 5;         // The maximum number of players that can wait in login queue
instanceId = 3;                 // DayZ server instance id, to identify the number of instances per box and their storage folders with persistence files
storeHouseStateDisabled = true; // Disable houses/doors persistence (value true/false), usable in case of problems with persistence
storageAutoFix = 1;             // Checks if the persistence files are corrupted and replaces corrupted ones with empty ones (value 0-1)
allowFilePatching = 1;			// if set to 1 it will enable connection of clients with ""-filePatching"" launch parameter enabled
respawnTime = 1;
disableBaseDamage = 1;			// set to 1 to disable damage/destruction of fence and watchtower
disableContainerDamage = 1;		// set to 1 to disable damage/destruction of tents, barrels, wooden crate and seachest
disableRespawnDialog = 1;       // set to 1 to disable the respawn dialog (new characters will be spawning as random)
enableCfgGameplayFile = 1;		//To enable the usage of the cfggameplay.json will be loaded and used by the game. 
defaultVisibility = 3000;			// highest terrain render distance on server (if higher than ""viewDistance="" in DayZ client profile, clientside parameter applies)
defaultObjectViewDistance = 3000;	// highest object render distance on server (if higher than ""preferredObjectViewDistance="" in DayZ client profile, clientside parameter applies)

class Missions
{
	class DayZ
	{
		template = ""dayzOffline.enoch""; // Mission to load on server startup. <MissionName>.<TerrainName>
	};
};";

            File.WriteAllText(profilePath, profileContent);
            MessageBox.Show($"Debug profile created at: {profilePath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
