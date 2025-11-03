using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Charges.FinalizeBilling
{
    public partial class PrescriptionSplitBillInfo : BasePageDialog
    {
        private string regNo
        {
            get
            {
                return (Request.QueryString["regNo"]);
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.VerificationFinalizeBilling;

            if (!IsPostBack)
            {
                ButtonOk.Visible = false;
                ButtonCancel.Visible = false;
            }
        }

        public override bool OnButtonOkClicked()
        {
            return true;
        }

        protected void grdPrescription_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                grdPrescription.DataSource = TransPrescriptions;
                grdPrescription.MasterTableView.GroupsDefaultExpanded = true;
            }
        }

        private DataTable TransPrescriptions
        {
            get
            {
                var presc = new TransPrescriptionQuery("a");
                var prescdt = new TransPrescriptionItemQuery("b");
                var reg = new RegistrationQuery("c");
                var medic = new ParamedicQuery("d");
                var disp = new ServiceUnitQuery("e");
                var guar = new GuarantorQuery("f");

                presc.Select
                    (
                    presc.RegistrationNo,
                    presc.PrescriptionNo,
                    presc.PrescriptionDate,
                    medic.ParamedicName,
                    disp.ServiceUnitName.As("DispensaryName"),
                    prescdt.LineAmount.Sum().As("Total"),
                    presc.IsSplitBill,
                    presc.IsCash,
                    guar.GuarantorName
                    );

                presc.InnerJoin(prescdt).On(prescdt.PrescriptionNo == presc.PrescriptionNo && presc.IsApproval == true && presc.IsVoid == false);
                presc.InnerJoin(reg).On(reg.RegistrationNo == presc.RegistrationNo && reg.FromRegistrationNo == regNo && reg.IsVoid == false && reg.IsFromDispensary == true);
                presc.LeftJoin(medic).On(medic.ParamedicID == presc.ParamedicID);
                presc.InnerJoin(disp).On(presc.ServiceUnitID == disp.ServiceUnitID);
                presc.InnerJoin(guar).On(guar.GuarantorID == reg.GuarantorID);

                presc.Where(presc.Or(presc.IsCash == true, presc.IsSplitBill == true));

                presc.OrderBy(presc.IsCash.Ascending, presc.PrescriptionNo.Ascending);
                presc.GroupBy(
                    presc.RegistrationNo,
                    presc.PrescriptionNo,
                    presc.PrescriptionDate,
                    medic.ParamedicName,
                    disp.ServiceUnitName, 
                    presc.IsSplitBill,
                    presc.IsCash,
                    guar.GuarantorName);

                DataTable dtbl = presc.LoadDataTable();

                foreach (DataRow row in dtbl.Rows)
                {
                    if (row["IsCash"].ToBoolean() == true)
                    {
                        var tpio = new TransPaymentItemOrderCollection();
                        tpio.Query.Where(tpio.Query.TransactionNo == row["PrescriptionNo"],
                                         tpio.Query.IsPaymentProceed == true, tpio.Query.IsPaymentReturned == false);
                        tpio.LoadAll();
                        if (tpio.Count > 0)
                            row.Delete();
                    }
                }
                dtbl.AcceptChanges();

                return dtbl;
            }
        }

        protected void grdPrescription_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var query = new TransPrescriptionItemQuery("a");
            var qItem = new ItemQuery("b");
            var qItemI = new ItemQuery("c");

            var emb = new EmbalaceQuery("x");
            var cons = new ConsumeMethodQuery("y");

            var total = new esQueryItem(query, "Total", esSystemType.Decimal);
            total = query.ResultQty * (query.Price - query.DiscountAmount);

            query.Select
                (
                    query,
                    qItem.ItemName.As("ItemName"),
                    qItemI.ItemName.As("ItemInterventionName"),
                    total.As("Total"),
                    "<(a.ParentNo + a.SequenceNo) as ORDERKEY>",
                    emb.EmbalaceLabel.As("EmbalaceLabel"),
                    cons.SRConsumeMethodName.As("SRConsumeMethodName")
                );
            query.InnerJoin(qItem).On(query.ItemID == qItem.ItemID);
            query.LeftJoin(qItemI).On(query.ItemInterventionID == qItemI.ItemID);
            query.LeftJoin(emb).On(query.EmbalaceID == emb.EmbalaceID);
            query.LeftJoin(cons).On(query.SRConsumeMethod == cons.SRConsumeMethod);
            query.Where(query.PrescriptionNo == e.DetailTableView.ParentItem.GetDataKeyValue("PrescriptionNo").ToString());
            query.OrderBy("ORDERKEY", esOrderByDirection.Ascending);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        protected string GetItemName(object isRFlag, object itemName)
        {
            if (isRFlag.Equals(true))
                return itemName.ToString();
            else
                return "&nbsp;&nbsp;&nbsp;" + itemName.ToString();
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

    }
}