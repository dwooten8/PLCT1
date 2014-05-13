using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protective.DAL
{
    public class RepositoryBase
    {
        public const string ConnectionString = "ProtectiveConnectionString";

        public IDbConnection GetConnection(string connectionName)
        {
            if (string.IsNullOrEmpty(connectionName))
                connectionName = ConnectionString;

            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[connectionName];
            if (settings == null) throw new DataException(string.Format("Connection {0} not found.", connectionName));

            string providerName = "System.Data.SqlClient";
            if (!string.IsNullOrEmpty(settings.ProviderName))
            {
                providerName = settings.ProviderName;
            }

            IDbConnection connection = DbProviderFactories.GetFactory(providerName).CreateConnection();
            connection.ConnectionString = settings.ConnectionString;
            return connection;
        }    
    }
}
