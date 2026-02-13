using FlowEngine.Application.DTOs;
using FlowEngine.Application.DTOs.Account.Requests;
using FlowEngine.Application.DTOs.Account.Responses;
using FlowEngine.Application.Interfaces.UserInterfaces;
using FlowEngine.Application.Wrappers;
using FlowEngine.Infrastructure.Identity.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FlowEngine.Infrastructure.Identity.Services
{
    public class GetUserServices(IdentityContext identityContext) : IGetUserServices
    {
        public async Task<PagedResponse<UserDto>> GetPagedUsers(GetAllUsersRequest model)
        {
            var skip = (model.PageNumber - 1) * model.PageSize;

            var users = identityContext.Users
                .Select(p => new UserDto()
                {
                    Name = p.Name,
                    Email = p.Email,
                    UserName = p.UserName,
                    PhoneNumber = p.PhoneNumber,
                    Id = p.Id,
                    Created = p.Created,
                    ProfileImage = p.ProfileImage
                });

            if (!string.IsNullOrEmpty(model.Name))
            {
                users = users.Where(p => p.Name.Contains(model.Name));
            }

            return new PaginationResponseDto<UserDto>(
                await users.Skip(skip).Take(model.PageSize).ToListAsync(),
                await users.CountAsync(),
                model.PageNumber,
                model.PageSize);

        }

        public async Task<UserDto> GetById(Guid userId)
        {
            var user = await identityContext.Users
                .Select(p => new UserDto()
                {
                    Name = p.Name,
                    Email = p.Email,
                    UserName = p.UserName,
                    PhoneNumber = p.PhoneNumber,
                    Id = p.Id,
                    Created = p.Created,
                    ProfileImage = p.ProfileImage
                }).Where(p => p.Id == userId)
                .FirstOrDefaultAsync();

            return user;
        }
    }
}
