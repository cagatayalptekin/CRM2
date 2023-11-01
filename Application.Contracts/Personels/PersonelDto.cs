using Application.Contracts.Departmans;
using Application.Contracts.Roles;
using Application.Contracts.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = Domain.Task;

namespace Application.Contracts.Personels
{
    public class PersonelDto
    {
        
        public int Id { get; set; }
        [Required]

        public int? DepartmanId { get; set; }
        [Required]
        public string Ad { get; set; } = null!;
        [Required]
        public string Soyad { get; set; } = null!;
        [Required]
        public short Yas { get; set; }
        [Required]
        public DateTime DogumTarihi { get; set; }
        [Required]
        public int? RoleId { get; set; }
        [ValidateNever]
        public virtual DepartmanDto? Departman { get; set; }
        [ValidateNever]
        public virtual RoleDto? Role { get; set; }

        public virtual ICollection<TaskDto> Tasks { get; set; } = new List<TaskDto>();
    }
}
