using System;
using System.Collections.Generic;

namespace ArchivewebApplication.Models;

public partial class Publication
{
    public int PublicationId { get; set; }

    public string Name { get; set; } = null!;

    public int File { get; set; }

    public int PublicationType { get; set; }

    public virtual PublicationType PublicationTypeNavigation { get; set; } = null!;

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    public virtual ICollection<File> Files { get; set; } = new List<File>();

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
