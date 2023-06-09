namespace DataAccessLayer.Repositories
{
    using DataAccessLayer.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUserData
    {
        //public List<User> GetUsers();
        public Task<string> RegisterUser(User user);
        public IQueryable<User> GetUsers();
    }
}
