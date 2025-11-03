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
    public partial class NutritionCareStandardNIC : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

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
                dtDiag.Query.SRNutritionCareTerminologyLevel.In(new string[] { "10", "30" }));
            dtDiag.LoadAll();

            //dtDiag = Common.NutritionCare.RemoveClosedDiagnosaAndChildRelated(dtDiag);

            Common.NutritionCare.SaveDiagL30(
                RegNo, PopulateSelectedNIC(dtDiag, idL10), dtDiag, hd, idL10);
            return true;
        }

        private long idL10
        {
            get
            {
                return System.Convert.ToInt64(Request.QueryString["idL10"]);
            }
        }

        private string RegNo
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

        protected void gridListRencana_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //bool DoFilter = true;

            //var diagdt = new NutritionCareDiagnoseTransDT();
            //if (diagdt.LoadByPrimaryKey(idL10))
            //{
            //    DoFilter = true;
            //}
            //else
            //{
            //    DoFilter = false;
            //}
            bool DoFilter = false;
            IsDiagnoseClosed = DoFilter;

            //var hd = Common.NursingCare.GetTransHD(RegNo);

            ((RadGrid)source).DataSource =
                NutritionCareDiagnoseTransDT.NutritionCarePlanning(
                /*hd[0].TransactionNo, */idL10);

            if (DoFilter)
            {
                string rowFilter = string.Format("Isnull(TransTerminologyID,'') <> ''");
                (((RadGrid)source).DataSource as DataTable).DefaultView.RowFilter = rowFilter;
            }
        }

        protected void gridListRencana_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem x = (GridDataItem)e.Item;
                var chk = x.FindControl("defaultChkBox") as System.Web.UI.WebControls.CheckBox;
                var chkIsDetail = x.FindControl("chkIsDetail") as System.Web.UI.WebControls.CheckBox;
                var txtTerminologyName = x.FindControl("txtTerminologyName") as Telerik.Web.UI.RadTextBox;
                if (chk != null)
                {
                    chk.Enabled = !IsDiagnoseClosed && chkIsDetail.Checked;
                    txtTerminologyName.ReadOnly = !chk.Enabled;
                    txtTerminologyName.Font.Bold = !chk.Enabled ? true : false;
                    txtTerminologyName.ForeColor = !chk.Enabled ? System.Drawing.Color.Red : System.Drawing.Color.Black;
                }
            }
        }

        private NutritionCareDiagnoseTransDT[] PopulateSelectedNIC(NutritionCareDiagnoseTransDTCollection dtDiag, long idL10)
        {
            List<NutritionCareDiagnoseTransDT> selectedNIC = new List<NutritionCareDiagnoseTransDT>();

            // kalau diagnosa ini sudah close skip saja tidak usah diambil lagi
            if ((dtDiag.Where(x => x.ID == idL10)).Count() == 0) return selectedNIC.ToArray();
            
            foreach (GridDataItem x in gridListRencana.MasterTableView.Items)
            {
                var chk = x.FindControl("defaultChkBox") as System.Web.UI.WebControls.CheckBox;
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        NutritionCareDiagnoseTransDT itm = new NutritionCareDiagnoseTransDT();
                        itm.TerminologyID = x.GetDataKeyValue("TerminologyID").ToString();
                        var tb = x.FindControl("txtTerminologyName") as Telerik.Web.UI.RadTextBox;
                        if (tb != null)
                        {
                            itm.TerminologyName = tb.Text;
                        }
                        itm.TerminologyParentID = x["TerminologyParentID"].Text;
                        itm.ParentID = idL10;
                        selectedNIC.Add(itm);
                    }
                }
            }
            return selectedNIC.ToArray();
        }
    }
}