using Microsoft.EntityFrameworkCore;

namespace API.ManyToMany
{
    public class ManyToManyDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("My-Many-ToMany-Memory-Db");
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
            });

            modelBuilder.Entity<UserRole>(builder =>
            {
                builder.ToTable("UserRoles");
                builder.Property(ur => ur.Id).ValueGeneratedOnAdd();
                builder.HasKey(r => r.Id);

                builder.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId);

                builder.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId);
            });
        }
    }
}