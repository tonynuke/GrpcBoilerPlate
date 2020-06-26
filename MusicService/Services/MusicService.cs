using System;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Music;

namespace MusicService.Services
{
    /// <summary>
    /// Музыкальный сервис.
    /// </summary>
    public class MusicService : Music.Music.MusicBase
    {
        /// <summary>
        /// Поисковик музыки.
        /// </summary>
        private readonly MusicSearcher musicSearcher;

        /// <summary>
        /// Получить альбомы по исполнителю.
        /// </summary>
        /// <param name="request">Запрос альбомов.</param>
        /// <param name="context">Контекст.</param>
        /// <returns>Коллекция альбомов.</returns>
        public override Task<AlbumCollectionResponse> GetAlbumsCollection(AlbumRequest request, ServerCallContext context)
        {
            var matchedArtists = this.musicSearcher.FindArtistByName(request.ArtsitName);

            var response = new AlbumCollectionResponse();

            foreach (var artist in matchedArtists)
            {
                foreach (var album in artist.Albums)
                {
                    var tracksDTO = album.Tracks.Select(
                        song => new Track { Title = song.Title, DurationInSec = song.DurationInSec });

                    var albumDTO = new Album { Artist = artist.Name, Title = album.Title, Year = album.Year };
                    albumDTO.Tracks.Add(tracksDTO);

                    response.Albums.Add(albumDTO);
                }
            }

            return Task.FromResult(response);
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="musicSearcher">Поисковик музыки.</param>
        public MusicService(MusicSearcher musicSearcher)
        {
            this.musicSearcher = musicSearcher ?? throw new ArgumentNullException(nameof(musicSearcher));
        }
    }
}
