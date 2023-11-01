using Application.Contracts.Personels;
using Application.Contracts.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Tasks
{
    public class TaskFormViewModel
    {
        public TaskDto TaskDto { get; set; }
        public IEnumerable<PersonelDto> Personels { get; set; }
    }
}
