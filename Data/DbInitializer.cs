using System;
using System.Linq;
using Bogus;
using Microsoft.EntityFrameworkCore;
using PracticoLaboratorio4.Models;

namespace PracticoLaboratorio4.Data
{
    public class DbInitializer
    {
        public static async void Initialize(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (await context.Peliculas.AnyAsync())
            {
                return;
            }

            int generoId = 1;

            Faker<Genero> testGenero = new Faker<Genero>("es")
                .RuleFor(g => g.Id, f => generoId++)
                // se le agrega el id para asegurarse que sea unico
                // no habia generos de peliculas asi que uso generos de musica
                .RuleFor(g => g.Descripcion, f => $"{generoId} {f.Music.Genre()}");

            Genero[] generos = testGenero.GenerateLazy(15).ToArray();

            await context.AddRangeAsync(generos);

            await context.SaveChangesAsync();

            int directorId = 1;

            Faker<Director> testDirector = new Faker<Director>("es")
                .RuleFor(d => d.Id, f => directorId++)
                .RuleFor(d => d.Nombre, f => f.Name.FullName())
                .RuleFor(d => d.Biografia, f => f.Lorem.Paragraphs())
                .RuleFor(d => d.UrlFoto, f => f.Image.PicsumUrl(200, 200));

            Director[] directores = testDirector.GenerateLazy(30).ToArray();

            await context.AddRangeAsync(directores);

            await context.SaveChangesAsync();

            int peliculaId = 1;

            Faker<Pelicula> testPelicula = new Faker<Pelicula>("es")
                .RuleFor(p => p.Id, f => peliculaId++)
                .RuleFor(p => p.Titulo, f => f.Lorem.Word())
                .RuleFor(p => p.Genero, f => f.PickRandom(generos))
                .RuleFor(p => p.Director, f => f.PickRandom(directores))
                .RuleFor(p => p.Resumen, f => f.Lorem.Paragraphs())
                .RuleFor(p => p.FechaEstreno, f => f.Date.Past(50))
                .RuleFor(p => p.UrlImagenPortada, f => f.Image.PicsumUrl(270, 400))
                .RuleFor(p => p.Trailer, f => f.Internet.UrlWithPath("https", "youtube.com"));

            Pelicula[] peliculas = testPelicula.GenerateLazy(50).ToArray();

            await context.AddRangeAsync(peliculas);

            await context.SaveChangesAsync();
        }
    }
}
