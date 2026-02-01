using FlowEngine.Domain.Projects.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlowEngine.Application.Interfaces
{
    public interface IFlowEngineServices
    {
        Task Start(string projectName);
        Task Stop(string projectName);

        Task LoadData(string projectName);
        Task CteateTemplate(string templateName);


        List<ProjectJob> GetAllJobs();
    }
}
