using Microsoft.EntityFrameworkCore;
using WikiMovies.Models;

namespace WikiMovies.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>option):base(option)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //seed category table
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = 1,
                NameCategory = "Accion",
                Status = true
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = 2,
                NameCategory = "Drama",
                Status = true
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = 3,
                NameCategory = "Ciencia Ficcion",
                Status = true
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = 4,
                NameCategory = "Anime",
                Status = true
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = 5,
                NameCategory = "Comedia",
                Status = true
            });
            //seed movies table
            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                Id=1,
                Name="Constantine",
                CategoryId=3
            });
            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                Id = 2,
                Name = "Hasta el ultimo hombre",
                CategoryId = 2
            });
            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                Id = 3,
                Name = "Jonny English",
                CategoryId = 5
            });
        }
    }
}
