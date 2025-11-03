using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.DynamicQuery;


namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class Prmrj : BasePageDialog
    {
        public string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        public string FromRegistrationNo
        {
            get
            {
                return Request.QueryString["fregno"];
            }
        }
        public string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicHealthRecord;

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "PRMRJ & Medical Discharge Summary of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }
            if (string.IsNullOrEmpty(lblChronicDisease.Text))
                lblChronicDisease.Text = Patient.ChronicDisease(PatientID);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonCancel.Text = "Close";
            ButtonOk.Visible = false;
        }

        protected void grdRegistrationInfoMedic_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRegistrationInfoMedic.DataSource = RegistrationInfoMedicDataTable();
        }

        private DataTable PRMRJ()
        {
            var que = new RegistrationInfoMedicQuery("a");
            var reg = new RegistrationQuery("x");
            que.InnerJoin(reg).On(que.RegistrationNo == reg.RegistrationNo);

            var su = new ServiceUnitQuery("su");
            que.InnerJoin(su).On(reg.ServiceUnitID == su.ServiceUnitID);

            var fu = new PrmrjFollowUpQuery("fu");
            que.LeftJoin(fu).On(que.RegistrationInfoMedicID == fu.RegistrationInfoMedicID);

            var asses = new PatientAssessmentQuery("asses");
            que.LeftJoin(asses).On(que.RegistrationInfoMedicID == asses.RegistrationInfoMedicID);

            que.Select(que, su.ServiceUnitName, asses.SRAssessmentType, fu.ImportantClinicalNotes, fu.Planning, fu.Remark,
                asses.IsInitialAssessment, "<CONVERT(BIT,CASE WHEN asses.RegistrationInfoMedicID IS NOT NULL AND asses.IsInitialAssessment=1 THEN 1 ELSE 0 END) IsNewPatient>");

            que.Where(reg.PatientID == PatientID, que.IsPRMRJ == true);
            que.OrderBy(que.RegistrationInfoMedicID.Descending);

            return que.LoadDataTable();
        }
        private DataTable RegistrationInfoMedicDataTable()
        {
            var dtbRim = new DataTable();
            dtbRim = PRMRJ();

            var dtb = new DataTable();
            dtb.Columns.Add(new DataColumn("RegistrationInfoMedicID", typeof(string)));
            dtb.Columns.Add(new DataColumn("SRMedicalNotesInputType", typeof(string)));
            dtb.Columns.Add(new DataColumn("RegInfo", typeof(string)));
            dtb.Columns.Add(new DataColumn("Notes", typeof(string)));
            dtb.Columns.Add(new DataColumn("FollowUp", typeof(string)));
            dtb.Columns.Add(new DataColumn("VitalSigns", typeof(string)));
            dtb.Columns.Add(new DataColumn("DateTimeInfo", typeof(DateTime)));
            dtb.Columns.Add(new DataColumn("CreatedByUserID", typeof(string)));
            dtb.Columns.Add(new DataColumn("IsDeleted", typeof(bool)));
            dtb.Columns.Add(new DataColumn("RegistrationNo", typeof(string)));
            dtb.Columns.Add(new DataColumn("IsNewPatient", typeof(bool)));
            dtb.Columns.Add(new DataColumn("ServiceUnitID", typeof(string)));
            dtb.Columns.Add(new DataColumn("SRAssessmentType", typeof(string)));
            dtb.Columns.Add(new DataColumn("IsInitialAssessment", typeof(bool)));
            dtb.Columns.Add(new DataColumn("ServiceUnitName", typeof(string)));

            var dv = dtbRim.DefaultView;
            dv.Sort = "DateTimeInfo DESC";
            var sortedDt = dv.ToTable();

            foreach (DataRow rim in sortedDt.Rows)
            {
                var newRow = dtb.NewRow();
                newRow["RegistrationInfoMedicID"] = rim["RegistrationInfoMedicID"];
                newRow["SRMedicalNotesInputType"] = rim["SRMedicalNotesInputType"];
                newRow["DateTimeInfo"] = rim["DateTimeInfo"];
                newRow["CreatedByUserID"] = rim["CreatedByUserID"];
                newRow["RegistrationNo"] = rim["RegistrationNo"];
                newRow["IsDeleted"] = rim["IsDeleted"] == DBNull.Value ? false : rim["IsDeleted"];
                newRow["IsNewPatient"] = rim["IsNewPatient"];
                newRow["ServiceUnitID"] = rim["ServiceUnitID"];
                newRow["SRAssessmentType"] = rim["SRAssessmentType"];
                newRow["IsInitialAssessment"] = rim["IsInitialAssessment"];
                newRow["ServiceUnitName"] = rim["ServiceUnitName"];

                var paramedicName = string.Empty;
                if (rim["ParamedicID"] != DBNull.Value && !string.IsNullOrEmpty(rim["ParamedicID"].ToString()))
                {
                    paramedicName = GetParamedicName(rim["ParamedicID"].ToString(), "");
                }
                else if (rim["CreatedByUserID"] != DBNull.Value && !string.IsNullOrEmpty(rim["CreatedByUserID"].ToString())) // Ambil dari Created User
                {
                    var user = new AppUser();
                    if (user.LoadByPrimaryKey(rim["CreatedByUserID"].ToString()))
                    {
                        paramedicName = user.UserName;
                        if (!string.IsNullOrEmpty(user.ParamedicID))
                        {
                            paramedicName = GetParamedicName(user.ParamedicID, user.UserName);
                        }
                    }
                }

                var strBuilder = new StringBuilder();
                if (Convert.ToBoolean(rim["IsDeleted"] == DBNull.Value ? false : rim["IsDeleted"]) == true) strBuilder.AppendLine("<div style='text-decoration:line-through;'>");

                strBuilder.AppendLine("<table width='100%'>");
                AddHtmlTableRow(rim["RegistrationNo"], strBuilder, "Reg No", 50);
                AddHtmlTableRow(rim["ServiceUnitName"], strBuilder, "S.Unit", 50);
                AddHtmlTableRow(string.Format("{0} {1}",
                    Convert.ToDateTime(rim["DateTimeInfo"]).ToString(AppConstant.DisplayFormat.Date),
                    Convert.ToDateTime(rim["DateTimeInfo"]).ToShortTimeString()), strBuilder, "Date", 50);
                AddHtmlTableRow(paramedicName, strBuilder, "Phys", 70);


                strBuilder.AppendLine("</table>");

                newRow["RegInfo"] = strBuilder.ToString();


                #region Notes
                strBuilder = new StringBuilder();
                strBuilder.AppendLine("<table width='100%'>");
                if (rim["SRMedicalNotesInputType"].ToString() == "SBAR" || rim["SRMedicalNotesInputType"].ToString() == "SOAP")
                {
                    AddHtmlTableRow(rim["Info1"], strBuilder, rim["SRMedicalNotesInputType"].ToString().Substring(0, 1), 10);
                    AddHtmlTableRow(rim["Info2"], strBuilder, rim["SRMedicalNotesInputType"].ToString().Substring(1, 1), 10);
                    AddHtmlTableRow(rim["Info3"], strBuilder, rim["SRMedicalNotesInputType"].ToString().Substring(2, 1), 10);
                    AddHtmlTableRow(rim["Info4"], strBuilder, rim["SRMedicalNotesInputType"].ToString().Substring(3, 1), 10);
                }
                else if (rim["SRMedicalNotesInputType"].ToString() == "MDS")
                {
                    var mds = new MedicalDischargeSummary();
                    mds.LoadByPrimaryKey(rim["RegistrationNo"].ToString());
                    var isRichTextMode = mds.IsRichTextMode ?? false;
                    var info1 = rim["Info1"].ToString();
                    strBuilder.AppendFormat("<tr><td colspan='2' style='padding-left:2px;'><a href=\"#\" onclick=\"showResumeMedis('{0}','{1}',{4}); return false;\"><img src=\"{3}/Images/Toolbar/views16.png\" border=\"0\" alt=\"\" title=\"Resume Medis\" />&nbsp;&nbsp;{2}</a></td></tr>", rim["RegistrationNo"], PatientID, info1, Helper.UrlRoot(), isRichTextMode.ToString().ToLower());
                }
                else
                {
                    var info1 = Regex.Replace(rim["Info1"] == DBNull.Value ? String.Empty : rim["Info1"].ToString(), @"\r\n?|\n", "<br />");
                    strBuilder.AppendFormat("<tr><td colspan='2' style='padding-left:2px;'>{0}</td></tr>", info1);

                }

                if (rim["AttendingNotes"] != DBNull.Value && rim["AttendingNotes"].ToString() != string.Empty)
                {
                    strBuilder.AppendFormat(
                        "<tr><td class='label' valign='top' style='width:10px;padding-left:2px'>N:</td><td valign='top'>{0}</td></tr>", rim["AttendingNotes"]);
                }

                strBuilder.AppendLine("</table>");

                if (Convert.ToBoolean(rim["IsDeleted"] == DBNull.Value ? false : rim["IsDeleted"]) == true) strBuilder.AppendLine("</div>");

                newRow["Notes"] = strBuilder.ToString();
                #endregion

                #region Follow Up
                strBuilder = new StringBuilder();
                strBuilder.AppendLine("<table width='100%'>");

                AddHtmlTableRow(rim["ImportantClinicalNotes"], strBuilder, "Important Clinical Notes", 150);
                AddHtmlTableRow(rim["Planning"], strBuilder, "Planning", 150);
                AddHtmlTableRow(rim["Remark"], strBuilder, "Remark", 150);

                strBuilder.AppendLine("</table>");

                newRow["FollowUp"] = strBuilder.ToString();
                #endregion
                // Add Row
                dtb.Rows.Add(newRow);
            }

            return dtb;
        }

        private static void AddHtmlTableRow(object val, StringBuilder strBuilder, string caption, int captionWidth)
        {
            var value = Regex.Replace(val == DBNull.Value ? String.Empty : val.ToString(), @"\r\n?|\n",
                "<br />");
            strBuilder.AppendFormat(
                "<tr><td class='label' valign='top' style='width:{2}px;padding-left:2px'>{0}</td><td valign='top'>{1}</td></tr>",
                caption, value, captionWidth);
            strBuilder.AppendLine();
        }



        private static string GetParamedicName(string paramedicID, string defaultParamedicName)
        {
            var paramedic = new Paramedic();
            var paramedicName = paramedic.LoadByPrimaryKey(paramedicID)
                ? paramedic.ParamedicName
                : defaultParamedicName;
            return paramedicName;
        }


    }
}
