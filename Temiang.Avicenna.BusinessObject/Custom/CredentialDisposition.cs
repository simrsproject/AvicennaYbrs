using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class CredentialDisposition
    {
        #region CekDataValid
        public string GetCredentialingRequestVoid(string dispositionNo)
        {
            return GetCredentialingRequestVoidProcess(dispositionNo);
        }
        private static string GetCredentialingRequestVoidProcess(string dispositionNo)
        {
            var msg = string.Empty;
            var cd = new CredentialProcessQuery("a");
            cd.Where(cd.DispositionNo == dispositionNo, cd.Or(cd.IsApproved == false, cd.IsVoid == true));
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
        #endregion

    }
}
