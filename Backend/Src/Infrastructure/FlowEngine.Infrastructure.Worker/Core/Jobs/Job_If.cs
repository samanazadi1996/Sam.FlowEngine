using FlowEngine.Domain.Projects.Constants;
using FlowEngine.Domain.Projects.Enums;
using FlowEngine.Infrastructure.Worker.Helpers;
using System.Data;

namespace FlowEngine.Infrastructure.Worker.Core.Jobs;

public sealed class Job_If : IJob
{
    public Job_If()
    {
        ClassName = this.GetType().FullName!;
        JobParameters = new()
        {
            { FlowEngineConst.Expression,new(JobParameterType.String)},
            { FlowEngineConst.True,new(JobParameterType.JobParameter_Execute)},
            { FlowEngineConst.False,new(JobParameterType.JobParameter_Execute)},
        };
    }
    public override async Task Execute(ProjectModel projectModel)
    {
        string nextJob = "";

        var expression = projectModel.GetValue(JobParameters, FlowEngineConst.Expression);

        if (EvaluateBoolExpression(expression))
        {
            var t = projectModel.GetValue(JobParameters, FlowEngineConst.True);
            if (!string.IsNullOrEmpty(t))
                nextJob = t;

        }
        else
        {
            var f = projectModel.GetValue(JobParameters, FlowEngineConst.False);
            if (!string.IsNullOrEmpty(f))
                nextJob = f;
        }

        await GotoNextJob(projectModel, [.. (this.NextJob ?? []).Union([nextJob])]);
    }

    public static bool EvaluateBoolExpression(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression))
            return false;

        expression = expression
            .Replace("&&", " AND ")
            .Replace("||", " OR ")
            .Replace("==", "=");

        var table = new DataTable();
        var result = table.Compute(expression, null);

        return Convert.ToBoolean(result);
    }

}
