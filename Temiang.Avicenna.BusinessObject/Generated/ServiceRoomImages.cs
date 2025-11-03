/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/7/2020 6:36:15 AM
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
	abstract public class esServiceRoomImagesCollection : esEntityCollectionWAuditLog
	{
		public esServiceRoomImagesCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ServiceRoomImagesCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esServiceRoomImagesQuery query)
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
			this.InitQuery(query as esServiceRoomImagesQuery);
		}
		#endregion
			
		virtual public ServiceRoomImages DetachEntity(ServiceRoomImages entity)
		{
			return base.DetachEntity(entity) as ServiceRoomImages;
		}
		
		virtual public ServiceRoomImages AttachEntity(ServiceRoomImages entity)
		{
			return base.AttachEntity(entity) as ServiceRoomImages;
		}
		
		virtual public void Combine(ServiceRoomImagesCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ServiceRoomImages this[int index]
		{
			get
			{
				return base[index] as ServiceRoomImages;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ServiceRoomImages);
		}
	}

	[Serializable]
	abstract public class esServiceRoomImages : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esServiceRoomImagesQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esServiceRoomImages()
		{
		}
	
		public esServiceRoomImages(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String roomID, Int32 seqNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(roomID, seqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(roomID, seqNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String roomID, Int32 seqNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(roomID, seqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(roomID, seqNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String roomID, Int32 seqNo)
		{
			esServiceRoomImagesQuery query = this.GetDynamicQuery();
			query.Where(query.RoomID==roomID, query.SeqNo==seqNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String roomID, Int32 seqNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RoomID",roomID);
			parms.Add("SeqNo",seqNo);
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
						case "RoomID": this.str.RoomID = (string)value; break;
						case "SeqNo": this.str.SeqNo = (string)value; break;
						case "IndexNo": this.str.IndexNo = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "SeqNo":
						
							if (value == null || value is System.Int32)
								this.SeqNo = (System.Int32?)value;
							break;
						case "IndexNo":
						
							if (value == null || value is System.Int32)
								this.IndexNo = (System.Int32?)value;
							break;
						case "Photo":
						
							if (value == null || value is System.Byte[])
								this.Photo = (System.Byte[])value;
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
		/// Maps to ServiceRoomImages.RoomID
		/// </summary>
		virtual public System.String RoomID
		{
			get
			{
				return base.GetSystemString(ServiceRoomImagesMetadata.ColumnNames.RoomID);
			}
			
			set
			{
				base.SetSystemString(ServiceRoomImagesMetadata.ColumnNames.RoomID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceRoomImages.SeqNo
		/// </summary>
		virtual public System.Int32? SeqNo
		{
			get
			{
				return base.GetSystemInt32(ServiceRoomImagesMetadata.ColumnNames.SeqNo);
			}
			
			set
			{
				base.SetSystemInt32(ServiceRoomImagesMetadata.ColumnNames.SeqNo, value);
			}
		}
		/// <summary>
		/// Maps to ServiceRoomImages.IndexNo
		/// </summary>
		virtual public System.Int32? IndexNo
		{
			get
			{
				return base.GetSystemInt32(ServiceRoomImagesMetadata.ColumnNames.IndexNo);
			}
			
			set
			{
				base.SetSystemInt32(ServiceRoomImagesMetadata.ColumnNames.IndexNo, value);
			}
		}
		/// <summary>
		/// Maps to ServiceRoomImages.Photo
		/// </summary>
		virtual public System.Byte[] Photo
		{
			get
			{
				return base.GetSystemByteArray(ServiceRoomImagesMetadata.ColumnNames.Photo);
			}
			
			set
			{
				base.SetSystemByteArray(ServiceRoomImagesMetadata.ColumnNames.Photo, value);
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
			public esStrings(esServiceRoomImages entity)
			{
				this.entity = entity;
			}
			public System.String RoomID
			{
				get
				{
					System.String data = entity.RoomID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RoomID = null;
					else entity.RoomID = Convert.ToString(value);
				}
			}
			public System.String SeqNo
			{
				get
				{
					System.Int32? data = entity.SeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SeqNo = null;
					else entity.SeqNo = Convert.ToInt32(value);
				}
			}
			public System.String IndexNo
			{
				get
				{
					System.Int32? data = entity.IndexNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IndexNo = null;
					else entity.IndexNo = Convert.ToInt32(value);
				}
			}
			private esServiceRoomImages entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esServiceRoomImagesQuery query)
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
				throw new Exception("esServiceRoomImages can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ServiceRoomImages : esServiceRoomImages
	{	
	}

	[Serializable]
	abstract public class esServiceRoomImagesQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ServiceRoomImagesMetadata.Meta();
			}
		}	
			
		public esQueryItem RoomID
		{
			get
			{
				return new esQueryItem(this, ServiceRoomImagesMetadata.ColumnNames.RoomID, esSystemType.String);
			}
		} 
			
		public esQueryItem SeqNo
		{
			get
			{
				return new esQueryItem(this, ServiceRoomImagesMetadata.ColumnNames.SeqNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem IndexNo
		{
			get
			{
				return new esQueryItem(this, ServiceRoomImagesMetadata.ColumnNames.IndexNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Photo
		{
			get
			{
				return new esQueryItem(this, ServiceRoomImagesMetadata.ColumnNames.Photo, esSystemType.ByteArray);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ServiceRoomImagesCollection")]
	public partial class ServiceRoomImagesCollection : esServiceRoomImagesCollection, IEnumerable< ServiceRoomImages>
	{
		public ServiceRoomImagesCollection()
		{

		}	
		
		public static implicit operator List< ServiceRoomImages>(ServiceRoomImagesCollection coll)
		{
			List< ServiceRoomImages> list = new List< ServiceRoomImages>();
			
			foreach (ServiceRoomImages emp in coll)
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
				return  ServiceRoomImagesMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceRoomImagesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ServiceRoomImages(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ServiceRoomImages();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ServiceRoomImagesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceRoomImagesQuery();
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
		public bool Load(ServiceRoomImagesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ServiceRoomImages AddNew()
		{
			ServiceRoomImages entity = base.AddNewEntity() as ServiceRoomImages;
			
			return entity;		
		}
		public ServiceRoomImages FindByPrimaryKey(String roomID, Int32 seqNo)
		{
			return base.FindByPrimaryKey(roomID, seqNo) as ServiceRoomImages;
		}

		#region IEnumerable< ServiceRoomImages> Members

		IEnumerator< ServiceRoomImages> IEnumerable< ServiceRoomImages>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ServiceRoomImages;
			}
		}

		#endregion
		
		private ServiceRoomImagesQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ServiceRoomImages' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ServiceRoomImages ({RoomID, SeqNo})")]
	[Serializable]
	public partial class ServiceRoomImages : esServiceRoomImages
	{
		public ServiceRoomImages()
		{
		}	
	
		public ServiceRoomImages(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ServiceRoomImagesMetadata.Meta();
			}
		}	
	
		override protected esServiceRoomImagesQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceRoomImagesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ServiceRoomImagesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceRoomImagesQuery();
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
		public bool Load(ServiceRoomImagesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ServiceRoomImagesQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ServiceRoomImagesQuery : esServiceRoomImagesQuery
	{
		public ServiceRoomImagesQuery()
		{

		}		
		
		public ServiceRoomImagesQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ServiceRoomImagesQuery";
        }
	}

	[Serializable]
	public partial class ServiceRoomImagesMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ServiceRoomImagesMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ServiceRoomImagesMetadata.ColumnNames.RoomID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceRoomImagesMetadata.PropertyNames.RoomID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceRoomImagesMetadata.ColumnNames.SeqNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceRoomImagesMetadata.PropertyNames.SeqNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceRoomImagesMetadata.ColumnNames.IndexNo, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceRoomImagesMetadata.PropertyNames.IndexNo;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceRoomImagesMetadata.ColumnNames.Photo, 3, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = ServiceRoomImagesMetadata.PropertyNames.Photo;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ServiceRoomImagesMetadata Meta()
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
			public const string RoomID = "RoomID";
			public const string SeqNo = "SeqNo";
			public const string IndexNo = "IndexNo";
			public const string Photo = "Photo";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RoomID = "RoomID";
			public const string SeqNo = "SeqNo";
			public const string IndexNo = "IndexNo";
			public const string Photo = "Photo";
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
			lock (typeof(ServiceRoomImagesMetadata))
			{
				if(ServiceRoomImagesMetadata.mapDelegates == null)
				{
					ServiceRoomImagesMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ServiceRoomImagesMetadata.meta == null)
				{
					ServiceRoomImagesMetadata.meta = new ServiceRoomImagesMetadata();
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
				
				meta.AddTypeMap("RoomID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SeqNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IndexNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Photo", new esTypeMap("image", "System.Byte[]"));
		

				meta.Source = "ServiceRoomImages";
				meta.Destination = "ServiceRoomImages";
				meta.spInsert = "proc_ServiceRoomImagesInsert";				
				meta.spUpdate = "proc_ServiceRoomImagesUpdate";		
				meta.spDelete = "proc_ServiceRoomImagesDelete";
				meta.spLoadAll = "proc_ServiceRoomImagesLoadAll";
				meta.spLoadByPrimaryKey = "proc_ServiceRoomImagesLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ServiceRoomImagesMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
