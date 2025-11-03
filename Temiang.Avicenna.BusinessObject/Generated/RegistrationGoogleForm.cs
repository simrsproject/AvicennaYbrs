/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/13/2021 11:00:44 PM
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
	abstract public class esRegistrationGoogleFormCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationGoogleFormCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "RegistrationGoogleFormCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esRegistrationGoogleFormQuery query)
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
			this.InitQuery(query as esRegistrationGoogleFormQuery);
		}
		#endregion
			
		virtual public RegistrationGoogleForm DetachEntity(RegistrationGoogleForm entity)
		{
			return base.DetachEntity(entity) as RegistrationGoogleForm;
		}
		
		virtual public RegistrationGoogleForm AttachEntity(RegistrationGoogleForm entity)
		{
			return base.AttachEntity(entity) as RegistrationGoogleForm;
		}
		
		virtual public void Combine(RegistrationGoogleFormCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RegistrationGoogleForm this[int index]
		{
			get
			{
				return base[index] as RegistrationGoogleForm;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationGoogleForm);
		}
	}

	[Serializable]
	abstract public class esRegistrationGoogleForm : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationGoogleFormQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esRegistrationGoogleForm()
		{
		}
	
		public esRegistrationGoogleForm(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(DateTime timestamp)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(timestamp);
			else
				return LoadByPrimaryKeyStoredProcedure(timestamp);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, DateTime timestamp)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(timestamp);
			else
				return LoadByPrimaryKeyStoredProcedure(timestamp);
		}
	
		private bool LoadByPrimaryKeyDynamic(DateTime timestamp)
		{
			esRegistrationGoogleFormQuery query = this.GetDynamicQuery();
			query.Where(query.Timestamp == timestamp);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(DateTime timestamp)
		{
			esParameters parms = new esParameters();
			parms.Add("Timestamp",timestamp);
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
						case "Timestamp": this.str.Timestamp = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Timestamp":
						
							if (value == null || value is System.DateTime)
								this.Timestamp = (System.DateTime?)value;
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
		/// Maps to RegistrationGoogleForm.Timestamp
		/// </summary>
		virtual public System.DateTime? Timestamp
		{
			get
			{
				return base.GetSystemDateTime(RegistrationGoogleFormMetadata.ColumnNames.Timestamp);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationGoogleFormMetadata.ColumnNames.Timestamp, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationGoogleForm.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationGoogleFormMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(RegistrationGoogleFormMetadata.ColumnNames.RegistrationNo, value);
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
			public esStrings(esRegistrationGoogleForm entity)
			{
				this.entity = entity;
			}
			public System.String Timestamp
			{
				get
				{
					System.DateTime? data = entity.Timestamp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Timestamp = null;
					else entity.Timestamp = Convert.ToDateTime(value);
				}
			}
			public System.String RegistrationNo
			{
				get
				{
					System.String data = entity.RegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNo = null;
					else entity.RegistrationNo = Convert.ToString(value);
				}
			}
			private esRegistrationGoogleForm entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationGoogleFormQuery query)
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
				throw new Exception("esRegistrationGoogleForm can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class RegistrationGoogleForm : esRegistrationGoogleForm
	{	
	}

	[Serializable]
	abstract public class esRegistrationGoogleFormQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationGoogleFormMetadata.Meta();
			}
		}	
			
		public esQueryItem Timestamp
		{
			get
			{
				return new esQueryItem(this, RegistrationGoogleFormMetadata.ColumnNames.Timestamp, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationGoogleFormMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationGoogleFormCollection")]
	public partial class RegistrationGoogleFormCollection : esRegistrationGoogleFormCollection, IEnumerable< RegistrationGoogleForm>
	{
		public RegistrationGoogleFormCollection()
		{

		}	
		
		public static implicit operator List< RegistrationGoogleForm>(RegistrationGoogleFormCollection coll)
		{
			List< RegistrationGoogleForm> list = new List< RegistrationGoogleForm>();
			
			foreach (RegistrationGoogleForm emp in coll)
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
				return  RegistrationGoogleFormMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationGoogleFormQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationGoogleForm(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationGoogleForm();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public RegistrationGoogleFormQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationGoogleFormQuery();
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
		public bool Load(RegistrationGoogleFormQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RegistrationGoogleForm AddNew()
		{
			RegistrationGoogleForm entity = base.AddNewEntity() as RegistrationGoogleForm;
			
			return entity;		
		}
		public RegistrationGoogleForm FindByPrimaryKey(DateTime timestamp)
		{
			return base.FindByPrimaryKey(timestamp) as RegistrationGoogleForm;
		}

		#region IEnumerable< RegistrationGoogleForm> Members

		IEnumerator< RegistrationGoogleForm> IEnumerable< RegistrationGoogleForm>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationGoogleForm;
			}
		}

		#endregion
		
		private RegistrationGoogleFormQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationGoogleForm' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RegistrationGoogleForm ({Timestamp})")]
	[Serializable]
	public partial class RegistrationGoogleForm : esRegistrationGoogleForm
	{
		public RegistrationGoogleForm()
		{
		}	
	
		public RegistrationGoogleForm(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationGoogleFormMetadata.Meta();
			}
		}	
	
		override protected esRegistrationGoogleFormQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationGoogleFormQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public RegistrationGoogleFormQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationGoogleFormQuery();
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
		public bool Load(RegistrationGoogleFormQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private RegistrationGoogleFormQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RegistrationGoogleFormQuery : esRegistrationGoogleFormQuery
	{
		public RegistrationGoogleFormQuery()
		{

		}		
		
		public RegistrationGoogleFormQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "RegistrationGoogleFormQuery";
        }
	}

	[Serializable]
	public partial class RegistrationGoogleFormMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationGoogleFormMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(RegistrationGoogleFormMetadata.ColumnNames.Timestamp, 0, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationGoogleFormMetadata.PropertyNames.Timestamp;
			c.IsInPrimaryKey = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationGoogleFormMetadata.ColumnNames.RegistrationNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationGoogleFormMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				

		}
		#endregion
	
		static public RegistrationGoogleFormMetadata Meta()
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
			public const string Timestamp = "Timestamp";
			public const string RegistrationNo = "RegistrationNo";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string Timestamp = "Timestamp";
			public const string RegistrationNo = "RegistrationNo";
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
			lock (typeof(RegistrationGoogleFormMetadata))
			{
				if(RegistrationGoogleFormMetadata.mapDelegates == null)
				{
					RegistrationGoogleFormMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RegistrationGoogleFormMetadata.meta == null)
				{
					RegistrationGoogleFormMetadata.meta = new RegistrationGoogleFormMetadata();
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
				
				meta.AddTypeMap("Timestamp", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "RegistrationGoogleForm";
				meta.Destination = "RegistrationGoogleForm";
				meta.spInsert = "proc_RegistrationGoogleFormInsert";				
				meta.spUpdate = "proc_RegistrationGoogleFormUpdate";		
				meta.spDelete = "proc_RegistrationGoogleFormDelete";
				meta.spLoadAll = "proc_RegistrationGoogleFormLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationGoogleFormLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationGoogleFormMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
