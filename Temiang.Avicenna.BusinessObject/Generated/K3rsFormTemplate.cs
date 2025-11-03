/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/18/2021 11:37:55 AM
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
	abstract public class esK3rsFormTemplateCollection : esEntityCollectionWAuditLog
	{
		public esK3rsFormTemplateCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "K3rsFormTemplateCollection";
		}

		#region Query Logic
		protected void InitQuery(esK3rsFormTemplateQuery query)
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
			this.InitQuery(query as esK3rsFormTemplateQuery);
		}
		#endregion

		virtual public K3rsFormTemplate DetachEntity(K3rsFormTemplate entity)
		{
			return base.DetachEntity(entity) as K3rsFormTemplate;
		}

		virtual public K3rsFormTemplate AttachEntity(K3rsFormTemplate entity)
		{
			return base.AttachEntity(entity) as K3rsFormTemplate;
		}

		virtual public void Combine(K3rsFormTemplateCollection collection)
		{
			base.Combine(collection);
		}

		new public K3rsFormTemplate this[int index]
		{
			get
			{
				return base[index] as K3rsFormTemplate;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(K3rsFormTemplate);
		}
	}

	[Serializable]
	abstract public class esK3rsFormTemplate : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esK3rsFormTemplateQuery GetDynamicQuery()
		{
			return null;
		}

		public esK3rsFormTemplate()
		{
		}

		public esK3rsFormTemplate(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 templateID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(templateID);
			else
				return LoadByPrimaryKeyStoredProcedure(templateID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 templateID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(templateID);
			else
				return LoadByPrimaryKeyStoredProcedure(templateID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 templateID)
		{
			esK3rsFormTemplateQuery query = this.GetDynamicQuery();
			query.Where(query.TemplateID == templateID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 templateID)
		{
			esParameters parms = new esParameters();
			parms.Add("TemplateID", templateID);
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
						case "TemplateID": this.str.TemplateID = (string)value; break;
						case "TemplateName": this.str.TemplateName = (string)value; break;
						case "Result": this.str.Result = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "TemplateID":

							if (value == null || value is System.Int32)
								this.TemplateID = (System.Int32?)value;
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
		/// Maps to K3rsFormTemplate.TemplateID
		/// </summary>
		virtual public System.Int32? TemplateID
		{
			get
			{
				return base.GetSystemInt32(K3rsFormTemplateMetadata.ColumnNames.TemplateID);
			}

			set
			{
				base.SetSystemInt32(K3rsFormTemplateMetadata.ColumnNames.TemplateID, value);
			}
		}
		/// <summary>
		/// Maps to K3rsFormTemplate.TemplateName
		/// </summary>
		virtual public System.String TemplateName
		{
			get
			{
				return base.GetSystemString(K3rsFormTemplateMetadata.ColumnNames.TemplateName);
			}

			set
			{
				base.SetSystemString(K3rsFormTemplateMetadata.ColumnNames.TemplateName, value);
			}
		}
		/// <summary>
		/// Maps to K3rsFormTemplate.Result
		/// </summary>
		virtual public System.String Result
		{
			get
			{
				return base.GetSystemString(K3rsFormTemplateMetadata.ColumnNames.Result);
			}

			set
			{
				base.SetSystemString(K3rsFormTemplateMetadata.ColumnNames.Result, value);
			}
		}
		/// <summary>
		/// Maps to K3rsFormTemplate.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(K3rsFormTemplateMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(K3rsFormTemplateMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to K3rsFormTemplate.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(K3rsFormTemplateMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(K3rsFormTemplateMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esK3rsFormTemplate entity)
			{
				this.entity = entity;
			}
			public System.String TemplateID
			{
				get
				{
					System.Int32? data = entity.TemplateID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TemplateID = null;
					else entity.TemplateID = Convert.ToInt32(value);
				}
			}
			public System.String TemplateName
			{
				get
				{
					System.String data = entity.TemplateName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TemplateName = null;
					else entity.TemplateName = Convert.ToString(value);
				}
			}
			public System.String Result
			{
				get
				{
					System.String data = entity.Result;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Result = null;
					else entity.Result = Convert.ToString(value);
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
			private esK3rsFormTemplate entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esK3rsFormTemplateQuery query)
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
				throw new Exception("esK3rsFormTemplate can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class K3rsFormTemplate : esK3rsFormTemplate
	{
	}

	[Serializable]
	abstract public class esK3rsFormTemplateQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return K3rsFormTemplateMetadata.Meta();
			}
		}

		public esQueryItem TemplateID
		{
			get
			{
				return new esQueryItem(this, K3rsFormTemplateMetadata.ColumnNames.TemplateID, esSystemType.Int32);
			}
		}

		public esQueryItem TemplateName
		{
			get
			{
				return new esQueryItem(this, K3rsFormTemplateMetadata.ColumnNames.TemplateName, esSystemType.String);
			}
		}

		public esQueryItem Result
		{
			get
			{
				return new esQueryItem(this, K3rsFormTemplateMetadata.ColumnNames.Result, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, K3rsFormTemplateMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, K3rsFormTemplateMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("K3rsFormTemplateCollection")]
	public partial class K3rsFormTemplateCollection : esK3rsFormTemplateCollection, IEnumerable<K3rsFormTemplate>
	{
		public K3rsFormTemplateCollection()
		{

		}

		public static implicit operator List<K3rsFormTemplate>(K3rsFormTemplateCollection coll)
		{
			List<K3rsFormTemplate> list = new List<K3rsFormTemplate>();

			foreach (K3rsFormTemplate emp in coll)
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
				return K3rsFormTemplateMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new K3rsFormTemplateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new K3rsFormTemplate(row);
		}

		override protected esEntity CreateEntity()
		{
			return new K3rsFormTemplate();
		}

		#endregion

		[BrowsableAttribute(false)]
		public K3rsFormTemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new K3rsFormTemplateQuery();
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
		public bool Load(K3rsFormTemplateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public K3rsFormTemplate AddNew()
		{
			K3rsFormTemplate entity = base.AddNewEntity() as K3rsFormTemplate;

			return entity;
		}
		public K3rsFormTemplate FindByPrimaryKey(Int32 templateID)
		{
			return base.FindByPrimaryKey(templateID) as K3rsFormTemplate;
		}

		#region IEnumerable< K3rsFormTemplate> Members

		IEnumerator<K3rsFormTemplate> IEnumerable<K3rsFormTemplate>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as K3rsFormTemplate;
			}
		}

		#endregion

		private K3rsFormTemplateQuery query;
	}


	/// <summary>
	/// Encapsulates the 'K3rsFormTemplate' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("K3rsFormTemplate ({TemplateID})")]
	[Serializable]
	public partial class K3rsFormTemplate : esK3rsFormTemplate
	{
		public K3rsFormTemplate()
		{
		}

		public K3rsFormTemplate(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return K3rsFormTemplateMetadata.Meta();
			}
		}

		override protected esK3rsFormTemplateQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new K3rsFormTemplateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public K3rsFormTemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new K3rsFormTemplateQuery();
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
		public bool Load(K3rsFormTemplateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private K3rsFormTemplateQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class K3rsFormTemplateQuery : esK3rsFormTemplateQuery
	{
		public K3rsFormTemplateQuery()
		{

		}

		public K3rsFormTemplateQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "K3rsFormTemplateQuery";
		}
	}

	[Serializable]
	public partial class K3rsFormTemplateMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected K3rsFormTemplateMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(K3rsFormTemplateMetadata.ColumnNames.TemplateID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = K3rsFormTemplateMetadata.PropertyNames.TemplateID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(K3rsFormTemplateMetadata.ColumnNames.TemplateName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = K3rsFormTemplateMetadata.PropertyNames.TemplateName;
			c.CharacterMaxLength = 100;
			_columns.Add(c);

			c = new esColumnMetadata(K3rsFormTemplateMetadata.ColumnNames.Result, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = K3rsFormTemplateMetadata.PropertyNames.Result;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c);

			c = new esColumnMetadata(K3rsFormTemplateMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = K3rsFormTemplateMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(K3rsFormTemplateMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = K3rsFormTemplateMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public K3rsFormTemplateMetadata Meta()
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
			public const string TemplateID = "TemplateID";
			public const string TemplateName = "TemplateName";
			public const string Result = "Result";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TemplateID = "TemplateID";
			public const string TemplateName = "TemplateName";
			public const string Result = "Result";
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
			lock (typeof(K3rsFormTemplateMetadata))
			{
				if (K3rsFormTemplateMetadata.mapDelegates == null)
				{
					K3rsFormTemplateMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (K3rsFormTemplateMetadata.meta == null)
				{
					K3rsFormTemplateMetadata.meta = new K3rsFormTemplateMetadata();
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

				meta.AddTypeMap("TemplateID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("TemplateName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Result", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "K3rsFormTemplate";
				meta.Destination = "K3rsFormTemplate";
				meta.spInsert = "proc_K3rsFormTemplateInsert";
				meta.spUpdate = "proc_K3rsFormTemplateUpdate";
				meta.spDelete = "proc_K3rsFormTemplateDelete";
				meta.spLoadAll = "proc_K3rsFormTemplateLoadAll";
				meta.spLoadByPrimaryKey = "proc_K3rsFormTemplateLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private K3rsFormTemplateMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
