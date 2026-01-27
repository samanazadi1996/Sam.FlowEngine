using FlowEngine.Domain.Projects.Constants;
using FlowEngine.Domain.Projects.Enums;
using FlowEngine.Infrastructure.Worker.Helpers;

namespace FlowEngine.Infrastructure.Worker.Core.Jobs;

public sealed class Job_Start : IJob
{
    public Job_Start()
    {
        ClassName = this.GetType().FullName!;
        JobParameters = new()
        {
            { FlowEngineConst.EnvironmentVariables,""},
        };
    }

    public override async Task Execute(ProjectModel projectModel)
    {
        try
        {
            ConsoleLogger.Log($"Start");

            projectModel.Data = [];

            projectModel.Data[FlowEngineConst.EnvironmentVariables] = projectModel.GetValue(JobParameters, FlowEngineConst.EnvironmentVariables);

        }
        catch (Exception ex)
        {
            ConsoleLogger.LogError(ex.Message);
        }
        finally
        {
            await GotoNextJob(projectModel, this.NextJob);
        }

    }
}

