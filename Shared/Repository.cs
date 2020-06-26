using System.Collections.Generic;
using System.Linq;

namespace Shared
{
    /// <summary>
    /// Репозиторий.
    /// </summary>
    /// <typeparam name="T">Элемент.</typeparam>
    public abstract class Repository<T>
    {
        /// <summary>
        /// Элементы.
        /// </summary>
        private readonly List<T> items = new List<T>();

        /// <summary>
        /// Добавить.
        /// </summary>
        /// <param name="item">Элемент.</param>
        public void Add(T item)
        {
            this.items.Add(item);
        }

        /// <summary>
        /// Найти элементы, удовлетворяющие спецификации.
        /// </summary>
        /// <param name="specification">Спецификация.</param>
        /// <returns>Элементы, удовлетворяющие спецификации.</returns>
        public IReadOnlyList<T> Find(Specification<T> specification)
        {
            return this.items.Where(specification.IsSatisfiedBy).ToList();
        }

        /// <summary>
        /// Получить все элементы.
        /// </summary>
        /// <returns>Элементы.</returns>
        public IReadOnlyList<T> GetAll()
        {
            return this.items;
        }
    }
}
