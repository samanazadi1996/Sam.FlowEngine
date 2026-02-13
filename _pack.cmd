@echo off

REM حذف تمام پوشه‌های bin و obj
echo Removing all bin and obj folders...
powershell -command "Get-ChildItem -Path .\Server\Src\ -Include bin,obj -Recurse -Directory | Remove-Item -Recurse -Force"

cd .\Client\FlowEngine\

echo Starting Angular build...
call ng build --configuration production

echo Build command finished, continuing...

cd ..\..\

mkdir ".\Server\Src\Presentation\FlowEngine.WebApi\wwwroot" 2>nul

xcopy ".\Client\FlowEngine\dist\FlowEngine\browser\*" ".\Server\Src\Presentation\FlowEngine.WebApi\wwwroot" /E /I /Y

echo Creating zip file...
powershell -command "Compress-Archive -Path .\Server\Src\* -DestinationPath output.zip -Force"

echo Done!
pause