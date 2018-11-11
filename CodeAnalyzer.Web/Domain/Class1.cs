using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Http;
using System.Data;
using Newtonsoft.Json.Linq;

namespace CodeAnalyzer.Web.Domain
{
    public class Customer
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class CustomerX
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class Class1
    {
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void Xdd()
        {
            List<CustomerX> list = new List<CustomerX>();

            for (int i = 0; i < 10; i++)
            {
                CustomerX x = new CustomerX();
                x.FirstName = RandomString(8);
                //x.LastName = RandomString(10);
                x.Email = RandomString(8);
                x.PhoneNumber = RandomString(10);
                list.Add(x);
            }

            //string res = JsonConvert.SerializeObject(list);
            string res = "sdfgadgadfg";

            List<Customer> customers = new List<Customer>();

            try
            {
                customers = JsonConvert.DeserializeObject<List<Customer>>(res);
            }
            catch
            {
                dynamic data = JsonConvert.DeserializeObject(res);
                Customer c = new Customer()
                {
                    ID = data.ID ?? 0,
                    FirstName = data?.FirstName ?? null,
                    LastName = data?.LastName ?? null,
                    EmailAddress = data?.EmailAddress ?? null,
                    PhoneNumber = data?.PhoneNumber ?? null
                };
                customers.Add(c);
            }       

            //string ID;
            //string FirstName = null;
            //string LastName = null;
            //string EmailAddress = null;
            //string PhoneNumber = null;

            foreach (Customer c in customers)
            {
                var IdParam = new SqlParameter("@ID", SqlDbType.Int);
                IdParam.Value = c.ID == 0 ? (object)c.ID : DBNull.Value;

                var FnameParam = new SqlParameter("@FirstName", SqlDbType.NVarChar);
                FnameParam.Value = (object)c.FirstName ?? DBNull.Value;

                var LnameParam = new SqlParameter("@LastName", SqlDbType.NVarChar);
                LnameParam.Value = (object)c.LastName ?? DBNull.Value;

                var EmailParam = new SqlParameter("@EmailAddress", SqlDbType.NVarChar);
                EmailParam.Value = (object)c.EmailAddress ?? DBNull.Value;

                var PhoneParam = new SqlParameter("@PhoneNumber", SqlDbType.NVarChar);
                PhoneParam.Value = (object)c.PhoneNumber ?? DBNull.Value;
            }

            int j = 0;
        }

        public async Task Xdd2()
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Customers;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("dbo.NewProcedure", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("ID", typeof(int)));
                    dt.Columns.Add(new DataColumn("FirstName", typeof(string)));
                    dt.Columns.Add(new DataColumn("LastName", typeof(string)));
                    dt.Columns.Add(new DataColumn("EmailAddress", typeof(string)));
                    dt.Columns.Add(new DataColumn("PhoneNumber", typeof(string)));

                    dt.Rows.Add(1006, "F5newnew", null, "@13", "3new");
                    dt.Rows.Add(0, null, "L11", "@18", "11");
                    dt.Rows.Add(0, "xd", "L11", "@19", null);

                    SqlParameter param = cmd.Parameters.AddWithValue("@CustTemp", dt);
                    // these next lines are important to map the C# DataTable object to the correct SQL User Defined Type
                    param.SqlDbType = SqlDbType.Structured;
                    param.TypeName = "dbo.CustomerTemp";

                    var SuccessParam = new SqlParameter("@Success", SqlDbType.Int);
                    SuccessParam.Direction = ParameterDirection.Output;
                    var MessageParam = new SqlParameter("@Message", SqlDbType.NVarChar, -1);
                    MessageParam.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(SuccessParam);
                    cmd.Parameters.Add(MessageParam);

                    //var IdParam = new SqlParameter("@ID", SqlDbType.Int);
                    //IdParam.Value = (object)ID ?? DBNull.Value;
                    //cmd.Parameters.Add(IdParam);

                    //var FnameParam = new SqlParameter("@FirstName", SqlDbType.NVarChar);
                    //FnameParam.Value = (object)FirstName ?? DBNull.Value;
                    //cmd.Parameters.Add(FnameParam);

                    //var LnameParam = new SqlParameter("@LastName", SqlDbType.NVarChar);
                    //LnameParam.Value = (object)LastName ?? DBNull.Value;
                    //cmd.Parameters.Add(LnameParam);

                    //var EmailParam = new SqlParameter("@EmailAddress", SqlDbType.NVarChar);
                    //EmailParam.Value = (object)EmailAddress ?? DBNull.Value;
                    //cmd.Parameters.Add(EmailParam);

                    //var PhoneParam = new SqlParameter("@PhoneNumber", SqlDbType.NVarChar);
                    //PhoneParam.Value = (object)PhoneNumber ?? DBNull.Value;
                    //cmd.Parameters.Add(PhoneParam);

                    //var SuccessParam = new SqlParameter("@Success", SqlDbType.Bit);
                    //SuccessParam.Direction = ParameterDirection.Output;
                    //var MessageParam = new SqlParameter("@Message", SqlDbType.NVarChar, 256);
                    //MessageParam.Direction = ParameterDirection.Output;

                    //cmd.Parameters.Add(SuccessParam);
                    //cmd.Parameters.Add(MessageParam);

                    await cmd.ExecuteNonQueryAsync();

                    //success = SuccessParam.Value as bool? ?? false;
                    //message = MessageParam.Value as string;
                }
            }
        }
    }
}
