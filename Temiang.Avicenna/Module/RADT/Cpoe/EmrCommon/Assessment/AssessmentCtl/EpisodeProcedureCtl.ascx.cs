using System;
using System.Collections;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    public partial class EpisodeProcedureCtl : BaseAssessmentCtl
    {
        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            Rebind(RegistrationInfoMedicID, DataModeCurrent != AppEnum.DataMode.Read);
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            // Save EpisodeProcedure
            Save(rim.RegistrationNo, rim.RegistrationInfoMedicID);
        }

        protected override void OnDataModeChanged(bool isEdited)
        {
            Rebind(RegistrationInfoMedicID, isEdited);
        }

        #endregion



        private string _sessionName = null;
        private string SessionName
        {
            get
            {
                if (_sessionName == null)
                    _sessionName = string.Format("epsproc_{0}", Helper.PageID(this.Page));
                return _sessionName;
            }
        }

        private RadComboBox cboProcedureSynonym
        {
            get { return (RadComboBox)Helper.FindControlRecursive(Page, "cboProcedureSynonym"); }
        }

        public void Save(string regNo, string rimid)
        {
            var diags = EpisodeProcedures;
            var isAddExist = false;
            foreach (var ed in diags)
            {
                if (ed.es.IsAdded)
                {
                    isAddExist = true;
                    break;
                }
            }

            var lastSeqNo = "0";
            if (isAddExist)
            {
                // Update SeqNo
                var qr = new EpisodeProcedureQuery("ed");
                qr.Select(qr.SequenceNo.Max().As("SequenceNo"));
                qr.Where(qr.RegistrationNo == regNo);
                var dtb = qr.LoadDataTable();

                if (dtb.Rows.Count > 0)
                {
                    lastSeqNo = (dtb.Rows[0][0]).ToString();
                    if (string.IsNullOrEmpty(lastSeqNo))
                        lastSeqNo = "0";
                }

            }
            foreach (var ed in diags)
            {
                if (ed.es.IsDeleted) continue;

                if (ed.es.IsAdded)
                {
                    ed.RegistrationNo = regNo;
                    ed.RegistrationInfoMedicID = rimid;
                    ed.ProcedureDate2 = ed.ProcedureDate;
                    ed.ProcedureTime2 = ed.ProcedureTime;
                    ed.ParamedicID2 = String.Empty;
                    ed.SRProcedureCategory = "04"; // 04	Kecil
                    ed.SRAnestesi = String.Empty;
                    ed.RoomID = String.Empty;
                    ed.IsCito = false;
                    ed.ParamedicID = AppSession.UserLogin.ParamedicID;
                    ed.IsVoid = false;

                    var newSeqNo = string.Format("{0}", int.Parse(lastSeqNo) + 1);
                    ed.SequenceNo = newSeqNo;
                    lastSeqNo = newSeqNo;
                }


                // Hapus jika kosong
                if (string.IsNullOrEmpty(ed.ProcedureID) && string.IsNullOrEmpty(ed.ProcedureName))
                {
                    ed.MarkAsDeleted();
                }
            }

            diags.Save();
        }

        private void Rebind(string rimid, bool isEdited)
        {
            // Load Data
            EpisodeProcedures = LoadEpisodeProcedures(rimid);

            //Toogle grid command
            RefreshMenu(isEdited);
        }

        public void RefreshMenu(bool isEdited)
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

        private EpisodeProcedureCollection LoadEpisodeProcedures(string rimid)
        {

            var qr = new EpisodeProcedureQuery("ed");

            qr.Where(qr.RegistrationInfoMedicID == rimid);
            qr.OrderBy(qr.SequenceNo.Ascending);

            var coll = new EpisodeProcedureCollection();
            coll.Load(qr);
            return coll;

        }


        protected void grdProcedure_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdProcedure.DataSource = EpisodeProcedures;
        }
        private EpisodeProcedureCollection EpisodeProcedures
        {
            get
            {
                if (Session[SessionName] == null)
                    return null;

                return (EpisodeProcedureCollection)Session[SessionName];
            }
            set
            {
                Session[SessionName] = value;
            }
        }
        private EpisodeProcedure CreateNewEpisodeProcedure(EpisodeProcedureCollection epProc)
        {
            var ed = epProc.AddNew();
            ed.ProcedureID = string.Empty;
            ed.ProcedureSynonym = string.Empty;

            var lastSeqNo = string.Empty;
            foreach (var item in epProc)
            {
                if (!string.IsNullOrEmpty(item.SequenceNo))
                    lastSeqNo = item.SequenceNo;
            }

            var newSeqNo = (lastSeqNo == null || string.IsNullOrEmpty(lastSeqNo))
                ? "1"
                : string.Format("{0}", int.Parse(lastSeqNo) + 1);

            ed.SequenceNo = newSeqNo;
            return ed;
        }
        protected void grdProcedure_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var editableItem = ((GridEditableItem)e.Item);

            //Last entity
            var ep = CreateNewEpisodeProcedure(EpisodeProcedures);

            //populate its properties
            Hashtable values = new Hashtable();
            editableItem.ExtractValues(values);
            ep.ProcedureID = (string)values["ProcedureID"];
            ep.ProcedureName = (string)values["ProcedureName"];
            var time = (string)values["ProcedureTime"]; // 0201
            ep.ProcedureTime = string.Format("{0}:{1}", time.Substring(0, 2), time.Substring(2, 2));
            ep.ProcedureSynonym = (string)values["ProcedureSynonym"];

            // ProcedureDate tidak di Bind karena RadDatePicker akan error ketika SelectedDate terset null
            var editFormItem = (GridEditFormItem)e.Item;
            var txtProcedureDate = (RadDatePicker)editFormItem.FindControl("txtProcedureDate");
            if (!txtProcedureDate.IsEmpty)
                ep.ProcedureDate = txtProcedureDate.SelectedDate;

            // Show hasil insert
            grdProcedure.Rebind();

        }
        protected void grdProcedure_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var editableItem = ((GridEditableItem)e.Item);
            var sequenceNo = editableItem.GetDataKeyValue("SequenceNo").ToString();
            var ep = EpisodeProcedures.FirstOrDefault(n => n.SequenceNo == sequenceNo);
            if (ep != null)
            {
                //update entity's state
                editableItem.UpdateValues(ep);

                ep.ProcedureTime = string.Format("{0}:{1}", ep.ProcedureTime.Substring(0, 2), ep.ProcedureTime.Substring(2, 2));

                // ProcedureDate tidak di Bind karena RadDatePicker akan error ketika SelectedDate terset null
                var editFormItem = (GridEditFormItem)e.Item;
                var txtProcedureDate = (RadDatePicker)editFormItem.FindControl("txtProcedureDate");
                if (!txtProcedureDate.IsEmpty)
                    ep.ProcedureDate = txtProcedureDate.SelectedDate;
            }
            grdProcedure.Rebind();

        }
        protected void grdProcedure_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var sequenceNo = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["SequenceNo"].ToString();
            var ep = EpisodeProcedures.FirstOrDefault(n => n.SequenceNo == sequenceNo);
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

        private DateTime _procedureDate;
        protected void grdProcedure_ItemDataBound(object sender, GridItemEventArgs e)
        {
            // ProcedureDate tidak di Bind karena RadDatePicker akan error ketika SelectedDate terset null

            // 01. Ambil value ProcedureDate 
            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;
                string itemValue = dataItem["ProcedureDate"].Text;
                if (!string.IsNullOrWhiteSpace(itemValue))
                {
                    var vals = itemValue.Split('/');
                    _procedureDate = new DateTime(vals[2].ToInt(), vals[1].ToInt(), vals[0].ToInt());
                }
            }

            //02. Set SelectedDate
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                if (e.Item.ItemIndex >= 0)
                {
                    ComboBox.PopulateWithProcedureSynonym(cboProcedureSynonym, EpisodeProcedures[e.Item.ItemIndex].ProcedureID);
                }

                var editFormItem = (GridEditFormItem)e.Item;
                var txtProcedureDate = (RadDatePicker)editFormItem.FindControl("txtProcedureDate");
                if (_procedureDate == null || _procedureDate.Year == 1)
                    txtProcedureDate.SelectedDate = DateTime.Today;
                else
                    txtProcedureDate.SelectedDate = _procedureDate;
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