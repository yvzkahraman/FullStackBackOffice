using System.Data.SqlClient;
using BackOffice.Data;
using BackOffice.Entities;
using BackOffice.Interfaces;

namespace BackOffice.Repos
{
    public class UserRepo :IRepo
    {

        public List<AppUser> GetAll()
        {

            var result = new List<AppUser>();


            SqlConnection connection = new SqlConnection(
                       "server=.; database=ReactBootcampDb2; user id=sa; password=<YourStrong@Passw0rd>"
                   );

            SqlCommand command = new SqlCommand();
            command.CommandText = "select * from Users";
            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;

            // Ob  Re Ma  

            // transaction 
            connection.Open();

            var reader = command.ExecuteReader();
            // command.ExecuteNonQuery();

            while (reader.Read())
            {
                // MAP TO DATABASE
                var user = new AppUser();
                user.Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                user.Name = reader.IsDBNull(1) ? "" : reader.GetString(1);
                user.Surname = reader.IsDBNull(2) ? "" : reader.GetString(2);
                user.Salary = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3);
                user.Username = reader.IsDBNull(4)?"":reader.GetString(4);
                user.Password = reader.IsDBNull(5)?"":reader.GetString(5);

                result.Add(user);
            }

            reader.Close();

            connection.Close();

            return result;

        }
    }
}