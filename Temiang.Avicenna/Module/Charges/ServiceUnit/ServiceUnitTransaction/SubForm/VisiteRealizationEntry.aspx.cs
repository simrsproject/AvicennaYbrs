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
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Charges.ServiceUnitTransaction
{
    public partial class VisiteRealizationEntry : BasePageDialogHistEntry
    {
        private string RegistrationNo
        {
            get { return Request.QueryString["regno"]; }
        }
        private string PatientID
        {
            get { return Request.QueryString["patid"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            Splitter.Orientation = Orientation.Horizontal;
            PaneEntry.Height = new Unit(410, UnitType.Pixel);
            //ToolBar.NavigationVisible = false;
            //ToolBar.ApprovalUnApprovalVisible = false;
            //ToolBar.VoidUnVoidVisible = false;
            //ToolBar.PrintVisible = false;
            //ToolBar.AddVisible = false;
            IsSingleRecordMode = true;

            ProgramID = AppConstant.Program.ServiceUnitTransaction;
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            this.Page.Title = "Patient Visite Order Realization";
        }
        #region override method
        public override void OnServerValidate(ValidateArgs args)
        {
            var selecteds = grdVisite.SelectedItems;
            if (selecteds.Count == 0)
            {
                args.IsCancel = true;
                args.MessageText = "Please select Visite Item for realization first";
            }
        }
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }
        protected override void OnMenuNewClick()
        {
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            // Save
            using (var trans = new esTransactionScope())
            {
                SaveRealizationAndServiceUnitCorrection();
                //Commit if success, Rollback if failed
                trans.Complete();
            }


        }

        private void SaveRealizationAndServiceUnitCorrection()
        {
            var realizations = grdVisite.SelectedItems;

            foreach (GridItem item in realizations)
            {
                var editableItem = ((GridEditableItem)item);
                var trNo = Convert.ToString(editableItem.GetDataKeyValue("TransactionNo"));
                var itemID = Convert.ToString(editableItem.GetDataKeyValue("ItemID"));

                // Update
                var visite = new TransChargesVisiteItem();
                if (visite.LoadByPrimaryKey(trNo, itemID))
                {
                    visite.RealizationQty = visite.RealizationQty + 1;
                    if (visite.RealizationQty >= visite.VisiteQty)
                    {
                        visite.IsClosed=true;
                    }
                    visite.Save();
                }

                // Add
                var realization = new TransChargesVisiteItemRealization();
                realization.TransactionNo = trNo;
                realization.ItemID = itemID;
                realization.RegistrationNo = RegistrationNo;
                realization.Save();
            }


            // Service Unit Correction
            var corrections = grdTransCharges.SelectedItems;
            var refNo = string.Empty;
            var suID = string.Empty;
            var seqNo = string.Empty;
            var tcItems = new TransChargesItemCollection();
            var tcItemComps = new TransChargesItemCompCollection();
            var tcItemConsumptions = new TransChargesItemConsumptionCollection();
            foreach (GridItem item in corrections)
            {
                var editableItem = ((GridEditableItem)item);
                if (refNo != Convert.ToString(editableItem.GetDataKeyValue("TransactionNo")))
                {
                    if (!string.IsNullOrEmpty(refNo))
                    {
                        //Save new SU Trans Correction
                        CreateServiceUnitCorrectionEntry(refNo, suID, tcItems, tcItemComps, tcItemConsumptions);
                    }

                    // Prepare new SU Transaction Correction
                    refNo = Convert.ToString(editableItem.GetDataKeyValue("TransactionNo"));
                    suID = Convert.ToString(editableItem.GetDataKeyValue("FromServiceUnitID"));
                    seqNo = Convert.ToString(editableItem.GetDataKeyValue("SequenceNo"));
                    tcItems = new TransChargesItemCollection();
                    tcItemComps = new TransChargesItemCompCollection();
                    tcItemConsumptions = new TransChargesItemConsumptionCollection();
                }

                var tcItem = new TransChargesItem();
                tcItem.LoadByPrimaryKey(refNo, seqNo);
                tcItem.MarkAllColumnsAsDirty(DataRowState.Added);

                #region item component
                var components = new TransChargesItemCompCollection();
                components.Query.Where(
                        components.Query.TransactionNo == refNo,
                        components.Query.SequenceNo == seqNo
                    );
                components.Query.OrderBy(components.Query.TariffComponentID.Ascending);
                components.LoadAll();

                foreach (var c in components)
                {
                    c.MarkAllColumnsAsDirty(DataRowState.Added);
                    tcItemComps.AttachEntity(c);
                }
                #endregion

                #region detail
                tcItem.ReferenceNo = tcItem.TransactionNo;
                tcItem.ReferenceSequenceNo = tcItem.SequenceNo;
                tcItem.StockQuantity = 0 - tcItem.StockQuantity;

                tcItem.DiscountAmount = tcItem.DiscountAmount;
                tcItem.ChargeQuantity = 0 - tcItem.ChargeQuantity;
                tcItem.IsBillProceed = false;
                tcItem.IsApprove = false;
                tcItem.IsVoid = false;

                tcItems.AttachEntity(tcItem);
                #endregion

                #region item consumption
                var consumptions = new TransChargesItemConsumptionCollection();
                consumptions.Query.Where(
                        consumptions.Query.TransactionNo == refNo,
                        consumptions.Query.SequenceNo == seqNo
                    );
                consumptions.Query.OrderBy(consumptions.Query.DetailItemID.Ascending);
                consumptions.LoadAll();

                foreach (var c in consumptions)
                {
                    c.MarkAllColumnsAsDirty(DataRowState.Added);

                    c.Qty = 0 - c.Qty;
                    c.QtyRealization = 0 - c.QtyRealization;

                    tcItemConsumptions.AttachEntity(c);
                }
                #endregion
            }

            if (!string.IsNullOrEmpty(refNo))
            {
                //Save new SU Trans Correction
                CreateServiceUnitCorrectionEntry(refNo, suID, tcItems, tcItemComps, tcItemConsumptions);
            }
        }

        #region Service Unit Correction Save
        private string CreateServiceUnitCorrectionEntry(string referenceNo, string fromServiceUnitID
            , TransChargesItemCollection transChargesItems
            , TransChargesItemCompCollection transChargesItemComps
            , TransChargesItemConsumptionCollection transChargesItemConsumptions)
        {
            var dateNow = (new DateTime()).NowAtSqlServer();
            var date = dateNow.Date;
            var autoNumber = Helper.GetNewAutoNumber(date, AppEnum.AutoNumber.TransactionNo);

            var entity = new TransCharges();
            entity.TransactionNo = autoNumber.LastCompleteNumber;
            // save autonumber immediately to decrease time gap between create and save
            autoNumber.Save();
            entity.RegistrationNo = RegistrationNo;
            entity.TransactionDate = date;
            entity.ReferenceNo = referenceNo;
            entity.FromServiceUnitID = fromServiceUnitID;

            var tr = new TransCharges();
            tr.LoadByPrimaryKey(referenceNo);
            entity.ToServiceUnitID = tr.ToServiceUnitID;
            entity.LocationID = tr.LocationID;// <-- mengikuti lokasi asal transaksi

            entity.ClassID = tr.ClassID; // <-- mengikuti kelas asal transaksi
            entity.RoomID = tr.RoomID; // <-- mengikuti room asal transaksi
            entity.BedID = tr.BedID; // <-- mengikuti bed asal transaksi


            entity.ReferenceNo = referenceNo;
            entity.DueDate = date;
            entity.SRShift = string.Empty;
            entity.SRItemType = string.Empty;
            entity.IsProceed = false;
            entity.IsApproved = false;
            entity.IsVoid = false;
            entity.IsAutoBillTransaction = tr.IsAutoBillTransaction ?? false;
            entity.IsBillProceed = false;
            entity.IsOrder = false;
            entity.IsCorrection = true;
            entity.Notes = string.Empty;

            //Last Update Status Detail
            foreach (var item in transChargesItems)
            {
                item.TransactionNo = entity.TransactionNo;
                item.IsCorrection = true;
                item.LastUpdateDateTime = dateNow;
                item.CreatedDateTime = dateNow;
            }

            foreach (var comp in transChargesItemComps)
            {
                comp.TransactionNo = entity.TransactionNo;
                comp.LastUpdateDateTime = dateNow;
            }

            foreach (var cons in transChargesItemConsumptions)
            {
                cons.TransactionNo = entity.TransactionNo;
                cons.LastUpdateDateTime = dateNow;
            }


            entity.Save();
            transChargesItems.Save();
            transChargesItemComps.Save();
            transChargesItemConsumptions.Save();

            return entity.TransactionNo;

        }
        #endregion

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

        //protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        //protected override void OnMenuMovePrevClick(ValidateArgs args)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        //protected override void OnMenuMoveNextClick(ValidateArgs args)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

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
        public override bool OnGetStatusMenuAdd()
        {
            return true;
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
            ajax.AddAjaxSetting(grdHist, grdHist);
            ajax.AddAjaxSetting(grdHist, grdVisite); // Cancel Realization

            ajax.AddAjaxSetting(grdVisite, grdVisite);
            ajax.AddAjaxSetting(grdVisite, grdHist); // Cancel Visite Order

            ajax.AddAjaxSetting(grdTransCharges, grdTransCharges);
        }
        #endregion

        #region Visite

        protected void grdVisite_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdVisite.DataSource = Visite(true);
        }
        protected void grdVisite_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "CloseVisite")
            {
                var pars = e.CommandArgument.ToString().Split('_');
                var visite = new TransChargesVisiteItem();
                if (visite.LoadByPrimaryKey(pars[0], pars[1]))
                {
                    if (!string.IsNullOrEmpty(visite.Notes))
                        visite.Notes = string.Format("{0}{1}{1}{2} - Manual close by {3}", visite.Notes, Environment.NewLine, DateTime.Now.ToString(AppConstant.DisplayFormat.DateTime), AppSession.UserLogin.UserName);
                    else
                        visite.Notes = string.Format("{0} - Manual close by {1}", DateTime.Now.ToString(AppConstant.DisplayFormat.DateTime), AppSession.UserLogin.UserName);

                    visite.IsClosed = true;
                    visite.Save();

                    grdVisite.Rebind();
                    grdHist.Rebind();
                }
            }
        }
        #endregion

        #region ServiceUnit Transaction
        protected void grdTransCharges_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdTransCharges.DataSource = TransCharges();
        }

        private DataTable TransCharges()
        {
            var query = new TransChargesQuery("a");
            var unit = new ServiceUnitQuery("d");
            var tcItem = new TransChargesItemQuery("e");
            var item = new ItemQuery("f");
            var cc = new CostCalculationQuery("g");

            query.Select
            (
                query.TransactionNo,
                query.ReferenceNo,
                query.TransactionDate,
                unit.ServiceUnitName,
                query.RegistrationNo,
                query.IsAutoBillTransaction,
                query.IsApproved,
                tcItem.IsVoid,
                query.IsCorrection,
                tcItem.IsBillProceed,
                @"<CASE WHEN ISNULL(e.FilmNo, '') = '' THEN f.ItemName ELSE f.ItemName + ' [' + e.FilmNo + ']' END AS ItemName>",
                query.LastUpdateByUserID,
                tcItem.ChargeQuantity,
                tcItem.Price,
                tcItem.ParamedicCollectionName,
                tcItem.CitoAmount,
                tcItem.DiscountAmount,
                query.FromServiceUnitID,
                cc.IntermBillNo,
                tcItem.SequenceNo,
                query.IsOrder,
                query.IsPackage,
                tcItem.ItemID,
                item.SRItemType
            );

            query.InnerJoin(tcItem).On(query.TransactionNo == tcItem.TransactionNo);
            query.InnerJoin(item).On(tcItem.ItemID == item.ItemID);
            query.LeftJoin(cc).On(tcItem.TransactionNo == cc.TransactionNo && tcItem.SequenceNo == cc.SequenceNo);
            query.InnerJoin(unit).On(query.FromServiceUnitID == unit.ServiceUnitID);

            query.Where
                (
                    query.RegistrationNo == RegistrationNo,
                    query.IsOrder == false,
                    query.IsCorrection == false,
                    query.IsApproved == true,
                    query.IsVoid == false,
                    query.Or(
                        tcItem.ParentNo.IsNull(),
                        tcItem.ParentNo == string.Empty
                    )
                );
            query.Where(query.Or(query.PackageReferenceNo.IsNull(), query.PackageReferenceNo == string.Empty));

            // Sub Query
            var tci = new TransChargesItemQuery("h");
            tci.Select(tci.TransactionNo);
            tci.Where(tci.TransactionNo == tcItem.TransactionNo, tci.SequenceNo == tcItem.SequenceNo, tci.IsExtraItem == true,
                          tci.IsSelectedExtraItem == false);
            query.Where(query.TransactionNo.NotIn(tci));


            // Hanya utk item yg tidak dikurangi (Correction Transaction) ...gampangnya dulu dianggap full correction
            var cor = new TransChargesItemQuery("cor");
            var corh = new TransChargesQuery("corh");
            cor.InnerJoin(corh).On(cor.TransactionNo == corh.TransactionNo);
            cor.Select(cor.ItemID);
            cor.Where(corh.RegistrationNo == RegistrationNo,
                    corh.IsOrder == false,
                    cor.IsCorrection == true,
                    corh.IsApproved == true,
                    corh.IsVoid==false,
                    cor.Or(
                        cor.ParentNo.IsNull(),
                        cor.ParentNo == string.Empty
                    ),
                    cor.ItemID == tcItem.ItemID
                    );
            query.Where(tcItem.ItemID.NotIn(cor));

            query.OrderBy(query.TransactionNo.Ascending, tcItem.SequenceNo.Ascending);
            query.es.Distinct = true;

            return query.LoadDataTable();
        }


        #endregion
        #region history
        protected void grdHist_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "CancelRealization")
            {
                var pars = e.CommandArgument.ToString().Split('_');

                using (var tr = new esTransactionScope())
                {
                    var realiz = new TransChargesVisiteItemRealization();
                    if (realiz.LoadByPrimaryKey(pars[0], pars[1], pars[2]))
                    {
                        // Kurangi realization & reopen
                        var visite = new TransChargesVisiteItem();
                        if (visite.LoadByPrimaryKey(pars[0], pars[1]))
                        {
                            // reopen if from automatic close
                            if (visite.RealizationQty == visite.VisiteQty && visite.IsClosed == true)
                                visite.IsClosed = false;

                            visite.RealizationQty = visite.RealizationQty - 1;
                            visite.Save();
                        }

                        // Void Service Unit Correction Entry 
                        // Tidak bisa otomatis krn many to many shg harus dilakukan manual dgn menambah ulang 

                        // Hapus Realization
                        realiz.MarkAsDeleted();
                        realiz.Save();

                        // Commit
                        tr.Complete();

                        grdHist.Rebind();
                        grdVisite.Rebind();
                    }
                }
            }

        }

        protected void grdHist_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdHist.DataSource = Visite(false);
        }

        private DataTable Visite(bool isAvailableOnly)
        {
            var query = new TransChargesVisiteItemQuery("vr");
            var sut = new TransChargesQuery("tc");
            query.InnerJoin(sut).On(query.TransactionNo == sut.TransactionNo);

            var reg = new RegistrationQuery("q");
            query.InnerJoin(reg).On(sut.RegistrationNo == reg.RegistrationNo);

            var item = new ItemQuery("i");
            query.InnerJoin(item).On(query.ItemID == item.ItemID);

            query.Select(reg.RegistrationNo, reg.RegistrationDate, item.ItemName,
                query.IsClosed, query.Notes, query.TransactionNo,
                query.ItemID, query.VisiteQty,
                query.RealizationQty, (query.VisiteQty - query.RealizationQty).As("AvailableQty"));
            query.Where(reg.PatientID == PatientID);
            query.OrderBy(reg.RegistrationNo.Descending);

            if (isAvailableOnly)
            {
                query.Where(query.IsClosed == false, reg.RegistrationNo < RegistrationNo);

                var subqr = new TransChargesVisiteItemRealizationQuery("rz");
                subqr.Select(subqr.TransactionNo);
                subqr.Where(subqr.TransactionNo == query.TransactionNo, subqr.ItemID == query.ItemID);

                query.Where(query.TransactionNo.NotIn(subqr));
            }

            var dtb = query.LoadDataTable();
            return dtb;
        }
        protected void grdHist_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            if (e.DetailTableView.Name != "detail") return;

            var trNo = e.DetailTableView.ParentItem.GetDataKeyValue("TransactionNo").ToString();
            var itemID = e.DetailTableView.ParentItem.GetDataKeyValue("ItemID").ToString();
            e.DetailTableView.DataSource = VisiteDetailHistory(trNo, itemID);

        }

        private DataTable VisiteDetailHistory(string trNo, string itemID)
        {
            var query = new TransChargesVisiteItemRealizationQuery("vr");
            var reg = new RegistrationQuery("q");
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);

            var su = new ServiceUnitQuery("s");
            query.InnerJoin(su).On(reg.ServiceUnitID == su.ServiceUnitID);

            var par = new ParamedicQuery("par");
            query.InnerJoin(par).On(reg.ParamedicID == par.ParamedicID);
            query.Select(query.TransactionNo,query.ItemID, query.RegistrationNo, reg.RegistrationDate, su.ServiceUnitName, par.ParamedicName);
            query.Where(query.TransactionNo == trNo, query.ItemID == itemID);
            var dtb = query.LoadDataTable();
            return dtb;
        }

        #endregion
    }
}
