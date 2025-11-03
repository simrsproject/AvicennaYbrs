using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data.SqlTypes;
using System.Text;
using System.Web.Services;
using Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NursingCare
{
    public partial class NursingCareStandardPickImplementation : BasePageDialog
    {
        //public string idL10
        //{
        //    get
        //    {
        //        return Request.QueryString["idL10"];
        //    }
        //}

        public string transNo
        {
            get
            {
                var ns = new NursingTransHDCollection();
                ns.Query.Where(ns.Query.RegistrationNo == RegNo);
                if (ns.LoadAll())
                {
                    return ns.First().TransactionNo;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        private string RegNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        private bool IsS
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Request.QueryString["isS"])) return true;
                return Request.QueryString["isS"].ToBoolean();
            }
        }
        private bool IsO
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Request.QueryString["isO"])) return true;
                return Request.QueryString["isO"].ToBoolean();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (!IsPostBack)
            {

            }
            else
            {

            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;
            if (!IsPostBack)
            {
                gridListImplementasi.Columns[0].Display = IsS;
                gridListImplementasi.Columns[1].Display = IsO;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            string sRetDS = string.Empty;
            string sRetDO = string.Empty;
            foreach (GridDataItem row in gridListImplementasi.MasterTableView.Items)
            {
                CheckBox chkDS = row.FindControl("chkDS") as CheckBox;
                if (chkDS != null)
                {
                    if (chkDS.Checked)
                    {
                        sRetDS = sRetDS + (sRetDS.Length > 0 ? ", " : "") + (row["Respond"].Controls[0] as DataBoundLiteralControl).Text;
                    }
                }

                CheckBox chkDO = row.FindControl("chkDO") as CheckBox;
                if (chkDO != null)
                {
                    if (chkDO.Checked)
                    {
                        sRetDO = sRetDO + (sRetDO.Length > 0 ? ", " : "") + (row["Respond"].Controls[0] as DataBoundLiteralControl).Text;
                    }
                }
            }

            do {
                sRetDS = sRetDS.Replace("  ", " ");
            } while(sRetDS.IndexOf("  ") >= 0);
            do
            {
                sRetDO = sRetDO.Replace("  ", " ");
            } while (sRetDO.IndexOf("  ") >= 0);

            sRetDS = sRetDS.Replace("\r\n", "").Replace("\t", "").Trim();
            sRetDO = sRetDO.Replace("\r\n", "").Replace("\t", "").Trim();
            
            sRetDS = Helper.StripHTML(sRetDS);
            sRetDO = Helper.StripHTML(sRetDO);
            sRetDS = System.Uri.EscapeUriString(sRetDS);
            sRetDO = System.Uri.EscapeUriString(sRetDO);
            sRetDS = sRetDS.Replace("'", "\\'");//HttpUtility.JavaScriptStringEncode(sRetDS);
            sRetDO = sRetDO.Replace("'", "\\'");// HttpUtility.JavaScriptStringEncode(sRetDO);

            if (string.IsNullOrEmpty(sRetDS) && string.IsNullOrEmpty(sRetDO))
            {
                return "oWnd.argument.result = 'Gak OK'";
            }

            return string.Format("oWnd.argument.result = 'OK'; oWnd.argument.dataDS = '{0}'; oWnd.argument.dataDO = '{1}'; oWnd.argument.isS = '{2}'; oWnd.argument.isO = '{3}';",
                sRetDS, sRetDO, IsS,IsO);
        }
        public override bool OnButtonOkClicked()
        {
            return true;
        }

        #region override method

        #endregion

        #region Implementasi

        //public string NursingTransNo
        //{
        //    get
        //    {
        //        if (Session["NursingTransNo" + RegNo] != null)
        //        {
        //            return Session["NursingTransNo" + RegNo].ToString();
        //        }

        //        var ret = string.Empty;

        //        var nsdt = new NursingDiagnosaTransDT();
        //        if (nsdt.LoadByPrimaryKey(System.Convert.ToInt32(idL10))) {
        //            var nshd = new NursingTransHD();
        //            if (nshd.LoadByPrimaryKey(nsdt.TransactionNo)) {
        //                return nshd.TransactionNo;
        //            }
        //        }

        //        return string.Empty;
        //    }
        //    set
        //    {
        //        Session["NursingTransNo" + RegNo] = value;
        //    }
        //}

        public string GetFullImplementationNameFormatted(string NursingDiagnosaName,
            string S, string O, string A, string P)
        {

            if (Equals(NursingDiagnosaName, "S B A R"))
            {
                return (
                    "S: " + S + System.Environment.NewLine +
                    "B: " + O + System.Environment.NewLine +
                    "A: " + A + System.Environment.NewLine +
                    "R: " + P);
            }
            else if (Equals(NursingDiagnosaName, "S O A P"))
            {
                return (
                    "S: " + S + System.Environment.NewLine +
                    "O: " + O + System.Environment.NewLine +
                    "A: " + A + System.Environment.NewLine +
                    "P: " + P);
            }
            else
            {
                return NursingDiagnosaName;
            }
        }

        public static string parsePhrlRespond(IEnumerable<PatientHealthRecordLine> phrls) {
            var respond = string.Empty;
            foreach (var phrl in phrls)
            {
                var resp = string.Empty;
                var q = new Question();
                if (q.LoadByPrimaryKey(phrl.QuestionID))
                {
                    if (phrl.QuestionAnswerNum.HasValue)
                    {
                        resp = phrl.QuestionAnswerPrefix + " " + phrl.QuestionAnswerNum.Value.ToString("##0.##") + " " + phrl.QuestionAnswerSuffix;
                    }
                    else if (!string.IsNullOrEmpty(phrl.QuestionAnswerSelectionLineID))
                    {
                        var qasl = new QuestionAnswerSelectionLine();
                        qasl.Query.Where(qasl.Query.QuestionAnswerSelectionID == q.QuestionAnswerSelectionID,
                            qasl.Query.QuestionAnswerSelectionLineID == phrl.QuestionAnswerSelectionLineID);
                        if (qasl.Load(qasl.Query))
                        {
                            resp = qasl.QuestionAnswerSelectionLineText;
                        }
                    }
                }
                resp = q.QuestionText + " " + resp;

                respond = respond + (respond.Length > 0 ? ", " : "") + resp;
            }

            return respond;
        }
        protected void gridListImplementasi_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var prColl = new NursingDiagnosaTransDTCollection();
            var regnos = Common.NursingCare.GetRelatedRegistrationsByNsTransNo(transNo);
            var ni = prColl.ImplementationByPage(regnos, true,
                    ((gridListImplementasi.CurrentPageIndex * gridListImplementasi.PageSize) + 1),
                    ((gridListImplementasi.CurrentPageIndex + 1) * gridListImplementasi.PageSize));

            var nRespond = ni.AsEnumerable().Where(x => !string.IsNullOrEmpty(x.Field<string>("ReferenceToPhrNo")));
            foreach (var n in nRespond)
            {
                var phrlColl = new PatientHealthRecordLineCollection();
                phrlColl.Query.Where(phrlColl.Query.TransactionNo == n["ReferenceToPhrNo"]);
                if (phrlColl.LoadAll())
                {
                    n["Respond2"] = parsePhrlRespond(phrlColl);
                }
            }

            gridListImplementasi.VirtualItemCount = prColl.ImplementationCount(regnos, true);

            gridListImplementasi.DataSource = ni;
        }

        protected void gridListImplementasi_ItemDataBound(object source, GridItemEventArgs e)
        {
            
        }
        #endregion
    }
}
