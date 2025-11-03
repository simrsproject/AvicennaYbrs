using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using DevExpress.XtraPrinting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.Module.Inventory;
using Temiang.Avicenna.Module.Inventory.Procurement;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Util
{
    public class ApprovalLevelUtil
    {
        public static DataTable ApprovalLevelQue(string transactionNo)
        {
            // Tombol Approve hanya muncul jika level pertama atau level sebelumnya sudah di approve pada row user bersangkutan
            var atq = new ApprovalTransactionQuery("a");
            var userq = new AppUserQuery("b");
            atq.InnerJoin(userq).On(atq.UserID == userq.UserID);

            var userApprq = new AppUserQuery("c");
            atq.LeftJoin(userApprq).On(atq.ApprovedByUserID == userApprq.UserID);

            atq.Where(atq.TransactionNo == transactionNo);
            atq.OrderBy(atq.ApprovalLevel.Ascending);
            atq.Select(atq.ApprovalLevel, atq.UserID, atq.ApprovedDate.As("ApprovalDateTime"), atq.IsApproved, userq.UserName,
                userApprq.UserName.As("ApprovalByUserName"));
            var dtbApprov = atq.LoadDataTable();

            // ApproveAble
            dtbApprov.Columns.Add("IsApproveAble", typeof(System.Boolean));
            dtbApprov.Columns.Add("IsUnApproveAble", typeof(System.Boolean));

            // Lengkapi approved user name 
            var isExistApproval = false;
            foreach (DataRow row in dtbApprov.Rows)
            {
                if (!string.IsNullOrEmpty(row["ApprovalByUserName"].ToString()) &&
                    !row["ApprovalByUserName"].Equals(row["UserName"]))
                {
                    row["UserName"] = string.Format("{0} / {1}", row["UserName"], row["ApprovalByUserName"]);
                }

                if (!isExistApproval && !string.IsNullOrEmpty(row["ApprovalByUserName"].ToString()))
                {
                    isExistApproval = true;
                }
            }

            // Jika sudah void maka tombol approve tidak diaktifkan
            var it = new ItemTransaction();
            it.LoadByPrimaryKey(transactionNo);
            if (it.IsVoid == true) return dtbApprov;

            // Set tombol Approve & UnApprove
            var userID = AppSession.UserLogin.UserID;
            var userApprovalRowPosition = 0;

            var userApprovalLevel = 0;
            var lastApprovalLevel = 0;

            var isHigherApprovalLevelCanBypass =
                AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsHigherApprovalLevelCanBypass);


            // Cari posisi row user login belum approve
            var i = 1;
            foreach (DataRow row in dtbApprov.Rows)
            {
                if (false.Equals(row["IsApproved"]) && userID.Equals(row["UserID"]))
                {
                    userApprovalRowPosition = i;
                    userApprovalLevel = row["ApprovalLevel"].ToInt();
                    if (!isHigherApprovalLevelCanBypass)
                        break;
                }
                i++;
            }

            // lastApprovalLevel
            i = 1;
            foreach (DataRow row in dtbApprov.Rows)
            {
                if (true.Equals(row["IsApproved"]))
                {
                    lastApprovalLevel = row["ApprovalLevel"].ToInt();
                }
                i++;
            }

            if (userApprovalRowPosition > 0)
            {
                var row = dtbApprov.Rows[userApprovalRowPosition - 1];
                if (isHigherApprovalLevelCanBypass)
                {
                    row["IsApproveAble"] = true;
                }
                else if (userApprovalLevel - lastApprovalLevel == 1) //1 level dibawahnya sudah di approve
                {
                    row["IsApproveAble"] = true;
                }
            }

            // Set posisi tombol UnApprove
            // Bisa di UnApprove level bawahnya jika approval levelnya sudah ada yg mengapprove
            if (isExistApproval)
            {
                var length  = dtbApprov.Rows.Count;
                for (int j = length; j > 0; j--)
                {
                    var row = dtbApprov.Rows[j-1];

                    if (userID.Equals(row["UserID"]))
                    {
                        // Tombol UnApprove muncul di level yg lebih tinggi dari level approval terakhir jika IsHigherApprovalLevelCanBypass==true
                        if (isHigherApprovalLevelCanBypass && lastApprovalLevel <= row["ApprovalLevel"].ToInt())
                        {
                            row["IsUnApproveAble"] = true;
                            break;
                        }

                        // UnApprove hanya bisa dilakukan oleh user yg sudah mengapprove yg paling tinggi levelnya 
                        if (true.Equals(row["IsApproved"]) && lastApprovalLevel == row["ApprovalLevel"].ToInt())
                        {
                            row["IsUnApproveAble"] = true;
                            break;
                        }
                    }
                }
            }
            return dtbApprov;
        }

        public static bool IsApprovalLevelInProgress(ValidateArgs args, string transactionNo)
        {
            var apps = new ApprovalTransactionCollection();
            apps.Query.Where(apps.Query.TransactionNo == transactionNo, apps.Query.IsApproved == true);
            apps.LoadAll();
            if (apps.Count > 0)
            {
                args.IsCancel = true;
                args.MessageText = "This transaction can't be canceled, this data has been approved level progress";
                return true;
            }
            return false;
        }

        #region Approve

        public static void Approve(ValidateArgs args, string transactionCode, string trNo, int apprLevel, string UserID)
        {
            var it = new ItemTransaction();
            if (it.LoadByPrimaryKey(trNo) && it.IsApproved == true) return;

            var isApprovalLevelFinal = false;
            if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsHigherApprovalLevelCanBypass))
            {
                // Approve beserta level dibawahnya
                var atc = new ApprovalTransactionCollection();
                atc.Query.Where(atc.Query.TransactionNo == trNo && atc.Query.ApprovalLevel <= apprLevel &&
                                atc.Query.IsApproved == 0);
                atc.LoadAll();

                if (atc.Count == 0) return; // Asumsi sudah diapprove browser lain atau user lain 

                foreach (var at in atc)
                {
                    at.IsApproved = true;
                    at.ApprovedDate = DateTime.Now;
                    at.ApprovedByUserID = UserID;

                    if (!isApprovalLevelFinal)
                        isApprovalLevelFinal = Convert.ToBoolean(at.IsApprovalLevelFinal);
                }
                atc.Save();
            }
            else
            {
                // Approve the same level user
                var atc = new ApprovalTransactionCollection();
                atc.Query.Where(atc.Query.TransactionNo == trNo && atc.Query.ApprovalLevel == apprLevel && atc.Query.IsApproved == 0);
                atc.LoadAll();

                if (atc.Count == 0) return; // Asumsi sudah diapprove browser lain atau user lain 

                foreach (var at in atc)
                {
                    at.IsApproved = true;
                    at.ApprovedDate = DateTime.Now;
                    at.ApprovedByUserID = UserID;
                    isApprovalLevelFinal = Convert.ToBoolean(at.IsApprovalLevelFinal);
                }
                atc.Save();
            }

            // Final
            if (isApprovalLevelFinal)
            {
                // Approve
                switch (transactionCode)
                {
                    case TransactionCode.PurchaseOrder:
                        ApprovePurchaseOrder(args, it, UserID);
                        // Send email to Supplier
                        EmailToSupplier(it);
                        break;
                    case TransactionCode.Distribution:
                        ApproveDistribution(args, it);
                        break;
                }

            }
            else
            {
                // Email to Approver next level
                if (!args.IsCancel)
                    EmailToApproverNextLevel(it, apprLevel);
            }

            // Email ke Purchasing Unit
            if (transactionCode == TransactionCode.PurchaseOrder)
            {
                if (!args.IsCancel)
                    EmailToPurchasingUnit(it, apprLevel);
            }
        }

        public static void UnApprove(ValidateArgs args, string transactionCode, string trNo, int apprLevel)
        {
            var it = new ItemTransaction();
            if (!it.LoadByPrimaryKey(trNo)) return;

            var isApprovalLevelFinal = false;
            if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsHigherApprovalLevelCanBypass))
            {
                // UnApprove beserta level dibawahnya
                var atc = new ApprovalTransactionCollection();
                atc.Query.Where(atc.Query.TransactionNo == trNo && atc.Query.ApprovalLevel <= apprLevel &&
                                atc.Query.IsApproved == 1);
                atc.LoadAll();

                if (atc.Count == 0) return; // Asumsi sudah diapprove browser lain atau user lain 

                foreach (var at in atc)
                {
                    at.IsApproved = false;
                    at.str.ApprovedDate = string.Empty;
                    at.str.ApprovedByUserID = string.Empty;

                    if (!isApprovalLevelFinal)
                        isApprovalLevelFinal = Convert.ToBoolean(at.IsApprovalLevelFinal);
                }
                atc.Save();
            }
            else
            {
                // Approve the same level user
                var atc = new ApprovalTransactionCollection();
                atc.Query.Where(atc.Query.TransactionNo == trNo && atc.Query.ApprovalLevel == apprLevel && atc.Query.IsApproved == 1);
                atc.LoadAll();

                if (atc.Count == 0) return; // Asumsi sudah diapprove browser lain atau user lain 

                foreach (var at in atc)
                {
                    at.IsApproved = false;
                    at.str.ApprovedDate = string.Empty;
                    at.str.ApprovedByUserID = string.Empty;
                    isApprovalLevelFinal = Convert.ToBoolean(at.IsApprovalLevelFinal);
                }
                atc.Save();
            }

            // Final
            if (isApprovalLevelFinal)
            {
                // Approve
                switch (transactionCode)
                {
                    case TransactionCode.PurchaseOrder:
                        UnApprovePurchaseOrder(args, it);
                        // Send email to Supplier
                        //EmailToSupplier(it);
                        break;
                    case TransactionCode.Distribution:
                        UnApproveDistribution(args, it);
                        break;
                }

            }
            //else
            //{
            //    // Email to Approver next level
            //    if (!args.IsCancel)
            //        EmailToApproverNextLevel(it, apprLevel);
            //}

            //// Email ke Purchasing Unit
            //if (transactionCode == TransactionCode.PurchaseOrder)
            //{
            //    if (!args.IsCancel)
            //        EmailToPurchasingUnit(it, apprLevel);
            //}
        }


        private static void ApprovePurchaseOrder(ValidateArgs args, ItemTransaction it)
        {
            ApprovePurchaseOrder(args, it, AppSession.UserLogin.UserID);
        }
        private static void ApprovePurchaseOrder(ValidateArgs args, ItemTransaction it, string UserID)
        {
            PurchaseOrderDetail.ApprovePurchaseOrder(args, it.IsNonMasterOrder ?? false, it.TransactionNo, UserID);
        }
        private static void UnApprovePurchaseOrder(ValidateArgs args, ItemTransaction it)
        {
            PurchaseOrderDetail.UnApprovePurchaseOrder(args, it.IsNonMasterOrder ?? false, it.TransactionNo, AppSession.UserLogin.UserID);
        }
        private static void ApproveDistribution(ValidateArgs args, ItemTransaction it)
        {
            Module.Inventory.Warehouse.DistributionDetail.ApproveDistribution(args, it);
        }
        private static void UnApproveDistribution(ValidateArgs args, ItemTransaction it)
        {
            Module.Inventory.Warehouse.DistributionDetail.UnApproveDistribution(args, it);
        }
        #endregion

        public static DataTable PurchaseOrderItem(string transactionNo, string itemType, string porLocationID)
        {
            //Load record
            var query = new ItemTransactionItemQuery("a");
            var iq = new ItemQuery("b");
            var itiref = new ItemTransactionItemQuery("itr");
            query.Select(
                query.TransactionNo,
                query.ItemID,
                iq.ItemName,
                query.SRItemUnit,
                query.Quantity,
                query.QuantityFinishInBaseUnit,
                query.SequenceNo,
                query.Price,
                query.Discount1Percentage,
                query.Discount2Percentage,
                query.Discount,
                query.Discount,
                query.IsBonusItem,
                query.IsClosed,
                query.Description,
                query.ConversionFactor,
                @"<(ISNULL(itr.Quantity, 0) * ISNULL(itr.ConversionFactor, 0))/a.ConversionFactor AS QtyRequest>"
            );
            query.LeftJoin(iq).On(query.ItemID == iq.ItemID);
            query.LeftJoin(itiref).On(itiref.TransactionNo == query.ReferenceNo &&
                                      itiref.SequenceNo == query.ReferenceSequenceNo);

            // Base Unit
            var ipnmq = new ItemProductNonMedicQuery("i2");
            var ikq = new ItemKitchenQuery("i2");
            var ipmq = new ItemProductMedicQuery("i2");

            switch (itemType)
            {
                case ItemType.NonMedical:
                    query.LeftJoin(ipnmq).On(query.ItemID == ipnmq.ItemID);
                    break;
                case ItemType.Kitchen:
                    query.LeftJoin(ikq).On(query.ItemID == ikq.ItemID);
                    break;
                default:
                    query.LeftJoin(ipmq).On(query.ItemID == ipmq.ItemID);
                    break;
            }

            // Balance Min Max
            var locationID = string.Empty;
            if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPorByStockGroup))
            {
                locationID = porLocationID;
                if (string.IsNullOrEmpty(locationID))
                {
                    query.Select(@"<CONVERT(decimal(10,2),0) AS BalanceSG>",
                        @"<CONVERT(decimal(10,2),0) AS Balance>",
                        @"<CONVERT(decimal(10,2),0) AS Minimum>",
                        @"<CONVERT(decimal(10,2),0) AS Maximum>",
                        @"<i2.SRItemUnit AS SRMasterBaseUnit>"
                        );
                }
                else
                {
                    var stockGroup = string.Empty;
                    var ibbsgq = new ItemBalanceByStockGroupQuery("c");
                    var loc = new Location();
                    loc.LoadByPrimaryKey(locationID);
                    if (!string.IsNullOrEmpty(loc.SRStockGroup))
                        stockGroup = loc.SRStockGroup;
                    query.LeftJoin(ibbsgq).On(query.ItemID == ibbsgq.ItemID && ibbsgq.SRStockGroup == stockGroup);


                    var ibq = new ItemBalanceQuery("bl");
                    query.LeftJoin(ibq).On(query.ItemID == ibq.ItemID && ibq.LocationID == locationID);


                    query.Select(@"<CONVERT(decimal(10,2),COALESCE(c.Balance,0)) AS BalanceSG>",
                        @"<CONVERT(decimal(10,2),COALESCE(bl.Balance,0)) AS Balance>",
                        @"<CONVERT(decimal(10,2),COALESCE(c.Minimum,0)) AS Minimum>",
                        @"<CONVERT(decimal(10,2),COALESCE(c.Maximum,0)) AS Maximum>",
                        @"<i2.SRItemUnit AS SRMasterBaseUnit>"
                        );
                }
            }
            else
            {
                locationID = ProcurementUtils.LocationIdByItemType(itemType);
                var ibq = new ItemBalanceQuery("c");
                if (string.IsNullOrEmpty(locationID))
                    locationID = "ABCD_EFG";
                query.LeftJoin(ibq).On(query.ItemID == ibq.ItemID && ibq.LocationID == locationID);

                query.Select(@"<CONVERT(decimal(10,2),0) AS BalanceSG>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Balance,0)) AS Balance>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Minimum,0)) AS Minimum>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Maximum,0)) AS Maximum>",
                    @"<i2.SRItemUnit AS SRMasterBaseUnit>"
                    );
            }
            // Sub Query BalanceTotal
            var itemBalTot = new ItemBalanceQuery("ibt");
            itemBalTot.Select((itemBalTot.Balance.Sum().As("BalanceTotal")));
            itemBalTot.Where(itemBalTot.ItemID == query.ItemID);
            query.Select(itemBalTot.Select().As("BalanceTotal"));

            query.Where(query.TransactionNo == transactionNo);
            query.OrderBy(query.ItemID.Ascending);

            DataTable dtb = query.LoadDataTable();

            return dtb;
        }


        #region Email
        private static string POContent(ItemTransaction it, bool isWithBalance)
        {
            var porLocationID = string.Empty;
            if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPorByStockGroup) && !string.IsNullOrEmpty(it.ReferenceNo))
            {
                var por = new ItemTransaction();
                por.LoadByPrimaryKey(it.ReferenceNo);
                porLocationID = por.FromLocationID;
            }
            var sb = new StringBuilder();
            var dtb = PurchaseOrderItem(it.TransactionNo, it.SRItemType, porLocationID);

            sb.AppendLine(Healthcare.AddressComplete());
            sb.Append(System.Environment.NewLine);
            sb.Append(System.Environment.NewLine);
            sb.AppendFormat("PO No : {0}", it.TransactionNo);
            sb.Append(System.Environment.NewLine);
            sb.AppendFormat("Date : {0}", Convert.ToDateTime(it.TransactionDate).ToString(AppConstant.DisplayFormat.Date));
            sb.Append(System.Environment.NewLine);


            var su = new ServiceUnit();
            su.LoadByPrimaryKey(it.FromServiceUnitID);
            sb.AppendFormat("Purchasing Unit : {0}", su.ServiceUnitName);
            sb.Append(System.Environment.NewLine);

            if (!string.IsNullOrEmpty(it.ReferenceNo))
            {
                var req = new ItemTransaction();
                if (req.LoadByPrimaryKey(it.ReferenceNo))
                {
                    su = new ServiceUnit();
                    su.LoadByPrimaryKey(req.FromServiceUnitID);

                    sb.AppendFormat("Request Unit : {0}", su.ServiceUnitName);
                    sb.Append(System.Environment.NewLine);
                }
            }

            var sup = new Supplier();
            sup.LoadByPrimaryKey(it.BusinessPartnerID);
            sb.AppendFormat("Supplier : {0}", sup.SupplierName);
            sb.Append(System.Environment.NewLine);

            sb.AppendFormat("Note : {0}", it.Notes);
            sb.Append(System.Environment.NewLine);
            sb.AppendLine("Item :");
            var no = 1;
            foreach (DataRow row in dtb.Rows)
            {
                if (isWithBalance)
                {
                    if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPorByStockGroup) &&
                        !string.IsNullOrEmpty(it.ReferenceNo))
                        sb.AppendFormat("{4}\t{0}\t{1}\t{2:n0} {3}\t(Balance Loc: {5:n0} SG: {6:n0} Tot: {7:n0} {8}) ", row["ItemID"],
                            row["Description"], row["Quantity"], row["SRItemUnit"], no, row["Balance"], row["BalanceSG"], row["BalanceTotal"], row["SRMasterBaseUnit"]);

                    else
                        sb.AppendFormat("{4}\t{0}\t{1}\t{2:n0} {3}\t(Balance: {5:n0} {6}) ", row["ItemID"],
                            row["Description"], row["Quantity"], row["SRItemUnit"], no, row["Balance"], row["SRMasterBaseUnit"]);
                }
                else
                    sb.AppendFormat("{4}\t{0}\t{1}\t{2:n0} {3}", row["ItemID"], row["Description"], row["Quantity"],
                        row["SRItemUnit"], no);

                sb.Append(System.Environment.NewLine); //Change line
                no++;
            }

            return sb.ToString();
        }

        private static void EmailToSupplier(ItemTransaction it)
        {
            var sup = new Supplier();
            sup.LoadByPrimaryKey(it.BusinessPartnerID);
            if (string.IsNullOrEmpty(sup.Email))
                return;

            var healthcare = new Healthcare();

            var par = new AppParameter();
            par.LoadByPrimaryKey("HealthcareID");

            healthcare.LoadByPrimaryKey(par.ParameterValue);

            var address = Healthcare.AddressComplete(healthcare);

            var bodyEmail = new StringBuilder();

            bodyEmail.AppendLine(Healthcare.AddressComplete());
            bodyEmail.AppendLine(string.Empty);
            bodyEmail.Append(POContent(it, false));

            Mail.SendMailUseOtherThread(sup.Email, string.Format("PO No : {0}, {1}", it.TransactionNo, healthcare.HealthcareName), bodyEmail.ToString());

        }

        private static void EmailToPurchasingUnit(ItemTransaction it, int apprLevel)
        {
            var su = new ServiceUnit();
            su.LoadByPrimaryKey(it.FromServiceUnitID);

            if (string.IsNullOrEmpty(su.Email))
                return;

            var query = new ApprovalTransactionQuery("a");
            var userq = new AppUserQuery("b");
            query.InnerJoin(userq).On(query.UserID == userq.UserID);
            var userApprq = new AppUserQuery("c");
            query.LeftJoin(userApprq).On(query.ApprovedByUserID == userApprq.UserID);

            query.Where(query.TransactionNo == it.TransactionNo);
            query.OrderBy(query.ApprovalLevel.Ascending);
            query.Select(query.ApprovalLevel, query.ApprovedDate, query.IsApproved, userq.UserName, userApprq.UserName.As("ApprovalByUserName"));
            var dtbApprov = query.LoadDataTable();

            var sb = new StringBuilder();
            sb.AppendLine("Approval Progress:");
            foreach (DataRow row in dtbApprov.Rows)
            {
                sb.AppendFormat("{0} {1}  @ {2} by: {3}", row["ApprovalLevel"], row["UserName"], row["ApprovedDate"] == DBNull.Value ? "-" : Convert.ToDateTime(row["ApprovedDate"]).ToString(AppConstant.DisplayFormat.DateTime), row["ApprovalByUserName"]);
                sb.Append(System.Environment.NewLine);
            }
            sb.Append(System.Environment.NewLine);
            sb.AppendLine(POContent(it, true));
            var bodyEmail = sb.ToString();

            Mail.SendMailUseOtherThread(su.Email, string.Format("PO No : {0} has approved {1} of {2} by {3}", it.TransactionNo, apprLevel,
                        dtbApprov.Rows.Count, AppSession.UserLogin.UserName), bodyEmail);
        }

        private static void EmailToApproverNextLevel(ItemTransaction it, int currApprovalLevel)
        {
            var atq = new ApprovalTransactionQuery("at");
            atq.Where(atq.TransactionNo == it.TransactionNo && atq.ApprovalLevel > currApprovalLevel && atq.IsApproved == 0);
            atq.es.Top = 1;
            atq.OrderBy(atq.ApprovalLevel.Ascending);

            var at = new ApprovalTransaction();
            if (at.Load(atq))
            {
                // Email to the same level user
                atq = new ApprovalTransactionQuery("at");
                atq.Select(atq.UserID);
                atq.Where(atq.TransactionNo == it.TransactionNo && atq.ApprovalLevel == at.ApprovalLevel && atq.IsApproved == 0);
                atq.OrderBy(atq.ApprovalLevel.Ascending);
                var dtb = atq.LoadDataTable();
                foreach (DataRow row in dtb.Rows)
                {
                    EmailToApprover(it, row["UserID"].ToString());
                }
            }
        }

        private static void EmailToApprover(ItemTransaction it, string userID)
        {
            var usr = new AppUser();
            usr.LoadByPrimaryKey(userID);
            if (string.IsNullOrEmpty(usr.Email))
                return;

            Mail.SendMailUseOtherThread(usr.Email, string.Format("Please approve PO No : {0}", it.TransactionNo), POContent(it, true));
        }

        /// <summary>
        /// Email To Approver, dipanggil dari PO Entry
        /// </summary>
        /// <param name="entity"></param>
        public static void EmailToApprover(ItemTransaction entity)
        {
            if (entity == null) return;

            // Email to user
            if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsHigherApprovalLevelCanBypass))
            {
                // Kirim email ke semua user approver
                var atc = new ApprovalTransactionCollection();
                atc.Query.Where(atc.Query.TransactionNo == entity.TransactionNo);
                atc.LoadAll();
                foreach (ApprovalTransaction at in atc)
                {
                    EmailToApprover(entity, at.UserID);
                }
            }
            else
            {
                // Kirim email ke user approver level 1
                EmailToApproverNextLevel(entity, 0);
            }
        }

        #endregion
    }
}
