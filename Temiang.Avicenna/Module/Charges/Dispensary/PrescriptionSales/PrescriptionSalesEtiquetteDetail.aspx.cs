using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class PrescriptionSalesEtiquetteDetail : BasePageDetail
    {
        #region Page Event & Initialize

        private string PrescriptionNo
        {
            get
            {
                return Request.QueryString["pno"];
            }
        }
        private string SequenceNo
        {
            get
            {
                return Request.QueryString["sno"];
            }
        }
        private string LocationId
        {
            get
            {
                return Request.QueryString["locid"];
            }
        }
        
        private string getp(string p)
        {
            return Request.QueryString[p];
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            //UrlPageSearch = "PrescriptionSalesSearch.aspx";
            // md=view&pno=RSP140708-0023&regno=REG/OP/140705-0007&type=sales&rt=opr&ono=#
            UrlPageList = "PrescriptionSalesDetail.aspx?md=" + getp("md") +
                "&pno=" + getp("pno") + "&regno=" + getp("regno") + "&type=" + getp("type") +
                "&rt=" + getp("rt") + "&ono=" + getp("ono");
            //ProgramID = AppConstant.Program.PrescriptionItemEtiquette;
            if (Request.QueryString["type"] == "sales")
            {
                //UrlPageList = Request.QueryString["rt"] == "opr"
                //                  ? "PrescriptionSalesList.aspx?type=sales&rt=" + Request.QueryString["rt"]
                //                  : "PrescriptionOrderList.aspx?type=sales&rt=" + Request.QueryString["rt"];
                ProgramID = Request.QueryString["rt"] == "opr"
                                ? AppConstant.Program.PrescriptionSalesOpr
                                : AppConstant.Program.PrescriptionSales;
            }
            else
            {
                //UrlPageList = "PrescriptionSalesList.aspx?type=realization&rt=" + Request.QueryString["rt"];
                ProgramID = Request.QueryString["rt"] == "opr" ? AppConstant.Program.PrescriptionRealizationOpr : AppConstant.Program.PrescriptionRealization;
            }

            if (!IsPostBack)
            {
                trSelectBatchNumber.Visible = !AppSession.Parameter.IsEnabledStockWithEdControl;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                var entity = new TransPrescriptionItemEtiquette();
                if (!entity.LoadByPrimaryKey(PrescriptionNo, SequenceNo))
                {
                    DataModeCurrent = AppEnum.DataMode.New;
                }

                var tpi = new TransPrescriptionItem();
                if (tpi.LoadByPrimaryKey(PrescriptionNo, SequenceNo))
                    txtItemID.Text = string.IsNullOrEmpty(tpi.ItemInterventionID) ? tpi.ItemID : tpi.ItemInterventionID;
                else txtItemID.Text = string.Empty;

                //CollapsePanel11.IsCollapsed = "true";
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            ToolBarMenuSearch.Visible = false;

            // hide approve void
            var tb = (RadToolBar)Helper.FindControlRecursive(this, "fw_tbarData");
            tb.Items[13].Visible = false;
            tb.Items[14].Visible = false;
            tb.Items[15].Visible = false;
            tb.Items[16].Visible = false;
            tb.Items[17].Visible = false;
            tb.Items[18].Visible = false;
            //

            foreach (RadToolBarButton btn in ((RadToolBarDropDown)tb.Items[20]).Buttons) {
                btn.Visible = btn.Text.ToLower().Contains("etiket");
            }

            ((RadToolBarDropDown)tb.Items[20]).Buttons[0].Visible = chkIsDrugOutside.Checked;
            ((RadToolBarDropDown)tb.Items[20]).Buttons[1].Visible = !chkIsDrugOutside.Checked;


            //((RadToolBarDropDown)tb.Items[20]).Buttons[2].Visible = false;
            //((RadToolBarDropDown)tb.Items[20]).Buttons[3].Visible = false;
        }
        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {

        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new TransPrescriptionItemEtiquette();
            if (entity.LoadByPrimaryKey(PrescriptionNo, SequenceNo))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var tpi = new TransPrescriptionItem();

            if (tpi.LoadByPrimaryKey(PrescriptionNo, SequenceNo))
            {
                if (tpi.IsVoid ?? false)
                    args.MessageText = AppConstant.Message.RecordHasVoided;
                else
                {
                    var entity = new TransPrescriptionItemEtiquette();
                    entity = SetEntityValue(entity);
                    SaveEntity(entity);
                }
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var tpi = new TransPrescriptionItem();

            if (tpi.LoadByPrimaryKey(PrescriptionNo, SequenceNo))
            {
                if (tpi.IsVoid ?? false)
                    args.MessageText = AppConstant.Message.RecordHasVoided;
                else
                {
                    var entity = new TransPrescriptionItemEtiquette();
                    entity = SetEntityValue(entity);
                    SaveEntity(entity);
                }
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("PrescriptionNo='{0}'", PrescriptionNo);
            auditLogFilter.TableName = "TransPrescription";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            switch (programID)
            {
                case AppConstant.Report.PrescriptionEtiket:
                    printJobParameters.AddNew("p_PrescriptionNo", PrescriptionNo);
                    printJobParameters.AddNew("p_SequenceNo", SequenceNo);
                    printJobParameters.AddNew("p_Label", "1");
                    break;
                case AppConstant.Report.PrescriptionEtiketLr:
                    printJobParameters.AddNew("p_PrescriptionNo", PrescriptionNo);
                    printJobParameters.AddNew("p_SequenceNo", SequenceNo);
                    printJobParameters.AddNew("p_Label", "2");
                    break;
                default:
                    printJobParameters.AddNew("p_PrescriptionNo", PrescriptionNo);
                    printJobParameters.AddNew("p_SequenceNo", SequenceNo);
                    printJobParameters.AddNew("p_Label", string.Empty);
                    printJobParameters.AddNew("p_UserID", AppSession.UserLogin.UserID);
                    break;
            }
        }

        //public override bool? OnGetStatusMenuApproval()
        //{
        //    return !(bool)ViewState["IsApproved"];
        //}

        //public override bool OnGetStatusMenuVoid()
        //{
        //    return !(bool)ViewState["IsVoid"];
        //}

        //protected override void OnDataModeChanged(DataMode oldVal, DataMode newVal)
        //{
        //    RefreshCommandItemTransPrescriptionItem(oldVal, newVal);
        //    chkIsComplete.Enabled = (newVal == DataMode.Read);
        //}

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new TransPrescriptionItemEtiquette();
            if (!entity.LoadByPrimaryKey(PrescriptionNo, SequenceNo))
            {
                //DataModeCurrent = DataMode.New;
                var isControlExpired = false;
                var tpi = new TransPrescriptionItem();
                if (tpi.LoadByPrimaryKey(PrescriptionNo, SequenceNo))
                {
                    entity.AddNew();
                    // set defaut
                    entity.PrescriptionNo = PrescriptionNo;
                    entity.SequenceNo = SequenceNo;
                    entity.IsDrugOutside = false;
                    // find item
                    var item = new Item();
                    if (item.LoadByPrimaryKey(string.IsNullOrEmpty(tpi.ItemInterventionID) ? tpi.ItemID : tpi.ItemInterventionID))
                    {
                        entity.ItemName = item.ItemName;
                        if (!(tpi.IsCompound ?? false))
                        {
                            /*zat aktif hanya ditampilkan untuk obat tunggal bukan racikan*/
                            entity.ItemName += GetZatActive(string.IsNullOrEmpty(tpi.ItemInterventionID) ? tpi.ItemID : tpi.ItemInterventionID);
                        }
                        else
                        {
                            /* jika racikan tambahkan dosis setelah nama obat */
                            entity.ItemName += " " + tpi.DosageQty + " " + tpi.SRDosageUnit;
                        }

                        var itempm = new ItemProductMedic();
                        if (itempm.LoadByPrimaryKey(string.IsNullOrEmpty(tpi.ItemInterventionID) ? tpi.ItemID : tpi.ItemInterventionID))
                        {
                            isControlExpired = itempm.IsControlExpired ?? false;

                            entity.IsDrugOutside = itempm.SRDrugLabelType == "2";
                            if (!string.IsNullOrEmpty(itempm.str.SRKeeping))
                            {
                                var sr = new AppStandardReferenceItem();
                                entity.Keeping = sr.LoadByPrimaryKey("Keeping", itempm.SRKeeping) ? sr.ItemName : "";
                            }
                            else
                                entity.Keeping = "";

                            if (!string.IsNullOrEmpty(itempm.SpecificInfo))
                                entity.SpecificInfo = itempm.SpecificInfo;
                            else
                            {
                                var std = new AppStandardReferenceItem();
                                if (std.LoadByPrimaryKey(AppEnum.StandardReference.ProductType.ToString(), itempm.SRProductType))
                                    entity.SpecificInfo = !string.IsNullOrEmpty(std.Note) ? std.Note : string.Empty;
                                else
                                    entity.SpecificInfo = string.Empty;
                            }

                            if (itempm.SRTherapyGroup == AppSession.Parameter.TherapyGroupAntibiotics)
                            {
                                if (itempm.SRProductType != AppSession.Parameter.ProductTypeInjeksi)
                                {
                                    if (string.IsNullOrEmpty(entity.SpecificInfo))
                                    {
                                        entity.SpecificInfo = "Antibiotik, habiskan";
                                    }
                                    else
                                    {
                                        entity.SpecificInfo += "; Antibiotik, habiskan";
                                    }
                                }
                            }
                        }
                        else
                            entity.SpecificInfo = string.Empty;

                        // if compound
                        if (tpi.IsCompound ?? false)
                        {
                            // find another compound
                            var tpiColl = new TransPrescriptionItemCollection();
                            tpiColl.Query.Where(tpiColl.Query.PrescriptionNo == tpi.PrescriptionNo &&
                                tpiColl.Query.ParentNo == tpi.SequenceNo);
                            tpiColl.Query.Load();
                            foreach (var tpi_i in tpiColl)
                            {
                                var item_i = new Item();
                                if (item_i.LoadByPrimaryKey(string.IsNullOrEmpty(tpi_i.ItemInterventionID) ? tpi_i.ItemID : tpi_i.ItemInterventionID))
                                {
                                    var srDosUnit = new AppStandardReferenceItem();
                                    var srDU = "";
                                    if (srDosUnit.LoadByPrimaryKey("DosageUnit", tpi.SRDosageUnit))
                                    {
                                        srDU = srDosUnit.ItemName;
                                    }

                                    //entity.ItemName += Environment.NewLine + item_i.ItemName;
                                    entity.ItemName += ", " + item_i.ItemName + " " + tpi_i.DosageQty + " " + tpi_i.SRDosageUnit;
                                }
                            }
                        }
                    }
                    var cm = new ConsumeMethod();
                    if (cm.LoadByPrimaryKey(tpi.SRConsumeMethod))
                    {
                        entity.ConsumeMethod = cm.SRConsumeMethodName;
                    }
                    if (!string.IsNullOrEmpty(tpi.SRConsumeMethod))
                    {
                        entity.ConsumeMethod += " " + tpi.ConsumeQty + " " + GetSrConsumeUnit(tpi.SRConsumeUnit);
                    }
                    if (!string.IsNullOrEmpty(tpi.Notes))
                        entity.ConsumeMethod += " " + tpi.Notes;
                    
                    if (AppSession.Parameter.IsEnabledStockWithEdControl && isControlExpired)
                    {
                        var defExpiredDate = Convert.ToDateTime("1/1/2999");
                        var movementQ = new ItemMovementQuery();
                        movementQ.Where(movementQ.TransactionNo == PrescriptionNo, movementQ.SequenceNo == SequenceNo, movementQ.ExpiredDate.IsNotNull(), movementQ.ExpiredDate != defExpiredDate, movementQ.BatchNumber != "-N/A-");
                        movementQ.OrderBy(movementQ.ExpiredDate.Ascending);
                        movementQ.es.Top = 1;
                        movementQ.es.WithNoLock = true;
                        var movement = new ItemMovement();
                        if (movement.Load(movementQ))
                        {
                            entity.BatchNumber = movement.BatchNumber;
                            entity.ExpiredDate = movement.ExpiredDate;
                        }
                        else
                        {
                            var balanceQ = new ItemBalanceDetailEdQuery();
                            balanceQ.Where(balanceQ.LocationID == LocationId, 
                                balanceQ.ItemID == (string.IsNullOrEmpty(tpi.ItemInterventionID) ? tpi.ItemID : tpi.ItemInterventionID), 
                                balanceQ.BatchNumber != "-N/A-", balanceQ.ExpiredDate != defExpiredDate,
                                balanceQ.Balance > 0, balanceQ.IsActive == true);
                            balanceQ.OrderBy(balanceQ.ExpiredDate.Ascending, balanceQ.CreatedDateTime.Ascending);
                            balanceQ.es.Top = 1;
                            balanceQ.es.WithNoLock = true;
                            var balance = new ItemBalanceDetailEd();
                            if (balance.Load(balanceQ))
                            {
                                entity.BatchNumber = balance.BatchNumber;
                                entity.ExpiredDate = balance.ExpiredDate;
                            }
                        }
                    }
                }
            }

            OnPopulateEntryControl(entity);
        }

        private string GetSrConsumeUnit(string SRDosageUnit)
        {
            var srDU = "";
            var em = new Embalace();
            if (em.LoadByPrimaryKey(SRDosageUnit))
                srDU = em.EmbalaceLabel;
            if (srDU == "")
            {
                var srDosUnit = new AppStandardReferenceItem();

                if (srDosUnit.LoadByPrimaryKey("DosageUnit", SRDosageUnit))
                {
                    srDU = srDosUnit.ItemName;
                }
                else { 
                    srDosUnit.QueryReset();
                    if (srDosUnit.LoadByPrimaryKey("GlobalConsumeUnit", SRDosageUnit)) {
                        srDU = srDosUnit.ItemName;
                    }
                }
            }

            return (srDU == "" ? SRDosageUnit : srDU);
        }

        private string GetZatActive(string itemID)
        {
            // find zat active
            var zacoll = new ZatActiveCollection();
            var qza = new ZatActiveQuery("a");
            var qi = new ItemQuery("b");
            var qiza = new ItemProductMedicZatActiveQuery("c");
            qza.InnerJoin(qiza).On(qza.ZatActiveID == qiza.ZatActiveID)
                .InnerJoin(qi).On(qiza.ItemID == qi.ItemID);
            qza.Where(qi.ItemID == itemID);
            zacoll.Load(qza);
            var zanames = string.Empty;
            foreach (var za in zacoll)
            {
                zanames += za.ZatActiveName.Trim() + ", ";
            }
            var zaname = (zanames == string.Empty ? string.Empty : "(" + zanames.Remove(zanames.Length - 2) + ")");
            return zaname;
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var tpi = (TransPrescriptionItemEtiquette)entity;
            // 1 txtPrescriptionNo.Text = tpi.PrescriptionNo;
            txtItemName.Text = tpi.ItemName;
            txtConsumeMethod.Text = tpi.ConsumeMethod;
            txtKeeping.Text = tpi.Keeping;
            txtSpesificInfo.Text = tpi.SpecificInfo;
            txtExpiredDate.SelectedDate = tpi.ExpiredDate;
            txtBatchNumber.Text = tpi.BatchNumber;

            chkIsDrugOutside.Checked = tpi.IsDrugOutside ?? false;
            txtNumberOfCopies.Value = tpi.NumberOfCopies ?? 1;
            // patien and registration detail
            /* 1 var tp = new TransPrescription();
            if (tp.LoadByPrimaryKey(tpi.PrescriptionNo))
            {
                PopulateRegistrationInformation(tp.RegistrationNo);

                var r = new Registration();
                if (r.LoadByPrimaryKey(tp.RegistrationNo))
                {
                    PopulatePatientInformation(r.PatientID);
                }
            }*/
        }

        /* 1 private void PopulateRegistrationInformation(string registrationNo)
        {
            if (string.IsNullOrEmpty(registrationNo))
                return;

            var registration = new Registration();
            if (registration.LoadByPrimaryKey(registrationNo))
            {
                var patient = new Patient();
                if (patient.LoadByPrimaryKey(registration.PatientID))
                {
                    txtMedicalNo.Text = patient.MedicalNo;
                    txtPatientName.Text = patient.PatientName;

                    optSexFemale.Checked = patient.Sex.Equals("F");
                    optSexMale.Checked = patient.Sex.Equals("M");
                    if (patient.Sex.Equals("F"))
                        optSexMale.Enabled = false;
                    else
                        optSexFemale.Enabled = false;

                    txtAgeDay.Value = Helper.GetAgeInDay(patient.DateOfBirth.Value);
                    txtAgeMonth.Value = Helper.GetAgeInMonth(patient.DateOfBirth.Value);
                    txtAgeYear.Value = Helper.GetAgeInYear(patient.DateOfBirth.Value);
                }

                txtServiceUnitID.Text = registration.ServiceUnitID;

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(txtServiceUnitID.Text);
                lblServiceUnitName.Text = unit.ServiceUnitName;
                txtRoomID.Text = registration.RoomID;

                var room = new ServiceRoom();
                room.LoadByPrimaryKey(txtRoomID.Text);
                lblRoomName.Text = room.RoomName;

                txtBedID.Text = registration.BedID;

                var prm = new Paramedic();
                if (prm.LoadByPrimaryKey(registration.ParamedicID)) {
                    txtParamedic.Text = prm.ParamedicName;
                }
                var grn = new Guarantor();
                if (grn.LoadByPrimaryKey(registration.GuarantorID))
                {
                    txtGuarantor.Text = grn.GuarantorName;
                }
                
                if (string.IsNullOrEmpty(Request.QueryString["mode"]))
                {
                    CollapsePanel11.Title = txtPatientName.Text +
                                            " [" + txtMedicalNo.Text + "] [" + txtRegistrationNo.Text + "] " +
                                            " - " + patient.DateOfBirth.Value.ToShortDateString() + " - " +
                                            (optSexMale.Checked ? "M [" : "F [") +
                                            txtParamedic.Text + "] " +
                                            lblServiceUnitName.Text + ", " +
                                            lblRoomName.Text + ", " +
                                            txtBedID.Text + ", " +
                                            txtGuarantor.Text;
                }
                else
                {
                    CollapsePanel11.Title = "Patient Information";
                }
            }
        }

        private void PopulatePatientInformation(string patientID)
        {
            if (string.IsNullOrEmpty(patientID))
                return;

            var patient = new Patient();
            if (patient.LoadByPrimaryKey(patientID))
            {
                txtMedicalNo.Text = patient.MedicalNo;
                txtPatientName.Text = patient.PatientName;

                optSexFemale.Checked = patient.Sex.Equals("F");
                optSexMale.Checked = patient.Sex.Equals("M");
                if (patient.Sex.Equals("F"))
                    optSexMale.Enabled = false;
                else
                    optSexFemale.Enabled = false;

                txtAgeDay.Value = Helper.GetAgeInDay(patient.DateOfBirth.Value);
                txtAgeMonth.Value = Helper.GetAgeInMonth(patient.DateOfBirth.Value);
                txtAgeYear.Value = Helper.GetAgeInYear(patient.DateOfBirth.Value);
            }
        }*/

        #endregion

        #region Private Method Standard

        private TransPrescriptionItemEtiquette SetEntityValue(TransPrescriptionItemEtiquette entity)
        {
            if (!entity.LoadByPrimaryKey(PrescriptionNo, SequenceNo))
            {
                entity.AddNew();
                entity.PrescriptionNo = PrescriptionNo;
                entity.SequenceNo = SequenceNo;

                entity.CreateDateTime = (new DateTime()).NowAtSqlServer();
                entity.CreateUserID = AppSession.UserLogin.UserID;
            }

            entity.ItemName = txtItemName.Text;
            entity.ConsumeMethod = txtConsumeMethod.Text;
            entity.Keeping = txtKeeping.Text;
            entity.SpecificInfo = txtSpesificInfo.Text;
            entity.ExpiredDate = null;
            if (txtExpiredDate.SelectedDate.HasValue)
            {
                entity.ExpiredDate = txtExpiredDate.SelectedDate.Value;
            }
            entity.BatchNumber = txtBatchNumber.Text;
            entity.IsDrugOutside = chkIsDrugOutside.Checked;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.LastUpdateUserID = AppSession.UserLogin.UserID;
            entity.NumberOfCopies = Convert.ToInt32(txtNumberOfCopies.Value);

            return entity;
        }

        private void SaveEntity(TransPrescriptionItemEtiquette entity)
        {
            var tp = new TransPrescription();
            tp.LoadByPrimaryKey(entity.PrescriptionNo);
            tp.IsProceedByPharmacist = true;
            //tp.LastUpdateByUserID = AppSession.UserLogin.UserID;
            //tp.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            using (var trans = new esTransactionScope())
            {
                entity.Save();
                tp.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new TransPrescriptionItemEtiquetteQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.PrescriptionNo == PrescriptionNo,
                        que.SequenceNo > SequenceNo
                    );
                que.OrderBy(que.PrescriptionNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.PrescriptionNo == PrescriptionNo,
                        que.SequenceNo < SequenceNo //txtPrescriptionNo.Text
                    );
                que.OrderBy(que.PrescriptionNo.Descending);
            }

            var entity = new TransPrescriptionItemEtiquette();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        protected override void OnMenuEditClick()
        {
            //if (txtPrescriptionNo.Text == string.Empty)
            //{
            //    OnPopulateEntryControl(new TransPrescription());

            //    if (cboServiceUnitID.Items.Count == 2)
            //        cboServiceUnitID.SelectedIndex = 1;

            //    txtRegistrationNo.Text = (string)ViewState["regno"];
            //    txtPrescriptionDate.SelectedDate = DateTime.Today;
            //    txtPrescriptionNo.Text = GetNewPrescriptionNo();

            //    if (Request.QueryString["md"] == "new")
            //        PopulateRegistrationInformation(txtRegistrationNo.Text);

            //    ViewState["IsApproved"] = false;
            //    ViewState["IsVoid"] = false;
            //}
        }

        protected void txtReferenceNo_TextChanged(object sender, EventArgs e)
        {
            if (txtReferenceNo.Text == string.Empty || !txtReferenceNo.Text.Contains("|"))
            {
                txtBatchNumber.Text = string.Empty;
                txtExpiredDate.SelectedDate = null;

                return;
            }
            var val = txtReferenceNo.Text.Split('|');

            txtBatchNumber.Text = val[0];
            txtExpiredDate.SelectedDate = Convert.ToDateTime(val[1]);
        }
    }
}


/* 1
 backup html
 <cc:CollapsePanel ID="CollapsePanel11" runat="server" Title="Patient Information" BorderStyle="Solid" BorderColor="ActiveBorder">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr valign="top">
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPrescriptionNo" runat="server" Text="Prescription No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPrescriptionNo" runat="server" Width="300px" MaxLength="20"
                                    ReadOnly="true" />
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="rfvPrescriptionNo" runat="server" ErrorMessage="Prescription No required."
                                    ValidationGroup="entry" ControlToValidate="txtPrescriptionNo" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPrescriptionDate" runat="server" Text="Prescription Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtPrescriptionDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                    DatePopupButton-Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                            <td width="20px">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblServiceUnitID" runat="server" Text="Dispensary"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtServiceUnitIDDispensary" runat="server" Width="300px"
                                    ReadOnly="true" />
                            </td>
                            <td width="20px">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20"
                                    ReadOnly="true" />
                                <a href="javascript:void(0);" onclick="javascript:openWinRegistrationInfo();" class="noti_Container">
                                    <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo" AssociatedControlID="txtRegistrationNo"
                                        Text=""></asp:Label>&nbsp; </a>
                            </td>
                            <td width="20px">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                            </td>
                            <td class="entry">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" MaxLength="15"
                                                ReadOnly="true" />
                                        </td>
                                        <td>
                                            
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="50"
                                    ReadOnly="true" />
                            </td>
                            <td width="20">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr style="height: 24px">
                            <td class="label">
                                <asp:Label ID="lblSex" runat="server" Text="Gender"></asp:Label>
                            </td>
                            <td class="entry">
                                <asp:RadioButton ID="optSexFemale" runat="server" Text="Female" GroupName="Sex" />
                                <asp:RadioButton ID="optSexMale" runat="server" Text="Male" GroupName="Sex" />
                            </td>
                            <td width="20">
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtAgeYear" runat="server" ReadOnly="true" Width="40px">
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                                &nbsp;Y&nbsp;
                                <telerik:RadNumericTextBox ID="txtAgeMonth" runat="server" ReadOnly="true" Width="40px">
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                                &nbsp;M&nbsp;
                                <telerik:RadNumericTextBox ID="txtAgeDay" runat="server" ReadOnly="true" Width="40px">
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                                &nbsp;D
                            </td>
                            <td width="20">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtParamedic" runat="server" Width="300px" MaxLength="50"
                                    ReadOnly="true" />
                            </td>
                            <td width="20px">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblClusterID" runat="server" Text="Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Width="100px" MaxLength="20"
                                    ReadOnly="true" />
                                &nbsp;
                                <asp:Label ID="lblServiceUnitName" runat="server"></asp:Label>
                            </td>
                            <td width="20">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr id="pnlRoom" runat="server">
                            <td class="label">
                                <asp:Label ID="lblRoomID" runat="server" Text="Room"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtRoomID" runat="server" Width="100px" MaxLength="20" ReadOnly="true" />
                                &nbsp;
                                <asp:Label ID="lblRoomName" runat="server"></asp:Label>
                            </td>
                            <td width="20">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr id="pnlBedID" runat="server">
                            <td class="label">
                                <asp:Label ID="lblBedID" runat="server" Text="Bed No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtBedID" runat="server" Width="100px" MaxLength="20" ReadOnly="true" />
                            </td>
                            <td width="20">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr id="Tr1" runat="server">
                            <td class="label">
                                <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtGuarantor" runat="server" Width="300px" MaxLength="50"
                                    ReadOnly="true" />
                            </td>
                            <td width="20">
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
 
 */