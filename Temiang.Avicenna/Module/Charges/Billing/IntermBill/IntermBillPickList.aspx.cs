using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Charges.Billing
{
    public partial class IntermBillPickList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            switch (Request.QueryString["type"])
            {
                case "usr":
                    ProgramID = AppConstant.Program.IntermBill;
                    break;
                case "all":
                    ProgramID = AppConstant.Program.IntermBillAll;
                    break;
            }
        }

        private DataTable CostCalculations()
        {
            var query = new CostCalculationQuery("a");
            var transQ = new VwTransactionQuery("b");
            var itemQ = new ItemQuery("c");
            var suQ = new ServiceUnitQuery("d");
            var pyQ = new TransPaymentItemOrderQuery("f");
            var transDetQ = new TransChargesItemQuery("g");

            var group = new esQueryItem(query, "Group", esSystemType.String);
            group = transQ.TransactionDate.Cast(esCastType.String);

            query.Select(
                query.IntermBillNo,
                query.RegistrationNo,
                query.TransactionNo,
                query.SequenceNo,
                query.ItemID,
                @"<CASE WHEN ISNULL(g.ParamedicCollectionName, '') = '' THEN c.ItemName ELSE c.ItemName + ' (' + g.ParamedicCollectionName + ')' END AS ItemName>",
                //itemQ.ItemName,
                query.PatientAmount,
                query.GuarantorAmount,
                query.DiscountAmount,
                transQ.TransactionDate,
                suQ.ServiceUnitName,
                group.As("Group")
                );
            query.InnerJoin(transQ).On(query.TransactionNo == transQ.TransactionNo);
            query.InnerJoin(itemQ).On(query.ItemID == itemQ.ItemID);
            query.InnerJoin(suQ).On(transQ.ServiceUnitID == suQ.ServiceUnitID);
            query.LeftJoin(pyQ).On(query.TransactionNo == pyQ.TransactionNo && query.SequenceNo == pyQ.SequenceNo &&
                                  pyQ.IsPaymentProceed == true && pyQ.IsPaymentReturned == false);
            query.LeftJoin(transDetQ).On(query.TransactionNo == transDetQ.TransactionNo &&
                                         query.SequenceNo == transDetQ.SequenceNo);

            if (bool.Parse(Request.QueryString["presc"]) == false)
            {
                var tcQ = new TransChargesQuery("g");
                query.InnerJoin(tcQ).On(query.TransactionNo == tcQ.TransactionNo);
            }

            query.Where(
                query.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(Request.QueryString["rno"])),
                transQ.TransactionDate >= Convert.ToDateTime(Request.QueryString["sd"]),
                transQ.TransactionDate < Convert.ToDateTime(Request.QueryString["ed"]).AddDays(1),
                query.IntermBillNo.IsNull(),
                pyQ.PaymentNo.IsNull()
                );
            if (!string.IsNullOrEmpty(Request.QueryString["su"]))
                query.Where(transQ.ServiceUnitID == Request.QueryString["su"]);
            if (!string.IsNullOrEmpty(Request.QueryString["it"]))
                query.Where(itemQ.SRItemType == Request.QueryString["it"]);
            
            query.OrderBy(
                suQ.ServiceUnitName.Ascending,
                transQ.TransactionDate.Ascending
                );

            var dtbm = query.LoadDataTable();

            foreach (DataRow row in dtbm.Rows)
            {
                row["Group"] = Convert.ToDateTime(row["Group"]).ToString(AppConstant.DisplayFormat.Date);
            }

            dtbm.AcceptChanges();

            return dtbm;
        }

        private DataTable CostCalculations2()
        {
            //tgl transaksi u/ item koreksi ambil tgl reference no. tujuannya adalah agak di cetakan billing tidak ada
            //transaksi minus. 

            var query = new CostCalculationQuery("a");
            var transQ = new VwTransactionQuery("b");
            var itemQ = new ItemQuery("c");
            var suQ = new ServiceUnitQuery("d");
            var pyQ = new TransPaymentItemOrderQuery("f");
            var transDetQ = new TransChargesItemQuery("g");

            var group = new esQueryItem(query, "Group", esSystemType.String);
            group = transQ.TransactionDate.Cast(esCastType.String);

            query.Select(
                query.IntermBillNo,
                query.RegistrationNo,
                query.TransactionNo,
                query.SequenceNo,
                query.ItemID,
                @"<CASE WHEN ISNULL(g.ParamedicCollectionName, '') = '' THEN c.ItemName ELSE c.ItemName + ' (' + g.ParamedicCollectionName + ')' END AS ItemName>",
                //itemQ.ItemName,
                query.PatientAmount,
                query.GuarantorAmount,
                query.DiscountAmount,
                transQ.TransactionDate,
                suQ.ServiceUnitName,
                group.As("Group")
                );
            query.InnerJoin(transQ).On(query.TransactionNo == transQ.TransactionNo);
            query.InnerJoin(itemQ).On(query.ItemID == itemQ.ItemID);
            query.InnerJoin(suQ).On(transQ.ServiceUnitID == suQ.ServiceUnitID);
            query.LeftJoin(pyQ).On(query.TransactionNo == pyQ.TransactionNo && query.SequenceNo == pyQ.SequenceNo &&
                                  pyQ.IsPaymentProceed == true && pyQ.IsPaymentReturned == false);
            query.LeftJoin(transDetQ).On(query.TransactionNo == transDetQ.TransactionNo &&
                                         query.SequenceNo == transDetQ.SequenceNo);

            if (bool.Parse(Request.QueryString["presc"]) == false)
            {
                var tcQ = new TransChargesQuery("g");
                query.InnerJoin(tcQ).On(query.TransactionNo == tcQ.TransactionNo);
            }

            query.Where(
                query.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(Request.QueryString["rno"])),
                transQ.FilterDate >= Convert.ToDateTime(Request.QueryString["sd"]),
                transQ.FilterDate < Convert.ToDateTime(Request.QueryString["ed"]).AddDays(1),
                query.IntermBillNo.IsNull(),
                pyQ.PaymentNo.IsNull()
                );
            query.OrderBy(
                suQ.ServiceUnitName.Ascending,
                transQ.TransactionDate.Ascending
                );

            var dtbm = query.LoadDataTable();

            foreach (DataRow row in dtbm.Rows)
            {
                row["Group"] = Convert.ToDateTime(row["Group"]).ToString(AppConstant.DisplayFormat.Date);
            }

            dtbm.AcceptChanges();

            return dtbm;


            //// transaction
            //var query = new CostCalculationQuery("a");
            //var transQ = new VwTransactionQuery("b");
            //var itemQ = new ItemQuery("c");
            //var suQ = new ServiceUnitQuery("d");
            //var pyQ = new TransPaymentItemOrderQuery("f");

            //var group = new esQueryItem(query, "Group", esSystemType.String);
            //group = transQ.TransactionDate.Cast(esCastType.String);

            //query.Select(
            //    query.IntermBillNo,
            //    query.RegistrationNo,
            //    query.TransactionNo,
            //    query.SequenceNo,
            //    query.ItemID,
            //    itemQ.ItemName,
            //    query.PatientAmount,
            //    query.GuarantorAmount,
            //    query.DiscountAmount,
            //    transQ.TransactionDate,
            //    suQ.ServiceUnitName,
            //    group.As("Group")
            //    );
            //query.InnerJoin(transQ).On(query.TransactionNo == transQ.TransactionNo);
            //query.InnerJoin(itemQ).On(query.ItemID == itemQ.ItemID);
            //query.InnerJoin(suQ).On(transQ.ServiceUnitID == suQ.ServiceUnitID);
            //query.LeftJoin(pyQ).On(query.TransactionNo == pyQ.TransactionNo && query.SequenceNo == pyQ.SequenceNo &&
            //                      pyQ.IsPaymentProceed == true && pyQ.IsPaymentReturned == false);

            //if (bool.Parse(Request.QueryString["presc"]) == false)
            //{
            //    var tcQ = new TransChargesQuery("g");
            //    query.InnerJoin(tcQ).On(query.TransactionNo == tcQ.TransactionNo);
            //}

            //query.Where(
            //    query.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(Request.QueryString["rno"])),
            //    transQ.TransactionDate.Date() >= Request.QueryString["sd"],
            //    transQ.TransactionDate.Date() <= Request.QueryString["ed"],
            //    query.IntermBillNo.IsNull(),
            //    pyQ.PaymentNo.IsNull(),
            //    transQ.IsCorrection == false
            //    );
            //query.OrderBy(
            //    suQ.ServiceUnitName.Ascending,
            //    transQ.TransactionDate.Ascending
            //    );

            //var dtb = query.LoadDataTable();

            //// correction
            //var query2 = new CostCalculationQuery("a");
            //var transQ2 = new TransChargesQuery("b");
            //var itemQ2 = new ItemQuery("c");
            //var suQ2 = new ServiceUnitQuery("d");
            //var pyQ2 = new TransPaymentItemOrderQuery("f");
            //var transCorrectionQ = new VwTransactionQuery("g");

            //var group2 = new esQueryItem(query2, "Group", esSystemType.String);
            //group2 = transCorrectionQ.TransactionDate.Cast(esCastType.String);

            //query2.Select(
            //    query2.IntermBillNo,
            //    query2.RegistrationNo,
            //    query2.TransactionNo,
            //    query2.SequenceNo,
            //    query2.ItemID,
            //    itemQ2.ItemName,
            //    query2.PatientAmount,
            //    query2.GuarantorAmount,
            //    query2.DiscountAmount,
            //    transCorrectionQ.TransactionDate,
            //    suQ2.ServiceUnitName,
            //    group2.As("Group")
            //    );
            //query2.InnerJoin(transQ2).On(query2.TransactionNo == transQ2.TransactionNo);
            //query2.InnerJoin(transCorrectionQ).On(transQ2.ReferenceNo == transCorrectionQ.TransactionNo);
            //query2.InnerJoin(itemQ2).On(query2.ItemID == itemQ2.ItemID);
            //query2.InnerJoin(suQ2).On(transQ2.ToServiceUnitID == suQ2.ServiceUnitID);
            //query2.LeftJoin(pyQ2).On(query2.TransactionNo == pyQ2.TransactionNo && query2.SequenceNo == pyQ2.SequenceNo &&
            //                      pyQ2.IsPaymentProceed == true && pyQ2.IsPaymentReturned == false);

            //query2.Where(
            //    query2.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(Request.QueryString["rno"])),
            //    transCorrectionQ.TransactionDate.Date() >= Request.QueryString["sd"],
            //    transCorrectionQ.TransactionDate.Date() <= Request.QueryString["ed"],
            //    query2.IntermBillNo.IsNull(),
            //    pyQ2.PaymentNo.IsNull(),
            //    transQ2.IsCorrection == true
            //    );
            //query2.OrderBy(
            //    suQ2.ServiceUnitName.Ascending,
            //    transQ2.TransactionDate.Ascending
            //    );

            //var dtb2 = query2.LoadDataTable();

            //DataTable dtbm = dtb;
            //dtbm.Merge(dtb2);

            //foreach (DataRow row in dtbm.Rows)
            //{
            //    row["Group"] = Convert.ToDateTime(row["Group"]).ToString(AppConstant.DisplayFormat.Date);
            //}

            //dtbm.AcceptChanges();

            //return dtbm;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            switch (AppSession.Parameter.HealthcareInitialAppsVersion)
            {
                case "RSSA":
                    ((RadGrid)source).DataSource = CostCalculations2();
                    break;

                default:
                    ((RadGrid)source).DataSource = CostCalculations();
                    break;
            }
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            var selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.command = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            var cc = (CostCalculationCollection)Session["IntermBill:collCostCalculation" + Request.UserHostName];

            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                if (((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                {
                    var entity = new CostCalculation();
                    entity.LoadByPrimaryKey(dataItem["RegistrationNo"].Text, dataItem["TransactionNo"].Text, dataItem["SequenceNo"].Text);
                    entity.IntermBillNo = Request.QueryString["ibno"];

                    cc.AttachEntity(entity);
                }
            }

            return true;
        }
    }
}
