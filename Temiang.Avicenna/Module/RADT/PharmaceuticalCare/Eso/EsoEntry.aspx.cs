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
    public partial class EsoEntry : BasePageDialogEntry
    {
        public override string RegistrationNo => Request.QueryString["regno"];
        public override string PatientID => Request.QueryString["patid"];

        private RegistrationEso _current;
        private RegistrationEso RegistrationEsoCurrent
        {
            get
            {
                if (_current == null)
                {
                    var ent = new RegistrationEso();
                    if (!IsPostBack)
                        ent.LoadByPrimaryKey(RegistrationNo, Request.QueryString["sn"].ToInt());
                    else
                        ent.LoadByPrimaryKey(RegistrationNo, txtEsoNo.Text.ToInt());

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
                    this.Title = "Drug Side Effects of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }

                // EsoComorbidities
                var coll = new AppStandardReferenceItemCollection();
                coll.Query.Where(
                    coll.Query.StandardReferenceID == AppEnum.StandardReference.EsoComorbid.ToString(),
                    coll.Query.IsActive == true);
                coll.Query.OrderBy(coll.Query.ItemID.Ascending);
                coll.LoadAll();

                foreach (var item in coll)
                {
                    cblEsoComorbidities.Items.Add(new ButtonListItem(item.ItemName, item.ItemID));
                }

                // EsoManifestations
                coll = new AppStandardReferenceItemCollection();
                coll.Query.Where(
                    coll.Query.StandardReferenceID == AppEnum.StandardReference.EsoManifestation.ToString(),
                    coll.Query.IsActive == true);
                coll.Query.OrderBy(coll.Query.ItemID.Ascending);
                coll.LoadAll();

                foreach (var item in coll)
                {
                    cblEsoManifestations.Items.Add(new ButtonListItem(item.ItemName, item.ItemID));
                }

                // EsoStatus
                StandardReference.InitializeIncludeSpace(cboSREsoStatus, AppEnum.StandardReference.EsoStatus);

                PopulateRegistrationInfo();
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void PopulateRegistrationInfo()
        {
            var reg = RegistrationCurrent;

            var pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);

            lblPatientName.Text = pat.PatientName;

            lblMedicalNo.Text = pat.MedicalNo;
            lblRegistrationDate.Text = Convert.ToDateTime(reg.RegistrationDate).ToString(AppConstant.DisplayFormat.DateShortMonth);
            lblRegistrationNo.Text = reg.RegistrationNo;
            lblGender.Text = pat.Sex == "M" ? "Male" : "Female";
            optPregnantStatus.Enabled = pat.Sex == "F";
            lblDateOfBirth.Text = string.Format("{0} ({1}y {2}m {3}d)", (pat.DateOfBirth ?? new DateTime()).ToString(AppConstant.DisplayFormat.DateShortMonth),
                reg.AgeInYear, reg.AgeInMonth, reg.AgeInDay);

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(RegistrationCurrent.ServiceUnitID);
            lblServiceUnit.Text = unit.ServiceUnitName;

            var val = VitalSign.LastVitalSignValue(RegistrationNo, FromRegistrationNo, VitalSign.VitalSignEnum.BodyWeight, DateTime.Now);
            lblBodyWeight.Text = String.Format("{0:N2} Kg", val);

            lblOccupation.Text = StandardReference.GetItemName(AppEnum.StandardReference.Occupation, pat.SROccupation);
            lblEthnic.Text = StandardReference.GetItemName(AppEnum.StandardReference.Ethnic, pat.SREthnic);

            PopulatePatientImage(reg.PatientID);
        }
        private void PopulatePatientImage(string patientID)
        {
            // Patient Photo
            imgPatientPhoto.ImageUrl = string.Empty;
            // Load from database
            var patientImg = new PatientImage();
            if (patientImg.LoadByPrimaryKey(patientID))
            {
                // Show Image
                if (patientImg.Photo != null)
                {
                    imgPatientPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}",
                        Convert.ToBase64String(patientImg.Photo));
                }
                else
                {
                    imgPatientPhoto.ImageUrl = lblGender.Text == "Male" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
                }
            }
            else
                imgPatientPhoto.ImageUrl = lblGender.Text == "Male" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";

        }

        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var ent = RegistrationEsoCurrent;
            txtEsoNo.Text = string.Format("{0:00000}", ent.EsoNo);
            txtEsoDateTime.SelectedDate = ent.EsoDateTime;

            txtMainDisease.Text = ent.MainDisease;
            optPregnantStatus.SelectedValue = ent.PregnantStatus;

            if (!string.IsNullOrEmpty(ent.EsoComorbidities) && ent.EsoComorbidities.Contains(";"))
            {
                var val = string.Concat(ent.EsoComorbidities, ";");
                foreach (ButtonListItem item in cblEsoComorbidities.Items)
                {
                    if (val.Contains(item.Value + ";"))
                        item.Selected = true;
                    else
                        item.Selected = false;
                }
            }

            if (!string.IsNullOrEmpty(ent.EsoManifestations) && ent.EsoManifestations.Contains(";"))
            {
                var val = string.Concat(ent.EsoManifestations, ";");
                foreach (ButtonListItem item in cblEsoManifestations.Items)
                {
                    if (val.Contains(item.Value + ";"))
                        item.Selected = true;
                    else
                        item.Selected = false;
                }
            }

            txtEsoOtherManifestation.Text = ent.EsoOtherManifestation;
            txtStartDateTime.SelectedDate = ent.StartDateTime;
            txtEndDateTime.SelectedDate = ent.EndDateTime;
            cboSREsoStatus.SelectedValue = ent.SREsoStatus;
            txtPrevEsoHistory.Text = ent.PrevEsoHistory;
            txtLaboratoryTest.Text = ent.LaboratoryTest;
            txtAssessmentNote.Text = ent.AssessmentNote;

            Session.Remove("ditems");
            grdItem.Rebind();

            Session.Remove("scaleprop");
            grdScale.Rebind();
        }
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            var isEdited = newVal != AppEnum.DataMode.Read;
        }
        protected override void OnMenuNewClick()
        {
            txtEsoNo.Text = string.Format("{0:00000}", NewEsoNo());
            var timeNow = (new DateTime()).NowAtSqlServer();
            txtEsoDateTime.SelectedDate = timeNow;

            Session.Remove("ditems");
            grdItem.Rebind();

            Session.Remove("scaleprop");
            grdScale.Rebind();
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Save(args, true);
        }

        private bool Save(ValidateArgs args, bool isNewRecord)
        {
            using (var trans = new esTransactionScope())
            {
                var ent = new RegistrationEso();
                if (isNewRecord || !ent.LoadByPrimaryKey(RegistrationNo, txtEsoNo.Text.ToInt()))
                {
                    ent.RegistrationNo = RegistrationNo;
                    ent.EsoNo = NewEsoNo();

                    // Save Unit History krn di reg di tgl berikutnya bisa berubah
                    SetServiceUnitHistory(ent, isNewRecord);
                }

                ent.EsoDateTime = txtEsoDateTime.SelectedDate;
                ent.MainDisease = txtMainDisease.Text;
                ent.ParamedicID = ParamedicTeam.DPJP(RegistrationNo).ParamedicID;
                ent.PregnantStatus = optPregnantStatus.SelectedValue;
                ent.EsoComorbidities = string.Join(";", cblEsoComorbidities.SelectedValues);
                ent.EsoManifestations = string.Join(";", cblEsoManifestations.SelectedValues);
                ent.EsoOtherManifestation = txtEsoOtherManifestation.Text;

                if (txtStartDateTime.IsEmpty)
                    ent.str.StartDateTime = string.Empty;
                else
                    ent.StartDateTime = txtStartDateTime.SelectedDate;

                if (txtEndDateTime.IsEmpty)
                    ent.str.EndDateTime = string.Empty;
                else
                    ent.EndDateTime = txtEndDateTime.SelectedDate;

                ent.SREsoStatus = cboSREsoStatus.SelectedValue;
                ent.PrevEsoHistory = txtPrevEsoHistory.Text;
                ent.LaboratoryTest = txtLaboratoryTest.Text;
                ent.AssessmentNote = txtAssessmentNote.Text;


                // RegistrationEsoItem
                var items = new RegistrationEsoItemCollection();
                items.Query.Where(items.Query.RegistrationNo == RegistrationNo, items.Query.EsoNo == txtEsoNo.Text.ToInt());
                items.LoadAll();
                items.MarkAllAsDeleted();

                var ditems = (DataTable)Session["ditems"];

                foreach (GridDataItem item in grdItem.MasterTableView.Items)
                {
                    var mrno = item.GetDataKeyValue("MedicationReceiveNo").ToInt();
                    var drugItem = items.AddNew();

                    drugItem.RegistrationNo = RegistrationNo;
                    drugItem.EsoNo = txtEsoNo.Text.ToInt();
                    drugItem.MedicationReceiveNo = mrno;
                    drugItem.ConsumeIndication = ((RadTextBox)item.FindControl("txtConsumeIndication")).Text;

                    var rowItem = ditems.Rows.Find(mrno);
                    if (rowItem["StartConsumeDateTime"] != DBNull.Value)
                        drugItem.StartConsumeDateTime = Convert.ToDateTime(rowItem["StartConsumeDateTime"]);

                    if (rowItem["EndConsumeDateTime"] != DBNull.Value)
                        drugItem.EndConsumeDateTime = Convert.ToDateTime(rowItem["EndConsumeDateTime"]);

                    var reqForm = Request.Form[string.Format("chkOnOff_{0}", mrno)];
                    var isSuspect = reqForm != null && "on".Equals(reqForm.ToLower());
                    drugItem.IsSuspect = isSuspect;
                }

                items.Save();

                // RegistrationEsoScale
                var scaleCol = new RegistrationEsoScaleCollection();
                if (DataModeCurrent == AppEnum.DataMode.Edit)
                {
                    scaleCol.Query.Where(scaleCol.Query.RegistrationNo == RegistrationNo, scaleCol.Query.EsoNo == txtEsoNo.Text.ToInt());
                    scaleCol.LoadAll();
                    scaleCol.MarkAllAsDeleted();
                    scaleCol.Save();
                }

                scaleCol = new RegistrationEsoScaleCollection();
                int? scoreTot = 0;
                var dtbScale = ScaleDataTable(RegistrationNo, txtEsoNo.Text.ToInt());
                foreach (DataRow row in dtbScale.Rows)
                {
                    var entLine = scaleCol.AddNew();
                    entLine.RegistrationNo = RegistrationNo;
                    entLine.EsoNo = txtEsoNo.Text.ToInt();
                    entLine.SREsoScale = row["ItemID"].ToString();
                    entLine.ScaleValue = 0;
                    if (row["ScaleStatus"] == DBNull.Value)
                    {
                        entLine.str.ScaleStatus = string.Empty;
                        entLine.ScaleValue = 0;
                    }
                    else
                    {
                        entLine.ScaleStatus = row["ScaleStatus"].ToString();
                        entLine.ScaleValue = row["ScaleValue"].ToInt();
                        scoreTot = scoreTot + entLine.ScaleValue;
                    }
                }

                ent.EsoScaleTotal = scoreTot;
                ent.Save();

                scaleCol.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
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

        private void SetServiceUnitHistory(RegistrationEso ent, bool isNewRecord)
        {
            // Set history Service Unit
            var isNotSet = true;
            if (!isNewRecord)
            {
                var pt = PatientTransfer.LoadLastTransfer(RegistrationNo, txtEsoDateTime.SelectedDate.Value);
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
            Session.Remove("ditems");
            grdItem.Rebind();

            Session.Remove("scaleprop");
            grdScale.Rebind();
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
            var ent = RegistrationEsoCurrent;
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
            return (RegistrationEsoCurrent.IsDeleted ?? false) == false && RegistrationEsoCurrent.CreatedByUserID == AppSession.UserLogin.UserID;
        }

        public override bool OnGetStatusMenuDelete()
        {
            // Bisa klik tombol Delete
            return (RegistrationEsoCurrent.IsDeleted ?? false) == false && RegistrationEsoCurrent.CreatedByUserID == AppSession.UserLogin.UserID;
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
            ajax.AddAjaxSetting(grdItem, grdItem);
            ajax.AddAjaxSetting(grdScale, grdScale);
        }
        #endregion

        private int NewEsoNo()
        {
            var qr = new RegistrationEsoQuery("a");
            var fb = new RegistrationEso();
            qr.Where(qr.RegistrationNo == RegistrationNo);
            qr.es.Top = 1;
            qr.OrderBy(qr.EsoNo.Descending);

            if (fb.Load(qr))
            {
                return fb.EsoNo.ToInt() + 1;
            }
            return 1;
        }

        #region Item
        protected void grdItem_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var grd = ((RadGrid)sender);
            grd.DataSource = ItemDataTable(RegistrationNo, txtEsoNo.Text.ToInt());
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


                            var qrUsed = new MedicationReceiveUsedQuery("a");
                            qrUsed.Where(qrUsed.MedicationReceiveNo == item.MedicationReceiveNo, qrUsed.RealizedDateTime.IsNotNull(), qrUsed.IsNotConsume != true);
                            qrUsed.OrderBy(qrUsed.RealizedDateTime.Ascending);
                            qrUsed.es.Top = 1;

                            var mrUsed = new MedicationReceiveUsed();
                            if (mrUsed.Load(qrUsed))
                                newRow["StartConsumeDateTime"] = mrUsed.RealizedDateTime;

                            qrUsed = new MedicationReceiveUsedQuery("a");
                            qrUsed.Where(qrUsed.MedicationReceiveNo == item.MedicationReceiveNo, qrUsed.RealizedDateTime.IsNotNull(), qrUsed.IsNotConsume != true);
                            qrUsed.OrderBy(qrUsed.RealizedDateTime.Descending);
                            qrUsed.es.Top = 1;
                            mrUsed = new MedicationReceiveUsed();
                            if (mrUsed.Load(qrUsed))
                                newRow["EndConsumeDateTime"] = mrUsed.RealizedDateTime;

                            ditems.Rows.Add(newRow);
                        }

                        Session.Remove("diSelecteds");
                    }

                    return (DataTable)obj;
                }
            }

            var qr = new RegistrationEsoItemQuery("a");
            var qrMr = new MedicationReceiveQuery("b");
            qr.InnerJoin(qrMr).On(qr.MedicationReceiveNo == qrMr.MedicationReceiveNo);
            qr.Where(qr.RegistrationNo == registrationNo, qr.EsoNo == drugObsNo);

            qr.OrderBy(qrMr.ItemDescription.Ascending);
            qr.Select(qr, qrMr.ItemDescription, qrMr.RefTransactionNo, qrMr.RefSequenceNo, qrMr.SRConsumeMethod, qrMr.ConsumeQty, qrMr.SRConsumeUnit);

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

            //var recNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["MedicationReceiveNo"]).ToInt();
            var recNo = item.GetDataKeyValue("MedicationReceiveNo").ToInt();

            var ditems = (DataTable)Session["ditems"];
            var row = ditems.Rows.Find(recNo);
            if (row != null)
            {
                row.Delete();
                ((RadGrid)sender).Rebind();
            }
        }
        #endregion

        #region Skala Probabilitas
        protected void grdScale_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var grd = ((RadGrid)sender);
            grd.DataSource = ScaleDataTable(RegistrationNo, txtEsoNo.Text.ToInt());

            grdScale.Columns[1].FooterText = String.Format("<b>Naranjo Score: {0}</b>", NaranjoScore());
        }

        private DataTable ScaleDataTable(string registrationNo, int esoNo)
        {
            if (IsPostBack)
            {
                object obj = Session["scaleprop"];
                if (obj != null)
                {
                    return (DataTable)obj;
                }
            }

            var que = new AppStandardReferenceItemQuery("sri");
            var qrLine = new RegistrationEsoScaleQuery("a");

            que.LeftJoin(qrLine)
            .On(que.ItemID == qrLine.SREsoScale && qrLine.RegistrationNo == registrationNo && qrLine.EsoNo == esoNo);

            que.Where(que.StandardReferenceID == AppEnum.StandardReference.EsoScale.ToString());
            que.OrderBy(que.LineNumber.Ascending);
            que.Select(que.ItemID, que.ItemName, que.Note, qrLine.ScaleStatus, qrLine.ScaleValue, "< 1 as GroupField>");
            var dtb = que.LoadDataTable();

            Session["scaleprop"] = dtb;
            return (DataTable)Session["scaleprop"];
        }

        protected void grdScale_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;
                var optScaleStatus = ((RadRadioButtonList)dataItem.FindControl("optScaleStatus"));
                var scaleStatus = dataItem["ScaleStatus"].Text;
                if (scaleStatus != "&nbsp;")
                    optScaleStatus.SelectedValue = scaleStatus;
            }
        }

        protected void grdScale_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem && e.CommandSource is RadRadioButton)
            {
                var ditem = (GridDataItem)e.Item;
                var itemID = ditem.GetDataKeyValue("ItemID");

                var scaleStatus = ((RadRadioButton)e.CommandSource).Value.ToInt();
                if (scaleStatus > 0)
                {
                    var dtb = ScaleDataTable(RegistrationNo, txtEsoNo.Text.ToInt());
                    foreach (DataRow row in dtb.Rows)
                    {
                        if (itemID.Equals(row["ItemID"]))
                        {
                            row["ScaleStatus"] = scaleStatus;
                            row["ScaleValue"] = row["Note"].ToString().Split(';')[scaleStatus - 1].ToInt();
                            break;
                        }
                    }
                }

            }

            grdScale.Rebind();
        }

        private string NaranjoScore()
        {
            var dtb = ScaleDataTable(RegistrationNo, txtEsoNo.Text.ToInt());
            var scoreTot = 0;
            foreach (DataRow row in dtb.Rows)
            {
                scoreTot = scoreTot + row["ScaleValue"].ToInt();
            }

            if (scoreTot >= 9)
                return "(>=9) Exact";
            if (scoreTot >= 5)
                return "(5-8) Most Probably";
            if (scoreTot >= 1)
                return "(1-4) Little Possibility";

            return "(0) Doubtful";
        }

        protected void grdScale_PreRender(object sender, EventArgs e)
        {
            // Hide header
            foreach (GridGroupHeaderItem item in grdScale.MasterTableView.GetItems(GridItemType.GroupHeader))
            {
                item.Visible = false;
            }

        }
        #endregion


    }
}
