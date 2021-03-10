using MusicStore.Models.DAL;

namespace MusicStore.Models {

    public class Depot {
        public DALUtilisateur Utilisateurs { get; private set; } = new DALUtilisateur();
        public DALGenre Genres { get; private set; } = new DALGenre();
        public DALALbum Albums { get; private set; } = new DALALbum();
        public DALPanier Panier { get; private set; } = new DALPanier();
    }

}