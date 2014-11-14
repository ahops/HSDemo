using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MvcWebRole1.Repository
{
    public abstract class ScriptedRepository<T> : IRepository<T> where T : Entities.EntityBase, new()
    {
        static string _root;
        static bool _isWebContext = true;

        static ScriptedRepository()
        {
            if (HttpContext.Current == null)
            {
                _isWebContext = false;
                _root = Environment.CurrentDirectory;
            }
            else
                _root = HttpContext.Current.Server.MapPath("~"); 
        }

        protected static bool IsWebContext { get { return _isWebContext; } }

        public virtual T Save(T item)
        {
            return ExecuteScalar(item, RepositoryAction.Save);
        }

        public void Delete(T item)
        {
            Execute(item, RepositoryAction.Delete);
        }

        public virtual void Delete(int id)
        {
            T item = new T();
            item.Id = id;
            Delete(item);
        }

        public virtual T Get(int id)
        {
            T item = new T();
            item.Id = id;
            return Get(item);
        }

        public virtual T Get(T item)
        {
            return ExecuteScalar(item, RepositoryAction.Get);
        }

        public virtual IEnumerable<T> List(T filter)
        {
            return ExecuteEnumerable(filter, RepositoryAction.List);
        }


        protected abstract string OnGetScriptPath(string webRootPath);

        protected string LoadScript(RepositoryAction action)
        {
            return File.ReadAllText(
                Path.Combine(OnGetScriptPath(_root), 
                string.Format("{0}{1}.sql", typeof(T).Name, action.ToString())));
        }

        protected string MapScript(T item, string script)
        {
            if (item == null) return script;
            foreach (MemberInfo mi in item.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase))
            {
                if (mi is PropertyInfo)
                    script = MapItem(((PropertyInfo)mi).PropertyType, ((PropertyInfo)mi).GetValue(item), mi.Name, script);
                else if (mi is FieldInfo)
                   script = MapItem(((FieldInfo)mi).FieldType, ((FieldInfo)mi).GetValue(item), mi.Name, script);
            }
            return script;
        }

        protected string MapScript(T item, RepositoryAction action)
        {
            return MapScript(item, LoadScript(action));
        }

        protected void Execute(T item, RepositoryAction action)
        {
            string script = MapScript(item, action);
            using (IDbConnection con = OnCreateConnection())
            {
                IDbCommand cmd = con.CreateCommand();
                cmd.CommandText = script;
                cmd.CommandType = CommandType.Text;

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }

        protected T ExecuteScalar(T item, RepositoryAction action)
        {
            string script = MapScript(item, action);
            using (IDbConnection con = OnCreateConnection())
            {
                IDbCommand cmd = con.CreateCommand();
                cmd.CommandText = script;
                cmd.CommandType = CommandType.Text;

                con.Open();
                IDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                    return MapReaderToEntity(rdr);
                else
                    return default(T);
            }
        }

        private T MapReaderToEntity(IDataReader dataReader)
        {
            T item = new T();
            
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                MapReaderValue(dataReader.GetName(i), dataReader[i], item);
            }
            return item;
        }

        private void MapReaderValue(string memberName, object readerValue, T item)
        {
            MemberInfo mi = item.GetType().GetMember(memberName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase).FirstOrDefault();
            if (mi == null) return;
            if (mi is PropertyInfo)
            {
                PropertyInfo pi = mi as PropertyInfo;
                if (!pi.DeclaringType.Equals(typeof(T)))
                    pi = pi.DeclaringType.GetProperty(memberName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                pi.SetValue(item, readerValue);

            }
            else if (mi is FieldInfo)
            {
                FieldInfo fi = mi as FieldInfo;
                if (!fi.DeclaringType.Equals(typeof(T)))
                    fi = fi.DeclaringType.GetField(memberName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                fi.SetValue(item, readerValue);
            }
        }

        protected IEnumerable<T> ExecuteEnumerable(T item, RepositoryAction action)
        {
            string script = MapScript(item, action);
            using (IDbConnection con = OnCreateConnection())
            {
                IDbCommand cmd = con.CreateCommand();
                cmd.CommandText = script;
                cmd.CommandType = CommandType.Text;

                con.Open();

                return MapReaderToEntities(cmd.ExecuteReader());
            }
        }

        private IEnumerable<T> MapReaderToEntities(IDataReader dataReader)
        {
            List<T> list = new List<T>();
            while (dataReader.Read())
                list.Add(MapReaderToEntity(dataReader));
            return list;
        }

        protected abstract IDbConnection OnCreateConnection();

        private string MapItem(Type valueType, object value, string name, string script)
        {
            return script.Replace("@" + name, SqlEncode(valueType, value));
        }

        private string SqlEncode(Type valueType, object value)
        {
            if (valueType == typeof(string))
                return SqlEncodeString(value);
            else if (valueType.IsEnum)
                return SqlEncodeOther((int)value);
            else
                return SqlEncodeOther(value);
        }

        private string SqlEncodeOther(object value)
        {
            if (value == null) return "";
            else return value.ToString();
        }

        private string SqlEncodeString(object value)
        {
            if (value == null) return "''";
            else return "'" + value.ToString() + "'";
        }
    }
}