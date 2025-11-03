using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Cpoe.Common
{
    [Obsolete("Ganti dgn yg di Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.EpisodeDiagnoseCtl, krn control ini masih labil (Handono Des 2019)", true)]
    public partial class EpisodeDiagnoseCtl : System.Web.UI.UserControl
    {
        protected bool ReadOnly
        {
            get
            {
                if (ViewState["dgro"] == null)
                    ViewState["dgro"] = false;

                return (bool)ViewState["dgro"];
            }
            set
            {
                ViewState["dgro"] = value;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            var episodeDiagnoses = EpisodeDiagnoses;
            if (episodeDiagnoses == null) return;

            // Tangkap __doPostBack dari EpisodeDiagnoseItemCtl
            // ctl00$ContentPlaceHolder1$ctl05$epDiagCtl$diag_3$cboDiagnoseID
            if (Request["__EVENTTARGET"] != null && Request["__EVENTTARGET"].Contains(this.UniqueID))
            {
                var pars = Request["__EVENTARGUMENT"].Split('_');
                switch (pars[0])
                {
                    case "ADD":
                        {
                            // Add Entity Collection
                            AddRow(episodeDiagnoses);
                            break;
                        }
                }
            }

            // Initialized control diag
            InitializedDiagnoseControl();
        }

        private static EpisodeDiagnose AddRow(EpisodeDiagnoseCollection episodeDiagnoses)
        {
            var ed = episodeDiagnoses.AddNew();
            ed.DiagnoseID = string.Empty;

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
            ed.IsVoid = false;
            return ed;
        }

        private EpisodeDiagnoseItemCtl CreateDiagnoseItemCtl(string seqNo, int rowNo)
        {
            var diagCtl = (EpisodeDiagnoseItemCtl)LoadControl("~/Module/RADT/Cpoe/Common/EpisodeDiagnoseItemCtl.ascx");
            diagCtl.ID = string.Format("diag_{0}", seqNo);
            diagCtl.RowNo = rowNo;
            return diagCtl;
        }

        protected override void OnLoad(EventArgs e)
        {
            var episodeDiagnoses = EpisodeDiagnoses;
            if (episodeDiagnoses == null) return;

            ApplyEntryValue(episodeDiagnoses);


            // Tangkap __doPostBack dari EpisodeDiagnoseItemCtl
            // DIlakukan di Load Event supaya viewstate sudah diload dulu dab baru dihajar lagi
            // ctl00$ContentPlaceHolder1$ctl05$epDiagCtl$diag_3$cboDiagnoseID
            if (Request["__EVENTTARGET"] != null && Request["__EVENTTARGET"].Contains(this.UniqueID))
            {
                var pars = Request["__EVENTARGUMENT"].Split('_');
                switch (pars[0])
                {
                    case "DEL":
                        {
                            foreach (var ed in episodeDiagnoses)
                            {
                                if (ed.es.IsDeleted) continue;

                                if (ed.SequenceNo == pars[1])
                                {
                                    if (ed.es.IsAdded)
                                        ed.MarkAsDeleted();
                                    else
                                        ed.IsVoid = true;
                                    break;
                                }
                            }

                            // ReInitialized control diag
                            InitializedDiagnoseControl();
                            break;
                        }
                }
            }


            base.OnLoad(e);
        }


        private EpisodeDiagnoseCollection EpisodeDiagnoses
        {
            get
            {
                if (Session["eds"] != null)
                    return (EpisodeDiagnoseCollection)Session["eds"];
                return null;
            }
            set { Session["eds"] = value; }
        }

        public void Save(string regNo)
        {
            var episodeDiagnoses = EpisodeDiagnoses;

            // Cek SequenceNo terakhir 
            var lastSeqNo = string.Empty;
            foreach (var ed in episodeDiagnoses)
            {
                if (ed.es.IsAdded) continue;

                lastSeqNo = ed.SequenceNo;
            }

            foreach (var ed in episodeDiagnoses)
            {
                if (ed.es.IsDeleted) continue;
                if (ed.es.IsAdded && (string.IsNullOrEmpty(ed.SRDiagnoseType) || string.IsNullOrEmpty(ed.DiagnosisText)))
                {
                    ed.MarkAsDeleted();
                }
                else if (ed.es.IsAdded)
                {
                    var newSeqNo = (lastSeqNo == null || string.IsNullOrEmpty(lastSeqNo)) ? "001" : string.Format("{0:000}", int.Parse(lastSeqNo) + 1);

                    ed.SequenceNo = newSeqNo;
                    lastSeqNo = newSeqNo;

                    ed.RegistrationNo = regNo;
                    ed.MorphologyID = string.Empty;
                    ed.ParamedicID = AppSession.UserLogin.ParamedicID;
                    ed.IsAcuteDisease = false;
                    ed.IsChronicDisease = false;
                    ed.IsConfirmed = false;
                    ed.IsVoid = false;
                }

                // Validate DiagnoseID
                if (ed.es.IsAdded || ed.es.IsModified)
                {
                    var diag = new Diagnose();
                    if (!diag.LoadByPrimaryKey(ed.DiagnoseID))
                        ed.DiagnoseID = string.Empty; //TODO: Why di EpisodeDiagnose tidak boleh NULL ...tanyakan pada mata yg sudah mengantuk
                }
            }

            episodeDiagnoses.Save();
        }

        private void ApplyEntryValue(EpisodeDiagnoseCollection episodeDiagnoses)
        {
            // Apply Value
            var i = 0;
            foreach (var ctl in pnlDiagnosis.Controls)
            {
                if (ctl is EpisodeDiagnoseItemCtl)
                {
                    var ctlEd = (EpisodeDiagnoseItemCtl)ctl;
                    foreach (var edUpd in episodeDiagnoses)
                    {
                        if (edUpd.SequenceNo == ctlEd.ID.Split('_')[1])
                        {
                            edUpd.SRDiagnoseType = ctlEd.SRDiagnoseType;
                            edUpd.DiagnoseID = ctlEd.DiagnoseID;
                            edUpd.DiagnosisText = ctlEd.DiagnosisText;
                            edUpd.Notes =
                                ctlEd.DiagnosisText; // Diisi jika bagian coding merubah kode nya, apa yg diketikan dokter tidak hilang (ide Imel Timika des 2019)
                            edUpd.IsOldCase = ctlEd.IsOldCase;
                            break;
                        }
                    }
                }
            }
        }

        public void Populate(string registrationNo, bool isReset = false, bool isReadOnly = true)
        {
            if (isReset || Session["eds"] == null || !registrationNo.Equals(Session["edsRegNo"]))
            {
                var eds = new EpisodeDiagnoseCollection();
                eds.Query.Where(eds.Query.RegistrationNo == registrationNo);
                eds.Query.OrderBy(eds.Query.SequenceNo.Ascending);
                eds.LoadAll();
                if (eds.Count == 0)
                {
                    AddRow(eds);
                }
                EpisodeDiagnoses = eds;
                Session["edsRegNo"] = registrationNo;
            }

            ReadOnly = isReadOnly;
            InitializedDiagnoseControl();
        }

        public void ImportWorkDiagnose(string registrationNo, bool resetFinalDiag)
        {
            ReadOnly = false;

            var eds = new EpisodeDiagnoseCollection();
            eds.Query.Where(eds.Query.RegistrationNo == registrationNo);
            eds.Query.OrderBy(eds.Query.SequenceNo.Ascending);
            eds.LoadAll();

            if (resetFinalDiag)
                eds.MarkAllAsDeleted();

            EpisodeDiagnoses = eds;
            Session["edsRegNo"] = registrationNo;

            // Import last 1 Main diagnose
            var workDiags = new RegistrationInfoMedicDiagnoseCollection();
            workDiags.Query.Where(workDiags.Query.RegistrationNo == registrationNo, workDiags.Query.IsVoid == false,
                workDiags.Query.SRDiagnoseType == AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            workDiags.Query.OrderBy(workDiags.Query.DiagnoseDateTime.Descending);
            workDiags.Query.es.Top = 1;
            workDiags.LoadAll();

            var mainSeqNo = string.Empty;
            if (workDiags.Count > 0)
            {
                var wdiag = workDiags[0];
                var ed = AddRow(eds);
                ed.SRDiagnoseType = wdiag.SRDiagnoseType;
                ed.DiagnoseID = wdiag.DiagnoseID;
                ed.DiagnosisText = wdiag.DiagnosisText;

                mainSeqNo = wdiag.SequenceNo;
            }

            // Other diagnose
            workDiags = new RegistrationInfoMedicDiagnoseCollection();
            workDiags.Query.Where(workDiags.Query.RegistrationNo == registrationNo,
                workDiags.Query.IsVoid == false,
                workDiags.Query.SRDiagnoseType != AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain),
                workDiags.Query.SequenceNo != mainSeqNo);
            workDiags.Query.OrderBy(workDiags.Query.DiagnoseDateTime.Descending, workDiags.Query.SRDiagnoseType.Ascending);
            workDiags.LoadAll();
            if (workDiags.Count > 0)
            {
                foreach (var wdiag in workDiags)
                {
                    var ed = AddRow(eds);
                    ed.SRDiagnoseType = wdiag.SRDiagnoseType;
                    ed.DiagnoseID = wdiag.DiagnoseID;
                    ed.DiagnosisText = wdiag.DiagnosisText;
                }
            }

            InitializedDiagnoseControl();
        }

        private void InitializedDiagnoseControl()
        {
            pnlDiagnosis.Controls.Clear();

            var episodeDiagnoses = EpisodeDiagnoses;
            if (episodeDiagnoses == null) return;

            var i = 1;
            foreach (var ed in episodeDiagnoses)
            {
                if (ed.es.IsDeleted) continue;
                if (ed.IsVoid ?? false) continue;

                var diagCtl = CreateDiagnoseItemCtl(ed.SequenceNo, i);
                diagCtl.ReadOnly = ReadOnly;

                // Set nilai awal, saat load akan tertimpa oleh viewstatenya atau yg diisi oleh user
                diagCtl.SRDiagnoseType = ed.SRDiagnoseType;
                diagCtl.SetDiagnoseID(ed.SequenceNo, ed.DiagnoseID, ed.DiagnosisText);
                diagCtl.IsOldCase = ed.IsOldCase ?? false;
                pnlDiagnosis.Controls.Add(diagCtl);
                i++;
            }
        }
    }
}