using FlowEngine.Domain.Projects.Enums;
using System.Collections.Generic;

namespace FlowEngine.Domain.Projects.ValueObjects
{
    public class ProjectJob
    {
        public string ClassName { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string?> JobParameters { get; set; }
        public List<string> NextJob { get; set; }
    }
}
