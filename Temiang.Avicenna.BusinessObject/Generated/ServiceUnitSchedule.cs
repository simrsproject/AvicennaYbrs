/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/23/2018 6:26:45 PM
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
	abstract public class esServiceUnitScheduleCollection : esEntityCollectionWAuditLog
	{
		public esServiceUnitScheduleCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ServiceUnitScheduleCollection";
		}

		#region Query Logic
		protected void InitQuery(esServiceUnitScheduleQuery query)
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
			this.InitQuery(query as esServiceUnitScheduleQuery);
		}
		#endregion
		
		virtual public ServiceUnitSchedule DetachEntity(ServiceUnitSchedule entity)
		{
			return base.DetachEntity(entity) as ServiceUnitSchedule;
		}
		
		virtual public ServiceUnitSchedule AttachEntity(ServiceUnitSchedule entity)
		{
			return base.AttachEntity(entity) as ServiceUnitSchedule;
		}
		
		virtual public void Combine(ServiceUnitScheduleCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ServiceUnitSchedule this[int index]
		{
			get
			{
				return base[index] as ServiceUnitSchedule;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ServiceUnitSchedule);
		}
	}



	[Serializable]
	abstract public class esServiceUnitSchedule : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esServiceUnitScheduleQuery GetDynamicQuery()
		{
			return null;
		}

		public esServiceUnitSchedule()
		{

		}

		public esServiceUnitSchedule(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 dayOfWeek, System.String serviceUnitID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(dayOfWeek, serviceUnitID);
			else
				return LoadByPrimaryKeyStoredProcedure(dayOfWeek, serviceUnitID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 dayOfWeek, System.String serviceUnitID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(dayOfWeek, serviceUnitID);
			else
				return LoadByPrimaryKeyStoredProcedure(dayOfWeek, serviceUnitID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 dayOfWeek, System.String serviceUnitID)
		{
			esServiceUnitScheduleQuery query = this.GetDynamicQuery();
			query.Where(query.DayOfWeek == dayOfWeek, query.ServiceUnitID == serviceUnitID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 dayOfWeek, System.String serviceUnitID)
		{
			esParameters parms = new esParameters();
			parms.Add("DayOfWeek",dayOfWeek);			parms.Add("ServiceUnitID",serviceUnitID);
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
						case "DayOfWeek": this.str.DayOfWeek = (string)value; break;							
						case "StartTime": this.str.StartTime = (string)value; break;							
						case "EndTime": this.str.EndTime = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "DayOfWeek":
						
							if (value == null || value is System.Int32)
								this.DayOfWeek = (System.Int32?)value;
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
		/// Maps to ServiceUnitSchedule.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(ServiceUnitScheduleMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitScheduleMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitSchedule.DayOfWeek
		/// </summary>
		virtual public System.Int32? DayOfWeek
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitScheduleMetadata.ColumnNames.DayOfWeek);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitScheduleMetadata.ColumnNames.DayOfWeek, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitSchedule.StartTime
		/// </summary>
		virtual public System.String StartTime
		{
			get
			{
				return base.GetSystemString(ServiceUnitScheduleMetadata.ColumnNames.StartTime);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitScheduleMetadata.ColumnNames.StartTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitSchedule.EndTime
		/// </summary>
		virtual public System.String EndTime
		{
			get
			{
				return base.GetSystemString(ServiceUnitScheduleMetadata.ColumnNames.EndTime);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitScheduleMetadata.ColumnNames.EndTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitSchedule.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitScheduleMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceUnitScheduleMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitSchedule.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ServiceUnitScheduleMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitScheduleMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esServiceUnitSchedule entity)
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
				
			public System.String DayOfWeek
			{
				get
				{
					System.Int32? data = entity.DayOfWeek;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DayOfWeek = null;
					else entity.DayOfWeek = Convert.ToInt32(value);
				}
			}
				
			public System.String StartTime
			{
				get
				{
					System.String data = entity.StartTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartTime = null;
					else entity.StartTime = Convert.ToString(value);
				}
			}
				
			public System.String EndTime
			{
				get
				{
					System.String data = entity.EndTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndTime = null;
					else entity.EndTime = Convert.ToString(value);
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
			

			private esServiceUnitSchedule entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esServiceUnitScheduleQuery query)
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
				throw new Exception("esServiceUnitSchedule can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ServiceUnitSchedule : esServiceUnitSchedule
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
	abstract public class esServiceUnitScheduleQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitScheduleMetadata.Meta();
			}
		}	
		

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitScheduleMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem DayOfWeek
		{
			get
			{
				return new esQueryItem(this, ServiceUnitScheduleMetadata.ColumnNames.DayOfWeek, esSystemType.Int32);
			}
		} 
		
		public esQueryItem StartTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitScheduleMetadata.ColumnNames.StartTime, esSystemType.String);
			}
		} 
		
		public esQueryItem EndTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitScheduleMetadata.ColumnNames.EndTime, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitScheduleMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitScheduleMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ServiceUnitScheduleCollection")]
	public partial class ServiceUnitScheduleCollection : esServiceUnitScheduleCollection, IEnumerable<ServiceUnitSchedule>
	{
		public ServiceUnitScheduleCollection()
		{

		}
		
		public static implicit operator List<ServiceUnitSchedule>(ServiceUnitScheduleCollection coll)
		{
			List<ServiceUnitSchedule> list = new List<ServiceUnitSchedule>();
			
			foreach (ServiceUnitSchedule emp in coll)
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
				return  ServiceUnitScheduleMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitScheduleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ServiceUnitSchedule(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ServiceUnitSchedule();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ServiceUnitScheduleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitScheduleQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ServiceUnitScheduleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ServiceUnitSchedule AddNew()
		{
			ServiceUnitSchedule entity = base.AddNewEntity() as ServiceUnitSchedule;
			
			return entity;
		}

		public ServiceUnitSchedule FindByPrimaryKey(System.Int32 dayOfWeek, System.String serviceUnitID)
		{
			return base.FindByPrimaryKey(dayOfWeek, serviceUnitID) as ServiceUnitSchedule;
		}


		#region IEnumerable<ServiceUnitSchedule> Members

		IEnumerator<ServiceUnitSchedule> IEnumerable<ServiceUnitSchedule>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ServiceUnitSchedule;
			}
		}

		#endregion
		
		private ServiceUnitScheduleQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ServiceUnitSchedule' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ServiceUnitSchedule ({ServiceUnitID},{DayOfWeek})")]
	[Serializable]
	public partial class ServiceUnitSchedule : esServiceUnitSchedule
	{
		public ServiceUnitSchedule()
		{

		}
	
		public ServiceUnitSchedule(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitScheduleMetadata.Meta();
			}
		}
		
		
		
		override protected esServiceUnitScheduleQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitScheduleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ServiceUnitScheduleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitScheduleQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ServiceUnitScheduleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ServiceUnitScheduleQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ServiceUnitScheduleQuery : esServiceUnitScheduleQuery
	{
		public ServiceUnitScheduleQuery()
		{

		}		
		
		public ServiceUnitScheduleQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ServiceUnitScheduleQuery";
        }
		
			
	}


	[Serializable]
	public partial class ServiceUnitScheduleMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ServiceUnitScheduleMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ServiceUnitScheduleMetadata.ColumnNames.ServiceUnitID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitScheduleMetadata.PropertyNames.ServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitScheduleMetadata.ColumnNames.DayOfWeek, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitScheduleMetadata.PropertyNames.DayOfWeek;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitScheduleMetadata.ColumnNames.StartTime, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitScheduleMetadata.PropertyNames.StartTime;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitScheduleMetadata.ColumnNames.EndTime, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitScheduleMetadata.PropertyNames.EndTime;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitScheduleMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitScheduleMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitScheduleMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitScheduleMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ServiceUnitScheduleMetadata Meta()
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
			 public const string DayOfWeek = "DayOfWeek";
			 public const string StartTime = "StartTime";
			 public const string EndTime = "EndTime";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string DayOfWeek = "DayOfWeek";
			 public const string StartTime = "StartTime";
			 public const string EndTime = "EndTime";
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
			lock (typeof(ServiceUnitScheduleMetadata))
			{
				if(ServiceUnitScheduleMetadata.mapDelegates == null)
				{
					ServiceUnitScheduleMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ServiceUnitScheduleMetadata.meta == null)
				{
					ServiceUnitScheduleMetadata.meta = new ServiceUnitScheduleMetadata();
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
				meta.AddTypeMap("DayOfWeek", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("StartTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EndTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ServiceUnitSchedule";
				meta.Destination = "ServiceUnitSchedule";
				
				meta.spInsert = "proc_ServiceUnitScheduleInsert";				
				meta.spUpdate = "proc_ServiceUnitScheduleUpdate";		
				meta.spDelete = "proc_ServiceUnitScheduleDelete";
				meta.spLoadAll = "proc_ServiceUnitScheduleLoadAll";
				meta.spLoadByPrimaryKey = "proc_ServiceUnitScheduleLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ServiceUnitScheduleMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
