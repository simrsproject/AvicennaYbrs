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
using System.Data.SqlTypes;
using System.Text;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Module.RADT.PharmaceuticalCare.DrugObservation;

namespace Temiang.Avicenna.Module.RADT.PharmaceuticalCare
{
    public partial class DrugObservationEntry : BasePageDialogEntry
    {
        public override string RegistrationNo => Request.QueryString["regno"];
        public override string PatientID => Request.QueryString["patid"];

        private RegistrationDrugObs _current;
        private RegistrationDrugObs RegistrationDrugObsCurrent
        {
            get
            {
                if (_current == null)
                {
                    var ent = new RegistrationDrugObs();
                    if (!IsPostBack)
                        ent.LoadByPrimaryKey(RegistrationNo, Request.QueryString["sn"].ToInt());
                    else
                        ent.LoadByPrimaryKey(RegistrationNo, txtDrugObsNo.Text.ToInt());

                    _current = ent;
                }

                return _current;
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (AppConstant.Program.PharmaceuticalCare.Equals(Request.QueryString["prgid"]))
                ProgramID = AppConstant.Program.PharmaceuticalCare;
            else
                ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            // Program Fiture
            //IsSingleRecordMode = true; //Save then close

            IsMedicalRecordEntry = true; //Activate deadline edit & add
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = true;

            ToolBar.EditVisible = true;
            ToolBar.AddVisible = false;
            // -------------------

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Inpatient Pharmacy Observation of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }

        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var ent = RegistrationDrugObsCurrent;
            txtDrugObsNo.Text = string.Format("{0:00000}", ent.DrugObsNo);
            txtDrugObsDateTime.SelectedDate = ent.DrugObsDateTime;

            optIsNeedPto.SelectedValue = (ent.IsNeedPto ?? false) ? "1" : "0";
            txtDrugInteractionRisk.Text = ent.DrugInteractionRisk;
            txtRecommendation.Text = ent.Recommendation;

            grdDrps.Rebind();
            grdPto.Rebind();

            Session.Remove("ditems");
            grdItem.Rebind();
        }
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            var isEdited = newVal != AppEnum.DataMode.Read;
        }
        protected override void OnMenuNewClick()
        {
            txtDrugObsNo.Text = string.Format("{0:00000}", NewDrugObsNo());
            var timeNow = (new DateTime()).NowAtSqlServer();
            txtDrugObsDateTime.SelectedDate = timeNow;
            grdDrps.Rebind();
            grdPto.Rebind();

            Session.Remove("ditems");
            grdItem.Rebind();
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Save(args, true);
        }

