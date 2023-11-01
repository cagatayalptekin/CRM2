using Application.Contracts.Personels;
using Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Tasks
{
    public class TaskDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string TaskName { get; set; } = null!;
        [Required]
        public int PersonelId { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [ValidateNever]
        public virtual PersonelDto Personel { get; set; }
    }
}
