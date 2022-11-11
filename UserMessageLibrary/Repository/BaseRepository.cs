using Microsoft.EntityFrameworkCore;
using UserMessageLibrary.Models;

namespace UserMessageLibrary.Repository
{
    /// <summary>
    /// Базовый репозиторий
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="dbContext">Контекст данных</param>
        public BaseRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = this._dbContext.Set<T>();
        }

        /// <summary>
        /// Сохранение изменний
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Добавить сущность
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <returns>Добавленнаясущность</returns>
        protected virtual async Task<T> AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Удалить сущность
        /// </summary>
        /// <param name="entity">сущность</param>
        /// <returns>реузльтат удаления</returns>
        protected virtual async Task<int> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);

            return await SaveChangesAsync();
        }

        /// <summary>
        /// Обновить сущность
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <returns>Обновленная сущность</returns>
        protected virtual async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Получить сущность по Ид
        /// </summary>
        /// <param name="id">Ид сущности</param>
        /// <returns>Сущность</returns>
        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(new object[] { id });
        }
    }
}
