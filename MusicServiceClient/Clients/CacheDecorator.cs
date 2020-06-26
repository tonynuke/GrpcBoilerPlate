using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Music;

namespace MusicServiceClient.Clients
{
    /// <summary>
    /// Декоратор кэша.
    /// </summary>
    public class CacheDecorator : ClientDecorator
    {
        /// <summary>
        /// Кэш.
        /// </summary>
        private readonly AlbumsCache albumsCache;

        public override async Task<IReadOnlyCollection<Album>> GetAlbumsByArtistName(string artistName)
        {
            try
            {
                var albums = await this.client.GetAlbumsByArtistName(artistName);
                await this.albumsCache.AddAsync(artistName, albums);
                return albums;
            }
            catch (RpcException exception) when (exception.StatusCode == StatusCode.Internal)
            {
                if (!this.albumsCache.Contains(artistName))
                {
                    await this.albumsCache.AddAsync(artistName, new List<Album>());
                }

                return this.albumsCache.Get(artistName);
            }
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="client">Клиент.</param>
        /// <param name="albumsCache">Кэш.</param>
        public CacheDecorator(IClient client, AlbumsCache albumsCache) : base(client)
        {
            this.albumsCache = albumsCache ?? throw new ArgumentNullException(nameof(albumsCache));
        }
    }
}