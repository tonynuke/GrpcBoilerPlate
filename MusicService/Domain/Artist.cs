using System.Collections.Generic;

namespace MusicService.Domain
{
    /// <summary>
    /// Исполнитель.
    /// </summary>
    public class Artist
    {
        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }

        private List<Album> albums = new List<Album>();

        /// <summary>
        /// Альбомы.
        /// </summary>
        public IReadOnlyCollection<Album> Albums => this.albums;

        /// <summary>
        /// Добавить альбом.
        /// </summary>
        /// <param name="album">Альбом.</param>
        public void AddAlbum(Album album)
        {
            this.albums.Add(album);
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Название.</param>
        public Artist(string name)
        {
            this.Name = name;
        }
    }
}
