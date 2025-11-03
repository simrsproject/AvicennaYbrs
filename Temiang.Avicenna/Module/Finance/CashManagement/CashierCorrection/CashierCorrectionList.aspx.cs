using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Drawing;

namespace Temiang.Avicenna.Module.Finance.CashManagement
{
    public partial class CashierCorrectionList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "CashierCorrectionSearch.aspx";
            UrlPageDetail = "CashierCorrectionDetail.aspx";

            this.WindowSearch.Height = 400;
            ProgramID = AppConstant.Program.CASHIER_CORRECTION;

            if (!IsPostBack)
            {

            }
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(TransPaymentCorrectionMetadata.ColumnNames.PaymentCorrectionNo).ToString();
            string url = string.Format("CashierCorrectionDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = PaymentCorrection;
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string PCNo = dataItem.GetDataKeyValue("PaymentCorrectionNo").ToString();
            //Load record

            var tpic = new TransPaymentItemCorrectionQuery("tpic");
            var tpi = new TransPaymentItemQuery("tpi");
            var tp = new TransPaymentQuery("tp");
            var reg = new RegistrationQuery("reg");
            var pat = new PatientQuery("pat");

            var pm = new PaymentMethodQuery("pm");

            var cpo = new AppStandardReferenceItemQuery("cpo");
            var cto = new AppStandardReferenceItemQuery("cto");
            var edco = new EDCMachineQuery("edco");

            var cpc = new AppStandardReferenceItemQuery("cpc");
            var ctc = new AppStandardReferenceItemQuery("ctc");
            var edcc = new EDCMachineQuery("edcc");


            tpic.InnerJoin(tpi).On(tpic.PaymentNo == tpi.PaymentNo && tpic.SequenceNo == tpi.SequenceNo)
                .InnerJoin(tp).On(tpic.PaymentNo == tp.PaymentNo)
                .InnerJoin(reg).On(tp.RegistrationNo == reg.RegistrationNo)
                .InnerJoin(pat).On(reg.PatientID == pat.PatientID)

                .InnerJoin(pm).On(tpi.SRPaymentType == pm.SRPaymentTypeID && tpi.SRPaymentMethod == pm.SRPaymentMethodID)

                .LeftJoin(cpo).On(cpo.StandardReferenceID == "CardProvider" && tpi.SRCardProvider == cpo.ItemID)
                .LeftJoin(cto).On(cto.StandardReferenceID == "CardType" && tpi.SRCardType == cto.ItemID)
                .LeftJoin(edco).On(tpi.EDCMachineID == edco.EDCMachineID)

                .LeftJoin(cpc).On(cpc.StandardReferenceID == "CardProvider" && tpic.SRCardProvider == cpc.ItemID)
                .LeftJoin(ctc).On(ctc.StandardReferenceID == "CardType" && tpic.SRCardType == ctc.ItemID)
                .LeftJoin(edcc).On(tpic.EDCMachineID == edcc.EDCMachineID)

                .Select(
                    tpic.PaymentCorrectionNo, tpic.PaymentNo, tpic.SequenceNo, reg.RegistrationNo, pat.PatientName,
                    pm.PaymentMethodName.As("PaymentMethodOName"), 
                    cpo.ItemName.As("CardProviderOName"), cto.ItemName.As("CardTypeOName"), edco.EDCMachineName.As("EDCMachineOName"),
                    cpc.ItemName.As("CardProviderCName"), ctc.ItemName.As("CardTypeCName"), edcc.EDCMachineName.As("EDCMachineCName"),
                    tpi.Amount
                )

                .Where(tpic.PaymentCorrectionNo == PCNo);

            DataTable dtb = tpic.LoadDataTable();
            //Apply
            e.DetailTableView.DataSource = dtb;
        }

        private DataTable PaymentCorrection
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                TransPaymentCorrectionQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (TransPaymentCorrectionQuery)Session[SessionNameForQuery];
                else
                {
                    query = new TransPaymentCorrectionQuery("a");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
