using System.ComponentModel.DataAnnotations.Schema;

namespace UserMessageLibrary.Models
{
    /// <summary>
    /// Контакт пользователя
    /// </summary>
    public partial class Contact : BaseEntity
    {
        /// <summary>
        /// ИД пользователя, на который ссылается контакт
        /// </summary>
        public Guid? ContactId { get; set; }

        /// <summary>
        /// ИД пользователя, владельца контакта
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// дата и время последней беседы с контактом
        /// </summary>
        public DateTime LastUpdateTime { get; set; }


        [ForeignKey("ContactId")]
        public virtual User? ContactNavigation { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
    }
}
