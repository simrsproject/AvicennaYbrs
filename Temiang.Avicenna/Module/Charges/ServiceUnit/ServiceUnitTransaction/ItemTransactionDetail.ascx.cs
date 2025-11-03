using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class ItemTransactionDetail : BaseUserControl
    {
        private AppAutoNumberLast _filmAutoNumber;

        public object DataItem { get; set; }

        private DataTable TariffComponents(IEnumerable<ItemTariffComponent> components)
        {
            DataTable dtb;
            InitTariffComponentTable(out dtb);

            foreach (var entity in components)
            {
                var newRow = dtb.NewRow();
                newRow["TariffComponentID"] = entity.TariffComponentID;
                if (ChkIsRoomIn.Checked && chkIsItemRoom.Checked)
                    newRow["Price"] = entity.Price - (entity.Price * Convert.ToDecimal(TxtTariffDiscForRoomIn.Value) / 100);
                else
                    newRow["Price"] = entity.Price;

                newRow["IsAllowDiscount"] = entity.IsAllowDiscount;
                newRow["IsAllowVariable"] = entity.IsAllowVariable;

                var comp = new TariffComponent();
                comp.LoadByPrimaryKey(entity.TariffComponentID);
                newRow["TariffComponentName"] = comp.TariffComponentName;
                newRow["IsTariffParamedic"] = comp.IsTariffParamedic ?? false;

                dtb.Rows.Add(newRow);
            }

            return dtb;
        }

        private string PageId
        {
            get { return ((HiddenField)Helper.FindControlRecursive(Page, "hdnPageId")).Value; }
        }

        private String LocationID
        {
            get
            {
                return ((RadComboBox)Helper.FindControlRecursive(Page, "cboLocationID")).SelectedValue;
            }
        }

        private CheckBox ChkIsRoomIn
        {
            get
            { return (CheckBox)Helper.FindControlRecursive(Page, "chkIsRoomIn"); }
        }

        private RadNumericTextBox TxtTariffDiscForRoomIn
        {
            get
            { return (RadNumericTextBox)Helper.FindControlRecursive(Page, "txtTariffDiscForRoomIn"); }
        }

        private RadComboBox CboGuarantorId
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboGuarantorID"); }
        }

        private RadTextBox TxtRegistrationNo
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtRegistrationNo"); }
        }

        private RadTextBox TxtPackageReferenceNo
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtPackageReferenceNo"); }
        }

        private RadDatePicker TxtTransactionDate
        {
            get
            { return (RadDatePicker)Helper.FindControlRecursive(Page, "txtTransactionDate"); }
        }

        private RadComboBox cboToServiceUnitID
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboToServiceUnitID"); }
        }

        private HiddenField HdnIsMandatoryBookingNo
        {
            get
            { return (HiddenField)Helper.FindControlRecursive(Page, "hdnIsMandatoryBookingNo"); }
        }

        private RadComboBox CboServiceUnitBookingNo
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboServiceUnitBookingNo"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            tdImage.Visible = AppSession.Parameter.ServiceUnitPathologyAnatomyID.Equals(ToServiceUnitID);

            ViewState["IsNewRecord"] = (DataItem is GridInsertionObject);
            if (DataItem is GridInsertionObject)
            {
                //
            }
            else
            {
                ViewState["ToServiceUnitID" + Request.UserHostName + PageId] = DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.ToServiceUnitID);
            }

            PopulateParamedicByServiceUnit();

            var reg = new Registration();
            reg.LoadByPrimaryKey(TxtRegistrationNo.Text);
            ViewState["GuarantorID" + Request.UserHostName + PageId] = reg.GuarantorID;
            hdnChargeClassId.Value = reg.ChargeClassID;
            hdnSrRegistrationType.Value = reg.SRRegistrationType;
            hdnItemConditionRuleID.Value = reg.ItemConditionRuleID;
            cboItemConditionRuleID.Enabled = string.IsNullOrEmpty(reg.ItemConditionRuleID);

            hdnIsNeedBookingNo.Value = (HdnIsMandatoryBookingNo.Value.ToString() == "y" && string.IsNullOrEmpty(CboServiceUnitBookingNo.SelectedValue)) ? "true" : "false";

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);
            ViewState["SRTariffType" + Request.UserHostName + PageId] = grr.SRTariffType;
            hdnSrTariffType.Value = grr.SRTariffType;

            var gbridging = new GuarantorBridgingCollection();
            gbridging.Query.Where(gbridging.Query.GuarantorID == reg.GuarantorID &&
                                  gbridging.Query.SRBridgingType == AppSession.Parameter.BridgingTypeBpjs &&
                                  gbridging.Query.IsActive == true);
            gbridging.LoadAll();
            if (gbridging.Count > 0)
                ViewState["IsBpjs" + Request.UserHostName + PageId] = "true";
            else
                ViewState["IsBpjs" + Request.UserHostName + PageId] = "false";

            //var unit = new ServiceUnit();
            //if (Request.QueryString["type"] == "tr" || Request.QueryString["type"] == "mcu")
            //{
            //    unit.LoadByPrimaryKey(((HtmlTableRow)Helper.FindControlRecursive(Page, "pnlResponUnit")).Visible
            //        ? ((RadComboBox)Helper.FindControlRecursive(Page, "cboResponUnit")).SelectedValue
            //        : ((RadComboBox)Helper.FindControlRecursive(Page, "cboFromServiceUnitID")).SelectedValue);
            //}
            //else
            //    unit.LoadByPrimaryKey(((RadComboBox)Helper.FindControlRecursive(Page, "cboToServiceUnitID")).SelectedValue);
            //ViewState["LocationID" + Request.UserHostName + PageId] = unit.LocationID;
            //ViewState["LocationID" + Request.UserHostName + PageId] = ((RadComboBox)Helper.FindControlRecursive(Page, "cboLocationID")).SelectedValue;

            StandardReference.InitializeIncludeSpace(cboSRDiscountReason, AppEnum.StandardReference.DiscountReason);
            StandardReference.InitializeIncludeSpace(cboCenterID, AppEnum.StandardReference.CenterID);
            StandardReference.InitializeIncludeSpace(cboSRCitoPercentage, AppEnum.StandardReference.CitoPercentage);

            ViewState["IsAdminCalculation"] = false;

            if (Request.QueryString["type"] == "tr" || Request.QueryString["type"] == "ds" || Request.QueryString["type"] == "mcu")
            {
                pnlParamedic.Visible = false;

                if (AppSession.Parameter.IsTariffPriceVisibleOnlyForAdm)
                {
                    pnlDiscountReason.Visible = (!string.IsNullOrEmpty(Request.QueryString["verif"]) &&
                                         (Request.QueryString["verif"] == "1"));
                }
                else
                    pnlDiscountReason.Visible = AppSession.Parameter.IsAllowDiscountOnTransEntry;

                pnlPackage.Visible = true;
            }
            else
            {
                pnlParamedic.Visible = false;
                pnlDiscountReason.Visible = false;
                pnlPackage.Visible = false;
            }

            trCenterID.Visible = Request.QueryString["resp"] == "1";
            trItemGroup.Visible = AppSession.Parameter.IsVisibleItemGroupOnTx;
            if (trItemGroup.Visible)
            {
                if (Session["cboItemGroupID" + Request.UserHostName + PageId] != null)
                {
                    var itmGroup = new ItemGroupQuery();
                    itmGroup.Where(itmGroup.ItemGroupID == Session["cboItemGroupID" + Request.UserHostName + PageId]);
                    cboItemGroupID.DataSource = itmGroup.LoadDataTable();
                    cboItemGroupID.DataBind();
                    cboItemGroupID.SelectedValue = Session["cboItemGroupID" + Request.UserHostName + PageId].ToString();
                }
            }

            if (DataItem is GridInsertionObject)
            {
                //cboItemID.Enabled = true;
                cboItemID.Enabled = (hdnIsNeedBookingNo.Value.ToString() == "false");
                lblAlertBookingNo.Visible = (hdnIsNeedBookingNo.Value.ToString() == "true");

                ViewState["IsNewRecord"] = true;

                var coll = (TransChargesItemCollection)Session["collTransChargesItem" + Request.UserHostName + PageId];
                if (coll.Count == 0)
                    hdnSequenceNo.Value = "001";
                else
                {
                    var sequenceNo = (coll.Where(c => c.ParentNo == string.Empty).OrderByDescending(c => c.SequenceNo).Select(c => c.SequenceNo)).Take(1);
                    int seqNo = int.Parse(sequenceNo.Single()) + 1;
                    hdnSequenceNo.Value = string.Format("{0:000}", seqNo);
                }

                //if (((RadComboBox)Helper.FindControlRecursive(Page, "cboToServiceUnitID")).SelectedValue == AppSession.Parameter.ServiceUnitLaboratoryID)
                //{
                //    var psdColl = new ParamedicScheduleDateCollection();
                //    DataTable dtb = psdColl.GetParamedicID(AppSession.Parameter.ServiceUnitLaboratoryID, hdnSrRegistrationType.Value);

                //    if (dtb.Rows.Count == 1)
                //    {
                //        DataView view = ((DataTable)ViewState["paramedic" + Request.UserHostName + PageId]).DefaultView;
                //        view.RowFilter = string.Format("ParamedicID LIKE '%{0}%' OR ParamedicName LIKE '%{0}%'",
                //            dtb.Rows[0]["ParamedicID"].ToString());
                //        cboParamedicID.DataSource = view;
                //        cboParamedicID.DataBind();

                //        cboParamedicID.SelectedValue = dtb.Rows[0]["ParamedicID"].ToString();
                //        cboParamedicID.Text = dtb.Rows[0]["ParamedicName"].ToString();
                //    }
                //}

                if (Request.QueryString["type"] == "ds")
                {
                    var psdColl = new ParamedicScheduleDateCollection();
                    DataTable dtb = psdColl.GetParamedicID(ToServiceUnitID, hdnSrRegistrationType.Value);

                    if (dtb.Rows.Count == 1)
                    {
                        DataView view = ((DataTable)ViewState["paramedic" + Request.UserHostName + PageId]).DefaultView;
                        view.RowFilter = string.Format("ParamedicID LIKE '%{0}%' OR ParamedicName LIKE '%{0}%'",
                            dtb.Rows[0]["ParamedicID"].ToString());
                        cboParamedicID.DataSource = view;
                        cboParamedicID.DataBind();

                        cboParamedicID.SelectedValue = dtb.Rows[0]["ParamedicID"].ToString();
                        cboParamedicID.Text = dtb.Rows[0]["ParamedicName"].ToString();
                    }
                }

                chkIsVoid.Enabled = false;
                return;
            }
            cboItemID.Enabled = false;
            lblAlertBookingNo.Visible = false;

            ViewState["IsNewRecord"] = false;
            hdnSequenceNo.Value = DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.SequenceNo).ToString();

            var itQuery = new ItemQuery("a");
            var ig = new ItemGroupQuery("ig");
            itQuery.LeftJoin(ig).On(itQuery.ItemGroupID == ig.ItemGroupID);

            itQuery.Where(itQuery.ItemID == (String)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.ItemID));
            itQuery.Select
                (
                    itQuery.ItemID,
                    (itQuery.ItemName + " [" + itQuery.ItemID + "]").As("ItemName"),
                    @"<0 AS Balance>",
                    itQuery.SRItemType,
                    @"<'' AS ServiceUnitID>",
                    itQuery.Notes,
                    ig.ItemGroupName
                );

            cboItemID.DataSource = itQuery.LoadDataTable(); //PopulateItem((String)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.ItemID), true);
            cboItemID.DataBind();
            cboItemID.SelectedValue = (String)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.ItemID);

            PopulateTariff((String)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.ItemID));

            chkIsCito.Checked = (bool)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.IsCito);
            cboSRCitoPercentage.SelectedValue = (String)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.SRCitoPercentage);

            txtChargeQuantity.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.ChargeQuantity));
            txtStockQuantity.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.StockQuantity));
            txtItemUnit.Text = (String)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.SRItemUnit);
            try
            {
                txtTariffDate.SelectedDate = Convert.ToDateTime(DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.TariffDate));
            }
            catch
            {
                txtTariffDate.SelectedDate = DateTime.Now;
            }

            if (Request.QueryString["type"] == "tr" || Request.QueryString["type"] == "ds" || Request.QueryString["type"] == "mcu")
            {
                if (ViewState["paramedic" + Request.UserHostName + PageId] == null)
                    PopulateParamedicByServiceUnit();

                DataView view = ((DataTable)ViewState["paramedic" + Request.UserHostName + PageId]).DefaultView;
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
            }
            else
                txtPriceJO.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.Price));

            if (Request.QueryString["type"] != "mcu")
                chkIsVoid.Enabled = true;

            chkIsVoid.Checked = (bool)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.IsVoid);
            txtNotes.Text = (String)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.Notes);
            chkIsItemRoom.Checked = (bool)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.IsItemRoom);
            txtFilmNo.Text = (String)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.FilmNo);

            if (!string.IsNullOrEmpty((String)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.ItemConditionRuleID)))
            {
                var ruleQ = new ItemConditionRuleQuery();
                ruleQ.Where(ruleQ.ItemConditionRuleID ==
                            (String)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.ItemConditionRuleID));
                cboItemConditionRuleID.DataSource = ruleQ.LoadDataTable();
                cboItemConditionRuleID.DataBind();
                cboItemConditionRuleID.SelectedValue = (String)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.ItemConditionRuleID);
            }

            if (trCenterID.Visible)
                cboCenterID.SelectedValue = (String)DataBinder.Eval(DataItem, TransChargesItemMetadata.ColumnNames.SRCenterID);

            lblMsg.Text = GetCoveredItem(cboItemID.SelectedValue);

            // Image
            if (tdImage.Visible)
            {
                var tciImgs = (TransChargesItemImageCollection)Session["collTransChargesItemImg" + Request.UserHostName + PageId];
                var img = tciImgs.FirstOrDefault(rec => rec.SequenceNo.Equals(hdnSequenceNo.Value) && rec.ImageNo.Equals(1));
                if (img != null)
                {
                    imgFromWebCam.ImageUrl = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(img.DocumentImage));
                    hdnImgFromWebCam.Value = imgFromWebCam.ImageUrl;
                }
            }
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(cboItemID.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Invalid Item Name.";
                return;
            }

            var item = new Item();
            if (item.LoadByPrimaryKey(cboItemID.SelectedValue))
            {
                if (AppSession.Parameter.IsValidateBpjsCoveredItemOnTx && !string.IsNullOrEmpty(lblMsg.Text))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = lblMsg.Text;
                    return;
                }

                if (ViewState["IsNewRecord"].Equals(true))
                {
                    var coll = (TransChargesItemCollection)Session["collTransChargesItem" + Request.UserHostName + PageId];

                    bool isExist = coll.Any(charges => (charges.ItemID.Equals(cboItemID.SelectedValue)) &&
                                                       (!(charges.IsVoid ?? false)));

                    if (isExist)
                    {
                        if (AppSession.Parameter.IsAllowDoubleItemServiceOnTxEntry)
                        {
                            if (item.SRItemType == "11" || item.SRItemType == "21" || item.SRItemType == "81")
                            {
                                args.IsValid = false;
                                ((CustomValidator)source).ErrorMessage = string.Format("Item ID: {0} has exist", cboItemID.SelectedValue);
                                return;
                            }
                        }
                        else
                        {
                            args.IsValid = false;
                            ((CustomValidator)source).ErrorMessage = string.Format("Item ID: {0} has exist", cboItemID.SelectedValue);
                            return;
                        }
                    }

                    // berhubung paket bukan hanya bisa di mcu saja tapi disemua tipe registrasi
                    // maka transaksi paket tidak boleh digabung dengan transaksi non-paket
                    if (coll.HasPackage || (coll.Count > 0 && item.SRItemType == "61"/*Package*/))
                    {
                        args.IsValid = false;
                        ((CustomValidator)source).ErrorMessage = "Item package can not be mixed with item non-package, please create new transaction number!";
                        return;
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
                if (item.SRItemType == ItemType.Medical ||
                    item.SRItemType == ItemType.NonMedical ||
                    item.SRItemType == ItemType.Kitchen)
                {
                    var balance = new ItemBalance();
                    if (balance.LoadByPrimaryKey(LocationID, cboItemID.SelectedValue))
                    {
                        if (balance.Balance < 0)
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
                    return;
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
                    if (!isNotValid)
                    {
                        var paramedic = (dataItem["TariffComponentName"].FindControl("cboPhysicianID") as RadComboBox);
                        if (paramedic.Visible)
                        {
                            if (!string.IsNullOrEmpty(paramedic.SelectedValue))
                            {
                                var par = new Paramedic();
                                if (!par.LoadByPrimaryKey(paramedic.SelectedValue))
                                {
                                    isNotValid = true;
                                    msg = "Invalid Physician.";
                                }
                            }
                            else
                            {
                                isNotValid = true;
                                msg = "Physician ID required.";
                            }
                        }
                    }
                    if (isNotValid)
                        break;
                }

                if (isNotValid)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = msg;
                    return;
                }

                // validate cito option
                if (chkIsCito.Checked && cboSRCitoPercentage.Enabled && cboSRCitoPercentage.SelectedValue == string.Empty)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "Cito option required";
                }
            }
            else
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Item not found";
            }

            var reg = new Registration();
            reg.LoadByPrimaryKey(TxtRegistrationNo.Text);
  
            ItemTariff tariff = (Helper.Tariff.GetItemTariff(((RadDatePicker)Helper.FindControlRecursive(Page, "txtTransactionDate")).SelectedDate.Value,
                                                             (string)ViewState["SRTariffType" + Request.UserHostName + PageId], ((RadTextBox)Helper.FindControlRecursive(Page, "txtClassID")).Text, ((RadTextBox)Helper.FindControlRecursive(Page, "txtClassID")).Text,
                                                             cboItemID.SelectedValue, (string)ViewState["GuarantorID" + Request.UserHostName + PageId], false, reg.SRRegistrationType) ??
                                 Helper.Tariff.GetItemTariff(((RadDatePicker)Helper.FindControlRecursive(Page, "txtTransactionDate")).SelectedDate.Value,
                                                             (string)ViewState["SRTariffType" + Request.UserHostName + PageId], AppSession.Parameter.DefaultTariffClass, ((RadTextBox)Helper.FindControlRecursive(Page, "txtClassID")).Text, cboItemID.SelectedValue,
                                                             (string)ViewState["GuarantorID" + Request.UserHostName + PageId], false, reg.SRRegistrationType)) ??
                                (Helper.Tariff.GetItemTariff(((RadDatePicker)Helper.FindControlRecursive(Page, "txtTransactionDate")).SelectedDate.Value,
                                                          AppSession.Parameter.DefaultTariffType, ((RadTextBox)Helper.FindControlRecursive(Page, "txtClassID")).Text, ((RadTextBox)Helper.FindControlRecursive(Page, "txtClassID")).Text,
                                                          cboItemID.SelectedValue, (string)ViewState["GuarantorID" + Request.UserHostName + PageId], false, reg.SRRegistrationType) ??
                              Helper.Tariff.GetItemTariff(((RadDatePicker)Helper.FindControlRecursive(Page, "txtTransactionDate")).SelectedDate.Value,
                                                          AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, ((RadTextBox)Helper.FindControlRecursive(Page, "txtClassID")).Text, cboItemID.SelectedValue,
                                                          (string)ViewState["GuarantorID" + Request.UserHostName + PageId], false, reg.SRRegistrationType));


            if (tariff == null)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Tariff not found";
                return;
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
                                                                     (string)ViewState["SRTariffType" + Request.UserHostName + PageId], ((RadTextBox)Helper.FindControlRecursive(Page, "txtClassID")).Text, ((RadTextBox)Helper.FindControlRecursive(Page, "txtClassID")).Text,
                                                                     cboItemID.SelectedValue, (string)ViewState["GuarantorID" + Request.UserHostName + PageId], false, reg.SRRegistrationType) ??
                                         Helper.Tariff.GetItemTariff(((RadDatePicker)Helper.FindControlRecursive(Page, "txtTransactionDate")).SelectedDate.Value,
                                                                     (string)ViewState["SRTariffType" + Request.UserHostName + PageId], AppSession.Parameter.DefaultTariffClass, ((RadTextBox)Helper.FindControlRecursive(Page, "txtClassID")).Text, cboItemID.SelectedValue,
                                                                     (string)ViewState["GuarantorID" + Request.UserHostName + PageId], false, reg.SRRegistrationType)) ??
                                        (Helper.Tariff.GetItemTariff(((RadDatePicker)Helper.FindControlRecursive(Page, "txtTransactionDate")).SelectedDate.Value,
                                                                  AppSession.Parameter.DefaultTariffType, ((RadTextBox)Helper.FindControlRecursive(Page, "txtClassID")).Text, ((RadTextBox)Helper.FindControlRecursive(Page, "txtClassID")).Text,
                                                                  cboItemID.SelectedValue, (string)ViewState["GuarantorID" + Request.UserHostName + PageId], false, reg.SRRegistrationType) ??
                                      Helper.Tariff.GetItemTariff(((RadDatePicker)Helper.FindControlRecursive(Page, "txtTransactionDate")).SelectedDate.Value,
                                                                  AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, ((RadTextBox)Helper.FindControlRecursive(Page, "txtClassID")).Text, cboItemID.SelectedValue,
                                                                  (string)ViewState["GuarantorID" + Request.UserHostName + PageId], false, reg.SRRegistrationType));

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
            {
                if (AppSession.Parameter.IsTariffPriceVisibleOnlyForAdm)
                {
                    if (string.IsNullOrEmpty(Request.QueryString["verif"]) || Request.QueryString["verif"] == "0")
                    {
                        price.Visible = false;
                    }
                }
                else
                    price.Visible = AppSession.Parameter.TariffComponentPriceVisible;
            }

            var validPrice = (dataItem["TariffComponentName"].FindControl("rfvPrice") as RequiredFieldValidator);
            validPrice.Visible = (dataItem["IsAllowVariable"].Text == "True");

            var discountPercent = (dataItem["TariffComponentName"].FindControl("txtPercentDiscount") as RadNumericTextBox);
            discountPercent.Visible = (dataItem["IsAllowDiscount"].Text == "True") && AppSession.Parameter.IsAllowDiscountOnTransEntry;

            var discount = (dataItem["TariffComponentName"].FindControl("txtDiscount") as RadNumericTextBox);
            discount.Visible = (dataItem["IsAllowDiscount"].Text == "True") &&
                AppSession.Parameter.IsAllowDiscountOnTransEntry &&
                (!AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare);
            discount.MaxValue = (comp == null ? Convert.ToDouble(dataItem["Price"].Text) : (double)comp.Price);
            discount.Value = (comp == null ? 0D : (double)comp.DiscountAmount);

            var validDiscount = (dataItem["TariffComponentName"].FindControl("rfvDiscount") as RequiredFieldValidator);
            validDiscount.Visible = (dataItem["IsAllowDiscount"].Text == "True") && AppSession.Parameter.IsAllowDiscountOnTransEntry;

            var paramedic = (dataItem["TariffComponentName"].FindControl("cboPhysicianID") as RadComboBox);
            paramedic.Visible = (dataItem["IsTariffParamedic"].Text == "True");

            var validParamedic = (dataItem["TariffComponentName"].FindControl("rfvPhysicianID") as RequiredFieldValidator);
            validParamedic.Visible = (dataItem["IsTariffParamedic"].Text == "True");

            if (paramedic.Visible)
            {
                if (AppSession.Parameter.IsDefaultEmptyPhysicianOnTransactionEntry)
                {
                    paramedic.Items.Clear();
                    paramedic.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

                    var table = ((DataTable)ViewState["paramedic" + Request.UserHostName + PageId]);
                    foreach (DataRow row in table.Rows)
                    {
                        paramedic.Items.Add(new RadComboBoxItem((string)row["ParamedicName"], (string)row["ParamedicID"]));
                    }

                    paramedic.SelectedValue = comp != null ? comp.ParamedicID : string.Empty;
                }
                else
                {
                    DataView view = ((DataTable)ViewState["paramedic" + Request.UserHostName + PageId]).DefaultView;
                    if (comp != null)
                        view.RowFilter = string.Format("ParamedicID = '{0}'", comp.ParamedicID);

                    paramedic.DataSource = view;
                    paramedic.DataBind();

                    //var isItemLab = false;
                    //var ilb = new ItemLaboratory();
                    //if (ilb.LoadByPrimaryKey(cboItemID.SelectedValue))
                    //{
                    //    isItemLab = true;
                    //}

                    ////paramedic.SelectedValue = comp != null ? comp.ParamedicID :
                    ////    (isItemLab ? AppSession.Parameter.ParamedicIdLabDefault : Request.QueryString["pid"]);

                    //if (comp != null)
                    //    paramedic.SelectedValue = comp.ParamedicID;
                    //else
                    //{
                    //    if (isItemLab)
                    //    {
                    //        var psdColl = new ParamedicScheduleDateCollection();
                    //        DataTable dtb = psdColl.GetParamedicID(AppSession.Parameter.ServiceUnitLaboratoryID, hdnSrRegistrationType.Value);

                    //        if (dtb.Rows.Count == 1)
                    //            paramedic.SelectedValue = dtb.Rows[0]["ParamedicID"].ToString();
                    //        else paramedic.SelectedValue = Request.QueryString["pid"];
                    //    }
                    //    else
                    //        paramedic.SelectedValue = Request.QueryString["pid"];
                    //}

                    if (comp != null)
                        paramedic.SelectedValue = comp.ParamedicID;
                    else
                    {
                        if (Request.QueryString["type"] == "ds")
                        {
                            var psdColl = new ParamedicScheduleDateCollection();
                            DataTable dtb = psdColl.GetParamedicID(ToServiceUnitID, hdnSrRegistrationType.Value);

                            if (dtb.Rows.Count == 1)
                                paramedic.SelectedValue = dtb.Rows[0]["ParamedicID"].ToString();
                            else paramedic.SelectedValue = Request.QueryString["pid"];
                        }
                        else
                            paramedic.SelectedValue = Request.QueryString["pid"];
                    }
                }
            }

            var percentFeeDiscount = (dataItem["TariffComponentName"].FindControl("txtPercentFeeDiscount") as RadNumericTextBox);
            percentFeeDiscount.Visible = (dataItem["IsAllowDiscount"].Text == "True") && (AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare);
            percentFeeDiscount.MaxValue = 100;
            percentFeeDiscount.Value = (comp == null ? 0D : (double?)comp.FeeDiscountPercentage);
        }

        private void PopulateParamedicByServiceUnit()
        {
            if (ViewState["paramedic" + Request.UserHostName + PageId] != null)
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
                rooms.Query.IsShowOnBookingOT == true,
                rooms.Query.IsActive == true
                );
            rooms.LoadAll();

            var r = (rooms.Where(o => o.ServiceUnitID == Request.QueryString["cid"]).Select(o => o.ServiceUnitID)).Distinct().SingleOrDefault();
            if (r != null && Request.QueryString["type"] == "tr")
            {
                medic.Where(medic.IsActive == true);
                ViewState["paramedic" + Request.UserHostName + PageId] = medic.LoadDataTable();

                return;
            }

            if (reg.SRRegistrationType != AppConstant.RegistrationType.InPatient && reg.SRRegistrationType != AppConstant.RegistrationType.EmergencyPatient)
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
                    else if (!string.IsNullOrEmpty(TxtPackageReferenceNo.Text))
                    {

                        if (ViewState["IsNewRecord"].Equals(true))
                        {
                            query.Where
                            (
                                query.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboToServiceUnitID")).SelectedValue,
                                medic.IsActive == true
                            );
                        }
                        else
                        {
                            // edit detail paket, to service unit lngsng ambil dari row data saja
                            query.Where
                            (
                                query.ServiceUnitID == ViewState["ToServiceUnitID" + Request.UserHostName + PageId].ToString(),
                                medic.IsActive == true
                            );
                        }
                    }
                    else
                    {
                        query.Where
                            (
                                query.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboFromServiceUnitID")).SelectedValue,
                                medic.IsActive == true
                            );
                    }

                }
                else
                {
                    query.Where
                        (
                            query.ServiceUnitID == ToServiceUnitID,
                            medic.IsActive == true
                        );
                }
                query.OrderBy(medic.ParamedicName.Ascending);
                ViewState["paramedic" + Request.UserHostName + PageId] = query.LoadDataTable();
            }
            else
            {
                if (Request.QueryString["type"] == "tr")
                {
                    medic.Where(medic.IsActive == true);
                    medic.OrderBy(medic.ParamedicName.Ascending);
                    ViewState["paramedic" + Request.UserHostName + PageId] = medic.LoadDataTable();
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
                            query.ServiceUnitID == ToServiceUnitID,
                            medic.IsActive == true
                        );
                    query.OrderBy(medic.ParamedicName.Ascending);
                    ViewState["paramedic" + Request.UserHostName + PageId] = query.LoadDataTable();
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
            var coll = (TransChargesItemCompCollection)Session["collTransChargesItemComp" + Request.UserHostName + PageId];
            return coll == null ? null : coll.FirstOrDefault(rec => rec.SequenceNo.Equals(hdnSequenceNo.Value) &&
                                                                    rec.TariffComponentID.Equals(tariffComponentID));
        }

        protected void txtChargeQuantity_TextChanged(object sender, EventArgs e)
        {
            if (!txtStockQuantity.ReadOnly)
                txtStockQuantity.Value = txtChargeQuantity.Value;

            var item = new Item();
            if (item.LoadByPrimaryKey(cboItemID.SelectedValue))
            {
                switch (item.SRItemType)
                {
                    case ItemType.Medical:
                        txtStockQuantity.Value = txtChargeQuantity.Value;
                        var ipm = new ItemProductMedic();
                        if (ipm.LoadByPrimaryKey(item.ItemID) && !(ipm.IsActualDeduct ?? false))
                        {
                            var x = Convert.ToDecimal(txtChargeQuantity.Value) - Math.Floor(Convert.ToDecimal(txtChargeQuantity.Value));
                            var deduct = new ItemProductDeductionDetail();
                            deduct.Query.Where(string.Format("<{0} BETWEEN MinAmount AND MaxAmount>", x));
                            if (deduct.Query.Load())
                                txtStockQuantity.Value = Convert.ToDouble(decimal.Truncate(Convert.ToDecimal(txtStockQuantity.Value)) + deduct.DeductionAmount);
                        }
                        break;
                    case ItemType.NonMedical:
                    case ItemType.Kitchen:
                        txtStockQuantity.Value = txtChargeQuantity.Value;
                        break;
                }
            }
        }

        protected void txtPercentDiscount1_TextChanged(object sender, EventArgs e)
        {
            txtDiscountAmount1.Value = (txtPercentDiscount1.Value / 100) * txtPrice1.Value;
        }

        protected void cboItemGroupID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemGroupName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemGroupID"].ToString();
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        private DataTable PopulateServiceItem(string searchText, bool VisibleOnly)
        {
            DataTable tbl = null;

            try
            {
                #region new - melihat batasan item di guarantor 
                var guar = new Guarantor();
                guar.LoadByPrimaryKey(CboGuarantorId.SelectedValue);

                DataTable tempTbl = null;
                string searchTextContain = string.Format("%{0}%", searchText);
                //item service
                var query = new ItemQuery("a");
                var suis = new ServiceUnitItemServiceQuery("c");
                var balance = new ItemBalanceQuery("d");

                var ig = new ItemGroupQuery("ig");
                query.LeftJoin(ig).On(query.ItemGroupID == ig.ItemGroupID);

                // Jika dipanggil dari layar EMR dan user adalah dokter dg smf terisi maka filter dg matrix SMF ItemService
                if (Request.QueryString["emr"] == "1" && !string.IsNullOrWhiteSpace(AppSession.UserLogin.SmfID))
                {
                    // Cek jika ada matrix baru dipakai filter
                    var mtx = new SmfItemService();
                    mtx.Query.es.Top = 1;
                    mtx.Query.Where(mtx.Query.SmfID == AppSession.UserLogin.SmfID);
                    if (mtx.Query.Load())
                    {
                        var smfis = new SmfItemServiceQuery("smfis");
                        query.InnerJoin(smfis).On(query.ItemID == smfis.ItemID & smfis.SmfID == AppSession.UserLogin.SmfID);

                        if (VisibleOnly) query.Where(smfis.IsVisible == VisibleOnly);
                    }
                }
                query.es.Top = 30;
                query.Select
                    (
                        query.ItemID,
                        (query.ItemName + " [" + query.ItemID + "]").As("ItemName"),
                        (balance.Balance.Coalesce("0")).As("Balance"),
                        query.SRItemType,
                        suis.ServiceUnitID.Coalesce("''"),
                        query.Notes,
                        ig.ItemGroupName
                    );

                if (Request.QueryString["type"] == "tr" || Request.QueryString["type"] == "mcu")
                {
                    if (Helper.FindControlRecursive(Page, "pnlResponUnit").Visible)
                        query.InnerJoin(suis).On(
                                query.ItemID == suis.ItemID &&
                                suis.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboResponUnit")).SelectedValue
                            );
                    else
                        query.InnerJoin(suis).On(
                                query.ItemID == suis.ItemID &&
                                suis.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboFromServiceUnitID")).SelectedValue
                            );
                }
                else
                {
                    query.InnerJoin(suis).On(
                            query.ItemID == suis.ItemID &&
                            suis.ServiceUnitID == ToServiceUnitID
                        );
                }
                query.LeftJoin(balance).On(
                        query.ItemID == balance.ItemID &&
                        balance.LocationID == LocationID
                    );

                if (VisibleOnly) query.Where(suis.IsVisible == VisibleOnly);

                query.Where(query.SRItemType == ItemType.Service,
                        query.Or(
                                query.ItemName.Like(searchTextContain),
                                query.ItemID.Like(searchTextContain)
                            ),
                        query.IsActive == true,
                        query.Or(
                                query.GuarantorID.IsNull(),
                                query.GuarantorID == string.Empty,
                                query.GuarantorID == CboGuarantorId.SelectedValue)
                    );
                if (!string.IsNullOrEmpty(cboItemGroupID.SelectedValue))
                    query.Where(query.ItemGroupID == cboItemGroupID.SelectedValue);

                if (Request.QueryString["verif"] == "1")
                    query.Where(suis.IsAllowEditByUserVerificated == true);

                var restrictions = new GuarantorItemRestrictionsQuery("rest");
                var itmrest = new ItemQuery("itmrest");
                restrictions.Select(restrictions.ItemID);
                restrictions.InnerJoin(itmrest).On(restrictions.ItemID == itmrest.ItemID);
                restrictions.Where(restrictions.GuarantorID == CboGuarantorId.SelectedValue,
                                   itmrest.SRItemType == ItemType.Service);
                DataTable dtRest = restrictions.LoadDataTable();
                if (dtRest.Rows.Count > 0)
                {
                    //query.InnerJoin(restrictions).On(query.ItemID == restrictions.ItemID && restrictions.GuarantorID == CboGuarantorId.SelectedValue);
                    if (guar.IsItemServiceRestrictionStatusAllowed ?? true)
                        query.Where(query.ItemID.In(restrictions));
                    else
                        query.Where(query.ItemID.NotIn(restrictions));
                }
                    
                //db:20231009 -> cek restriction dari mappingan ItemServiceProcedure
                if (HdnIsMandatoryBookingNo.Value.ToString() == "y" && !string.IsNullOrEmpty(CboServiceUnitBookingNo.SelectedValue))
                {
                    var booking = new ServiceUnitBooking();
                    if (booking.LoadByPrimaryKey(CboServiceUnitBookingNo.SelectedValue) && (!string.IsNullOrEmpty(booking.SRProcedure1) || !string.IsNullOrEmpty(booking.SRProcedure2)))
                    {
                        var procItem = new ItemServiceProcedureQuery();
                        procItem.Where(procItem.Or(procItem.SRProcedure == booking.SRProcedure1, procItem.SRProcedure == booking.SRProcedure2));
                        procItem.Select(procItem.ItemID);
                        if (procItem.LoadDataTable().Rows.Count > 0)
                            query.Where(query.ItemID.In(procItem));
                    }
                }

                // -- item casemix
                //if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP" && AppSession.Parameter.HealthcareInitial == "RSI")
                //{
                //    if (!string.IsNullOrWhiteSpace(Request.QueryString["casemix"]) && Request.QueryString["casemix"] == "1")
                //    {
                //        var casemix = new CasemixCoveredDetailCollection();
                //        if (casemix.LoadAll()) query.Where(query.ItemID.In(casemix.Select(c => c.ItemID)));
                //    }
                //}

                // TODO: Saya samakan dgn kondisi di ItemPickerList, sesuaikan jika tidak cocok (Handono 22-08-07)
                // diremark semua, udah gak perlu dipisah lagi krn di LIS sudah akomodir (deby - 6 sept 2022)
                //if (AppParameter.IsYes(AppParameter.ParameterItem.IsCasemixCoveredEnable))
                //            if (Helper.GuarantorBpjsCasemix.Contains(CboGuarantorId.SelectedValue) && AppSession.Parameter.CasemixValidationRegistrationType.Contains(hdnSrRegistrationType.Value))
                //            {
                //                if (Request.QueryString["type"] == "jo" || Request.QueryString["type"] == "ds")
                //                {
                //                    //var cmixCover = new CasemixCoveredDetailQuery("cmix");
                //                    //cmixCover.Select(cmixCover.ItemID);
                //                    //cmixCover.Where(cmixCover.ItemID == query.ItemID);


                //                    //var unit = $find("<%= cboResponUnit.ClientID %>");
                //                    //if (unit == null)
                //                    //    unit = $find("<%= cboToServiceUnitID.ClientID %>");
                //                    //if (unit == null)
                //                    //    unit = $find("<%= cboFromServiceUnitID.ClientID %>");
                //                    var serviceUnitID = string.Empty;

                //                    if (Helper.FindControlRecursive(Page, "pnlResponUnit").Visible)
                //                        serviceUnitID = ((RadComboBox)Helper.FindControlRecursive(Page, "cboResponUnit")).SelectedValue;
                //                    else if (Helper.FindControlRecursive(Page, "pnlJobOrder").Visible)
                //                        serviceUnitID = ((RadComboBox)Helper.FindControlRecursive(Page, "cboToServiceUnitID")).SelectedValue;
                //                    else
                //                        serviceUnitID = ((RadComboBox)Helper.FindControlRecursive(Page, "cboFromServiceUnitID")).SelectedValue;

                ////                    if (serviceUnitID == AppSession.Parameter.ServiceUnitRadiologyID
                ////|| serviceUnitID == AppSession.Parameter.ServiceUnitRadiologyID2
                ////|| AppSession.Parameter.ServiceUnitRadiologyIdArray.Contains(serviceUnitID)
                ////|| AppSession.Parameter.ServiceUnitRadiologyIDs.Contains(serviceUnitID)
                ////|| serviceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID
                ////|| AppSession.Parameter.ServiceUnitLaboratoryIdArray.Contains(serviceUnitID))
                ////                    {
                ////                        if (!string.IsNullOrWhiteSpace(Request.QueryString["casemix"]) && Request.QueryString["casemix"] == "1")
                ////                            // Select ItemID In CasemixCoveredDetail
                ////                            query.Where(query.ItemID.In(cmixCover));
                ////                        else
                ////                            // Select ItemID Not In CasemixCoveredDetail
                ////                            query.Where(query.ItemID.NotIn(cmixCover));
                ////                    }
                //                }
                //            }
                // -- END item casemix


                tempTbl = query.LoadDataTable();

                //item lab
                if (tempTbl.Rows.Count < 30)
                {
                    query = new ItemQuery("a");
                    suis = new ServiceUnitItemServiceQuery("c");
                    balance = new ItemBalanceQuery("d");
                    ig = new ItemGroupQuery("ig");
                    query.LeftJoin(ig).On(query.ItemGroupID == ig.ItemGroupID);

                    query.es.Top = 30;
                    query.Select
                        (
                            query.ItemID,
                            (query.ItemName + " [" + query.ItemID + "]").As("ItemName"),
                            (balance.Balance.Coalesce("0")).As("Balance"),
                            query.SRItemType,
                            suis.ServiceUnitID.Coalesce("''"),
                            query.Notes,
                            ig.ItemGroupName
                        );

                    if (Request.QueryString["type"] == "tr" || Request.QueryString["type"] == "mcu")
                    {
                        if (Helper.FindControlRecursive(Page, "pnlResponUnit").Visible)
                            query.InnerJoin(suis).On(
                                    query.ItemID == suis.ItemID &&
                                    suis.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboResponUnit")).SelectedValue
                                );
                        else
                            query.InnerJoin(suis).On(
                                    query.ItemID == suis.ItemID &&
                                    suis.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboFromServiceUnitID")).SelectedValue
                                );
                    }
                    else
                    {
                        query.InnerJoin(suis).On(
                                query.ItemID == suis.ItemID &&
                                suis.ServiceUnitID == ToServiceUnitID
                            );
                    }
                    query.LeftJoin(balance).On(
                        query.ItemID == balance.ItemID &&
                        balance.LocationID == LocationID
                    );
                    if (VisibleOnly) query.Where(suis.IsVisible == VisibleOnly);

                    query.Where(query.SRItemType == ItemType.Laboratory,
                            query.Or(
                                    query.ItemName.Like(searchTextContain),
                                    query.ItemID.Like(searchTextContain)
                                ),
                            query.IsActive == true,
                            query.Or(
                                    query.GuarantorID.IsNull(),
                                    query.GuarantorID == string.Empty,
                                    query.GuarantorID == CboGuarantorId.SelectedValue)
                        );

                    if (!string.IsNullOrEmpty(cboItemGroupID.SelectedValue))
                        query.Where(query.ItemGroupID == cboItemGroupID.SelectedValue);

                    if (Request.QueryString["verif"] == "1")
                        query.Where(suis.IsAllowEditByUserVerificated == true);

                    restrictions = new GuarantorItemRestrictionsQuery("rest");
                    itmrest = new ItemQuery("itmrest");
                    restrictions.Select(restrictions.ItemID);
                    restrictions.InnerJoin(itmrest).On(restrictions.ItemID == itmrest.ItemID);
                    restrictions.Where(restrictions.GuarantorID == CboGuarantorId.SelectedValue,
                                       itmrest.SRItemType == ItemType.Laboratory);
                    DataTable dtRest2 = restrictions.LoadDataTable();
                    if (dtRest2.Rows.Count > 0)
                    {
                        //query.InnerJoin(restrictions).On(query.ItemID == restrictions.ItemID && restrictions.GuarantorID == CboGuarantorId.SelectedValue);
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
                    suis = new ServiceUnitItemServiceQuery("c");
                    balance = new ItemBalanceQuery("d");
                    ig = new ItemGroupQuery("ig");
                    query.LeftJoin(ig).On(query.ItemGroupID == ig.ItemGroupID);
                    query.es.Top = 30;
                    query.Select
                        (
                            query.ItemID,
                            (query.ItemName + " [" + query.ItemID + "]").As("ItemName"),
                           (balance.Balance.Coalesce("0")).As("Balance"),
                            query.SRItemType,
                            suis.ServiceUnitID.Coalesce("''"),
                            query.Notes,
                            ig.ItemGroupName
                        );

                    if (Request.QueryString["type"] == "tr" || Request.QueryString["type"] == "mcu")
                    {
                        if (Helper.FindControlRecursive(Page, "pnlResponUnit").Visible)
                            query.InnerJoin(suis).On(
                                    query.ItemID == suis.ItemID &&
                                    suis.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboResponUnit")).SelectedValue
                                );
                        else
                            query.InnerJoin(suis).On(
                                    query.ItemID == suis.ItemID &&
                                    suis.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboFromServiceUnitID")).SelectedValue
                                );
                    }
                    else
                    {
                        query.InnerJoin(suis).On(
                                query.ItemID == suis.ItemID &&
                                suis.ServiceUnitID == ToServiceUnitID
                            );
                    }
                    query.LeftJoin(balance).On(
                        query.ItemID == balance.ItemID &&
                        balance.LocationID == LocationID);
                    if (VisibleOnly) query.Where(suis.IsVisible == VisibleOnly);

                    query.Where(query.SRItemType == ItemType.Radiology,
                            query.Or(
                                    query.ItemName.Like(searchTextContain),
                                    query.ItemID.Like(searchTextContain)
                                ),
                            query.IsActive == true,
                            query.Or(
                                    query.GuarantorID.IsNull(),
                                    query.GuarantorID == string.Empty,
                                    query.GuarantorID == CboGuarantorId.SelectedValue)
                        );

                    if (!string.IsNullOrEmpty(cboItemGroupID.SelectedValue))
                        query.Where(query.ItemGroupID == cboItemGroupID.SelectedValue);

                    if (Request.QueryString["verif"] == "1")
                        query.Where(suis.IsAllowEditByUserVerificated == true);

                    restrictions = new GuarantorItemRestrictionsQuery("rest");
                    itmrest = new ItemQuery("itmrest");
                    restrictions.Select(restrictions.ItemID);
                    restrictions.InnerJoin(itmrest).On(restrictions.ItemID == itmrest.ItemID);
                    restrictions.Where(restrictions.GuarantorID == CboGuarantorId.SelectedValue,
                                       itmrest.SRItemType == ItemType.Radiology);
                    DataTable dtRest2 = restrictions.LoadDataTable();
                    if (dtRest2.Rows.Count > 0)
                    {
                        //query.InnerJoin(restrictions).On(query.ItemID == restrictions.ItemID && restrictions.GuarantorID == CboGuarantorId.SelectedValue);
                        if (guar.IsItemRadRestrictionStatusAllowed ?? true)
                            query.Where(query.ItemID.In(restrictions));
                        else
                            query.Where(query.ItemID.NotIn(restrictions));
                    }
                        
                    tempTbl.Merge(query.LoadDataTable());
                }

                //item package tanpa periode  berlaku
                if (tempTbl.Rows.Count < 30)
                {
                    query = new ItemQuery("a");
                    suis = new ServiceUnitItemServiceQuery("c");
                    balance = new ItemBalanceQuery("d");
                    ig = new ItemGroupQuery("ig");
                    query.LeftJoin(ig).On(query.ItemGroupID == ig.ItemGroupID);

                    query.es.Top = 30;
                    query.Select
                        (
                            query.ItemID,
                            (query.ItemName + " [" + query.ItemID + "]").As("ItemName"),
                            (balance.Balance.Coalesce("0")).As("Balance"),
                            query.SRItemType,
                            suis.ServiceUnitID.Coalesce("''"),
                            query.Notes,
                            ig.ItemGroupName
                        );

                    if (Request.QueryString["type"] == "tr" || Request.QueryString["type"] == "mcu")
                    {
                        if (Helper.FindControlRecursive(Page, "pnlResponUnit").Visible)
                            query.InnerJoin(suis).On(
                                    query.ItemID == suis.ItemID &&
                                    suis.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboResponUnit")).SelectedValue
                                );
                        else
                            query.InnerJoin(suis).On(
                                    query.ItemID == suis.ItemID &&
                                    suis.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboFromServiceUnitID")).SelectedValue
                                );
                    }
                    else
                    {
                        query.InnerJoin(suis).On(
                                query.ItemID == suis.ItemID &&
                                suis.ServiceUnitID == ToServiceUnitID
                            );
                    }
                    query.LeftJoin(balance).On(
                        query.ItemID == balance.ItemID &&
                        balance.LocationID == LocationID);
                    if (VisibleOnly) query.Where(suis.IsVisible == VisibleOnly);

                    query.Where(query.SRItemType == ItemType.Package,
                            query.Or(
                                    query.ItemName.Like(searchTextContain),
                                    query.ItemID.Like(searchTextContain)
                                ),
                            query.IsActive == true,
                            query.Or(
                                    query.GuarantorID.IsNull(),
                                    query.GuarantorID == string.Empty,
                                    query.GuarantorID == CboGuarantorId.SelectedValue),
                            //query.ValidityPeriodFrom.IsNull(),
                            //query.ValidityPeriodTo.IsNull()
                            query.Or(query.ValidityPeriodFrom.IsNull(), query.ValidityPeriodFrom <= TxtTransactionDate.SelectedDate),
                            query.Or(query.ValidityPeriodTo.IsNull(), query.ValidityPeriodTo >= TxtTransactionDate.SelectedDate)
                        );

                    if (!string.IsNullOrEmpty(cboItemGroupID.SelectedValue))
                        query.Where(query.ItemGroupID == cboItemGroupID.SelectedValue);

                    if (Request.QueryString["verif"] == "1")
                        query.Where(suis.IsAllowEditByUserVerificated == true);

                    restrictions = new GuarantorItemRestrictionsQuery("rest");
                    itmrest = new ItemQuery("itmrest");
                    restrictions.Select(restrictions.ItemID);
                    restrictions.InnerJoin(itmrest).On(restrictions.ItemID == itmrest.ItemID);
                    restrictions.Where(restrictions.GuarantorID == CboGuarantorId.SelectedValue,
                                       itmrest.SRItemType == ItemType.Package);
                    DataTable dtRest2 = restrictions.LoadDataTable();
                    if (dtRest2.Rows.Count > 0)
                    {
                        //query.InnerJoin(restrictions).On(query.ItemID == restrictions.ItemID && restrictions.GuarantorID == CboGuarantorId.SelectedValue);
                        if (guar.IsItemServiceRestrictionStatusAllowed ?? true)
                            query.Where(query.ItemID.In(restrictions));
                        else
                            query.Where(query.ItemID.NotIn(restrictions));
                    }
                        
                    tempTbl.Merge(query.LoadDataTable());
                }

                //item package dg periode  berlaku
                //remark by db:20231118 - digabung di atas
                //if (tempTbl.Rows.Count < 30)
                //{
                //    query = new ItemQuery("a");
                //    suis = new ServiceUnitItemServiceQuery("c");
                //    balance = new ItemBalanceQuery("d");
                //    ig = new ItemGroupQuery("ig");
                //    query.LeftJoin(ig).On(query.ItemGroupID == ig.ItemGroupID);

                //    query.es.Top = 30;
                //    query.Select
                //        (
                //            query.ItemID,
                //            (query.ItemName + " [" + query.ItemID + "]").As("ItemName"),
                //            (balance.Balance.Coalesce("0")).As("Balance"),
                //            query.SRItemType,
                //            suis.ServiceUnitID.Coalesce("''"),
                //            query.Notes,
                //            ig.ItemGroupName
                //        );

                //    if (Request.QueryString["type"] == "tr" || Request.QueryString["type"] == "mcu")
                //    {
                //        if (Helper.FindControlRecursive(Page, "pnlResponUnit").Visible)
                //            query.InnerJoin(suis).On(
                //                    query.ItemID == suis.ItemID &&
                //                    suis.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboResponUnit")).SelectedValue
                //                );
                //        else
                //            query.InnerJoin(suis).On(
                //                    query.ItemID == suis.ItemID &&
                //                    suis.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboFromServiceUnitID")).SelectedValue
                //                );
                //    }
                //    else
                //    {
                //        query.InnerJoin(suis).On(
                //                query.ItemID == suis.ItemID &&
                //                suis.ServiceUnitID == ToServiceUnitID
                //            );
                //    }
                //    query.LeftJoin(balance).On(
                //        query.ItemID == balance.ItemID &&
                //        balance.LocationID == LocationID);
                //    if (VisibleOnly) query.Where(suis.IsVisible == VisibleOnly);

                //    query.Where(query.SRItemType == ItemType.Package,
                //            query.Or(
                //                    query.ItemName.Like(searchTextContain),
                //                    query.ItemID.Like(searchTextContain)
                //                ),
                //            query.IsActive == true,
                //            query.Or(
                //                    query.GuarantorID.IsNull(),
                //                    query.GuarantorID == string.Empty,
                //                    query.GuarantorID == CboGuarantorId.SelectedValue),
                //            query.ValidityPeriodFrom <= TxtTransactionDate.SelectedDate,
                //            query.ValidityPeriodTo >= TxtTransactionDate.SelectedDate
                //        );

                //    if (!string.IsNullOrEmpty(cboItemGroupID.SelectedValue))
                //        query.Where(query.ItemGroupID == cboItemGroupID.SelectedValue);

                //    if (Request.QueryString["verif"] == "1")
                //        query.Where(suis.IsAllowEditByUserVerificated == true);

                //    restrictions = new GuarantorItemRestrictionsQuery("rest");
                //    itmrest = new ItemQuery("itmrest");
                //    restrictions.Select(restrictions.ItemID);
                //    restrictions.InnerJoin(itmrest).On(restrictions.ItemID == itmrest.ItemID);
                //    restrictions.Where(restrictions.GuarantorID == CboGuarantorId.SelectedValue,
                //                       itmrest.SRItemType == ItemType.Package);
                //    DataTable dtRest2 = restrictions.LoadDataTable();
                //    if (dtRest2.Rows.Count > 0)
                //    {
                //        //query.InnerJoin(restrictions).On(query.ItemID == restrictions.ItemID && restrictions.GuarantorID == CboGuarantorId.SelectedValue);
                //        if (guar.IsItemServiceRestrictionStatusAllowed ?? true)
                //            query.Where(query.ItemID.In(restrictions));
                //        else
                //            query.Where(query.ItemID.NotIn(restrictions));
                //    }

                //    tempTbl.Merge(query.LoadDataTable());
                //}

                // Sort
                var dataSorted = tempTbl.Select("", "ItemName ASC").CopyToDataTable();

                dataSorted.AcceptChanges();
                tbl = dataSorted;
                #endregion
            }
            catch (Exception ex)
            {

            }

            return tbl;
        }

        private DataTable PopulateServiceItemGroup(string searchText, bool VisibleOnly)
        {
            DataTable tbl = null;
            string searchTextContain = string.Format("%{0}%", searchText);
            try
            {
                #region new - melihat batasan item di guarantor 
                var guar = new Guarantor();
                guar.LoadByPrimaryKey(CboGuarantorId.SelectedValue);

                DataTable tempTbl = null;
                //item service
                var query = new ItemQuery("a");
                var itemgroup = new ItemGroupQuery("b");
                var itemUnit = new ServiceUnitItemServiceQuery("c");
                var balance = new ItemBalanceQuery("d");

                query.es.Top = 10;
                query.es.Distinct = true;
                query.Select
                    (
                        query.ItemGroupID,
                        itemgroup.ItemGroupName
                    );
                query.InnerJoin(itemgroup).On(itemgroup.ItemGroupID == query.ItemGroupID);

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
                                itemUnit.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboFromServiceUnitID")).SelectedValue
                            );
                }
                else
                {
                    query.InnerJoin(itemUnit).On(
                            query.ItemID == itemUnit.ItemID &&
                            itemUnit.ServiceUnitID == ToServiceUnitID
                        );
                }
                query.LeftJoin(balance).On(
                        query.ItemID == balance.ItemID &&
                        balance.LocationID == LocationID
                    );
                if (VisibleOnly) query.Where(itemUnit.IsVisible == VisibleOnly);


                query.Where(query.SRItemType == ItemType.Service,
                        query.Or(
                                itemgroup.ItemGroupName.Like(searchTextContain),
                                query.ItemGroupID.Like(searchTextContain)
                            ),
                        query.IsActive == true,
                        query.Or(
                                query.GuarantorID.IsNull(),
                                query.GuarantorID == string.Empty,
                                query.GuarantorID == CboGuarantorId.SelectedValue)
                    );

                if (Request.QueryString["verif"] == "1")
                    query.Where(itemUnit.IsAllowEditByUserVerificated == true);

                var restrictions = new GuarantorItemRestrictionsQuery("rest");
                var itmrest = new ItemQuery("itmrest");
                restrictions.Select(restrictions.ItemID);
                restrictions.InnerJoin(itmrest).On(restrictions.ItemID == itmrest.ItemID);
                restrictions.Where(restrictions.GuarantorID == CboGuarantorId.SelectedValue,
                                   itmrest.SRItemType == ItemType.Service);
                DataTable dtRest = restrictions.LoadDataTable();
                if (dtRest.Rows.Count > 0)
                {
                    //query.InnerJoin(restrictions).On(query.ItemID == restrictions.ItemID && restrictions.GuarantorID == CboGuarantorId.SelectedValue);
                    if (guar.IsItemServiceRestrictionStatusAllowed ?? true)
                        query.Where(query.ItemID.In(restrictions));
                    else
                        query.Where(query.ItemID.NotIn(restrictions));
                }

                //db:20231009 -> cek restriction dari mappingan ItemServiceProcedure
                if (HdnIsMandatoryBookingNo.Value.ToString() == "y" && !string.IsNullOrEmpty(CboServiceUnitBookingNo.SelectedValue))
                {
                    var booking = new ServiceUnitBooking();
                    if (booking.LoadByPrimaryKey(CboServiceUnitBookingNo.SelectedValue) && (!string.IsNullOrEmpty(booking.SRProcedure1) || !string.IsNullOrEmpty(booking.SRProcedure2)))
                    {
                        var procItem = new ItemServiceProcedureQuery();
                        procItem.Where(procItem.Or(procItem.SRProcedure == booking.SRProcedure1, procItem.SRProcedure == booking.SRProcedure2));
                        procItem.Select(procItem.ItemID);
                        if (procItem.LoadDataTable().Rows.Count > 0)
                            query.Where(query.ItemID.In(procItem));
                    }
                }

                tempTbl = query.LoadDataTable();

                //item lab
                if (tempTbl.Rows.Count < 10)
                {
                    query = new ItemQuery("a");
                    itemgroup = new ItemGroupQuery("b");
                    itemUnit = new ServiceUnitItemServiceQuery("c");
                    balance = new ItemBalanceQuery("d");

                    query.es.Distinct = true;
                    query.Select
                        (
                            query.ItemGroupID,
                            itemgroup.ItemGroupName
                        );
                    query.InnerJoin(itemgroup).On(itemgroup.ItemGroupID == query.ItemGroupID);

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
                                    itemUnit.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboFromServiceUnitID")).SelectedValue
                                );
                    }
                    else
                    {
                        query.InnerJoin(itemUnit).On(
                                query.ItemID == itemUnit.ItemID &&
                                itemUnit.ServiceUnitID == ToServiceUnitID
                            );
                    }
                    query.LeftJoin(balance).On(
                        query.ItemID == balance.ItemID &&
                        balance.LocationID == LocationID
                    );
                    if (VisibleOnly) query.Where(itemUnit.IsVisible == VisibleOnly);

                    query.Where(query.SRItemType == ItemType.Laboratory,
                            query.Or(
                                    itemgroup.ItemGroupName.Like(searchTextContain),
                                    query.ItemGroupID.Like(searchTextContain)
                                ),
                            query.IsActive == true,
                            query.Or(
                                    query.GuarantorID.IsNull(),
                                    query.GuarantorID == string.Empty,
                                    query.GuarantorID == CboGuarantorId.SelectedValue)
                        );

                    if (Request.QueryString["verif"] == "1")
                        query.Where(itemUnit.IsAllowEditByUserVerificated == true);

                    restrictions = new GuarantorItemRestrictionsQuery("rest");
                    itmrest = new ItemQuery("itmrest");
                    restrictions.Select(restrictions.ItemID);
                    restrictions.InnerJoin(itmrest).On(restrictions.ItemID == itmrest.ItemID);
                    restrictions.Where(restrictions.GuarantorID == CboGuarantorId.SelectedValue,
                                       itmrest.SRItemType == ItemType.Laboratory);
                    DataTable dtRest2 = restrictions.LoadDataTable();
                    if (dtRest2.Rows.Count > 0)
                    {
                        //query.InnerJoin(restrictions).On(query.ItemID == restrictions.ItemID && restrictions.GuarantorID == CboGuarantorId.SelectedValue);
                        if (guar.IsItemLabRestrictionStatusAllowed ?? true)
                            query.Where(query.ItemID.In(restrictions));
                        else
                            query.Where(query.ItemID.NotIn(restrictions));
                    }

                    tempTbl.Merge(query.LoadDataTable());
                }

                //item rad
                if (tempTbl.Rows.Count < 10)
                {
                    query = new ItemQuery("a");
                    itemgroup = new ItemGroupQuery("b");
                    itemUnit = new ServiceUnitItemServiceQuery("c");
                    balance = new ItemBalanceQuery("d");

                    query.es.Top = 10;
                    query.es.Distinct = true;
                    query.Select
                        (
                            query.ItemGroupID,
                            itemgroup.ItemGroupName
                        );
                    query.InnerJoin(itemgroup).On(itemgroup.ItemGroupID == query.ItemGroupID);

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
                                    itemUnit.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboFromServiceUnitID")).SelectedValue
                                );
                    }
                    else
                    {
                        query.InnerJoin(itemUnit).On(
                                query.ItemID == itemUnit.ItemID &&
                                itemUnit.ServiceUnitID == ToServiceUnitID
                            );
                    }
                    query.LeftJoin(balance).On(
                        query.ItemID == balance.ItemID &&
                        balance.LocationID == LocationID);
                    if (VisibleOnly) query.Where(itemUnit.IsVisible == VisibleOnly);

                    query.Where(query.SRItemType == ItemType.Radiology,
                            query.Or(
                                    itemgroup.ItemGroupName.Like(searchTextContain),
                                    query.ItemGroupID.Like(searchTextContain)
                                ),
                            query.IsActive == true,
                            query.Or(
                                    query.GuarantorID.IsNull(),
                                    query.GuarantorID == string.Empty,
                                    query.GuarantorID == CboGuarantorId.SelectedValue)
                        );

                    if (Request.QueryString["verif"] == "1")
                        query.Where(itemUnit.IsAllowEditByUserVerificated == true);

                    restrictions = new GuarantorItemRestrictionsQuery("rest");
                    itmrest = new ItemQuery("itmrest");
                    restrictions.Select(restrictions.ItemID);
                    restrictions.InnerJoin(itmrest).On(restrictions.ItemID == itmrest.ItemID);
                    restrictions.Where(restrictions.GuarantorID == CboGuarantorId.SelectedValue,
                                       itmrest.SRItemType == ItemType.Radiology);
                    DataTable dtRest2 = restrictions.LoadDataTable();
                    if (dtRest2.Rows.Count > 0)
                    {
                        //query.InnerJoin(restrictions).On(query.ItemID == restrictions.ItemID && restrictions.GuarantorID == CboGuarantorId.SelectedValue);
                        if (guar.IsItemRadRestrictionStatusAllowed ?? true)
                            query.Where(query.ItemID.In(restrictions));
                        else
                            query.Where(query.ItemID.NotIn(restrictions));
                    }

                    tempTbl.Merge(query.LoadDataTable());
                }

                //item package
                if (tempTbl.Rows.Count < 10)
                {
                    query = new ItemQuery("a");
                    itemgroup = new ItemGroupQuery("b");
                    itemUnit = new ServiceUnitItemServiceQuery("c");
                    balance = new ItemBalanceQuery("d");

                    query.es.Top = 10;
                    query.es.Distinct = true;
                    query.Select
                        (
                            query.ItemGroupID,
                            itemgroup.ItemGroupName
                        );
                    query.InnerJoin(itemgroup).On(itemgroup.ItemGroupID == query.ItemGroupID);

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
                                    itemUnit.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboFromServiceUnitID")).SelectedValue
                                );
                    }
                    else
                    {
                        query.InnerJoin(itemUnit).On(
                                query.ItemID == itemUnit.ItemID &&
                                itemUnit.ServiceUnitID == ToServiceUnitID
                            );
                    }
                    query.LeftJoin(balance).On(
                        query.ItemID == balance.ItemID &&
                        balance.LocationID == LocationID);
                    if (VisibleOnly) query.Where(itemUnit.IsVisible == VisibleOnly);

                    query.Where(query.SRItemType == ItemType.Package,
                            query.Or(
                                    itemgroup.ItemGroupName.Like(searchTextContain),
                                    query.ItemGroupID.Like(searchTextContain)
                                ),
                            query.IsActive == true,
                            query.Or(
                                    query.GuarantorID.IsNull(),
                                    query.GuarantorID == string.Empty,
                                    query.GuarantorID == CboGuarantorId.SelectedValue),
                            query.Or(query.ValidityPeriodFrom.IsNull(), query.ValidityPeriodFrom <= TxtTransactionDate.SelectedDate),
                            query.Or(query.ValidityPeriodTo.IsNull(), query.ValidityPeriodTo >= TxtTransactionDate.SelectedDate)
                        );

                    if (!string.IsNullOrEmpty(cboItemGroupID.SelectedValue))
                        query.Where(query.ItemGroupID == cboItemGroupID.SelectedValue);

                    if (Request.QueryString["verif"] == "1")
                        query.Where(itemUnit.IsAllowEditByUserVerificated == true);

                    restrictions = new GuarantorItemRestrictionsQuery("rest");
                    itmrest = new ItemQuery("itmrest");
                    restrictions.Select(restrictions.ItemID);
                    restrictions.InnerJoin(itmrest).On(restrictions.ItemID == itmrest.ItemID);
                    restrictions.Where(restrictions.GuarantorID == CboGuarantorId.SelectedValue,
                                       itmrest.SRItemType == ItemType.Package);
                    DataTable dtRest2 = restrictions.LoadDataTable();
                    if (dtRest2.Rows.Count > 0)
                    {
                        //query.InnerJoin(restrictions).On(query.ItemID == restrictions.ItemID && restrictions.GuarantorID == CboGuarantorId.SelectedValue);
                        if (guar.IsItemServiceRestrictionStatusAllowed ?? true)
                            query.Where(query.ItemID.In(restrictions));
                        else
                            query.Where(query.ItemID.NotIn(restrictions));
                    }

                    tempTbl.Merge(query.LoadDataTable());
                }

                // Sort
                var dataSorted = tempTbl.Select("", "ItemGroupName ASC").CopyToDataTable();

                dataSorted.AcceptChanges();
                tbl = dataSorted;
                #endregion
            }
            catch (Exception ex)
            {

            }

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
                if (guar.LoadByPrimaryKey(CboGuarantorId.SelectedValue))
                {
                    isFornas = (guar.IsItemRestrictionsFornas ?? false);
                    isFormularium = (guar.IsItemRestrictionsFormularium ?? false);
                    isGeneric = (guar.IsItemRestrictionsGeneric ?? false);
                    isNonGeneric = (guar.IsItemRestrictionsNonGeneric ?? false);
                    isNonGenericLimited = (guar.IsItemRestrictionsNonGenericLimited ?? false);
                }
                    
                var query = new ItemQuery("a");
                var balance = new ItemBalanceQuery("b");
                var prod = new VwItemProductMedicNonMedicQuery("p");
                var ig = new ItemGroupQuery("ig");
                query.es.Top = 30;
                query.Select
                    (
                        query.ItemID,
                        query.ItemName,
                        (balance.Balance.Coalesce("0")).As("Balance"),
                        query.SRItemType,
                        "<'' AS ServiceUnitID>",
                        query.Notes,
                        ig.ItemGroupName
                    );
                query.InnerJoin(prod).On(
                    query.ItemID == prod.ItemID &&
                    prod.IsSalesAvailable == true
                    );
                query.InnerJoin(balance).On
                    (
                        query.ItemID == balance.ItemID &&
                        balance.LocationID == LocationID
                    );
                query.LeftJoin(ig).On
                    (
                        query.ItemGroupID == ig.ItemGroupID
                    );
                query.Where
                    (
                        query.Or
                            (
                            prod.Barcode == parameter,
                                query.ItemName.Like(searchTextContain),
                                query.ItemID.Like(searchTextContain)
                            ),
                        query.IsActive == true
                    );

                if (!string.IsNullOrEmpty(cboItemGroupID.SelectedValue))
                    query.Where(query.ItemGroupID == cboItemGroupID.SelectedValue);

                var xx = new List<Temiang.Dal.DynamicQuery.esComparison>();

                if (isFornas)
                    xx.Add(prod.IsFornas == true);

                if (isFormularium)
                    xx.Add(prod.IsFormularium == true);

                if (isGeneric)
                    xx.Add(prod.IsGeneric == true);

                if (isNonGeneric)
                    xx.Add(prod.IsNonGeneric == true);

                if (isNonGenericLimited)
                    xx.Add(prod.IsNonGenericLimited == true);

                if (xx.Count > 0)
                    query.Where(query.Or(xx.ToArray()));
                else
                {
                    var restrictions = new GuarantorItemRestrictionsQuery("rest");
                    var itmrest = new ItemQuery("itmrest");
                    restrictions.Select(restrictions.ItemID);
                    restrictions.InnerJoin(itmrest).On(restrictions.ItemID == itmrest.ItemID);
                    restrictions.Where(restrictions.GuarantorID == CboGuarantorId.SelectedValue,
                                       itmrest.SRItemType.In(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen));
                    DataTable dtRest = restrictions.LoadDataTable();
                    if (dtRest.Rows.Count > 0)
                    {
                        //query.InnerJoin(restrictions).On(query.ItemID == restrictions.ItemID && restrictions.GuarantorID == CboGuarantorId.SelectedValue);
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

        private DataTable PopulateProductItemGroup(string parameter)
        {
            DataTable tbl = null;
            string searchTextContain = string.Format("%{0}%", parameter);

            try
            {
                DataTable tempTbl = null;
                #region new - melihat batasan item di guarantor
                var isFornas = false;
                var guar = new Guarantor();
                if (guar.LoadByPrimaryKey(CboGuarantorId.SelectedValue))
                    isFornas = guar.IsItemRestrictionsFornas ?? false;

                var query = new ItemQuery("a");
                var itemgroup = new ItemGroupQuery("c");
                var balance = new ItemBalanceQuery("b");
                var prod = new VwItemProductMedicNonMedicQuery("p");
                query.es.Top = 10;
                query.es.Distinct = true;
                query.Select
                    (
                        query.ItemGroupID,
                        @"<c.ItemGroupName + '  **INV**' AS 'ItemGroupName'>"
                    //itemgroup.ItemGroupName
                    );
                query.InnerJoin(itemgroup).On(itemgroup.ItemGroupID == query.ItemGroupID);
                query.InnerJoin(prod).On(
                    query.ItemID == prod.ItemID &&
                    prod.IsSalesAvailable == true
                    );
                query.InnerJoin(balance).On
                    (
                        query.ItemID == balance.ItemID &&
                        balance.LocationID == LocationID
                    );
                query.Where
                    (
                        query.Or
                            (
                                itemgroup.ItemGroupName.Like(searchTextContain),
                                query.ItemGroupID.Like(searchTextContain)
                            ),
                        query.IsActive == true
                    );

                if (isFornas)
                    query.Where(prod.IsFornas == true);
                else
                {
                    var restrictions = new GuarantorItemRestrictionsQuery("rest");
                    var itmrest = new ItemQuery("itmrest");
                    restrictions.InnerJoin(itmrest).On(restrictions.ItemID == itmrest.ItemID);
                    restrictions.Where(restrictions.GuarantorID == CboGuarantorId.SelectedValue,
                                       itmrest.SRItemType.In(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen));
                    DataTable dtRest = restrictions.LoadDataTable();
                    if (dtRest.Rows.Count > 0)
                        query.InnerJoin(restrictions).On(query.ItemID == restrictions.ItemID &&
                                                         restrictions.GuarantorID == CboGuarantorId.SelectedValue);
                }

                if (AppSession.Parameter.IsListItemForTxOnlyInStock)
                    query.Where(balance.Balance > 0);

                tempTbl = query.LoadDataTable();

                // Sort
                var dataSorted = tempTbl.Select("", "ItemGroupName ASC").CopyToDataTable();

                dataSorted.AcceptChanges();
                tbl = dataSorted;
                #endregion
            }
            catch
            {
            }

            return tbl;
        }

        private DataTable PopulateItem(string parameter, bool VisibleOnly)
        {
            var tbl = new DataTable();

            DataTable temp1 = PopulateServiceItem(parameter, VisibleOnly);

            if (temp1 != null)
                tbl.Merge(temp1);

            if (Request.QueryString["type"] != "jo" && Request.QueryString["type"] != "mcu")
            {
                if (Request.QueryString["verif"] == "1")
                {
                    if (AppSession.Parameter.IsItemProductAllowEditByUserVerificated)
                    {
                        DataTable temp2 = PopulateProductItem(parameter);
                        if (temp2 != null)
                            tbl.Merge(temp2);
                    }
                }
                else
                {
                    DataTable temp2 = PopulateProductItem(parameter);
                    if (temp2 != null)
                        tbl.Merge(temp2);
                }
            }


            //// Dipindah ke Query (Handono 22-08-07)
            //// item casemix
            //if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP" && AppSession.Parameter.HealthcareInitial == "RSI")
            //    if (Request.QueryString["type"] == "jo")
            //    {
            //        if (!string.IsNullOrWhiteSpace(Request.QueryString["casemix"]) && Request.QueryString["casemix"] == "1")
            //        {
            //            // Select ItemID In CasemixCoveredDetail
            //            var casemix = new CasemixCoveredDetailCollection();
            //            if (casemix.LoadAll() && tbl.Rows.Count > 0)
            //            {
            //                foreach (DataRow row in tbl.Rows)
            //                {
            //                    if (!casemix.Select(c => c.ItemID).Contains(row["ItemID"].ToString())) row.Delete();
            //                }
            //                tbl.AcceptChanges();

            //                //tbl = tbl.AsEnumerable().Where(t => casemix.Select(c => c.ItemID).Contains(t.Field<string>("ItemID"))).CopyToDataTable();
            //            }
            //        }
            //        else
            //        {
            //            // Select ItemID Not In CasemixCoveredDetail
            //            var casemix = new CasemixCoveredDetailCollection();
            //            if (casemix.LoadAll() && tbl.Rows.Count > 0)
            //            {
            //                foreach (DataRow row in tbl.Rows)
            //                {
            //                    if (casemix.Select(c => c.ItemID).Contains(row["ItemID"].ToString())) row.Delete();
            //                }
            //                tbl.AcceptChanges();

            //                //tbl = tbl.AsEnumerable().Where(t => !casemix.Select(c => c.ItemID).Contains(t.Field<string>("ItemID"))).CopyToDataTable();
            //            }
            //        }
            //    }
            //}

            return tbl;
        }

        private DataTable PopulateItemGroup(string parameter, bool VisibleOnly)
        {
            var tbl = new DataTable();

            DataTable temp1 = PopulateServiceItemGroup(parameter, VisibleOnly);

            if (temp1 != null)
                tbl.Merge(temp1);

            if (Request.QueryString["type"] != "jo" && Request.QueryString["type"] != "mcu")
            {
                if (Request.QueryString["verif"] == "1")
                {
                    if (AppSession.Parameter.IsItemProductAllowEditByUserVerificated)
                    {
                        DataTable temp2 = PopulateProductItemGroup(parameter);
                        if (temp2 != null)
                            tbl.Merge(temp2);
                    }
                }
                else
                {
                    DataTable temp2 = PopulateProductItemGroup(parameter);
                    if (temp2 != null)
                        tbl.Merge(temp2);
                }
            }

            return tbl;
        }

        protected void cboItemGroupID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            DataTable tbl = PopulateItemGroup(e.Text, true);
            cboItemGroupID.DataSource = tbl.Rows.Count == 0 ? PopulateItemGroup(e.Text, true) : tbl;
            cboItemGroupID.DataBind();
        }

        protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            DataTable tbl = PopulateItem(e.Text, true);
            cboItemID.DataSource = tbl.Rows.Count == 0 ? PopulateItem(e.Text, true) : tbl;
            cboItemID.DataBind();
        }

        private void PopulateTariff(string itemID)
        {
            var entity = new Item();
            entity.LoadByPrimaryKey(itemID);

            var tariffDate = ((RadDatePicker)Helper.FindControlRecursive(Page, "txtTransactionDate")).SelectedDate.Value.Date;

            var reg = new Registration();
            reg.LoadByPrimaryKey(TxtRegistrationNo.Text);

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);

            switch (entity.SRItemType)
            {
                case ItemType.Medical:
                    {
                        var item = new ItemProductMedic();
                        item.LoadByPrimaryKey(itemID);

                        txtChargeQuantity.Value = 1;
                        txtItemUnit.Text = item.SRItemUnit;
                        txtStockQuantity.ReadOnly = AppSession.Parameter.IsReadonlyStockQtyOnTransChargesItem; //false;
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
                        txtStockQuantity.ReadOnly = AppSession.Parameter.IsReadonlyStockQtyOnTransChargesItem; //false;
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
                        txtStockQuantity.ReadOnly = AppSession.Parameter.IsReadonlyStockQtyOnTransChargesItem; //false;
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
                        if (grr.TariffCalculationMethod == 1) tariffDate = reg.RegistrationDate.Value.Date;
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
                        if (grr.TariffCalculationMethod == 1) tariffDate = reg.RegistrationDate.Value.Date;
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
                        if (grr.TariffCalculationMethod == 1) tariffDate = reg.RegistrationDate.Value.Date;
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
                        if (grr.TariffCalculationMethod == 1) tariffDate = reg.RegistrationDate.Value.Date;
                    }
                    break;
                default:
                    txtChargeQuantity.Value = 1;
                    txtItemUnit.Text = "X";
                    txtStockQuantity.ReadOnly = true;
                    txtStockQuantity.Value = 0D;
                    chkIsItemTypeService.Checked = false;

                    ClearFlagCheckBox();
                    pnlAsset.Visible = false;
                    chkIsPackage.Checked = true;
                    if (grr.TariffCalculationMethod == 1) tariffDate = reg.RegistrationDate.Value.Date;
                    break;
            }

            txtTariffDate.SelectedDate = tariffDate;

            var itemRooms = new AppStandardReferenceItemCollection();
            itemRooms.Query.Where(itemRooms.Query.StandardReferenceID == AppEnum.StandardReference.ItemTariffRoom,
                                  itemRooms.Query.ItemID == itemID, itemRooms.Query.IsActive == true);
            itemRooms.LoadAll();
            chkIsItemRoom.Checked = itemRooms.Count > 0;

            var compColl = new TariffComponentCollection();
            compColl.Query.OrderBy(compColl.Query.TariffComponentID.Ascending);
            compColl.LoadAll();

            System.Collections.Generic.IEnumerable<ItemTariffComponent> coll = null;
            ItemTariff tariff = null;
            // jika detail package

            if (!string.IsNullOrEmpty(TxtPackageReferenceNo.Text))
            {
                var tciPkgColl = new TransChargesItemCollection();
                tciPkgColl.Query.Where(tciPkgColl.Query.TransactionNo == TxtPackageReferenceNo.Text);
                tciPkgColl.LoadAll();
                var tciPkg = tciPkgColl.First();

                coll = Helper.Tariff.GetItemTariffComponentDetailPackageCollection(tciPkg.ItemID, itemID);

                // tariff
                tariff = Helper.Tariff.GetItemTariffDetailPackage(coll);

                ViewState["IsAdminCalculation"] = false;
            }
            else
            {
                coll = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, (string)ViewState["SRTariffType" + Request.UserHostName + PageId],
                    ((RadTextBox)Helper.FindControlRecursive(Page, "txtClassID")).Text, itemID);
                if (!coll.Any())
                    coll = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, (string)ViewState["SRTariffType" + Request.UserHostName + PageId],
                        AppSession.Parameter.DefaultTariffClass, itemID);
                if (!coll.Any())
                    coll = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, AppSession.Parameter.DefaultTariffType,
                        ((RadTextBox)Helper.FindControlRecursive(Page, "txtClassID")).Text, itemID);
                if (!coll.Any())
                    coll = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, AppSession.Parameter.DefaultTariffType,
                        AppSession.Parameter.DefaultTariffClass, itemID);

                tariff = (Helper.Tariff.GetItemTariff(tariffDate, (string)ViewState["SRTariffType" + Request.UserHostName + PageId], ((RadTextBox)Helper.FindControlRecursive(Page, "txtClassID")).Text, ((RadTextBox)Helper.FindControlRecursive(Page, "txtClassID")).Text, itemID, (string)ViewState["GuarantorID" + Request.UserHostName + PageId], false, reg.SRRegistrationType) ??
                          Helper.Tariff.GetItemTariff(tariffDate, (string)ViewState["SRTariffType" + Request.UserHostName + PageId], AppSession.Parameter.DefaultTariffClass, ((RadTextBox)Helper.FindControlRecursive(Page, "txtClassID")).Text, itemID, (string)ViewState["GuarantorID" + Request.UserHostName + PageId], false, reg.SRRegistrationType)) ??
                         (Helper.Tariff.GetItemTariff(tariffDate, AppSession.Parameter.DefaultTariffType, ((RadTextBox)Helper.FindControlRecursive(Page, "txtClassID")).Text, ((RadTextBox)Helper.FindControlRecursive(Page, "txtClassID")).Text, itemID, (string)ViewState["GuarantorID" + Request.UserHostName + PageId], false, reg.SRRegistrationType) ??
                          Helper.Tariff.GetItemTariff(tariffDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, ((RadTextBox)Helper.FindControlRecursive(Page, "txtClassID")).Text, itemID, (string)ViewState["GuarantorID" + Request.UserHostName + PageId], false, reg.SRRegistrationType));

                if (tariff != null)
                {
                    chkIsVariable.Enabled = tariff.IsAllowVariable ?? false;
                    chkIsVariable.Checked = tariff.IsAllowVariable ?? false;
                    chkIsDiscount.Enabled = tariff.IsAllowDiscount ?? false;
                    chkIsCito.Enabled = tariff.IsAllowCito ?? false;
                    cboSRCitoPercentage.Enabled = chkIsCito.Enabled && (tariff.IsCitoFromStandardReference ?? false);
                    ViewState["IsAdminCalculation"] = tariff.IsAdminCalculation ?? false;
                }
            }

            if (Request.QueryString["type"] == "tr" || Request.QueryString["type"] == "ds" || Request.QueryString["type"] == "mcu")
            {
                pnltariff.Visible = true;

                if (!coll.Any() || entity.SRItemType == ItemType.Medical || entity.SRItemType == ItemType.NonMedical || entity.SRItemType == ItemType.Kitchen)
                {
                    chkIsItemProduct.Checked = true;
                    if (AppSession.Parameter.IsTariffPriceVisibleOnlyForAdm)
                    {
                        pnlVariableAndDiscount.Visible = (!string.IsNullOrEmpty(Request.QueryString["verif"]) &&
                                             (Request.QueryString["verif"] == "1"));
                    }
                    else
                        pnlVariableAndDiscount.Visible = AppSession.Parameter.TariffComponentPriceVisible;

                    pnlComponentTariff.Visible = false;
                    pnlPriceJO.Visible = false;
                    if (chkIsItemRoom.Checked && ChkIsRoomIn.Checked)
                        txtPrice1.Value = tariff != null ? (double)(tariff.Price ?? 0) - ((double)(tariff.Price ?? 0) * TxtTariffDiscForRoomIn.Value / 100) : 0D;
                    else
                        txtPrice1.Value = tariff != null ? (double)(tariff.Price ?? 0) : 0D;

                    tbDiscount.Visible = false;
                }
                else
                {
                    chkIsItemProduct.Checked = false;
                    pnlVariableAndDiscount.Visible = false;
                    pnlComponentTariff.Visible = true;
                    pnlPriceJO.Visible = false;

                    tbDiscount.Visible = false;

                    PopulateTariffComponentGrid(coll);
                }
            }
            else
            {
                //khusus rsi, item lab roche auto realisasi, jd langsung isi tariff dr awal
                if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP")
                {
                    if (AppSession.Parameter.HealthcareInitial == "RSI")
                    {
                        if (cboToServiceUnitID.SelectedValue == AppSession.Parameter.ServiceUnitLaboratoryID)
                        {
                            var item = new Item();
                            item.LoadByPrimaryKey(itemID);

                            if (!string.IsNullOrEmpty(item.ItemIDExternal))
                            {
                                pnltariff.Visible = true;

                                chkIsItemProduct.Checked = false;
                                pnlVariableAndDiscount.Visible = false;
                                pnlComponentTariff.Visible = true;
                                pnlPriceJO.Visible = false;

                                tbDiscount.Visible = false;

                                PopulateTariffComponentGrid(coll);
                            }
                            else pnltariff.Visible = false;
                        }
                        else pnltariff.Visible = false;
                    }
                    else pnltariff.Visible = false;
                }
                else pnltariff.Visible = false;


                if (pnltariff.Visible == false)
                {
                    //Kalau JO Component Tariff nya di munculkan supaya bisa ganti tariff
                    if ((tariff != null) && (tariff.IsAllowVariable ?? false))
                    {
                        pnltariff.Visible = true;

                        chkIsItemProduct.Checked = false;
                        pnlVariableAndDiscount.Visible = false;
                        pnlComponentTariff.Visible = true;
                        pnlPriceJO.Visible = false;

                        tbDiscount.Visible = false;

                        PopulateTariffComponentGrid(coll);
                    }
                    else
                    {
                        if (AppSession.Parameter.IsTariffPriceVisibleOnlyForAdm)
                        {
                            pnlPriceJO.Visible = (!string.IsNullOrEmpty(Request.QueryString["verif"]) &&
                                                 (Request.QueryString["verif"] == "1"));
                        }
                        else
                            pnlPriceJO.Visible = AppSession.Parameter.TariffComponentPriceVisible;

                        if (chkIsItemRoom.Checked && ChkIsRoomIn.Checked) txtPriceJO.Value = tariff != null ? (double)(tariff.Price ?? 0) - ((double)(tariff.Price ?? 0) * TxtTariffDiscForRoomIn.Value / 100) : 0D;
                        else txtPriceJO.Value = tariff != null ? (double)(tariff.Price ?? 0) : 0D;

                        txtPriceJO.ReadOnly = tariff == null || !(tariff.IsAllowVariable ?? false);
                    }
                }
               
                //if (AppSession.Parameter.IsTariffPriceVisibleOnlyForAdm)
                //{
                //    pnlPriceJO.Visible = (!string.IsNullOrEmpty(Request.QueryString["verif"]) &&
                //                         (Request.QueryString["verif"] == "1"));
                //}
                //else
                //{
                //    if (AppSession.Parameter.TariffComponentPriceVisible)
                //    {
                //        pnltariff.Visible = true;
                //        chkIsItemProduct.Checked = false;
                //        pnlVariableAndDiscount.Visible = false;
                //        if (tariff.IsAllowVariable == false)
                //        {
                //            pnlComponentTariff.Visible = false;
                //            pnlPriceJO.Visible = true;
                //        }
                //        else
                //        {
                //            pnlPriceJO.Visible = false;
                //            pnlComponentTariff.Visible = true;
                //        }                        
                //        tbDiscount.Visible = false;
                //        PopulateTariffComponentGrid(coll);
                //    }
                //    //pnlPriceJO.Visible = AppSession.Parameter.TariffComponentPriceVisible;
                //}
                //if (chkIsItemRoom.Checked && ChkIsRoomIn.Checked) txtPriceJO.Value = tariff != null ? (double)(tariff.Price ?? 0) - ((double)(tariff.Price ?? 0) * TxtTariffDiscForRoomIn.Value / 100) : 0D;
                //else txtPriceJO.Value = tariff != null ? (double)(tariff.Price ?? 0) : 0D;

                //txtPriceJO.ReadOnly = tariff == null || !(tariff.IsAllowVariable ?? false);
            }
        }

        private void PopulateItemConditionRuleFromRegistration(string itemId)
        {
            var promo = new ItemConditionRuleItem();
            if (promo.LoadByPrimaryKey(hdnItemConditionRuleID.Value, itemId))
            {
                var toServiceUnit = string.Empty;
                if (Request.QueryString["type"] == "tr" || Request.QueryString["type"] == "mcu")
                {
                    if (Helper.FindControlRecursive(Page, "pnlResponUnit").Visible)
                        toServiceUnit = ((RadComboBox)Helper.FindControlRecursive(Page, "cboResponUnit")).SelectedValue;
                    else
                        toServiceUnit = ((RadComboBox)Helper.FindControlRecursive(Page, "cboFromServiceUnitID")).SelectedValue;
                }
                else
                {
                    toServiceUnit = ((RadComboBox)Helper.FindControlRecursive(Page, "cboToServiceUnitID")).SelectedValue;
                }

                var unit = new ItemConditionRuleServiceUnit();
                if (unit.LoadByPrimaryKey(hdnItemConditionRuleID.Value, toServiceUnit))
                {
                    var q = new ItemConditionRuleQuery();
                    q.Where(q.ItemConditionRuleID == hdnItemConditionRuleID.Value);
                    cboItemConditionRuleID.DataSource = q.LoadDataTable();
                    cboItemConditionRuleID.DataBind();
                    cboItemConditionRuleID.SelectedValue = hdnItemConditionRuleID.Value;
                }
                else
                {
                    cboItemConditionRuleID.Items.Clear();
                    cboItemConditionRuleID.SelectedValue = string.Empty;
                    cboItemConditionRuleID.Text = string.Empty;
                }
            }
            else
            {
                cboItemConditionRuleID.Items.Clear();
                cboItemConditionRuleID.SelectedValue = string.Empty;
                cboItemConditionRuleID.Text = string.Empty;
            }
        }

        private string GetCoveredItem(string itemId)
        {
            var grr = new Guarantor();
            if (grr.LoadByPrimaryKey(CboGuarantorId.SelectedValue) && grr.SRGuarantorType != AppSession.Parameter.GuarantorTypeSelf)
            {
                var reg = new Registration();
                if (reg.LoadByPrimaryKey(TxtRegistrationNo.Text) && reg.SRBussinesMethod == AppSession.Parameter.BusinessMethodCoverage)
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
                        regType = reg.SRRegistrationType;

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
                    types.Query.Where(types.Query.GuarantorID == CboGuarantorId.SelectedValue);
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
                    if (grrItem.LoadByPrimaryKey(CboGuarantorId.SelectedValue, itemId))
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

                    var regItems = new RegistrationItemRuleCollection();
                    regItems.Query.Where(regItems.Query.RegistrationNo == TxtRegistrationNo.Text, regItems.Query.ItemID == itemId);
                    regItems.LoadAll();
                    foreach (var regItem in regItems)
                    {
                        srGuarantorRuleType = regItem.SRGuarantorRuleType;
                        amountValue = regItem.AmountValue ?? 0;
                        isValueInPercent = regItem.IsValueInPercent ?? false;
                        isInclude = regItem.IsInclude ?? false;
                        isGuarantor = regItem.IsToGuarantor ?? false;
                    }

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
            }

            return string.Empty;
        }

        protected void cboItemGroupID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value))
            {
                cboItemGroupID.SelectedValue = string.Empty;
                cboItemGroupID.Text = string.Empty;
            }

            cboItemID.Items.Clear();
            cboItemID.Text = string.Empty;

            pnltariff.Visible = false;
            chkIsItemProduct.Checked = false;
            pnlVariableAndDiscount.Visible = false;
            pnlComponentTariff.Visible = false;

            ClearFlagCheckBox();

            txtChargeQuantity.Value = 1;
            txtStockQuantity.ReadOnly = false;
            txtStockQuantity.Value = 0D;
            txtItemUnit.Text = string.Empty;
            txtTariffDate.SelectedDate = TxtTransactionDate.SelectedDate;
            cboSRDiscountReason.SelectedValue = string.Empty;
            chkIsAssetUtilization.Checked = false;
            chkIsAssetUtilization.Enabled = false;
            cboAssetID.Enabled = false;
            chkIsItemRoom.Checked = false;

            Session["cboItemGroupID" + Request.UserHostName + PageId] = e.Value;
        }

        protected void cboItemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (e.Value == string.Empty)
            {
                pnltariff.Visible = false;
                chkIsItemProduct.Checked = false;
                pnlVariableAndDiscount.Visible = false;
                pnlComponentTariff.Visible = false;

                ClearFlagCheckBox();

                txtChargeQuantity.Value = 1;
                txtStockQuantity.ReadOnly = false;
                txtStockQuantity.Value = 0D;
                txtItemUnit.Text = string.Empty;
                txtTariffDate.SelectedDate = TxtTransactionDate.SelectedDate;
                cboSRDiscountReason.SelectedValue = string.Empty;
                chkIsAssetUtilization.Checked = false;
                chkIsAssetUtilization.Enabled = false;
                cboAssetID.Enabled = false;
                chkIsItemRoom.Checked = false;

                return;
            }

            PopulateTariff(e.Value);
            lblMsg.Text = GetCoveredItem(e.Value);
            txtChargeQuantity.Focus();

            if (string.IsNullOrWhiteSpace(txtFilmNo.Text))
            {
                if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP" && AppSession.Parameter.HealthcareInitial == "RSI")
                {
                    var i = new Item();
                    i.LoadByPrimaryKey(e.Value);
                    if (i.SRItemType != ItemType.Radiology) return;
                    if (i.ItemIDExternal == "CR") _filmAutoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.CRRadiologyFilmNo);
                    else if (i.ItemIDExternal == "CT") _filmAutoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.CTRadiologyFilmNo);
                    else return;
                    txtFilmNo.Text = _filmAutoNumber.LastCompleteNumber;
                }
            }

            PopulateItemConditionRuleFromRegistration(e.Value);
        }

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            if (ViewState["paramedic" + Request.UserHostName + PageId] == null)
                PopulateParamedicByServiceUnit();

            DataView view = ((DataTable)ViewState["paramedic" + Request.UserHostName + PageId]).DefaultView;
            view.RowFilter = string.Format("ParamedicID LIKE '%{0}%' OR ParamedicName LIKE '%{0}%'", e.Text);
            cboParamedicID.DataSource = view;
            cboParamedicID.DataBind();

            //if (ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
            //{
            //    var psdColl = new ParamedicScheduleDateCollection();
            //    DataTable dtb = psdColl.GetParamedicID(AppSession.Parameter.ServiceUnitLaboratoryID, hdnSrRegistrationType.Value);

            //    if (dtb.Rows.Count == 1)
            //    {
            //        cboParamedicID.SelectedValue = dtb.Rows[0]["ParamedicID"].ToString();
            //        cboParamedicID.Text = dtb.Rows[0]["ParamedicName"].ToString();
            //    }
            //}

            if (Request.QueryString["type"] == "ds")
            {
                var psdColl = new ParamedicScheduleDateCollection();
                DataTable dtb = psdColl.GetParamedicID(ToServiceUnitID, hdnSrRegistrationType.Value);

                if (dtb.Rows.Count == 1)
                {
                    cboParamedicID.SelectedValue = dtb.Rows[0]["ParamedicID"].ToString();
                    cboParamedicID.Text = dtb.Rows[0]["ParamedicName"].ToString();
                }
            }
        }

        protected void cboPhysicianID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        private string _toSericeUnitID;

        private string ToServiceUnitID
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_toSericeUnitID))
                    _toSericeUnitID = ((RadComboBox)Helper.FindControlRecursive(Page, "cboToServiceUnitID")).SelectedValue;

                return _toSericeUnitID;
            }
        }
        protected void cboPhysicianID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            if (ViewState["paramedic" + Request.UserHostName + PageId] == null)
                PopulateParamedicByServiceUnit();

            DataView view = ((DataTable)ViewState["paramedic" + Request.UserHostName + PageId]).DefaultView;
            view.RowFilter = string.Format("ParamedicID LIKE '%{0}%' OR ParamedicName LIKE '%{0}%'", e.Text);

            var combo = o as RadComboBox;

            combo.DataSource = view;
            combo.DataBind();

            //if (ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
            if (Request.QueryString["type"] == "ds")
            {
                var psdColl = new ParamedicScheduleDateCollection();
                DataTable dtb = psdColl.GetParamedicID(ToServiceUnitID, hdnSrRegistrationType.Value);

                if (dtb.Rows.Count == 1)
                {
                    combo.SelectedValue = dtb.Rows[0]["ParamedicID"].ToString();
                    combo.Text = dtb.Rows[0]["ParamedicName"].ToString();
                }
            }
        }

        protected void cboItemConditionRuleID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemConditionRuleName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemConditionRuleID"].ToString();
        }

        protected void cboItemConditionRuleID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(TxtRegistrationNo.Text);

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);

            var tariffDate = grr.TariffCalculationMethod == 1
                ? reg.RegistrationDate.Value.Date
                : TxtTransactionDate.SelectedDate.Value.Date;

            string searchTextContain = string.Format("%{0}%", e.Text);

            var query = new ItemConditionRuleQuery("a");
            var icrsuq = new ItemConditionRuleServiceUnitQuery("c");

            if (Request.QueryString["type"] == "tr" || Request.QueryString["type"] == "mcu")
            {
                if (Helper.FindControlRecursive(Page, "pnlResponUnit").Visible)
                    query.InnerJoin(icrsuq).On(
                            query.ItemConditionRuleID == icrsuq.ItemConditionRuleID &&
                            icrsuq.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboResponUnit")).SelectedValue
                        );
                else
                    query.InnerJoin(icrsuq).On(
                            query.ItemConditionRuleID == icrsuq.ItemConditionRuleID &&
                            icrsuq.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboFromServiceUnitID")).SelectedValue
                        );
            }
            else
            {
                query.InnerJoin(icrsuq).On(
                        query.ItemConditionRuleID == icrsuq.ItemConditionRuleID &&
                        icrsuq.ServiceUnitID == ((RadComboBox)Helper.FindControlRecursive(Page, "cboToServiceUnitID")).SelectedValue
                    );
            }
            query.Select(query.ItemConditionRuleID, query.ItemConditionRuleName);
            query.Where
                (
                 query.StartingDate.Date() <= tariffDate,
                 query.EndingDate.Date() >= tariffDate,
                 query.Or
                     (
                         query.ItemConditionRuleID.Like(searchTextContain),
                         query.ItemConditionRuleName.Like(searchTextContain)
                     )
                );
            query.es.Top = 10;
            DataTable dtb = query.LoadDataTable();
            cboItemConditionRuleID.DataSource = dtb;
            cboItemConditionRuleID.DataBind();
        }

        #region Properties for return entry value

        public String ImageCaptureInString
        {
            get { return hdnImgFromWebCam.Value; }
        }

        public String SequenceNo
        {
            get { return hdnSequenceNo.Value; }
            set { hdnSequenceNo.Value = value; }
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

        public DateTime? TariffDate
        {
            get { return txtTariffDate.SelectedDate; }
        }

        public Decimal Price
        {
            get
            {
                //if (Request.QueryString["type"] == "jo" && AppSession.Parameter.TariffComponentPriceVisible == false)  
                if (Request.QueryString["type"] == "jo" && pnltariff.Visible == false)                
                    return Convert.ToDecimal(txtPriceJO.Value);                 

                if (pnlVariableAndDiscount.Visible || chkIsItemProduct.Checked)
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
                decimal basicCitoAmount = 0;
                if (chkIsCito.Checked)
                {
                    DateTime tariffDate;

                    var itm = new Item();
                    itm.LoadByPrimaryKey(cboItemID.SelectedValue);

                    if (itm.SRItemType == ItemType.Medical || itm.SRItemType == ItemType.NonMedical ||
                        itm.SRItemType == ItemType.Kitchen)
                    {
                        tariffDate = TxtTransactionDate.SelectedDate.Value.Date;
                    }
                    else
                    {
                        var reg = new Registration();
                        reg.LoadByPrimaryKey(TxtRegistrationNo.Text);

                        var grr = new Guarantor();
                        grr.LoadByPrimaryKey(reg.GuarantorID);

                        tariffDate = grr.TariffCalculationMethod == 1
                            ? reg.RegistrationDate.Value.Date
                            : TxtTransactionDate.SelectedDate.Value.Date;
                    }

                    var tariff = new ItemTariff();
                    if (!tariff.Load(GetItemTariffQuery(tariffDate, hdnSrTariffType.Value, hdnChargeClassId.Value, cboItemID.SelectedValue)))
                        if (!tariff.Load(GetItemTariffQuery(tariffDate, hdnSrTariffType.Value, AppSession.Parameter.DefaultTariffClass, cboItemID.SelectedValue)))
                            if (!tariff.Load(GetItemTariffQuery(tariffDate, AppSession.Parameter.DefaultTariffType, hdnChargeClassId.Value, cboItemID.SelectedValue)))
                                tariff.Load(GetItemTariffQuery(tariffDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, cboItemID.SelectedValue));

                    tariff.UpdateCitoFromStdRef(SRCitoPercentage);

                    basicCitoAmount = tariff.CitoValue ?? 0;
                }

                foreach (GridDataItem dataItem in grdTariff.MasterTableView.Items)
                {
                    if ((dataItem["TariffComponentName"].FindControl("txtPercentDiscount") as RadNumericTextBox).Text != string.Empty)
                    {
                        decimal diskonpercent = (decimal)(dataItem["TariffComponentName"].FindControl("txtPercentDiscount") as RadNumericTextBox).Value;

                        decimal totalPrice = (decimal)(dataItem["TariffComponentName"].FindControl("txtPrice") as RadNumericTextBox).Value;
                        if (basicCitoAmount > 0)
                            totalPrice = totalPrice + (totalPrice * basicCitoAmount / 100);

                        discount += totalPrice * (diskonpercent / 100);
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

                if (pnlVariableAndDiscount.Visible || chkIsItemProduct.Checked)
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

        public String FilmNo
        {
            get { return txtFilmNo.Text; }
        }

        public String ItemConditionRuleID
        {
            get { return cboItemConditionRuleID.SelectedValue; }
        }

        public String ItemConditionRuleName
        {
            get { return cboItemConditionRuleID.Text; }
        }

        public Boolean IsItemTypeService
        {
            get { return chkIsItemTypeService.Checked; }
        }

        public TransChargesItemCompCollection TariffComponent
        {
            get
            {
                if (pnlVariableAndDiscount.Visible || chkIsItemProduct.Checked)
                    return null;

                decimal basicCitoAmount = 0;
                if (chkIsCito.Checked)
                {
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(TxtRegistrationNo.Text);

                    var grr = new Guarantor();
                    grr.LoadByPrimaryKey(reg.GuarantorID);

                    var tariffDate = grr.TariffCalculationMethod == 1
                        ? reg.RegistrationDate.Value.Date
                        : TxtTransactionDate.SelectedDate.Value.Date;

                    var tariff = new ItemTariff();
                    if (!tariff.Load(GetItemTariffQuery(tariffDate, hdnSrTariffType.Value, hdnChargeClassId.Value, cboItemID.SelectedValue)))
                        if (!tariff.Load(GetItemTariffQuery(tariffDate, hdnSrTariffType.Value, AppSession.Parameter.DefaultTariffClass, cboItemID.SelectedValue)))
                            if (!tariff.Load(GetItemTariffQuery(tariffDate, AppSession.Parameter.DefaultTariffType, hdnChargeClassId.Value, cboItemID.SelectedValue)))
                                tariff.Load(GetItemTariffQuery(tariffDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, cboItemID.SelectedValue));

                    tariff.UpdateCitoFromStdRef(SRCitoPercentage);

                    basicCitoAmount = tariff.CitoValue ?? 0;
                }

                var coll = new TransChargesItemCompCollection();
                foreach (GridDataItem dataItem in grdTariff.MasterTableView.Items)
                {
                    TransChargesItemComp entity = coll.AddNew();
                    entity.SequenceNo = hdnSequenceNo.Value;
                    entity.TariffComponentID = dataItem["TariffComponentID"].Text;

                    if ((dataItem["TariffComponentName"].FindControl("txtPrice") as RadNumericTextBox).Text == string.Empty)
                        entity.Price = (decimal)0D;
                    else
                        entity.Price = (decimal)(dataItem["TariffComponentName"].FindControl("txtPrice") as RadNumericTextBox).Value;

                    if (basicCitoAmount > 0)
                        entity.CitoAmount = entity.Price * basicCitoAmount / 100;
                    else entity.CitoAmount = 0;

                    if ((dataItem["TariffComponentName"].FindControl("txtPercentDiscount") as RadNumericTextBox).Text != string.Empty)
                    {
                        //decimal price = (decimal)(dataItem["TariffComponentName"].FindControl("txtPrice") as RadNumericTextBox).Value;
                        decimal diskonpercent = (decimal)(dataItem["TariffComponentName"].FindControl("txtPercentDiscount") as RadNumericTextBox).Value;

                        entity.DiscountAmount = (entity.Price + entity.CitoAmount) * (diskonpercent / 100);
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

                    entity.FeeDiscountPercentage = (decimal)((dataItem["TariffComponentName"].FindControl("txtPercentFeeDiscount") as RadNumericTextBox).Value ?? 0);
                }
                return coll;
            }
        }

        #endregion

        public static ItemTariffQuery GetItemTariffQuery(DateTime transDate, string tariffType, string classID, string itemID)
        {
            var query = new ItemTariffQuery();
            query.es.Top = 1;
            query.Where
            (
                query.SRTariffType == tariffType,
                query.ClassID == classID,
                query.ItemID == itemID,
                query.StartingDate <= transDate
            );
            query.OrderBy(query.StartingDate.Descending);

            return query;
        }
    }
}