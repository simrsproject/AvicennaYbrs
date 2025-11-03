/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/27/2021 3:15:11 PM
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
	abstract public class esInvoiceSupplierItemCollection : esEntityCollectionWAuditLog
	{
		public esInvoiceSupplierItemCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "InvoiceSupplierItemCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esInvoiceSupplierItemQuery query)
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
			this.InitQuery(query as esInvoiceSupplierItemQuery);
		}
		#endregion
			
		virtual public InvoiceSupplierItem DetachEntity(InvoiceSupplierItem entity)
		{
			return base.DetachEntity(entity) as InvoiceSupplierItem;
		}
		
		virtual public InvoiceSupplierItem AttachEntity(InvoiceSupplierItem entity)
		{
			return base.AttachEntity(entity) as InvoiceSupplierItem;
		}
		
		virtual public void Combine(InvoiceSupplierItemCollection collection)
		{
			base.Combine(collection);
		}
		
		new public InvoiceSupplierItem this[int index]
		{
			get
			{
				return base[index] as InvoiceSupplierItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(InvoiceSupplierItem);
		}
	}

	[Serializable]
	abstract public class esInvoiceSupplierItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esInvoiceSupplierItemQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esInvoiceSupplierItem()
		{
		}
	
		public esInvoiceSupplierItem(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String invoiceNo, String transactionNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(invoiceNo, transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(invoiceNo, transactionNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String invoiceNo, String transactionNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(invoiceNo, transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(invoiceNo, transactionNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String invoiceNo, String transactionNo)
		{
			esInvoiceSupplierItemQuery query = this.GetDynamicQuery();
			query.Where(query.InvoiceNo==invoiceNo, query.TransactionNo==transactionNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String invoiceNo, String transactionNo)
		{
			esParameters parms = new esParameters();
			parms.Add("InvoiceNo",invoiceNo);
			parms.Add("TransactionNo",transactionNo);
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
			if(this.Row == null) this.AddNew();
			
			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if(value == null || value is System.String)
				{				
					// Use the strongly typed property
					switch (name)
					{
						case "InvoiceNo": this.str.InvoiceNo = (string)value; break;
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "TransactionDate": this.str.TransactionDate = (string)value; break;
						case "VerifyAmount": this.str.VerifyAmount = (string)value; break;
						case "PaymentAmount": this.str.PaymentAmount = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "AccountID": this.str.AccountID = (string)value; break;
						case "Amount": this.str.Amount = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "VoucherID": this.str.VoucherID = (string)value; break;
						case "AgingDate": this.str.AgingDate = (string)value; break;
						case "SRInvoicePayment": this.str.SRInvoicePayment = (string)value; break;
						case "BankID": this.str.BankID = (string)value; break;
						case "BankAccountNo": this.str.BankAccountNo = (string)value; break;
						case "VerifyDate": this.str.VerifyDate = (string)value; break;
						case "VerifyByUserID": this.str.VerifyByUserID = (string)value; break;
						case "PaymentDate": this.str.PaymentDate = (string)value; break;
						case "PaymentByUserID": this.str.PaymentByUserID = (string)value; break;
						case "IsPaymentApproved": this.str.IsPaymentApproved = (string)value; break;
						case "PaymentApprovedDate": this.str.PaymentApprovedDate = (string)value; break;
						case "PaymentApprovedByUserID": this.str.PaymentApprovedByUserID = (string)value; break;
						case "PPnAmount": this.str.PPnAmount = (string)value; break;
						case "PPh22Amount": this.str.PPh22Amount = (string)value; break;
						case "PPh23Amount": this.str.PPh23Amount = (string)value; break;
						case "CurrencyID": this.str.CurrencyID = (string)value; break;
						case "CurrencyRate": this.str.CurrencyRate = (string)value; break;
						case "StampAmount": this.str.StampAmount = (string)value; break;
						case "InvoiceReferenceNo": this.str.InvoiceReferenceNo = (string)value; break;
						case "InvoiceSN": this.str.InvoiceSN = (string)value; break;
						case "TaxInvoiceDate": this.str.TaxInvoiceDate = (string)value; break;
						case "OtherDeduction": this.str.OtherDeduction = (string)value; break;
						case "IsAdditionalInvoice": this.str.IsAdditionalInvoice = (string)value; break;
						case "ChartOfAccountId": this.str.ChartOfAccountId = (string)value; break;
						case "SubLedgerId": this.str.SubLedgerId = (string)value; break;
						case "DownPaymentAmount": this.str.DownPaymentAmount = (string)value; break;
						case "SRPph": this.str.SRPph = (string)value; break;
						case "PphPercentage": this.str.PphPercentage = (string)value; break;
						case "PphAmount": this.str.PphAmount = (string)value; break;
						case "SRItemType": this.str.SRItemType = (string)value; break;
						case "IsPpnExcluded": this.str.IsPpnExcluded = (string)value; break;
                        case "RoundingAmount": this.str.RoundingAmount = (string)value; break;
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
						case "VerifyAmount":
						
							if (value == null || value is System.Decimal)
								this.VerifyAmount = (System.Decimal?)value;
							break;
						case "PaymentAmount":
						
							if (value == null || value is System.Decimal)
								this.PaymentAmount = (System.Decimal?)value;
							break;
						case "Amount":
						
							if (value == null || value is System.Decimal)
								this.Amount = (System.Decimal?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "AgingDate":
						
							if (value == null || value is System.DateTime)
								this.AgingDate = (System.DateTime?)value;
							break;
						case "VerifyDate":
						
							if (value == null || value is System.DateTime)
								this.VerifyDate = (System.DateTime?)value;
							break;
						case "PaymentDate":
						
							if (value == null || value is System.DateTime)
								this.PaymentDate = (System.DateTime?)value;
							break;
						case "IsPaymentApproved":
						
							if (value == null || value is System.Boolean)
								this.IsPaymentApproved = (System.Boolean?)value;
							break;
						case "PaymentApprovedDate":
						
							if (value == null || value is System.DateTime)
								this.PaymentApprovedDate = (System.DateTime?)value;
							break;
						case "PPnAmount":
						
							if (value == null || value is System.Decimal)
								this.PPnAmount = (System.Decimal?)value;
							break;
						case "PPh22Amount":
						
							if (value == null || value is System.Decimal)
								this.PPh22Amount = (System.Decimal?)value;
							break;
						case "PPh23Amount":
						
							if (value == null || value is System.Decimal)
								this.PPh23Amount = (System.Decimal?)value;
							break;
						case "CurrencyRate":
						
							if (value == null || value is System.Decimal)
								this.CurrencyRate = (System.Decimal?)value;
							break;
						case "StampAmount":
						
							if (value == null || value is System.Decimal)
								this.StampAmount = (System.Decimal?)value;
							break;
						case "TaxInvoiceDate":
						
							if (value == null || value is System.DateTime)
								this.TaxInvoiceDate = (System.DateTime?)value;
							break;
						case "OtherDeduction":
						
							if (value == null || value is System.Decimal)
								this.OtherDeduction = (System.Decimal?)value;
							break;
						case "IsAdditionalInvoice":
						
							if (value == null || value is System.Boolean)
								this.IsAdditionalInvoice = (System.Boolean?)value;
							break;
						case "ChartOfAccountId":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountId = (System.Int32?)value;
							break;
						case "SubLedgerId":
						
							if (value == null || value is System.Int32)
								this.SubLedgerId = (System.Int32?)value;
							break;
						case "DownPaymentAmount":
						
							if (value == null || value is System.Decimal)
								this.DownPaymentAmount = (System.Decimal?)value;
							break;
						case "PphPercentage":
						
							if (value == null || value is System.Decimal)
								this.PphPercentage = (System.Decimal?)value;
							break;
						case "PphAmount":
						
							if (value == null || value is System.Decimal)
								this.PphAmount = (System.Decimal?)value;
							break;
						case "IsPpnExcluded":
						
							if (value == null || value is System.Boolean)
								this.IsPpnExcluded = (System.Boolean?)value;
							break;
                        case "RoundingAmount":

                            if (value == null || value is System.Decimal)
                                this.RoundingAmount = (System.Decimal?)value;
                            break;
                        default:
							break;
					}
				}
			}
			else if(this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}

		/// <summary>
		/// Maps to InvoiceSupplierItem.InvoiceNo
		/// </summary>
		virtual public System.String InvoiceNo
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierItemMetadata.ColumnNames.InvoiceNo);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierItemMetadata.ColumnNames.InvoiceNo, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierItemMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierItemMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(InvoiceSupplierItemMetadata.ColumnNames.TransactionDate);
			}
			
			set
			{
				base.SetSystemDateTime(InvoiceSupplierItemMetadata.ColumnNames.TransactionDate, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.VerifyAmount
		/// </summary>
		virtual public System.Decimal? VerifyAmount
		{
			get
			{
				return base.GetSystemDecimal(InvoiceSupplierItemMetadata.ColumnNames.VerifyAmount);
			}
			
			set
			{
				base.SetSystemDecimal(InvoiceSupplierItemMetadata.ColumnNames.VerifyAmount, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.PaymentAmount
		/// </summary>
		virtual public System.Decimal? PaymentAmount
		{
			get
			{
				return base.GetSystemDecimal(InvoiceSupplierItemMetadata.ColumnNames.PaymentAmount);
			}
			
			set
			{
				base.SetSystemDecimal(InvoiceSupplierItemMetadata.ColumnNames.PaymentAmount, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierItemMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierItemMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.AccountID
		/// </summary>
		virtual public System.String AccountID
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierItemMetadata.ColumnNames.AccountID);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierItemMetadata.ColumnNames.AccountID, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.Amount
		/// </summary>
		virtual public System.Decimal? Amount
		{
			get
			{
				return base.GetSystemDecimal(InvoiceSupplierItemMetadata.ColumnNames.Amount);
			}
			
			set
			{
				base.SetSystemDecimal(InvoiceSupplierItemMetadata.ColumnNames.Amount, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(InvoiceSupplierItemMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(InvoiceSupplierItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierItemMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.VoucherID
		/// </summary>
		virtual public System.String VoucherID
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierItemMetadata.ColumnNames.VoucherID);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierItemMetadata.ColumnNames.VoucherID, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.AgingDate
		/// </summary>
		virtual public System.DateTime? AgingDate
		{
			get
			{
				return base.GetSystemDateTime(InvoiceSupplierItemMetadata.ColumnNames.AgingDate);
			}
			
			set
			{
				base.SetSystemDateTime(InvoiceSupplierItemMetadata.ColumnNames.AgingDate, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.SRInvoicePayment
		/// </summary>
		virtual public System.String SRInvoicePayment
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierItemMetadata.ColumnNames.SRInvoicePayment);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierItemMetadata.ColumnNames.SRInvoicePayment, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.BankID
		/// </summary>
		virtual public System.String BankID
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierItemMetadata.ColumnNames.BankID);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierItemMetadata.ColumnNames.BankID, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.BankAccountNo
		/// </summary>
		virtual public System.String BankAccountNo
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierItemMetadata.ColumnNames.BankAccountNo);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierItemMetadata.ColumnNames.BankAccountNo, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.VerifyDate
		/// </summary>
		virtual public System.DateTime? VerifyDate
		{
			get
			{
				return base.GetSystemDateTime(InvoiceSupplierItemMetadata.ColumnNames.VerifyDate);
			}
			
			set
			{
				base.SetSystemDateTime(InvoiceSupplierItemMetadata.ColumnNames.VerifyDate, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.VerifyByUserID
		/// </summary>
		virtual public System.String VerifyByUserID
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierItemMetadata.ColumnNames.VerifyByUserID);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierItemMetadata.ColumnNames.VerifyByUserID, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.PaymentDate
		/// </summary>
		virtual public System.DateTime? PaymentDate
		{
			get
			{
				return base.GetSystemDateTime(InvoiceSupplierItemMetadata.ColumnNames.PaymentDate);
			}
			
			set
			{
				base.SetSystemDateTime(InvoiceSupplierItemMetadata.ColumnNames.PaymentDate, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.PaymentByUserID
		/// </summary>
		virtual public System.String PaymentByUserID
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierItemMetadata.ColumnNames.PaymentByUserID);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierItemMetadata.ColumnNames.PaymentByUserID, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.IsPaymentApproved
		/// </summary>
		virtual public System.Boolean? IsPaymentApproved
		{
			get
			{
				return base.GetSystemBoolean(InvoiceSupplierItemMetadata.ColumnNames.IsPaymentApproved);
			}
			
			set
			{
				base.SetSystemBoolean(InvoiceSupplierItemMetadata.ColumnNames.IsPaymentApproved, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.PaymentApprovedDate
		/// </summary>
		virtual public System.DateTime? PaymentApprovedDate
		{
			get
			{
				return base.GetSystemDateTime(InvoiceSupplierItemMetadata.ColumnNames.PaymentApprovedDate);
			}
			
			set
			{
				base.SetSystemDateTime(InvoiceSupplierItemMetadata.ColumnNames.PaymentApprovedDate, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.PaymentApprovedByUserID
		/// </summary>
		virtual public System.String PaymentApprovedByUserID
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierItemMetadata.ColumnNames.PaymentApprovedByUserID);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierItemMetadata.ColumnNames.PaymentApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.PPnAmount
		/// </summary>
		virtual public System.Decimal? PPnAmount
		{
			get
			{
				return base.GetSystemDecimal(InvoiceSupplierItemMetadata.ColumnNames.PPnAmount);
			}
			
			set
			{
				base.SetSystemDecimal(InvoiceSupplierItemMetadata.ColumnNames.PPnAmount, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.PPh22Amount
		/// </summary>
		virtual public System.Decimal? PPh22Amount
		{
			get
			{
				return base.GetSystemDecimal(InvoiceSupplierItemMetadata.ColumnNames.PPh22Amount);
			}
			
			set
			{
				base.SetSystemDecimal(InvoiceSupplierItemMetadata.ColumnNames.PPh22Amount, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.PPh23Amount
		/// </summary>
		virtual public System.Decimal? PPh23Amount
		{
			get
			{
				return base.GetSystemDecimal(InvoiceSupplierItemMetadata.ColumnNames.PPh23Amount);
			}
			
			set
			{
				base.SetSystemDecimal(InvoiceSupplierItemMetadata.ColumnNames.PPh23Amount, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.CurrencyID
		/// </summary>
		virtual public System.String CurrencyID
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierItemMetadata.ColumnNames.CurrencyID);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierItemMetadata.ColumnNames.CurrencyID, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.CurrencyRate
		/// </summary>
		virtual public System.Decimal? CurrencyRate
		{
			get
			{
				return base.GetSystemDecimal(InvoiceSupplierItemMetadata.ColumnNames.CurrencyRate);
			}
			
			set
			{
				base.SetSystemDecimal(InvoiceSupplierItemMetadata.ColumnNames.CurrencyRate, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.StampAmount
		/// </summary>
		virtual public System.Decimal? StampAmount
		{
			get
			{
				return base.GetSystemDecimal(InvoiceSupplierItemMetadata.ColumnNames.StampAmount);
			}
			
			set
			{
				base.SetSystemDecimal(InvoiceSupplierItemMetadata.ColumnNames.StampAmount, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.InvoiceReferenceNo
		/// </summary>
		virtual public System.String InvoiceReferenceNo
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierItemMetadata.ColumnNames.InvoiceReferenceNo);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierItemMetadata.ColumnNames.InvoiceReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.InvoiceSN
		/// </summary>
		virtual public System.String InvoiceSN
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierItemMetadata.ColumnNames.InvoiceSN);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierItemMetadata.ColumnNames.InvoiceSN, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.TaxInvoiceDate
		/// </summary>
		virtual public System.DateTime? TaxInvoiceDate
		{
			get
			{
				return base.GetSystemDateTime(InvoiceSupplierItemMetadata.ColumnNames.TaxInvoiceDate);
			}
			
			set
			{
				base.SetSystemDateTime(InvoiceSupplierItemMetadata.ColumnNames.TaxInvoiceDate, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.OtherDeduction
		/// </summary>
		virtual public System.Decimal? OtherDeduction
		{
			get
			{
				return base.GetSystemDecimal(InvoiceSupplierItemMetadata.ColumnNames.OtherDeduction);
			}
			
			set
			{
				base.SetSystemDecimal(InvoiceSupplierItemMetadata.ColumnNames.OtherDeduction, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.IsAdditionalInvoice
		/// </summary>
		virtual public System.Boolean? IsAdditionalInvoice
		{
			get
			{
				return base.GetSystemBoolean(InvoiceSupplierItemMetadata.ColumnNames.IsAdditionalInvoice);
			}
			
			set
			{
				base.SetSystemBoolean(InvoiceSupplierItemMetadata.ColumnNames.IsAdditionalInvoice, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.ChartOfAccountId
		/// </summary>
		virtual public System.Int32? ChartOfAccountId
		{
			get
			{
				return base.GetSystemInt32(InvoiceSupplierItemMetadata.ColumnNames.ChartOfAccountId);
			}
			
			set
			{
				base.SetSystemInt32(InvoiceSupplierItemMetadata.ColumnNames.ChartOfAccountId, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.SubLedgerId
		/// </summary>
		virtual public System.Int32? SubLedgerId
		{
			get
			{
				return base.GetSystemInt32(InvoiceSupplierItemMetadata.ColumnNames.SubLedgerId);
			}
			
			set
			{
				base.SetSystemInt32(InvoiceSupplierItemMetadata.ColumnNames.SubLedgerId, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.DownPaymentAmount
		/// </summary>
		virtual public System.Decimal? DownPaymentAmount
		{
			get
			{
				return base.GetSystemDecimal(InvoiceSupplierItemMetadata.ColumnNames.DownPaymentAmount);
			}
			
			set
			{
				base.SetSystemDecimal(InvoiceSupplierItemMetadata.ColumnNames.DownPaymentAmount, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.SRPph
		/// </summary>
		virtual public System.String SRPph
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierItemMetadata.ColumnNames.SRPph);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierItemMetadata.ColumnNames.SRPph, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.PphPercentage
		/// </summary>
		virtual public System.Decimal? PphPercentage
		{
			get
			{
				return base.GetSystemDecimal(InvoiceSupplierItemMetadata.ColumnNames.PphPercentage);
			}
			
			set
			{
				base.SetSystemDecimal(InvoiceSupplierItemMetadata.ColumnNames.PphPercentage, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.PphAmount
		/// </summary>
		virtual public System.Decimal? PphAmount
		{
			get
			{
				return base.GetSystemDecimal(InvoiceSupplierItemMetadata.ColumnNames.PphAmount);
			}
			
			set
			{
				base.SetSystemDecimal(InvoiceSupplierItemMetadata.ColumnNames.PphAmount, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.SRItemType
		/// </summary>
		virtual public System.String SRItemType
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierItemMetadata.ColumnNames.SRItemType);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierItemMetadata.ColumnNames.SRItemType, value);
			}
		}
		/// <summary>
		/// Maps to InvoiceSupplierItem.IsPpnExcluded
		/// </summary>
		virtual public System.Boolean? IsPpnExcluded
		{
			get
			{
				return base.GetSystemBoolean(InvoiceSupplierItemMetadata.ColumnNames.IsPpnExcluded);
			}
			
			set
			{
				base.SetSystemBoolean(InvoiceSupplierItemMetadata.ColumnNames.IsPpnExcluded, value);
			}
		}
        /// <summary>
        /// Maps to InvoiceSupplierItem.RoundingAmount
        /// </summary>
        virtual public System.Decimal? RoundingAmount
        {
            get
            {
                return base.GetSystemDecimal(InvoiceSupplierItemMetadata.ColumnNames.RoundingAmount);
            }

            set
            {
                base.SetSystemDecimal(InvoiceSupplierItemMetadata.ColumnNames.RoundingAmount, value);
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
        [BrowsableAttribute( false )]		
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
			public esStrings(esInvoiceSupplierItem entity)
			{
				this.entity = entity;
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
			public System.String VerifyAmount
			{
				get
				{
					System.Decimal? data = entity.VerifyAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerifyAmount = null;
					else entity.VerifyAmount = Convert.ToDecimal(value);
				}
			}
			public System.String PaymentAmount
			{
				get
				{
					System.Decimal? data = entity.PaymentAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentAmount = null;
					else entity.PaymentAmount = Convert.ToDecimal(value);
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
			public System.String AccountID
			{
				get
				{
					System.String data = entity.AccountID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AccountID = null;
					else entity.AccountID = Convert.ToString(value);
				}
			}
			public System.String Amount
			{
				get
				{
					System.Decimal? data = entity.Amount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Amount = null;
					else entity.Amount = Convert.ToDecimal(value);
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
			public System.String VoucherID
			{
				get
				{
					System.String data = entity.VoucherID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoucherID = null;
					else entity.VoucherID = Convert.ToString(value);
				}
			}
			public System.String AgingDate
			{
				get
				{
					System.DateTime? data = entity.AgingDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AgingDate = null;
					else entity.AgingDate = Convert.ToDateTime(value);
				}
			}
			public System.String SRInvoicePayment
			{
				get
				{
					System.String data = entity.SRInvoicePayment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRInvoicePayment = null;
					else entity.SRInvoicePayment = Convert.ToString(value);
				}
			}
			public System.String BankID
			{
				get
				{
					System.String data = entity.BankID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BankID = null;
					else entity.BankID = Convert.ToString(value);
				}
			}
			public System.String BankAccountNo
			{
				get
				{
					System.String data = entity.BankAccountNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BankAccountNo = null;
					else entity.BankAccountNo = Convert.ToString(value);
				}
			}
			public System.String VerifyDate
			{
				get
				{
					System.DateTime? data = entity.VerifyDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerifyDate = null;
					else entity.VerifyDate = Convert.ToDateTime(value);
				}
			}
			public System.String VerifyByUserID
			{
				get
				{
					System.String data = entity.VerifyByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerifyByUserID = null;
					else entity.VerifyByUserID = Convert.ToString(value);
				}
			}
			public System.String PaymentDate
			{
				get
				{
					System.DateTime? data = entity.PaymentDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentDate = null;
					else entity.PaymentDate = Convert.ToDateTime(value);
				}
			}
			public System.String PaymentByUserID
			{
				get
				{
					System.String data = entity.PaymentByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentByUserID = null;
					else entity.PaymentByUserID = Convert.ToString(value);
				}
			}
			public System.String IsPaymentApproved
			{
				get
				{
					System.Boolean? data = entity.IsPaymentApproved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPaymentApproved = null;
					else entity.IsPaymentApproved = Convert.ToBoolean(value);
				}
			}
			public System.String PaymentApprovedDate
			{
				get
				{
					System.DateTime? data = entity.PaymentApprovedDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentApprovedDate = null;
					else entity.PaymentApprovedDate = Convert.ToDateTime(value);
				}
			}
			public System.String PaymentApprovedByUserID
			{
				get
				{
					System.String data = entity.PaymentApprovedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentApprovedByUserID = null;
					else entity.PaymentApprovedByUserID = Convert.ToString(value);
				}
			}
			public System.String PPnAmount
			{
				get
				{
					System.Decimal? data = entity.PPnAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PPnAmount = null;
					else entity.PPnAmount = Convert.ToDecimal(value);
				}
			}
			public System.String PPh22Amount
			{
				get
				{
					System.Decimal? data = entity.PPh22Amount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PPh22Amount = null;
					else entity.PPh22Amount = Convert.ToDecimal(value);
				}
			}
			public System.String PPh23Amount
			{
				get
				{
					System.Decimal? data = entity.PPh23Amount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PPh23Amount = null;
					else entity.PPh23Amount = Convert.ToDecimal(value);
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
			public System.String InvoiceReferenceNo
			{
				get
				{
					System.String data = entity.InvoiceReferenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InvoiceReferenceNo = null;
					else entity.InvoiceReferenceNo = Convert.ToString(value);
				}
			}
			public System.String InvoiceSN
			{
				get
				{
					System.String data = entity.InvoiceSN;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InvoiceSN = null;
					else entity.InvoiceSN = Convert.ToString(value);
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
			public System.String OtherDeduction
			{
				get
				{
					System.Decimal? data = entity.OtherDeduction;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OtherDeduction = null;
					else entity.OtherDeduction = Convert.ToDecimal(value);
				}
			}
			public System.String IsAdditionalInvoice
			{
				get
				{
					System.Boolean? data = entity.IsAdditionalInvoice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAdditionalInvoice = null;
					else entity.IsAdditionalInvoice = Convert.ToBoolean(value);
				}
			}
			public System.String ChartOfAccountId
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountId = null;
					else entity.ChartOfAccountId = Convert.ToInt32(value);
				}
			}
			public System.String SubLedgerId
			{
				get
				{
					System.Int32? data = entity.SubLedgerId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubLedgerId = null;
					else entity.SubLedgerId = Convert.ToInt32(value);
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
			public System.String IsPpnExcluded
			{
				get
				{
					System.Boolean? data = entity.IsPpnExcluded;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPpnExcluded = null;
					else entity.IsPpnExcluded = Convert.ToBoolean(value);
				}
			}
            public System.String RoundingAmount
            {
                get
                {
                    System.Decimal? data = entity.RoundingAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RoundingAmount = null;
                    else entity.RoundingAmount = Convert.ToDecimal(value);
                }
            }
            private esInvoiceSupplierItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esInvoiceSupplierItemQuery query)
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
				throw new Exception("esInvoiceSupplierItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class InvoiceSupplierItem : esInvoiceSupplierItem
	{	
	}

	[Serializable]
	abstract public class esInvoiceSupplierItemQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return InvoiceSupplierItemMetadata.Meta();
			}
		}	
			
		public esQueryItem InvoiceNo
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.InvoiceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
			
		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem VerifyAmount
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.VerifyAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem PaymentAmount
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.PaymentAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
			
		public esQueryItem AccountID
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.AccountID, esSystemType.String);
			}
		} 
			
		public esQueryItem Amount
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.Amount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem VoucherID
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.VoucherID, esSystemType.String);
			}
		} 
			
		public esQueryItem AgingDate
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.AgingDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem SRInvoicePayment
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.SRInvoicePayment, esSystemType.String);
			}
		} 
			
		public esQueryItem BankID
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.BankID, esSystemType.String);
			}
		} 
			
		public esQueryItem BankAccountNo
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.BankAccountNo, esSystemType.String);
			}
		} 
			
		public esQueryItem VerifyDate
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.VerifyDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem VerifyByUserID
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.VerifyByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem PaymentDate
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.PaymentDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem PaymentByUserID
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.PaymentByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsPaymentApproved
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.IsPaymentApproved, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem PaymentApprovedDate
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.PaymentApprovedDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem PaymentApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.PaymentApprovedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem PPnAmount
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.PPnAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem PPh22Amount
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.PPh22Amount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem PPh23Amount
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.PPh23Amount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CurrencyID
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.CurrencyID, esSystemType.String);
			}
		} 
			
		public esQueryItem CurrencyRate
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.CurrencyRate, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem StampAmount
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.StampAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem InvoiceReferenceNo
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.InvoiceReferenceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem InvoiceSN
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.InvoiceSN, esSystemType.String);
			}
		} 
			
		public esQueryItem TaxInvoiceDate
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.TaxInvoiceDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem OtherDeduction
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.OtherDeduction, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem IsAdditionalInvoice
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.IsAdditionalInvoice, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ChartOfAccountId
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.ChartOfAccountId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubLedgerId
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.SubLedgerId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem DownPaymentAmount
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.DownPaymentAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem SRPph
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.SRPph, esSystemType.String);
			}
		} 
			
		public esQueryItem PphPercentage
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.PphPercentage, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem PphAmount
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.PphAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem SRItemType
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.SRItemType, esSystemType.String);
			}
		} 
			
		public esQueryItem IsPpnExcluded
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.IsPpnExcluded, esSystemType.Boolean);
			}
		}

        public esQueryItem RoundingAmount
        {
            get
            {
                return new esQueryItem(this, InvoiceSupplierItemMetadata.ColumnNames.RoundingAmount, esSystemType.Decimal);
            }
        }
    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("InvoiceSupplierItemCollection")]
	public partial class InvoiceSupplierItemCollection : esInvoiceSupplierItemCollection, IEnumerable< InvoiceSupplierItem>
	{
		public InvoiceSupplierItemCollection()
		{

		}	
		
		public static implicit operator List< InvoiceSupplierItem>(InvoiceSupplierItemCollection coll)
		{
			List< InvoiceSupplierItem> list = new List< InvoiceSupplierItem>();
			
			foreach (InvoiceSupplierItem emp in coll)
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
				return  InvoiceSupplierItemMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new InvoiceSupplierItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new InvoiceSupplierItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new InvoiceSupplierItem();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public InvoiceSupplierItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new InvoiceSupplierItemQuery();
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
		public bool Load(InvoiceSupplierItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public InvoiceSupplierItem AddNew()
		{
			InvoiceSupplierItem entity = base.AddNewEntity() as InvoiceSupplierItem;
			
			return entity;		
		}
		public InvoiceSupplierItem FindByPrimaryKey(String invoiceNo, String transactionNo)
		{
			return base.FindByPrimaryKey(invoiceNo, transactionNo) as InvoiceSupplierItem;
		}

		#region IEnumerable< InvoiceSupplierItem> Members

		IEnumerator< InvoiceSupplierItem> IEnumerable< InvoiceSupplierItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as InvoiceSupplierItem;
			}
		}

		#endregion
		
		private InvoiceSupplierItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'InvoiceSupplierItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("InvoiceSupplierItem ({InvoiceNo, TransactionNo})")]
	[Serializable]
	public partial class InvoiceSupplierItem : esInvoiceSupplierItem
	{
		public InvoiceSupplierItem()
		{
		}	
	
		public InvoiceSupplierItem(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return InvoiceSupplierItemMetadata.Meta();
			}
		}	
	
		override protected esInvoiceSupplierItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new InvoiceSupplierItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public InvoiceSupplierItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new InvoiceSupplierItemQuery();
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
		public bool Load(InvoiceSupplierItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private InvoiceSupplierItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class InvoiceSupplierItemQuery : esInvoiceSupplierItemQuery
	{
		public InvoiceSupplierItemQuery()
		{

		}		
		
		public InvoiceSupplierItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "InvoiceSupplierItemQuery";
        }
	}

	[Serializable]
	public partial class InvoiceSupplierItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected InvoiceSupplierItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.InvoiceNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.InvoiceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.TransactionNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.TransactionDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.TransactionDate;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.VerifyAmount, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.VerifyAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.PaymentAmount, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.PaymentAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.Notes, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.AccountID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.AccountID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.Amount, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.Amount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.VoucherID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.VoucherID;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.AgingDate, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.AgingDate;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.SRInvoicePayment, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.SRInvoicePayment;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.BankID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.BankID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.BankAccountNo, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.BankAccountNo;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.VerifyDate, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.VerifyDate;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.VerifyByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.VerifyByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.PaymentDate, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.PaymentDate;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.PaymentByUserID, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.PaymentByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.IsPaymentApproved, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.IsPaymentApproved;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.PaymentApprovedDate, 20, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.PaymentApprovedDate;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.PaymentApprovedByUserID, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.PaymentApprovedByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.PPnAmount, 22, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.PPnAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.PPh22Amount, 23, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.PPh22Amount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.PPh23Amount, 24, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.PPh23Amount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.CurrencyID, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.CurrencyID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.CurrencyRate, 26, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.CurrencyRate;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.StampAmount, 27, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.StampAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.InvoiceReferenceNo, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.InvoiceReferenceNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.InvoiceSN, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.InvoiceSN;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.TaxInvoiceDate, 30, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.TaxInvoiceDate;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.OtherDeduction, 31, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.OtherDeduction;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.IsAdditionalInvoice, 32, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.IsAdditionalInvoice;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.ChartOfAccountId, 33, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.ChartOfAccountId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.SubLedgerId, 34, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.SubLedgerId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.DownPaymentAmount, 35, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.DownPaymentAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.SRPph, 36, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.SRPph;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.PphPercentage, 37, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.PphPercentage;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.PphAmount, 38, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.PphAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.SRItemType, 39, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.SRItemType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.IsPpnExcluded, 40, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.IsPpnExcluded;
			c.IsNullable = true;
			_columns.Add(c);

            c = new esColumnMetadata(InvoiceSupplierItemMetadata.ColumnNames.RoundingAmount, 7, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = InvoiceSupplierItemMetadata.PropertyNames.RoundingAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

        }
		#endregion
	
		static public InvoiceSupplierItemMetadata Meta()
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
			get	{ return base._columns; }
		}
		
		#region ColumnNames
		public class ColumnNames
		{ 
			public const string InvoiceNo = "InvoiceNo";
			public const string TransactionNo = "TransactionNo";
			public const string TransactionDate = "TransactionDate";
			public const string VerifyAmount = "VerifyAmount";
			public const string PaymentAmount = "PaymentAmount";
			public const string Notes = "Notes";
			public const string AccountID = "AccountID";
			public const string Amount = "Amount";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string VoucherID = "VoucherID";
			public const string AgingDate = "AgingDate";
			public const string SRInvoicePayment = "SRInvoicePayment";
			public const string BankID = "BankID";
			public const string BankAccountNo = "BankAccountNo";
			public const string VerifyDate = "VerifyDate";
			public const string VerifyByUserID = "VerifyByUserID";
			public const string PaymentDate = "PaymentDate";
			public const string PaymentByUserID = "PaymentByUserID";
			public const string IsPaymentApproved = "IsPaymentApproved";
			public const string PaymentApprovedDate = "PaymentApprovedDate";
			public const string PaymentApprovedByUserID = "PaymentApprovedByUserID";
			public const string PPnAmount = "PPnAmount";
			public const string PPh22Amount = "PPh22Amount";
			public const string PPh23Amount = "PPh23Amount";
			public const string CurrencyID = "CurrencyID";
			public const string CurrencyRate = "CurrencyRate";
			public const string StampAmount = "StampAmount";
			public const string InvoiceReferenceNo = "InvoiceReferenceNo";
			public const string InvoiceSN = "InvoiceSN";
			public const string TaxInvoiceDate = "TaxInvoiceDate";
			public const string OtherDeduction = "OtherDeduction";
			public const string IsAdditionalInvoice = "IsAdditionalInvoice";
			public const string ChartOfAccountId = "ChartOfAccountId";
			public const string SubLedgerId = "SubLedgerId";
			public const string DownPaymentAmount = "DownPaymentAmount";
			public const string SRPph = "SRPph";
			public const string PphPercentage = "PphPercentage";
			public const string PphAmount = "PphAmount";
			public const string SRItemType = "SRItemType";
			public const string IsPpnExcluded = "IsPpnExcluded";
			public const string RoundingAmount = "RoundingAmount";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string InvoiceNo = "InvoiceNo";
			public const string TransactionNo = "TransactionNo";
			public const string TransactionDate = "TransactionDate";
			public const string VerifyAmount = "VerifyAmount";
			public const string PaymentAmount = "PaymentAmount";
			public const string Notes = "Notes";
			public const string AccountID = "AccountID";
			public const string Amount = "Amount";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string VoucherID = "VoucherID";
			public const string AgingDate = "AgingDate";
			public const string SRInvoicePayment = "SRInvoicePayment";
			public const string BankID = "BankID";
			public const string BankAccountNo = "BankAccountNo";
			public const string VerifyDate = "VerifyDate";
			public const string VerifyByUserID = "VerifyByUserID";
			public const string PaymentDate = "PaymentDate";
			public const string PaymentByUserID = "PaymentByUserID";
			public const string IsPaymentApproved = "IsPaymentApproved";
			public const string PaymentApprovedDate = "PaymentApprovedDate";
			public const string PaymentApprovedByUserID = "PaymentApprovedByUserID";
			public const string PPnAmount = "PPnAmount";
			public const string PPh22Amount = "PPh22Amount";
			public const string PPh23Amount = "PPh23Amount";
			public const string CurrencyID = "CurrencyID";
			public const string CurrencyRate = "CurrencyRate";
			public const string StampAmount = "StampAmount";
			public const string InvoiceReferenceNo = "InvoiceReferenceNo";
			public const string InvoiceSN = "InvoiceSN";
			public const string TaxInvoiceDate = "TaxInvoiceDate";
			public const string OtherDeduction = "OtherDeduction";
			public const string IsAdditionalInvoice = "IsAdditionalInvoice";
			public const string ChartOfAccountId = "ChartOfAccountId";
			public const string SubLedgerId = "SubLedgerId";
			public const string DownPaymentAmount = "DownPaymentAmount";
			public const string SRPph = "SRPph";
			public const string PphPercentage = "PphPercentage";
			public const string PphAmount = "PphAmount";
			public const string SRItemType = "SRItemType";
			public const string IsPpnExcluded = "IsPpnExcluded";
            public const string RoundingAmount = "RoundingAmount";
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
			lock (typeof(InvoiceSupplierItemMetadata))
			{
				if(InvoiceSupplierItemMetadata.mapDelegates == null)
				{
					InvoiceSupplierItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (InvoiceSupplierItemMetadata.meta == null)
				{
					InvoiceSupplierItemMetadata.meta = new InvoiceSupplierItemMetadata();
				}
				
				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}			

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if(!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();
				
				meta.AddTypeMap("InvoiceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VerifyAmount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("PaymentAmount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AccountID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Amount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VoucherID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AgingDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRInvoicePayment", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BankID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BankAccountNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VerifyDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VerifyByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PaymentByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPaymentApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PaymentApprovedDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PaymentApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PPnAmount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("PPh22Amount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("PPh23Amount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CurrencyID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CurrencyRate", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("StampAmount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("InvoiceReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InvoiceSN", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TaxInvoiceDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("OtherDeduction", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsAdditionalInvoice", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ChartOfAccountId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubLedgerId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DownPaymentAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRPph", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PphPercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PphAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRItemType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPpnExcluded", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("RoundingAmount", new esTypeMap("numeric", "System.Decimal"));
		

				meta.Source = "InvoiceSupplierItem";
				meta.Destination = "InvoiceSupplierItem";
				meta.spInsert = "proc_InvoiceSupplierItemInsert";				
				meta.spUpdate = "proc_InvoiceSupplierItemUpdate";		
				meta.spDelete = "proc_InvoiceSupplierItemDelete";
				meta.spLoadAll = "proc_InvoiceSupplierItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_InvoiceSupplierItemLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private InvoiceSupplierItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
