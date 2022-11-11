using UserMessageLibrary.Models;

namespace UserMessageLibrary
{
    public partial class User : BaseEntity
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// состояние пользователя: онлайн, офлайн
        /// </summary>
        public bool State { get; set; }
    }
}
