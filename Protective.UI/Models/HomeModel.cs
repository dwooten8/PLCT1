using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Protective.Core.Entity;

namespace Protective.UI.Models
{
    public class HomeModel
    {
        public HomeModel()
        {
            MessageList = new List<Message>();
            Message = new Message();
            Error = string.Empty;
        }

        public List<Message> MessageList { get; set; }

        public Message Message { get; set; }

        public string Error { get; set; }
    }
}