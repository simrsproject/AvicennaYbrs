using System;
using System.Data;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ErrorMessage
    {
        public static void CreateSave(Exception ex, string UserID) {
            var msg = new ErrorMessage();
            msg.AddNew();

            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += Environment.NewLine;
            message += string.Format("Message: {0}", ex.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", ex.StackTrace ?? "");
            message += Environment.NewLine;
            message += string.Format("Source: {0}", ex.Source ?? "");
            message += Environment.NewLine;
            if (ex.TargetSite != null)
            {
                message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
                message += Environment.NewLine;
            }
            message += Environment.NewLine;

            msg.Message = message;
            msg.CreatedBy = UserID;
            msg.CreatedDateTime = DateTime.Now;
            msg.Save();
        }
    }
}
