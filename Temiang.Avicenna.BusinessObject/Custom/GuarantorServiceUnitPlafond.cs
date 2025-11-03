using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class GuarantorServiceUnitPlafond
    {
        public string ServiceUnitName
        {
            get { return GetColumn("refToServiceUnit_ServiceUnitName").ToString(); }
            set { SetColumn("refToServiceUnit_ServiceUnitName", value); }
        }

        public static decimal GetPlafondAmount(string guarantorId, string serviceUnitId, bool isBpjs)
        {
            decimal plafondAmt = 0;
            var plafond = new GuarantorServiceUnitPlafond();
            if (plafond.LoadByPrimaryKey(guarantorId, serviceUnitId))
                plafondAmt = plafond.PlafondAmount ?? 0;
            else if (isBpjs)
                plafondAmt = Convert.ToDecimal(AppParameter.GetParameterValue(AppParameter.ParameterItem.NonInPatientBpjsPlafond));
            
            return plafondAmt;
        }
        
    }
}
