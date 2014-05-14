using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protective.Core.Entity
{
    public class Message
    {
        public Message()
        {
            IsStarred = false;
            AddedDate = DateTime.Now;
        }

        /// <summary>
        /// The message's Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The message text
        /// </summary>
        [Required]
        [StringLength(100)]
        public string MessageText { get; set; }

        /// <summary>
        /// If the message is starred
        /// </summary>
        public bool IsStarred { get; set; }

        /// <summary>
        /// When the message was added
        /// </summary>
        public DateTime AddedDate { get; set; }
    }
}
