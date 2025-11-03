/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/10/2021 11:11:45 AM
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
	abstract public class esUserProgramLogCollection : esEntityCollectionWAuditLog
	{
		public esUserProgramLogCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "UserProgramLogCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esUserProgramLogQuery query)
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
			this.InitQuery(query as esUserProgramLogQuery);
		}
		#endregion
			
		virtual public UserProgramLog DetachEntity(UserProgramLog entity)
		{
			return base.DetachEntity(entity) as UserProgramLog;
		}
		
		virtual public UserProgramLog AttachEntity(UserProgramLog entity)
		{
			return base.AttachEntity(entity) as UserProgramLog;
		}
		
		virtual public void Combine(UserProgramLogCollection collection)
		{
			base.Combine(collection);
		}
		
		new public UserProgramLog this[int index]
		{
			get
			{
				return base[index] as UserProgramLog;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(UserProgramLog);
		}
	}

	[Serializable]
	abstract public class esUserProgramLog : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esUserProgramLogQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esUserProgramLog()
		{
		}
	
		public esUserProgramLog(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 userProgramLogID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(userProgramLogID);
			else
				return LoadByPrimaryKeyStoredProcedure(userProgramLogID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 userProgramLogID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(userProgramLogID);
			else
				return LoadByPrimaryKeyStoredProcedure(userProgramLogID);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int64 userProgramLogID)
		{
			esUserProgramLogQuery query = this.GetDynamicQuery();
			query.Where(query.UserProgramLogID == userProgramLogID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int64 userProgramLogID)
		{
			esParameters parms = new esParameters();
			parms.Add("UserProgramLogID",userProgramLogID);
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
						case "UserProgramLogID": this.str.UserProgramLogID = (string)value; break;
						case "UserLogID": this.str.UserLogID = (string)value; break;
						case "ProgramID": this.str.ProgramID = (string)value; break;
						case "AccessDateTime": this.str.AccessDateTime = (string)value; break;
						case "Parameter": this.str.Parameter = (string)value; break;
						case "ResponseTime": this.str.ResponseTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "UserProgramLogID":
						
							if (value == null || value is System.Int64)
								this.UserProgramLogID = (System.Int64?)value;
							break;
						case "UserLogID":
						
							if (value == null || value is System.Int64)
								this.UserLogID = (System.Int64?)value;
							break;
						case "AccessDateTime":
						
							if (value == null || value is System.DateTime)
								this.AccessDateTime = (System.DateTime?)value;
							break;
						case "ResponseTime":
						
							if (value == null || value is System.Int32)
								this.ResponseTime = (System.Int32?)value;
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
		/// Maps to UserProgramLog.UserProgramLogID
		/// </summary>
		virtual public System.Int64? UserProgramLogID
		{
			get
			{
				return base.GetSystemInt64(UserProgramLogMetadata.ColumnNames.UserProgramLogID);
			}
			
			set
			{
				base.SetSystemInt64(UserProgramLogMetadata.ColumnNames.UserProgramLogID, value);
			}
		}
		/// <summary>
		/// Maps to UserProgramLog.UserLogID
		/// </summary>
		virtual public System.Int64? UserLogID
		{
			get
			{
				return base.GetSystemInt64(UserProgramLogMetadata.ColumnNames.UserLogID);
			}
			
			set
			{
				base.SetSystemInt64(UserProgramLogMetadata.ColumnNames.UserLogID, value);
			}
		}
		/// <summary>
		/// Maps to UserProgramLog.ProgramID
		/// </summary>
		virtual public System.String ProgramID
		{
			get
			{
				return base.GetSystemString(UserProgramLogMetadata.ColumnNames.ProgramID);
			}
			
			set
			{
				base.SetSystemString(UserProgramLogMetadata.ColumnNames.ProgramID, value);
			}
		}
		/// <summary>
		/// Maps to UserProgramLog.AccessDateTime
		/// </summary>
		virtual public System.DateTime? AccessDateTime
		{
			get
			{
				return base.GetSystemDateTime(UserProgramLogMetadata.ColumnNames.AccessDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(UserProgramLogMetadata.ColumnNames.AccessDateTime, value);
			}
		}
		/// <summary>
		/// Maps to UserProgramLog.Parameter
		/// </summary>
		virtual public System.String Parameter
		{
			get
			{
				return base.GetSystemString(UserProgramLogMetadata.ColumnNames.Parameter);
			}
			
			set
			{
				base.SetSystemString(UserProgramLogMetadata.ColumnNames.Parameter, value);
			}
		}
		/// <summary>
		/// Maps to UserProgramLog.ResponseTime
		/// </summary>
		virtual public System.Int32? ResponseTime
		{
			get
			{
				return base.GetSystemInt32(UserProgramLogMetadata.ColumnNames.ResponseTime);
			}
			
			set
			{
				base.SetSystemInt32(UserProgramLogMetadata.ColumnNames.ResponseTime, value);
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
			public esStrings(esUserProgramLog entity)
			{
				this.entity = entity;
			}
			public System.String UserProgramLogID
			{
				get
				{
					System.Int64? data = entity.UserProgramLogID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UserProgramLogID = null;
					else entity.UserProgramLogID = Convert.ToInt64(value);
				}
			}
			public System.String UserLogID
			{
				get
				{
					System.Int64? data = entity.UserLogID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UserLogID = null;
					else entity.UserLogID = Convert.ToInt64(value);
				}
			}
			public System.String ProgramID
			{
				get
				{
					System.String data = entity.ProgramID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProgramID = null;
					else entity.ProgramID = Convert.ToString(value);
				}
			}
			public System.String AccessDateTime
			{
				get
				{
					System.DateTime? data = entity.AccessDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AccessDateTime = null;
					else entity.AccessDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String Parameter
			{
				get
				{
					System.String data = entity.Parameter;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Parameter = null;
					else entity.Parameter = Convert.ToString(value);
				}
			}
			public System.String ResponseTime
			{
				get
				{
					System.Int32? data = entity.ResponseTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResponseTime = null;
					else entity.ResponseTime = Convert.ToInt32(value);
				}
			}
			private esUserProgramLog entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esUserProgramLogQuery query)
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
				throw new Exception("esUserProgramLog can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class UserProgramLog : esUserProgramLog
	{	
	}

	[Serializable]
	abstract public class esUserProgramLogQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return UserProgramLogMetadata.Meta();
			}
		}	
			
		public esQueryItem UserProgramLogID
		{
			get
			{
				return new esQueryItem(this, UserProgramLogMetadata.ColumnNames.UserProgramLogID, esSystemType.Int64);
			}
		} 
			
		public esQueryItem UserLogID
		{
			get
			{
				return new esQueryItem(this, UserProgramLogMetadata.ColumnNames.UserLogID, esSystemType.Int64);
			}
		} 
			
		public esQueryItem ProgramID
		{
			get
			{
				return new esQueryItem(this, UserProgramLogMetadata.ColumnNames.ProgramID, esSystemType.String);
			}
		} 
			
		public esQueryItem AccessDateTime
		{
			get
			{
				return new esQueryItem(this, UserProgramLogMetadata.ColumnNames.AccessDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem Parameter
		{
			get
			{
				return new esQueryItem(this, UserProgramLogMetadata.ColumnNames.Parameter, esSystemType.String);
			}
		} 
			
		public esQueryItem ResponseTime
		{
			get
			{
				return new esQueryItem(this, UserProgramLogMetadata.ColumnNames.ResponseTime, esSystemType.Int32);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("UserProgramLogCollection")]
	public partial class UserProgramLogCollection : esUserProgramLogCollection, IEnumerable< UserProgramLog>
	{
		public UserProgramLogCollection()
		{

		}	
		
		public static implicit operator List< UserProgramLog>(UserProgramLogCollection coll)
		{
			List< UserProgramLog> list = new List< UserProgramLog>();
			
			foreach (UserProgramLog emp in coll)
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
				return  UserProgramLogMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new UserProgramLogQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new UserProgramLog(row);
		}

		override protected esEntity CreateEntity()
		{
			return new UserProgramLog();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public UserProgramLogQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new UserProgramLogQuery();
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
		public bool Load(UserProgramLogQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public UserProgramLog AddNew()
		{
			UserProgramLog entity = base.AddNewEntity() as UserProgramLog;
			
			return entity;		
		}
		public UserProgramLog FindByPrimaryKey(Int64 userProgramLogID)
		{
			return base.FindByPrimaryKey(userProgramLogID) as UserProgramLog;
		}

		#region IEnumerable< UserProgramLog> Members

		IEnumerator< UserProgramLog> IEnumerable< UserProgramLog>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as UserProgramLog;
			}
		}

		#endregion
		
		private UserProgramLogQuery query;
	}


	/// <summary>
	/// Encapsulates the 'UserProgramLog' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("UserProgramLog ({UserProgramLogID})")]
	[Serializable]
	public partial class UserProgramLog : esUserProgramLog
	{
		public UserProgramLog()
		{
		}	
	
		public UserProgramLog(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return UserProgramLogMetadata.Meta();
			}
		}	
	
		override protected esUserProgramLogQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new UserProgramLogQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public UserProgramLogQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new UserProgramLogQuery();
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
		public bool Load(UserProgramLogQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private UserProgramLogQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class UserProgramLogQuery : esUserProgramLogQuery
	{
		public UserProgramLogQuery()
		{

		}		
		
		public UserProgramLogQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "UserProgramLogQuery";
        }
	}

	[Serializable]
	public partial class UserProgramLogMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected UserProgramLogMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(UserProgramLogMetadata.ColumnNames.UserProgramLogID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = UserProgramLogMetadata.PropertyNames.UserProgramLogID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c); 
				
			c = new esColumnMetadata(UserProgramLogMetadata.ColumnNames.UserLogID, 1, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = UserProgramLogMetadata.PropertyNames.UserLogID;
			c.NumericPrecision = 19;
			_columns.Add(c); 
				
			c = new esColumnMetadata(UserProgramLogMetadata.ColumnNames.ProgramID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = UserProgramLogMetadata.PropertyNames.ProgramID;
			c.CharacterMaxLength = 30;
			_columns.Add(c); 
				
			c = new esColumnMetadata(UserProgramLogMetadata.ColumnNames.AccessDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = UserProgramLogMetadata.PropertyNames.AccessDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(UserProgramLogMetadata.ColumnNames.Parameter, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = UserProgramLogMetadata.PropertyNames.Parameter;
			c.CharacterMaxLength = 2000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(UserProgramLogMetadata.ColumnNames.ResponseTime, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = UserProgramLogMetadata.PropertyNames.ResponseTime;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public UserProgramLogMetadata Meta()
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
			public const string UserProgramLogID = "UserProgramLogID";
			public const string UserLogID = "UserLogID";
			public const string ProgramID = "ProgramID";
			public const string AccessDateTime = "AccessDateTime";
			public const string Parameter = "Parameter";
			public const string ResponseTime = "ResponseTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string UserProgramLogID = "UserProgramLogID";
			public const string UserLogID = "UserLogID";
			public const string ProgramID = "ProgramID";
			public const string AccessDateTime = "AccessDateTime";
			public const string Parameter = "Parameter";
			public const string ResponseTime = "ResponseTime";
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
			lock (typeof(UserProgramLogMetadata))
			{
				if(UserProgramLogMetadata.mapDelegates == null)
				{
					UserProgramLogMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (UserProgramLogMetadata.meta == null)
				{
					UserProgramLogMetadata.meta = new UserProgramLogMetadata();
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
				
				meta.AddTypeMap("UserProgramLogID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("UserLogID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("ProgramID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AccessDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Parameter", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ResponseTime", new esTypeMap("int", "System.Int32"));
		

				meta.Source = "UserProgramLog";
				meta.Destination = "UserProgramLog";
				meta.spInsert = "proc_UserProgramLogInsert";				
				meta.spUpdate = "proc_UserProgramLogUpdate";		
				meta.spDelete = "proc_UserProgramLogDelete";
				meta.spLoadAll = "proc_UserProgramLogLoadAll";
				meta.spLoadByPrimaryKey = "proc_UserProgramLogLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private UserProgramLogMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
