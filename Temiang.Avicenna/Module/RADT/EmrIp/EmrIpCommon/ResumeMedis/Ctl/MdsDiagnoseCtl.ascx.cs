using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr.Mds.Ctl
{
    /// <summary>
    /// Entrian Diagnosa Medical Discharge Summary 
    /// </summary>
    /// ------------------------------------------------------------
    /// Created By : Handono
    /// -------------------------------------------------------------
    /// Handono 230921:
    /// #TK-572 RSSTJ - Pada Asesmen Dokter, kolom ICD X bagian Diagnose (deskripsi) dibuat read only
    /// #TK-567 RSSTJ - Penambahan parameter mandatory untuk kolom ICD X 
    /// Handono 231109: (base RSI req) 
    /// - Row pertama bisa untuk selain Main Diagnose krn Main Diagnose biasanya hanya untuk dientry DPJP saja sehingga untuk dokter lain jadi bermasalah
    /// - Jika belum ada main diagnose maka untuk DPJP default yg muncul adalah main diagnose dan sealinnya adalah secondary diagnose
    /// - Main diagnose hanya bisa disi 1 row
    /// -------------------------------------------------------------
    public partial class MdsDiagnoseCtl : System.Web.UI.UserControl
    {
        private bool IsCallFromCaseMix => Request.QueryString["csmix"] == "1";
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

        public string RegistrationType
        {
            get
            {
                if (ViewState["regtype"] == null)
                {
                    return "IPR"; // Default
                }
                else
                    return (string)ViewState["regtype"];
            }
            set
            {
                ViewState["regtype"] = value;
            }
        }

        private RadComboBox cboSynonym
        {
            get { return (RadComboBox)Helper.FindControlRecursive(Page, "cboSynonym"); }
        }

        public void Save(bool isNewMode = false)
        {
            // Jika untuk entri MDS dengan mode new maka simpan diagnosa finalnya (EpisodeDiagnose), tetapi jika mode edit dan EP statusnya confirmed jangan ditimpa ulang
            // Karena kemungkinan diagnosa finalnya sudah ditangani atau dimodif oleh bagian Coder (Handono 230330 RSI)

            // Existed EpisodeDiagnose for update
            var epDiags = new EpisodeDiagnoseCollection();
            if (!IsCallFromCaseMix) //MDS DPJP mode
            {
                var edq = new EpisodeDiagnoseQuery("ed");
                edq.Where(edq.RegistrationNo == RegistrationNo);
                epDiags.Load(edq);
            }


            DataTable dtbMdsLast = null;
            DataTable dtbEpLast = null;
            var mdsLastSeqNo = string.Empty;
            var epLastSeqNo = string.Empty;
            var srDiagnoseType = string.Empty;
            var diagnoseID = string.Empty;
            var diagnosisText = string.Empty;
            var diagnoseSynonym = string.Empty;
            bool? isVoid = null;
            var mdsDiags = MdsDiagnoses;

            foreach (var mdsDiag in mdsDiags)
            {
                if (mdsDiag.es.IsAdded && (string.IsNullOrEmpty(mdsDiag.SRDiagnoseType) ||
                                           string.IsNullOrEmpty(mdsDiag.DiagnosisText)))
                {
                    mdsDiag.MarkAsDeleted();
                }

                if (!IsCallFromCaseMix) //MDS DPJP mode
                {
                    if (mdsDiag.es.IsDeleted) // Record lama yg dihapus
                    {
                        srDiagnoseType = mdsDiag.GetOriginalColumnValue("SRDiagnoseType").ToString();
                        diagnoseID = mdsDiag.GetOriginalColumnValue("DiagnoseID").ToString();
                        diagnosisText = mdsDiag.GetOriginalColumnValue("DiagnosisText").ToString();
                        diagnoseSynonym = mdsDiag.GetOriginalColumnValue("DiagnoseSynonym").ToString();

                        foreach (var edr in epDiags)
                        {
                            if (!edr.es.IsDeleted
                                && (edr.IsConfirmed == null || edr.IsConfirmed == false)
                                && edr.SRDiagnoseType == srDiagnoseType
                                && edr.DiagnoseID == diagnoseID
                                && edr.DiagnoseSynonym == diagnoseSynonym
                                && edr.DiagnosisText == diagnosisText)
                            {
                                edr.MarkAsDeleted();
                                break;
                            }
                        }

                        continue;
                    }
                }

                // Skip jika tidak dimodif
                if (!mdsDiag.es.IsAdded && !mdsDiag.es.IsModified) continue;

                if (mdsDiag.es.IsAdded) // Record baru
                {
                    // Cek ulang SequenceNo terakhir di DB utk mencegah error jika ada user lain yg nyalip insert Diagnosa nya
                    if (dtbMdsLast == null && string.IsNullOrEmpty(mdsLastSeqNo))
                    {
                        if (IsCallFromCaseMix)
                        {
                            var qr = new MedicalDischargeSummaryDiagnoseCmxQuery("mdsd");
                            qr.Select(qr.SequenceNo);
                            qr.Where(qr.RegistrationNo == RegistrationNo);
                            qr.OrderBy(qr.SequenceNo.Descending);
                            qr.es.Top = 1;

                            // Load
                            dtbMdsLast = qr.LoadDataTable();
                        }
                        else
                        {
                            var qr = new MedicalDischargeSummaryDiagnoseQuery("mdsd");
                            qr.Select(qr.SequenceNo);
                            qr.Where(qr.RegistrationNo == RegistrationNo);
                            qr.OrderBy(qr.SequenceNo.Descending);
                            qr.es.Top = 1;

                            // Load
                            dtbMdsLast = qr.LoadDataTable();
                        }
                        
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
                diagnoseSynonym = mdsDiag.es.IsModified
                    ? mdsDiag.GetOriginalColumnValue("DiagnoseSynonym").ToString()
                    : mdsDiag.DiagnoseSynonym;

                isVoid = mdsDiag.es.IsModified
                    ? (bool?)mdsDiag.GetOriginalColumnValue("IsVoid")
                    : mdsDiag.IsVoid;

                if (!IsCallFromCaseMix) // Hanya MDS Dokter saja yg save ke EpisodeDiagnose
                {
                    EpisodeDiagnose ed = null;
                    foreach (var edr in epDiags)
                    {
                        if (edr.SRDiagnoseType == srDiagnoseType
                            && edr.DiagnoseID == diagnoseID
                            && edr.DiagnosisText == diagnosisText
                            && edr.DiagnoseSynonym == diagnoseSynonym
                            && edr.IsVoid == isVoid)
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
                        ed.Notes = string.Format("Dx DPJP: {0} {1} by: {2}", mdsDiag.DiagnoseID, mdsDiag.DiagnosisText, Paramedic.GetParamedicName(mdsDiag.ParamedicID));
                        ed.DiagnoseSynonym = mdsDiag.DiagnoseSynonym;
                    }
                }
            }

            if (!IsCallFromCaseMix)
                epDiags.Save();

            if (IsCallFromCaseMix)
            {
                // Switch save destination
                var meta = mdsDiags.es.Meta.GetProviderMetadata("esDefault");
                meta.Destination = "MedicalDischargeSummaryDiagnoseCmx";
            }
            mdsDiags.Save();

            // Harus direset ke aslinya karena jika tidak maka akan selalu pakai setingan terakhir walaupun untuk variable baru
            if (IsCallFromCaseMix)
            {
                // Switch query source
                mdsDiags.Query.es.QuerySource = "MedicalDischargeSummaryDiagnose";

                // Switch save destination
                var meta = mdsDiags.es.Meta.GetProviderMetadata("esDefault");
                meta.Destination = "MedicalDischargeSummaryDiagnose";
            }
        }

        /// <summary>
        /// Import diagnosa dari entrian asesmen dokter atau icd x yg lain
        /// </summary>
        /// <param name="registrationNo"></param>
        /// <param name="resetFinalDiag"></param>
        public void ImportDiagnose(string registrationNo, bool isResetMdsDiag)
        {
            if (RegistrationType == "IPR")
                ImportFromWorkDiagnose(registrationNo, isResetMdsDiag);                
            else
                ImportFromEpisodeDiagnose(registrationNo, isResetMdsDiag);

        }
        private void ImportFromWorkDiagnose(string registrationNo, bool isResetMdsDiag)
        {
            //// Cegah import jika sudah ada, resetFinalDiag digunakan pada lbtnResetFinalDiag tetapi tetap hanya untuk yg beum pernah import
            //if (MdsDiagnoses.Count > 0) return;

            //var eds = MdsDiagnoses;
            //if (resetFinalDiag)
            //{
            //    foreach (var ed in eds)
            //    {
            //        if (ed.es.IsDeleted) continue;
            //        if (ed.es.IsAdded)
            //            ed.MarkAsDeleted();
            //        else
            //            ed.IsVoid = true;
            //    }
            //}
            var mdsDiags = MdsDiagnoses;

            if (!isResetMdsDiag && mdsDiags.Count > 0) return;

            if (isResetMdsDiag && mdsDiags.Count > 0)
            {
                foreach (var diag in mdsDiags)
                {
                    if (diag.es.IsDeleted) continue;
                    if (diag.es.IsAdded)
                        diag.MarkAsDeleted();
                    else
                        diag.IsVoid = true;
                }
            }

            // Import last 1 Main diagnose
            var workDiags = new RegistrationInfoMedicDiagnoseCollection();
            var wdQr = new RegistrationInfoMedicDiagnoseQuery("wd");
            wdQr.Where(wdQr.RegistrationNo == registrationNo, wdQr.IsVoid == false,
                wdQr.SRDiagnoseType == AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            wdQr.Select(wdQr.SRDiagnoseType, wdQr.DiagnoseID, wdQr.DiagnosisText, wdQr.ExternalCauseID, wdQr.IsOldCase);
            wdQr.OrderBy(wdQr.DiagnoseDateTime.Descending);
            wdQr.es.Top = 1;
            workDiags.Load(wdQr);

            var mainSeqNo = string.Empty;
            if (workDiags.Count > 0)
            {
                var wdiag = workDiags[0];
                var mdsDiag = CreateNewMdsDiagnose(mdsDiags);
                mdsDiag.SRDiagnoseType = wdiag.SRDiagnoseType;
                mdsDiag.DiagnoseID = wdiag.DiagnoseID;
                mdsDiag.DiagnosisText = wdiag.DiagnosisText;
                mdsDiag.ExternalCauseID = wdiag.ExternalCauseID;
                mdsDiag.IsOldCase = wdiag.IsOldCase;
                mdsDiag.DiagnoseType =
                    StandardReference.GetItemName(AppEnum.StandardReference.DiagnoseType, wdiag.SRDiagnoseType);
                mdsDiag.DiagnoseSynonym = wdiag.DiagnoseSynonym;

                mainSeqNo = wdiag.SequenceNo;
            }

            // Other diagnose
            workDiags = new RegistrationInfoMedicDiagnoseCollection();
            wdQr = new RegistrationInfoMedicDiagnoseQuery("wd");
            wdQr.Where(wdQr.RegistrationNo == registrationNo,
                wdQr.IsVoid == false,
                wdQr.SRDiagnoseType != AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            if (!string.IsNullOrWhiteSpace(mainSeqNo))
                wdQr.Where(wdQr.SequenceNo != mainSeqNo);
            wdQr.OrderBy(wdQr.SRDiagnoseType.Ascending, wdQr.DiagnoseID.Ascending, wdQr.ExternalCauseID.Ascending);
            wdQr.es.Distinct = true;
            wdQr.Select(wdQr.SRDiagnoseType, wdQr.DiagnoseID, wdQr.DiagnosisText, wdQr.ExternalCauseID, wdQr.IsOldCase);
            workDiags.Load(wdQr);
            if (workDiags.Count > 0)
            {
                foreach (var wdiag in workDiags)
                {
                    var mdsDiag = CreateNewMdsDiagnose(mdsDiags);
                    mdsDiag.SRDiagnoseType = wdiag.SRDiagnoseType;
                    mdsDiag.DiagnoseID = wdiag.DiagnoseID;
                    mdsDiag.DiagnosisText = wdiag.DiagnosisText;
                    mdsDiag.ExternalCauseID = wdiag.ExternalCauseID;
                    mdsDiag.IsOldCase = wdiag.IsOldCase;
                    mdsDiag.DiagnoseType =
                        StandardReference.GetItemName(AppEnum.StandardReference.DiagnoseType, wdiag.SRDiagnoseType);
                    mdsDiag.DiagnoseSynonym = wdiag.DiagnoseSynonym;
                }
            }

            grdDiagnose.Rebind();
        }

        private void ImportFromEpisodeDiagnose(string registrationNo, bool isResetMdsDiag)
        {
            var mdsDiags = MdsDiagnoses;

            if (!isResetMdsDiag && mdsDiags.Count > 0) return;

            if (isResetMdsDiag && mdsDiags.Count > 0)
            {
                foreach (var mdsDiag in mdsDiags)
                {
                    if (mdsDiag.es.IsDeleted) continue;
                    if (mdsDiag.es.IsAdded)
                        mdsDiag.MarkAsDeleted();
                    else
                        mdsDiag.IsVoid = true;
                }
            }

            // Import last 1 Main diagnose
            var epDiags = new EpisodeDiagnoseCollection();
            var epDiagQr = new EpisodeDiagnoseQuery("ed");
            epDiagQr.Where(epDiagQr.RegistrationNo == registrationNo, epDiagQr.IsVoid == false,
                epDiagQr.SRDiagnoseType == AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            epDiagQr.Select(epDiagQr.SRDiagnoseType, epDiagQr.DiagnoseID, epDiagQr.DiagnosisText, epDiagQr.ExternalCauseID, epDiagQr.IsOldCase);
            epDiagQr.OrderBy(epDiagQr.CreateDateTime.Descending);
            epDiagQr.es.Top = 1;
            epDiags.Load(epDiagQr);

            var mainSeqNo = string.Empty;
            if (epDiags.Count > 0)
            {
                var epdiag = epDiags[0];

                var diag = CreateNewMdsDiagnose(mdsDiags);
                diag.SRDiagnoseType = epdiag.SRDiagnoseType;
                diag.DiagnoseID = epdiag.DiagnoseID;
                diag.DiagnosisText = epdiag.DiagnosisText;
                diag.ExternalCauseID = epdiag.ExternalCauseID;
                diag.IsOldCase = epdiag.IsOldCase;
                diag.DiagnoseType =
                    StandardReference.GetItemName(AppEnum.StandardReference.DiagnoseType, epdiag.SRDiagnoseType);
                diag.DiagnoseSynonym = epdiag.DiagnoseSynonym;

                mainSeqNo = epdiag.SequenceNo;
            }

            // Other diagnose
            epDiags = new EpisodeDiagnoseCollection();
            epDiagQr = new EpisodeDiagnoseQuery("wd");
            epDiagQr.Where(epDiagQr.RegistrationNo == registrationNo,
                epDiagQr.IsVoid == false,
                epDiagQr.SRDiagnoseType != AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            if (!string.IsNullOrWhiteSpace(mainSeqNo))
                epDiagQr.Where(epDiagQr.SequenceNo != mainSeqNo);
            epDiagQr.OrderBy(epDiagQr.SRDiagnoseType.Ascending, epDiagQr.DiagnoseID.Ascending, epDiagQr.ExternalCauseID.Ascending);
            epDiagQr.es.Distinct = true;
            epDiagQr.Select(epDiagQr.SRDiagnoseType, epDiagQr.DiagnoseID, epDiagQr.DiagnosisText, epDiagQr.ExternalCauseID, epDiagQr.IsOldCase);
            epDiags.Load(epDiagQr);
            if (epDiags.Count > 0)
            {
                foreach (var epDiag in epDiags)
                {
                    var ed = CreateNewMdsDiagnose(mdsDiags);
                    ed.SRDiagnoseType = epDiag.SRDiagnoseType;
                    ed.DiagnoseID = epDiag.DiagnoseID;
                    ed.DiagnosisText = epDiag.DiagnosisText;
                    ed.ExternalCauseID = epDiag.ExternalCauseID;
                    ed.IsOldCase = epDiag.IsOldCase;
                    ed.DiagnoseType =
                        StandardReference.GetItemName(AppEnum.StandardReference.DiagnoseType, epDiag.SRDiagnoseType);
                    ed.DiagnoseSynonym = epDiag.DiagnoseSynonym;
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

                if (IsCallFromCaseMix)
                {
                    // Switch query source
                    coll.Query.es.QuerySource = "MedicalDischargeSummaryDiagnoseCmx";
                    qr.es.QuerySource = "MedicalDischargeSummaryDiagnoseCmx";

                }

                coll.Load(qr);

                // Harus direset ke aslinya karena jika tidak maka akan selalu pakai setingan terakhir walaupun untuk variable baru
                if (IsCallFromCaseMix)
                {
                    // Switch query source
                    coll.Query.es.QuerySource = "MedicalDischargeSummaryDiagnose";
                    qr.es.QuerySource = "MedicalDischargeSummaryDiagnose";

                }
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
            ed.DiagnoseSynonym = string.Empty;

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
            ep.DiagnoseSynonym = (string)values["DiagnoseSynonym"];

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
                var diagTypeCurrentRow = string.Empty;
                if (e.Item.ItemIndex >= 0) // Not Insert
                {
                    sequenceNo = MdsDiagnoses[e.Item.ItemIndex].SequenceNo;
                    diagTypeCurrentRow = MdsDiagnoses[e.Item.ItemIndex].SRDiagnoseType;

                    ComboBox.PopulateWithDiagnoseSynonym(cboSynonym, MdsDiagnoses[e.Item.ItemIndex].DiagnoseID);
                }

                var cbo = (RadDropDownList)editItem.FindControl("cboSRDiagnoseType");
                var diagnoseTypeMain = AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain);

                cbo.Items.Clear();
                var coll = StandardReference.LoadStandardReferenceItemCollection(AppEnum.StandardReference.DiagnoseType);

                //var isExistMainDiag = false;
                //foreach (var diag in MdsDiagnoses)
                //{
                //    if (diag.SRDiagnoseType == diagnoseTypeMain && (diag.IsVoid ?? false) == false)
                //    {
                //        isExistMainDiag = true;
                //        break;
                //    }
                //}

                //foreach (var line in coll)
                //{
                //    //if (diagnoseTypeMain.ToLower() == line.ItemID.ToLower() && (sequenceNo == "001" || EpisodeDiagnoses.Count == 0)
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
                foreach (var diag in MdsDiagnoses)
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
                    if (ParamedicTeam.IsParamedicTeamStatusDpjp(RegistrationNo, AppSession.UserLogin.ParamedicID))
                        ComboBox.SelectedValue(cbo, diagnoseTypeMain);
                    else
                        ComboBox.SelectedValue(cbo, diagnoseTypeSecondary);
                }
                else
                    ComboBox.SelectedValue(cbo, diagTypeCurrentRow);

                // #TK-572 RSSTJ - Pada Asesmen Dokter, kolom ICD X bagian Diagnose (deskripsi) dibuat read only (Handono 230921)
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
    }
}