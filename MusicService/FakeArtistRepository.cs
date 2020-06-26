using MusicService.Domain;
using Shared;

namespace MusicService
{
    /// <summary>
    /// Заглушка репозитория исполнителей.
    /// </summary>
    public sealed class FakeArtistRepository : Repository<Artist>
    {
        private static Artist AbDd
        {
            get
            {
                var album = new Album("Low Voltage") { Year = 1985 };
                album.AddTrack(new Track("Baby, Please Go", 292));
                album.AddTrack(new Track("She's Got nothing", 292));
                album.AddTrack(new Track("Big Lover", 339));

                var artist = new Artist("Ab/Db");
                artist.AddAlbum(album);

                return artist;
            }
        }

        private static Artist WhiteSabbath
        {
            get
            {
                var album = new Album("White Sabbath") { Year = 1980, };
                album.AddTrack(new Track("White Sabbath", 376));
                album.AddTrack(new Track("The Witcher", 264));

                var artist = new Artist("White Sabbath");
                artist.AddAlbum(album);

                return artist;
            }
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public FakeArtistRepository()
        {
            this.Add(AbDd);
            this.Add(WhiteSabbath);
        }
    }
}
