@ECHO off
DEL "J:\Documents\ME578\Lab_7\Provided\Provided\startup\Lab_7_Visual_Studios.dll"
DEL "J:\Documents\ME578\Lab_7\Provided\Provided\startup\Lab_7_Visual_Studios.pdb"
XCOPY "J:\Documents\ME578\Lab_7\Lab_7_Visual_Studios\Lab_7_Visual_Studios\bin\Debug\Lab_7_Visual_Studios.dll" "J:\Documents\ME578\Lab_7\Provided\Provided\startup"
XCOPY "J:\Documents\ME578\Lab_7\Lab_7_Visual_Studios\Lab_7_Visual_Studios\bin\Debug\Lab_7_Visual_Studios.pdb" "J:\Documents\ME578\Lab_7\Provided\Provided\startup"

SET UGII_USER_DIR=%~dp0
START "ME 578" "%SystemDrive%\Program Files\Siemens\NX 10.0\UGII\ugraf.exe"
EXIT 0