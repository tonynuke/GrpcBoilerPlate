using System;
using Shared;

namespace MusicService.Domain
{
    public class ArtistNameSpecification : Specification<Artist>
    {
        private readonly string artistName;

        protected override Func<Artist, bool> condition()
        {
            return artist => artist.Name.ToLowerInvariant().Contains(this.artistName.ToLowerInvariant());
        }

        public ArtistNameSpecification(string artistName)
        {
            this.artistName = artistName ?? throw new ArgumentNullException(nameof(artistName));
        }
    }
}