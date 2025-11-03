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
    public partial class NursingCareStandardEvaluation : BasePageDialog
    {
        //private NursingDiagnosaTransDTCollection NursingEvaluationIntervention
        //{
        //    get
        //    {
        //        //if (IsPostBack)
        //        //{
        //        object obj = Session["collNursingEvaluationIntervention" + RegNo];
        //        if (obj == null)
        //        {
        //            obj = Session["collNursingEvaluationIntervention" + RegNo] = new NursingDiagnosaTransDTCollection();
        //        }
        //        //}
        //        return ((NursingDiagnosaTransDTCollection)(obj));
        //    }
        //    set
        //    {
        //        string sessionName = "collNursingEvaluationIntervention" + RegNo;
        //        Session[sessionName] = value;
        //    }
        //}
        //private NursingDiagnosaTransDT Evaluation
        //{
        //    get
        //    {
        //        //if (IsPostBack)
        //        //{
        //        object obj = Session["NursingEvaluation" + RegNo];
        //        if (obj == null)
        //        {
        //            obj = Session["NursingEvaluation" + RegNo] = new NursingDiagnosaTransDTCollection();
        //        }
        //        //}
        //        return ((NursingDiagnosaTransDT)(obj));
        //    }
        //    set
        //    {
        //        string sessionName = "NursingEvaluation" + RegNo;
        //        Session[sessionName] = value;
        //    }
        //}

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
                LoadCombo();
                txtDateTimeImplementation.SelectedDate = DateTime.Now;
                txtS.Text = string.Empty;
                txtO.Text = string.Empty;
                txtA.Text = string.Empty;
                var diagdt = new NursingDiagnosaTransDT();
                if (diagdt.LoadByPrimaryKey(idL10))
                {
                    if (diagdt.SRNsType == "04")
                    {
                        txtA.Text = FullDiagnosaName;
                    }
                    else
                    {
                        txtA.Text = diagdt.NursingDiagnosaName;
                    }
                }
                    
                txtPpaInstruction.Text = string.Empty;
                hfID.Value = string.Empty;

                gridListTarget.Columns[0].Visible = false;
                gridListTarget.Columns[4].Visible = true;

                tabStrip.Tabs[0].Visible = false;
                tabStrip.Tabs[1].Selected = true;
                pgNoc.Visible = false;
                pgNic.Selected = true;

                tabStrip.Tabs[0].Text = (diagdt.SRNsType == "04") ? AppSession.Parameter.NsOutcome02 :  AppSession.Parameter.NsOutcome;
                tabStrip.Tabs[1].Text = (new string[] { "04", "05"}).Contains(diagdt.SRNsType) ? "Interventions" :AppSession.Parameter.NsIntervention;


                if (AppSession.Parameter.IsNsOutcomeShowScale)
                {
                    var diag = new NursingDiagnosa();
                    if (diag.LoadByPrimaryKey(diagdt.NursingDiagnosaID))
                    {
                        if (diag.SRNsDiagnosaType == AppConstant.NsDiagnosaType.Nursing)
                        {
                            gridListTarget.Columns[4].Visible = true;

                            tabStrip.Tabs[0].Visible = true;
                            tabStrip.Tabs[1].Selected = false;
                            tabStrip.Tabs[0].Selected = true;
                            pgNoc.Visible = true;
                            pgNic.Selected = false;
                            pgNoc.Selected = true;
                        }
                    }
                }
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
            PopulateOutcome(dtDiag);
            //var dtEval = GetNursingEvaluation();

            Common.NursingCare.SaveDiagL40(dtDiag, GetNursingEvaluation(),
                PopulateNursingEvaluationInterventionSelected(), hd, idL10);

            return true;
        }

        protected void cboP_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            // do something
            gridListRencana.Rebind();
        }

        private void LoadCombo()
        {
            Common.ComboBox.StandardReferenceItem(cboP, AppEnum.StandardReference.NursingCarePlanning.ToString());
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
            entity.NursingDiagnosaName = "SOAP";
            entity.S = txtS.Text;
            entity.O = txtO.Text;
            entity.A = txtA.Text + " " + rblA.SelectedValue;
            entity.PpaInstruction = txtPpaInstruction.Text;
            entity.SRNursingCarePlanning = cboP.SelectedValue;
            var stdf = new AppStandardReferenceItem();
            if (stdf.LoadByPrimaryKey("NursingCarePlanning", cboP.SelectedValue))
            {
                entity.P = stdf.ItemName;
            }
            entity.SRNursingDiagnosaLevel = "40";

            var NursingDiagnosaParentID = diagL10.NursingDiagnosaID;
            var ParentID = diagL10.ID;

            entity.NursingDiagnosaID = string.Empty;
            entity.NursingDiagnosaName = string.Empty;
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


                //var chk = x.FindControl("defaultChkBox") as System.Web.UI.WebControls.CheckBox;
                //if (chk != null)
                //{
                //    if (chk.Checked)
                //    {
                //        var adt = new NursingDiagnosaTransDT();
                //        adt.NursingDiagnosaID = x.GetDataKeyValue("NursingDiagnosaID").ToString();
                //        var tb = x.FindControl("txtNursingDiagnosaName") as Telerik.Web.UI.RadTextBox;
                //        if (tb != null)
                //        {
                //            adt.NursingDiagnosaName = tb.Text;
                //        }
                //        adt.NursingDiagnosaParentID = x["NursingDiagnosaParentID"].Text;
                //        var chkStop = x.FindControl("chkStop") as System.Web.UI.WebControls.CheckBox;
                //        if (chkStop != null)
                //        {
                //            if (chkStop.Checked)
                //            {
                //                adt.SRNursingCarePlanning = "01";
                //            }
                //            else
                //            {
                //                adt.SRNursingCarePlanning = cboP.SelectedValue;
                //            }
                //        }

                //        adt.ParentID = idL10;
                //        adt.TmpIdEvaluation = string.Empty;

                //        selectedNic.AttachEntity(adt);
                //    }
                //}
            }
            return selectedNic;
        }

        public void PopulateOutcome(NursingDiagnosaTransDTCollection dtDiag)
        {
            foreach (GridDataItem x in gridListTarget.MasterTableView.Items)
            {
                var rbl = (x.FindControl("rbDefaultSkala") as RadioButtonList);
                if (rbl != null)
                {
                    var dt = dtDiag.Where(y => y.ID == System.Convert.ToInt64(x.GetDataKeyValue("ID"))).FirstOrDefault();
                    if (dt != null) {
                        dt.Skala = System.Convert.ToInt32(rbl.SelectedValue);
                    }
                }
            }
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

        private void SetEvaluation(NursingDiagnosaTransDTCollection dtDiag)
        {
            //// jika ada tambahan intervensi set disini
            //List<NursingDiagnosaTransDT> newIntvs = new List<NursingDiagnosaTransDT>();
            //foreach (var i in NursingEvaluationIntervention)
            //{
            //    var oldIntv = dtDiag.Where(x => x.SRNursingDiagnosaLevel == "30" &&
            //        x.ParentID == i.ParentID && x.NursingDiagnosaID == i.NursingDiagnosaID);
            //    if (oldIntv.Count() > 0)
            //    {
            //        // jika sudah ada maka update status terakhirnya
            //        foreach (var oi in oldIntv)
            //        {
            //            // yang sudah pernah ada berarti diteruskan / stop
            //            oi.SRNursingCarePlanning = (i.SRNursingCarePlanning == "01") ?
            //                "01" /*stop*/: "02"/*diteruskan*/;
            //            //oi.SRNursingCarePlanning = i.SRNursingCarePlanning;
            //        }
            //    }
            //    else
            //    {
            //        newIntvs.Add(i);
            //    }
            //}
            //// tambahan intervensi
            //if (newIntvs.Count > 0)
            //    SetNIC(dtDiag, newIntvs.ToArray(), true);

            //// evaluasi harus update headernya
            //foreach (var ev in NursingEvaluations)
            //{
            //    if (ev.es.IsAdded)
            //    {
            //        var s = (dtDiag.Select(x => x.Id)).ToArray();

            //        var d = dtDiag.Where(x => x.Id == ev.ParentID).First();
            //        // update status diagnosa
            //        d.SRNursingCarePlanning = ev.SRNursingCarePlanning;
            //        d.P = ev.P;
            //        // update juga tanggalnya
            //        d.ExecuteDateTime = DateTime.Now;/*nanti tambah field sendiri aja*/
            //        // update reexamine, kalau stop berarti 0
            //        d.Reexamine = (ev.SRNursingCarePlanning != "01");

            //        // update status etiologynya
            //        // cari etiology yang belum stop.
            //        var eth = dtDiag.Where(x => x.NursingDiagnosaParentID == d.NursingDiagnosaID &&
            //            x.SRNursingDiagnosaLevel == "11" /*Etiology*/ &&
            //            x.SRNursingCarePlanning != "01" /*STOP*/);
            //        foreach (var et in eth)
            //        {
            //            et.SRNursingCarePlanning = ev.SRNursingCarePlanning;
            //            et.P = ev.P;
            //        }
            //    }
            //}
        }

        private void SaveEvaluation(NursingTransHD hd, NursingDiagnosaTransDTCollection dtDiag)
        {
            //using (esTransactionScope trans = new esTransactionScope())
            //{
            //    //hd.Save();
            //    dtDiag.Save();
            //    NursingEvaluations.Save();
            //    //Commit if success, Rollback if failed
            //    trans.Complete();
            //}

            //// insert detail evaluation
            //// kumpulkan id evaluasi yang baru tersimpan
            //var dEvaColl = NursingDiagnosaTransDT.DetailEvaluation(txtNursingTransNo.Text);
            //var newEva = NursingEvaluations.Where(x => !(dEvaColl.Select(y => y.EvaluationID)).Contains(x.Id));
            //foreach (var eva in newEva)
            //{
            //    // dikali intervensi yang ada
            //    var intrvs = dtDiag.Where(x => x.ParentID == eva.ParentID && x.SRNursingDiagnosaLevel == "30");
            //    foreach (var intrv in intrvs)
            //    {
            //        var EvsI = dEvaColl.AddNew();
            //        EvsI.EvaluationID = eva.Id;
            //        EvsI.InterventionID = intrv.Id;
            //        EvsI.NursingInterventionID = intrv.NursingDiagnosaID;
            //        EvsI.SRNursingCarePlanning = intrv.SRNursingCarePlanning;
            //        EvsI.CreateByUserID = AppSession.UserLogin.UserID;
            //        EvsI.CreateDateTime = DateTime.Now;
            //    }
            //}

            //using (esTransactionScope trans = new esTransactionScope())
            //{
            //    dEvaColl.Save();
            //    trans.Complete();
            //}

            //NursingEvaluationIntervention = null;
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
            if (cboP.SelectedValue == "01")/*stop*/
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
                    chk.Enabled = cboP.SelectedValue != "01";
                }

                var chkSwitch = x.FindControl("chkSwitch");
                //var lbl = x.FindControl("lblSwitch");
                ((System.Web.UI.HtmlControls.HtmlControl)x.FindControl("lblSwitch")).Attributes.Add("for", chkSwitch.ClientID);
            }
        }

        private void FilterGridListRencana()
        {
            switch (cboP.SelectedValue)
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

        #region NOC
        protected void gridListTarget_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var hd = Common.NursingCare.GetTransHD(RegNo);

            var dt = NursingDiagnosaTransDT.NursingTarget(hd[0].TransactionNo, idL10);
            dt.Columns.Add("t1", typeof(string));
            dt.Columns.Add("t2", typeof(string));
            dt.Columns.Add("t3", typeof(string));
            dt.Columns.Add("t4", typeof(string));
            dt.Columns.Add("t5", typeof(string));
            foreach (System.Data.DataRow r in dt.Rows)
            {
                if (r["NursingDiagnosaNameEdited"].ToString().ToLower().Contains(" meningkat"))
                {
                    r["t1"] = "Menurun";
                    r["t2"] = "Cukup Menurun";
                    r["t3"] = "Sedang";
                    r["t4"] = "Cukup Meningkat";
                    r["t5"] = "Meningkat";
                }
                if (r["NursingDiagnosaNameEdited"].ToString().ToLower().Contains(" membaik"))
                {
                    r["t1"] = "Memburuk";
                    r["t2"] = "Cukup Memburuk";
                    r["t3"] = "Sedang";
                    r["t4"] = "Cukup Membaik";
                    r["t5"] = "Membaik";
                }
                if (r["NursingDiagnosaNameEdited"].ToString().ToLower().Contains(" menurun"))
                {
                    r["t1"] = "Meningkat";
                    r["t2"] = "Cukup Meningkat";
                    r["t3"] = "Sedang";
                    r["t4"] = "Cukup Menurun";
                    r["t5"] = "Menurun";
                }
            }

            ((RadGrid)source).DataSource = dt;

            if (true)
            {
                string rowFilter = string.Format("Isnull(TransNursingDiagnosaID,'') <> ''");
                (((RadGrid)source).DataSource as DataTable).DefaultView.RowFilter = rowFilter;
            }
        }

        protected void gridListTarget_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem x = (GridDataItem)e.Item;
                var chk = x.FindControl("defaultChkBox") as System.Web.UI.WebControls.CheckBox;
                if (chk != null)
                {
                    chk.Enabled = !IsDiagnoseClosed;
                }

                var rbScale = x.FindControl("rbDefaultSkala") as RadioButtonList;
                if (rbScale != null)
                {
                    rbScale.Items[0].Attributes.Add("title", (x.DataItem as DataRowView)["t1"].ToString());
                    rbScale.Items[1].Attributes.Add("title", (x.DataItem as DataRowView)["t2"].ToString());
                    rbScale.Items[2].Attributes.Add("title", (x.DataItem as DataRowView)["t3"].ToString());
                    rbScale.Items[3].Attributes.Add("title", (x.DataItem as DataRowView)["t4"].ToString());
                    rbScale.Items[4].Attributes.Add("title", (x.DataItem as DataRowView)["t5"].ToString());
                }
            }
        }
        #endregion
    }
}
