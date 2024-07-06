using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    internal class RoleRepository : GenericRepository<IdentityRole>, IRoleRepository
    {
        private readonly StoreContext _storeContext;

        public RoleRepository(StoreContext storeContext) : base(storeContext)
        {
           _storeContext = storeContext;
        }

        public async Task<string> GetUserRole(string userId)
        {
           var userRole = await _storeContext.UserRoles
                .FirstOrDefaultAsync(role => role.UserId == userId);

            if (userRole == null) return null;

            var role = await _storeContext.Roles.FirstOrDefaultAsync(roles => roles.Id == userRole.RoleId);

            return role.Name;
      
        }
    }
}
