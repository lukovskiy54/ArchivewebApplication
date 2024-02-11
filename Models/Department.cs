using System;
using System.Collections.Generic;

namespace ArchivewebApplication.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string Name { get; set; } = null!;

    public int OrganizationId { get; set; }

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    public virtual Organization Organization { get; set; } = null!;
}
