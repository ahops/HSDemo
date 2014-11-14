using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MvcWebRole1.Repository
{


    internal static class RepositoryFactory
    {
        
        /// <summary>
        /// Creates the instance of the currently configured repository provider for the given type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal static IRepository<T> Create<T>() where T : Entities.EntityBase, new()
        {
            // uses naming convention based on entity name + repository to find corresponding repository implementation in web.config
            string typeName = typeof(T).Name + "Repository";
            return Activator.CreateInstance(Assembly.GetExecutingAssembly().GetType(ConfigurationManager.AppSettings[typeName])) as IRepository<T>;
        }
    }
}