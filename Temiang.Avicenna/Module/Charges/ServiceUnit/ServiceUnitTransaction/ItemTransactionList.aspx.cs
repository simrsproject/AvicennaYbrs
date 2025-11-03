using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class ItemTransactionList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var reg = new Registration();
                if (reg.LoadByPrimaryKey(Request.QueryString["reg"]))
                {
                    var patient = new Patient();
                    patient.LoadByPrimaryKey(reg.PatientID);

                    txtFromDate.SelectedDate = reg.RegistrationDate;
                    txtToDate.SelectedDate = DateTime.Now;

                    Title = "Transaction List : " + patient.PatientName + "  [MRN : " + patient.MedicalNo + " | " + reg.RegistrationNo + "]";
                }

                if (AppSession.Parameter.IsTariffPriceVisibleOnlyForAdm)
                {
                    grdList.Columns.FindByUniqueName("Price").Visible = false;
                    grdList.Columns.FindByUniqueName("Discount").Visible = false;
                    grdList.Columns.FindByUniqueName("CitoAmount").Visible = false;
                    grdList.Columns.FindByUniqueName("Total").Visible = false;
                }

                (Helper.FindControlRecursive(this, "btnOk") as Button).Visible = false;
                (Helper.FindControlRecursive(this, "btnCancel") as Button).Text = "Close";
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var query = new TransChargesItemQuery("a");
            var item = new ItemQuery("b");
            var header = new TransChargesQuery("c");
            var unit = new ServiceUnitQuery("d");
            var cost = new CostCalculationQuery("e");
            var reg = new RegistrationQuery("f");
            var pat = new PatientQuery("i");
            var cls = new ClassQuery("cls");
            var tcReff = new TransChargesQuery("tcReff");
            var usr = new AppUserServiceUnitQuery("usr");

            var group = new esQueryItem(query, "Group", esSystemType.String);
            group = header.TransactionDate.Cast(esCastType.String);

            var std = new AppStandardReferenceItemQuery("z");

            query.Select
                (
                    header.RegistrationNo,
                    query.TransactionNo,
                    query.SequenceNo,
                    @"<CASE WHEN a.IsApprove = 1 AND c.IsApproved = 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsApprove>",
                    query.IsVoid,
                    @"<CASE WHEN a.IsBillProceed = 1 AND c.IsBillProceed = 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsBillProceed>",
                    query.IsOrderRealization,
                    query.ItemID,
                    query.ChargeQuantity,
                    query.SRItemUnit,
                    query.Price,
                    query.DiscountAmount,
                    query.CitoAmount,
                    header.LastUpdateByUserID,
                    @"<CASE WHEN e.LastUpdateDateTime = '01/01/1900 00:00:00.000' THEN '' 
                                ELSE e.LastUpdateDateTime END AS 'LastUpdateDateTime'>",
                    @"<CASE WHEN a.IsBillProceed = 1 AND c.IsBillProceed = 1 THEN 
                                CASE WHEN c.IsCorrection = 1 
                                    THEN 0 - (((ABS(a.ChargeQuantity) * a.Price) - a.DiscountAmount) + a.CitoAmount)
                                    ELSE ((a.ChargeQuantity * a.Price) - a.DiscountAmount) + a.CitoAmount
                                END
                            ELSE 0 END AS Total>",
                    "<b.[ItemName] + case ISNULL(a.[ParamedicCollectionName],'') when '' then '' else (' (' + a.[ParamedicCollectionName] + ')') end as ItemName>",
                    header.TransactionDate,
                    header.ToServiceUnitID.As("ServiceUnitID"),
                    header.ClassID,
                    unit.ServiceUnitName,
                    header.IsOrder,
                    group.As("Group"),
                    "<'' AS ORDERKEY>",
                    "<'1' AS TYPE>",
                    reg.IsHoldTransactionEntry,
                    @"<CAST(0 AS BIT) AS IsPaymentProceed>",
                    @"<'' AS PaymentNo>",
                    @"<CASE WHEN e.IntermBillNo IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsIntermBillProceed>",
                    @"<CASE WHEN e.IntermBillNo IS NULL THEN '' ELSE ' - ' + e.IntermBillNo END AS IntermBillNo>",
                    @"<CAST(0 AS BIT) AS IsPaymentProceedReff>",
                    pat.PatientName,
                    header.IsCorrection,
                    @"<ISNULL(tcReff.TransactionDate, c.TransactionDate) AS OrderDate>",
                    @"<ISNULL(tcReff.TransactionNo, c.TransactionNo) AS OrderTransNo>",
                    header.ExecutionDate,
                    cls.ClassName,
                    query.IsCorrection.As("IsCorrected"),
                    std.ItemName.Coalesce("''").As("DiscountReason"),
                    @"<ISNULL(e.IntermBillNo, '') AS CcIntermBillNo>"
                );

            query.LeftJoin(std).On(query.SRDiscountReason == std.ItemID && std.StandardReferenceID == AppEnum.StandardReference.DiscountReason.ToString());

            query.InnerJoin(header).On(query.TransactionNo == header.TransactionNo);
            query.InnerJoin(reg).On(header.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            query.InnerJoin(item).On(query.ItemID == item.ItemID);
            query.LeftJoin(cls).On(header.ClassID == cls.ClassID);
            query.InnerJoin(unit).On(header.ToServiceUnitID == unit.ServiceUnitID);
            query.LeftJoin(cost).On(
                    query.TransactionNo == cost.TransactionNo &&
                    query.SequenceNo == cost.SequenceNo
                );
            query.LeftJoin(tcReff).On(header.ReferenceNo == tcReff.TransactionNo);
            query.InnerJoin(usr).On(usr.ServiceUnitID == header.ToServiceUnitID && usr.UserID == AppSession.UserLogin.UserID);

            query.Where(
                header.RegistrationNo == Request.QueryString["reg"].ToString(),
                query.Or(
                        header.PackageReferenceNo == string.Empty,
                        header.PackageReferenceNo.IsNull()
                        ),
                header.IsVoid == false,
                query.IsVoid == false,
                query.Or(
                    query.ParentNo == string.Empty,
                    query.ParentNo.IsNull()
                    )
                );

            if (!txtFromDate.IsEmpty && !txtToDate.IsEmpty)
                query.Where(header.TransactionDate.Date().Between(txtFromDate.SelectedDate.Value.Date,
                                                                  txtToDate.SelectedDate.Value.Date));
            if (!string.IsNullOrEmpty(cboItemID.SelectedValue))
                query.Where(query.ItemID == cboItemID.SelectedValue);

            switch (Request.QueryString["type"])
            {
                case "jo":
                    query.Where(header.IsOrder == true, header.IsProceed == true, header.IsCorrection == false);
                    break;
                case "ds":
                    query.Where(header.IsOrder == true, header.IsProceed == false, header.IsCorrection == false);
                    break;
                case "tr":
                    query.Where(header.IsOrder == false, header.IsCorrection == false);
                    break;
                case "cr":
                    query.Where(header.IsCorrection == true);
                    break;
            }

            query.OrderBy
                (
                    header.ToServiceUnitID.Ascending,
                    header.TransactionDate.Ascending,
                    query.TransactionNo.Ascending,
                    query.SequenceNo.Ascending
                );

            DataTable tbl = query.LoadDataTable();

            foreach (DataRow row in tbl.Rows)
            {
                var charges = new TransChargesItemCollection();
                charges.Query.Where(charges.Query.TransactionNo == row["TransactionNo"], charges.Query.IsVoid == false);
                charges.LoadAll();
                decimal subTotal = 0;
                foreach (var x in charges)
                {
                    if ((bool)row["IsApprove"] & (bool)row["IsBillProceed"])
                    {
                        if ((bool)row["IsCorrection"])
                            subTotal += (0 -
                                         ((Math.Abs(x.ChargeQuantity ?? 0) * (x.Price ?? 0)) - (x.DiscountAmount ?? 0) +
                                          (x.CitoAmount ?? 0)));
                        else
                            subTotal += (((x.ChargeQuantity ?? 0) * (x.Price ?? 0)) - (x.DiscountAmount ?? 0) + (x.CitoAmount ?? 0));
                    }
                }

                row["Group"] = Convert.ToDateTime(row["Group"]).ToString(AppConstant.DisplayFormat.Date) + " - " +
                              row["TransactionNo"] + " - " + row["ClassName"];
                if ((bool)row["IsOrder"] && !(bool)row["IsOrderRealization"])
                    row["Total"] = 0D;
            }

            tbl.AcceptChanges();

            grdList.DataSource = tbl;
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"] + " [" + ((DataRowView)e.Item.DataItem)["ItemID"] + "]";
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ItemQuery("a");
            var tciq = new TransChargesItemQuery("b");
            var tcq = new TransChargesQuery("c");
            var rq = new RegistrationQuery("d");

            query.InnerJoin(tciq).On(query.ItemID == tciq.ItemID);
            query.InnerJoin(tcq).On(tciq.TransactionNo == tcq.TransactionNo);
            query.InnerJoin(rq).On(tcq.RegistrationNo == rq.RegistrationNo);

            query.es.Top = 20;
            query.es.Distinct = true;
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );

            query.Where(
                    query.Or(
                            query.ItemName.Like(searchTextContain),
                            query.ItemID.Like(searchTextContain)
                        ),
                    rq.RegistrationNo == Request.QueryString["reg"]);
            //rq.PatientID == Request.QueryString["pid"]);

            switch (Request.QueryString["type"])
            {
                case "jo":
                    query.Where(tcq.IsOrder == true, tcq.IsProceed == true, tcq.IsCorrection == false);
                    break;
                case "ds":
                    query.Where(tcq.IsOrder == true, tcq.IsProceed == false, tcq.IsCorrection == false);
                    break;
                case "tr":
                    query.Where(tcq.IsOrder == false, tcq.IsCorrection == false);
                    break;
                case "cr":
                    query.Where(tcq.IsCorrection == true);
                    break;
            }

            query.OrderBy(query.ItemName.Ascending);

            cboItemID.DataSource = query.LoadDataTable();
            cboItemID.DataBind();
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        public string GetStatus(object isOrder, object IsOrderRealization, object IsApprove)
        {
            if (IsApprove.Equals(false))
                return "<img src=\"../../../../Images/Toolbar/post16_d.png\" border=\"0\" />";
            else
            {
                if (isOrder.Equals(false))
                    return "<img src=\"../../../../Images/Toolbar/post16.png\" border=\"0\" />";
                else
                {
                    if (IsOrderRealization.Equals(false))
                        return "<img src=\"../../../../Images/Toolbar/post16_d.png\" border=\"0\" />";
                    else
                        return "<img src=\"../../../../Images/Toolbar/post16.png\" border=\"0\" />";
                }
            }
        }
    }
}