namespace DataAccessLayer.Repositories
{
    using DataAccessLayer.Model;
    using Microsoft.Data.SqlClient;
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

        public User GetUserById(int ID)
        {
            var value = new SqlParameter("@ID", ID);
            var data = _sampleDBContext.User.FromSqlRaw<User>(@"Exec user_sp @ID", value).AsEnumerable();
            if(true)
            {
                return data.FirstOrDefault();

            }
        }

        public string prime(int num)
        {
            int count = 0;
            if (num > 1)
            {
                for (int i = 2; i < num / 2; i++)
                {
                    if (num % i == 0)
                    {
                        count++;
                    }

                }
                if (count == 0)
                {
                    return "Prime number";
                }
                else
                {
                    return "Not a Prime number";
                }
            }
            else
            {
                return "Not a Prime number";
            }
        }

    }
}
