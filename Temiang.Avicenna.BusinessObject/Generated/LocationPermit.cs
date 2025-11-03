/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:19 PM
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
	abstract public class esLocationPermitCollection : esEntityCollectionWAuditLog
	{
		public esLocationPermitCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "LocationPermitCollection";
		}

		#region Query Logic
		protected void InitQuery(esLocationPermitQuery query)
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
			this.InitQuery(query as esLocationPermitQuery);
		}
		#endregion
		
		virtual public LocationPermit DetachEntity(LocationPermit entity)
		{
			return base.DetachEntity(entity) as LocationPermit;
		}
		
		virtual public LocationPermit AttachEntity(LocationPermit entity)
		{
			return base.AttachEntity(entity) as LocationPermit;
		}
		
		virtual public void Combine(LocationPermitCollection collection)
		{
			base.Combine(collection);
		}
		
		new public LocationPermit this[int index]
		{
			get
			{
				return base[index] as LocationPermit;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LocationPermit);
		}
	}



	[Serializable]
	abstract public class esLocationPermit : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLocationPermitQuery GetDynamicQuery()
		{
			return null;
		}

		public esLocationPermit()
		{

		}

		public esLocationPermit(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String permitID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(permitID);
			else
				return LoadByPrimaryKeyStoredProcedure(permitID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String permitID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(permitID);
			else
				return LoadByPrimaryKeyStoredProcedure(permitID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String permitID)
		{
			esLocationPermitQuery query = this.GetDynamicQuery();
			query.Where(query.PermitID == permitID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String permitID)
		{
			esParameters parms = new esParameters();
			parms.Add("PermitID",permitID);
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
						case "PermitID": this.str.PermitID = (string)value; break;							
						case "PermitName": this.str.PermitName = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
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
		/// Maps to LocationPermit.PermitID
		/// </summary>
		virtual public System.String PermitID
		{
			get
			{
				return base.GetSystemString(LocationPermitMetadata.ColumnNames.PermitID);
			}
			
			set
			{
				base.SetSystemString(LocationPermitMetadata.ColumnNames.PermitID, value);
			}
		}
		
		/// <summary>
		/// Maps to LocationPermit.PermitName
		/// </summary>
		virtual public System.String PermitName
		{
			get
			{
				return base.GetSystemString(LocationPermitMetadata.ColumnNames.PermitName);
			}
			
			set
			{
				base.SetSystemString(LocationPermitMetadata.ColumnNames.PermitName, value);
			}
		}
		
		/// <summary>
		/// Maps to LocationPermit.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(LocationPermitMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(LocationPermitMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to LocationPermit.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(LocationPermitMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(LocationPermitMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to LocationPermit.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(LocationPermitMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(LocationPermitMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esLocationPermit entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PermitID
			{
				get
				{
					System.String data = entity.PermitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PermitID = null;
					else entity.PermitID = Convert.ToString(value);
				}
			}
				
			public System.String PermitName
			{
				get
				{
					System.String data = entity.PermitName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PermitName = null;
					else entity.PermitName = Convert.ToString(value);
				}
			}
				
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
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
			

			private esLocationPermit entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLocationPermitQuery query)
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
				throw new Exception("esLocationPermit can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class LocationPermit : esLocationPermit
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
	abstract public class esLocationPermitQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return LocationPermitMetadata.Meta();
			}
		}	
		

		public esQueryItem PermitID
		{
			get
			{
				return new esQueryItem(this, LocationPermitMetadata.ColumnNames.PermitID, esSystemType.String);
			}
		} 
		
		public esQueryItem PermitName
		{
			get
			{
				return new esQueryItem(this, LocationPermitMetadata.ColumnNames.PermitName, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, LocationPermitMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, LocationPermitMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, LocationPermitMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LocationPermitCollection")]
	public partial class LocationPermitCollection : esLocationPermitCollection, IEnumerable<LocationPermit>
	{
		public LocationPermitCollection()
		{

		}
		
		public static implicit operator List<LocationPermit>(LocationPermitCollection coll)
		{
			List<LocationPermit> list = new List<LocationPermit>();
			
			foreach (LocationPermit emp in coll)
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
				return  LocationPermitMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LocationPermitQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LocationPermit(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LocationPermit();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public LocationPermitQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LocationPermitQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(LocationPermitQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public LocationPermit AddNew()
		{
			LocationPermit entity = base.AddNewEntity() as LocationPermit;
			
			return entity;
		}

		public LocationPermit FindByPrimaryKey(System.String permitID)
		{
			return base.FindByPrimaryKey(permitID) as LocationPermit;
		}


		#region IEnumerable<LocationPermit> Members

		IEnumerator<LocationPermit> IEnumerable<LocationPermit>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as LocationPermit;
			}
		}

		#endregion
		
		private LocationPermitQuery query;
	}


	/// <summary>
	/// Encapsulates the 'LocationPermit' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("LocationPermit ({PermitID})")]
	[Serializable]
	public partial class LocationPermit : esLocationPermit
	{
		public LocationPermit()
		{

		}
	
		public LocationPermit(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LocationPermitMetadata.Meta();
			}
		}
		
		
		
		override protected esLocationPermitQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LocationPermitQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public LocationPermitQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LocationPermitQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(LocationPermitQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private LocationPermitQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class LocationPermitQuery : esLocationPermitQuery
	{
		public LocationPermitQuery()
		{

		}		
		
		public LocationPermitQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "LocationPermitQuery";
        }
		
			
	}


	[Serializable]
	public partial class LocationPermitMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LocationPermitMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LocationPermitMetadata.ColumnNames.PermitID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = LocationPermitMetadata.PropertyNames.PermitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(LocationPermitMetadata.ColumnNames.PermitName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = LocationPermitMetadata.PropertyNames.PermitName;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(LocationPermitMetadata.ColumnNames.IsActive, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LocationPermitMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c);
				
			c = new esColumnMetadata(LocationPermitMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LocationPermitMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LocationPermitMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = LocationPermitMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public LocationPermitMetadata Meta()
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
			 public const string PermitID = "PermitID";
			 public const string PermitName = "PermitName";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PermitID = "PermitID";
			 public const string PermitName = "PermitName";
			 public const string IsActive = "IsActive";
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
			lock (typeof(LocationPermitMetadata))
			{
				if(LocationPermitMetadata.mapDelegates == null)
				{
					LocationPermitMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (LocationPermitMetadata.meta == null)
				{
					LocationPermitMetadata.meta = new LocationPermitMetadata();
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
				

				meta.AddTypeMap("PermitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PermitName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "LocationPermit";
				meta.Destination = "LocationPermit";
				
				meta.spInsert = "proc_LocationPermitInsert";				
				meta.spUpdate = "proc_LocationPermitUpdate";		
				meta.spDelete = "proc_LocationPermitDelete";
				meta.spLoadAll = "proc_LocationPermitLoadAll";
				meta.spLoadByPrimaryKey = "proc_LocationPermitLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LocationPermitMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
