using System;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.AssetManagement.Management
{
    public partial class SanitationWasteInformationList : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.SanitationWasteInformation;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRWasteType, AppEnum.StandardReference.WasteType);
            }
        }

        protected void grdItemBalance_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemBalance.DataSource = ItemBalances;
        }

        private DataTable ItemBalances
        {
            get
            {
                var balanceQ = new SanitationWasteItemBalanceQuery("a");
                var itemQ = new AppStandardReferenceItemQuery("b");
                balanceQ.InnerJoin(itemQ).On(itemQ.StandardReferenceID == AppEnum.StandardReference.WasteType.ToString() && itemQ.ItemID == balanceQ.SRWasteType);

                balanceQ.Select
                (
                    balanceQ.SRWasteType,
                    itemQ.ItemName.As("WasteTypeName"),
                    balanceQ.Balance
                );

                if (!string.IsNullOrEmpty(cboSRWasteType.SelectedValue))
                    balanceQ.Where(balanceQ.SRWasteType == cboSRWasteType.SelectedValue);

                if (chkOnlyInStock.Checked)
                    balanceQ.Where(balanceQ.Balance > 0);

                balanceQ.es.Top = AppSession.Parameter.MaxResultRecord;

                balanceQ.es.Distinct = true;

                balanceQ.OrderBy(itemQ.ItemName.Ascending);

                return balanceQ.LoadDataTable();
            }
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            grdItemBalance.Rebind();
        }

        protected void chkOnlyInStock_CheckedChanged(object sender, EventArgs e)
        {
            grdItemBalance.Rebind();
        }
    }
}