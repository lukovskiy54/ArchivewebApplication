using System;
using System.Collections.Generic;

namespace ArchivewebApplication.Models;

public partial class PublicationType
{
    public int TypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Publication> Publications { get; set; } = new List<Publication>();
}
