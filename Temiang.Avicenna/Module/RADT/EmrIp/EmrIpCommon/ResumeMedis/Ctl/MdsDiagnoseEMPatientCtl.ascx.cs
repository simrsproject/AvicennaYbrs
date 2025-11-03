using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr.Mds.Ctl
{
    /// <summary>
    /// Entrian Diagnosa AKhir untuk Soap Rawat Jalan dan Discharge Summary 
    /// </summary>
    /// ------------------------------------------------------------
    /// Created By : Diky
    /// -------------------------------------------------------------

    public partial class MdsDiagnoseEMPatientCtl : System.Web.UI.UserControl
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

        public bool IsHasNotCoder
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

        public void Save(bool isNewMode = false)
        {
            // Jika untuk entri MDS dengan mode new maka simpan diagnosa finalnya (EpisodeDiagnose), tetapi jika mode edit dan EP statusnya confirmed jangan ditimpa ulang
            // Karena kemungkinan diagnosa finalnya sudah ditangani atau dimodif oleh bagian Coder (Handono 230330 RSI)

            // Existed EpisodeDiagnose for update
            var edq = new EpisodeDiagnoseQuery("ed");
            edq.Where(edq.RegistrationNo == RegistrationNo);
            var epDiags = new EpisodeDiagnoseCollection();
            epDiags.Load(edq);


            DataTable dtbMdsLast = null;
            DataTable dtbEpLast = null;
            var mdsLastSeqNo = string.Empty;
            var epLastSeqNo = string.Empty;
            var srDiagnoseType = string.Empty;
            var diagnoseID = string.Empty;
            var diagnosisText = string.Empty;
            var mdsDiags = MdsDiagnoses;
            foreach (var mdsDiag in mdsDiags)
            {
                if (mdsDiag.es.IsAdded && (string.IsNullOrEmpty(mdsDiag.SRDiagnoseType) ||
                                           string.IsNullOrEmpty(mdsDiag.DiagnosisText)))
                {
                    mdsDiag.MarkAsDeleted();
                }

                if (mdsDiag.es.IsDeleted) // Record lama yg dihapus
                {
                    srDiagnoseType = mdsDiag.GetOriginalColumnValue("SRDiagnoseType").ToString();
                    diagnoseID = mdsDiag.GetOriginalColumnValue("DiagnoseID").ToString();
                    diagnosisText = mdsDiag.GetOriginalColumnValue("DiagnosisText").ToString();

                    foreach (var edr in epDiags)
                    {
                        if (!edr.es.IsDeleted
                            && (edr.IsConfirmed == null || edr.IsConfirmed == false)
                            && edr.SRDiagnoseType == srDiagnoseType
                            && edr.DiagnoseID == diagnoseID
                            && edr.DiagnosisText == diagnosisText)
                        {
                            edr.MarkAsDeleted();
                            break;
                        }
                    }

                    continue;
                }

                // Skip jika tidak dimodif
                if (!mdsDiag.es.IsAdded && !mdsDiag.es.IsModified) continue;

                if (mdsDiag.es.IsAdded) // Record baru
                {
                    // Cek ulang SequenceNo terakhir di DB utk mencegah error jika ada user lain yg nyalip insert Diagnosa nya
                    if (dtbMdsLast == null && string.IsNullOrEmpty(mdsLastSeqNo))
                    {
                        var qr = new MedicalDischargeSummaryDiagnoseQuery("mdsd");
                        qr.Select(qr.SequenceNo);
                        qr.Where(qr.RegistrationNo == RegistrationNo);
                        qr.OrderBy(qr.SequenceNo.Descending);
                        qr.es.Top = 1;

                        dtbMdsLast = qr.LoadDataTable();
                        if (dtbMdsLast.Rows.Count > 0)
                            mdsLastSeqNo = dtbMdsLast.Rows[0][0].ToString();
                        else
                            mdsLastSeqNo = "000";
                    }

                    var newSeqNo = (mdsLastSeqNo == null || string.IsNullOrEmpty(mdsLastSeqNo))
                        ? "001"
                        : string.Format("{0:000}", int.Parse(mdsLastSeqNo) + 1);
                    mdsDiag.SequenceNo = newSeqNo;
                    mdsLastSeqNo = newSeqNo;
                }

                // Check and update EpisodeDiagnose
                srDiagnoseType = mdsDiag.es.IsModified
                    ? mdsDiag.GetOriginalColumnValue("SRDiagnoseType").ToString()
                    : mdsDiag.SRDiagnoseType;
                diagnoseID = mdsDiag.es.IsModified
                    ? mdsDiag.GetOriginalColumnValue("DiagnoseID").ToString()
                    : mdsDiag.DiagnoseID;
                diagnosisText = mdsDiag.es.IsModified
                    ? mdsDiag.GetOriginalColumnValue("DiagnosisText").ToString()
                    : mdsDiag.DiagnosisText;

                EpisodeDiagnose ed = null;
                foreach (var edr in epDiags)
                {
                    if (edr.SRDiagnoseType == srDiagnoseType
                        && edr.DiagnoseID == diagnoseID
                        && edr.DiagnosisText == diagnosisText)
                    {
                        ed = edr;
                        break;
                    }
                }

                if (ed == null)
                {
                    // Cek ulang SequenceNo terakhir di DB utk mencegah error jika ada user lain yg nyalip insert Diagnosa nya
                    if (dtbEpLast == null && string.IsNullOrEmpty(epLastSeqNo))
                    {
                        var qr = new EpisodeDiagnoseQuery("epd");
                        qr.Select(qr.SequenceNo);
                        qr.Where(qr.RegistrationNo == RegistrationNo);
                        qr.OrderBy(qr.SequenceNo.Descending);
                        qr.es.Top = 1;

                        dtbEpLast = qr.LoadDataTable();
                        if (dtbEpLast.Rows.Count > 0)
                            epLastSeqNo = dtbEpLast.Rows[0][0].ToString();
                        else
                            epLastSeqNo = "000";
                    }

                    var newSeqNo = (epLastSeqNo == null || string.IsNullOrEmpty(epLastSeqNo))
                        ? "001"
                        : string.Format("{0:000}", int.Parse(epLastSeqNo) + 1);
                    epLastSeqNo = newSeqNo;

                    ed = epDiags.AddNew();
                    ed.RegistrationNo = mdsDiag.RegistrationNo;
                    ed.SequenceNo = epLastSeqNo;
                }

                if (!ed.es.IsDeleted && (ed.IsVoid == null || ed.IsVoid == false) &&
                    (ed.IsConfirmed == null || ed.IsConfirmed == false))
                {
                    //// Cek Notes nya jika berbeda maka sudah dirubah oleh bagian coder (utk mode modified)
                    //// Notes terisi DiagoneText nya pada saat dokter entry Diagnose di asesmen
                    //if (!ed.es.IsAdded && !string.IsNullOrWhiteSpace(ed.Notes) && ed.Notes != ed.DiagnosisText)
                    //    continue;

                    ed.DiagnoseID = mdsDiag.DiagnoseID;
                    ed.DiagnosisText = mdsDiag.DiagnosisText;
                    ed.SRDiagnoseType = mdsDiag.SRDiagnoseType;
                    ed.ExternalCauseID = mdsDiag.ExternalCauseID;
                    ed.IsOldCase = mdsDiag.IsOldCase ?? false; //Not Nullable
                    ed.IsVoid = mdsDiag.IsVoid ?? false;
                    ed.CreateDateTime = mdsDiag.CreatedDateTime;
                    ed.CreateByUserID = mdsDiag.CreatedByUserID;
                    ed.LastUpdateDateTime = mdsDiag.LastUpdateDateTime;
                    ed.LastUpdateByUserID = mdsDiag.LastUpdateByUserID;
                    ed.ParamedicID = mdsDiag.ParamedicID;
                }
            }

            epDiags.Save();
            mdsDiags.Save();
        }
        public void ImportWorkDiagnose(string registrationNo, bool resetFinalDiag)
        {
            // Cegah import jika sudah ada, resetFinalDiag digunakan pada lbtnResetFinalDiag tetapi tetap hanya untuk yg beum pernah import
            if (MdsDiagnoses.Count > 0) return;

            var eds = MdsDiagnoses;
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
            var edc = new EpisodeDiagnoseCollection();
            var edq = new EpisodeDiagnoseQuery("ed");
            edq.Where(edq.RegistrationNo == registrationNo, edq.IsVoid == false,
                edq.SRDiagnoseType == AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            edq.Select(edq.SRDiagnoseType, edq.DiagnoseID, edq.DiagnosisText, edq.ExternalCauseID, edq.IsOldCase);
            edq.OrderBy(edq.CreateDateTime.Descending);
            edq.es.Top = 1;
            edc.Load(edq);

            var mainSeqNo = string.Empty;
            if (edc.Count > 0)
            {
                var wdiag = edc[0];
                var ed = CreateNewMdsDiagnose(eds);
                ed.SRDiagnoseType = wdiag.SRDiagnoseType;
                ed.DiagnoseID = wdiag.DiagnoseID;
                ed.DiagnosisText = wdiag.DiagnosisText;
                ed.ExternalCauseID = wdiag.ExternalCauseID;
                ed.IsOldCase = wdiag.IsOldCase;
                ed.DiagnoseType =
                    StandardReference.GetItemName(AppEnum.StandardReference.DiagnoseType, wdiag.SRDiagnoseType);

                mainSeqNo = wdiag.SequenceNo;
            }

            // Other diagnose
            edc = new EpisodeDiagnoseCollection();
            edq = new EpisodeDiagnoseQuery("ed");
            edq.Where(edq.RegistrationNo == registrationNo,
                edq.IsVoid == false,
                edq.SRDiagnoseType != AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            if (!string.IsNullOrWhiteSpace(mainSeqNo))
                edq.Where(edq.SequenceNo != mainSeqNo);
            edq.OrderBy(edq.SRDiagnoseType.Ascending, edq.DiagnoseID.Ascending, edq.ExternalCauseID.Ascending);
            edq.es.Distinct = true;
            edq.Select(edq.SRDiagnoseType, edq.DiagnoseID, edq.DiagnosisText, edq.ExternalCauseID, edq.IsOldCase);
            edc.Load(edq);
            if (edc.Count > 0)
            {
                foreach (var wdiag in edc)
                {
                    var ed = CreateNewMdsDiagnose(eds);
                    ed.SRDiagnoseType = wdiag.SRDiagnoseType;
                    ed.DiagnoseID = wdiag.DiagnoseID;
                    ed.DiagnosisText = wdiag.DiagnosisText;
                    ed.ExternalCauseID = wdiag.ExternalCauseID;
                    ed.IsOldCase = wdiag.IsOldCase;
                    ed.DiagnoseType =
                        StandardReference.GetItemName(AppEnum.StandardReference.DiagnoseType, wdiag.SRDiagnoseType);
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
        private void FixMdsDiagnoseEntry(MedicalDischargeSummaryDiagnose ep, GridEditableItem editableItem)
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


            ep.DiagnoseType = StandardReference.GetItemName(AppEnum.StandardReference.DiagnoseType, ep.SRDiagnoseType);
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
                var qr = new MedicalDischargeSummaryDiagnoseQuery("ed");
                var stdi = new AppStandardReferenceItemQuery("sdti");
                qr.InnerJoin(stdi)
                    .On(qr.SRDiagnoseType == stdi.ItemID & stdi.StandardReferenceID == "DiagnoseType");
                var ec = new DiagnoseQuery("ec");
                qr.LeftJoin(ec).On(qr.ExternalCauseID == ec.DiagnoseID);
                qr.Select(qr, stdi.ItemName.As("refToAppStandardReferenceItem_SRDiagnoseType"),
                    ec.DiagnoseName.As("refToDiagnose_DiagnoseName4Ec"));

                qr.Where(qr.RegistrationNo == RegistrationNo);
                qr.OrderBy(qr.SequenceNo.Ascending);
                var coll = new MedicalDischargeSummaryDiagnoseCollection();
                coll.Load(qr);
                MdsDiagnoses = coll;
            }
            grdDiagnose.DataSource = MdsDiagnoses;
        }

        private MedicalDischargeSummaryDiagnoseCollection MdsDiagnoses
        {
            get
            {
                if (Session[SessionName] == null)
                    return null;

                return (MedicalDischargeSummaryDiagnoseCollection)Session[SessionName];
            }
            set
            {
                Session[SessionName] = value;
            }
        }
        private MedicalDischargeSummaryDiagnose CreateNewMdsDiagnose(MedicalDischargeSummaryDiagnoseCollection mdsDiags)
        {
            var ed = mdsDiags.AddNew();
            ed.DiagnoseID = string.Empty;

            var lastSeqNo = string.Empty;
            foreach (var item in mdsDiags)
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
            ed.ParamedicID = AppSession.UserLogin.ParamedicID;
            ed.IsVoid = false;
            ed.IsOldCase = false;

            return ed;
        }
        protected void grdDiagnose_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var editableItem = ((GridEditableItem)e.Item);
            OnInsertCommandMds(editableItem);
            // Show hasil insert
            grdDiagnose.Rebind();

        }

        private void OnInsertCommandMds(GridEditableItem editableItem)
        {
            //Last entity
            var ep = CreateNewMdsDiagnose(MdsDiagnoses);

            //populate its properties
            Hashtable values = new Hashtable();
            editableItem.ExtractValues(values);
            ep.DiagnoseID = (string)values["DiagnoseID"];
            ep.SRDiagnoseType = (string)values["SRDiagnoseType"];
            ep.DiagnosisText = (string)values["DiagnosisText"];
            ep.ExternalCauseID = (string)values["ExternalCauseID"];
            
            // Lengkapi termasuk ep.IsOldCase
            FixMdsDiagnoseEntry(ep, editableItem);
        }

        protected void grdDiagnose_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var editableItem = ((GridEditableItem)e.Item);
            var sequenceNo = editableItem.GetDataKeyValue("SequenceNo").ToString();
            var ep = MdsDiagnoses.FirstOrDefault(n => n.SequenceNo == sequenceNo);
            if (ep != null)
            {
                //update entity's state
                editableItem.UpdateValues(ep);

                // Lengkapi
                FixMdsDiagnoseEntry(ep, editableItem);

            }
            grdDiagnose.Rebind();

        }
        protected void grdDiagnose_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var sequenceNo = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["SequenceNo"].ToString();
            var ep = MdsDiagnoses.FirstOrDefault(n => n.SequenceNo == sequenceNo);
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
                var diagType = string.Empty;
                if (e.Item.ItemIndex >= 0) // Not Insert
                {
                    sequenceNo = MdsDiagnoses[e.Item.ItemIndex].SequenceNo;
                    diagType = MdsDiagnoses[e.Item.ItemIndex].SRDiagnoseType;
                }

                var cbo = (RadDropDownList)editItem.FindControl("cboSRDiagnoseType");
                var diagnoseTypeMain = AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain);

                cbo.Items.Clear();
                var coll = StandardReference.LoadStandardReferenceItemCollection(AppEnum.StandardReference.DiagnoseType);

                var isExistMainDiag = false;
                foreach (var diag in MdsDiagnoses)
                {
                    if (diag.SRDiagnoseType == diagnoseTypeMain && (diag.IsVoid ?? false) == false)
                    {
                        isExistMainDiag = true;
                        break;
                    }
                }

                foreach (var line in coll)
                {
                    //if (diagnoseTypeMain.ToLower() == line.ItemID.ToLower() && (sequenceNo == "001" || EpisodeDiagnoses.Count == 0)
                    if (diagnoseTypeMain.ToLower() == line.ItemID.ToLower() && (sequenceNo == "001" || !isExistMainDiag))
                    {
                        // Baris pertama untuk Main Diagnose
                        cbo.Items.Add(new DropDownListItem(line.ItemName, line.ItemID));
                        ComboBox.SelectedValue(cbo, diagnoseTypeMain);
                        break;
                    }

                    if (diagnoseTypeMain.ToLower() != line.ItemID.ToLower())
                    {
                        cbo.Items.Add(new DropDownListItem(line.ItemName, line.ItemID));
                    }
                }

                ComboBox.SelectedValue(cbo, diagType);
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

    }
}