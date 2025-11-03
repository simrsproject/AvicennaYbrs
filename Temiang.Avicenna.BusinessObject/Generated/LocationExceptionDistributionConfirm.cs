/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/26/2022 10:25:02 AM
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
	abstract public class esLocationExceptionDistributionConfirmCollection : esEntityCollectionWAuditLog
	{
		public esLocationExceptionDistributionConfirmCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "LocationExceptionDistributionConfirmCollection";
		}

		#region Query Logic
		protected void InitQuery(esLocationExceptionDistributionConfirmQuery query)
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
			this.InitQuery(query as esLocationExceptionDistributionConfirmQuery);
		}
		#endregion

		virtual public LocationExceptionDistributionConfirm DetachEntity(LocationExceptionDistributionConfirm entity)
		{
			return base.DetachEntity(entity) as LocationExceptionDistributionConfirm;
		}

		virtual public LocationExceptionDistributionConfirm AttachEntity(LocationExceptionDistributionConfirm entity)
		{
			return base.AttachEntity(entity) as LocationExceptionDistributionConfirm;
		}

		virtual public void Combine(LocationExceptionDistributionConfirmCollection collection)
		{
			base.Combine(collection);
		}

		new public LocationExceptionDistributionConfirm this[int index]
		{
			get
			{
				return base[index] as LocationExceptionDistributionConfirm;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LocationExceptionDistributionConfirm);
		}
	}

	[Serializable]
	abstract public class esLocationExceptionDistributionConfirm : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLocationExceptionDistributionConfirmQuery GetDynamicQuery()
		{
			return null;
		}

		public esLocationExceptionDistributionConfirm()
		{
		}

		public esLocationExceptionDistributionConfirm(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String locationID, String locationExceptionID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(locationID, locationExceptionID);
			else
				return LoadByPrimaryKeyStoredProcedure(locationID, locationExceptionID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String locationID, String locationExceptionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(locationID, locationExceptionID);
			else
				return LoadByPrimaryKeyStoredProcedure(locationID, locationExceptionID);
		}

		private bool LoadByPrimaryKeyDynamic(String locationID, String locationExceptionID)
		{
			esLocationExceptionDistributionConfirmQuery query = this.GetDynamicQuery();
			query.Where(query.LocationID == locationID, query.LocationExceptionID == locationExceptionID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String locationID, String locationExceptionID)
		{
			esParameters parms = new esParameters();
			parms.Add("LocationID", locationID);
			parms.Add("LocationExceptionID", locationExceptionID);
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
						case "LocationID": this.str.LocationID = (string)value; break;
						case "LocationExceptionID": this.str.LocationExceptionID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
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
		/// Maps to LocationExceptionDistributionConfirm.LocationID
		/// </summary>
		virtual public System.String LocationID
		{
			get
			{
				return base.GetSystemString(LocationExceptionDistributionConfirmMetadata.ColumnNames.LocationID);
			}

			set
			{
				base.SetSystemString(LocationExceptionDistributionConfirmMetadata.ColumnNames.LocationID, value);
			}
		}
		/// <summary>
		/// Maps to LocationExceptionDistributionConfirm.LocationExceptionID
		/// </summary>
		virtual public System.String LocationExceptionID
		{
			get
			{
				return base.GetSystemString(LocationExceptionDistributionConfirmMetadata.ColumnNames.LocationExceptionID);
			}

			set
			{
				base.SetSystemString(LocationExceptionDistributionConfirmMetadata.ColumnNames.LocationExceptionID, value);
			}
		}
		/// <summary>
		/// Maps to LocationExceptionDistributionConfirm.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(LocationExceptionDistributionConfirmMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(LocationExceptionDistributionConfirmMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to LocationExceptionDistributionConfirm.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(LocationExceptionDistributionConfirmMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(LocationExceptionDistributionConfirmMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esLocationExceptionDistributionConfirm entity)
			{
				this.entity = entity;
			}
			public System.String LocationID
			{
				get
				{
					System.String data = entity.LocationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LocationID = null;
					else entity.LocationID = Convert.ToString(value);
				}
			}
			public System.String LocationExceptionID
			{
				get
				{
					System.String data = entity.LocationExceptionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LocationExceptionID = null;
					else entity.LocationExceptionID = Convert.ToString(value);
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
			private esLocationExceptionDistributionConfirm entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLocationExceptionDistributionConfirmQuery query)
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
				throw new Exception("esLocationExceptionDistributionConfirm can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class LocationExceptionDistributionConfirm : esLocationExceptionDistributionConfirm
	{
	}

	[Serializable]
	abstract public class esLocationExceptionDistributionConfirmQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return LocationExceptionDistributionConfirmMetadata.Meta();
			}
		}

		public esQueryItem LocationID
		{
			get
			{
				return new esQueryItem(this, LocationExceptionDistributionConfirmMetadata.ColumnNames.LocationID, esSystemType.String);
			}
		}

		public esQueryItem LocationExceptionID
		{
			get
			{
				return new esQueryItem(this, LocationExceptionDistributionConfirmMetadata.ColumnNames.LocationExceptionID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, LocationExceptionDistributionConfirmMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, LocationExceptionDistributionConfirmMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LocationExceptionDistributionConfirmCollection")]
	public partial class LocationExceptionDistributionConfirmCollection : esLocationExceptionDistributionConfirmCollection, IEnumerable<LocationExceptionDistributionConfirm>
	{
		public LocationExceptionDistributionConfirmCollection()
		{

		}

		public static implicit operator List<LocationExceptionDistributionConfirm>(LocationExceptionDistributionConfirmCollection coll)
		{
			List<LocationExceptionDistributionConfirm> list = new List<LocationExceptionDistributionConfirm>();

			foreach (LocationExceptionDistributionConfirm emp in coll)
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
				return LocationExceptionDistributionConfirmMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LocationExceptionDistributionConfirmQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LocationExceptionDistributionConfirm(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LocationExceptionDistributionConfirm();
		}

		#endregion

		[BrowsableAttribute(false)]
		public LocationExceptionDistributionConfirmQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LocationExceptionDistributionConfirmQuery();
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
		public bool Load(LocationExceptionDistributionConfirmQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public LocationExceptionDistributionConfirm AddNew()
		{
			LocationExceptionDistributionConfirm entity = base.AddNewEntity() as LocationExceptionDistributionConfirm;

			return entity;
		}
		public LocationExceptionDistributionConfirm FindByPrimaryKey(String locationID, String locationExceptionID)
		{
			return base.FindByPrimaryKey(locationID, locationExceptionID) as LocationExceptionDistributionConfirm;
		}

		#region IEnumerable< LocationExceptionDistributionConfirm> Members

		IEnumerator<LocationExceptionDistributionConfirm> IEnumerable<LocationExceptionDistributionConfirm>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as LocationExceptionDistributionConfirm;
			}
		}

		#endregion

		private LocationExceptionDistributionConfirmQuery query;
	}


	/// <summary>
	/// Encapsulates the 'LocationExceptionDistributionConfirm' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("LocationExceptionDistributionConfirm ({LocationID, LocationExceptionID})")]
	[Serializable]
	public partial class LocationExceptionDistributionConfirm : esLocationExceptionDistributionConfirm
	{
		public LocationExceptionDistributionConfirm()
		{
		}

		public LocationExceptionDistributionConfirm(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LocationExceptionDistributionConfirmMetadata.Meta();
			}
		}

		override protected esLocationExceptionDistributionConfirmQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LocationExceptionDistributionConfirmQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public LocationExceptionDistributionConfirmQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LocationExceptionDistributionConfirmQuery();
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
		public bool Load(LocationExceptionDistributionConfirmQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private LocationExceptionDistributionConfirmQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class LocationExceptionDistributionConfirmQuery : esLocationExceptionDistributionConfirmQuery
	{
		public LocationExceptionDistributionConfirmQuery()
		{

		}

		public LocationExceptionDistributionConfirmQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "LocationExceptionDistributionConfirmQuery";
		}
	}

	[Serializable]
	public partial class LocationExceptionDistributionConfirmMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LocationExceptionDistributionConfirmMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LocationExceptionDistributionConfirmMetadata.ColumnNames.LocationID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = LocationExceptionDistributionConfirmMetadata.PropertyNames.LocationID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(LocationExceptionDistributionConfirmMetadata.ColumnNames.LocationExceptionID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = LocationExceptionDistributionConfirmMetadata.PropertyNames.LocationExceptionID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(LocationExceptionDistributionConfirmMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LocationExceptionDistributionConfirmMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationExceptionDistributionConfirmMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = LocationExceptionDistributionConfirmMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public LocationExceptionDistributionConfirmMetadata Meta()
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
			public const string LocationID = "LocationID";
			public const string LocationExceptionID = "LocationExceptionID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string LocationID = "LocationID";
			public const string LocationExceptionID = "LocationExceptionID";
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
			lock (typeof(LocationExceptionDistributionConfirmMetadata))
			{
				if (LocationExceptionDistributionConfirmMetadata.mapDelegates == null)
				{
					LocationExceptionDistributionConfirmMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (LocationExceptionDistributionConfirmMetadata.meta == null)
				{
					LocationExceptionDistributionConfirmMetadata.meta = new LocationExceptionDistributionConfirmMetadata();
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

				meta.AddTypeMap("LocationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LocationExceptionID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "LocationExceptionDistributionConfirm";
				meta.Destination = "LocationExceptionDistributionConfirm";
				meta.spInsert = "proc_LocationExceptionDistributionConfirmInsert";
				meta.spUpdate = "proc_LocationExceptionDistributionConfirmUpdate";
				meta.spDelete = "proc_LocationExceptionDistributionConfirmDelete";
				meta.spLoadAll = "proc_LocationExceptionDistributionConfirmLoadAll";
				meta.spLoadByPrimaryKey = "proc_LocationExceptionDistributionConfirmLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LocationExceptionDistributionConfirmMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
