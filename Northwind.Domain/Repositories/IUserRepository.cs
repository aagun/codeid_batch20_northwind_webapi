
using Northwind.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Repositories
{
    public interface IUserRepository
    {
        bool SignIn(string userName,string userPassword);

        string SignOut(string userName, string userPassword);

        IEnumerable<Users> FindAllUser();

        Task<IEnumerable<Users>> FindAllUserAsync();

        Users FindUserByUsername(string userName);

        void Insert(Users User);

        int Edit(Users User);

        void Remove(Users User);
    }
}
