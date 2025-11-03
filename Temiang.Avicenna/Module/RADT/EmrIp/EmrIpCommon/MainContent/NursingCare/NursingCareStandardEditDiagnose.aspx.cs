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
    public partial class NursingCareStandardEditDiagnose : BasePageDialog
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
                dtDiag.Query.SRNursingDiagnosaLevel.In(new string[] { "10" }));
            dtDiag.LoadAll();

            dtDiag = Common.NursingCare.RemoveClosedDiagnosaAndChildRelated(dtDiag);

            Common.NursingCare.SaveDiagL10(RegNo, PopulateSelectedDiagnosaDiagnosa(dtDiag), dtDiag, hd);
            return true;
        }

        private string RegNo
        {
            get {
                return Request.QueryString["regno"];
            }
        }

        private string selectedDiag {
            get {
                return Request.QueryString["diagTypes"];
            }
        }

        private NursingDiagnosaTransDT[] PopulateSelectedDiagnosaDiagnosa(NursingDiagnosaTransDTCollection dtDiag)
        {
            List<NursingDiagnosaTransDT> selectedDiag = new List<NursingDiagnosaTransDT>();
            foreach (GridDataItem x in gridListDiagnosa.MasterTableView.Items)
            {
                var IdDiag = System.Convert.ToInt64(x.GetDataKeyValue("ID"));

                // kalau diagnosa ini sudah close skip saja tidak usah diambil lagi
                if ((dtDiag.Where(y => y.ID == IdDiag)).Count() == 0) continue;
                if ((dtDiag.Where(y => y.ID == IdDiag)).First().SRNursingCarePlanning == "01") continue;

                var txt = x.FindControl("txtDefaultPriority") as RadTextBox;
                if (txt != null)
                {
                    NursingDiagnosaTransDT itm = new NursingDiagnosaTransDT();
                    itm.ID = IdDiag;
                    itm.Priority = Int32.Parse(txt.Text);
                    itm.TransactionNo = Common.NursingCare.GetTransHD(RegNo)[0].TransactionNo;
                    itm.NursingDiagnosaID = x.GetDataKeyValue("NursingDiagnosaID").ToString();
                    itm.NursingDiagnosaName = (x.FindControl("txtNursingDiagnosaNameTransDT") as RadTextBox).Text; // x["NursingDiagnosaName"].Text;
                    itm.NursingDiagnosaParentID = x["NursingDiagnosaParentID"].Text;

                    var dtp = x.FindControl("rdtDateDiag") as RadDateTimePicker;
                    if (dtp != null) {
                        itm.ExecuteDateTime = dtp.SelectedDate;
                    }

                    var txt1 = x.FindControl("txtDefaultEvalPeriod") as RadNumericTextBox;
                    var txt2 = x.FindControl("txtDefaultPeriodConversionInHour") as RadNumericTextBox;
                    if (txt1 != null && txt2 != null)
                    {
                        itm.EvalPeriod = txt1.Value.ToInt();
                        itm.PeriodConversionInHour = txt2.Value.ToInt();
                    }

                    selectedDiag.Add(itm);
                }

            }
            return selectedDiag.ToArray();
        }

        protected void gridListDiagnosa_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var hd = Common.NursingCare.GetTransHD(RegNo);

            var xx = selectedDiag;

            var selectedTypes = xx.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            ((RadGrid)source).DataSource =
                NursingDiagnosaTransDT.NursingDiagnosaFullDefinition(hd[0].TransactionNo, selectedTypes.ToList());
        }
    }
}
