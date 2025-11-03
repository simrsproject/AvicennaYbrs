using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class ItemConsumptionPackageDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ServiceUnitTransaction;

            if (!IsPostBack)
            {
                var item = new Item();
                item.LoadByPrimaryKey(Request.QueryString["item"]);

                Title = "Item Consumption : " + item.ItemName + " [" + Request.QueryString["item"] + "]";

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(Request.QueryString["unit"]);
                ViewState["LocationID" + Request.UserHostName + Request.QueryString["pageId"]] = unit.GetMainLocationId(unit.ServiceUnitID);

                cboItemID.DataSource = PopulateProductItem(Request.QueryString["item"]);
                cboItemID.DataBind();
                cboItemID.SelectedValue = Request.QueryString["item"];
            }
        }

        private DataTable PopulateProductItem(string parameter)
        {
            if (ViewState["LocationID" + Request.UserHostName + Request.QueryString["pageId"]] == null)
            {
                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(Request.QueryString["unit"]);
                ViewState["LocationID" + Request.UserHostName + Request.QueryString["pageId"]] = unit.GetMainLocationId(unit.ServiceUnitID);
            }

            DataTable tbl = null;

            try
            {
                string searchTextContain = string.Format("%{0}%", parameter);
                var query = new ItemQuery("a");
                var balance = new ItemBalanceQuery("d");

                query.es.Top = 15;
                query.Select
                    (
                        query.ItemID,
                        query.ItemName,
                        (balance.Balance.Coalesce("0") - balance.Booking.Coalesce("0")).As("Balance"),
                        query.SRItemType,
                        "<'' AS ServiceUnitID>"
                    );
                query.InnerJoin(balance).On
                    (
                        query.ItemID == balance.ItemID &
                        balance.LocationID == ViewState["LocationID" + Request.UserHostName + Request.QueryString["pageId"]]
                    );
                query.Where
                    (
                        query.SRItemType.In
                            (
                                ItemType.Medical,
                                ItemType.NonMedical
                            ),
                        query.Or
                            (
                                query.ItemName.Like(searchTextContain),
                                query.ItemID.Like(searchTextContain)
                            ),
                        query.IsActive == true
                    );
                query.OrderBy(query.ItemName.Ascending);

                tbl = query.LoadDataTable();
                String item2 = string.Empty;

                foreach (DataRow row in tbl.Rows)
                {
                    var item1 = (string)row["ItemID"];
                    if (item1 != item2)
                        item2 = (string)row["ItemID"];
                    else
                        row.Delete();
                }

                tbl.AcceptChanges();
            }
            catch
            {
            }

            return tbl;
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            cboItemID.DataSource = PopulateProductItem(e.Text);
            cboItemID.DataBind();
        }

        public override bool OnButtonOkClicked()
        {
            if (!IsValid || string.IsNullOrEmpty(cboItemID.SelectedValue))
                return false;

            var item = ((TransChargesItemCollection)Session["collTransChargesItem" + Request.UserHostName + Request.QueryString["pageId"]]).FindByPrimaryKey(Request.QueryString["trans"],
                                                                                                       Request.QueryString["seq"]);
            if (item != null)
            {
                item.ItemID = cboItemID.SelectedValue;
                item.ItemName = cboItemID.Text;
            }

            var cons = ((TransChargesItemConsumptionCollection)Session["collTransChargesItemConsumption" + Request.UserHostName + Request.QueryString["pageId"]]).FindByPrimaryKey(
                    Request.QueryString["trans"], Request.QueryString["seq"], Request.QueryString["item"]);
            if (cons != null)
            {
                cons.DetailItemID = cboItemID.SelectedValue;
                cons.ItemName = cboItemID.Text;
            }
            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.rebind = 'rebind'";
        }
    }
}
