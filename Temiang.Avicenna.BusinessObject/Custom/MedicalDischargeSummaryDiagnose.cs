using System;
using System.Data;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class MedicalDischargeSummaryDiagnose
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


        public static string MainDiagnose(string registrationNo)
        {
            var ed = new MedicalDischargeSummaryDiagnose();
            ed.Query.Where(ed.Query.RegistrationNo == registrationNo, ed.Query.SRDiagnoseType == AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            ed.Query.es.Top = 1;
            if (ed.Query.Load())
                return ed.DiagnosisText;
            return string.Empty;
        }
    }
}
