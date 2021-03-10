namespace MusicStore.Models.DataModels {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Genre {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GenreId { get; set; }
        [Required, StringLength(50)]
        public string Nom { get; set; }
    }
}