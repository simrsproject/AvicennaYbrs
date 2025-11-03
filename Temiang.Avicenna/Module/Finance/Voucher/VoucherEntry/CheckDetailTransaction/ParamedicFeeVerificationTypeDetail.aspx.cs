using System;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.CheckDetailTransaction
{
    public partial class ParamedicFeeVerificationTypeDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.VOUCHER_MEMORIAL;

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["src"]))
                {
                    ((Button)Helper.FindControlRecursive(Page, "btnOk")).Visible = false;
                    ((Button)Helper.FindControlRecursive(Page, "btnCancel")).Visible = false;
                }

                ViewState["VerificationNo"] = string.Empty;
            }
        }

        private DataTable ParamedicFeeVerifications
        {
            get
            {
                if (ViewState["ParamedicFeeVerifications"] != null)
                    return (DataTable)ViewState["ParamedicFeeVerifications"];

                var query = new ParamedicFeeVerificationQuery("a");
                var prm = new ParamedicQuery("b");
                var journal = new JournalTransactionsQuery("c");

                var journalId = Request.QueryString["ivd"];

                query.Select
                    (
                        query.VerificationNo.As("TransactionNo"),
                        query.VerificationDate,
                        "<a.paramedicID + ' ' + b.ParamedicName AS Paramedic>",
                        query.StartDate,
                        query.EndDate,
                        query.VerificationAmount,
                        query.TaxAmount,
                        query.IsApproved,
                        query.IsVoid,
                        query.LastUpdateByUserID,
                        journal.RefferenceNumber
                    );

                query.InnerJoin(prm).On(query.ParamedicID == prm.ParamedicID);
                query.InnerJoin(journal).On(query.VerificationNo == journal.RefferenceNumber);
               
                query.Where
                    (
                        journal.JournalId == journalId
                    );


                query.OrderBy
                    (
                        query.VerificationNo.Ascending
                    );


                DataTable tbl = query.LoadDataTable();

                ViewState["ParamedicFeeVerifications"] = tbl;
                return tbl;
            }
            set
            { ViewState["ParamedicFeeVerifications"] = value; }
        }



        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var journalId = Request.QueryString["ivd"];
            JournalTransactions entity = JournalTransactions.Get(Convert.ToInt32(journalId));

            grdDetail.DataSource = ParamedicFeeVerifications;

        }

        protected void grdFeeCalculation_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdFeeCalculation.DataSource = ParamedicFeeTransChargesItemCompByDischargeDate().AsQueryable();
        }
        private ParamedicFeeTransChargesItemCompByDischargeDateCollection
            ParamedicFeeTransChargesItemCompByDischargeDate()
        {

            var coll = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();

            var query = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("a");
            //var txhQ = new TransChargesQuery("c");
            var itemQ = new ItemQuery("d");
            var regQ = new RegistrationQuery("e");
            var patQ = new PatientQuery("f");
            var guarQ = new GuarantorQuery("j");
            //var tcQ = new TariffComponentQuery("k");
            var journal = new JournalTransactionsQuery("jn");

            var journalId = Request.QueryString["ivd"];

            query.InnerJoin(journal).On(query.VerificationNo == journal.RefferenceNumber/* && journal.JournalId == journalId*/);
            //query.LeftJoin(txhQ).On(query.TransactionNo == txhQ.TransactionNo);
            query.LeftJoin(itemQ).On(query.ItemID == itemQ.ItemID);
            query.InnerJoin(regQ).On(query.RegistrationNo == regQ.RegistrationNo);
            query.InnerJoin(patQ).On(regQ.PatientID == patQ.PatientID);
            query.InnerJoin(guarQ).On(regQ.GuarantorID == guarQ.GuarantorID);
            //query.LeftJoin(tcQ).On(query.TariffComponentID == tcQ.TariffComponentID);

            query.Where(journal.JournalId == journalId);
            query.OrderBy(regQ.RegistrationNo.Ascending, itemQ.ItemID.Ascending);

            query.Select
                (
                    query,
                    "<ISNULL(d.ItemName, j.GuarantorName) refToItem_ItemName>",
                    //itemQ.ItemName.As("refToItem_ItemName"),
                    query.RegistrationNo.As("refToTransCharges_RegistrationNo"),
                    patQ.MedicalNo.As("refToPatient_MedicalNo"),
                    patQ.PatientName.As("refToPatient_PatientName"),
                    guarQ.GuarantorName.As("refToGuarantor_GuarantorName"),
                    "<'' AS refToVwClosedRegistration_PaymentMethod>"
                    //tcQ.IsIncludeInTaxCalc.As("refToTariffComponent_IsIncludeInTaxCalc")
                );

            coll.Load(query);

            return coll;
        }

        protected void grdAddDeduc_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdAddDeduc.DataSource = ParamedicFeeAddDeducs();
        }

        private ParamedicFeeAddDeducCollection ParamedicFeeAddDeducs()
        {
            
                var coll = new ParamedicFeeAddDeducCollection();

                var query = new ParamedicFeeAddDeducQuery("a");
                var stdQuery = new AppStandardReferenceItemQuery("b");

                var journal = new JournalTransactionsQuery("jn");

                var journalId = Request.QueryString["ivd"];

                query.InnerJoin(journal).On(query.VerificationNo == journal.RefferenceNumber);
                query.InnerJoin(stdQuery).On(query.SRParamedicFeeAdjustType == stdQuery.ItemID &&
                                             stdQuery.StandardReferenceID == "ParamedicFeeAdjustType");

                query.Where(journal.JournalId == journalId);
                query.OrderBy(query.TransactionNo.Ascending);

                query.Select
                    (
                        query,
                        stdQuery.ItemName.As("refToAppStandardReferenceItem_ItemName")
                    );

                coll.Load(query);
                return coll;
                
        }
    }
}
