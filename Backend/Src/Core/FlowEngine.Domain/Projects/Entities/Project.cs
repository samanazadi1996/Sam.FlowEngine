using FlowEngine.Domain.Common;
using System.Collections.Generic;

namespace FlowEngine.Domain.Projects.Entities
{
    public class Project : AuditableBaseEntity
    {
        private Project()
        {
        }
        public Project(string projectName)
        {
            ProjectName = projectName;
            //Price = price;
            //BarCode = barCode;
        }
        public string ProjectName { get; set; }
        public List<ProjectJob>? ProjectJobs { get; set; }
        public bool Started { get; set; }
    }

    public class ProjectJob: BaseEntity
    {
        public string ClassName { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string?>? JobParameters { get; set; }
        public List<string>? NextJob { get; set; }

        public long ProjectId { get; set; }
        public Project Project { get; set; }
    }

}
