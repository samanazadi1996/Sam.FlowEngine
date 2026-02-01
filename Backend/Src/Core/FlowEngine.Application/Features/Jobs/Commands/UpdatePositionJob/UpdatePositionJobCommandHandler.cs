using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Interfaces.Repositories;
using FlowEngine.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace FlowEnginex.Application.Features.Jobs.Commands.UpdatePositionJob;

public class UpdatePositionJobCommandHandler(IUnitOfWork unitOfWork, IJobRepository jobRepository) : IRequestHandler<UpdatePositionJobCommand, BaseResult>
{
    public async Task<BaseResult> Handle(UpdatePositionJobCommand request, CancellationToken cancellationToken)
    {
        var job = await jobRepository.GetByIdAsync(request.JobId);

        job.Position = new System.Drawing.Point(request.X, request.Y);
       
        await unitOfWork.SaveChangesAsync();

        return BaseResult.Ok();
    }
}