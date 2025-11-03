namespace Temiang.Avicenna.BusinessObject
{
    public partial class PpiAntimicrobialApplications
    {
        public string TherapyGroupName
        {
            get { return GetColumn("refToAppStdRef_TherapyGroup").ToString(); }
            set { SetColumn("refToAppStdRef_TherapyGroup", value); }
        }

        public string TherapyName
        {
            get { return GetColumn("refToTherapy_TherapyName").ToString(); }
            set { SetColumn("refToTherapy_TherapyName", value); }
        }

        public string AntimicrobialApplicationTimingName
        {
            get { return GetColumn("refToAppStdRef_Timing").ToString(); }
            set { SetColumn("refToAppStdRef_Timing", value); }
        }
    }
}
