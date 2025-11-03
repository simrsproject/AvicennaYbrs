/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/16/2020 7:09:21 PM
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
	abstract public class esThrScheduleItemCollection : esEntityCollectionWAuditLog
	{
		public esThrScheduleItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ThrScheduleItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esThrScheduleItemQuery query)
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
			this.InitQuery(query as esThrScheduleItemQuery);
		}
		#endregion

		virtual public ThrScheduleItem DetachEntity(ThrScheduleItem entity)
		{
			return base.DetachEntity(entity) as ThrScheduleItem;
		}

		virtual public ThrScheduleItem AttachEntity(ThrScheduleItem entity)
		{
			return base.AttachEntity(entity) as ThrScheduleItem;
		}

		virtual public void Combine(ThrScheduleItemCollection collection)
		{
			base.Combine(collection);
		}

		new public ThrScheduleItem this[int index]
		{
			get
			{
				return base[index] as ThrScheduleItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ThrScheduleItem);
		}
	}

	[Serializable]
	abstract public class esThrScheduleItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esThrScheduleItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esThrScheduleItem()
		{
		}

		public esThrScheduleItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 counterItemID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(counterItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(counterItemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 counterItemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(counterItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(counterItemID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 counterItemID)
		{
			esThrScheduleItemQuery query = this.GetDynamicQuery();
			query.Where(query.CounterItemID == counterItemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 counterItemID)
		{
			esParameters parms = new esParameters();
			parms.Add("CounterItemID", counterItemID);
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
						case "CounterItemID": this.str.CounterItemID = (string)value; break;
						case "CounterID": this.str.CounterID = (string)value; break;
						case "SRReligion": this.str.SRReligion = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "CounterItemID":

							if (value == null || value is System.Int32)
								this.CounterItemID = (System.Int32?)value;
							break;
						case "CounterID":

							if (value == null || value is System.Int32)
								this.CounterID = (System.Int32?)value;
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
		/// Maps to ThrScheduleItem.CounterItemID
		/// </summary>
		virtual public System.Int32? CounterItemID
		{
			get
			{
				return base.GetSystemInt32(ThrScheduleItemMetadata.ColumnNames.CounterItemID);
			}

			set
			{
				base.SetSystemInt32(ThrScheduleItemMetadata.ColumnNames.CounterItemID, value);
			}
		}
		/// <summary>
		/// Maps to ThrScheduleItem.CounterID
		/// </summary>
		virtual public System.Int32? CounterID
		{
			get
			{
				return base.GetSystemInt32(ThrScheduleItemMetadata.ColumnNames.CounterID);
			}

			set
			{
				base.SetSystemInt32(ThrScheduleItemMetadata.ColumnNames.CounterID, value);
			}
		}
		/// <summary>
		/// Maps to ThrScheduleItem.SRReligion
		/// </summary>
		virtual public System.String SRReligion
		{
			get
			{
				return base.GetSystemString(ThrScheduleItemMetadata.ColumnNames.SRReligion);
			}

			set
			{
				base.SetSystemString(ThrScheduleItemMetadata.ColumnNames.SRReligion, value);
			}
		}
		/// <summary>
		/// Maps to ThrScheduleItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ThrScheduleItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ThrScheduleItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ThrScheduleItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ThrScheduleItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ThrScheduleItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esThrScheduleItem entity)
			{
				this.entity = entity;
			}
			public System.String CounterItemID
			{
				get
				{
					System.Int32? data = entity.CounterItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CounterItemID = null;
					else entity.CounterItemID = Convert.ToInt32(value);
				}
			}
			public System.String CounterID
			{
				get
				{
					System.Int32? data = entity.CounterID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CounterID = null;
					else entity.CounterID = Convert.ToInt32(value);
				}
			}
			public System.String SRReligion
			{
				get
				{
					System.String data = entity.SRReligion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRReligion = null;
					else entity.SRReligion = Convert.ToString(value);
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
			private esThrScheduleItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esThrScheduleItemQuery query)
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
				throw new Exception("esThrScheduleItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ThrScheduleItem : esThrScheduleItem
	{
	}

	[Serializable]
	abstract public class esThrScheduleItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ThrScheduleItemMetadata.Meta();
			}
		}

		public esQueryItem CounterItemID
		{
			get
			{
				return new esQueryItem(this, ThrScheduleItemMetadata.ColumnNames.CounterItemID, esSystemType.Int32);
			}
		}

		public esQueryItem CounterID
		{
			get
			{
				return new esQueryItem(this, ThrScheduleItemMetadata.ColumnNames.CounterID, esSystemType.Int32);
			}
		}

		public esQueryItem SRReligion
		{
			get
			{
				return new esQueryItem(this, ThrScheduleItemMetadata.ColumnNames.SRReligion, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ThrScheduleItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ThrScheduleItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ThrScheduleItemCollection")]
	public partial class ThrScheduleItemCollection : esThrScheduleItemCollection, IEnumerable<ThrScheduleItem>
	{
		public ThrScheduleItemCollection()
		{

		}

		public static implicit operator List<ThrScheduleItem>(ThrScheduleItemCollection coll)
		{
			List<ThrScheduleItem> list = new List<ThrScheduleItem>();

			foreach (ThrScheduleItem emp in coll)
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
				return ThrScheduleItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ThrScheduleItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ThrScheduleItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ThrScheduleItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ThrScheduleItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ThrScheduleItemQuery();
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
		public bool Load(ThrScheduleItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ThrScheduleItem AddNew()
		{
			ThrScheduleItem entity = base.AddNewEntity() as ThrScheduleItem;

			return entity;
		}
		public ThrScheduleItem FindByPrimaryKey(Int32 counterItemID)
		{
			return base.FindByPrimaryKey(counterItemID) as ThrScheduleItem;
		}

		#region IEnumerable< ThrScheduleItem> Members

		IEnumerator<ThrScheduleItem> IEnumerable<ThrScheduleItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ThrScheduleItem;
			}
		}

		#endregion

		private ThrScheduleItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ThrScheduleItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ThrScheduleItem ({CounterItemID})")]
	[Serializable]
	public partial class ThrScheduleItem : esThrScheduleItem
	{
		public ThrScheduleItem()
		{
		}

		public ThrScheduleItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ThrScheduleItemMetadata.Meta();
			}
		}

		override protected esThrScheduleItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ThrScheduleItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ThrScheduleItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ThrScheduleItemQuery();
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
		public bool Load(ThrScheduleItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ThrScheduleItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ThrScheduleItemQuery : esThrScheduleItemQuery
	{
		public ThrScheduleItemQuery()
		{

		}

		public ThrScheduleItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ThrScheduleItemQuery";
		}
	}

	[Serializable]
	public partial class ThrScheduleItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ThrScheduleItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ThrScheduleItemMetadata.ColumnNames.CounterItemID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ThrScheduleItemMetadata.PropertyNames.CounterItemID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ThrScheduleItemMetadata.ColumnNames.CounterID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ThrScheduleItemMetadata.PropertyNames.CounterID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ThrScheduleItemMetadata.ColumnNames.SRReligion, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ThrScheduleItemMetadata.PropertyNames.SRReligion;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ThrScheduleItemMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ThrScheduleItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ThrScheduleItemMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ThrScheduleItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ThrScheduleItemMetadata Meta()
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
			public const string CounterItemID = "CounterItemID";
			public const string CounterID = "CounterID";
			public const string SRReligion = "SRReligion";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string CounterItemID = "CounterItemID";
			public const string CounterID = "CounterID";
			public const string SRReligion = "SRReligion";
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
			lock (typeof(ThrScheduleItemMetadata))
			{
				if (ThrScheduleItemMetadata.mapDelegates == null)
				{
					ThrScheduleItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ThrScheduleItemMetadata.meta == null)
				{
					ThrScheduleItemMetadata.meta = new ThrScheduleItemMetadata();
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

				meta.AddTypeMap("CounterItemID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CounterID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRReligion", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "ThrScheduleItem";
				meta.Destination = "ThrScheduleItem";
				meta.spInsert = "proc_ThrScheduleItemInsert";
				meta.spUpdate = "proc_ThrScheduleItemUpdate";
				meta.spDelete = "proc_ThrScheduleItemDelete";
				meta.spLoadAll = "proc_ThrScheduleItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_ThrScheduleItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ThrScheduleItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
