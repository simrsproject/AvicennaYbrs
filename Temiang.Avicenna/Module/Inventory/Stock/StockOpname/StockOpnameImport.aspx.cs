using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using System.Data;
using System.IO;
using System.Configuration;

namespace Temiang.Avicenna.Module.Inventory.Stock
{
    public partial class StockOpnameImport : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.StockOpname;
        }

        public override bool OnButtonOkClicked()
        {
            if (!fileuploadExcel.HasFile) return true;

            //if (ConfigurationManager.AppSettings["DocumentFolder"] == null) return true;
            //if (!Directory.Exists(ConfigurationManager.AppSettings["DocumentFolder"])) Directory.CreateDirectory(ConfigurationManager.AppSettings["DocumentFolder"]);
            //string path = ConfigurationManager.AppSettings["DocumentFolder"] + fileuploadExcel.PostedFile.FileName;
            
            string tmp_doc = AppParameter.GetParameterValue(AppParameter.ParameterItem.TmpDocumentFolder);
            if (string.IsNullOrEmpty(tmp_doc))
                tmp_doc = ConfigurationManager.AppSettings["DocumentFolder"];

            if (string.IsNullOrEmpty(tmp_doc)) return true;
            if (!Directory.Exists(tmp_doc))
                Directory.CreateDirectory(tmp_doc);
            string path = tmp_doc + fileuploadExcel.PostedFile.FileName;

            fileuploadExcel.SaveAs(path);

            try
            {
                DataTable table = Common.CreateExcelFile.LoadExcelFileToDataTable(path);
                if (table.Rows.Count > 0)
                {
                    //update row
                    foreach (DataRow row in table.Rows)
                    {
                        var iti = new ItemTransactionItem();
                        if (!iti.LoadByPrimaryKey(row["TransactionNo"].ToString(), row["SequenceNo"].ToString())) continue;
                        var isoa = new ItemStockOpnameApproval();
                        if (!isoa.LoadByPrimaryKey(iti.TransactionNo, iti.PageNo ?? 0)) continue;
                        if (isoa.IsApproved ?? false) continue;
                        iti.Quantity = Convert.ToDecimal(row["ActualQty"]);
                        iti.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        iti.LastUpdateDateTime = DateTime.Now;
                        iti.Save();
                    }

                    //approve

                }
                File.Delete(path);
            }
            catch (Exception e)
            {
                var i = e.Message.ToString();
                File.Delete(path);
            }

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.command = '';oWnd.argument.trno = ''";
        }

        private DataTable GetDataGridDataTable(string transactionNo)
        {
            esDynamicQuery query = GetQueryDetail(transactionNo);
            DataTable dtb = query.LoadDataTable();
            return dtb;
        }

        private esDynamicQuery GetQueryDetail(string transactionNo)
        {
            var query = new ItemTransactionItemQuery("a");
            var item = new ItemQuery("b");
            //var prevBal = new ItemStockOpnamePrevBalanceQuery("c");
            var medic = new ItemProductMedicQuery("d");
            var nonmedic = new ItemProductNonMedicQuery("e");
            //var std = new AppStandardReferenceItemQuery("f");
            //var ib = new ItemBalanceQuery("g");
            var kitchen = new ItemKitchenQuery("h");

            var trans = new ItemTransactionQuery("i");
            //var unit = new ServiceUnitQuery("j");
            //var loc = new LocationQuery("k");

            query.Select
                (
                //unit.ServiceUnitName,
                //loc.LocationName,
                //trans.TransactionDate,
                    query.TransactionNo,
                    query.SequenceNo,
                    query.ItemID,
                    item.ItemName.As("ItemName"),
                //prevBal.Quantity.As("PrevQty"),
                    query.Quantity.As("ActualQty"),
                    query.SRItemUnit//,
                //std.ItemName.As("ItemBinName")
                );
            query.InnerJoin(item).On(query.ItemID == item.ItemID);
            query.InnerJoin(trans).On(query.TransactionNo == trans.TransactionNo);
            //query.InnerJoin(unit).On(trans.FromServiceUnitID == unit.ServiceUnitID);
            //query.InnerJoin(loc).On(trans.FromLocationID == loc.LocationID && unit.LocationID == loc.LocationID);

            //query.InnerJoin(ib).On(ib.LocationID == trans.FromLocationID && ib.ItemID == item.ItemID);
            //query.LeftJoin(std).On(ib.SRItemBin == std.ItemID && std.StandardReferenceID == AppEnum.StandardReference.ItemBin);

            if (trans.SRItemType == BusinessObject.Reference.ItemType.Medical)
            {
                query.Select("<CAST((CASE WHEN d.IsInventoryItem = 1 THEN 1 ELSE 0 END) AS INT) AS IsInventoryItem>");
                query.InnerJoin(medic).On(item.ItemID == medic.ItemID);

            }
            else if (trans.SRItemType == BusinessObject.Reference.ItemType.NonMedical)
            {
                query.Select("<CAST((CASE WHEN e.IsInventoryItem = 1 THEN 1 ELSE 0 END) AS INT) AS IsInventoryItem>");
                query.InnerJoin(nonmedic).On(item.ItemID == nonmedic.ItemID);
            }
            else if (trans.SRItemType == BusinessObject.Reference.ItemType.Kitchen)
            {
                query.Select("<CAST((CASE WHEN h.IsInventoryItem = 1 THEN 1 ELSE 0 END) AS INT) AS IsInventoryItem>");
                query.InnerJoin(kitchen).On(item.ItemID == kitchen.ItemID);
            }

            //query.LeftJoin(prevBal).On(query.TransactionNo == prevBal.TransactionNo & query.ItemID == prevBal.ItemID);
            query.Where(query.TransactionNo == transactionNo);
            query.OrderBy(query.SequenceNo.Ascending);
            return query;
        }
    }
}
