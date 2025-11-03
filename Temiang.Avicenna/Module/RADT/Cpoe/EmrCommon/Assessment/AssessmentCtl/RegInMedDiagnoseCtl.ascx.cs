using System;
using System.Collections;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.UI.Diagram;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    /// <summary>
    /// Entrian Diagnosa untuk Rawat Inap
    /// </summary>
    /// Diagnosa Rawat Inap terdiri dari :
    /// 1. Diagnosa Awal : direkan ke RegistrationInfoMedicDiagnose 
    /// 2. Diagnosa Kerja : direkan ke RegistrationInfoMedicDiagnose
    /// 3. Diagnosa Akhir : direkan ke EpisodeDiagnose di entrian MEdical Discharge Summary
    /// ------------------------------------------------------------
    /// Created By : Handono (Timika Desember 2019)
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
    public partial class RegInMedDiagnoseCtl : System.Web.UI.UserControl
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
        private RadComboBox cboSynonym
        {
            get { return (RadComboBox)Helper.FindControlRecursive(Page, "cboSynonym"); }
        }
        public void Save(ValidateArgs args, string registrationNo, string rimid, string paramedicID, DateTime diagDateTime)
        {
            // Save Work Diagnose
            var diags = RegistrationInfoMedicDiagnoses;

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

            System.Data.DataTable dtbLast = null;
            var lastSeqNo = string.Empty;

            foreach (var ed in diags)
            {
                if (ed.es.IsDeleted) continue;
                if (ed.es.IsAdded && (string.IsNullOrEmpty(ed.SRDiagnoseType) ||
                                      string.IsNullOrEmpty(ed.DiagnosisText)))
                {
                    ed.MarkAsDeleted();
                }
                else if (ed.es.IsAdded)
                {
                    // Set Header Value
                    ed.RegistrationInfoMedicID = rimid;
                    ed.DiagnoseDateTime = diagDateTime;
                    ed.RegistrationNo = registrationNo;
                    ed.ParamedicID = paramedicID;

                    // Cek ulang SequenceNo terakhir di DB utk mencegah error jika ada user lain yg nyalip insert Diagnosa nya
                    if (dtbLast == null && string.IsNullOrEmpty(lastSeqNo))
                    {
                        var qr = new RegistrationInfoMedicDiagnoseQuery("ed");
                        qr.Select(qr.SequenceNo);
                        qr.Where(qr.RegistrationInfoMedicID == rimid);
                        qr.OrderBy(qr.SequenceNo.Descending);
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
            }
            diags.Save();
        }

        public void Rebind(string rimid, bool isEdited)
        {
            hdnRegistrationInfoMedicID.Value = rimid;

            // Load Data
            RegistrationInfoMedicDiagnoses = LoadRegistrationInfoMedicDiagnoses(rimid);

            //Toogle grid command
            RefreshMenu(isEdited);
        }

        public void RefreshMenu(bool isEdited)
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

        private void FixDiagnoseEntry(RegistrationInfoMedicDiagnose ep, GridEditableItem editableItem)
        {
            ep.IsOldCase = ((RadCheckBox)editableItem.FindControl("chkIsOldCase")).Checked; // Kagak bisa pakai Bind jadi harus dicari sendiri valuenya

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
            //            epq.RegistrationNo != RegistrationNo,
            //            regq.IsTransferedToInpatient == false
            //        )
            //        .OrderBy(regq.RegistrationDate.Descending);

            //    epq.es.Top = 1;

            //    if (epColl.Load(epq))
            //    {
            //        ep.IsOldCase = (epColl.First().DiagnoseID == ep.DiagnoseID);
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
            //// Load Data
            //if (!IsPostBack || RegistrationInfoMedicDiagnoses == null)
            //{
            //    RegistrationInfoMedicDiagnoses = LoadRegistrationInfoMedicDiagnoses(hdnRegistrationInfoMedicID.Value);
            //}

            grdDiagnose.DataSource = RegistrationInfoMedicDiagnoses;
        }

        private RegistrationInfoMedicDiagnoseCollection LoadRegistrationInfoMedicDiagnoses(string rimid)
        {
            var qr = new RegistrationInfoMedicDiagnoseQuery("ed");
            var stdi = new AppStandardReferenceItemQuery("sdti");
            qr.InnerJoin(stdi).On(qr.SRDiagnoseType == stdi.ItemID & stdi.StandardReferenceID == "DiagnoseType");
            qr.Select(qr, stdi.ItemName.As("refToAppStandardReferenceItem_SRDiagnoseType"));

            qr.Where(qr.RegistrationInfoMedicID == rimid);
            qr.OrderBy(qr.SequenceNo.Ascending);


            var coll = new RegistrationInfoMedicDiagnoseCollection();
            coll.Load(qr);
            return coll;
        }

        private RegistrationInfoMedicDiagnoseCollection RegistrationInfoMedicDiagnoses
        {
            get
            {
                if (Session[SessionName] == null)
                    return null;

                return (RegistrationInfoMedicDiagnoseCollection)Session[SessionName];
            }
            set
            {
                Session[SessionName] = value;
            }
        }
        private RegistrationInfoMedicDiagnose CreateNewDiagnose(RegistrationInfoMedicDiagnoseCollection episodeDiagnoses)
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

            ed.IsVoid = false;

            return ed;
        }
        protected void grdDiagnose_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var editableItem = ((GridEditableItem)e.Item);
            Hashtable values = new Hashtable();
            editableItem.ExtractValues(values);

            //Last entity
            var ep = CreateNewDiagnose(RegistrationInfoMedicDiagnoses);

            ep.DiagnoseID = (string)values["DiagnoseID"];
            ep.SRDiagnoseType = (string)values["SRDiagnoseType"];
            ep.DiagnosisText = (string)values["DiagnosisText"];
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
            var ep = RegistrationInfoMedicDiagnoses.FirstOrDefault(n => n.SequenceNo == sequenceNo);
            if (ep != null)
            {
                //update entity's state
                editableItem.UpdateValues(ep);

                // Lengkapi
                FixDiagnoseEntry(ep, editableItem);

                //ep.IsOldCase = ((RadCheckBox)editableItem.FindControl("chkIsOldCase")).Checked;
                //ep.IsOldCase = false;
                //if (ep.IsOldCase == true)
                //{
                //    ep.IsOldCase = false;
                //}

            }
            grdDiagnose.Rebind();

        }
        protected void grdDiagnose_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var sequenceNo = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["SequenceNo"].ToString();
            var ep = RegistrationInfoMedicDiagnoses.FirstOrDefault(n => n.SequenceNo == sequenceNo);
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
                    sequenceNo = RegistrationInfoMedicDiagnoses[e.Item.ItemIndex].SequenceNo;
                    diagTypeCurrentRow = RegistrationInfoMedicDiagnoses[e.Item.ItemIndex].SRDiagnoseType;

                    ComboBox.PopulateWithDiagnoseSynonym(cboSynonym, RegistrationInfoMedicDiagnoses[e.Item.ItemIndex].DiagnoseID);
                }

                var cbo = (RadDropDownList)editItem.FindControl("cboSRDiagnoseType");
                var diagnoseTypeMain = AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain);

                cbo.Items.Clear();
                var coll = StandardReference.LoadStandardReferenceItemCollection(AppEnum.StandardReference.DiagnoseType);

                //foreach (var line in coll)
                //{
                //if (diagnoseTypeMain.ToLower() == line.ItemID.ToLower() && (sequenceNo == "001" || RegistrationInfoMedicDiagnoses.Count == 0))
                //{
                //    // Baris pertama untuk Main Diagnose
                //    cbo.Items.Add(new DropDownListItem(line.ItemName, line.ItemID));
                //    ComboBox.SelectedValue(cbo, diagnoseTypeMain);
                //    break;
                //}

                //if (diagnoseTypeMain.ToLower() != line.ItemID.ToLower())
                //{
                //    cbo.Items.Add(new DropDownListItem(line.ItemName, line.ItemID));
                //}
                //}

                //ComboBox.SelectedValue(cbo, diagTypeCurrentRow);


                // Dokter bebas memilih tipe diagnose karena Main Diagnose biasanya hanya diisi oleh DPJP
                // Pada Progress Note jika Main Diagnose atau yg lainnya tidak berubah maka tidak usah diisi shg baris pertama bukan hanya utk main diagnose saja
                // Main Daignose hanya bisa diisi 1 baris
                // (Handono 231108 req by RSI)
                var isExistMainDiagnose = false;
                foreach (var diag in RegistrationInfoMedicDiagnoses)
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
    }
}