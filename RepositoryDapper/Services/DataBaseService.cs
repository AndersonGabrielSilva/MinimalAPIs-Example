using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDapper.Services
{
    public static class DataBaseService
    {
        public static SqlConnection GetConnectionFactory()
        {
            const string CONNECTION_STRING = @"CONECTIONSTRING";

            return new SqlConnection(CONNECTION_STRING);
        }

    }
}
