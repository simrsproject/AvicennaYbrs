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
using System.Collections;

namespace Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.CheckDetailTransaction
{
    public partial class IncomeTypeDetail : BasePageDialog
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

                ViewState["TransactionNo"] = string.Empty;
            }
        }

        private DataTable TransPrescriptionItems
        {
            get
            {
                if (ViewState["TransPrescriptionItems"] != null)
                    return (DataTable)ViewState["TransPrescriptionItems"];

                var query = new TransPrescriptionItemQuery("a");
                var item = new ItemQuery("b");
                var header = new TransPrescriptionQuery("c");
                var journal = new JournalTransactionsQuery("d");
                var balance = new ItemMovementQuery("e");
                var itempm = new ItemProductMedicQuery("f");
                
                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = header.PrescriptionDate.Cast(esCastType.String);
                var journalId = Request.QueryString["ivd"];
                
                


                query.Select
                    (
                        
                        query.PrescriptionNo.As("TransactionNo"),
                        query.SequenceNo,
                        query.IsApprove,
                        query.IsVoid,
                        query.IsBillProceed,
                        "<CAST(0 AS BIT) AS IsOrderRealization>",
                        "<CASE WHEN a.ItemInterventionID = '' THEN a.ItemID ELSE a.ItemInterventionID END AS ItemID>",
                         //query.ResultQty.As("Quantity"),
                         @"< (e.QuantityOut - e.QuantityIn) as Quantity >",
                        query.SRItemUnit,
                        //"<CASE WHEN a.CostPrice = 0 AND (e.price = 0 OR e.price is NULL)THEN f.PriceInBasedUnitWVat " +
                        //" WHEN a.CostPrice = 0 AND e.price <> 0 THEN e.Price " +
                        //"ELSE a.CostPrice END AS CostPrice>",
                        balance.CostPrice, //ganti dari TransPrescriptionItem -> ItemMovement
                        @"< (a.embalaceamount + a.recipeAmount) as RecipeAMount >",
                        query.Price,
                        query.DiscountAmount,
                        query.LastUpdateByUserID,
                        //"<CAST(0 AS NUMERIC(18, 2)) AS CitoAmount>",
                        // "<CASE WHEN a.ResultQty < 0 THEN ((a.ResultQty * a.Price) + a.DiscountAmount - a.recipeAmount - a.embalaceamount) " +
                        // "ELSE ((a.ResultQty * a.Price) - a.DiscountAmount + a.recipeAmount + a.embalaceamount)END AS Total>",                           
                        "<CAST(0 AS NUMERIC(18, 2)) AS CitoAmount>",
                         "<CASE WHEN (e.QuantityOut - e.QuantityIn) < 0 THEN (((e.QuantityOut - e.QuantityIn) * a.Price) + a.DiscountAmount - a.recipeAmount - a.embalaceamount) " +
                         "ELSE (((e.QuantityOut - e.QuantityIn) * a.Price) - a.DiscountAmount + a.recipeAmount + a.embalaceamount)END AS Total>",
                        @"<CASE WHEN a.ItemInterventionID = '' THEN b.ItemName 
                            ELSE (SELECT ItemName FROM Item WHERE ItemID = a.ItemInterventionID) END AS ItemName>",
                        header.PrescriptionDate.As("TransactionDate"),
                        header.ServiceUnitID,
                        
                        "<CAST(0 AS BIT) AS IsOrder>",
                        group.As("Group"),
                        "<(a.ParentNo + a.SequenceNo) as ORDERKEY>",
                        "<'2' AS TYPE>",
                        journal.RefferenceNumber
                        
                       
                    );

                query.InnerJoin(header).On(query.PrescriptionNo == header.PrescriptionNo);
                query.LeftJoin(balance).On(query.PrescriptionNo == balance.TransactionNo && query.ItemID == balance.ItemID);
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.LeftJoin(itempm).On(query.ItemID == itempm.ItemID);
                query.InnerJoin(journal).On(query.PrescriptionNo == journal.RefferenceNumber);
                
                
                query.Where
                    (
                        journal.JournalId == journalId
                    );


                query.OrderBy
                    (
                        query.PrescriptionNo.Ascending,
                        query.SequenceNo.Ascending
                    );
                query.OrderBy("ORDERKEY", esOrderByDirection.Ascending);

                DataTable tbl = query.LoadDataTable();

                tbl.AcceptChanges();

                //foreach (DataRow row in tbl.Rows)
                //{
                //    row["Group"] = row["ServiceUnitName"] + " - " + row["TransactionNo"] + " - " + Convert.ToDateTime(row["Group"]).ToString(AppConstant.DisplayFormat.Date);
                //}

                ViewState["TransPrescriptionItems"] = tbl;
                return tbl;
            }
            set
            { ViewState["TransPrescriptionItems"] = value; }
        }

        private DataTable TransChargesItems
        {
            get
            {
                if (ViewState["TransChargesItems"] != null)
                    return (DataTable)ViewState["TransChargesItems"];

                var query = new TransChargesItemQuery("a");
                var item = new ItemQuery("b");
                var header = new TransChargesQuery("c");
                var journal = new JournalTransactionsQuery("d");
                var balance = new ItemMovementQuery("e");
                //var reg = new RegistrationQuery("f");
                
                //var group = new esQueryItem(query, "Group", esSystemType.String);
                //group = header.TransactionDate.Cast(esCastType.String);
                var journalId = Request.QueryString["ivd"];


                query.Select
                    (
                        header.RegistrationNo,
                        query.TransactionNo,
                        query.SequenceNo,
                        query.IsApprove,
                        query.IsVoid,
                        query.IsPackage,
                        //"<CASE WHEN a.CostPrice = 0 THEN e.Price ELSE a.CostPrice END AS CostPrice>",
                        query.CostPrice,
                        query.IsBillProceed,
                        query.IsOrderRealization,
                        query.ItemID,
                        item.ItemName,
                        query.ChargeQuantity,
                        query.SRItemUnit,
                        query.Price,
                        query.DiscountAmount,
                        query.CitoAmount,
                        @"< 0 as RecipeAmount >",
                        query.LastUpdateByUserID,
                        query.ChargeQuantity.As("Quantity"),
                        "<CASE WHEN a.ChargeQuantity >= 0 THEN ((a.ChargeQuantity * a.Price) - a.DiscountAmount + a.CitoAmount) " +
                        "ELSE ((a.ChargeQuantity * a.Price) + a.DiscountAmount - a.CitoAmount) END AS Total>",
                        header.TransactionDate,
                        header.ToServiceUnitID.As("ServiceUnitID")//,
                       
                        //reg.IsHoldTransactionEntry
                        
                    );

                query.InnerJoin(header).On(query.TransactionNo == header.TransactionNo);
                //query.LeftJoin(balance).On(query.TransactionNo == balance.TransactionNo & query.ItemID == balance.ItemID);
                //query.InnerJoin(reg).On(header.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.InnerJoin(journal).On(query.TransactionNo == journal.RefferenceNumber);
                
                query.Where(

                        journal.JournalId == journalId,
                        query.IsApprove == true
                        
                    );
             
                query.OrderBy
                    (
                        query.TransactionNo.Ascending,
                        query.SequenceNo.Ascending
                    );

                DataTable tbl = query.LoadDataTable();

                tbl.AcceptChanges();

                // paket untuk jurnal acrual
                if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsPackageRevenueOnMainPackage)) {
                    var tnoPkg = tbl.AsEnumerable().Where(r => ((bool)r["IsPackage"])).Select(r => r["TransactionNo"]).Distinct().ToArray();
                    if (tnoPkg.Length > 0) {
                        query = new TransChargesItemQuery("a");
                        item = new ItemQuery("b");
                        header = new TransChargesQuery("c");
                        //reg = new RegistrationQuery("f");

                        query.Select
                            (
                                header.RegistrationNo,
                                query.TransactionNo,
                                query.SequenceNo,
                                query.IsApprove,
                                query.IsVoid,
                                query.IsPackage,
                                //"<CASE WHEN a.CostPrice = 0 THEN e.Price ELSE a.CostPrice END AS CostPrice>",
                                query.CostPrice,
                                query.IsBillProceed,
                                query.IsOrderRealization,
                                query.ItemID,
                                item.ItemName,
                                query.ChargeQuantity,
                                query.SRItemUnit,
                                query.Price,
                                query.DiscountAmount,
                                query.CitoAmount,
                                @"< 0 as RecipeAmount >",
                                query.LastUpdateByUserID,
                                query.ChargeQuantity.As("Quantity"),
                                "<CASE WHEN a.ChargeQuantity >= 0 THEN ((a.ChargeQuantity * a.Price) - a.DiscountAmount + a.CitoAmount) " +
                                "ELSE ((a.ChargeQuantity * a.Price) + a.DiscountAmount - a.CitoAmount) END AS Total>",
                                header.TransactionDate,
                                header.ToServiceUnitID.As("ServiceUnitID")//,

                               // "<cast(0 as bit) IsHoldTransactionEntry>"

                            );

                        query.InnerJoin(header).On(query.TransactionNo == header.TransactionNo);
                        query.InnerJoin(item).On(query.ItemID == item.ItemID);
                        query.Where(
                            header.PackageReferenceNo.In(tnoPkg)
                            );

                        query.OrderBy
                            (
                                query.TransactionNo.Ascending,
                                query.SequenceNo.Ascending
                            );

                        DataTable tblDpkg = query.LoadDataTable();
                        tbl.Merge(tblDpkg);
                    }
                }
                //foreach (DataRow row in tbl.Rows)
                //{
                //    row["Group"] = row["ServiceUnitName"] + " - " + row["TransactionNo"] + " - " + Convert.ToDateTime(row["Group"]).ToString(AppConstant.DisplayFormat.Date);
                //    if ((bool)row["IsOrder"] && !(bool)row["IsOrderRealization"])
                //        row["Total"] = 0D;
                //}

                ViewState["TransChargesItems"] = tbl;

                return tbl;
            }
            set
            { ViewState["TransChargesItems"] = value; }
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var journalId = Request.QueryString["ivd"];
            JournalTransactions entity = JournalTransactions.Get(Convert.ToInt32(journalId));
            
            grdDetail.DataSource = (entity.RefferenceNumber.Substring(0, 2) == "RS") ? grdDetail.DataSource = TransPrescriptionItems : TransChargesItems;
            
        }

        protected void grdDetail_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                bool isVoid = (grdDetail.DataSource as DataTable).AsEnumerable().Where(i => i.Field<string>("SequenceNo") == (e.Item as GridDataItem).GetDataKeyValue("SequenceNo").ToString())
                                                                                .Select(i => i.Field<bool>("IsVoid"))
                                                                                .Take(1)
                                                                                .Single();
                if (isVoid)
                {
                    for (var i = 0; i < e.Item.Cells.Count; i++)
                    {
                        if (i > 0 && i < e.Item.Cells.Count)
                            e.Item.Cells[i].Font.Strikeout = true;

                    }
                }
            }
        }
    }
}
