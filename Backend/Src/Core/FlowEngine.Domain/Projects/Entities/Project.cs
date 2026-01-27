using FlowEngine.Domain.Common;
using FlowEngine.Domain.Projects.ValueObjects;
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
        public List<ProjectJob>? Jobs { get; set; }
        public bool Started { get; set; }
    }
}