        private bool Save(ValidateArgs args, bool isNewRecord)
        {
            using (var trans = new esTransactionScope())
            {
                var ent = new RegistrationDrugObs();
                if (isNewRecord || !ent.LoadByPrimaryKey(RegistrationNo, txtDrugObsNo.Text.ToInt()))
                {
                    ent.RegistrationNo = RegistrationNo;
                    ent.DrugObsNo = NewDrugObsNo();

                    // Save Unit History krn di reg di tgl berikutnya bisa berubah
                    SetServiceUnitHistory(ent, isNewRecord);
                }
                ent.IsNeedPto = (optIsNeedPto.SelectedValue == "1");
                ent.DrugObsDateTime = txtDrugObsDateTime.SelectedDate;
                ent.DrugInteractionRisk = txtDrugInteractionRisk.Text;
                ent.Recommendation = txtRecommendation.Text;
                ent.Save();

                // RegistrationDrugObsDrps
                var drpsCol = new RegistrationDrugObsDrpsCollection();
                if (DataModeCurrent == AppEnum.DataMode.Edit)
                {
                    drpsCol.Query.Where(drpsCol.Query.RegistrationNo == RegistrationNo, drpsCol.Query.DrugObsNo == txtDrugObsNo.Text.ToInt());
                    drpsCol.LoadAll();
                    drpsCol.MarkAllAsDeleted();
                    drpsCol.Save();
                }

                drpsCol = new RegistrationDrugObsDrpsCollection();
                foreach (GridDataItem item in grdDrps.MasterTableView.Items)
                {
                    var optYesNo = ((RadRadioButtonList)item.FindControl("optYesNo"));
                    var entLine = drpsCol.AddNew();
                    entLine.RegistrationNo = RegistrationNo;
                    entLine.DrugObsNo = txtDrugObsNo.Text.ToInt();
                    entLine.SRDrps = item.GetDataKeyValue("ItemID").ToString();
                    if (string.IsNullOrEmpty(optYesNo.SelectedValue))
                        entLine.str.IsYes = string.Empty;
                    else
                        entLine.IsYes = optYesNo.SelectedValue == "1";
                }

                drpsCol.Save();



                // RegistrationDrugObsPto
                var ptoCol = new RegistrationDrugObsPtoCollection();
                if (DataModeCurrent == AppEnum.DataMode.Edit)
                {
                    ptoCol.Query.Where(ptoCol.Query.RegistrationNo == RegistrationNo, ptoCol.Query.DrugObsNo == txtDrugObsNo.Text.ToInt());
                    ptoCol.LoadAll();
                    ptoCol.MarkAllAsDeleted();
                    ptoCol.Save();
                }

                ptoCol = new RegistrationDrugObsPtoCollection();
                foreach (GridDataItem item in grdPto.MasterTableView.Items)
                {

                    var entLine = ptoCol.AddNew();
                    entLine.RegistrationNo = RegistrationNo;
                    entLine.DrugObsNo = txtDrugObsNo.Text.ToInt();
                    entLine.SRPto = item.GetDataKeyValue("ItemID").ToString();

                    var optYesNo = ((RadRadioButtonList)item.FindControl("optYesNo"));
                    if (string.IsNullOrEmpty(optYesNo.SelectedValue))
                        entLine.str.IsYes = string.Empty;
                    else
                        entLine.IsYes = optYesNo.SelectedValue == "1";

                    var txtYesNotes = item.FindControl("txtYesNotes");
                    if (txtYesNotes is RadTextBox)
                    {
                        entLine.YesNotes = ((RadTextBox)txtYesNotes).Text;
                    }

                    var chkList = item.FindControl("chkList");
                    if (chkList is RadCheckBoxList)
                    {
                        var selectedValues = ((RadCheckBoxList)chkList).SelectedValues;
                        if (selectedValues != null)
                        {
                            entLine.IsDrugDuplicate = selectedValues.Contains("DO;");
                            entLine.IsMoreThan7Days = selectedValues.Contains("7D;");
                            entLine.IsAgeMoreThan65y = selectedValues.Contains("AG65;");
                            entLine.IsSindromGeriatry = selectedValues.Contains("SG;");
                        }
                    }
                }
                ptoCol.Save();


                // RegistrationDrugObsItem
                var items = new RegistrationDrugObsItemCollection();
                items.Query.Where(items.Query.RegistrationNo == RegistrationNo, items.Query.DrugObsNo == txtDrugObsNo.Text.ToInt());
                items.LoadAll();
                items.MarkAllAsDeleted();

                foreach (GridDataItem item in grdItem.MasterTableView.Items)
                {
                    var mrno = item.GetDataKeyValue("MedicationReceiveNo").ToInt();
                    var drugItem = items.AddNew();

                    drugItem.RegistrationNo = RegistrationNo;
                    drugItem.DrugObsNo = txtDrugObsNo.Text.ToInt();
                    drugItem.MedicationReceiveNo = mrno;
                    drugItem.FollowUp = ((RadTextBox)item.FindControl("txtFollowUp")).Text;
                    drugItem.Notes = ((RadTextBox)item.FindControl("txtNotes")).Text;

                    //var mre = new MedicationReceive();
                    //mre.LoadByPrimaryKey(mrno);

                    //// Utk keperluan dicetakan
                    //items.DrugName = DrugName(mre.RefTransactionNo, mre.RefSequenceNo, mre.ItemDescription);
                }

                items.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            grdDrps.Rebind();
            grdPto.Rebind();
            return true;
        }

        #region drugName
        private string DrugName(string presNo, string seqNo, string itemdesc)
        {

            if (!string.IsNullOrEmpty(presNo))
            {
                var prescItem = new TransPrescriptionItem();
                if (prescItem.LoadByPrimaryKey(presNo, seqNo))
                {
                    if (prescItem.IsCompound ?? false)
                    {
                        return PrescriptionDrugNameCompound(prescItem);
                    }
                    else
                    {
                        return string.Format("{0} {1}", ItemName(prescItem), prescItem.Notes);
                    }
                }
            }

            return itemdesc;
        }

        private string PrescriptionDrugNameCompound(TransPrescriptionItem prescItem)
        {
            // Obat Racikan
            var sbItem = new StringBuilder();

            var emb = new Embalace();
            emb.LoadByPrimaryKey(prescItem.EmbalaceID);
            sbItem.AppendFormat("<span>{0} {1}</span>",
                ItemName(prescItem), prescItem.Notes);

            // Detil racikan
            var coll = new TransPrescriptionItemCollection();
            coll.Query.Where(coll.Query.PrescriptionNo == prescItem.PrescriptionNo, coll.Query.ParentNo == prescItem.SequenceNo);
            coll.Query.OrderBy(coll.Query.SequenceNo.Ascending);
            coll.LoadAll();

            foreach (var entChild in coll)
            {
                sbItem.AppendFormat("<br/><span style=\"padding-left:10px;\">• {0}</span>", ItemName(entChild));
            }
            return sbItem.ToString();
        }
        private static string ItemName(TransPrescriptionItem prescItem)
        {
            var item = new Item();
            string itemID;
            if (prescItem.ItemInterventionID != null && !string.IsNullOrEmpty(prescItem.ItemInterventionID.ToString()) && !prescItem.ItemID.Equals(prescItem.ItemInterventionID))
            {
                itemID = prescItem.ItemInterventionID;
            }
            else
            {
                itemID = prescItem.ItemID;
            }

            item.LoadByPrimaryKey(itemID);
            return item.ItemName;
        }

        #endregion

        private void SetServiceUnitHistory(RegistrationDrugObs ent, bool isNewRecord)
        {
            // Set history Service Unit
            var isNotSet = true;
            if (!isNewRecord)
            {
                var pt = PatientTransfer.LoadLastTransfer(RegistrationNo, txtDrugObsDateTime.SelectedDate.Value);
                if (pt != null)
                {
                    ent.ServiceUnitID = pt.ToServiceUnitID;
                    isNotSet = false;
                }
            }

            if (isNotSet)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(RegistrationNo);
                ent.ServiceUnitID = reg.ServiceUnitID;
            }

        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Save(args, false);
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
            grdDrps.Rebind();
            grdPto.Rebind();

            Session.Remove("ditems");
            grdItem.Rebind();
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
            var ent = RegistrationDrugObsCurrent;
            ent.IsDeleted = true;
            ent.Save();
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
            // Bisa klik tombol Edit
            return (RegistrationDrugObsCurrent.IsDeleted ?? false) == false && RegistrationDrugObsCurrent.CreatedByUserID == AppSession.UserLogin.UserID;
        }

