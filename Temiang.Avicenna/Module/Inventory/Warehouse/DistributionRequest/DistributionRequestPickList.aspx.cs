using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class DistributionRequestPickList : BasePageDialog
    {
        private bool _isNeedValidatedMax;

        public int GetInt(object o)
        {
            return o.ToInt();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.DistributionRequest;

            var loc = new Location();
            if (loc.LoadByPrimaryKey(Request.QueryString["lid"]))
            {
                switch (Request.QueryString["it"])
                {
                    case BusinessObject.Reference.ItemType.Medical:
                        _isNeedValidatedMax = loc.IsValidateMaxValueOnDistReqForIpm ?? false;
                        break;
                    case BusinessObject.Reference.ItemType.NonMedical:
                        _isNeedValidatedMax = loc.IsValidateMaxValueOnDistReqForIpnm ?? false;
                        break;
                    case BusinessObject.Reference.ItemType.Kitchen:
                        _isNeedValidatedMax = loc.IsValidateMaxValueOnDistReqForIk ?? false;
                        break;
                }
                txtFromLocation.Text = loc.LocationName;
            }
            else
            {
                _isNeedValidatedMax = false;
                txtFromLocation.Text = string.Empty;
            }
                

            if (!string.IsNullOrEmpty(Request.QueryString["tlid"]))
            {
                loc = new Location();
                loc.LoadByPrimaryKey(Request.QueryString["tlid"]);
                txtToLocation.Text = loc.LocationName;
            }
            else
                txtToLocation.Text = string.Empty;

            grdDetail.Columns.FindByUniqueName("BalanceTo").Visible = AppSession.Parameter.IsDistributionRequestPickListWithBalanceToInfo;
            grdDetail.Columns.FindByUniqueName("tempColQtyInput").Visible = !_isNeedValidatedMax;
            grdDetail.Columns.FindByUniqueName("tempColQtyInputWithMax").Visible = _isNeedValidatedMax;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                StoreEntryValue();
            }
            else
            {
                Session.Remove("DistRequest:Selection");
                Session.Remove("DistRequest:ItemSelected");
            }
        }

        private void StoreEntryValue()
        {
            var dtb = RequestItemSelectionDataTable;
            if (dtb == null) return;
            foreach (GridDataItem dataItem in grdDetail.MasterTableView.Items)
            {
                var itemId = dataItem["ItemID"].Text;
                var row = dtb.Rows.Find(itemId);
                row["IsSelect"] = ((CheckBox)dataItem.FindControl("detailChkbox")).Checked;
                if (_isNeedValidatedMax)
                    row["QtyInput"] = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQtyInput2")).Value ?? 0);
                else
                    row["QtyInput"] = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQtyInput")).Value ?? 0);
            }
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack)
            {
                RequestItemSelectionDataTable = LoadRequestItemSelectionDataTable();
            }
            grdDetail.DataSource = RequestItemSelectionDataTable;
        }

        private DataTable LoadRequestItemSelectionDataTable()
        {
            var query = new ItemBalanceQuery("a");
            var qrRef = new AppStandardReferenceItemQuery("c");

            if (Request.QueryString["it"] == BusinessObject.Reference.ItemType.Medical)
            {
                var qrItemMed = new ItemProductMedicQuery("b");
                query.InnerJoin(qrItemMed).On(query.ItemID == qrItemMed.ItemID && qrItemMed.IsConsignment == false);
                query.LeftJoin(qrRef).On(
                    qrItemMed.SRItemUnit == qrRef.ItemID &&
                    qrRef.StandardReferenceID == AppEnum.StandardReference.ItemUnit
                    );

                query.Select(
                    qrItemMed.SRItemUnit,
                    qrItemMed.SRPurchaseUnit,
                    qrItemMed.ConversionFactor
                    );

            }
            else if (Request.QueryString["it"] == BusinessObject.Reference.ItemType.NonMedical)
            {
                var qrItemMed = new ItemProductNonMedicQuery("b");
                query.InnerJoin(qrItemMed).On(query.ItemID == qrItemMed.ItemID && qrItemMed.IsConsignment == false);
                query.LeftJoin(qrRef).On(
                    qrItemMed.SRItemUnit == qrRef.ItemID &&
                    qrRef.StandardReferenceID == AppEnum.StandardReference.ItemUnit
                    );

                query.Select(
                    qrItemMed.SRItemUnit,
                    qrItemMed.SRPurchaseUnit,
                    qrItemMed.ConversionFactor
                    );
            }
            else
            {
                var qrItemMed = new ItemKitchenQuery("b");
                query.InnerJoin(qrItemMed).On(query.ItemID == qrItemMed.ItemID);
                query.LeftJoin(qrRef).On(
                    qrItemMed.SRItemUnit == qrRef.ItemID &&
                    qrRef.StandardReferenceID == AppEnum.StandardReference.ItemUnit
                    );

                query.Select(
                    qrItemMed.SRItemUnit,
                    qrItemMed.SRPurchaseUnit,
                    qrItemMed.ConversionFactor
                    );
            }

            var qrItem = new ItemQuery("d");
            query.InnerJoin(qrItem).On(query.ItemID == qrItem.ItemID && qrItem.IsActive == true);
            var balto = new ItemBalanceQuery("balto");
            query.LeftJoin(balto).On(balto.LocationID == Request.QueryString["tlid"] && balto.ItemID == query.ItemID);

            query.Where(
                query.LocationID == Request.QueryString["lid"]
                );

            var itemGroupID = Page.Request.QueryString["igid"];
            if (!string.IsNullOrEmpty(itemGroupID))
                query.Where(qrItem.ItemGroupID == itemGroupID);

            if (AppSession.Parameter.IsDistributionRequestOnlyForUnderMinValue)
                query.Where(query.Balance < query.Minimum);
            else
                query.Where(query.Balance <= query.Minimum, query.Maximum > 0);

            query.Select(
                @"<0 AS IsSelect>",
                query.ItemID,
                query.Minimum,
                query.Maximum,
                query.Balance,
                (query.Maximum - query.Balance).As("QtyMax"),
                (query.Maximum - query.Balance).As("QtyInput"),
                qrItem.ItemName,
                balto.Balance.Coalesce("0").As("BalanceTo")
                );

            query.OrderBy(qrItem.ItemName.Ascending);

            var dtb = query.LoadDataTable();
            dtb.PrimaryKey = new[] { dtb.Columns["ItemID"] };
            return dtb;
        }

        public override bool OnButtonOkClicked()
        {
            RequestItemSelectedDataTable =
                RequestItemSelectionDataTable.Select().Where(
                    row => (1.Equals(row["IsSelect"]) && Convert.ToDecimal(row["QtyInput"]) > 0)).CopyToDataTable();
            return true;
        }
        private DataTable RequestItemSelectedDataTable
        {
            set
            {
                Session["DistRequest:ItemSelected"] = value;
            }
        }
        private DataTable RequestItemSelectionDataTable
        {
            get
            {
                var result = Session["DistRequest:Selection"];
                if (result == null)
                {
                    return null;
                }
                return (DataTable)result;
            }
            set
            {
                Session["DistRequest:Selection"] = value;
            }
        }
        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            //StoreEntryValue();
            var dtb = RequestItemSelectionDataTable;
            var value = ((CheckBox)sender).Checked;
            foreach (DataRow row in dtb.Rows)
            {
                row["IsSelect"] = value;
            }
            grdDetail.Rebind();
        }
    }
}