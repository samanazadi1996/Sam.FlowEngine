using FlowEngine.Infrastructure.Worker.Core;

namespace FlowEngine.Infrastructure.Worker.Services;

public class FlowEngineContext
{
    public Dictionary<string, List<ProjectModel>> Projects { get; set; } = [];
}
