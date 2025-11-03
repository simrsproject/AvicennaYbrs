/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/20/2019 9:55:42 AM
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
	abstract public class esNursingDiagnosaNsTypeCollection : esEntityCollectionWAuditLog
	{
		public esNursingDiagnosaNsTypeCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "NursingDiagnosaNsTypeCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esNursingDiagnosaNsTypeQuery query)
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
			this.InitQuery(query as esNursingDiagnosaNsTypeQuery);
		}
		#endregion
			
		virtual public NursingDiagnosaNsType DetachEntity(NursingDiagnosaNsType entity)
		{
			return base.DetachEntity(entity) as NursingDiagnosaNsType;
		}
		
		virtual public NursingDiagnosaNsType AttachEntity(NursingDiagnosaNsType entity)
		{
			return base.AttachEntity(entity) as NursingDiagnosaNsType;
		}
		
		virtual public void Combine(NursingDiagnosaNsTypeCollection collection)
		{
			base.Combine(collection);
		}
		
		new public NursingDiagnosaNsType this[int index]
		{
			get
			{
				return base[index] as NursingDiagnosaNsType;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(NursingDiagnosaNsType);
		}
	}

	[Serializable]
	abstract public class esNursingDiagnosaNsType : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esNursingDiagnosaNsTypeQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esNursingDiagnosaNsType()
		{
		}
	
		public esNursingDiagnosaNsType(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String nursingDiagnosaID, String sRNsType)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(nursingDiagnosaID, sRNsType);
			else
				return LoadByPrimaryKeyStoredProcedure(nursingDiagnosaID, sRNsType);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String nursingDiagnosaID, String sRNsType)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(nursingDiagnosaID, sRNsType);
			else
				return LoadByPrimaryKeyStoredProcedure(nursingDiagnosaID, sRNsType);
		}
	
		private bool LoadByPrimaryKeyDynamic(String nursingDiagnosaID, String sRNsType)
		{
			esNursingDiagnosaNsTypeQuery query = this.GetDynamicQuery();
			query.Where(query.NursingDiagnosaID==nursingDiagnosaID, query.SRNsType==sRNsType);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String nursingDiagnosaID, String sRNsType)
		{
			esParameters parms = new esParameters();
			parms.Add("NursingDiagnosaID",nursingDiagnosaID);
			parms.Add("SRNsType",sRNsType);
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
						case "NursingDiagnosaID": this.str.NursingDiagnosaID = (string)value; break;
						case "SRNsType": this.str.SRNsType = (string)value; break;
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
		/// Maps to NursingDiagnosaNsType.NursingDiagnosaID
		/// </summary>
		virtual public System.String NursingDiagnosaID
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaNsTypeMetadata.ColumnNames.NursingDiagnosaID);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaNsTypeMetadata.ColumnNames.NursingDiagnosaID, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaNsType.SRNsType
		/// </summary>
		virtual public System.String SRNsType
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaNsTypeMetadata.ColumnNames.SRNsType);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaNsTypeMetadata.ColumnNames.SRNsType, value);
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
			public esStrings(esNursingDiagnosaNsType entity)
			{
				this.entity = entity;
			}
			public System.String NursingDiagnosaID
			{
				get
				{
					System.String data = entity.NursingDiagnosaID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NursingDiagnosaID = null;
					else entity.NursingDiagnosaID = Convert.ToString(value);
				}
			}
			public System.String SRNsType
			{
				get
				{
					System.String data = entity.SRNsType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRNsType = null;
					else entity.SRNsType = Convert.ToString(value);
				}
			}
			private esNursingDiagnosaNsType entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esNursingDiagnosaNsTypeQuery query)
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
				throw new Exception("esNursingDiagnosaNsType can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class NursingDiagnosaNsType : esNursingDiagnosaNsType
	{	
	}

	[Serializable]
	abstract public class esNursingDiagnosaNsTypeQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return NursingDiagnosaNsTypeMetadata.Meta();
			}
		}	
			
		public esQueryItem NursingDiagnosaID
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaNsTypeMetadata.ColumnNames.NursingDiagnosaID, esSystemType.String);
			}
		} 
			
		public esQueryItem SRNsType
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaNsTypeMetadata.ColumnNames.SRNsType, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("NursingDiagnosaNsTypeCollection")]
	public partial class NursingDiagnosaNsTypeCollection : esNursingDiagnosaNsTypeCollection, IEnumerable< NursingDiagnosaNsType>
	{
		public NursingDiagnosaNsTypeCollection()
		{

		}	
		
		public static implicit operator List< NursingDiagnosaNsType>(NursingDiagnosaNsTypeCollection coll)
		{
			List< NursingDiagnosaNsType> list = new List< NursingDiagnosaNsType>();
			
			foreach (NursingDiagnosaNsType emp in coll)
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
				return  NursingDiagnosaNsTypeMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NursingDiagnosaNsTypeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new NursingDiagnosaNsType(row);
		}

		override protected esEntity CreateEntity()
		{
			return new NursingDiagnosaNsType();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public NursingDiagnosaNsTypeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NursingDiagnosaNsTypeQuery();
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
		public bool Load(NursingDiagnosaNsTypeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public NursingDiagnosaNsType AddNew()
		{
			NursingDiagnosaNsType entity = base.AddNewEntity() as NursingDiagnosaNsType;
			
			return entity;		
		}
		public NursingDiagnosaNsType FindByPrimaryKey(String nursingDiagnosaID, String sRNsType)
		{
			return base.FindByPrimaryKey(nursingDiagnosaID, sRNsType) as NursingDiagnosaNsType;
		}

		#region IEnumerable< NursingDiagnosaNsType> Members

		IEnumerator< NursingDiagnosaNsType> IEnumerable< NursingDiagnosaNsType>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as NursingDiagnosaNsType;
			}
		}

		#endregion
		
		private NursingDiagnosaNsTypeQuery query;
	}


	/// <summary>
	/// Encapsulates the 'NursingDiagnosaNsType' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("NursingDiagnosaNsType ({NursingDiagnosaID, SRNsType})")]
	[Serializable]
	public partial class NursingDiagnosaNsType : esNursingDiagnosaNsType
	{
		public NursingDiagnosaNsType()
		{
		}	
	
		public NursingDiagnosaNsType(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return NursingDiagnosaNsTypeMetadata.Meta();
			}
		}	
	
		override protected esNursingDiagnosaNsTypeQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NursingDiagnosaNsTypeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public NursingDiagnosaNsTypeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NursingDiagnosaNsTypeQuery();
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
		public bool Load(NursingDiagnosaNsTypeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private NursingDiagnosaNsTypeQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class NursingDiagnosaNsTypeQuery : esNursingDiagnosaNsTypeQuery
	{
		public NursingDiagnosaNsTypeQuery()
		{

		}		
		
		public NursingDiagnosaNsTypeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "NursingDiagnosaNsTypeQuery";
        }
	}

	[Serializable]
	public partial class NursingDiagnosaNsTypeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected NursingDiagnosaNsTypeMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(NursingDiagnosaNsTypeMetadata.ColumnNames.NursingDiagnosaID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaNsTypeMetadata.PropertyNames.NursingDiagnosaID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaNsTypeMetadata.ColumnNames.SRNsType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaNsTypeMetadata.PropertyNames.SRNsType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public NursingDiagnosaNsTypeMetadata Meta()
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
			public const string NursingDiagnosaID = "NursingDiagnosaID";
			public const string SRNsType = "SRNsType";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string NursingDiagnosaID = "NursingDiagnosaID";
			public const string SRNsType = "SRNsType";
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
			lock (typeof(NursingDiagnosaNsTypeMetadata))
			{
				if(NursingDiagnosaNsTypeMetadata.mapDelegates == null)
				{
					NursingDiagnosaNsTypeMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (NursingDiagnosaNsTypeMetadata.meta == null)
				{
					NursingDiagnosaNsTypeMetadata.meta = new NursingDiagnosaNsTypeMetadata();
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
				
				meta.AddTypeMap("NursingDiagnosaID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRNsType", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "NursingDiagnosaNsType";
				meta.Destination = "NursingDiagnosaNsType";
				meta.spInsert = "proc_NursingDiagnosaNsTypeInsert";				
				meta.spUpdate = "proc_NursingDiagnosaNsTypeUpdate";		
				meta.spDelete = "proc_NursingDiagnosaNsTypeDelete";
				meta.spLoadAll = "proc_NursingDiagnosaNsTypeLoadAll";
				meta.spLoadByPrimaryKey = "proc_NursingDiagnosaNsTypeLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private NursingDiagnosaNsTypeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
