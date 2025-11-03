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
    public partial class NursingCareStandardNOC : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (AppSession.Parameter.IsNsOutcomeShowScale) {
                    var diagdt = new NursingDiagnosaTransDT();
                    if (diagdt.LoadByPrimaryKey(idL10)) {
                        var diag = new NursingDiagnosa();
                        if (diag.LoadByPrimaryKey(diagdt.NursingDiagnosaID)) {
                            if (diag.SRNsDiagnosaType == AppConstant.NsDiagnosaType.Nursing) {
                                gridListTarget.Columns[4].Visible = true;
                            }
                        }
                    }
                }
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
                dtDiag.Query.SRNursingDiagnosaLevel.In(new string[] { "10","20","21" }));
            dtDiag.LoadAll();

            dtDiag = Common.NursingCare.RemoveClosedDiagnosaAndChildRelated(dtDiag);

            Common.NursingCare.SaveDiagL20_21(
                RegNo, PopulateSelectedDiagnosaNOCTarget(dtDiag, hd.TransactionNo, idL10), dtDiag, hd, idL10);
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

        protected void gridListTarget_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
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

            var hd = Common.NursingCare.GetTransHD(RegNo);

            var dt = NursingDiagnosaTransDT.NursingTarget(hd[0].TransactionNo, idL10);
            dt.Columns.Add("t1", typeof(string));
            dt.Columns.Add("t2", typeof(string));
            dt.Columns.Add("t3", typeof(string));
            dt.Columns.Add("t4", typeof(string));
            dt.Columns.Add("t5", typeof(string));
            foreach (System.Data.DataRow r in dt.Rows) {
                if (r["NursingDiagnosaNameEdited"].ToString().ToLower().Contains(" meningkat")) {
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
                

            if (DoFilter)
            {
                string rowFilter = string.Format("Isnull(TransNursingDiagnosaID,'') <> ''");
                (((RadGrid)source).DataSource as DataTable).DefaultView.RowFilter = rowFilter;
            }

            var nd = new NursingDiagnosa();
            if (nd.LoadByPrimaryKey(diagdt.NursingDiagnosaID))
            {
                if (nd.SRNsDiagnosaType == "01")
                {
                    gridListTarget.Columns[1].HeaderText = AppSession.Parameter.NsOutcome;
                }
                else if (nd.SRNsDiagnosaType == "02") {
                    gridListTarget.Columns[1].HeaderText = AppSession.Parameter.NsOutcome02;
                }
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
                if (rbScale != null) {
                    rbScale.Items[0].Attributes.Add("title", (x.DataItem as DataRowView)["t1"].ToString());
                    rbScale.Items[1].Attributes.Add("title", (x.DataItem as DataRowView)["t2"].ToString());
                    rbScale.Items[2].Attributes.Add("title", (x.DataItem as DataRowView)["t3"].ToString());
                    rbScale.Items[3].Attributes.Add("title", (x.DataItem as DataRowView)["t4"].ToString());
                    rbScale.Items[4].Attributes.Add("title", (x.DataItem as DataRowView)["t5"].ToString());
                }
            }
        }

        private NursingDiagnosaTransDT[] PopulateSelectedDiagnosaNOCTarget(NursingDiagnosaTransDTCollection dtDiag, 
            string TransactionNo, long idL10)
        {
            List<NursingDiagnosaTransDT> selectedDiag = new List<NursingDiagnosaTransDT>();

            // kalau diagnosa ini sudah close skip saja tidak usah diambil lagi
            if ((dtDiag.Where(x => x.ID == idL10)).Count() == 0) return selectedDiag.ToArray();
            if ((dtDiag.Where(x => x.ID == idL10)).First().SRNursingCarePlanning == "01") return selectedDiag.ToArray();

            foreach (GridDataItem x in gridListTarget.MasterTableView.Items)
            {
                var chk = x.FindControl("defaultChkBox") as System.Web.UI.WebControls.CheckBox;
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        var rb = x.FindControl("rbDefaultSkala") as System.Web.UI.WebControls.RadioButtonList;
                        if (rb != null)
                        {
                            var txtTarget = x.FindControl("txtDefaultTarget") as RadNumericTextBox;
                            if (txtTarget != null)
                            {
                                NursingDiagnosaTransDT itm = new NursingDiagnosaTransDT();
                                itm.Skala = int.Parse(rb.SelectedValue);
                                itm.Target = txtTarget.Value.Value.ToInt();
                                itm.TransactionNo = TransactionNo;
                                itm.NursingDiagnosaID = x.GetDataKeyValue("NursingDiagnosaID").ToString();
                                //itm.NursingDiagnosaName = x["NursingDiagnosaName"].Text;
                                var tb = x.FindControl("txtNursingDiagnosaName") as Telerik.Web.UI.RadTextBox;
                                if (tb != null)
                                {
                                    itm.NursingDiagnosaName = tb.Text;
                                }
                                itm.NursingDiagnosaParentID = x["NursingDiagnosaParentID"].Text;
                                //itm.ParentID = IdDiag;
                                selectedDiag.Add(itm);
                            }
                        }
                    }
                }
            }
            return selectedDiag.ToArray();
        }
    }
}
