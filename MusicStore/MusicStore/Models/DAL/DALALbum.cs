namespace MusicStore.Models.DAL
{
    using MusicStore.Models.DataModels;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;

    public class DALALbum
    {
        
        protected string chConnexion = ConfigurationManager.ConnectionStrings["MusicStore"].ConnectionString;


        protected const string Album_INSERT = @"insert into Album (Titre,AnneeParution,Description,GenreId,Artiste,Prix) OUTPUT INSERTED.AlbumId VALUES(@TITRE,@ANNEEPARUTION,@DESCRIPTION,@GENREID,@ARTISTE,@PRIX)";
        protected const string Album_DELETE = @"DELETE Album WHERE AlbumId=@ALBUMID";
        protected const string Album_UPDATE = @"UPDATE Album SET Titre=@TITRE,AnneeParution=@ANNEEPARUTION,Description=@DESCRIPTION,GenreId=@GENREID,Artiste=@ARTISTE,Prix=@PRIX WHERE AlbumId=@ALBUMID";
        protected const string Album_SELECT = @"SELECT * FROM Album";
        protected const string Album_FIND = @"SELECT * FROM Album WHERE AlbumId=@ALBUMID";

        public void Add(Album a)
        {
            using (SqlConnection connection = new SqlConnection(this.chConnexion))
            {
                SqlCommand commande = new SqlCommand(Album_INSERT, connection);
                commande.Parameters.AddWithValue("TITRE", a.Titre);
                commande.Parameters.AddWithValue("ANNEEPARUTION", a.AnneeParution);
                commande.Parameters.AddWithValue("DESCRIPTION", a.Description);
                commande.Parameters.AddWithValue("GENREID", a.GenreId);
                commande.Parameters.AddWithValue("ARTISTE", a.Artiste);
                commande.Parameters.AddWithValue("PRIX", a.Prix);
                connection.Open();
                a.AlbumId = (int)commande.ExecuteScalar();
                connection.Close();
            }

        }

        public void Delete(Album a)
        {
            using (SqlConnection connection = new SqlConnection(this.chConnexion))
            {
                SqlCommand commande = new SqlCommand(Album_DELETE, connection);
                commande.Parameters.AddWithValue("ALBUMID", a.AlbumId);
                connection.Open();
                commande.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Update(Album a)
        {
            using (SqlConnection connection = new SqlConnection(this.chConnexion))
            {
                SqlCommand commande = new SqlCommand(Album_UPDATE, connection);
                commande.Parameters.AddWithValue("ALBUMID", a.AlbumId);
                commande.Parameters.AddWithValue("TITRE", a.Titre);
                commande.Parameters.AddWithValue("ANNEEPARUTION", a.AnneeParution);
                commande.Parameters.AddWithValue("DESCRIPTION", a.Description);
                commande.Parameters.AddWithValue("GENREID", a.GenreId);
                commande.Parameters.AddWithValue("ARTISTE", a.Artiste);
                commande.Parameters.AddWithValue("PRIX", a.Prix);
                connection.Open();
                commande.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<Album> List()
        {
            using (SqlConnection connection = new SqlConnection(this.chConnexion))
            {
                SqlCommand commande = new SqlCommand(Album_SELECT, connection);
                connection.Open();
                SqlDataReader dr = commande.ExecuteReader();
                List<Album> la = new List<Album>();

                while (dr.Read())
                {
                    la.Add(new Album
                    {
                        AlbumId = (int)dr["AlbumId"],
                        Titre = dr["Titre"].ToString(),
                        AnneeParution = (int)dr["AnneeParution"],
                        Description = dr["Description"].ToString(),
                        GenreId = (int)dr["GenreId"],
                        Artiste = dr["Artiste"].ToString(),

                        //pour le prix au lieu de double
                        Prix = Convert.ToDecimal(dr["Prix"])
                    });
                }
                dr.Close();
                connection.Close();
                return la;

            }
        }

        public Album Find(int AlbumId)
        {
            using (SqlConnection connection = new SqlConnection(this.chConnexion))
            {
                SqlCommand commande = new SqlCommand(Album_FIND, connection);
                commande.Parameters.AddWithValue("ALBUMID", AlbumId);
                connection.Open();
                SqlDataReader dr = commande.ExecuteReader();
                Album a = null;

                if (dr.Read())
                {
                    a = new Album
                    {
                        AlbumId = (int)dr["AlbumId"],
                        Titre = dr["Titre"].ToString(),
                        AnneeParution = (int)dr["AnneeParution"],
                        Description = dr["Description"].ToString(),
                        GenreId = (int)dr["GenreId"],
                        Artiste = dr["Artiste"].ToString(),
                        Prix = Convert.ToDecimal(dr["Prix"])
                    };
                }
                dr.Close();
                connection.Close();
                return a;
            }
        }
    }
}