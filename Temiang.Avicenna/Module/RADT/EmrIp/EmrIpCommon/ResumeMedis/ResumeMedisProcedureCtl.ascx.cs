using System;
using System.Collections;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class ResumeMedisProcedureCtl : System.Web.UI.UserControl
    {
        private string SessionName => string.Format("mdsproc_{0}", Helper.PageID(this.Page));
        private bool IsCallFromCaseMix => Request.QueryString["csmix"] == "1";

        private RadComboBox cboProcedureSynonym
        {
            get { return (RadComboBox)Helper.FindControlRecursive(Page, "cboProcedureSynonym"); }
        }

        public void Save()
        {
            var diags = DischargeProcedures;


            foreach (var ed in diags)
            {
                if (ed.es.IsDeleted) continue;

                // Request RSI-> ICD 9 tetap bisa disimpan tanpa mengisi ID nya (Handono 230308)
                //if (ed.es.IsAdded && string.IsNullOrEmpty(ed.ProcedureID))
                //{
                //    ed.MarkAsDeleted();
                //}

                // Tapi hapus jika kosong
                if (string.IsNullOrEmpty(ed.ProcedureID) && string.IsNullOrEmpty(ed.ProcedureName))
                {
                    ed.MarkAsDeleted();
                }
            }

            if (IsCallFromCaseMix)
            {
                // Switch save destination
                var meta = diags.es.Meta.GetProviderMetadata("esDefault");
                meta.Destination = "MedicalDischargeSummaryProcedureCmx";
            }
            diags.Save();

            // Harus direset ke aslinya karena jika tidak maka akan selalu pakai setingan terakhir walaupun untuk variable baru
            if (IsCallFromCaseMix)
            {
                // Switch save destination
                var meta = diags.es.Meta.GetProviderMetadata("esDefault");
                meta.Destination = "MedicalDischargeSummaryProcedure";
            }
        }
        public void ImportEpisodeProcedure(string registrationNo, bool resetFinalDiag)
        {
            // Cegah import jika sudah ada
            if (DischargeProcedures.Count > 0) return;

            var eps = DischargeProcedures;
            if (resetFinalDiag)
            {
                foreach (var ed in eps)
                {
                    if (ed.es.IsDeleted) continue;
                    if (ed.es.IsAdded)
                        ed.MarkAsDeleted();
                    else
                        ed.IsVoid = true;
                }
            }

            // Import last All Procedures
            var epProcs = new EpisodeProcedureCollection();
            epProcs.Query.Where(epProcs.Query.RegistrationNo == registrationNo,
                epProcs.Query.IsVoid == false);
            epProcs.Query.OrderBy(epProcs.Query.ProcedureDate.Ascending);
            epProcs.LoadAll();
            if (epProcs.Count > 0)
            {
                foreach (var proc in epProcs)
                {
                    var ed = CreateNewlDischargeProcedure(eps);

                    ed.ParamedicID = proc.ParamedicID;

                    // Override ambil ParamedicID dari yg membuat laporan Operasi
                    if (proc.BookingNo != null && proc.OpNotesSeqNo != null)
                    {
                        var note = new ServiceUnitBookingOperatingNotes();
                        if (note.LoadByPrimaryKey(proc.BookingNo, proc.OpNotesSeqNo))
                            ed.ParamedicID = note.ParamedicID;
                    }

                    ed.ProcedureID = proc.ProcedureID;
                    ed.ProcedureName = proc.ProcedureName;
                    ed.ProcedureSynonym = proc.ProcedureSynonym;
                }
            }
            grdProcedure.Rebind();

        }

        public void Rebind(bool isEdited)
        {
            //Toogle grid command
            grdProcedure.Columns[0].Visible = isEdited;
            grdProcedure.Columns[grdProcedure.Columns.Count - 1].Visible = isEdited;

            grdProcedure.MasterTableView.CommandItemDisplay = isEdited
                ? GridCommandItemDisplay.Top
                : GridCommandItemDisplay.None;

            if (!isEdited)
            {
                grdProcedure.MasterTableView.ClearEditItems();
                grdProcedure.MasterTableView.IsItemInserted = false;
            }
            else
            {
                // Insert Mode
                grdProcedure.MasterTableView.IsItemInserted = true;
            }

            //Perbaharui tampilan dan data
            grdProcedure.Rebind();
        }

        private string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        protected void grdProcedure_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack || DischargeProcedures == null)
            {
                var qr = new MedicalDischargeSummaryProcedureQuery("ed");

                qr.Where(qr.RegistrationNo == RegistrationNo);
                qr.OrderBy(qr.SequenceNo.Ascending);

                var coll = new MedicalDischargeSummaryProcedureCollection();
                if (IsCallFromCaseMix)
                {
                    qr.es.QuerySource = "MedicalDischargeSummaryProcedureCmx";
                    coll.Query.es.QuerySource = "MedicalDischargeSummaryProcedureCmx";
                }
                coll.Load(qr);

                // Harus direset ke aslinya karena jika tidak maka akan selalu pakai setingan terakhir walaupun untuk variable baru
                if (IsCallFromCaseMix)
                {
                    // Switch query source
                    qr.es.QuerySource = "MedicalDischargeSummaryProcedure";
                    coll.Query.es.QuerySource = "MedicalDischargeSummaryProcedure";
                }

                DischargeProcedures = coll;
            }

            grdProcedure.DataSource = DischargeProcedures;
        }
        private MedicalDischargeSummaryProcedureCollection DischargeProcedures
        {
            get
            {
                if (Session[SessionName] == null)
                    return null;

                return (MedicalDischargeSummaryProcedureCollection)Session[SessionName];
            }
            set
            {
                Session[SessionName] = value;
            }
        }
        private MedicalDischargeSummaryProcedure CreateNewlDischargeProcedure(MedicalDischargeSummaryProcedureCollection dischargeProc)
        {
            var ed = dischargeProc.AddNew();
            ed.ProcedureID = string.Empty;
            ed.ProcedureSynonym = string.Empty;

            var lastSeqNo = string.Empty;
            foreach (var item in dischargeProc)
            {
                if (!string.IsNullOrEmpty(item.SequenceNo))
                    lastSeqNo = item.SequenceNo;
            }

            var newSeqNo = (lastSeqNo == null || string.IsNullOrEmpty(lastSeqNo))
                ? "001"
                : string.Format("{0:000}", int.Parse(lastSeqNo) + 1);
            ed.SequenceNo = newSeqNo;
            ed.RegistrationNo = RegistrationNo;
            ed.ParamedicID = AppSession.UserLogin.ParamedicID;
            ed.IsVoid = false;

            return ed;
        }
        protected void grdProcedure_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var editableItem = ((GridEditableItem)e.Item);

            //Last entity
            var ep = CreateNewlDischargeProcedure(DischargeProcedures);

            //populate its properties
            Hashtable values = new Hashtable();
            editableItem.ExtractValues(values);
            ep.ProcedureID = (string)values["ProcedureID"];
            ep.ProcedureName = (string)values["ProcedureName"];
            ep.ProcedureSynonym = (string)values["ProcedureSynonym"];

            // Show hasil insert
            grdProcedure.Rebind();

        }
        protected void grdProcedure_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var editableItem = ((GridEditableItem)e.Item);
            var sequenceNo = editableItem.GetDataKeyValue("SequenceNo").ToString();
            var ep = DischargeProcedures.FirstOrDefault(n => n.SequenceNo == sequenceNo);
            if (ep != null)
            {
                //update entity's state
                editableItem.UpdateValues(ep);
            }
            grdProcedure.Rebind();

        }
        protected void grdProcedure_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var sequenceNo = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["SequenceNo"].ToString();
            var ep = DischargeProcedures.FirstOrDefault(n => n.SequenceNo == sequenceNo);
            if (ep != null)
            {
                if (ep.es.IsAdded)
                    ep.MarkAsDeleted();
                else
                    ep.IsVoid = !ep.IsVoid;
            }
        }

        protected void grdProcedure_OnItemCommand(object sender, GridCommandEventArgs e)
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

        protected void grdProcedure_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                if (e.Item.ItemIndex >= 0)
                {
                    ComboBox.PopulateWithProcedureSynonym(cboProcedureSynonym, DischargeProcedures[e.Item.ItemIndex].ProcedureID);
                }
            }
        }

        protected void cboProcedureID_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value))
            {
                cboProcedureSynonym.Items.Clear();
                cboProcedureSynonym.SelectedValue = string.Empty;
                cboProcedureSynonym.Text = string.Empty;

                return;
            }

            ComboBox.PopulateWithProcedureSynonym(cboProcedureSynonym, e.Value);
            cboProcedureSynonym.SelectedIndex = 1;
        }
    }
}