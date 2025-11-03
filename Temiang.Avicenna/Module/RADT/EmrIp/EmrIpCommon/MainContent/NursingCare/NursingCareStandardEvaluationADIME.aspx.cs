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


namespace Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NursingCare
{
    public partial class NursingCareStandardEvaluationADIME : BasePageDialog
    {
        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            switch (eventArgument)
            {

            }
        }

        private string PVal {
            get {
                switch (cboP.SelectedValue) {
                    case "03": {
                            return "03";
                            //break;
                        }
                    default: {
                            return "02";
                            //break;
                        }
                }
                return "02"; // <-- biarkan revisi karena intervensi untuk gizi tetap lanjut walau diagnosanya statusnya close
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDateTimeImplementation.SelectedDate = DateTime.Now;
                txtA.Text = string.Empty;
                txtD.Text = string.Empty;
                //txtI.Text = string.Empty;
                txtM.Text = string.Empty;
                txtE.Text = string.Empty;
                var diagdt = new NursingDiagnosaTransDT();
                if (diagdt.LoadByPrimaryKey(idL10))
                {
                    txtD.Text = FullDiagnosaName;
                }
                    
                txtPpaInstruction.Text = string.Empty;
                hfID.Value = string.Empty;

                Common.ComboBox.StandardReferenceItem(cboP, AppEnum.StandardReference.NursingCarePlanning.ToString());
            }
            if (ReadOnly) {
                ButtonCancel.Text = "Close";
                ButtonOk.Visible = false;
                tblEntryEval.Visible = false;
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
            Page.Validate();
            if (!Page.IsValid) return false;

            var hd = Common.NursingCare.SetTransHD(RegNo, AppSession.UserLogin.UserID);

            var dtDiag = new NursingDiagnosaTransDTCollection();
            dtDiag.Query.Where(dtDiag.Query.TransactionNo == hd.TransactionNo,
                dtDiag.Query.SRNursingDiagnosaLevel.In(new string[] { "10", "20", "21", "30", "40" }));
            dtDiag.LoadAll();

            dtDiag = Common.NursingCare.RemoveClosedDiagnosaAndChildRelated(dtDiag);

            Common.NursingCare.SaveDiagL40(dtDiag, GetNursingEvaluation(),
                PopulateNursingEvaluationInterventionSelected(), hd, idL10);

            return true;
        }

        private NursingDiagnosaTransDT GetNursingEvaluation() {
            var diagL10 = new NursingDiagnosaTransDT();
            diagL10.LoadByPrimaryKey(idL10);
            NursingDiagnosaTransDT entity = new NursingDiagnosaTransDT(); ;

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

            entity.TransactionNo = Common.NursingCare.GetTransHD(RegNo)[0].TransactionNo;
            entity.S = txtA.Text;
            entity.O = txtD.Text;
            //entity.A = txtI.Text;
            entity.P = txtM.Text;
            entity.Info5 = txtE.Text;

            entity.PpaInstruction = txtPpaInstruction.Text;
            entity.SRNursingCarePlanning = cboP.SelectedValue;
            var stdf = new AppStandardReferenceItem();
            //if (stdf.LoadByPrimaryKey("NursingCarePlanning", cboP.SelectedValue))
            //{
            //    entity.P = stdf.ItemName;
            //}
            entity.SRNursingDiagnosaLevel = "40";

            var NursingDiagnosaParentID = diagL10.NursingDiagnosaID;
            var ParentID = diagL10.ID;

            entity.NursingDiagnosaID = string.Empty;
            entity.NursingDiagnosaName = "ADIME";
            entity.NursingDiagnosaParentID = NursingDiagnosaParentID;
            entity.ParentID = ParentID;
            entity.Priority = 0;
            entity.EvalPeriod = 0;
            entity.PeriodConversionInHour = 24;
            entity.Skala = 1;
            entity.Target = 0;
            entity.Evaluasi = 1;
            entity.Respond = string.Empty;
            entity.Reexamine = false;
            entity.ExecuteDateTime = txtDateTimeImplementation.SelectedDate.Value;

            entity.TmpNursingDiagnosaID = string.Empty;
            entity.TmpNursingDiagnosaParentID = string.Empty;

            return entity;
        }

        private NursingDiagnosaTransDTCollection PopulateNursingEvaluationInterventionSelected() {
            // ambil intervensi 
            NursingDiagnosaTransDTCollection selectedNic = new NursingDiagnosaTransDTCollection();
            foreach (GridDataItem x in gridListRencana.MasterTableView.Items)
            {
                var chkSwitch = (x.FindControl("chkSwitch") as System.Web.UI.HtmlControls.HtmlInputCheckBox);
                if (chkSwitch.Checked) {
                    var adt = new NursingDiagnosaTransDT();
                    adt.NursingDiagnosaID = x.GetDataKeyValue("NursingDiagnosaID").ToString();
                    var tb = x.FindControl("txtNursingDiagnosaName") as Telerik.Web.UI.RadTextBox;
                    if (tb != null)
                    {
                        adt.NursingDiagnosaName = tb.Text;
                    }
                    adt.NursingDiagnosaParentID = x["NursingDiagnosaParentID"].Text;

                    adt.ParentID = idL10;
                    adt.TmpIdEvaluation = string.Empty;

                    selectedNic.AttachEntity(adt);
                }
            }
            return selectedNic;
        }

        public long idL10
        {
            get {
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

        public string FullDiagnosaName {
            get
            {
                return Request.QueryString["name"];
            }
        }

        public bool ReadOnly {
            get {
                return Request.QueryString["readonly"] == "1"; 
            }
        }

        public bool IsDiagnoseClosed
        {
            get { return (bool)(ViewState["IsDiagnoseClosed"] ?? false); }
            set { ViewState["IsDiagnoseClosed"] = value; }
        }

        protected void cboP_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            // do something
            gridListRencana.Rebind();
        }

        protected void gridListEvaluasi_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var diag = new NursingDiagnosaTransDT();
            if (diag.LoadByPrimaryKey(idL10))
            {
                IsDiagnoseClosed = diag.SRNursingCarePlanning == "01";
                if (IsDiagnoseClosed/*closed*/)
                {
                    //gridListEvaluasi.MasterTableView
                    ((RadGrid)source).MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                    ((RadGrid)source).MasterTableView.Columns[0].Visible = false;
                }
                else
                {
                    ((RadGrid)source).MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                    ((RadGrid)source).MasterTableView.Columns[0].Visible = false;
                }
                var ds = NursingDiagnosaTransDT.Evaluation(idL10);
                var eis = ds.Select(x => x.ID.Value).ToArray();
                // get interventions
                
                
                foreach (var ev in ds) {
                    ev.A = NursingDiagnosaTransDT.DetailEvaluationByEvaluationIdHtml(ev.ID.Value);
                    ev.A = ev.A.Replace("[BASEURL]", Helper.UrlRoot());
                }

                ((RadGrid)source).DataSource = ds.OrderByDescending(x => x.ExecuteDateTime);
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
            //GridDataItem item = e.Item as GridDataItem;
            //if (item == null) return;

            //try
            //{
            //    string SID = item.OwnerTableView.DataKeyValues[e.Item.ItemIndex][NursingDiagnosaTransDTMetadata.ColumnNames.Id].ToString();

            //    //String SID = item[NursingDiagnosaTransDTMetadata.ColumnNames.Id].Text;
            //    if (Equals(SID, "&nbsp;") || string.IsNullOrEmpty(SID)) throw new Exception("No ID");
            //    //NursingDiagnosaTransDT entity = FindNursingDiagnosaTransDTByID(Int64.Parse(SID));
            //    var entity = NursingEvaluations.Where(x => x.Id == Int64.Parse(SID)).First();
            //    if (entity != null)
            //        entity.MarkAsDeleted();
            //}
            //catch (Exception ex)
            //{
            //    // find by TmpNursingDiagnosaID
            //    String TmpNursingDiagnosaID = item.OwnerTableView.DataKeyValues[e.Item.ItemIndex][NursingDiagnosaTransDTMetadata.ColumnNames.TmpNursingDiagnosaID].ToString();
            //    //item[NursingDiagnosaTransDTMetadata.ColumnNames.TmpNursingDiagnosaID].Text;
            //    if (!string.IsNullOrEmpty(TmpNursingDiagnosaID))
            //    {
            //        var entity = NursingEvaluations.Where(x => x.TmpNursingDiagnosaID == TmpNursingDiagnosaID).First();
            //        if (entity != null)
            //            entity.MarkAsDeleted();
            //    }
            //}
        }

        
        private void SetNOCEvaluation(NursingDiagnosaTransDTCollection dtDiag)
        {
            SetSelectedDiagnosaEvaluasi(dtDiag);

            // evaluation tersimpan di session NursingEvaluations
        }

        private void SetSelectedDiagnosaEvaluasi(NursingDiagnosaTransDTCollection dtDiag)
        {
            GridItem[] nestedViewItems = gridListEvaluasi.MasterTableView.GetItems(GridItemType.NestedView);
            foreach (GridNestedViewItem nestedViewItem in nestedViewItems)
            {
                //foreach (GridTableView nestedView in nestedViewItem.NestedTableViews)
                //{
                // DETAIL 1

                // DETAIL 2
                var nestedView = nestedViewItem.NestedTableViews[1];
                foreach (GridDataItem x in nestedView.Items)
                {
                    var chk = x.FindControl("defaultChkBox") as System.Web.UI.WebControls.CheckBox;
                    if (chk != null)
                    {
                        if (chk.Checked)
                        {
                            var rb = x.FindControl("rbDefaultEvaluasi") as System.Web.UI.WebControls.RadioButtonList;
                            if (rb != null)
                            {
                                var ndID = x.GetDataKeyValue("NursingDiagnosaID").ToString();
                                var dts = from a in dtDiag where a.NursingDiagnosaID == ndID select a;
                                foreach (var dt in dts)
                                {
                                    dt.Evaluasi = int.Parse(rb.SelectedValue);
                                    //var d = x.FindControl("ExecuteDateTime") as RadDateTimePicker;
                                    //if (d != null) {
                                    //    dt.ExecuteDateTime = d.SelectedDate;
                                    //}
                                }
                            }
                        }
                    }
                }
                //}
            }
        }


        //SetNOCEvaluation(dtDiag);
        //        SetEvaluation(dtDiag);
        //        SaveEvaluation(hd, dtDiag);

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            ////Check duplicate key
            //if (ViewState["IsNewRecord"].Equals(true))
            //{
            //    NursingDiagnosaTransDTCollection coll = (NursingDiagnosaTransDTCollection)Session[SessionName];

            //    string NursingDiagnosaID = cboNursingDiagnosa.SelectedValue;
            //    bool isExist = false;
            //    if (!string.IsNullOrEmpty(NursingDiagnosaID))
            //    {
            //        // validasi hanya yang ada masternya aja
            //        foreach (NursingDiagnosaTransDT trns in coll)
            //        {
            //            if (trns.NursingDiagnosaID.Equals(NursingDiagnosaID))
            //            {
            //                isExist = true;
            //                break;
            //            }
            //        }
            //    }
            //    if (isExist)
            //    {
            //        args.IsValid = false;
            //        ((CustomValidator)source).ErrorMessage = string.Format("Implementation ID: {0} has exist", NursingDiagnosaID);
            //    }
            //}
        }

        #region NIC
        protected void gridListRencana_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            // ambil ID Diagnosa
            var dt = NursingDiagnosaTransDT.NursingPlanning(idL10);
            if (PVal == "01")/*stop*/
                foreach (System.Data.DataRow r in dt.Rows) {
                    r["Status"] = false;
                }
            dt.AcceptChanges();

            gridListRencana.DataSource = dt;
                
            FilterGridListRencana();
        }
        protected void gridListRencana_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem x = (GridDataItem)e.Item;
                var chk = x.FindControl("chkStop") as System.Web.UI.WebControls.CheckBox;
                if (chk != null)
                {
                    chk.Enabled = PVal != "01";
                }

                var chkSwitch = x.FindControl("chkSwitch");
                //var lbl = x.FindControl("lblSwitch");
                ((System.Web.UI.HtmlControls.HtmlControl)x.FindControl("lblSwitch")).Attributes.Add("for", chkSwitch.ClientID);
            }
        }

        private void FilterGridListRencana()
        {
            switch (PVal)
            {
                case "":
                case "01":
                case "02":
                    {
                        string rowFilter = string.Format("Isnull(TransNursingDiagnosaID,'') <> ''");
                        (gridListRencana.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
                        break;
                    }
                case "03":
                    {
                        (gridListRencana.DataSource as DataTable).DefaultView.RowFilter = "";
                        break;
                    }
            }
        }
        #endregion
    }
}
