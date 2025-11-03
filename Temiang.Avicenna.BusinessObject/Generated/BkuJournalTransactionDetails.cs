/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/4/2021 11:24:34 AM
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
	abstract public class esBkuJournalTransactionDetailsCollection : esEntityCollectionWAuditLog
	{
		public esBkuJournalTransactionDetailsCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "BkuJournalTransactionDetailsCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esBkuJournalTransactionDetailsQuery query)
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
			this.InitQuery(query as esBkuJournalTransactionDetailsQuery);
		}
		#endregion
			
		virtual public BkuJournalTransactionDetails DetachEntity(BkuJournalTransactionDetails entity)
		{
			return base.DetachEntity(entity) as BkuJournalTransactionDetails;
		}
		
		virtual public BkuJournalTransactionDetails AttachEntity(BkuJournalTransactionDetails entity)
		{
			return base.AttachEntity(entity) as BkuJournalTransactionDetails;
		}
		
		virtual public void Combine(BkuJournalTransactionDetailsCollection collection)
		{
			base.Combine(collection);
		}
		
		new public BkuJournalTransactionDetails this[int index]
		{
			get
			{
				return base[index] as BkuJournalTransactionDetails;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BkuJournalTransactionDetails);
		}
	}

	[Serializable]
	abstract public class esBkuJournalTransactionDetails : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBkuJournalTransactionDetailsQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esBkuJournalTransactionDetails()
		{
		}
	
		public esBkuJournalTransactionDetails(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 bkuDetailId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bkuDetailId);
			else
				return LoadByPrimaryKeyStoredProcedure(bkuDetailId);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 bkuDetailId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bkuDetailId);
			else
				return LoadByPrimaryKeyStoredProcedure(bkuDetailId);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 bkuDetailId)
		{
			esBkuJournalTransactionDetailsQuery query = this.GetDynamicQuery();
			query.Where(query.BkuDetailId==bkuDetailId);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 bkuDetailId)
		{
			esParameters parms = new esParameters();
			parms.Add("BkuDetailId",bkuDetailId);
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
						case "BkuDetailId": this.str.BkuDetailId = (string)value; break;
						case "BkuJournalId": this.str.BkuJournalId = (string)value; break;
						case "JournalDetailIdToCopy": this.str.JournalDetailIdToCopy = (string)value; break;
						case "ChartOfAccountId": this.str.ChartOfAccountId = (string)value; break;
						case "Debit": this.str.Debit = (string)value; break;
						case "Credit": this.str.Credit = (string)value; break;
						case "Description": this.str.Description = (string)value; break;
						case "SubLedgerId": this.str.SubLedgerId = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "BkuDetailId":
						
							if (value == null || value is System.Int32)
								this.BkuDetailId = (System.Int32?)value;
							break;
						case "BkuJournalId":
						
							if (value == null || value is System.Int32)
								this.BkuJournalId = (System.Int32?)value;
							break;
						case "JournalDetailIdToCopy":
						
							if (value == null || value is System.Int32)
								this.JournalDetailIdToCopy = (System.Int32?)value;
							break;
						case "ChartOfAccountId":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountId = (System.Int32?)value;
							break;
						case "Debit":
						
							if (value == null || value is System.Decimal)
								this.Debit = (System.Decimal?)value;
							break;
						case "Credit":
						
							if (value == null || value is System.Decimal)
								this.Credit = (System.Decimal?)value;
							break;
						case "SubLedgerId":
						
							if (value == null || value is System.Int32)
								this.SubLedgerId = (System.Int32?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
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
		/// Maps to BkuJournalTransactionDetails.BkuDetailId
		/// </summary>
		virtual public System.Int32? BkuDetailId
		{
			get
			{
				return base.GetSystemInt32(BkuJournalTransactionDetailsMetadata.ColumnNames.BkuDetailId);
			}
			
			set
			{
				base.SetSystemInt32(BkuJournalTransactionDetailsMetadata.ColumnNames.BkuDetailId, value);
			}
		}
		/// <summary>
		/// Maps to BkuJournalTransactionDetails.BkuJournalId
		/// </summary>
		virtual public System.Int32? BkuJournalId
		{
			get
			{
				return base.GetSystemInt32(BkuJournalTransactionDetailsMetadata.ColumnNames.BkuJournalId);
			}
			
			set
			{
				base.SetSystemInt32(BkuJournalTransactionDetailsMetadata.ColumnNames.BkuJournalId, value);
			}
		}
		/// <summary>
		/// Maps to BkuJournalTransactionDetails.JournalDetailIdToCopy
		/// </summary>
		virtual public System.Int32? JournalDetailIdToCopy
		{
			get
			{
				return base.GetSystemInt32(BkuJournalTransactionDetailsMetadata.ColumnNames.JournalDetailIdToCopy);
			}
			
			set
			{
				base.SetSystemInt32(BkuJournalTransactionDetailsMetadata.ColumnNames.JournalDetailIdToCopy, value);
			}
		}
		/// <summary>
		/// Maps to BkuJournalTransactionDetails.ChartOfAccountId
		/// </summary>
		virtual public System.Int32? ChartOfAccountId
		{
			get
			{
				return base.GetSystemInt32(BkuJournalTransactionDetailsMetadata.ColumnNames.ChartOfAccountId);
			}
			
			set
			{
				base.SetSystemInt32(BkuJournalTransactionDetailsMetadata.ColumnNames.ChartOfAccountId, value);
			}
		}
		/// <summary>
		/// Maps to BkuJournalTransactionDetails.Debit
		/// </summary>
		virtual public System.Decimal? Debit
		{
			get
			{
				return base.GetSystemDecimal(BkuJournalTransactionDetailsMetadata.ColumnNames.Debit);
			}
			
			set
			{
				base.SetSystemDecimal(BkuJournalTransactionDetailsMetadata.ColumnNames.Debit, value);
			}
		}
		/// <summary>
		/// Maps to BkuJournalTransactionDetails.Credit
		/// </summary>
		virtual public System.Decimal? Credit
		{
			get
			{
				return base.GetSystemDecimal(BkuJournalTransactionDetailsMetadata.ColumnNames.Credit);
			}
			
			set
			{
				base.SetSystemDecimal(BkuJournalTransactionDetailsMetadata.ColumnNames.Credit, value);
			}
		}
		/// <summary>
		/// Maps to BkuJournalTransactionDetails.Description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(BkuJournalTransactionDetailsMetadata.ColumnNames.Description);
			}
			
			set
			{
				base.SetSystemString(BkuJournalTransactionDetailsMetadata.ColumnNames.Description, value);
			}
		}
		/// <summary>
		/// Maps to BkuJournalTransactionDetails.SubLedgerId
		/// </summary>
		virtual public System.Int32? SubLedgerId
		{
			get
			{
				return base.GetSystemInt32(BkuJournalTransactionDetailsMetadata.ColumnNames.SubLedgerId);
			}
			
			set
			{
				base.SetSystemInt32(BkuJournalTransactionDetailsMetadata.ColumnNames.SubLedgerId, value);
			}
		}
		/// <summary>
		/// Maps to BkuJournalTransactionDetails.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BkuJournalTransactionDetailsMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BkuJournalTransactionDetailsMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BkuJournalTransactionDetails.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(BkuJournalTransactionDetailsMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(BkuJournalTransactionDetailsMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BkuJournalTransactionDetails.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BkuJournalTransactionDetailsMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BkuJournalTransactionDetailsMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BkuJournalTransactionDetails.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BkuJournalTransactionDetailsMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(BkuJournalTransactionDetailsMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esBkuJournalTransactionDetails entity)
			{
				this.entity = entity;
			}
			public System.String BkuDetailId
			{
				get
				{
					System.Int32? data = entity.BkuDetailId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BkuDetailId = null;
					else entity.BkuDetailId = Convert.ToInt32(value);
				}
			}
			public System.String BkuJournalId
			{
				get
				{
					System.Int32? data = entity.BkuJournalId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BkuJournalId = null;
					else entity.BkuJournalId = Convert.ToInt32(value);
				}
			}
			public System.String JournalDetailIdToCopy
			{
				get
				{
					System.Int32? data = entity.JournalDetailIdToCopy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalDetailIdToCopy = null;
					else entity.JournalDetailIdToCopy = Convert.ToInt32(value);
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
			private esBkuJournalTransactionDetails entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBkuJournalTransactionDetailsQuery query)
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
				throw new Exception("esBkuJournalTransactionDetails can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class BkuJournalTransactionDetails : esBkuJournalTransactionDetails
	{	
	}

	[Serializable]
	abstract public class esBkuJournalTransactionDetailsQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return BkuJournalTransactionDetailsMetadata.Meta();
			}
		}	
			
		public esQueryItem BkuDetailId
		{
			get
			{
				return new esQueryItem(this, BkuJournalTransactionDetailsMetadata.ColumnNames.BkuDetailId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem BkuJournalId
		{
			get
			{
				return new esQueryItem(this, BkuJournalTransactionDetailsMetadata.ColumnNames.BkuJournalId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem JournalDetailIdToCopy
		{
			get
			{
				return new esQueryItem(this, BkuJournalTransactionDetailsMetadata.ColumnNames.JournalDetailIdToCopy, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountId
		{
			get
			{
				return new esQueryItem(this, BkuJournalTransactionDetailsMetadata.ColumnNames.ChartOfAccountId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Debit
		{
			get
			{
				return new esQueryItem(this, BkuJournalTransactionDetailsMetadata.ColumnNames.Debit, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Credit
		{
			get
			{
				return new esQueryItem(this, BkuJournalTransactionDetailsMetadata.ColumnNames.Credit, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, BkuJournalTransactionDetailsMetadata.ColumnNames.Description, esSystemType.String);
			}
		} 
			
		public esQueryItem SubLedgerId
		{
			get
			{
				return new esQueryItem(this, BkuJournalTransactionDetailsMetadata.ColumnNames.SubLedgerId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, BkuJournalTransactionDetailsMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, BkuJournalTransactionDetailsMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BkuJournalTransactionDetailsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BkuJournalTransactionDetailsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BkuJournalTransactionDetailsCollection")]
	public partial class BkuJournalTransactionDetailsCollection : esBkuJournalTransactionDetailsCollection, IEnumerable< BkuJournalTransactionDetails>
	{
		public BkuJournalTransactionDetailsCollection()
		{

		}	
		
		public static implicit operator List< BkuJournalTransactionDetails>(BkuJournalTransactionDetailsCollection coll)
		{
			List< BkuJournalTransactionDetails> list = new List< BkuJournalTransactionDetails>();
			
			foreach (BkuJournalTransactionDetails emp in coll)
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
				return  BkuJournalTransactionDetailsMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BkuJournalTransactionDetailsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BkuJournalTransactionDetails(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BkuJournalTransactionDetails();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public BkuJournalTransactionDetailsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BkuJournalTransactionDetailsQuery();
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
		public bool Load(BkuJournalTransactionDetailsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public BkuJournalTransactionDetails AddNew()
		{
			BkuJournalTransactionDetails entity = base.AddNewEntity() as BkuJournalTransactionDetails;
			
			return entity;		
		}
		public BkuJournalTransactionDetails FindByPrimaryKey(Int32 bkuDetailId)
		{
			return base.FindByPrimaryKey(bkuDetailId) as BkuJournalTransactionDetails;
		}

		#region IEnumerable< BkuJournalTransactionDetails> Members

		IEnumerator< BkuJournalTransactionDetails> IEnumerable< BkuJournalTransactionDetails>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as BkuJournalTransactionDetails;
			}
		}

		#endregion
		
		private BkuJournalTransactionDetailsQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BkuJournalTransactionDetails' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("BkuJournalTransactionDetails ({BkuDetailId})")]
	[Serializable]
	public partial class BkuJournalTransactionDetails : esBkuJournalTransactionDetails
	{
		public BkuJournalTransactionDetails()
		{
		}	
	
		public BkuJournalTransactionDetails(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BkuJournalTransactionDetailsMetadata.Meta();
			}
		}	
	
		override protected esBkuJournalTransactionDetailsQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BkuJournalTransactionDetailsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public BkuJournalTransactionDetailsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BkuJournalTransactionDetailsQuery();
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
		public bool Load(BkuJournalTransactionDetailsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private BkuJournalTransactionDetailsQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class BkuJournalTransactionDetailsQuery : esBkuJournalTransactionDetailsQuery
	{
		public BkuJournalTransactionDetailsQuery()
		{

		}		
		
		public BkuJournalTransactionDetailsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "BkuJournalTransactionDetailsQuery";
        }
	}

	[Serializable]
	public partial class BkuJournalTransactionDetailsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BkuJournalTransactionDetailsMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(BkuJournalTransactionDetailsMetadata.ColumnNames.BkuDetailId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BkuJournalTransactionDetailsMetadata.PropertyNames.BkuDetailId;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BkuJournalTransactionDetailsMetadata.ColumnNames.BkuJournalId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BkuJournalTransactionDetailsMetadata.PropertyNames.BkuJournalId;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BkuJournalTransactionDetailsMetadata.ColumnNames.JournalDetailIdToCopy, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BkuJournalTransactionDetailsMetadata.PropertyNames.JournalDetailIdToCopy;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BkuJournalTransactionDetailsMetadata.ColumnNames.ChartOfAccountId, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BkuJournalTransactionDetailsMetadata.PropertyNames.ChartOfAccountId;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BkuJournalTransactionDetailsMetadata.ColumnNames.Debit, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BkuJournalTransactionDetailsMetadata.PropertyNames.Debit;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BkuJournalTransactionDetailsMetadata.ColumnNames.Credit, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BkuJournalTransactionDetailsMetadata.PropertyNames.Credit;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(BkuJournalTransactionDetailsMetadata.ColumnNames.Description, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = BkuJournalTransactionDetailsMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 255;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BkuJournalTransactionDetailsMetadata.ColumnNames.SubLedgerId, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BkuJournalTransactionDetailsMetadata.PropertyNames.SubLedgerId;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BkuJournalTransactionDetailsMetadata.ColumnNames.CreateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BkuJournalTransactionDetailsMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BkuJournalTransactionDetailsMetadata.ColumnNames.CreateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = BkuJournalTransactionDetailsMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BkuJournalTransactionDetailsMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BkuJournalTransactionDetailsMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BkuJournalTransactionDetailsMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = BkuJournalTransactionDetailsMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public BkuJournalTransactionDetailsMetadata Meta()
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
			public const string BkuDetailId = "BkuDetailId";
			public const string BkuJournalId = "BkuJournalId";
			public const string JournalDetailIdToCopy = "JournalDetailIdToCopy";
			public const string ChartOfAccountId = "ChartOfAccountId";
			public const string Debit = "Debit";
			public const string Credit = "Credit";
			public const string Description = "Description";
			public const string SubLedgerId = "SubLedgerId";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string BkuDetailId = "BkuDetailId";
			public const string BkuJournalId = "BkuJournalId";
			public const string JournalDetailIdToCopy = "JournalDetailIdToCopy";
			public const string ChartOfAccountId = "ChartOfAccountId";
			public const string Debit = "Debit";
			public const string Credit = "Credit";
			public const string Description = "Description";
			public const string SubLedgerId = "SubLedgerId";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
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
			lock (typeof(BkuJournalTransactionDetailsMetadata))
			{
				if(BkuJournalTransactionDetailsMetadata.mapDelegates == null)
				{
					BkuJournalTransactionDetailsMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (BkuJournalTransactionDetailsMetadata.meta == null)
				{
					BkuJournalTransactionDetailsMetadata.meta = new BkuJournalTransactionDetailsMetadata();
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
				
				meta.AddTypeMap("BkuDetailId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("BkuJournalId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("JournalDetailIdToCopy", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Debit", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("Credit", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("Description", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SubLedgerId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "BkuJournalTransactionDetails";
				meta.Destination = "BkuJournalTransactionDetails";
				meta.spInsert = "proc_BkuJournalTransactionDetailsInsert";				
				meta.spUpdate = "proc_BkuJournalTransactionDetailsUpdate";		
				meta.spDelete = "proc_BkuJournalTransactionDetailsDelete";
				meta.spLoadAll = "proc_BkuJournalTransactionDetailsLoadAll";
				meta.spLoadByPrimaryKey = "proc_BkuJournalTransactionDetailsLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BkuJournalTransactionDetailsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
