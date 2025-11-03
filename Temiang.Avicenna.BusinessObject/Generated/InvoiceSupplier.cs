/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 12/7/2020 7:10:11 PM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;


using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;



namespace Temiang.Avicenna.BusinessObject
{

	[Serializable]
	abstract public class esInvoiceSupplierCollection : esEntityCollectionWAuditLog
	{
		public esInvoiceSupplierCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "InvoiceSupplierCollection";
		}

		#region Query Logic
		protected void InitQuery(esInvoiceSupplierQuery query)
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
			this.InitQuery(query as esInvoiceSupplierQuery);
		}
		#endregion
		
		virtual public InvoiceSupplier DetachEntity(InvoiceSupplier entity)
		{
			return base.DetachEntity(entity) as InvoiceSupplier;
		}
		
		virtual public InvoiceSupplier AttachEntity(InvoiceSupplier entity)
		{
			return base.AttachEntity(entity) as InvoiceSupplier;
		}
		
		virtual public void Combine(InvoiceSupplierCollection collection)
		{
			base.Combine(collection);
		}
		
		new public InvoiceSupplier this[int index]
		{
			get
			{
				return base[index] as InvoiceSupplier;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(InvoiceSupplier);
		}
	}



	[Serializable]
	abstract public class esInvoiceSupplier : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esInvoiceSupplierQuery GetDynamicQuery()
		{
			return null;
		}

		public esInvoiceSupplier()
		{

		}

		public esInvoiceSupplier(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String invoiceNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(invoiceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(invoiceNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String invoiceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(invoiceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(invoiceNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String invoiceNo)
		{
			esInvoiceSupplierQuery query = this.GetDynamicQuery();
			query.Where(query.InvoiceNo == invoiceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String invoiceNo)
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
						case "InvoiceSuppNo": this.str.InvoiceSuppNo = (string)value; break;							
						case "SupplierID": this.str.SupplierID = (string)value; break;							
						case "InvoiceDate": this.str.InvoiceDate = (string)value; break;							
						case "InvoiceDueDate": this.str.InvoiceDueDate = (string)value; break;							
						case "InvoiceTOP": this.str.InvoiceTOP = (string)value; break;							
						case "InvoiceNotes": this.str.InvoiceNotes = (string)value; break;							
						case "SRPayableStatus": this.str.SRPayableStatus = (string)value; break;							
						case "VoucherID": this.str.VoucherID = (string)value; break;							
						case "IsApproved": this.str.IsApproved = (string)value; break;							
						case "ApprovedDate": this.str.ApprovedDate = (string)value; break;							
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;							
						case "IsVoid": this.str.IsVoid = (string)value; break;							
						case "VoidDate": this.str.VoidDate = (string)value; break;							
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "SRInvoicePayment": this.str.SRInvoicePayment = (string)value; break;							
						case "BankID": this.str.BankID = (string)value; break;							
						case "BankAccountNo": this.str.BankAccountNo = (string)value; break;							
						case "VerifyDate": this.str.VerifyDate = (string)value; break;							
						case "VerifyByUserID": this.str.VerifyByUserID = (string)value; break;							
						case "IsInvoicePayment": this.str.IsInvoicePayment = (string)value; break;							
						case "InvoiceReferenceNo": this.str.InvoiceReferenceNo = (string)value; break;							
						case "IsPaymentApproved": this.str.IsPaymentApproved = (string)value; break;							
						case "PaymentApprovedDateTime": this.str.PaymentApprovedDateTime = (string)value; break;							
						case "PaymentApprovedByUserID": this.str.PaymentApprovedByUserID = (string)value; break;							
						case "IsConsignment": this.str.IsConsignment = (string)value; break;							
						case "IsWriteOff": this.str.IsWriteOff = (string)value; break;							
						case "Reason": this.str.Reason = (string)value; break;							
						case "IsAddPayment": this.str.IsAddPayment = (string)value; break;							
						case "InvoiceReceivedDate": this.str.InvoiceReceivedDate = (string)value; break;							
						case "CashTransactionReconcileId": this.str.CashTransactionReconcileId = (string)value; break;							
						case "InvoicePaymentPlanDate": this.str.InvoicePaymentPlanDate = (string)value; break;							
						case "PaymentOrderNo": this.str.PaymentOrderNo = (string)value; break;							
						case "BkuAccountID": this.str.BkuAccountID = (string)value; break;
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
						
						case "IsInvoicePayment":
						
							if (value == null || value is System.Boolean)
								this.IsInvoicePayment = (System.Boolean?)value;
							break;
						
						case "IsPaymentApproved":
						
							if (value == null || value is System.Boolean)
								this.IsPaymentApproved = (System.Boolean?)value;
							break;
						
						case "PaymentApprovedDateTime":
						
							if (value == null || value is System.DateTime)
								this.PaymentApprovedDateTime = (System.DateTime?)value;
							break;
						
						case "IsConsignment":
						
							if (value == null || value is System.Boolean)
								this.IsConsignment = (System.Boolean?)value;
							break;
						
						case "IsWriteOff":
						
							if (value == null || value is System.Boolean)
								this.IsWriteOff = (System.Boolean?)value;
							break;
						
						case "IsAddPayment":
						
							if (value == null || value is System.Boolean)
								this.IsAddPayment = (System.Boolean?)value;
							break;
						
						case "InvoiceReceivedDate":
						
							if (value == null || value is System.DateTime)
								this.InvoiceReceivedDate = (System.DateTime?)value;
							break;
						
						case "CashTransactionReconcileId":
						
							if (value == null || value is System.Int32)
								this.CashTransactionReconcileId = (System.Int32?)value;
							break;
						
						case "InvoicePaymentPlanDate":
						
							if (value == null || value is System.DateTime)
								this.InvoicePaymentPlanDate = (System.DateTime?)value;
							break;
						
						case "BkuAccountID":
						
							if (value == null || value is System.Int32)
								this.BkuAccountID = (System.Int32?)value;
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
		/// Maps to InvoiceSupplier.InvoiceNo
		/// </summary>
		virtual public System.String InvoiceNo
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierMetadata.ColumnNames.InvoiceNo);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierMetadata.ColumnNames.InvoiceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.InvoiceSuppNo
		/// </summary>
		virtual public System.String InvoiceSuppNo
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierMetadata.ColumnNames.InvoiceSuppNo);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierMetadata.ColumnNames.InvoiceSuppNo, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.SupplierID
		/// </summary>
		virtual public System.String SupplierID
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierMetadata.ColumnNames.SupplierID);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierMetadata.ColumnNames.SupplierID, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.InvoiceDate
		/// </summary>
		virtual public System.DateTime? InvoiceDate
		{
			get
			{
				return base.GetSystemDateTime(InvoiceSupplierMetadata.ColumnNames.InvoiceDate);
			}
			
			set
			{
				base.SetSystemDateTime(InvoiceSupplierMetadata.ColumnNames.InvoiceDate, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.InvoiceDueDate
		/// </summary>
		virtual public System.DateTime? InvoiceDueDate
		{
			get
			{
				return base.GetSystemDateTime(InvoiceSupplierMetadata.ColumnNames.InvoiceDueDate);
			}
			
			set
			{
				base.SetSystemDateTime(InvoiceSupplierMetadata.ColumnNames.InvoiceDueDate, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.InvoiceTOP
		/// </summary>
		virtual public System.Decimal? InvoiceTOP
		{
			get
			{
				return base.GetSystemDecimal(InvoiceSupplierMetadata.ColumnNames.InvoiceTOP);
			}
			
			set
			{
				base.SetSystemDecimal(InvoiceSupplierMetadata.ColumnNames.InvoiceTOP, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.InvoiceNotes
		/// </summary>
		virtual public System.String InvoiceNotes
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierMetadata.ColumnNames.InvoiceNotes);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierMetadata.ColumnNames.InvoiceNotes, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.SRPayableStatus
		/// </summary>
		virtual public System.String SRPayableStatus
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierMetadata.ColumnNames.SRPayableStatus);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierMetadata.ColumnNames.SRPayableStatus, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.VoucherID
		/// </summary>
		virtual public System.String VoucherID
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierMetadata.ColumnNames.VoucherID);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierMetadata.ColumnNames.VoucherID, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(InvoiceSupplierMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(InvoiceSupplierMetadata.ColumnNames.IsApproved, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.ApprovedDate
		/// </summary>
		virtual public System.DateTime? ApprovedDate
		{
			get
			{
				return base.GetSystemDateTime(InvoiceSupplierMetadata.ColumnNames.ApprovedDate);
			}
			
			set
			{
				base.SetSystemDateTime(InvoiceSupplierMetadata.ColumnNames.ApprovedDate, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierMetadata.ColumnNames.ApprovedByUserID);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(InvoiceSupplierMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(InvoiceSupplierMetadata.ColumnNames.IsVoid, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.VoidDate
		/// </summary>
		virtual public System.DateTime? VoidDate
		{
			get
			{
				return base.GetSystemDateTime(InvoiceSupplierMetadata.ColumnNames.VoidDate);
			}
			
			set
			{
				base.SetSystemDateTime(InvoiceSupplierMetadata.ColumnNames.VoidDate, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierMetadata.ColumnNames.VoidByUserID);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(InvoiceSupplierMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(InvoiceSupplierMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.SRInvoicePayment
		/// </summary>
		virtual public System.String SRInvoicePayment
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierMetadata.ColumnNames.SRInvoicePayment);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierMetadata.ColumnNames.SRInvoicePayment, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.BankID
		/// </summary>
		virtual public System.String BankID
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierMetadata.ColumnNames.BankID);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierMetadata.ColumnNames.BankID, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.BankAccountNo
		/// </summary>
		virtual public System.String BankAccountNo
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierMetadata.ColumnNames.BankAccountNo);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierMetadata.ColumnNames.BankAccountNo, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.VerifyDate
		/// </summary>
		virtual public System.DateTime? VerifyDate
		{
			get
			{
				return base.GetSystemDateTime(InvoiceSupplierMetadata.ColumnNames.VerifyDate);
			}
			
			set
			{
				base.SetSystemDateTime(InvoiceSupplierMetadata.ColumnNames.VerifyDate, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.VerifyByUserID
		/// </summary>
		virtual public System.String VerifyByUserID
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierMetadata.ColumnNames.VerifyByUserID);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierMetadata.ColumnNames.VerifyByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.IsInvoicePayment
		/// </summary>
		virtual public System.Boolean? IsInvoicePayment
		{
			get
			{
				return base.GetSystemBoolean(InvoiceSupplierMetadata.ColumnNames.IsInvoicePayment);
			}
			
			set
			{
				base.SetSystemBoolean(InvoiceSupplierMetadata.ColumnNames.IsInvoicePayment, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.InvoiceReferenceNo
		/// </summary>
		virtual public System.String InvoiceReferenceNo
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierMetadata.ColumnNames.InvoiceReferenceNo);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierMetadata.ColumnNames.InvoiceReferenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.IsPaymentApproved
		/// </summary>
		virtual public System.Boolean? IsPaymentApproved
		{
			get
			{
				return base.GetSystemBoolean(InvoiceSupplierMetadata.ColumnNames.IsPaymentApproved);
			}
			
			set
			{
				base.SetSystemBoolean(InvoiceSupplierMetadata.ColumnNames.IsPaymentApproved, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.PaymentApprovedDateTime
		/// </summary>
		virtual public System.DateTime? PaymentApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(InvoiceSupplierMetadata.ColumnNames.PaymentApprovedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(InvoiceSupplierMetadata.ColumnNames.PaymentApprovedDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.PaymentApprovedByUserID
		/// </summary>
		virtual public System.String PaymentApprovedByUserID
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierMetadata.ColumnNames.PaymentApprovedByUserID);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierMetadata.ColumnNames.PaymentApprovedByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.IsConsignment
		/// </summary>
		virtual public System.Boolean? IsConsignment
		{
			get
			{
				return base.GetSystemBoolean(InvoiceSupplierMetadata.ColumnNames.IsConsignment);
			}
			
			set
			{
				base.SetSystemBoolean(InvoiceSupplierMetadata.ColumnNames.IsConsignment, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.IsWriteOff
		/// </summary>
		virtual public System.Boolean? IsWriteOff
		{
			get
			{
				return base.GetSystemBoolean(InvoiceSupplierMetadata.ColumnNames.IsWriteOff);
			}
			
			set
			{
				base.SetSystemBoolean(InvoiceSupplierMetadata.ColumnNames.IsWriteOff, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.Reason
		/// </summary>
		virtual public System.String Reason
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierMetadata.ColumnNames.Reason);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierMetadata.ColumnNames.Reason, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.IsAddPayment
		/// </summary>
		virtual public System.Boolean? IsAddPayment
		{
			get
			{
				return base.GetSystemBoolean(InvoiceSupplierMetadata.ColumnNames.IsAddPayment);
			}
			
			set
			{
				base.SetSystemBoolean(InvoiceSupplierMetadata.ColumnNames.IsAddPayment, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.InvoiceReceivedDate
		/// </summary>
		virtual public System.DateTime? InvoiceReceivedDate
		{
			get
			{
				return base.GetSystemDateTime(InvoiceSupplierMetadata.ColumnNames.InvoiceReceivedDate);
			}
			
			set
			{
				base.SetSystemDateTime(InvoiceSupplierMetadata.ColumnNames.InvoiceReceivedDate, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.CashTransactionReconcileId
		/// </summary>
		virtual public System.Int32? CashTransactionReconcileId
		{
			get
			{
				return base.GetSystemInt32(InvoiceSupplierMetadata.ColumnNames.CashTransactionReconcileId);
			}
			
			set
			{
				base.SetSystemInt32(InvoiceSupplierMetadata.ColumnNames.CashTransactionReconcileId, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.InvoicePaymentPlanDate
		/// </summary>
		virtual public System.DateTime? InvoicePaymentPlanDate
		{
			get
			{
				return base.GetSystemDateTime(InvoiceSupplierMetadata.ColumnNames.InvoicePaymentPlanDate);
			}
			
			set
			{
				base.SetSystemDateTime(InvoiceSupplierMetadata.ColumnNames.InvoicePaymentPlanDate, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.PaymentOrderNo
		/// </summary>
		virtual public System.String PaymentOrderNo
		{
			get
			{
				return base.GetSystemString(InvoiceSupplierMetadata.ColumnNames.PaymentOrderNo);
			}
			
			set
			{
				base.SetSystemString(InvoiceSupplierMetadata.ColumnNames.PaymentOrderNo, value);
			}
		}
		
		/// <summary>
		/// Maps to InvoiceSupplier.BkuAccountID
		/// </summary>
		virtual public System.Int32? BkuAccountID
		{
			get
			{
				return base.GetSystemInt32(InvoiceSupplierMetadata.ColumnNames.BkuAccountID);
			}
			
			set
			{
				base.SetSystemInt32(InvoiceSupplierMetadata.ColumnNames.BkuAccountID, value);
			}
		}
		
		#endregion	

		#region String Properties


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
			public esStrings(esInvoiceSupplier entity)
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
				
			public System.String InvoiceSuppNo
			{
				get
				{
					System.String data = entity.InvoiceSuppNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InvoiceSuppNo = null;
					else entity.InvoiceSuppNo = Convert.ToString(value);
				}
			}
				
			public System.String SupplierID
			{
				get
				{
					System.String data = entity.SupplierID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SupplierID = null;
					else entity.SupplierID = Convert.ToString(value);
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
				
			public System.String SRPayableStatus
			{
				get
				{
					System.String data = entity.SRPayableStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPayableStatus = null;
					else entity.SRPayableStatus = Convert.ToString(value);
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
				
			public System.String PaymentApprovedDateTime
			{
				get
				{
					System.DateTime? data = entity.PaymentApprovedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentApprovedDateTime = null;
					else entity.PaymentApprovedDateTime = Convert.ToDateTime(value);
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
				
			public System.String InvoiceReceivedDate
			{
				get
				{
					System.DateTime? data = entity.InvoiceReceivedDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InvoiceReceivedDate = null;
					else entity.InvoiceReceivedDate = Convert.ToDateTime(value);
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
				
			public System.String InvoicePaymentPlanDate
			{
				get
				{
					System.DateTime? data = entity.InvoicePaymentPlanDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InvoicePaymentPlanDate = null;
					else entity.InvoicePaymentPlanDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String PaymentOrderNo
			{
				get
				{
					System.String data = entity.PaymentOrderNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentOrderNo = null;
					else entity.PaymentOrderNo = Convert.ToString(value);
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
			

			private esInvoiceSupplier entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esInvoiceSupplierQuery query)
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
				throw new Exception("esInvoiceSupplier can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esInvoiceSupplierQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return InvoiceSupplierMetadata.Meta();
			}
		}	
		

		public esQueryItem InvoiceNo
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.InvoiceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem InvoiceSuppNo
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.InvoiceSuppNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SupplierID
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.SupplierID, esSystemType.String);
			}
		} 
		
		public esQueryItem InvoiceDate
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.InvoiceDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem InvoiceDueDate
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.InvoiceDueDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem InvoiceTOP
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.InvoiceTOP, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem InvoiceNotes
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.InvoiceNotes, esSystemType.String);
			}
		} 
		
		public esQueryItem SRPayableStatus
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.SRPayableStatus, esSystemType.String);
			}
		} 
		
		public esQueryItem VoucherID
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.VoucherID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ApprovedDate
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.ApprovedDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem VoidDate
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.VoidDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRInvoicePayment
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.SRInvoicePayment, esSystemType.String);
			}
		} 
		
		public esQueryItem BankID
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.BankID, esSystemType.String);
			}
		} 
		
		public esQueryItem BankAccountNo
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.BankAccountNo, esSystemType.String);
			}
		} 
		
		public esQueryItem VerifyDate
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.VerifyDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem VerifyByUserID
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.VerifyByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsInvoicePayment
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.IsInvoicePayment, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem InvoiceReferenceNo
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.InvoiceReferenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem IsPaymentApproved
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.IsPaymentApproved, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem PaymentApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.PaymentApprovedDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem PaymentApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.PaymentApprovedByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsConsignment
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.IsConsignment, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsWriteOff
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.IsWriteOff, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem Reason
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.Reason, esSystemType.String);
			}
		} 
		
		public esQueryItem IsAddPayment
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.IsAddPayment, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem InvoiceReceivedDate
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.InvoiceReceivedDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem CashTransactionReconcileId
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.CashTransactionReconcileId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem InvoicePaymentPlanDate
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.InvoicePaymentPlanDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem PaymentOrderNo
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.PaymentOrderNo, esSystemType.String);
			}
		} 
		
		public esQueryItem BkuAccountID
		{
			get
			{
				return new esQueryItem(this, InvoiceSupplierMetadata.ColumnNames.BkuAccountID, esSystemType.Int32);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("InvoiceSupplierCollection")]
	public partial class InvoiceSupplierCollection : esInvoiceSupplierCollection, IEnumerable<InvoiceSupplier>
	{
		public InvoiceSupplierCollection()
		{

		}
		
		public static implicit operator List<InvoiceSupplier>(InvoiceSupplierCollection coll)
		{
			List<InvoiceSupplier> list = new List<InvoiceSupplier>();
			
			foreach (InvoiceSupplier emp in coll)
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
				return  InvoiceSupplierMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new InvoiceSupplierQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new InvoiceSupplier(row);
		}

		override protected esEntity CreateEntity()
		{
			return new InvoiceSupplier();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public InvoiceSupplierQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new InvoiceSupplierQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(InvoiceSupplierQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public InvoiceSupplier AddNew()
		{
			InvoiceSupplier entity = base.AddNewEntity() as InvoiceSupplier;
			
			return entity;
		}

		public InvoiceSupplier FindByPrimaryKey(System.String invoiceNo)
		{
			return base.FindByPrimaryKey(invoiceNo) as InvoiceSupplier;
		}


		#region IEnumerable<InvoiceSupplier> Members

		IEnumerator<InvoiceSupplier> IEnumerable<InvoiceSupplier>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as InvoiceSupplier;
			}
		}

		#endregion
		
		private InvoiceSupplierQuery query;
	}


	/// <summary>
	/// Encapsulates the 'InvoiceSupplier' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("InvoiceSupplier ({InvoiceNo})")]
	[Serializable]
	public partial class InvoiceSupplier : esInvoiceSupplier
	{
		public InvoiceSupplier()
		{

		}
	
		public InvoiceSupplier(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return InvoiceSupplierMetadata.Meta();
			}
		}
		
		
		
		override protected esInvoiceSupplierQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new InvoiceSupplierQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public InvoiceSupplierQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new InvoiceSupplierQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(InvoiceSupplierQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private InvoiceSupplierQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class InvoiceSupplierQuery : esInvoiceSupplierQuery
	{
		public InvoiceSupplierQuery()
		{

		}		
		
		public InvoiceSupplierQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "InvoiceSupplierQuery";
        }
		
			
	}


	[Serializable]
	public partial class InvoiceSupplierMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected InvoiceSupplierMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.InvoiceNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.InvoiceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.InvoiceSuppNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.InvoiceSuppNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.SupplierID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.SupplierID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.InvoiceDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.InvoiceDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.InvoiceDueDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.InvoiceDueDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.InvoiceTOP, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.InvoiceTOP;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.InvoiceNotes, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.InvoiceNotes;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.SRPayableStatus, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.SRPayableStatus;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.VoucherID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.VoucherID;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.IsApproved, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.IsApproved;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.ApprovedDate, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.ApprovedDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.ApprovedByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.IsVoid, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.IsVoid;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.VoidDate, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.VoidDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.VoidByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.LastUpdateDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.LastUpdateByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.SRInvoicePayment, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.SRInvoicePayment;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.BankID, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.BankID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.BankAccountNo, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.BankAccountNo;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.VerifyDate, 20, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.VerifyDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.VerifyByUserID, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.VerifyByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.IsInvoicePayment, 22, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.IsInvoicePayment;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.InvoiceReferenceNo, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.InvoiceReferenceNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.IsPaymentApproved, 24, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.IsPaymentApproved;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.PaymentApprovedDateTime, 25, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.PaymentApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.PaymentApprovedByUserID, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.PaymentApprovedByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.IsConsignment, 27, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.IsConsignment;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.IsWriteOff, 28, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.IsWriteOff;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.Reason, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.Reason;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.IsAddPayment, 30, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.IsAddPayment;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.InvoiceReceivedDate, 31, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.InvoiceReceivedDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.CashTransactionReconcileId, 32, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.CashTransactionReconcileId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.InvoicePaymentPlanDate, 33, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.InvoicePaymentPlanDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.PaymentOrderNo, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.PaymentOrderNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InvoiceSupplierMetadata.ColumnNames.BkuAccountID, 35, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = InvoiceSupplierMetadata.PropertyNames.BkuAccountID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public InvoiceSupplierMetadata Meta()
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
			 public const string InvoiceSuppNo = "InvoiceSuppNo";
			 public const string SupplierID = "SupplierID";
			 public const string InvoiceDate = "InvoiceDate";
			 public const string InvoiceDueDate = "InvoiceDueDate";
			 public const string InvoiceTOP = "InvoiceTOP";
			 public const string InvoiceNotes = "InvoiceNotes";
			 public const string SRPayableStatus = "SRPayableStatus";
			 public const string VoucherID = "VoucherID";
			 public const string IsApproved = "IsApproved";
			 public const string ApprovedDate = "ApprovedDate";
			 public const string ApprovedByUserID = "ApprovedByUserID";
			 public const string IsVoid = "IsVoid";
			 public const string VoidDate = "VoidDate";
			 public const string VoidByUserID = "VoidByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string SRInvoicePayment = "SRInvoicePayment";
			 public const string BankID = "BankID";
			 public const string BankAccountNo = "BankAccountNo";
			 public const string VerifyDate = "VerifyDate";
			 public const string VerifyByUserID = "VerifyByUserID";
			 public const string IsInvoicePayment = "IsInvoicePayment";
			 public const string InvoiceReferenceNo = "InvoiceReferenceNo";
			 public const string IsPaymentApproved = "IsPaymentApproved";
			 public const string PaymentApprovedDateTime = "PaymentApprovedDateTime";
			 public const string PaymentApprovedByUserID = "PaymentApprovedByUserID";
			 public const string IsConsignment = "IsConsignment";
			 public const string IsWriteOff = "IsWriteOff";
			 public const string Reason = "Reason";
			 public const string IsAddPayment = "IsAddPayment";
			 public const string InvoiceReceivedDate = "InvoiceReceivedDate";
			 public const string CashTransactionReconcileId = "CashTransactionReconcileId";
			 public const string InvoicePaymentPlanDate = "InvoicePaymentPlanDate";
			 public const string PaymentOrderNo = "PaymentOrderNo";
			 public const string BkuAccountID = "BkuAccountID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string InvoiceNo = "InvoiceNo";
			 public const string InvoiceSuppNo = "InvoiceSuppNo";
			 public const string SupplierID = "SupplierID";
			 public const string InvoiceDate = "InvoiceDate";
			 public const string InvoiceDueDate = "InvoiceDueDate";
			 public const string InvoiceTOP = "InvoiceTOP";
			 public const string InvoiceNotes = "InvoiceNotes";
			 public const string SRPayableStatus = "SRPayableStatus";
			 public const string VoucherID = "VoucherID";
			 public const string IsApproved = "IsApproved";
			 public const string ApprovedDate = "ApprovedDate";
			 public const string ApprovedByUserID = "ApprovedByUserID";
			 public const string IsVoid = "IsVoid";
			 public const string VoidDate = "VoidDate";
			 public const string VoidByUserID = "VoidByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string SRInvoicePayment = "SRInvoicePayment";
			 public const string BankID = "BankID";
			 public const string BankAccountNo = "BankAccountNo";
			 public const string VerifyDate = "VerifyDate";
			 public const string VerifyByUserID = "VerifyByUserID";
			 public const string IsInvoicePayment = "IsInvoicePayment";
			 public const string InvoiceReferenceNo = "InvoiceReferenceNo";
			 public const string IsPaymentApproved = "IsPaymentApproved";
			 public const string PaymentApprovedDateTime = "PaymentApprovedDateTime";
			 public const string PaymentApprovedByUserID = "PaymentApprovedByUserID";
			 public const string IsConsignment = "IsConsignment";
			 public const string IsWriteOff = "IsWriteOff";
			 public const string Reason = "Reason";
			 public const string IsAddPayment = "IsAddPayment";
			 public const string InvoiceReceivedDate = "InvoiceReceivedDate";
			 public const string CashTransactionReconcileId = "CashTransactionReconcileId";
			 public const string InvoicePaymentPlanDate = "InvoicePaymentPlanDate";
			 public const string PaymentOrderNo = "PaymentOrderNo";
			 public const string BkuAccountID = "BkuAccountID";
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
			lock (typeof(InvoiceSupplierMetadata))
			{
				if(InvoiceSupplierMetadata.mapDelegates == null)
				{
					InvoiceSupplierMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (InvoiceSupplierMetadata.meta == null)
				{
					InvoiceSupplierMetadata.meta = new InvoiceSupplierMetadata();
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
				meta.AddTypeMap("InvoiceSuppNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SupplierID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InvoiceDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("InvoiceDueDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("InvoiceTOP", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("InvoiceNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPayableStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VoucherID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRInvoicePayment", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BankID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BankAccountNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VerifyDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VerifyByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsInvoicePayment", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("InvoiceReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPaymentApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PaymentApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PaymentApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsConsignment", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsWriteOff", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Reason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsAddPayment", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("InvoiceReceivedDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CashTransactionReconcileId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("InvoicePaymentPlanDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PaymentOrderNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BkuAccountID", new esTypeMap("int", "System.Int32"));			
				
				
				
				meta.Source = "InvoiceSupplier";
				meta.Destination = "InvoiceSupplier";
				
				meta.spInsert = "proc_InvoiceSupplierInsert";				
				meta.spUpdate = "proc_InvoiceSupplierUpdate";		
				meta.spDelete = "proc_InvoiceSupplierDelete";
				meta.spLoadAll = "proc_InvoiceSupplierLoadAll";
				meta.spLoadByPrimaryKey = "proc_InvoiceSupplierLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private InvoiceSupplierMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
