@echo off
echo Launching Workbench with mod: P:\Bitcoin_Mining_Rig
start /D "D:\OffHD\Steam\steamapps\common\DayZ Tools\Bin\Workbench" workbenchApp.exe "-mod=P:\GB_Clothing"
echo Creating symbolic link for mod...
mklink /J "%cd%\SvenLoading" "P:\Bitcoin_Mining_Rig"
echo Workbench launch complete.
pause