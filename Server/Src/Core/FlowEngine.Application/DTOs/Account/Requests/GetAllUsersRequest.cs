using FlowEngine.Application.Parameters;

namespace FlowEngine.Application.DTOs.Account.Requests
{
    public class GetAllUsersRequest : PaginationRequestParameter
    {
        public string Name { get; set; }
    }
}
