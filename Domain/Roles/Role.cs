using Core;
using System;
using System.Collections.Generic;

namespace Domain;

public partial class Role:IEntity
{
    public int Id { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<Personel> Personels { get; set; } = new List<Personel>();
}
