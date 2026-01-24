using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Interfaces.Repositories;
using FlowEngine.Domain.Projects.Entities;
using FlowEngine.Infrastructure.Worker.Core;
using FlowEngine.Infrastructure.Worker.Seeds;

namespace FlowEngine.Infrastructure.Worker.Services;

public class FlowEngineServices(IUnitOfWork unitOfWork, FlowEngineContext flowEngineContext, IProjectRepository projectRepository, IAuthenticatedUserService authenticatedUser) : IFlowEngineServices
{
    public async Task Save(string projectName)
    {
        if (!flowEngineContext.Projects.TryGetValue(authenticatedUser.UserId, out var projects))
            return;

        var project = projects.FirstOrDefault(p => p.ProjectName.Equals(projectName));

        if (project is null) return;

        await projectRepository.AddAsync(project);
    }

    public async Task LoadData(string projectName)
    {

        var files = await projectRepository.GetAllAsync(
            string.IsNullOrEmpty(authenticatedUser.UserId) ? null : new Guid(authenticatedUser.UserId),
            projectName);
        
        foreach (var data in files)
        {
            var temp = new ProjectModel(data.ProjectName)
            {
                Data = data.Data,
                Started = false,
                Jobs = []
            };
            foreach (var j in data.Jobs ?? [])
            {
                var type = Type.GetType(j.ClassName);
                var job = (Activator.CreateInstance(type!) as IJob)!;
                job.ClassName = j.ClassName;
                job.Name = j.Name;
                job.NextJob = j.NextJob;
                if (j.JobParameters is not null)
                {
                    foreach (var item in j.JobParameters)
                        job.UpdateJobParameter(item.Key, item.Value.Value);
                }

                temp.Jobs.Add(job);
            }
            var userId = data.CreatedBy.ToString();

            if (!flowEngineContext.Projects.TryGetValue(userId, out var _))
                flowEngineContext.Projects[userId] = [];

            var currentProject = flowEngineContext.Projects[userId].FirstOrDefault(p => p.ProjectName == projectName);

            if (currentProject is not null)
            {
                currentProject.Stop();
                flowEngineContext.Projects[userId].Remove(currentProject);
            }
            flowEngineContext.Projects[userId].Add(temp);

            if (data.Started)
                temp.Start();
        }

    }

    public async Task Start(string projectName)
    {
        if (!flowEngineContext.Projects.TryGetValue(authenticatedUser.UserId, out var projects))
            return;

        var project = projects.FirstOrDefault(p => p.ProjectName.Equals(projectName));

        if (project is null)
            return;

        project.Start();
    }

    public async Task Stop(string projectName)
    {
        if (!flowEngineContext.Projects.TryGetValue(authenticatedUser.UserId, out var projects))
            return;

        var project = projects.FirstOrDefault(p => p.ProjectName.Equals(projectName));

        if (project is null)
            return;

        project.Stop();
    }

    public async Task CteateTemplate(string templateName)
    {
        Project? project = null;

        if (templateName == "Test")
            project = DefaultData.GetTestTemplate();

        if (templateName == "Test2")
            project = DefaultData.GetTestTemplate2();

        if (templateName == "Test3")
            project = DefaultData.GetTestTemplate3();

        if (project is not null)
        {
            await projectRepository.AddAsync(project);

            await unitOfWork.SaveChangesAsync();

            await LoadData(project.ProjectName);
        }
    }
}
