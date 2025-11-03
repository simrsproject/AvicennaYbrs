using System;
using System.Data;
using System.Linq;
using System.Text;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class UddItemEntry : BasePageDialogEntry
    {
        int _antibioticMaxConsumeDay = AppParameter.GetParameterValue(AppParameter.ParameterItem.AntibioticMaxConsumeDay).ToInt();

        protected string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        protected string GuarantorID
        {
            get
            {
                return RegistrationCurrent.GuarantorID;
            }
        }
        private string ParamedicID
        {
            get
            {
                return Request.QueryString["parid"];
            }
        }

        private string _abrForLine = null;
        protected string AntibioticRestrictionForLine
        {
            get
            {
                if (_abrForLine == null)
                    _abrForLine = AppParameter.GetParameterValue(AppParameter.ParameterItem.AntibioticRestrictionForLine);

                return _abrForLine;
            }
        }

        private Registration _registration;
        private Registration RegistrationCurrent
        {
            get
            {
                if (_registration == null)
                {
                    _registration = new Registration();
                    _registration.LoadByPrimaryKey(RegistrationNo);

                }

                return _registration;
            }
        }


        private string _patientID;
        protected string PatientID
        {
            get
            {
                // Jangan ambil dari QueryString krn bisa jadi utk PatientID yg berbeda tetapi masih pasien yg sama (PatientRelated)
                //return Request.QueryString["patid"];
                if (!string.IsNullOrEmpty(RegistrationNo) && string.IsNullOrEmpty(_patientID))
                {
                    _patientID = RegistrationCurrent.PatientID;
                }

                return _patientID;
            }
        }

        //protected bool IsAntibioticRestrictionApplied
        //{
        //    get
        //    {
        //        return AppSession.Parameter.IsRasproEnable && AppParameter.IsYes(AppParameter.ParameterItem.IsAntibioticRestriction) && RegistrationCurrent.SRRegistrationType == AppConstant.RegistrationType.InPatient;
        //    }
        //}

        #region override method

        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshMenuRaspro(newVal);
            cboServiceUnitID.Enabled = (newVal == AppEnum.DataMode.Read);

            if (pnlRaspro.Visible == true && newVal == AppEnum.DataMode.Read)
            {
                // Refresh info raspro setelah user klik cancel
                var rr = new RegistrationRaspro();
                rr.Query.es.Top = 1;
                rr.Query.OrderBy(rr.Query.SeqNo.Descending);
                rr.Query.Where(rr.Query.RegistrationNo == RegistrationNo);
                if (rr.Query.Load())
                {
                    var usedRasproSeqno = 0;
                    litAntibioticSuggest.Text = AbRestriction.AntibioticSuggestion(rr, ref usedRasproSeqno);
                }
                else
                    litAntibioticSuggest.Text = String.Empty;
            }

            if (newVal == AppEnum.DataMode.Read)
            {
                grdUddItem.EditIndexes.Clear();
                grdUddItem.MasterTableView.IsItemInserted = false;
                grdUddItem.MasterTableView.ClearEditItems();
            }


            bool isModeRead = (newVal == AppEnum.DataMode.Read);
            grdUddItem.Columns[0].Visible = !isModeRead;
            grdUddItem.Columns[grdUddItem.Columns.Count - 2].Visible = !isModeRead;
            grdUddItem.Columns[grdUddItem.Columns.Count - 3].Visible = isModeRead && (AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor); // Stop Continue
            grdUddItem.MasterTableView.CommandItemDisplay = !isModeRead
                                                                             ? GridCommandItemDisplay.Top
                                                                             : GridCommandItemDisplay.None;
            UddItems = null;
            grdUddItem.Rebind();
        }
        protected override void OnMenuNewClick()
        {

        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            //Tidak memakai mode New
            //using (var trans = new esTransactionScope())
            //{
            //    UddItems.Save();
            //    trans.Complete();
            //}

            //if (IsRasproEnableApplied && hdnRasproIsNew.Value == "1")
            //{
            //    SaveRegistrationRasproItem();
            //    UpdateStatusUddItem();
            //}
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            using (var trans = new esTransactionScope())
            {
                //Cari AB yg berubah terapinya
                if (IsRasproEnableApplied)
                {
                    var rowState = UddItems.RowStateFilter;

                    // Modified
                    UddItems.RowStateFilter = DataViewRowState.ModifiedOriginal;
                    UpdateRelatedRasproTable();

                    //Deleted
                    UddItems.RowStateFilter = DataViewRowState.Deleted;
                    UpdateRelatedRasproTable();

                    //Release filter
                    UddItems.RowStateFilter = rowState;
                }

                UddItems.Save();
                trans.Complete();
            }

            if (IsRasproEnableApplied && hdnRasproIsNew.Value == "1")
            {
                // Untuk item baru
                SaveNewRegistrationRasproItem();
            }
        }

        private void UpdateRelatedRasproTable()
        {
            foreach (var item in UddItems)
            {
                var ipm = new ItemProductMedic();
                ipm.LoadByPrimaryKey(item.GetOriginalColumnValue("ItemID").ToString());

                if (true.Equals(ipm.IsAntibiotic ?? false) && ((item.es.IsModified &&
                    (!item.GetOriginalColumnValue("ItemID").Equals(item.ItemID)
                    || !item.GetOriginalColumnValue("SRConsumeMethod").Equals(item.SRConsumeMethod)
                    || !item.GetOriginalColumnValue("ConsumeQty").Equals(item.ConsumeQty)
                    || !item.GetOriginalColumnValue("SRConsumeUnit").Equals(item.SRConsumeUnit))) || item.es.IsDeleted))
                {
                    // Update RegistrationRasproItem
                    var ritem = new RegistrationRasproItem();
                    ritem.Query.Where(ritem.Query.RegistrationNo == item.GetOriginalColumnValue("RegistrationNo"),
                        ritem.Query.RasproSeqNo == item.GetOriginalColumnValue("RasproSeqNo"),
                        ritem.Query.ItemID == item.GetOriginalColumnValue("ItemID").ToString(),
                        ritem.Query.SRConsumeMethod == item.GetOriginalColumnValue("SRConsumeMethod").ToString(),
                        ritem.Query.ConsumeQty == item.GetOriginalColumnValue("ConsumeQty").ToString(),
                        ritem.Query.SRConsumeUnit == item.GetOriginalColumnValue("SRConsumeUnit").ToString()
                        );
                    if (ritem.Query.Load())
                    {
                        if (item.es.IsModified)
                        {
                            ritem.ItemID = item.ItemID;
                            ritem.SRConsumeMethod = item.SRConsumeMethod;
                            ritem.ConsumeQty = item.ConsumeQty;
                            ritem.SRConsumeUnit = item.SRConsumeUnit;
                        }
                        else if (item.es.IsDeleted)
                        {
                            ritem.MarkAsDeleted();
                        }
                        ritem.Save();
                    }

                }
            }

        }

        protected override void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            // Harus sudah CheckinConfirmHistory
            var cicQr = new CheckinConfirmHistoryQuery("cic");
            cicQr.Where(cicQr.RegistrationNo == RegistrationNo);
            cicQr.es.Top = 1;

            var cic = new CheckinConfirmHistory();
            if (!cic.Load(cicQr))
            {
                args.IsCancel = true;
                args.MessageText = "Patient with current registration have not Checkin Confirm";
                return;
            }

            // Untuk DPJP harus sudah ada asesmen dulu (Handono 230822)
            if (IsUserParamedicDpjp() && AppSession.Parameter.IsEmrPhysicianAssessmentMandatory)
            {
                var pass = new PatientAssessment();
                var passq = new PatientAssessmentQuery();
                passq.Select(passq.RegistrationNo);
                passq.Where(passq.RegistrationNo == RegistrationNo,
                    passq.Or(passq.IsDeleted.IsNull(), passq.IsDeleted == false));
                passq.es.Top = 1;

                if (!pass.Load(passq))
                {
                    args.IsCancel = true;
                    args.MessageText = "Create UDD Item Template not allowed before assessment, please create assessment first";
                    return;
                }
            }

        }

        protected override void OnMenuEditClick()
        {
            UddItems = null; //Supaya reload ulang
            grdUddItem.MasterTableView.IsItemInserted = false;
            grdUddItem.MasterTableView.ClearEditItems();
            grdUddItem.Rebind();
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
            return false;
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

            ajax.AddAjaxSetting(grdUddItem, lblZatActiveInteraction);

            ajax.AddAjaxSetting(cboServiceUnitID, grdUddItem);
            ajax.AddAjaxSetting(cboServiceUnitID, hdnLocationID);


            // Hanya In Patient dan jika AntibioticRestriction diterapkan
            if (IsRasproEnableApplied)
            {
                ajax.AddAjaxSetting(grdUddItem, litRasproInfo);
            }
        }
        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            // Program Fiture
            IsSingleRecordMode = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = false;
            // -------------------

            if (!IsPostBack)
            {
                // Hanya In Patient dan jika AntibioticRestriction diterapkan
                pnlRaspro.Visible = IsRasproEnableApplied;
                clpAntibioticSuggest.Visible = IsRasproEnableApplied;

                var reg = RegistrationCurrent;
                // Use for query history 
                hdnRegistrationType.Value = reg.SRRegistrationType;
                hdnFromRegistrationNo.Value = reg.FromRegistrationNo;


                /// --- Modif jadi bisa dipilih lokasinya (Handono 2022 06) ---///
                ////Default paremetr untuk Lookup ItemID
                //var su = new ServiceUnit();
                //su.LoadByPrimaryKey(reg.ServiceUnitID);
                //if ((su.ServiceUnitPharmacyID ?? string.Empty) == string.Empty)
                //    hdnServiceUnitID.Value = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient ? AppSession.Parameter.ServiceUnitPharmacyID : AppSession.Parameter.ServiceUnitPharmacyIdOpr;
                //else
                //    hdnServiceUnitID.Value = su.ServiceUnitPharmacyID;

                //hdnLocationID.Value = (new ServiceUnit()).GetMainLocationId(hdnServiceUnitID.Value);
                /// --- End Remark ---///


                var suid = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient ? AppSession.Parameter.ServiceUnitPharmacyID : AppSession.Parameter.ServiceUnitPharmacyIdOpr;
                //var locid = (new ServiceUnit()).GetMainLocationId(suid);


                // Overide
                var guar = new Guarantor();
                if (guar.LoadByPrimaryKey(reg.GuarantorID))
                {
                    hdnFornas.Value = (guar.IsItemRestrictionsFornas ?? false).ToString();
                    switch (reg.SRRegistrationType.ToUpper())
                    {
                        case "IPR":
                            {
                                if (!string.IsNullOrEmpty(guar.PrescriptionServiceUnitIdIPR) &&
                                    !string.IsNullOrEmpty(guar.PrescriptionLocationIdIPR))
                                {
                                    suid = guar.PrescriptionServiceUnitIdIPR;
                                    //locid = guar.PrescriptionLocationIdIPR;
                                }
                                break;
                            }
                        case "EMR":
                            {
                                if (!string.IsNullOrEmpty(guar.PrescriptionServiceUnitIdEMR) &&
                                    !string.IsNullOrEmpty(guar.PrescriptionLocationIdEMR))
                                {
                                    suid = guar.PrescriptionServiceUnitIdEMR;
                                    //locid = guar.PrescriptionLocationIdEMR;
                                }
                                break;
                            }
                        default:
                            {
                                if (!string.IsNullOrEmpty(guar.PrescriptionServiceUnitIdOPR) &&
                                    !string.IsNullOrEmpty(guar.PrescriptionLocationIdOPR))
                                {
                                    suid = guar.PrescriptionServiceUnitIdOPR;
                                    //locid = guar.PrescriptionLocationIdOPR;
                                }
                                break;
                            }
                    }

                    ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, Temiang.Avicenna.BusinessObject.Reference.TransactionCode.Prescription, false);
                    // Remove for not in parameter
                    var serviceUnitIdListForUdd = AppParameter.GetParameterValue(AppParameter.ParameterItem.ServiceUnitIdListForUdd);
                    if (string.IsNullOrEmpty(serviceUnitIdListForUdd))
                        serviceUnitIdListForUdd = AppParameter.GetParameterValue(AppParameter.ParameterItem.ServiceUnitPharmacyID);

                    if (!string.IsNullOrEmpty(serviceUnitIdListForUdd))
                    {
                        serviceUnitIdListForUdd = String.Concat(serviceUnitIdListForUdd, ";");
                        var itemSelecteds = new List<RadComboBoxItem>();
                        foreach (RadComboBoxItem item in cboServiceUnitID.Items)
                        {
                            if (!string.IsNullOrEmpty(item.Value) && serviceUnitIdListForUdd.Contains(String.Concat(item.Value, ";")))
                                itemSelecteds.Add(item);
                        }

                        cboServiceUnitID.Items.Clear();
                        cboServiceUnitID.Items.AddRange(itemSelecteds);
                    }

                    if (!string.IsNullOrWhiteSpace(suid))
                    {
                        ComboBox.SelectedValue(cboServiceUnitID, suid);
                        var locid = (new ServiceUnit()).GetMainLocationId(suid);
                        hdnLocationID.Value = locid;
                    }

                    //if (!string.IsNullOrWhiteSpace(locid))
                    //    hdnLocationID.Value = locid;

                }
            }
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            hdnLocationID.Value = (new ServiceUnit()).GetMainLocationId(cboServiceUnitID.SelectedValue); ;

            UddItems = null;
            grdUddItem.Rebind();
        }


        private bool? _isRasproEnableApplied = null;
        protected bool IsRasproEnableApplied
        {
            get
            {
                if (_isRasproEnableApplied == null)
                    _isRasproEnableApplied = AppSession.Parameter.IsRasproEnable && RegistrationCurrent.SRRegistrationType == AppConstant.RegistrationType.InPatient;

                return _isRasproEnableApplied ?? false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                litPatientAllergy.Text = PrescriptionEntry.PatientAllergy(PatientID);

                if (pnlRaspro.Visible)
                {

                    // JIKA ADA PERUBAHAN DISINI MAKA SESUAIKAN JUGA YANG DI PrescriptionEntry.aspx.cs

                    var rr = new RegistrationRaspro();
                    if (string.IsNullOrWhiteSpace(hdnRasproSeqNo.Value) || Session["RasproSeqNo"] != null) // Jika belum diload atau setelah create RASPRO baru
                    {
                        if (Session["RasproSeqNo"] != null)
                        {
                            // Ambil value RasproSeqNo, akan ada nilainya jika sehabis mengisi RASPRO Form
                            hdnRasproSeqNo.Value = Session["RasproSeqNo"].ToString();
                            hdnRasproType.Value = Session["SRRaspro"].ToString();

                            if (hdnRasproType.Value == "RASPRAJA")
                                UddItems = null; //Reset supaya reload

                            if (Session["RasproRefNo"] != null)
                            {
                                hdnRasproRefNo.Value = Session["RasproRefNo"].ToString();
                                Session.Remove("RasproRefNo");
                            }
                            //Reset
                            Session.Remove("RasproSeqNo");
                            Session.Remove("SRRaspro");


                            rr.LoadByPrimaryKey(RegistrationNo, hdnRasproSeqNo.Value.ToInt());

                            // Info dari form raspro baru
                            //hdnAbLevel.Value = rr.AntibioticLevel == 0 ? string.Empty : rr.AntibioticLevel.ToString();
                            //hdnAbRestrictionID.Value = rr.AbRestrictionID.ToString();
                            hdnRasproSeqNo4Filter.Value = "0"; //Harus "0" krn dipakai di lookUp Item
                            hdnRasproIsNew.Value = "1"; // Dipakai untuk proses save UddItem dan pemilihan AB utk Raspro Form
                        }
                        else
                        {
                            // Ambil raspro form terakhir
                            rr.Query.es.Top = 1;
                            rr.Query.OrderBy(rr.Query.SeqNo.Descending);
                            rr.Query.Where(rr.Query.RegistrationNo == RegistrationNo);
                            if (rr.Query.Load())
                            {
                                //hdnRasproSeqNo.Value = rr.SeqNo.ToString();
                                //hdnRasproType.Value = rr.SRRaspro;
                                //hdnRasproRefNo.Value = rr.ReferenceNo;
                                //hdnAbLevel.Value = rr.AntibioticLevel == 0 ? string.Empty : rr.AntibioticLevel.ToString();
                                //hdnAbRestrictionID.Value = rr.AbRestrictionID.ToString();

                                // Cek jika belum ada AB Itemnya anggap sebagai mode AB selection (from raspro baru)
                                var ritem = new RegistrationRasproItem();
                                ritem.Query.Where(ritem.Query.RegistrationNo == rr.RegistrationNo);
                                if (rr.SRRaspro == AppConstant.RasproType.Raspraja)
                                    ritem.Query.Where(ritem.Query.RasprajaSeqNo == rr.SeqNo);
                                else
                                    ritem.Query.Where(ritem.Query.RasproSeqNo == rr.SeqNo);

                                ritem.Query.es.Top = 1;
                                if (ritem.Query.Load())
                                {
                                    // 2. Cek jika AB belum di approve resepnya maka anggap sebagai mode AB selection (from raspro baru)
                                    var ritems = new RegistrationRasproItemCollection();
                                    ritems.Query.Where(ritem.Query.RegistrationNo == rr.RegistrationNo, ritem.Query.RasproSeqNo == rr.SeqNo);
                                    ritems.LoadAll();
                                    var rasproItems = new List<string>();
                                    foreach (var item in ritems)
                                    {
                                        rasproItems.Add(item.ItemID);
                                    }

                                    var tp = new TransPrescriptionQuery("tp");
                                    var tpi = new TransPrescriptionItemQuery("tpi");
                                    tp.InnerJoin(tpi).On(tp.PrescriptionNo == tpi.PrescriptionNo);
                                    tp.Where(tp.RegistrationNo == RegistrationNo, tp.PrescriptionDate > rr.RasproDateTime, tp.IsApproval == true, tpi.ItemID.In(rasproItems));
                                    tp.Select(tp.PrescriptionNo);
                                    tp.es.Top = 1;
                                    var dtbCheck = tp.LoadDataTable();
                                    if (dtbCheck.Rows.Count > 0)
                                        hdnRasproSeqNo4Filter.Value = hdnRasproSeqNo.Value; // Close mode new raspro
                                    else
                                        hdnRasproSeqNo4Filter.Value = "0"; // mode new raspro form
                                }

                                else
                                    hdnRasproSeqNo4Filter.Value = "0"; // mode new raspro form
                            }
                            hdnRasproIsNew.Value = hdnRasproSeqNo4Filter.Value == "0" ? "1" : "0";
                        }

                        if (rr != null && rr.RegistrationNo == RegistrationNo)
                        {
                            hdnAbLevel.Value = rr.AntibioticLevel == 0 ? string.Empty : rr.AntibioticLevel.ToString();

                            hdnAbRestrictionID.Value = String.Empty;
                            if (rr.AbRestrictionID != null)
                                hdnAbRestrictionID.Value = rr.AbRestrictionID.ToString();

                            hdnRasproSeqNo.Value = rr.SeqNo.ToString();

                            hdnRasproType.Value = rr.SRRaspro;
                            hdnRasproRefNo.Value = rr.ReferenceNo;
                        }
                    }
                    else
                    {
                        rr.LoadByPrimaryKey(RegistrationNo, hdnRasproSeqNo.Value.ToInt());
                    }

                    // Populate disini krn dijalankan juga dari rebind setelah isi popup raspro
                    if (!string.IsNullOrEmpty(hdnRasproSeqNo.Value))
                    {
                        var usedRasproSeqno = 0;
                        litAntibioticSuggest.Text = AbRestriction.AntibioticSuggestion(rr, ref usedRasproSeqno);
                        hdnRasproSeqNo4Filter.Value = usedRasproSeqno.ToString(); // Untuk filter lookup item

                        //Exam Order Lab Tr No (Rspro Culture)
                        grdLaboratoryCultureResult.Visible = false;
                        if (hdnRasproType.Value.Equals("RASPATUR") && !string.IsNullOrWhiteSpace(hdnRasproRefNo.Value))
                        {
                            grdLaboratoryCultureResult.Visible = true;
                            switch (AppSession.Parameter.LisInterop)
                            {
                                case "LINK_LIS":
                                    //string labNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ResultValue"]);
                                    //grdLaboratoryCultureResult.DataSource = MainContent.ExamOrderHistCtl.LaboratoryResult(RegistrationNo, transactionNo, labNo);
                                    break;
                                default:
                                    grdLaboratoryCultureResult.DataSource = MainContent.ExamOrderHistCtl.LaboratoryResult(RegistrationNo, hdnRasproRefNo.Value);
                                    break;
                            }
                            grdLaboratoryCultureResult.Rebind();
                        }

                    }
                }

            }
        }

        private void RefreshMenuRaspro(AppEnum.DataMode dataMode)
        {
            if (pnlRaspro.Visible == false) return;
            // Default
            lnkNewRasal.Enabled = false;
            lnkNewRaslan.Enabled = false;
            lnkNewRaspatur.Enabled = false;
            lnkNewRaspraja.Enabled = false;

            var rasproEnabled = dataMode == AppEnum.DataMode.Read && IsUserInParamedicTeam(RegistrationCurrent);
            if (rasproEnabled)
            {
                lnkNewRasal.Enabled = true;

                // Cek jika sudah dibuat rasal yg terisi infeksi nya maka disabled
                var lastrr = new RegistrationRaspro();
                lastrr.Query.Where(lastrr.Query.RegistrationNo == RegistrationNo, lastrr.Query.Or(lastrr.Query.SRRaspro == AppConstant.RasproType.Rasal, lastrr.Query.SRRaspro == AppConstant.RasproType.Raspatur), lastrr.Query.AbRestrictionID.IsNotNull());
                lastrr.Query.OrderBy(lastrr.Query.SeqNo.Descending);
                lastrr.Query.es.Top = 1;
                if (lastrr.Query.Load())
                    lnkNewRasal.Enabled = false;

                //RASLAN 
                lnkNewRaslan.Enabled = !lnkNewRasal.Enabled || !string.IsNullOrWhiteSpace(AntibioticRestrictionForLine);

                //RASPRAJA
                if (!lnkNewRasal.Enabled)
                {
                    // Cek raspraja exist
                    var rr = new RegistrationRaspro();
                    rr.Query.Where(rr.Query.RegistrationNo == RegistrationNo, rr.Query.SRRaspro == AppConstant.RasproType.Raspraja);
                    rr.Query.es.Top = 1;
                    if (!rr.Query.Load())
                    {
                        // Cek item timeout di raspro terakhir yg bukan profilaxis (raspro lama dioverride oleh raspro yg baru)
                        rr = new RegistrationRaspro();
                        rr.Query.Where(rr.Query.RegistrationNo == RegistrationNo, rr.Query.SRRaspro != AppConstant.RasproType.Prophylaxis);
                        rr.Query.es.Top = 1;
                        rr.Query.OrderBy(rr.Query.SeqNo.Descending);
                        if (rr.Query.Load())
                        {
                            var antibioticMaxConsumeDay = AppParameter.GetParameterValue(AppParameter.ParameterItem.AntibioticMaxConsumeDay).ToInt();
                            var rriColl = new RegistrationRasproItemCollection();
                            rriColl.Query.Where(rriColl.Query.RegistrationNo == rr.RegistrationNo, rriColl.Query.RasproSeqNo == rr.SeqNo, rriColl.Query.StopDateTime.IsNull());
                            rriColl.LoadAll();
                            foreach (var item in rriColl)
                            {
                                var consumeDayNo = MedicationReceiveUsed.ConsumedDay(item.RegistrationNo, item.ItemID,
                                    item.SRConsumeMethod, item.ConsumeQty, item.SRConsumeUnit);
                                if (consumeDayNo >= antibioticMaxConsumeDay)
                                {
                                    lnkNewRaspraja.Enabled = true;
                                    break;
                                }
                            }
                        }
                    }
                }

                //lnkNewRaspatur.Enabled = rasproEnabled && PrescriptionEntry.LabTestCultureResultExist(MergeRegistrations); //TODO: Enabled jika sudah ada hasil lab kultur atau ada dok attacth hasil kultur
                lnkNewRaspatur.Enabled = true; // Dibuat true dulu, pikirkan cara untuk hasil kultur dari lab luar

                //// Request Profilaksis muncul setelah ada booking kamar OK / VK
                //var subq = new ServiceUnitBookingQuery("a");
                //subq.Where(subq.RegistrationNo == RegistrationNo);
                //subq.es.Top = 1;
                //var sub = new ServiceUnitBooking();
                //if (sub.Load(subq) && !string.IsNullOrWhiteSpace(sub.RegistrationNo))
                //    lnkNewProphylaxis.Enabled = true;

                // Profilaksis hanya utuk AB yg akan dikonsumsi sebelum operasi bukan untuk UDD
            }

        }
        #region Record Detail Method Function UddItem

        private UddItemCollection UddItems
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collUddItem" + Request.UserHostName];
                    if (obj != null)
                        return ((UddItemCollection)(obj));
                }

                var coll = new UddItemCollection();

                var query = new UddItemQuery("a");
                var qItem = new ItemQuery("b");
                var cons = new ConsumeMethodQuery("cm");
                var emb = new EmbalaceQuery("e");
                var acdcpc = new AppStandardReferenceItemQuery("acdcpc");
                var rr = new RegistrationRasproQuery("rr");
                var ipm = new ItemProductMedicQuery("ipm");
                var par = new ParamedicQuery("par");
                var usr = new AppUserQuery("usr");
                query.Select
                    (
                        query,
                        qItem.ItemName.As("refToItem_ItemName"),
                         rr.SRRaspro.As("refToRR_SRRaspro"),
                          qItem.ItemName.As("refToItem_ItemName"),
                        "<(a.ParentNo + a.SequenceNo) as ORDERKEY>",
                        "<COALESCE(cm.SRConsumeMethodName,'') + ' ' + COALESCE(acdcpc.ItemName,'') as refToConsumeMethod_SRConsumeMethodName>",
                        emb.EmbalaceLabel.Coalesce("''").As("refToEmbalace_EmbalaceLabel"),
                        ipm.IsAntibiotic.As("refToIpm_IsAntibiotic"), "<CONVERT(BIT,1) as ref_IsOldRecord>", "<CONVERT(int,0) as ref_ConsumeDayNo>",
                        par.ParamedicName.Coalesce("''").As("refToParamedic_ParamedicName"),
                        usr.UserName.As("refToAppUser_UserName")
                    );
                query.InnerJoin(qItem).On(query.ItemID == qItem.ItemID);
                query.LeftJoin(cons).On(query.SRConsumeMethod == cons.SRConsumeMethod);
                query.LeftJoin(emb).On(query.EmbalaceID == emb.EmbalaceID);
                query.LeftJoin(acdcpc).On(query.AcPcDc == acdcpc.ItemID &&
                                          acdcpc.StandardReferenceID == AppEnum.StandardReference.MedicationConsume);
                query.LeftJoin(rr).On(query.RegistrationNo == rr.RegistrationNo & query.RasproSeqNo == rr.SeqNo);
                query.InnerJoin(ipm).On(query.ItemID == ipm.ItemID);
                query.LeftJoin(par).On(query.ParamedicID == par.ParamedicID);
                query.LeftJoin(usr).On(query.LastUpdateByUserID == usr.UserID);

                query.Where(query.RegistrationNo == RegistrationNo, query.LocationID == hdnLocationID.Value);
                query.OrderBy("ORDERKEY", esOrderByDirection.Ascending);
                coll.Load(query);

                // ConsumeDayNo diambil dari Realisasi Medication
                foreach (var item in coll)
                {
                    if (item.IsAntibiotic)
                    {
                        item.ConsumeDayNo = MedicationReceiveUsed.ConsumedDay(item.RegistrationNo, item.ItemID, item.SRConsumeMethod, item.ConsumeQty, item.SRConsumeUnit);
                        item.AcceptChanges(); // Supaya tidak disave kalau field lainnya tidak dirubah karena menimnbulkan pertanyaan dari dokter yg tidak akan merasa merubahnya (Handono 2025-02-27)
                    }
                }
                
                Session["collUddItem" + Request.UserHostName] = coll;
                return coll;
            }
            set
            { Session["collUddItem" + Request.UserHostName] = value; }
        }

        //protected void grdUddItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        //{

        //    if (pnlRaspro.Visible)
        //    {
        //        if (string.IsNullOrWhiteSpace(hdnRasproSeqNo.Value) || Session["RasproSeqNo"] != null) // Jika belum diload atau setelah create RASPRO baru
        //        {
        //            hdnRasproIsNew.Value = "0";
        //            if (Session["RasproSeqNo"] != null)
        //            {
        //                // Ambil value RasproSeqNo, akan ada nilainya jika sehabis mengisi RASPRO Form
        //                hdnRasproSeqNo.Value = Session["RasproSeqNo"].ToString();
        //                hdnRasproType.Value = Session["SRRaspro"].ToString();
        //                if (hdnRasproType.Value == "RASPRAJA")
        //                    UddItems = null;

        //                if (Session["RasproRefNo"] != null)
        //                {
        //                    hdnRasproRefNo.Value = Session["RasproRefNo"].ToString();
        //                    Session.Remove("RasproRefNo");
        //                }
        //                Session.Remove("RasproSeqNo");
        //                Session.Remove("SRRaspro");

        //                hdnRasproIsNew.Value = "1"; //Mode bisa pilih antibiotik dari master mapping restrictionnya
        //            }
        //            else
        //            {
        //                // Ambil raspro form terakhir
        //                var rr = new RegistrationRaspro();
        //                rr.Query.es.Top = 1;
        //                rr.Query.OrderBy(rr.Query.SeqNo.Descending);
        //                rr.Query.Where(rr.Query.RegistrationNo == RegistrationNo);
        //                if (rr.Query.Load())
        //                {
        //                    hdnRasproSeqNo.Value = rr.SeqNo.ToString();
        //                    hdnRasproType.Value = rr.SRRaspro;
        //                    hdnRasproRefNo.Value = rr.ReferenceNo;
        //                }
        //            }
        //        }

        //        // Populate disini krn dijalankan juga dari rebind setelah isi popup raspro
        //        var abLevel = 0;
        //        var abRestrictionID = string.Empty;

        //        if (!string.IsNullOrEmpty(hdnRasproSeqNo.Value))
        //        {
        //            var isRasproItemHasSelected = false;
        //            var isShowEditMenu = DataModeCurrent == AppEnum.DataMode.Read && UddItems.Count == 0 && hdnRasproIsNew.Value.Equals("1");
        //            litAntibioticSuggest.Text = PrescriptionEntry.AntibioticSuggestion(isShowEditMenu, RegistrationNo, hdnRasproSeqNo.Value.ToInt(), ref abLevel, ref abRestrictionID);

        //            if (!isRasproItemHasSelected)
        //                hdnRasproIsNew.Value = "1";

        //            //Exam Order Lab Tr No (Rspro Culture)
        //            grdLaboratoryCultureResult.Visible = false;
        //            if (hdnRasproType.Value.Equals("RASPATUR") && !string.IsNullOrWhiteSpace(hdnRasproRefNo.Value))
        //            {
        //                grdLaboratoryCultureResult.Visible = true;
        //                switch (AppSession.Parameter.LisInterop)
        //                {
        //                    case "LINK_LIS":
        //                        //string labNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ResultValue"]);
        //                        //grdLaboratoryCultureResult.DataSource = MainContent.ExamOrderHistCtl.LaboratoryResult(RegistrationNo, transactionNo, labNo);
        //                        break;
        //                    default:
        //                        grdLaboratoryCultureResult.DataSource = MainContent.ExamOrderHistCtl.LaboratoryResult(RegistrationNo, hdnRasproRefNo.Value);
        //                        break;
        //                }

        //                grdLaboratoryCultureResult.Rebind();
        //            }

        //        }
        //        hdnAbLevel.Value = abLevel == 0 ? string.Empty : abLevel.ToString();
        //        hdnAbRestrictionID.Value = abRestrictionID;

        //        grdUddItem.DataSource = UddItems; //Set setelahnya untuk load ulang jika lewat proses reset UddItems
        //    }
        //    else
        //        grdUddItem.DataSource = UddItems;
        //}

        protected void grdUddItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            // ZatActiveInteraction
            var itemIds = new List<string>();
            foreach (var item in UddItems)
            {
                if (!(item.IsStop ?? false))
                    itemIds.Add(item.ItemID);
            }
            lblZatActiveInteraction.Text = PrescriptionEntry.ZatActiveInteraction(itemIds);


            grdUddItem.DataSource = UddItems;


            if (pnlRaspro.Visible)
            {

                var rr = new RegistrationRaspro();
                if (rr.LoadByPrimaryKey(RegistrationNo, hdnRasproSeqNo.Value.ToInt()))
                {
                    // Cek apakah sudah didefinisikan AB nya
                    var abi = new RegistrationRasproItem();
                    abi.Query.es.Top = 1;
                    abi.Query.Where(abi.Query.RegistrationNo == RegistrationNo, abi.Query.RasproSeqNo == hdnRasproSeqNo.Value.ToInt());
                    var isAbExist = abi.Query.Load();

                    string linkEdit = isAbExist ? String.Empty : string.Format("&nbsp;&nbsp;<a href=\"#\" onclick=\"javascript:showRaspro('{0}', '{1}','edit'); return false;\"><img src=\"{2}/Images/Toolbar/edit16.png\"  alt=\"View\" /></a>", rr.SRRaspro, rr.SeqNo, Helper.UrlRoot());

                    var abr = new AbRestriction();
                    abr.LoadByPrimaryKey(rr.AbRestrictionID);

                    //TODO: Untuk info Profilaksis
                    litRasproInfo.Text = string.Format(@"<fieldset>
                <legend><b>LAST RASPRO FORM {4}</b></legend>
<strong>Type:</strong>&nbsp;{0}&nbsp;&nbsp;<strong>Create Date:</strong>&nbsp;{1}&nbsp;<strong>No:</strong>&nbsp;{2}&nbsp;<strong>Focus Infection:</strong>&nbsp;{3}<br />
            </fieldset>", rr.SRRaspro, rr.RasproDateTime.Value.ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute), rr.SeqNo, abr.AbRestrictionName, linkEdit);
                }
            }
        }

        protected void grdUddItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            var sequenceNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][UddItemMetadata.ColumnNames.SequenceNo]);
            var entity = FindUddItem(sequenceNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdUddItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][UddItemMetadata.ColumnNames.SequenceNo]);
            var entity = FindUddItem(sequenceNo);
            if (entity != null)
            {
                // Delete compound
                foreach (var prescItem in UddItems.Where(prescItem => sequenceNo.Equals(prescItem.ParentNo)))
                {
                    prescItem.MarkAsDeleted();
                }

                entity.MarkAsDeleted();
            }

            grdUddItem.Rebind();
        }

        protected void grdUddItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = UddItems.AddNew();
            entity.ParamedicID = AppSession.UserLogin.ParamedicID;
            SetEntityValue(entity, e);

            entity.RasproSeqNo = hdnRasproSeqNo.Value.ToInt();

            e.Canceled = true;
            grdUddItem.Rebind();
        }

        private UddItem FindUddItem(String sequenceNo)
        {
            return UddItems.FirstOrDefault(rec => rec.SequenceNo.Equals(sequenceNo));
        }

        private void SetEntityValue(UddItem entity, GridCommandEventArgs e)
        {
            var userControl = (UddItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.LastUpdateByUserName = AppSession.UserLogin.UserName;

                entity.RegistrationNo = RegistrationNo;
                entity.LocationID = hdnLocationID.Value;
                entity.SequenceNo = userControl.SequenceNo;
                entity.ParentNo = userControl.ParentNo;
                entity.IsRFlag = string.IsNullOrEmpty(userControl.ParentNo);
                entity.IsCompound = userControl.IsCompound;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.SRItemUnit = userControl.ItemUnit;
                entity.ItemQtyInString = userControl.ItemQtyInString;
                entity.IsUsingDosageUnit = false;
                entity.SRDosageUnit = userControl.DosageUnit;
                entity.FrequencyOfDosing = 0;
                entity.DosingPeriod = string.Empty;
                entity.NumberOfDosage = 0;
                entity.DurationOfDosing = 0;
                entity.AcPcDc = userControl.AcPcDc;
                entity.SRMedicationRoute = string.Empty;
                entity.PrescriptionQty = userControl.ResultQty;

                entity.EmbalaceID = userControl.EmbalaceID;
                entity.IsUseSweetener = false;
                entity.StartDateTime = userControl.StartDateTime;
                entity.Notes = userControl.Notes;
                entity.SRConsumeMethod = userControl.ConsumeMethod;
                entity.SRConsumeMethodName = string.Format("{0} {1}", userControl.ConsumeMethodName, userControl.AcPcDcName);
                entity.DosageQty = userControl.DosageQty;
                entity.EmbalaceQty = userControl.EmbalaceQty;
                entity.EmbalaceLabel = userControl.EmbalaceLabel;

                entity.ConsumeQty = userControl.QtyConsume;
                entity.SRConsumeUnit = userControl.SRConsumeUnit;
                entity.IsStop = false;
                entity.StartDateTime = userControl.StartDateTime;

                var ipm = new ItemProductMedic();
                ipm.LoadByPrimaryKey(entity.ItemID);
                entity.IsAntibiotic = ipm.IsAntibiotic ?? false;


            }
        }

        #endregion

        protected string GetItemName(object isRFlag, object itemName)
        {
            if (isRFlag.Equals(true))
                return itemName.ToString();
            return "&nbsp;&nbsp;&nbsp;" + itemName;
        }

        protected void grdUddItem_ItemCommand(object sender, GridCommandEventArgs e)
        {
            // Hanya 1 row edit atau insert
            if (e.CommandName == RadGrid.EditCommandName)
            {
                grdUddItem.MasterTableView.IsItemInserted = false;
            }
            if (e.CommandName == RadGrid.InitInsertCommandName)
            {
                grdUddItem.MasterTableView.ClearEditItems();
            }

            var cmdName = e.CommandName.ToLower();
            if (cmdName == "stop" || cmdName == "continue")
            {
                var item = e.Item as GridDataItem;
                if (item == null) return;

                var sequenceNo = e.CommandArgument.ToString();
                var entity = FindUddItem(sequenceNo);
                if (entity != null)
                {
                    // Stop compound
                    foreach (var prescItem in UddItems.Where(prescItem => sequenceNo.Equals(prescItem.ParentNo)))
                    {
                        prescItem.IsStop = (cmdName == "stop");
                        prescItem.LastUpdateByUserName = AppSession.UserLogin.UserName;
                    }

                    entity.IsStop = (cmdName == "stop");
                    entity.LastUpdateByUserName = AppSession.UserLogin.UserName;

                    if (entity.IsStop == true)
                        entity.StopDateTime = DateTime.Now;
                    else
                        entity.str.StartDateTime = String.Empty;
                }
                UddItems.Save();
                grdUddItem.Rebind();
            }
        }

        protected void grdUddItem_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;

                if ("True".Equals(item["IsStop"].Text))
                {
                    item.Font.Strikeout = true;
                }

                if ("True".Equals(item["IsAntibiotic"].Text))
                {
                    // Red utk AntibioticMaxConsumeDay
                    if (item["ConsumeDayNo"].Text.ToInt() >= _antibioticMaxConsumeDay)
                        item.ForeColor = System.Drawing.Color.Red;
                    else
                        item.ForeColor = System.Drawing.Color.Blue;
                }
                if (grdUddItem.Columns[0].Visible == false && ("True".Equals(item["IsOldRecord"].Text))) // Mode view saat bisa stop/continue
                {
                    var lnkStop = e.Item.FindControl("lnkStop") as LinkButton;
                    var lnkContinue = e.Item.FindControl("lnkContinue") as LinkButton;

                    if (item["ParentNo"].Text.Equals("&nbsp;")) // kosong
                    {
                        // Header Compound atau obat patent
                        lnkStop.Visible = true;
                        lnkContinue.Visible = true;
                    }
                    else
                    {
                        // Detil Compound
                        lnkStop.Visible = false;
                        lnkContinue.Visible = false;
                    }

                    // AB bisa di stop continue
                    //// Override
                    //if ("True".Equals(item["IsAntibiotic"].Text) && !hdnRasproSeqNo.Value.Equals(item["RasproSeqNo"].Text))
                    //{
                    //    lnkStop.Visible = false;
                    //    lnkContinue.Visible = false;
                    //}
                    //else
                    //{


                    // Switch menu
                    if ("True".Equals(item["IsStop"].Text))
                    {
                        lnkStop.Visible = false;
                    }
                    else
                    {
                        lnkContinue.Visible = false;
                    }
                    //}
                }


                // Menu Edit
                if (grdUddItem.Columns[0].Visible == true && ("True".Equals(item["IsOldRecord"].Text))) // Mode Edit
                {
                    if ("True".Equals(item["IsStop"].Text))
                    {
                        ((ImageButton)item["EditButton"].Controls[0]).Visible = false;
                        ((ImageButton)item["DeleteButton"].Controls[0]).Visible = false;
                    }
                    else if ("True".Equals(item["IsAntibiotic"].Text))
                    {
                        // AB bisa diedit selama belum dibuat tx prescriptionnya nya
                        var tp = new TransPrescriptionQuery("tp");
                        var tpi = new TransPrescriptionItemQuery("tpi");
                        tp.InnerJoin(tpi).On(tp.PrescriptionNo == tpi.PrescriptionNo);
                        tp.es.Top = 1;
                        tp.Select(tp);
                        tp.Where(tp.RegistrationNo == RegistrationNo, tpi.ItemID == item["ItemID"].Text,
                            tpi.SRConsumeMethod == item["SRConsumeMethod"].Text, tpi.ConsumeQty == item["ConsumeQty"].Text, tpi.SRConsumeUnit == item["SRConsumeUnit"].Text);
                        var tpEnt = new TransPrescription();

                        if (tpEnt.Load(tp))
                        {
                            ((ImageButton)item["EditButton"].Controls[0]).Visible = false;
                            ((ImageButton)item["DeleteButton"].Controls[0]).Visible = false;
                        }
                    }
                }
            }
        }


        protected void grdVitalSign_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdVitalSign.DataSource = VitalSign.VitalSignLastValue(RegistrationNo, hdnFromRegistrationNo.Value, true,
                DateTime.Now);
        }

        #region RASPRO enable
        private void SaveNewRegistrationRasproItem()
        {
            // Raspraja tidak menambah AB, hanya untuk memperpanjang masa peresepan saja
            // Dan yg tidak dipilih bisa dibuat untuk Raspraja yg lain
            if (hdnRasproType.Value == AppConstant.RasproType.Raspraja) return;

            string registrationNo = RegistrationNo;
            int rasproSeqNo = hdnRasproSeqNo.Value.ToInt();

            using (var trans = new esTransactionScope())
            {
                var udItem = new UddItemQuery("p");
                var udItems = new UddItemCollection();
                var qItemMedic = new ItemProductMedicQuery("im");
                udItem.InnerJoin(qItemMedic).On(udItem.ItemID == qItemMedic.ItemID);

                udItem.Where(udItem.RegistrationNo == registrationNo, udItem.RasproSeqNo == rasproSeqNo, qItemMedic.IsAntibiotic == true);
                udItems.Load(udItem);

                foreach (var udi in udItems)
                {
                    var za = new ItemProductMedicZatActive();
                    za.Query.Where(za.Query.ItemID == udi.ItemID);
                    za.Query.es.Top = 1; //TODO: bagaimana yg za nya >1
                    if (za.Query.Load())
                    {
                        //Jika ada Zat Active
                        var ritem = new RegistrationRasproItem();
                        if (!ritem.LoadByPrimaryKey(registrationNo, udi.RasproSeqNo.Value, udi.ItemID))
                        {
                            ritem = new RegistrationRasproItem();
                            ritem.ZatActiveID = za.ZatActiveID;
                            ritem.RegistrationNo = RegistrationNo;
                            ritem.RasproSeqNo = udi.RasproSeqNo;
                            ritem.ItemID = udi.ItemID;
                        }
                        ritem.ConsumeQty = udi.ConsumeQty;
                        ritem.SRConsumeUnit = udi.SRConsumeUnit;
                        ritem.SRItemUnit = udi.SRItemUnit;
                        ritem.SRDosageUnit = udi.SRDosageUnit;
                        ritem.AcPcDc = udi.AcPcDc;
                        ritem.StartDateTime = udi.StartDateTime;
                        ritem.SRConsumeMethod = udi.SRConsumeMethod;
                        ritem.SRMedicationRoute = udi.SRMedicationRoute;
                        ritem.DosageQty = udi.DosageQty;
                        ritem.EmbalaceQty = udi.EmbalaceQty;
                        ritem.StartDateTime = udi.StartDateTime;
                        ritem.Save();
                    }
                }

                trans.Complete();
            }
        }

        //private void UpdateStatusUddItem()
        //{
        //    // Untuk raslan akan men stop semua AB sebelumnya
        //    // Pada saat klik ok form raslan 
        //    // Karena AB yg diberikan harus dgn level stratifikasi yg sama yg dipilih di raspro form yg di add

        //    if (hdnRasproIsNew.Value == "1" && hdnRasproType.Value == "RASLAN")
        //    {
        //        using (var trans = new esTransactionScope())
        //        {
        //            var uddItems = new UddItemCollection();
        //            var qr = new UddItemQuery("ui");
        //            var qItem = new ItemProductMedicQuery("b");
        //            qr.InnerJoin(qItem).On(qr.ItemID == qItem.ItemID);
        //            qr.Where(qr.RegistrationNo == RegistrationNo, qr.RasproSeqNo < hdnRasproSeqNo.Value.ToInt(), qItem.IsAntibiotic == true);

        //            uddItems.Load(qr);
        //            foreach (var itemAB in uddItems)
        //            {
        //                if (itemAB.IsStop == true) continue;

        //                var uddItem = new UddItem();
        //                if (uddItem.LoadByPrimaryKey(itemAB.RegistrationNo, itemAB.LocationID, itemAB.SequenceNo))
        //                {
        //                    if (!string.IsNullOrEmpty(uddItem.ParentNo)) // Racikan
        //                    {
        //                        // Stop status di header nya
        //                        var uddCompound = new UddItem();
        //                        uddCompound.LoadByPrimaryKey(uddItem.RegistrationNo, uddItem.LocationID, uddItem.ParentNo);

        //                        uddCompound.IsStop = true;
        //                        uddCompound.StopDateTime = DateTime.Now;

        //                        uddCompound.Save();
        //                    }
        //                    uddItem.IsStop = true;
        //                    uddItem.StopDateTime = DateTime.Now;
        //                    uddItem.Save();
        //                }
        //            }
        //            trans.Complete();
        //        }
        //    }
        //}


        #endregion

    }
}
