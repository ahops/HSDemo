using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcWebRole1.Repository
{
    public enum RepositoryAction
    {
        Save,
        Delete,
        Get,
        List
    }

    public interface IRepository<T> where T : Entities.EntityBase, new()
    {
        /// <summary>
        /// Save the item, creating if new, or updating if it exists, returning a fully populated instance of the item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        T Save(T item);
        /// <summary>
        /// Deletes the provided item
        /// </summary>
        /// <param name="item"></param>
        void Delete(T item);
        /// <summary>
        /// Deletes the item with the corresponing ID
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
        /// <summary>
        /// Returns a populated item with the given id if found, otherwise, returns null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);
        /// <summary>
        /// Returns a fully populated item using the given item's Id property
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        T Get(T item);
        /// <summary>
        /// Returns a list of items matching the provided input filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEnumerable<T> List(T filter);
    }
}