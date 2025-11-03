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
    public partial class NursingCareStandardNIC : BasePageDialog
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
            var hd = Common.NursingCare.SetTransHD(RegNo, AppSession.UserLogin.UserID);

            var dtDiag = new NursingDiagnosaTransDTCollection();
            dtDiag.Query.Where(dtDiag.Query.TransactionNo == hd.TransactionNo,
                dtDiag.Query.SRNursingDiagnosaLevel.In(new string[] { "10","30" }));
            dtDiag.LoadAll();

            dtDiag = Common.NursingCare.RemoveClosedDiagnosaAndChildRelated(dtDiag);

            Common.NursingCare.SaveDiagL30(
                RegNo, PopulateSelectedNIC(dtDiag, idL10), dtDiag, hd, idL10);
            return true;
        }

        private long idL10
        {
            get {
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

        public string FullDiagnosaName {
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
            bool DoFilter = true;

            var diagdt = new NursingDiagnosaTransDT();
            if (diagdt.LoadByPrimaryKey(idL10) && (diagdt.SRNursingCarePlanning == "01"/*closed*/))
            {
                DoFilter = true;
            }
            else
            {
                DoFilter = false;
            }

            IsDiagnoseClosed = DoFilter;

            //var hd = Common.NursingCare.GetTransHD(RegNo);

            ((RadGrid)source).DataSource =
                NursingDiagnosaTransDT.NursingPlanning(
                    /*hd[0].TransactionNo, */idL10);

            if (DoFilter)
            {
                string rowFilter = string.Format("Isnull(TransNursingDiagnosaID,'') <> ''");
                (((RadGrid)source).DataSource as DataTable).DefaultView.RowFilter = rowFilter;
            }

            var nd = new NursingDiagnosa();
            if (nd.LoadByPrimaryKey(diagdt.NursingDiagnosaID)) {
                if (nd.SRNsDiagnosaType == "01")
                {
                    gridListRencana.Columns[1].HeaderText = AppSession.Parameter.NsIntervention;
                }
            }
        }

        protected void gridListRencana_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem x = (GridDataItem)e.Item;
                var chk = x.FindControl("defaultChkBox") as System.Web.UI.WebControls.CheckBox;
                if (chk != null)
                {
                    chk.Enabled = !IsDiagnoseClosed;
                }
            }
        }

        private NursingDiagnosaTransDT[] PopulateSelectedNIC(NursingDiagnosaTransDTCollection dtDiag, long idL10)
        {
            List<NursingDiagnosaTransDT> selectedNIC = new List<NursingDiagnosaTransDT>();

            // kalau diagnosa ini sudah close skip saja tidak usah diambil lagi
            if ((dtDiag.Where(x => x.ID == idL10)).Count() == 0) return selectedNIC.ToArray();
            if ((dtDiag.Where(x => x.ID == idL10)).First().SRNursingCarePlanning == "01") return selectedNIC.ToArray();

            foreach (GridDataItem x in gridListRencana.MasterTableView.Items)
            {
                var chk = x.FindControl("defaultChkBox") as System.Web.UI.WebControls.CheckBox;
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        NursingDiagnosaTransDT itm = new NursingDiagnosaTransDT();
                        itm.NursingDiagnosaID = x.GetDataKeyValue("NursingDiagnosaID").ToString();
                        //itm.NursingDiagnosaName = x["NursingDiagnosaName"].Text;
                        var tb = x.FindControl("txtNursingDiagnosaName") as Telerik.Web.UI.RadTextBox;
                        if (tb != null)
                        {
                            itm.NursingDiagnosaName = tb.Text;
                        }
                        itm.NursingDiagnosaParentID = x["NursingDiagnosaParentID"].Text;
                        itm.ParentID = idL10;
                        selectedNIC.Add(itm);
                    }
                }
            }
            return selectedNIC.ToArray();
        }
    }
}
