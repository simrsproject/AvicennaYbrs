using System;
using System.Data;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI;
using System.Collections.Generic;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NutritionCare
{
    public partial class NutritionCareStandardDetailEvaluation : BaseUserControl
    {
        private object _dataItem;

        private long IdDiagL10
        {
            get
            {
                return System.Convert.ToInt64(Request.QueryString["idL10"]);
            }
        }

        private string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }

        private RadTextBox txtNutritionCareTransNo
        {
            get
            {
                return (RadTextBox)Helper.FindControlRecursive(Page, "txtNutritionCareTransNo");
            }
        }

        private NutritionCareDiagnoseTransDTCollection NutritionCareEvaluations
        {
            get
            {
                //if (IsPostBack)
                //{
                object obj = Session["collNutritionCareEvaluation" + RegistrationNo];
                if (obj != null)
                {
                    return ((NutritionCareDiagnoseTransDTCollection)(obj));
                }
                //}

                var coll = NutritionCareDiagnoseTransDT.Evaluation(txtNutritionCareTransNo.Text);

                Session["collNutritionCareEvaluation" + RegistrationNo] = coll;
                return coll;
            }
            set
            {
                string sessionName = "collNutritionCareEvaluation" + RegistrationNo;
                Session[sessionName] = value;
            }
        }

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        public string SessionName
        {
            get
            {
                return "collNutritionCareEvaluation" + RegistrationNo; //_" + SelectedParentID;
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                txtDateTimeImplementation.SelectedDate = DateTime.Now;
                txtMonitoringEvaluation.Text = string.Empty;
                hfID.Value = string.Empty;
               
                return;
            }
            ViewState["IsNewRecord"] = false;

            var query = new NutritionCareTerminologyQuery("a");

            query.Select
                (
                    query.TerminologyID,
                    query.TerminologyName
                );
            query.Where(query.TerminologyID == (String)DataBinder.Eval(DataItem, NutritionCareDiagnoseTransDTMetadata.ColumnNames.TerminologyID));

            txtDateTimeImplementation.SelectedDate = (DateTime)DataBinder.Eval(DataItem, NutritionCareDiagnoseTransDTMetadata.ColumnNames.ExecuteDateTime);
            txtMonitoringEvaluation.Text = (String)DataBinder.Eval(DataItem, NutritionCareDiagnoseTransDTMetadata.ColumnNames.ME);
            hfID.Value = (DataBinder.Eval(DataItem, NutritionCareDiagnoseTransDTMetadata.ColumnNames.ID) ?? string.Empty).ToString();
            hfTmpTerminologyID.Value = (DataBinder.Eval(DataItem, NutritionCareDiagnoseTransDTMetadata.ColumnNames.TmpTerminologyID)).ToString();
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
        }

        #region Properties for return entry value

        public String MonitoringEvaluation
        {
            get { return txtMonitoringEvaluation.Text; }
        }

        public String RecordID
        {
            get { return hfID.Value; }
        }

        public String TmpTerminologyID
        {
            get { return hfTmpTerminologyID.Value; }
        }

        public NutritionCareDiagnoseTransDT[] NIC
        {
            get
            {
                List<NutritionCareDiagnoseTransDT> selectedNic = new List<NutritionCareDiagnoseTransDT>();
                foreach (GridDataItem x in gridListRencana.MasterTableView.Items)
                {
                    var chk = x.FindControl("defaultChkBox") as System.Web.UI.WebControls.CheckBox;
                    if (chk != null)
                    {
                        if (chk.Checked)
                        {
                            var adt = new NutritionCareDiagnoseTransDT();
                            adt.TerminologyID = x.GetDataKeyValue("TerminologyID").ToString();
                            var tb = x.FindControl("txtNutritionCareDiagnosaName") as Telerik.Web.UI.RadTextBox;
                            if (tb != null)
                            {
                                adt.TerminologyName = tb.Text;
                            }
                            adt.TerminologyParentID = x["NursingDiagnosaParentID"].Text;
                            adt.ParentID = IdDiagL10;
                            adt.TmpIdEvaluation = TmpTerminologyID;

                            selectedNic.Add(adt);
                        }
                    }
                }
                return selectedNic.ToArray();
            }
        }
        #endregion
        #region Method & Event

        #endregion
        #region NIC
        protected void gridListRencana_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            // ambil ID Diagnosa
            gridListRencana.DataSource =
                NutritionCareDiagnoseTransDT.NutritionCarePlanning(IdDiagL10);

            string rowFilter = string.Format("Isnull(TransTerminologyID,'') <> ''");
            (gridListRencana.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
        }

        #endregion
    }
}