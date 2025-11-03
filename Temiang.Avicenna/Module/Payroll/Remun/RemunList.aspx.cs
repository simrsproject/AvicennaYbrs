using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.Remun
{
    public partial class RemunList : BasePage
    {
        public static void PopulateMonth(RadComboBox cboMonth) {
            cboMonth.Items.Add(new RadComboBoxItem("", ""));
            cboMonth.Items.Add(new RadComboBoxItem("January", "1"));
            cboMonth.Items.Add(new RadComboBoxItem("February", "2"));
            cboMonth.Items.Add(new RadComboBoxItem("March", "3"));
            cboMonth.Items.Add(new RadComboBoxItem("April", "4"));
            cboMonth.Items.Add(new RadComboBoxItem("May", "5"));
            cboMonth.Items.Add(new RadComboBoxItem("June", "6"));
            cboMonth.Items.Add(new RadComboBoxItem("July", "7"));
            cboMonth.Items.Add(new RadComboBoxItem("August", "8"));
            cboMonth.Items.Add(new RadComboBoxItem("September", "9"));
            cboMonth.Items.Add(new RadComboBoxItem("October", "10"));
            cboMonth.Items.Add(new RadComboBoxItem("November", "11"));
            cboMonth.Items.Add(new RadComboBoxItem("December", "12"));
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.EmployeeRemun;

            if (!IsPostBack)
            {
                PopulateMonth(cboMonth);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;

            if (eventArgument == "edit")
            {
                //RedirectToPageDetail();
            }
        }

        //private void RedirectToPageDetail()
        //{
        //    string id = string.Empty;
            
        //    string parentID = string.Empty;
        //    if (grdList.SelectedItems.Count > 0)
        //    {
        //        GridDataItem item = (GridDataItem)grdList.MasterTableView.Items[grdList.SelectedItems[0].ItemIndex];
        //        //id = item["ReferenceNo"].Text;
        //        id = item.GetDataKeyValue("TransactionNo").ToString();
        //    }
        //    else
        //    {
        //        return;
        //    }
           
        //    if (id.Equals(string.Empty)) return;

        //    string url = string.Format("RemunDetail.aspx?md={0}&id={1}", "edit", id);
        //    Page.Response.Redirect(url, true);
        //}

        #region Grid Outstanding
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var remColl = new EmployeeRemunCollection();
            if (!string.IsNullOrEmpty(txtYear.Text))
            {
                remColl.Query.Where(remColl.Query.PeriodYear == txtYear.Text);
            }
            if (!string.IsNullOrEmpty(cboMonth.SelectedValue))
            {
                remColl.Query.Where(remColl.Query.PeriodMonth == cboMonth.SelectedValue);
            }
            remColl.LoadAll();

            grdList.DataSource = remColl;
        }

        #endregion
        
        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
        }
    }
}