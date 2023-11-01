using Application.Contracts.Personels;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Roles
{
    public class RoleDto
    {
        public int Id { get; set; }
        [Required]
        public string? RoleName { get; set; }

        public virtual ICollection<PersonelDto> Personels { get; set; } = new List<PersonelDto>();
    }
}
