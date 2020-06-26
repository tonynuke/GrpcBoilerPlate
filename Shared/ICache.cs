using System.Threading.Tasks;

namespace Shared
{
    /// <summary>
    /// Интерфейс кэша.
    /// </summary>
    /// <typeparam name="Tkey">Ключ.</typeparam>
    /// <typeparam name="TValue">Значение.</typeparam>
    public interface ICache <Tkey, TValue>
    {
        /// <summary>
        /// Добавить значение по ключу.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <param name="value">Значение.</param>
        /// <returns>Задача.</returns>
        Task AddAsync(Tkey key, TValue value);

        /// <summary>
        /// Получить значение по ключу.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <returns>Значение.</returns>
        TValue Get(Tkey key);

        /// <summary>
        /// Определить есть ли заданный ключ.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <returns>True, если ключ содержится.</returns>
        bool Contains(Tkey key);
    }
}