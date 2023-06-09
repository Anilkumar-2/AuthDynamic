namespace DataAccessLayer.Repositories
{
    using DataAccessLayer.Model;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UserData:IUserData
    {
        private readonly SampleDBContext _sampleDBContext;
        public UserData(SampleDBContext sampleDBContext)
        {
            _sampleDBContext = sampleDBContext;
        }
        //public List<User> GetUsers()
        //{
        //    var data = _sampleDBContext.User.ToList();
        //    return data;
        //}

        public IQueryable<User> GetUsers()
        {
            //return _sampleDBContext.User
            //    .Include(x => x.Role);

            return _sampleDBContext.User
                .Include(x => x.Role);

            //var data = _sampleDBContext.User.ToList();
        }


        public async Task<string> RegisterUser(User user)
        {
            var data = new User()
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                RoleId = user.RoleId
            };

            await _sampleDBContext.User.AddAsync(data);
            await _sampleDBContext.SaveChangesAsync();
            return "Registration is Successfull";
        }

    }
}
