using System.Collections.Generic;
using System.Threading.Tasks;
using Music;

namespace MusicServiceClient.Clients
{
    /// <summary>
    /// Интерфейс клиента.
    /// </summary>
    public interface IClient
    {
        /// <summary>
        /// Получить альбомы по названию исполнителя.
        /// </summary>
        /// <param name="artistName">Название исполнителя</param>
        /// <returns>Альбомы.</returns>
        Task<IReadOnlyCollection<Album>> GetAlbumsByArtistName(string artistName);
    }
}