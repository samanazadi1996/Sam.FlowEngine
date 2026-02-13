@echo off

REM حذف تمام پوشه‌های bin و obj
echo Removing all bin and obj folders...
powershell -command "Get-ChildItem -Path .\Backend\Src\ -Include bin,obj -Recurse -Directory | Remove-Item -Recurse -Force"

cd .\FrontEnd\FlowEngine\

echo Starting Angular build...
call ng build --configuration production

echo Build command finished, continuing...

cd ..\..\

mkdir ".\Backend\Src\Presentation\FlowEngine.WebApi\wwwroot" 2>nul

xcopy ".\FrontEnd\FlowEngine\dist\FlowEngine\browser\*" ".\Backend\Src\Presentation\FlowEngine.WebApi\wwwroot" /E /I /Y

echo Creating zip file...
powershell -command "Compress-Archive -Path .\Backend\Src\* -DestinationPath output.zip -Force"

echo Done!
pause