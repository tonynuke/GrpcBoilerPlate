using System;
using System.Collections.Generic;
using System.Linq;
using MusicService.Domain;

namespace MusicService.Services
{
    /// <summary>
    /// Поисковик музыки.
    /// </summary>
    public class MusicSearcher
    {
        /// <summary>
        /// Репозиторий исполнителей.
        /// </summary>
        private readonly FakeArtistRepository artistsRepository;

        /// <summary>
        /// Найти исполнителя по имени.
        /// </summary>
        /// <param name="artistName">Название исполнителя.</param>
        /// <returns>Исполнитель.</returns>
        public IReadOnlyCollection<Artist> FindArtistByName(string artistName)
        {
            var artistNameSpecification = new ArtistNameSpecification(artistName);
            return this.artistsRepository.Find(artistNameSpecification);
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="artistsRepository">Репозиторий исполнителей.</param>
        public MusicSearcher(FakeArtistRepository artistsRepository)
        {
            this.artistsRepository = artistsRepository ?? throw new ArgumentNullException(nameof(artistsRepository));
        }
    }
}
