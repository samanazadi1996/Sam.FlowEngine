using FlowEngine.Domain.Projects.Entities;
using FlowEngine.Domain.Projects.ValueObjects;
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

            builder.Property(p => p.Data)
                .HasConversion(
                v => JsonSerializer.Serialize(v),
                v => JsonSerializer.Deserialize<Dictionary<string, string>>(v) ?? new Dictionary<string, string>()
                );

            builder.Property(p => p.Jobs)
                .HasConversion(
                v => JsonSerializer.Serialize(v),
                v => JsonSerializer.Deserialize<List<ProjectJob>>(v) ?? new List<ProjectJob>()
                );

        }
    }
}
