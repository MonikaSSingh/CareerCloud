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
    public class CompanyProfileRepository : IDataRepository<CompanyProfilePoco>
    {
        private readonly string _connStr;
        public CompanyProfileRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params CompanyProfilePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;

                foreach (CompanyProfilePoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Profiles]
                                       ([Id]
                                       ,[Registration_Date]
                                       ,[Company_Website]
                                       ,[Contact_Phone]
                                       ,[Contact_Name]
                                       ,[Company_Logo])
                                 VALUES
                                       (@Id
                                       ,@Registration_Date
                                       ,@Company_Website
                                       ,@Contact_Phone
                                       ,@Contact_Name
                                       ,@Company_Logo)";

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);
                    
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

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;

                cmd.CommandText = @"SELECT [Id]
                                  ,[Registration_Date]
                                  ,[Company_Website]
                                  ,[Contact_Phone]
                                  ,[Contact_Name]
                                  ,[Company_Logo]
                                  ,[Time_Stamp]
                              FROM [dbo].[Company_Profiles]";

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                CompanyProfilePoco[] pocos = new CompanyProfilePoco[500];
                int index = 0;

                while (reader.Read())
                {
                    CompanyProfilePoco poco = new CompanyProfilePoco();
                    poco.Id = reader.GetGuid(0);
                    poco.RegistrationDate = reader.GetDateTime(1);
                    poco.CompanyWebsite = reader.IsDBNull(2)? null:reader.GetString(2);
                    poco.ContactPhone = reader.GetString(3);
                    poco.ContactName = reader.IsDBNull(4) ? null : reader.GetString(4);
                    //poco.CompanyLogo = (byte[]?)reader[5];
                    if (!Convert.IsDBNull(reader[5]))
                    {
                        poco.CompanyLogo = (byte[])reader[5];
                    }
                    if (!Convert.IsDBNull(reader[6]))
                    {
                        poco.CompanyLogo = (byte[])reader[6];
                    }
                    //poco.TimeStamp = (byte[]?)reader[7];

                    pocos[index] = poco;
                    index++;
                }
                connection.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;

                foreach (CompanyProfilePoco item in items)
                {
                    cmd.CommandText = @"Delete From Company_Profiles
                                     Where [Id]=@Id";

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    connection.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;

                foreach (CompanyProfilePoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Profiles]
                                   SET
                                       [Registration_Date] = @Registration_Date
                                      ,[Company_Website] = @Company_Website
                                      ,[Contact_Phone] = @Contact_Phone
                                      ,[Contact_Name] = @Contact_Name
                                      ,[Company_Logo] = @Company_Logo
                                    WHERE [Id]=@Id";

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                    cmd.Parameters.AddWithValue("Contact_Phone", item.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);
                    
                    connection.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}
