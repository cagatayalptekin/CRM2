
using Core;
using System;
using System.Collections.Generic;

namespace Domain;

public partial class Personel:IEntity
{
    public int Id { get; set; }

    public int? DepartmanId { get; set; }

    public string Ad { get; set; } = null!;

    public string Soyad { get; set; } = null!;

    public short Yas { get; set; }

    public DateTime DogumTarihi { get; set; }

    public int? RoleId { get; set; }

    public virtual Departman? Departman { get; set; }

    public virtual Role? Role { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
