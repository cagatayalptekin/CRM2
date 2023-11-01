

using Core;
using System;
using System.Collections.Generic;

namespace Domain;

public partial class Departman : IEntity
{
    public int Id { get; set; }

    public string DepartmanName { get; set; } = null!;

    public virtual ICollection<Personel> Personels { get; set; } = new List<Personel>();
   
}
