using Core;
using Domain.Personels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Roles
{
    public class EfRoleRepository: EfEntityRepositoryBase<Role, CompanyContext>, IRoleRepository
    {
        public EfRoleRepository(CompanyContext context) : base(context)
        {

        }
    }
}
