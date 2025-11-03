using System.Text;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class Healthcare
    {
        public static string AddressComplete()
        {
            var par = new AppParameter();
            par.LoadByPrimaryKey("HealthcareID");

            return AddressComplete(par.ParameterValue);
        }
        public static string AddressComplete(string healthCareID)
        {
            var healthcare = new Healthcare();
            if (healthcare.LoadByPrimaryKey(healthCareID))
                return AddressComplete(healthcare);
            else
                return string.Empty;

        }
        public static string AddressComplete(Healthcare healthcare)
        {
            string healthCareInfo = string.Empty;
            healthCareInfo = healthcare.HealthcareName;
            healthCareInfo += "\n" + healthcare.AddressLine1;
            if (healthcare.AddressLine2 != string.Empty)
                healthCareInfo += "\n" + healthcare.AddressLine2;
            if (healthcare.PhoneNo != string.Empty)
                healthCareInfo += "\nPhone " + healthcare.PhoneNo;
            if (healthcare.FaxNo != string.Empty)
                healthCareInfo += "\nFax " + healthcare.FaxNo;
            return healthCareInfo;
        }

        public static string GetHealthcareName()
        {
            string healthCareInfo = string.Empty;
            var healthcare = new Healthcare();

            var par = new AppParameter();
            par.LoadByPrimaryKey(AppParameter.ParameterItem.HealthcareID.ToString());

            if (healthcare.LoadByPrimaryKey(par.ParameterValue))
            {
                healthCareInfo = healthcare.HealthcareName;
            }
            return healthCareInfo;
        }
        public static string GetHealthcareCity()
        {
            string healthCareInfo = string.Empty;
            var healthcare = new Healthcare();

            var par = new AppParameter();
            par.LoadByPrimaryKey(AppParameter.ParameterItem.HealthcareID.ToString());

            if (healthcare.LoadByPrimaryKey(par.ParameterValue))
            {
                healthCareInfo = healthcare.City;
            }
            return healthCareInfo;
        }
        public static Healthcare GetHealthcare()
        {
            var healthcare = new Healthcare();
            var par = new AppParameter();
            par.LoadByPrimaryKey(AppParameter.ParameterItem.HealthcareID.ToString());

            if (!healthcare.LoadByPrimaryKey(par.ParameterValue))
            {
                throw  new System.Exception("Healthcare must define first in Parameter or in in Healthcare table");
                return null;
            }

            return healthcare;
        }
        public string ReportTitle()
        {
            var sbHealthcareInfo = new StringBuilder();

            sbHealthcareInfo.AppendLine(this.HealthcareName.ToUpper());

            sbHealthcareInfo.Append(this.AddressLine1.ToUpper());

            if (!string.IsNullOrEmpty(this.AddressLine2))
            {
                sbHealthcareInfo.AppendLine("");
                sbHealthcareInfo.Append(this.AddressLine2.ToUpper());
            }

            sbHealthcareInfo.Append(" " + this.City.ToUpper() + " " + this.ZipCode);

            if (!string.IsNullOrEmpty(this.PhoneNo))
            {
                sbHealthcareInfo.AppendLine("");
                sbHealthcareInfo.AppendFormat("Phone: {0}", this.PhoneNo);
            }
            return sbHealthcareInfo.ToString();
        }
    }
}
