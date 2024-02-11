using System;
using System.Collections.Generic;

namespace ArchivewebApplication.Models;

public partial class Organization
{
    public int OrganizationId { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
}
