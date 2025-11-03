/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/25/2021 6:32:29 PM
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
	abstract public class esPCareReferenceItemMappingCollection : esEntityCollectionWAuditLog
	{
		public esPCareReferenceItemMappingCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "PCareReferenceItemMappingCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esPCareReferenceItemMappingQuery query)
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
			this.InitQuery(query as esPCareReferenceItemMappingQuery);
		}
		#endregion
			
		virtual public PCareReferenceItemMapping DetachEntity(PCareReferenceItemMapping entity)
		{
			return base.DetachEntity(entity) as PCareReferenceItemMapping;
		}
		
		virtual public PCareReferenceItemMapping AttachEntity(PCareReferenceItemMapping entity)
		{
			return base.AttachEntity(entity) as PCareReferenceItemMapping;
		}
		
		virtual public void Combine(PCareReferenceItemMappingCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PCareReferenceItemMapping this[int index]
		{
			get
			{
				return base[index] as PCareReferenceItemMapping;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PCareReferenceItemMapping);
		}
	}

	[Serializable]
	abstract public class esPCareReferenceItemMapping : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPCareReferenceItemMappingQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esPCareReferenceItemMapping()
		{
		}
	
		public esPCareReferenceItemMapping(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String referenceID, String mappingWithID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(referenceID, mappingWithID);
			else
				return LoadByPrimaryKeyStoredProcedure(referenceID, mappingWithID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String referenceID, String mappingWithID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(referenceID, mappingWithID);
			else
				return LoadByPrimaryKeyStoredProcedure(referenceID, mappingWithID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String referenceID, String mappingWithID)
		{
			esPCareReferenceItemMappingQuery query = this.GetDynamicQuery();
			query.Where(query.ReferenceID == referenceID, query.MappingWithID == mappingWithID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String referenceID, String mappingWithID)
		{
			esParameters parms = new esParameters();
			parms.Add("ReferenceID",referenceID);
			parms.Add("MappingWithID",mappingWithID);
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
						case "ReferenceID": this.str.ReferenceID = (string)value; break;
						case "MappingWithID": this.str.MappingWithID = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
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
		/// Maps to PCareReferenceItemMapping.ReferenceID
		/// </summary>
		virtual public System.String ReferenceID
		{
			get
			{
				return base.GetSystemString(PCareReferenceItemMappingMetadata.ColumnNames.ReferenceID);
			}
			
			set
			{
				base.SetSystemString(PCareReferenceItemMappingMetadata.ColumnNames.ReferenceID, value);
			}
		}
		/// <summary>
		/// Maps to PCareReferenceItemMapping.MappingWithID
		/// </summary>
		virtual public System.String MappingWithID
		{
			get
			{
				return base.GetSystemString(PCareReferenceItemMappingMetadata.ColumnNames.MappingWithID);
			}
			
			set
			{
				base.SetSystemString(PCareReferenceItemMappingMetadata.ColumnNames.MappingWithID, value);
			}
		}
		/// <summary>
		/// Maps to PCareReferenceItemMapping.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(PCareReferenceItemMappingMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(PCareReferenceItemMappingMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to PCareReferenceItemMapping.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PCareReferenceItemMappingMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PCareReferenceItemMappingMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PCareReferenceItemMapping.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PCareReferenceItemMappingMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PCareReferenceItemMappingMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPCareReferenceItemMapping entity)
			{
				this.entity = entity;
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
			public System.String MappingWithID
			{
				get
				{
					System.String data = entity.MappingWithID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MappingWithID = null;
					else entity.MappingWithID = Convert.ToString(value);
				}
			}
			public System.String ItemID
			{
				get
				{
					System.String data = entity.ItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemID = null;
					else entity.ItemID = Convert.ToString(value);
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
			private esPCareReferenceItemMapping entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPCareReferenceItemMappingQuery query)
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
				throw new Exception("esPCareReferenceItemMapping can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PCareReferenceItemMapping : esPCareReferenceItemMapping
	{	
	}

	[Serializable]
	abstract public class esPCareReferenceItemMappingQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return PCareReferenceItemMappingMetadata.Meta();
			}
		}	
			
		public esQueryItem ReferenceID
		{
			get
			{
				return new esQueryItem(this, PCareReferenceItemMappingMetadata.ColumnNames.ReferenceID, esSystemType.String);
			}
		} 
			
		public esQueryItem MappingWithID
		{
			get
			{
				return new esQueryItem(this, PCareReferenceItemMappingMetadata.ColumnNames.MappingWithID, esSystemType.String);
			}
		} 
			
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, PCareReferenceItemMappingMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PCareReferenceItemMappingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PCareReferenceItemMappingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PCareReferenceItemMappingCollection")]
	public partial class PCareReferenceItemMappingCollection : esPCareReferenceItemMappingCollection, IEnumerable< PCareReferenceItemMapping>
	{
		public PCareReferenceItemMappingCollection()
		{

		}	
		
		public static implicit operator List< PCareReferenceItemMapping>(PCareReferenceItemMappingCollection coll)
		{
			List< PCareReferenceItemMapping> list = new List< PCareReferenceItemMapping>();
			
			foreach (PCareReferenceItemMapping emp in coll)
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
				return  PCareReferenceItemMappingMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PCareReferenceItemMappingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PCareReferenceItemMapping(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PCareReferenceItemMapping();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public PCareReferenceItemMappingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PCareReferenceItemMappingQuery();
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
		public bool Load(PCareReferenceItemMappingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PCareReferenceItemMapping AddNew()
		{
			PCareReferenceItemMapping entity = base.AddNewEntity() as PCareReferenceItemMapping;
			
			return entity;		
		}
		public PCareReferenceItemMapping FindByPrimaryKey(String referenceID, String mappingWithID)
		{
			return base.FindByPrimaryKey(referenceID, mappingWithID) as PCareReferenceItemMapping;
		}

		#region IEnumerable< PCareReferenceItemMapping> Members

		IEnumerator< PCareReferenceItemMapping> IEnumerable< PCareReferenceItemMapping>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PCareReferenceItemMapping;
			}
		}

		#endregion
		
		private PCareReferenceItemMappingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PCareReferenceItemMapping' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PCareReferenceItemMapping ({ReferenceID, MappingWithID})")]
	[Serializable]
	public partial class PCareReferenceItemMapping : esPCareReferenceItemMapping
	{
		public PCareReferenceItemMapping()
		{
		}	
	
		public PCareReferenceItemMapping(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PCareReferenceItemMappingMetadata.Meta();
			}
		}	
	
		override protected esPCareReferenceItemMappingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PCareReferenceItemMappingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public PCareReferenceItemMappingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PCareReferenceItemMappingQuery();
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
		public bool Load(PCareReferenceItemMappingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private PCareReferenceItemMappingQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PCareReferenceItemMappingQuery : esPCareReferenceItemMappingQuery
	{
		public PCareReferenceItemMappingQuery()
		{

		}		
		
		public PCareReferenceItemMappingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "PCareReferenceItemMappingQuery";
        }
	}

	[Serializable]
	public partial class PCareReferenceItemMappingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PCareReferenceItemMappingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(PCareReferenceItemMappingMetadata.ColumnNames.ReferenceID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PCareReferenceItemMappingMetadata.PropertyNames.ReferenceID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PCareReferenceItemMappingMetadata.ColumnNames.MappingWithID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PCareReferenceItemMappingMetadata.PropertyNames.MappingWithID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 30;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PCareReferenceItemMappingMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PCareReferenceItemMappingMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PCareReferenceItemMappingMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PCareReferenceItemMappingMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PCareReferenceItemMappingMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PCareReferenceItemMappingMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public PCareReferenceItemMappingMetadata Meta()
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
			public const string ReferenceID = "ReferenceID";
			public const string MappingWithID = "MappingWithID";
			public const string ItemID = "ItemID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string ReferenceID = "ReferenceID";
			public const string MappingWithID = "MappingWithID";
			public const string ItemID = "ItemID";
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
			lock (typeof(PCareReferenceItemMappingMetadata))
			{
				if(PCareReferenceItemMappingMetadata.mapDelegates == null)
				{
					PCareReferenceItemMappingMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PCareReferenceItemMappingMetadata.meta == null)
				{
					PCareReferenceItemMappingMetadata.meta = new PCareReferenceItemMappingMetadata();
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
				
				meta.AddTypeMap("ReferenceID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MappingWithID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "PCareReferenceItemMapping";
				meta.Destination = "PCareReferenceItemMapping";
				meta.spInsert = "proc_PCareReferenceItemMappingInsert";				
				meta.spUpdate = "proc_PCareReferenceItemMappingUpdate";		
				meta.spDelete = "proc_PCareReferenceItemMappingDelete";
				meta.spLoadAll = "proc_PCareReferenceItemMappingLoadAll";
				meta.spLoadByPrimaryKey = "proc_PCareReferenceItemMappingLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PCareReferenceItemMappingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
