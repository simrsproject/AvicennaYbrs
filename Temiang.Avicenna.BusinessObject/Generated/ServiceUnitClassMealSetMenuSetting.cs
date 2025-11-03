/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/25/2014 1:34:55 PM
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
	abstract public class esServiceUnitClassMealSetMenuSettingCollection : esEntityCollectionWAuditLog
	{
		public esServiceUnitClassMealSetMenuSettingCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ServiceUnitClassMealSetMenuSettingCollection";
		}

		#region Query Logic
		protected void InitQuery(esServiceUnitClassMealSetMenuSettingQuery query)
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
			this.InitQuery(query as esServiceUnitClassMealSetMenuSettingQuery);
		}
		#endregion
		
		virtual public ServiceUnitClassMealSetMenuSetting DetachEntity(ServiceUnitClassMealSetMenuSetting entity)
		{
			return base.DetachEntity(entity) as ServiceUnitClassMealSetMenuSetting;
		}
		
		virtual public ServiceUnitClassMealSetMenuSetting AttachEntity(ServiceUnitClassMealSetMenuSetting entity)
		{
			return base.AttachEntity(entity) as ServiceUnitClassMealSetMenuSetting;
		}
		
		virtual public void Combine(ServiceUnitClassMealSetMenuSettingCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ServiceUnitClassMealSetMenuSetting this[int index]
		{
			get
			{
				return base[index] as ServiceUnitClassMealSetMenuSetting;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ServiceUnitClassMealSetMenuSetting);
		}
	}



	[Serializable]
	abstract public class esServiceUnitClassMealSetMenuSetting : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esServiceUnitClassMealSetMenuSettingQuery GetDynamicQuery()
		{
			return null;
		}

		public esServiceUnitClassMealSetMenuSetting()
		{

		}

		public esServiceUnitClassMealSetMenuSetting(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String serviceUnitID, System.String classID, System.String sRMealSet)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitID, classID, sRMealSet);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitID, classID, sRMealSet);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String serviceUnitID, System.String classID, System.String sRMealSet)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitID, classID, sRMealSet);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitID, classID, sRMealSet);
		}

		private bool LoadByPrimaryKeyDynamic(System.String serviceUnitID, System.String classID, System.String sRMealSet)
		{
			esServiceUnitClassMealSetMenuSettingQuery query = this.GetDynamicQuery();
			query.Where(query.ServiceUnitID == serviceUnitID, query.ClassID == classID, query.SRMealSet == sRMealSet);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String serviceUnitID, System.String classID, System.String sRMealSet)
		{
			esParameters parms = new esParameters();
			parms.Add("ServiceUnitID",serviceUnitID);			parms.Add("ClassID",classID);			parms.Add("SRMealSet",sRMealSet);
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
						case "ClassID": this.str.ClassID = (string)value; break;							
						case "SRMealSet": this.str.SRMealSet = (string)value; break;							
						case "IsOptional": this.str.IsOptional = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsOptional":
						
							if (value == null || value is System.Boolean)
								this.IsOptional = (System.Boolean?)value;
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
		/// Maps to ServiceUnitClassMealSetMenuSetting.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(ServiceUnitClassMealSetMenuSettingMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitClassMealSetMenuSettingMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitClassMealSetMenuSetting.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(ServiceUnitClassMealSetMenuSettingMetadata.ColumnNames.ClassID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitClassMealSetMenuSettingMetadata.ColumnNames.ClassID, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitClassMealSetMenuSetting.SRMealSet
		/// </summary>
		virtual public System.String SRMealSet
		{
			get
			{
				return base.GetSystemString(ServiceUnitClassMealSetMenuSettingMetadata.ColumnNames.SRMealSet);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitClassMealSetMenuSettingMetadata.ColumnNames.SRMealSet, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitClassMealSetMenuSetting.IsOptional
		/// </summary>
		virtual public System.Boolean? IsOptional
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitClassMealSetMenuSettingMetadata.ColumnNames.IsOptional);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceUnitClassMealSetMenuSettingMetadata.ColumnNames.IsOptional, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitClassMealSetMenuSetting.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitClassMealSetMenuSettingMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceUnitClassMealSetMenuSettingMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitClassMealSetMenuSetting.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ServiceUnitClassMealSetMenuSettingMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitClassMealSetMenuSettingMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esServiceUnitClassMealSetMenuSetting entity)
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
				
			public System.String ClassID
			{
				get
				{
					System.String data = entity.ClassID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClassID = null;
					else entity.ClassID = Convert.ToString(value);
				}
			}
				
			public System.String SRMealSet
			{
				get
				{
					System.String data = entity.SRMealSet;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMealSet = null;
					else entity.SRMealSet = Convert.ToString(value);
				}
			}
				
			public System.String IsOptional
			{
				get
				{
					System.Boolean? data = entity.IsOptional;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOptional = null;
					else entity.IsOptional = Convert.ToBoolean(value);
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
			

			private esServiceUnitClassMealSetMenuSetting entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esServiceUnitClassMealSetMenuSettingQuery query)
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
				throw new Exception("esServiceUnitClassMealSetMenuSetting can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ServiceUnitClassMealSetMenuSetting : esServiceUnitClassMealSetMenuSetting
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
	abstract public class esServiceUnitClassMealSetMenuSettingQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitClassMealSetMenuSettingMetadata.Meta();
			}
		}	
		

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitClassMealSetMenuSettingMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitClassMealSetMenuSettingMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRMealSet
		{
			get
			{
				return new esQueryItem(this, ServiceUnitClassMealSetMenuSettingMetadata.ColumnNames.SRMealSet, esSystemType.String);
			}
		} 
		
		public esQueryItem IsOptional
		{
			get
			{
				return new esQueryItem(this, ServiceUnitClassMealSetMenuSettingMetadata.ColumnNames.IsOptional, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitClassMealSetMenuSettingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitClassMealSetMenuSettingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ServiceUnitClassMealSetMenuSettingCollection")]
	public partial class ServiceUnitClassMealSetMenuSettingCollection : esServiceUnitClassMealSetMenuSettingCollection, IEnumerable<ServiceUnitClassMealSetMenuSetting>
	{
		public ServiceUnitClassMealSetMenuSettingCollection()
		{

		}
		
		public static implicit operator List<ServiceUnitClassMealSetMenuSetting>(ServiceUnitClassMealSetMenuSettingCollection coll)
		{
			List<ServiceUnitClassMealSetMenuSetting> list = new List<ServiceUnitClassMealSetMenuSetting>();
			
			foreach (ServiceUnitClassMealSetMenuSetting emp in coll)
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
				return  ServiceUnitClassMealSetMenuSettingMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitClassMealSetMenuSettingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ServiceUnitClassMealSetMenuSetting(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ServiceUnitClassMealSetMenuSetting();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ServiceUnitClassMealSetMenuSettingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitClassMealSetMenuSettingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ServiceUnitClassMealSetMenuSettingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ServiceUnitClassMealSetMenuSetting AddNew()
		{
			ServiceUnitClassMealSetMenuSetting entity = base.AddNewEntity() as ServiceUnitClassMealSetMenuSetting;
			
			return entity;
		}

		public ServiceUnitClassMealSetMenuSetting FindByPrimaryKey(System.String serviceUnitID, System.String classID, System.String sRMealSet)
		{
			return base.FindByPrimaryKey(serviceUnitID, classID, sRMealSet) as ServiceUnitClassMealSetMenuSetting;
		}


		#region IEnumerable<ServiceUnitClassMealSetMenuSetting> Members

		IEnumerator<ServiceUnitClassMealSetMenuSetting> IEnumerable<ServiceUnitClassMealSetMenuSetting>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ServiceUnitClassMealSetMenuSetting;
			}
		}

		#endregion
		
		private ServiceUnitClassMealSetMenuSettingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ServiceUnitClassMealSetMenuSetting' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ServiceUnitClassMealSetMenuSetting ({ServiceUnitID},{ClassID},{SRMealSet})")]
	[Serializable]
	public partial class ServiceUnitClassMealSetMenuSetting : esServiceUnitClassMealSetMenuSetting
	{
		public ServiceUnitClassMealSetMenuSetting()
		{

		}
	
		public ServiceUnitClassMealSetMenuSetting(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitClassMealSetMenuSettingMetadata.Meta();
			}
		}
		
		
		
		override protected esServiceUnitClassMealSetMenuSettingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitClassMealSetMenuSettingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ServiceUnitClassMealSetMenuSettingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitClassMealSetMenuSettingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ServiceUnitClassMealSetMenuSettingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ServiceUnitClassMealSetMenuSettingQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ServiceUnitClassMealSetMenuSettingQuery : esServiceUnitClassMealSetMenuSettingQuery
	{
		public ServiceUnitClassMealSetMenuSettingQuery()
		{

		}		
		
		public ServiceUnitClassMealSetMenuSettingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ServiceUnitClassMealSetMenuSettingQuery";
        }
		
			
	}


	[Serializable]
	public partial class ServiceUnitClassMealSetMenuSettingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ServiceUnitClassMealSetMenuSettingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ServiceUnitClassMealSetMenuSettingMetadata.ColumnNames.ServiceUnitID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitClassMealSetMenuSettingMetadata.PropertyNames.ServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitClassMealSetMenuSettingMetadata.ColumnNames.ClassID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitClassMealSetMenuSettingMetadata.PropertyNames.ClassID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitClassMealSetMenuSettingMetadata.ColumnNames.SRMealSet, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitClassMealSetMenuSettingMetadata.PropertyNames.SRMealSet;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitClassMealSetMenuSettingMetadata.ColumnNames.IsOptional, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitClassMealSetMenuSettingMetadata.PropertyNames.IsOptional;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitClassMealSetMenuSettingMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitClassMealSetMenuSettingMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitClassMealSetMenuSettingMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitClassMealSetMenuSettingMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ServiceUnitClassMealSetMenuSettingMetadata Meta()
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
			 public const string ClassID = "ClassID";
			 public const string SRMealSet = "SRMealSet";
			 public const string IsOptional = "IsOptional";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string ClassID = "ClassID";
			 public const string SRMealSet = "SRMealSet";
			 public const string IsOptional = "IsOptional";
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
			lock (typeof(ServiceUnitClassMealSetMenuSettingMetadata))
			{
				if(ServiceUnitClassMealSetMenuSettingMetadata.mapDelegates == null)
				{
					ServiceUnitClassMealSetMenuSettingMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ServiceUnitClassMealSetMenuSettingMetadata.meta == null)
				{
					ServiceUnitClassMealSetMenuSettingMetadata.meta = new ServiceUnitClassMealSetMenuSettingMetadata();
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
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMealSet", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOptional", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ServiceUnitClassMealSetMenuSetting";
				meta.Destination = "ServiceUnitClassMealSetMenuSetting";
				
				meta.spInsert = "proc_ServiceUnitClassMealSetMenuSettingInsert";				
				meta.spUpdate = "proc_ServiceUnitClassMealSetMenuSettingUpdate";		
				meta.spDelete = "proc_ServiceUnitClassMealSetMenuSettingDelete";
				meta.spLoadAll = "proc_ServiceUnitClassMealSetMenuSettingLoadAll";
				meta.spLoadByPrimaryKey = "proc_ServiceUnitClassMealSetMenuSettingLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ServiceUnitClassMealSetMenuSettingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
