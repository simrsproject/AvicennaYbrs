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
	abstract public class esBkuJournalTransactionsCollection : esEntityCollectionWAuditLog
	{
		public esBkuJournalTransactionsCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "BkuJournalTransactionsCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esBkuJournalTransactionsQuery query)
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
			this.InitQuery(query as esBkuJournalTransactionsQuery);
		}
		#endregion
			
		virtual public BkuJournalTransactions DetachEntity(BkuJournalTransactions entity)
		{
			return base.DetachEntity(entity) as BkuJournalTransactions;
		}
		
		virtual public BkuJournalTransactions AttachEntity(BkuJournalTransactions entity)
		{
			return base.AttachEntity(entity) as BkuJournalTransactions;
		}
		
		virtual public void Combine(BkuJournalTransactionsCollection collection)
		{
			base.Combine(collection);
		}
		
		new public BkuJournalTransactions this[int index]
		{
			get
			{
				return base[index] as BkuJournalTransactions;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BkuJournalTransactions);
		}
	}

	[Serializable]
	abstract public class esBkuJournalTransactions : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBkuJournalTransactionsQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esBkuJournalTransactions()
		{
		}
	
		public esBkuJournalTransactions(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 bkuJournalId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bkuJournalId);
			else
				return LoadByPrimaryKeyStoredProcedure(bkuJournalId);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 bkuJournalId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bkuJournalId);
			else
				return LoadByPrimaryKeyStoredProcedure(bkuJournalId);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 bkuJournalId)
		{
			esBkuJournalTransactionsQuery query = this.GetDynamicQuery();
			query.Where(query.BkuJournalId==bkuJournalId);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 bkuJournalId)
		{
			esParameters parms = new esParameters();
			parms.Add("BkuJournalId",bkuJournalId);
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
						case "BkuJournalId": this.str.BkuJournalId = (string)value; break;
						case "CashTransactionId": this.str.CashTransactionId = (string)value; break;
						case "JournalIdToCopy": this.str.JournalIdToCopy = (string)value; break;
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
						case "BkuJournalId":
						
							if (value == null || value is System.Int32)
								this.BkuJournalId = (System.Int32?)value;
							break;
						case "CashTransactionId":
						
							if (value == null || value is System.Int32)
								this.CashTransactionId = (System.Int32?)value;
							break;
						case "JournalIdToCopy":
						
							if (value == null || value is System.Int32)
								this.JournalIdToCopy = (System.Int32?)value;
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
		/// Maps to BkuJournalTransactions.BkuJournalId
		/// </summary>
		virtual public System.Int32? BkuJournalId
		{
			get
			{
				return base.GetSystemInt32(BkuJournalTransactionsMetadata.ColumnNames.BkuJournalId);
			}
			
			set
			{
				base.SetSystemInt32(BkuJournalTransactionsMetadata.ColumnNames.BkuJournalId, value);
			}
		}
		/// <summary>
		/// Maps to BkuJournalTransactions.CashTransactionId
		/// </summary>
		virtual public System.Int32? CashTransactionId
		{
			get
			{
				return base.GetSystemInt32(BkuJournalTransactionsMetadata.ColumnNames.CashTransactionId);
			}
			
			set
			{
				base.SetSystemInt32(BkuJournalTransactionsMetadata.ColumnNames.CashTransactionId, value);
			}
		}
		/// <summary>
		/// Maps to BkuJournalTransactions.JournalIdToCopy
		/// </summary>
		virtual public System.Int32? JournalIdToCopy
		{
			get
			{
				return base.GetSystemInt32(BkuJournalTransactionsMetadata.ColumnNames.JournalIdToCopy);
			}
			
			set
			{
				base.SetSystemInt32(BkuJournalTransactionsMetadata.ColumnNames.JournalIdToCopy, value);
			}
		}
		/// <summary>
		/// Maps to BkuJournalTransactions.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BkuJournalTransactionsMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BkuJournalTransactionsMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BkuJournalTransactions.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(BkuJournalTransactionsMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(BkuJournalTransactionsMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BkuJournalTransactions.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BkuJournalTransactionsMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BkuJournalTransactionsMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BkuJournalTransactions.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BkuJournalTransactionsMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(BkuJournalTransactionsMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esBkuJournalTransactions entity)
			{
				this.entity = entity;
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
			public System.String CashTransactionId
			{
				get
				{
					System.Int32? data = entity.CashTransactionId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CashTransactionId = null;
					else entity.CashTransactionId = Convert.ToInt32(value);
				}
			}
			public System.String JournalIdToCopy
			{
				get
				{
					System.Int32? data = entity.JournalIdToCopy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalIdToCopy = null;
					else entity.JournalIdToCopy = Convert.ToInt32(value);
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
			private esBkuJournalTransactions entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBkuJournalTransactionsQuery query)
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
				throw new Exception("esBkuJournalTransactions can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class BkuJournalTransactions : esBkuJournalTransactions
	{	
	}

	[Serializable]
	abstract public class esBkuJournalTransactionsQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return BkuJournalTransactionsMetadata.Meta();
			}
		}	
			
		public esQueryItem BkuJournalId
		{
			get
			{
				return new esQueryItem(this, BkuJournalTransactionsMetadata.ColumnNames.BkuJournalId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem CashTransactionId
		{
			get
			{
				return new esQueryItem(this, BkuJournalTransactionsMetadata.ColumnNames.CashTransactionId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem JournalIdToCopy
		{
			get
			{
				return new esQueryItem(this, BkuJournalTransactionsMetadata.ColumnNames.JournalIdToCopy, esSystemType.Int32);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, BkuJournalTransactionsMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, BkuJournalTransactionsMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BkuJournalTransactionsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BkuJournalTransactionsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BkuJournalTransactionsCollection")]
	public partial class BkuJournalTransactionsCollection : esBkuJournalTransactionsCollection, IEnumerable< BkuJournalTransactions>
	{
		public BkuJournalTransactionsCollection()
		{

		}	
		
		public static implicit operator List< BkuJournalTransactions>(BkuJournalTransactionsCollection coll)
		{
			List< BkuJournalTransactions> list = new List< BkuJournalTransactions>();
			
			foreach (BkuJournalTransactions emp in coll)
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
				return  BkuJournalTransactionsMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BkuJournalTransactionsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BkuJournalTransactions(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BkuJournalTransactions();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public BkuJournalTransactionsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BkuJournalTransactionsQuery();
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
		public bool Load(BkuJournalTransactionsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public BkuJournalTransactions AddNew()
		{
			BkuJournalTransactions entity = base.AddNewEntity() as BkuJournalTransactions;
			
			return entity;		
		}
		public BkuJournalTransactions FindByPrimaryKey(Int32 bkuJournalId)
		{
			return base.FindByPrimaryKey(bkuJournalId) as BkuJournalTransactions;
		}

		#region IEnumerable< BkuJournalTransactions> Members

		IEnumerator< BkuJournalTransactions> IEnumerable< BkuJournalTransactions>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as BkuJournalTransactions;
			}
		}

		#endregion
		
		private BkuJournalTransactionsQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BkuJournalTransactions' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("BkuJournalTransactions ({BkuJournalId})")]
	[Serializable]
	public partial class BkuJournalTransactions : esBkuJournalTransactions
	{
		public BkuJournalTransactions()
		{
		}	
	
		public BkuJournalTransactions(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BkuJournalTransactionsMetadata.Meta();
			}
		}	
	
		override protected esBkuJournalTransactionsQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BkuJournalTransactionsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public BkuJournalTransactionsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BkuJournalTransactionsQuery();
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
		public bool Load(BkuJournalTransactionsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private BkuJournalTransactionsQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class BkuJournalTransactionsQuery : esBkuJournalTransactionsQuery
	{
		public BkuJournalTransactionsQuery()
		{

		}		
		
		public BkuJournalTransactionsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "BkuJournalTransactionsQuery";
        }
	}

	[Serializable]
	public partial class BkuJournalTransactionsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BkuJournalTransactionsMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(BkuJournalTransactionsMetadata.ColumnNames.BkuJournalId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BkuJournalTransactionsMetadata.PropertyNames.BkuJournalId;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BkuJournalTransactionsMetadata.ColumnNames.CashTransactionId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BkuJournalTransactionsMetadata.PropertyNames.CashTransactionId;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BkuJournalTransactionsMetadata.ColumnNames.JournalIdToCopy, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BkuJournalTransactionsMetadata.PropertyNames.JournalIdToCopy;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BkuJournalTransactionsMetadata.ColumnNames.CreateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BkuJournalTransactionsMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BkuJournalTransactionsMetadata.ColumnNames.CreateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = BkuJournalTransactionsMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BkuJournalTransactionsMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BkuJournalTransactionsMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BkuJournalTransactionsMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = BkuJournalTransactionsMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public BkuJournalTransactionsMetadata Meta()
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
			public const string BkuJournalId = "BkuJournalId";
			public const string CashTransactionId = "CashTransactionId";
			public const string JournalIdToCopy = "JournalIdToCopy";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string BkuJournalId = "BkuJournalId";
			public const string CashTransactionId = "CashTransactionId";
			public const string JournalIdToCopy = "JournalIdToCopy";
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
			lock (typeof(BkuJournalTransactionsMetadata))
			{
				if(BkuJournalTransactionsMetadata.mapDelegates == null)
				{
					BkuJournalTransactionsMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (BkuJournalTransactionsMetadata.meta == null)
				{
					BkuJournalTransactionsMetadata.meta = new BkuJournalTransactionsMetadata();
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
				
				meta.AddTypeMap("BkuJournalId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CashTransactionId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("JournalIdToCopy", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "BkuJournalTransactions";
				meta.Destination = "BkuJournalTransactions";
				meta.spInsert = "proc_BkuJournalTransactionsInsert";				
				meta.spUpdate = "proc_BkuJournalTransactionsUpdate";		
				meta.spDelete = "proc_BkuJournalTransactionsDelete";
				meta.spLoadAll = "proc_BkuJournalTransactionsLoadAll";
				meta.spLoadByPrimaryKey = "proc_BkuJournalTransactionsLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BkuJournalTransactionsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
