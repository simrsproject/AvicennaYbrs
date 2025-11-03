using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Budgeting
{
    public partial class BudgetingItemApprovalList : BasePage
    {
        private bool isApprovalModule
        {
            get
            {
                return Request.QueryString["Approval"] == null ? false : Request.QueryString["Approval"].Equals("1");
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.BUDGETING_ITEM_APPROVAL;


            if (!IsPostBack)
            {
                var bg = new BudgetingQuery();
                bg.Where(
                    bg.IsVoid == false,
                    bg.IsApprove == true,
                    bg.SRBudgetingVerifyStatus == "03")
                    .OrderBy(bg.Periode.Descending)
                    .Select(bg.Periode.Cast(Dal.DynamicQuery.esCastType.String));
                bg.es.Distinct = true;
                var dtb = bg.LoadDataTable();

                cboYear.Items.Clear();
                cboYear.Items.Add(new RadComboBoxItem("", "0"));
                foreach (System.Data.DataRow row in dtb.Rows) {
                    cboYear.Items.Add(new RadComboBoxItem(row["Periode"].ToString(), row["Periode"].ToString()));
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();

            RadToolBar2.Items[0].Enabled = false;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;
        }

        private void RedirectToPageDetail()
        {
            string id = string.Empty;
            
            string parentID = string.Empty;
            if (grdList.SelectedItems.Count > 0)
            {
                GridDataItem item = (GridDataItem)grdList.MasterTableView.Items[grdList.SelectedItems[0].ItemIndex];
                //id = item["ReferenceNo"].Text;
                id = item.GetDataKeyValue("TransactionNo").ToString();
            }
            else
            {
                return;
            }
           
            if (id.Equals(string.Empty)) return;

            string url = string.Format("BudgetingDetail.aspx?md={0}&id={1}&Approval={2}", "edit", id, Request.QueryString["Approval"]);
            Page.Response.Redirect(url, true);
        }

        #region Grid Outstanding
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var bdi = new BudgetingDetailItemQuery("bdi");
            var b = new BudgetingQuery("b");
            var i = new ItemQuery("i");
            var su = new ServiceUnitQuery("su");
            var coa = new ChartOfAccountsQuery("coa");
            bdi.InnerJoin(b).On(bdi.BudgetingNo == b.BudgetingNo && bdi.Revision == b.Revision)
                .InnerJoin(i).On(bdi.ItemID == i.ItemID)
                .InnerJoin(su).On(b.ServiceUnitID == su.ServiceUnitID)
                .InnerJoin(coa).On(bdi.ChartOfAccountID == coa.ChartOfAccountId)
                .Where(
                    b.Periode == cboYear.SelectedValue,
                    bdi.IsAsset == true,
                    "<bdi.Price * bdi.Qty > " + AppSession.Parameter.BudgetOfAssetNeedExtraApprovalLimit.ToString() + ">",
                    b.SRBudgetingVerifyStatus == "03"
                ).Select(
                    bdi.BudgetingNo, bdi.Revision, bdi.ChartOfAccountID, bdi.ItemID, 
                    b.ServiceUnitID, su.ServiceUnitName, coa.ChartOfAccountName,
                    i.ItemName, bdi.Qty, bdi.SRItemUnit, bdi.Price,
                    bdi.IsAssetApproved, bdi.IsAssetRejected, bdi.RejectNotes
                );
            var tbl = bdi.LoadDataTable();
            grdList.DataSource = tbl;
        }

        protected void grdList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName) {
                case "Approval": {
                        var bno = (e.Item as GridDataItem).GetDataKeyValue("BudgetingNo").ToString();
                        var rev = System.Convert.ToInt32((e.Item as GridDataItem).GetDataKeyValue("Revision"));
                        var coaid = System.Convert.ToInt32((e.Item as GridDataItem).GetDataKeyValue("ChartOfAccountID"));
                        var itemid = (e.Item as GridDataItem).GetDataKeyValue("ItemID").ToString();
                        var bdi = new BudgetingDetailItem();
                        if (bdi.LoadByPrimaryKey(bno, rev, coaid, itemid)) {
                            bdi.IsAssetApproved = true;
                            bdi.AssetApprovedBy = AppSession.UserLogin.UserID;
                            bdi.AssetApprovedDateTime = DateTime.Now;

                            bdi.Save();
                        }
                        break;
                    }
                case "Reject":
                    {
                        var bno = (e.Item as GridDataItem).GetDataKeyValue("BudgetingNo").ToString();
                        var rev = System.Convert.ToInt32((e.Item as GridDataItem).GetDataKeyValue("Revision"));
                        var coaid = System.Convert.ToInt32((e.Item as GridDataItem).GetDataKeyValue("ChartOfAccountID"));
                        var itemid = (e.Item as GridDataItem).GetDataKeyValue("ItemID").ToString();
                        var bdi = new BudgetingDetailItem();
                        if (bdi.LoadByPrimaryKey(bno, rev, coaid, itemid))
                        {
                            bdi.IsAssetRejected = true;
                            bdi.AssetRejectedBy = AppSession.UserLogin.UserID;
                            bdi.AssetRejectedDateTime = DateTime.Now;
                            bdi.RejectNotes = hfRejectionReason.Value;
                            bdi.Save();
                        }
                        break;
                    }
            }

            grdList.Rebind();
        }
        #endregion

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
        }
    }
}