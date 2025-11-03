using System;
using System.Collections;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    /// <summary>
    /// Entrian Diagnosa AKhir untuk Soap Rawat Jalan dan Discharge Summary 
    /// </summary>
    /// ------------------------------------------------------------
    /// Created By : Handono (Timika Desmber 2019)
    /// -------------------------------------------------------------
    /// Handono 230825:
    /// #TK-572 RSSTJ - Pada Asesmen Dokter, kolom ICD X bagian Diagnose (deskripsi) dibuat read only
    /// Handono 230921:
    /// #TK-567 RSSTJ - Penambahan parameter mandatory untuk kolom ICD X 
    /// Handono 231109: (base RSI req) 
    /// - Row pertama bisa untuk selain Main Diagnose krn Main Diagnose biasanya hanya untuk dientry DPJP saja sehingga untuk dokter lain jadi bermasalah
    /// - Jika belum ada main diagnose maka untuk DPJP default yg muncul adalah main diagnose dan sealinnya adalah secondary diagnose
    /// - Main diagnose hanya bisa disi 1 row
    /// -------------------------------------------------------------
    public partial class EpisodeDiagnoseCtl : System.Web.UI.UserControl
    {
        private string _sessionName = null;
        private string SessionName
        {
            get
            {
                if (_sessionName == null)
                    _sessionName = string.Format("findiag_{0}", Helper.PageID(this.Page));
                return _sessionName;
            }
        }

        public bool IsDischargeSummaryMode
        {
            get
            {
                if (ViewState["idm"] == null)
                {
                    return false;
                }
                else
                    return (bool)ViewState["idm"];
            }
            set
            {
                ViewState["idm"] = value;
            }
        }

        //private RadTextBox TxtRegistrationNo
        //{
        //    get
        //    { return (RadTextBox)Helper.FindControlRecursive(Page, "txtRegistrationNo"); }
        //}

        private RadComboBox cboSynonym
        {
            get { return (RadComboBox)Helper.FindControlRecursive(Page, "cboSynonym"); }
        }

        public void Save(ValidateArgs args)
        {
            System.Data.DataTable dtbLast = null;
            var lastSeqNo = string.Empty;
            var diags = EpisodeDiagnoses;

            // Add IsEmrIcdXListMandatory parameter (Handono 231003)
            var isIcdXListMandatorByPass = true;
            if (diags.Count == 0 && AppParameter.IsYes(AppParameter.ParameterItem.IsEmrIcdXListMandatory))
            {
                // Ignore ICD X Mandatory if current page use autosave (Req by Imel 231003)
                if (this.Page is BasePageDialogEntry)
                {
                    var page = (BasePageDialogEntry)this.Page;
                    if (!(page.ToolBarMenuAutoSaveVisible && page.AutoSaveInterval > 0))
                    {
                        isIcdXListMandatorByPass = false;
                    }
                }
                else
                {
                    isIcdXListMandatorByPass = false;
                }
            }

            if (!isIcdXListMandatorByPass)
            {
                args.IsCancel = true;
                args.MessageText = "Please entry ICD X first";
                return;
            }

            foreach (var ed in diags)
            {
                if (ed.es.IsDeleted)
                {
                    if (IsDischargeSummaryMode)
                    {
                        var mdsd = new MedicalDischargeSummaryDiagnose();
                        if (mdsd.LoadByPrimaryKey(ed.GetOriginalColumnValue("RegistrationNo").ToString(), ed.GetOriginalColumnValue("SequenceNo").ToString()))
                        {
                            mdsd.MarkAsDeleted();
                            mdsd.Save();
                        }
                    }
                    continue;
                }

                if (ed.es.IsAdded && string.IsNullOrEmpty(ed.SRDiagnoseType) && string.IsNullOrEmpty(ed.DiagnosisText))
                {
                    // Cek ulang SequenceNo terakhir di DB utk mencegah error jika ada user lain yg nyalip insert Diagnosa nya
                    if (dtbLast == null && string.IsNullOrEmpty(lastSeqNo))
                    {
                        var qr = new EpisodeDiagnoseQuery("ed");
                        qr.Select(qr.SequenceNo);
                        qr.Where(qr.RegistrationNo == RegistrationNo);
                        qr.OrderBy("<CONVERT(INT,ed.SequenceNo) DESC>"); // Gunakan convert krn isi SequenceNo ternyata tidak standard menggunakan prefix 0 (Handono 231004)
                        qr.es.Top = 1;

                        dtbLast = qr.LoadDataTable();
                        if (dtbLast.Rows.Count > 0)
                            lastSeqNo = dtbLast.Rows[0][0].ToString();
                        else
                            lastSeqNo = "000";
                    }
                    var newSeqNo = (lastSeqNo == null || string.IsNullOrEmpty(lastSeqNo))
                        ? "001"
                        : string.Format("{0:000}", int.Parse(lastSeqNo) + 1);
                    ed.SequenceNo = newSeqNo;
                    lastSeqNo = newSeqNo;

                }

                if (IsDischargeSummaryMode && string.IsNullOrEmpty(ed.SRDiagnoseType) && string.IsNullOrEmpty(ed.DiagnosisText))
                {
                    var mdsd = new MedicalDischargeSummaryDiagnose();
                    if (!mdsd.LoadByPrimaryKey(ed.RegistrationNo, ed.SequenceNo))
                    {
                        mdsd = new MedicalDischargeSummaryDiagnose();
                        mdsd.RegistrationNo = ed.RegistrationNo;
                        mdsd.SequenceNo = ed.SequenceNo;
                    }
                    mdsd.DiagnoseID = ed.DiagnoseID;
                    mdsd.DiagnosisText = ed.DiagnosisText;
                    mdsd.SRDiagnoseType = ed.SRDiagnoseType;
                    mdsd.ExternalCauseID = ed.ExternalCauseID;
                    mdsd.IsOldCase = ed.IsOldCase ?? false; //Not Nullable
                    mdsd.IsVoid = ed.IsVoid ?? false;
                    mdsd.LastUpdateDateTime = ed.LastUpdateDateTime;
                    mdsd.LastUpdateByUserID = ed.LastUpdateByUserID;
                    mdsd.DiagnoseSynonym = ed.DiagnoseSynonym;
                    mdsd.Save();
                }

                if (ed.es.IsAdded && (string.IsNullOrEmpty(ed.SRDiagnoseType) ||
                                      string.IsNullOrEmpty(ed.DiagnosisText)))
                {
                    ed.MarkAsDeleted();
                }
            }

            diags.Save();
        }
        public void ImportWorkDiagnose(string registrationNo, bool resetFinalDiag)
        {
            // Cegah import jika sudah ada, resetFinalDiag digunakan pada lbtnResetFinalDiag tetapi tetap hanya untuk yg beum pernah import
            if (EpisodeDiagnoses.Count > 0) return;

            var eds = EpisodeDiagnoses;
            if (resetFinalDiag)
            {
                foreach (var ed in eds)
                {
                    if (ed.es.IsDeleted) continue;
                    if (ed.es.IsAdded)
                        ed.MarkAsDeleted();
                    else
                        ed.IsVoid = true;
                }
            }

            // Import last 1 Main diagnose
            var workDiags = new RegistrationInfoMedicDiagnoseCollection();
            var wdQr = new RegistrationInfoMedicDiagnoseQuery("wd");
            wdQr.Where(wdQr.RegistrationNo == registrationNo, wdQr.IsVoid == false,
                wdQr.SRDiagnoseType == AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            wdQr.OrderBy(wdQr.DiagnoseDateTime.Descending);
            wdQr.es.Top = 1;
            workDiags.Load(wdQr);

            var mainSeqNo = string.Empty;
            if (workDiags.Count > 0)
            {
                var wdiag = workDiags[0];
                var ed = CreateNewDiagnose(eds);
                ed.SRDiagnoseType = wdiag.SRDiagnoseType;
                ed.DiagnoseID = wdiag.DiagnoseID;
                ed.DiagnosisText = wdiag.DiagnosisText;
                ed.ExternalCauseID = wdiag.ExternalCauseID;
                ed.DiagnoseType =
                    StandardReference.GetItemName(AppEnum.StandardReference.DiagnoseType, wdiag.SRDiagnoseType);
                ed.DiagnoseSynonym = wdiag.DiagnoseSynonym;

                mainSeqNo = wdiag.SequenceNo;
            }

            // Other diagnose
            workDiags = new RegistrationInfoMedicDiagnoseCollection();
            wdQr = new RegistrationInfoMedicDiagnoseQuery("wd");
            wdQr.Where(wdQr.RegistrationNo == registrationNo,
                wdQr.IsVoid == false,
                wdQr.SRDiagnoseType != AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain),
                wdQr.SequenceNo != mainSeqNo);
            wdQr.OrderBy(wdQr.SRDiagnoseType.Ascending, wdQr.DiagnoseID.Ascending, wdQr.ExternalCauseID.Ascending);
            wdQr.es.Distinct = true;
            wdQr.Select(wdQr.SRDiagnoseType, wdQr.DiagnoseID, wdQr.ExternalCauseID);
            workDiags.Load(wdQr);
            if (workDiags.Count > 0)
            {
                foreach (var wdiag in workDiags)
                {
                    var ed = CreateNewDiagnose(eds);
                    ed.SRDiagnoseType = wdiag.SRDiagnoseType;
                    ed.DiagnoseID = wdiag.DiagnoseID;
                    ed.DiagnosisText = wdiag.DiagnoseID;
                    ed.ExternalCauseID = wdiag.ExternalCauseID;
                    ed.DiagnoseType =
                        StandardReference.GetItemName(AppEnum.StandardReference.DiagnoseType, wdiag.SRDiagnoseType);
                    ed.DiagnoseSynonym = wdiag.DiagnoseSynonym;
                }
            }

            grdDiagnose.Rebind();
        }

        public void Rebind(bool isEdited)
        {
            //Toogle grid command
            grdDiagnose.Columns[0].Visible = isEdited;
            grdDiagnose.Columns[grdDiagnose.Columns.Count - 1].Visible = isEdited;

            grdDiagnose.MasterTableView.CommandItemDisplay = isEdited
                ? GridCommandItemDisplay.Top
                : GridCommandItemDisplay.None;

            if (!isEdited)
            {
                grdDiagnose.MasterTableView.ClearEditItems();
                grdDiagnose.MasterTableView.IsItemInserted = false;
            }
            else
            {
                // Insert Mode
                grdDiagnose.MasterTableView.IsItemInserted = true;
            }

            //Perbaharui tampilan dan data
            grdDiagnose.Rebind();
        }
        private void FixDiagnoseEntry(EpisodeDiagnose ep, GridEditableItem editableItem)
        {
            ep.IsOldCase = ((RadCheckBox)editableItem.FindControl("chkIsOldCase")).Checked; // Kagak bisa pakai Bind jadi harus dicari sendiri valuenya
            ep.IsOldCase = ep.IsOldCase ?? false; //Not Nullable

            if (!string.IsNullOrEmpty(ep.DiagnoseID))
            {
                var icd = new Diagnose();
                if (icd.LoadByPrimaryKey(ep.DiagnoseID))
                {
                    if (string.IsNullOrWhiteSpace(ep.DiagnosisText))
                        ep.DiagnosisText = icd.DiagnoseName;
                }
                else
                    ep.DiagnoseID = string.Empty;

            }

            if (!string.IsNullOrEmpty(ep.ExternalCauseID))
            {
                var icd = new Diagnose();
                if (icd.LoadByPrimaryKey(ep.ExternalCauseID))
                {
                    ep.ExternalCauseName = icd.DiagnoseName;
                }
                else
                    ep.ExternalCauseName = string.Empty;
            }
            else
                ep.str.ExternalCauseID = string.Empty; //(Set NULL yg kosong)


            ep.Notes = ep.DiagnosisText; //Yg diisi / diplih dokter simpan di Note juga Jika bagian Coding merubah code maka catatan dokter tidak hilang
            ep.DiagnoseType = StandardReference.GetItemName(AppEnum.StandardReference.DiagnoseType, ep.SRDiagnoseType);
            ep.DiagnoseSynonym = ep.DiagnoseSynonym;


            ////Nurul - Tambahan: Untuk baca ceklis OldCase, Jika Diagnosa sama dengan Diagnosa terakhir, OldCase akan terceklis
            //var reg = new Registration();

            //if (reg.LoadByPrimaryKey(RegistrationNo))
            //{
            //    var epColl = new EpisodeDiagnoseCollection();
            //    var epq = new EpisodeDiagnoseQuery("epq");
            //    var regq = new RegistrationQuery("regq");

            //    epq.InnerJoin(regq).On(epq.RegistrationNo == regq.RegistrationNo)
            //        .Where(
            //            regq.PatientID == reg.PatientID,
            //            epq.SRDiagnoseType == AppSession.Parameter.DiagnoseTypeMain,
            //            epq.RegistrationNo != RegistrationNo
            //        )
            //        .OrderBy(regq.RegistrationDate.Descending, epq.SequenceNo.Descending);

            //    epq.es.Top = 1;

            //    if (epColl.Load(epq))
            //    {
            //        ep.IsOldCase = (epColl.First().DiagnoseID == ep.DiagnoseID);
            //        //chkIsOldCase.Checked = (epColl.First().DiagnoseID == ep.DiagnoseID);
            //    }
            //}

        }
        private string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }


        protected void grdDiagnose_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack || Session[SessionName] == null) // || !RegistrationNo.Equals(Session["edsRegNo"]))
            {
                var qr = new EpisodeDiagnoseQuery("ed");
                var stdi = new AppStandardReferenceItemQuery("sdti");
                qr.InnerJoin(stdi).On(qr.SRDiagnoseType == stdi.ItemID & stdi.StandardReferenceID == "DiagnoseType");
                var ec = new DiagnoseQuery("ec");
                qr.LeftJoin(ec).On(qr.ExternalCauseID == ec.DiagnoseID);
                qr.Select(qr, stdi.ItemName.As("refToAppStandardReferenceItem_SRDiagnoseType"), ec.DiagnoseName.As("refToDiagnose_DiagnoseName4Ec"));

                qr.Where(qr.RegistrationNo == RegistrationNo);
                qr.OrderBy(qr.SequenceNo.Ascending);
                var coll = new EpisodeDiagnoseCollection();
                coll.Load(qr);
                EpisodeDiagnoses = coll;

                if (IsDischargeSummaryMode)
                {
                    // Update dengan MedicalDischargeSummaryDiagnose
                    foreach (var ed in EpisodeDiagnoses)
                    {
                        var mdsd = new MedicalDischargeSummaryDiagnose();
                        if (mdsd.LoadByPrimaryKey(ed.RegistrationNo, ed.SequenceNo))
                        {
                            ed.DiagnoseID = mdsd.DiagnoseID;
                            ed.DiagnosisText = mdsd.DiagnosisText;
                            ed.IsOldCase = mdsd.IsOldCase ?? false; //Not Nullable
                            ed.ExternalCauseID = mdsd.ExternalCauseID;
                            ed.SRDiagnoseType = mdsd.SRDiagnoseType;
                            ed.IsVoid = mdsd.IsVoid;
                            ed.DiagnoseSynonym = mdsd.DiagnoseSynonym;
                        }
                    }
                }

                //Session["edsRegNo"] = RegistrationNo;
            }



            grdDiagnose.DataSource = EpisodeDiagnoses;
        }
        private EpisodeDiagnoseCollection EpisodeDiagnoses
        {
            get
            {
                if (Session[SessionName] == null)
                    return null;

                return (EpisodeDiagnoseCollection)Session[SessionName];
            }
            set
            {
                Session[SessionName] = value;
            }
        }
        private EpisodeDiagnose CreateNewDiagnose(EpisodeDiagnoseCollection episodeDiagnoses)
        {
            var ed = episodeDiagnoses.AddNew();
            ed.DiagnoseID = string.Empty;
            ed.DiagnoseSynonym = string.Empty;

            var lastSeqNo = string.Empty;
            foreach (var item in episodeDiagnoses)
            {
                if (!string.IsNullOrEmpty(item.SequenceNo))
                    lastSeqNo = item.SequenceNo;
            }

            var newSeqNo = (lastSeqNo == null || string.IsNullOrEmpty(lastSeqNo))
                ? "001"
                : string.Format("{0:000}", int.Parse(lastSeqNo) + 1);
            ed.SequenceNo = newSeqNo;
            if (ed.SequenceNo == "001")
                ed.SRDiagnoseType = AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain);
            else
                ed.SRDiagnoseType = string.Empty;

            ed.RegistrationNo = RegistrationNo;
            ed.MorphologyID = string.Empty;
            ed.ParamedicID = AppSession.UserLogin.ParamedicID;
            ed.IsAcuteDisease = false;
            ed.IsChronicDisease = false;
            ed.IsConfirmed = false;
            ed.IsVoid = false;

            return ed;
        }
        protected void grdDiagnose_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var editableItem = ((GridEditableItem)e.Item);

            //Last entity
            var ep = CreateNewDiagnose(EpisodeDiagnoses);

            //populate its properties
            Hashtable values = new Hashtable();
            editableItem.ExtractValues(values);
            ep.DiagnoseID = (string)values["DiagnoseID"];
            ep.SRDiagnoseType = (string)values["SRDiagnoseType"];
            ep.DiagnosisText = (string)values["DiagnosisText"];
            ep.ExternalCauseID = (string)values["ExternalCauseID"];
            ep.DiagnoseSynonym = (string)values["DiagnoseSynonym"];

            // Lengkapi
            FixDiagnoseEntry(ep, editableItem);

            // Show hasil insert
            grdDiagnose.Rebind();

        }
        protected void grdDiagnose_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var editableItem = ((GridEditableItem)e.Item);
            var sequenceNo = editableItem.GetDataKeyValue("SequenceNo").ToString();
            var ep = EpisodeDiagnoses.FirstOrDefault(n => n.SequenceNo == sequenceNo);
            if (ep != null)
            {
                //update entity's state
                editableItem.UpdateValues(ep);

                // Lengkapi
                FixDiagnoseEntry(ep, editableItem);

            }
            grdDiagnose.Rebind();

        }
        protected void grdDiagnose_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var sequenceNo = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["SequenceNo"].ToString();
            var ep = EpisodeDiagnoses.FirstOrDefault(n => n.SequenceNo == sequenceNo);
            if (ep != null)
            {
                if (ep.es.IsAdded)
                    ep.MarkAsDeleted();
                else
                    ep.IsVoid = !ep.IsVoid;
            }
        }
        protected void grdDiagnose_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {

                var editItem = (GridEditableItem)e.Item;
                var sequenceNo = string.Empty;
                var diagTypeCurrentRow = string.Empty;
                if (e.Item.ItemIndex >= 0) // Not Insert
                {
                    sequenceNo = EpisodeDiagnoses[e.Item.ItemIndex].SequenceNo;
                    diagTypeCurrentRow = EpisodeDiagnoses[e.Item.ItemIndex].SRDiagnoseType;

                    ComboBox.PopulateWithDiagnoseSynonym(cboSynonym, EpisodeDiagnoses[e.Item.ItemIndex].DiagnoseID);
                }

                // cboSRDiagnoseType
                var cbo = (RadDropDownList)editItem.FindControl("cboSRDiagnoseType");
                var diagnoseTypeMain = AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain);

                cbo.Items.Clear();
                var coll = StandardReference.LoadStandardReferenceItemCollection(AppEnum.StandardReference.DiagnoseType);

                //var isExistMainDiag = false;
                //foreach (var diag in EpisodeDiagnoses)
                //{
                //    if (diag.SRDiagnoseType == diagnoseTypeMain && (diag.IsVoid ?? false) == false)
                //    {
                //        isExistMainDiag = true;
                //        break;
                //    }
                //}

                //foreach (var line in coll)
                //{
                //    if (diagnoseTypeMain.ToLower() == line.ItemID.ToLower() && (sequenceNo == "001" || !isExistMainDiag))
                //    {
                //        // Baris pertama untuk Main Diagnose
                //        cbo.Items.Add(new DropDownListItem(line.ItemName, line.ItemID));
                //        ComboBox.SelectedValue(cbo, diagnoseTypeMain);
                //        break;
                //    }

                //    if (diagnoseTypeMain.ToLower() != line.ItemID.ToLower())
                //    {
                //        cbo.Items.Add(new DropDownListItem(line.ItemName, line.ItemID));
                //    }
                //}

                //ComboBox.SelectedValue(cbo, diagType);


                // Dokter bebas memilih tipe diagnose karena Main Diagnose biasanya hanya diisi oleh DPJP
                // Main Daignose hanya bisa diisi 1 baris
                // (Handono 231108 req by RSI)
                var isExistMainDiagnose = false;
                foreach (var diag in EpisodeDiagnoses)
                {
                    if (diagnoseTypeMain.Equals(diag.SRDiagnoseType) && (diag.IsVoid ?? false) == false)
                    {
                        isExistMainDiagnose = true;
                        break;
                    }
                }

                var diagnoseTypeSecondary = string.Empty;
                foreach (var line in coll)
                {
                    if (diagnoseTypeMain.ToLower() == line.ItemID.ToLower())
                    {
                        if (!isExistMainDiagnose || diagnoseTypeMain.Equals(diagTypeCurrentRow))
                            cbo.Items.Add(new DropDownListItem(line.ItemName, line.ItemID));
                    }
                    else
                    {
                        cbo.Items.Add(new DropDownListItem(line.ItemName, line.ItemID));
                    }

                    if (line.ItemName.ToLower().Contains("secondary") || line.ItemName.ToLower().Contains("sekunder") || line.ItemName.ToLower().Contains("kedua"))
                        diagnoseTypeSecondary = line.ItemID;
                }

                // Default diagnose type 
                if (!isExistMainDiagnose && string.IsNullOrEmpty(diagTypeCurrentRow))
                {
                    // DPJP: default Main Diagnose
                    // Non DPJP: default DiagnoseTypeSecondary
                    if (ParamedicTeam.IsParamedicTeamStatusDpjp(RegistrationNo,AppSession.UserLogin.ParamedicID))
                        ComboBox.SelectedValue(cbo, diagnoseTypeMain);
                    else
                        ComboBox.SelectedValue(cbo, diagnoseTypeSecondary);
                }
                else
                    ComboBox.SelectedValue(cbo, diagTypeCurrentRow);

                // #TK-572 RSSTJ - Pada Asesmen Dokter, kolom ICD X bagian Diagnose (deskripsi) dibuat read only (Handono 230825)
                // #TK-567 RSSTJ - Penambahan parameter mandatory untuk kolom ICD X (Handono 230921)
                if (AppParameter.IsYes(AppParameter.ParameterItem.IsEmrIcdXTextReadOnly))
                {
                    var txtDiagnosisText = (RadTextBox)editItem.FindControl("txtDiagnosisText");
                    txtDiagnosisText.ReadOnly = true;
                }
                else
                {
                    var rfvDiagnoseID = (RequiredFieldValidator)editItem.FindControl("rfvDiagnoseID");
                    rfvDiagnoseID.ValidationGroup = "none";
                }
            }
        }

        protected void grdDiagnose_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            RadGrid grid = (sender as RadGrid);
            if (e.CommandName == RadGrid.InitInsertCommandName) //"Add new" button clicked
            {
                grid.MasterTableView.ClearEditItems(); // Close Edit Template
            }
            else if (e.CommandName == RadGrid.EditCommandName)
            {
                e.Item.OwnerTableView.IsItemInserted = false;  // Close Insert Template
            }

        }
        protected string ExternalCauseNameValue(GridItem gdi)
        {
            try
            {
                return DataBinder.Eval(gdi.DataItem, "ExternalCauseName").ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        protected void cboDiagnoseID_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value))
            {
                cboSynonym.Items.Clear();
                cboSynonym.SelectedValue = string.Empty;
                cboSynonym.Text = string.Empty;

                return;
            }

            ComboBox.PopulateWithDiagnoseSynonym(cboSynonym, e.Value);
            cboSynonym.SelectedIndex = 1;
        }

        protected void chkIsOldCase_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkIsOldCase.Checked)
            //{
            //    var reg = new Registration();
            //    reg.LoadByPrimaryKey(RegistrationNo);
            //    var patId = reg.PatientID;

            //    var qreg = new RegistrationQuery();
            //    qreg.es.Top = 1;
            //    qreg.Where(qreg.PatientID == patId, qreg.RegistrationNo < RegistrationNo);
            //    qreg.OrderBy(qreg.RegistrationNo.Descending);

            //    reg = new Registration();
            //    if (reg.Load(qreg))
            //    {

            //        var epd = new EpisodeDiagnose();
            //        var qepd = new EpisodeDiagnoseQuery();
            //        qepd.Where(qepd.RegistrationNo == reg.RegistrationNo, qepd.SRDiagnoseType == cboSRDiagnoseType.SelectedValue);
            //        qepd.es.Top = 1;
            //        if (epd.Load(qepd))
            //        {


            //            ComboBox.PopulateWithOneDiagnose(cboDiagnoseID, epd.DiagnoseID);
            //            txtDiagnoseText.Text = epd.DiagnosisText;

            //            ComboBox.PopulateWithOneDiagnose(cbo)

            //        }

            //    }
            //}

        }
    }
}