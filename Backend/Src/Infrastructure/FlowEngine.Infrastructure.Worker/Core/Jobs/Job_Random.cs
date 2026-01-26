using FlowEngine.Domain.Projects.Constants;
using FlowEngine.Domain.Projects.Enums;
using FlowEngine.Infrastructure.Worker.Helpers;

namespace FlowEngine.Infrastructure.Worker.Core.Jobs;

public sealed class Job_Random : IJob
{
    public Job_Random()
    {
        ClassName = this.GetType().FullName!;
        JobParameters = new()
        {
            { FlowEngineConst.From,new(JobParameterType.Int,"0")},
            { FlowEngineConst.To,new(JobParameterType.Int,"1")},
        };
    }


    public override async Task Execute(ProjectModel projectModel)
    {
        try
        {
            var from = int.Parse(projectModel.GetValue(JobParameters, FlowEngineConst.From));

            var to = int.Parse(projectModel.GetValue(JobParameters, FlowEngineConst.To));

            var rnd = new Random();

            var value = rnd.Next(from, to);
            projectModel.Data ??= [];
            projectModel.Data[this.Name] = value.ToString();

            ConsoleLogger.Log($"Generate RandoNumber {value}");

            await GotoNextJob(projectModel, this.NextJob);
        }
        catch (Exception ex)
        {
            ConsoleLogger.LogError(ex.Message);
        }

    }
}
