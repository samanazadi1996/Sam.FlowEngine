using Cronos;
using FlowEngine.Domain.Projects.Constants;
using FlowEngine.Infrastructure.Worker.Helpers;

namespace FlowEngine.Infrastructure.Worker.Core.Jobs;

public sealed class Job_Schedule : IJob
{
    public Job_Schedule()
    {
        ClassName = this.GetType().FullName!;
        JobParameters = new()
        {
            { FlowEngineConst.EnvironmentVariables,""},

            { FlowEngineConst.CronExpression,CronExpression.EveryMinute.ToString()},
        };
    }

    public override async Task Execute(ProjectModel projectModel)
    {
        try
        {
            var cronExpression = projectModel.GetValue(JobParameters, FlowEngineConst.CronExpression);

            var cron = CronExpression.Parse(cronExpression);

            var nextExecutionTime = cron.GetNextOccurrence(DateTime.UtcNow, TimeZoneInfo.Local);

            projectModel.Data = [];
            projectModel.Data[FlowEngineConst.EnvironmentVariables] = projectModel.GetValue(JobParameters, FlowEngineConst.EnvironmentVariables);

            while (projectModel.Started)
            {
                var now = DateTime.Now;

                if (nextExecutionTime.HasValue && now >= nextExecutionTime.Value)
                {
                    await GotoNextJob(projectModel, this.NextJob);
                }

                await Task.Delay(1000 * 60);
            }
        }
        catch (Exception ex)
        {
            ConsoleLogger.LogError(ex.Message);
        }
    }

}
