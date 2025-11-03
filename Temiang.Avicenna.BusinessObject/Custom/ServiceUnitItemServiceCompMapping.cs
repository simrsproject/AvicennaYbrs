using System;
using System.Collections.Generic;
using Temiang.Dal.Interfaces;
using System.Data;
using Temiang.Dal.DynamicQuery;

﻿namespace Temiang.Avicenna.BusinessObject
 {
     public partial class ServiceUnitItemServiceCompMapping
     {
         public string TariffComponentName
         {
             get { return GetColumn("refToTariffComponent_TariffComponentName").ToString(); }
             set { SetColumn("refToTariffComponent_TariffComponentName", value); }
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

         public string RegistrationType
         {
             get { return GetColumn("refToAppStandardReferenceItem_RegistrationType").ToString(); }
             set { SetColumn("refToAppStandardReferenceItem_RegistrationType", value); }
         }

         public string GuarantorIncomeGroupName
         {
             get { return GetColumn("refToAppStandardReferenceItem_GuarantorIncomeGroup").ToString(); }
             set { SetColumn("refToAppStandardReferenceItem_GuarantorIncomeGroup", value); }
         }
     }

     public partial class ServiceUnitItemServiceCompMappingCollection
     {
         public DataTable GetMappingByPaymentNo(string PaymentNo)
         {
             string cmd = "sp_GetServiceUnitItemServiceCompMappingByPaymentNo";
             var pars = new esParameters();
             var pdParamedicID = new esParameter("PaymentNo", PaymentNo, esParameterDirection.Input, DbType.String, 15);
             pars.Add(pdParamedicID);

             return FillDataTable(esQueryType.StoredProcedure, cmd, pars);
         }
     }
 }

