using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccessUsers
{
    public class UsersRepository : IUsersRepository
    {

        private ConfigurationData _connectionString;

        public UsersRepository(ConfigurationData connectionString)
        {
            _connectionString = connectionString;
        }

        protected SqlConnection DBConnection()
        {
            return new SqlConnection(_connectionString.ConnectionString);
        }


        public List<UserInfoME> GetUsers()
        {
            List<UserInfoME> users = new List<UserInfoME>();

            try
            {
                SqlConnection dbConnection = DBConnection();
                SqlCommand command = new SqlCommand("GET_USERS", dbConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandTimeout = 9999;

                dbConnection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UserInfoME user = new UserInfoME();
                                                                                                             
                        user.UsuId = (reader.GetValue(0) != DBNull.Value ? reader.GetInt32(0) : 0);
                        user.UserName = (!reader.IsDBNull(1) ? reader.GetString(1) : string.Empty);
                                                
                        user.UsuClient = new ClientME();
                        user.UsuClient.ClientId = (reader.GetValue(2) != DBNull.Value ? reader.GetInt32(2) : 0);

                        user.UsuClient.Identification = new ClientIdentiME();
                        user.UsuClient.Identification.IdentyId = (reader.GetValue(3) != DBNull.Value ? reader.GetInt32(3) : 0);
                        user.UsuClient.Identification.IdentiType = (!reader.IsDBNull(4) ? reader.GetString(4) : string.Empty);
                        
                        user.UsuClient.IdentiNumber = (!reader.IsDBNull(5) ? reader.GetString(5) : string.Empty);
                        
                        user.UsuClient.ClientName = (!reader.IsDBNull(6) ? reader.GetString(6) : string.Empty);
                        user.UsuClient.ClientLastName = (!reader.IsDBNull(7) ? reader.GetString(7) : string.Empty);

                        user.UsuClient.RolId = new RolesME();
                        user.UsuClient.RolId.RolID = (reader.GetValue(8) != DBNull.Value ? reader.GetInt32(8) : 0);
                        user.UsuClient.RolId.RolType = (!reader.IsDBNull(9) ? reader.GetString(9) : string.Empty);

                        user.UsuClient.GenreId = new GenreME();
                        user.UsuClient.GenreId.GenreId = (reader.GetValue(10) != DBNull.Value ? reader.GetInt32(10) : 0);
                        user.UsuClient.GenreId.GenreType = (!reader.IsDBNull(11) ? reader.GetString(11) : string.Empty);

                        user.UsuClient.RelatId = new RelationshipME();
                        user.UsuClient.RelatId.RelatId = (reader.GetValue(12) != DBNull.Value ? reader.GetInt32(12) : 0);
                        user.UsuClient.RelatId.RelatType = (!reader.IsDBNull(13) ? reader.GetString(13) : string.Empty);
                        
                        user.UsuClient.ClientAge = (reader.GetValue(14) != DBNull.Value ? reader.GetInt32(14) : 0);
                        user.UsuClient.ClientBirthday = (reader.GetValue(15) != DBNull.Value ? reader.GetDateTime(15) : DateTime.MinValue);
                        
                        user.UsuClient.UnderAgeId = new UnderAgeME();
                        user.UsuClient.UnderAgeId.UnderAgeId = (reader.GetValue(16) != DBNull.Value ? reader.GetInt32(16) : 0);
                        user.UsuClient.UnderAgeId.UnderAge = (!reader.IsDBNull(17) ? reader.GetString(17) : string.Empty);

                        user.UserPassword = (!reader.IsDBNull(18) ? reader.GetString(18) : string.Empty);

                        users.Add(user);
                    }
                }

                dbConnection.Close();
                dbConnection.Dispose();

            }

            catch (Exception ex)
            {
                
                ex.Message.ToString();
            }

            return users;
        }
        

        public UserInfoME GetUserById(int id)
        {
            UserInfoME user = new UserInfoME();

            try
            {
                SqlConnection dbConnection = DBConnection();
                SqlCommand command = new SqlCommand("GET_USER_BY_ID", dbConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandTimeout = 9999;
                command.Parameters.AddWithValue("@USU_ID", id);

                dbConnection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        user.UsuId = (reader.GetValue(0) != DBNull.Value ? reader.GetInt32(0) : 0);
                        user.UserName = (!reader.IsDBNull(1) ? reader.GetString(1) : string.Empty);

                        user.UsuClient = new ClientME();
                        user.UsuClient.ClientId = (reader.GetValue(2) != DBNull.Value ? reader.GetInt32(2) : 0);

                        user.UsuClient.Identification = new ClientIdentiME();
                        user.UsuClient.Identification.IdentyId = (reader.GetValue(3) != DBNull.Value ? reader.GetInt32(3) : 0);
                        user.UsuClient.Identification.IdentiType = (!reader.IsDBNull(4) ? reader.GetString(4) : string.Empty);

                        user.UsuClient.IdentiNumber = (!reader.IsDBNull(5) ? reader.GetString(5) : string.Empty);

                        user.UsuClient.ClientName = (!reader.IsDBNull(6) ? reader.GetString(6) : string.Empty);
                        user.UsuClient.ClientLastName = (!reader.IsDBNull(7) ? reader.GetString(7) : string.Empty);

                        user.UsuClient.RolId = new RolesME();
                        user.UsuClient.RolId.RolID = (reader.GetValue(8) != DBNull.Value ? reader.GetInt32(8) : 0);
                        user.UsuClient.RolId.RolType = (!reader.IsDBNull(9) ? reader.GetString(9) : string.Empty);

                        user.UsuClient.GenreId = new GenreME();
                        user.UsuClient.GenreId.GenreId = (reader.GetValue(10) != DBNull.Value ? reader.GetInt32(10) : 0);
                        user.UsuClient.GenreId.GenreType = (!reader.IsDBNull(11) ? reader.GetString(11) : string.Empty);

                        user.UsuClient.RelatId = new RelationshipME();
                        user.UsuClient.RelatId.RelatId = (reader.GetValue(12) != DBNull.Value ? reader.GetInt32(12) : 0);
                        user.UsuClient.RelatId.RelatType = (!reader.IsDBNull(13) ? reader.GetString(13) : string.Empty);

                        user.UsuClient.ClientAge = (reader.GetValue(14) != DBNull.Value ? reader.GetInt32(14) : 0);
                        user.UsuClient.ClientBirthday = (reader.GetValue(15) != DBNull.Value ? reader.GetDateTime(15) : DateTime.MinValue);

                        user.UsuClient.UnderAgeId = new UnderAgeME();
                        user.UsuClient.UnderAgeId.UnderAgeId = (reader.GetValue(16) != DBNull.Value ? reader.GetInt32(16) : 0);
                        user.UsuClient.UnderAgeId.UnderAge = (!reader.IsDBNull(17) ? reader.GetString(17) : string.Empty);

                        user.UserPassword = (!reader.IsDBNull(18) ? reader.GetString(18) : string.Empty);
                    }
                }

                dbConnection.Close();
                dbConnection.Dispose();

            }

            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return user;
        }
        
        
        public int CreateUser(UserInfoME user)
        {

            int id = 0;

            try
            {
                using (SqlConnection dbConnection = DBConnection())
                {
                    SqlCommand command = new SqlCommand("CREATE_USER", dbConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 9999;

                    dbConnection.Open();

                    command.Parameters.AddWithValue("@IDENTI_ID", user.UsuClient.Identification.IdentyId);
                    command.Parameters.AddWithValue("@IDENTI_NUMBER", user.UsuClient.IdentiNumber);
                    command.Parameters.AddWithValue("@CLIENT_NAME", user.UsuClient.ClientName);
                    command.Parameters.AddWithValue("@CLIENT_LAST_NAME", user.UsuClient.ClientLastName);
                    command.Parameters.AddWithValue("@GENRE_ID", user.UsuClient.GenreId.GenreId);
                    command.Parameters.AddWithValue("@RELAT_ID", user.UsuClient.RelatId.RelatId);
                    command.Parameters.AddWithValue("@CLIENT_AGE", user.UsuClient.ClientAge);
                    command.Parameters.AddWithValue("@CLIENT_BIRTHDAY", user.UsuClient.ClientBirthday);
                    command.Parameters.AddWithValue("@USERNAME", user.UserName);
                    command.Parameters.AddWithValue("@USERPASSWORD", user.UserPassword);


                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            id = dr.GetInt32(0);
                        }
                    }

                    dbConnection.Close();
                    dbConnection.Dispose();

                }
            }

            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return id;

        }


        public UserInfoME ValidateUser(UserInfoME user)
        {
            try
            {
                SqlConnection dbConnection = DBConnection();
                SqlCommand command = new SqlCommand("VALIDATE_USER", dbConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandTimeout = 9999;
                command.Parameters.AddWithValue("@USERNAME", user.UserName);
                command.Parameters.AddWithValue("@USERPASSWORD", user.UserPassword);

                dbConnection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user.UsuId = (reader.GetValue(0) != DBNull.Value ? reader.GetInt32(0) : 0);
                        user.UserName = (!reader.IsDBNull(1) ? reader.GetString(1) : string.Empty);
                    }
                }

                dbConnection.Close();
                dbConnection.Dispose();

            }

            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return user;


        }

        
        public int ModifyUser(UserInfoME user)
        {

            int UpdateUser = 0;

            try
            {
                using (SqlConnection dbConnection = DBConnection())
                {
                    SqlCommand command = new SqlCommand("MODIFY_USER", dbConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 9999;


                    command.Parameters.AddWithValue("@USU_ID", user.UsuId);
                    command.Parameters.AddWithValue("@IDENTI_ID", user.UsuClient.Identification.IdentyId);
                    command.Parameters.AddWithValue("@IDENTI_NUMBER", user.UsuClient.IdentiNumber);
                    command.Parameters.AddWithValue("@CLIENT_NAME", user.UsuClient.ClientName);
                    command.Parameters.AddWithValue("@CLIENT_LAST_NAME", user.UsuClient.ClientLastName);
                    command.Parameters.AddWithValue("@GENRE_ID", user.UsuClient.GenreId.GenreId);
                    command.Parameters.AddWithValue("@RELAT_ID", user.UsuClient.RelatId.RelatId);
                    command.Parameters.AddWithValue("@CLIENT_BIRTHDAY", user.UsuClient.ClientBirthday);
                    command.Parameters.AddWithValue("@USERNAME", user.UserName);
                    command.Parameters.AddWithValue("@PASSWORD", user.UserPassword);
                    
                    dbConnection.Open();

                    command.ExecuteNonQuery();

                    UpdateUser = 1;

                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }

            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return UpdateUser;
        }


        public int DeleteUser(int id)
        {

            int ValidateDelete = 0;

            try
            {
                using (SqlConnection dbConnection = DBConnection())
                {
                    SqlCommand command = new SqlCommand("DELETE_USER", dbConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 9999;

                    dbConnection.Open();

                    command.Parameters.AddWithValue("@USU_ID", id);

                    command.ExecuteNonQuery();

                    ValidateDelete = 1;

                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }

            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return ValidateDelete;

        }

    }
}
