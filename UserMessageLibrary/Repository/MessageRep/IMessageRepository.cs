using UserMessageLibrary.Models;

namespace UserMessageLibrary.Repository.MessageRep
{
    public interface IMessageRepository : IRepository<Message>
    {
        /// <summary>
        /// поиск сообщения по строке в списке сообщений пользователя
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <param name="content">Строка для поиска</param>
        /// <returns>Сообщения</returns>
        List<Message> FindAllByUserAndContent(Guid userId, string content);

        /// <summary>
        /// получение списка сообщение пользователя
        /// </summary>
        /// <param name="userId"Ид пользователя></param>
        /// <returns>Сообщения</returns>
        List<Message> GetAllByUserId(Guid userId);

        /// <summary>
        /// добавление нового сообщения
        /// </summary>
        /// <param name="message">Сообщение</param>
        Task AddAsync(Message message);
    }
}
