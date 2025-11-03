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
    public partial class NursingCareStandardAddDiagnose : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            if (PopulateSelectedDiagnosaProblem().Count() > 0)
            {
                return "oWnd.argument.result = 'OK'";
            }
            else {
                return "oWnd.argument.result = 'Gak OK'";
            }
        }
        public override bool OnButtonOkClicked()
        {
            // ambil SRNsType dari form terakhir yang diimport
            var SRNsType = string.Empty;
            try
            {
                SRNsType = Common.NursingCare.NursingAssessmentGetLastImportedType(RegNo);
            }
            catch (Exception ex) {
                ShowMessage(ex.Message);
                return false;
            }

            Common.NursingCare.SaveDiagL11(RegNo, PopulateSelectedDiagnosaProblem(), null, SRNsType);
            return true;
        }

        private string RegNo
        {
            get {
                return Request.QueryString["regno"];
            }
        }

        #region Nursing Problem
        private NursingDiagnosaTransDT[] PopulateSelectedDiagnosaProblem()
        {
            List<NursingDiagnosaTransDT> selectedDiag = new List<NursingDiagnosaTransDT>();
            foreach (GridDataItem x in gridListProblem.MasterTableView.Items)
            {
                var chk = x.FindControl("defaultChkBox") as System.Web.UI.WebControls.CheckBox;
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        var adt = new NursingDiagnosaTransDT();
                        adt.NursingDiagnosaID = x.GetDataKeyValue("NursingDiagnosaID").ToString();
                        var txt = x.FindControl("txtEtiologyName") as RadTextBox;
                        adt.NursingDiagnosaName = txt.Text; //x["NursingDiagnosaName"].Text;
                        adt.NursingDiagnosaParentID = x["NursingDiagnosaParentID"].Text;
                        selectedDiag.Add(adt);
                    }
                }
            }
            return selectedDiag.ToArray();
        }

        protected void gridListProblem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var SRNsType = string.Empty;
            try
            {
                SRNsType = Common.NursingCare.NursingAssessmentGetLastImportedType(RegNo);
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
                gridListProblem.DataSource = null;
                return;
            }

            // pastikan diagnosa yang sedang jalan tidak bisa dipilih lagi
            var hdColl = Common.NursingCare.GetTransHD(RegNo);
            var dttbl = NursingDiagnosaTransDT.NursingProblemAvailable(hdColl[0].TransactionNo, SRNsType);
            gridListProblem.DataSource = dttbl;
        }
        #endregion
    }
}
