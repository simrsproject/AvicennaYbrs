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

namespace Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NutritionCare
{
    public partial class NutritionCareStandardPickImplementation : BasePageDialog
    {
        public string idL10
        {
            get
            {
                return Request.QueryString["idL10"];
            }
        }
        private string RegNo
        {
            get
            {
                return Request.QueryString["regno"];
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
                        sRetDS = sRetDS + (sRetDS.Length > 0 ? "|" : "") + (row["Respond"].Controls[0] as DataBoundLiteralControl).Text;
                    }
                }

                CheckBox chkDO = row.FindControl("chkDO") as CheckBox;
                if (chkDO != null)
                {
                    if (chkDO.Checked)
                    {
                        sRetDO = sRetDO + (sRetDO.Length > 0 ? "|" : "") + (row["Respond"].Controls[0] as DataBoundLiteralControl).Text;
                    }
                }
            }

            do
            {
                sRetDS = sRetDS.Replace("  ", " ");
            } while (sRetDS.IndexOf("  ") >= 0);
            do
            {
                sRetDO = sRetDO.Replace("  ", " ");
            } while (sRetDO.IndexOf("  ") >= 0);

            sRetDS = sRetDS.Replace("\r\n", "").Replace("\t", "").Trim();
            sRetDO = sRetDO.Replace("\r\n", "").Replace("\t", "").Trim();

