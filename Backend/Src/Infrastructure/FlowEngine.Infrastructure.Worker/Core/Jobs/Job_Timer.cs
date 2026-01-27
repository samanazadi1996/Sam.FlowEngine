using FlowEngine.Domain.Projects.Constants;
using FlowEngine.Domain.Projects.Enums;
using FlowEngine.Infrastructure.Worker.Helpers;

namespace FlowEngine.Infrastructure.Worker.Core.Jobs;

public sealed class Job_Timer : IJob
{
    public Job_Timer()
    {
        ClassName = this.GetType().FullName!;
        JobParameters = new()
        {
            { FlowEngineConst.IntervalMs,"3000"},

            { FlowEngineConst.EnvironmentVariables,""},
        };
    }

    public override async Task Execute(ProjectModel projectModel)
    {
        try
        {
            var interval = int.Parse(projectModel.GetValue(JobParameters, FlowEngineConst.IntervalMs));

            ConsoleLogger.Log($"Run Timer with {interval}");

            projectModel.Data = [];
            projectModel.Data[FlowEngineConst.EnvironmentVariables] = projectModel.GetValue(JobParameters, FlowEngineConst.EnvironmentVariables);

            while (projectModel.Started)
            {
                ConsoleLogger.Log($"Timer with {interval} Executed");

                await GotoNextJob(projectModel, this.NextJob);

                Thread.Sleep(interval);
            }
        }
        catch (Exception ex)
        {
            ConsoleLogger.LogError(ex.Message);
        }
    }
}
