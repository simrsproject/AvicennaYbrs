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
    public partial class EpisodeDiagDetail : BaseUserControl
    {
        private RadTextBox TxtRegistrationNo
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtRegistrationNo"); }
        }

        public object DataItem { get; set; }

        protected override void OnDataBinding(EventArgs e)
        {
            var medic = new ParamedicQuery("a");

            medic.Select
                (
                    medic.ParamedicID,
                    medic.ParamedicName
                );
            //medic.Where(medic.IsActive == true);

            //DataTable table = medic.LoadDataTable();

            //cboParamedicID.Items.Clear();
            //cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            //foreach (DataRow row in table.Rows)
            //{
            //    cboParamedicID.Items.Add(new RadComboBoxItem(row["ParamedicName"].ToString(), row["ParamedicID"].ToString()));
            //}

            var reg = new Registration();
            reg.LoadByPrimaryKey(TxtRegistrationNo.Text);

            //cboParamedicID.SelectedValue = reg.ParamedicID;

            StandardReference.InitializeIncludeSpace(cboSRDiagnoseType, AppEnum.StandardReference.DiagnoseType, true);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var collDiagnose = (EpisodeDiagnoseCollection)Session["collEpisodeDiagnose" + Request.UserHostName];
                if (!collDiagnose.Any())
                {
                    ViewState["SequenceNo"] = "001";
                    cboSRDiagnoseType.SelectedValue = AppSession.Parameter.DiagnoseTypeMain;
                }
                else
                {
                    int seqNo = 0;
                    foreach (EpisodeDiagnose item in collDiagnose)
                    {
                        if (int.Parse(item.SequenceNo) > seqNo)
                            seqNo = int.Parse(item.SequenceNo);
                    }
                    ViewState["SequenceNo"] = string.Format("{0:000}", seqNo + 1);
                }

                //if (cboParamedicID.Items.Count > 1)
                //    cboParamedicID.SelectedValue = reg.ParamedicID;

                medic.Where(medic.ParamedicID == reg.ParamedicID);
                cboParamedicID.DataSource = medic.LoadDataTable();
                cboParamedicID.DataBind();
                cboParamedicID.SelectedValue = reg.ParamedicID;

                chkIsOldCase.Checked = reg.IsOldCase ?? false;
                chkIsConfirmed.Checked = true;
                return;
            }
            ViewState["IsNewRecord"] = false;
            ViewState["SequenceNo"] = (String)DataBinder.Eval(DataItem, EpisodeDiagnoseMetadata.ColumnNames.SequenceNo);

            var qDiag = new DiagnoseQuery();
            qDiag.Where(qDiag.DiagnoseID == (String)DataBinder.Eval(DataItem, EpisodeDiagnoseMetadata.ColumnNames.DiagnoseID));
            DataTable dtb = qDiag.LoadDataTable();

            cboDiagnoseID.DataSource = dtb;
            cboDiagnoseID.DataBind();

            cboDiagnoseID.SelectedValue = (String)DataBinder.Eval(DataItem, EpisodeDiagnoseMetadata.ColumnNames.DiagnoseID);
            PopulateDiagnoseName(true);
            cboSRDiagnoseType.SelectedValue = (String)DataBinder.Eval(DataItem, EpisodeDiagnoseMetadata.ColumnNames.SRDiagnoseType);
            txtDiagnosisText.Text = (String)DataBinder.Eval(DataItem, EpisodeDiagnoseMetadata.ColumnNames.DiagnosisText);

            if (!string.IsNullOrEmpty((String)DataBinder.Eval(DataItem, EpisodeDiagnoseMetadata.ColumnNames.MorphologyID)))
            {
                var qmorf = new MorphologyQuery();
                qmorf.Where(qmorf.MorphologyID == (String)DataBinder.Eval(DataItem, EpisodeDiagnoseMetadata.ColumnNames.MorphologyID));
                DataTable dtbm = qmorf.LoadDataTable();

                cboMorphologyID.DataSource = dtbm;
                cboMorphologyID.DataBind();

                cboMorphologyID.SelectedValue = (String)DataBinder.Eval(DataItem, EpisodeDiagnoseMetadata.ColumnNames.MorphologyID);
            }

            if (!string.IsNullOrEmpty((String)DataBinder.Eval(DataItem, EpisodeDiagnoseMetadata.ColumnNames.ExternalCauseID)))
            {
                var qext = new DiagnoseQuery();
                qext.Where(qext.DiagnoseID == (String)DataBinder.Eval(DataItem, EpisodeDiagnoseMetadata.ColumnNames.ExternalCauseID));
                DataTable dtbe = qext.LoadDataTable();

                cboExternalCauseID.DataSource = dtbe;
                cboExternalCauseID.DataBind();

                cboExternalCauseID.SelectedValue = (String)DataBinder.Eval(DataItem, EpisodeDiagnoseMetadata.ColumnNames.ExternalCauseID);
            }

            medic.Where(medic.ParamedicID == (String)DataBinder.Eval(DataItem, EpisodeDiagnoseMetadata.ColumnNames.ParamedicID));
            cboParamedicID.DataSource = medic.LoadDataTable();
            cboParamedicID.DataBind();
            cboParamedicID.SelectedValue = (String)DataBinder.Eval(DataItem, EpisodeDiagnoseMetadata.ColumnNames.ParamedicID);

            chkAcute.Checked = (bool)DataBinder.Eval(DataItem, EpisodeDiagnoseMetadata.ColumnNames.IsAcuteDisease);
            chkIsChronicDisease.Checked = (bool)DataBinder.Eval(DataItem, EpisodeDiagnoseMetadata.ColumnNames.IsChronicDisease);
            chkIsOldCase.Checked = (bool)DataBinder.Eval(DataItem, EpisodeDiagnoseMetadata.ColumnNames.IsOldCase);
            chkIsConfirmed.Checked = (bool)DataBinder.Eval(DataItem, EpisodeDiagnoseMetadata.ColumnNames.IsConfirmed);
            chkIsVoid.Checked = (bool)DataBinder.Eval(DataItem, EpisodeDiagnoseMetadata.ColumnNames.IsVoid);
            txtNotes.Text = (String)DataBinder.Eval(DataItem, EpisodeDiagnoseMetadata.ColumnNames.Notes);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(cboDiagnoseID.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Diagnosis ID required.");
                return;
            }

            EpisodeDiagnoseCollection coll = (EpisodeDiagnoseCollection)Session["collEpisodeDiagnose" + Request.UserHostName];

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                string id = cboDiagnoseID.SelectedValue;
                bool isExist = coll.Any(item => item.SRDiagnoseType.Equals(cboSRDiagnoseType.SelectedValue) && item.SRDiagnoseType.Equals(AppSession.Parameter.DiagnoseTypeMain) && item.IsVoid == false);
                //cek dobel diagnosa utama
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "Main Diagnosis has exist, only 1 main diagnosis is permitted.";
                    return;
                }

                isExist = coll.Any(item => item.SRDiagnoseType.Equals(cboSRDiagnoseType.SelectedValue) && item.SRDiagnoseType.Equals(AppSession.Parameter.DiagnoseTypeDeathDiagnosis) && item.IsVoid == false);
                //  cek dobel diagnosa kematian DiagnoseType-007 : 07 - Diagnosa Kematian
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "Death Diagnosis has exist, only 1 death diagnosis is permitted.";
                    return;
                }
            }
            else
            {
                var seqNo = ViewState["SequenceNo"].ToString();
                string id = cboDiagnoseID.SelectedValue;

                bool isExist = coll.Any(item => item.SRDiagnoseType.Equals(cboSRDiagnoseType.SelectedValue) && item.SRDiagnoseType.Equals(AppSession.Parameter.DiagnoseTypeMain) && item.IsVoid == false && item.SequenceNo != seqNo);
                //cek dobel diagnosa utama
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "Main Diagnosis has exist, only 1 main diagnosis is permitted.";
                    return;
                }

                isExist = coll.Any(item => item.SRDiagnoseType.Equals(cboSRDiagnoseType.SelectedValue) && item.SRDiagnoseType.Equals(AppSession.Parameter.DiagnoseTypeDeathDiagnosis) && item.IsVoid == false && item.SequenceNo != seqNo);
                //  cek dobel diagnosa kematian DiagnoseType-007 : 07 - Diagnosa Kematian
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "Death Diagnosis has exist, only 1 death diagnosis is permitted.";
                    return;
                }

                isExist = coll.Any(item => item.DiagnoseID.Equals(id) & item.SRDiagnoseType == cboSRDiagnoseType.SelectedValue & item.IsVoid == false & item.SequenceNo != seqNo);
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Diagnose ID : {0} as {1} has exist", id, cboSRDiagnoseType.Text);
                    return;
                }
            }

            if (AppSession.Parameter.IsEpisodeDiagValidateExtCauseAndMorp)
            {
                if (!string.IsNullOrEmpty(cboDiagnoseID.SelectedValue))
                {
                    if (cboDiagnoseID.SelectedValue.Substring(0, 1) == "S" || cboDiagnoseID.SelectedValue.Substring(0, 1) == "T")
                    {
                        if (string.IsNullOrEmpty(cboExternalCauseID.SelectedValue))
                        {
                            args.IsValid = false;
                            ((CustomValidator)source).ErrorMessage = string.Format("External Cause required.");
                            return;
                        }
                    }

                    if (cboDiagnoseID.SelectedValue.Substring(0, 1) == "D" || cboDiagnoseID.SelectedValue.Substring(0, 1) == "C")
                    {
                        var exceptions = new AppStandardReferenceItemCollection();
                        exceptions.Query.Where(
                            exceptions.Query.StandardReferenceID == AppEnum.StandardReference.IcdXMorphologyExc,
                            exceptions.Query.ItemID == cboDiagnoseID.SelectedValue);
                        exceptions.LoadAll();
                        if (string.IsNullOrEmpty(cboMorphologyID.SelectedValue) & exceptions.Count == 0)
                        {
                            args.IsValid = false;
                            ((CustomValidator)source).ErrorMessage = string.Format("Morphology required.");
                            return;
                        }
                    }
                }
            }
        }

        #region Properties for return entry value

        public String SequenceNo
        {
            get { return (string)ViewState["SequenceNo"]; }
        }

        public String DiagnoseID
        {
            get { return cboDiagnoseID.SelectedValue; }
        }

        public String DiagnoseName
        {
            get { return txtDiagnosisText.Text; }
        }

        public String SRDiagnoseType
        {
            get { return cboSRDiagnoseType.SelectedValue; }
        }

        public String DiagnoseType
        {
            get { return cboSRDiagnoseType.Text; }
        }

        public String DiagnosisText
        {
            get { return txtDiagnosisText.Text; }
        }

        public String MorphologyID
        {
            get { return cboMorphologyID.SelectedValue; }
        }

        public String MorphologyName
        {
            get { return cboMorphologyID.Text; }
        }

        public String ParamedicID
        {
            get { return cboParamedicID.SelectedValue; }
        }

        public String ParamedicName
        {
            get { return cboParamedicID.Text; }
        }

        public Boolean IsAcuteDisease
        {
            get { return chkAcute.Checked; }
        }

        public Boolean IsChronicDisease
        {
            get { return chkIsChronicDisease.Checked; }
        }

        public Boolean IsOldCase
        {
            get { return chkIsOldCase.Checked; }
        }

        public Boolean IsConfirmed
        {
            get { return chkIsConfirmed.Checked; }
        }

        public Boolean IsVoid
        {
            get { return chkIsVoid.Checked; }
        }

        public String Notes
        {
            get { return txtNotes.Text; }
        }

        public String ExternalCauseID
        {
            get { return cboExternalCauseID.SelectedValue; }
        }

        #endregion

        #region Method & Event TextChanged

        protected void cboDiagnoseID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateDiagnoseName(true);
            ChkOldCaseAuto();
        }

        private void PopulateDiagnoseName(bool isResetIdIfNotExist)
        {
            if (cboDiagnoseID.SelectedValue == string.Empty)
            {
                txtDiagnosisText.Text = string.Empty;
                cboExternalCauseID.Items.Clear();
                cboMorphologyID.Items.Clear();
                return;
            }

            Diagnose entity = new Diagnose();
            if (entity.LoadByPrimaryKey(cboDiagnoseID.SelectedValue))
                txtDiagnosisText.Text = entity.DiagnoseName;
            else
            {
                txtDiagnosisText.Text = string.Empty;
                if (isResetIdIfNotExist)
                    cboDiagnoseID.SelectedValue = string.Empty;
            }
        }

        private void ChkOldCaseAuto() {
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(TxtRegistrationNo.Text)) {
                var epColl = new EpisodeDiagnoseCollection();
                var epq = new EpisodeDiagnoseQuery("epq");
                var regq = new RegistrationQuery("regq");

                epq.InnerJoin(regq).On(epq.RegistrationNo == regq.RegistrationNo)
                    .Where(
                        regq.PatientID == reg.PatientID,
                        epq.SRDiagnoseType == AppSession.Parameter.DiagnoseTypeMain,
                        epq.RegistrationNo != reg.RegistrationNo
                    )
                    .OrderBy(regq.RegistrationDate.Descending);

                epq.es.Top = 1;

                if (epColl.Load(epq)) {
                    chkIsOldCase.Checked = (epColl.First().DiagnoseID == cboDiagnoseID.SelectedValue);
                }
            }
        }

        #endregion

        protected void cboDiagnoseID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["DiagnoseID"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["DiagnoseID"].ToString();
        }

        protected void cboDiagnoseID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            DiagnoseQuery query = new DiagnoseQuery();

            query.es.Top = 50;
            query.Where(query.IsActive == true);
            if (AppSession.Parameter.HealthcareInitial == "RSMP")
                query.Where
                (
                 query.Or
                        (
                            query.DiagnoseName.Like(searchTextContain),
                            query.DiagnoseID.Equal(searchTextContain)
                        )
                );
            else
                query.Where
                    (
                     query.Or
                            (
                                query.DiagnoseName.Like(searchTextContain),
                                query.DiagnoseID.Like(searchTextContain)
                            )
                    );
            query.OrderBy(query.DiagnoseName.Ascending);
            DataTable dtb = query.LoadDataTable();

            cboDiagnoseID.DataSource = dtb;
            cboDiagnoseID.DataBind();
        }

        protected void cboExternalCauseID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new DiagnoseQuery();

            query.es.Top = 10;
            query.Where
                (
                query.IsActive == true,
                query.Or
                        (
                            query.DiagnoseName.Like(searchTextContain),
                            query.DiagnoseID.Like(searchTextContain)
                        ),
                    query.Or(query.DiagnoseID.Substring(1, 1) == "V",
                             query.DiagnoseID.Substring(1, 1) == "W",
                             query.DiagnoseID.Substring(1, 1) == "X",
                             query.DiagnoseID.Substring(1, 1) == "Y"));
            query.OrderBy(query.DiagnoseID.Ascending);
            DataTable dtb = query.LoadDataTable();

            cboExternalCauseID.DataSource = dtb;
            cboExternalCauseID.DataBind();
        }

        protected void cboExternalCauseID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["DiagnoseID"] + " - " +
                          ((DataRowView)e.Item.DataItem)["DiagnoseName"];
            e.Item.Value = ((DataRowView)e.Item.DataItem)["DiagnoseID"].ToString();
        }

        protected void cboMorphologyID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["MorphologyID"] + " - " +
                          ((DataRowView)e.Item.DataItem)["MorphologyName"];
            e.Item.Value = ((DataRowView)e.Item.DataItem)["MorphologyID"].ToString();
        }

        protected void cboMorphologyID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new MorphologyQuery();
            query.es.Top = 20;
            query.Where
                (
                    query.Or(query.MorphologyName.Like(searchTextContain),
                    query.MorphologyID.Like(searchTextContain))
                );
            query.OrderBy(query.MorphologyID.Ascending);

            cboMorphologyID.DataSource = query.LoadDataTable();
            cboMorphologyID.DataBind();
        }

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ParamedicQuery();
            query.es.Top = 20;
            query.Where
                (
                    query.Or(query.ParamedicID == e.Text, query.ParamedicName.Like(searchTextContain)),
                    query.IsActive == true
                );
            query.OrderBy(query.ParamedicName.Ascending);

            cboParamedicID.DataSource = query.LoadDataTable();
            cboParamedicID.DataBind();
        }
    }
}