namespace Temiang.Avicenna.BusinessObject
{
    public partial class PpiRiskFactorsItem
    {
        public string SignsOfInfectionName
        {
            get { return GetColumn("refToAppStdRef_SignsOfInfection").ToString(); }
            set { SetColumn("refToAppStdRef_SignsOfInfection", value); }
        }
    }
}
