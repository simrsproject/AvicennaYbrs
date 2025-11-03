/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 21/07/2023 10:57:14
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
	abstract public class esInvoicesCollection : esEntityCollectionWAuditLog
	{
		public esInvoicesCollection()
		{

		}
				
		
		protected override string GetCollectionName()
		{
			return "InvoicesCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esInvoicesQuery query)
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
			this.InitQuery(query as esInvoicesQuery);
		}
		#endregion
			
		virtual public Invoices DetachEntity(Invoices entity)
		{
			return base.DetachEntity(entity) as Invoices;
		}
		
		virtual public Invoices AttachEntity(Invoices entity)
		{
			return base.AttachEntity(entity) as Invoices;
		}
		
		virtual public void Combine(InvoicesCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Invoices this[int index]
		{
			get
			{
				return base[index] as Invoices;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Invoices);
		}
	}

	[Serializable]
	abstract public class esInvoices : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esInvoicesQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esInvoices()
		{
		}
	
		public esInvoices(DataRow row)
			: base(row)
		{
		}
				
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String invoiceNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(invoiceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(invoiceNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String invoiceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(invoiceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(invoiceNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String invoiceNo)
		{
			esInvoicesQuery query = this.GetDynamicQuery();
			query.Where(query.InvoiceNo == invoiceNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String invoiceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("InvoiceNo",invoiceNo);
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
						case "SRReceivableType": this.str.SRReceivableType = (string)value; break;
						case "InvoiceDate": this.str.InvoiceDate = (string)value; break;
						case "InvoiceDueDate": this.str.InvoiceDueDate = (string)value; break;
						case "InvoiceTOP": this.str.InvoiceTOP = (string)value; break;
						case "GuarantorID": this.str.GuarantorID = (string)value; break;
						case "SRInvoicePayment": this.str.SRInvoicePayment = (string)value; break;
						case "BankID": this.str.BankID = (string)value; break;
						case "BankAccountNo": this.str.BankAccountNo = (string)value; break;
						case "InvoiceNotes": this.str.InvoiceNotes = (string)value; break;
						case "PaymentDate": this.str.PaymentDate = (string)value; break;
						case "SRReceivableStatus": this.str.SRReceivableStatus = (string)value; break;
						case "VoucherID": this.str.VoucherID = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDate": this.str.ApprovedDate = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDate": this.str.VoidDate = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "VerifyDate": this.str.VerifyDate = (string)value; break;
						case "VerifyByUserID": this.str.VerifyByUserID = (string)value; break;
						case "PaymentByUserID": this.str.PaymentByUserID = (string)value; break;
						case "AgingDate": this.str.AgingDate = (string)value; break;
						case "IsPaymentApproved": this.str.IsPaymentApproved = (string)value; break;
						case "PaymentApprovedDate": this.str.PaymentApprovedDate = (string)value; break;
						case "PaymentApprovedByUserID": this.str.PaymentApprovedByUserID = (string)value; break;
						case "SRPaymentType": this.str.SRPaymentType = (string)value; break;
						case "SRPaymentMethod": this.str.SRPaymentMethod = (string)value; break;
						case "SRCardProvider": this.str.SRCardProvider = (string)value; break;
						case "SRCardType": this.str.SRCardType = (string)value; break;
						case "SRDiscountReason": this.str.SRDiscountReason = (string)value; break;
						case "EDCMachineID": this.str.EDCMachineID = (string)value; break;
						case "CardHolderName": this.str.CardHolderName = (string)value; break;
						case "CardFeeAmount": this.str.CardFeeAmount = (string)value; break;
						case "IsInvoicePayment": this.str.IsInvoicePayment = (string)value; break;
						case "InvoiceReferenceNo": this.str.InvoiceReferenceNo = (string)value; break;
						case "Reason": this.str.Reason = (string)value; break;
						case "IsWriteOff": this.str.IsWriteOff = (string)value; break;
						case "PrintReceiptAsName": this.str.PrintReceiptAsName = (string)value; break;
						case "IsAddPayment": this.str.IsAddPayment = (string)value; break;
						case "IsDiscountInPercantege": this.str.IsDiscountInPercantege = (string)value; break;
						case "DiscountPercentage": this.str.DiscountPercentage = (string)value; break;
						case "DiscountAmount": this.str.DiscountAmount = (string)value; break;
						case "TransferDate": this.str.TransferDate = (string)value; break;
						case "TransferNumber": this.str.TransferNumber = (string)value; break;
						case "IsAdditionalInvoice": this.str.IsAdditionalInvoice = (string)value; break;
						case "CashTransactionReconcileId": this.str.CashTransactionReconcileId = (string)value; break;
						case "StartPeriod": this.str.StartPeriod = (string)value; break;
						case "EndPeriod": this.str.EndPeriod = (string)value; break;
						case "BkuAccountID": this.str.BkuAccountID = (string)value; break;
						case "VoidReason": this.str.VoidReason = (string)value; break;
						case "SRPhysicianFeeProportionalStatus": this.str.SRPhysicianFeeProportionalStatus = (string)value; break;
						case "PhysicianFeeProportionalPercentage": this.str.PhysicianFeeProportionalPercentage = (string)value; break;
						case "PhysicianFeeProportionalErrMessage": this.str.PhysicianFeeProportionalErrMessage = (string)value; break;
						case "RoundingAmount": this.str.RoundingAmount = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "InvoiceDate":
						
							if (value == null || value is System.DateTime)
								this.InvoiceDate = (System.DateTime?)value;
							break;
						case "InvoiceDueDate":
						
							if (value == null || value is System.DateTime)
								this.InvoiceDueDate = (System.DateTime?)value;
							break;
						case "InvoiceTOP":
						
							if (value == null || value is System.Decimal)
								this.InvoiceTOP = (System.Decimal?)value;
							break;
						case "PaymentDate":
						
							if (value == null || value is System.DateTime)
								this.PaymentDate = (System.DateTime?)value;
							break;
						case "IsApproved":
						
							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "ApprovedDate":
						
							if (value == null || value is System.DateTime)
								this.ApprovedDate = (System.DateTime?)value;
							break;
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "VoidDate":
						
							if (value == null || value is System.DateTime)
								this.VoidDate = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "VerifyDate":
						
							if (value == null || value is System.DateTime)
								this.VerifyDate = (System.DateTime?)value;
							break;
						case "AgingDate":
						
							if (value == null || value is System.DateTime)
								this.AgingDate = (System.DateTime?)value;
							break;
						case "IsPaymentApproved":
						
							if (value == null || value is System.Boolean)
								this.IsPaymentApproved = (System.Boolean?)value;
							break;
						case "PaymentApprovedDate":
						
							if (value == null || value is System.DateTime)
								this.PaymentApprovedDate = (System.DateTime?)value;
							break;
						case "CardFeeAmount":
						
							if (value == null || value is System.Decimal)
								this.CardFeeAmount = (System.Decimal?)value;
							break;
						case "IsInvoicePayment":
						
							if (value == null || value is System.Boolean)
								this.IsInvoicePayment = (System.Boolean?)value;
							break;
						case "IsWriteOff":
						
							if (value == null || value is System.Boolean)
								this.IsWriteOff = (System.Boolean?)value;
							break;
						case "IsAddPayment":
						
							if (value == null || value is System.Boolean)
								this.IsAddPayment = (System.Boolean?)value;
							break;
						case "IsDiscountInPercantege":
						
							if (value == null || value is System.Boolean)
								this.IsDiscountInPercantege = (System.Boolean?)value;
							break;
						case "DiscountPercentage":
						
							if (value == null || value is System.Decimal)
								this.DiscountPercentage = (System.Decimal?)value;
							break;
						case "DiscountAmount":
						
							if (value == null || value is System.Decimal)
								this.DiscountAmount = (System.Decimal?)value;
							break;
						case "TransferDate":
						
							if (value == null || value is System.DateTime)
								this.TransferDate = (System.DateTime?)value;
							break;
						case "IsAdditionalInvoice":
						
							if (value == null || value is System.Boolean)
								this.IsAdditionalInvoice = (System.Boolean?)value;
							break;
						case "CashTransactionReconcileId":
						
							if (value == null || value is System.Int32)
								this.CashTransactionReconcileId = (System.Int32?)value;
							break;
						case "StartPeriod":
						
							if (value == null || value is System.DateTime)
								this.StartPeriod = (System.DateTime?)value;
							break;
						case "EndPeriod":
						
							if (value == null || value is System.DateTime)
								this.EndPeriod = (System.DateTime?)value;
							break;
						case "BkuAccountID":
						
							if (value == null || value is System.Int32)
								this.BkuAccountID = (System.Int32?)value;
							break;
						case "PhysicianFeeProportionalPercentage":
						
							if (value == null || value is System.Int32)
								this.PhysicianFeeProportionalPercentage = (System.Int32?)value;
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
		/// Maps to Invoices.InvoiceNo
		/// </summary>
		virtual public System.String InvoiceNo
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.InvoiceNo);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.InvoiceNo, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.SRReceivableType
		/// </summary>
		virtual public System.String SRReceivableType
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.SRReceivableType);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.SRReceivableType, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.InvoiceDate
		/// </summary>
		virtual public System.DateTime? InvoiceDate
		{
			get
			{
				return base.GetSystemDateTime(InvoicesMetadata.ColumnNames.InvoiceDate);
			}
			
			set
			{
				base.SetSystemDateTime(InvoicesMetadata.ColumnNames.InvoiceDate, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.InvoiceDueDate
		/// </summary>
		virtual public System.DateTime? InvoiceDueDate
		{
			get
			{
				return base.GetSystemDateTime(InvoicesMetadata.ColumnNames.InvoiceDueDate);
			}
			
			set
			{
				base.SetSystemDateTime(InvoicesMetadata.ColumnNames.InvoiceDueDate, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.InvoiceTOP
		/// </summary>
		virtual public System.Decimal? InvoiceTOP
		{
			get
			{
				return base.GetSystemDecimal(InvoicesMetadata.ColumnNames.InvoiceTOP);
			}
			
			set
			{
				base.SetSystemDecimal(InvoicesMetadata.ColumnNames.InvoiceTOP, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.GuarantorID);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.GuarantorID, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.SRInvoicePayment
		/// </summary>
		virtual public System.String SRInvoicePayment
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.SRInvoicePayment);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.SRInvoicePayment, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.BankID
		/// </summary>
		virtual public System.String BankID
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.BankID);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.BankID, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.BankAccountNo
		/// </summary>
		virtual public System.String BankAccountNo
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.BankAccountNo);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.BankAccountNo, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.InvoiceNotes
		/// </summary>
		virtual public System.String InvoiceNotes
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.InvoiceNotes);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.InvoiceNotes, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.PaymentDate
		/// </summary>
		virtual public System.DateTime? PaymentDate
		{
			get
			{
				return base.GetSystemDateTime(InvoicesMetadata.ColumnNames.PaymentDate);
			}
			
			set
			{
				base.SetSystemDateTime(InvoicesMetadata.ColumnNames.PaymentDate, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.SRReceivableStatus
		/// </summary>
		virtual public System.String SRReceivableStatus
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.SRReceivableStatus);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.SRReceivableStatus, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.VoucherID
		/// </summary>
		virtual public System.String VoucherID
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.VoucherID);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.VoucherID, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(InvoicesMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(InvoicesMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.ApprovedDate
		/// </summary>
		virtual public System.DateTime? ApprovedDate
		{
			get
			{
				return base.GetSystemDateTime(InvoicesMetadata.ColumnNames.ApprovedDate);
			}
			
			set
			{
				base.SetSystemDateTime(InvoicesMetadata.ColumnNames.ApprovedDate, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.ApprovedByUserID);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(InvoicesMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(InvoicesMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.VoidDate
		/// </summary>
		virtual public System.DateTime? VoidDate
		{
			get
			{
				return base.GetSystemDateTime(InvoicesMetadata.ColumnNames.VoidDate);
			}
			
			set
			{
				base.SetSystemDateTime(InvoicesMetadata.ColumnNames.VoidDate, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.VoidByUserID);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(InvoicesMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(InvoicesMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.VerifyDate
		/// </summary>
		virtual public System.DateTime? VerifyDate
		{
			get
			{
				return base.GetSystemDateTime(InvoicesMetadata.ColumnNames.VerifyDate);
			}
			
			set
			{
				base.SetSystemDateTime(InvoicesMetadata.ColumnNames.VerifyDate, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.VerifyByUserID
		/// </summary>
		virtual public System.String VerifyByUserID
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.VerifyByUserID);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.VerifyByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.PaymentByUserID
		/// </summary>
		virtual public System.String PaymentByUserID
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.PaymentByUserID);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.PaymentByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.AgingDate
		/// </summary>
		virtual public System.DateTime? AgingDate
		{
			get
			{
				return base.GetSystemDateTime(InvoicesMetadata.ColumnNames.AgingDate);
			}
			
			set
			{
				base.SetSystemDateTime(InvoicesMetadata.ColumnNames.AgingDate, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.IsPaymentApproved
		/// </summary>
		virtual public System.Boolean? IsPaymentApproved
		{
			get
			{
				return base.GetSystemBoolean(InvoicesMetadata.ColumnNames.IsPaymentApproved);
			}
			
			set
			{
				base.SetSystemBoolean(InvoicesMetadata.ColumnNames.IsPaymentApproved, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.PaymentApprovedDate
		/// </summary>
		virtual public System.DateTime? PaymentApprovedDate
		{
			get
			{
				return base.GetSystemDateTime(InvoicesMetadata.ColumnNames.PaymentApprovedDate);
			}
			
			set
			{
				base.SetSystemDateTime(InvoicesMetadata.ColumnNames.PaymentApprovedDate, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.PaymentApprovedByUserID
		/// </summary>
		virtual public System.String PaymentApprovedByUserID
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.PaymentApprovedByUserID);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.PaymentApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.SRPaymentType
		/// </summary>
		virtual public System.String SRPaymentType
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.SRPaymentType);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.SRPaymentType, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.SRPaymentMethod
		/// </summary>
		virtual public System.String SRPaymentMethod
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.SRPaymentMethod);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.SRPaymentMethod, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.SRCardProvider
		/// </summary>
		virtual public System.String SRCardProvider
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.SRCardProvider);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.SRCardProvider, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.SRCardType
		/// </summary>
		virtual public System.String SRCardType
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.SRCardType);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.SRCardType, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.SRDiscountReason
		/// </summary>
		virtual public System.String SRDiscountReason
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.SRDiscountReason);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.SRDiscountReason, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.EDCMachineID
		/// </summary>
		virtual public System.String EDCMachineID
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.EDCMachineID);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.EDCMachineID, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.CardHolderName
		/// </summary>
		virtual public System.String CardHolderName
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.CardHolderName);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.CardHolderName, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.CardFeeAmount
		/// </summary>
		virtual public System.Decimal? CardFeeAmount
		{
			get
			{
				return base.GetSystemDecimal(InvoicesMetadata.ColumnNames.CardFeeAmount);
			}
			
			set
			{
				base.SetSystemDecimal(InvoicesMetadata.ColumnNames.CardFeeAmount, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.IsInvoicePayment
		/// </summary>
		virtual public System.Boolean? IsInvoicePayment
		{
			get
			{
				return base.GetSystemBoolean(InvoicesMetadata.ColumnNames.IsInvoicePayment);
			}
			
			set
			{
				base.SetSystemBoolean(InvoicesMetadata.ColumnNames.IsInvoicePayment, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.InvoiceReferenceNo
		/// </summary>
		virtual public System.String InvoiceReferenceNo
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.InvoiceReferenceNo);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.InvoiceReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.Reason
		/// </summary>
		virtual public System.String Reason
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.Reason);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.Reason, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.IsWriteOff
		/// </summary>
		virtual public System.Boolean? IsWriteOff
		{
			get
			{
				return base.GetSystemBoolean(InvoicesMetadata.ColumnNames.IsWriteOff);
			}
			
			set
			{
				base.SetSystemBoolean(InvoicesMetadata.ColumnNames.IsWriteOff, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.PrintReceiptAsName
		/// </summary>
		virtual public System.String PrintReceiptAsName
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.PrintReceiptAsName);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.PrintReceiptAsName, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.IsAddPayment
		/// </summary>
		virtual public System.Boolean? IsAddPayment
		{
			get
			{
				return base.GetSystemBoolean(InvoicesMetadata.ColumnNames.IsAddPayment);
			}
			
			set
			{
				base.SetSystemBoolean(InvoicesMetadata.ColumnNames.IsAddPayment, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.IsDiscountInPercantege
		/// </summary>
		virtual public System.Boolean? IsDiscountInPercantege
		{
			get
			{
				return base.GetSystemBoolean(InvoicesMetadata.ColumnNames.IsDiscountInPercantege);
			}
			
			set
			{
				base.SetSystemBoolean(InvoicesMetadata.ColumnNames.IsDiscountInPercantege, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.DiscountPercentage
		/// </summary>
		virtual public System.Decimal? DiscountPercentage
		{
			get
			{
				return base.GetSystemDecimal(InvoicesMetadata.ColumnNames.DiscountPercentage);
			}
			
			set
			{
				base.SetSystemDecimal(InvoicesMetadata.ColumnNames.DiscountPercentage, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.DiscountAmount
		/// </summary>
		virtual public System.Decimal? DiscountAmount
		{
			get
			{
				return base.GetSystemDecimal(InvoicesMetadata.ColumnNames.DiscountAmount);
			}
			
			set
			{
				base.SetSystemDecimal(InvoicesMetadata.ColumnNames.DiscountAmount, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.TransferDate
		/// </summary>
		virtual public System.DateTime? TransferDate
		{
			get
			{
				return base.GetSystemDateTime(InvoicesMetadata.ColumnNames.TransferDate);
			}
			
			set
			{
				base.SetSystemDateTime(InvoicesMetadata.ColumnNames.TransferDate, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.TransferNumber
		/// </summary>
		virtual public System.String TransferNumber
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.TransferNumber);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.TransferNumber, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.IsAdditionalInvoice
		/// </summary>
		virtual public System.Boolean? IsAdditionalInvoice
		{
			get
			{
				return base.GetSystemBoolean(InvoicesMetadata.ColumnNames.IsAdditionalInvoice);
			}
			
			set
			{
				base.SetSystemBoolean(InvoicesMetadata.ColumnNames.IsAdditionalInvoice, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.CashTransactionReconcileId
		/// </summary>
		virtual public System.Int32? CashTransactionReconcileId
		{
			get
			{
				return base.GetSystemInt32(InvoicesMetadata.ColumnNames.CashTransactionReconcileId);
			}
			
			set
			{
				base.SetSystemInt32(InvoicesMetadata.ColumnNames.CashTransactionReconcileId, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.StartPeriod
		/// </summary>
		virtual public System.DateTime? StartPeriod
		{
			get
			{
				return base.GetSystemDateTime(InvoicesMetadata.ColumnNames.StartPeriod);
			}
			
			set
			{
				base.SetSystemDateTime(InvoicesMetadata.ColumnNames.StartPeriod, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.EndPeriod
		/// </summary>
		virtual public System.DateTime? EndPeriod
		{
			get
			{
				return base.GetSystemDateTime(InvoicesMetadata.ColumnNames.EndPeriod);
			}
			
			set
			{
				base.SetSystemDateTime(InvoicesMetadata.ColumnNames.EndPeriod, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.BkuAccountID
		/// </summary>
		virtual public System.Int32? BkuAccountID
		{
			get
			{
				return base.GetSystemInt32(InvoicesMetadata.ColumnNames.BkuAccountID);
			}
			
			set
			{
				base.SetSystemInt32(InvoicesMetadata.ColumnNames.BkuAccountID, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.VoidReason
		/// </summary>
		virtual public System.String VoidReason
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.VoidReason);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.VoidReason, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.SRPhysicianFeeProportionalStatus
		/// </summary>
		virtual public System.String SRPhysicianFeeProportionalStatus
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.SRPhysicianFeeProportionalStatus);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.SRPhysicianFeeProportionalStatus, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.PhysicianFeeProportionalPercentage
		/// </summary>
		virtual public System.Int32? PhysicianFeeProportionalPercentage
		{
			get
			{
				return base.GetSystemInt32(InvoicesMetadata.ColumnNames.PhysicianFeeProportionalPercentage);
			}
			
			set
			{
				base.SetSystemInt32(InvoicesMetadata.ColumnNames.PhysicianFeeProportionalPercentage, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.PhysicianFeeProportionalErrMessage
		/// </summary>
		virtual public System.String PhysicianFeeProportionalErrMessage
		{
			get
			{
				return base.GetSystemString(InvoicesMetadata.ColumnNames.PhysicianFeeProportionalErrMessage);
			}
			
			set
			{
				base.SetSystemString(InvoicesMetadata.ColumnNames.PhysicianFeeProportionalErrMessage, value);
			}
		}
		/// <summary>
		/// Maps to Invoices.RoundingAmount
		/// </summary>
		virtual public System.Decimal? RoundingAmount
		{
			get
			{
				return base.GetSystemDecimal(InvoicesMetadata.ColumnNames.RoundingAmount);
			}
			
			set
			{
				base.SetSystemDecimal(InvoicesMetadata.ColumnNames.RoundingAmount, value);
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
			public esStrings(esInvoices entity)
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
			public System.String SRReceivableType
			{
				get
				{
					System.String data = entity.SRReceivableType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRReceivableType = null;
					else entity.SRReceivableType = Convert.ToString(value);
				}
			}
			public System.String InvoiceDate
			{
				get
				{
					System.DateTime? data = entity.InvoiceDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InvoiceDate = null;
					else entity.InvoiceDate = Convert.ToDateTime(value);
				}
			}
			public System.String InvoiceDueDate
			{
				get
				{
					System.DateTime? data = entity.InvoiceDueDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InvoiceDueDate = null;
					else entity.InvoiceDueDate = Convert.ToDateTime(value);
				}
			}
			public System.String InvoiceTOP
			{
				get
				{
					System.Decimal? data = entity.InvoiceTOP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InvoiceTOP = null;
					else entity.InvoiceTOP = Convert.ToDecimal(value);
				}
			}
			public System.String GuarantorID
			{
				get
				{
					System.String data = entity.GuarantorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorID = null;
					else entity.GuarantorID = Convert.ToString(value);
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
			public System.String InvoiceNotes
			{
				get
				{
					System.String data = entity.InvoiceNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InvoiceNotes = null;
					else entity.InvoiceNotes = Convert.ToString(value);
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
			public System.String SRReceivableStatus
			{
				get
				{
					System.String data = entity.SRReceivableStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRReceivableStatus = null;
					else entity.SRReceivableStatus = Convert.ToString(value);
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
			public System.String SRPaymentMethod
			{
				get
				{
					System.String data = entity.SRPaymentMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPaymentMethod = null;
					else entity.SRPaymentMethod = Convert.ToString(value);
				}
			}
			public System.String SRCardProvider
			{
				get
				{
					System.String data = entity.SRCardProvider;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCardProvider = null;
					else entity.SRCardProvider = Convert.ToString(value);
				}
			}
			public System.String SRCardType
			{
				get
				{
					System.String data = entity.SRCardType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCardType = null;
					else entity.SRCardType = Convert.ToString(value);
				}
			}
			public System.String SRDiscountReason
			{
				get
				{
					System.String data = entity.SRDiscountReason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDiscountReason = null;
					else entity.SRDiscountReason = Convert.ToString(value);
				}
			}
			public System.String EDCMachineID
			{
				get
				{
					System.String data = entity.EDCMachineID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EDCMachineID = null;
					else entity.EDCMachineID = Convert.ToString(value);
				}
			}
			public System.String CardHolderName
			{
				get
				{
					System.String data = entity.CardHolderName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CardHolderName = null;
					else entity.CardHolderName = Convert.ToString(value);
				}
			}
			public System.String CardFeeAmount
			{
				get
				{
					System.Decimal? data = entity.CardFeeAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CardFeeAmount = null;
					else entity.CardFeeAmount = Convert.ToDecimal(value);
				}
			}
			public System.String IsInvoicePayment
			{
				get
				{
					System.Boolean? data = entity.IsInvoicePayment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInvoicePayment = null;
					else entity.IsInvoicePayment = Convert.ToBoolean(value);
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
			public System.String Reason
			{
				get
				{
					System.String data = entity.Reason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Reason = null;
					else entity.Reason = Convert.ToString(value);
				}
			}
			public System.String IsWriteOff
			{
				get
				{
					System.Boolean? data = entity.IsWriteOff;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsWriteOff = null;
					else entity.IsWriteOff = Convert.ToBoolean(value);
				}
			}
			public System.String PrintReceiptAsName
			{
				get
				{
					System.String data = entity.PrintReceiptAsName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrintReceiptAsName = null;
					else entity.PrintReceiptAsName = Convert.ToString(value);
				}
			}
			public System.String IsAddPayment
			{
				get
				{
					System.Boolean? data = entity.IsAddPayment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAddPayment = null;
					else entity.IsAddPayment = Convert.ToBoolean(value);
				}
			}
			public System.String IsDiscountInPercantege
			{
				get
				{
					System.Boolean? data = entity.IsDiscountInPercantege;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDiscountInPercantege = null;
					else entity.IsDiscountInPercantege = Convert.ToBoolean(value);
				}
			}
			public System.String DiscountPercentage
			{
				get
				{
					System.Decimal? data = entity.DiscountPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiscountPercentage = null;
					else entity.DiscountPercentage = Convert.ToDecimal(value);
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
			public System.String TransferDate
			{
				get
				{
					System.DateTime? data = entity.TransferDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransferDate = null;
					else entity.TransferDate = Convert.ToDateTime(value);
				}
			}
			public System.String TransferNumber
			{
				get
				{
					System.String data = entity.TransferNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransferNumber = null;
					else entity.TransferNumber = Convert.ToString(value);
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
			public System.String CashTransactionReconcileId
			{
				get
				{
					System.Int32? data = entity.CashTransactionReconcileId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CashTransactionReconcileId = null;
					else entity.CashTransactionReconcileId = Convert.ToInt32(value);
				}
			}
			public System.String StartPeriod
			{
				get
				{
					System.DateTime? data = entity.StartPeriod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartPeriod = null;
					else entity.StartPeriod = Convert.ToDateTime(value);
				}
			}
			public System.String EndPeriod
			{
				get
				{
					System.DateTime? data = entity.EndPeriod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndPeriod = null;
					else entity.EndPeriod = Convert.ToDateTime(value);
				}
			}
			public System.String BkuAccountID
			{
				get
				{
					System.Int32? data = entity.BkuAccountID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BkuAccountID = null;
					else entity.BkuAccountID = Convert.ToInt32(value);
				}
			}
			public System.String VoidReason
			{
				get
				{
					System.String data = entity.VoidReason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidReason = null;
					else entity.VoidReason = Convert.ToString(value);
				}
			}
			public System.String SRPhysicianFeeProportionalStatus
			{
				get
				{
					System.String data = entity.SRPhysicianFeeProportionalStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPhysicianFeeProportionalStatus = null;
					else entity.SRPhysicianFeeProportionalStatus = Convert.ToString(value);
				}
			}
			public System.String PhysicianFeeProportionalPercentage
			{
				get
				{
					System.Int32? data = entity.PhysicianFeeProportionalPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PhysicianFeeProportionalPercentage = null;
					else entity.PhysicianFeeProportionalPercentage = Convert.ToInt32(value);
				}
			}
			public System.String PhysicianFeeProportionalErrMessage
			{
				get
				{
					System.String data = entity.PhysicianFeeProportionalErrMessage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PhysicianFeeProportionalErrMessage = null;
					else entity.PhysicianFeeProportionalErrMessage = Convert.ToString(value);
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
			private esInvoices entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esInvoicesQuery query)
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
				throw new Exception("esInvoices can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class Invoices : esInvoices
	{	
	}

	[Serializable]
	abstract public class esInvoicesQuery : esDynamicQuery
	{		
		
		override protected IMetadata Meta
		{
			get
			{
				return InvoicesMetadata.Meta();
			}
		}	
			
		public esQueryItem InvoiceNo
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.InvoiceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SRReceivableType
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.SRReceivableType, esSystemType.String);
			}
		} 
			
		public esQueryItem InvoiceDate
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.InvoiceDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem InvoiceDueDate
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.InvoiceDueDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem InvoiceTOP
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.InvoiceTOP, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		} 
			
		public esQueryItem SRInvoicePayment
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.SRInvoicePayment, esSystemType.String);
			}
		} 
			
		public esQueryItem BankID
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.BankID, esSystemType.String);
			}
		} 
			
		public esQueryItem BankAccountNo
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.BankAccountNo, esSystemType.String);
			}
		} 
			
		public esQueryItem InvoiceNotes
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.InvoiceNotes, esSystemType.String);
			}
		} 
			
		public esQueryItem PaymentDate
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.PaymentDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem SRReceivableStatus
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.SRReceivableStatus, esSystemType.String);
			}
		} 
			
		public esQueryItem VoucherID
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.VoucherID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ApprovedDate
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.ApprovedDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem VoidDate
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.VoidDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem VerifyDate
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.VerifyDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem VerifyByUserID
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.VerifyByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem PaymentByUserID
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.PaymentByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem AgingDate
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.AgingDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem IsPaymentApproved
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.IsPaymentApproved, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem PaymentApprovedDate
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.PaymentApprovedDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem PaymentApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.PaymentApprovedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem SRPaymentType
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.SRPaymentType, esSystemType.String);
			}
		} 
			
		public esQueryItem SRPaymentMethod
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.SRPaymentMethod, esSystemType.String);
			}
		} 
			
		public esQueryItem SRCardProvider
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.SRCardProvider, esSystemType.String);
			}
		} 
			
		public esQueryItem SRCardType
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.SRCardType, esSystemType.String);
			}
		} 
			
		public esQueryItem SRDiscountReason
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.SRDiscountReason, esSystemType.String);
			}
		} 
			
		public esQueryItem EDCMachineID
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.EDCMachineID, esSystemType.String);
			}
		} 
			
		public esQueryItem CardHolderName
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.CardHolderName, esSystemType.String);
			}
		} 
			
		public esQueryItem CardFeeAmount
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.CardFeeAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem IsInvoicePayment
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.IsInvoicePayment, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem InvoiceReferenceNo
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.InvoiceReferenceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem Reason
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.Reason, esSystemType.String);
			}
		} 
			
		public esQueryItem IsWriteOff
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.IsWriteOff, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem PrintReceiptAsName
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.PrintReceiptAsName, esSystemType.String);
			}
		} 
			
		public esQueryItem IsAddPayment
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.IsAddPayment, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsDiscountInPercantege
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.IsDiscountInPercantege, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem DiscountPercentage
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.DiscountPercentage, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem DiscountAmount
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.DiscountAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem TransferDate
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.TransferDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem TransferNumber
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.TransferNumber, esSystemType.String);
			}
		} 
			
		public esQueryItem IsAdditionalInvoice
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.IsAdditionalInvoice, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem CashTransactionReconcileId
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.CashTransactionReconcileId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem StartPeriod
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.StartPeriod, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem EndPeriod
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.EndPeriod, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem BkuAccountID
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.BkuAccountID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem VoidReason
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.VoidReason, esSystemType.String);
			}
		} 
			
		public esQueryItem SRPhysicianFeeProportionalStatus
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.SRPhysicianFeeProportionalStatus, esSystemType.String);
			}
		} 
			
		public esQueryItem PhysicianFeeProportionalPercentage
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.PhysicianFeeProportionalPercentage, esSystemType.Int32);
			}
		} 
			
		public esQueryItem PhysicianFeeProportionalErrMessage
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.PhysicianFeeProportionalErrMessage, esSystemType.String);
			}
		} 
			
		public esQueryItem RoundingAmount
		{
			get
			{
				return new esQueryItem(this, InvoicesMetadata.ColumnNames.RoundingAmount, esSystemType.Decimal);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("InvoicesCollection")]
	public partial class InvoicesCollection : esInvoicesCollection, IEnumerable< Invoices>
	{
		public InvoicesCollection()
		{

		}	
		
		public static implicit operator List< Invoices>(InvoicesCollection coll)
		{
			List< Invoices> list = new List< Invoices>();
			
			foreach (Invoices emp in coll)
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
				return  InvoicesMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new InvoicesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Invoices(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Invoices();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public InvoicesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new InvoicesQuery();
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
		public bool Load(InvoicesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public Invoices AddNew()
		{
			Invoices entity = base.AddNewEntity() as Invoices;
			
			return entity;		
		}
		public Invoices FindByPrimaryKey(String invoiceNo)
		{
			return base.FindByPrimaryKey(invoiceNo) as Invoices;
		}

		#region IEnumerable< Invoices> Members

		IEnumerator< Invoices> IEnumerable< Invoices>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Invoices;
			}
		}

		#endregion
		
		private InvoicesQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Invoices' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("Invoices ({InvoiceNo})")]
	[Serializable]
	public partial class Invoices : esInvoices
	{
		public Invoices()
		{
		}	
	
		public Invoices(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return InvoicesMetadata.Meta();
			}
		}	
	
		override protected esInvoicesQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new InvoicesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public InvoicesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new InvoicesQuery();
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
		public bool Load(InvoicesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private InvoicesQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class InvoicesQuery : esInvoicesQuery
	{
		public InvoicesQuery()
		{

		}		
		
		public InvoicesQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "InvoicesQuery";
        }
	}

	[Serializable]
	public partial class InvoicesMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected InvoicesMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.InvoiceNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.InvoiceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.SRReceivableType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.SRReceivableType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.InvoiceDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoicesMetadata.PropertyNames.InvoiceDate;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.InvoiceDueDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoicesMetadata.PropertyNames.InvoiceDueDate;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.InvoiceTOP, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoicesMetadata.PropertyNames.InvoiceTOP;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.GuarantorID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.GuarantorID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.SRInvoicePayment, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.SRInvoicePayment;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.BankID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.BankID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.BankAccountNo, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.BankAccountNo;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.InvoiceNotes, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.InvoiceNotes;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.PaymentDate, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoicesMetadata.PropertyNames.PaymentDate;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.SRReceivableStatus, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.SRReceivableStatus;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.VoucherID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.VoucherID;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.IsApproved, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = InvoicesMetadata.PropertyNames.IsApproved;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.ApprovedDate, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoicesMetadata.PropertyNames.ApprovedDate;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.ApprovedByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.IsVoid, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = InvoicesMetadata.PropertyNames.IsVoid;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.VoidDate, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoicesMetadata.PropertyNames.VoidDate;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.VoidByUserID, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.LastUpdateDateTime, 19, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoicesMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.LastUpdateByUserID, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.VerifyDate, 21, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoicesMetadata.PropertyNames.VerifyDate;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.VerifyByUserID, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.VerifyByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.PaymentByUserID, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.PaymentByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.AgingDate, 24, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoicesMetadata.PropertyNames.AgingDate;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.IsPaymentApproved, 25, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = InvoicesMetadata.PropertyNames.IsPaymentApproved;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.PaymentApprovedDate, 26, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoicesMetadata.PropertyNames.PaymentApprovedDate;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.PaymentApprovedByUserID, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.PaymentApprovedByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.SRPaymentType, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.SRPaymentType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.SRPaymentMethod, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.SRPaymentMethod;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.SRCardProvider, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.SRCardProvider;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.SRCardType, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.SRCardType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.SRDiscountReason, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.SRDiscountReason;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.EDCMachineID, 33, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.EDCMachineID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.CardHolderName, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.CardHolderName;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.CardFeeAmount, 35, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoicesMetadata.PropertyNames.CardFeeAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.IsInvoicePayment, 36, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = InvoicesMetadata.PropertyNames.IsInvoicePayment;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.InvoiceReferenceNo, 37, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.InvoiceReferenceNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.Reason, 38, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.Reason;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.IsWriteOff, 39, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = InvoicesMetadata.PropertyNames.IsWriteOff;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.PrintReceiptAsName, 40, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.PrintReceiptAsName;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.IsAddPayment, 41, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = InvoicesMetadata.PropertyNames.IsAddPayment;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.IsDiscountInPercantege, 42, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = InvoicesMetadata.PropertyNames.IsDiscountInPercantege;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.DiscountPercentage, 43, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoicesMetadata.PropertyNames.DiscountPercentage;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.DiscountAmount, 44, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoicesMetadata.PropertyNames.DiscountAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.TransferDate, 45, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoicesMetadata.PropertyNames.TransferDate;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.TransferNumber, 46, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.TransferNumber;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.IsAdditionalInvoice, 47, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = InvoicesMetadata.PropertyNames.IsAdditionalInvoice;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.CashTransactionReconcileId, 48, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = InvoicesMetadata.PropertyNames.CashTransactionReconcileId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.StartPeriod, 49, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoicesMetadata.PropertyNames.StartPeriod;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.EndPeriod, 50, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoicesMetadata.PropertyNames.EndPeriod;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.BkuAccountID, 51, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = InvoicesMetadata.PropertyNames.BkuAccountID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.VoidReason, 52, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.VoidReason;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.SRPhysicianFeeProportionalStatus, 53, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.SRPhysicianFeeProportionalStatus;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.PhysicianFeeProportionalPercentage, 54, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = InvoicesMetadata.PropertyNames.PhysicianFeeProportionalPercentage;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.PhysicianFeeProportionalErrMessage, 55, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesMetadata.PropertyNames.PhysicianFeeProportionalErrMessage;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(InvoicesMetadata.ColumnNames.RoundingAmount, 56, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoicesMetadata.PropertyNames.RoundingAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public InvoicesMetadata Meta()
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
			public const string SRReceivableType = "SRReceivableType";
			public const string InvoiceDate = "InvoiceDate";
			public const string InvoiceDueDate = "InvoiceDueDate";
			public const string InvoiceTOP = "InvoiceTOP";
			public const string GuarantorID = "GuarantorID";
			public const string SRInvoicePayment = "SRInvoicePayment";
			public const string BankID = "BankID";
			public const string BankAccountNo = "BankAccountNo";
			public const string InvoiceNotes = "InvoiceNotes";
			public const string PaymentDate = "PaymentDate";
			public const string SRReceivableStatus = "SRReceivableStatus";
			public const string VoucherID = "VoucherID";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDate = "ApprovedDate";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDate = "VoidDate";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string VerifyDate = "VerifyDate";
			public const string VerifyByUserID = "VerifyByUserID";
			public const string PaymentByUserID = "PaymentByUserID";
			public const string AgingDate = "AgingDate";
			public const string IsPaymentApproved = "IsPaymentApproved";
			public const string PaymentApprovedDate = "PaymentApprovedDate";
			public const string PaymentApprovedByUserID = "PaymentApprovedByUserID";
			public const string SRPaymentType = "SRPaymentType";
			public const string SRPaymentMethod = "SRPaymentMethod";
			public const string SRCardProvider = "SRCardProvider";
			public const string SRCardType = "SRCardType";
			public const string SRDiscountReason = "SRDiscountReason";
			public const string EDCMachineID = "EDCMachineID";
			public const string CardHolderName = "CardHolderName";
			public const string CardFeeAmount = "CardFeeAmount";
			public const string IsInvoicePayment = "IsInvoicePayment";
			public const string InvoiceReferenceNo = "InvoiceReferenceNo";
			public const string Reason = "Reason";
			public const string IsWriteOff = "IsWriteOff";
			public const string PrintReceiptAsName = "PrintReceiptAsName";
			public const string IsAddPayment = "IsAddPayment";
			public const string IsDiscountInPercantege = "IsDiscountInPercantege";
			public const string DiscountPercentage = "DiscountPercentage";
			public const string DiscountAmount = "DiscountAmount";
			public const string TransferDate = "TransferDate";
			public const string TransferNumber = "TransferNumber";
			public const string IsAdditionalInvoice = "IsAdditionalInvoice";
			public const string CashTransactionReconcileId = "CashTransactionReconcileId";
			public const string StartPeriod = "StartPeriod";
			public const string EndPeriod = "EndPeriod";
			public const string BkuAccountID = "BkuAccountID";
			public const string VoidReason = "VoidReason";
			public const string SRPhysicianFeeProportionalStatus = "SRPhysicianFeeProportionalStatus";
			public const string PhysicianFeeProportionalPercentage = "PhysicianFeeProportionalPercentage";
			public const string PhysicianFeeProportionalErrMessage = "PhysicianFeeProportionalErrMessage";
			public const string RoundingAmount = "RoundingAmount";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string InvoiceNo = "InvoiceNo";
			public const string SRReceivableType = "SRReceivableType";
			public const string InvoiceDate = "InvoiceDate";
			public const string InvoiceDueDate = "InvoiceDueDate";
			public const string InvoiceTOP = "InvoiceTOP";
			public const string GuarantorID = "GuarantorID";
			public const string SRInvoicePayment = "SRInvoicePayment";
			public const string BankID = "BankID";
			public const string BankAccountNo = "BankAccountNo";
			public const string InvoiceNotes = "InvoiceNotes";
			public const string PaymentDate = "PaymentDate";
			public const string SRReceivableStatus = "SRReceivableStatus";
			public const string VoucherID = "VoucherID";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDate = "ApprovedDate";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDate = "VoidDate";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string VerifyDate = "VerifyDate";
			public const string VerifyByUserID = "VerifyByUserID";
			public const string PaymentByUserID = "PaymentByUserID";
			public const string AgingDate = "AgingDate";
			public const string IsPaymentApproved = "IsPaymentApproved";
			public const string PaymentApprovedDate = "PaymentApprovedDate";
			public const string PaymentApprovedByUserID = "PaymentApprovedByUserID";
			public const string SRPaymentType = "SRPaymentType";
			public const string SRPaymentMethod = "SRPaymentMethod";
			public const string SRCardProvider = "SRCardProvider";
			public const string SRCardType = "SRCardType";
			public const string SRDiscountReason = "SRDiscountReason";
			public const string EDCMachineID = "EDCMachineID";
			public const string CardHolderName = "CardHolderName";
			public const string CardFeeAmount = "CardFeeAmount";
			public const string IsInvoicePayment = "IsInvoicePayment";
			public const string InvoiceReferenceNo = "InvoiceReferenceNo";
			public const string Reason = "Reason";
			public const string IsWriteOff = "IsWriteOff";
			public const string PrintReceiptAsName = "PrintReceiptAsName";
			public const string IsAddPayment = "IsAddPayment";
			public const string IsDiscountInPercantege = "IsDiscountInPercantege";
			public const string DiscountPercentage = "DiscountPercentage";
			public const string DiscountAmount = "DiscountAmount";
			public const string TransferDate = "TransferDate";
			public const string TransferNumber = "TransferNumber";
			public const string IsAdditionalInvoice = "IsAdditionalInvoice";
			public const string CashTransactionReconcileId = "CashTransactionReconcileId";
			public const string StartPeriod = "StartPeriod";
			public const string EndPeriod = "EndPeriod";
			public const string BkuAccountID = "BkuAccountID";
			public const string VoidReason = "VoidReason";
			public const string SRPhysicianFeeProportionalStatus = "SRPhysicianFeeProportionalStatus";
			public const string PhysicianFeeProportionalPercentage = "PhysicianFeeProportionalPercentage";
			public const string PhysicianFeeProportionalErrMessage = "PhysicianFeeProportionalErrMessage";
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
			lock (typeof(InvoicesMetadata))
			{
				if(InvoicesMetadata.mapDelegates == null)
				{
					InvoicesMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (InvoicesMetadata.meta == null)
				{
					InvoicesMetadata.meta = new InvoicesMetadata();
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
				meta.AddTypeMap("SRReceivableType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InvoiceDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("InvoiceDueDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("InvoiceTOP", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRInvoicePayment", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BankID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BankAccountNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InvoiceNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRReceivableStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VoucherID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VerifyDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VerifyByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AgingDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsPaymentApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PaymentApprovedDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PaymentApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPaymentType", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("SRPaymentMethod", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("SRCardProvider", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("SRCardType", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("SRDiscountReason", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("EDCMachineID", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("CardHolderName", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("CardFeeAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsInvoicePayment", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("InvoiceReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Reason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsWriteOff", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PrintReceiptAsName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsAddPayment", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsDiscountInPercantege", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("DiscountPercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DiscountAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("TransferDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("TransferNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsAdditionalInvoice", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CashTransactionReconcileId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("StartPeriod", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("EndPeriod", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("BkuAccountID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("VoidReason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPhysicianFeeProportionalStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PhysicianFeeProportionalPercentage", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PhysicianFeeProportionalErrMessage", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RoundingAmount", new esTypeMap("decimal", "System.Decimal"));
		

				meta.Source = "Invoices";
				meta.Destination = "Invoices";
				meta.spInsert = "proc_InvoicesInsert";				
				meta.spUpdate = "proc_InvoicesUpdate";		
				meta.spDelete = "proc_InvoicesDelete";
				meta.spLoadAll = "proc_InvoicesLoadAll";
				meta.spLoadByPrimaryKey = "proc_InvoicesLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private InvoicesMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
