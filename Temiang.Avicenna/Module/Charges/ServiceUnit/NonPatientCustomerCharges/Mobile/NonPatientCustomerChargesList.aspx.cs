using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Charges.Mobile
{
    public partial class NonPatientCustomerChargesList : BasePageBootstrap
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Page.Theme = "";
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.NonPatientCustomerChargesMobile;

            hfServiceUnitID.Value = AppSession.Parameter.ServiceUnitIDForCafe;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(ItemTransactionMetadata.ColumnNames.TransactionNo).ToString();
            string url = string.Format("NonPatientCustomerChargesDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }
    }
}