using Application.Contracts.Departmans;
using Application.Contracts.Personels;
using Application.Contracts.Roles;
using Application.Contracts.Tasks;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = Domain.Task;

namespace Application
{
    public class CompanyAutoMapper:Profile
    {
        public CompanyAutoMapper()
        {
            CreateMap<Departman, DepartmanDto>();
            CreateMap<DepartmanDto, Departman>();
            CreateMap<Personel, PersonelDto>();
            CreateMap<PersonelDto, Personel >();
            CreateMap<RoleDto, Role>();
            CreateMap<Role, RoleDto>();
            CreateMap<Task, TaskDto>();
            CreateMap<TaskDto, Task>();
        }
        
    }
}
