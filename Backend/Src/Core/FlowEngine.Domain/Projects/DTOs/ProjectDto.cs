using FlowEngine.Domain.Projects.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FlowEngine.Domain.Projects.DTOs
{
    public class ProjectDto
    {
        public static List<ProjectDto> Parse(List<Project> proj)
        {
            return proj.Select(p => new ProjectDto()
            {
                Id = p.Id,
                ProjectName = p.ProjectName,
                Jobs = p.ProjectJobs.Select(j => new ProjectJobDto()
                {
                    Id = j.Id,
                    Name = j.Name,
                    ClassName = j.ClassName,
                    JobParameters = j.JobParameters,
                    NextJob = j.NextJob,
                    Position = j.Position,
                    ProjectId = j.ProjectId,
                }).ToList(),
                Started = p.Started
            }).ToList();
        }
        public static ProjectDto Parse(Project p)
        {
            return new ProjectDto()
            {
                Id = p.Id,
                ProjectName = p.ProjectName,
                Jobs = p.ProjectJobs.Select(j => new ProjectJobDto()
                {
                    Id = j.Id,
                    Name = j.Name,
                    ClassName = j.ClassName,
                    JobParameters = j.JobParameters,
                    NextJob = j.NextJob,
                    Position = j.Position,
                    ProjectId = j.ProjectId,
                }).ToList(),
                Started = p.Started
            };
        }
        public long Id { get; set; }
        public string ProjectName { get; set; }

        public List<ProjectJobDto> Jobs { get; set; }
        public bool Started { get; set; }
    }
    public class ProjectJobDto
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public string ClassName { get; set; }
        public string Name { get; set; }
        public Point? Position { get; set; }
        public Dictionary<string, string?> JobParameters { get; set; }
        public List<long> NextJob { get; set; }

        public static ProjectJobDto Parse(ProjectJob j)
        {
            return new ProjectJobDto()
            {
                Id = j.Id,
                Name = j.Name,
                ClassName = j.ClassName,
                JobParameters = j.JobParameters,
                NextJob = j.NextJob,
                Position = j.Position,
                ProjectId = j.ProjectId,
            };
        }
    }
}
