/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 4/4/2023 3:26:52 PM
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
	abstract public class esMenuVersionCollection : esEntityCollectionWAuditLog
	{
		public esMenuVersionCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "MenuVersionCollection";
		}

		#region Query Logic
		protected void InitQuery(esMenuVersionQuery query)
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
			this.InitQuery(query as esMenuVersionQuery);
		}
		#endregion

		virtual public MenuVersion DetachEntity(MenuVersion entity)
		{
			return base.DetachEntity(entity) as MenuVersion;
		}

		virtual public MenuVersion AttachEntity(MenuVersion entity)
		{
			return base.AttachEntity(entity) as MenuVersion;
		}

		virtual public void Combine(MenuVersionCollection collection)
		{
			base.Combine(collection);
		}

		new public MenuVersion this[int index]
		{
			get
			{
				return base[index] as MenuVersion;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MenuVersion);
		}
	}

	[Serializable]
	abstract public class esMenuVersion : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMenuVersionQuery GetDynamicQuery()
		{
			return null;
		}

		public esMenuVersion()
		{
		}

		public esMenuVersion(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String versionID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(versionID);
			else
				return LoadByPrimaryKeyStoredProcedure(versionID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String versionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(versionID);
			else
				return LoadByPrimaryKeyStoredProcedure(versionID);
		}

		private bool LoadByPrimaryKeyDynamic(String versionID)
		{
			esMenuVersionQuery query = this.GetDynamicQuery();
			query.Where(query.VersionID == versionID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String versionID)
		{
			esParameters parms = new esParameters();
			parms.Add("VersionID", versionID);
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
			if (this.Row == null) this.AddNew();

			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if (value == null || value is System.String)
				{
					// Use the strongly typed property
					switch (name)
					{
						case "VersionID": this.str.VersionID = (string)value; break;
						case "VersionName": this.str.VersionName = (string)value; break;
						case "Cycle": this.str.Cycle = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "IsExtra": this.str.IsExtra = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsPlusOneRule": this.str.IsPlusOneRule = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "Cycle":

							if (value == null || value is System.Int16)
								this.Cycle = (System.Int16?)value;
							break;
						case "IsActive":

							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "IsExtra":

							if (value == null || value is System.Boolean)
								this.IsExtra = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsPlusOneRule":

							if (value == null || value is System.Boolean)
								this.IsPlusOneRule = (System.Boolean?)value;
							break;

						default:
							break;
					}
				}
			}
			else if (this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}

		/// <summary>
		/// Maps to MenuVersion.VersionID
		/// </summary>
		virtual public System.String VersionID
		{
			get
			{
				return base.GetSystemString(MenuVersionMetadata.ColumnNames.VersionID);
			}

			set
			{
				base.SetSystemString(MenuVersionMetadata.ColumnNames.VersionID, value);
			}
		}
		/// <summary>
		/// Maps to MenuVersion.VersionName
		/// </summary>
		virtual public System.String VersionName
		{
			get
			{
				return base.GetSystemString(MenuVersionMetadata.ColumnNames.VersionName);
			}

			set
			{
				base.SetSystemString(MenuVersionMetadata.ColumnNames.VersionName, value);
			}
		}
		/// <summary>
		/// Maps to MenuVersion.Cycle
		/// </summary>
		virtual public System.Int16? Cycle
		{
			get
			{
				return base.GetSystemInt16(MenuVersionMetadata.ColumnNames.Cycle);
			}

			set
			{
				base.SetSystemInt16(MenuVersionMetadata.ColumnNames.Cycle, value);
			}
		}
		/// <summary>
		/// Maps to MenuVersion.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(MenuVersionMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(MenuVersionMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to MenuVersion.IsExtra
		/// </summary>
		virtual public System.Boolean? IsExtra
		{
			get
			{
				return base.GetSystemBoolean(MenuVersionMetadata.ColumnNames.IsExtra);
			}

			set
			{
				base.SetSystemBoolean(MenuVersionMetadata.ColumnNames.IsExtra, value);
			}
		}
		/// <summary>
		/// Maps to MenuVersion.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MenuVersionMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(MenuVersionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MenuVersion.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MenuVersionMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(MenuVersionMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to MenuVersion.IsPlusOneRule
		/// </summary>
		virtual public System.Boolean? IsPlusOneRule
		{
			get
			{
				return base.GetSystemBoolean(MenuVersionMetadata.ColumnNames.IsPlusOneRule);
			}

			set
			{
				base.SetSystemBoolean(MenuVersionMetadata.ColumnNames.IsPlusOneRule, value);
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
		[BrowsableAttribute(false)]
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
			public esStrings(esMenuVersion entity)
			{
				this.entity = entity;
			}
			public System.String VersionID
			{
				get
				{
					System.String data = entity.VersionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VersionID = null;
					else entity.VersionID = Convert.ToString(value);
				}
			}
			public System.String VersionName
			{
				get
				{
					System.String data = entity.VersionName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VersionName = null;
					else entity.VersionName = Convert.ToString(value);
				}
			}
			public System.String Cycle
			{
				get
				{
					System.Int16? data = entity.Cycle;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Cycle = null;
					else entity.Cycle = Convert.ToInt16(value);
				}
			}
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
				}
			}
			public System.String IsExtra
			{
				get
				{
					System.Boolean? data = entity.IsExtra;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsExtra = null;
					else entity.IsExtra = Convert.ToBoolean(value);
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
			public System.String IsPlusOneRule
			{
				get
				{
					System.Boolean? data = entity.IsPlusOneRule;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPlusOneRule = null;
					else entity.IsPlusOneRule = Convert.ToBoolean(value);
				}
			}
			private esMenuVersion entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMenuVersionQuery query)
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
				throw new Exception("esMenuVersion can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class MenuVersion : esMenuVersion
	{
	}

	[Serializable]
	abstract public class esMenuVersionQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return MenuVersionMetadata.Meta();
			}
		}

		public esQueryItem VersionID
		{
			get
			{
				return new esQueryItem(this, MenuVersionMetadata.ColumnNames.VersionID, esSystemType.String);
			}
		}

		public esQueryItem VersionName
		{
			get
			{
				return new esQueryItem(this, MenuVersionMetadata.ColumnNames.VersionName, esSystemType.String);
			}
		}

		public esQueryItem Cycle
		{
			get
			{
				return new esQueryItem(this, MenuVersionMetadata.ColumnNames.Cycle, esSystemType.Int16);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, MenuVersionMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem IsExtra
		{
			get
			{
				return new esQueryItem(this, MenuVersionMetadata.ColumnNames.IsExtra, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MenuVersionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MenuVersionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsPlusOneRule
		{
			get
			{
				return new esQueryItem(this, MenuVersionMetadata.ColumnNames.IsPlusOneRule, esSystemType.Boolean);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MenuVersionCollection")]
	public partial class MenuVersionCollection : esMenuVersionCollection, IEnumerable<MenuVersion>
	{
		public MenuVersionCollection()
		{

		}

		public static implicit operator List<MenuVersion>(MenuVersionCollection coll)
		{
			List<MenuVersion> list = new List<MenuVersion>();

			foreach (MenuVersion emp in coll)
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
				return MenuVersionMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MenuVersionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MenuVersion(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MenuVersion();
		}

		#endregion

		[BrowsableAttribute(false)]
		public MenuVersionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MenuVersionQuery();
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
		public bool Load(MenuVersionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public MenuVersion AddNew()
		{
			MenuVersion entity = base.AddNewEntity() as MenuVersion;

			return entity;
		}
		public MenuVersion FindByPrimaryKey(String versionID)
		{
			return base.FindByPrimaryKey(versionID) as MenuVersion;
		}

		#region IEnumerable< MenuVersion> Members

		IEnumerator<MenuVersion> IEnumerable<MenuVersion>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as MenuVersion;
			}
		}

		#endregion

		private MenuVersionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MenuVersion' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("MenuVersion ({VersionID})")]
	[Serializable]
	public partial class MenuVersion : esMenuVersion
	{
		public MenuVersion()
		{
		}

		public MenuVersion(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MenuVersionMetadata.Meta();
			}
		}

		override protected esMenuVersionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MenuVersionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public MenuVersionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MenuVersionQuery();
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
		public bool Load(MenuVersionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private MenuVersionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class MenuVersionQuery : esMenuVersionQuery
	{
		public MenuVersionQuery()
		{

		}

		public MenuVersionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "MenuVersionQuery";
		}
	}

	[Serializable]
	public partial class MenuVersionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MenuVersionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MenuVersionMetadata.ColumnNames.VersionID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = MenuVersionMetadata.PropertyNames.VersionID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(MenuVersionMetadata.ColumnNames.VersionName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = MenuVersionMetadata.PropertyNames.VersionName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);

			c = new esColumnMetadata(MenuVersionMetadata.ColumnNames.Cycle, 2, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = MenuVersionMetadata.PropertyNames.Cycle;
			c.NumericPrecision = 5;
			_columns.Add(c);

			c = new esColumnMetadata(MenuVersionMetadata.ColumnNames.IsActive, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MenuVersionMetadata.PropertyNames.IsActive;
			_columns.Add(c);

			c = new esColumnMetadata(MenuVersionMetadata.ColumnNames.IsExtra, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MenuVersionMetadata.PropertyNames.IsExtra;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MenuVersionMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MenuVersionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MenuVersionMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = MenuVersionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MenuVersionMetadata.ColumnNames.IsPlusOneRule, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MenuVersionMetadata.PropertyNames.IsPlusOneRule;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public MenuVersionMetadata Meta()
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
			get { return base._columns; }
		}

		#region ColumnNames
		public class ColumnNames
		{
			public const string VersionID = "VersionID";
			public const string VersionName = "VersionName";
			public const string Cycle = "Cycle";
			public const string IsActive = "IsActive";
			public const string IsExtra = "IsExtra";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsPlusOneRule = "IsPlusOneRule";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string VersionID = "VersionID";
			public const string VersionName = "VersionName";
			public const string Cycle = "Cycle";
			public const string IsActive = "IsActive";
			public const string IsExtra = "IsExtra";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsPlusOneRule = "IsPlusOneRule";
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
			lock (typeof(MenuVersionMetadata))
			{
				if (MenuVersionMetadata.mapDelegates == null)
				{
					MenuVersionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (MenuVersionMetadata.meta == null)
				{
					MenuVersionMetadata.meta = new MenuVersionMetadata();
				}

				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if (!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();

				meta.AddTypeMap("VersionID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VersionName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Cycle", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsExtra", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPlusOneRule", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "MenuVersion";
				meta.Destination = "MenuVersion";
				meta.spInsert = "proc_MenuVersionInsert";
				meta.spUpdate = "proc_MenuVersionUpdate";
				meta.spDelete = "proc_MenuVersionDelete";
				meta.spLoadAll = "proc_MenuVersionLoadAll";
				meta.spLoadByPrimaryKey = "proc_MenuVersionLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MenuVersionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
