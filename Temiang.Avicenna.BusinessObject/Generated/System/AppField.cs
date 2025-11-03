/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/12/2023 6:25:32 PM
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
	abstract public class esAppFieldCollection : esEntityCollectionWAuditLog
	{
		public esAppFieldCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "AppFieldCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esAppFieldQuery query)
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
			this.InitQuery(query as esAppFieldQuery);
		}
		#endregion
			
		virtual public AppField DetachEntity(AppField entity)
		{
			return base.DetachEntity(entity) as AppField;
		}
		
		virtual public AppField AttachEntity(AppField entity)
		{
			return base.AttachEntity(entity) as AppField;
		}
		
		virtual public void Combine(AppFieldCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AppField this[int index]
		{
			get
			{
				return base[index] as AppField;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppField);
		}
	}

	[Serializable]
	abstract public class esAppField : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppFieldQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esAppField()
		{
		}
	
		public esAppField(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 fieldID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(fieldID);
			else
				return LoadByPrimaryKeyStoredProcedure(fieldID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 fieldID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(fieldID);
			else
				return LoadByPrimaryKeyStoredProcedure(fieldID);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 fieldID)
		{
			esAppFieldQuery query = this.GetDynamicQuery();
			query.Where(query.FieldID == fieldID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 fieldID)
		{
			esParameters parms = new esParameters();
			parms.Add("FieldID",fieldID);
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
						case "FieldID": this.str.FieldID = (string)value; break;
						case "FieldName": this.str.FieldName = (string)value; break;
						case "FieldType": this.str.FieldType = (string)value; break;
						case "Description": this.str.Description = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "FieldID":
						
							if (value == null || value is System.Int32)
								this.FieldID = (System.Int32?)value;
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
		/// Maps to AppField.FieldID
		/// </summary>
		virtual public System.Int32? FieldID
		{
			get
			{
				return base.GetSystemInt32(AppFieldMetadata.ColumnNames.FieldID);
			}
			
			set
			{
				base.SetSystemInt32(AppFieldMetadata.ColumnNames.FieldID, value);
			}
		}
		/// <summary>
		/// Maps to AppField.FieldName
		/// </summary>
		virtual public System.String FieldName
		{
			get
			{
				return base.GetSystemString(AppFieldMetadata.ColumnNames.FieldName);
			}
			
			set
			{
				base.SetSystemString(AppFieldMetadata.ColumnNames.FieldName, value);
			}
		}
		/// <summary>
		/// Maps to AppField.FieldType
		/// </summary>
		virtual public System.String FieldType
		{
			get
			{
				return base.GetSystemString(AppFieldMetadata.ColumnNames.FieldType);
			}
			
			set
			{
				base.SetSystemString(AppFieldMetadata.ColumnNames.FieldType, value);
			}
		}
		/// <summary>
		/// Maps to AppField.Description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(AppFieldMetadata.ColumnNames.Description);
			}
			
			set
			{
				base.SetSystemString(AppFieldMetadata.ColumnNames.Description, value);
			}
		}
		/// <summary>
		/// Maps to AppField.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(AppFieldMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(AppFieldMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AppField.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppFieldMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AppFieldMetadata.ColumnNames.CreateDateTime, value);
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
			public esStrings(esAppField entity)
			{
				this.entity = entity;
			}
			public System.String FieldID
			{
				get
				{
					System.Int32? data = entity.FieldID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FieldID = null;
					else entity.FieldID = Convert.ToInt32(value);
				}
			}
			public System.String FieldName
			{
				get
				{
					System.String data = entity.FieldName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FieldName = null;
					else entity.FieldName = Convert.ToString(value);
				}
			}
			public System.String FieldType
			{
				get
				{
					System.String data = entity.FieldType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FieldType = null;
					else entity.FieldType = Convert.ToString(value);
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
			private esAppField entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppFieldQuery query)
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
				throw new Exception("esAppField can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AppField : esAppField
	{	
	}

	[Serializable]
	abstract public class esAppFieldQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return AppFieldMetadata.Meta();
			}
		}	
			
		public esQueryItem FieldID
		{
			get
			{
				return new esQueryItem(this, AppFieldMetadata.ColumnNames.FieldID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem FieldName
		{
			get
			{
				return new esQueryItem(this, AppFieldMetadata.ColumnNames.FieldName, esSystemType.String);
			}
		} 
			
		public esQueryItem FieldType
		{
			get
			{
				return new esQueryItem(this, AppFieldMetadata.ColumnNames.FieldType, esSystemType.String);
			}
		} 
			
		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, AppFieldMetadata.ColumnNames.Description, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, AppFieldMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, AppFieldMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppFieldCollection")]
	public partial class AppFieldCollection : esAppFieldCollection, IEnumerable< AppField>
	{
		public AppFieldCollection()
		{

		}	
		
		public static implicit operator List< AppField>(AppFieldCollection coll)
		{
			List< AppField> list = new List< AppField>();
			
			foreach (AppField emp in coll)
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
				return  AppFieldMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppFieldQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppField(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppField();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public AppFieldQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppFieldQuery();
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
		public bool Load(AppFieldQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AppField AddNew()
		{
			AppField entity = base.AddNewEntity() as AppField;
			
			return entity;		
		}
		public AppField FindByPrimaryKey(Int32 fieldID)
		{
			return base.FindByPrimaryKey(fieldID) as AppField;
		}

		#region IEnumerable< AppField> Members

		IEnumerator< AppField> IEnumerable< AppField>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AppField;
			}
		}

		#endregion
		
		private AppFieldQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppField' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AppField ({FieldID})")]
	[Serializable]
	public partial class AppField : esAppField
	{
		public AppField()
		{
		}	
	
		public AppField(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppFieldMetadata.Meta();
			}
		}	
	
		override protected esAppFieldQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppFieldQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public AppFieldQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppFieldQuery();
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
		public bool Load(AppFieldQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private AppFieldQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppFieldQuery : esAppFieldQuery
	{
		public AppFieldQuery()
		{

		}		
		
		public AppFieldQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "AppFieldQuery";
        }
	}

	[Serializable]
	public partial class AppFieldMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppFieldMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(AppFieldMetadata.ColumnNames.FieldID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppFieldMetadata.PropertyNames.FieldID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppFieldMetadata.ColumnNames.FieldName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AppFieldMetadata.PropertyNames.FieldName;
			c.CharacterMaxLength = 200;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppFieldMetadata.ColumnNames.FieldType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AppFieldMetadata.PropertyNames.FieldType;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppFieldMetadata.ColumnNames.Description, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AppFieldMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 400;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppFieldMetadata.ColumnNames.CreateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AppFieldMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppFieldMetadata.ColumnNames.CreateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppFieldMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public AppFieldMetadata Meta()
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
			public const string FieldID = "FieldID";
			public const string FieldName = "FieldName";
			public const string FieldType = "FieldType";
			public const string Description = "Description";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string FieldID = "FieldID";
			public const string FieldName = "FieldName";
			public const string FieldType = "FieldType";
			public const string Description = "Description";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
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
			lock (typeof(AppFieldMetadata))
			{
				if(AppFieldMetadata.mapDelegates == null)
				{
					AppFieldMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AppFieldMetadata.meta == null)
				{
					AppFieldMetadata.meta = new AppFieldMetadata();
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
				
				meta.AddTypeMap("FieldID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("FieldName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FieldType", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "AppField";
				meta.Destination = "AppField";
				meta.spInsert = "proc_AppFieldInsert";				
				meta.spUpdate = "proc_AppFieldUpdate";		
				meta.spDelete = "proc_AppFieldDelete";
				meta.spLoadAll = "proc_AppFieldLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppFieldLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppFieldMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