        public override bool OnGetStatusMenuDelete()
        {
            // Bisa klik tombol Delete
            return (RegistrationDrugObsCurrent.IsDeleted ?? false) == false && RegistrationDrugObsCurrent.CreatedByUserID == AppSession.UserLogin.UserID;
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

        private int NewDrugObsNo()
        {
            var qr = new RegistrationDrugObsQuery("a");
            var fb = new RegistrationDrugObs();
            qr.Where(qr.RegistrationNo == RegistrationNo);
            qr.es.Top = 1;
            qr.OrderBy(qr.DrugObsNo.Descending);

            if (fb.Load(qr))
            {
                return fb.DrugObsNo.ToInt() + 1;
            }
            return 1;
        }

        #region DRPs
        protected void grdDrps_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var grd = ((RadGrid)sender);
            grd.DataSource = DrpsDataTable(RegistrationNo, txtDrugObsNo.Text.ToInt());
        }

        private DataTable DrpsDataTable(string registrationNo, int drugObsNo)
        {
            var que = new AppStandardReferenceItemQuery("sri");
            var qrLine = new RegistrationDrugObsDrpsQuery("a");

            que.LeftJoin(qrLine)
            .On(que.ItemID == qrLine.SRDrps && qrLine.RegistrationNo == registrationNo && qrLine.DrugObsNo == drugObsNo);

            que.Where(que.StandardReferenceID == "DRPS");
            que.OrderBy(que.LineNumber.Ascending);
            que.Select(que.ItemID, que.ItemName, qrLine.IsYes);
            return que.LoadDataTable();
        }

