using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Moq;
using Music;
using MusicServiceClient;
using MusicServiceClient.Clients;
using NUnit.Framework;

namespace MusicClient.UnitTests
{
    [TestFixture]
    public class CacheDecoratorTests
    {
        private Album CreateAlbum()
        {
            var expectedArtist = "artist";
            var expectedTitle = "title";
            var expectedYear = 2020;

            return new Album { Artist = expectedArtist, Title = expectedTitle, Year = expectedYear };
        }

        [Test]
        public async Task When_ConnectionException_CacheContainsData_ClientGetDataFromCache()
        {
            var expectedAlbum = this.CreateAlbum();
            var expectedAlbums = new List<Album> { expectedAlbum };

            string dumpPath = "cache";
            var fileSystemStub = new MockFileSystem();
            var cache = new AlbumsCache(fileSystemStub, dumpPath);
            await cache.AddAsync(expectedAlbum.Artist, expectedAlbums);

            var clientMock = new Mock<IClient>();
            var connectionException = new RpcException(new Status(StatusCode.Internal, string.Empty));
            clientMock.Setup(client => client.GetAlbumsByArtistName(It.IsAny<string>()))
                .Throws(connectionException).Verifiable();

            var cachedClient = new CacheDecorator(clientMock.Object, cache);
            var actualAlbumsCollection = await cachedClient.GetAlbumsByArtistName(expectedAlbum.Artist);
            var actualAlbum = actualAlbumsCollection.First();

            Assert.AreEqual(expectedAlbum.Title, actualAlbum.Title);

            clientMock.VerifyAll();
        }
    }
}