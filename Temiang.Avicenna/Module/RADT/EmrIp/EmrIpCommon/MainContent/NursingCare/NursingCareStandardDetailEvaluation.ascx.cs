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

namespace Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NursingCare
{
    public partial class NursingCareStandardDetailEvaluation : BaseUserControl
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

        private RadTextBox txtNursingTransNo
        {
            get
            {
                return (RadTextBox)Helper.FindControlRecursive(Page, "txtNursingTransNo");
            }
        }

        private NursingDiagnosaTransDTCollection NursingEvaluations
        {
            get
            {
                //if (IsPostBack)
                //{
                object obj = Session["collNursingEvaluation" + RegistrationNo];
                if (obj != null)
                {
                    return ((NursingDiagnosaTransDTCollection)(obj));
                }
                //}

                var coll = NursingDiagnosaTransDT.Evaluation(txtNursingTransNo.Text);

                Session["collNursingEvaluation" + RegistrationNo] = coll;
                return coll;
            }
            set
            {
                string sessionName = "collNursingEvaluation" + RegistrationNo;
                Session[sessionName] = value;
            }
        }

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        //public string SelectedParentID
        //{
        //    get
        //    {
        //        var prn = ((GridDataItem)DataItem).OwnerTableView.ParentItem.GetDataKeyValue("NursingDiagnosaID").ToString();

        //        return prn;

        //        //var grid = (RadGrid)Helper.FindControlRecursive(Page, "gridListImplementasiDiagnosa");
        //        //string parentID = string.Empty;
        //        //if (grid.SelectedItems.Count > 0)
        //        //{
        //        //    GridDataItem item = (GridDataItem)grid.MasterTableView.Items[grid.SelectedItems[0].ItemIndex];
        //        //    parentID = item.GetDataKeyValue(NursingDiagnosaMetadata.ColumnNames.NursingDiagnosaID).ToString();
        //        //}
        //        //return parentID;
        //    }
        //}

        public string SessionName
        {
            get
            {
                return "collNursingEvaluation" + RegistrationNo; //_" + SelectedParentID;
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            LoadCombo();

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                txtDateTimeImplementation.SelectedDate = DateTime.Now;
                txtS.Text = string.Empty;
                txtO.Text = string.Empty;
                txtA.Text = string.Empty;
                hfID.Value = string.Empty;
                //hfTmpNursingDiagnosaID.Value = NursingDiagnosaTransDT.GetTmpNursingDiagnosaID(NursingEvaluations);
                return;
            }
            ViewState["IsNewRecord"] = false;

            NursingDiagnosaQuery query = new NursingDiagnosaQuery("a");

            query.Select
                (
                    query.NursingDiagnosaID,
                    query.NursingDiagnosaName
                );
            query.Where(query.NursingDiagnosaID == (String)DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.NursingDiagnosaID));

            ComboBox.SelectedValue(cboP, (String)DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.SRNursingCarePlanning));
            txtDateTimeImplementation.SelectedDate = (DateTime)DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.ExecuteDateTime);
            txtS.Text = (String)DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.S);
            txtO.Text = (String)DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.O);
            txtA.Text = (String)DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.A);
            txtPpaInstruction.Text = (String)DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.PpaInstruction);
            hfID.Value = (DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.ID) ?? string.Empty).ToString();
            hfTmpNursingDiagnosaID.Value = (DataBinder.Eval(DataItem, NursingDiagnosaTransDTMetadata.ColumnNames.TmpNursingDiagnosaID)).ToString();

            // disable edit status ketika sudah pernah disimpan
            DisableEditByStatus();
        }
        private void DisableEditByStatus() {
            var st = string.IsNullOrEmpty(hfID.Value);

            cboP.Enabled = st;
        }
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
		
		#region Properties for return entry value

        public String S
        {
            get { return txtS.Text; }
        }

        public String O
        {
            get { return txtO.Text; }
        }

        public DateTime? ImplementationDateTime {
            get { return txtDateTimeImplementation.SelectedDate; }
        }

        public String A
        {
            get { return txtA.Text; }
        }        


        public String P
        {
            get { return cboP.SelectedValue; }
        }

        public String PpaInstruction
        {
            get { return txtPpaInstruction.Text; }
        }

        public String RecordID
        {
            get { return hfID.Value; }
        }

        public String TmpNursingDiagnosaID
        {
            get { return hfTmpNursingDiagnosaID.Value; }
        }

        public NursingDiagnosaTransDT[] NIC {
            get {
                List<NursingDiagnosaTransDT> selectedNic = new List<NursingDiagnosaTransDT>();
                foreach (GridDataItem x in gridListRencana.MasterTableView.Items)
                {
                    var chk = x.FindControl("defaultChkBox") as System.Web.UI.WebControls.CheckBox;
                    if (chk != null)
                    {
                        if (chk.Checked)
                        {
                            var adt = new NursingDiagnosaTransDT();
                            adt.NursingDiagnosaID = x.GetDataKeyValue("NursingDiagnosaID").ToString();
                            var tb = x.FindControl("txtNursingDiagnosaName") as Telerik.Web.UI.RadTextBox;
                            if (tb != null)
                            {
                                adt.NursingDiagnosaName = tb.Text;
                            }
                            adt.NursingDiagnosaParentID = x["NursingDiagnosaParentID"].Text;
                            var chkStop = x.FindControl("chkStop") as System.Web.UI.WebControls.CheckBox;
                            if (chkStop != null)
                            {
                                if (chkStop.Checked)
                                {
                                    adt.SRNursingCarePlanning = "01";
                                }
                                else {
                                    adt.SRNursingCarePlanning = cboP.SelectedValue;
                                }
                            }

                            adt.ParentID = IdDiagL10;
                            adt.TmpIdEvaluation = TmpNursingDiagnosaID;
                            
                            selectedNic.Add(adt);
                        }
                    }
                }
                return selectedNic.ToArray();
            }
        }
        #endregion
		#region Method & Event
        protected void cboP_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            // do something
            gridListRencana.Rebind();
        }

        private void LoadCombo()
        {
            Common.ComboBox.StandardReferenceItem(cboP, AppEnum.StandardReference.NursingCarePlanning.ToString());
        }
		#endregion
        #region NIC
        protected void gridListRencana_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            // ambil ID Diagnosa
            gridListRencana.DataSource =
                NursingDiagnosaTransDT.NursingPlanning(IdDiagL10);

            FilterGridListRencana();

            //if (DataModeCurrent == AppEnum.DataMode.Read ||
            //    (DataModeCurrent != AppEnum.DataMode.Read && !pgvRencana.Selected))
            //{
            //    string rowFilter = string.Format("Isnull(TransNursingDiagnosaID,'') <> ''");
            //    (e.DetailTableView.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
            //}
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
            }
        }

        private void FilterGridListRencana() {
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
    }
}
