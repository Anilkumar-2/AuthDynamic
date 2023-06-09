using System;
using System.Collections.Generic;

namespace DataAccessLayer.Model
{
    public partial class UserRoles
    {
        public UserRoles()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
