using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Departmans
{
    public class EfDepartmanRepository: EfEntityRepositoryBase<Departman, CompanyContext>, IDepartmanRepository
    {
        public EfDepartmanRepository(CompanyContext context) : base(context)
        {

        }


    }
}
