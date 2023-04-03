using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccessClients
{
    public class ClientsRepository : IClientsRepository
    {

        private ConfigurationData _connectionString;

        public ClientsRepository(ConfigurationData connectionString)
        {
            _connectionString = connectionString;
        }

        protected SqlConnection DBConnection()
        {
            return new SqlConnection(_connectionString.ConnectionString);
        }

        public List<ClientME> GetClients()
        {

            List<ClientME> clients = new List<ClientME>();

            try
            {
                SqlConnection dbConnection = DBConnection();
                SqlCommand command = new SqlCommand("GET_CLIENTS", dbConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandTimeout = 9999;

                dbConnection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ClientME client = new ClientME();

                        client.ClientId = (reader.GetValue(0) != DBNull.Value ? reader.GetInt32(0) : 0);

                        client.RolId = new RolesME();
                        client.RolId.RolID = (reader.GetValue(1) != DBNull.Value ? reader.GetInt32(1) : 0);
                        client.RolId.RolType = (!reader.IsDBNull(2) ? reader.GetString(2) : string.Empty);

                        client.Identification = new ClientIdentiME();
                        client.Identification.IdentyId = (reader.GetValue(3) != DBNull.Value ? reader.GetInt32(3) : 0);
                        client.Identification.IdentiType = (!reader.IsDBNull(4) ? reader.GetString(4) : string.Empty);
                       
                        client.IdentiNumber = (!reader.IsDBNull(5) ? reader.GetString(5) : string.Empty);
                        
                        client.ClientName = (!reader.IsDBNull(6) ? reader.GetString(6) : string.Empty);
                        client.ClientLastName = (!reader.IsDBNull(7) ? reader.GetString(7) : string.Empty);

                        client.GenreId = new GenreME();
                        client.GenreId.GenreId = (reader.GetValue(8) != DBNull.Value ? reader.GetInt32(8) : 0);
                        client.GenreId.GenreType = (!reader.IsDBNull(9) ? reader.GetString(9) : string.Empty);

                        client.RelatId = new RelationshipME();
                        client.RelatId.RelatId = (reader.GetValue(10) != DBNull.Value ? reader.GetInt32(10) : 0);
                        client.RelatId.RelatType = (!reader.IsDBNull(11) ? reader.GetString(11) : string.Empty);

                        client.ClientAge = (reader.GetValue(12) != DBNull.Value ? reader.GetInt32(12) : 0);
                        client.ClientBirthday = (reader.GetValue(13) != DBNull.Value ? reader.GetDateTime(13) : DateTime.MinValue);

                        client.UnderAgeId = new UnderAgeME();
                        client.UnderAgeId.UnderAgeId = (reader.GetValue(14) != DBNull.Value ? reader.GetInt32(14) : 0);
                        client.UnderAgeId.UnderAge = (!reader.IsDBNull(15) ? reader.GetString(15) : string.Empty);

                        client.UsuId = (reader.GetValue(16) != DBNull.Value ? reader.GetInt32(16) : 0);

                        clients.Add(client);
                    }
                }

                dbConnection.Close();
                dbConnection.Dispose();

            }

            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return clients;

        }
        
        
        public ClientME GetClientById(int id)
        {
            ClientME client = new ClientME();

            try
            {
                SqlConnection dbConnection = DBConnection();
                SqlCommand command = new SqlCommand("GET_CLIENT_BY_ID", dbConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandTimeout = 9999;
                command.Parameters.AddWithValue("@CLIENT_ID", id);

                dbConnection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        client.ClientId = (reader.GetValue(0) != DBNull.Value ? reader.GetInt32(0) : 0);

                        client.RolId = new RolesME();
                        client.RolId.RolID = (reader.GetValue(1) != DBNull.Value ? reader.GetInt32(1) : 0);
                        client.RolId.RolType = (!reader.IsDBNull(2) ? reader.GetString(2) : string.Empty);

                        client.Identification = new ClientIdentiME();
                        client.Identification.IdentyId = (reader.GetValue(3) != DBNull.Value ? reader.GetInt32(3) : 0);
                        client.Identification.IdentiType = (!reader.IsDBNull(4) ? reader.GetString(4) : string.Empty);

                        client.IdentiNumber = (!reader.IsDBNull(5) ? reader.GetString(5) : string.Empty);

                        client.ClientName = (!reader.IsDBNull(6) ? reader.GetString(6) : string.Empty);
                        client.ClientLastName = (!reader.IsDBNull(7) ? reader.GetString(7) : string.Empty);

                        client.GenreId = new GenreME();
                        client.GenreId.GenreId = (reader.GetValue(8) != DBNull.Value ? reader.GetInt32(8) : 0);
                        client.GenreId.GenreType = (!reader.IsDBNull(9) ? reader.GetString(9) : string.Empty);

                        client.RelatId = new RelationshipME();
                        client.RelatId.RelatId = (reader.GetValue(10) != DBNull.Value ? reader.GetInt32(10) : 0);
                        client.RelatId.RelatType = (!reader.IsDBNull(11) ? reader.GetString(11) : string.Empty);

                        client.ClientAge = (reader.GetValue(12) != DBNull.Value ? reader.GetInt32(12) : 0);
                        client.ClientBirthday = (reader.GetValue(13) != DBNull.Value ? reader.GetDateTime(13) : DateTime.MinValue);

                        client.UnderAgeId = new UnderAgeME();
                        client.UnderAgeId.UnderAgeId = (reader.GetValue(14) != DBNull.Value ? reader.GetInt32(14) : 0);
                        client.UnderAgeId.UnderAge = (!reader.IsDBNull(15) ? reader.GetString(15) : string.Empty);

                        client.UsuId = (reader.GetValue(16) != DBNull.Value ? reader.GetInt32(16) : 0);
                    }
                }

                dbConnection.Close();
                dbConnection.Dispose();

            }

            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return client;
        }


        public int CreateClient(ClientME client)
        {
            int id = 0;

            try
            {
                using (SqlConnection dbConnection = DBConnection())
                {
                    SqlCommand command = new SqlCommand("CREATE_CLIENT", dbConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 9999;

                    dbConnection.Open();

                    command.Parameters.AddWithValue("@USU_ID", client.UsuId);
                    command.Parameters.AddWithValue("@IDENTI_ID", client.Identification.IdentyId);
                    command.Parameters.AddWithValue("@IDENTI_NUMBER", client.IdentiNumber);
                    command.Parameters.AddWithValue("@CLIENT_NAME", client.ClientName);
                    command.Parameters.AddWithValue("@CLIENT_LAST_NAME", client.ClientLastName);
                    command.Parameters.AddWithValue("@GENRE_ID", client.GenreId.GenreId);
                    command.Parameters.AddWithValue("@RELAT_ID", client.RelatId.RelatId);
                    command.Parameters.AddWithValue("@CLIENT_AGE", client.ClientAge);
                    command.Parameters.AddWithValue("@CLIENT_BIRTHDAY", client.ClientBirthday);
                    
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


        public int ModifyClient(ClientME client)
        {

            int UpdateUser = 0;

            try
            {
                using (SqlConnection dbConnection = DBConnection())
                {
                    SqlCommand command = new SqlCommand("MODIFY_CLIENT", dbConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 9999;

                    dbConnection.Open();

                    command.Parameters.AddWithValue("@USU_ID", client.ClientId );
                    command.Parameters.AddWithValue("@IDENTI_ID", client.Identification.IdentyId);
                    command.Parameters.AddWithValue("@IDENTI_NUMBER", client.IdentiNumber);
                    command.Parameters.AddWithValue("@CLIENT_NAME", client.ClientName);
                    command.Parameters.AddWithValue("@CLIENT_LAST_NAME", client.ClientLastName);
                    command.Parameters.AddWithValue("@GENRE_ID", client.GenreId.GenreId);
                    command.Parameters.AddWithValue("@RELAT_ID", client.RelatId.RelatId);
                    command.Parameters.AddWithValue("@CLIENT_AGE", client.ClientAge);
                    command.Parameters.AddWithValue("@CLIENT_BIRTHDAY", client.ClientBirthday);
                    command.Parameters.AddWithValue("@CLIENT_ID", client.ClientId);

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


        public int DeleteClient(int id)
        {

            int ValidateDelete = 0;

            try
            {
                using (SqlConnection dbConnection = DBConnection())
                {
                    SqlCommand command = new SqlCommand("DELETE_CLIENT", dbConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 9999;

                    dbConnection.Open();

                    command.Parameters.AddWithValue("@CLIENT_ID", id);

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
