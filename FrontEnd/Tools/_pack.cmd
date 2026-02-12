cd ..\FlowEngine\

ng build --configuration production

mkdir "..\..\Backend\Src\Presentation\FlowEngine.WebApi\wwwroot\browser"

move ".\dist\FlowEngine\browser" "..\..\Backend\Src\Presentation\FlowEngine.WebApi\browser"