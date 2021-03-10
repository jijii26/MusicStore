using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicStore.Models.ViewModels.Album
{
    //permet l'ajout de mes albums
    [CustomValidation(typeof(AjouterAlbum), "ValiderCover")]
    public class AjouterAlbum
    {
        public static ValidationResult ValiderCover(AjouterAlbum album)
        {
            if (album.CoverFile == null)
            {
                return new ValidationResult("il manque une image veuiller en mettre un ", new[] { "" });
            }
            else if (album.CoverFile != null && album.CoverFile.ContentType != "image/jpeg")
            {
                return new ValidationResult("il manque une image veuiller en mettre un de type jpg", new[] { "" });
            }
            return ValidationResult.Success;
        }

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
        public HttpPostedFileBase CoverFile { get; set; }
    }
}