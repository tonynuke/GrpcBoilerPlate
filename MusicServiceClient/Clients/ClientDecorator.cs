using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Music;

namespace MusicServiceClient.Clients
{
    /// <summary>
    /// Декоратор клиента.
    /// </summary>
    public abstract class ClientDecorator : IClient
    {
        protected readonly IClient client;

        public virtual Task<IReadOnlyCollection<Album>> GetAlbumsByArtistName(string artistName)
        {
            return this.client.GetAlbumsByArtistName(artistName);
        }

        protected ClientDecorator(IClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }
    }
}