            if (string.IsNullOrEmpty(sRetDS) && string.IsNullOrEmpty(sRetDO))
            {
                return "oWnd.argument.result = 'Gak OK'";
            }
            else
            {
                return string.Format("oWnd.argument.result = 'OK'; oWnd.argument.dataDS = '{0}'; oWnd.argument.dataDO = '{1}';",
                    System.Uri.EscapeUriString(sRetDS), System.Uri.EscapeUriString(sRetDO));
                //return "oWnd.argument.result = 'OK'; oWnd.argument.data = '"+ sRet + "';";
                //return "oWnd.argument.result = 'OK'; oWnd.argument.data = 'xxxxxxx'";
            }
        }
        public override bool OnButtonOkClicked()
        {
            //string sRet = string.Empty;
            //foreach (GridDataItem row in gridListImplementasi.MasterTableView.Items) {
            //    CheckBox chk = row.FindControl("chk") as CheckBox;
            //    if (chk != null) {
            //        if (chk.Checked) {
            //            sRet = sRet + (sRet.Length > 0 ? "|" : "") + (row["Respond"].Controls[0] as DataBoundLiteralControl).Text;
            //        }
            //    }
            //}

            // try to fire 
            //OnGetAdditionalJavaScriptCloseAndApply();

            return true;
        }

        #region override method

        #endregion

        #region Implementasi
        public DataTable ImplementationCountPerIntervention
        {
            get
            {
                if (ViewState["ImplementationCountPerIntervention" + RegNo] != null)
                {
                    return (DataTable)ViewState["ImplementationCountPerIntervention" + RegNo];
                }

                return null;
            }
            set
            {
                ViewState["ImplementationCountPerIntervention" + RegNo] = value;
            }
        }

        public string SelectedImplementationNIC
        {
            get
            {
                if (Session["SelectedImplementationNIC" + RegNo] != null)
                {
                    return Session["SelectedImplementationNIC" + RegNo].ToString();
                }

                return string.Empty;
            }
            set
            {
                Session["SelectedImplementationNIC" + RegNo] = value;
            }
        }

        public string NutritionCareTransNo
        {
            get
            {
                if (Session["NutritionCareTransNo" + RegNo] != null)
                {
                    return Session["NutritionCareTransNo" + RegNo].ToString();
                }

                var ret = string.Empty;

                var nsdt = new NutritionCareDiagnoseTransDT();
                if (nsdt.LoadByPrimaryKey(System.Convert.ToInt32(idL10)))
                {
                    var nshd = new NutritionCareTransHD();
                    if (nshd.LoadByPrimaryKey(nsdt.TransactionNo))
                    {
                        return nshd.TransactionNo;
                    }
                }

                return string.Empty;
            }
            set
            {
                Session["NutritionCareTransNo" + RegNo] = value;
            }
        }

        private NutritionCareDiagnoseTransDTCollection NewOrEditedNursingImplementations
        {
            get
            {
                //if (IsPostBack)
                //{
                object obj = Session["collNutritionCareImplementation" + RegNo];
                if (obj != null)
                {
                    return ((NutritionCareDiagnoseTransDTCollection)(obj));
                }
                //}

                var coll = new NutritionCareDiagnoseTransDTCollection();
                Session["collNutritionCareImplementation" + RegNo] = coll;
                return coll;
            }
            set
            {
                string sessionName = "collNutritionCareImplementation" + RegNo;
                Session[sessionName] = value;
            }
        }

        private PatientHealthRecordLineCollection NewOrEditedPhrLine
        {
            get
            {
                object obj = Session["collNewOrEditedPhrLine" + RegNo];
                if (obj != null)
                {
                    return ((PatientHealthRecordLineCollection)(obj));
                }

                var coll = new PatientHealthRecordLineCollection();
                Session["collNewOrEditedPhrLine" + RegNo] = coll;
                return coll;
            }
            set
            {
                string sessionName = "collNewOrEditedPhrLine" + RegNo;
                Session[sessionName] = value;
            }
        }

        private List<long> IdImplementationDeleted
        {
            get
            {
                //if (IsPostBack)
                //{
                object obj = Session["listIdImplementationDeleted" + RegNo];
                if (obj != null)
                {
                    return ((List<long>)(obj));
                }
                //}

                var coll = new List<long>();
                Session["listIdImplementationDeleted" + RegNo] = coll;
                return coll;
            }
            set
            {
                string sessionName = "listIdImplementationDeleted" + RegNo;
                Session[sessionName] = value;
            }
        }

        private DataTable NutritionCareImplementasi(bool OpenOnly)
        {
            var query = new NutritionCareTerminologyQuery("a");
            var prDiag = new NutritionCareTerminologyQuery("b");
            var dt = new NutritionCareDiagnoseTransDTQuery("c");
            var dtDiag = new NutritionCareDiagnoseTransDTQuery("d");

            query.es.Distinct = true;

            query.InnerJoin(prDiag).On(query.TerminologyParentID == prDiag.TerminologyID
                & query.SRNutritionCareTerminologyLevel == "30");
            query.InnerJoin(dtDiag).On(prDiag.TerminologyID == dtDiag.TerminologyID
                & dtDiag.TransactionNo == NutritionCareTransNo);
            query.InnerJoin(dt).On(query.TerminologyID == dt.TerminologyID
                & dt.TransactionNo == NutritionCareTransNo);

            if (OpenOnly)
            {
                query.Where("<ISNULL(c.SRNutritionCarePlanning,'') <> '01'>", "<ISNULL(d.SRNutritionCarePlanning,'') <> '01'>");
            }

            query.Select(
                query,
                prDiag.TerminologyID.As("DiagID"),
                prDiag.TerminologyName.As("DiagName"),
                dt.ID,
                dt.TerminologyID.As("TransTerminologyID"),
                "<ISNULL(c.TerminologyName, a.TerminologyName) TerminologyNameEdited>"
                )
                .OrderBy(dtDiag.CreateDateTime.Ascending);

            var dttbl = query.LoadDataTable();
            return dttbl;
        }

        public string GetFullImplementationNameFormatted(string TerminologyName,
            string S, string O, string A, string P)
        {

            if (Equals(TerminologyName, "S B A R"))
            {
                return (
                    "S: " + S + System.Environment.NewLine +
                    "B: " + O + System.Environment.NewLine +
                    "A: " + A + System.Environment.NewLine +
                    "R: " + P);
            }
            else if (Equals(TerminologyName, "S O A P"))
            {
                return (
                    "S: " + S + System.Environment.NewLine +
                    "O: " + O + System.Environment.NewLine +
                    "A: " + A + System.Environment.NewLine +
                    "P: " + P);
            }
            else
            {
                return TerminologyName;
            }
        }

        public static string parsePhrlRespond(IEnumerable<PatientHealthRecordLine> phrls)
        {
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
            var prColl = new NutritionCareDiagnoseTransDTCollection();
            var ni = prColl.ImplementationByPage(NutritionCareTransNo, true,
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

            gridListImplementasi.VirtualItemCount = prColl.ImplementationCount(NutritionCareTransNo, true);

            gridListImplementasi.DataSource = ni;
        }

        protected void gridListImplementasi_ItemDataBound(object source, GridItemEventArgs e)
        {

        }
        #endregion
    }
}