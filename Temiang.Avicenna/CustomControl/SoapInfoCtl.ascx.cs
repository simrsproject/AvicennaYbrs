using System;
using System.Data;
using System.Text;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.CustomControl
{
    public partial class SoapInfoCtl : System.Web.UI.UserControl
    {
        public void PopulateSoap(string registrationNo)
        {
            //if (!pnlOrderNote.Visible) return;
            if (string.IsNullOrWhiteSpace(registrationNo)) return; // Untuk yg tidak pakai RegistrationNo seperti OTC

            var strb = new StringBuilder();
            strb.AppendLine("<table width=\"100%\"><tr><td width=\"25%\" class=\"rgHeader\">Subjective</td><td width=\"25%\" class=\"rgHeader\">Objective</td><td width=\"25%\" class=\"rgHeader\">Assessmnet</td><td width=\"25%\" class=\"rgHeader\">Planning</td></tr></table>");
            strb.AppendLine("<div style=\"overflow: auto;width:100%; height: 140px;\"><table width=\"100%\">");

            var regNos = Registration.RelatedRegistrations(registrationNo);

            // From table EpisodeSOAPE
            var soapColl = new EpisodeSOAPECollection();
            soapColl.Query.Where(
                soapColl.Query.RegistrationNo.In(regNos),
                soapColl.Query.IsVoid == false,
                soapColl.Query.Or(soapColl.Query.Imported.IsNull(), soapColl.Query.Imported == false)
                );
            soapColl.LoadAll();

            var i = 0;
            foreach (var soap in soapColl)
            {
                if (!string.IsNullOrWhiteSpace(string.Concat(soap.Subjective, soap.Objective, soap.Assesment, soap.Planning)))
                {
                    i++;
                    var className = i % 2 == 0 ? "rgAltRow" : "rgRow";
                    strb.AppendLine("<tr>");
                    strb.AppendFormat("<td valign=\"top\" class=\"{3}\" width=\"25%\"><b>{0} {1}</b><br />{2}<br /></td>", soap.SOAPEDate.Value.ToString(AppConstant.DisplayFormat.DateShortMonth),soap.SOAPETime, soap.Subjective.Trim().Replace(System.Environment.NewLine, "<br />"), className);
                    strb.AppendFormat("<td valign=\"top\" class=\"{3}\" width=\"25%\"><b>{0} {1}</b><br />{2}</td>", soap.SOAPEDate.Value.ToString(AppConstant.DisplayFormat.DateShortMonth),soap.SOAPETime, soap.Objective.Trim().Replace(System.Environment.NewLine, "<br />"), className);
                    strb.AppendFormat("<td valign=\"top\" class=\"{3}\" width=\"25%\"><b>{0} {1}</b><br />{2}</td>", soap.SOAPEDate.Value.ToString(AppConstant.DisplayFormat.DateShortMonth),soap.SOAPETime, soap.Assesment.Trim().Replace(System.Environment.NewLine, "<br />"), className);
                    strb.AppendFormat("<td valign=\"top\" class=\"{3}\" width=\"25%\"><b>{0} {1}</b><br />{2}</td>", soap.SOAPEDate.Value.ToString(AppConstant.DisplayFormat.DateShortMonth),soap.SOAPETime, soap.Planning.Trim().Replace(System.Environment.NewLine, "<br />"), className);
                    strb.AppendLine("</tr>");
                }
            }
            // From table RegistrationInfoMedic
            var rimColl = new RegistrationInfoMedicCollection();
            rimColl.Query.Where(
                rimColl.Query.RegistrationNo.In(regNos),
                rimColl.Query.SRMedicalNotesInputType == "SOAP",
                rimColl.Query.Or(rimColl.Query.IsDeleted.IsNull(), rimColl.Query.IsDeleted == false)
                );
            rimColl.Query.OrderBy(rimColl.Query.DateTimeInfo.Descending);
            rimColl.LoadAll();


            foreach (var rim in rimColl)
            {
                if (!string.IsNullOrWhiteSpace(string.Concat(rim.Info1, rim.Info2, rim.Info3, rim.Info4)))
                {
                    i++;
                    var className = i % 2 == 0 ? "rgAltRow" : "rgRow";
                    strb.AppendLine("<tr>");
                    strb.AppendFormat("<td valign=\"top\" class=\"{2}\" width=\"25%\"><b>{0}</b><br />{1}<br /></td>", rim.DateTimeInfo.Value.ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute), rim.Info1.Trim().Replace(System.Environment.NewLine, "<br />"), className);
                    strb.AppendFormat("<td valign=\"top\" class=\"{2}\" width=\"25%\"><b>{0}</b><br />{1}</td>", rim.DateTimeInfo.Value.ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute), rim.Info2.Trim().Replace(System.Environment.NewLine, "<br />"), className);
                    strb.AppendFormat("<td valign=\"top\" class=\"{2}\" width=\"25%\"><b>{0}</b><br />{1}</td>", rim.DateTimeInfo.Value.ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute), rim.Info3.Trim().Replace(System.Environment.NewLine, "<br />"), className);
                    strb.AppendFormat("<td valign=\"top\" class=\"{2}\" width=\"25%\"><b>{0}</b><br />{1}</td>", rim.DateTimeInfo.Value.ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute), rim.Info4.Trim().Replace(System.Environment.NewLine, "<br />"), className);
                    strb.AppendLine("</tr>");
                }
            }
            strb.AppendLine("</table></div>");
            litSoap.Text = strb.ToString();
        }

    }
}