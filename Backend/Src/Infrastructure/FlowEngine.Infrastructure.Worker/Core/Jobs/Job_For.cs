using FlowEngine.Domain.Projects.Constants;
using FlowEngine.Infrastructure.Worker.Helpers;
using System.Text.Json;

namespace FlowEngine.Infrastructure.Worker.Core.Jobs;

public sealed class Job_For : IJob
{
    public Job_For()
    {
        ClassName = this.GetType().FullName!;
        JobParameters = new()
        {
            { FlowEngineConst.From,"1"},
            { FlowEngineConst.To,"10" },
        };
    }


    public override async Task Execute(ProjectModel projectModel)
    {
        try
        {
            var from = int.Parse(projectModel.GetValue(JobParameters, FlowEngineConst.From));

            var to = int.Parse(projectModel.GetValue(JobParameters, FlowEngineConst.To));

            ConsoleLogger.Log($"Run Loop From: {from} To: {to}");

            for (global::System.Int32 i = from; i < to; i++)
            {
                await GotoNextJob(projectModel, this.NextJob);

                projectModel.Data ??= [];
                projectModel.Data[this.Name] = JsonSerializer.Serialize(new { Index = i });
            }

        }
        catch (Exception ex)
        {
            ConsoleLogger.LogError(ex.Message);
        }

    }
}
