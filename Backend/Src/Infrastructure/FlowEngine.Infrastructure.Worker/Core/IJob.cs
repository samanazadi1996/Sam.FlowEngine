using FlowEngine.Domain.Projects.ValueObjects;
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
                JobParameters[key].Value = value;
            }
        return this;
    }
    public async Task GotoNextJob(ProjectModel projectModel, List<string> nextJobs)
    {
        if (!projectModel.Started) return;

        if (nextJobs is null) return;

        foreach (var item in nextJobs)
        {
            var jobs = projectModel.Jobs.FirstOrDefault(p => p.Name == item);

            if (jobs != null)
            {
                ConsoleLogger.Log($"Run Job {item}");
                await jobs.Execute(projectModel);
            }

        }
    }
}


