using System.ComponentModel.DataAnnotations;

namespace LaLaFaMusicPortal.Models
{
    public class RegistrationRequest
    {
        public int RegistrationRequestId { get; set; }

        [Required]
        [StringLength(200)]
        public string Username { get; set; }

        [Required]
        [StringLength(200)]
        public string Password { get; set; }

        [Required]
        [StringLength(200)]
        public string Email { get; set; }

        public bool IsProcessed { get; set; }
    }
}
