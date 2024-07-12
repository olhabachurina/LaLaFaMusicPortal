using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace LaLaFaMusicPortal.Models
{
    public class Genre
    {
        public int GenreId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<Song> Songs { get; set; }
    }
}
