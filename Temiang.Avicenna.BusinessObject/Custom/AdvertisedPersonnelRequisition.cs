using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class AdvertisedPersonnelRequisition
    {
        public string RequestedByPersonName
        {
            get { return GetColumn("refTo_RequestedByPersonName").ToString(); }
            set { SetColumn("refTo_RequestedByPersonName", value); }
        }

        public string RecruitmentPlanName
        {
            get { return GetColumn("refTo_RecruitmentPlanName").ToString(); }
            set { SetColumn("refTo_RecruitmentPlanName", value); }
        }

        public string NumberOfRequiredEmployee
        {
            get { return GetColumn("refTo_NumberOfRequiredEmployee").ToString(); }
            set { SetColumn("refTo_NumberOfRequiredEmployee", value); }
        }

        public string OrganizationUnitName
        {
            get { return GetColumn("refTo_OrganizationUnitName").ToString(); }
            set { SetColumn("refTo_OrganizationUnitName", value); }
        }
    }
}