
using Northwind.Domain.Entities;
using Northwind.Domain.Repositories;
using Northwind.Persistence.Base;
using Northwind.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Persistence.Repositories
{
    internal class UsersRepository : RepositoryBase<Users>, IUserRepository
    {
        public UsersRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public int Edit(Users User)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Users> FindAllUser()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Users>> FindAllUserAsync()
        {
            throw new NotImplementedException();
        }

        public Users FindUserByUsername(string userName)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT user_id as UserId,user_name as UserName FROM dbo.users where user_name=@userName;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@userName",
                        DataType = DbType.String,
                        Value = userName
                    }
                }
            };

            var dataSet = FindByCondition<Users>(model);

            Users? item = dataSet.Current ?? null;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }


            return item;
        }

        public void Insert(Users User)
        {
            throw new NotImplementedException();
        }

        public void Remove(Users User)
        {
            throw new NotImplementedException();
        }

        public bool SignIn(string userName, string userPassword)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "dbo.SignIn",
                CommandType = CommandType.StoredProcedure,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@pUserName",
                        DataType = DbType.String,
                        Value = userName
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@pUserPassword",
                        DataType = DbType.String,
                        Value = userPassword
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@responseMessage",
                        DataType = DbType.String
                    }
                }
            };

            string result = _adoContext.ExecuteStoreProcedure(model, "@responseMessage",250);
            _adoContext.Dispose();

            
            return result == "User successfully logged in" ? true : false;
        }

        public string SignOut(string userName, string userPassword)
        {
            throw new NotImplementedException();
        }
    }
}
