using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Interfaces.Repositories;
using FlowEngine.Domain.Projects.Entities;
using FlowEngine.Infrastructure.Worker.Core;
using FlowEngine.Infrastructure.Worker.Seeds;
using System.Reflection;

namespace FlowEngine.Infrastructure.Worker.Services;

public class FlowEngineServices(IUnitOfWork unitOfWork, FlowEngineContext flowEngineContext, IProjectRepository projectRepository,
    IJobRepository jobRepository,
    IAuthenticatedUserService authenticatedUser) : IFlowEngineServices
{
    public async Task Save(string projectName)
    {
        if (!flowEngineContext.Projects.TryGetValue(authenticatedUser.UserId, out var projects))
            return;

        var project = projects.FirstOrDefault(p => p.ProjectName.Equals(projectName));

        if (project is null) return;

        await projectRepository.AddAsync(project);
    }

    public async Task LoadData(long? projectId)
    {

        var files = await projectRepository.GetAllAsync(
            string.IsNullOrEmpty(authenticatedUser.UserId) ? null : new Guid(authenticatedUser.UserId),
            projectId);

        foreach (var data in files)
        {
            var temp = new ProjectModel(data.ProjectName)
            {
                Id=data.Id,
                Started = false,
                Jobs = []
            };
            foreach (var j in data.ProjectJobs ?? [])
            {
                var type = Type.GetType(j.ClassName);
                var job = (Activator.CreateInstance(type!) as IJob)!;
                job.Id = j.Id;
                job.ClassName = j.ClassName;
                job.Name = j.Name;
                job.NextJob = j.NextJob;
                if (j.JobParameters is not null)
                {
                    foreach (var item in j.JobParameters)
                        job.UpdateJobParameter(item.Key, item.Value);
                }

                temp.Jobs.Add(job);
            }
            var userId = data.CreatedBy.ToString();

            if (!flowEngineContext.Projects.TryGetValue(userId, out var _))
                flowEngineContext.Projects[userId] = [];

            var currentProject = flowEngineContext.Projects[userId].FirstOrDefault(p => p.Id == projectId);

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

    public async Task Start(long projectId)
    {
        if (!flowEngineContext.Projects.TryGetValue(authenticatedUser.UserId, out var projects))
            return;

        var project = projects.FirstOrDefault(p => p.Id == projectId);

        if (project is null)
            return;

        project.Start();
    }

    public async Task Stop(long projectId)
    {
        if (!flowEngineContext.Projects.TryGetValue(authenticatedUser.UserId, out var projects))
            return;

        var project = projects.FirstOrDefault(p => p.Id == projectId);

        if (project is null)
            return;

        project.Stop();
    }

    public async Task CteateTemplate(string templateName)
    {
        Project? project = null;
        Dictionary<string, string>? NextJobs = null;
        List<Tuple<string, string, string>>? ParamsNextJobs = null;
        if (templateName == "Test")
        {
            var data = DefaultData.GetTestTemplate();
            project = data.Project;
            NextJobs = data.NextJobs;
        }

        if (templateName == "Test2")
        {
            var data = DefaultData.GetTestTemplate2();
            project = data.Project;
            NextJobs = data.NextJobs;
        }

        if (templateName == "Test3")
        {
            var data = DefaultData.GetTestTemplate3();
            project = data.Project;
            NextJobs = data.NextJobs;
            ParamsNextJobs = data.paramsNextJobs;
        }

        if (templateName == "Test4")
        {
            var data = DefaultData.GetTestTemplate4();
            project = data.Project;
            NextJobs = data.NextJobs;
        }

        if (project is not null)
        {
            await projectRepository.AddAsync(project);

            await unitOfWork.SaveChangesAsync();

            if (NextJobs is not null)
                foreach (var item in NextJobs)
                {
                    var job = project.ProjectJobs!.FirstOrDefault(p => p.Name == item.Key);
                    if (job is not null)
                        job.NextJob = [project.ProjectJobs!.First(p => p.Name == item.Value).Id];
                }

            if (ParamsNextJobs is not null)
                foreach (var item in ParamsNextJobs)
                {
                    var job = project.ProjectJobs!.FirstOrDefault(p => p.Name == item.Item1);
                    if (job is not null)
                    {
                        var nJob = project.ProjectJobs!.FirstOrDefault(p => p.Name == item.Item3);
                        if (nJob is not null)
                        {
                            job.JobParameters ??= [];
                            job.JobParameters[item.Item2] = nJob.Id + "";
                        }
                        jobRepository.Update(job);
                    }
                }

            await unitOfWork.SaveChangesAsync();

            await LoadData(project.Id);
        }
    }

    public List<ProjectJob> GetAllJobs()
    {
        List<ProjectJob> result = [];
        var jobs = Assembly.GetAssembly(typeof(IJob))!.GetTypes().Where(p => p.BaseType == typeof(IJob));

        foreach (var item in jobs)
        {
            var job = Activator.CreateInstance(item)! as IJob;

            result.Add(job);
        }
        return result;
    }
}
