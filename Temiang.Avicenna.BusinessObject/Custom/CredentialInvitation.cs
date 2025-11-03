using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class CredentialInvitation
    {
        #region CekDataValid
        public string GetCredentialingRequestVoid(string invitationNo)
        {
            return GetCredentialingRequestVoidProcess(invitationNo);
        }
        private static string GetCredentialingRequestVoidProcess(string invitationNo)
        {
            var msg = string.Empty;
            var cd = new CredentialProcessQuery("a");
            cd.Where(cd.InvitationNo == invitationNo, cd.Or(cd.IsApproved == false, cd.IsVoid == true));
            cd.Select(cd.TransactionNo);
            DataTable dtb = cd.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    if (msg == string.Empty)
                        msg = row["TransactionNo"].ToString();
                    else
                        msg += ", " + row["TransactionNo"].ToString();
                }
            }

            return msg;
        }

        public string GetEmptyTeamLetterNo(string invitationNo)
        {
            return GetEmptyLetterNo(invitationNo);
        }
        private static string GetEmptyLetterNo(string invitationNo)
        {
            var msg = string.Empty;
            var cd = new CredentialInvitationTeamQuery("a");
            var pinfo = new PersonalInfoQuery("b");
            cd.InnerJoin(pinfo).On(pinfo.PersonID == cd.PersonID);
            cd.Where(cd.InvitationNo == invitationNo, cd.Or(cd.InvitationLetterNo.IsNull(), cd.InvitationLetterNo == string.Empty));
            cd.Select(pinfo.EmployeeName);
            DataTable dtb = cd.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    if (msg == string.Empty)
                        msg = row["EmployeeName"].ToString();
                    else
                        msg += ", " + row["EmployeeName"].ToString();
                }
            }

            return msg;
        }
        #endregion
    }
}
