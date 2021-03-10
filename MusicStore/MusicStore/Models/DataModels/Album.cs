namespace MusicStore.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    public class Album
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AlbumId { get; set; }
     
        [Required, StringLength(100)]
        public string Titre { get; set; }
        [Required, Range(1930, 2100)]
        public int AnneeParution { get; set; }
        [Required, DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required, StringLength(50)]
        public string Artiste { get; set; }
        [Required, Range(10, 100), DataType(DataType.Currency)]
        public decimal Prix { get; set; }
        [Required]
        public int GenreId { get; set; }

        [NotMapped]
        public string Cover { get => $"/Content/Images/Albums/{AlbumId}.jpg"; }

       
    }


}