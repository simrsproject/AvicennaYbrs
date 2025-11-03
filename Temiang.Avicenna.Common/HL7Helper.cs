using System.Linq;
//using HL7.Dotnetcore;
namespace Temiang.Avicenna.Common
{
    public class HL7Helper
    {
        public static string Execute(string message)
        {
            return string.Empty;
            //return NHapi.Model.V23.Helper.HL7Helper.GetParsingMessage(message);

        }

        public static string GetResultUrl(string strMessage)
        {
            //Message message = new Message(strMessage);
            //bool isParsed = message.ParseMessage();
            //if (!isParsed) return string.Empty;
            //Segment segList = message.Segments("OBX").Where(m => m.Fields(2).Value == "RP").SingleOrDefault();
            //if (segList == null) return string.Empty;
            //return segList.Fields(7).Value;

            return string.Empty;
        }
    }
}
