using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tasks
{
    public class EfTaskRepository: EfEntityRepositoryBase<Task, CompanyContext>,ITaskRepository
    {
        public EfTaskRepository(CompanyContext context) : base(context)
        {

        }
    }
}
