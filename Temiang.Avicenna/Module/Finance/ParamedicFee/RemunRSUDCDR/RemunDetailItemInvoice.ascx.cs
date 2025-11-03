using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee
{
    public partial class RemunDetailItemInvoice : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            cboGuarantorID.Items.Clear();
            var guars = new GuarantorCollection();
            guars.Query.Where(guars.Query.IsActive == true)
                .OrderBy(guars.Query.GuarantorName.Ascending);
            guars.Query.Load();
            cboGuarantorID.DataSource = guars;
            cboGuarantorID.DataBind();

            if (DataItem is GridInsertionObject)
            {
                return;
            }
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {

        }

        #region Method & Event TextChanged

        protected void Page_Init(object sender, EventArgs e)
        {
            cboGuarantorID.ItemDataBound += cboGuarantorID_ItemDataBound;
        }

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var entity = ((Guarantor)e.Item.DataItem);
            e.Item.Text = entity.GuarantorName + " - " + entity.GuarantorID;
            e.Item.Value = entity.GuarantorID;

        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdInvoices.Rebind();
        }

        protected void grdInvoices_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdInvoices.DataSource = Getinvoices();
        }

        private DataTable Getinvoices() {
            var g = new GuarantorQuery("g");
            var iv = new InvoicesQuery("iv");
            var ivi = new InvoicesItemQuery("ivi");
            var fiv = new ServiceFeeRemunRsucdrInvoicesQuery("fiv");
            var sfr = new ServiceFeeRemunRsucdrQuery("sfr");

            g.InnerJoin(iv).On(g.GuarantorID == iv.GuarantorID && iv.IsInvoicePayment == false)
                .LeftJoin(fiv).On(iv.InvoiceNo == fiv.InvoiceNo)
                .LeftJoin(sfr).On(sfr.RemunID == fiv.RemunID && sfr.IsVoid == false)
                .InnerJoin(ivi).On(iv.InvoiceNo == ivi.InvoiceNo)
                .Where(g.IsParamedicFeeRemun == true, sfr.RemunID.IsNull())
                .GroupBy(
                    iv.InvoiceNo
                ).Select(
                    iv.InvoiceNo, ivi.Amount.Sum()
                );
                
            if (!string.IsNullOrEmpty(cboGuarantorID.SelectedValue)) {
                g.Where(g.GuarantorID == cboGuarantorID.SelectedValue);
            }
            if (!string.IsNullOrEmpty(txtInvoiceNo.Text)) {
                g.Where(iv.InvoiceNo == txtInvoiceNo.Text);
            }
            g.es.Top = 150;

            return g.LoadDataTable();
        }
        #endregion

        #region Properties for return entry value
        public Dictionary<string, decimal> InvoicesAmount {
            get {
                Dictionary<string, decimal> invs = new Dictionary<string, decimal>();
                foreach (GridDataItem gdi in grdInvoices.MasterTableView.Items.Cast<GridDataItem>())
                {
                    var chkBox = ((System.Web.UI.WebControls.CheckBox)gdi.FindControl("chkDetail"));
                    if (chkBox != null)
                    {
                        if (chkBox.Checked)
                        {
                            invs.Add(gdi.GetDataKeyValue("InvoiceNo").ToString(), System.Convert.ToDecimal(gdi["Amount"].Text));
                        }
                    }
                }

                return invs;
            }
        }
        #endregion

        
    }
}