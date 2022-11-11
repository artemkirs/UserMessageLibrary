using System.ComponentModel.DataAnnotations.Schema;

namespace UserMessageLibrary.Models
{
    public partial class Message : BaseEntity
    {
        /// <summary>
        /// ИД пользователя, отправившего сообщение
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// пользователя, получившего сообщение
        /// </summary>
        public Guid? ContactId { get; set; }

        /// <summary>
        /// дата и время отправки сообщения
        /// </summary>
        public DateTime? SendTime { get; set; }

        /// <summary>
        /// дата и время получения сообщения
        /// </summary>
        public DateTime? DeliveryTime { get; set; }

        /// <summary>
        /// текст сообщения
        /// </summary>
        public string Content { get; set; }

        [ForeignKey("ContactId")]
        public virtual User? Contact { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
    }
}
