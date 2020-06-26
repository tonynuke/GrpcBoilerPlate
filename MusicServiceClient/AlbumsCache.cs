using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Text.Json;
using System.Threading.Tasks;
using Music;
using Shared;

namespace MusicServiceClient
{
    /// <summary>
    /// Кэш альбомов.
    /// </summary>
    public class AlbumsCache
    {
        /// <summary>
        /// Файловая система.
        /// </summary>
        private readonly IFileSystem fileSystem;

        /// <summary>
        /// Путь хранения кэша.
        /// </summary>
        private readonly string cacheDumpPath;

        /// <summary>
        /// Кэш.
        /// </summary>
        private Dictionary<string, IReadOnlyCollection<Album>> cache = new Dictionary<string, IReadOnlyCollection<Album>>();

        /// <summary>
        /// Добавить альбомы исполнителя.
        /// </summary>
        /// <param name="artistName">Исполнитель.</param>
        /// <param name="albums">Альбомы.</param>
        /// <returns>Задача.</returns>
        public Task AddAsync(string artistName, IReadOnlyCollection<Album> albums)
        {
            this.cache[artistName] = albums;
            return this.SaveCacheAsync();
        }

        /// <summary>
        /// Получить альбомы исполнителя.
        /// </summary>
        /// <param name="artistName">Исполнитель.</param>
        /// <returns>альбомы исполнителя.</returns>

        public IReadOnlyCollection<Album> Get(string artistName)
        {
            return this.cache[artistName];
        }

        /// <summary>
        /// Определить есть ли заданный исполнитель в кэше.
        /// </summary>
        /// <param name="artistName">Исполнитель..</param>
        /// <returns>True, если исполнитель содержится в кэше.</returns>

        public bool Contains(string artistName)
        {
            return this.cache.ContainsKey(artistName);
        }

        /// <summary>
        /// Сохранить кэш.
        /// </summary>
        /// <returns>Задача.</returns>
        private Task SaveCacheAsync()
        {
            var cacheDump = JsonSerializer.Serialize(this.cache);
            return this.fileSystem.File.WriteAllTextAsync(this.cacheDumpPath, cacheDump);
        }

        /// <summary>
        /// Восстановить кэш.
        /// </summary>
        /// <returns>Задача.</returns>
        public async Task Restore()
        {
            var cacheDump = await this.fileSystem.File.ReadAllTextAsync(this.cacheDumpPath);
            this.cache = JsonSerializer.Deserialize<Dictionary<string, IReadOnlyCollection<Album>>>(cacheDump);
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="fileSystem">Файловая система.</param>
        /// <param name="dumpPath">Путь хранения кэша.</param>
        public AlbumsCache(IFileSystem fileSystem, string dumpPath)
        {
            this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
            this.cacheDumpPath = dumpPath ?? throw new ArgumentNullException(nameof(dumpPath));
        }
    }
}