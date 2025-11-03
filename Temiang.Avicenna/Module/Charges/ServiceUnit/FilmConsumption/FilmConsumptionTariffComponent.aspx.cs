using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;


namespace Temiang.Avicenna.Module.Charges
{
    public partial class FilmConsumptionTariffComponent : BasePageDialog
    {
        private DataTable TariffComponents
        {
            get
            {
                var hd = new TransCharges();
                hd.LoadByPrimaryKey(Request.QueryString["joNo"]);

                var reg = new Registration();
                reg.LoadByPrimaryKey(hd.RegistrationNo);

                var grr = new Guarantor();
                grr.LoadByPrimaryKey(reg.GuarantorID);

                var tariffColl = new TariffComponentCollection();
                tariffColl.Query.OrderBy(tariffColl.Query.TariffComponentID.Ascending);
                tariffColl.LoadAll();

                var coll = new TransChargesItemCompCollection();
                coll.Query.Where(coll.Query.TransactionNo == Request.QueryString["joNo"],
                                 coll.Query.SequenceNo == Request.QueryString["seqNo"]);
                coll.LoadAll();

                DataTable dtb;
                InitTariffComponentTable(out dtb);

                foreach (var entity in coll)
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

                    dtb.Rows.Add(newRow);
                }

                return dtb;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.FilmConsumptionEntry;

            if (!IsPostBack)
            {
                if (Request.QueryString["type"] == BusinessObject.Reference.ItemType.Medical ||
                    Request.QueryString["type"] == BusinessObject.Reference.ItemType.NonMedical)
                    pnlTariffNonComponent.Visible = true;
                else
                {
                    pnlTariffComponent.Visible = true;

                    grdTariff.MasterTableView.Columns[3].Visible = AppSession.Parameter.TariffComponentPriceVisible;
                    grdTariff.MasterTableView.Columns[4].Visible = AppSession.Parameter.TariffComponentPriceVisible;
                }

                var charges = (TransChargesItemCollection)Session["collTransChargesItem" + Request.UserHostName];
                foreach (var entity in charges.Where(entity => entity.SequenceNo == Request.QueryString["seqNo"]))
                {
                    if (pnlTariffNonComponent.Visible)
                    {
                        txtPrice1.Value = Convert.ToDouble(entity.Price);

                        var hd = new TransCharges();
                        hd.LoadByPrimaryKey(entity.TransactionNo);

                        var reg = new Registration();
                        reg.LoadByPrimaryKey(hd.RegistrationNo);

                        var grr = new Guarantor();
                        grr.LoadByPrimaryKey(reg.GuarantorID);

                        var tariff = (Helper.Tariff.GetItemTariff(hd.TransactionDate.Value, grr.SRTariffType, hd.ClassID, hd.ClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                      Helper.Tariff.GetItemTariff(hd.TransactionDate.Value, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, hd.ClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                                     (Helper.Tariff.GetItemTariff(hd.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, hd.ClassID, hd.ClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                      Helper.Tariff.GetItemTariff(hd.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, hd.ClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType));

                        chkIsVariable.Enabled = tariff.IsAllowVariable ?? false;
                        chkIsDiscount.Enabled = tariff.IsAllowDiscount ?? false;

                        txtDiscountAmount1.Value = Convert.ToDouble(entity.DiscountAmount);
                        if (chkIsDiscount.Enabled && txtDiscountAmount1.Value > 0)
                            chkIsDiscount.Checked = true;
                    }
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

            var comp = FindTransChargesItemComp(dataItem["TariffComponentID"].Text);

            var price = (dataItem["TariffComponentName"].FindControl("txtPrice") as RadNumericTextBox);
            price.ReadOnly = true;
            price.Value = (comp == null ? Convert.ToDouble(dataItem["Price"].Text) : (double)comp.Price);

            var validPrice = (dataItem["TariffComponentName"].FindControl("rfvPrice") as RequiredFieldValidator);
            validPrice.Visible = false;

            var discount = (dataItem["TariffComponentName"].FindControl("txtDiscount") as RadNumericTextBox);
            discount.ReadOnly = true;
            discount.MaxValue = (comp == null ? Convert.ToDouble(dataItem["Price"].Text) : (double)comp.Price);
            discount.Value = (comp == null ? 0D : (double)comp.DiscountAmount);

            var validDiscount = (dataItem["TariffComponentName"].FindControl("rfvDiscount") as RequiredFieldValidator);
            validDiscount.Visible = false;

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
                    paramedic.SelectedValue = comp.ParamedicID;
            }
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

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (pnlTariffComponent.Visible)
            {
                var appprg = new AppProgram();
                if (appprg.LoadByPrimaryKey("05.07.03"))
                {
                    if (appprg.NavigateUrl.Trim()
                        .IndexOf("ParamedicFeeVerificationByDischargeDateList.aspx") > 0)
                    {
                        foreach (GridDataItem dataItem in grdTariff.MasterTableView.Items)
                        {
                            // cek jasa medis sudah verifikasi atau belum

                            var comp = ((TransChargesItemCompCollection)Session["collTransChargesItemComp" + Request.UserHostName]).FindByPrimaryKey(
                                Request.QueryString["joNo"], Request.QueryString["seqNo"], dataItem["TariffComponentID"].Text);
                            if (comp != null)
                            {
                                string msg = ParamedicFeeTransChargesItemCompByDischargeDate.IsParamedicFeeVerified(
                                    Request.QueryString["joNo"], Request.QueryString["seqNo"],
                                    dataItem["TariffComponentID"].Text);

                                if (!string.IsNullOrEmpty(msg))
                                { 
                                    // Sudah verifikasi jasmed
                                    args.IsValid = false;
                                    ((CustomValidator)source).ErrorMessage = string.Format("Physician can't be changed. {0}!", msg);
                                }
                            }
                        }
                    }
                }
            }
        }

        public override bool OnButtonOkClicked()
        {
            Validate();
            if (!IsValid)
                return false;

            var p = string.Empty;
            var pCode = string.Empty;
            if (pnlTariffComponent.Visible)
            {
                foreach (GridDataItem dataItem in grdTariff.MasterTableView.Items)
                {
                    var comp = ((TransChargesItemCompCollection)Session["collTransChargesItemComp" + Request.UserHostName]).FindByPrimaryKey(
                        Request.QueryString["joNo"], Request.QueryString["seqNo"], dataItem["TariffComponentID"].Text);
                    if (comp != null)
                    {
                        comp.ParamedicID = (dataItem["TariffComponentName"].FindControl("cboPhysicianID") as RadComboBox).SelectedValue;
                        comp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        comp.LastUpdateDateTime = DateTime.Now;
                    }

                    if (!string.IsNullOrEmpty(comp.ParamedicID))
                    {
                        var tComp = new TariffComponent();
                        if (tComp.LoadByPrimaryKey(comp.TariffComponentID))
                        {
                            if (tComp.IsPrintParamedicInSlip ?? false)
                            {
                                var par = new Paramedic();
                                par.LoadByPrimaryKey(comp.ParamedicID);

                                if (p.Length == 0)
                                {
                                    if (par.IsPrintInSlip ?? true)
                                        p = par.ParamedicName;
                                    pCode = par.ParamedicID;
                                }
                                else if (!p.Contains(par.ParamedicName))
                                {
                                    if (par.IsPrintInSlip ?? true)
                                        p = p + "; " + par.ParamedicName;
                                }
                            }
                        }
                    }
                }
            }

            var hd = new TransCharges();
            hd.LoadByPrimaryKey(Request.QueryString["joNo"]);

            var reg = new Registration();
            reg.LoadByPrimaryKey(hd.RegistrationNo);

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);

            var detail = ((TransChargesItemCollection)Session["collTransChargesItem" + Request.UserHostName]).FindByPrimaryKey(Request.QueryString["joNo"],
                Request.QueryString["seqNo"]);
            if (detail != null)
            {
                detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                detail.LastUpdateDateTime = DateTime.Now;
                detail.ParamedicCollectionName = p;
                if (!pCode.Equals(string.Empty)) detail.ParamedicID = pCode;
                detail.UpdateRealizationUserID = AppSession.UserLogin.UserID;
                detail.UpdateRealizationDateTime = DateTime.Now;
                detail.FilmNo = txtFilmNo.Text;
            }

            return true;
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

                //set to default tariff
                var tariff = (Helper.Tariff.GetItemTariff(hd.TransactionDate.Value, grr.SRTariffType, hd.ClassID, hd.ClassID, dt.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                              Helper.Tariff.GetItemTariff(hd.TransactionDate.Value, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, hd.ClassID, dt.ItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                             (Helper.Tariff.GetItemTariff(hd.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, hd.ClassID, hd.ClassID, dt.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                              Helper.Tariff.GetItemTariff(hd.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, hd.ClassID, dt.ItemID, reg.GuarantorID, false, reg.SRRegistrationType));

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
    }
}
