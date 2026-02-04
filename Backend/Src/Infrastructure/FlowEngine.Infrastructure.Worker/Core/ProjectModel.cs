using FlowEngine.Domain.Projects.Entities;
using FlowEngine.Infrastructure.Worker.Core.Jobs;
using FlowEngine.Infrastructure.Worker.Helpers;

namespace FlowEngine.Infrastructure.Worker.Core;

public class ProjectModel(string name) : Project(name)
{
    public Dictionary<string, string>? Data { get; set; }

    public List<IJob> Jobs { get; set; }

    public void Start()
    {
        if (Started) return;

        Data?.Clear();
        Started = true;

        Type[] starterJobs = [typeof(Job_Start), typeof(Job_Timer)];


        foreach (var job in Jobs.Where(j => starterJobs.Contains(j.GetType())))
        {
            Task.Run(async () =>
             {
                 try
                 {
                     await job.Execute(this);
                 }
                 catch (Exception ex)
                 {
                     ConsoleLogger.Log(ex.ToString());
                 }
             });
        }
    }

    public void Stop()
    {
        Started = false;
    }

}


