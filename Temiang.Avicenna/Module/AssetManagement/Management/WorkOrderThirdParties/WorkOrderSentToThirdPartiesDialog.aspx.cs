using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.AssetManagement
{
    public partial class WorkOrderSentToThirdPartiesDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Title = "Edit Sent To Third Parties : " + Request.QueryString["wono"];
                var wo = new AssetWorkOrder();
                wo.LoadByPrimaryKey(Request.QueryString["wono"]);
                txtDateSent.SelectedDate = wo.SentToThirdPartiesDateTime;
                txtLetterNo.Text = wo.LetterNo;
                
                if (!string.IsNullOrEmpty(wo.SupplierID))
                {
                    var supp = new SupplierQuery();
                    supp.Where(supp.SupplierID == wo.SupplierID);
                    cboSupplierID.DataSource = supp.LoadDataTable();
                    cboSupplierID.DataBind();
                    cboSupplierID.SelectedValue = wo.SupplierID;
                }
                else
                {
                    cboSupplierID.Items.Clear();
                    cboSupplierID.Text = string.Empty;
                }
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebindo'";
        }

        public override bool OnButtonOkClicked()
        {
            if (!Page.IsValid)
                return false;

            if (string.IsNullOrEmpty(txtLetterNo.Text))
            {
                ShowInformationHeader("Letter No is required.");
                return false;
            }

            if (string.IsNullOrEmpty(cboSupplierID.SelectedValue))
            {
                ShowInformationHeader("Supplier is required.");
                return false;
            }

            var wo = new AssetWorkOrder();
            wo.LoadByPrimaryKey(Request.QueryString["wono"]);
            wo.SentToThirdPartiesDateTime = txtDateSent.SelectedDate;
            wo.LetterNo = txtLetterNo.Text;
            wo.SupplierID = cboSupplierID.SelectedValue;
            wo.LastUpdateByUserID = AppSession.UserLogin.UserID;
            wo.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            wo.Save();

            return true;
        }

        protected void cboSupplierID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SupplierName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SupplierID"].ToString();
        }

        protected void cboSupplierID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SupplierQuery("a");
            query.Where(
                query.Or(query.SupplierID == e.Text,
                query.SupplierName.Like(searchTextContain)),
                query.IsActive == true
                );
            query.Select(query.SupplierID, query.SupplierName);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSupplierID.DataSource = dtb;
            cboSupplierID.DataBind();
        }
    }
}
