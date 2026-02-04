using FlowEngine.Domain.Projects.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlowEngine.Application.Interfaces
{
    public interface IFlowEngineServices
    {
        Task Start(long projectId);
        Task Stop(long projectId);

        Task LoadData(long? projectId);
        Task CteateTemplate(string templateName);


        List<ProjectJobDto> GetAllJobs();
        Dictionary<string,string> GetData(long projectId);
    }
}
