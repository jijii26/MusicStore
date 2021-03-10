using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MusicStore.Models.ViewModels
{
    [CustomValidation(typeof(ModifierAlbum), "ValiderCover")]
    public class ModifierAlbum
    {
        public static ValidationResult ValiderCover(ModifierAlbum album)
        {
            if (album.CoverFile == null)
            {
                return ValidationResult.Success;
            }
            else if (album.CoverFile.ContentType != "image/jpeg")
            {
                return new ValidationResult("L'image doit être un fichier jpg.", new[] { "" });
            }
            return ValidationResult.Success;
        }

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
        public HttpPostedFileBase CoverFile { get; set; }
    }
}
