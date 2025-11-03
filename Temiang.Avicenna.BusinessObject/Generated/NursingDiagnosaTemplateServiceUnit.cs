/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/29/2019 5:50:15 PM
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
	abstract public class esNursingDiagnosaTemplateServiceUnitCollection : esEntityCollectionWAuditLog
	{
		public esNursingDiagnosaTemplateServiceUnitCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "NursingDiagnosaTemplateServiceUnitCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esNursingDiagnosaTemplateServiceUnitQuery query)
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
			this.InitQuery(query as esNursingDiagnosaTemplateServiceUnitQuery);
		}
		#endregion
			
		virtual public NursingDiagnosaTemplateServiceUnit DetachEntity(NursingDiagnosaTemplateServiceUnit entity)
		{
			return base.DetachEntity(entity) as NursingDiagnosaTemplateServiceUnit;
		}
		
		virtual public NursingDiagnosaTemplateServiceUnit AttachEntity(NursingDiagnosaTemplateServiceUnit entity)
		{
			return base.AttachEntity(entity) as NursingDiagnosaTemplateServiceUnit;
		}
		
		virtual public void Combine(NursingDiagnosaTemplateServiceUnitCollection collection)
		{
			base.Combine(collection);
		}
		
		new public NursingDiagnosaTemplateServiceUnit this[int index]
		{
			get
			{
				return base[index] as NursingDiagnosaTemplateServiceUnit;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(NursingDiagnosaTemplateServiceUnit);
		}
	}

	[Serializable]
	abstract public class esNursingDiagnosaTemplateServiceUnit : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esNursingDiagnosaTemplateServiceUnitQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esNursingDiagnosaTemplateServiceUnit()
		{
		}
	
		public esNursingDiagnosaTemplateServiceUnit(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 templateID, String serviceUnitID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(templateID, serviceUnitID);
			else
				return LoadByPrimaryKeyStoredProcedure(templateID, serviceUnitID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 templateID, String serviceUnitID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(templateID, serviceUnitID);
			else
				return LoadByPrimaryKeyStoredProcedure(templateID, serviceUnitID);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 templateID, String serviceUnitID)
		{
			esNursingDiagnosaTemplateServiceUnitQuery query = this.GetDynamicQuery();
			query.Where(query.TemplateID==templateID, query.ServiceUnitID==serviceUnitID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 templateID, String serviceUnitID)
		{
			esParameters parms = new esParameters();
			parms.Add("TemplateID",templateID);
			parms.Add("ServiceUnitID",serviceUnitID);
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
						case "TemplateID": this.str.TemplateID = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
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
		/// Maps to NursingDiagnosaTemplateServiceUnit.TemplateID
		/// </summary>
		virtual public System.Int32? TemplateID
		{
			get
			{
				return base.GetSystemInt32(NursingDiagnosaTemplateServiceUnitMetadata.ColumnNames.TemplateID);
			}
			
			set
			{
				base.SetSystemInt32(NursingDiagnosaTemplateServiceUnitMetadata.ColumnNames.TemplateID, value);
			}
		}
		/// <summary>
		/// Maps to NursingDiagnosaTemplateServiceUnit.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaTemplateServiceUnitMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaTemplateServiceUnitMetadata.ColumnNames.ServiceUnitID, value);
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
			public esStrings(esNursingDiagnosaTemplateServiceUnit entity)
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
			public System.String ServiceUnitID
			{
				get
				{
					System.String data = entity.ServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitID = null;
					else entity.ServiceUnitID = Convert.ToString(value);
				}
			}
			private esNursingDiagnosaTemplateServiceUnit entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esNursingDiagnosaTemplateServiceUnitQuery query)
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
				throw new Exception("esNursingDiagnosaTemplateServiceUnit can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class NursingDiagnosaTemplateServiceUnit : esNursingDiagnosaTemplateServiceUnit
	{	
	}

	[Serializable]
	abstract public class esNursingDiagnosaTemplateServiceUnitQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return NursingDiagnosaTemplateServiceUnitMetadata.Meta();
			}
		}	
			
		public esQueryItem TemplateID
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTemplateServiceUnitMetadata.ColumnNames.TemplateID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaTemplateServiceUnitMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("NursingDiagnosaTemplateServiceUnitCollection")]
	public partial class NursingDiagnosaTemplateServiceUnitCollection : esNursingDiagnosaTemplateServiceUnitCollection, IEnumerable< NursingDiagnosaTemplateServiceUnit>
	{
		public NursingDiagnosaTemplateServiceUnitCollection()
		{

		}	
		
		public static implicit operator List< NursingDiagnosaTemplateServiceUnit>(NursingDiagnosaTemplateServiceUnitCollection coll)
		{
			List< NursingDiagnosaTemplateServiceUnit> list = new List< NursingDiagnosaTemplateServiceUnit>();
			
			foreach (NursingDiagnosaTemplateServiceUnit emp in coll)
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
				return  NursingDiagnosaTemplateServiceUnitMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NursingDiagnosaTemplateServiceUnitQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new NursingDiagnosaTemplateServiceUnit(row);
		}

		override protected esEntity CreateEntity()
		{
			return new NursingDiagnosaTemplateServiceUnit();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public NursingDiagnosaTemplateServiceUnitQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NursingDiagnosaTemplateServiceUnitQuery();
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
		public bool Load(NursingDiagnosaTemplateServiceUnitQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public NursingDiagnosaTemplateServiceUnit AddNew()
		{
			NursingDiagnosaTemplateServiceUnit entity = base.AddNewEntity() as NursingDiagnosaTemplateServiceUnit;
			
			return entity;		
		}
		public NursingDiagnosaTemplateServiceUnit FindByPrimaryKey(Int32 templateID, String serviceUnitID)
		{
			return base.FindByPrimaryKey(templateID, serviceUnitID) as NursingDiagnosaTemplateServiceUnit;
		}

		#region IEnumerable< NursingDiagnosaTemplateServiceUnit> Members

		IEnumerator< NursingDiagnosaTemplateServiceUnit> IEnumerable< NursingDiagnosaTemplateServiceUnit>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as NursingDiagnosaTemplateServiceUnit;
			}
		}

		#endregion
		
		private NursingDiagnosaTemplateServiceUnitQuery query;
	}


	/// <summary>
	/// Encapsulates the 'NursingDiagnosaTemplateServiceUnit' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("NursingDiagnosaTemplateServiceUnit ({TemplateID, ServiceUnitID})")]
	[Serializable]
	public partial class NursingDiagnosaTemplateServiceUnit : esNursingDiagnosaTemplateServiceUnit
	{
		public NursingDiagnosaTemplateServiceUnit()
		{
		}	
	
		public NursingDiagnosaTemplateServiceUnit(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return NursingDiagnosaTemplateServiceUnitMetadata.Meta();
			}
		}	
	
		override protected esNursingDiagnosaTemplateServiceUnitQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NursingDiagnosaTemplateServiceUnitQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public NursingDiagnosaTemplateServiceUnitQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NursingDiagnosaTemplateServiceUnitQuery();
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
		public bool Load(NursingDiagnosaTemplateServiceUnitQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private NursingDiagnosaTemplateServiceUnitQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class NursingDiagnosaTemplateServiceUnitQuery : esNursingDiagnosaTemplateServiceUnitQuery
	{
		public NursingDiagnosaTemplateServiceUnitQuery()
		{

		}		
		
		public NursingDiagnosaTemplateServiceUnitQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "NursingDiagnosaTemplateServiceUnitQuery";
        }
	}

	[Serializable]
	public partial class NursingDiagnosaTemplateServiceUnitMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected NursingDiagnosaTemplateServiceUnitMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(NursingDiagnosaTemplateServiceUnitMetadata.ColumnNames.TemplateID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NursingDiagnosaTemplateServiceUnitMetadata.PropertyNames.TemplateID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingDiagnosaTemplateServiceUnitMetadata.ColumnNames.ServiceUnitID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaTemplateServiceUnitMetadata.PropertyNames.ServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public NursingDiagnosaTemplateServiceUnitMetadata Meta()
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
			public const string TemplateID = "TemplateID";
			public const string ServiceUnitID = "ServiceUnitID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string TemplateID = "TemplateID";
			public const string ServiceUnitID = "ServiceUnitID";
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
			lock (typeof(NursingDiagnosaTemplateServiceUnitMetadata))
			{
				if(NursingDiagnosaTemplateServiceUnitMetadata.mapDelegates == null)
				{
					NursingDiagnosaTemplateServiceUnitMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (NursingDiagnosaTemplateServiceUnitMetadata.meta == null)
				{
					NursingDiagnosaTemplateServiceUnitMetadata.meta = new NursingDiagnosaTemplateServiceUnitMetadata();
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
				
				meta.AddTypeMap("TemplateID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "NursingDiagnosaTemplateServiceUnit";
				meta.Destination = "NursingDiagnosaTemplateServiceUnit";
				meta.spInsert = "proc_NursingDiagnosaTemplateServiceUnitInsert";				
				meta.spUpdate = "proc_NursingDiagnosaTemplateServiceUnitUpdate";		
				meta.spDelete = "proc_NursingDiagnosaTemplateServiceUnitDelete";
				meta.spLoadAll = "proc_NursingDiagnosaTemplateServiceUnitLoadAll";
				meta.spLoadByPrimaryKey = "proc_NursingDiagnosaTemplateServiceUnitLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private NursingDiagnosaTemplateServiceUnitMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
