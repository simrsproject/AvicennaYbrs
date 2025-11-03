using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class CompletenessAnalysisHistoryDetailList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            var btnOk = (Button)Helper.FindControlRecursive(Master, "btnOk");
            btnOk.Visible = false;
            var btnCancel = (Button)Helper.FindControlRecursive(Master, "btnCancel");
            btnCancel.Text = "Close";

            ProgramID = AppConstant.Program.MedicalRecordFileCompletenessAnalysis;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var query = new MedicalRecordFileCompletenessHistoryItemQuery("a");
            var qdoc = new DocumentFilesQuery("b");
            query.InnerJoin(qdoc).On(qdoc.DocumentFilesID == query.DocumentFilesID);
            query.Where(query.TxId == Convert.ToInt64(Request.QueryString["id"]));
            query.Select(
                query.TxId,
                query.DocumentFilesID,
                qdoc.DocumentName, 
                qdoc.DocumentNumber,
                query.Notes);

            grdItem.DataSource = query.LoadDataTable();
        }
        
    }
}