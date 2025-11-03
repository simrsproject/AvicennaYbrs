using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee
{
    public partial class ParamedicFeeRemunDetailItemDialog : BasePageDialog
    {
        private int RemunID
        {
            get
            {
                return System.Convert.ToInt32(Request.QueryString["remunid"]);
            }
        }
        private string ParamedicID {
            get {
                return Request.QueryString["parid"];
            }
        }
        private string ItemID
        {
            get
            {
                return Request.QueryString["itemid"];
            }
        }

        private string RemunNo
        {
            get
            {
                return Request.QueryString["remunno"];
            }
        }

        private DataTable feeRemunDetailTransaction
        {
            get
            {
                return (DataTable)Session["feeRemunDetailTransaction"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonOk.Visible = false;
                ButtonCancel.Text = "Close";
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            return true;
        }

        protected void grdRemunParamedic_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
            var tc = new TransChargesQuery("tc");
            //var suit = new ServiceUnitItemServiceQuery("suit");
            var i = new ItemQuery("i");
            var par = new ParamedicQuery("par");
            var idismf = new ItemIdiItemSmfQuery("idismf");
            var idic = new ItemIdiQuery("idic");
            var reg = new RegistrationQuery("reg");
            var pat = new PatientQuery("pat");
            var guar = new GuarantorQuery("guar");
            var su = new ServiceUnitQuery("su");
            var res = new TestResultQuery("res");

            var tpio = new TransPaymentItemOrderQuery("tpio");
            var tpiop = new TransPaymentQuery("tpiop");

            var cc = new CostCalculationQuery("cc");

            var tpib = new TransPaymentItemIntermBillQuery("tpib");
            var tpibp = new TransPaymentQuery("tpibp");

            var tpibg = new TransPaymentItemIntermBillGuarantorQuery("tpibg");
            var tpibgp = new TransPaymentQuery("tpibgp");


            fee.InnerJoin(tc).On(fee.TransactionNo == tc.TransactionNo)
                .InnerJoin(i).On(fee.ItemID == i.ItemID)
                .InnerJoin(par).On(fee.ParamedicID == par.ParamedicID)
                .InnerJoin(reg).On(tc.RegistrationNo == reg.RegistrationNo)
                .InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                .InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID && guar.SRGuarantorType == "09"/*BPJS*/)
                //.LeftJoin(suit).On(fee.ItemID == suit.ItemID && tc.ToServiceUnitID == suit.ServiceUnitID)
                .LeftJoin(idismf).On(idismf.ItemID == fee.ItemID && idismf.SmfID == par.SRParamedicRL1)
                .LeftJoin(idic).On(idismf.IdiCode == idic.IdiCode)
                .InnerJoin(su).On(tc.ToServiceUnitID == su.ServiceUnitID)
                .LeftJoin(res).On(fee.TransactionNo == res.TransactionNo && fee.ItemID == res.ItemID)

                .LeftJoin(tpio).On(fee.TransactionNo == tpio.TransactionNo && fee.SequenceNo == tpio.SequenceNo && tpio.IsPaymentProceed == true && tpio.IsPaymentReturned == false)
                .LeftJoin(tpiop).On(tpio.PaymentNo == tpiop.PaymentNo)

                .LeftJoin(cc).On(fee.TransactionNo == cc.TransactionNo && fee.SequenceNo == cc.SequenceNo)

                .LeftJoin(tpib).On(cc.IntermBillNo == tpib.IntermBillNo && tpib.IsPaymentProceed == true && tpib.IsPaymentReturned == false)
                .LeftJoin(tpibp).On(tpib.PaymentNo == tpibp.PaymentNo)

                .LeftJoin(tpibg).On(cc.IntermBillNo == tpibg.IntermBillNo && tpibg.IsPaymentProceed == true && tpibg.IsPaymentReturned == false)
                .LeftJoin(tpibgp).On(tpibg.PaymentNo == tpibgp.PaymentNo)

                .Select(fee.TransactionNo, fee.SequenceNo,
                    pat.MedicalNo, pat.PatientName, reg.RegistrationNo, i.ItemName, su.ServiceUnitName, tc.ExecutionDate, fee.Qty)
                //fee.ParamedicID, par.ParamedicName, fee.ItemID, i.ItemName, tc.ToServiceUnitID.As("ServiceUnitID"), suit.IdiCode, idic.IdiName,
                //fee.Qty.Sum(), (idic.F1 + idic.F21 + idic.F22 + idic.F23 + idic.F3 + idic.F4).Coalesce("0").As("Score"),
                //idic.Rvu.Coalesce("0").As("Rvu"))
                .Where(
                    fee.DischargeDateMergeTo.IsNotNull(),
                    fee.ParamedicID == ParamedicID, fee.ItemID == ItemID)
                .Where(
                    fee.Or(
                        fee.And(
                            //su.IsUsingJobOrder == true, 
                            i.SRItemType == "41",
                            res.ItemID.IsNotNull()),
                            //su.IsUsingJobOrder == false
                            i.SRItemType != "41"
                        )
                ); // hanya yang sudah ada hasil

            if (!string.IsNullOrEmpty(RemunNo))
            {
                var remun = new ParamedicFeeRemunByIdi();
                if (remun.LoadByRemunNo(RemunNo))
                {
                    fee.Where(fee.RemunByIdiID == remun.RemunID);
                }
                else
                {
                    fee.Where(fee.RemunByIdiID == -1);
                }
            }
            else {
                // unsave data, load from session
                if (feeRemunDetailTransaction != null)
                {
                    if (feeRemunDetailTransaction.Rows.Count > 0) {
                        fee.Where(fee.TransactionNo.In(
                            feeRemunDetailTransaction.AsEnumerable().Where(r => r["ParamedicID"].ToString() == ParamedicID && r["ItemID"].ToString() == ItemID)
                            .Select(r => r["TransactionNo"])),
                            fee.ItemID == ItemID,
                            fee.ParamedicID == ParamedicID
                         );
                    }
                    else
                    {
                        fee.Where(fee.RemunByIdiID == -1);
                    }
                }
                else {
                    fee.Where(fee.RemunByIdiID == -1);
                }
            }

            var dtFee = fee.LoadDataTable();

            grdRemunParamedic.DataSource = dtFee;
        }
    }
}
