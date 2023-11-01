using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Departmans
{
    public interface IDepartmanService
    {
        public Task<ActionResult<IEnumerable<DepartmanDto>>> getAllDepartmans();
        public Task<ActionResult<DepartmanDto>> AddDepartman(DepartmanDto departmanDto);
        public Task<ActionResult<DepartmanDto>> getDepartmanById(int id);
        public Task<ActionResult<DepartmanDto>> Guncelle(DepartmanDto departmanDto);
        public Task<ActionResult<DepartmanDto>> deleteDepartman(int id);
        public Task<ActionResult<Boolean>> isDepartmanExist(DepartmanDto departmanDto);



    }
}
