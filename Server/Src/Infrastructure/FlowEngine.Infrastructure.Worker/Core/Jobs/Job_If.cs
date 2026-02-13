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
            { FlowEngineConst.Expression,null},
            { FlowEngineConst.IfTrue,null},
            { FlowEngineConst.IfFalse,null},
        };
    }
    public override async Task Execute(ProjectModel projectModel)
    {
        try
        {

            long? nextJob = null;

            var expression = projectModel.GetValue(JobParameters, FlowEngineConst.Expression);

            if (EvaluateBoolExpression(expression))
            {
                var t = projectModel.GetValue(JobParameters, FlowEngineConst.IfTrue);
                if (!string.IsNullOrEmpty(t))
                    nextJob = Convert.ToInt64(t);

            }
            else
            {
                var f = projectModel.GetValue(JobParameters, FlowEngineConst.IfFalse);
                if (!string.IsNullOrEmpty(f))
                    nextJob = Convert.ToInt64(f);
            }

            await GotoNextJob(projectModel, [.. (this.NextJob ?? []).Union([nextJob!.Value])]);
        }
        catch (Exception ex)
        {
            ConsoleLogger.LogError(ex.Message);
        }
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
