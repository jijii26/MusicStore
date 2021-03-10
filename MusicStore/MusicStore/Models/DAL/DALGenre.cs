namespace MusicStore.Models.DAL
{
    using MusicStore.Models.DataModels;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;

    public class DALGenre
    {
        protected string chConnexion = ConfigurationManager.ConnectionStrings["MusicStore"].ConnectionString;


        protected const string Genre_INSERT = @"INSERT INTO Genre (Nom) OUTPUT INSERTED.GenreId VALUES(@NOM)";
        protected const string Genre_DELETE = @"DELETE Genre WHERE GenreId=@GENREID";
        protected const string Genre_UPDATE = @"UPDATE Genre SET Nom=@NOM WHERE GenreId = @GENREID";
        protected const string Genre_SELECT = @"SELECT * FROM Genre";
        protected const string Genre_FIND = @"SELECT * FROM Genre WHERE GenreId = @GENREID";
        public void Add(Genre g)
        {
            using (SqlConnection connection = new SqlConnection(this.chConnexion))
            {
                SqlCommand commande = new SqlCommand(Genre_INSERT, connection);
                commande.Parameters.AddWithValue("NOM", g.Nom);
                connection.Open();
                g.GenreId = Convert.ToInt32(commande.ExecuteScalar());
                connection.Close();
            }

        }

        public void Delete(Genre g)
        {
            using (SqlConnection connection = new SqlConnection(this.chConnexion))
            {
                SqlCommand commande = new SqlCommand(Genre_DELETE, connection);
                commande.Parameters.AddWithValue("GENREID", g.GenreId);
                connection.Open();
                commande.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Update(Genre g)
        {
            using (SqlConnection connection = new SqlConnection(this.chConnexion))
            {
                SqlCommand commande = new SqlCommand(Genre_UPDATE, connection);
                commande.Parameters.AddWithValue("GENREID", g.GenreId);
                commande.Parameters.AddWithValue("NOM", g.Nom);
                connection.Open();
                commande.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<Genre> List()
        {
            using (SqlConnection connection = new SqlConnection(this.chConnexion))
            {
                SqlCommand commande = new SqlCommand(Genre_SELECT, connection);
                connection.Open();
                SqlDataReader dr = commande.ExecuteReader();
                List<Genre> lg = new List<Genre>();
               
                while (dr.Read())
                {
                    lg.Add(new Genre
                    {
                        GenreId = (int)dr["GenreId"],
                        Nom = dr["Nom"].ToString()
                    });
                }
                dr.Close();
                connection.Close();
                return lg;

            }
        }

        public Genre Find(int GenreId)
        {
            using (SqlConnection connection = new SqlConnection(this.chConnexion))
            {
                SqlCommand commande = new SqlCommand(Genre_FIND, connection);
                commande.Parameters.AddWithValue("GENREID", GenreId);
                connection.Open();
                SqlDataReader dr = commande.ExecuteReader();
                Genre g = null;
               
                if (dr.Read())
                {
                    g = new Genre
                    {
                        GenreId = (int)dr["GenreId"],
                        Nom = dr["Nom"].ToString()
                    };
                }
                dr.Close();
                connection.Close();
                return g;

            }
        }
    }
}