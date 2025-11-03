using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;
using System;
namespace Temiang.Avicenna.BusinessObject
{
    public partial class TransPrescriptionCollection
    {
        public DataTable TransPrescriptionHistory(string patientID, string registrationNo)
        {
            esParameters par = new esParameters();

            string commandText = @"
                    SELECT FORMAT(d.[RegistrationDate], 'yyyy-MM-dd') + a.[RegistrationNo] RegDateRegNo, a.[RegistrationNo],d.[RegistrationDate],a.[PrescriptionNo],a.[PrescriptionDate],a.[IsPrescriptionReturn],a.[IsFromSOAP],
	                    b.[ServiceUnitName] AS 'ServiceUnitName',g.[ItemName] AS 'RegistrationTypeName',c.[ParamedicName] 
                    FROM [TransPrescription] a 
                    INNER JOIN [Registration] d ON d.[RegistrationNo] = a.[RegistrationNo] 
                    INNER JOIN [ServiceUnit] b ON b.[ServiceUnitID] = a.[ServiceUnitID] 
                    LEFT JOIN [AppStandardReferenceItem] g ON (g.[ItemID] = d.[SRRegistrationType] AND g.[StandardReferenceID] = 'RegistrationType') 
                    LEFT JOIN [Paramedic] c ON a.[ParamedicID] = c.[ParamedicID] 
                    WHERE (d.[PatientID] = '" + patientID + "' ) " +
                                 "AND a.[RegistrationNo] LIKE '" + registrationNo + @"%' /*ORDER BY a.[PrescriptionDate] ASC, a.[PrescriptionNo] ASC*/
                    UNION ALL
                    SELECT FORMAT(d.[RegistrationDate], 'yyyy-MM-dd') + a.[RegistrationNo] RegDateRegNo, a.[RegistrationNo],d.[RegistrationDate],a.[PrescriptionNo],a.[PrescriptionDate],a.[IsPrescriptionReturn],a.[IsFromSOAP],
	                    b.[ServiceUnitName] AS 'ServiceUnitName',g.[ItemName] AS 'RegistrationTypeName',c.[ParamedicName] 
                    FROM [TransPrescription] a 
                    INNER JOIN [Registration] d ON d.[RegistrationNo] = a.[RegistrationNo] 
                    INNER JOIN [ServiceUnit] b ON b.[ServiceUnitID] = a.[ServiceUnitID] 
                    LEFT JOIN [AppStandardReferenceItem] g ON (g.[ItemID] = d.[SRRegistrationType] AND g.[StandardReferenceID] = 'RegistrationType') 
                    LEFT JOIN [Paramedic] c ON a.[ParamedicID] = c.[ParamedicID] 
                    WHERE (d.PatientID IN (SELECT x.RelatedPatientID FROM PatientRelated x WHERE x.PatientID = '" + patientID + "')) " +
                                 "AND a.[RegistrationNo] LIKE '" + registrationNo + "%' ORDER BY a.[PrescriptionDate] ASC, a.[PrescriptionNo] ASC";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable GetQueueWithPaging(string IpAddress, string ServiceUnitID, bool IsComplete)
        {
            string cmd = string.Empty;
            cmd = "sp_PrescriptionQueue";

            var pars = new esParameters();
            var pIP = new esParameter("IP", IpAddress, esParameterDirection.Input, DbType.String, 15);
            pars.Add(pIP);
            var pServiceUnitID = new esParameter("ServiceUnitID", ServiceUnitID, esParameterDirection.Input, DbType.String, 10);
            pars.Add(pServiceUnitID);
            var pIsComplete = new esParameter("IsComplete", IsComplete, esParameterDirection.Input, DbType.Boolean, 1);
            pars.Add(pIsComplete);

            es.Connection.CommandTimeout = 600;

            return FillDataTable(esQueryType.StoredProcedure, cmd, pars);
        }

        public DataTable GetQueue3ColWithPaging(string IpAddress, string ServiceUnitID, int iProgress)
        {
            string cmd = string.Empty;
            cmd = "sp_PrescriptionQueue3Col";

            var pars = new esParameters();
            var pIP = new esParameter("IP", IpAddress, esParameterDirection.Input, DbType.String, 15);
            pars.Add(pIP);
            var pServiceUnitID = new esParameter("ServiceUnitID", ServiceUnitID, esParameterDirection.Input, DbType.String, 10);
            pars.Add(pServiceUnitID);
            var pProgress = new esParameter("iProgress", iProgress, esParameterDirection.Input, DbType.Int32, 1);
            pars.Add(pProgress);

            es.Connection.CommandTimeout = 600;

            return FillDataTable(esQueryType.StoredProcedure, cmd, pars);
        }

        public DataTable GetQueue6ColWithPaging(string IpAddress, string ServiceUnitID, int iProgress)
        {
            string cmd = string.Empty;
            cmd = "sp_PrescriptionQueue6Col";

            var pars = new esParameters();
            var pIP = new esParameter("IP", IpAddress, esParameterDirection.Input, DbType.String, 15);
            pars.Add(pIP);
            var pServiceUnitID = new esParameter("ServiceUnitID", ServiceUnitID, esParameterDirection.Input, DbType.String, 10);
            pars.Add(pServiceUnitID);
            var pProgress = new esParameter("iProgress", iProgress, esParameterDirection.Input, DbType.Int32, 1);
            pars.Add(pProgress);

            es.Connection.CommandTimeout = 600;

            return FillDataTable(esQueryType.StoredProcedure, cmd, pars);
        }

        public DataTable GetQueueByCodeWithPaging(string IpAddress, string ServiceUnitID, string Code)
        {
            string cmd = string.Empty;
            cmd = "sp_PrescriptionQueueByCode";

            var pars = new esParameters();
            var pIP = new esParameter("IP", IpAddress, esParameterDirection.Input, DbType.String, 15);
            pars.Add(pIP);
            var pServiceUnitID = new esParameter("ServiceUnitID", ServiceUnitID, esParameterDirection.Input, DbType.String, 10);
            pars.Add(pServiceUnitID);
            var pCode = new esParameter("Code", Code, esParameterDirection.Input, DbType.String, 2);
            pars.Add(pCode);

            es.Connection.CommandTimeout = 600;

            return FillDataTable(esQueryType.StoredProcedure, cmd, pars);
        }

        public DataTable GetQueueByDate(DateTime TransactionDate, string ServiceUnitID)
        {
            string cmd = string.Empty;
            cmd = "sp_PrescriptionQueueByDate";

            var pars = new esParameters();
            var pTD = new esParameter("TransactionDate", TransactionDate, esParameterDirection.Input, DbType.Date, 20);
            pars.Add(pTD);
            var pServiceUnitID = new esParameter("ServiceUnitID", ServiceUnitID, esParameterDirection.Input, DbType.String, 10);
            pars.Add(pServiceUnitID);

            es.Connection.CommandTimeout = 600;

            return FillDataTable(esQueryType.StoredProcedure, cmd, pars);
        }

        public DataTable GetQueueByByMedicalNoAndRegNo(string MedicalNo,string RegistrationNo)
        {
            string cmd = string.Empty;
            cmd = "sp_PrescriptionQueueByMedicalNoAndRegNo";

            var pars = new esParameters();
            var pMedicalNo = new esParameter("MedicalNo", MedicalNo, esParameterDirection.Input, DbType.String, 20);
            pars.Add(pMedicalNo);
            var pRegistrationNo = new esParameter("RegistrationNo", RegistrationNo, esParameterDirection.Input, DbType.String, 20);
            pars.Add(pRegistrationNo);

            es.Connection.CommandTimeout = 600;

            return FillDataTable(esQueryType.StoredProcedure, cmd, pars);
        }
    }
}

