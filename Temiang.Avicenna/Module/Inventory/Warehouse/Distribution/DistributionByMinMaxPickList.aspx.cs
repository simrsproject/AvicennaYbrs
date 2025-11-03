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
    public partial class DistributionByMinMaxPickList : BasePageDialog
    {
        private bool _isNeedValidatedMax;

        public int GetInt(object o)
        {
            return o.ToInt();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.Distribution;

            var loc = new Location();
            if (loc.LoadByPrimaryKey(Request.QueryString["tloc"]))
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
                txtToLocation.Text = loc.LocationName;
            }
            else
            {
                _isNeedValidatedMax = false;
                txtToLocation.Text = string.Empty;
            }


            if (!string.IsNullOrEmpty(Request.QueryString["floc"]))
            {
                loc = new Location();
                loc.LoadByPrimaryKey(Request.QueryString["floc"]);
                
                txtFromLocation.Text = loc.LocationName;
            }
            else
                txtFromLocation.Text = string.Empty;

            grdDetail.Columns.FindByUniqueName("BalanceFrom").Visible = AppSession.Parameter.IsDistributionRequestPickListWithBalanceToInfo;
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
                Session.Remove("DistributionPicklist:Selection");
                Session.Remove("DistributionPicklist:ItemSelected");
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

                row["FromServiceUnitID"] = Request.QueryString["fsu"].ToString();
                row["FromLocationID"] = Request.QueryString["floc"].ToString();
                row["ToServiceUnitID"] = Request.QueryString["tsu"].ToString();
                row["ToLocationID"] = Request.QueryString["tloc"].ToString();
                row["SRItemType"] = Request.QueryString["it"].ToString();
                row["ItemGroupID"] = Request.QueryString["ig"].ToString();
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
            var balfrom = new ItemBalanceQuery("balto");
            query.LeftJoin(balfrom).On(balfrom.LocationID == Request.QueryString["floc"] && balfrom.ItemID == query.ItemID);

            query.Where(
                query.LocationID == Request.QueryString["tloc"]
                );

            var itemGroupID = Page.Request.QueryString["ig"];
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
                balfrom.Balance.Coalesce("0").As("BalanceFrom"),
                @"<'' AS FromServiceUnitID>",
                @"<'' AS FromLocationID>",
                @"<'' AS ToServiceUnitID>",
                @"<'' AS ToLocationID>",
                @"<'' AS SRItemType>",
                @"<'' AS ItemGroupID>"
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
                Session["DistributionPicklist:ItemSelected"] = value;
            }
        }
        private DataTable RequestItemSelectionDataTable
        {
            get
            {
                var result = Session["DistributionPicklist:Selection"];
                if (result == null)
                {
                    return null;
                }
                return (DataTable)result;
            }
            set
            {
                Session["DistributionPicklist:Selection"] = value;
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