/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/8/2020 1:04:42 PM
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
	abstract public class esWebServiceAPILogCollection : esEntityCollectionWAuditLog
	{
		public esWebServiceAPILogCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "WebServiceAPILogCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esWebServiceAPILogQuery query)
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
			this.InitQuery(query as esWebServiceAPILogQuery);
		}
		#endregion
			
		virtual public WebServiceAPILog DetachEntity(WebServiceAPILog entity)
		{
			return base.DetachEntity(entity) as WebServiceAPILog;
		}
		
		virtual public WebServiceAPILog AttachEntity(WebServiceAPILog entity)
		{
			return base.AttachEntity(entity) as WebServiceAPILog;
		}
		
		virtual public void Combine(WebServiceAPILogCollection collection)
		{
			base.Combine(collection);
		}
		
		new public WebServiceAPILog this[int index]
		{
			get
			{
				return base[index] as WebServiceAPILog;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(WebServiceAPILog);
		}
	}

	[Serializable]
	abstract public class esWebServiceAPILog : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esWebServiceAPILogQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esWebServiceAPILog()
		{
		}
	
		public esWebServiceAPILog(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 iD)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(iD);
			else
				return LoadByPrimaryKeyStoredProcedure(iD);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 iD)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(iD);
			else
				return LoadByPrimaryKeyStoredProcedure(iD);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 iD)
		{
			esWebServiceAPILogQuery query = this.GetDynamicQuery();
			query.Where(query.ID==iD);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 iD)
		{
			esParameters parms = new esParameters();
			parms.Add("ID",iD);
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
						case "ID": this.str.ID = (string)value; break;
						case "DateRequest": this.str.DateRequest = (string)value; break;
						case "IPAddress": this.str.IPAddress = (string)value; break;
						case "UrlAddress": this.str.UrlAddress = (string)value; break;
						case "Params": this.str.Params = (string)value; break;
						case "Response": this.str.Response = (string)value; break;
						case "Totalms": this.str.Totalms = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ID":
						
							if (value == null || value is System.Int32)
								this.ID = (System.Int32?)value;
							break;
						case "DateRequest":
						
							if (value == null || value is System.DateTime)
								this.DateRequest = (System.DateTime?)value;
							break;
						case "Totalms":
						
							if (value == null || value is System.Int32)
								this.Totalms = (System.Int32?)value;
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
		/// Maps to WebServiceAPILog.ID
		/// </summary>
		virtual public System.Int32? ID
		{
			get
			{
				return base.GetSystemInt32(WebServiceAPILogMetadata.ColumnNames.ID);
			}
			
			set
			{
				base.SetSystemInt32(WebServiceAPILogMetadata.ColumnNames.ID, value);
			}
		}
		/// <summary>
		/// Maps to WebServiceAPILog.DateRequest
		/// </summary>
		virtual public System.DateTime? DateRequest
		{
			get
			{
				return base.GetSystemDateTime(WebServiceAPILogMetadata.ColumnNames.DateRequest);
			}
			
			set
			{
				base.SetSystemDateTime(WebServiceAPILogMetadata.ColumnNames.DateRequest, value);
			}
		}
		/// <summary>
		/// Maps to WebServiceAPILog.IPAddress
		/// </summary>
		virtual public System.String IPAddress
		{
			get
			{
				return base.GetSystemString(WebServiceAPILogMetadata.ColumnNames.IPAddress);
			}
			
			set
			{
				base.SetSystemString(WebServiceAPILogMetadata.ColumnNames.IPAddress, value);
			}
		}
		/// <summary>
		/// Maps to WebServiceAPILog.UrlAddress
		/// </summary>
		virtual public System.String UrlAddress
		{
			get
			{
				return base.GetSystemString(WebServiceAPILogMetadata.ColumnNames.UrlAddress);
			}
			
			set
			{
				base.SetSystemString(WebServiceAPILogMetadata.ColumnNames.UrlAddress, value);
			}
		}
		/// <summary>
		/// Maps to WebServiceAPILog.Params
		/// </summary>
		virtual public System.String Params
		{
			get
			{
				return base.GetSystemString(WebServiceAPILogMetadata.ColumnNames.Params);
			}
			
			set
			{
				base.SetSystemString(WebServiceAPILogMetadata.ColumnNames.Params, value);
			}
		}
		/// <summary>
		/// Maps to WebServiceAPILog.Response
		/// </summary>
		virtual public System.String Response
		{
			get
			{
				return base.GetSystemString(WebServiceAPILogMetadata.ColumnNames.Response);
			}
			
			set
			{
				base.SetSystemString(WebServiceAPILogMetadata.ColumnNames.Response, value);
			}
		}
		/// <summary>
		/// Maps to WebServiceAPILog.Totalms
		/// </summary>
		virtual public System.Int32? Totalms
		{
			get
			{
				return base.GetSystemInt32(WebServiceAPILogMetadata.ColumnNames.Totalms);
			}
			
			set
			{
				base.SetSystemInt32(WebServiceAPILogMetadata.ColumnNames.Totalms, value);
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
			public esStrings(esWebServiceAPILog entity)
			{
				this.entity = entity;
			}
			public System.String ID
			{
				get
				{
					System.Int32? data = entity.ID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ID = null;
					else entity.ID = Convert.ToInt32(value);
				}
			}
			public System.String DateRequest
			{
				get
				{
					System.DateTime? data = entity.DateRequest;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateRequest = null;
					else entity.DateRequest = Convert.ToDateTime(value);
				}
			}
			public System.String IPAddress
			{
				get
				{
					System.String data = entity.IPAddress;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IPAddress = null;
					else entity.IPAddress = Convert.ToString(value);
				}
			}
			public System.String UrlAddress
			{
				get
				{
					System.String data = entity.UrlAddress;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UrlAddress = null;
					else entity.UrlAddress = Convert.ToString(value);
				}
			}
			public System.String Params
			{
				get
				{
					System.String data = entity.Params;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Params = null;
					else entity.Params = Convert.ToString(value);
				}
			}
			public System.String Response
			{
				get
				{
					System.String data = entity.Response;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Response = null;
					else entity.Response = Convert.ToString(value);
				}
			}
			public System.String Totalms
			{
				get
				{
					System.Int32? data = entity.Totalms;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Totalms = null;
					else entity.Totalms = Convert.ToInt32(value);
				}
			}
			private esWebServiceAPILog entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esWebServiceAPILogQuery query)
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
				throw new Exception("esWebServiceAPILog can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class WebServiceAPILog : esWebServiceAPILog
	{	
	}

	[Serializable]
	abstract public class esWebServiceAPILogQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return WebServiceAPILogMetadata.Meta();
			}
		}	
			
		public esQueryItem ID
		{
			get
			{
				return new esQueryItem(this, WebServiceAPILogMetadata.ColumnNames.ID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem DateRequest
		{
			get
			{
				return new esQueryItem(this, WebServiceAPILogMetadata.ColumnNames.DateRequest, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem IPAddress
		{
			get
			{
				return new esQueryItem(this, WebServiceAPILogMetadata.ColumnNames.IPAddress, esSystemType.String);
			}
		} 
			
		public esQueryItem UrlAddress
		{
			get
			{
				return new esQueryItem(this, WebServiceAPILogMetadata.ColumnNames.UrlAddress, esSystemType.String);
			}
		} 
			
		public esQueryItem Params
		{
			get
			{
				return new esQueryItem(this, WebServiceAPILogMetadata.ColumnNames.Params, esSystemType.String);
			}
		} 
			
		public esQueryItem Response
		{
			get
			{
				return new esQueryItem(this, WebServiceAPILogMetadata.ColumnNames.Response, esSystemType.String);
			}
		} 
			
		public esQueryItem Totalms
		{
			get
			{
				return new esQueryItem(this, WebServiceAPILogMetadata.ColumnNames.Totalms, esSystemType.Int32);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("WebServiceAPILogCollection")]
	public partial class WebServiceAPILogCollection : esWebServiceAPILogCollection, IEnumerable< WebServiceAPILog>
	{
		public WebServiceAPILogCollection()
		{

		}	
		
		public static implicit operator List< WebServiceAPILog>(WebServiceAPILogCollection coll)
		{
			List< WebServiceAPILog> list = new List< WebServiceAPILog>();
			
			foreach (WebServiceAPILog emp in coll)
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
				return  WebServiceAPILogMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WebServiceAPILogQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new WebServiceAPILog(row);
		}

		override protected esEntity CreateEntity()
		{
			return new WebServiceAPILog();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public WebServiceAPILogQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WebServiceAPILogQuery();
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
		public bool Load(WebServiceAPILogQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public WebServiceAPILog AddNew()
		{
			WebServiceAPILog entity = base.AddNewEntity() as WebServiceAPILog;
			
			return entity;		
		}
		public WebServiceAPILog FindByPrimaryKey(Int32 iD)
		{
			return base.FindByPrimaryKey(iD) as WebServiceAPILog;
		}

		#region IEnumerable< WebServiceAPILog> Members

		IEnumerator< WebServiceAPILog> IEnumerable< WebServiceAPILog>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as WebServiceAPILog;
			}
		}

		#endregion
		
		private WebServiceAPILogQuery query;
	}


	/// <summary>
	/// Encapsulates the 'WebServiceAPILog' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("WebServiceAPILog ({ID})")]
	[Serializable]
	public partial class WebServiceAPILog : esWebServiceAPILog
	{
		public WebServiceAPILog()
		{
		}	
	
		public WebServiceAPILog(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return WebServiceAPILogMetadata.Meta();
			}
		}	
	
		override protected esWebServiceAPILogQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WebServiceAPILogQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public WebServiceAPILogQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WebServiceAPILogQuery();
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
		public bool Load(WebServiceAPILogQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private WebServiceAPILogQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class WebServiceAPILogQuery : esWebServiceAPILogQuery
	{
		public WebServiceAPILogQuery()
		{

		}		
		
		public WebServiceAPILogQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "WebServiceAPILogQuery";
        }
	}

	[Serializable]
	public partial class WebServiceAPILogMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected WebServiceAPILogMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(WebServiceAPILogMetadata.ColumnNames.ID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WebServiceAPILogMetadata.PropertyNames.ID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(WebServiceAPILogMetadata.ColumnNames.DateRequest, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WebServiceAPILogMetadata.PropertyNames.DateRequest;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(WebServiceAPILogMetadata.ColumnNames.IPAddress, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = WebServiceAPILogMetadata.PropertyNames.IPAddress;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(WebServiceAPILogMetadata.ColumnNames.UrlAddress, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = WebServiceAPILogMetadata.PropertyNames.UrlAddress;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(WebServiceAPILogMetadata.ColumnNames.Params, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = WebServiceAPILogMetadata.PropertyNames.Params;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(WebServiceAPILogMetadata.ColumnNames.Response, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = WebServiceAPILogMetadata.PropertyNames.Response;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(WebServiceAPILogMetadata.ColumnNames.Totalms, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WebServiceAPILogMetadata.PropertyNames.Totalms;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public WebServiceAPILogMetadata Meta()
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
			public const string ID = "ID";
			public const string DateRequest = "DateRequest";
			public const string IPAddress = "IPAddress";
			public const string UrlAddress = "UrlAddress";
			public const string Params = "Params";
			public const string Response = "Response";
			public const string Totalms = "Totalms";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string ID = "ID";
			public const string DateRequest = "DateRequest";
			public const string IPAddress = "IPAddress";
			public const string UrlAddress = "UrlAddress";
			public const string Params = "Params";
			public const string Response = "Response";
			public const string Totalms = "Totalms";
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
			lock (typeof(WebServiceAPILogMetadata))
			{
				if(WebServiceAPILogMetadata.mapDelegates == null)
				{
					WebServiceAPILogMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (WebServiceAPILogMetadata.meta == null)
				{
					WebServiceAPILogMetadata.meta = new WebServiceAPILogMetadata();
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
				
				meta.AddTypeMap("ID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DateRequest", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IPAddress", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("UrlAddress", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Params", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Response", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Totalms", new esTypeMap("int", "System.Int32"));
		

				meta.Source = "WebServiceAPILog";
				meta.Destination = "WebServiceAPILog";
				meta.spInsert = "proc_WebServiceAPILogInsert";				
				meta.spUpdate = "proc_WebServiceAPILogUpdate";		
				meta.spDelete = "proc_WebServiceAPILogDelete";
				meta.spLoadAll = "proc_WebServiceAPILogLoadAll";
				meta.spLoadByPrimaryKey = "proc_WebServiceAPILogLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private WebServiceAPILogMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
