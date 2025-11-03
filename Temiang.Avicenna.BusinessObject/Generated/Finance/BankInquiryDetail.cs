/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/30/2021 1:56:56 PM
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
	abstract public class esBankInquiryDetailCollection : esEntityCollectionWAuditLog
	{
		public esBankInquiryDetailCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "BankInquiryDetailCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esBankInquiryDetailQuery query)
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
			this.InitQuery(query as esBankInquiryDetailQuery);
		}
		#endregion
			
		virtual public BankInquiryDetail DetachEntity(BankInquiryDetail entity)
		{
			return base.DetachEntity(entity) as BankInquiryDetail;
		}
		
		virtual public BankInquiryDetail AttachEntity(BankInquiryDetail entity)
		{
			return base.AttachEntity(entity) as BankInquiryDetail;
		}
		
		virtual public void Combine(BankInquiryDetailCollection collection)
		{
			base.Combine(collection);
		}
		
		new public BankInquiryDetail this[int index]
		{
			get
			{
				return base[index] as BankInquiryDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BankInquiryDetail);
		}
	}

	[Serializable]
	abstract public class esBankInquiryDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBankInquiryDetailQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esBankInquiryDetail()
		{
		}
	
		public esBankInquiryDetail(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 transactionID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 transactionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionID);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 transactionID)
		{
			esBankInquiryDetailQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionID==transactionID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 transactionID)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionID",transactionID);
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
						case "TransactionID": this.str.TransactionID = (string)value; break;
						case "InquiryID": this.str.InquiryID = (string)value; break;
						case "ReconcileID": this.str.ReconcileID = (string)value; break;
						case "RelatedTransactionNo": this.str.RelatedTransactionNo = (string)value; break;
						case "TransactionDateTime": this.str.TransactionDateTime = (string)value; break;
						case "Description": this.str.Description = (string)value; break;
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
						case "Debit": this.str.Debit = (string)value; break;
						case "Credit": this.str.Credit = (string)value; break;
						case "Balance": this.str.Balance = (string)value; break;
						case "SRCashTransactionCode": this.str.SRCashTransactionCode = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "TransactionID":
						
							if (value == null || value is System.Int32)
								this.TransactionID = (System.Int32?)value;
							break;
						case "InquiryID":
						
							if (value == null || value is System.Int32)
								this.InquiryID = (System.Int32?)value;
							break;
						case "ReconcileID":
						
							if (value == null || value is System.Int32)
								this.ReconcileID = (System.Int32?)value;
							break;
						case "TransactionDateTime":
						
							if (value == null || value is System.DateTime)
								this.TransactionDateTime = (System.DateTime?)value;
							break;
						case "Debit":
						
							if (value == null || value is System.Decimal)
								this.Debit = (System.Decimal?)value;
							break;
						case "Credit":
						
							if (value == null || value is System.Decimal)
								this.Credit = (System.Decimal?)value;
							break;
						case "Balance":
						
							if (value == null || value is System.Decimal)
								this.Balance = (System.Decimal?)value;
							break;
						case "CreatedDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
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
		/// Maps to BankInquiryDetail.TransactionID
		/// </summary>
		virtual public System.Int32? TransactionID
		{
			get
			{
				return base.GetSystemInt32(BankInquiryDetailMetadata.ColumnNames.TransactionID);
			}
			
			set
			{
				base.SetSystemInt32(BankInquiryDetailMetadata.ColumnNames.TransactionID, value);
			}
		}
		/// <summary>
		/// Maps to BankInquiryDetail.InquiryID
		/// </summary>
		virtual public System.Int32? InquiryID
		{
			get
			{
				return base.GetSystemInt32(BankInquiryDetailMetadata.ColumnNames.InquiryID);
			}
			
			set
			{
				base.SetSystemInt32(BankInquiryDetailMetadata.ColumnNames.InquiryID, value);
			}
		}
		/// <summary>
		/// Maps to BankInquiryDetail.ReconcileID
		/// </summary>
		virtual public System.Int32? ReconcileID
		{
			get
			{
				return base.GetSystemInt32(BankInquiryDetailMetadata.ColumnNames.ReconcileID);
			}
			
			set
			{
				base.SetSystemInt32(BankInquiryDetailMetadata.ColumnNames.ReconcileID, value);
			}
		}
		/// <summary>
		/// Maps to BankInquiryDetail.RelatedTransactionNo
		/// </summary>
		virtual public System.String RelatedTransactionNo
		{
			get
			{
				return base.GetSystemString(BankInquiryDetailMetadata.ColumnNames.RelatedTransactionNo);
			}
			
			set
			{
				base.SetSystemString(BankInquiryDetailMetadata.ColumnNames.RelatedTransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to BankInquiryDetail.TransactionDateTime
		/// </summary>
		virtual public System.DateTime? TransactionDateTime
		{
			get
			{
				return base.GetSystemDateTime(BankInquiryDetailMetadata.ColumnNames.TransactionDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BankInquiryDetailMetadata.ColumnNames.TransactionDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BankInquiryDetail.Description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(BankInquiryDetailMetadata.ColumnNames.Description);
			}
			
			set
			{
				base.SetSystemString(BankInquiryDetailMetadata.ColumnNames.Description, value);
			}
		}
		/// <summary>
		/// Maps to BankInquiryDetail.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(BankInquiryDetailMetadata.ColumnNames.ReferenceNo);
			}
			
			set
			{
				base.SetSystemString(BankInquiryDetailMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to BankInquiryDetail.Debit
		/// </summary>
		virtual public System.Decimal? Debit
		{
			get
			{
				return base.GetSystemDecimal(BankInquiryDetailMetadata.ColumnNames.Debit);
			}
			
			set
			{
				base.SetSystemDecimal(BankInquiryDetailMetadata.ColumnNames.Debit, value);
			}
		}
		/// <summary>
		/// Maps to BankInquiryDetail.Credit
		/// </summary>
		virtual public System.Decimal? Credit
		{
			get
			{
				return base.GetSystemDecimal(BankInquiryDetailMetadata.ColumnNames.Credit);
			}
			
			set
			{
				base.SetSystemDecimal(BankInquiryDetailMetadata.ColumnNames.Credit, value);
			}
		}
		/// <summary>
		/// Maps to BankInquiryDetail.Balance
		/// </summary>
		virtual public System.Decimal? Balance
		{
			get
			{
				return base.GetSystemDecimal(BankInquiryDetailMetadata.ColumnNames.Balance);
			}
			
			set
			{
				base.SetSystemDecimal(BankInquiryDetailMetadata.ColumnNames.Balance, value);
			}
		}
		/// <summary>
		/// Maps to BankInquiryDetail.SRCashTransactionCode
		/// </summary>
		virtual public System.String SRCashTransactionCode
		{
			get
			{
				return base.GetSystemString(BankInquiryDetailMetadata.ColumnNames.SRCashTransactionCode);
			}
			
			set
			{
				base.SetSystemString(BankInquiryDetailMetadata.ColumnNames.SRCashTransactionCode, value);
			}
		}
		/// <summary>
		/// Maps to BankInquiryDetail.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(BankInquiryDetailMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BankInquiryDetailMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BankInquiryDetail.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(BankInquiryDetailMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(BankInquiryDetailMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BankInquiryDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BankInquiryDetailMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BankInquiryDetailMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BankInquiryDetail.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BankInquiryDetailMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(BankInquiryDetailMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esBankInquiryDetail entity)
			{
				this.entity = entity;
			}
			public System.String TransactionID
			{
				get
				{
					System.Int32? data = entity.TransactionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionID = null;
					else entity.TransactionID = Convert.ToInt32(value);
				}
			}
			public System.String InquiryID
			{
				get
				{
					System.Int32? data = entity.InquiryID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InquiryID = null;
					else entity.InquiryID = Convert.ToInt32(value);
				}
			}
			public System.String ReconcileID
			{
				get
				{
					System.Int32? data = entity.ReconcileID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReconcileID = null;
					else entity.ReconcileID = Convert.ToInt32(value);
				}
			}
			public System.String RelatedTransactionNo
			{
				get
				{
					System.String data = entity.RelatedTransactionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RelatedTransactionNo = null;
					else entity.RelatedTransactionNo = Convert.ToString(value);
				}
			}
			public System.String TransactionDateTime
			{
				get
				{
					System.DateTime? data = entity.TransactionDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionDateTime = null;
					else entity.TransactionDateTime = Convert.ToDateTime(value);
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
			public System.String Debit
			{
				get
				{
					System.Decimal? data = entity.Debit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Debit = null;
					else entity.Debit = Convert.ToDecimal(value);
				}
			}
			public System.String Credit
			{
				get
				{
					System.Decimal? data = entity.Credit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Credit = null;
					else entity.Credit = Convert.ToDecimal(value);
				}
			}
			public System.String Balance
			{
				get
				{
					System.Decimal? data = entity.Balance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Balance = null;
					else entity.Balance = Convert.ToDecimal(value);
				}
			}
			public System.String SRCashTransactionCode
			{
				get
				{
					System.String data = entity.SRCashTransactionCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCashTransactionCode = null;
					else entity.SRCashTransactionCode = Convert.ToString(value);
				}
			}
			public System.String CreatedDateTime
			{
				get
				{
					System.DateTime? data = entity.CreatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedDateTime = null;
					else entity.CreatedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CreatedByUserID
			{
				get
				{
					System.String data = entity.CreatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedByUserID = null;
					else entity.CreatedByUserID = Convert.ToString(value);
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
			private esBankInquiryDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBankInquiryDetailQuery query)
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
				throw new Exception("esBankInquiryDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class BankInquiryDetail : esBankInquiryDetail
	{	
	}

	[Serializable]
	abstract public class esBankInquiryDetailQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return BankInquiryDetailMetadata.Meta();
			}
		}	
			
		public esQueryItem TransactionID
		{
			get
			{
				return new esQueryItem(this, BankInquiryDetailMetadata.ColumnNames.TransactionID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem InquiryID
		{
			get
			{
				return new esQueryItem(this, BankInquiryDetailMetadata.ColumnNames.InquiryID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ReconcileID
		{
			get
			{
				return new esQueryItem(this, BankInquiryDetailMetadata.ColumnNames.ReconcileID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem RelatedTransactionNo
		{
			get
			{
				return new esQueryItem(this, BankInquiryDetailMetadata.ColumnNames.RelatedTransactionNo, esSystemType.String);
			}
		} 
			
		public esQueryItem TransactionDateTime
		{
			get
			{
				return new esQueryItem(this, BankInquiryDetailMetadata.ColumnNames.TransactionDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, BankInquiryDetailMetadata.ColumnNames.Description, esSystemType.String);
			}
		} 
			
		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, BankInquiryDetailMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem Debit
		{
			get
			{
				return new esQueryItem(this, BankInquiryDetailMetadata.ColumnNames.Debit, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Credit
		{
			get
			{
				return new esQueryItem(this, BankInquiryDetailMetadata.ColumnNames.Credit, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Balance
		{
			get
			{
				return new esQueryItem(this, BankInquiryDetailMetadata.ColumnNames.Balance, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem SRCashTransactionCode
		{
			get
			{
				return new esQueryItem(this, BankInquiryDetailMetadata.ColumnNames.SRCashTransactionCode, esSystemType.String);
			}
		} 
			
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, BankInquiryDetailMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, BankInquiryDetailMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BankInquiryDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BankInquiryDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BankInquiryDetailCollection")]
	public partial class BankInquiryDetailCollection : esBankInquiryDetailCollection, IEnumerable< BankInquiryDetail>
	{
		public BankInquiryDetailCollection()
		{

		}	
		
		public static implicit operator List< BankInquiryDetail>(BankInquiryDetailCollection coll)
		{
			List< BankInquiryDetail> list = new List< BankInquiryDetail>();
			
			foreach (BankInquiryDetail emp in coll)
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
				return  BankInquiryDetailMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BankInquiryDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BankInquiryDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BankInquiryDetail();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public BankInquiryDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BankInquiryDetailQuery();
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
		public bool Load(BankInquiryDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public BankInquiryDetail AddNew()
		{
			BankInquiryDetail entity = base.AddNewEntity() as BankInquiryDetail;
			
			return entity;		
		}
		public BankInquiryDetail FindByPrimaryKey(Int32 transactionID)
		{
			return base.FindByPrimaryKey(transactionID) as BankInquiryDetail;
		}

		#region IEnumerable< BankInquiryDetail> Members

		IEnumerator< BankInquiryDetail> IEnumerable< BankInquiryDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as BankInquiryDetail;
			}
		}

		#endregion
		
		private BankInquiryDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BankInquiryDetail' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("BankInquiryDetail ({TransactionID})")]
	[Serializable]
	public partial class BankInquiryDetail : esBankInquiryDetail
	{
		public BankInquiryDetail()
		{
		}	
	
		public BankInquiryDetail(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BankInquiryDetailMetadata.Meta();
			}
		}	
	
		override protected esBankInquiryDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BankInquiryDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public BankInquiryDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BankInquiryDetailQuery();
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
		public bool Load(BankInquiryDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private BankInquiryDetailQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class BankInquiryDetailQuery : esBankInquiryDetailQuery
	{
		public BankInquiryDetailQuery()
		{

		}		
		
		public BankInquiryDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "BankInquiryDetailQuery";
        }
	}

	[Serializable]
	public partial class BankInquiryDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BankInquiryDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(BankInquiryDetailMetadata.ColumnNames.TransactionID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BankInquiryDetailMetadata.PropertyNames.TransactionID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BankInquiryDetailMetadata.ColumnNames.InquiryID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BankInquiryDetailMetadata.PropertyNames.InquiryID;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BankInquiryDetailMetadata.ColumnNames.ReconcileID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BankInquiryDetailMetadata.PropertyNames.ReconcileID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BankInquiryDetailMetadata.ColumnNames.RelatedTransactionNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = BankInquiryDetailMetadata.PropertyNames.RelatedTransactionNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BankInquiryDetailMetadata.ColumnNames.TransactionDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BankInquiryDetailMetadata.PropertyNames.TransactionDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BankInquiryDetailMetadata.ColumnNames.Description, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = BankInquiryDetailMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 255;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BankInquiryDetailMetadata.ColumnNames.ReferenceNo, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = BankInquiryDetailMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 30;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BankInquiryDetailMetadata.ColumnNames.Debit, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BankInquiryDetailMetadata.PropertyNames.Debit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BankInquiryDetailMetadata.ColumnNames.Credit, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BankInquiryDetailMetadata.PropertyNames.Credit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BankInquiryDetailMetadata.ColumnNames.Balance, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BankInquiryDetailMetadata.PropertyNames.Balance;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BankInquiryDetailMetadata.ColumnNames.SRCashTransactionCode, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = BankInquiryDetailMetadata.PropertyNames.SRCashTransactionCode;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BankInquiryDetailMetadata.ColumnNames.CreatedDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BankInquiryDetailMetadata.PropertyNames.CreatedDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BankInquiryDetailMetadata.ColumnNames.CreatedByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = BankInquiryDetailMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BankInquiryDetailMetadata.ColumnNames.LastUpdateDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BankInquiryDetailMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BankInquiryDetailMetadata.ColumnNames.LastUpdateByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = BankInquiryDetailMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public BankInquiryDetailMetadata Meta()
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
			public const string TransactionID = "TransactionID";
			public const string InquiryID = "InquiryID";
			public const string ReconcileID = "ReconcileID";
			public const string RelatedTransactionNo = "RelatedTransactionNo";
			public const string TransactionDateTime = "TransactionDateTime";
			public const string Description = "Description";
			public const string ReferenceNo = "ReferenceNo";
			public const string Debit = "Debit";
			public const string Credit = "Credit";
			public const string Balance = "Balance";
			public const string SRCashTransactionCode = "SRCashTransactionCode";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string TransactionID = "TransactionID";
			public const string InquiryID = "InquiryID";
			public const string ReconcileID = "ReconcileID";
			public const string RelatedTransactionNo = "RelatedTransactionNo";
			public const string TransactionDateTime = "TransactionDateTime";
			public const string Description = "Description";
			public const string ReferenceNo = "ReferenceNo";
			public const string Debit = "Debit";
			public const string Credit = "Credit";
			public const string Balance = "Balance";
			public const string SRCashTransactionCode = "SRCashTransactionCode";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
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
			lock (typeof(BankInquiryDetailMetadata))
			{
				if(BankInquiryDetailMetadata.mapDelegates == null)
				{
					BankInquiryDetailMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (BankInquiryDetailMetadata.meta == null)
				{
					BankInquiryDetailMetadata.meta = new BankInquiryDetailMetadata();
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
				
				meta.AddTypeMap("TransactionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("InquiryID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ReconcileID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RelatedTransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Description", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Debit", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("Credit", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("Balance", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("SRCashTransactionCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "BankInquiryDetail";
				meta.Destination = "BankInquiryDetail";
				meta.spInsert = "proc_BankInquiryDetailInsert";				
				meta.spUpdate = "proc_BankInquiryDetailUpdate";		
				meta.spDelete = "proc_BankInquiryDetailDelete";
				meta.spLoadAll = "proc_BankInquiryDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_BankInquiryDetailLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BankInquiryDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
