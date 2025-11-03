/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/3/2023 2:57:02 PM
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
	abstract public class esPersonalInfoGoogleFormCollection : esEntityCollectionWAuditLog
	{
		public esPersonalInfoGoogleFormCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "PersonalInfoGoogleFormCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esPersonalInfoGoogleFormQuery query)
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
			this.InitQuery(query as esPersonalInfoGoogleFormQuery);
		}
		#endregion
			
		virtual public PersonalInfoGoogleForm DetachEntity(PersonalInfoGoogleForm entity)
		{
			return base.DetachEntity(entity) as PersonalInfoGoogleForm;
		}
		
		virtual public PersonalInfoGoogleForm AttachEntity(PersonalInfoGoogleForm entity)
		{
			return base.AttachEntity(entity) as PersonalInfoGoogleForm;
		}
		
		virtual public void Combine(PersonalInfoGoogleFormCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PersonalInfoGoogleForm this[int index]
		{
			get
			{
				return base[index] as PersonalInfoGoogleForm;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PersonalInfoGoogleForm);
		}
	}

	[Serializable]
	abstract public class esPersonalInfoGoogleForm : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPersonalInfoGoogleFormQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esPersonalInfoGoogleForm()
		{
		}
	
		public esPersonalInfoGoogleForm(DataRow row)
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
			esPersonalInfoGoogleFormQuery query = this.GetDynamicQuery();
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
						case "PersonID": this.str.PersonID = (string)value; break;
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
						case "PersonID":
						
							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
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
		/// Maps to PersonalInfoGoogleForm.Timestamp
		/// </summary>
		virtual public System.DateTime? Timestamp
		{
			get
			{
				return base.GetSystemDateTime(PersonalInfoGoogleFormMetadata.ColumnNames.Timestamp);
			}
			
			set
			{
				base.SetSystemDateTime(PersonalInfoGoogleFormMetadata.ColumnNames.Timestamp, value);
			}
		}
		/// <summary>
		/// Maps to PersonalInfoGoogleForm.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(PersonalInfoGoogleFormMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				base.SetSystemInt32(PersonalInfoGoogleFormMetadata.ColumnNames.PersonID, value);
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
			public esStrings(esPersonalInfoGoogleForm entity)
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
			public System.String PersonID
			{
				get
				{
					System.Int32? data = entity.PersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonID = null;
					else entity.PersonID = Convert.ToInt32(value);
				}
			}
			private esPersonalInfoGoogleForm entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPersonalInfoGoogleFormQuery query)
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
				throw new Exception("esPersonalInfoGoogleForm can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PersonalInfoGoogleForm : esPersonalInfoGoogleForm
	{	
	}

	[Serializable]
	abstract public class esPersonalInfoGoogleFormQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return PersonalInfoGoogleFormMetadata.Meta();
			}
		}	
			
		public esQueryItem Timestamp
		{
			get
			{
				return new esQueryItem(this, PersonalInfoGoogleFormMetadata.ColumnNames.Timestamp, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, PersonalInfoGoogleFormMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PersonalInfoGoogleFormCollection")]
	public partial class PersonalInfoGoogleFormCollection : esPersonalInfoGoogleFormCollection, IEnumerable< PersonalInfoGoogleForm>
	{
		public PersonalInfoGoogleFormCollection()
		{

		}	
		
		public static implicit operator List< PersonalInfoGoogleForm>(PersonalInfoGoogleFormCollection coll)
		{
			List< PersonalInfoGoogleForm> list = new List< PersonalInfoGoogleForm>();
			
			foreach (PersonalInfoGoogleForm emp in coll)
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
				return  PersonalInfoGoogleFormMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalInfoGoogleFormQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PersonalInfoGoogleForm(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PersonalInfoGoogleForm();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public PersonalInfoGoogleFormQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalInfoGoogleFormQuery();
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
		public bool Load(PersonalInfoGoogleFormQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PersonalInfoGoogleForm AddNew()
		{
			PersonalInfoGoogleForm entity = base.AddNewEntity() as PersonalInfoGoogleForm;
			
			return entity;		
		}
		public PersonalInfoGoogleForm FindByPrimaryKey(DateTime timestamp)
		{
			return base.FindByPrimaryKey(timestamp) as PersonalInfoGoogleForm;
		}

		#region IEnumerable< PersonalInfoGoogleForm> Members

		IEnumerator< PersonalInfoGoogleForm> IEnumerable< PersonalInfoGoogleForm>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PersonalInfoGoogleForm;
			}
		}

		#endregion
		
		private PersonalInfoGoogleFormQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PersonalInfoGoogleForm' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PersonalInfoGoogleForm ({Timestamp})")]
	[Serializable]
	public partial class PersonalInfoGoogleForm : esPersonalInfoGoogleForm
	{
		public PersonalInfoGoogleForm()
		{
		}	
	
		public PersonalInfoGoogleForm(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PersonalInfoGoogleFormMetadata.Meta();
			}
		}	
	
		override protected esPersonalInfoGoogleFormQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalInfoGoogleFormQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public PersonalInfoGoogleFormQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalInfoGoogleFormQuery();
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
		public bool Load(PersonalInfoGoogleFormQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private PersonalInfoGoogleFormQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PersonalInfoGoogleFormQuery : esPersonalInfoGoogleFormQuery
	{
		public PersonalInfoGoogleFormQuery()
		{

		}		
		
		public PersonalInfoGoogleFormQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "PersonalInfoGoogleFormQuery";
        }
	}

	[Serializable]
	public partial class PersonalInfoGoogleFormMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PersonalInfoGoogleFormMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(PersonalInfoGoogleFormMetadata.ColumnNames.Timestamp, 0, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalInfoGoogleFormMetadata.PropertyNames.Timestamp;
			c.IsInPrimaryKey = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PersonalInfoGoogleFormMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalInfoGoogleFormMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public PersonalInfoGoogleFormMetadata Meta()
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
			public const string PersonID = "PersonID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string Timestamp = "Timestamp";
			public const string PersonID = "PersonID";
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
			lock (typeof(PersonalInfoGoogleFormMetadata))
			{
				if(PersonalInfoGoogleFormMetadata.mapDelegates == null)
				{
					PersonalInfoGoogleFormMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PersonalInfoGoogleFormMetadata.meta == null)
				{
					PersonalInfoGoogleFormMetadata.meta = new PersonalInfoGoogleFormMetadata();
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
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
		

				meta.Source = "PersonalInfoGoogleForm";
				meta.Destination = "PersonalInfoGoogleForm";
				meta.spInsert = "proc_PersonalInfoGoogleFormInsert";				
				meta.spUpdate = "proc_PersonalInfoGoogleFormUpdate";		
				meta.spDelete = "proc_PersonalInfoGoogleFormDelete";
				meta.spLoadAll = "proc_PersonalInfoGoogleFormLoadAll";
				meta.spLoadByPrimaryKey = "proc_PersonalInfoGoogleFormLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PersonalInfoGoogleFormMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
