using System;
using System.Data;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RegistrationInfoMedicDiagnose
    {
        public string DiagnoseName
        {
            get { return GetColumn("refToDiagnose_DiagnoseName").ToString(); }
            set { SetColumn("refToDiagnose_DiagnoseName", value); }
        }

        public string DiagnoseType
        {
            get { return GetColumn("refToAppStandardReferenceItem_SRDiagnoseType").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_SRDiagnoseType", value); }
        }

        public string ParamedicName
        {
            get { return GetColumn("refToParamedic_ParamedicName").ToString(); }
            set { SetColumn("refToParamedic_ParamedicName", value); }
        }

        public string CreateByUserName
        {
            get { return GetColumn("refToAppUser_CreateByUserID").ToString(); }
            set { SetColumn("refToAppUser_CreateByUserID", value); }
        }

        public string LastUpdateByUserName
        {
            get { return GetColumn("refToAppUser_LastUpdateByUserID").ToString(); }
            set { SetColumn("refToAppUser_LastUpdateByUserID", value); }
        }

        public string DiagnoseSummaryHtml()
        {
            return DiagnoseSummaryHtml(this.RegistrationNo);
        }

        public static string DiagnoseSummaryHtml(string registrationNo)
        {
            var que = new RegistrationInfoMedicDiagnoseQuery("a");
            var queRef = new AppStandardReferenceItemQuery("b");
            que.InnerJoin(queRef).On(que.SRDiagnoseType == queRef.ItemID).Where(queRef.StandardReferenceID == "DiagnoseType");
            que.Select(que.DiagnoseDateTime, que.DiagnosisText, que.DiagnoseID, que.IsVoid, queRef.ItemName.As("DiagnoseType"));
            que.Where(que.RegistrationNo == registrationNo);
            que.OrderBy(queRef.ItemName.Ascending, que.DiagnoseDateTime.Descending);
            var dtb = que.LoadDataTable();
            if (dtb == null || dtb.Rows.Count == 0) return string.Empty;

            var sb = new StringBuilder();
            sb.AppendLine("<table style='width:100%'>");

            var diagnoseType = string.Empty;
            foreach (DataRow dataRow in dtb.Rows)
            {
                if (string.IsNullOrWhiteSpace(diagnoseType) || !diagnoseType.Equals(dataRow["DiagnoseType"]))
                {
                    diagnoseType = dataRow["DiagnoseType"].ToString();
                    sb.AppendFormat("<tr><td style='font-weight: bold;' colspan='2'>{0}</td></tr>", diagnoseType);
                }

                // Todo: Format date supaya bisa dipakai di BO
                sb.AppendFormat("<tr><td style='width:100px;vertical-align: top;'>{3}</td><td style='vertical-align: top;{0}'>{1} {2}</td></tr>", true.Equals(dataRow["IsVoid"]) ? "text-decoration: line-through;" 
                    : string.Empty, dataRow["DiagnoseID"], dataRow["DiagnosisText"], Convert.ToDateTime(dataRow["DiagnoseDateTime"]).ToString("dd/MM/yyyy HH:mm"));
            }
            sb.AppendLine("</table>");
            return sb.ToString();
        }

        public static string DiagnoseSummary(string registrationNo)
        {
            var que = new RegistrationInfoMedicDiagnoseQuery("a");
            var queRef = new AppStandardReferenceItemQuery("b");
            que.InnerJoin(queRef).On(que.SRDiagnoseType == queRef.ItemID).Where(queRef.StandardReferenceID == "DiagnoseType");
            que.Select(que.DiagnoseDateTime, que.DiagnosisText, que.DiagnoseID, que.IsVoid, queRef.ItemName.As("DiagnoseType"));
            que.Where(que.RegistrationNo == registrationNo);
            que.OrderBy(queRef.ItemName.Ascending, que.DiagnoseDateTime.Descending);
            var dtb = que.LoadDataTable();
            if (dtb == null || dtb.Rows.Count == 0) return string.Empty;

            var sb = new StringBuilder();

            var diagnoseType = string.Empty;
            foreach (DataRow dataRow in dtb.Rows)
            {
                if (string.IsNullOrWhiteSpace(diagnoseType) || !diagnoseType.Equals(dataRow["DiagnoseType"]))
                {
                    if (!string.IsNullOrWhiteSpace(diagnoseType))
                    {
                        sb.AppendLine(string.Empty);
                        sb.AppendLine(string.Empty);
                    }

                    diagnoseType = dataRow["DiagnoseType"].ToString();
                    sb.AppendFormat("{0}:", diagnoseType);
                }
                sb.AppendLine(string.Empty);
                sb.AppendFormat("- {0} {1}", dataRow["DiagnoseID"], dataRow["DiagnosisText"]);
            }
            return sb.ToString();
        }

        public string DiagnoseSummaryCurrentSoap()
        {
            return DiagnoseSummaryCurrentSoap(this.RegistrationInfoMedicID);
        }        
        public static string DiagnoseSummaryCurrentSoap(string regInfoMedicID)
        {
            var strIcd = new StringBuilder();
            var eds = new RegistrationInfoMedicDiagnoseCollection();
            eds.Query.Where(eds.Query.RegistrationInfoMedicID == regInfoMedicID, eds.Query.IsVoid==false);
            eds.Query.OrderBy(eds.Query.SequenceNo.Ascending);
            if (eds.LoadAll() && eds.Count > 0)
            {
                strIcd.AppendLine("ICD 10:");
                foreach (RegistrationInfoMedicDiagnose ed in eds)
                {
                    var stdi = new AppStandardReferenceItem();
                    if (stdi.LoadByPrimaryKey("DiagnoseType", ed.SRDiagnoseType))
                    {
                        strIcd.AppendFormat("  {0}: {1} {2}", stdi.ItemName, ed.DiagnoseID, ed.DiagnosisText);
                        strIcd.AppendLine(string.Empty);
                    }
                }
            }
            return strIcd.ToString();
        }
        public static string MainDiagnose(string registrationNo)
        {
            var ed = new RegistrationInfoMedicDiagnose();
            ed.Query.Where(ed.Query.RegistrationNo == registrationNo, ed.Query.SRDiagnoseType == AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            ed.Query.es.Top = 1;
            if (ed.Query.Load())
                return ed.DiagnosisText;
            return string.Empty;
        }
    }
}
