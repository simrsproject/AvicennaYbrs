using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class NonPatientCustomerChargesItemDetail : BaseUserControl
    {
        public object DataItem { get; set; }

        private DataTable TariffComponents(IEnumerable<ItemTariffComponent> components)
        {
            DataTable dtb;
            InitTariffComponentTable(out dtb);

            foreach (var entity in components)
            {
                var newRow = dtb.NewRow();
                newRow["TariffComponentID"] = entity.TariffComponentID;
                //if (ChkIsRoomIn.Checked && chkIsItemRoom.Checked)
                //    newRow["Price"] = entity.Price - (entity.Price * Convert.ToDecimal(TxtTariffDiscForRoomIn.Value) / 100);
                //else
                newRow["Price"] = entity.Price;

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

        //private CheckBox ChkIsRoomIn
        //{
        //    get
        //    { return (CheckBox)Helper.FindControlRecursive(Page, "chkIsRoomIn"); }
        //}

        //private RadNumericTextBox TxtTariffDiscForRoomIn
        //{
        //    get
        //    { return (RadNumericTextBox)Helper.FindControlRecursive(Page, "txtTariffDiscForRoomIn"); }
        //}

        //private RadTextBox TxtGuarantorId
        //{
        //    get
        //    { return (RadTextBox)Helper.FindControlRecursive(Page, "txtGuarantorID"); }
        //}

        private RadComboBox CboGuarantorID
        {
            get { return (RadComboBox)Helper.FindControlRecursive(Page, "cboGuarantorID"); }
        }

        private RadTextBox TxtRegistrationNo
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtRegistrationNo"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            PopulateParamedicByServiceUnit();

            //var reg = new Registration();
            //reg.LoadByPrimaryKey(TxtRegistrationNo.Text);
            //ViewState["GuarantorID" + Request.UserHostName] = reg.GuarantorID;
            string guarantorId = AppSession.Parameter.SelfGuarantor;
            if (!string.IsNullOrEmpty(CboGuarantorID.SelectedValue))
                guarantorId = CboGuarantorID.SelectedValue;

            ViewState["GuarantorID" + Request.UserHostName] = guarantorId;
            var grr = new Guarantor();
            if (grr.LoadByPrimaryKey(guarantorId))
                ViewState["SRTariffType" + Request.UserHostName] = grr.SRTariffType;
            else
                ViewState["SRTariffType" + Request.UserHostName] = AppSession.Parameter.DefaultTariffType;

            var unit = new ServiceUnit();
            //if (Request.QueryString["type"] == "tr" || Request.QueryString["type"] == "mcu")
            //{
            //    unit.LoadByPrimaryKey(((HtmlTableRow)Helper.FindControlRecursive(Page, "pnlResponUnit")).Visible
            //        ? ((RadComboBox)Helper.FindControlRecursive(Page, "cboResponUnit")).SelectedValue
            //        : ((RadTextBox)Helper.FindControlRecursive(Page, "txtFromServiceUnitID")).Text);
            //}
            //else
            if (unit.LoadByPrimaryKey(((RadComboBox)Helper.FindControlRecursive(Page, "cboServiceUnitID")).SelectedValue))
                ViewState["LocationID" + Request.UserHostName] = unit.GetMainLocationId(unit.ServiceUnitID);
            else
                ViewState["LocationID" + Request.UserHostName] = string.Empty;

            StandardReference.InitializeIncludeSpace(cboSRDiscountReason, AppEnum.StandardReference.DiscountReason);
            StandardReference.InitializeIncludeSpace(cboCenterID, AppEnum.StandardReference.CenterID);
            StandardReference.InitializeIncludeSpace(cboSRCitoPercentage, AppEnum.StandardReference.CitoPercentage);

            ViewState["IsAdminCalculation"] = false;

            if (Request.QueryString["type"] == "tr" || Request.QueryString["type"] == "ds" || Request.QueryString["type"] == "mcu")
            {
                pnlParamedic.Visible = false;
                pnlDiscountReason.Visible = true;
                pnlPackage.Visible = true;
            }
            else
            {
                pnlParamedic.Visible = false;
                pnlDiscountReason.Visible = false;
                pnlPackage.Visible = false;
            }

            trCenterID.Visible = Request.QueryString["resp"] == "1";

            if (DataItem is GridInsertionObject)
            {
                cboItemID.Enabled = true;
                ViewState["IsNewRecord"] = true;

                var coll = (TransChargesItemCollection)Session["collTransChargesItem" + Request.UserHostName];
                if (coll.Count == 0)
                    ViewState["SequenceNo" + Request.UserHostName] = "001";
                else
                {
                    var sequenceNo = (coll.Where(c => c.ParentNo == string.Empty).OrderByDescending(c => c.SequenceNo).Select(c => c.SequenceNo)).Take(1);
                    int seqNo = int.Parse(sequenceNo.Single()) + 1;
                    ViewState["SequenceNo" + Request.UserHostName] = string.Format("{0:000}", seqNo);
                }

                chkIsVoid.Enabled = false;
                return;
            }
            cboItemID.Enabled = false;

            ViewState["IsNewRecord"] = false;
            ViewState["SequenceNo" + Request.UserHostName] = DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.SequenceNo);

            cboItemID.DataSource = PopulateItem((String)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.ItemID));
            cboItemID.DataBind();
            cboItemID.SelectedValue = (String)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.ItemID);

            PopulateTariff((String)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.ItemID));

            chkIsCito.Checked = (bool)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.IsCito);
            cboSRCitoPercentage.SelectedValue = (String)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.SRCitoPercentage);

            txtChargeQuantity.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.ChargeQuantity));
            txtStockQuantity.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.StockQuantity));
            txtItemUnit.Text = (String)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.SRItemUnit);

            // if (Request.QueryString["type"] == "tr" || Request.QueryString["type"] == "ds" || Request.QueryString["type"] == "mcu")
            // {
            //if (ViewState["paramedic" + Request.UserHostName] == null)
            //    PopulateParamedicByServiceUnit();

            DataView view = ((DataTable)ViewState["paramedic" + Request.UserHostName]).DefaultView;
            view.RowFilter = string.Format("ParamedicID LIKE '%{0}%' OR ParamedicName LIKE '%{0}%'",
                DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.ParamedicID));
            cboParamedicID.DataSource = view;
            cboParamedicID.DataBind();

            cboParamedicID.SelectedValue = (String)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.ParamedicID);
            ViewState["IsAdminCalculation"] = (bool)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.IsAdminCalculation);
            chkIsVariable.Checked = (bool)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.IsVariable);
            txtPrice1.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.Price));

            txtDiscountAmount1.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.DiscountAmount));
            if (chkIsDiscount.Enabled && txtDiscountAmount1.Value > 0)
                chkIsDiscount.Checked = true;

            cboSRDiscountReason.SelectedValue = (String)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.SRDiscountReason);
            pnlAsset.Visible = (bool)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.IsAssetUtilization);
            chkIsAssetUtilization.Checked = (bool)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.IsAssetUtilization);
            cboAssetID.SelectedValue = (String)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.AssetID);
            chkIsPackage.Checked = (bool)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.IsPackage);
            //  }
            //else
            //    txtPriceJO.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.Price));

            if (Request.QueryString["type"] != "mcu")
                chkIsVoid.Enabled = true;

            chkIsVoid.Checked = (bool)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.IsVoid);
            txtNotes.Text = (String)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.Notes);
            chkIsItemRoom.Checked = (bool)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.IsItemRoom);

            if (trCenterID.Visible)
                cboCenterID.SelectedValue = (String)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.SRCenterID);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (TransChargesItemCollection)Session["collTransChargesItem" + Request.UserHostName];

                bool isExist = coll.Any(charges => (charges.ItemID.Equals(cboItemID.SelectedValue)) &&
                                                   (!(charges.IsVoid ?? false)));

                if (isExist)
                {
                    switch (AppSession.Parameter.HealthcareInitialAppsVersion)
                    {
                        case "RSSA":
                            var it = new Item();
                            it.LoadByPrimaryKey(cboItemID.SelectedValue);
                            if (it.SRItemType == "11" || it.SRItemType == "21")
                            {
                                args.IsValid = false;
                                ((CustomValidator)source).ErrorMessage = string.Format("Item ID: {0} has exist", cboItemID.SelectedValue);
                                return;
                            }
                            break;

                        default:
                            args.IsValid = false;
                            ((CustomValidator)source).ErrorMessage = string.Format("Item ID: {0} has exist", cboItemID.SelectedValue);
                            return;
                    }
                }
            }

            if (txtChargeQuantity.Value <= 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Charge quantity value must be greater then 0";
                return;
            }

            if (txtStockQuantity.Value < 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Stock quantity value can't less then 0";
                return;
            }

            //validate stock
            var item = new Item();
            if (item.LoadByPrimaryKey(cboItemID.SelectedValue))
            {
                if (item.SRItemType == ItemType.Medical ||
                    item.SRItemType == ItemType.NonMedical)
                {
                    var balance = new ItemBalance();
                    if (balance.LoadByPrimaryKey((string)ViewState["LocationID" + Request.UserHostName], cboItemID.SelectedValue))
                    {
                        if (balance.Balance <= 0)
                        {
                            args.IsValid = false;
                            ((CustomValidator)source).ErrorMessage = "Insufficient balance of item";
                            return;
                        }
                        if (balance.Balance < (decimal)txtStockQuantity.Value)
                        {
                            args.IsValid = false;
                            ((CustomValidator)source).ErrorMessage = "Insufficient balance of item";
                            return;
                        }
                    }
                    else
                    {
                        args.IsValid = false;
                        ((CustomValidator)source).ErrorMessage = "Insufficient balance of item";
                        return;
                    }
                }
                else
                {
                    var cons = new ItemConsumptionCollection();
                    cons.Query.Where(cons.Query.ItemID == item.ItemID);
                    cons.LoadAll();

                    if (cons.Count > 0)
                    {
                        foreach (ItemConsumption entity in cons)
                        {
                            var balance = new ItemBalance();
                            if (!balance.LoadByPrimaryKey((string)ViewState["LocationID" + Request.UserHostName], entity.DetailItemID))
                            {
                                args.IsValid = false;
                                ((CustomValidator)source).ErrorMessage = "Insufficient balance of detail item";
                                return;
                            }
                            if (balance.Balance < entity.Qty)
                            {
                                args.IsValid = false;
                                ((CustomValidator)source).ErrorMessage = "Insufficient balance of detail item";
                                return;
                            }
                        }
                    }
                }
            }

            //validate discount reason
            if (chkIsDiscount.Checked && cboSRDiscountReason.SelectedIndex == -1)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Discount Reason is required";
                return;
            }

            //validate asset id
            if (chkIsAssetUtilization.Checked && cboAssetID.SelectedValue == string.Empty)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Asset ID is required";
            }

            //validate disc
            decimal maxdisc = AppSession.Parameter.MaxDiscTxInPercentage;
            string msg = string.Empty;
            bool isNotValid = false;
            foreach (GridDataItem dataItem in grdTariff.MasterTableView.Items)
            {
                if ((dataItem["TariffComponentName"].FindControl("txtPercentDiscount") as RadNumericTextBox).Visible &&
                    (dataItem["TariffComponentName"].FindControl("txtPercentDiscount") as RadNumericTextBox).Text != string.Empty)
                {
                    decimal diskonpercent = (decimal)(dataItem["TariffComponentName"].FindControl("txtPercentDiscount") as RadNumericTextBox).Value;

                    isNotValid = (diskonpercent > maxdisc);
                    if (isNotValid)
                        msg = "Discount can't more than " + maxdisc.ToString() + "%.";
                }
                else
                {
                    if ((dataItem["TariffComponentName"].FindControl("txtDiscount") as RadNumericTextBox).Visible &&
                        (dataItem["TariffComponentName"].FindControl("txtDiscount") as RadNumericTextBox).Text != string.Empty)
                    {
                        decimal price = (decimal)(dataItem["TariffComponentName"].FindControl("txtPrice") as RadNumericTextBox).Value;
                        if (price > 0)
                        {
                            decimal discAmt = (decimal)(dataItem["TariffComponentName"].FindControl("txtDiscount") as RadNumericTextBox).Value;
                            isNotValid = (((discAmt / price) * 100) > maxdisc);
                            if (isNotValid)
                                msg = "Discount can't more than " + string.Format("{0:n0}", (price * maxdisc / 100)) + ".";
                        }
                    }
                }
            }
            if (isNotValid)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = msg;
            }

            // validate cito option
            if (chkIsCito.Checked && cboSRCitoPercentage.Enabled && cboSRCitoPercentage.SelectedValue == string.Empty)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Cito option required";
            }
        }

        protected void chkIsVariable_CheckedChanged(object sender, EventArgs e)
        {
            if (Request.QueryString["type"] != "jo")
            {
                if (chkIsVariable.Checked)
                    txtPrice1.ReadOnly = false;
                else
                {
                    txtPrice1.ReadOnly = true;

                    var reg = new Registration();
                    reg.LoadByPrimaryKey(TxtRegistrationNo.Text);

                    //set to default tariff
                    ItemTariff tariff = (Helper.Tariff.GetItemTariff(((RadDatePicker)Helper.FindControlRecursive(Page, "txtTransactionDate")).SelectedDate.Value,
                                                                     (string)ViewState["SRTariffType" + Request.UserHostName], AppSession.Parameter.OutPatientClassID, AppSession.Parameter.OutPatientClassID,
                                                                     cboItemID.SelectedValue, (string)ViewState["GuarantorID" + Request.UserHostName], false, reg.SRRegistrationType) ??
                                         Helper.Tariff.GetItemTariff(((RadDatePicker)Helper.FindControlRecursive(Page, "txtTransactionDate")).SelectedDate.Value,
                                                                     (string)ViewState["SRTariffType" + Request.UserHostName], AppSession.Parameter.DefaultTariffClass, AppSession.Parameter.OutPatientClassID, cboItemID.SelectedValue,
                                                                     (string)ViewState["GuarantorID" + Request.UserHostName], false, reg.SRRegistrationType)) ??
                                        (Helper.Tariff.GetItemTariff(((RadDatePicker)Helper.FindControlRecursive(Page, "txtTransactionDate")).SelectedDate.Value,
                                                                  AppSession.Parameter.DefaultTariffType, AppSession.Parameter.OutPatientClassID, AppSession.Parameter.OutPatientClassID,
                                                                  cboItemID.SelectedValue, (string)ViewState["GuarantorID" + Request.UserHostName], false, reg.SRRegistrationType) ??
                                      Helper.Tariff.GetItemTariff(((RadDatePicker)Helper.FindControlRecursive(Page, "txtTransactionDate")).SelectedDate.Value,
                                                                  AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, AppSession.Parameter.OutPatientClassID, cboItemID.SelectedValue,
                                                                  (string)ViewState["GuarantorID" + Request.UserHostName], false, reg.SRRegistrationType));

                    txtPrice1.Value = (double)tariff.Price;
                }
            }
        }

        protected void chkIsDiscount_CheckedChanged(object sender, EventArgs e)
        {
            if (Request.QueryString["type"] != "jo")
            {
                txtDiscountAmount1.MaxValue = (txtPrice1.Value ?? 0);
                if (chkIsDiscount.Checked)
                {
                    txtDiscountAmount1.ReadOnly = false;
                    txtPercentDiscount1.ReadOnly = false;
                }
                else
                {
                    txtDiscountAmount1.ReadOnly = true;
                    txtPercentDiscount1.ReadOnly = true;
                    txtDiscountAmount1.Value = 0D;
                    txtPercentDiscount1.Value = 0D;
                }
            }
        }

        protected void chkIsAssetUtilization_CheckedChanged(object sender, EventArgs e)
        {
            if (Request.QueryString["type"] != "jo")
                cboAssetID.Enabled = chkIsAssetUtilization.Checked;
        }

        private void ClearFlagCheckBox()
        {
            chkIsVariable.Checked = false;
            chkIsVariable.Enabled = false;
            chkIsDiscount.Checked = false;
            chkIsDiscount.Enabled = false;
            chkIsCito.Checked = false;
            chkIsCito.Enabled = false;
            cboSRCitoPercentage.Enabled = chkIsCito.Enabled;
            chkIsAssetUtilization.Checked = false;
            chkIsAssetUtilization.Enabled = false;
            chkIsPackage.Checked = false;
            chkIsItemRoom.Checked = false;
        }

        protected void grdTariff_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
                e.Item.PreRender += grdTariff_ItemPreRender;
        }

        private void grdTariff_ItemPreRender(object sender, EventArgs e)
        {
            var dataItem = sender as GridDataItem;
            if (dataItem == null)
                return;

            TransChargesItemComp comp = FindTransChargesItemComp(dataItem["TariffComponentID"].Text);

            var price = (dataItem["TariffComponentName"].FindControl("txtPrice") as RadNumericTextBox);
            price.ReadOnly = !(dataItem["IsAllowVariable"].Text == "True");
            price.Value = (comp == null ? Convert.ToDouble(dataItem["Price"].Text) : (double)comp.Price);

            if (dataItem["IsAllowVariable"].Text == "True")
                price.Visible = true;
            else
                price.Visible = AppSession.Parameter.TariffComponentPriceVisible;

            var validPrice = (dataItem["TariffComponentName"].FindControl("rfvPrice") as RequiredFieldValidator);
            validPrice.Visible = (dataItem["IsAllowVariable"].Text == "True");

            var discountPercent = (dataItem["TariffComponentName"].FindControl("txtPercentDiscount") as RadNumericTextBox);
            discountPercent.Visible = (dataItem["IsAllowDiscount"].Text == "True");

            var discount = (dataItem["TariffComponentName"].FindControl("txtDiscount") as RadNumericTextBox);
            discount.Visible = (dataItem["IsAllowDiscount"].Text == "True");
            discount.MaxValue = (comp == null ? Convert.ToDouble(dataItem["Price"].Text) : (double)comp.Price);
            discount.Value = (comp == null ? 0D : (double)comp.DiscountAmount);

            var validDiscount = (dataItem["TariffComponentName"].FindControl("rfvDiscount") as RequiredFieldValidator);
            validDiscount.Visible = (dataItem["IsAllowDiscount"].Text == "True");

            var paramedic = (dataItem["TariffComponentName"].FindControl("cboPhysicianID") as RadComboBox);
            paramedic.Visible = (dataItem["IsTariffParamedic"].Text == "True");

            var validParamedic = (dataItem["TariffComponentName"].FindControl("rfvPhysicianID") as RequiredFieldValidator);
            validParamedic.Visible = (dataItem["IsTariffParamedic"].Text == "True");

            //if (paramedic.Visible)
            //{
            //    DataView view = ((DataTable)ViewState["paramedic" + Request.UserHostName]).DefaultView;
            //    if (comp != null)
            //        view.RowFilter = string.Format("ParamedicID = '{0}'", comp.ParamedicID);

            //    paramedic.DataSource = view;
            //    paramedic.DataBind();

            //    paramedic.SelectedValue = comp != null ? comp.ParamedicID : Request.QueryString["pid"];
            //}
        }

        private void PopulateParamedicByServiceUnit()
        {
            if (ViewState["paramedic" + Request.UserHostName] != null)
                return;

            var query = new ServiceUnitParamedicQuery("a");
            var medic = new ParamedicQuery("b");
            var que = new ServiceUnitQueQuery("c");

            string regNo = TxtRegistrationNo.Text;

            var reg = new Registration();
            reg.LoadByPrimaryKey(regNo);

            var rooms = new ServiceRoomCollection();
            rooms.Query.Where(
                rooms.Query.IsOperatingRoom == true,
                rooms.Query.IsActive == true
                );
            rooms.LoadAll();

            var r = (rooms.Where(o => o.ServiceUnitID == Request.QueryString["cid"]).Select(o => o.ServiceUnitID)).Distinct().SingleOrDefault();
            if (r != null)
            {
                medic.Where(medic.IsActive == true);
                ViewState["paramedic" + Request.UserHostName] = medic.LoadDataTable();

                return;
            }

            if (reg.SRRegistrationType != AppConstant.RegistrationType.InPatient)
            {
                query.Select
                    (
                        query.ParamedicID,
                        medic.ParamedicName
                    );

                query.InnerJoin(medic).On(query.ParamedicID == medic.ParamedicID);

                if (reg.SRRegistrationType == AppConstant.RegistrationType.ClusterPatient)
                {
                    query.InnerJoin(que).On(medic.ParamedicID == que.ParamedicID);
                    query.Where(que.RegistrationNo == regNo);
                }

                if (Request.QueryString["type"] == "tr" || Request.QueryString["type"] == "mcu")
                {
                    if (Helper.FindControlRecursive(Page, "pnlResponUnit").Visible)
                        query.Where
                        (
                            query.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboResponUnit")).SelectedValue,
                            medic.IsActive == true
                        );
                    else
                        query.Where
                            (
                                query.ServiceUnitID == ((RadTextBox)Helper.FindControlRecursive(Page, "txtFromServiceUnitID")).Text,
                                medic.IsActive == true
                            );
                }
                else
                {
                    query.Where
                        (
                            query.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboServiceUnitID")).SelectedValue,
                            medic.IsActive == true
                        );
                }

                ViewState["paramedic" + Request.UserHostName] = query.LoadDataTable();
            }
            else
            {
                if (Request.QueryString["type"] == "tr")
                {
                    medic.Where(medic.IsActive == true);
                    ViewState["paramedic" + Request.UserHostName] = medic.LoadDataTable();
                }
                else
                {
                    query.Select
                    (
                        query.ParamedicID,
                        medic.ParamedicName
                    );

                    query.InnerJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                    query.Where
                        (
                            query.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboServiceUnitID")).SelectedValue,
                            medic.IsActive == true
                        );
                    ViewState["paramedic" + Request.UserHostName] = query.LoadDataTable();
                }
            }
        }

        private void PopulateTariffComponentGrid(IEnumerable<ItemTariffComponent> components)
        {
            //grdTariff.MasterTableView.Columns[3].Visible = AppSession.Parameter.TariffComponentPriceVisible;
            //grdTariff.MasterTableView.Columns[4].Visible = AppSession.Parameter.TariffComponentPriceVisible;

            //Display Data Detail
            //TariffComponents = null; //Reset Record Detail
            grdTariff.DataSource = TariffComponents(components); //Requery
            grdTariff.DataBind();
        }

        private void InitTariffComponentTable(out DataTable dataTable)
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
            return coll == null ? null : coll.FirstOrDefault(rec => rec.SequenceNo.Equals((string)ViewState["SequenceNo" + Request.UserHostName]) &&
                                                                    rec.TariffComponentID.Equals(tariffComponentID));
        }

        protected void txtChargeQuantity_TextChanged(object sender, EventArgs e)
        {
            if (!txtStockQuantity.ReadOnly)
                txtStockQuantity.Value = txtChargeQuantity.Value;
        }

        protected void txtPercentDiscount1_TextChanged(object sender, EventArgs e)
        {
            txtDiscountAmount1.Value = (txtPercentDiscount1.Value / 100) * txtPrice1.Value;
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        private DataTable PopulateServiceItem(string searchText)
        {
            DataTable tbl = null;
            string searchTextContain = string.Format("%{0}%", searchText);
            try
            {
                #region new - melihat batasan item di guarantor
                var guar = new Guarantor();
                guar.LoadByPrimaryKey(ViewState["GuarantorID" + Request.UserHostName].ToString());

                DataTable tempTbl = null;
                //item service
                var query = new ItemQuery("a");
                var itemUnit = new ServiceUnitItemServiceQuery("c");
                var balance = new ItemBalanceQuery("d");

                query.es.Top = 30;
                query.Select
                    (
                        query.ItemID,
                        (query.ItemName + " [" + query.ItemID + "]").As("ItemName"),
                        (balance.Balance.Coalesce("0")).As("Balance"),
                        query.SRItemType,
                        itemUnit.ServiceUnitID.Coalesce("''")
                    );
                if (Request.QueryString["type"] == "tr" || Request.QueryString["type"] == "mcu")
                {
                    if (Helper.FindControlRecursive(Page, "pnlResponUnit").Visible)
                        query.InnerJoin(itemUnit).On(
                                query.ItemID == itemUnit.ItemID &&
                                itemUnit.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboResponUnit")).SelectedValue
                            );
                    else
                        query.InnerJoin(itemUnit).On(
                                query.ItemID == itemUnit.ItemID &&
                                itemUnit.ServiceUnitID == ((RadTextBox)Helper.FindControlRecursive(Page, "txtFromServiceUnitID")).Text
                            );
                }
                else
                {
                    query.InnerJoin(itemUnit).On(
                            query.ItemID == itemUnit.ItemID &&
                            itemUnit.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboServiceUnitID")).SelectedValue
                        );
                }
                query.LeftJoin(balance).On(
                        query.ItemID == balance.ItemID &&
                        balance.LocationID == (string)ViewState["LocationID" + Request.UserHostName]
                    );
                query.Where(query.SRItemType == ItemType.Service,
                        query.Or(
                                query.ItemName.Like(searchTextContain),
                                query.ItemID.Like(searchTextContain)
                            ),
                        query.IsActive == true,
                        query.Or(
                                query.GuarantorID.IsNull(),
                                query.GuarantorID == string.Empty,
                                query.GuarantorID == ViewState["GuarantorID" + Request.UserHostName].ToString())
                    );


                var restrictions = new GuarantorItemRestrictionsQuery("rest");
                var itmrest = new ItemQuery("itmrest");
                restrictions.Select(restrictions.ItemID);
                restrictions.InnerJoin(itmrest).On(restrictions.ItemID == itmrest.ItemID);
                restrictions.Where(restrictions.GuarantorID == ViewState["GuarantorID" + Request.UserHostName].ToString(),
                                   itmrest.SRItemType == ItemType.Service);
                DataTable dtRest = restrictions.LoadDataTable();
                if (dtRest.Rows.Count > 0)
                {
                    //query.InnerJoin(restrictions).On(query.ItemID == restrictions.ItemID && restrictions.GuarantorID == GuarantorID.SelectedValue);
                    if (guar.IsItemServiceRestrictionStatusAllowed ?? true)
                        query.Where(query.ItemID.In(restrictions));
                    else
                        query.Where(query.ItemID.NotIn(restrictions));
                }
                    
                tempTbl = query.LoadDataTable();

                //item lab
                if (tempTbl.Rows.Count < 30)
                {
                    query = new ItemQuery("a");
                    itemUnit = new ServiceUnitItemServiceQuery("c");
                    balance = new ItemBalanceQuery("d");

                    query.es.Top = 30;
                    query.Select
                        (
                            query.ItemID,
                            (query.ItemName + " [" + query.ItemID + "]").As("ItemName"),
                            (balance.Balance.Coalesce("0")).As("Balance"),
                            query.SRItemType,
                            itemUnit.ServiceUnitID.Coalesce("''")
                        );
                    if (Request.QueryString["type"] == "tr" || Request.QueryString["type"] == "mcu")
                    {
                        if (Helper.FindControlRecursive(Page, "pnlResponUnit").Visible)
                            query.InnerJoin(itemUnit).On(
                                    query.ItemID == itemUnit.ItemID &&
                                    itemUnit.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboResponUnit")).SelectedValue
                                );
                        else
                            query.InnerJoin(itemUnit).On(
                                    query.ItemID == itemUnit.ItemID &&
                                    itemUnit.ServiceUnitID == ((RadTextBox)Helper.FindControlRecursive(Page, "txtFromServiceUnitID")).Text
                                );
                    }
                    else
                    {
                        query.InnerJoin(itemUnit).On(
                                query.ItemID == itemUnit.ItemID &&
                                itemUnit.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboServiceUnitID")).SelectedValue
                            );
                    }
                    query.LeftJoin(balance).On(
                        query.ItemID == balance.ItemID &&
                        balance.LocationID == (string)ViewState["LocationID" + Request.UserHostName]
                    );
                    query.Where(query.SRItemType == ItemType.Laboratory,
                            query.Or(
                                    query.ItemName.Like(searchTextContain),
                                    query.ItemID.Like(searchTextContain)
                                ),
                            query.IsActive == true,
                            query.Or(
                                    query.GuarantorID.IsNull(),
                                    query.GuarantorID == string.Empty,
                                    query.GuarantorID == ViewState["GuarantorID" + Request.UserHostName].ToString())
                        );

                    restrictions = new GuarantorItemRestrictionsQuery("rest");
                    itmrest = new ItemQuery("itmrest");
                    restrictions.Select(restrictions.ItemID);
                    restrictions.InnerJoin(itmrest).On(restrictions.ItemID == itmrest.ItemID);
                    restrictions.Where(restrictions.GuarantorID == ViewState["GuarantorID" + Request.UserHostName].ToString(),
                                       itmrest.SRItemType == ItemType.Laboratory);
                    DataTable dtRest2 = restrictions.LoadDataTable();
                    if (dtRest2.Rows.Count > 0)
                    {
                        //query.InnerJoin(restrictions).On(query.ItemID == restrictions.ItemID && restrictions.GuarantorID == GuarantorID.SelectedValue);
                        if (guar.IsItemLabRestrictionStatusAllowed ?? true)
                            query.Where(query.ItemID.In(restrictions));
                        else
                            query.Where(query.ItemID.NotIn(restrictions));
                    }
                        
                    tempTbl.Merge(query.LoadDataTable());
                }

                //item rad
                if (tempTbl.Rows.Count < 30)
                {
                    query = new ItemQuery("a");
                    itemUnit = new ServiceUnitItemServiceQuery("c");
                    balance = new ItemBalanceQuery("d");

                    query.es.Top = 30;
                    query.Select
                        (
                            query.ItemID,
                            (query.ItemName + " [" + query.ItemID + "]").As("ItemName"),
                            (balance.Balance.Coalesce("0")).As("Balance"),
                            query.SRItemType,
                            itemUnit.ServiceUnitID.Coalesce("''")
                        );
                    if (Request.QueryString["type"] == "tr" || Request.QueryString["type"] == "mcu")
                    {
                        if (Helper.FindControlRecursive(Page, "pnlResponUnit").Visible)
                            query.InnerJoin(itemUnit).On(
                                    query.ItemID == itemUnit.ItemID &&
                                    itemUnit.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboResponUnit")).SelectedValue
                                );
                        else
                            query.InnerJoin(itemUnit).On(
                                    query.ItemID == itemUnit.ItemID &&
                                    itemUnit.ServiceUnitID == ((RadTextBox)Helper.FindControlRecursive(Page, "txtFromServiceUnitID")).Text
                                );
                    }
                    else
                    {
                        query.InnerJoin(itemUnit).On(
                                query.ItemID == itemUnit.ItemID &&
                                itemUnit.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboServiceUnitID")).SelectedValue
                            );
                    }
                    query.LeftJoin(balance).On(
                        query.ItemID == balance.ItemID &&
                        balance.LocationID == (string)ViewState["LocationID" + Request.UserHostName]
                    );
                    query.Where(query.SRItemType == ItemType.Radiology,
                            query.Or(
                                    query.ItemName.Like(searchTextContain),
                                    query.ItemID.Like(searchTextContain)
                                ),
                            query.IsActive == true,
                            query.Or(
                                    query.GuarantorID.IsNull(),
                                    query.GuarantorID == string.Empty,
                                    query.GuarantorID == ViewState["GuarantorID" + Request.UserHostName].ToString())
                        );

                    restrictions = new GuarantorItemRestrictionsQuery("rest");
                    itmrest = new ItemQuery("itmrest");
                    restrictions.Select(restrictions.ItemID);
                    restrictions.InnerJoin(itmrest).On(restrictions.ItemID == itmrest.ItemID);
                    restrictions.Where(restrictions.GuarantorID == ViewState["GuarantorID" + Request.UserHostName].ToString(),
                                       itmrest.SRItemType == ItemType.Radiology);
                    DataTable dtRest2 = restrictions.LoadDataTable();
                    if (dtRest2.Rows.Count > 0)
                    {
                        //query.InnerJoin(restrictions).On(query.ItemID == restrictions.ItemID && restrictions.GuarantorID == GuarantorID.SelectedValue);
                        if (guar.IsItemRadRestrictionStatusAllowed ?? true)
                            query.Where(query.ItemID.In(restrictions));
                        else
                            query.Where(query.ItemID.NotIn(restrictions));
                    }

                    tempTbl.Merge(query.LoadDataTable());
                }

                //item package
                if (tempTbl.Rows.Count < 30)
                {
                    query = new ItemQuery("a");
                    itemUnit = new ServiceUnitItemServiceQuery("c");
                    balance = new ItemBalanceQuery("d");

                    query.es.Top = 30;
                    query.Select
                        (
                            query.ItemID,
                            (query.ItemName + " [" + query.ItemID + "]").As("ItemName"),
                            (balance.Balance.Coalesce("0")).As("Balance"),
                            query.SRItemType,
                            itemUnit.ServiceUnitID.Coalesce("''")
                        );
                    if (Request.QueryString["type"] == "tr" || Request.QueryString["type"] == "mcu")
                    {
                        if (Helper.FindControlRecursive(Page, "pnlResponUnit").Visible)
                            query.InnerJoin(itemUnit).On(
                                    query.ItemID == itemUnit.ItemID &&
                                    itemUnit.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboResponUnit")).SelectedValue
                                );
                        else
                            query.InnerJoin(itemUnit).On(
                                    query.ItemID == itemUnit.ItemID &&
                                    itemUnit.ServiceUnitID == ((RadTextBox)Helper.FindControlRecursive(Page, "txtFromServiceUnitID")).Text
                                );
                    }
                    else
                    {
                        query.InnerJoin(itemUnit).On(
                                query.ItemID == itemUnit.ItemID &&
                                itemUnit.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboServiceUnitID")).SelectedValue
                            );
                    }
                    query.LeftJoin(balance).On(
                        query.ItemID == balance.ItemID &&
                        balance.LocationID == (string)ViewState["LocationID" + Request.UserHostName]
                    );
                    query.Where(query.SRItemType == ItemType.Package,
                            query.Or(
                                    query.ItemName.Like(searchTextContain),
                                    query.ItemID.Like(searchTextContain)
                                ),
                            query.IsActive == true,
                            query.Or(
                                    query.GuarantorID.IsNull(),
                                    query.GuarantorID == string.Empty,
                                    query.GuarantorID == ViewState["GuarantorID" + Request.UserHostName].ToString())
                        );

                    restrictions = new GuarantorItemRestrictionsQuery("rest");
                    itmrest = new ItemQuery("itmrest");
                    restrictions.Select(restrictions.ItemID);
                    restrictions.InnerJoin(itmrest).On(restrictions.ItemID == itmrest.ItemID);
                    restrictions.Where(restrictions.GuarantorID == ViewState["GuarantorID" + Request.UserHostName].ToString(),
                                       itmrest.SRItemType == ItemType.Package);
                    DataTable dtRest2 = restrictions.LoadDataTable();
                    if (dtRest2.Rows.Count > 0)
                    {
                        //query.InnerJoin(restrictions).On(query.ItemID == restrictions.ItemID && restrictions.GuarantorID == GuarantorID.SelectedValue);
                        if (guar.IsItemServiceRestrictionStatusAllowed ?? true)
                            query.Where(query.ItemID.In(restrictions));
                        else
                            query.Where(query.ItemID.NotIn(restrictions));
                    }

                    tempTbl.Merge(query.LoadDataTable());
                }

                // Sort
                var dataSorted = tempTbl.Select("", "ItemName ASC").CopyToDataTable();

                dataSorted.AcceptChanges();
                tbl = dataSorted;
                #endregion
            }
            catch
            { }

            return tbl;
        }

        private DataTable PopulateProductItem(string parameter)
        {
            DataTable tbl = null;
            string searchTextContain = string.Format("%{0}%", parameter);

            try
            {
                #region new - melihat batasan item di guarantor
                bool isFornas = false;
                bool isFormularium = false;
                bool isGeneric = false;
                bool isNonGeneric = false;
                bool isNonGenericLimited = false;

                var guar = new Guarantor();
                if (guar.LoadByPrimaryKey(ViewState["GuarantorID" + Request.UserHostName].ToString()))
                {
                    isFornas = (guar.IsItemRestrictionsFornas ?? false);
                    isFormularium = (guar.IsItemRestrictionsFormularium ?? false);
                    isGeneric = (guar.IsItemRestrictionsGeneric ?? false);
                    isNonGeneric = (guar.IsItemRestrictionsNonGeneric ?? false);
                    isNonGenericLimited = (guar.IsItemRestrictionsNonGenericLimited ?? false);
                }

                var query = new ItemQuery("a");
                var balance = new ItemBalanceQuery("b");
                var product = new VwItemProductMedicNonMedicQuery("c");

                query.es.Top = 30;
                query.Select
                    (
                        query.ItemID,
                        query.ItemName,
                        (balance.Balance.Coalesce("0")).As("Balance"),
                        query.SRItemType,
                        "<'' AS ServiceUnitID>"
                    );
                query.InnerJoin(product).On(
                    query.ItemID == product.ItemID &&
                    product.IsSalesAvailable == true
                    );
                query.InnerJoin(balance).On
                    (
                        query.ItemID == balance.ItemID &&
                        balance.LocationID == (string)ViewState["LocationID" + Request.UserHostName]
                    );
                query.Where
                    (
                        query.Or
                            (
                                query.ItemName.Like(searchTextContain),
                                query.ItemID.Like(searchTextContain)
                            ),
                        query.IsActive == true
                    );

                var xx = new List<Temiang.Dal.DynamicQuery.esComparison>();

                if (isFornas)
                    xx.Add(product.IsFornas == true);

                if (isFormularium)
                    xx.Add(product.IsFormularium == true);

                if (isGeneric)
                    xx.Add(product.IsGeneric == true);

                if (isNonGeneric)
                    xx.Add(product.IsNonGeneric == true);

                if (isNonGenericLimited)
                    xx.Add(product.IsNonGenericLimited == true);

                if (xx.Count > 0)
                    query.Where(query.Or(xx.ToArray()));
                else
                {
                    var restrictions = new GuarantorItemRestrictionsQuery("rest");
                    var itmrest = new ItemQuery("itmrest");
                    restrictions.Select(restrictions.ItemID);
                    restrictions.InnerJoin(itmrest).On(restrictions.ItemID == itmrest.ItemID);
                    restrictions.Where(restrictions.GuarantorID == ViewState["GuarantorID" + Request.UserHostName].ToString(),
                                       itmrest.SRItemType.In(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen));
                    DataTable dtRest = restrictions.LoadDataTable();
                    if (dtRest.Rows.Count > 0)
                    {
                        //query.InnerJoin(restrictions).On(query.ItemID == restrictions.ItemID && restrictions.GuarantorID == GuarantorId.SelectedValue);
                        if (guar.IsItemProductRestrictionStatusAllowed ?? true)
                            query.Where(query.ItemID.In(restrictions));
                        else
                            query.Where(query.ItemID.NotIn(restrictions));
                    }
                }

                if (AppSession.Parameter.IsListItemForTxOnlyInStock)
                    query.Where(balance.Balance > 0);
                query.OrderBy(query.SRItemType.Ascending, query.ItemName.Ascending);

                tbl = query.LoadDataTable();
                #endregion
            }
            catch
            {
            }

            return tbl;
        }

        private DataTable PopulateItem(string parameter)
        {
            var tbl = new DataTable();

            DataTable temp1 = PopulateServiceItem(parameter);

            if (temp1 != null)
                tbl.Merge(temp1);

            //if (Request.QueryString["type"] != "mcu")
            {
                DataTable temp2 = PopulateProductItem(parameter);
                if (temp2 != null)
                    tbl.Merge(temp2);
            }

            return tbl;
        }

        protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            DataTable tbl = PopulateItem(e.Text);
            cboItemID.DataSource = tbl.Rows.Count == 0 ? PopulateItem(e.Text) : tbl;
            cboItemID.DataBind();
        }

        private void PopulateTariff(string itemID)
        {
            var entity = new Item();
            entity.LoadByPrimaryKey(itemID);

            switch (entity.SRItemType)
            {
                case ItemType.Medical:
                    {
                        var item = new ItemProductMedic();
                        item.LoadByPrimaryKey(itemID);

                        txtChargeQuantity.Value = 1;
                        txtItemUnit.Text = item.SRItemUnit;
                        txtStockQuantity.ReadOnly = false;
                        txtStockQuantity.Value = 1D;
                        chkIsItemTypeService.Checked = false;

                        ClearFlagCheckBox();
                    }
                    break;
                case ItemType.NonMedical:
                    {
                        var item = new ItemProductNonMedic();
                        item.LoadByPrimaryKey(itemID);

                        txtChargeQuantity.Value = 1;
                        txtItemUnit.Text = item.SRItemUnit;
                        txtStockQuantity.ReadOnly = false;
                        txtStockQuantity.Value = 1D;
                        chkIsItemTypeService.Checked = false;

                        ClearFlagCheckBox();
                    }
                    break;
                    case ItemType.Kitchen:
                    {
                        var item = new ItemKitchen();
                        item.LoadByPrimaryKey(itemID);

                        txtChargeQuantity.Value = 1;
                        txtItemUnit.Text = item.SRItemUnit;
                        txtStockQuantity.ReadOnly = false;
                        txtStockQuantity.Value = 1D;
                        chkIsItemTypeService.Checked = false;

                        ClearFlagCheckBox();
                    }
                    break;
                case ItemType.Service:
                    {
                        var item = new ItemService();
                        item.LoadByPrimaryKey(itemID);

                        txtChargeQuantity.Value = 1;
                        txtItemUnit.Text = item.SRItemUnit;
                        txtStockQuantity.ReadOnly = true;
                        txtStockQuantity.Value = 0D;
                        chkIsItemTypeService.Checked = true;

                        ClearFlagCheckBox();

                        pnlAsset.Visible = false;
                    }
                    break;
                case ItemType.Diagnostic:
                    {
                        var item = new ItemDiagnostic();
                        item.LoadByPrimaryKey(itemID);

                        txtChargeQuantity.Value = 1;
                        txtItemUnit.Text = "X";
                        txtStockQuantity.ReadOnly = true;
                        txtStockQuantity.Value = 0D;
                        chkIsItemTypeService.Checked = true;

                        ClearFlagCheckBox();

                        pnlAsset.Visible = false;
                    }
                    break;
                case ItemType.Laboratory:
                    {
                        var item = new ItemLaboratory();
                        item.LoadByPrimaryKey(itemID);

                        txtChargeQuantity.Value = 1;
                        txtItemUnit.Text = "X";
                        txtStockQuantity.ReadOnly = true;
                        txtStockQuantity.Value = 0D;
                        chkIsItemTypeService.Checked = true;

                        ClearFlagCheckBox();

                        pnlAsset.Visible = false;
                    }
                    break;
                case ItemType.Radiology:
                    {
                        var item = new ItemRadiology();
                        item.LoadByPrimaryKey(itemID);

                        txtChargeQuantity.Value = 1;
                        txtItemUnit.Text = "X";
                        txtStockQuantity.ReadOnly = true;
                        txtStockQuantity.Value = 0D;
                        chkIsItemTypeService.Checked = true;

                        ClearFlagCheckBox();

                        pnlAsset.Visible = false;
                    }
                    break;
                default:
                    txtChargeQuantity.Value = 1;
                    txtItemUnit.Text = "X";
                    txtStockQuantity.ReadOnly = true;
                    txtStockQuantity.Value = 0D;
                    ClearFlagCheckBox();
                    pnlAsset.Visible = false;
                    chkIsPackage.Checked = true;
                    chkIsItemTypeService.Checked = false;
                    break;
            }

            var itemRooms = new AppStandardReferenceItemCollection();
            itemRooms.Query.Where(itemRooms.Query.StandardReferenceID == "ItemTariffRoom",
                                  itemRooms.Query.ItemID == itemID, itemRooms.Query.IsActive == true);
            itemRooms.LoadAll();
            chkIsItemRoom.Checked = itemRooms.Count > 0;

            var compColl = new TariffComponentCollection();
            compColl.Query.OrderBy(compColl.Query.TariffComponentID.Ascending);
            compColl.LoadAll();

            var transDate = ((RadDatePicker)Helper.FindControlRecursive(Page, "txtTransactionDate")).SelectedDate.Value;

            var coll = Helper.Tariff.GetItemTariffComponentCollection(transDate, (string)ViewState["SRTariffType" + Request.UserHostName],
                AppSession.Parameter.OutPatientClassID, itemID);
            if (!coll.Any())
                coll = Helper.Tariff.GetItemTariffComponentCollection(transDate, (string)ViewState["SRTariffType" + Request.UserHostName],
                    AppSession.Parameter.DefaultTariffClass, itemID);
            if (!coll.Any())
                coll = Helper.Tariff.GetItemTariffComponentCollection(transDate, AppSession.Parameter.DefaultTariffType,
                    AppSession.Parameter.OutPatientClassID, itemID);
            if (!coll.Any())
                coll = Helper.Tariff.GetItemTariffComponentCollection(transDate, AppSession.Parameter.DefaultTariffType,
                       AppSession.Parameter.DefaultTariffClass, itemID);

            //var reg = new Registration();
            //reg.LoadByPrimaryKey(TxtRegistrationNo.Text);

            //var grr = new Guarantor();
            //grr.LoadByPrimaryKey(GuarantorID.SelectedValue);

            //if (grr.TariffCalculationMethod == 1) transDate = reg.RegistrationDate.Value.Date;

            //var tariff = Helper.Tariff.GetItemTariff(transDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, AppSession.Parameter.DefaultTariffClass, itemID, (string)ViewState["GuarantorID" + Request.UserHostName], false, reg.SRRegistrationType);
            var tariff = (Helper.Tariff.GetItemTariff(transDate, (string)ViewState["SRTariffType" + Request.UserHostName], AppSession.Parameter.OutPatientClassID, AppSession.Parameter.OutPatientClassID, itemID, (string)ViewState["GuarantorID" + Request.UserHostName], false, AppConstant.RegistrationType.OutPatient) ??
                          Helper.Tariff.GetItemTariff(transDate, (string)ViewState["SRTariffType" + Request.UserHostName], AppSession.Parameter.DefaultTariffClass, AppSession.Parameter.OutPatientClassID, itemID, (string)ViewState["GuarantorID" + Request.UserHostName], false, AppConstant.RegistrationType.OutPatient)) ??
                         (Helper.Tariff.GetItemTariff(transDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.OutPatientClassID, AppSession.Parameter.OutPatientClassID, itemID, (string)ViewState["GuarantorID" + Request.UserHostName], false, AppConstant.RegistrationType.OutPatient) ??
                          Helper.Tariff.GetItemTariff(transDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, AppSession.Parameter.OutPatientClassID, itemID, (string)ViewState["GuarantorID" + Request.UserHostName], false, AppConstant.RegistrationType.OutPatient));

            if (tariff != null)
            {
                chkIsVariable.Enabled = tariff.IsAllowVariable ?? false;
                chkIsDiscount.Enabled = tariff.IsAllowDiscount ?? false;
                chkIsCito.Enabled = tariff.IsAllowCito ?? false;
                cboSRCitoPercentage.Enabled = chkIsCito.Enabled && (tariff.IsCitoFromStandardReference ?? false);
                ViewState["IsAdminCalculation"] = tariff.IsAdminCalculation ?? false;
            }

            //  if (Request.QueryString["type"] == "tr" || Request.QueryString["type"] == "ds" || Request.QueryString["type"] == "mcu")
            //  {
            pnltariff.Visible = true;

            if (!coll.Any() || entity.SRItemType == ItemType.Medical || entity.SRItemType == ItemType.NonMedical)
            {
                pnlVariableAndDiscount.Visible = true;
                pnlComponentTariff.Visible = false;
                pnlPriceJO.Visible = false;
                //if (chkIsItemRoom.Checked && ChkIsRoomIn.Checked)
                //    txtPrice1.Value = tariff != null
                //                          ? (double)(tariff.Price ?? 0) -
                //                            ((double)(tariff.Price ?? 0) * TxtTariffDiscForRoomIn.Value / 100)
                //                          : 0D;
                //else
                txtPrice1.Value = tariff != null ? (double)(tariff.Price ?? 0) : 0D;
            }
            else
            {
                pnlVariableAndDiscount.Visible = false;
                pnlComponentTariff.Visible = true;
                pnlPriceJO.Visible = false;

                PopulateTariffComponentGrid(coll);
            }
            // }
            //else
            //{
            //    pnltariff.Visible = false;

            //    pnlPriceJO.Visible = true;
            //    if (chkIsItemRoom.Checked && ChkIsRoomIn.Checked)
            //        txtPriceJO.Value = tariff != null
            //                              ? (double)(tariff.Price ?? 0) -
            //                                ((double)(tariff.Price ?? 0) * TxtTariffDiscForRoomIn.Value / 100)
            //                              : 0D;
            //    else
            //        txtPriceJO.Value = tariff != null ? (double)(tariff.Price ?? 0) : 0D;
            //}
        }

        private string GetCoveredItem(string itemId)
        {
            var grr = new Guarantor();
            if (grr.LoadByPrimaryKey(ViewState["GuarantorID" + Request.UserHostName].ToString()) && grr.SRGuarantorType != AppSession.Parameter.GuarantorTypeSelf && grr.SRBusinessMethod == AppSession.Parameter.BusinessMethodCoverage)
            {
                bool isUsingDefaultItemRule = grr.IsItemRuleUsingDefaultAmountValue ?? false;
                bool isInclude, isGuarantor;
                string srGuarantorRuleType = grr.SRGuarantorRuleType;
                decimal amountValue = grr.AmountValue ?? 0;
                bool isValueInPercent = grr.IsValueInPercent ?? true;

                string regType;

                if (isUsingDefaultItemRule)
                    regType = "IPR";
                else
                    regType = "OPR";

                switch (regType)
                {
                    case "IPR":
                        amountValue = grr.AmountValue ?? 0;
                        break;
                    case "EMR":
                        amountValue = grr.EmergencyAmountValue ?? 0;
                        break;
                    default:
                        amountValue = grr.OutpatientAmountValue ?? 0;
                        break;
                }

                var types = new GuarantorItemTypeRuleCollection();
                types.Query.Where(types.Query.GuarantorID == grr.GuarantorID);
                types.LoadAll();

                var item = new Item();
                item.LoadByPrimaryKey(itemId);
                if (item.SRItemType == ItemType.Medical)
                {
                    isInclude = grr.IsIncludeItemMedical ?? false;
                    isGuarantor = grr.IsIncludeItemMedicalToGuarantor ?? false;
                }
                else if (item.SRItemType == ItemType.NonMedical || item.SRItemType == ItemType.Kitchen)
                {
                    isInclude = grr.IsIncludeItemNonMedical ?? false;
                    isGuarantor = grr.IsIncludeItemNonMedicalToGuarantor ?? false;
                }
                else
                {
                    isInclude = true;
                    if (types.AsEnumerable().Any()) isGuarantor = types.SingleOrDefault(t => t.SRItemType == item.SRItemType).IsToGuarantor ?? false;
                    else isGuarantor = true;
                }

                var grrItem = new GuarantorItemRule();
                if (grrItem.LoadByPrimaryKey(grr.GuarantorID, itemId))
                {
                    srGuarantorRuleType = grrItem.SRGuarantorRuleType;

                    switch (regType)
                    {
                        case "IPR":
                            amountValue = grrItem.AmountValue ?? 0;
                            break;
                        case "EMR":
                            amountValue = grrItem.EmergencyAmountValue ?? 0;
                            break;
                        default:
                            amountValue = grrItem.OutpatientAmountValue ?? 0;
                            break;
                    }

                    isValueInPercent = grrItem.IsValueInPercent ?? false;
                    isInclude = grrItem.IsInclude ?? false;
                    isGuarantor = grrItem.IsToGuarantor ?? false;
                }

                //var regItems = new RegistrationItemRuleCollection();
                //regItems.Query.Where(regItems.Query.RegistrationNo == TxtRegistrationNo.Text, regItems.Query.ItemID == itemId);
                //regItems.LoadAll();
                //foreach (var regItem in regItems)
                //{
                //    srGuarantorRuleType = regItem.SRGuarantorRuleType;
                //    amountValue = regItem.AmountValue ?? 0;
                //    isValueInPercent = regItem.IsValueInPercent ?? false;
                //    isInclude = regItem.IsInclude ?? false;
                //    isGuarantor = regItem.IsToGuarantor ?? false;
                //}

                if (string.IsNullOrEmpty(srGuarantorRuleType) & isGuarantor)
                    return string.Empty;

                if (string.IsNullOrEmpty(srGuarantorRuleType) & isGuarantor == false)
                    return "** Item are not covered by guarantor.";

                var msg = "** Item guarantor rule: ";
                if (isInclude)
                {
                    var std = new AppStandardReferenceItem();
                    if (std.LoadByPrimaryKey(AppEnum.StandardReference.GuarantorRuleType.ToString(), srGuarantorRuleType))
                    {
                        msg += std.ItemName + " with value ";

                        if (isValueInPercent)
                            msg += string.Format("{0:n0}", amountValue) + "%";
                        else
                            msg += "Rp. " + string.Format("{0:n0}", amountValue);

                        if (isGuarantor)
                            msg += " to Guarantor";
                        else
                            msg += " to Patient";
                    }
                }

                return msg;
            }
            return string.Empty;


            //if (ViewState["GuarantorID" + Request.UserHostName] != AppSession.Parameter.SelfGuarantor)
            //{
            //    bool isInclude, isGuarantor;
            //    string srGuarantorRuleType = string.Empty;
            //    decimal amountValue = 0;
            //    bool isValueInPercent = true;

            //    var grr = new Guarantor();
            //    grr.LoadByPrimaryKey(ViewState["GuarantorID" + Request.UserHostName]);

            //    var types = new GuarantorItemTypeRuleCollection();
            //    types.Query.Where(types.Query.GuarantorID == ViewState["GuarantorID" + Request.UserHostName]);
            //    types.LoadAll();

            //    var item = new Item();
            //    item.LoadByPrimaryKey(itemId);
            //    if (item.SRItemType == ItemType.Medical)
            //    {
            //        isInclude = grr.IsIncludeItemMedical ?? false;
            //        isGuarantor = grr.IsIncludeItemMedicalToGuarantor ?? false;
            //    }
            //    else if (item.SRItemType == ItemType.NonMedical || item.SRItemType == ItemType.Kitchen)
            //    {
            //        isInclude = grr.IsIncludeItemNonMedical ?? false;
            //        isGuarantor = grr.IsIncludeItemNonMedicalToGuarantor ?? false;
            //    }
            //    else
            //    {
            //        isInclude = true;
            //        if (types.AsEnumerable().Any())
            //            isGuarantor = types.SingleOrDefault(t => t.SRItemType == item.SRItemType).IsToGuarantor ?? false;
            //        else
            //            isGuarantor = true;
            //    }

            //    var grrItem = new GuarantorItemRule();
            //    if (grrItem.LoadByPrimaryKey(ViewState["GuarantorID" + Request.UserHostName], itemId))
            //    {
            //        srGuarantorRuleType = grrItem.SRGuarantorRuleType;
            //        amountValue = grrItem.AmountValue ?? 0;
            //        isValueInPercent = grrItem.IsValueInPercent ?? false;
            //        isInclude = grrItem.IsInclude ?? false;
            //        isGuarantor = grrItem.IsToGuarantor ?? false;
            //    }

            //    //var regItems = new RegistrationItemRuleCollection();
            //    //regItems.Query.Where(regItems.Query.RegistrationNo == TxtRegistrationNo.Text, regItems.Query.ItemID == itemId);
            //    //regItems.LoadAll();
            //    //foreach (var regItem in regItems)
            //    //{
            //    //    srGuarantorRuleType = regItem.SRGuarantorRuleType;
            //    //    amountValue = regItem.AmountValue ?? 0;
            //    //    isValueInPercent = regItem.IsValueInPercent ?? false;
            //    //    isInclude = regItem.IsInclude ?? false;
            //    //    isGuarantor = regItem.IsToGuarantor ?? false;
            //    //}

            //    if (srGuarantorRuleType == string.Empty & isGuarantor)
            //        return string.Empty;

            //    if (srGuarantorRuleType == string.Empty & isGuarantor == false)
            //        return "** Item not covered by the guarantor.";

            //    var msg = "** Item guarantor rule: ";
            //    if (isInclude)
            //    {
            //        var std = new AppStandardReferenceItem();
            //        if (std.LoadByPrimaryKey(AppEnum.StandardReference.GuarantorRuleType.ToString(), srGuarantorRuleType))
            //        {
            //            msg += std.ItemName + " with value ";

            //            if (isValueInPercent)
            //                msg += string.Format("{0:n0}", amountValue) + "%";
            //            else
            //                msg += "Rp. " + string.Format("{0:n0}", amountValue);

            //            if (isGuarantor)
            //                msg += " to Guarantor";
            //            else
            //                msg += " to Patient";
            //        }
            //    }

            //    return msg;
            //}

            //return string.Empty;
        }

        protected void cboItemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (e.Value == string.Empty)
            {
                pnltariff.Visible = false;
                pnlVariableAndDiscount.Visible = false;
                pnlComponentTariff.Visible = false;

                ClearFlagCheckBox();

                txtChargeQuantity.Value = 1;
                txtStockQuantity.ReadOnly = false;
                txtStockQuantity.Value = 0D;
                txtItemUnit.Text = string.Empty;
                cboSRDiscountReason.SelectedValue = string.Empty;
                chkIsAssetUtilization.Checked = false;
                chkIsAssetUtilization.Enabled = false;
                cboAssetID.Enabled = false;
                chkIsItemRoom.Checked = false;

                return;
            }

            PopulateTariff(e.Value);
            lblMsg.Text = GetCoveredItem(e.Value);
        }

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            if (ViewState["paramedic" + Request.UserHostName] == null)
                PopulateParamedicByServiceUnit();

            DataView view = ((DataTable)ViewState["paramedic" + Request.UserHostName]).DefaultView;
            view.RowFilter = string.Format("ParamedicID LIKE '%{0}%' OR ParamedicName LIKE '%{0}%'", e.Text);
            cboParamedicID.DataSource = view;
            cboParamedicID.DataBind();
        }

        protected void cboPhysicianID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboPhysicianID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            if (ViewState["paramedic" + Request.UserHostName] == null)
                PopulateParamedicByServiceUnit();

            DataView view = ((DataTable)ViewState["paramedic" + Request.UserHostName]).DefaultView;
            view.RowFilter = string.Format("ParamedicID LIKE '%{0}%' OR ParamedicName LIKE '%{0}%'", e.Text);

            var combo = o as RadComboBox;

            combo.DataSource = view;
            combo.DataBind();
        }

        #region Properties for return entry value

        public String SequenceNo
        {
            get { return (string)ViewState["SequenceNo" + Request.UserHostName]; }
        }

        public String ItemID
        {
            get { return cboItemID.SelectedValue; }
        }

        public String ItemName
        {
            get { return cboItemID.Text; }
        }

        public String ParamedicID
        {
            get { return cboParamedicID.SelectedValue; }
        }

        public String ParamedicName
        {
            get { return cboParamedicID.Text; }
        }

        public Boolean IsAdminCalculation
        {
            get { return (bool)ViewState["IsAdminCalculation"]; }
        }

        public Boolean IsVariable
        {
            get { return chkIsVariable.Checked; }
        }

        public Boolean IsCito
        {
            get { return chkIsCito.Checked; }
        }

        public string SRCitoPercentage
        {
            get { return cboSRCitoPercentage.SelectedValue; }
        }

        public Boolean IsDiscount
        {
            get { return chkIsDiscount.Checked; }
        }

        public Decimal ChargeQuantity
        {
            get { return Convert.ToDecimal(txtChargeQuantity.Value); }
        }

        public Decimal StockQuantity
        {
            get { return Convert.ToDecimal(txtStockQuantity.Value); }
        }

        public String SRItemUnit
        {
            get { return txtItemUnit.Text; }
        }

        public Decimal Price
        {
            get
            {
                if (Request.QueryString["type"] == "jo")
                    return Convert.ToDecimal(txtPriceJO.Value);
                if (pnlVariableAndDiscount.Visible)
                    return Convert.ToDecimal(txtPrice1.Value);

                decimal price = 0;
                foreach (GridDataItem dataItem in grdTariff.MasterTableView.Items)
                {
                    price +=
                        (decimal)
                        (dataItem["TariffComponentName"].FindControl("txtPrice") as RadNumericTextBox).Value;
                }
                return price;
            }
        }

        public Decimal DiscountAmount
        {
            get
            {
                decimal discount = 0;

                foreach (GridDataItem dataItem in grdTariff.MasterTableView.Items)
                {
                    if ((dataItem["TariffComponentName"].FindControl("txtPercentDiscount") as RadNumericTextBox).Text != string.Empty)
                    {
                        decimal diskonpercent = (decimal)(dataItem["TariffComponentName"].FindControl("txtPercentDiscount") as RadNumericTextBox).Value;

                        discount += (decimal)(dataItem["TariffComponentName"].FindControl("txtPrice") as RadNumericTextBox).Value * (diskonpercent / 100);
                    }
                    else
                    {
                        if ((dataItem["TariffComponentName"].FindControl("txtDiscount") as RadNumericTextBox).Text == string.Empty)
                            discount += (decimal)0D;
                        else
                            discount += (decimal)(dataItem["TariffComponentName"].FindControl("txtDiscount") as RadNumericTextBox).Value;
                    }
                }

                discount = Convert.ToDecimal(txtChargeQuantity.Value ?? 0) * discount;

                if (pnlVariableAndDiscount.Visible)
                    return Convert.ToDecimal(txtChargeQuantity.Value * txtDiscountAmount1.Value);

                return discount;
            }
        }

        public String SRDiscountReason
        {
            get { return cboSRDiscountReason.SelectedValue; }
        }

        public Boolean IsAssetUtilization
        {
            get { return chkIsAssetUtilization.Checked; }
        }

        public String AssetID
        {
            get { return cboAssetID.SelectedValue; }
        }

        public Boolean IsPackage
        {
            get { return chkIsPackage.Checked; }
        }

        public Boolean IsVoid
        {
            get { return chkIsVoid.Checked; }
        }

        public Boolean IsNewRecord
        {
            get { return (bool)ViewState["IsNewRecord"]; }
        }

        public String Notes
        {
            get { return txtNotes.Text; }
        }

        public String CenterID
        {
            get
            {
                if (trCenterID.Visible)
                    return cboCenterID.SelectedValue;
                return string.Empty;
            }
        }

        public Boolean IsItemRoom
        {
            get { return chkIsItemRoom.Checked; }
        }

        public Boolean IsItemTypeService
        {
            get { return chkIsItemTypeService.Checked; }
        }

        public TransChargesItemCompCollection TariffComponent
        {
            get
            {
                if (pnlVariableAndDiscount.Visible)
                    return null;
                var coll = new TransChargesItemCompCollection();
                foreach (GridDataItem dataItem in grdTariff.MasterTableView.Items)
                {
                    TransChargesItemComp entity = coll.AddNew();
                    entity.SequenceNo = (string)ViewState["SequenceNo" + Request.UserHostName];
                    entity.TariffComponentID = dataItem["TariffComponentID"].Text;
                    if ((dataItem["TariffComponentName"].FindControl("txtPrice") as RadNumericTextBox).Text == string.Empty)
                        entity.Price = (decimal)0D;
                    else
                        entity.Price = (decimal)(dataItem["TariffComponentName"].FindControl("txtPrice") as RadNumericTextBox).Value;

                    if ((dataItem["TariffComponentName"].FindControl("txtPercentDiscount") as RadNumericTextBox).Text != string.Empty)
                    {
                        //decimal price = (decimal)(dataItem["TariffComponentName"].FindControl("txtPrice") as RadNumericTextBox).Value;
                        decimal diskonpercent = (decimal)(dataItem["TariffComponentName"].FindControl("txtPercentDiscount") as RadNumericTextBox).Value;

                        entity.DiscountAmount = entity.Price * (diskonpercent / 100);
                        //(dataItem["TariffComponentName"].FindControl("txtDiscount") as RadNumericTextBox).Text = Convert.ToString(price * diskonpercent / 100);
                    }
                    else
                    {
                        if ((dataItem["TariffComponentName"].FindControl("txtDiscount") as RadNumericTextBox).Text == string.Empty)
                            entity.DiscountAmount = (decimal)0D;
                        else
                            entity.DiscountAmount = (decimal)(dataItem["TariffComponentName"].FindControl("txtDiscount") as RadNumericTextBox).Value;
                    }
                    entity.ParamedicID = (dataItem["TariffComponentName"].FindControl("cboPhysicianID") as RadComboBox).SelectedValue;
                }
                return coll;
            }
        }

        #endregion
    }
}