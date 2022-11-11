namespace UserMessageLibrary.Repository.UserRep
{
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// получения пользователя по ИД
        /// </summary>
        /// <param name="id">Ид пользователя</param>
        /// <returns>Пользователь</returns>
        Task<User?> GetByIdAsync(Guid id);

        /// <summary>
        /// получение пользователя по имени
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        /// <returns></returns>
        Task<User?> GetByNameAsync(string name);

        /// <summary>
        /// поиск пользователей по имени
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        /// <returns>Пользователи</returns>
        List<User> FindByName(string name);

        /// <summary>
        /// добавление нового пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>ИД пользователя</returns>
        Task<Guid> AddAsync(User user);

        /// <summary>
        /// обновление данных пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        Task UpdateAsync(User user);
    }
}
