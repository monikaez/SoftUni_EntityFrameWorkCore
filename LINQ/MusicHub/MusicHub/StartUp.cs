using MusicHub.Data.Models;

namespace MusicHub
{
    using System;
    using System.Diagnostics;
    using System.Text;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Protocols.OpenIdConnect;
    using MusicHub.Data.Models;

    public class StartUp
    {
        public static void Main()
        {
            MusicHubDbContext context =
                new MusicHubDbContext();

            DbInitializer.ResetDatabase(context);

            //Test your solutions here
            string output = string.Empty;

            // Test 02.Export Albums Info
            //output = ExportAlbumsInfo(context, 9);

            //Test 03.Export Songs Above Duration
            output = ExportSongsAboveDuration(context, 4);

            Console.WriteLine(output);
        }
        //2 
        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var albumInfo = context.Producers
                .First(p => p.Id == producerId)
                .Albums
                .Select(a => new
                {
                    AlbumName = a.Name,
                    ReleaseDate = a.ReleaseDate.ToString("MM/dd/yyyy"),
                    ProduserName = a.Producer.Name,
                    Songs = a.Songs.Select(s => new
                    {
                        SongName = s.Name,
                        Price = s.Price,
                        SongWriterName = s.Writer.Name
                    })
                    .OrderByDescending(s => s.SongName)
                    .ThenBy(s => s.SongWriterName),
                    AlbumPrice = a.Price
                })
                .OrderByDescending(a => a.AlbumPrice);
            //output
            StringBuilder sb = new();
            foreach (var album in albumInfo)
            {
                sb.AppendLine($"-AlbumName: {album.AlbumName}");
                sb.AppendLine($"-ReleaseDate: {album.ReleaseDate}");
                sb.AppendLine($"-ProducerName: {album.ProduserName}");
                sb.AppendLine("-Songs:");
                if (album.Songs.Any())
                {
                    int counter = 0;
                    foreach (var song in album.Songs)
                    {
                        counter++;
                        sb.AppendLine($"---#{counter}");
                        sb.AppendLine($"---SongName: {song.SongName}");
                        sb.AppendLine($"---Price: {song.Price:f2}");
                        sb.AppendLine($"---Writer: {song.SongWriterName}");

                    }
                }
                sb.AppendLine($"-AlbumPrice: {album.AlbumPrice:f2}");
            }
            return sb.ToString().TrimEnd();
        }

        //3.Songs Above Given Duration
        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            //For each Song, export its Name, Performer Full Name, Writer Name, Album Producer and Duration(in format("c")).Sort the Songs by their Name(ascending), and then by Writer(ascending). If a Song has more than one Performer, export all of them and sort them(ascending). If there are no Performers for a given song, don't print the "---Performer" line at all.
            //find sonds
            var songs = context.Songs
           .Include(s=>s.SongPerformers)
           .ThenInclude(sp=>sp.Performer)
           .Include(s=>s.Writer)
           .Include(s=>s.Album)
           .ThenInclude(a=>a.Producer).AsEnumerable()
            .Where(s => s.Duration.TotalSeconds > duration)
            .Select(s => new
            {
                SongName = s.Name,
                WriterName = s.Writer.Name,
                Performers = s.SongPerformers
                    .Select(p => new
                    {
                        PerformerFullName = $"{p.Performer.FirstName} {p.Performer.LastName}"
                    })
                    .OrderBy(p => p.PerformerFullName)
                    .ToList(),
                AlbumProducer = s.Album.Producer.Name,
                Duration = s.Duration.ToString("c")
            })
            .OrderBy(s => s.SongName)
            .ThenBy(s => s.WriterName)
            .ToList();

            //Output
            int counter = 0;
            StringBuilder sb = new StringBuilder();

            foreach (var song in songs)
            {
                counter++;
                sb.AppendLine($"-Song #{counter}");
                sb.AppendLine($"---SongName: {song.SongName}");
                sb.AppendLine($"---Writer: {song.WriterName}");

                if (song.Performers.Any())
                {
                    foreach (var performer in song.Performers)
                    {
                        sb.AppendLine($"---Performer: {performer.PerformerFullName}");
                    }
                }

                sb.AppendLine($"---AlbumProducer: {song.AlbumProducer}");
                sb.AppendLine($"---Duration: {song.Duration}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}

