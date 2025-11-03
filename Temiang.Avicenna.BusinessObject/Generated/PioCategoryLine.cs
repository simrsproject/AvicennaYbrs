/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/23/2020 11:06:23 AM
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
	abstract public class esPioCategoryLineCollection : esEntityCollectionWAuditLog
	{
		public esPioCategoryLineCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "PioCategoryLineCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esPioCategoryLineQuery query)
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
			this.InitQuery(query as esPioCategoryLineQuery);
		}
		#endregion
			
		virtual public PioCategoryLine DetachEntity(PioCategoryLine entity)
		{
			return base.DetachEntity(entity) as PioCategoryLine;
		}
		
		virtual public PioCategoryLine AttachEntity(PioCategoryLine entity)
		{
			return base.AttachEntity(entity) as PioCategoryLine;
		}
		
		virtual public void Combine(PioCategoryLineCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PioCategoryLine this[int index]
		{
			get
			{
				return base[index] as PioCategoryLine;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PioCategoryLine);
		}
	}

	[Serializable]
	abstract public class esPioCategoryLine : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPioCategoryLineQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esPioCategoryLine()
		{
		}
	
		public esPioCategoryLine(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 pioNo, String sRPioCategory)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(pioNo, sRPioCategory);
			else
				return LoadByPrimaryKeyStoredProcedure(pioNo, sRPioCategory);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 pioNo, String sRPioCategory)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(pioNo, sRPioCategory);
			else
				return LoadByPrimaryKeyStoredProcedure(pioNo, sRPioCategory);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 pioNo, String sRPioCategory)
		{
			esPioCategoryLineQuery query = this.GetDynamicQuery();
			query.Where(query.PioNo == pioNo, query.SRPioCategory == sRPioCategory);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 pioNo, String sRPioCategory)
		{
			esParameters parms = new esParameters();
			parms.Add("PioNo",pioNo);
			parms.Add("SRPioCategory",sRPioCategory);
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
						case "PioNo": this.str.PioNo = (string)value; break;
						case "SRPioCategory": this.str.SRPioCategory = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PioNo":
						
							if (value == null || value is System.Int32)
								this.PioNo = (System.Int32?)value;
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
		/// Maps to PioCategoryLine.PioNo
		/// </summary>
		virtual public System.Int32? PioNo
		{
			get
			{
				return base.GetSystemInt32(PioCategoryLineMetadata.ColumnNames.PioNo);
			}
			
			set
			{
				base.SetSystemInt32(PioCategoryLineMetadata.ColumnNames.PioNo, value);
			}
		}
		/// <summary>
		/// Maps to PioCategoryLine.SRPioCategory
		/// </summary>
		virtual public System.String SRPioCategory
		{
			get
			{
				return base.GetSystemString(PioCategoryLineMetadata.ColumnNames.SRPioCategory);
			}
			
			set
			{
				base.SetSystemString(PioCategoryLineMetadata.ColumnNames.SRPioCategory, value);
			}
		}
		/// <summary>
		/// Maps to PioCategoryLine.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PioCategoryLineMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PioCategoryLineMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PioCategoryLine.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PioCategoryLineMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PioCategoryLineMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPioCategoryLine entity)
			{
				this.entity = entity;
			}
			public System.String PioNo
			{
				get
				{
					System.Int32? data = entity.PioNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PioNo = null;
					else entity.PioNo = Convert.ToInt32(value);
				}
			}
			public System.String SRPioCategory
			{
				get
				{
					System.String data = entity.SRPioCategory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPioCategory = null;
					else entity.SRPioCategory = Convert.ToString(value);
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
			private esPioCategoryLine entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPioCategoryLineQuery query)
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
				throw new Exception("esPioCategoryLine can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PioCategoryLine : esPioCategoryLine
	{	
	}

	[Serializable]
	abstract public class esPioCategoryLineQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return PioCategoryLineMetadata.Meta();
			}
		}	
			
		public esQueryItem PioNo
		{
			get
			{
				return new esQueryItem(this, PioCategoryLineMetadata.ColumnNames.PioNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SRPioCategory
		{
			get
			{
				return new esQueryItem(this, PioCategoryLineMetadata.ColumnNames.SRPioCategory, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PioCategoryLineMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PioCategoryLineMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PioCategoryLineCollection")]
	public partial class PioCategoryLineCollection : esPioCategoryLineCollection, IEnumerable< PioCategoryLine>
	{
		public PioCategoryLineCollection()
		{

		}	
		
		public static implicit operator List< PioCategoryLine>(PioCategoryLineCollection coll)
		{
			List< PioCategoryLine> list = new List< PioCategoryLine>();
			
			foreach (PioCategoryLine emp in coll)
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
				return  PioCategoryLineMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PioCategoryLineQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PioCategoryLine(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PioCategoryLine();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public PioCategoryLineQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PioCategoryLineQuery();
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
		public bool Load(PioCategoryLineQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PioCategoryLine AddNew()
		{
			PioCategoryLine entity = base.AddNewEntity() as PioCategoryLine;
			
			return entity;		
		}
		public PioCategoryLine FindByPrimaryKey(Int32 pioNo, String sRPioCategory)
		{
			return base.FindByPrimaryKey(pioNo, sRPioCategory) as PioCategoryLine;
		}

		#region IEnumerable< PioCategoryLine> Members

		IEnumerator< PioCategoryLine> IEnumerable< PioCategoryLine>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PioCategoryLine;
			}
		}

		#endregion
		
		private PioCategoryLineQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PioCategoryLine' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PioCategoryLine ({PioNo, SRPioCategory})")]
	[Serializable]
	public partial class PioCategoryLine : esPioCategoryLine
	{
		public PioCategoryLine()
		{
		}	
	
		public PioCategoryLine(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PioCategoryLineMetadata.Meta();
			}
		}	
	
		override protected esPioCategoryLineQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PioCategoryLineQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public PioCategoryLineQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PioCategoryLineQuery();
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
		public bool Load(PioCategoryLineQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private PioCategoryLineQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PioCategoryLineQuery : esPioCategoryLineQuery
	{
		public PioCategoryLineQuery()
		{

		}		
		
		public PioCategoryLineQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "PioCategoryLineQuery";
        }
	}

	[Serializable]
	public partial class PioCategoryLineMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PioCategoryLineMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(PioCategoryLineMetadata.ColumnNames.PioNo, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PioCategoryLineMetadata.PropertyNames.PioNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PioCategoryLineMetadata.ColumnNames.SRPioCategory, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PioCategoryLineMetadata.PropertyNames.SRPioCategory;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PioCategoryLineMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PioCategoryLineMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PioCategoryLineMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PioCategoryLineMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public PioCategoryLineMetadata Meta()
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
			public const string PioNo = "PioNo";
			public const string SRPioCategory = "SRPioCategory";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string PioNo = "PioNo";
			public const string SRPioCategory = "SRPioCategory";
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
			lock (typeof(PioCategoryLineMetadata))
			{
				if(PioCategoryLineMetadata.mapDelegates == null)
				{
					PioCategoryLineMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PioCategoryLineMetadata.meta == null)
				{
					PioCategoryLineMetadata.meta = new PioCategoryLineMetadata();
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
				
				meta.AddTypeMap("PioNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRPioCategory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "PioCategoryLine";
				meta.Destination = "PioCategoryLine";
				meta.spInsert = "proc_PioCategoryLineInsert";				
				meta.spUpdate = "proc_PioCategoryLineUpdate";		
				meta.spDelete = "proc_PioCategoryLineDelete";
				meta.spLoadAll = "proc_PioCategoryLineLoadAll";
				meta.spLoadByPrimaryKey = "proc_PioCategoryLineLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PioCategoryLineMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
