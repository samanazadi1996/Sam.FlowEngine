using FlowEngine.Application.Interfaces;
using FlowEngine.Domain.Projects.Entities;
using FlowEngine.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FlowEngine.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IAuthenticatedUserService authenticatedUser) : DbContext(options)
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectJob> ProjectJobs { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            ChangeTracker.ApplyAuditing(authenticatedUser);

            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            this.ConfigureDecimalProperties(builder);

            base.OnModelCreating(builder);
        }
    }
}