using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;
using System;
using System.Linq;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class CredentialProcessCollection
    {
        public DataTable CredentialingDispositionOutstanding()
        {
            esParameters par = new esParameters();

            string commandText = "SELECT cp.TransactionNo, cp.TransactionDate, p.EmployeeNumber, " +
                            "LTRIM(p.PreTitle + ' ') + RTRIM(p.FirstName + ' ' + p.MiddleName) + RTRIM(' ' + p.LastName) + RTRIM(' ' + p.PostTitle) AS EmployeeName," +
                            "stdp.ItemName AS ProfessionGroupName, stda.ItemName AS ClinicalWorkAreaName, stdl.ItemName AS ClinicalAuthorityLevelName " +
                        "FROM CredentialProcess AS cp " +
                        "INNER JOIN PersonalInfo AS p ON p.PersonID = cp.PersonID " +
                        "INNER JOIN(SELECT x.ItemID, x.ItemName FROM AppStandardReferenceItem x WHERE x.StandardReferenceID = 'ProfessionGroup') stdp ON stdp.ItemID = cp.SRProfessionGroup " +
                        "INNER JOIN(SELECT x.ItemID, x.ItemName FROM AppStandardReferenceItem x WHERE x.StandardReferenceID = 'ClinicalWorkArea') stda ON stda.ItemID = cp.SRClinicalWorkArea " +
                        "LEFT JOIN(SELECT x.ItemID, x.ItemName FROM AppStandardReferenceItem x WHERE x.StandardReferenceID = 'ClinicalAuthorityLevel') stdl ON stdl.ItemID = cp.SRClinicalAuthorityLevel " +
                        "WHERE ISNULL(cp.DispositionNo, '') = '' " +
                        "ORDER BY cp.TransactionNo";


            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable CredentialingDispositionOutstandingV2(string dispositionNo, string srProfessionGroup)
        {
            esParameters par = new esParameters();

            string commandText = "SELECT cp.TransactionNo, cp.TransactionDate, cp.PersonID, p.EmployeeNumber, " +
                            "LTRIM(p.PreTitle + ' ') + RTRIM(p.FirstName + ' ' + p.MiddleName) + RTRIM(' ' + p.LastName) + RTRIM(' ' + p.PostTitle) AS EmployeeName," +
                            "stdp.ItemName AS ProfessionGroupName, stda.ItemName AS ClinicalWorkAreaName, stdl.ItemName AS ClinicalAuthorityLevelName " +
                        "FROM CredentialProcess AS cp " +
                        "INNER JOIN PersonalInfo AS p ON p.PersonID = cp.PersonID " +
                        "INNER JOIN(SELECT x.ItemID, x.ItemName FROM AppStandardReferenceItem x WHERE x.StandardReferenceID = 'ProfessionGroup') stdp ON stdp.ItemID = cp.SRProfessionGroup " +
                        "INNER JOIN(SELECT x.ItemID, x.ItemName FROM AppStandardReferenceItem x WHERE x.StandardReferenceID = 'ClinicalWorkArea') stda ON stda.ItemID = cp.SRClinicalWorkArea " +
                        "LEFT JOIN(SELECT x.ItemID, x.ItemName FROM AppStandardReferenceItem x WHERE x.StandardReferenceID = 'ClinicalAuthorityLevel') stdl ON stdl.ItemID = cp.SRClinicalAuthorityLevel " +
                        "LEFT JOIN CredentialDispositionItem AS cdi ON cdi.TransactionNo = cp.TransactionNo " +
                        "LEFT JOIN CredentialDisposition AS cd ON cd.DispositionNo = cdi.DispositionNo " +
                        "WHERE cp.IsApproved = 1 AND cp.IsCi = 0 AND (ISNULL(cp.DispositionNo, '') = '' OR (cdi.DispositionNo = '" + dispositionNo + "' " +
                                    "AND ISNULL(cd.IsApproved, 0) = 0 AND ISNULL(cd.IsVoid, 0) = 0)) ";

            if (!string.IsNullOrEmpty(srProfessionGroup))
                commandText += "AND cp.[SRProfessionGroup] = '" + srProfessionGroup + "' ";

            commandText += "ORDER BY cp.TransactionNo";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable CredentialingSchedulingOutstanding(string srProfessionGroup)
        {
            esParameters par = new esParameters();

            string commandText = "SELECT cp.DispositionNo, disp.DispositionDate, disp.ReferenceNo, cp.TransactionNo, cp.TransactionDate, p.EmployeeNumber, " +
                            "LTRIM(p.PreTitle + ' ') + RTRIM(p.FirstName + ' ' + p.MiddleName) + RTRIM(' ' + p.LastName) + RTRIM(' ' + p.PostTitle) AS EmployeeName," +
                            "stdp.ItemName AS ProfessionGroupName, stda.ItemName AS ClinicalWorkAreaName, stdl.ItemName AS ClinicalAuthorityLevelName " +
                        "FROM CredentialProcess AS cp " +
                        "INNER JOIN PersonalInfo AS p ON p.PersonID = cp.PersonID " +
                        "INNER JOIN(SELECT x.ItemID, x.ItemName FROM AppStandardReferenceItem x WHERE x.StandardReferenceID = 'ProfessionGroup') stdp ON stdp.ItemID = cp.SRProfessionGroup " +
                        "INNER JOIN(SELECT x.ItemID, x.ItemName FROM AppStandardReferenceItem x WHERE x.StandardReferenceID = 'ClinicalWorkArea') stda ON stda.ItemID = cp.SRClinicalWorkArea " +
                        "LEFT JOIN(SELECT x.ItemID, x.ItemName FROM AppStandardReferenceItem x WHERE x.StandardReferenceID = 'ClinicalAuthorityLevel') stdl ON stdl.ItemID = cp.SRClinicalAuthorityLevel " +
                        "INNER JOIN CredentialDisposition disp ON disp.DispositionNo = cp.DispositionNo AND disp.IsApproved = 1 " +
                        "WHERE cp.IsDocumentChecking = 1 AND cp.ScheduleDate IS NULL AND cp.IsCi = 0 ";

            if (!string.IsNullOrEmpty(srProfessionGroup))
                commandText += "AND cp.[SRProfessionGroup] = '" + srProfessionGroup + "' ";
            commandText += "ORDER BY cp.DispositionNo, cp.TransactionNo";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable CredentialingInvitationOutstanding(string invitationNo, string srProfessionGroup, DateTime scheduleDate)
        {
            esParameters par = new esParameters();

            string commandText = "SELECT cp.TransactionNo, cp.TransactionDate, cp.PersonID, p.EmployeeNumber, " +
                            "LTRIM(p.PreTitle + ' ') + RTRIM(p.FirstName + ' ' + p.MiddleName) + RTRIM(' ' + p.LastName) + RTRIM(' ' + p.PostTitle) AS EmployeeName," +
                            "stdp.ItemName AS ProfessionGroupName, stda.ItemName AS ClinicalWorkAreaName, stdl.ItemName AS ClinicalAuthorityLevelName " +
                        "FROM CredentialProcess AS cp " +
                        "INNER JOIN PersonalInfo AS p ON p.PersonID = cp.PersonID " +
                        "INNER JOIN(SELECT x.ItemID, x.ItemName FROM AppStandardReferenceItem x WHERE x.StandardReferenceID = 'ProfessionGroup') stdp ON stdp.ItemID = cp.SRProfessionGroup " +
                        "INNER JOIN(SELECT x.ItemID, x.ItemName FROM AppStandardReferenceItem x WHERE x.StandardReferenceID = 'ClinicalWorkArea') stda ON stda.ItemID = cp.SRClinicalWorkArea " +
                        "LEFT JOIN(SELECT x.ItemID, x.ItemName FROM AppStandardReferenceItem x WHERE x.StandardReferenceID = 'ClinicalAuthorityLevel') stdl ON stdl.ItemID = cp.SRClinicalAuthorityLevel " +
                        "LEFT JOIN CredentialInvitationItem AS cdi ON cdi.TransactionNo = cp.TransactionNo " +
                        "LEFT JOIN CredentialInvitation AS cd ON cd.InvitationNo = cdi.InvitationNo " +
                        "WHERE cp.IsApproved = 1 AND cp.IsCi = 0 AND (ISNULL(cp.InvitationNo, '') = '' OR (cdi.InvitationNo = '" + invitationNo + "' " +
                                    "AND ISNULL(cd.IsApproved, 0) = 0 AND ISNULL(cd.IsVoid, 0) = 0)) ";

            commandText += "AND cp.[SRProfessionGroup] = '" + srProfessionGroup + "' ";
            commandText += "AND cp.[ScheduleDate] = '" + scheduleDate + "' ";

            commandText += "ORDER BY cp.TransactionNo";

            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