        protected void grdDrps_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;
                var optYesNo = ((RadRadioButtonList)dataItem.FindControl("optYesNo"));
                var isYes = dataItem["IsYes"].Text;
                if (isYes != "&nbsp;")
                    optYesNo.SelectedValue = isYes.Equals("True") ? "1" : "0";
            }
        }
        #endregion

        #region PTO
        protected void grdPto_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var grd = ((RadGrid)sender);
            grd.DataSource = PtoDataTable(RegistrationNo, txtDrugObsNo.Text.ToInt());
        }

        private DataTable PtoDataTable(string registrationNo, int drugObsNo)
        {
            var que = new AppStandardReferenceItemQuery("sri");
            var qrLine = new RegistrationDrugObsPtoQuery("a");

            que.LeftJoin(qrLine)
            .On(que.ItemID == qrLine.SRPto && qrLine.RegistrationNo == registrationNo && qrLine.DrugObsNo == drugObsNo);

            que.Where(que.StandardReferenceID == "PTO");
            que.OrderBy(que.LineNumber.Ascending);
            que.Select(que.ItemID, que.ItemName, que.ReferenceID, qrLine);
            return que.LoadDataTable();
        }

        protected void grdPto_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var emptyVal = "&nbsp;";
                var dataItem = (GridDataItem)e.Item;
                var controlRef = dataItem["ReferenceID"].Text; // YN;7D;DO;AG65;SG;

                var optYesNo = ((RadRadioButtonList)dataItem.FindControl("optYesNo"));
                var isYes = dataItem["IsYes"].Text;
                if (isYes != emptyVal)
                    optYesNo.SelectedValue = isYes.Equals("True") ? "1" : "0";

                if (controlRef.Contains("YTN;")) //Yes Notes
                {
                    var txtYesNotes = ((RadTextBox)dataItem.FindControl("txtYesNotes"));
                    txtYesNotes.Text = emptyVal.Equals(dataItem["YesNotes"].Text) ? String.Empty : dataItem["YesNotes"].Text;
                }
                else
                {
                    ((RadTextBox)dataItem.FindControl("txtYesNotes")).Visible = false;
                }

                if (controlRef.Contains("7D;") || controlRef.Contains("DO;") || controlRef.Contains("AG65;") || controlRef.Contains("SG;"))
                {
                    var chkList = (RadCheckBoxList)dataItem.FindControl("chkList");
                    if (controlRef.Contains("7D;"))
                    {
                        var item = new ButtonListItem(">= 7 sediaan", "7D;");
                        item.Selected = dataItem["IsMoreThan7Days"].Text.Equals("True");
                        chkList.Items.Add(item);
                    }
                    if (controlRef.Contains("DO;"))
                    {
                        var item = new ButtonListItem("Duplikasi Obat", "DO;");
                        item.Selected = dataItem["IsDrugDuplicate"].Text.Equals("True");
                        chkList.Items.Add(item);
                    }
                    if (controlRef.Contains("AG65;"))
                    {
                        var item = new ButtonListItem("Usia >= 65 tahun", "AG65;");
                        item.Selected = dataItem["IsAgeMoreThan65y"].Text.Equals("True");
                        chkList.Items.Add(item);
                    }
                    if (controlRef.Contains("SG;"))
                    {
                        var item = new ButtonListItem("Sind. Geriatri", "SG;");
                        item.Selected = dataItem["IsSindromGeriatry"].Text.Equals("True");
                        chkList.Items.Add(item);
                    }
                }
                else
                    ((RadCheckBoxList)dataItem.FindControl("chkList")).Visible = false;

            }
        }
        #endregion


        #region Item
        protected void grdItem_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var grd = ((RadGrid)sender);
            grd.DataSource = ItemDataTable(RegistrationNo, txtDrugObsNo.Text.ToInt());
        }

        private DataTable ItemDataTable(string registrationNo, int drugObsNo)
        {
            if (IsPostBack)
            {
                object obj = Session["ditems"];
                if (obj != null)
                {
                    // Check result from Picker
                    if (Session["diSelecteds"] != null)
                    {
                        var drugItemSelecteds = (IList<DrugItem>)Session["diSelecteds"];

                        // Add
                        var ditems = (DataTable)Session["ditems"];
                        foreach (var item in drugItemSelecteds)
                        {
                            if (ditems.Rows.Find(item.MedicationReceiveNo) != null) continue;

                            var newRow = ditems.NewRow();
                            newRow["MedicationReceiveNo"] = item.MedicationReceiveNo;
                            newRow["ItemDescription"] = item.ItemDescription;
                            newRow["ConsumeMethod"] = item.ConsumeMethod;

                            ditems.Rows.Add(newRow);
                        }

                        Session.Remove("diSelecteds");
                    }

                    return (DataTable)obj;
                }
            }

            var qr = new RegistrationDrugObsItemQuery("a");
            var qrMr = new MedicationReceiveQuery("b");
            qr.InnerJoin(qrMr).On(qr.MedicationReceiveNo == qrMr.MedicationReceiveNo);
            qr.Where(qr.RegistrationNo == registrationNo, qr.DrugObsNo == drugObsNo);

            qr.OrderBy(qrMr.ItemDescription.Ascending);
            qr.Select(qr.MedicationReceiveNo, qr.FollowUp, qr.Notes, qrMr.ItemDescription, qrMr.RefTransactionNo, qrMr.RefSequenceNo, qrMr.SRConsumeMethod, qrMr.ConsumeQty, qrMr.SRConsumeUnit);

            var dtb = qr.LoadDataTable();
            dtb.Columns.Add(new DataColumn("ConsumeMethod", typeof(string)));
            dtb.PrimaryKey = new DataColumn[] { dtb.Columns["MedicationReceiveNo"] };

            foreach (DataRow row in dtb.Rows)
            {
                var itemDesc = row["ItemDescription"].ToString();
                if (row["RefTransactionNo"] != DBNull.Value && row["RefSequenceNo"] != DBNull.Value)
                    itemDesc = MedicationReceive.PrescriptionItemDescription(row["RefTransactionNo"].ToString(), row["RefSequenceNo"].ToString(), row["ItemDescription"].ToString(), false, false);

                row["ItemDescription"] = itemDesc;

                var cons = new ConsumeMethod();
                cons.LoadByPrimaryKey(row["SRConsumeMethod"].ToString());
                row["ConsumeMethod"] = string.Format("{0} @{1} {2}", cons.SRConsumeMethodName, row["ConsumeQty"], row["SRConsumeUnit"]);
            }

            Session["ditems"] = dtb;
            return dtb;
        }


        protected void grdItem_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var recNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["MedicationReceiveNo"]).ToInt();

            var ditems = (DataTable)Session["ditems"];
            var row = ditems.Rows.Find(recNo);
            if (row != null)
            {
                row.Delete();
                ((RadGrid)sender).Rebind();
            }
        }
        #endregion

    }
}
