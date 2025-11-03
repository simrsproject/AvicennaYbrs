/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/25/2021 9:10:08 PM
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
	abstract public class esBkuJournalMessagesCollection : esEntityCollectionWAuditLog
	{
		public esBkuJournalMessagesCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "BkuJournalMessagesCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esBkuJournalMessagesQuery query)
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
			this.InitQuery(query as esBkuJournalMessagesQuery);
		}
		#endregion
			
		virtual public BkuJournalMessages DetachEntity(BkuJournalMessages entity)
		{
			return base.DetachEntity(entity) as BkuJournalMessages;
		}
		
		virtual public BkuJournalMessages AttachEntity(BkuJournalMessages entity)
		{
			return base.AttachEntity(entity) as BkuJournalMessages;
		}
		
		virtual public void Combine(BkuJournalMessagesCollection collection)
		{
			base.Combine(collection);
		}
		
		new public BkuJournalMessages this[int index]
		{
			get
			{
				return base[index] as BkuJournalMessages;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BkuJournalMessages);
		}
	}

	[Serializable]
	abstract public class esBkuJournalMessages : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBkuJournalMessagesQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esBkuJournalMessages()
		{
		}
	
		public esBkuJournalMessages(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 bkuJournalMessagesId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bkuJournalMessagesId);
			else
				return LoadByPrimaryKeyStoredProcedure(bkuJournalMessagesId);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 bkuJournalMessagesId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bkuJournalMessagesId);
			else
				return LoadByPrimaryKeyStoredProcedure(bkuJournalMessagesId);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 bkuJournalMessagesId)
		{
			esBkuJournalMessagesQuery query = this.GetDynamicQuery();
			query.Where(query.BkuJournalMessagesId==bkuJournalMessagesId);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 bkuJournalMessagesId)
		{
			esParameters parms = new esParameters();
			parms.Add("BkuJournalMessagesId",bkuJournalMessagesId);
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
						case "BkuJournalMessagesId": this.str.BkuJournalMessagesId = (string)value; break;
						case "BkuJournalId": this.str.BkuJournalId = (string)value; break;
						case "DetailJournalId": this.str.DetailJournalId = (string)value; break;
						case "Message": this.str.Message = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "BkuJournalMessagesId":
						
							if (value == null || value is System.Int32)
								this.BkuJournalMessagesId = (System.Int32?)value;
							break;
						case "BkuJournalId":
						
							if (value == null || value is System.Int32)
								this.BkuJournalId = (System.Int32?)value;
							break;
						case "DetailJournalId":
						
							if (value == null || value is System.Int32)
								this.DetailJournalId = (System.Int32?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
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
		/// Maps to BkuJournalMessages.BkuJournalMessagesId
		/// </summary>
		virtual public System.Int32? BkuJournalMessagesId
		{
			get
			{
				return base.GetSystemInt32(BkuJournalMessagesMetadata.ColumnNames.BkuJournalMessagesId);
			}
			
			set
			{
				base.SetSystemInt32(BkuJournalMessagesMetadata.ColumnNames.BkuJournalMessagesId, value);
			}
		}
		/// <summary>
		/// Maps to BkuJournalMessages.BkuJournalId
		/// </summary>
		virtual public System.Int32? BkuJournalId
		{
			get
			{
				return base.GetSystemInt32(BkuJournalMessagesMetadata.ColumnNames.BkuJournalId);
			}
			
			set
			{
				base.SetSystemInt32(BkuJournalMessagesMetadata.ColumnNames.BkuJournalId, value);
			}
		}
		/// <summary>
		/// Maps to BkuJournalMessages.DetailJournalId
		/// </summary>
		virtual public System.Int32? DetailJournalId
		{
			get
			{
				return base.GetSystemInt32(BkuJournalMessagesMetadata.ColumnNames.DetailJournalId);
			}
			
			set
			{
				base.SetSystemInt32(BkuJournalMessagesMetadata.ColumnNames.DetailJournalId, value);
			}
		}
		/// <summary>
		/// Maps to BkuJournalMessages.Message
		/// </summary>
		virtual public System.String Message
		{
			get
			{
				return base.GetSystemString(BkuJournalMessagesMetadata.ColumnNames.Message);
			}
			
			set
			{
				base.SetSystemString(BkuJournalMessagesMetadata.ColumnNames.Message, value);
			}
		}
		/// <summary>
		/// Maps to BkuJournalMessages.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BkuJournalMessagesMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BkuJournalMessagesMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BkuJournalMessages.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(BkuJournalMessagesMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(BkuJournalMessagesMetadata.ColumnNames.CreateByUserID, value);
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
			public esStrings(esBkuJournalMessages entity)
			{
				this.entity = entity;
			}
			public System.String BkuJournalMessagesId
			{
				get
				{
					System.Int32? data = entity.BkuJournalMessagesId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BkuJournalMessagesId = null;
					else entity.BkuJournalMessagesId = Convert.ToInt32(value);
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
			public System.String DetailJournalId
			{
				get
				{
					System.Int32? data = entity.DetailJournalId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DetailJournalId = null;
					else entity.DetailJournalId = Convert.ToInt32(value);
				}
			}
			public System.String Message
			{
				get
				{
					System.String data = entity.Message;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Message = null;
					else entity.Message = Convert.ToString(value);
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
			private esBkuJournalMessages entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBkuJournalMessagesQuery query)
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
				throw new Exception("esBkuJournalMessages can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class BkuJournalMessages : esBkuJournalMessages
	{	
	}

	[Serializable]
	abstract public class esBkuJournalMessagesQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return BkuJournalMessagesMetadata.Meta();
			}
		}	
			
		public esQueryItem BkuJournalMessagesId
		{
			get
			{
				return new esQueryItem(this, BkuJournalMessagesMetadata.ColumnNames.BkuJournalMessagesId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem BkuJournalId
		{
			get
			{
				return new esQueryItem(this, BkuJournalMessagesMetadata.ColumnNames.BkuJournalId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem DetailJournalId
		{
			get
			{
				return new esQueryItem(this, BkuJournalMessagesMetadata.ColumnNames.DetailJournalId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Message
		{
			get
			{
				return new esQueryItem(this, BkuJournalMessagesMetadata.ColumnNames.Message, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, BkuJournalMessagesMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, BkuJournalMessagesMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BkuJournalMessagesCollection")]
	public partial class BkuJournalMessagesCollection : esBkuJournalMessagesCollection, IEnumerable< BkuJournalMessages>
	{
		public BkuJournalMessagesCollection()
		{

		}	
		
		public static implicit operator List< BkuJournalMessages>(BkuJournalMessagesCollection coll)
		{
			List< BkuJournalMessages> list = new List< BkuJournalMessages>();
			
			foreach (BkuJournalMessages emp in coll)
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
				return  BkuJournalMessagesMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BkuJournalMessagesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BkuJournalMessages(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BkuJournalMessages();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public BkuJournalMessagesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BkuJournalMessagesQuery();
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
		public bool Load(BkuJournalMessagesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public BkuJournalMessages AddNew()
		{
			BkuJournalMessages entity = base.AddNewEntity() as BkuJournalMessages;
			
			return entity;		
		}
		public BkuJournalMessages FindByPrimaryKey(Int32 bkuJournalMessagesId)
		{
			return base.FindByPrimaryKey(bkuJournalMessagesId) as BkuJournalMessages;
		}

		#region IEnumerable< BkuJournalMessages> Members

		IEnumerator< BkuJournalMessages> IEnumerable< BkuJournalMessages>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as BkuJournalMessages;
			}
		}

		#endregion
		
		private BkuJournalMessagesQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BkuJournalMessages' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("BkuJournalMessages ({BkuJournalMessagesId})")]
	[Serializable]
	public partial class BkuJournalMessages : esBkuJournalMessages
	{
		public BkuJournalMessages()
		{
		}	
	
		public BkuJournalMessages(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BkuJournalMessagesMetadata.Meta();
			}
		}	
	
		override protected esBkuJournalMessagesQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BkuJournalMessagesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public BkuJournalMessagesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BkuJournalMessagesQuery();
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
		public bool Load(BkuJournalMessagesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private BkuJournalMessagesQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class BkuJournalMessagesQuery : esBkuJournalMessagesQuery
	{
		public BkuJournalMessagesQuery()
		{

		}		
		
		public BkuJournalMessagesQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "BkuJournalMessagesQuery";
        }
	}

	[Serializable]
	public partial class BkuJournalMessagesMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BkuJournalMessagesMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(BkuJournalMessagesMetadata.ColumnNames.BkuJournalMessagesId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BkuJournalMessagesMetadata.PropertyNames.BkuJournalMessagesId;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BkuJournalMessagesMetadata.ColumnNames.BkuJournalId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BkuJournalMessagesMetadata.PropertyNames.BkuJournalId;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BkuJournalMessagesMetadata.ColumnNames.DetailJournalId, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BkuJournalMessagesMetadata.PropertyNames.DetailJournalId;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BkuJournalMessagesMetadata.ColumnNames.Message, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = BkuJournalMessagesMetadata.PropertyNames.Message;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BkuJournalMessagesMetadata.ColumnNames.CreateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BkuJournalMessagesMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(BkuJournalMessagesMetadata.ColumnNames.CreateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = BkuJournalMessagesMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public BkuJournalMessagesMetadata Meta()
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
			public const string BkuJournalMessagesId = "BkuJournalMessagesId";
			public const string BkuJournalId = "BkuJournalId";
			public const string DetailJournalId = "DetailJournalId";
			public const string Message = "Message";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string BkuJournalMessagesId = "BkuJournalMessagesId";
			public const string BkuJournalId = "BkuJournalId";
			public const string DetailJournalId = "DetailJournalId";
			public const string Message = "Message";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
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
			lock (typeof(BkuJournalMessagesMetadata))
			{
				if(BkuJournalMessagesMetadata.mapDelegates == null)
				{
					BkuJournalMessagesMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (BkuJournalMessagesMetadata.meta == null)
				{
					BkuJournalMessagesMetadata.meta = new BkuJournalMessagesMetadata();
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
				
				meta.AddTypeMap("BkuJournalMessagesId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("BkuJournalId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DetailJournalId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Message", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "BkuJournalMessages";
				meta.Destination = "BkuJournalMessages";
				meta.spInsert = "proc_BkuJournalMessagesInsert";				
				meta.spUpdate = "proc_BkuJournalMessagesUpdate";		
				meta.spDelete = "proc_BkuJournalMessagesDelete";
				meta.spLoadAll = "proc_BkuJournalMessagesLoadAll";
				meta.spLoadByPrimaryKey = "proc_BkuJournalMessagesLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BkuJournalMessagesMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
