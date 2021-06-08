using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace core_projekt2
{
    public class DatabaseManipulation
    {

        private static string connectionString = @"Data Source=(localDb)\LocalDBGlamBay;Initial Catalog=master;Integrated Security=True";
        private static readonly object pLockObj = new object();
        private SqlCommand cmd2 { get; set; }


        public DatabaseManipulation()
        {
            cmd2 = new SqlCommand();
        }

        public DatabaseManipulation CommandEdit<T>(string name, T value)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = name;


            Type valueType = typeof(T);
            //TODO
            /*
            if (valueType == typeof(Ecategory))
            {
                int obj2 = (int)Convert.ChangeType(value, typeof(int));
                if (obj2 == 0)
                    param.Value = DBNull.Value;
                else
                    param.Value = obj2;
            }
            else
                param.Value = value;*/
            cmd2.Parameters.Add(param);
            return this;
        }

        public void Clear()
        {
            cmd2.Dispose();
            cmd2 = new SqlCommand();

        }

        public async Task<int> UpdateInsertDelete(string query) //  Non Query Execution Async
        {
            int res = 0;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using(SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            cmd2.Transaction = transaction;
                            cmd2.Connection = connection;
                            cmd2.CommandText = query;
                            res = await cmd2.ExecuteNonQueryAsync();

                            Clear();

                            if (res > 0)
                            {
                                transaction.Commit();
                                return 1;
                            }

                            else
                            {
                                transaction.Rollback();
                                return 0;
                            }
                        }
                        catch (SqlException e)
                        {
                                transaction.Rollback();
                                Console.WriteLine(e.Message);
                                return res;
                        }

                    }
                }
           //return res;
        }
           


        public async Task<List<T>> DbSelect<T>(string query) // Execute Just Query
        {
            List<T> list = new List<T>();
            Type type = typeof(T);

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            while (await dataReader.ReadAsync())
                            {
                                T obj = (T)Activator.CreateInstance(type);

                                var props = type.GetProperties().ToList();

                                foreach (var prop in props)
                                {
                                    var cName = dataReader.GetOrdinal(prop.Name);
                                    prop.SetValue(obj, dataReader.IsDBNull(cName) ? null : dataReader.GetValue(cName));
                                }
                                list.Add(obj);
                            }
                        }

                    }

                }
            }
            catch (SqlException e)
            {

                Console.WriteLine(e.Message);

            }

            return list;
        }
        public async Task<List<T>> DbSelect<T>(string query, bool CustomAtribute) // Execute Query with special cmd 
        {
            List<T> list = new List<T>();
            Type type = typeof(T);

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    cmd2.CommandText = query;
                    cmd2.Connection = connection;

                    using (SqlDataReader dataReader = cmd2.ExecuteReader())
                    {
                        while (await dataReader.ReadAsync())
                        {
                            T obj = (T)Activator.CreateInstance(type);

                            var props = type.GetProperties().ToList();

                            foreach (var prop in props)
                            {
                                var cName = dataReader.GetOrdinal(prop.Name);
                                prop.SetValue(obj, dataReader.IsDBNull(cName) ? null : dataReader.GetValue(cName));
                            }
                            list.Add(obj);
                        }
                    }



                }
            }
            catch (SqlException e)
            {

                Console.WriteLine(e.Message);

            }
            Clear();
            return list;
        }
        public async Task<T> dbScalar<T>(string query) // count, int
        {
            T res = default(T);
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        res = (T)await cmd.ExecuteScalarAsync();

                        return res;
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return res;
            }

        }

    }
}
