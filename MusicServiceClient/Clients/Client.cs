using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Music;
using MusicClient = Music.Music.MusicClient;

namespace MusicServiceClient.Clients
{
    /// <summary>
    /// Кдиент.
    /// </summary>
    public class Client : IClient
    {
        /// <summary>
        /// Клиент для вызова удаленных процедур.
        /// </summary>
        private readonly MusicClient rpcClient;

        public virtual async Task<IReadOnlyCollection<Album>> GetAlbumsByArtistName(string artistName)
        {
            var albumRequest = new AlbumRequest { ArtsitName = artistName };
            var response = await this.rpcClient.GetAlbumsCollectionAsync(albumRequest);
            return response.Albums;
        }

        /// <summary>
        /// Констурктор.
        /// </summary>
        /// <param name="client">Клиент для вызова удаленных процедур.</param>
        public Client(MusicClient client)
        {
            this.rpcClient = client ?? throw new ArgumentNullException(nameof(client));
        }
    }
}
