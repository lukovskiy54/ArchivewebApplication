using System;
using System.Collections.Generic;

namespace ArchivewebApplication.Models;

public partial class File
{
    public int FileId { get; set; }

    public byte[] FileObject { get; set; } = null!;

    public int DownloadCount { get; set; }

    public DateOnly LastChangedDate { get; set; }

    public int PagesCount { get; set; }

    public virtual ICollection<Publication> Publications { get; set; } = new List<Publication>();
}
