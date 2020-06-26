using System.Collections.Generic;

namespace MusicService.Domain
{
    /// <summary>
    /// Альбом.
    /// </summary>
    public class Album
    {
        private List<Track> tracks = new List<Track>();

        /// <summary>
        /// Трэки.
        /// </summary>
        public IReadOnlyCollection<Track> Tracks => this.tracks;

        /// <summary>
        /// Название.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Год издания.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Добавить трэк.
        /// </summary>
        /// <param name="track">Трэк.</param>
        public void AddTrack(Track track)
        {
            this.tracks.Add(track);
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="title">Название.</param>
        public Album(string title)
        {
            this.Title = title;
        }
    }
}