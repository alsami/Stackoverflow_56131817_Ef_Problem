using Microsoft.EntityFrameworkCore;

namespace API.OneToMany
{
    public class OneToManyDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("My-One-ToMany-Memory-Db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("Users");
                builder.HasKey(u => u.Id);
                builder.Property(u => u.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Role>(builder =>
            {
                builder.ToTable("Roles");
                builder.Property(r => r.Id).ValueGeneratedOnAdd();
                builder.HasKey(r => r.Id);
                builder.HasMany(r => r.Users)
                    .WithOne(u => u.Role)
                    .HasForeignKey(u => u.RoleId);
            });
        }
    }
}