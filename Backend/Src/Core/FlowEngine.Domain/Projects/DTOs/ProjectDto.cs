using FlowEngine.Domain.Projects.ValueObjects;
using System.Collections.Generic;

namespace FlowEngine.Domain.Projects.DTOs
{
    public class ProjectDto
    {
        public string ProjectName { get; set; }

        public Dictionary<string, object> Data { get; set; }

        public List<ProjectJob> Jobs { get; set; }
        public bool Started { get; set; }
    }
}
