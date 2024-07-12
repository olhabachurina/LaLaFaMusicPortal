using System.ComponentModel.DataAnnotations;

namespace LaLaFaMusicPortal.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string PasswordHash { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}
