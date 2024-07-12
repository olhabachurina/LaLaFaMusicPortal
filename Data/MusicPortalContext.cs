using LaLaFaMusicPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace LaLaFaMusicPortal.Data
{
    public class MusicPortalContext : DbContext
    {
        public MusicPortalContext(DbContextOptions<MusicPortalContext> options)
            : base(options)
        {
        }

        public DbSet<LaLaFaMusicPortal.Models.Genre> Genres { get; set; }
        public DbSet<LaLaFaMusicPortal.Models.Song> Songs { get; set; }
        public DbSet<LaLaFaMusicPortal.Models.User> Users { get; set; }
        public DbSet<LaLaFaMusicPortal.Models.RegistrationRequest> RegistrationRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for genres
            modelBuilder.Entity<LaLaFaMusicPortal.Models.Genre>().HasData(
                new LaLaFaMusicPortal.Models.Genre { GenreId = 1, Name = "Classical" },
                new LaLaFaMusicPortal.Models.Genre { GenreId = 2, Name = "Jazz" },
                new LaLaFaMusicPortal.Models.Genre { GenreId = 3, Name = "Hip-Hop/Rap" },
                new LaLaFaMusicPortal.Models.Genre { GenreId = 4, Name = "Disco" },
                new LaLaFaMusicPortal.Models.Genre { GenreId = 5, Name = "Rock" },
                new LaLaFaMusicPortal.Models.Genre { GenreId = 6, Name = "Pop Music" },
                new LaLaFaMusicPortal.Models.Genre { GenreId = 7, Name = "Blues" }
            );

            // Seed admin user with hashed password
            var passwordHash = BCrypt.Net.BCrypt.HashPassword("adminpassword");
            modelBuilder.Entity<LaLaFaMusicPortal.Models.User>().HasData(
                new LaLaFaMusicPortal.Models.User { UserId = 1, Username = "admin", PasswordHash = passwordHash, Email = "admin@example.com",IsAdmin = true, IsActive = true }
            );
            // Seed songs with mood and file paths
            modelBuilder.Entity<LaLaFaMusicPortal.Models.Song>().HasData(
                  new LaLaFaMusicPortal.Models.Song
                  {
                    SongId = 1,
                    Title = "Espresso",
                    Artist = "Sabrina Carpenter",
                    Mood = "Happy",
                    GenreId = 6,
                    UserId = 1,
                    FilePath = "/files/Sabrina Carpenter - Espresso.mp3",
                    ImagePath = "/images/Sabrina.jpg",
                    VideoPath = "/video/y2mate.com - Sabrina Carpenter  Espresso Official Video_360P.mp4"
                },
               new LaLaFaMusicPortal.Models.Song
               {
                    SongId = 2,
                    Title = "Englishman In New York",
                    Artist = "Sting",
                    Mood = "Sad",
                    GenreId = 5,
                    UserId = 1,
                    FilePath = "/files/Sting - Englishman In New York.mp3",
                    ImagePath = "/images/Sting.jpg",
                    VideoPath = "/video/utomp3.com - Sting  Englishman In New York.mp4"
                }
            );
        }

        public void EnsureSeedData()
        {
           
            if (Database.EnsureCreated())
            {
                
                if (!Users.Any())
                {
                    var passwordHash = BCrypt.Net.BCrypt.HashPassword("adminpassword");
                    Users.Add(new LaLaFaMusicPortal.Models.User { Username = "admin", PasswordHash = passwordHash, IsAdmin = true, IsActive = true });
                }


                if (!Genres.Any())
                {
                    Genres.AddRange(
                        new LaLaFaMusicPortal.Models.Genre { Name = "Classical" },
                        new LaLaFaMusicPortal.Models.Genre { Name = "Jazz" },
                        new LaLaFaMusicPortal.Models.Genre { Name = "Hip-Hop/Rap" },
                        new LaLaFaMusicPortal.Models.Genre { Name = "Disco" },
                        new LaLaFaMusicPortal.Models.Genre { Name = "Rock" },
                        new LaLaFaMusicPortal.Models.Genre { Name = "Pop Music" },
                        new LaLaFaMusicPortal.Models.Genre { Name = "Blues" }
                    );
                }

                if (!Songs.Any())
                {
                    Songs.AddRange(
                         new LaLaFaMusicPortal.Models.Song
                         {
                            Title = "Espresso",
                            Artist = "Sabrina Carpenter",
                            Mood = "Happy",
                            GenreId = 6,
                            UserId = 1,
                            FilePath = "/files/Sabrina Carpenter - Espresso.mp3",
                            ImagePath = "/images/Sabrina.jpg",
                            VideoPath = "/video/y2mate.com - Sabrina Carpenter  Espresso Official Video_360P.mp4"
                        },
                        new LaLaFaMusicPortal.Models.Song
                        {
                            Title = "Englishman In New York",
                            Artist = "Sting",
                            Mood = "Sad",
                            GenreId = 5,
                            UserId = 1,
                            FilePath = "/files/Sting - Englishman In New York.mp3",
                            ImagePath = "/images/Sting.jpg",
                            VideoPath = "/video/utomp3.com - Sting  Englishman In New York.mp4"
                        }
                    );
                }

                SaveChanges();
            }
        }
    }
}