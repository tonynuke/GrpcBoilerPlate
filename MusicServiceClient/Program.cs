using System;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using MusicServiceClient.Clients;
using MusicClient = Music.Music.MusicClient;

namespace MusicServiceClient
{
    public class Program
    {
        private static IServiceProvider CreateServiceProvider()
        {
            var serviceCollection = new ServiceCollection()
                .AddSingleton<AlbumsCache>(provider =>
                {
                    var fileSystem = new FileSystem();
                    string exeFileName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                    string cacheFileName = "cache";
                    string cacheFilePath = Path.Combine(fileSystem.FileInfo.FromFileName(exeFileName).DirectoryName, cacheFileName);
                    return new AlbumsCache(fileSystem, cacheFilePath);
                })
                .AddSingleton<IClient>(provider =>
                {
                    var channel = GrpcChannel.ForAddress("https://localhost:5001");
                    var rpcClient = new MusicClient(channel);
                    var client = new Client(rpcClient);
                    var cache = provider.GetService<AlbumsCache>();
                    return new CacheDecorator(client, cache);
                });

            return serviceCollection.BuildServiceProvider();
        }

        private static void Main(string[] args)
        {
            var serviceProvider = CreateServiceProvider();
            var client = serviceProvider.GetService<IClient>();

            string artistName = "Ab/Db";
            var albums = client.GetAlbumsByArtistName(artistName).Result;

            string albumName = albums.First().Title;
            Console.WriteLine(albumName);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
