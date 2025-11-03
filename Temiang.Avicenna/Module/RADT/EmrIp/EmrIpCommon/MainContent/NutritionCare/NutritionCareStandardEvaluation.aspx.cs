using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NutritionCare
{
    public partial class NutritionCareStandardEvaluation : BasePageDialog
    {
        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            switch (eventArgument)
            {

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDateTimeImplementation.SelectedDate = DateTime.Now;
                txtMonitoringEvaluation.Text = string.Empty;
                //var diag = new NutritionCareDiagnoseTransDT();
                //if (diag.LoadByPrimaryKey(idL10))
                //{
                //    txtA.Text = diag.TerminologyName;
                //}

                hfID.Value = string.Empty;
            }
        }
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            if (true)
            {
                return "oWnd.argument.result = 'OK'";
            }
            else
            {
                return "oWnd.argument.result = 'Gak OK'";
            }
        }
        public override bool OnButtonOkClicked()
        {
            var hd = Common.NutritionCare.SetTransHD(RegNo, AppSession.UserLogin.UserID);

            var dtDiag = new NutritionCareDiagnoseTransDTCollection();
            dtDiag.Query.Where(dtDiag.Query.TransactionNo == hd.TransactionNo,
                dtDiag.Query.SRNutritionCareTerminologyLevel.In(new string[] { "10", "30", "40" }));
            dtDiag.LoadAll();

            //dtDiag = Common.NutritionCare.RemoveClosedDiagnosaAndChildRelated(dtDiag);

            var dtEval = GetNutritionCareEvaluation();

            Common.NutritionCare.SaveDiagL40(dtDiag, GetNutritionCareEvaluation(),
                PopulateNutritionCareEvaluationIntervention(), hd, idL10);

            return true;
        }

        private NutritionCareDiagnoseTransDT GetNutritionCareEvaluation()
        {
            var diagL10 = new NutritionCareDiagnoseTransDT();
            diagL10.LoadByPrimaryKey(idL10);
            NutritionCareDiagnoseTransDT entity = new NutritionCareDiagnoseTransDT(); ;

            var id = hfID.Value;
            if (!string.IsNullOrEmpty(id))
            {
                entity.LoadByPrimaryKey(System.Convert.ToInt64(id));
                // edit yang sudah pernah disimpan di database
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
            else
            {
                // ini data baru coy
                entity.AddNew();

                entity.CreateByUserID = AppSession.UserLogin.UserID;
                entity.CreateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            entity.TransactionNo = Common.NutritionCare.GetTransHD(RegNo)[0].TransactionNo;
            entity.SRNutritionCareTerminologyLevel = "40";

            var TerminologyParentID = diagL10.TerminologyID;
            var ParentID = diagL10.ID;

            entity.TerminologyID = string.Empty;
            entity.TerminologyName = string.Empty;
            entity.TerminologyParentID = TerminologyParentID;
            entity.ParentID = ParentID;

            var dttbl = NutritionCareDiagnoseTransDT.NutritionCareDiagnosaFullDefinitionSingleWithNicNoc(entity.TransactionNo, entity.ParentID ?? 0);
            entity.O = string.Empty; //(string)dttbl.Rows[0]["NOC"];
            entity.D = (string)dttbl.Rows[0]["TerminologyNameDisplay"];
            entity.I = (string)dttbl.Rows[0]["NIC"];
            entity.ME = txtMonitoringEvaluation.Text;
            entity.ExecuteDateTime = txtDateTimeImplementation.SelectedDate.Value;

            entity.TmpTerminologyID = string.Empty;
            entity.TmpTerminologyParentID = string.Empty;

            return entity;
        }

        private NutritionCareDiagnoseTransDTCollection PopulateNutritionCareEvaluationIntervention()
        {
            // ambil intervensi 
            NutritionCareDiagnoseTransDTCollection selectedNic = new NutritionCareDiagnoseTransDTCollection();
            foreach (GridDataItem x in gridListRencana.MasterTableView.Items)
            {
                var chk = x.FindControl("defaultChkBox") as System.Web.UI.WebControls.CheckBox;
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        var adt = new NutritionCareDiagnoseTransDT();
                        adt.TerminologyID = x.GetDataKeyValue("TerminologyID").ToString();
                        var tb = x.FindControl("txtTerminologyName") as Telerik.Web.UI.RadTextBox;
                        if (tb != null)
                        {
                            adt.TerminologyName = tb.Text;
                        }
                        adt.TerminologyParentID = x["TerminologyParentID"].Text;
                        adt.ParentID = idL10;
                        adt.TmpIdEvaluation = string.Empty;

                        selectedNic.AttachEntity(adt);
                    }
                }
            }
            return selectedNic;
        }

        public long idL10
        {
            get
            {
                return System.Convert.ToInt64(Request.QueryString["idL10"]);
            }
        }

        public string RegNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }

        public string FullDiagnosaName
        {
            get
            {
                return Request.QueryString["name"];
            }
        }

        public bool IsDiagnoseClosed
        {
            get { return (bool)(ViewState["IsDiagnoseClosed"] ?? false); }
            set { ViewState["IsDiagnoseClosed"] = value; }
        }

        protected void gridListEvaluasi_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var diag = new NutritionCareDiagnoseTransDT();
            if (diag.LoadByPrimaryKey(idL10))
            {
                ((RadGrid)source).MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                ((RadGrid)source).MasterTableView.Columns[0].Visible = false;

                var ds = NutritionCareDiagnoseTransDT.Evaluation(idL10);

                ((RadGrid)source).DataSource = ds;
            }
        }

        protected void gridListEvaluasi_ItemDataBound(object source, GridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case (GridItemType.AlternatingItem):
                case (GridItemType.Item):
                    {

                        break;
                    }
            }
        }

        protected void gridListEvaluasi_DeleteCommand(object source, GridCommandEventArgs e)
        {
            
        }

        private void SetEvaluation(NutritionCareDiagnoseTransDTCollection dtDiag)
        {
            
        }

        private void SaveEvaluation(NutritionCareTransHD hd, NutritionCareDiagnoseTransDTCollection dtDiag)
        {
           
        }

        //private void SetNOCEvaluation(NutritionCareDiagnoseTransDTCollection dtDiag)
        //{
        //    SetSelectedDiagnosaEvaluasi(dtDiag);
        //}

        //private void SetSelectedDiagnosaEvaluasi(NutritionCareDiagnoseTransDTCollection dtDiag)
        //{
        //    GridItem[] nestedViewItems = gridListEvaluasi.MasterTableView.GetItems(GridItemType.NestedView);
        //    foreach (GridNestedViewItem nestedViewItem in nestedViewItems)
        //    {
        //        //foreach (GridTableView nestedView in nestedViewItem.NestedTableViews)
        //        //{
        //        // DETAIL 1

        //        // DETAIL 2
        //        var nestedView = nestedViewItem.NestedTableViews[1];
        //        foreach (GridDataItem x in nestedView.Items)
        //        {
        //            var chk = x.FindControl("defaultChkBox") as System.Web.UI.WebControls.CheckBox;
        //            if (chk != null)
        //            {
        //                if (chk.Checked)
        //                {
        //                    var rb = x.FindControl("rbDefaultEvaluasi") as System.Web.UI.WebControls.RadioButtonList;
        //                    if (rb != null)
        //                    {
        //                        var ndID = x.GetDataKeyValue("TerminologyID").ToString();
        //                        var dts = from a in dtDiag where a.TerminologyID == ndID select a;
        //                        foreach (var dt in dts)
        //                        {
        //                            dt.Evaluasi = int.Parse(rb.SelectedValue);
        //                            //var d = x.FindControl("ExecuteDateTime") as RadDateTimePicker;
        //                            //if (d != null) {
        //                            //    dt.ExecuteDateTime = d.SelectedDate;
        //                            //}
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        //}
        //    }
        //}


        //SetNOCEvaluation(dtDiag);
        //        SetEvaluation(dtDiag);
        //        SaveEvaluation(hd, dtDiag);

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            
        }

        #region NIC
        protected void gridListRencana_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            // ambil ID Diagnosa
            gridListRencana.DataSource =
                NutritionCareDiagnoseTransDT.NutritionCarePlanning(idL10);

            FilterGridListRencana();
        }
        protected void gridListRencana_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem x = (GridDataItem)e.Item;
                
                var defaultchk = x.FindControl("defaultChkBox") as System.Web.UI.WebControls.CheckBox;
                var chkIsDetail = x.FindControl("chkIsDetail") as System.Web.UI.WebControls.CheckBox;
                var chkTransTerminologyID = x.FindControl("chkTransTerminologyID") as System.Web.UI.WebControls.CheckBox;
                var txtTerminologyName = x.FindControl("txtTerminologyName") as Telerik.Web.UI.RadTextBox;

                if (defaultchk != null)
                {
                    defaultchk.Enabled = chkIsDetail.Checked;
                    txtTerminologyName.ReadOnly = !defaultchk.Enabled;
                    txtTerminologyName.Font.Bold = !defaultchk.Enabled ? true : false;
                    txtTerminologyName.ForeColor = !defaultchk.Enabled ? System.Drawing.Color.Red : System.Drawing.Color.Black;
                }
            }
        }

        private void FilterGridListRencana()
        {
            string rowFilter = string.Format("Isnull(TransTerminologyID,'') <> ''");
            (gridListRencana.DataSource as DataTable).DefaultView.RowFilter = rowFilter;

            //switch (cboP.SelectedValue)
            //{
            //    case "":
            //    case "01":
            //    case "02":
            //        {
            //            string rowFilter = string.Format("Isnull(TransTerminologyID,'') <> ''");
            //            (gridListRencana.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
            //            break;
            //        }
            //    case "03":
            //        {
            //            (gridListRencana.DataSource as DataTable).DefaultView.RowFilter = "";
            //            break;
            //        }
            //}
        }
        #endregion
    }
}