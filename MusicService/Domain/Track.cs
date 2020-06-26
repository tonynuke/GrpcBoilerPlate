namespace MusicService.Domain
{
    /// <summary>
    /// Музыкальное произведение.
    /// </summary>
    public class Track
    {
        /// <summary>
        /// Название.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Продолжительность в секундах.
        /// </summary>
        public int DurationInSec { get; set; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="title">Название.</param>
        /// <param name="durationInSec">Продолжительность в секундах.</param>
        public Track(string title, int durationInSec)
        {
            this.Title = title;
            this.DurationInSec = durationInSec;
        }
    }
}