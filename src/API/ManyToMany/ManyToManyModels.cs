using System.Collections.Generic;

namespace API.ManyToMany
{
    public class User
    {
        public int Id { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }

    public class UserRole
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}