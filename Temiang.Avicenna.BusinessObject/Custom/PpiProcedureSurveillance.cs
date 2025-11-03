using System;
namespace Temiang.Avicenna.BusinessObject
{
    public partial class PpiProcedureSurveillance
    {
        public string Diagnose
        {
            get { return GetColumn("refToServiceBooking_Diagnose").ToString(); }
            set { SetColumn("refToServiceBooking_Diagnose", value); }
        }

        public DateTime RealizationDateTimeFrom
        {
            get { return Convert.ToDateTime(GetColumn("refToServiceBooking_RealizationDateTimeFrom")); }
            set { SetColumn("refToServiceBooking_RealizationDateTimeFrom", value); }
        }

        public DateTime RealizationDateTimeTo
        {
            get { return Convert.ToDateTime(GetColumn("refToServiceBooking_RealizationDateTimeTo")); }
            set { SetColumn("refToServiceBooking_RealizationDateTimeTo", value); }
        }

        public Boolean IsCito
        {
            get { return Convert.ToBoolean(GetColumn("refToServiceBooking_IsCito")); }
            set { SetColumn("refToServiceBooking_IsCito", value); }
        }

        public string ProcedureClassificationName
        {
            get { return GetColumn("refToAppStdRef_ProcedureClassification").ToString(); }
            set { SetColumn("refToAppStdRef_ProcedureClassification", value); }
        }

        public string TypesOfSurgeryName
        {
            get { return GetColumn("refToAppStdRef_TypesOfSurgery").ToString(); }
            set { SetColumn("refToAppStdRef_TypesOfSurgery", value); }
        }

        public string WoundClassificationName
        {
            get { return GetColumn("refToAppStdRef_WoundClassification").ToString(); }
            set { SetColumn("refToAppStdRef_WoundClassification", value); }
        }

        public string AsaScoreName
        {
            get { return GetColumn("refToAppStdRef_AsaScore").ToString(); }
            set { SetColumn("refToAppStdRef_AsaScore", value); }
        }

        public string TTimeName
        {
            get { return GetColumn("refToAppStdRef_TTime").ToString(); }
            set { SetColumn("refToAppStdRef_TTime", value); }
        }
    }
}
