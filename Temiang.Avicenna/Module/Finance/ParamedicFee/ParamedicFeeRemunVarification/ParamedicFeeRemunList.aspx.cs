using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee
{
    public partial class ParamedicFeeRemunList : BasePage
    {
        public static void PopulateYear(RadComboBox cboYear) {
            var tc = new AppAutoNumberTransactionCode();
            if (tc.LoadByPrimaryKey(BusinessObject.Reference.TransactionCode.RemunerationByIdi))
            {
                var au = new AppAutoNumber();
                var cAu = new AppAutoNumberCollection();
                var qAu = new AppAutoNumberQuery();
                qAu.Where(qAu.SRAutoNumber == tc.SRAutoNumber);
                cAu.Load(qAu);

                // start from year of efective date of budgetplan
                int year = cAu[0].EffectiveDate.Value.Year;

                var lYear = new System.Collections.Generic.List<string>();
                cboYear.Items.Add(new RadComboBoxItem("", ""));
                for (var i = DateTime.Now.Year + 1; i >= year; i--)
                {
                    lYear.Add(i.ToString());
                    cboYear.Items.Add(new RadComboBoxItem(i.ToString(), i.ToString()));
                }
            }
        }
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
            ProgramID = AppConstant.Program.ParamedicFeeRemunerationByIDI;

            if (!IsPostBack)
            {
                PopulateYear(cboYear);
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

            string url = string.Format("ParamedicFeeRemunDetail.aspx?md={0}&id={1}", "edit", id);
            Page.Response.Redirect(url, true);
        }

        #region Grid Outstanding
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var remColl = new ParamedicFeeRemunByIdiCollection();
            //if (!string.IsNullOrEmpty(cboYear.SelectedValue)) {
            //    remColl.Query.Where(remColl.Query.Year == cboYear.SelectedValue);
            //}
            //if (!string.IsNullOrEmpty(cboMonth.SelectedValue))
            //{
            //    remColl.Query.Where(remColl.Query.Month == cboMonth.SelectedValue);
            //}
            if (!string.IsNullOrEmpty(txtRemunNo.Text))
            {
                remColl.Query.Where(remColl.Query.RemunNo == txtRemunNo.Text);
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