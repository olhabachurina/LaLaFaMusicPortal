using System.ComponentModel.DataAnnotations;

namespace LaLaFaMusicPortal.Models
{
    public class Song
    {
        public int SongId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(200)]
        public string Artist { get; set; }

        public string? FilePath { get; set; }
        public string? ImagePath { get; set; }
        public string? VideoPath { get; set; }

        [Required]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        [StringLength(50)]
        public string Mood { get; set; }
    }
}