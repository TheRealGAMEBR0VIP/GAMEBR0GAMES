@echo off
echo Launching DayZ Client
start /D "D:\OffHD\Steam\steamapps\common\DayZ" DayZDiag_x64.exe -mod=D:\OffHD\Steam\steamapps\common\DayZ\!Workshop\@CF;D:\OffHD\Steam\steamapps\common\DayZ\!Workshop\@Community-Online-Tools;P:\Bitcoin_Mining_Rig;P:\GB_Bears;P:\GB_CC;P:\TWDZ_Zombies -forcedebugger -scriptDebug=true -dologs -filePatching -scrAllowFileWrite -connect=127.0.0.1 -port=2302 -newErrorsAreWarnings=1 -mission=D:\OffHD\Steam\steamapps\common\DayZServer\mpmissions\dayzOffline.enoch
echo Client launch complete.
pause