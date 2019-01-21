@ECHO OFF
"C:\Program Files\Unity\Editor\Unity.exe" -batchmode -projectPath "%~dp0" -executeMethod Assets.Script.Editor.UnitTest.EnterPoint.Main 
EXIT