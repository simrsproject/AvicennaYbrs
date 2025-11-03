
﻿namespace Temiang.Avicenna.BusinessObject
 {
     public partial class ServiceUnitProductAccountMapping
     {
         public string ProductAccountName
         {
             get { return GetColumn("refToProductAccount_ProductAccountName").ToString(); }
             set { SetColumn("refToProductAccount_ProductAccountName", value); }
         }

         public string COARevenueName
         {
             get { return GetColumn("refToChartOfAccounts_COARevenueName").ToString(); }
             set { SetColumn("refToChartOfAccounts_COARevenueName", value); }
         }

         public string SubledgerRevenueName
         {
             get { return GetColumn("refToSubledgers_SubledgerRevenueName").ToString(); }
             set { SetColumn("refToSubledgers_SubledgerRevenueName", value); }
         }

         public string COADiscountName
         {
             get { return GetColumn("refToChartOfAccounts_COADiscountName").ToString(); }
             set { SetColumn("refToChartOfAccounts_COADiscountName", value); }
         }

         public string SubledgerDiscountName
         {
             get { return GetColumn("refToSubledgers_SubledgerDiscountName").ToString(); }
             set { SetColumn("refToSubledgers_SubledgerDiscountName", value); }
         }

         public string COACostName
         {
             get { return GetColumn("refToChartOfAccounts_COACostName").ToString(); }
             set { SetColumn("refToChartOfAccounts_COACostName", value); }
         }

         public string SubledgerCostName
         {
             get { return GetColumn("refToSubledgers_SubledgerCostName").ToString(); }
             set { SetColumn("refToSubledgers_SubledgerCostName", value); }
         }

         public string COAExpenseName
         {
             get { return GetColumn("refToChartOfAccounts_COAExpenseName").ToString(); }
             set { SetColumn("refToChartOfAccounts_COAExpenseName", value); }
         }

         public string SubledgerExpenseName
         {
             get { return GetColumn("refToSubledgers_SubledgerExpenseName").ToString(); }
             set { SetColumn("refToSubledgers_SubledgerExpenseName", value); }
         }

         public string COAAccrualName
         {
             get { return GetColumn("refToChartOfAccounts_COAAccrualName").ToString(); }
             set { SetColumn("refToChartOfAccounts_COAAccrualName", value); }
         }

         public string SubledgerAccrualName
         {
             get { return GetColumn("refToSubledgers_SubledgerAccrualName").ToString(); }
             set { SetColumn("refToSubledgers_SubledgerAccrualName", value); }
         }

         public string COAInventoryName
         {
             get { return GetColumn("refToChartOfAccounts_COAInventoryName").ToString(); }
             set { SetColumn("refToChartOfAccounts_COAInventoryName", value); }
         }

         public string SubledgerInventoryName
         {
             get { return GetColumn("refToSubledgers_SubledgerInventoryName").ToString(); }
             set { SetColumn("refToSubledgers_SubledgerInventoryName", value); }
         }
     }
 }

