@echo off
echo Launching DayZ Server (Diagnostic)
start /D "D:\OffHD\Steam\steamapps\common\DayZServer" DayZServer_x64.exe -port=2302 -noPause -forcedebugger -scriptDebug=true -dologs -adminlogs -server "-mod=D:\OffHD\Steam\steamapps\common\DayZ\!Workshop\@Community-Online-Tools;D:\OffHD\Steam\steamapps\common\DayZ\!Workshop\@CF;" "-config=%CD%\Debugprofile.cfg" "-profiles=%CD%\Debugprofile" -scrAllowFileWrite -filePatching "-mission=D:\OffHD\Steam\steamapps\common\DayZServer\mpmissions\dayzOffline.enoch" -newErrorsAreWarnings=1
echo Server launch complete.
pause