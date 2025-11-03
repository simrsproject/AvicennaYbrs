using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.BudgetingByItem
{
    public partial class BudgetingList : BasePage
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
            if (isApprovalModule)
                ProgramID = AppConstant.Program.BUDGETING_APPROVAL;
            else
                ProgramID = AppConstant.Program.BUDGETING;


            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, BusinessObject.Reference.TransactionCode.BudgetPlan,
                    !isApprovalModule);
                StandardReference.InitializeIncludeSpace(cboSRBudgetingGroup, AppEnum.StandardReference.BudgetingGroup);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();

            if (isApprovalModule) {
                RadToolBar2.Items[0].Enabled = false;
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;

            if (eventArgument == "edit")
            {
                RedirectToPageDetail();
            }
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
            int iCount;

            BudgetingCollection bColl = new BudgetingCollection();

            var tbl = bColl.LoadByPage(txtTransactionNo.Text, txtYear.Text, cboServiceUnitID.SelectedValue, cboSRBudgetingGroup.SelectedValue, true,
                ((grdList.CurrentPageIndex * grdList.PageSize) + 1),
                    ((grdList.CurrentPageIndex + 1) * grdList.PageSize), out iCount);

            grdList.VirtualItemCount = iCount;
            grdList.DataSource = tbl;
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                var status = ((DataRowView)dataItem.DataItem).Row.Field<string>("SRBudgetingVerifyStatus").ToString();
                switch (status) {
                    case "01": {
                        dataItem.ForeColor = System.Drawing.Color.Black;
                        break;
                    }
                    case "02":
                        {
                            dataItem.ForeColor = System.Drawing.Color.Red;
                            break;
                        }
                    case "03":
                        {
                            dataItem.ForeColor = System.Drawing.Color.Green;
                            break;
                        }
                }
            }
        }

        #endregion
        
        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
        }
    }
}