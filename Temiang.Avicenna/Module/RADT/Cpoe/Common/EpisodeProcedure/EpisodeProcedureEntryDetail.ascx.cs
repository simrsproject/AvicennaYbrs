using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using DataRow = System.Data.DataRow;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class EpisodeProcedureEntryDetail : BaseUserControl
    {
        private RadTextBox TxtSRProcedureCategory
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtSRProcedureCategory"); }
        }

        public object DataItem { get; set; }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRProcedureCategory, AppEnum.StandardReference.ProcedureCategory);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var collProcedure = (EpisodeProcedureCollection)Session["collEpisodeProcedure" + Request.UserHostName];
                if (!collProcedure.Any())
                    ViewState["EpProcSequenceNo"] = "001";
                else
                {
                    int seqNo = 0;
                    foreach (EpisodeProcedure item in collProcedure)
                    {
                        if (int.Parse(item.SequenceNo) > seqNo)
                            seqNo = int.Parse(item.SequenceNo);
                    }
                    ViewState["EpProcSequenceNo"] = string.Format("{0:000}", seqNo + 1);
                }
                cboSRProcedureCategory.SelectedValue = TxtSRProcedureCategory.Text;

                hdnCreatedBy.Value = AppSession.UserLogin.UserID;
                return;
            }
            ViewState["IsNewRecord"] = false;
            ViewState["EpProcSequenceNo"] = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.SequenceNo);

            var qProc = new ProcedureQuery();
            qProc.Where(qProc.ProcedureID == (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.ProcedureID));
            var tab = qProc.LoadDataTable();
            foreach (DataRow row in tab.Rows)
            {
                cboProcedureID.Items.Add(new RadComboBoxItem(row["ProcedureID"].ToString(), row["ProcedureID"].ToString()));
            }

            var qProcSyn = new ProcedureSynonymQuery("a");
            qProcSyn.Where(qProcSyn.ProcedureID == (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.ProcedureID));
            DataTable dtbSyn = qProcSyn.LoadDataTable();

            foreach (DataRow row in dtbSyn.Rows)
            {
                cboSynonym.Items.Add(new RadComboBoxItem(row["SynonymText"].ToString(), row["SequenceNo"].ToString()));
            }

            cboProcedureID.SelectedValue = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.ProcedureID);
            txtProcedureText.Text = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.ProcedureName);
            cboSRProcedureCategory.SelectedValue = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.SRProcedureCategory);
            hdnCreatedBy.Value = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.CreateByUserID);

            cboSynonym.Text = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.ProcedureSynonym);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (EpisodeProcedureCollection)Session["collEpisodeProcedure" + Request.UserHostName];

                //TODO: Betulkan cara pengecekannya
                string procId = cboProcedureID.SelectedValue;
                string procName = txtProcedureText.Text;
                //bool isExist = coll.Any(item => item.ProcedureID.Equals(procId) && item.ProceduxreName.Equals(procName) && item.IsVoid == false);
                bool isExist = coll.Any(item => item.SequenceNo.Equals(SequenceNo)); // ketika ada 2x tindakan dengan diagnose-icd9 yg sama bisa di input - apip 20251008
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Procedure: [{0}] {1} has exist", procId, procName);
                    return;
                }
            }
            if (txtProcedureText.Text == string.Empty)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Procedure Text required.");
                return;
            }
            if (string.IsNullOrEmpty(cboSRProcedureCategory.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Category required.");
                return;
            }
        }

        #region Properties for return entry value

        public String SequenceNo
        {
            get { return (string)ViewState["EpProcSequenceNo"]; }
        }

        public String ProcedureID
        {
            get { return cboProcedureID.SelectedValue; }
        }

        public String ProcedureName
        {
            get { return txtProcedureText.Text; }
        }

        public String SRProcedureCategory
        {
            get { return cboSRProcedureCategory.SelectedValue; }
        }

        public String ProcedureCategoryName
        {
            get { return cboSRProcedureCategory.Text; }
        }

        public String CreatedByUserID
        {
            get { return hdnCreatedBy.Value; }
        }

        public String ProcedureSynonym
        {
            get { return cboSynonym.Text; }
        }
        #endregion

        protected void cboProcedureID_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value))
            {
                cboSynonym.Items.Clear();
                cboSynonym.SelectedValue = string.Empty;
                cboSynonym.Text = string.Empty;

                return;
            }

            ComboBox.PopulateWithProcedureSynonym(cboSynonym, e.Value);
            cboSynonym.SelectedIndex = 1;
        }
    }
}