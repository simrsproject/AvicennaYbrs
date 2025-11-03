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
using System.Text.RegularExpressions;
using System.Collections;
using System.Text;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class ExamOrderEntry : BasePageDialogEntry
    {
        protected string TransactionNo
        {
            get
            {
                var trno = Request.QueryString["trno"];
                return trno == "undefined" ? string.Empty : trno;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            // Program Fiture
            IsSingleRecordMode = true; //Save then close
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = false;
            // -------------------

            if (!IsPostBack)
            {

                TransChargesItems = null;
                TransChargesItemComps = null;
                TransChargesItemConsumptions = null;


                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Exam Order for : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }

                ComboBox.PopulateWithServiceUnitForTransactionJO(cboServiceUnitIDJO, TransactionCode.JobOrder, false);

            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var header = new TransCharges();
            header.LoadByPrimaryKey(TransactionNo);

            cboServiceUnitIDJO.SelectedValue = header.ToServiceUnitID;
            cboServiceUnitIDJO.Enabled = false;

            txtTransactionDateJO.SelectedDate = header.TransactionDate.Value.Date;
            txtNotesJO.Text = header.Notes;

            var query = new TransChargesItemQuery("a");
            var item = new ItemQuery("b");

            query.Select(
                query,
                item.ItemName.As("refToItem_ItemName")
                );
            query.InnerJoin(item).On(query.ItemID == item.ItemID);
            query.OrderBy(query.SequenceNo.Ascending);

            // Sub Query filter
            var tci = new TransChargesItemQuery("d");
            tci.Select(tci.TransactionNo, tci.SequenceNo);
            tci.Where(tci.TransactionNo == TransactionNo, tci.SequenceNo == query.SequenceNo,
                tci.IsExtraItem == true,
                tci.IsSelectedExtraItem == false);
            query.Where(query.TransactionNo == TransactionNo, query.NotExists(tci));

            var coll = new TransChargesItemCollection();
            coll.Load(query);

            TransChargesItems = coll;

            grdTransChargesItem.Rebind();
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }
        protected override void OnMenuNewClick()
        {
            TransChargesItems = null;
            TransChargesItemComps = null;
            TransChargesItemComps.LoadAll();

            TransChargesItemConsumptions = null;
            TransChargesItemConsumptions.LoadAll();

            grdTransChargesItem.Rebind();

            cboServiceUnitIDJO.SelectedValue = string.Empty;
            cboServiceUnitIDJO.Enabled = true;
            txtTransactionDateJO.SelectedDate = DateTime.Now.Date;
            txtNotesJO.Text = string.Empty;
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Save();
        }


        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Save();
        }

        protected override void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
        }

        protected override void OnMenuEditClick()
        {
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuNewClick(ValidateArgs args)
        {

        }
        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        protected override void OnMenuRejournalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            return string.Empty;
        }
        public override string OnGetScriptToolBarSaveClicking()
        {
            return string.Empty;
        }
        public override bool OnGetStatusMenuEdit()
        {
            return true;
        }

        public override bool OnGetStatusMenuDelete()
        {
            return true;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return true;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return true;
        }

        protected override void OnInitializeAjaxManager(RadAjaxManager ajaxManager)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        protected void cboItemIDJO_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            DataTable tbl = PopulateServiceItem(e.Text);
            (o as RadComboBox).DataSource = tbl.Rows.Count == 0 ? PopulateServiceItem(e.Text) : tbl;
            (o as RadComboBox).DataBind();
        }

        protected void cboItemIDJO_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        private DataTable PopulateServiceItem(string searchText)
        {
            DataTable tbl = null;

            try
            {
                string searchTextContain = string.Format("%{0}%", searchText);

                var query = new ItemQuery("a");
                var itemUnit = new ServiceUnitItemServiceQuery("c");

                query.es.Top = 15;
                query.Select
                    (
                        query.ItemID,
                        (query.ItemName + " [" + query.ItemID + "]").As("ItemName")
                    );
                query.InnerJoin(itemUnit).On
                    (
                        query.ItemID == itemUnit.ItemID &&
                        itemUnit.ServiceUnitID == cboServiceUnitIDJO.SelectedValue
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

        private ItemTariffQuery GetItemTariffQuery(DateTime tariffDate, string tariffType, string classID, string itemID)
        {
            var query = new ItemTariffQuery();
            query.es.Top = 1;
            query.Where
                (
                    query.SRTariffType == tariffType,
                    query.ClassID == classID,
                    query.ItemID == itemID,
                    query.StartingDate <= tariffDate
                );
            query.OrderBy(query.StartingDate.Descending);

            return query;
        }

        private TransChargesItemCollection TransChargesItems
        {
            get
            {
                if (Session["collTransChargesItem" + Request.UserHostName] == null)
                {
                    var query = new TransChargesItemQuery("a");
                    query.Select(
                        query,
                        "<'' AS refToItem_ItemName>"
                        );
                    query.Where(query.TransactionNo == string.Empty);

                    var coll = new TransChargesItemCollection();
                    coll.Load(query);

                    Session["collTransChargesItem" + Request.UserHostName] = coll;
                }
                return Session["collTransChargesItem" + Request.UserHostName] as TransChargesItemCollection;
            }
            set { Session["collTransChargesItem" + Request.UserHostName] = value; }
        }

        private TransChargesItemCompCollection TransChargesItemComps
        {
            // TransChargesItemComp disini tidak perlu di isi dan akan diisi sewaktu job order realization
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collTransChargesItemComp" + Request.UserHostName];
                    if (obj != null)
                        return ((TransChargesItemCompCollection)(obj));
                }

                var coll = new TransChargesItemCompCollection();

                var query = new TransChargesItemCompQuery("a");
                var comp = new TariffComponentQuery("b");
                var tci = new TransChargesItemQuery("d");

                tci.Select(tci.TransactionNo, tci.SequenceNo);
                tci.Where(tci.TransactionNo == query.TransactionNo, tci.SequenceNo == query.SequenceNo,
                    tci.IsExtraItem == true,
                    tci.IsSelectedExtraItem == false);

                query.Select(
                    query,
                    comp.TariffComponentName.As("refToTariffComponent_TariffComponentName"),
                    comp.IsTariffParamedic
                );
                query.InnerJoin(comp).On(query.TariffComponentID == comp.TariffComponentID);

                if (Request.QueryString["type"] == "mcu")
                    query.Where(query.TransactionNo == TransactionNo, query.NotExists(tci));
                else
                    query.Where(query.TransactionNo == TransactionNo);

                query.OrderBy(
                    query.SequenceNo.Ascending,
                    query.TariffComponentID.Ascending
                );

                coll.Load(query);

                Session["collTransChargesItemComp" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collTransChargesItemComp" + Request.UserHostName] = value; }
        }
        private TransChargesItemConsumptionCollection TransChargesItemConsumptions
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collTransChargesItemConsumption" + Request.UserHostName];
                    if (obj != null)
                        return ((TransChargesItemConsumptionCollection)(obj));
                }

                var coll = new TransChargesItemConsumptionCollection();

                var query = new TransChargesItemConsumptionQuery("a");
                var item = new ItemQuery("b");
                var tci = new TransChargesItemQuery("d");

                tci.Select(tci.TransactionNo, tci.SequenceNo);
                tci.Where(tci.TransactionNo == query.TransactionNo, tci.SequenceNo == query.SequenceNo,
                    tci.IsExtraItem == true,
                    tci.IsSelectedExtraItem == false);

                query.Select(
                    query,
                    item.ItemName.As("refToItem_ItemName")
                );
                query.InnerJoin(item).On(query.DetailItemID == item.ItemID);

                if (Request.QueryString["type"] == "mcu")
                    query.Where(query.TransactionNo == TransactionNo, query.NotExists(tci));
                else
                    query.Where(query.TransactionNo == TransactionNo);

                coll.Load(query);

                Session["collTransChargesItemConsumption" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collTransChargesItemConsumption" + Request.UserHostName] = value; }
        }
        protected void grdTransChargesItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdTransChargesItem.DataSource = TransChargesItems;
        }

        protected void grdTransChargesItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;
            var sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"]);
            var entity = TransChargesItems.SingleOrDefault(j => j.SequenceNo == sequenceNo);
            if (entity == null) return;

            if (entity != null)
            {
                if (Request.QueryString["mod"] == "new")
                {
                    if (!(entity.IsPackage ?? false))
                    {
                        foreach (var detail in TransChargesItems.Where(d => d.ParentNo == entity.SequenceNo))
                        {
                            detail.MarkAsDeleted();
                        }

                        foreach (var comps in TransChargesItemComps.Where(comps => comps.SequenceNo == sequenceNo))
                        {
                            comps.MarkAsDeleted();
                        }

                        foreach (var consm in TransChargesItemConsumptions.Where(consm => consm.SequenceNo == sequenceNo))
                        {
                            consm.MarkAsDeleted();
                        }
                    }
                    else
                    {
                        foreach (TransChargesItem pac in TransChargesItems.Where(pac => pac.ParentNo == sequenceNo || pac.SequenceNo == sequenceNo))
                        {
                            foreach (var comp in TransChargesItemComps.Where(comp => comp.SequenceNo == pac.SequenceNo))
                            {
                                comp.MarkAsDeleted();
                            }

                            foreach (var cons in TransChargesItemConsumptions.Where(cons => cons.SequenceNo == pac.SequenceNo))
                            {
                                cons.MarkAsDeleted();
                            }

                            pac.MarkAsDeleted();
                        }

                    }
                    entity.MarkAsDeleted();
                }
                else
                {
                    var transactionNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][TransChargesItemMetadata.ColumnNames.TransactionNo]);
                    var hd = new TransCharges();
                    if (hd.LoadByPrimaryKey(transactionNo))
                    {
                        if (string.IsNullOrEmpty(hd.PackageReferenceNo))
                            entity.MarkAsDeleted();
                        else
                            entity.IsVoid = true;
                    }
                }
            }

            cboServiceUnitIDJO.Enabled = !TransChargesItems.Any();
            grdTransChargesItem.Rebind();
        }

        protected void grdTransChargesItem_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Insert")
            {
                var cmdItem = grdTransChargesItem.MasterTableView.GetItems(GridItemType.CommandItem)[0];

                if (string.IsNullOrEmpty((cmdItem.FindControl("cboItemIDJO") as RadComboBox).SelectedValue))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "order", "alert('Item order required');", true);
                    return;
                }

                var detail = TransChargesItems.SingleOrDefault(j => j.ItemID == (cmdItem.FindControl("cboItemIDJO") as RadComboBox).SelectedValue);
                if (detail != null) return;

                var reg = new Registration();
                reg.LoadByPrimaryKey(RegistrationNo);

                var grr = new Guarantor();
                grr.LoadByPrimaryKey(reg.GuarantorID);

                var tariffDate = grr.TariffCalculationMethod == 1
                    ? reg.RegistrationDate.Value.Date
                    : (new DateTime()).NowAtSqlServer().Date;

                detail = TransChargesItems.AddNew();

                detail.TransactionNo = string.Empty;

                var lastItem = (TransChargesItems.OrderByDescending(j => j.SequenceNo)).Take(1).SingleOrDefault();
                detail.SequenceNo = (lastItem == null || string.IsNullOrEmpty(lastItem.SequenceNo)) ? "001" : string.Format("{0:000}", int.Parse(lastItem.SequenceNo) + 1);

                detail.ParentNo = string.Empty;
                detail.ReferenceNo = string.Empty;
                detail.ReferenceSequenceNo = string.Empty;
                detail.ItemID = (cmdItem.FindControl("cboItemIDJO") as RadComboBox).SelectedValue;
                detail.ItemName = (cmdItem.FindControl("cboItemIDJO") as RadComboBox).Text;
                detail.ChargeClassID = reg.ChargeClassID;
                detail.ParamedicID = ParamedicID;
                detail.TariffDate = tariffDate;

                var tariff = new ItemTariff();
                if (!tariff.Load(GetItemTariffQuery(tariffDate, grr.SRTariffType, reg.ChargeClassID, detail.ItemID)))
                    if (!tariff.Load(GetItemTariffQuery(tariffDate, AppSession.Parameter.DefaultTariffType, reg.ChargeClassID, detail.ItemID)))
                        if (!tariff.Load(GetItemTariffQuery(tariffDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, detail.ItemID)))
                            tariff.Load(GetItemTariffQuery(tariffDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, detail.ItemID));

                detail.IsAdminCalculation = tariff.IsAdminCalculation;
                detail.IsVariable = false;
                detail.IsCito = false;
                detail.ChargeQuantity = 1;
                detail.StockQuantity = 0;
                detail.SRItemUnit = "X";
                detail.CostPrice = 0;
                detail.Price = tariff.Price;
                detail.CitoAmount = !(detail.IsCito ?? false) ? 0 : ((!tariff.IsCitoInPercent ?? false) ? tariff.CitoValue : (tariff.CitoValue / 100) * detail.Price);
                detail.RoundingAmount = Helper.RoundingDiff;
                detail.SRDiscountReason = string.Empty;
                detail.IsAssetUtilization = false;
                detail.AssetID = string.Empty;
                detail.IsBillProceed = false;
                detail.IsOrderRealization = false;
                detail.IsPackage = false;
                detail.IsVoid = false;
                detail.Notes = string.Empty;
                detail.IsItemTypeService = true;
                detail.SRCenterID = string.Empty;
                detail.IsApprove = false;

                //item lab detail khusus rs pake lis offile avicenna
                var item = new Item();
                item.LoadByPrimaryKey(detail.ItemID);
                if (item.SRItemType == ItemType.Laboratory)
                {
                    var labs = new ItemLaboratoryProfileCollection();
                    labs.Query.Where(labs.Query.ParentItemID == detail.ItemID);
                    labs.Query.OrderBy(labs.Query.DisplaySequence.Ascending);
                    if (labs.Query.Load())
                    {
                        var createdDateTime = (new DateTime()).NowAtSqlServer();
                        int i = 0;
                        foreach (var lab in labs)
                        {
                            i += 1;
                            var entityLab = TransChargesItems.AddNew();
                            entityLab.TransactionNo = TransactionNo;

                            var sequenceNo = (TransChargesItems.OrderByDescending(c => c.SequenceNo).Select(c => c.SequenceNo)).Take(1).SingleOrDefault();
                            if (i == 1)
                                entityLab.SequenceNo = detail.SequenceNo + string.Format("{0:000}", int.Parse(sequenceNo));
                            else
                                entityLab.SequenceNo = string.Format("{0:000000}", int.Parse(sequenceNo) + 1);
                            entityLab.ParentNo = detail.SequenceNo;
                            entityLab.ReferenceNo = string.Empty;
                            entityLab.ReferenceSequenceNo = string.Empty;
                            entityLab.ItemID = lab.DetailItemID;
                            entityLab.ItemName = string.Empty;
                            entityLab.ChargeClassID = reg.ChargeClassID;
                            entityLab.ParamedicID = ParamedicID;
                            //entityLab.ParamedicName = ParamedicName;
                            entityLab.IsAdminCalculation = false;
                            entityLab.IsVariable = false;
                            entityLab.IsCito = false;
                            entityLab.ChargeQuantity = 0;
                            entityLab.StockQuantity = 0;
                            entityLab.SRItemUnit = "X";
                            entityLab.CostPrice = 0;
                            entityLab.Price = 0;
                            entityLab.CitoAmount = 0;
                            entityLab.IsCitoInPercent = false;
                            entityLab.BasicCitoAmount = 0;
                            entityLab.RoundingAmount = 0;
                            entityLab.SRDiscountReason = string.Empty;
                            entityLab.IsAssetUtilization = false;
                            entityLab.AssetID = string.Empty;
                            entityLab.IsBillProceed = false;
                            entityLab.IsOrderRealization = false;
                            entityLab.IsPackage = false;
                            entityLab.IsVoid = false;
                            entityLab.Notes = string.Empty;
                            entityLab.IsItemTypeService = false;
                            entityLab.TariffDate = tariffDate;
                            entityLab.SRCenterID = string.Empty;
                            entityLab.IsApprove = false;
                            entityLab.IsItemRoom = false;

                            entityLab.CreatedByUserID = AppSession.UserLogin.UserID;
                            entityLab.CreatedDateTime = createdDateTime;
                        }
                    }
                }

                cboServiceUnitIDJO.Enabled = !TransChargesItems.Any();
                grdTransChargesItem.Rebind();
            }
        }

        private void Save()
        {
            if (!TransChargesItems.Any()) return;

            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);

            TransCharges header;
            AppAutoNumberLast autoNumber = null;

            if (string.IsNullOrEmpty(TransactionNo))
            {
                autoNumber = Helper.GetNewAutoNumber(txtTransactionDateJO.SelectedDate.Value.Date, AppEnum.AutoNumber.JobOrderNo);
                header = new TransCharges
                {
                    TransactionNo = autoNumber.LastCompleteNumber,
                    RegistrationNo = RegistrationNo,
                    TransactionDate = txtTransactionDateJO.SelectedDate,
                    ExecutionDate = txtTransactionDateJO.SelectedDate,
                    ReferenceNo = string.Empty,
                    ResponUnitID = string.Empty,
                    FromServiceUnitID = reg.ServiceUnitID,
                    ToServiceUnitID = cboServiceUnitIDJO.SelectedValue,
                    LocationID = (new ServiceUnit()).GetMainLocationId(cboServiceUnitIDJO.SelectedValue),
                    SRTypeResult = string.Empty,
                    ClassID = reg.ChargeClassID,
                    RoomID = reg.RoomID,
                    BedID = reg.BedID,
                    DueDate = txtTransactionDateJO.SelectedDate,
                    SRShift = Registration.GetShiftID(),
                    SRItemType = string.Empty,
                    IsProceed = true,
                    IsApproved = true,
                    IsVoid = false,
                    IsAutoBillTransaction = false,
                    IsBillProceed = false,
                    IsOrder = true,
                    IsCorrection = false,
                    Notes = txtNotesJO.Text,
                    PhysicianSenders = AppSession.UserLogin.ParamedicName,
                    LastUpdateByUserID = AppSession.UserLogin.UserID,
                    LastUpdateDateTime = DateTime.Now,
                    CreatedByUserID = AppSession.UserLogin.UserID,
                    CreatedDateTime = DateTime.Now
                };
            }
            else
            {
                header = new TransCharges();
                header.LoadByPrimaryKey(TransactionNo);
                header.TransactionDate = txtTransactionDateJO.SelectedDate;
                header.ToServiceUnitID = cboServiceUnitIDJO.SelectedValue;
                header.LocationID = (new ServiceUnit()).GetMainLocationId(cboServiceUnitIDJO.SelectedValue);
                header.Notes = txtNotesJO.Text;
            }

            var planning = string.Format(" || {0} {1:dd MMM yy}:", cboServiceUnitIDJO.Text, txtTransactionDateJO.SelectedDate);
            foreach (var detail in TransChargesItems)
            {
                planning += string.Format("{0}, )", detail.ItemName);

                detail.TransactionNo = string.IsNullOrEmpty(TransactionNo) ? header.TransactionNo : TransactionNo;
            }

            // Simpan ke planning pada SOAP
            var rim = new RegistrationInfoMedicCollection();
            rim.Query.Where(
                rim.Query.RegistrationNo == RegistrationNo &&
                rim.Query.SRMedicalNotesInputType == "SOAP" &&
                rim.Query.LastUpdateByUserID == AppSession.UserLogin.UserID
                );
            rim.Query.Load();

            foreach (var row in rim)
            {
                row.Info4 += planning;
            }

            if (TransChargesItemConsumptions.Count > 0)
            {
                foreach (var cons in TransChargesItemConsumptions)
                {
                    cons.TransactionNo = header.TransactionNo;
                    cons.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    cons.LastUpdateDateTime = DateTime.Now;
                }
            }

            using (var trans = new esTransactionScope())
            {
                if (string.IsNullOrEmpty(TransactionNo))
                {
                    autoNumber.Save();
                }
                header.Save();
                TransChargesItems.Save();
                TransChargesItemConsumptions.Save();
                rim.Save();

                trans.Complete();
            }
        }
        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            //job order
            if (sourceControl is RadGrid && eventArgument == "rebind")
            {

                if ((sourceControl as RadGrid).UniqueID == grdTransChargesItem.UniqueID)
                {
                    cboServiceUnitIDJO.Enabled = !TransChargesItems.Any();
                    grdTransChargesItem.Rebind();
                }
            }
        }

        protected void grdTransChargesItem_ItemCreated(object sender, GridItemEventArgs e)
        {
            grdTransChargesItem_OnItemCreated(TransChargesItems, sender, e);
        }
        public static void grdTransChargesItem_OnItemCreated(TransChargesItemCollection jobOrders,
            object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                if (jobOrders.Count < e.Item.DataSetIndex)
                {
                    var item = jobOrders[e.Item.DataSetIndex];
                    if (item != null)
                    {
                        if (item.IsVoid ?? false)
                        {
                            for (var i = 0; i < e.Item.Cells.Count; i++)
                            {
                                if (i > 0 && i < e.Item.Cells.Count)
                                    e.Item.Cells[i].Font.Strikeout = true;

                            }
                        }
                    }
                }
            }
        }

    }
}
