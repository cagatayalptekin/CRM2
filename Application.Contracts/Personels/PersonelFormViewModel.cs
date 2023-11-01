using Application.Contracts.Departmans;
using Application.Contracts.Roles;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Personels
{
    public class PersonelFormViewModel
    {
        [Required(ErrorMessage = "This field is required.")]
        public IEnumerable<DepartmanDto> Departmans { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public IEnumerable<RoleDto> Roles { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public PersonelDto PersonelDto { get; set; }
    }
}
