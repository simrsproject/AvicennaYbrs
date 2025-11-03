/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/22/2021 12:45:36 PM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
	[Serializable]
	abstract public class esItemTransactionCollection : esEntityCollectionWAuditLog
	{
		public esItemTransactionCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ItemTransactionCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemTransactionQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			query.es2.Connection = ((IEntityCollection)this).Connection;
		}

		protected bool OnQueryLoaded(DataTable table)
		{
			this.PopulateCollection(table);
			return (this.RowCount > 0) ? true : false;
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery(query as esItemTransactionQuery);
		}
		#endregion

		virtual public ItemTransaction DetachEntity(ItemTransaction entity)
		{
			return base.DetachEntity(entity) as ItemTransaction;
		}

		virtual public ItemTransaction AttachEntity(ItemTransaction entity)
		{
			return base.AttachEntity(entity) as ItemTransaction;
		}

		virtual public void Combine(ItemTransactionCollection collection)
		{
			base.Combine(collection);
		}

		new public ItemTransaction this[int index]
		{
			get
			{
				return base[index] as ItemTransaction;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemTransaction);
		}
	}

	[Serializable]
	abstract public class esItemTransaction : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemTransactionQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemTransaction()
		{
		}

		public esItemTransaction(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
		}

		/// <summary>
		/// Loads an entity by primary key
		/// </summary>
		/// <remarks>
		/// Requires primary keys be defined on all tables.
		/// If a table does not have a primary key set,
		/// this method will not compile.
		/// </remarks>
		/// <param name="sqlAccessType">Either esSqlAccessType StoredProcedure or DynamicSQL</param>
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo)
		{
			esItemTransactionQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion

		#region Properties

		public override void SetProperties(IDictionary values)
		{
			foreach (string propertyName in values.Keys)
			{
				this.SetProperty(propertyName, values[propertyName]);
			}
		}

		public override void SetProperty(string name, object value)
		{
			if (this.Row == null) this.AddNew();

			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if (value == null || value is System.String)
				{
					// Use the strongly typed property
					switch (name)
					{
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "TransactionCode": this.str.TransactionCode = (string)value; break;
						case "TransactionDate": this.str.TransactionDate = (string)value; break;
						case "BusinessPartnerID": this.str.BusinessPartnerID = (string)value; break;
						case "InvoiceNo": this.str.InvoiceNo = (string)value; break;
						case "CurrencyID": this.str.CurrencyID = (string)value; break;
						case "CurrencyRate": this.str.CurrencyRate = (string)value; break;
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
						case "ReferenceDate": this.str.ReferenceDate = (string)value; break;
						case "FromServiceUnitID": this.str.FromServiceUnitID = (string)value; break;
						case "FromLocationID": this.str.FromLocationID = (string)value; break;
						case "ToServiceUnitID": this.str.ToServiceUnitID = (string)value; break;
						case "ToLocationID": this.str.ToLocationID = (string)value; break;
						case "TermID": this.str.TermID = (string)value; break;
						case "SRItemType": this.str.SRItemType = (string)value; break;
						case "DiscountAmount": this.str.DiscountAmount = (string)value; break;
						case "ChargesAmount": this.str.ChargesAmount = (string)value; break;
						case "StampAmount": this.str.StampAmount = (string)value; break;
						case "DownPaymentAmount": this.str.DownPaymentAmount = (string)value; break;
						case "DownPaymentReferenceNo": this.str.DownPaymentReferenceNo = (string)value; break;
						case "SRDownPaymentType": this.str.SRDownPaymentType = (string)value; break;
						case "SRAdjustmentType": this.str.SRAdjustmentType = (string)value; break;
						case "SRDistributionType": this.str.SRDistributionType = (string)value; break;
						case "SRPurchaseReturnType": this.str.SRPurchaseReturnType = (string)value; break;
						case "SRPurchaseOrderType": this.str.SRPurchaseOrderType = (string)value; break;
						case "TaxPercentage": this.str.TaxPercentage = (string)value; break;
						case "TaxAmount": this.str.TaxAmount = (string)value; break;
						case "IsTaxable": this.str.IsTaxable = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDate": this.str.VoidDate = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDate": this.str.ApprovedDate = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsClosed": this.str.IsClosed = (string)value; break;
						case "IsBySystem": this.str.IsBySystem = (string)value; break;
						case "IsNonMasterOrder": this.str.IsNonMasterOrder = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "ProductAccountID": this.str.ProductAccountID = (string)value; break;
						case "LeadTime": this.str.LeadTime = (string)value; break;
						case "Pph22": this.str.Pph22 = (string)value; break;
						case "Pph23": this.str.Pph23 = (string)value; break;
						case "ContractNo": this.str.ContractNo = (string)value; break;
						case "PriorChargesAmount": this.str.PriorChargesAmount = (string)value; break;
						case "PriorTaxAmount": this.str.PriorTaxAmount = (string)value; break;
						case "CustomerID": this.str.CustomerID = (string)value; break;
						case "TaxInvoiceNo": this.str.TaxInvoiceNo = (string)value; break;
						case "TaxInvoiceDate": this.str.TaxInvoiceDate = (string)value; break;
						case "SRPaymentType": this.str.SRPaymentType = (string)value; break;
						case "SRPurchaseCategorization": this.str.SRPurchaseCategorization = (string)value; break;
						case "IsInventoryItem": this.str.IsInventoryItem = (string)value; break;
						case "TermOfPayment": this.str.TermOfPayment = (string)value; break;
						case "IsAssets": this.str.IsAssets = (string)value; break;
						case "BudgetPlanCounter": this.str.BudgetPlanCounter = (string)value; break;
						case "ServiceUnitCostID": this.str.ServiceUnitCostID = (string)value; break;
						case "IsPph22InPercent": this.str.IsPph22InPercent = (string)value; break;
						case "Pph22Percentage": this.str.Pph22Percentage = (string)value; break;
						case "IsPph23InPercent": this.str.IsPph23InPercent = (string)value; break;
						case "Pph23Percentage": this.str.Pph23Percentage = (string)value; break;
						case "IsConsignment": this.str.IsConsignment = (string)value; break;
						case "AmountTaxed": this.str.AmountTaxed = (string)value; break;
						case "RevisionNumber": this.str.RevisionNumber = (string)value; break;
						case "PrintNumber": this.str.PrintNumber = (string)value; break;
						case "LastPrintedDateTime": this.str.LastPrintedDateTime = (string)value; break;
						case "LastPrintedByUserID": this.str.LastPrintedByUserID = (string)value; break;
						case "AdvanceAmount": this.str.AdvanceAmount = (string)value; break;
						case "DeliveryOrdersNo": this.str.DeliveryOrdersNo = (string)value; break;
						case "DeliveryOrdersDate": this.str.DeliveryOrdersDate = (string)value; break;
						case "PphAmount": this.str.PphAmount = (string)value; break;
						case "InvoiceSupplierDate": this.str.InvoiceSupplierDate = (string)value; break;
						case "CashTransactionReconcileId": this.str.CashTransactionReconcileId = (string)value; break;
						case "ItemGroupID": this.str.ItemGroupID = (string)value; break;
						case "SRPph": this.str.SRPph = (string)value; break;
						case "PphPercentage": this.str.PphPercentage = (string)value; break;
						case "IsConsignmentAlreadyReceived": this.str.IsConsignmentAlreadyReceived = (string)value; break;
						case "PlanningDate": this.str.PlanningDate = (string)value; break;
						case "IsInstallmentType": this.str.IsInstallmentType = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "SRItemCategory": this.str.SRItemCategory = (string)value; break;
						case "SRProcurementType": this.str.SRProcurementType = (string)value; break;
						case "ContractDate": this.str.ContractDate = (string)value; break;
						case "CheckNo": this.str.CheckNo = (string)value; break;
						case "CheckDate": this.str.CheckDate = (string)value; break;
						case "SalesMarginPercentage": this.str.SalesMarginPercentage = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "TransactionDate":

							if (value == null || value is System.DateTime)
								this.TransactionDate = (System.DateTime?)value;
							break;
						case "CurrencyRate":

							if (value == null || value is System.Decimal)
								this.CurrencyRate = (System.Decimal?)value;
							break;
						case "ReferenceDate":

							if (value == null || value is System.DateTime)
								this.ReferenceDate = (System.DateTime?)value;
							break;
						case "DiscountAmount":

							if (value == null || value is System.Decimal)
								this.DiscountAmount = (System.Decimal?)value;
							break;
						case "ChargesAmount":

							if (value == null || value is System.Decimal)
								this.ChargesAmount = (System.Decimal?)value;
							break;
						case "StampAmount":

							if (value == null || value is System.Decimal)
								this.StampAmount = (System.Decimal?)value;
							break;
						case "DownPaymentAmount":

							if (value == null || value is System.Decimal)
								this.DownPaymentAmount = (System.Decimal?)value;
							break;
						case "TaxPercentage":

							if (value == null || value is System.Decimal)
								this.TaxPercentage = (System.Decimal?)value;
							break;
						case "TaxAmount":

							if (value == null || value is System.Decimal)
								this.TaxAmount = (System.Decimal?)value;
							break;
						case "IsTaxable":

							if (value == null || value is System.Int16)
								this.IsTaxable = (System.Int16?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "VoidDate":

							if (value == null || value is System.DateTime)
								this.VoidDate = (System.DateTime?)value;
							break;
						case "IsApproved":

							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "ApprovedDate":

							if (value == null || value is System.DateTime)
								this.ApprovedDate = (System.DateTime?)value;
							break;
						case "IsClosed":

							if (value == null || value is System.Boolean)
								this.IsClosed = (System.Boolean?)value;
							break;
						case "IsBySystem":

							if (value == null || value is System.Boolean)
								this.IsBySystem = (System.Boolean?)value;
							break;
						case "IsNonMasterOrder":

							if (value == null || value is System.Boolean)
								this.IsNonMasterOrder = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "Pph22":

							if (value == null || value is System.Decimal)
								this.Pph22 = (System.Decimal?)value;
							break;
						case "Pph23":

							if (value == null || value is System.Decimal)
								this.Pph23 = (System.Decimal?)value;
							break;
						case "PriorChargesAmount":

							if (value == null || value is System.Decimal)
								this.PriorChargesAmount = (System.Decimal?)value;
							break;
						case "PriorTaxAmount":

							if (value == null || value is System.Decimal)
								this.PriorTaxAmount = (System.Decimal?)value;
							break;
						case "TaxInvoiceDate":

							if (value == null || value is System.DateTime)
								this.TaxInvoiceDate = (System.DateTime?)value;
							break;
						case "IsInventoryItem":

							if (value == null || value is System.Boolean)
								this.IsInventoryItem = (System.Boolean?)value;
							break;
						case "TermOfPayment":

							if (value == null || value is System.Decimal)
								this.TermOfPayment = (System.Decimal?)value;
							break;
						case "IsAssets":

							if (value == null || value is System.Boolean)
								this.IsAssets = (System.Boolean?)value;
							break;
						case "BudgetPlanCounter":

							if (value == null || value is System.Int32)
								this.BudgetPlanCounter = (System.Int32?)value;
							break;
						case "IsPph22InPercent":

							if (value == null || value is System.Boolean)
								this.IsPph22InPercent = (System.Boolean?)value;
							break;
						case "Pph22Percentage":

							if (value == null || value is System.Decimal)
								this.Pph22Percentage = (System.Decimal?)value;
							break;
						case "IsPph23InPercent":

							if (value == null || value is System.Boolean)
								this.IsPph23InPercent = (System.Boolean?)value;
							break;
						case "Pph23Percentage":

							if (value == null || value is System.Decimal)
								this.Pph23Percentage = (System.Decimal?)value;
							break;
						case "IsConsignment":

							if (value == null || value is System.Boolean)
								this.IsConsignment = (System.Boolean?)value;
							break;
						case "AmountTaxed":

							if (value == null || value is System.Decimal)
								this.AmountTaxed = (System.Decimal?)value;
							break;
						case "RevisionNumber":

							if (value == null || value is System.Int16)
								this.RevisionNumber = (System.Int16?)value;
							break;
						case "PrintNumber":

							if (value == null || value is System.Int16)
								this.PrintNumber = (System.Int16?)value;
							break;
						case "LastPrintedDateTime":

							if (value == null || value is System.DateTime)
								this.LastPrintedDateTime = (System.DateTime?)value;
							break;
						case "AdvanceAmount":

							if (value == null || value is System.Decimal)
								this.AdvanceAmount = (System.Decimal?)value;
							break;
						case "DeliveryOrdersDate":

							if (value == null || value is System.DateTime)
								this.DeliveryOrdersDate = (System.DateTime?)value;
							break;
						case "PphAmount":

							if (value == null || value is System.Decimal)
								this.PphAmount = (System.Decimal?)value;
							break;
						case "InvoiceSupplierDate":

							if (value == null || value is System.DateTime)
								this.InvoiceSupplierDate = (System.DateTime?)value;
							break;
						case "PphPercentage":

							if (value == null || value is System.Decimal)
								this.PphPercentage = (System.Decimal?)value;
							break;
						case "IsConsignmentAlreadyReceived":

							if (value == null || value is System.Boolean)
								this.IsConsignmentAlreadyReceived = (System.Boolean?)value;
							break;
						case "PlanningDate":

							if (value == null || value is System.DateTime)
								this.PlanningDate = (System.DateTime?)value;
							break;
						case "IsInstallmentType":

							if (value == null || value is System.Boolean)
								this.IsInstallmentType = (System.Boolean?)value;
							break;
						case "CreateDateTime":

							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "ContractDate":

							if (value == null || value is System.DateTime)
								this.ContractDate = (System.DateTime?)value;
							break;
						case "CheckDate":

							if (value == null || value is System.DateTime)
								this.CheckDate = (System.DateTime?)value;
							break;
						case "SalesMarginPercentage":

							if (value == null || value is System.Decimal)
								this.SalesMarginPercentage = (System.Decimal?)value;
							break;

						default:
							break;
					}
				}
			}
			else if (this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}

		/// <summary>
		/// Maps to ItemTransaction.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.TransactionCode
		/// </summary>
		virtual public System.String TransactionCode
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.TransactionCode);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.TransactionCode, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTransactionMetadata.ColumnNames.TransactionDate);
			}

			set
			{
				base.SetSystemDateTime(ItemTransactionMetadata.ColumnNames.TransactionDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.BusinessPartnerID
		/// </summary>
		virtual public System.String BusinessPartnerID
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.BusinessPartnerID);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.BusinessPartnerID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.InvoiceNo
		/// </summary>
		virtual public System.String InvoiceNo
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.InvoiceNo);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.InvoiceNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.CurrencyID
		/// </summary>
		virtual public System.String CurrencyID
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.CurrencyID);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.CurrencyID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.CurrencyRate
		/// </summary>
		virtual public System.Decimal? CurrencyRate
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionMetadata.ColumnNames.CurrencyRate);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionMetadata.ColumnNames.CurrencyRate, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.ReferenceNo);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.ReferenceDate
		/// </summary>
		virtual public System.DateTime? ReferenceDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTransactionMetadata.ColumnNames.ReferenceDate);
			}

			set
			{
				base.SetSystemDateTime(ItemTransactionMetadata.ColumnNames.ReferenceDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.FromServiceUnitID
		/// </summary>
		virtual public System.String FromServiceUnitID
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.FromServiceUnitID);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.FromServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.FromLocationID
		/// </summary>
		virtual public System.String FromLocationID
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.FromLocationID);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.FromLocationID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.ToServiceUnitID
		/// </summary>
		virtual public System.String ToServiceUnitID
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.ToServiceUnitID);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.ToServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.ToLocationID
		/// </summary>
		virtual public System.String ToLocationID
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.ToLocationID);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.ToLocationID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.TermID
		/// </summary>
		virtual public System.String TermID
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.TermID);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.TermID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.SRItemType
		/// </summary>
		virtual public System.String SRItemType
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.SRItemType);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.SRItemType, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.DiscountAmount
		/// </summary>
		virtual public System.Decimal? DiscountAmount
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionMetadata.ColumnNames.DiscountAmount);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionMetadata.ColumnNames.DiscountAmount, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.ChargesAmount
		/// </summary>
		virtual public System.Decimal? ChargesAmount
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionMetadata.ColumnNames.ChargesAmount);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionMetadata.ColumnNames.ChargesAmount, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.StampAmount
		/// </summary>
		virtual public System.Decimal? StampAmount
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionMetadata.ColumnNames.StampAmount);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionMetadata.ColumnNames.StampAmount, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.DownPaymentAmount
		/// </summary>
		virtual public System.Decimal? DownPaymentAmount
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionMetadata.ColumnNames.DownPaymentAmount);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionMetadata.ColumnNames.DownPaymentAmount, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.DownPaymentReferenceNo
		/// </summary>
		virtual public System.String DownPaymentReferenceNo
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.DownPaymentReferenceNo);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.DownPaymentReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.SRDownPaymentType
		/// </summary>
		virtual public System.String SRDownPaymentType
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.SRDownPaymentType);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.SRDownPaymentType, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.SRAdjustmentType
		/// </summary>
		virtual public System.String SRAdjustmentType
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.SRAdjustmentType);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.SRAdjustmentType, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.SRDistributionType
		/// </summary>
		virtual public System.String SRDistributionType
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.SRDistributionType);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.SRDistributionType, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.SRPurchaseReturnType
		/// </summary>
		virtual public System.String SRPurchaseReturnType
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.SRPurchaseReturnType);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.SRPurchaseReturnType, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.SRPurchaseOrderType
		/// </summary>
		virtual public System.String SRPurchaseOrderType
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.SRPurchaseOrderType);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.SRPurchaseOrderType, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.TaxPercentage
		/// </summary>
		virtual public System.Decimal? TaxPercentage
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionMetadata.ColumnNames.TaxPercentage);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionMetadata.ColumnNames.TaxPercentage, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.TaxAmount
		/// </summary>
		virtual public System.Decimal? TaxAmount
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionMetadata.ColumnNames.TaxAmount);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionMetadata.ColumnNames.TaxAmount, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.IsTaxable
		/// </summary>
		virtual public System.Int16? IsTaxable
		{
			get
			{
				return base.GetSystemInt16(ItemTransactionMetadata.ColumnNames.IsTaxable);
			}

			set
			{
				base.SetSystemInt16(ItemTransactionMetadata.ColumnNames.IsTaxable, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(ItemTransactionMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.VoidDate
		/// </summary>
		virtual public System.DateTime? VoidDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTransactionMetadata.ColumnNames.VoidDate);
			}

			set
			{
				base.SetSystemDateTime(ItemTransactionMetadata.ColumnNames.VoidDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(ItemTransactionMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.ApprovedDate
		/// </summary>
		virtual public System.DateTime? ApprovedDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTransactionMetadata.ColumnNames.ApprovedDate);
			}

			set
			{
				base.SetSystemDateTime(ItemTransactionMetadata.ColumnNames.ApprovedDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.IsClosed
		/// </summary>
		virtual public System.Boolean? IsClosed
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionMetadata.ColumnNames.IsClosed);
			}

			set
			{
				base.SetSystemBoolean(ItemTransactionMetadata.ColumnNames.IsClosed, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.IsBySystem
		/// </summary>
		virtual public System.Boolean? IsBySystem
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionMetadata.ColumnNames.IsBySystem);
			}

			set
			{
				base.SetSystemBoolean(ItemTransactionMetadata.ColumnNames.IsBySystem, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.IsNonMasterOrder
		/// </summary>
		virtual public System.Boolean? IsNonMasterOrder
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionMetadata.ColumnNames.IsNonMasterOrder);
			}

			set
			{
				base.SetSystemBoolean(ItemTransactionMetadata.ColumnNames.IsNonMasterOrder, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemTransactionMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ItemTransactionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.ProductAccountID
		/// </summary>
		virtual public System.String ProductAccountID
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.ProductAccountID);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.ProductAccountID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.LeadTime
		/// </summary>
		virtual public System.String LeadTime
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.LeadTime);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.LeadTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.Pph22
		/// </summary>
		virtual public System.Decimal? Pph22
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionMetadata.ColumnNames.Pph22);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionMetadata.ColumnNames.Pph22, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.Pph23
		/// </summary>
		virtual public System.Decimal? Pph23
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionMetadata.ColumnNames.Pph23);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionMetadata.ColumnNames.Pph23, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.ContractNo
		/// </summary>
		virtual public System.String ContractNo
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.ContractNo);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.ContractNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.PriorChargesAmount
		/// </summary>
		virtual public System.Decimal? PriorChargesAmount
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionMetadata.ColumnNames.PriorChargesAmount);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionMetadata.ColumnNames.PriorChargesAmount, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.PriorTaxAmount
		/// </summary>
		virtual public System.Decimal? PriorTaxAmount
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionMetadata.ColumnNames.PriorTaxAmount);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionMetadata.ColumnNames.PriorTaxAmount, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.CustomerID
		/// </summary>
		virtual public System.String CustomerID
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.CustomerID);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.CustomerID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.TaxInvoiceNo
		/// </summary>
		virtual public System.String TaxInvoiceNo
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.TaxInvoiceNo);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.TaxInvoiceNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.TaxInvoiceDate
		/// </summary>
		virtual public System.DateTime? TaxInvoiceDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTransactionMetadata.ColumnNames.TaxInvoiceDate);
			}

			set
			{
				base.SetSystemDateTime(ItemTransactionMetadata.ColumnNames.TaxInvoiceDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.SRPaymentType
		/// </summary>
		virtual public System.String SRPaymentType
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.SRPaymentType);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.SRPaymentType, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.SRPurchaseCategorization
		/// </summary>
		virtual public System.String SRPurchaseCategorization
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.SRPurchaseCategorization);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.SRPurchaseCategorization, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.IsInventoryItem
		/// </summary>
		virtual public System.Boolean? IsInventoryItem
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionMetadata.ColumnNames.IsInventoryItem);
			}

			set
			{
				base.SetSystemBoolean(ItemTransactionMetadata.ColumnNames.IsInventoryItem, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.TermOfPayment
		/// </summary>
		virtual public System.Decimal? TermOfPayment
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionMetadata.ColumnNames.TermOfPayment);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionMetadata.ColumnNames.TermOfPayment, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.IsAssets
		/// </summary>
		virtual public System.Boolean? IsAssets
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionMetadata.ColumnNames.IsAssets);
			}

			set
			{
				base.SetSystemBoolean(ItemTransactionMetadata.ColumnNames.IsAssets, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.BudgetPlanCounter
		/// </summary>
		virtual public System.Int32? BudgetPlanCounter
		{
			get
			{
				return base.GetSystemInt32(ItemTransactionMetadata.ColumnNames.BudgetPlanCounter);
			}

			set
			{
				base.SetSystemInt32(ItemTransactionMetadata.ColumnNames.BudgetPlanCounter, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.ServiceUnitCostID
		/// </summary>
		virtual public System.String ServiceUnitCostID
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.ServiceUnitCostID);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.ServiceUnitCostID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.IsPph22InPercent
		/// </summary>
		virtual public System.Boolean? IsPph22InPercent
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionMetadata.ColumnNames.IsPph22InPercent);
			}

			set
			{
				base.SetSystemBoolean(ItemTransactionMetadata.ColumnNames.IsPph22InPercent, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.Pph22Percentage
		/// </summary>
		virtual public System.Decimal? Pph22Percentage
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionMetadata.ColumnNames.Pph22Percentage);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionMetadata.ColumnNames.Pph22Percentage, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.IsPph23InPercent
		/// </summary>
		virtual public System.Boolean? IsPph23InPercent
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionMetadata.ColumnNames.IsPph23InPercent);
			}

			set
			{
				base.SetSystemBoolean(ItemTransactionMetadata.ColumnNames.IsPph23InPercent, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.Pph23Percentage
		/// </summary>
		virtual public System.Decimal? Pph23Percentage
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionMetadata.ColumnNames.Pph23Percentage);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionMetadata.ColumnNames.Pph23Percentage, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.IsConsignment
		/// </summary>
		virtual public System.Boolean? IsConsignment
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionMetadata.ColumnNames.IsConsignment);
			}

			set
			{
				base.SetSystemBoolean(ItemTransactionMetadata.ColumnNames.IsConsignment, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.AmountTaxed
		/// </summary>
		virtual public System.Decimal? AmountTaxed
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionMetadata.ColumnNames.AmountTaxed);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionMetadata.ColumnNames.AmountTaxed, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.RevisionNumber
		/// </summary>
		virtual public System.Int16? RevisionNumber
		{
			get
			{
				return base.GetSystemInt16(ItemTransactionMetadata.ColumnNames.RevisionNumber);
			}

			set
			{
				base.SetSystemInt16(ItemTransactionMetadata.ColumnNames.RevisionNumber, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.PrintNumber
		/// </summary>
		virtual public System.Int16? PrintNumber
		{
			get
			{
				return base.GetSystemInt16(ItemTransactionMetadata.ColumnNames.PrintNumber);
			}

			set
			{
				base.SetSystemInt16(ItemTransactionMetadata.ColumnNames.PrintNumber, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.LastPrintedDateTime
		/// </summary>
		virtual public System.DateTime? LastPrintedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemTransactionMetadata.ColumnNames.LastPrintedDateTime);
			}

			set
			{
				base.SetSystemDateTime(ItemTransactionMetadata.ColumnNames.LastPrintedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.LastPrintedByUserID
		/// </summary>
		virtual public System.String LastPrintedByUserID
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.LastPrintedByUserID);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.LastPrintedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.AdvanceAmount
		/// </summary>
		virtual public System.Decimal? AdvanceAmount
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionMetadata.ColumnNames.AdvanceAmount);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionMetadata.ColumnNames.AdvanceAmount, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.DeliveryOrdersNo
		/// </summary>
		virtual public System.String DeliveryOrdersNo
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.DeliveryOrdersNo);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.DeliveryOrdersNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.DeliveryOrdersDate
		/// </summary>
		virtual public System.DateTime? DeliveryOrdersDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTransactionMetadata.ColumnNames.DeliveryOrdersDate);
			}

			set
			{
				base.SetSystemDateTime(ItemTransactionMetadata.ColumnNames.DeliveryOrdersDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.PphAmount
		/// </summary>
		virtual public System.Decimal? PphAmount
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionMetadata.ColumnNames.PphAmount);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionMetadata.ColumnNames.PphAmount, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.InvoiceSupplierDate
		/// </summary>
		virtual public System.DateTime? InvoiceSupplierDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTransactionMetadata.ColumnNames.InvoiceSupplierDate);
			}

			set
			{
				base.SetSystemDateTime(ItemTransactionMetadata.ColumnNames.InvoiceSupplierDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.CashTransactionReconcileId
		/// </summary>
		virtual public System.String CashTransactionReconcileId
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.CashTransactionReconcileId);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.CashTransactionReconcileId, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.ItemGroupID
		/// </summary>
		virtual public System.String ItemGroupID
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.ItemGroupID);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.ItemGroupID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.SRPph
		/// </summary>
		virtual public System.String SRPph
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.SRPph);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.SRPph, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.PphPercentage
		/// </summary>
		virtual public System.Decimal? PphPercentage
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionMetadata.ColumnNames.PphPercentage);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionMetadata.ColumnNames.PphPercentage, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.IsConsignmentAlreadyReceived
		/// </summary>
		virtual public System.Boolean? IsConsignmentAlreadyReceived
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionMetadata.ColumnNames.IsConsignmentAlreadyReceived);
			}

			set
			{
				base.SetSystemBoolean(ItemTransactionMetadata.ColumnNames.IsConsignmentAlreadyReceived, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.PlanningDate
		/// </summary>
		virtual public System.DateTime? PlanningDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTransactionMetadata.ColumnNames.PlanningDate);
			}

			set
			{
				base.SetSystemDateTime(ItemTransactionMetadata.ColumnNames.PlanningDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.IsInstallmentType
		/// </summary>
		virtual public System.Boolean? IsInstallmentType
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionMetadata.ColumnNames.IsInstallmentType);
			}

			set
			{
				base.SetSystemBoolean(ItemTransactionMetadata.ColumnNames.IsInstallmentType, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemTransactionMetadata.ColumnNames.CreateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ItemTransactionMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.CreateByUserID);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.SRItemCategory
		/// </summary>
		virtual public System.String SRItemCategory
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.SRItemCategory);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.SRItemCategory, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.SRProcurementType
		/// </summary>
		virtual public System.String SRProcurementType
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.SRProcurementType);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.SRProcurementType, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.ContractDate
		/// </summary>
		virtual public System.DateTime? ContractDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTransactionMetadata.ColumnNames.ContractDate);
			}

			set
			{
				base.SetSystemDateTime(ItemTransactionMetadata.ColumnNames.ContractDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.CheckNo
		/// </summary>
		virtual public System.String CheckNo
		{
			get
			{
				return base.GetSystemString(ItemTransactionMetadata.ColumnNames.CheckNo);
			}

			set
			{
				base.SetSystemString(ItemTransactionMetadata.ColumnNames.CheckNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.CheckDate
		/// </summary>
		virtual public System.DateTime? CheckDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTransactionMetadata.ColumnNames.CheckDate);
			}

			set
			{
				base.SetSystemDateTime(ItemTransactionMetadata.ColumnNames.CheckDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransaction.SalesMarginPercentage
		/// </summary>
		virtual public System.Decimal? SalesMarginPercentage
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionMetadata.ColumnNames.SalesMarginPercentage);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionMetadata.ColumnNames.SalesMarginPercentage, value);
			}
		}

		#endregion

		#region String Properties

		/// <summary>
		/// Converts an entity's properties to
		/// and from strings.
		/// </summary>
		/// <remarks>
		/// The str properties Get and Set provide easy conversion
		/// between a string and a property's data type. Not all
		/// data types will get a str property.
		/// </remarks>
		/// <example>
		/// Set a datetime from a string.
		/// <code>
		/// Employees entity = new Employees();
		/// entity.LoadByPrimaryKey(10);
		/// entity.str.HireDate = "2007-01-01 00:00:00";
		/// entity.Save();
		/// </code>
		/// Get a datetime as a string.
		/// <code>
		/// Employees entity = new Employees();
		/// entity.LoadByPrimaryKey(10);
		/// string theDate = entity.str.HireDate;
		/// </code>
		/// </example>
		[BrowsableAttribute(false)]
		public esStrings str
		{
			get
			{
				if (esstrings == null)
				{
					esstrings = new esStrings(this);
				}
				return esstrings;
			}
		}

		[Serializable]
		sealed public class esStrings
		{
			public esStrings(esItemTransaction entity)
			{
				this.entity = entity;
			}
			public System.String TransactionNo
			{
				get
				{
					System.String data = entity.TransactionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNo = null;
					else entity.TransactionNo = Convert.ToString(value);
				}
			}
			public System.String TransactionCode
			{
				get
				{
					System.String data = entity.TransactionCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionCode = null;
					else entity.TransactionCode = Convert.ToString(value);
				}
			}
			public System.String TransactionDate
			{
				get
				{
					System.DateTime? data = entity.TransactionDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionDate = null;
					else entity.TransactionDate = Convert.ToDateTime(value);
				}
			}
			public System.String BusinessPartnerID
			{
				get
				{
					System.String data = entity.BusinessPartnerID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BusinessPartnerID = null;
					else entity.BusinessPartnerID = Convert.ToString(value);
				}
			}
			public System.String InvoiceNo
			{
				get
				{
					System.String data = entity.InvoiceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InvoiceNo = null;
					else entity.InvoiceNo = Convert.ToString(value);
				}
			}
			public System.String CurrencyID
			{
				get
				{
					System.String data = entity.CurrencyID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CurrencyID = null;
					else entity.CurrencyID = Convert.ToString(value);
				}
			}
			public System.String CurrencyRate
			{
				get
				{
					System.Decimal? data = entity.CurrencyRate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CurrencyRate = null;
					else entity.CurrencyRate = Convert.ToDecimal(value);
				}
			}
			public System.String ReferenceNo
			{
				get
				{
					System.String data = entity.ReferenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceNo = null;
					else entity.ReferenceNo = Convert.ToString(value);
				}
			}
			public System.String ReferenceDate
			{
				get
				{
					System.DateTime? data = entity.ReferenceDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceDate = null;
					else entity.ReferenceDate = Convert.ToDateTime(value);
				}
			}
			public System.String FromServiceUnitID
			{
				get
				{
					System.String data = entity.FromServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromServiceUnitID = null;
					else entity.FromServiceUnitID = Convert.ToString(value);
				}
			}
			public System.String FromLocationID
			{
				get
				{
					System.String data = entity.FromLocationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromLocationID = null;
					else entity.FromLocationID = Convert.ToString(value);
				}
			}
			public System.String ToServiceUnitID
			{
				get
				{
					System.String data = entity.ToServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToServiceUnitID = null;
					else entity.ToServiceUnitID = Convert.ToString(value);
				}
			}
			public System.String ToLocationID
			{
				get
				{
					System.String data = entity.ToLocationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToLocationID = null;
					else entity.ToLocationID = Convert.ToString(value);
				}
			}
			public System.String TermID
			{
				get
				{
					System.String data = entity.TermID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TermID = null;
					else entity.TermID = Convert.ToString(value);
				}
			}
			public System.String SRItemType
			{
				get
				{
					System.String data = entity.SRItemType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRItemType = null;
					else entity.SRItemType = Convert.ToString(value);
				}
			}
			public System.String DiscountAmount
			{
				get
				{
					System.Decimal? data = entity.DiscountAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiscountAmount = null;
					else entity.DiscountAmount = Convert.ToDecimal(value);
				}
			}
			public System.String ChargesAmount
			{
				get
				{
					System.Decimal? data = entity.ChargesAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChargesAmount = null;
					else entity.ChargesAmount = Convert.ToDecimal(value);
				}
			}
			public System.String StampAmount
			{
				get
				{
					System.Decimal? data = entity.StampAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StampAmount = null;
					else entity.StampAmount = Convert.ToDecimal(value);
				}
			}
			public System.String DownPaymentAmount
			{
				get
				{
					System.Decimal? data = entity.DownPaymentAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DownPaymentAmount = null;
					else entity.DownPaymentAmount = Convert.ToDecimal(value);
				}
			}
			public System.String DownPaymentReferenceNo
			{
				get
				{
					System.String data = entity.DownPaymentReferenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DownPaymentReferenceNo = null;
					else entity.DownPaymentReferenceNo = Convert.ToString(value);
				}
			}
			public System.String SRDownPaymentType
			{
				get
				{
					System.String data = entity.SRDownPaymentType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDownPaymentType = null;
					else entity.SRDownPaymentType = Convert.ToString(value);
				}
			}
			public System.String SRAdjustmentType
			{
				get
				{
					System.String data = entity.SRAdjustmentType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAdjustmentType = null;
					else entity.SRAdjustmentType = Convert.ToString(value);
				}
			}
			public System.String SRDistributionType
			{
				get
				{
					System.String data = entity.SRDistributionType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDistributionType = null;
					else entity.SRDistributionType = Convert.ToString(value);
				}
			}
			public System.String SRPurchaseReturnType
			{
				get
				{
					System.String data = entity.SRPurchaseReturnType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPurchaseReturnType = null;
					else entity.SRPurchaseReturnType = Convert.ToString(value);
				}
			}
			public System.String SRPurchaseOrderType
			{
				get
				{
					System.String data = entity.SRPurchaseOrderType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPurchaseOrderType = null;
					else entity.SRPurchaseOrderType = Convert.ToString(value);
				}
			}
			public System.String TaxPercentage
			{
				get
				{
					System.Decimal? data = entity.TaxPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TaxPercentage = null;
					else entity.TaxPercentage = Convert.ToDecimal(value);
				}
			}
			public System.String TaxAmount
			{
				get
				{
					System.Decimal? data = entity.TaxAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TaxAmount = null;
					else entity.TaxAmount = Convert.ToDecimal(value);
				}
			}
			public System.String IsTaxable
			{
				get
				{
					System.Int16? data = entity.IsTaxable;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTaxable = null;
					else entity.IsTaxable = Convert.ToInt16(value);
				}
			}
			public System.String IsVoid
			{
				get
				{
					System.Boolean? data = entity.IsVoid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVoid = null;
					else entity.IsVoid = Convert.ToBoolean(value);
				}
			}
			public System.String VoidDate
			{
				get
				{
					System.DateTime? data = entity.VoidDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidDate = null;
					else entity.VoidDate = Convert.ToDateTime(value);
				}
			}
			public System.String VoidByUserID
			{
				get
				{
					System.String data = entity.VoidByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidByUserID = null;
					else entity.VoidByUserID = Convert.ToString(value);
				}
			}
			public System.String IsApproved
			{
				get
				{
					System.Boolean? data = entity.IsApproved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApproved = null;
					else entity.IsApproved = Convert.ToBoolean(value);
				}
			}
			public System.String ApprovedDate
			{
				get
				{
					System.DateTime? data = entity.ApprovedDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedDate = null;
					else entity.ApprovedDate = Convert.ToDateTime(value);
				}
			}
			public System.String ApprovedByUserID
			{
				get
				{
					System.String data = entity.ApprovedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedByUserID = null;
					else entity.ApprovedByUserID = Convert.ToString(value);
				}
			}
			public System.String IsClosed
			{
				get
				{
					System.Boolean? data = entity.IsClosed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsClosed = null;
					else entity.IsClosed = Convert.ToBoolean(value);
				}
			}
			public System.String IsBySystem
			{
				get
				{
					System.Boolean? data = entity.IsBySystem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBySystem = null;
					else entity.IsBySystem = Convert.ToBoolean(value);
				}
			}
			public System.String IsNonMasterOrder
			{
				get
				{
					System.Boolean? data = entity.IsNonMasterOrder;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNonMasterOrder = null;
					else entity.IsNonMasterOrder = Convert.ToBoolean(value);
				}
			}
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
				}
			}
			public System.String LastUpdateDateTime
			{
				get
				{
					System.DateTime? data = entity.LastUpdateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastUpdateDateTime = null;
					else entity.LastUpdateDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String LastUpdateByUserID
			{
				get
				{
					System.String data = entity.LastUpdateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastUpdateByUserID = null;
					else entity.LastUpdateByUserID = Convert.ToString(value);
				}
			}
			public System.String ProductAccountID
			{
				get
				{
					System.String data = entity.ProductAccountID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProductAccountID = null;
					else entity.ProductAccountID = Convert.ToString(value);
				}
			}
			public System.String LeadTime
			{
				get
				{
					System.String data = entity.LeadTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LeadTime = null;
					else entity.LeadTime = Convert.ToString(value);
				}
			}
			public System.String Pph22
			{
				get
				{
					System.Decimal? data = entity.Pph22;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pph22 = null;
					else entity.Pph22 = Convert.ToDecimal(value);
				}
			}
			public System.String Pph23
			{
				get
				{
					System.Decimal? data = entity.Pph23;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pph23 = null;
					else entity.Pph23 = Convert.ToDecimal(value);
				}
			}
			public System.String ContractNo
			{
				get
				{
					System.String data = entity.ContractNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ContractNo = null;
					else entity.ContractNo = Convert.ToString(value);
				}
			}
			public System.String PriorChargesAmount
			{
				get
				{
					System.Decimal? data = entity.PriorChargesAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriorChargesAmount = null;
					else entity.PriorChargesAmount = Convert.ToDecimal(value);
				}
			}
			public System.String PriorTaxAmount
			{
				get
				{
					System.Decimal? data = entity.PriorTaxAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriorTaxAmount = null;
					else entity.PriorTaxAmount = Convert.ToDecimal(value);
				}
			}
			public System.String CustomerID
			{
				get
				{
					System.String data = entity.CustomerID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CustomerID = null;
					else entity.CustomerID = Convert.ToString(value);
				}
			}
			public System.String TaxInvoiceNo
			{
				get
				{
					System.String data = entity.TaxInvoiceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TaxInvoiceNo = null;
					else entity.TaxInvoiceNo = Convert.ToString(value);
				}
			}
			public System.String TaxInvoiceDate
			{
				get
				{
					System.DateTime? data = entity.TaxInvoiceDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TaxInvoiceDate = null;
					else entity.TaxInvoiceDate = Convert.ToDateTime(value);
				}
			}
			public System.String SRPaymentType
			{
				get
				{
					System.String data = entity.SRPaymentType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPaymentType = null;
					else entity.SRPaymentType = Convert.ToString(value);
				}
			}
			public System.String SRPurchaseCategorization
			{
				get
				{
					System.String data = entity.SRPurchaseCategorization;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPurchaseCategorization = null;
					else entity.SRPurchaseCategorization = Convert.ToString(value);
				}
			}
			public System.String IsInventoryItem
			{
				get
				{
					System.Boolean? data = entity.IsInventoryItem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInventoryItem = null;
					else entity.IsInventoryItem = Convert.ToBoolean(value);
				}
			}
			public System.String TermOfPayment
			{
				get
				{
					System.Decimal? data = entity.TermOfPayment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TermOfPayment = null;
					else entity.TermOfPayment = Convert.ToDecimal(value);
				}
			}
			public System.String IsAssets
			{
				get
				{
					System.Boolean? data = entity.IsAssets;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAssets = null;
					else entity.IsAssets = Convert.ToBoolean(value);
				}
			}
			public System.String BudgetPlanCounter
			{
				get
				{
					System.Int32? data = entity.BudgetPlanCounter;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BudgetPlanCounter = null;
					else entity.BudgetPlanCounter = Convert.ToInt32(value);
				}
			}
			public System.String ServiceUnitCostID
			{
				get
				{
					System.String data = entity.ServiceUnitCostID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitCostID = null;
					else entity.ServiceUnitCostID = Convert.ToString(value);
				}
			}
			public System.String IsPph22InPercent
			{
				get
				{
					System.Boolean? data = entity.IsPph22InPercent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPph22InPercent = null;
					else entity.IsPph22InPercent = Convert.ToBoolean(value);
				}
			}
			public System.String Pph22Percentage
			{
				get
				{
					System.Decimal? data = entity.Pph22Percentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pph22Percentage = null;
					else entity.Pph22Percentage = Convert.ToDecimal(value);
				}
			}
			public System.String IsPph23InPercent
			{
				get
				{
					System.Boolean? data = entity.IsPph23InPercent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPph23InPercent = null;
					else entity.IsPph23InPercent = Convert.ToBoolean(value);
				}
			}
			public System.String Pph23Percentage
			{
				get
				{
					System.Decimal? data = entity.Pph23Percentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pph23Percentage = null;
					else entity.Pph23Percentage = Convert.ToDecimal(value);
				}
			}
			public System.String IsConsignment
			{
				get
				{
					System.Boolean? data = entity.IsConsignment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsConsignment = null;
					else entity.IsConsignment = Convert.ToBoolean(value);
				}
			}
			public System.String AmountTaxed
			{
				get
				{
					System.Decimal? data = entity.AmountTaxed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AmountTaxed = null;
					else entity.AmountTaxed = Convert.ToDecimal(value);
				}
			}
			public System.String RevisionNumber
			{
				get
				{
					System.Int16? data = entity.RevisionNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RevisionNumber = null;
					else entity.RevisionNumber = Convert.ToInt16(value);
				}
			}
			public System.String PrintNumber
			{
				get
				{
					System.Int16? data = entity.PrintNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrintNumber = null;
					else entity.PrintNumber = Convert.ToInt16(value);
				}
			}
			public System.String LastPrintedDateTime
			{
				get
				{
					System.DateTime? data = entity.LastPrintedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastPrintedDateTime = null;
					else entity.LastPrintedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String LastPrintedByUserID
			{
				get
				{
					System.String data = entity.LastPrintedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastPrintedByUserID = null;
					else entity.LastPrintedByUserID = Convert.ToString(value);
				}
			}
			public System.String AdvanceAmount
			{
				get
				{
					System.Decimal? data = entity.AdvanceAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdvanceAmount = null;
					else entity.AdvanceAmount = Convert.ToDecimal(value);
				}
			}
			public System.String DeliveryOrdersNo
			{
				get
				{
					System.String data = entity.DeliveryOrdersNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeliveryOrdersNo = null;
					else entity.DeliveryOrdersNo = Convert.ToString(value);
				}
			}
			public System.String DeliveryOrdersDate
			{
				get
				{
					System.DateTime? data = entity.DeliveryOrdersDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeliveryOrdersDate = null;
					else entity.DeliveryOrdersDate = Convert.ToDateTime(value);
				}
			}
			public System.String PphAmount
			{
				get
				{
					System.Decimal? data = entity.PphAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PphAmount = null;
					else entity.PphAmount = Convert.ToDecimal(value);
				}
			}
			public System.String InvoiceSupplierDate
			{
				get
				{
					System.DateTime? data = entity.InvoiceSupplierDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InvoiceSupplierDate = null;
					else entity.InvoiceSupplierDate = Convert.ToDateTime(value);
				}
			}
			public System.String CashTransactionReconcileId
			{
				get
				{
					System.String data = entity.CashTransactionReconcileId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CashTransactionReconcileId = null;
					else entity.CashTransactionReconcileId = Convert.ToString(value);
				}
			}
			public System.String ItemGroupID
			{
				get
				{
					System.String data = entity.ItemGroupID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemGroupID = null;
					else entity.ItemGroupID = Convert.ToString(value);
				}
			}
			public System.String SRPph
			{
				get
				{
					System.String data = entity.SRPph;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPph = null;
					else entity.SRPph = Convert.ToString(value);
				}
			}
			public System.String PphPercentage
			{
				get
				{
					System.Decimal? data = entity.PphPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PphPercentage = null;
					else entity.PphPercentage = Convert.ToDecimal(value);
				}
			}
			public System.String IsConsignmentAlreadyReceived
			{
				get
				{
					System.Boolean? data = entity.IsConsignmentAlreadyReceived;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsConsignmentAlreadyReceived = null;
					else entity.IsConsignmentAlreadyReceived = Convert.ToBoolean(value);
				}
			}
			public System.String PlanningDate
			{
				get
				{
					System.DateTime? data = entity.PlanningDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PlanningDate = null;
					else entity.PlanningDate = Convert.ToDateTime(value);
				}
			}
			public System.String IsInstallmentType
			{
				get
				{
					System.Boolean? data = entity.IsInstallmentType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInstallmentType = null;
					else entity.IsInstallmentType = Convert.ToBoolean(value);
				}
			}
			public System.String CreateDateTime
			{
				get
				{
					System.DateTime? data = entity.CreateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateDateTime = null;
					else entity.CreateDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CreateByUserID
			{
				get
				{
					System.String data = entity.CreateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateByUserID = null;
					else entity.CreateByUserID = Convert.ToString(value);
				}
			}
			public System.String SRItemCategory
			{
				get
				{
					System.String data = entity.SRItemCategory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRItemCategory = null;
					else entity.SRItemCategory = Convert.ToString(value);
				}
			}
			public System.String SRProcurementType
			{
				get
				{
					System.String data = entity.SRProcurementType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRProcurementType = null;
					else entity.SRProcurementType = Convert.ToString(value);
				}
			}
			public System.String ContractDate
			{
				get
				{
					System.DateTime? data = entity.ContractDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ContractDate = null;
					else entity.ContractDate = Convert.ToDateTime(value);
				}
			}
			public System.String CheckNo
			{
				get
				{
					System.String data = entity.CheckNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CheckNo = null;
					else entity.CheckNo = Convert.ToString(value);
				}
			}
			public System.String CheckDate
			{
				get
				{
					System.DateTime? data = entity.CheckDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CheckDate = null;
					else entity.CheckDate = Convert.ToDateTime(value);
				}
			}
			public System.String SalesMarginPercentage
			{
				get
				{
					System.Decimal? data = entity.SalesMarginPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalesMarginPercentage = null;
					else entity.SalesMarginPercentage = Convert.ToDecimal(value);
				}
			}
			private esItemTransaction entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemTransactionQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			query.es2.Connection = ((IEntity)this).Connection;
		}

		[System.Diagnostics.DebuggerNonUserCode]
		protected bool OnQueryLoaded(DataTable table)
		{
			bool dataFound = this.PopulateEntity(table);

			if (this.RowCount > 1)
			{
				throw new Exception("esItemTransaction can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ItemTransaction : esItemTransaction
	{
	}

	[Serializable]
	abstract public class esItemTransactionQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ItemTransactionMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem TransactionCode
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.TransactionCode, esSystemType.String);
			}
		}

		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		}

		public esQueryItem BusinessPartnerID
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.BusinessPartnerID, esSystemType.String);
			}
		}

		public esQueryItem InvoiceNo
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.InvoiceNo, esSystemType.String);
			}
		}

		public esQueryItem CurrencyID
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.CurrencyID, esSystemType.String);
			}
		}

		public esQueryItem CurrencyRate
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.CurrencyRate, esSystemType.Decimal);
			}
		}

		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		}

		public esQueryItem ReferenceDate
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.ReferenceDate, esSystemType.DateTime);
			}
		}

		public esQueryItem FromServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.FromServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem FromLocationID
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.FromLocationID, esSystemType.String);
			}
		}

		public esQueryItem ToServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.ToServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem ToLocationID
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.ToLocationID, esSystemType.String);
			}
		}

		public esQueryItem TermID
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.TermID, esSystemType.String);
			}
		}

		public esQueryItem SRItemType
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.SRItemType, esSystemType.String);
			}
		}

		public esQueryItem DiscountAmount
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.DiscountAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem ChargesAmount
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.ChargesAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem StampAmount
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.StampAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem DownPaymentAmount
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.DownPaymentAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem DownPaymentReferenceNo
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.DownPaymentReferenceNo, esSystemType.String);
			}
		}

		public esQueryItem SRDownPaymentType
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.SRDownPaymentType, esSystemType.String);
			}
		}

		public esQueryItem SRAdjustmentType
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.SRAdjustmentType, esSystemType.String);
			}
		}

		public esQueryItem SRDistributionType
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.SRDistributionType, esSystemType.String);
			}
		}

		public esQueryItem SRPurchaseReturnType
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.SRPurchaseReturnType, esSystemType.String);
			}
		}

		public esQueryItem SRPurchaseOrderType
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.SRPurchaseOrderType, esSystemType.String);
			}
		}

		public esQueryItem TaxPercentage
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.TaxPercentage, esSystemType.Decimal);
			}
		}

		public esQueryItem TaxAmount
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.TaxAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem IsTaxable
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.IsTaxable, esSystemType.Int16);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDate
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.VoidDate, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDate
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.ApprovedDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsClosed
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.IsClosed, esSystemType.Boolean);
			}
		}

		public esQueryItem IsBySystem
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.IsBySystem, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNonMasterOrder
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.IsNonMasterOrder, esSystemType.Boolean);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem ProductAccountID
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.ProductAccountID, esSystemType.String);
			}
		}

		public esQueryItem LeadTime
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.LeadTime, esSystemType.String);
			}
		}

		public esQueryItem Pph22
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.Pph22, esSystemType.Decimal);
			}
		}

		public esQueryItem Pph23
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.Pph23, esSystemType.Decimal);
			}
		}

		public esQueryItem ContractNo
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.ContractNo, esSystemType.String);
			}
		}

		public esQueryItem PriorChargesAmount
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.PriorChargesAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem PriorTaxAmount
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.PriorTaxAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem CustomerID
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.CustomerID, esSystemType.String);
			}
		}

		public esQueryItem TaxInvoiceNo
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.TaxInvoiceNo, esSystemType.String);
			}
		}

		public esQueryItem TaxInvoiceDate
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.TaxInvoiceDate, esSystemType.DateTime);
			}
		}

		public esQueryItem SRPaymentType
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.SRPaymentType, esSystemType.String);
			}
		}

		public esQueryItem SRPurchaseCategorization
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.SRPurchaseCategorization, esSystemType.String);
			}
		}

		public esQueryItem IsInventoryItem
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.IsInventoryItem, esSystemType.Boolean);
			}
		}

		public esQueryItem TermOfPayment
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.TermOfPayment, esSystemType.Decimal);
			}
		}

		public esQueryItem IsAssets
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.IsAssets, esSystemType.Boolean);
			}
		}

		public esQueryItem BudgetPlanCounter
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.BudgetPlanCounter, esSystemType.Int32);
			}
		}

		public esQueryItem ServiceUnitCostID
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.ServiceUnitCostID, esSystemType.String);
			}
		}

		public esQueryItem IsPph22InPercent
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.IsPph22InPercent, esSystemType.Boolean);
			}
		}

		public esQueryItem Pph22Percentage
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.Pph22Percentage, esSystemType.Decimal);
			}
		}

		public esQueryItem IsPph23InPercent
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.IsPph23InPercent, esSystemType.Boolean);
			}
		}

		public esQueryItem Pph23Percentage
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.Pph23Percentage, esSystemType.Decimal);
			}
		}

		public esQueryItem IsConsignment
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.IsConsignment, esSystemType.Boolean);
			}
		}

		public esQueryItem AmountTaxed
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.AmountTaxed, esSystemType.Decimal);
			}
		}

		public esQueryItem RevisionNumber
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.RevisionNumber, esSystemType.Int16);
			}
		}

		public esQueryItem PrintNumber
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.PrintNumber, esSystemType.Int16);
			}
		}

		public esQueryItem LastPrintedDateTime
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.LastPrintedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastPrintedByUserID
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.LastPrintedByUserID, esSystemType.String);
			}
		}

		public esQueryItem AdvanceAmount
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.AdvanceAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem DeliveryOrdersNo
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.DeliveryOrdersNo, esSystemType.String);
			}
		}

		public esQueryItem DeliveryOrdersDate
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.DeliveryOrdersDate, esSystemType.DateTime);
			}
		}

		public esQueryItem PphAmount
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.PphAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem InvoiceSupplierDate
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.InvoiceSupplierDate, esSystemType.DateTime);
			}
		}

		public esQueryItem CashTransactionReconcileId
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.CashTransactionReconcileId, esSystemType.String);
			}
		}

		public esQueryItem ItemGroupID
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.ItemGroupID, esSystemType.String);
			}
		}

		public esQueryItem SRPph
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.SRPph, esSystemType.String);
			}
		}

		public esQueryItem PphPercentage
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.PphPercentage, esSystemType.Decimal);
			}
		}

		public esQueryItem IsConsignmentAlreadyReceived
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.IsConsignmentAlreadyReceived, esSystemType.Boolean);
			}
		}

		public esQueryItem PlanningDate
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.PlanningDate, esSystemType.DateTime);
			}
		}

		public esQueryItem IsInstallmentType
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.IsInstallmentType, esSystemType.Boolean);
			}
		}

		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		}

		public esQueryItem SRItemCategory
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.SRItemCategory, esSystemType.String);
			}
		}

		public esQueryItem SRProcurementType
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.SRProcurementType, esSystemType.String);
			}
		}

		public esQueryItem ContractDate
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.ContractDate, esSystemType.DateTime);
			}
		}

		public esQueryItem CheckNo
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.CheckNo, esSystemType.String);
			}
		}

		public esQueryItem CheckDate
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.CheckDate, esSystemType.DateTime);
			}
		}

		public esQueryItem SalesMarginPercentage
		{
			get
			{
				return new esQueryItem(this, ItemTransactionMetadata.ColumnNames.SalesMarginPercentage, esSystemType.Decimal);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemTransactionCollection")]
	public partial class ItemTransactionCollection : esItemTransactionCollection, IEnumerable<ItemTransaction>
	{
		public ItemTransactionCollection()
		{

		}

		public static implicit operator List<ItemTransaction>(ItemTransactionCollection coll)
		{
			List<ItemTransaction> list = new List<ItemTransaction>();

			foreach (ItemTransaction emp in coll)
			{
				list.Add(emp);
			}

			return list;
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemTransactionMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemTransactionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemTransaction(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemTransaction();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ItemTransactionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemTransactionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		/// <summary>
		/// Useful for building up conditional queries.
		/// In most cases, before loading an entity or collection,
		/// you should instantiate a new one. This method was added
		/// to handle specialized circumstances, and should not be
		/// used as a substitute for that.
		/// </summary>
		/// <remarks>
		/// This just sets obj.Query to null/Nothing.
		/// In most cases, you will 'new' your object before
		/// loading it, rather than calling this method.
		/// It only affects obj.Query.Load(), so is not useful
		/// when Joins are involved, or for many other situations.
		/// Because it clears out any obj.Query.Where clauses,
		/// it can be useful for building conditional queries on the fly.
		/// <code>
		/// public bool ReQuery(string lastName, string firstName)
		/// {
		///     this.QueryReset();
		///     
		///     if(!String.IsNullOrEmpty(lastName))
		///     {
		///         this.Query.Where(
		///             this.Query.LastName == lastName);
		///     }
		///     if(!String.IsNullOrEmpty(firstName))
		///     {
		///         this.Query.Where(
		///             this.Query.FirstName == firstName);
		///     }
		///     
		///     return this.Query.Load();
		/// }
		/// </code>
		/// <code lang="vbnet">
		/// Public Function ReQuery(ByVal lastName As String, _
		///     ByVal firstName As String) As Boolean
		/// 
		///     Me.QueryReset()
		/// 
		///     If Not [String].IsNullOrEmpty(lastName) Then
		///         Me.Query.Where(Me.Query.LastName = lastName)
		///     End If
		///     If Not [String].IsNullOrEmpty(firstName) Then
		///         Me.Query.Where(Me.Query.FirstName = firstName)
		///     End If
		/// 
		///     Return Me.Query.Load()
		/// End Function
		/// </code>
		/// </remarks>
		public void QueryReset()
		{
			this.query = null;
		}

		/// <summary>
		/// Used to custom load a Join query.
		/// Returns true if at least one record was loaded.
		/// </summary>
		/// <remarks>
		/// Provides support for InnerJoin, LeftJoin,
		/// RightJoin, and FullJoin. You must provide an alias
		/// for each query when instantiating them.
		/// <code>
		/// EmployeeCollection collection = new EmployeeCollection();
		/// 
		/// EmployeeQuery emp = new EmployeeQuery("eq");
		/// CustomerQuery cust = new CustomerQuery("cq");
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
		/// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
		/// 
		/// collection.Load(emp);
		/// </code>
		/// <code lang="vbnet">
		/// Dim collection As New EmployeeCollection()
		/// 
		/// Dim emp As New EmployeeQuery("eq")
		/// Dim cust As New CustomerQuery("cq")
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
		/// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
		/// 
		/// collection.Load(emp)
		/// </code>
		/// </remarks>
		/// <param name="query">The query object instance name.</param>
		/// <returns>True if at least one record was loaded.</returns>
		public bool Load(ItemTransactionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ItemTransaction AddNew()
		{
			ItemTransaction entity = base.AddNewEntity() as ItemTransaction;

			return entity;
		}
		public ItemTransaction FindByPrimaryKey(String transactionNo)
		{
			return base.FindByPrimaryKey(transactionNo) as ItemTransaction;
		}

		#region IEnumerable< ItemTransaction> Members

		IEnumerator<ItemTransaction> IEnumerable<ItemTransaction>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ItemTransaction;
			}
		}

		#endregion

		private ItemTransactionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemTransaction' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ItemTransaction ({TransactionNo})")]
	[Serializable]
	public partial class ItemTransaction : esItemTransaction
	{
		public ItemTransaction()
		{
		}

		public ItemTransaction(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemTransactionMetadata.Meta();
			}
		}

		override protected esItemTransactionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemTransactionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ItemTransactionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemTransactionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		/// <summary>
		/// Useful for building up conditional queries.
		/// In most cases, before loading an entity or collection,
		/// you should instantiate a new one. This method was added
		/// to handle specialized circumstances, and should not be
		/// used as a substitute for that.
		/// </summary>
		/// <remarks>
		/// This just sets obj.Query to null/Nothing.
		/// In most cases, you will 'new' your object before
		/// loading it, rather than calling this method.
		/// It only affects obj.Query.Load(), so is not useful
		/// when Joins are involved, or for many other situations.
		/// Because it clears out any obj.Query.Where clauses,
		/// it can be useful for building conditional queries on the fly.
		/// <code>
		/// public bool ReQuery(string lastName, string firstName)
		/// {
		///     this.QueryReset();
		///     
		///     if(!String.IsNullOrEmpty(lastName))
		///     {
		///         this.Query.Where(
		///             this.Query.LastName == lastName);
		///     }
		///     if(!String.IsNullOrEmpty(firstName))
		///     {
		///         this.Query.Where(
		///             this.Query.FirstName == firstName);
		///     }
		///     
		///     return this.Query.Load();
		/// }
		/// </code>
		/// <code lang="vbnet">
		/// Public Function ReQuery(ByVal lastName As String, _
		///     ByVal firstName As String) As Boolean
		/// 
		///     Me.QueryReset()
		/// 
		///     If Not [String].IsNullOrEmpty(lastName) Then
		///         Me.Query.Where(Me.Query.LastName = lastName)
		///     End If
		///     If Not [String].IsNullOrEmpty(firstName) Then
		///         Me.Query.Where(Me.Query.FirstName = firstName)
		///     End If
		/// 
		///     Return Me.Query.Load()
		/// End Function
		/// </code>
		/// </remarks>
		public void QueryReset()
		{
			this.query = null;
		}

		/// <summary>
		/// Used to custom load a Join query.
		/// Returns true if at least one row is loaded.
		/// For an entity, an exception will be thrown
		/// if more than one row is loaded.
		/// </summary>
		/// <remarks>
		/// Provides support for InnerJoin, LeftJoin,
		/// RightJoin, and FullJoin. You must provide an alias
		/// for each query when instantiating them.
		/// <code>
		/// EmployeeCollection collection = new EmployeeCollection();
		/// 
		/// EmployeeQuery emp = new EmployeeQuery("eq");
		/// CustomerQuery cust = new CustomerQuery("cq");
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
		/// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
		/// 
		/// collection.Load(emp);
		/// </code>
		/// <code lang="vbnet">
		/// Dim collection As New EmployeeCollection()
		/// 
		/// Dim emp As New EmployeeQuery("eq")
		/// Dim cust As New CustomerQuery("cq")
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
		/// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
		/// 
		/// collection.Load(emp)
		/// </code>
		/// </remarks>
		/// <param name="query">The query object instance name.</param>
		/// <returns>True if at least one record was loaded.</returns>
		public bool Load(ItemTransactionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ItemTransactionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ItemTransactionQuery : esItemTransactionQuery
	{
		public ItemTransactionQuery()
		{

		}

		public ItemTransactionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ItemTransactionQuery";
		}
	}

	[Serializable]
	public partial class ItemTransactionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemTransactionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.TransactionCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.TransactionCode;
			c.CharacterMaxLength = 3;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.TransactionDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.TransactionDate;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.BusinessPartnerID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.BusinessPartnerID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.InvoiceNo, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.InvoiceNo;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.CurrencyID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.CurrencyID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.CurrencyRate, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.CurrencyRate;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.ReferenceNo, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.ReferenceDate, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.ReferenceDate;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.FromServiceUnitID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.FromServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.FromLocationID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.FromLocationID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.ToServiceUnitID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.ToServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.ToLocationID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.ToLocationID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.TermID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.TermID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.SRItemType, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.SRItemType;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.DiscountAmount, 15, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.DiscountAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.ChargesAmount, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.ChargesAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.StampAmount, 17, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.StampAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.DownPaymentAmount, 18, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.DownPaymentAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.DownPaymentReferenceNo, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.DownPaymentReferenceNo;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.SRDownPaymentType, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.SRDownPaymentType;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.SRAdjustmentType, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.SRAdjustmentType;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.SRDistributionType, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.SRDistributionType;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.SRPurchaseReturnType, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.SRPurchaseReturnType;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.SRPurchaseOrderType, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.SRPurchaseOrderType;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.TaxPercentage, 25, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.TaxPercentage;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.TaxAmount, 26, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.TaxAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.IsTaxable, 27, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.IsTaxable;
			c.NumericPrecision = 5;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.IsVoid, 28, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.IsVoid;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.VoidDate, 29, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.VoidDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.VoidByUserID, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.IsApproved, 31, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.IsApproved;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.ApprovedDate, 32, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.ApprovedDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.ApprovedByUserID, 33, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.IsClosed, 34, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.IsClosed;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.IsBySystem, 35, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.IsBySystem;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.IsNonMasterOrder, 36, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.IsNonMasterOrder;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.Notes, 37, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.LastUpdateDateTime, 38, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.LastUpdateByUserID, 39, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.ProductAccountID, 40, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.ProductAccountID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.LeadTime, 41, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.LeadTime;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.Pph22, 42, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.Pph22;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.Pph23, 43, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.Pph23;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.ContractNo, 44, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.ContractNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.PriorChargesAmount, 45, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.PriorChargesAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.PriorTaxAmount, 46, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.PriorTaxAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.CustomerID, 47, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.CustomerID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.TaxInvoiceNo, 48, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.TaxInvoiceNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.TaxInvoiceDate, 49, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.TaxInvoiceDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.SRPaymentType, 50, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.SRPaymentType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.SRPurchaseCategorization, 51, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.SRPurchaseCategorization;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.IsInventoryItem, 52, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.IsInventoryItem;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.TermOfPayment, 53, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.TermOfPayment;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.IsAssets, 54, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.IsAssets;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.BudgetPlanCounter, 55, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.BudgetPlanCounter;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.ServiceUnitCostID, 56, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.ServiceUnitCostID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.IsPph22InPercent, 57, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.IsPph22InPercent;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.Pph22Percentage, 58, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.Pph22Percentage;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.IsPph23InPercent, 59, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.IsPph23InPercent;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.Pph23Percentage, 60, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.Pph23Percentage;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.IsConsignment, 61, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.IsConsignment;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.AmountTaxed, 62, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.AmountTaxed;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.RevisionNumber, 63, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.RevisionNumber;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.PrintNumber, 64, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.PrintNumber;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.LastPrintedDateTime, 65, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.LastPrintedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.LastPrintedByUserID, 66, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.LastPrintedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.AdvanceAmount, 67, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.AdvanceAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.DeliveryOrdersNo, 68, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.DeliveryOrdersNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.DeliveryOrdersDate, 69, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.DeliveryOrdersDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.PphAmount, 70, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.PphAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.InvoiceSupplierDate, 71, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.InvoiceSupplierDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.CashTransactionReconcileId, 72, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.CashTransactionReconcileId;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.ItemGroupID, 73, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.ItemGroupID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.SRPph, 74, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.SRPph;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.PphPercentage, 75, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.PphPercentage;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.IsConsignmentAlreadyReceived, 76, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.IsConsignmentAlreadyReceived;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.PlanningDate, 77, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.PlanningDate;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.IsInstallmentType, 78, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.IsInstallmentType;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.CreateDateTime, 79, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.CreateByUserID, 80, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.SRItemCategory, 81, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.SRItemCategory;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.SRProcurementType, 82, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.SRProcurementType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.ContractDate, 83, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.ContractDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.CheckNo, 84, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.CheckNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.CheckDate, 85, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.CheckDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionMetadata.ColumnNames.SalesMarginPercentage, 86, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionMetadata.PropertyNames.SalesMarginPercentage;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ItemTransactionMetadata Meta()
		{
			return meta;
		}

		public Guid DataID
		{
			get { return base._dataID; }
		}

		public bool MultiProviderMode
		{
			get { return false; }
		}

		public esColumnMetadataCollection Columns
		{
			get { return base._columns; }
		}

		#region ColumnNames
		public class ColumnNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string TransactionCode = "TransactionCode";
			public const string TransactionDate = "TransactionDate";
			public const string BusinessPartnerID = "BusinessPartnerID";
			public const string InvoiceNo = "InvoiceNo";
			public const string CurrencyID = "CurrencyID";
			public const string CurrencyRate = "CurrencyRate";
			public const string ReferenceNo = "ReferenceNo";
			public const string ReferenceDate = "ReferenceDate";
			public const string FromServiceUnitID = "FromServiceUnitID";
			public const string FromLocationID = "FromLocationID";
			public const string ToServiceUnitID = "ToServiceUnitID";
			public const string ToLocationID = "ToLocationID";
			public const string TermID = "TermID";
			public const string SRItemType = "SRItemType";
			public const string DiscountAmount = "DiscountAmount";
			public const string ChargesAmount = "ChargesAmount";
			public const string StampAmount = "StampAmount";
			public const string DownPaymentAmount = "DownPaymentAmount";
			public const string DownPaymentReferenceNo = "DownPaymentReferenceNo";
			public const string SRDownPaymentType = "SRDownPaymentType";
			public const string SRAdjustmentType = "SRAdjustmentType";
			public const string SRDistributionType = "SRDistributionType";
			public const string SRPurchaseReturnType = "SRPurchaseReturnType";
			public const string SRPurchaseOrderType = "SRPurchaseOrderType";
			public const string TaxPercentage = "TaxPercentage";
			public const string TaxAmount = "TaxAmount";
			public const string IsTaxable = "IsTaxable";
			public const string IsVoid = "IsVoid";
			public const string VoidDate = "VoidDate";
			public const string VoidByUserID = "VoidByUserID";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDate = "ApprovedDate";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsClosed = "IsClosed";
			public const string IsBySystem = "IsBySystem";
			public const string IsNonMasterOrder = "IsNonMasterOrder";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ProductAccountID = "ProductAccountID";
			public const string LeadTime = "LeadTime";
			public const string Pph22 = "Pph22";
			public const string Pph23 = "Pph23";
			public const string ContractNo = "ContractNo";
			public const string PriorChargesAmount = "PriorChargesAmount";
			public const string PriorTaxAmount = "PriorTaxAmount";
			public const string CustomerID = "CustomerID";
			public const string TaxInvoiceNo = "TaxInvoiceNo";
			public const string TaxInvoiceDate = "TaxInvoiceDate";
			public const string SRPaymentType = "SRPaymentType";
			public const string SRPurchaseCategorization = "SRPurchaseCategorization";
			public const string IsInventoryItem = "IsInventoryItem";
			public const string TermOfPayment = "TermOfPayment";
			public const string IsAssets = "IsAssets";
			public const string BudgetPlanCounter = "BudgetPlanCounter";
			public const string ServiceUnitCostID = "ServiceUnitCostID";
			public const string IsPph22InPercent = "IsPph22InPercent";
			public const string Pph22Percentage = "Pph22Percentage";
			public const string IsPph23InPercent = "IsPph23InPercent";
			public const string Pph23Percentage = "Pph23Percentage";
			public const string IsConsignment = "IsConsignment";
			public const string AmountTaxed = "AmountTaxed";
			public const string RevisionNumber = "RevisionNumber";
			public const string PrintNumber = "PrintNumber";
			public const string LastPrintedDateTime = "LastPrintedDateTime";
			public const string LastPrintedByUserID = "LastPrintedByUserID";
			public const string AdvanceAmount = "AdvanceAmount";
			public const string DeliveryOrdersNo = "DeliveryOrdersNo";
			public const string DeliveryOrdersDate = "DeliveryOrdersDate";
			public const string PphAmount = "PphAmount";
			public const string InvoiceSupplierDate = "InvoiceSupplierDate";
			public const string CashTransactionReconcileId = "CashTransactionReconcileId";
			public const string ItemGroupID = "ItemGroupID";
			public const string SRPph = "SRPph";
			public const string PphPercentage = "PphPercentage";
			public const string IsConsignmentAlreadyReceived = "IsConsignmentAlreadyReceived";
			public const string PlanningDate = "PlanningDate";
			public const string IsInstallmentType = "IsInstallmentType";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string SRItemCategory = "SRItemCategory";
			public const string SRProcurementType = "SRProcurementType";
			public const string ContractDate = "ContractDate";
			public const string CheckNo = "CheckNo";
			public const string CheckDate = "CheckDate";
			public const string SalesMarginPercentage = "SalesMarginPercentage";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string TransactionCode = "TransactionCode";
			public const string TransactionDate = "TransactionDate";
			public const string BusinessPartnerID = "BusinessPartnerID";
			public const string InvoiceNo = "InvoiceNo";
			public const string CurrencyID = "CurrencyID";
			public const string CurrencyRate = "CurrencyRate";
			public const string ReferenceNo = "ReferenceNo";
			public const string ReferenceDate = "ReferenceDate";
			public const string FromServiceUnitID = "FromServiceUnitID";
			public const string FromLocationID = "FromLocationID";
			public const string ToServiceUnitID = "ToServiceUnitID";
			public const string ToLocationID = "ToLocationID";
			public const string TermID = "TermID";
			public const string SRItemType = "SRItemType";
			public const string DiscountAmount = "DiscountAmount";
			public const string ChargesAmount = "ChargesAmount";
			public const string StampAmount = "StampAmount";
			public const string DownPaymentAmount = "DownPaymentAmount";
			public const string DownPaymentReferenceNo = "DownPaymentReferenceNo";
			public const string SRDownPaymentType = "SRDownPaymentType";
			public const string SRAdjustmentType = "SRAdjustmentType";
			public const string SRDistributionType = "SRDistributionType";
			public const string SRPurchaseReturnType = "SRPurchaseReturnType";
			public const string SRPurchaseOrderType = "SRPurchaseOrderType";
			public const string TaxPercentage = "TaxPercentage";
			public const string TaxAmount = "TaxAmount";
			public const string IsTaxable = "IsTaxable";
			public const string IsVoid = "IsVoid";
			public const string VoidDate = "VoidDate";
			public const string VoidByUserID = "VoidByUserID";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDate = "ApprovedDate";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsClosed = "IsClosed";
			public const string IsBySystem = "IsBySystem";
			public const string IsNonMasterOrder = "IsNonMasterOrder";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ProductAccountID = "ProductAccountID";
			public const string LeadTime = "LeadTime";
			public const string Pph22 = "Pph22";
			public const string Pph23 = "Pph23";
			public const string ContractNo = "ContractNo";
			public const string PriorChargesAmount = "PriorChargesAmount";
			public const string PriorTaxAmount = "PriorTaxAmount";
			public const string CustomerID = "CustomerID";
			public const string TaxInvoiceNo = "TaxInvoiceNo";
			public const string TaxInvoiceDate = "TaxInvoiceDate";
			public const string SRPaymentType = "SRPaymentType";
			public const string SRPurchaseCategorization = "SRPurchaseCategorization";
			public const string IsInventoryItem = "IsInventoryItem";
			public const string TermOfPayment = "TermOfPayment";
			public const string IsAssets = "IsAssets";
			public const string BudgetPlanCounter = "BudgetPlanCounter";
			public const string ServiceUnitCostID = "ServiceUnitCostID";
			public const string IsPph22InPercent = "IsPph22InPercent";
			public const string Pph22Percentage = "Pph22Percentage";
			public const string IsPph23InPercent = "IsPph23InPercent";
			public const string Pph23Percentage = "Pph23Percentage";
			public const string IsConsignment = "IsConsignment";
			public const string AmountTaxed = "AmountTaxed";
			public const string RevisionNumber = "RevisionNumber";
			public const string PrintNumber = "PrintNumber";
			public const string LastPrintedDateTime = "LastPrintedDateTime";
			public const string LastPrintedByUserID = "LastPrintedByUserID";
			public const string AdvanceAmount = "AdvanceAmount";
			public const string DeliveryOrdersNo = "DeliveryOrdersNo";
			public const string DeliveryOrdersDate = "DeliveryOrdersDate";
			public const string PphAmount = "PphAmount";
			public const string InvoiceSupplierDate = "InvoiceSupplierDate";
			public const string CashTransactionReconcileId = "CashTransactionReconcileId";
			public const string ItemGroupID = "ItemGroupID";
			public const string SRPph = "SRPph";
			public const string PphPercentage = "PphPercentage";
			public const string IsConsignmentAlreadyReceived = "IsConsignmentAlreadyReceived";
			public const string PlanningDate = "PlanningDate";
			public const string IsInstallmentType = "IsInstallmentType";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string SRItemCategory = "SRItemCategory";
			public const string SRProcurementType = "SRProcurementType";
			public const string ContractDate = "ContractDate";
			public const string CheckNo = "CheckNo";
			public const string CheckDate = "CheckDate";
			public const string SalesMarginPercentage = "SalesMarginPercentage";
		}
		#endregion

		public esProviderSpecificMetadata GetProviderMetadata(string mapName)
		{
			MapToMeta mapMethod = mapDelegates[mapName];

			if (mapMethod != null)
				return mapMethod(mapName);
			else
				return null;
		}

		#region MAP esDefault

		static private int RegisterDelegateesDefault()
		{
			// This is only executed once per the life of the application
			lock (typeof(ItemTransactionMetadata))
			{
				if (ItemTransactionMetadata.mapDelegates == null)
				{
					ItemTransactionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ItemTransactionMetadata.meta == null)
				{
					ItemTransactionMetadata.meta = new ItemTransactionMetadata();
				}

				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if (!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();

				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionCode", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("BusinessPartnerID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InvoiceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CurrencyID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CurrencyRate", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("FromServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromLocationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ToServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ToLocationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TermID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRItemType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DiscountAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ChargesAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("StampAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DownPaymentAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DownPaymentReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRDownPaymentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRAdjustmentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRDistributionType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPurchaseReturnType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPurchaseOrderType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TaxPercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("TaxAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsTaxable", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsClosed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBySystem", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNonMasterOrder", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProductAccountID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LeadTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Pph22", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Pph23", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ContractNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PriorChargesAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PriorTaxAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CustomerID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TaxInvoiceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TaxInvoiceDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRPaymentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPurchaseCategorization", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsInventoryItem", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("TermOfPayment", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsAssets", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("BudgetPlanCounter", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ServiceUnitCostID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPph22InPercent", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Pph22Percentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsPph23InPercent", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Pph23Percentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsConsignment", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("AmountTaxed", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RevisionNumber", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("PrintNumber", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("LastPrintedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastPrintedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AdvanceAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DeliveryOrdersNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DeliveryOrdersDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PphAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("InvoiceSupplierDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CashTransactionReconcileId", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemGroupID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPph", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PphPercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsConsignmentAlreadyReceived", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PlanningDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsInstallmentType", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRItemCategory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRProcurementType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ContractDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CheckNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CheckDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SalesMarginPercentage", new esTypeMap("numeric", "System.Decimal"));


				meta.Source = "ItemTransaction";
				meta.Destination = "ItemTransaction";
				meta.spInsert = "proc_ItemTransactionInsert";
				meta.spUpdate = "proc_ItemTransactionUpdate";
				meta.spDelete = "proc_ItemTransactionDelete";
				meta.spLoadAll = "proc_ItemTransactionLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemTransactionLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemTransactionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
