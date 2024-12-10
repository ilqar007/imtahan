using imtahan.BLL.DomainModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace imtahan.DAL.DataContext
{
    public class ImtahanContext : DbContext
    {
        public ImtahanContext()
        {
        }

        public ImtahanContext(DbContextOptions<ImtahanContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exam>()
                .HasKey(c => new { c.ClassCode, c.StudentNumber });
        }

        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
    }
}