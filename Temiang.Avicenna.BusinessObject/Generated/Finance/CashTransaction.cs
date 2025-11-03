/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/4/2021 8:07:38 AM
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
	abstract public class esCashTransactionCollection : esEntityCollectionWAuditLog
	{
		public esCashTransactionCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "CashTransactionCollection";
		}

		#region Query Logic
		protected void InitQuery(esCashTransactionQuery query)
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
			this.InitQuery(query as esCashTransactionQuery);
		}
		#endregion
		
		virtual public CashTransaction DetachEntity(CashTransaction entity)
		{
			return base.DetachEntity(entity) as CashTransaction;
		}
		
		virtual public CashTransaction AttachEntity(CashTransaction entity)
		{
			return base.AttachEntity(entity) as CashTransaction;
		}
		
		virtual public void Combine(CashTransactionCollection collection)
		{
			base.Combine(collection);
		}
		
		new public CashTransaction this[int index]
		{
			get
			{
				return base[index] as CashTransaction;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CashTransaction);
		}
	}



	[Serializable]
	abstract public class esCashTransaction : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCashTransactionQuery GetDynamicQuery()
		{
			return null;
		}

		public esCashTransaction()
		{

		}

		public esCashTransaction(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 transactionId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionId);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionId);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 transactionId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionId);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionId);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 transactionId)
		{
			esCashTransactionQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionId == transactionId);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 transactionId)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionId",transactionId);
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
						case "TransactionId": this.str.TransactionId = (string)value; break;							
						case "PostingId": this.str.PostingId = (string)value; break;							
						case "BankId": this.str.BankId = (string)value; break;							
						case "ChartOfAccountId": this.str.ChartOfAccountId = (string)value; break;							
						case "TransactionDate": this.str.TransactionDate = (string)value; break;							
						case "TransactionType": this.str.TransactionType = (string)value; break;							
						case "PaymentType": this.str.PaymentType = (string)value; break;							
						case "PaymentMethod": this.str.PaymentMethod = (string)value; break;							
						case "NormalBalance": this.str.NormalBalance = (string)value; break;							
						case "Module": this.str.Module = (string)value; break;							
						case "CurrencyCode": this.str.CurrencyCode = (string)value; break;							
						case "CurrencyRate": this.str.CurrencyRate = (string)value; break;							
						case "IsPosted": this.str.IsPosted = (string)value; break;							
						case "IsCleared": this.str.IsCleared = (string)value; break;							
						case "IsVoid": this.str.IsVoid = (string)value; break;							
						case "ChequeNumber": this.str.ChequeNumber = (string)value; break;							
						case "DocumentNumber": this.str.DocumentNumber = (string)value; break;							
						case "Description": this.str.Description = (string)value; break;							
						case "JournalId": this.str.JournalId = (string)value; break;							
						case "VoidDate": this.str.VoidDate = (string)value; break;							
						case "DateCreated": this.str.DateCreated = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "CreatedBy": this.str.CreatedBy = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "JournalNumber": this.str.JournalNumber = (string)value; break;							
						case "ClearedDateTime": this.str.ClearedDateTime = (string)value; break;							
						case "ClearedBy": this.str.ClearedBy = (string)value; break;							
						case "DetailIdRef": this.str.DetailIdRef = (string)value; break;							
						case "DueDate": this.str.DueDate = (string)value; break;							
						case "BudgetingCode": this.str.BudgetingCode = (string)value; break;							
						case "ReceivedFromOrPaidTo": this.str.ReceivedFromOrPaidTo = (string)value; break;							
						case "IsAutoCashEntry": this.str.IsAutoCashEntry = (string)value; break;							
						case "BkuAccountID": this.str.BkuAccountID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "TransactionId":
						
							if (value == null || value is System.Int32)
								this.TransactionId = (System.Int32?)value;
							break;
						
						case "PostingId":
						
							if (value == null || value is System.Int32)
								this.PostingId = (System.Int32?)value;
							break;
						
						case "ChartOfAccountId":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountId = (System.Int32?)value;
							break;
						
						case "TransactionDate":
						
							if (value == null || value is System.DateTime)
								this.TransactionDate = (System.DateTime?)value;
							break;
						
						case "CurrencyRate":
						
							if (value == null || value is System.Decimal)
								this.CurrencyRate = (System.Decimal?)value;
							break;
						
						case "IsPosted":
						
							if (value == null || value is System.Boolean)
								this.IsPosted = (System.Boolean?)value;
							break;
						
						case "IsCleared":
						
							if (value == null || value is System.Boolean)
								this.IsCleared = (System.Boolean?)value;
							break;
						
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						
						case "JournalId":
						
							if (value == null || value is System.Int32)
								this.JournalId = (System.Int32?)value;
							break;
						
						case "VoidDate":
						
							if (value == null || value is System.DateTime)
								this.VoidDate = (System.DateTime?)value;
							break;
						
						case "DateCreated":
						
							if (value == null || value is System.DateTime)
								this.DateCreated = (System.DateTime?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "ClearedDateTime":
						
							if (value == null || value is System.DateTime)
								this.ClearedDateTime = (System.DateTime?)value;
							break;
						
						case "DetailIdRef":
						
							if (value == null || value is System.Int32)
								this.DetailIdRef = (System.Int32?)value;
							break;
						
						case "DueDate":
						
							if (value == null || value is System.DateTime)
								this.DueDate = (System.DateTime?)value;
							break;
						
						case "IsAutoCashEntry":
						
							if (value == null || value is System.Boolean)
								this.IsAutoCashEntry = (System.Boolean?)value;
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
		/// Maps to CashTransaction.TransactionId
		/// </summary>
		virtual public System.Int32? TransactionId
		{
			get
			{
				return base.GetSystemInt32(CashTransactionMetadata.ColumnNames.TransactionId);
			}
			
			set
			{
				base.SetSystemInt32(CashTransactionMetadata.ColumnNames.TransactionId, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.PostingId
		/// </summary>
		virtual public System.Int32? PostingId
		{
			get
			{
				return base.GetSystemInt32(CashTransactionMetadata.ColumnNames.PostingId);
			}
			
			set
			{
				base.SetSystemInt32(CashTransactionMetadata.ColumnNames.PostingId, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.BankId
		/// </summary>
		virtual public System.String BankId
		{
			get
			{
				return base.GetSystemString(CashTransactionMetadata.ColumnNames.BankId);
			}
			
			set
			{
				base.SetSystemString(CashTransactionMetadata.ColumnNames.BankId, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.ChartOfAccountId
		/// </summary>
		virtual public System.Int32? ChartOfAccountId
		{
			get
			{
				return base.GetSystemInt32(CashTransactionMetadata.ColumnNames.ChartOfAccountId);
			}
			
			set
			{
				base.SetSystemInt32(CashTransactionMetadata.ColumnNames.ChartOfAccountId, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(CashTransactionMetadata.ColumnNames.TransactionDate);
			}
			
			set
			{
				base.SetSystemDateTime(CashTransactionMetadata.ColumnNames.TransactionDate, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.TransactionType
		/// </summary>
		virtual public System.String TransactionType
		{
			get
			{
				return base.GetSystemString(CashTransactionMetadata.ColumnNames.TransactionType);
			}
			
			set
			{
				base.SetSystemString(CashTransactionMetadata.ColumnNames.TransactionType, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.PaymentType
		/// </summary>
		virtual public System.String PaymentType
		{
			get
			{
				return base.GetSystemString(CashTransactionMetadata.ColumnNames.PaymentType);
			}
			
			set
			{
				base.SetSystemString(CashTransactionMetadata.ColumnNames.PaymentType, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.PaymentMethod
		/// </summary>
		virtual public System.String PaymentMethod
		{
			get
			{
				return base.GetSystemString(CashTransactionMetadata.ColumnNames.PaymentMethod);
			}
			
			set
			{
				base.SetSystemString(CashTransactionMetadata.ColumnNames.PaymentMethod, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.NormalBalance
		/// </summary>
		virtual public System.String NormalBalance
		{
			get
			{
				return base.GetSystemString(CashTransactionMetadata.ColumnNames.NormalBalance);
			}
			
			set
			{
				base.SetSystemString(CashTransactionMetadata.ColumnNames.NormalBalance, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.Module
		/// </summary>
		virtual public System.String Module
		{
			get
			{
				return base.GetSystemString(CashTransactionMetadata.ColumnNames.Module);
			}
			
			set
			{
				base.SetSystemString(CashTransactionMetadata.ColumnNames.Module, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.CurrencyCode
		/// </summary>
		virtual public System.String CurrencyCode
		{
			get
			{
				return base.GetSystemString(CashTransactionMetadata.ColumnNames.CurrencyCode);
			}
			
			set
			{
				base.SetSystemString(CashTransactionMetadata.ColumnNames.CurrencyCode, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.CurrencyRate
		/// </summary>
		virtual public System.Decimal? CurrencyRate
		{
			get
			{
				return base.GetSystemDecimal(CashTransactionMetadata.ColumnNames.CurrencyRate);
			}
			
			set
			{
				base.SetSystemDecimal(CashTransactionMetadata.ColumnNames.CurrencyRate, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.IsPosted
		/// </summary>
		virtual public System.Boolean? IsPosted
		{
			get
			{
				return base.GetSystemBoolean(CashTransactionMetadata.ColumnNames.IsPosted);
			}
			
			set
			{
				base.SetSystemBoolean(CashTransactionMetadata.ColumnNames.IsPosted, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.IsCleared
		/// </summary>
		virtual public System.Boolean? IsCleared
		{
			get
			{
				return base.GetSystemBoolean(CashTransactionMetadata.ColumnNames.IsCleared);
			}
			
			set
			{
				base.SetSystemBoolean(CashTransactionMetadata.ColumnNames.IsCleared, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(CashTransactionMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(CashTransactionMetadata.ColumnNames.IsVoid, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.ChequeNumber
		/// </summary>
		virtual public System.String ChequeNumber
		{
			get
			{
				return base.GetSystemString(CashTransactionMetadata.ColumnNames.ChequeNumber);
			}
			
			set
			{
				base.SetSystemString(CashTransactionMetadata.ColumnNames.ChequeNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.DocumentNumber
		/// </summary>
		virtual public System.String DocumentNumber
		{
			get
			{
				return base.GetSystemString(CashTransactionMetadata.ColumnNames.DocumentNumber);
			}
			
			set
			{
				base.SetSystemString(CashTransactionMetadata.ColumnNames.DocumentNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.Description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(CashTransactionMetadata.ColumnNames.Description);
			}
			
			set
			{
				base.SetSystemString(CashTransactionMetadata.ColumnNames.Description, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.JournalId
		/// </summary>
		virtual public System.Int32? JournalId
		{
			get
			{
				return base.GetSystemInt32(CashTransactionMetadata.ColumnNames.JournalId);
			}
			
			set
			{
				base.SetSystemInt32(CashTransactionMetadata.ColumnNames.JournalId, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.VoidDate
		/// </summary>
		virtual public System.DateTime? VoidDate
		{
			get
			{
				return base.GetSystemDateTime(CashTransactionMetadata.ColumnNames.VoidDate);
			}
			
			set
			{
				base.SetSystemDateTime(CashTransactionMetadata.ColumnNames.VoidDate, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.DateCreated
		/// </summary>
		virtual public System.DateTime? DateCreated
		{
			get
			{
				return base.GetSystemDateTime(CashTransactionMetadata.ColumnNames.DateCreated);
			}
			
			set
			{
				base.SetSystemDateTime(CashTransactionMetadata.ColumnNames.DateCreated, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CashTransactionMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(CashTransactionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.CreatedBy
		/// </summary>
		virtual public System.String CreatedBy
		{
			get
			{
				return base.GetSystemString(CashTransactionMetadata.ColumnNames.CreatedBy);
			}
			
			set
			{
				base.SetSystemString(CashTransactionMetadata.ColumnNames.CreatedBy, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CashTransactionMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(CashTransactionMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.JournalNumber
		/// </summary>
		virtual public System.String JournalNumber
		{
			get
			{
				return base.GetSystemString(CashTransactionMetadata.ColumnNames.JournalNumber);
			}
			
			set
			{
				base.SetSystemString(CashTransactionMetadata.ColumnNames.JournalNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.ClearedDateTime
		/// </summary>
		virtual public System.DateTime? ClearedDateTime
		{
			get
			{
				return base.GetSystemDateTime(CashTransactionMetadata.ColumnNames.ClearedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(CashTransactionMetadata.ColumnNames.ClearedDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.ClearedBy
		/// </summary>
		virtual public System.String ClearedBy
		{
			get
			{
				return base.GetSystemString(CashTransactionMetadata.ColumnNames.ClearedBy);
			}
			
			set
			{
				base.SetSystemString(CashTransactionMetadata.ColumnNames.ClearedBy, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.DetailIdRef
		/// </summary>
		virtual public System.Int32? DetailIdRef
		{
			get
			{
				return base.GetSystemInt32(CashTransactionMetadata.ColumnNames.DetailIdRef);
			}
			
			set
			{
				base.SetSystemInt32(CashTransactionMetadata.ColumnNames.DetailIdRef, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.DueDate
		/// </summary>
		virtual public System.DateTime? DueDate
		{
			get
			{
				return base.GetSystemDateTime(CashTransactionMetadata.ColumnNames.DueDate);
			}
			
			set
			{
				base.SetSystemDateTime(CashTransactionMetadata.ColumnNames.DueDate, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.BudgetingCode
		/// </summary>
		virtual public System.String BudgetingCode
		{
			get
			{
				return base.GetSystemString(CashTransactionMetadata.ColumnNames.BudgetingCode);
			}
			
			set
			{
				base.SetSystemString(CashTransactionMetadata.ColumnNames.BudgetingCode, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.ReceivedFromOrPaidTo
		/// </summary>
		virtual public System.String ReceivedFromOrPaidTo
		{
			get
			{
				return base.GetSystemString(CashTransactionMetadata.ColumnNames.ReceivedFromOrPaidTo);
			}
			
			set
			{
				base.SetSystemString(CashTransactionMetadata.ColumnNames.ReceivedFromOrPaidTo, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.IsAutoCashEntry
		/// </summary>
		virtual public System.Boolean? IsAutoCashEntry
		{
			get
			{
				return base.GetSystemBoolean(CashTransactionMetadata.ColumnNames.IsAutoCashEntry);
			}
			
			set
			{
				base.SetSystemBoolean(CashTransactionMetadata.ColumnNames.IsAutoCashEntry, value);
			}
		}
		
		/// <summary>
		/// Maps to CashTransaction.BkuAccountID
		/// </summary>
		virtual public System.Int32? BkuAccountID
		{
			get
			{
				return base.GetSystemInt32(CashTransactionMetadata.ColumnNames.BkuAccountID);
			}
			
			set
			{
				base.SetSystemInt32(CashTransactionMetadata.ColumnNames.BkuAccountID, value);
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
			public esStrings(esCashTransaction entity)
			{
				this.entity = entity;
			}
			
	
			public System.String TransactionId
			{
				get
				{
					System.Int32? data = entity.TransactionId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionId = null;
					else entity.TransactionId = Convert.ToInt32(value);
				}
			}
				
			public System.String PostingId
			{
				get
				{
					System.Int32? data = entity.PostingId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PostingId = null;
					else entity.PostingId = Convert.ToInt32(value);
				}
			}
				
			public System.String BankId
			{
				get
				{
					System.String data = entity.BankId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BankId = null;
					else entity.BankId = Convert.ToString(value);
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
				
			public System.String TransactionType
			{
				get
				{
					System.String data = entity.TransactionType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionType = null;
					else entity.TransactionType = Convert.ToString(value);
				}
			}
				
			public System.String PaymentType
			{
				get
				{
					System.String data = entity.PaymentType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentType = null;
					else entity.PaymentType = Convert.ToString(value);
				}
			}
				
			public System.String PaymentMethod
			{
				get
				{
					System.String data = entity.PaymentMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentMethod = null;
					else entity.PaymentMethod = Convert.ToString(value);
				}
			}
				
			public System.String NormalBalance
			{
				get
				{
					System.String data = entity.NormalBalance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NormalBalance = null;
					else entity.NormalBalance = Convert.ToString(value);
				}
			}
				
			public System.String Module
			{
				get
				{
					System.String data = entity.Module;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Module = null;
					else entity.Module = Convert.ToString(value);
				}
			}
				
			public System.String CurrencyCode
			{
				get
				{
					System.String data = entity.CurrencyCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CurrencyCode = null;
					else entity.CurrencyCode = Convert.ToString(value);
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
				
			public System.String IsPosted
			{
				get
				{
					System.Boolean? data = entity.IsPosted;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPosted = null;
					else entity.IsPosted = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsCleared
			{
				get
				{
					System.Boolean? data = entity.IsCleared;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCleared = null;
					else entity.IsCleared = Convert.ToBoolean(value);
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
				
			public System.String ChequeNumber
			{
				get
				{
					System.String data = entity.ChequeNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChequeNumber = null;
					else entity.ChequeNumber = Convert.ToString(value);
				}
			}
				
			public System.String DocumentNumber
			{
				get
				{
					System.String data = entity.DocumentNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DocumentNumber = null;
					else entity.DocumentNumber = Convert.ToString(value);
				}
			}
				
			public System.String Description
			{
				get
				{
					System.String data = entity.Description;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Description = null;
					else entity.Description = Convert.ToString(value);
				}
			}
				
			public System.String JournalId
			{
				get
				{
					System.Int32? data = entity.JournalId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalId = null;
					else entity.JournalId = Convert.ToInt32(value);
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
				
			public System.String DateCreated
			{
				get
				{
					System.DateTime? data = entity.DateCreated;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateCreated = null;
					else entity.DateCreated = Convert.ToDateTime(value);
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
				
			public System.String CreatedBy
			{
				get
				{
					System.String data = entity.CreatedBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedBy = null;
					else entity.CreatedBy = Convert.ToString(value);
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
				
			public System.String JournalNumber
			{
				get
				{
					System.String data = entity.JournalNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalNumber = null;
					else entity.JournalNumber = Convert.ToString(value);
				}
			}
				
			public System.String ClearedDateTime
			{
				get
				{
					System.DateTime? data = entity.ClearedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClearedDateTime = null;
					else entity.ClearedDateTime = Convert.ToDateTime(value);
				}
			}
				
			public System.String ClearedBy
			{
				get
				{
					System.String data = entity.ClearedBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClearedBy = null;
					else entity.ClearedBy = Convert.ToString(value);
				}
			}
				
			public System.String DetailIdRef
			{
				get
				{
					System.Int32? data = entity.DetailIdRef;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DetailIdRef = null;
					else entity.DetailIdRef = Convert.ToInt32(value);
				}
			}
				
			public System.String DueDate
			{
				get
				{
					System.DateTime? data = entity.DueDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DueDate = null;
					else entity.DueDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String BudgetingCode
			{
				get
				{
					System.String data = entity.BudgetingCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BudgetingCode = null;
					else entity.BudgetingCode = Convert.ToString(value);
				}
			}
				
			public System.String ReceivedFromOrPaidTo
			{
				get
				{
					System.String data = entity.ReceivedFromOrPaidTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReceivedFromOrPaidTo = null;
					else entity.ReceivedFromOrPaidTo = Convert.ToString(value);
				}
			}
				
			public System.String IsAutoCashEntry
			{
				get
				{
					System.Boolean? data = entity.IsAutoCashEntry;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAutoCashEntry = null;
					else entity.IsAutoCashEntry = Convert.ToBoolean(value);
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
			

			private esCashTransaction entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCashTransactionQuery query)
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
				throw new Exception("esCashTransaction can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esCashTransactionQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CashTransactionMetadata.Meta();
			}
		}	
		

		public esQueryItem TransactionId
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.TransactionId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PostingId
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.PostingId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem BankId
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.BankId, esSystemType.String);
			}
		} 
		
		public esQueryItem ChartOfAccountId
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.ChartOfAccountId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem TransactionType
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.TransactionType, esSystemType.String);
			}
		} 
		
		public esQueryItem PaymentType
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.PaymentType, esSystemType.String);
			}
		} 
		
		public esQueryItem PaymentMethod
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.PaymentMethod, esSystemType.String);
			}
		} 
		
		public esQueryItem NormalBalance
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.NormalBalance, esSystemType.String);
			}
		} 
		
		public esQueryItem Module
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.Module, esSystemType.String);
			}
		} 
		
		public esQueryItem CurrencyCode
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.CurrencyCode, esSystemType.String);
			}
		} 
		
		public esQueryItem CurrencyRate
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.CurrencyRate, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsPosted
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.IsPosted, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsCleared
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.IsCleared, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ChequeNumber
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.ChequeNumber, esSystemType.String);
			}
		} 
		
		public esQueryItem DocumentNumber
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.DocumentNumber, esSystemType.String);
			}
		} 
		
		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.Description, esSystemType.String);
			}
		} 
		
		public esQueryItem JournalId
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.JournalId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem VoidDate
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.VoidDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem DateCreated
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.DateCreated, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem CreatedBy
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.CreatedBy, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem JournalNumber
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.JournalNumber, esSystemType.String);
			}
		} 
		
		public esQueryItem ClearedDateTime
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.ClearedDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ClearedBy
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.ClearedBy, esSystemType.String);
			}
		} 
		
		public esQueryItem DetailIdRef
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.DetailIdRef, esSystemType.Int32);
			}
		} 
		
		public esQueryItem DueDate
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.DueDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem BudgetingCode
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.BudgetingCode, esSystemType.String);
			}
		} 
		
		public esQueryItem ReceivedFromOrPaidTo
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.ReceivedFromOrPaidTo, esSystemType.String);
			}
		} 
		
		public esQueryItem IsAutoCashEntry
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.IsAutoCashEntry, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem BkuAccountID
		{
			get
			{
				return new esQueryItem(this, CashTransactionMetadata.ColumnNames.BkuAccountID, esSystemType.Int32);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CashTransactionCollection")]
	public partial class CashTransactionCollection : esCashTransactionCollection, IEnumerable<CashTransaction>
	{
		public CashTransactionCollection()
		{

		}
		
		public static implicit operator List<CashTransaction>(CashTransactionCollection coll)
		{
			List<CashTransaction> list = new List<CashTransaction>();
			
			foreach (CashTransaction emp in coll)
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
				return  CashTransactionMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CashTransactionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CashTransaction(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CashTransaction();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public CashTransactionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CashTransactionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(CashTransactionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public CashTransaction AddNew()
		{
			CashTransaction entity = base.AddNewEntity() as CashTransaction;
			
			return entity;
		}

		public CashTransaction FindByPrimaryKey(System.Int32 transactionId)
		{
			return base.FindByPrimaryKey(transactionId) as CashTransaction;
		}


		#region IEnumerable<CashTransaction> Members

		IEnumerator<CashTransaction> IEnumerable<CashTransaction>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as CashTransaction;
			}
		}

		#endregion
		
		private CashTransactionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CashTransaction' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("CashTransaction ({TransactionId})")]
	[Serializable]
	public partial class CashTransaction : esCashTransaction
	{
		public CashTransaction()
		{

		}
	
		public CashTransaction(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CashTransactionMetadata.Meta();
			}
		}
		
		
		
		override protected esCashTransactionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CashTransactionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public CashTransactionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CashTransactionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(CashTransactionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private CashTransactionQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class CashTransactionQuery : esCashTransactionQuery
	{
		public CashTransactionQuery()
		{

		}		
		
		public CashTransactionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "CashTransactionQuery";
        }
		
			
	}


	[Serializable]
	public partial class CashTransactionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CashTransactionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.TransactionId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CashTransactionMetadata.PropertyNames.TransactionId;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.PostingId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CashTransactionMetadata.PropertyNames.PostingId;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.BankId, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionMetadata.PropertyNames.BankId;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.ChartOfAccountId, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CashTransactionMetadata.PropertyNames.ChartOfAccountId;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.TransactionDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CashTransactionMetadata.PropertyNames.TransactionDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.TransactionType, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionMetadata.PropertyNames.TransactionType;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.PaymentType, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionMetadata.PropertyNames.PaymentType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.PaymentMethod, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionMetadata.PropertyNames.PaymentMethod;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.NormalBalance, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionMetadata.PropertyNames.NormalBalance;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.Module, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionMetadata.PropertyNames.Module;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.CurrencyCode, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionMetadata.PropertyNames.CurrencyCode;
			c.CharacterMaxLength = 3;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.CurrencyRate, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CashTransactionMetadata.PropertyNames.CurrencyRate;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.IsPosted, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CashTransactionMetadata.PropertyNames.IsPosted;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.IsCleared, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CashTransactionMetadata.PropertyNames.IsCleared;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.IsVoid, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CashTransactionMetadata.PropertyNames.IsVoid;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.ChequeNumber, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionMetadata.PropertyNames.ChequeNumber;
			c.CharacterMaxLength = 75;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.DocumentNumber, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionMetadata.PropertyNames.DocumentNumber;
			c.CharacterMaxLength = 125;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.Description, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 255;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.JournalId, 18, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CashTransactionMetadata.PropertyNames.JournalId;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.VoidDate, 19, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CashTransactionMetadata.PropertyNames.VoidDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.DateCreated, 20, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CashTransactionMetadata.PropertyNames.DateCreated;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.LastUpdateDateTime, 21, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CashTransactionMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.CreatedBy, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionMetadata.PropertyNames.CreatedBy;
			c.CharacterMaxLength = 25;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.LastUpdateByUserID, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 25;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.JournalNumber, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionMetadata.PropertyNames.JournalNumber;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.ClearedDateTime, 25, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CashTransactionMetadata.PropertyNames.ClearedDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.ClearedBy, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionMetadata.PropertyNames.ClearedBy;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.DetailIdRef, 27, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CashTransactionMetadata.PropertyNames.DetailIdRef;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.DueDate, 28, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CashTransactionMetadata.PropertyNames.DueDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.BudgetingCode, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionMetadata.PropertyNames.BudgetingCode;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.ReceivedFromOrPaidTo, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = CashTransactionMetadata.PropertyNames.ReceivedFromOrPaidTo;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.IsAutoCashEntry, 31, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CashTransactionMetadata.PropertyNames.IsAutoCashEntry;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CashTransactionMetadata.ColumnNames.BkuAccountID, 32, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CashTransactionMetadata.PropertyNames.BkuAccountID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public CashTransactionMetadata Meta()
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
			 public const string TransactionId = "TransactionId";
			 public const string PostingId = "PostingId";
			 public const string BankId = "BankId";
			 public const string ChartOfAccountId = "ChartOfAccountId";
			 public const string TransactionDate = "TransactionDate";
			 public const string TransactionType = "TransactionType";
			 public const string PaymentType = "PaymentType";
			 public const string PaymentMethod = "PaymentMethod";
			 public const string NormalBalance = "NormalBalance";
			 public const string Module = "Module";
			 public const string CurrencyCode = "CurrencyCode";
			 public const string CurrencyRate = "CurrencyRate";
			 public const string IsPosted = "IsPosted";
			 public const string IsCleared = "IsCleared";
			 public const string IsVoid = "IsVoid";
			 public const string ChequeNumber = "ChequeNumber";
			 public const string DocumentNumber = "DocumentNumber";
			 public const string Description = "Description";
			 public const string JournalId = "JournalId";
			 public const string VoidDate = "VoidDate";
			 public const string DateCreated = "DateCreated";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string CreatedBy = "CreatedBy";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string JournalNumber = "JournalNumber";
			 public const string ClearedDateTime = "ClearedDateTime";
			 public const string ClearedBy = "ClearedBy";
			 public const string DetailIdRef = "DetailIdRef";
			 public const string DueDate = "DueDate";
			 public const string BudgetingCode = "BudgetingCode";
			 public const string ReceivedFromOrPaidTo = "ReceivedFromOrPaidTo";
			 public const string IsAutoCashEntry = "IsAutoCashEntry";
			 public const string BkuAccountID = "BkuAccountID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string TransactionId = "TransactionId";
			 public const string PostingId = "PostingId";
			 public const string BankId = "BankId";
			 public const string ChartOfAccountId = "ChartOfAccountId";
			 public const string TransactionDate = "TransactionDate";
			 public const string TransactionType = "TransactionType";
			 public const string PaymentType = "PaymentType";
			 public const string PaymentMethod = "PaymentMethod";
			 public const string NormalBalance = "NormalBalance";
			 public const string Module = "Module";
			 public const string CurrencyCode = "CurrencyCode";
			 public const string CurrencyRate = "CurrencyRate";
			 public const string IsPosted = "IsPosted";
			 public const string IsCleared = "IsCleared";
			 public const string IsVoid = "IsVoid";
			 public const string ChequeNumber = "ChequeNumber";
			 public const string DocumentNumber = "DocumentNumber";
			 public const string Description = "Description";
			 public const string JournalId = "JournalId";
			 public const string VoidDate = "VoidDate";
			 public const string DateCreated = "DateCreated";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string CreatedBy = "CreatedBy";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string JournalNumber = "JournalNumber";
			 public const string ClearedDateTime = "ClearedDateTime";
			 public const string ClearedBy = "ClearedBy";
			 public const string DetailIdRef = "DetailIdRef";
			 public const string DueDate = "DueDate";
			 public const string BudgetingCode = "BudgetingCode";
			 public const string ReceivedFromOrPaidTo = "ReceivedFromOrPaidTo";
			 public const string IsAutoCashEntry = "IsAutoCashEntry";
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
			lock (typeof(CashTransactionMetadata))
			{
				if(CashTransactionMetadata.mapDelegates == null)
				{
					CashTransactionMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CashTransactionMetadata.meta == null)
				{
					CashTransactionMetadata.meta = new CashTransactionMetadata();
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
				

				meta.AddTypeMap("TransactionId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PostingId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("BankId", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChartOfAccountId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("TransactionType", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("PaymentType", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("PaymentMethod", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("NormalBalance", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Module", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("CurrencyCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CurrencyRate", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("IsPosted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCleared", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ChequeNumber", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("DocumentNumber", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("JournalId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("VoidDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("DateCreated", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedBy", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("JournalNumber", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("ClearedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ClearedBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DetailIdRef", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DueDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("BudgetingCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReceivedFromOrPaidTo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsAutoCashEntry", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("BkuAccountID", new esTypeMap("int", "System.Int32"));			
				
				
				
				meta.Source = "CashTransaction";
				meta.Destination = "CashTransaction";
				
				meta.spInsert = "proc_CashTransactionInsert";				
				meta.spUpdate = "proc_CashTransactionUpdate";		
				meta.spDelete = "proc_CashTransactionDelete";
				meta.spLoadAll = "proc_CashTransactionLoadAll";
				meta.spLoadByPrimaryKey = "proc_CashTransactionLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CashTransactionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
