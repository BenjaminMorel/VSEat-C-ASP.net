using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDB
    {
        private IConfiguration Configuration { get; }
        public UserDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ShowUser()
        {       
             string connectionString = Configuration.GetConnectionString("DefaultConnection");


            try
            {
                
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[User]";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            User MyUser = new User();

                            MyUser.IdUser = (int)dr["IdUser"];
                            MyUser.FirstName = (string)dr["FirstName"];
                            MyUser.LastName = (string)dr["LastName"];
                            MyUser.PhoneNumber = (string)dr["PhoneNumber"];
                            MyUser.EmailAddress = (string)dr["EmailAddress"];


                            Console.Write(MyUser.ToString());
                        }
                    }

                

                }
            }
            catch (Exception e)
            {
                Console.Write("ERROR\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n");
            }
        }
    }
}
