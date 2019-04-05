using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using CrudTest.TO;

namespace CrudTest.DAL
{
    public class UserDAL
    {
        private string StringConnection = WebConfigurationManager.ConnectionStrings["dbCrudTest"].ConnectionString;

        ///<summary>Exclui um usuário pelo ID
        ///<param name="id">Id do usuário</param>
        ///</summary>
        public void DeleteById(long pId)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "DELETE [User] WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", pId);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        ///<summary>Lista todos os usuários
        ///<returns>Retorna uma lista com todos os usuários cadastrados</returns>
        ///</summary>
        public List<UserTO> GetAll()
        {
            string sql = "Select Id, Email, Name, Telephone FROM [User] ORDER BY Name";
            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                List<UserTO> lstUser = new List<UserTO>();
                UserTO user = null;

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            user = new UserTO
                            {
                                Id = (long)reader["Id"],
                                Email = reader["Email"].ToString(),
                                Name = reader["Name"].ToString(),
                                Telephone = reader["Telephone"].ToString()
                            };
                            lstUser.Add(user);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return lstUser;
            }
        }

        ///<summary>Obtém um usuário pelo ID
        ///<param name="id">Id do registro que obtido.</param>
        ///<returns>Retorna um usuário pelo id.</returns>
        ///</summary>
        public UserTO GetById(long pId)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "Select Id, Email, Name, Telephone FROM [User] WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", pId);
                UserTO user = null;

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                user = new UserTO
                                {
                                    Id = (long)reader["Id"],
                                    Email = reader["Email"].ToString(),
                                    Name = reader["Name"].ToString(),
                                    Telephone = reader["Telephone"].ToString()
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return user;
            }
        }

        ///<summary>Cadastra os dados de um usuário
        ///<param name="pUser">Objeto contendo os dados do usuário a ser cadastrado</param>
        ///</summary>
        public void Save(UserTO pUser)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "INSERT INTO [User] (Email, Name, Telephone) VALUES (@Email, @Name, @Telephone)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", pUser.Email);
                cmd.Parameters.AddWithValue("@Name", pUser.Name);

                if (!string.IsNullOrWhiteSpace(pUser.Telephone))
                    cmd.Parameters.AddWithValue("@Telephone", pUser.Telephone);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        ///<summary>Atualiza os dados de um usuário
        ///<param name="pUser">Objeto contendo os dados do usuário a ser atualizado</param>
        ///</summary>
        public void Update(UserTO pUser)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "UPDATE [User] SET Email = @Email, Name = @Name, Telephone = @Telephone WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", pUser.Id);
                cmd.Parameters.AddWithValue("@Email", pUser.Email);
                cmd.Parameters.AddWithValue("@Name", pUser.Name);

                if(!string.IsNullOrWhiteSpace(pUser.Telephone))
                    cmd.Parameters.AddWithValue("@Telephone", pUser.Telephone);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
