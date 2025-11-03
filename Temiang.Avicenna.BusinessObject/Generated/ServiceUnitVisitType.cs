/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:26 PM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;


using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;



namespace Temiang.Avicenna.BusinessObject
{

	[Serializable]
	abstract public class esServiceUnitVisitTypeCollection : esEntityCollectionWAuditLog
	{
		public esServiceUnitVisitTypeCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ServiceUnitVisitTypeCollection";
		}

		#region Query Logic
		protected void InitQuery(esServiceUnitVisitTypeQuery query)
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
			this.InitQuery(query as esServiceUnitVisitTypeQuery);
		}
		#endregion
		
		virtual public ServiceUnitVisitType DetachEntity(ServiceUnitVisitType entity)
		{
			return base.DetachEntity(entity) as ServiceUnitVisitType;
		}
		
		virtual public ServiceUnitVisitType AttachEntity(ServiceUnitVisitType entity)
		{
			return base.AttachEntity(entity) as ServiceUnitVisitType;
		}
		
		virtual public void Combine(ServiceUnitVisitTypeCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ServiceUnitVisitType this[int index]
		{
			get
			{
				return base[index] as ServiceUnitVisitType;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ServiceUnitVisitType);
		}
	}



	[Serializable]
	abstract public class esServiceUnitVisitType : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esServiceUnitVisitTypeQuery GetDynamicQuery()
		{
			return null;
		}

		public esServiceUnitVisitType()
		{

		}

		public esServiceUnitVisitType(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String serviceUnitID, System.String visitTypeID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitID, visitTypeID);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitID, visitTypeID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String serviceUnitID, System.String visitTypeID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitID, visitTypeID);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitID, visitTypeID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String serviceUnitID, System.String visitTypeID)
		{
			esServiceUnitVisitTypeQuery query = this.GetDynamicQuery();
			query.Where(query.ServiceUnitID == serviceUnitID, query.VisitTypeID == visitTypeID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String serviceUnitID, System.String visitTypeID)
		{
			esParameters parms = new esParameters();
			parms.Add("ServiceUnitID",serviceUnitID);			parms.Add("VisitTypeID",visitTypeID);
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
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;							
						case "VisitTypeID": this.str.VisitTypeID = (string)value; break;							
						case "VisitDuration": this.str.VisitDuration = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "VisitDuration":
						
							if (value == null || value is System.Byte)
								this.VisitDuration = (System.Byte?)value;
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
		/// Maps to ServiceUnitVisitType.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(ServiceUnitVisitTypeMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitVisitTypeMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitVisitType.VisitTypeID
		/// </summary>
		virtual public System.String VisitTypeID
		{
			get
			{
				return base.GetSystemString(ServiceUnitVisitTypeMetadata.ColumnNames.VisitTypeID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitVisitTypeMetadata.ColumnNames.VisitTypeID, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitVisitType.VisitDuration
		/// </summary>
		virtual public System.Byte? VisitDuration
		{
			get
			{
				return base.GetSystemByte(ServiceUnitVisitTypeMetadata.ColumnNames.VisitDuration);
			}
			
			set
			{
				base.SetSystemByte(ServiceUnitVisitTypeMetadata.ColumnNames.VisitDuration, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitVisitType.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitVisitTypeMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceUnitVisitTypeMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitVisitType.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ServiceUnitVisitTypeMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitVisitTypeMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		#endregion	

		#region String Properties


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
			public esStrings(esServiceUnitVisitType entity)
			{
				this.entity = entity;
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
				
			public System.String VisitTypeID
			{
				get
				{
					System.String data = entity.VisitTypeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VisitTypeID = null;
					else entity.VisitTypeID = Convert.ToString(value);
				}
			}
				
			public System.String VisitDuration
			{
				get
				{
					System.Byte? data = entity.VisitDuration;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VisitDuration = null;
					else entity.VisitDuration = Convert.ToByte(value);
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
			

			private esServiceUnitVisitType entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esServiceUnitVisitTypeQuery query)
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
				throw new Exception("esServiceUnitVisitType can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ServiceUnitVisitType : esServiceUnitVisitType
	{

		
		/// <summary>
		/// Used internally by the entity's hierarchical properties.
		/// </summary>
		protected override List<esPropertyDescriptor> GetHierarchicalProperties()
		{
			List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();
			
		
			return props;
		}	
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PreSave.
		/// </summary>
		protected override void ApplyPreSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostSave.
		/// </summary>
		protected override void ApplyPostSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostOneToOneSave.
		/// </summary>
		protected override void ApplyPostOneSaveKeys()
		{
		}
		
	}



	[Serializable]
	abstract public class esServiceUnitVisitTypeQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitVisitTypeMetadata.Meta();
			}
		}	
		

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitVisitTypeMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem VisitTypeID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitVisitTypeMetadata.ColumnNames.VisitTypeID, esSystemType.String);
			}
		} 
		
		public esQueryItem VisitDuration
		{
			get
			{
				return new esQueryItem(this, ServiceUnitVisitTypeMetadata.ColumnNames.VisitDuration, esSystemType.Byte);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitVisitTypeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitVisitTypeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ServiceUnitVisitTypeCollection")]
	public partial class ServiceUnitVisitTypeCollection : esServiceUnitVisitTypeCollection, IEnumerable<ServiceUnitVisitType>
	{
		public ServiceUnitVisitTypeCollection()
		{

		}
		
		public static implicit operator List<ServiceUnitVisitType>(ServiceUnitVisitTypeCollection coll)
		{
			List<ServiceUnitVisitType> list = new List<ServiceUnitVisitType>();
			
			foreach (ServiceUnitVisitType emp in coll)
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
				return  ServiceUnitVisitTypeMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitVisitTypeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ServiceUnitVisitType(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ServiceUnitVisitType();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ServiceUnitVisitTypeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitVisitTypeQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ServiceUnitVisitTypeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ServiceUnitVisitType AddNew()
		{
			ServiceUnitVisitType entity = base.AddNewEntity() as ServiceUnitVisitType;
			
			return entity;
		}

		public ServiceUnitVisitType FindByPrimaryKey(System.String serviceUnitID, System.String visitTypeID)
		{
			return base.FindByPrimaryKey(serviceUnitID, visitTypeID) as ServiceUnitVisitType;
		}


		#region IEnumerable<ServiceUnitVisitType> Members

		IEnumerator<ServiceUnitVisitType> IEnumerable<ServiceUnitVisitType>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ServiceUnitVisitType;
			}
		}

		#endregion
		
		private ServiceUnitVisitTypeQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ServiceUnitVisitType' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ServiceUnitVisitType ({ServiceUnitID},{VisitTypeID})")]
	[Serializable]
	public partial class ServiceUnitVisitType : esServiceUnitVisitType
	{
		public ServiceUnitVisitType()
		{

		}
	
		public ServiceUnitVisitType(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitVisitTypeMetadata.Meta();
			}
		}
		
		
		
		override protected esServiceUnitVisitTypeQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitVisitTypeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ServiceUnitVisitTypeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitVisitTypeQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ServiceUnitVisitTypeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ServiceUnitVisitTypeQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ServiceUnitVisitTypeQuery : esServiceUnitVisitTypeQuery
	{
		public ServiceUnitVisitTypeQuery()
		{

		}		
		
		public ServiceUnitVisitTypeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ServiceUnitVisitTypeQuery";
        }
		
			
	}


	[Serializable]
	public partial class ServiceUnitVisitTypeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ServiceUnitVisitTypeMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ServiceUnitVisitTypeMetadata.ColumnNames.ServiceUnitID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitVisitTypeMetadata.PropertyNames.ServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitVisitTypeMetadata.ColumnNames.VisitTypeID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitVisitTypeMetadata.PropertyNames.VisitTypeID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitVisitTypeMetadata.ColumnNames.VisitDuration, 2, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = ServiceUnitVisitTypeMetadata.PropertyNames.VisitDuration;
			c.NumericPrecision = 3;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitVisitTypeMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitVisitTypeMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitVisitTypeMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitVisitTypeMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ServiceUnitVisitTypeMetadata Meta()
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
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string VisitTypeID = "VisitTypeID";
			 public const string VisitDuration = "VisitDuration";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string VisitTypeID = "VisitTypeID";
			 public const string VisitDuration = "VisitDuration";
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
			lock (typeof(ServiceUnitVisitTypeMetadata))
			{
				if(ServiceUnitVisitTypeMetadata.mapDelegates == null)
				{
					ServiceUnitVisitTypeMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ServiceUnitVisitTypeMetadata.meta == null)
				{
					ServiceUnitVisitTypeMetadata.meta = new ServiceUnitVisitTypeMetadata();
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
				

				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VisitTypeID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VisitDuration", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ServiceUnitVisitType";
				meta.Destination = "ServiceUnitVisitType";
				
				meta.spInsert = "proc_ServiceUnitVisitTypeInsert";				
				meta.spUpdate = "proc_ServiceUnitVisitTypeUpdate";		
				meta.spDelete = "proc_ServiceUnitVisitTypeDelete";
				meta.spLoadAll = "proc_ServiceUnitVisitTypeLoadAll";
				meta.spLoadByPrimaryKey = "proc_ServiceUnitVisitTypeLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ServiceUnitVisitTypeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
