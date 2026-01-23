using FlowEngine.Domain.Projects.Constants;
using FlowEngine.Domain.Projects.Enums;
using FlowEngine.Infrastructure.Worker.Helpers;

namespace FlowEngine.Infrastructure.Worker.Core.Jobs;

public sealed class Job_If : IJob
{
    public Job_If()
    {
        ClassName = this.GetType().FullName!;
        JobParameters = new()
        {
            { FlowEngineConst.True,new(JobParameterType.JobParameter_Execute)},
            { FlowEngineConst.False,new(JobParameterType.JobParameter_Execute)},
        };
    }
    public override async Task Execute(ProjectModel projectModel)
    {
        this.NextJob ??= [];
        if (true)
        {
            var t = projectModel.GetValue(JobParameters, FlowEngineConst.True);
            if (!string.IsNullOrEmpty(t))
                this.NextJob.Add(t);

        }
        else
        {
            var f = projectModel.GetValue(JobParameters, FlowEngineConst.False);
            if (!string.IsNullOrEmpty(f))
                this.NextJob.Add(f);
        }

        await GotoNextJob(projectModel, this.NextJob);
    }
}
