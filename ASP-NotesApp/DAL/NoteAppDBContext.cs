using ASP_NotesApp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ASP_NotesApp.DAL
{
    public class NoteAppDBContext : DbContext 
    {
        public NoteAppDBContext() { }

        public NoteAppDBContext(DbContextOptions<NoteAppDBContext> options)
            : base(options) 
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Note> Notes { get; set; } = null!;
        public DbSet<NoteStatus> NoteStatuses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {          
            modelBuilder.Entity<NoteStatus>().ToTable("NoteStatus");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<NoteStatus>().HasData(
                new NoteStatus { Id = 1, Name = "Active" },
                new NoteStatus { Id = 2, Name = "Archive" },
                new NoteStatus { Id = 3, Name = "Deleted" }
                );
            
        }
    }
}
