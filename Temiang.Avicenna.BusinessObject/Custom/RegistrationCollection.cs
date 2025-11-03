using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;
namespace Temiang.Avicenna.BusinessObject
{
    public partial class RegistrationCollection
    {
        public DataTable RegistrationHistory(string patientID, string bedStatusPending)
        {
            esParameters par = new esParameters();
            string commandText = @"SELECT r.[PatientID],r.[RegistrationDate],r.[RegistrationTime],r.[RegistrationNo],r.[IsClosed],r.[IsVoid], " +
                        "p.[MedicalNo],((((p.[FirstName]+' ')+RTRIM(p.[MiddleName]))+' ')+RTRIM(p.[LastName])) AS 'PatientName', " +
                        "p.[Sex],m.[ParamedicName],s.[ServiceUnitName],d.[RoomName],e.[ClassName],r.[BedID],r.[SRRegistrationType], " +
                        "f.[ItemName] AS 'RegistrationTypeName',r.[DischargeDate],r.[DischargeTime],r.[LastCreateUserID], " +
                        "r.IsHoldTransactionEntry, r.IsHoldTransactionEntryByUserID, g.[NoteCount], " +
                        "CASE WHEN ISNULL(b.[SRBedStatus], '') = '" + bedStatusPending + "' THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsBedStatusPending " +
                    "FROM [Registration] r " +
                    "INNER JOIN [Patient] p ON r.[PatientID] = p.[PatientID] " +
                    "LEFT JOIN [Paramedic] m ON r.[ParamedicID] = m.[ParamedicID] " +
                    "LEFT JOIN [ServiceUnit] s ON r.[ServiceUnitID] = s.[ServiceUnitID] " +
                    "LEFT JOIN [ServiceRoom] d ON r.[RoomID] = d.[RoomID] " +
                    "LEFT JOIN [Class] e ON r.[ClassID] = e.[ClassID] " +
                    "LEFT JOIN [AppStandardReferenceItem] f ON (f.[ItemID] = r.[SRRegistrationType] AND f.[StandardReferenceID] = 'RegistrationType') " +
                    "LEFT JOIN [RegistrationInfoSumary] g ON g.[RegistrationNo] = r.[RegistrationNo] AND g.[NoteCount] > 0 " +
                    "LEFT JOIN [Bed] b On b.[BedID] = r.[BedID] AND b.[RegistrationNo] = r.[RegistrationNo] " +
                    "WHERE ((r.[PatientID] = '" + patientID + "' OR r.PatientID IN (SELECT x.RelatedPatientID FROM PatientRelated x WHERE x.PatientID = '" + patientID + "'))) ORDER BY r.[RegistrationDate] ASC";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable RegistrationHistoryForSOAP(string patientID)
        {
            esParameters par = new esParameters();
            string commandText = @"SELECT r.[PatientID],p.[MedicalNo],r.[RegistrationDate],r.[RegistrationTime],
                        r.[RegistrationNo],((((p.[FirstName]+' ')+RTRIM(p.[MiddleName]))+' ')+RTRIM(p.[LastName])) AS 'PatientName',
	                    p.[Sex],m.[ParamedicName],s.[ServiceUnitName],d.[RoomName],r.[BedID],std.[ItemName] 
                    FROM [Registration] r 
                    INNER JOIN [Patient] p ON r.[PatientID] = p.[PatientID] 
                    LEFT JOIN [Paramedic] m ON r.[ParamedicID] = m.[ParamedicID] 
                    LEFT JOIN [ServiceUnit] s ON r.[ServiceUnitID] = s.[ServiceUnitID] 
                    LEFT JOIN [ServiceRoom] d ON r.[RoomID] = d.[RoomID] 
                    LEFT JOIN [AppStandardReferenceItem] std ON r.[SRTriage] = std.[ItemID] 
                    WHERE ((r.[PatientID] = '" + patientID + "' OR r.PatientID IN (SELECT x.RelatedPatientID FROM PatientRelated x WHERE x.PatientID = '" + patientID + "'))) AND r.[IsVoid] = 0 ORDER BY r.[RegistrationDate] DESC, r.[RegistrationTime] DESC";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable RegistrationHistoryForJobOrder(string patientID, string toServiceUnitID)
        {
            esParameters par = new esParameters();
            string commandText = @"SELECT r.[RegistrationNo], m.[ParamedicName],s.[ServiceUnitName],tc.[TransactionNo],tc.[TransactionDate],
                        tc.[ExecutionDate],su.[ServiceUnitName] AS [FromServiceUnitName], 
                        CASE WHEN tcc.ReferenceNo IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsCorrected
                    FROM [Registration] r 
                    INNER JOIN [Patient] p ON r.[PatientID] = p.[PatientID] 
                    LEFT JOIN [Paramedic] m ON r.[ParamedicID] = m.[ParamedicID] 
                    INNER JOIN [TransCharges] tc ON tc.[RegistrationNo] = r.[RegistrationNo] AND tc.[IsApproved] = 1 AND tc.[IsOrder] = 1 
                        AND tc.[ToServiceUnitID] = '" + toServiceUnitID + "' " +
                    @"INNER JOIN [ServiceUnit] s ON tc.[ToServiceUnitID] = s.[ServiceUnitID] 
                    INNER JOIN [ServiceUnit] su ON tc.[FromServiceUnitID] = su.[ServiceUnitID] 
                    LEFT JOIN (SELECT DISTINCT x.ReferenceNo FROM [TransCharges] x WHERE x.IsCorrection = 1) tcc ON tcc.ReferenceNo = tc.TransactionNo
                    WHERE ((r.[PatientID] = '" + patientID + "' OR r.PatientID IN (SELECT x.RelatedPatientID FROM PatientRelated x WHERE x.PatientID = '" + patientID + "'))) " + 
                        @"AND r.[IsVoid] = 0 ORDER BY r.[RegistrationDate] ASC";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public int RegistrationUpdateQue(string RegistrationNo, int ChangeToNo)
        {
            string cmd = @"sp_changeque";
            esParameters pars = new esParameters();
            var pdRegistrationNo = new esParameter("RegistrationNoToChange", RegistrationNo, esParameterDirection.Input, DbType.String, 20);
            pars.Add(pdRegistrationNo);
            var pdChangeToNo = new esParameter("ChangeToNo", ChangeToNo, esParameterDirection.Input, DbType.Int16, 0);
            pars.Add(pdChangeToNo);

            return ExecuteNonQuery(esQueryType.StoredProcedure, cmd, pars);
        }

        public DataTable RegistrationPendingRealizationForCafe(string ServiceUnitID, string DateFrom, string DateTo)
        {
            string cmd = "sp_getRegOutstandingRealization";
            var pars = new esParameters();
            var pSuid = new esParameter("ServiceUnitID", ServiceUnitID, esParameterDirection.Input, DbType.String, 0);
            pars.Add(pSuid);
            var pDateFrom = new esParameter("DateFrom", DateFrom, esParameterDirection.Input, DbType.DateTime, 0);
            pars.Add(pDateFrom);
            var pDateTo = new esParameter("DateTo", DateFrom, esParameterDirection.Input, DbType.DateTime, 0);
            pars.Add(pDateTo);

            return FillDataTable(esQueryType.StoredProcedure, cmd, pars);
        }

        public DataTable RegistrationLastVisit(string patientID, bool isNew, string registrationNo, string registrationDateTime)
        {
            esParameters par = new esParameters();
            string commandText = @"SELECT TOP 1 r.*, ISNULL(r.DischargeDate, GETDATE()) AS 'DischargeDateX'
                    FROM [Registration] r 
                    WHERE r.[PatientID] = '" + patientID + "' AND r.[IsVoid] = 0 AND r.[IsFromDispensary] = 0 AND r.[IsConsul] = 0 AND r.[IsNonPatient] = 0 ";

            if (!isNew)
                commandText += "AND r.[RegistrationNo] <> '" + registrationNo + "' AND (LEFT(CONVERT(VARCHAR, r.RegistrationDate, 20), 11) + r.RegistrationTime) < '" + registrationDateTime + "'";

            commandText += "ORDER BY r.[RegistrationDate] DESC, r.[RegistrationTime] DESC ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable GetRegistrationDisplayEmergency(string IpAddress, string ServiceUnitID)
        {
            string cmd = string.Empty;
            cmd = "sp_RegistrationDisplayEmergency";
            var pars = new esParameters();
            var pIP = new esParameter("IP", IpAddress, esParameterDirection.Input, DbType.String, 15);
            pars.Add(pIP);
            var pServiceUnitID = new esParameter("ServiceUnitID", ServiceUnitID, esParameterDirection.Input, DbType.String, 10);
            pars.Add(pServiceUnitID);

            es.Connection.CommandTimeout = 600;

            return FillDataTable(esQueryType.StoredProcedure, cmd, pars);
        }


        public DataTable PoliklinikList(string IpAddress, string ServiceUnitID)
        {
            string cmd = string.Empty;
            cmd = "sp_PoliklinikQueList";
            var pars = new esParameters();
            var pIP = new esParameter("IP", IpAddress, esParameterDirection.Input, DbType.String, 15);
            pars.Add(pIP);
            var pServiceUnitID = new esParameter("ServiceUnitID", ServiceUnitID, esParameterDirection.Input, DbType.String, 10);
            pars.Add(pServiceUnitID);

            es.Connection.CommandTimeout = 600;

            return FillDataTable(esQueryType.StoredProcedure, cmd, pars);
        }

    }
}

