/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/19/2023 12:02:46 PM
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
	abstract public class esSignPoolCollection : esEntityCollectionWAuditLog
	{
		public esSignPoolCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "SignPoolCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esSignPoolQuery query)
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
			this.InitQuery(query as esSignPoolQuery);
		}
		#endregion
			
		virtual public SignPool DetachEntity(SignPool entity)
		{
			return base.DetachEntity(entity) as SignPool;
		}
		
		virtual public SignPool AttachEntity(SignPool entity)
		{
			return base.AttachEntity(entity) as SignPool;
		}
		
		virtual public void Combine(SignPoolCollection collection)
		{
			base.Combine(collection);
		}
		
		new public SignPool this[int index]
		{
			get
			{
				return base[index] as SignPool;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SignPool);
		}
	}

	[Serializable]
	abstract public class esSignPool : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSignPoolQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esSignPool()
		{
		}
	
		public esSignPool(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 signID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(signID);
			else
				return LoadByPrimaryKeyStoredProcedure(signID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 signID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(signID);
			else
				return LoadByPrimaryKeyStoredProcedure(signID);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int64 signID)
		{
			esSignPoolQuery query = this.GetDynamicQuery();
			query.Where(query.SignID == signID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int64 signID)
		{
			esParameters parms = new esParameters();
			parms.Add("SignID",signID);
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
						case "SignID": this.str.SignID = (string)value; break;
						case "SignDateTime": this.str.SignDateTime = (string)value; break;
						case "SRRelationShip": this.str.SRRelationShip = (string)value; break;
						case "SignByID": this.str.SignByID = (string)value; break;
						case "SignByName": this.str.SignByName = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "SignID":
						
							if (value == null || value is System.Int64)
								this.SignID = (System.Int64?)value;
							break;
						case "SignImg":
						
							if (value == null || value is System.Byte[])
								this.SignImg = (System.Byte[])value;
							break;
						case "SignDateTime":
						
							if (value == null || value is System.DateTime)
								this.SignDateTime = (System.DateTime?)value;
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
		/// Maps to SignPool.SignID
		/// </summary>
		virtual public System.Int64? SignID
		{
			get
			{
				return base.GetSystemInt64(SignPoolMetadata.ColumnNames.SignID);
			}
			
			set
			{
				base.SetSystemInt64(SignPoolMetadata.ColumnNames.SignID, value);
			}
		}
		/// <summary>
		/// Maps to SignPool.SignImg
		/// </summary>
		virtual public System.Byte[] SignImg
		{
			get
			{
				return base.GetSystemByteArray(SignPoolMetadata.ColumnNames.SignImg);
			}
			
			set
			{
				base.SetSystemByteArray(SignPoolMetadata.ColumnNames.SignImg, value);
			}
		}
		/// <summary>
		/// Maps to SignPool.SignDateTime
		/// </summary>
		virtual public System.DateTime? SignDateTime
		{
			get
			{
				return base.GetSystemDateTime(SignPoolMetadata.ColumnNames.SignDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(SignPoolMetadata.ColumnNames.SignDateTime, value);
			}
		}
		/// <summary>
		/// Maps to SignPool.SRRelationShip
		/// </summary>
		virtual public System.String SRRelationShip
		{
			get
			{
				return base.GetSystemString(SignPoolMetadata.ColumnNames.SRRelationShip);
			}
			
			set
			{
				base.SetSystemString(SignPoolMetadata.ColumnNames.SRRelationShip, value);
			}
		}
		/// <summary>
		/// Maps to SignPool.SignByID
		/// </summary>
		virtual public System.String SignByID
		{
			get
			{
				return base.GetSystemString(SignPoolMetadata.ColumnNames.SignByID);
			}
			
			set
			{
				base.SetSystemString(SignPoolMetadata.ColumnNames.SignByID, value);
			}
		}
		/// <summary>
		/// Maps to SignPool.SignByName
		/// </summary>
		virtual public System.String SignByName
		{
			get
			{
				return base.GetSystemString(SignPoolMetadata.ColumnNames.SignByName);
			}
			
			set
			{
				base.SetSystemString(SignPoolMetadata.ColumnNames.SignByName, value);
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
			public esStrings(esSignPool entity)
			{
				this.entity = entity;
			}
			public System.String SignID
			{
				get
				{
					System.Int64? data = entity.SignID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SignID = null;
					else entity.SignID = Convert.ToInt64(value);
				}
			}
			public System.String SignDateTime
			{
				get
				{
					System.DateTime? data = entity.SignDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SignDateTime = null;
					else entity.SignDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String SRRelationShip
			{
				get
				{
					System.String data = entity.SRRelationShip;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRelationShip = null;
					else entity.SRRelationShip = Convert.ToString(value);
				}
			}
			public System.String SignByID
			{
				get
				{
					System.String data = entity.SignByID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SignByID = null;
					else entity.SignByID = Convert.ToString(value);
				}
			}
			public System.String SignByName
			{
				get
				{
					System.String data = entity.SignByName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SignByName = null;
					else entity.SignByName = Convert.ToString(value);
				}
			}
			private esSignPool entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSignPoolQuery query)
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
				throw new Exception("esSignPool can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class SignPool : esSignPool
	{	
	}

	[Serializable]
	abstract public class esSignPoolQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return SignPoolMetadata.Meta();
			}
		}	
			
		public esQueryItem SignID
		{
			get
			{
				return new esQueryItem(this, SignPoolMetadata.ColumnNames.SignID, esSystemType.Int64);
			}
		} 
			
		public esQueryItem SignImg
		{
			get
			{
				return new esQueryItem(this, SignPoolMetadata.ColumnNames.SignImg, esSystemType.ByteArray);
			}
		} 
			
		public esQueryItem SignDateTime
		{
			get
			{
				return new esQueryItem(this, SignPoolMetadata.ColumnNames.SignDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem SRRelationShip
		{
			get
			{
				return new esQueryItem(this, SignPoolMetadata.ColumnNames.SRRelationShip, esSystemType.String);
			}
		} 
			
		public esQueryItem SignByID
		{
			get
			{
				return new esQueryItem(this, SignPoolMetadata.ColumnNames.SignByID, esSystemType.String);
			}
		} 
			
		public esQueryItem SignByName
		{
			get
			{
				return new esQueryItem(this, SignPoolMetadata.ColumnNames.SignByName, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SignPoolCollection")]
	public partial class SignPoolCollection : esSignPoolCollection, IEnumerable< SignPool>
	{
		public SignPoolCollection()
		{

		}	
		
		public static implicit operator List< SignPool>(SignPoolCollection coll)
		{
			List< SignPool> list = new List< SignPool>();
			
			foreach (SignPool emp in coll)
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
				return  SignPoolMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SignPoolQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SignPool(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SignPool();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public SignPoolQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SignPoolQuery();
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
		public bool Load(SignPoolQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public SignPool AddNew()
		{
			SignPool entity = base.AddNewEntity() as SignPool;
			
			return entity;		
		}
		public SignPool FindByPrimaryKey(Int64 signID)
		{
			return base.FindByPrimaryKey(signID) as SignPool;
		}

		#region IEnumerable< SignPool> Members

		IEnumerator< SignPool> IEnumerable< SignPool>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as SignPool;
			}
		}

		#endregion
		
		private SignPoolQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SignPool' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("SignPool ({SignID})")]
	[Serializable]
	public partial class SignPool : esSignPool
	{
		public SignPool()
		{
		}	
	
		public SignPool(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SignPoolMetadata.Meta();
			}
		}	
	
		override protected esSignPoolQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SignPoolQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public SignPoolQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SignPoolQuery();
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
		public bool Load(SignPoolQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private SignPoolQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class SignPoolQuery : esSignPoolQuery
	{
		public SignPoolQuery()
		{

		}		
		
		public SignPoolQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "SignPoolQuery";
        }
	}

	[Serializable]
	public partial class SignPoolMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SignPoolMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(SignPoolMetadata.ColumnNames.SignID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = SignPoolMetadata.PropertyNames.SignID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c); 
				
			c = new esColumnMetadata(SignPoolMetadata.ColumnNames.SignImg, 1, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = SignPoolMetadata.PropertyNames.SignImg;
			c.NumericPrecision = 0;
			_columns.Add(c); 
				
			c = new esColumnMetadata(SignPoolMetadata.ColumnNames.SignDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SignPoolMetadata.PropertyNames.SignDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(SignPoolMetadata.ColumnNames.SRRelationShip, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = SignPoolMetadata.PropertyNames.SRRelationShip;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(SignPoolMetadata.ColumnNames.SignByID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = SignPoolMetadata.PropertyNames.SignByID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(SignPoolMetadata.ColumnNames.SignByName, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = SignPoolMetadata.PropertyNames.SignByName;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public SignPoolMetadata Meta()
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
			public const string SignID = "SignID";
			public const string SignImg = "SignImg";
			public const string SignDateTime = "SignDateTime";
			public const string SRRelationShip = "SRRelationShip";
			public const string SignByID = "SignByID";
			public const string SignByName = "SignByName";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string SignID = "SignID";
			public const string SignImg = "SignImg";
			public const string SignDateTime = "SignDateTime";
			public const string SRRelationShip = "SRRelationShip";
			public const string SignByID = "SignByID";
			public const string SignByName = "SignByName";
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
			lock (typeof(SignPoolMetadata))
			{
				if(SignPoolMetadata.mapDelegates == null)
				{
					SignPoolMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (SignPoolMetadata.meta == null)
				{
					SignPoolMetadata.meta = new SignPoolMetadata();
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
				
				meta.AddTypeMap("SignID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("SignImg", new esTypeMap("image", "System.Byte[]"));
				meta.AddTypeMap("SignDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRRelationShip", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SignByID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SignByName", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "SignPool";
				meta.Destination = "SignPool";
				meta.spInsert = "proc_SignPoolInsert";				
				meta.spUpdate = "proc_SignPoolUpdate";		
				meta.spDelete = "proc_SignPoolDelete";
				meta.spLoadAll = "proc_SignPoolLoadAll";
				meta.spLoadByPrimaryKey = "proc_SignPoolLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SignPoolMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
