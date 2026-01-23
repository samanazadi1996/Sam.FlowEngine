using FlowEngine.Infrastructure.Worker.Helpers;

namespace FlowEngine.Infrastructure.Worker.Core.Jobs;

public sealed class Job_Start : IJob
{
    public Job_Start()
    {
        ClassName = this.GetType().FullName!;
    }

    public override async Task Execute(ProjectModel projectModel)
    {
        ConsoleLogger.Log($"Start");

        await GotoNextJob(projectModel, this.NextJob);
    }
}

