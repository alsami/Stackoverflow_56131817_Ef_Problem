using System.Collections.Generic;

namespace API.OneToMany
{
    public class User
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }
    }

    public class Role
    {
        public Role()
        {
            this.Users = new List<User>();
        }

        public int Id { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
