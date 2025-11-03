using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.Common
{
    public class MessageException : Exception
    {
        public AppMessage.MessageIdEnum MessageID { get; set; }
        public string[] Arguments { get; set; }
        public string MessageText { get; set; }
        public string MessageType { get; set; }

        public MessageException(AppMessage.MessageIdEnum messageId):base(messageId.ToString())
        {
            MessageID = messageId;
        }
        public MessageException(AppMessage.MessageIdEnum messageId, params string[] argument)
            : base(messageId.ToString())
        {
            MessageID = messageId;
            Arguments = argument;
        }

    }
}
