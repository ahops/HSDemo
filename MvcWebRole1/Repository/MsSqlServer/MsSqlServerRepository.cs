using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MvcWebRole1.Repository.MsSqlServer
{
    public abstract class MsSqlServerRepository<T> : ScriptedRepository<T> where T : Entities.EntityBase, new()
    {
        protected override string OnGetScriptPath(string webRootPath)
        {
            if (IsWebContext)
            {
                return Path.Combine(webRootPath, @"bin\Repository\MsSqlServer\Scripts");
            }
            else
            {
                return Path.Combine(webRootPath, @"Repository\MsSqlServer\Scripts");
            }
        }

        protected override System.Data.IDbConnection OnCreateConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["HSDemo"].ConnectionString);
        }
    }
}