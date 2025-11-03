using System;
using System.Data;
using System.Web.UI;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class PatientChargesDetailItemComp : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.MedicalRecordHistory;

            ((Button)Helper.FindControlRecursive(Page, "btnOk")).Visible = false;
            ((Button)Helper.FindControlRecursive(Page, "btnCancel")).Visible = false;

            if (!IsPostBack)
            {
                string transNo = Request.QueryString["tno"].ToString();
                string seqNo = Request.QueryString["sno"].ToString();

                var tx = new TransChargesItem();
                tx.LoadByPrimaryKey(transNo, seqNo);

                string itemId = tx.ItemID;

                var i = new Item();
                i.LoadByPrimaryKey(itemId);

                txtItemName.Text = i.ItemName;
            }

        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = TransChargesItemComps;
        }

        private DataTable TransChargesItemComps
        {
            get
            {
                string transNo = Request.QueryString["tno"].ToString();
                string seqNo = Request.QueryString["sno"].ToString();

                var tcic = new TransChargesItemCompQuery("a");
                var tc = new TariffComponentQuery("b");
                tcic.InnerJoin(tc).On(tcic.TariffComponentID == tc.TariffComponentID);
                tcic.Where(tcic.TransactionNo == transNo, tcic.SequenceNo == seqNo);
                tcic.OrderBy(tcic.TariffComponentID.Ascending);

                tcic.Select(tcic.TariffComponentID, tc.TariffComponentName, tcic.Price, tcic.DiscountAmount);

                DataTable dtb = tcic.LoadDataTable();

                return dtb;
                
            }
        }
    }
}
