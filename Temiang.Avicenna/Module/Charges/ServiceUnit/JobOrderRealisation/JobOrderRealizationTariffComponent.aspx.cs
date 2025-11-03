using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class JobOrderRealizationTariffComponent : BasePageDialog
    {
        private AppAutoNumberLast _filmAutoNumber;
        private string _srRegistrationType, _serviceUnitId;

        private DataTable TariffComponents
        {
            get
            {
                DataTable dtb;
                InitTariffComponentTable(out dtb);

                var hd = new TransCharges();
                hd.LoadByPrimaryKey(Request.QueryString["joNo"]);

                if (!string.IsNullOrEmpty(hd.PackageReferenceNo))
                {
                    var tariffColl = new TransChargesItemCompCollection();
                    tariffColl.Query.Where(tariffColl.Query.TransactionNo == hd.TransactionNo,
                                           tariffColl.Query.SequenceNo == Request.QueryString["seqNo"]);
                    tariffColl.Query.OrderBy(tariffColl.Query.TariffComponentID.Ascending);
                    tariffColl.LoadAll();

                    foreach (var entity in tariffColl)
                    {
                        var newRow = dtb.NewRow();
                        newRow["TariffComponentID"] = entity.TariffComponentID;
                        newRow["Price"] = entity.Price;
                        newRow["IsAllowDiscount"] = false;
                        newRow["IsAllowVariable"] = false;

                        var comp = new BusinessObject.TariffComponent();
                        comp.LoadByPrimaryKey(entity.TariffComponentID);
                        newRow["TariffComponentName"] = comp.TariffComponentName;
                        newRow["IsTariffParamedic"] = comp.IsTariffParamedic;

                        newRow["FeeDiscountPercentage"] = entity.FeeDiscountPercentage ?? 0;
                        newRow["FeeDiscount"] = entity.FeeDiscount ?? 0;

                        dtb.Rows.Add(newRow);
                    }
                }
                else
                {
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(hd.RegistrationNo);

                    var grr = new Guarantor();
                    grr.LoadByPrimaryKey(reg.GuarantorID);

                    var tariffDate = grr.TariffCalculationMethod == 1
                        ? reg.RegistrationDate.Value.Date
                        : hd.ExecutionDate.Value.Date;

                    var tariffColl = new TariffComponentCollection();
                    tariffColl.Query.OrderBy(tariffColl.Query.TariffComponentID.Ascending);
                    tariffColl.LoadAll();

                    var coll = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, grr.SRTariffType,
                        reg.ChargeClassID, Request.QueryString["itemID"]);
                    if (!coll.Any())
                        coll = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, grr.SRTariffType,
                            AppSession.Parameter.DefaultTariffClass, Request.QueryString["itemID"]);
                    if (!coll.Any())
                        coll = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, AppSession.Parameter.DefaultTariffType,
                            reg.ChargeClassID, Request.QueryString["itemID"]);
                    if (!coll.Any())
                        coll = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, AppSession.Parameter.DefaultTariffType,
                            AppSession.Parameter.DefaultTariffClass, Request.QueryString["itemID"]);

                    foreach (var entity in coll)
                    {
                        var newRow = dtb.NewRow();
                        newRow["TariffComponentID"] = entity.TariffComponentID;
                        newRow["Price"] = entity.Price;
                        newRow["IsAllowDiscount"] = entity.IsAllowDiscount;
                        newRow["IsAllowVariable"] = entity.IsAllowVariable;

                        var comp = new BusinessObject.TariffComponent();
                        comp.LoadByPrimaryKey(entity.TariffComponentID);
                        newRow["TariffComponentName"] = comp.TariffComponentName;
                        newRow["IsTariffParamedic"] = comp.IsTariffParamedic;

                        newRow["FeeDiscountPercentage"] = 0;
                        newRow["FeeDiscount"] = 0;

                        dtb.Rows.Add(newRow);
                    }
                }

                return dtb;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //ProgramID = Request.QueryString["disch"] == "0" ? AppConstant.Program.JobOrderRealisation : AppConstant.Program.JobOrderRealisationVerification;

            if (!IsPostBack)
            {
                var hd = new TransCharges();
                hd.LoadByPrimaryKey(Request.QueryString["joNo"]);

                var reg = new Registration();
                reg.LoadByPrimaryKey(hd.RegistrationNo);

                _srRegistrationType = reg.SRRegistrationType;
                _serviceUnitId = hd.ToServiceUnitID;

                var grr = new Guarantor();
                grr.LoadByPrimaryKey(reg.GuarantorID);

                var tariffDate = grr.TariffCalculationMethod == 1
                    ? reg.RegistrationDate.Value.Date
                    : hd.ExecutionDate.Value;

                var param = new ParamedicQuery("a");
                var unit = new ServiceUnitParamedicQuery("b");

                param.Select
                    (
                        param.ParamedicID,
                        param.ParamedicName
                    );
                param.InnerJoin(unit).On(param.ParamedicID == unit.ParamedicID);
                param.Where
                    (
                        unit.ServiceUnitID == hd.ToServiceUnitID,
                        param.IsActive == true
                    );

                var tbl = param.LoadDataTable();

                cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (DataRow row in tbl.Rows)
                {
                    cboParamedicID.Items.Add(new RadComboBoxItem((string)row["ParamedicName"], (string)row["ParamedicID"]));
                }

                StandardReference.InitializeIncludeSpace(cboDiscountReason, AppEnum.StandardReference.DiscountReason);

                if (Request.QueryString["type"] == BusinessObject.Reference.ItemType.Medical ||
                    Request.QueryString["type"] == BusinessObject.Reference.ItemType.NonMedical)
                    pnlNonComponent.Visible = true;
                else
                {
                    pnlComponent.Visible = true;

                    grdTariff.MasterTableView.Columns[3].Visible = AppSession.Parameter.TariffComponentPriceVisible;
                    grdTariff.MasterTableView.Columns[4].Visible = AppSession.Parameter.TariffComponentPriceVisible;
                }

                var charges = (TransChargesItemCollection)Session["collTransChargesItem" + Request.UserHostName];
                foreach (var entity in charges.Where(entity => entity.SequenceNo == Request.QueryString["seqNo"]))
                {
                    cboParamedicID.SelectedValue = entity.ParamedicID;
                    txtFilmNo.Text = entity.FilmNo;
                    cboDiscountReason.SelectedValue = entity.SRDiscountReason;

                    if (pnlNonComponent.Visible)
                    {
                        txtPrice1.Value = Convert.ToDouble(entity.Price);

                        var tariff = (Helper.Tariff.GetItemTariff(tariffDate, grr.SRTariffType, hd.ClassID, hd.ClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                      Helper.Tariff.GetItemTariff(tariffDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, hd.ClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                                     (Helper.Tariff.GetItemTariff(tariffDate, AppSession.Parameter.DefaultTariffType, hd.ClassID, hd.ClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                      Helper.Tariff.GetItemTariff(tariffDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, hd.ClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType));

                        chkIsVariable.Enabled = tariff.IsAllowVariable ?? false;
                        chkIsDiscount.Enabled = tariff.IsAllowDiscount ?? false;

                        txtDiscountAmount1.Value = Convert.ToDouble(entity.DiscountAmount);
                        if (chkIsDiscount.Enabled && txtDiscountAmount1.Value > 0) chkIsDiscount.Checked = true;
                    }
                    break;
                }

                if (string.IsNullOrEmpty(cboParamedicID.SelectedValue))
                {
                    var psdColl = new ParamedicScheduleDateCollection();
                    DataTable dtb = psdColl.GetParamedicID(hd.ToServiceUnitID, _srRegistrationType);

                    if (dtb.Rows.Count == 1)
                    {
                        cboParamedicID.SelectedValue = dtb.Rows[0]["ParamedicID"].ToString();
                        cboParamedicID.Text = dtb.Rows[0]["ParamedicName"].ToString();
                    }
                }

                cboParamedicID.AutoPostBack = pnlComponent.Visible;

                if (string.IsNullOrWhiteSpace(txtFilmNo.Text))
                {
                    if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP" && AppSession.Parameter.HealthcareInitial == "RSI")
                    {
                        var entity = charges.Where(c => c.SequenceNo == Request.QueryString["seqNo"]).Single();
                        var i = new Item();
                        i.LoadByPrimaryKey(entity.ItemID);
                        if (i.SRItemType != ItemType.Radiology) return;
                        if (i.ItemIDExternal == "CR") _filmAutoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.CRRadiologyFilmNo);
                        else if (i.ItemIDExternal == "CT") _filmAutoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.CTRadiologyFilmNo);
                        else return;
                        txtFilmNo.Text = _filmAutoNumber.LastCompleteNumber;
                    }
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

            var comp = FindTransChargesItemComp(dataItem["TariffComponentID"].Text);

            var price = (dataItem["TariffComponentName"].FindControl("txtPrice") as RadNumericTextBox);
            price.ReadOnly = !(dataItem["IsAllowVariable"].Text == "True");
            price.Value = (comp == null ? Convert.ToDouble(dataItem["Price"].Text) : (double)comp.Price);

            var validPrice = (dataItem["TariffComponentName"].FindControl("rfvPrice") as RequiredFieldValidator);
            validPrice.Visible = (dataItem["IsAllowVariable"].Text == "True");

            var discount = (dataItem["TariffComponentName"].FindControl("txtDiscount") as RadNumericTextBox);
            discount.ReadOnly = !((dataItem["IsAllowDiscount"].Text == "True")&& (!AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare));
            discount.MaxValue = (comp == null ? Convert.ToDouble(dataItem["Price"].Text) : (double)comp.Price);
            discount.Value = (comp == null ? 0D : (((double?)comp.DiscountAmount) ?? 0));
            /*DiscountAmount itu sudah mix sama discount FeeDiscount, jadi harus dipecah dulu*/
            discount.Value -= (comp == null ? 0D : (((double?)comp.FeeDiscount) ?? 0));

            var validDiscount = (dataItem["TariffComponentName"].FindControl("rfvDiscount") as RequiredFieldValidator);
            validDiscount.Visible = (dataItem["IsAllowDiscount"].Text == "True") 
                && (!AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare);

            var paramedic = (dataItem["TariffComponentName"].FindControl("cboPhysicianID") as RadComboBox);
            paramedic.Visible = (dataItem["IsTariffParamedic"].Text == "True");

            var validParamedic = (dataItem["TariffComponentName"].FindControl("rfvPhysicianID") as RequiredFieldValidator);
            validParamedic.Visible = (dataItem["IsTariffParamedic"].Text == "True");

            if (paramedic.Visible)
            {
                paramedic.Items.Clear();
                paramedic.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

                var table = ((DataTable)ViewState["paramedic" + Request.UserHostName]);
                foreach (DataRow row in table.Rows)
                {
                    paramedic.Items.Add(new RadComboBoxItem((string)row["ParamedicName"], (string)row["ParamedicID"]));
                }

                if (comp != null)
                {
                    //var isItemLab = false;
                    //var ilb = new ItemLaboratory();
                    //if (ilb.LoadByPrimaryKey(Request.QueryString["itemID"]))
                    //{
                    //    isItemLab = true;
                    //}

                    ////paramedic.SelectedValue = string.IsNullOrEmpty(comp.ParamedicID) ?
                    ////    (isItemLab ? AppSession.Parameter.ParamedicIdLabDefault : comp.ParamedicID)
                    ////    : comp.ParamedicID;

                    //if (string.IsNullOrEmpty(comp.ParamedicID))
                    //{
                    //    if (isItemLab)
                    //    {
                    //        var psdColl = new ParamedicScheduleDateCollection();
                    //        DataTable dtb = psdColl.GetParamedicID(AppSession.Parameter.ServiceUnitLaboratoryID, _srRegistrationType);

                    //        if (dtb.Rows.Count == 1)
                    //            paramedic.SelectedValue = dtb.Rows[0]["ParamedicID"].ToString();
                    //        else paramedic.SelectedValue = comp.ParamedicID;
                    //    }
                    //    else
                    //        paramedic.SelectedValue = comp.ParamedicID;
                    //}
                    //else
                    //    paramedic.SelectedValue = comp.ParamedicID;

                    var psdColl = new ParamedicScheduleDateCollection();
                    DataTable dtb = psdColl.GetParamedicID(_serviceUnitId, _srRegistrationType);

                    if (dtb.Rows.Count == 1)
                        paramedic.SelectedValue = dtb.Rows[0]["ParamedicID"].ToString();
                    else paramedic.SelectedValue = comp.ParamedicID;
                }
            }

            var percentFeeDiscount = (dataItem["TariffComponentName"].FindControl("txtPercentFeeDiscount") as RadNumericTextBox);
            percentFeeDiscount.Visible = (dataItem["IsAllowDiscount"].Text == "True")
                && (AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare);
            if (percentFeeDiscount.Visible)
                percentFeeDiscount.Value = (comp == null ? 0D : (double?)comp.FeeDiscountPercentage);
        }

        private static void InitTariffComponentTable(out DataTable dataTable)
        {
            var tempTable = new DataTable();

            var column = new DataColumn { DataType = Type.GetType("System.String"), ColumnName = "TariffComponentID" };
            tempTable.Columns.Add(column);

            column = new DataColumn { DataType = Type.GetType("System.Decimal"), ColumnName = "Price" };
            tempTable.Columns.Add(column);

            column = new DataColumn { DataType = Type.GetType("System.Boolean"), ColumnName = "IsAllowDiscount" };
            tempTable.Columns.Add(column);

            column = new DataColumn { DataType = Type.GetType("System.Boolean"), ColumnName = "IsAllowVariable" };
            tempTable.Columns.Add(column);

            column = new DataColumn { DataType = Type.GetType("System.String"), ColumnName = "TariffComponentName" };
            tempTable.Columns.Add(column);

            column = new DataColumn { DataType = Type.GetType("System.Boolean"), ColumnName = "IsTariffParamedic" };
            tempTable.Columns.Add(column);

            column = new DataColumn { DataType = Type.GetType("System.Decimal"), ColumnName = "FeeDiscountPercentage" };
            tempTable.Columns.Add(column);

            column = new DataColumn { DataType = Type.GetType("System.Decimal"), ColumnName = "FeeDiscount" };
            tempTable.Columns.Add(column);

            dataTable = tempTable;
        }

        private TransChargesItemComp FindTransChargesItemComp(string tariffComponentID)
        {
            var coll = (TransChargesItemCompCollection)Session["collTransChargesItemComp" + Request.UserHostName];
            return coll == null ? null :
                coll.FirstOrDefault(rec => rec.SequenceNo.Equals(Request.QueryString["seqNo"]) && rec.TariffComponentID.Equals(tariffComponentID));
        }

        protected void grdTariff_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            PopulateParamedicByServiceUnit();

            grdTariff.DataSource = TariffComponents;
        }

        private void PopulateParamedicByServiceUnit()
        {
            if (ViewState["paramedic" + Request.UserHostName] != null)
                return;

            var hd = new TransCharges();
            hd.LoadByPrimaryKey(Request.QueryString["joNo"]);

            var query = new ServiceUnitParamedicQuery("a");
            var medic = new ParamedicQuery("b");

            query.Select(
                query.ParamedicID,
                medic.ParamedicName
                );
            query.InnerJoin(medic).On(query.ParamedicID == medic.ParamedicID);
            query.Where(
                query.ServiceUnitID == hd.ToServiceUnitID,
                medic.IsActive == true
                );

            ViewState["paramedic" + Request.UserHostName] = query.LoadDataTable();
        }

        public override bool OnButtonOkClicked()
        {
            HideInformationHeader();
            Validate();
            if (!IsValid) return false;

            decimal disc = 0, price = 0, cito = 0;
            var p = string.Empty;
            var pCode = string.Empty;

            var detail = ((TransChargesItemCollection)Session["collTransChargesItem" + Request.UserHostName]).FindByPrimaryKey(Request.QueryString["joNo"],
                Request.QueryString["seqNo"]);

            var ir = new ItemRadiology();
            if (ir.LoadByPrimaryKey(detail.ItemID) && string.IsNullOrEmpty(txtFilmNo.Text))
            {
                ShowInformationHeader("Film No is required.");
                return false;
            }

            var hd = new TransCharges();
            hd.LoadByPrimaryKey(Request.QueryString["joNo"]);

            var reg = new Registration();
            reg.LoadByPrimaryKey(hd.RegistrationNo);

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);

            if (pnlComponent.Visible)
            {
                foreach (GridDataItem dataItem in grdTariff.MasterTableView.Items)
                {
                    var comp = ((TransChargesItemCompCollection)Session["collTransChargesItemComp" + Request.UserHostName]).FindByPrimaryKey(
                        Request.QueryString["joNo"], Request.QueryString["seqNo"], dataItem["TariffComponentID"].Text);
                    if (comp == null)
                    {
                        comp = ((TransChargesItemCompCollection)Session["collTransChargesItemComp" + Request.UserHostName]).AddNew();
                        comp.TransactionNo = Request.QueryString["joNo"];
                        comp.SequenceNo = Request.QueryString["seqNo"];
                    }

                    comp.TariffComponentID = dataItem["TariffComponentID"].Text;

                    comp.Price = string.IsNullOrEmpty((dataItem["TariffComponentName"].FindControl("txtPrice") as RadNumericTextBox).Text) ?
                        (decimal)0D :
                        (decimal)(dataItem["TariffComponentName"].FindControl("txtPrice") as RadNumericTextBox).Value;

                    if ((dataItem["TariffComponentName"].FindControl("txtDiscountPercent") as RadNumericTextBox).Text != string.Empty)
                    {
                        var diskonpercent = (decimal)(dataItem["TariffComponentName"].FindControl("txtDiscountPercent") as RadNumericTextBox).Value;
                        comp.DiscountAmount = comp.Price * (diskonpercent / 100);
                    }
                    else
                    {
                        comp.DiscountAmount = string.IsNullOrEmpty((dataItem["TariffComponentName"].FindControl("txtDiscount") as RadNumericTextBox).Text) ?
                            (decimal)0D :
                            (decimal)(dataItem["TariffComponentName"].FindControl("txtDiscount") as RadNumericTextBox).Value;
                    }

                    if (detail != null)
                    {
                        if (detail.IsCito ?? false)
                            comp.CitoAmount = (detail.IsCitoInPercent ?? false)
                                                    ? (detail.BasicCitoAmount / 100) * comp.Price
                                                    : detail.BasicCitoAmount;
                        else
                            comp.CitoAmount = 0;
                    }
                    else
                        comp.CitoAmount = 0;

                    comp.DiscountAmount = string.IsNullOrEmpty((dataItem["TariffComponentName"].FindControl("txtDiscount") as RadNumericTextBox).Text) ?
                            (decimal)0D :
                            (decimal)(dataItem["TariffComponentName"].FindControl("txtDiscount") as RadNumericTextBox).Value;

                    comp.ParamedicID = (dataItem["TariffComponentName"].FindControl("cboPhysicianID") as RadComboBox).SelectedValue;
                    
                    comp.FeeDiscountPercentage = (decimal?)(dataItem["TariffComponentName"].FindControl("txtPercentFeeDiscount") as RadNumericTextBox).Value;
                    var fee = comp.CalculateParamedicPercentDiscount(
                        AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare,
                        reg.RegistrationNo, detail.ItemID, (comp.DiscountAmount ?? 0),
                        AppSession.UserLogin.UserID, hd.ClassID, hd.ToServiceUnitID);

                    disc += (comp.DiscountAmount ?? 0);
                    price += (comp.Price ?? 0);
                    cito += (comp.CitoAmount ?? 0);

                    comp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    comp.LastUpdateDateTime = DateTime.Now;

                    if (!string.IsNullOrEmpty(comp.ParamedicID))
                    {
                        var tComp = new TariffComponent();
                        if (tComp.LoadByPrimaryKey(comp.TariffComponentID))
                        {
                            if (tComp.IsPrintParamedicInSlip ?? false)
                            {
                                var par = new Paramedic();
                                par.LoadByPrimaryKey(comp.ParamedicID);
                                if (par.IsPrintInSlip ?? true)
                                {
                                    if (p.Length == 0)
                                    {
                                        p = par.ParamedicName;
                                        pCode = par.ParamedicID;
                                    }
                                    else if (!p.Contains(par.ParamedicName))
                                        p = p + "; " + par.ParamedicName;
                                }
                            }
                        }
                    }
                }
            }
            else
                disc = (decimal)txtDiscountAmount1.Value;

            if (detail != null)
            {
                detail.DiscountAmount = detail.ChargeQuantity * disc;
                detail.Price = price;
                detail.CitoAmount = detail.ChargeQuantity * cito;
                detail.Total = detail.ChargeQuantity * ((detail.Price - detail.DiscountAmount) + detail.CitoAmount);
                detail.IsOrderRealization = true;
                detail.IsBillProceed = true;
                detail.IsApprove = true;
                detail.ParamedicID = cboParamedicID.SelectedValue;
                detail.SRDiscountReason = cboDiscountReason.SelectedValue;
                detail.IsVoid = false;
                detail.FilmNo = txtFilmNo.Text;
                detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                detail.LastUpdateDateTime = DateTime.Now;
                detail.ParamedicCollectionName = p;
                if (!pCode.Equals(string.Empty)) detail.ParamedicID = pCode;
            }

            return true;
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            var detail = new TransChargesItem();
            detail.LoadByPrimaryKey(Request.QueryString["joNo"], Request.QueryString["seqNo"]);

            if (detail.IsOrderRealization ?? false)
            {
                cboParamedicID.Enabled = false;
                txtFilmNo.ReadOnly = true;
                cboDiscountReason.Enabled = false;
                grdTariff.Enabled = false;

                (Helper.FindControlRecursive(Page, "btnOk") as Button).Enabled = false;
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.command = 'rebind'";
        }

        protected void chkIsVariable_CheckedChanged(object sender, EventArgs e)
        {
            txtPrice1.ReadOnly = !(chkIsVariable.Checked);
            if (!chkIsVariable.Checked)
            {
                txtPrice1.Enabled = false;

                var hd = new TransCharges();
                hd.LoadByPrimaryKey(Request.QueryString["joNo"]);

                var dt = new TransChargesItem();
                dt.LoadByPrimaryKey(Request.QueryString["joNo"], Request.QueryString["seqNo"]);

                var reg = new Registration();
                reg.LoadByPrimaryKey(hd.RegistrationNo);

                var grr = new Guarantor();
                grr.LoadByPrimaryKey(reg.GuarantorID);

                var tariffDate = grr.TariffCalculationMethod == 1
                    ? reg.RegistrationDate.Value.Date
                    : hd.ExecutionDate.Value;

                //set to default tariff
                var tariff = (Helper.Tariff.GetItemTariff(tariffDate, grr.SRTariffType, hd.ClassID, hd.ClassID, dt.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                              Helper.Tariff.GetItemTariff(tariffDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, hd.ClassID, dt.ItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                             (Helper.Tariff.GetItemTariff(tariffDate, AppSession.Parameter.DefaultTariffType, hd.ClassID, hd.ClassID, dt.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                              Helper.Tariff.GetItemTariff(tariffDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, hd.ClassID, dt.ItemID, reg.GuarantorID, false, reg.SRRegistrationType));

                txtPrice1.Value = (double)tariff.Price;
            }
        }

        protected void chkIsDiscount_CheckedChanged(object sender, EventArgs e)
        {
            txtDiscountPercent.ReadOnly = !(chkIsDiscount.Checked);
            txtDiscountAmount1.ReadOnly = !(chkIsDiscount.Checked);
            if (!chkIsDiscount.Checked)
            {
                txtDiscountPercent.Value = 0D;
                txtDiscountAmount1.Value = 0D;
            }
        }

        protected void txtDiscountPercent_TextChanged(object sender, EventArgs e)
        {
            txtDiscountAmount1.Value = txtDiscountPercent.Value / 100 * txtPrice1.Value;
        }

        protected void cboParamedicID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            foreach (GridDataItem dataItem in grdTariff.MasterTableView.Items)
            {
                var cboPhysicianID = (dataItem["TariffComponentName"].FindControl("cboPhysicianID") as RadComboBox);
                if (cboPhysicianID != null)
                    cboPhysicianID.SelectedValue = e.Value;
            }
        }
    }
}
