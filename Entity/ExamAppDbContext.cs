using ExamApp.Models;
using System.Data.Entity;

namespace ExamApp.Entity
{
    public class ExamAppDbContext : DbContext
    {
        public ExamAppDbContext() : base("name=ExamApp") { Database.SetInitializer(new ExamAppContextInitializer()); }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
    }
}