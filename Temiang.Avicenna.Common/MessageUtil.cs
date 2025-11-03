using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;

namespace Temiang.Avicenna.Common
{
    public class MessageUtil
    {
        public class Message
        {
            public string MessageTextOriginal { get; set; }
            public string MessageText { get; set; }
            public bool IsError { get; set; }
        }
        public static string GetMessageText(AppMessage.MessageIdEnum messageID)
        {
            var cacheID = "msg:" + messageID;
            var cache = HttpContext.Current.Session[cacheID];
            if (cache == null)
            {
                var msg = AppMessage.GetMessageText(messageID);
                HttpContext.Current.Session[cacheID] = msg;
                return msg;
            }
            return (string)cache;
        }

        public static Message GetMessage(MessageException msgEx)
        {
            var message = new Message { IsError = true };
            var cacheID = "msgEx:" + msgEx.MessageID;
            var messageSession = HttpContext.Current.Session[cacheID];
            if (messageSession != null)
            {
                var messageSessionUnbox = (Message)messageSession;
                message.MessageTextOriginal = messageSessionUnbox.MessageTextOriginal;
                message.MessageText = messageSessionUnbox.MessageText;
                message.IsError = messageSessionUnbox.IsError;
            }
            else
            {
                var entity = AppMessage.LoadByPrimaryKeyAndAddIfNotExist(msgEx.MessageID);
                message.MessageTextOriginal = string.IsNullOrEmpty(entity.MessageTextCustom)
                                          ? entity.MessageText
                                          : entity.MessageTextCustom;


                message.IsError = entity.IsError??false;
                //Add Session
                HttpContext.Current.Session[cacheID] = message;
            }

            if (string.IsNullOrEmpty(message.MessageTextOriginal))
                message.MessageText = GetMessageText(AppMessage.MessageIdEnum.CommonMsg);
            else
                if (msgEx.Arguments != null)
                    message.MessageText = string.Format(message.MessageTextOriginal, msgEx.Arguments);
                else
                    message.MessageText = message.MessageTextOriginal;

            return message;
        }

        public static Message GetMessage(SqlException sqlEx)
        {
            var message = new Message { IsError = true };
            var cacheID = "msgEx:" + sqlEx.Number;
            var messageSession = HttpContext.Current.Session[cacheID];
            if (messageSession != null)
            {
                var messageSessionUnbox = (Message)messageSession;
                message.MessageText = messageSessionUnbox.MessageText;
                message.IsError = messageSessionUnbox.IsError;
            }
            else
            {
                var entity = new AppMessage();
                switch (sqlEx.Number)
                {
                    case 2601: // Duplicate Key
                    case 2627: // Duplicate Primary Key
                        entity =
                            AppMessage.LoadByPrimaryKeyAndAddIfNotExist(AppMessage.MessageIdEnum.DuplicatePrimaryKey);
                        message.MessageText = string.IsNullOrEmpty(entity.MessageTextCustom) ? entity.MessageText : entity.MessageTextCustom;
                        message.IsError = entity.IsError??false;
                        //Add Session
                        HttpContext.Current.Session[cacheID] = message;
                        break;
                    case 547: // The DELETE statement conflicted with the REFERENCE ...
                        entity =
                            AppMessage.LoadByPrimaryKeyAndAddIfNotExist(AppMessage.MessageIdEnum.DeleteConflicted);
                        message.MessageText = string.IsNullOrEmpty(entity.MessageTextCustom) ? entity.MessageText : entity.MessageTextCustom;
                        message.IsError = entity.IsError??false;
                        //Add Session
                        HttpContext.Current.Session[cacheID] = message;
                        break;
                    case 50000: //Message From Triger
                        message.MessageText = sqlEx.Message;
                        //message.MessageType = "A";
                        message.IsError = false;
                        break;
                    default:
                        var messageID = sqlEx.Number.ToString();
                        if (entity.LoadByPrimaryKey(messageID))
                        {
                            if (!String.IsNullOrEmpty(entity.MessageTextCustom))
                            {
                                message.MessageText = entity.MessageTextCustom;
                                message.IsError = entity.IsError??false;
                                //Add Session
                                HttpContext.Current.Session[cacheID] = message;
                            }
                        }
                        else
                        {
                            //insert to AppMessage
                            entity = new AppMessage
                                         {
                                             MessageID = sqlEx.Number.ToString(),
                                             MessageText = sqlEx.Message,
                                             IsError = true
                                         };
                            entity.Save();
                        }
                        break;
                }
            }


            if (string.IsNullOrEmpty(message.MessageText))
                message.MessageText = GetMessageText(AppMessage.MessageIdEnum.CommonMsg);

            return message;
        }
    }
}
