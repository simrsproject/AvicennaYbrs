using System;
using System.Text;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Text.RegularExpressions;

namespace Temiang.Avicenna.Module.RADT.Ppra.Common
{
    /// <summary>
    /// Layar untuk keperluan perawat melihat status resep yg sudah complete tetapi belum diambil
    /// Dipanggil dari layar EMR List
    /// </summary>
    public partial class GyssensCommonInfo : BasePageDialog
    {

        private int SeqNo => Request.QueryString["seqno"].ToInt();
        private string InfoType => Request.QueryString["infotype"];

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ButtonOk.Visible = false;
            ButtonCancel.Text = "Close";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                switch (InfoType)
                {
                    case "absuggest":
                        Title = "Antibiotic Suggestion";
                        litInfo.Text = AbSuggestion();
                        break;
                    case "soap":
                        Title = "SOAP";
                        litInfo.Text = Soap();
                        break;
                    default:
                        break;
                }

            }
        }
        private string AbSuggestion()
        {
            var gys = new RegistrationGyssens();
            if (gys.LoadByPrimaryKey(RegistrationNo, SeqNo))
            {
                if (gys.RasproSeqNo > 0)
                {
                    var userasproSeqNo = 0;
                    var rr = new RegistrationRaspro();
                    rr.LoadByPrimaryKey(RegistrationNo, gys.RasproSeqNo??0);
                    return AbRestriction.AntibioticSuggestion(rr,ref userasproSeqNo);
                }
            }
            return "";
        }

        private string Soap()
        {
            // First SOAP
            var rim = new RegistrationInfoMedic();
            rim.Query.es.Top = 1;
            rim.Query.OrderBy(rim.Query.RegistrationInfoMedicID.Descending);
            rim.Query.Where(rim.Query.RegistrationNo == RegistrationNo, rim.Query.SRMedicalNotesInputType == "SOAP");
            if (rim.Query.Load())
            {
                var sbNote = new StringBuilder();
                var info1 = ReplaceWitBreakLineHTML(rim.Info1);
                sbNote.AppendLine("<table style=\"width:100%\">");
                sbNote.AppendFormat(
                    "<tr><td class='label' valign='top' style='font-weight: bold;width:{2}px; padding-left:2px'>{0}:</td><td valign='top'>{1}</td></tr>",
                    "S", info1, 10);
                sbNote.AppendLine();

                var info2 = ReplaceWitBreakLineHTML(rim.Info2);
                sbNote.AppendFormat(
                    "<tr><td class='label' valign='top' style='font-weight: bold;width:{2}px; padding-left:2px'>{0}:</td><td valign='top'>{1}</td></tr>",
                    "O", info2, 10);
                sbNote.AppendLine();

                var info3 = ReplaceWitBreakLineHTML(rim.Info3);
                sbNote.AppendFormat(
                    "<tr><td class='label' valign='top' style='font-weight: bold;width:{2}px; padding-left:2px'>{0}:</td><td valign='top'>{1}</td></tr>",
                    "A", info3, 10);
                sbNote.AppendLine();

                // Planning
                string planning;
                // Dari asesmen tambah hist resepnya di Planning
                if (!string.IsNullOrEmpty(rim.PrescriptionCurrentDay))
                    planning = string.Format("{0}<br/><br/>{1}", FormatToHtml(rim.Info4),
                        FormatToHtml(rim.PrescriptionCurrentDay));
                else
                    planning = FormatToHtml(rim.Info4);

                sbNote.AppendFormat(
                    "<tr><td class='label' valign='top' style='font-weight: bold; width:{2}px;padding-left:2px'>{0}:</td><td valign='top'>{1}</td></tr>",
                    "P", planning, 10);
                sbNote.AppendLine("</table>");
                return sbNote.ToString();
            }
            return "SOAP Not Found";
        }

        private static string FormatToHtml(object value)
        {
            return Regex.Replace(value == null || value == DBNull.Value ? String.Empty : value.ToString(), @"\r\n?|\n", "<br />");
        }

        private static string ReplaceWitBreakLineHTML(string text)
        {
            return Regex.Replace(text, @"\r\n?|\n", "<br />");
        }
    }
}