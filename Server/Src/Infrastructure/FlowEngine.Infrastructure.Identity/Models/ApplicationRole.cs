using Microsoft.AspNetCore.Identity;
using System;

namespace FlowEngine.Infrastructure.Identity.Models
{
    public class ApplicationRole(string name) : IdentityRole<Guid>(name)
    {
    }
}
