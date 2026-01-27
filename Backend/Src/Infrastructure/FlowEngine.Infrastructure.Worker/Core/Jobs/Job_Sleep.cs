using FlowEngine.Domain.Projects.Constants;
using FlowEngine.Domain.Projects.Enums;
using FlowEngine.Infrastructure.Worker.Helpers;

namespace FlowEngine.Infrastructure.Worker.Core.Jobs;

public sealed class Job_Sleep : IJob
{
    public Job_Sleep()
    {
        ClassName = this.GetType().FullName!;
        JobParameters = new()
        {
            { FlowEngineConst.SleepTimeMs,"100"}
        };
    }


    public override async Task Execute(ProjectModel projectModel)
    {
        try
        {

            var sleepTimeMs = int.Parse(projectModel.GetValue(JobParameters, FlowEngineConst.SleepTimeMs));

            ConsoleLogger.Log($"Sleep {sleepTimeMs} ms");

            Thread.Sleep(sleepTimeMs);

            await GotoNextJob(projectModel, this.NextJob);

        }
        catch (Exception ex)
        {
            ConsoleLogger.LogError(ex.Message);
        }

    }
}
