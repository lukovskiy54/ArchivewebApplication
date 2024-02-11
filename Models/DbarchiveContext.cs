using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ArchivewebApplication.Models;

public partial class DbarchiveContext : DbContext
{
    public DbarchiveContext()
    {
    }

    public DbarchiveContext(DbContextOptions<DbarchiveContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<File> Files { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<Publication> Publications { get; set; }

    public virtual DbSet<PublicationType> PublicationTypes { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-1KGN05J; Database=DBArchive; Trusted_Connection=True; Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Department).WithMany(p => p.Authors)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Authors_Departments1");

            entity.HasMany(d => d.Publications).WithMany(p => p.Authors)
                .UsingEntity<Dictionary<string, object>>(
                    "AuthorPublication",
                    r => r.HasOne<Publication>().WithMany()
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AuthorPublication_Publications"),
                    l => l.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AuthorPublication_Authors"),
                    j =>
                    {
                        j.HasKey("AuthorId", "PublicationId");
                        j.ToTable("AuthorPublication");
                    });
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Organization).WithMany(p => p.Departments)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Departments_Organizations");
        });

        modelBuilder.Entity<File>(entity =>
        {
            entity.Property(e => e.FileObject).HasMaxLength(50);
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Url)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<Publication>(entity =>
        {
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.PublicationTypeNavigation).WithMany(p => p.Publications)
                .HasForeignKey(d => d.PublicationType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Publications_PublicationTypes");

            entity.HasMany(d => d.Files).WithMany(p => p.Publications)
                .UsingEntity<Dictionary<string, object>>(
                    "PublicationFile",
                    r => r.HasOne<File>().WithMany()
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PublicationFiles_Files"),
                    l => l.HasOne<Publication>().WithMany()
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PublicationFiles_Publications"),
                    j =>
                    {
                        j.HasKey("PublicationId", "FileId");
                        j.ToTable("PublicationFiles");
                        j.IndexerProperty<int>("FileId").ValueGeneratedOnAdd();
                    });

            entity.HasMany(d => d.Subjects).WithMany(p => p.Publications)
                .UsingEntity<Dictionary<string, object>>(
                    "PublicationSubject",
                    r => r.HasOne<Subject>().WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PublicationSubjects_Subjects"),
                    l => l.HasOne<Publication>().WithMany()
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PublicationSubjects_Publications"),
                    j =>
                    {
                        j.HasKey("PublicationId", "SubjectId");
                        j.ToTable("PublicationSubjects");
                        j.IndexerProperty<int>("SubjectId").ValueGeneratedOnAdd();
                    });
        });

        modelBuilder.Entity<PublicationType>(entity =>
        {
            entity.HasKey(e => e.TypeId);

            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
