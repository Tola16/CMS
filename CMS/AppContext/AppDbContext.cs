using CMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CMS.AppContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          

            modelBuilder.Entity<Genre>()
                .HasMany(genre => genre.Movies)
                .WithOne(movie => movie.Genre)
                .HasForeignKey(movie => movie.GenreId);

            modelBuilder.Entity<Customer>()
                .HasMany(customer => customer.Bookings)
                .WithOne(booking => booking.Customer)
                .HasForeignKey(booking => booking.CustomerId);

            modelBuilder.Entity<Movie>()
                .HasMany(movie => movie.Bookings)
                .WithOne(booking => booking.Movie)
                .HasForeignKey(booking => booking.MovieId);

            modelBuilder.Entity<Genre>()
                .HasIndex(genre => genre.Name)
                .IsUnique();

            modelBuilder.Entity<Movie>()
                .HasIndex(movie => movie.Title)
                .IsUnique();

            modelBuilder.Entity<Customer>()
                .HasIndex(customer => customer.Email)
                .IsUnique();

            modelBuilder.Entity<Booking>()
                .Property(booking => booking.BookingDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Movie>()
                .Property(movie => movie.TicketPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Comedy" },
                new Genre { Id = 3, Name = "Drama" },
                new Genre { Id = 4, Name = "Science Fiction" },
                new Genre { Id = 5, Name = "Animation" });

            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1, Title = "Skyline Pursuit", Director = "Nora Hassan", Duration = 118, ReleaseYear = 2022, Rating = 7.8m, TicketPrice = 12.50m, AvailableSeats = 80, GenreId = 1 },
                new Movie { Id = 2, Title = "The Last Signal", Director = "Omar Khaled", Duration = 132, ReleaseYear = 2024, Rating = 8.2m, TicketPrice = 14.00m, AvailableSeats = 65, GenreId = 4 },
                new Movie { Id = 3, Title = "A Quiet Promise", Director = "Maya Farouk", Duration = 104, ReleaseYear = 2021, Rating = 7.1m, TicketPrice = 10.00m, AvailableSeats = 90, GenreId = 3 },
                new Movie { Id = 4, Title = "Weekend Mix-Up", Director = "Karim Adel", Duration = 96, ReleaseYear = 2020, Rating = 6.9m, TicketPrice = 9.50m, AvailableSeats = 110, GenreId = 2 },
                new Movie { Id = 5, Title = "Orbit Nine", Director = "Lina Samir", Duration = 145, ReleaseYear = 2025, Rating = 8.5m, TicketPrice = 15.00m, AvailableSeats = 55, GenreId = 4 },
                new Movie { Id = 6, Title = "The Paper Kingdom", Director = "Salma Nabil", Duration = 101, ReleaseYear = 2023, Rating = 7.6m, TicketPrice = 11.00m, AvailableSeats = 75, GenreId = 5 },
                new Movie { Id = 7, Title = "Crossing Paths", Director = "Youssef Tarek", Duration = 126, ReleaseYear = 2019, Rating = 7.3m, TicketPrice = 10.50m, AvailableSeats = 95, GenreId = 3 },
                new Movie { Id = 8, Title = "Midnight Circuit", Director = "Hana Mostafa", Duration = 110, ReleaseYear = 2026, Rating = 8.0m, TicketPrice = 13.25m, AvailableSeats = 70, GenreId = 1 });

            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, FullName = "Mariam Ahmed", Email = "mariam.ahmed@example.com", PhoneNumber = "01010000001" },
                new Customer { Id = 2, FullName = "Adam Nasser", Email = "adam.nasser@example.com", PhoneNumber = "01010000002" },
                new Customer { Id = 3, FullName = "Layla Fathy", Email = "layla.fathy@example.com", PhoneNumber = "01010000003" },
                new Customer { Id = 4, FullName = "Ziad Mostafa", Email = "ziad.mostafa@example.com", PhoneNumber = "01010000004" },
                new Customer { Id = 5, FullName = "Farah Mahmoud", Email = "farah.mahmoud@example.com", PhoneNumber = "01010000005" });

            modelBuilder.Entity<Booking>().HasData(
                new Booking { Id = 1, BookingDate = new DateTime(2026, 9, 20), NumberOfTickets = 2, TotalPrice = 25.00m, CustomerId = 1, MovieId = 1 },
                new Booking { Id = 2, BookingDate = new DateTime(2026, 9, 21), NumberOfTickets = 1, TotalPrice = 14.00m, CustomerId = 2, MovieId = 2 },
                new Booking { Id = 3, BookingDate = new DateTime(2026, 9, 22), NumberOfTickets = 3, TotalPrice = 30.00m, CustomerId = 3, MovieId = 3 },
                new Booking { Id = 4, BookingDate = new DateTime(2026, 9, 23), NumberOfTickets = 2, TotalPrice = 30.00m, CustomerId = 4, MovieId = 5 },
                new Booking { Id = 5, BookingDate = new DateTime(2026, 9, 24), NumberOfTickets = 1, TotalPrice = 11.00m, CustomerId = 5, MovieId = 6 });
        }
    }
}
