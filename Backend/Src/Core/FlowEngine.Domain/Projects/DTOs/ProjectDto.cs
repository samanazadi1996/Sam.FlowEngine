using FlowEngine.Domain.Projects.Entities;
using System.Collections.Generic;
using System.Drawing;

namespace FlowEngine.Domain.Projects.DTOs
{
    public class ProjectDto
    {
        public long Id { get; set; }
        public string ProjectName { get; set; }

        public List<ProjectJobDto> Jobs { get; set; }
        public bool Started { get; set; }
    }
    public class ProjectJobDto
    {
        public long Id { get; set; }
        public string ClassName { get; set; }
        public string Name { get; set; }
        public Point? Position { get; set; }
        public Dictionary<string, string?> JobParameters { get; set; }
        public List<long> NextJob { get; set; }
    }
}
