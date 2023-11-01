using Core;

using System;
using System.Collections.Generic;

namespace Domain;

public partial class Task:IEntity
{
    public int Id { get; set; }

    public string TaskName { get; set; } = null!;

    public int PersonelId { get; set; }

    public DateTime Deadline { get; set; }

    public virtual Personel Personel { get; set; } = null!;
}
