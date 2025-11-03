using System;
using System.Data;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class EpisodeDiagnose
    {
        public string DiagnoseName
        {
            get { return GetColumn("refToDiagnose_DiagnoseName").ToString(); }
            set { SetColumn("refToDiagnose_DiagnoseName", value); }
        }

        public string DiagnoseType
        {
            get
            {
                var val = GetColumn("refToAppStandardReferenceItem_SRDiagnoseType");
                if (val == null) return String.Empty;
                return val.ToString();
            }
            set { SetColumn("refToAppStandardReferenceItem_SRDiagnoseType", value); }
        }
        public string ExternalCauseName
        {
            get
            {
                var val = GetColumn("refToDiagnose_DiagnoseName4Ec");
                if (val == null) return String.Empty;
                return val.ToString();
            }
            set { SetColumn("refToDiagnose_DiagnoseName4Ec", value); }
        }
        public string MorphologyName
        {
            get { return GetColumn("refToMorphology_MorphologyName").ToString(); }
            set { SetColumn("refToMorphology_MorphologyName", value); }
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
            var que = new EpisodeDiagnoseQuery("a");
            var queRef = new AppStandardReferenceItemQuery("b");
            que.InnerJoin(queRef).On(que.SRDiagnoseType == queRef.ItemID).Where(queRef.StandardReferenceID == "DiagnoseType");
            que.Select(que, queRef.ItemName.As("DiagnoseType"));
            que.Where(que.RegistrationNo == registrationNo);
            que.OrderBy(queRef.ItemName.Ascending, que.CreateDateTime.Descending);

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
                    sb.AppendFormat("<tr><td style='font-weight: bold;'>{0}</td></tr>", diagnoseType);
                }

                sb.AppendFormat("<tr><td style='padding-left:6px;{0}'>{1} {2}</td></tr>", true.Equals(dataRow["IsVoid"]) ? "text-decoration: line-through;" : string.Empty, dataRow["DiagnoseID"], dataRow["DiagnosisText"]);
                var externalCauseID = Convert.ToString(dataRow["ExternalCauseID"]);
                if (!string.IsNullOrEmpty(externalCauseID))
                {
                    var diag = new Diagnose();
                    diag.LoadByPrimaryKey(externalCauseID);

                    sb.AppendFormat("<tr><td style='padding-left:6px;{0}'>Ext Cause : {1} {2}</td></tr>", true.Equals(dataRow["IsVoid"]) ? "text-decoration: line-through;" : string.Empty, diag.DiagnoseID, diag.DiagnoseName);
                }
                var morphologyID = Convert.ToString(dataRow["MorphologyID"]);
                if (!string.IsNullOrEmpty(morphologyID))
                {
                    var morp = new Morphology();
                    morp.LoadByPrimaryKey(morphologyID);

                    sb.AppendFormat("<tr><td style='padding-left:6px;{0}'>Morphology : {1} {2}</td></tr>", true.Equals(dataRow["IsVoid"]) ? "text-decoration: line-through;" : string.Empty, morp.MorphologyID, morp.MorphologyName);
                }
            }
            sb.AppendLine("</table>");
            return sb.ToString();
        }

        public string DiagnoseSummary()
        {
            return DiagnoseSummary(this.RegistrationNo);
        }
        public static string DiagnoseSummary(string registrationNo)
        {
            var strIcd = new StringBuilder();
            var eds = new EpisodeDiagnoseCollection();
            eds.Query.Where(eds.Query.RegistrationNo == registrationNo, eds.Query.IsVoid == false);
            if (eds.LoadAll() && eds.Count > 0)
            {
                strIcd.AppendLine("ICD 10:");
                foreach (EpisodeDiagnose ed in eds)
                {
                    var stdi = new AppStandardReferenceItem();
                    if (stdi.LoadByPrimaryKey("DiagnoseType", ed.SRDiagnoseType))
                    {
                        strIcd.AppendFormat("  {0}: {1} {2}, Synonym: {3}", stdi.ItemName, ed.DiagnoseID, ed.DiagnosisText, ed.DiagnoseSynonym);
                        strIcd.AppendLine(string.Empty);
                    }
                }
            }
            return strIcd.ToString();
        }
        public static string MainDiagnose(string registrationNo)
        {
            var ed = new EpisodeDiagnose();
            ed.Query.Where(ed.Query.RegistrationNo == registrationNo, ed.Query.SRDiagnoseType == AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            ed.Query.es.Top = 1;
            if (ed.Query.Load())
                return ed.DiagnosisText;
            return string.Empty;
        }
    }
}
