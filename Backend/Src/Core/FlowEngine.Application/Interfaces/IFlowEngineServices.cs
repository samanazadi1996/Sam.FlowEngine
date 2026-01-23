using FlowEngine.Application.Interfaces.Repositories;
using FlowEngine.Application.Wrappers;
using System.Threading.Tasks;

namespace FlowEngine.Application.Interfaces
{
    public interface IFlowEngineServices
    {
        Task Start(string projectName);
        Task Stop(string projectName);

        Task LoadData(string projectName);
        Task Save(string projectName);
        Task CteateTemplate(string templateName);
    }
}
