////////////////////////////////////////////////////////////////////
//DeRap: config.bin
//Produced from mikero's Dos Tools Dll version 9.91
//https://mikero.bytex.digital/Downloads
//'now' is Sat Jan 04 22:10:09 2025 : 'file' last modified on Thu Dec 12 09:24:47 2024
////////////////////////////////////////////////////////////////////

#define _ARMA_

class CfgPatches
{
	class JM_COT_Scripts
	{
		units[] = {};
		weapons[] = {};
		requiredVersion = 0.1;
		requiredAddons[] = {"JM_CF_Scripts","JM_COT_GUI","DZ_Data"};
	};
};
class CfgMods
{
	class JM_CommunityOnlineTools
	{
		dir = "JM";
		picture = "";
		action = "";
		hideName = 1;
		hidePicture = 1;
		name = "Community Online Tools";
		credits = "Jacob_Mango, DannyDog, Arkensor";
		creditsJson = "JM/COT/Scripts/Data/Credits.json";
		author = "Jacob_Mango";
		authorID = "0";
		version = "0.0.0";
		versionPath = "JM/COT/Scripts/Data/Version.hpp";
		inputs = "JM/COT/Scripts/Data/Inputs.xml";
		extra = 0;
		type = "mod";
		dependencies[] = {"Game","World","Mission"};
		class defs
		{
			class engineScriptModule
			{
				value = "";
				files[] = {"JM/COT/Scripts/Common","JM/COT/Scripts/1_Core"};
			};
			class gameScriptModule
			{
				value = "";
				files[] = {"JM/COT/Scripts/Common","JM/COT/Scripts/3_Game"};
			};
			class worldScriptModule
			{
				value = "";
				files[] = {"JM/COT/Scripts/Common","JM/COT/Scripts/4_World"};
			};
			class missionScriptModule
			{
				value = "";
				files[] = {"JM/COT/Scripts/Common","VPPAdminTools/Definitions","JM/COT/Scripts/5_Mission"};
			};
		};
	};
};
