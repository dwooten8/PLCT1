using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Protective.Core.Entity;
using Protective.Core.Interfaces.Repository;

namespace Protective.DAL.Repository
{
    public class MessageRepository : RepositoryBase, IRepository<Message>
    {
        public Message Create(Message item)
        {
            string sql = @"INSERT INTO [dbo].[Messages]
                               (MessageText, IsStarred, AddedDate)
                         VALUES
                               (@MessageText ,@IsStarred ,@AddedDate); SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection connection = GetConnection(RepositoryBase.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@MessageText", item.MessageText);
                parameters.Add("@IsStarred", item.IsStarred);
                parameters.Add("@AddedDate", item.AddedDate);

                connection.Open();
                item.Id = connection.Query<int>(sql, parameters, commandType: CommandType.Text).FirstOrDefault();

                if (item.Id <= 0)
                    throw new DataException("Unable to create the message.");

                return item;
            }
        }

        public bool Update(Message item)
        {
            string sql = "Update [dbo].[Messages] set IsStarred = @isStarred  where Id = @id; SET @count = @@ROWCOUNT";

            using (IDbConnection connection = GetConnection(RepositoryBase.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@isStarred", item.IsStarred);
                parameters.Add("@id", item.Id);
                parameters.Add("@count", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Open();
                int count = connection.Query<int>(sql, parameters, commandType: CommandType.Text).SingleOrDefault();
                int result = parameters.Get<int>("@count");
                return (result > 0);
            }
        }

        public bool Delete(Message item)
        {
            string sql = "DELETE FROM [dbo].[Messages] where Id = @id; SET @count = @@ROWCOUNT";

            using (IDbConnection connection = GetConnection(RepositoryBase.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", item.Id);
                parameters.Add("@count", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Open();
                int count = connection.Query<int>(sql, parameters, commandType: CommandType.Text).SingleOrDefault();
                int result = parameters.Get<int>("@count");
                return (result > 0);
            }
        }

        public Message GetEntity(Message item)
        {
            string sql = @"SELECT Id, MessageText, IsStarred, AddedDate
                                FROM [dbo].[Messages]
                                WHERE Id = @id";

            using (IDbConnection connection = GetConnection(RepositoryBase.ConnectionString))
            {              
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", item.Id);

                connection.Open();
                Message message = connection.Query<Message>(sql, parameters, commandType: CommandType.Text).FirstOrDefault();
                return message;
            }
        }

        public List<Message> GetList()
        {
            string sql = @"SELECT Id, MessageText, IsStarred, AddedDate
                                FROM [dbo].[Messages]";

            using (IDbConnection connection = GetConnection(RepositoryBase.ConnectionString))
            {
                connection.Open();
                List<Message> list = connection.Query<Message>(sql, commandType: CommandType.Text).ToList();
                return list;
            }
        }
    }
}
