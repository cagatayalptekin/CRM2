using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Personels
{
    public class EfPersonelRepository:EfEntityRepositoryBase<Personel, CompanyContext>,IPersonelRepository
    {
        public EfPersonelRepository(CompanyContext context) : base(context)
        {

        }
    }
}
