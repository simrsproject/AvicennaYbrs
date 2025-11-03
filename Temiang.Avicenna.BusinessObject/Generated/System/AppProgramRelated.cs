/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/03/19 10:28:26 AM
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
	abstract public class esAppProgramRelatedCollection : esEntityCollectionWAuditLog
	{
		public esAppProgramRelatedCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "AppProgramRelatedCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esAppProgramRelatedQuery query)
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
			this.InitQuery(query as esAppProgramRelatedQuery);
		}
		#endregion
			
		virtual public AppProgramRelated DetachEntity(AppProgramRelated entity)
		{
			return base.DetachEntity(entity) as AppProgramRelated;
		}
		
		virtual public AppProgramRelated AttachEntity(AppProgramRelated entity)
		{
			return base.AttachEntity(entity) as AppProgramRelated;
		}
		
		virtual public void Combine(AppProgramRelatedCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AppProgramRelated this[int index]
		{
			get
			{
				return base[index] as AppProgramRelated;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppProgramRelated);
		}
	}

	[Serializable]
	abstract public class esAppProgramRelated : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppProgramRelatedQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esAppProgramRelated()
		{
		}
	
		public esAppProgramRelated(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String programID, String relatedProgramID, String referenceID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(programID, relatedProgramID, referenceID);
			else
				return LoadByPrimaryKeyStoredProcedure(programID, relatedProgramID, referenceID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String programID, String relatedProgramID, String referenceID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(programID, relatedProgramID, referenceID);
			else
				return LoadByPrimaryKeyStoredProcedure(programID, relatedProgramID, referenceID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String programID, String relatedProgramID, String referenceID)
		{
			esAppProgramRelatedQuery query = this.GetDynamicQuery();
			query.Where(query.ProgramID == programID, query.RelatedProgramID == relatedProgramID, query.ReferenceID == referenceID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String programID, String relatedProgramID, String referenceID)
		{
			esParameters parms = new esParameters();
			parms.Add("ProgramID",programID);
			parms.Add("RelatedProgramID",relatedProgramID);
			parms.Add("ReferenceID",referenceID);
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
						case "ProgramID": this.str.ProgramID = (string)value; break;
						case "RelatedProgramID": this.str.RelatedProgramID = (string)value; break;
						case "ReferenceID": this.str.ReferenceID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
					
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
		/// Maps to AppProgramRelated.ProgramID
		/// </summary>
		virtual public System.String ProgramID
		{
			get
			{
				return base.GetSystemString(AppProgramRelatedMetadata.ColumnNames.ProgramID);
			}
			
			set
			{
				base.SetSystemString(AppProgramRelatedMetadata.ColumnNames.ProgramID, value);
			}
		}
		/// <summary>
		/// Maps to AppProgramRelated.RelatedProgramID
		/// </summary>
		virtual public System.String RelatedProgramID
		{
			get
			{
				return base.GetSystemString(AppProgramRelatedMetadata.ColumnNames.RelatedProgramID);
			}
			
			set
			{
				base.SetSystemString(AppProgramRelatedMetadata.ColumnNames.RelatedProgramID, value);
			}
		}
		/// <summary>
		/// Maps to AppProgramRelated.ReferenceID
		/// </summary>
		virtual public System.String ReferenceID
		{
			get
			{
				return base.GetSystemString(AppProgramRelatedMetadata.ColumnNames.ReferenceID);
			}
			
			set
			{
				base.SetSystemString(AppProgramRelatedMetadata.ColumnNames.ReferenceID, value);
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
			public esStrings(esAppProgramRelated entity)
			{
				this.entity = entity;
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
			public System.String RelatedProgramID
			{
				get
				{
					System.String data = entity.RelatedProgramID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RelatedProgramID = null;
					else entity.RelatedProgramID = Convert.ToString(value);
				}
			}
			public System.String ReferenceID
			{
				get
				{
					System.String data = entity.ReferenceID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceID = null;
					else entity.ReferenceID = Convert.ToString(value);
				}
			}
			private esAppProgramRelated entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppProgramRelatedQuery query)
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
				throw new Exception("esAppProgramRelated can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AppProgramRelated : esAppProgramRelated
	{	
	}

	[Serializable]
	abstract public class esAppProgramRelatedQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return AppProgramRelatedMetadata.Meta();
			}
		}	
			
		public esQueryItem ProgramID
		{
			get
			{
				return new esQueryItem(this, AppProgramRelatedMetadata.ColumnNames.ProgramID, esSystemType.String);
			}
		} 
			
		public esQueryItem RelatedProgramID
		{
			get
			{
				return new esQueryItem(this, AppProgramRelatedMetadata.ColumnNames.RelatedProgramID, esSystemType.String);
			}
		} 
			
		public esQueryItem ReferenceID
		{
			get
			{
				return new esQueryItem(this, AppProgramRelatedMetadata.ColumnNames.ReferenceID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppProgramRelatedCollection")]
	public partial class AppProgramRelatedCollection : esAppProgramRelatedCollection, IEnumerable< AppProgramRelated>
	{
		public AppProgramRelatedCollection()
		{

		}	
		
		public static implicit operator List< AppProgramRelated>(AppProgramRelatedCollection coll)
		{
			List< AppProgramRelated> list = new List< AppProgramRelated>();
			
			foreach (AppProgramRelated emp in coll)
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
				return  AppProgramRelatedMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppProgramRelatedQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppProgramRelated(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppProgramRelated();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public AppProgramRelatedQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppProgramRelatedQuery();
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
		public bool Load(AppProgramRelatedQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AppProgramRelated AddNew()
		{
			AppProgramRelated entity = base.AddNewEntity() as AppProgramRelated;
			
			return entity;		
		}
		public AppProgramRelated FindByPrimaryKey(String programID, String relatedProgramID, String referenceID)
		{
			return base.FindByPrimaryKey(programID, relatedProgramID, referenceID) as AppProgramRelated;
		}

		#region IEnumerable< AppProgramRelated> Members

		IEnumerator< AppProgramRelated> IEnumerable< AppProgramRelated>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AppProgramRelated;
			}
		}

		#endregion
		
		private AppProgramRelatedQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppProgramRelated' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AppProgramRelated ({ProgramID, RelatedProgramID, ReferenceID})")]
	[Serializable]
	public partial class AppProgramRelated : esAppProgramRelated
	{
		public AppProgramRelated()
		{
		}	
	
		public AppProgramRelated(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppProgramRelatedMetadata.Meta();
			}
		}	
	
		override protected esAppProgramRelatedQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppProgramRelatedQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public AppProgramRelatedQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppProgramRelatedQuery();
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
		public bool Load(AppProgramRelatedQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private AppProgramRelatedQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppProgramRelatedQuery : esAppProgramRelatedQuery
	{
		public AppProgramRelatedQuery()
		{

		}		
		
		public AppProgramRelatedQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "AppProgramRelatedQuery";
        }
	}

	[Serializable]
	public partial class AppProgramRelatedMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppProgramRelatedMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(AppProgramRelatedMetadata.ColumnNames.ProgramID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AppProgramRelatedMetadata.PropertyNames.ProgramID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 30;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramRelatedMetadata.ColumnNames.RelatedProgramID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AppProgramRelatedMetadata.PropertyNames.RelatedProgramID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 30;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppProgramRelatedMetadata.ColumnNames.ReferenceID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AppProgramRelatedMetadata.PropertyNames.ReferenceID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 30;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public AppProgramRelatedMetadata Meta()
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
			public const string ProgramID = "ProgramID";
			public const string RelatedProgramID = "RelatedProgramID";
			public const string ReferenceID = "ReferenceID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string ProgramID = "ProgramID";
			public const string RelatedProgramID = "RelatedProgramID";
			public const string ReferenceID = "ReferenceID";
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
			lock (typeof(AppProgramRelatedMetadata))
			{
				if(AppProgramRelatedMetadata.mapDelegates == null)
				{
					AppProgramRelatedMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AppProgramRelatedMetadata.meta == null)
				{
					AppProgramRelatedMetadata.meta = new AppProgramRelatedMetadata();
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
				
				meta.AddTypeMap("ProgramID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RelatedProgramID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "AppProgramRelated";
				meta.Destination = "AppProgramRelated";
				meta.spInsert = "proc_AppProgramRelatedInsert";				
				meta.spUpdate = "proc_AppProgramRelatedUpdate";		
				meta.spDelete = "proc_AppProgramRelatedDelete";
				meta.spLoadAll = "proc_AppProgramRelatedLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppProgramRelatedLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppProgramRelatedMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
