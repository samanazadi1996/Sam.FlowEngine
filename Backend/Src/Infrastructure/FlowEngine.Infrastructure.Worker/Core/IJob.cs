using FlowEngine.Domain.Projects.Entities;
using FlowEngine.Infrastructure.Worker.Helpers;

namespace FlowEngine.Infrastructure.Worker.Core;

public abstract class IJob : ProjectJob
{
    public abstract Task Execute(ProjectModel projectModel);
    public IJob UpdateJobParameter(string key, string? value)
    {
        if (JobParameters != null)
            if (JobParameters.TryGetValue(key, out var _))
            {
                JobParameters[key] = value;
            }
        return this;
    }
    public async Task GotoNextJob(ProjectModel projectModel, List<long>? nextJobs)
    {
        if (!projectModel.Started) return;

        if (nextJobs is null) return;

        foreach (var item in nextJobs)
        {
            var job = projectModel.Jobs.FirstOrDefault(p => p.Id == item);

            if (job != null)
            {
                ConsoleLogger.Log($"Run Job {job.Name}");
                Task.Run(async () => await job.Execute(projectModel));
            }

        }
    }
}


