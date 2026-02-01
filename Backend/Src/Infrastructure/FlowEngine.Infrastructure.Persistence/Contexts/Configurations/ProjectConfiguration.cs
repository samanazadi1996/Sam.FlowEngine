using FlowEngine.Domain.Projects.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Text.Json;

namespace FlowEngine.Infrastructure.Persistence.Contexts.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {

        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.ProjectJobs)
                   .WithOne(x => x.Project)
                   .HasForeignKey(x => x.ProjectId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
    public class ProjectJobConfiguration : IEntityTypeConfiguration<ProjectJob>
    {

        public void Configure(EntityTypeBuilder<ProjectJob> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.NextJob)
                .HasConversion(
                v => JsonSerializer.Serialize(v),
                v => JsonSerializer.Deserialize<List<string>>(v) ?? new List<string>()
                );

            builder.Property(p => p.JobParameters)
                .HasConversion(
                v => JsonSerializer.Serialize(v),
                v => JsonSerializer.Deserialize<Dictionary<string, string>>(v) ?? new Dictionary<string, string>()
                );

        }
    }
}
