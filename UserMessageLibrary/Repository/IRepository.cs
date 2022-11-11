using UserMessageLibrary.Models;

namespace UserMessageLibrary.Repository
{
    /// <summary>
    /// Базовый интерфейс репозитория
    /// </summary>
    /// <typeparam name="T">Сущность</typeparam>
    public interface IRepository <T> where T : BaseEntity
    {
    }
}
