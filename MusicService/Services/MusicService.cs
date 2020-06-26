using System;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Music;

namespace MusicService.Services
{
    /// <summary>
    /// ����������� ������.
    /// </summary>
    public class MusicService : Music.Music.MusicBase
    {
        /// <summary>
        /// ��������� ������.
        /// </summary>
        private readonly MusicSearcher musicSearcher;

        /// <summary>
        /// �������� ������� �� �����������.
        /// </summary>
        /// <param name="request">������ ��������.</param>
        /// <param name="context">��������.</param>
        /// <returns>��������� ��������.</returns>
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
        /// �����������.
        /// </summary>
        /// <param name="musicSearcher">��������� ������.</param>
        public MusicService(MusicSearcher musicSearcher)
        {
            this.musicSearcher = musicSearcher ?? throw new ArgumentNullException(nameof(musicSearcher));
        }
    }
}
