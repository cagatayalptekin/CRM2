using Application.Contracts.Departmans;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Roles
{
    public interface IRolesService
    {
        public Task<ActionResult<IEnumerable<RoleDto>>> getAllRoles();
        public Task<ActionResult<RoleDto>> AddRole(RoleDto roleDto);
        public Task<ActionResult<RoleDto>> getRoleById(int id);
        public Task<ActionResult<RoleDto>> Guncelle(RoleDto roleDto);
        public Task<ActionResult<RoleDto>> deleteRole(int id);
        public Task<ActionResult<Boolean>> isRoleExist(RoleDto roleDto);

    }
}
