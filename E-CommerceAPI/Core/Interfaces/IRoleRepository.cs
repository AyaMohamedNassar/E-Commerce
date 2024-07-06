using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRoleRepository: IGenericRepository<IdentityRole>
    {
        Task<string> GetUserRole(string userId);
    }
}
