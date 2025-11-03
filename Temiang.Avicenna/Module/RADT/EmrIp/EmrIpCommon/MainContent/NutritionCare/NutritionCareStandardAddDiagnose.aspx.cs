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
    public partial class NutritionCareStandardAddDiagnose : BasePageDialog
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
            else
            {
                return "oWnd.argument.result = 'Gak OK'";
            }
        }
        public override bool OnButtonOkClicked()
        {
            Common.NutritionCare.SaveDiagL11(RegNo, PopulateSelectedDiagnosaProblem());
            return true;
        }

        private string RegNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }

        #region Nutrition Care Problem
        private NutritionCareDiagnoseTransDT[] PopulateSelectedDiagnosaProblem()
        {
            List<NutritionCareDiagnoseTransDT> selectedDiag = new List<NutritionCareDiagnoseTransDT>();
            foreach (GridDataItem x in gridListProblem.MasterTableView.Items)
            {
                var chk = x.FindControl("defaultChkBox") as System.Web.UI.WebControls.CheckBox;
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        var adt = new NutritionCareDiagnoseTransDT();
                        adt.TerminologyID = x.GetDataKeyValue("TerminologyID").ToString();
                        adt.TerminologyName = x["TerminologyName"].Text;
                        adt.TerminologyParentID = x["TerminologyParentID"].Text;
                        selectedDiag.Add(adt);
                    }
                }
            }
            return selectedDiag.ToArray();
        }

        protected void gridListProblem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            // pastikan diagnosa yang sedang jalan tidak bisa dipilih lagi
            var hdColl = Common.NutritionCare.GetTransHD(RegNo);
            var dttbl = NutritionCareDiagnoseTransDT.NutritionCareProblemAvailable(hdColl[0].TransactionNo);
            gridListProblem.DataSource = dttbl;
        }
        #endregion
    }
}