using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
    public class SystemCountryCodeRepository : IDataRepository<SystemCountryCodePoco>
    {
        private readonly string _connStr;
        public SystemCountryCodeRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                foreach (SystemCountryCodePoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO[dbo].[System_Country_Codes]
                                      ([Code],
                                       [Name])
                                        Values
                                        (@Code,@Name)";

                    cmd.Parameters.AddWithValue("@Code", item.Code);
                    cmd.Parameters.AddWithValue("@Name", item.Name);

                    connection.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SystemCountryCodePoco> GetAll(params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"SELECT [Code]
                                  ,[Name]
                                  FROM [dbo].[System_Country_Codes]";

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                SystemCountryCodePoco[] pocos = new SystemCountryCodePoco[100];
                int index = 0;

                while (reader.Read())
                {
                    SystemCountryCodePoco poco = new SystemCountryCodePoco();
                    poco.Code = reader.GetString(0);
                    poco.Name = reader.GetString(1);

                    pocos[index] = poco;
                    index++;
                }
                connection.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<SystemCountryCodePoco> GetList(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemCountryCodePoco GetSingle(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemCountryCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;

                foreach (var item in items)
                {
                    cmd.CommandText = @"Delete System_Country_Codes
                                       Where [Code]= @Code";

                    cmd.Parameters.AddWithValue("@Code", item.Code);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;

                foreach (var item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[System_Country_Codes]
                                      Set
                                          [Code] = @Code,
                                          [Name] =@Name
                                          WHERE[Code]= @Code";

                    cmd.Parameters.AddWithValue("@Code", item.Code);
                    cmd.Parameters.AddWithValue("@Name", item.Name);

                    connection.Open();
                    int count = cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}
