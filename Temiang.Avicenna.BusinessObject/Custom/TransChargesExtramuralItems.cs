using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;
using System;
namespace Temiang.Avicenna.BusinessObject
{
    public partial class TransChargesExtramuralItems
    {
        public string SRExtramuralItemName
        {
            get { return GetColumn("refToStdRef_ItemName").ToString(); }
            set { SetColumn("refToStdRef_ItemName", value); }
        }
        public decimal Guaranty
        {
            get { return System.Convert.ToDecimal(GetColumn("refToStdRef_Guaranty")); }
            set { SetColumn("refToStdRef_Guaranty", value); }
        }
        
    }

    public partial class TransChargesExtramuralItemsCollection {
        public DataTable GetOutstanding(string ServiceUnitID, string RegistrationNo, DateTime? RegistrationDate, string PatientName) {
            esParameters par = new esParameters();
            if (!string.IsNullOrEmpty(ServiceUnitID)) {
                var pSuid = par.Add("ServiceUnitID", ServiceUnitID);
            }
            if (!string.IsNullOrEmpty(RegistrationNo))
            {
                var pRegNo = par.Add("RegistrationNo", "%" + RegistrationNo + "%");
            }
            if (RegistrationDate.HasValue)
            {
                var pRegNo = par.Add("RegistrationDate", RegistrationDate.Value);
            }
            if (!string.IsNullOrEmpty(PatientName))
            {
                var pPName = par.Add("PatientName", "%" + PatientName + "%");
            }

            string commandText = string.Format(@"SELECT * FROM(
                SELECT tc.TransactionNo, tc.RegistrationNo, tc.TransactionDate, ISNULL(asri.ItemName, '') AS SalutationName, 
                    RTRIM(RTRIM(p.FirstName + ' ' + p.MiddleName) + ' ' + p.LastName) PatientName,
                    su.ServiceUnitName,
                    tce.LeasingPeriodInDays,
	                ROW_NUMBER() OVER (PARTITION BY tc.TransactionNo ORDER BY tce.LeasingPeriodInDays) RN
                FROM TransCharges AS tc
                INNER JOIN TransChargesExtramuralItems AS tce ON tc.TransactionNo = tce.TransactionNo
                INNER JOIN Registration r on tc.RegistrationNo = r.RegistrationNo
                INNER JOIN Patient p on r.PatientID = p.PatientID
                INNER JOIN ServiceUnit su on tc.ToServiceUnitID = su.ServiceUnitID
                LEFT JOIN AppStandardReferenceItem AS asri ON asri.StandardReferenceID = 'Salutation' AND asri.ItemID = p.SRSalutation
                WHERE tce.IsReturned = 0 {0} {1} {2} {3}
                ) a WHERE a.RN = 1", 
                !string.IsNullOrEmpty(ServiceUnitID) ? ("AND tc.ToServiceUnitID = @ServiceUnitID") :"",
                !string.IsNullOrEmpty(RegistrationNo) ? ("AND (r.RegistrationNo like @RegistrationNo OR REPLACE(p.MedicalNo, '-','') like REPLACE(@RegistrationNo,'-',''))") : "",
                RegistrationDate.HasValue ? ("AND RegistrationDate = @RegistrationDate") : "",
                !string.IsNullOrEmpty(PatientName) ? ("AND RTRIM(RTRIM(p.FirstName + ' ' + p.MiddleName) + ' ' + p.LastName) like @PatientName") : ""
                );

            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
