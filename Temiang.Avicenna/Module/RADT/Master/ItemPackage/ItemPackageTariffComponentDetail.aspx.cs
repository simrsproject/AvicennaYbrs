using System;
using System.Data;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ItemPackageTariffComponentDetail : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PackageItem;

            if (!IsPostBack)
            {
                StandardReference.Initialize(cboTariffType, AppEnum.StandardReference.TariffType);
                cboTariffType.SelectedValue = AppSession.Parameter.DefaultTariffType;

                var cls = new ClassCollection();
                cls.Query.Where(cls.Query.IsActive == true);
                cls.Query.Load();

                foreach (var cl in cls)
                {
                    cboClassID.Items.Add(new RadComboBoxItem(cl.ClassName, cl.ClassID));
                }
                cboClassID.SelectedValue = AppSession.Parameter.DefaultTariffClass;

                var item = new Item();
                item.LoadByPrimaryKey(Request.QueryString["itemID"]);

                txtItemID.Text = item.ItemName + " [" + item.ItemID + "]";
                chkIsDiscountInPercent.Checked = Request.QueryString["ip"] == "True";
                txtDiscountValue.Value = Convert.ToDouble(Request.QueryString["dv"]);

                grdTariff.Columns[5].Visible = !chkIsDiscountInPercent.Checked && AppSession.Parameter.HealthcareInitial == "RSCH";
                pnlDiscount.Visible = AppSession.Parameter.HealthcareInitial == "RSCH";

                switch (item.SRItemType)
                {
                    case BusinessObject.Reference.ItemType.Medical:
                    case BusinessObject.Reference.ItemType.NonMedical:
                    case BusinessObject.Reference.ItemType.Kitchen:
                        RadAjaxManagerProxy1.AjaxSettings.AddAjaxSetting(cboTariffType, txtProductPrice);
                        RadAjaxManagerProxy1.AjaxSettings.AddAjaxSetting(cboClassID, txtProductPrice);
                        RadAjaxManagerProxy1.AjaxSettings.AddAjaxSetting(cboTariffType, txtProductPriceReference);
                        RadAjaxManagerProxy1.AjaxSettings.AddAjaxSetting(cboClassID, txtProductPriceReference);
                        pnlProduct.Visible = true;

                        var tariff = Helper.Tariff.GetItemTariff(DateTime.Now.Date, cboTariffType.SelectedValue,
                                                                 cboClassID.SelectedValue, cboClassID.SelectedValue,
                                                                 Request.QueryString["itemID"],
                                                                 AppSession.Parameter.SelfGuarantor, false, AppConstant.RegistrationType.OutPatient);
                        txtProductPrice.Value = Convert.ToDouble(tariff.Price);
                        txtProductPriceReference.Value = Convert.ToDouble(tariff.Price);

                        var comps = ((ItemPackageTariffComponentCollection)Session["collItemPackageTariffComponent"]).Where(c => c.DetailItemID == Request.QueryString["itemID"]);
                        if (comps.Any())
                        {
                            txtProductPrice.Value = (double?)comps.SingleOrDefault().Price;
                        }

                        break;
                    default:
                        RadAjaxManagerProxy1.AjaxSettings.AddAjaxSetting(cboTariffType, grdTariff);
                        RadAjaxManagerProxy1.AjaxSettings.AddAjaxSetting(cboClassID, grdTariff);
                        pnlService.Visible = true;
                        break;
                }
            }
        }

        protected void grdTariff_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
                e.Item.PreRender += grdTariff_ItemPreRender;
        }

        private void grdTariff_ItemPreRender(object sender, EventArgs e)
        {
            var dataItem = sender as GridDataItem;
            if (dataItem == null || IsPostBack)
                return;

            //Display Diskon Persen, Diskon Nominal dan Total
            var nprice = (dataItem["TariffComponentName"].FindControl("txtNormalPrice") as RadNumericTextBox);
            nprice.Value = Convert.ToDouble(dataItem["Price"].Text);

            var discount = (dataItem["TariffComponentName"].FindControl("txtDiscount") as RadNumericTextBox);
            discount.Value = Convert.ToDouble(dataItem["Discount"].Text);

            var total = (dataItem["TariffComponentName"].FindControl("txtPrice") as RadNumericTextBox);
            total.Value = nprice.Value - discount.Value;

            var disc = (dataItem["TariffComponentName"].FindControl("txtDiscountPersen") as RadNumericTextBox);
            disc.Value = discount.Value / nprice.Value * 100;
        }

        private static DataTable InitTariffComponentTable()
        {
            var tempTable = new DataTable();

            var column = new DataColumn { DataType = Type.GetType("System.String"), ColumnName = "TariffComponentID" };
            tempTable.Columns.Add(column);

            column = new DataColumn { DataType = Type.GetType("System.Decimal"), ColumnName = "Price" };
            tempTable.Columns.Add(column);

            column = new DataColumn { DataType = Type.GetType("System.Decimal"), ColumnName = "Discount" };
            tempTable.Columns.Add(column);

            column = new DataColumn { DataType = Type.GetType("System.Boolean"), ColumnName = "IsAllowDiscount" };
            tempTable.Columns.Add(column);

            column = new DataColumn { DataType = Type.GetType("System.Boolean"), ColumnName = "IsAllowVariable" };
            tempTable.Columns.Add(column);

            column = new DataColumn { DataType = Type.GetType("System.String"), ColumnName = "TariffComponentName" };
            tempTable.Columns.Add(column);

            column = new DataColumn { DataType = Type.GetType("System.Boolean"), ColumnName = "IsTariffParamedic" };
            tempTable.Columns.Add(column);

            return tempTable;
        }

        protected void grdTariff_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var comps = ((ItemPackageTariffComponentCollection)Session["collItemPackageTariffComponent"]).Where(c => c.DetailItemID == Request.QueryString["itemID"]);
            if (comps.Any())
            {
                var tariff = TariffComponents;

                foreach (DataRow tc in tariff.Rows)
                {
                    var cc = comps.SingleOrDefault(c => c.TariffComponentID == tc["TariffComponentID"].ToString()) ;
                    if (cc != null)
                    {
                        tc["Price"] = cc.Price;
                        tc["Discount"] = cc.Discount;
                    }
                    else
                    {
                        tc["Price"] = 0;
                        tc["Discount"] = 0;
                    }
                }

                tariff.AcceptChanges();

                grdTariff.DataSource = tariff;
            }
            else
                grdTariff.DataSource = TariffComponents;
        }

        private DataTable TariffComponents
        {
            get
            {
                var tariffColl = new TariffComponentCollection();
                tariffColl.Query.OrderBy(tariffColl.Query.TariffComponentID.Ascending);
                tariffColl.LoadAll();

                var coll = Helper.Tariff.GetItemTariffComponentCollection(DateTime.Now.Date, cboTariffType.SelectedValue,
                                cboClassID.SelectedValue, Request.QueryString["itemID"]);

                var dtb = InitTariffComponentTable();

                foreach (var entity in coll)
                {
                    var newRow = dtb.NewRow();
                    newRow["TariffComponentID"] = entity.TariffComponentID;
                    newRow["Price"] = entity.Price;
                    if (chkIsDiscountInPercent.Checked)
                        newRow["Discount"] = entity.Price*Convert.ToDecimal(txtDiscountValue.Value)/100;
                    else
                        newRow["Discount"] = 0D;
                    newRow["IsAllowDiscount"] = entity.IsAllowDiscount;
                    newRow["IsAllowVariable"] = entity.IsAllowVariable;

                    var comp = new TariffComponent();
                    comp.LoadByPrimaryKey(entity.TariffComponentID);
                    newRow["TariffComponentName"] = comp.TariffComponentName;
                    newRow["IsTariffParamedic"] = comp.IsTariffParamedic;

                    dtb.Rows.Add(newRow);
                }

                return dtb;
            }
        }

        public override bool OnButtonOkClicked()
        {
            var item = new Item();
            item.LoadByPrimaryKey(Request.QueryString["itemID"]);
            switch (item.SRItemType)
            {
                case BusinessObject.Reference.ItemType.Medical:
                case BusinessObject.Reference.ItemType.NonMedical:
                case BusinessObject.Reference.ItemType.Kitchen:
                    {
                        var comps = ((ItemPackageTariffComponentCollection)Session["collItemPackageTariffComponent"]).Where(c => c.DetailItemID == Request.QueryString["itemID"]);
                        var comp = comps.SingleOrDefault(c => c.DetailItemID == Request.QueryString["itemID"]) ??
                                       ((ItemPackageTariffComponentCollection)Session["collItemPackageTariffComponent"]).AddNew();

                        comp.DetailItemID = Request.QueryString["ItemID"];
                        comp.TariffComponentID = string.Empty;
                        comp.Price = Convert.ToDecimal(txtProductPrice.Value);
                        comp.Discount = Convert.ToDecimal(0);
                        break;
                    }
                default:
                    {
                        var comps = ((ItemPackageTariffComponentCollection)Session["collItemPackageTariffComponent"]).Where(c => c.DetailItemID == Request.QueryString["itemID"]);

                        foreach (GridDataItem dataItem in grdTariff.MasterTableView.Items)
                        {
                            var comp = comps.SingleOrDefault(c => c.DetailItemID == Request.QueryString["itemID"] &&
                                                                  c.TariffComponentID == dataItem["TariffComponentID"].Text) ??
                                       ((ItemPackageTariffComponentCollection)Session["collItemPackageTariffComponent"]).AddNew();

                            comp.DetailItemID = Request.QueryString["ItemID"];
                            comp.TariffComponentID = dataItem["TariffComponentID"].Text;
                            comp.Price = Convert.ToDecimal((dataItem.FindControl("txtPrice") as RadNumericTextBox).Value);
                            if (chkIsDiscountInPercent.Checked)
                                comp.Discount = comp.Price*Convert.ToDecimal(txtDiscountValue.Value)/100;
                            else
                                comp.Discount = Convert.ToDecimal((dataItem.FindControl("txtDiscount") as RadNumericTextBox).Value);

                            //jika discount nominal = 0
                            if (comp.Discount == 0)
                                comp.Discount = comp.Price * Convert.ToDecimal((dataItem.FindControl("txtDiscountPersen") as RadNumericTextBox).Value) / 100;

                            // 
                            //if (comp.Price <= 0)
                            //{
                            //    ShowInformationHeader("Price must be greater than 0.");
                            //    return false;
                            //}
                            if (comp.Price < 0)
                            {
                                ShowInformationHeader("Invalid price.");
                                return false;
                            }
                            if (comp.Discount < 0)
                            {
                                ShowInformationHeader("Discount can't be less than 0.");
                                return false;
                            }
                            if (comp.Discount > comp.Price)
                            {
                                ShowInformationHeader("Discount can't be greater than Price.");
                                return false;
                            }
                        }
                        break;
                    }
            }
            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        protected void cboTariffType_OnSelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            grdTariff.Rebind();
        }
    }
}
