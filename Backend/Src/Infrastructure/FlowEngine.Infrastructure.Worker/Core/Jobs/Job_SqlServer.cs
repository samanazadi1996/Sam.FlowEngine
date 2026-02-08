using FlowEngine.Domain.Projects.Constants;
using FlowEngine.Infrastructure.Worker.Helpers;
using System.Data.SqlClient;
using System.Text.Json;

namespace FlowEngine.Infrastructure.Worker.Core.Jobs;

public sealed class Job_SqlServer : IJob
{
    public Job_SqlServer()
    {
        ClassName = this.GetType().FullName!;
        JobParameters = new()
        {
            { FlowEngineConst.Query,@"SELECT TOP (1000) [Id]
      ,[ProjectName]
      ,[Started]
      ,[Created]
  FROM [FlowEngine].[dbo].[Projects]"},

            { FlowEngineConst.ConnectionString,"Data Source=.;Initial Catalog=FlowEngine;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True"},
        };
    }

    public override async Task Execute(ProjectModel projectModel)
    {
        try
        {
            var query = projectModel.GetValue(JobParameters, FlowEngineConst.Query);
            var connectionString = projectModel.GetValue(JobParameters, FlowEngineConst.ConnectionString);

            var data = await ExecuteQueryToJsonAsync(connectionString, query);

            projectModel.Data ??= [];
            projectModel.Data[this.Name] = data;

            ConsoleLogger.Log($"Run SqlServer Query '{query}'");

            await GotoNextJob(projectModel, this.NextJob);
        }
        catch (Exception ex)
        {
            ConsoleLogger.LogError(ex.Message);
        }
    }

    public async Task<string> ExecuteQueryToJsonAsync(string connectionString, string query)
    {
        var results = new List<Dictionary<string, object>>();

        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            using var command = new SqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var row = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                }

                results.Add(row);
            }
        }


        return JsonSerializer.Serialize(new { Data = results });
    }

}
