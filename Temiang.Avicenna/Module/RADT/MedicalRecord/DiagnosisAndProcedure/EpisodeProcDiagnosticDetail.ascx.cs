using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class EpisodeProcDiagnosticDetail : BaseUserControl
    {
        private RadTextBox TxtRegistrationNo
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtRegistrationNo"); }
        }

        public object DataItem { get; set; }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var collProcedure = (EpisodeProcedureCollection)Session["collEpisodeProcedureDiagnostic" + Request.UserHostName];
                if (!collProcedure.Any())
                    ViewState["SequenceNo"] = "901";
                else
                {
                    //int seqNo = int.Parse(collProcedure[collProcedure.Count - 1].SequenceNo) + 1;
                    //ViewState["SequenceNo"] = string.Format("{0:000}", seqNo);

                    int seqNo = 0;
                    foreach (EpisodeProcedure item in collProcedure)
                    {
                        if (int.Parse(item.SequenceNo) > seqNo)
                            seqNo = int.Parse(item.SequenceNo);
                    }
                    ViewState["SequenceNo"] = string.Format("{0:000}", seqNo + 1);
                }

                var reg = new Registration();
                reg.LoadByPrimaryKey(TxtRegistrationNo.Text);
                if (!string.IsNullOrEmpty(reg.ParamedicID))
                {
                    var mq = new ParamedicQuery();
                    mq.Where(mq.ParamedicID == reg.ParamedicID);
                    cboParamedicID.DataSource = mq.LoadDataTable();
                    cboParamedicID.DataBind();
                    cboParamedicID.SelectedValue = reg.ParamedicID;
                }

                txtProcedureDate.SelectedDate = DateTime.Now;
                txtProcedureTime.Text = DateTime.Now.ToString("HH:mm");

                if (cboParamedicID.Items.Count > 1)
                    cboParamedicID.SelectedValue = reg.ParamedicID;

                return;
            }
            ViewState["IsNewRecord"] = false;
            ViewState["SequenceNo"] = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.SequenceNo);

            var qProc = new ProcedureQuery();
            qProc.Where(qProc.ProcedureID == (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.ProcedureID));
            var tab = qProc.LoadDataTable();
            cboProcedureID.DataSource = tab;
            cboProcedureID.DataBind();

            txtProcedureText.Text = tab.Rows[0]["ProcedureName"].ToString();
            txtProcedureDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.ProcedureDate);
            txtProcedureTime.Text = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.ProcedureTime);

            var surgeon1 = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.ParamedicID);
            if (!string.IsNullOrEmpty(surgeon1))
            {
                var mq = new ParamedicQuery();
                mq.Where(mq.ParamedicID == surgeon1);
                cboParamedicID.DataSource = mq.LoadDataTable();
                cboParamedicID.DataBind();
                cboParamedicID.SelectedValue = surgeon1;
            }

            cboProcedureID.SelectedValue = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.ProcedureID);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(cboProcedureID.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Procedure required.");
                return;
            }
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (EpisodeProcedureCollection)Session["collEpisodeProcedureDiagnostic" + Request.UserHostName];

                //TODO: Betulkan cara pengecekannya
                string itemID = cboProcedureID.SelectedValue;
                string procedureTime = txtProcedureTime.Text;
                bool isExist = coll.Any(item => item.ProcedureID.Equals(itemID) && item.ProcedureTime.Equals(procedureTime));
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Procedure ID: {0} has exist.", itemID);
                }
            }
        }

        protected void cboProcedureID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ProcedureID"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ProcedureID"].ToString();
        }

        protected void cboProcedureID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);

            var query = new ProcedureQuery();
            if (AppSession.Parameter.HealthcareInitial == "RSMP")
                query.Where
                    (
                        query.Or
                            (
                                 query.ProcedureName.Like(searchTextContain),
                                 query.ProcedureID.Equal(e.Text)
                            )
                    );
            else
                query.Where
                    (
                        query.Or
                            (
                                 query.ProcedureName.Like(searchTextContain),
                                 query.ProcedureID.Like(searchTextContain)
                            )
                    );
            query.OrderBy(query.ProcedureName.Ascending);
            query.es.Top = 50;

            cboProcedureID.DataSource = query.LoadDataTable();
            cboProcedureID.DataBind();
        }

        protected void cboProcedureID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value))
            {
                txtProcedureText.Text = string.Empty;
                return;
            }
            var proc = new Procedure();
            proc.LoadByPrimaryKey(e.Value);
            txtProcedureText.Text = proc.ProcedureName;
        }

        protected void cboPhysicianID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboPhysicianID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var cbo = (RadComboBox)o;

            cbo.Items.Clear();
            string searchTextContain = string.Format("%{0}%", e.Text);
            var medic = new ParamedicQuery("a");
            medic.Where(
                medic.ParamedicName.Like(searchTextContain),
                medic.IsActive == true);
            medic.Select(medic.ParamedicID, medic.ParamedicName);

            cbo.DataSource = medic.LoadDataTable();
            cbo.DataBind();
        }

        #region Properties for return entry value

        public String SequenceNo
        {
            get { return (string)ViewState["SequenceNo"]; }
        }

        public DateTime ProcedureDate
        {
            get { return txtProcedureDate.SelectedDate.Value; }
        }

        public String ProcedureTime
        {
            get { return txtProcedureTime.TextWithLiterals; }
        }

        public String ParamedicID
        {
            get { return cboParamedicID.SelectedValue; }
        }

        public String ParamedicName
        {
            get { return cboParamedicID.Text; }
        }

        public String ProcedureID
        {
            get { return cboProcedureID.SelectedValue; }
        }

        public String ProcedureName
        {
            get { return txtProcedureText.Text; }
        }

        #endregion
    }
}