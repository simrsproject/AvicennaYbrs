using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Emr.EmrCommon
{
    public partial class SwitchRegistration : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Footer.Visible = false;
            if (!IsPostBack)
            {
                Title = "Switch Registration";
                var reg = new RegistrationQuery("r");
                var su = new ServiceUnitQuery("su");
                reg.InnerJoin(su).On(reg.ServiceUnitID == su.ServiceUnitID);

                var par = new ParamedicQuery("par");
                reg.InnerJoin(par).On(reg.ParamedicID == par.ParamedicID);

                var patientIds = Patient.PatientRelateds(PatientID);
                if (patientIds.Count == 1)
                    reg.Where(reg.PatientID == PatientID);
                else
                    reg.Where(reg.PatientID.In(patientIds));

                reg.Where(reg.IsVoid == false, reg.IsFromDispensary == false, reg.IsDirectPrescriptionReturn == false, reg.IsNonPatient == false);
                reg.Select(reg.RegistrationNo, reg.RegistrationDateTime, reg.ParamedicID,
                    reg.Complaint, reg.SRRegistrationType, reg.LOSInYear, reg.LOSInMonth, reg.LOSInDay,
                    reg.FromRegistrationNo, reg.ServiceUnitID, reg.RoomID, reg.ParamedicID,
                    reg.PatientID, su.ServiceUnitName, par.ParamedicName);
                reg.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationNo.Descending);


                var userType = AppSession.UserLogin.SRUserType;
                var dtb = reg.LoadDataTable();
                dtb.Columns.Add("RegistrationDate", typeof(string));
                dtb.Columns.Add("Url", typeof(string));
                dtb.Columns.Add("Description", typeof(string));
                dtb.Columns.Add("SwitchCaption", typeof(string));
                foreach (DataRow row in dtb.Rows)
                {
                    row["LOSInDay"] = (row["LOSInYear"].ToInt() * 365) + (row["LOSInMonth"].ToInt() * 30) + row["LOSInDay"].ToInt(); // LOSInDay yg dipakai u/ tampilan 
                    row["RegistrationDate"] = ((DateTime)row["RegistrationDateTime"]).ToString(AppConstant.DisplayFormat.DateShortMonth);
                    if (!AppConstant.RegistrationType.InPatient.Equals(row["SRRegistrationType"]))
                    {
                        // First SOAP
                        var rim = new RegistrationInfoMedic();
                        rim.Query.es.Top = 1;
                        rim.Query.OrderBy(rim.Query.RegistrationInfoMedicID.Descending);
                        rim.Query.Where(rim.Query.RegistrationNo == row["RegistrationNo"], rim.Query.SRMedicalNotesInputType == "SOAP");
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

                            row["Description"] = sbNote.ToString();
                        }
                        else
                            row["Description"] = string.Empty;
                    }
                    else
                    {
                        row["Description"] = string.Format("<b>Chief Complaint:</b><br/>{0}<br/>{1}", row["Complaint"], EpisodeDiagnose.DiagnoseSummaryHtml(row["RegistrationNo"].ToString()));
                    }


                    // TODO: Check access right dilakukan di EMR Detail
                    //var isAccessRight = true;
                    //if (userType == AppUser.UserType.Nurse)
                    //    isAccessRight = ServiceUnitIdRights.Contains(String.Format("{0}|", row["ServiceUnitID"]));

                    //if (isAccessRight)
                    //{
                    row["SwitchCaption"] = "Switch";

                    if (AppConstant.RegistrationType.InPatient.Equals(row["SRRegistrationType"]))
                        row["Url"] = string.Format("{0}/Module/RADT/EmrIp/EmrIpDetail.aspx?rt={1}&regno={2}&fregno={3}&parid={4}&unit={5}&room={6}&patid={7}",
                            Helper.UrlRoot(),
                            row["SRRegistrationType"],
                            row["RegistrationNo"],
                            row["FromRegistrationNo"],
                            row["ParamedicID"],
                            row["ServiceUnitID"],
                            row["RoomID"],
                            row["PatientID"]
                            );
                    else
                        row["Url"] = string.Format("{0}/Module/RADT/Cpoe/EmrDetail.aspx?rt={1}&regno={2}&fregno={3}&parid={4}&unit={5}&room={6}&patid={7}",
                            Helper.UrlRoot(),
                            row["SRRegistrationType"],
                            row["RegistrationNo"],
                            row["FromRegistrationNo"],
                            row["ParamedicID"],
                            row["ServiceUnitID"],
                            row["RoomID"],
                            row["PatientID"]
                            );
                    //}
                    //else
                    //{
                    //    row["SwitchCaption"] = string.Empty;
                    //    row["Url"] = string.Empty;
                    //}


                }

                timelineReg.DataSource = dtb;
                timelineReg.DataBind();
            }
        }

        private string _serviceUnitIdRights = null;
        private string ServiceUnitIdRights
        {
            get
            {
                if (_serviceUnitIdRights == null)
                {
                    var coll = new AppUserServiceUnitCollection();
                    coll.Query.Where(coll.Query.UserID == AppSession.UserLogin.UserID);
                    coll.LoadAll();
                    _serviceUnitIdRights = String.Empty;
                    foreach (var su in coll)
                    {
                        _serviceUnitIdRights = String.Concat(_serviceUnitIdRights, su.ServiceUnitID, "|");
                    }
                }
                return _serviceUnitIdRights;
            }
        }
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            //if (grdReport.SelectedValue != null)
            //{
            //    return "oWnd.argument.print = '" + Page.Request.QueryString["regno"] + "|" + grdReport.SelectedValue +
            //           "'";

            //}
            return string.Empty;
        }
        public override bool OnButtonOkClicked()
        {

            return false;
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
