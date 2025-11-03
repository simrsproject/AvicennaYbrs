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
    public partial class NutritionCareStandardEditDiagnose : BasePageDialog
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
                dtDiag.Query.SRNutritionCareTerminologyLevel.In(new string[] { "10" }));
            dtDiag.LoadAll();

            //dtDiag = Common.NutritionCare.RemoveClosedDiagnosaAndChildRelated(dtDiag);

            Common.NutritionCare.SaveDiagL10(RegNo, PopulateSelectedDiagnosaDiagnosa(dtDiag), dtDiag, hd);
            return true;
        }

        private string RegNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }

        private NutritionCareDiagnoseTransDT[] PopulateSelectedDiagnosaDiagnosa(NutritionCareDiagnoseTransDTCollection dtDiag)
        {
            List<NutritionCareDiagnoseTransDT> selectedDiag = new List<NutritionCareDiagnoseTransDT>();
            foreach (GridDataItem x in gridListDiagnosa.MasterTableView.Items)
            {
                var IdDiag = System.Convert.ToInt64(x.GetDataKeyValue("ID"));

                // kalau diagnosa ini sudah close skip saja tidak usah diambil lagi
                if ((dtDiag.Where(y => y.ID == IdDiag)).Count() == 0) continue;
                
                var txt = x.FindControl("txtDefaultPriority") as RadTextBox;
                if (txt != null)
                {
                    NutritionCareDiagnoseTransDT itm = new NutritionCareDiagnoseTransDT();
                    itm.ID = IdDiag;
                    itm.TransactionNo = Common.NutritionCare.GetTransHD(RegNo)[0].TransactionNo;
                    itm.TerminologyID = x.GetDataKeyValue("TerminologyID").ToString();
                    itm.TerminologyName = x["TerminologyName"].Text;
                    itm.TerminologyParentID = x["TerminologyParentID"].Text;

                    var txtDS = x.FindControl("txtS") as RadTextBox;
                    var txtDO = x.FindControl("txtO") as RadTextBox;
                    if (txtDS != null && txtDO != null)
                    {
                        itm.S = txtDS.Text;
                        itm.O = txtDO.Text;
                    }

                    selectedDiag.Add(itm);
                }

            }
            return selectedDiag.ToArray();
        }

        protected void gridListDiagnosa_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var hd = Common.NutritionCare.GetTransHD(RegNo);
            ((RadGrid)source).DataSource =
                NutritionCareDiagnoseTransDT.NutritionCareDiagnosaFullDefinition(hd[0].TransactionNo);
        }
    }
}