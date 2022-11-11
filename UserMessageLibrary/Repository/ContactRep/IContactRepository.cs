using UserMessageLibrary.Models;

namespace UserMessageLibrary.Repository.ContactRep
{
    public interface IContactRepository : IRepository<Contact>
    {
        /// <summary>
        /// Обновить контакт
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        Task UpdateAsync(Contact contact);

        /// <summary>
        /// получение списка контактов пользователя
        /// </summary>
        /// <param name="id">Ид пользователя</param>
        /// <returns>Контакты пользователя</returns>
        List<Contact> GetAllByUserId(Guid id);

        /// <summary>
        /// поиск контакта по имени в списке контактов пользователя
        /// </summary>
        /// <param name="contactName">Имя контакта</param>
        /// <param name="userId">Ид пользователя</param>
        /// <returns>Контакт</returns>
        Task<Contact?> GetByContactNameAsync(string contactName, Guid userId);

        /// <summary>
        /// удаление контакта
        /// </summary>
        /// <param name="contact">Контакт</param>
        Task DeleteAsync(Contact contact);

        /// <summary>
        /// добавление нового контакта в список контактов пользователя
        /// </summary>
        /// <param name="contact">Контакт</param>
        Task AddAsync(Contact contact);

        /// <summary>
        /// получение контакта пользователя
        /// </summary>
        /// <param name="id">Ид контакта</param>
        /// <returns>Контакт</returns>
        Task<Contact?> GetByIdAsync(Guid id);

    }
}
