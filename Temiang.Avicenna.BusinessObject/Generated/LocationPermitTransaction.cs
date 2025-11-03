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
	abstract public class esLocationPermitTransactionCollection : esEntityCollectionWAuditLog
	{
		public esLocationPermitTransactionCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "LocationPermitTransactionCollection";
		}

		#region Query Logic
		protected void InitQuery(esLocationPermitTransactionQuery query)
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
			this.InitQuery(query as esLocationPermitTransactionQuery);
		}
		#endregion
		
		virtual public LocationPermitTransaction DetachEntity(LocationPermitTransaction entity)
		{
			return base.DetachEntity(entity) as LocationPermitTransaction;
		}
		
		virtual public LocationPermitTransaction AttachEntity(LocationPermitTransaction entity)
		{
			return base.AttachEntity(entity) as LocationPermitTransaction;
		}
		
		virtual public void Combine(LocationPermitTransactionCollection collection)
		{
			base.Combine(collection);
		}
		
		new public LocationPermitTransaction this[int index]
		{
			get
			{
				return base[index] as LocationPermitTransaction;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LocationPermitTransaction);
		}
	}



	[Serializable]
	abstract public class esLocationPermitTransaction : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLocationPermitTransactionQuery GetDynamicQuery()
		{
			return null;
		}

		public esLocationPermitTransaction()
		{

		}

		public esLocationPermitTransaction(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String permitID, System.String sRTransactionType)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(permitID, sRTransactionType);
			else
				return LoadByPrimaryKeyStoredProcedure(permitID, sRTransactionType);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String permitID, System.String sRTransactionType)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(permitID, sRTransactionType);
			else
				return LoadByPrimaryKeyStoredProcedure(permitID, sRTransactionType);
		}

		private bool LoadByPrimaryKeyDynamic(System.String permitID, System.String sRTransactionType)
		{
			esLocationPermitTransactionQuery query = this.GetDynamicQuery();
			query.Where(query.PermitID == permitID, query.SRTransactionType == sRTransactionType);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String permitID, System.String sRTransactionType)
		{
			esParameters parms = new esParameters();
			parms.Add("PermitID",permitID);			parms.Add("SRTransactionType",sRTransactionType);
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
						case "SRTransactionType": this.str.SRTransactionType = (string)value; break;							
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
		/// Maps to LocationPermitTransaction.PermitID
		/// </summary>
		virtual public System.String PermitID
		{
			get
			{
				return base.GetSystemString(LocationPermitTransactionMetadata.ColumnNames.PermitID);
			}
			
			set
			{
				base.SetSystemString(LocationPermitTransactionMetadata.ColumnNames.PermitID, value);
			}
		}
		
		/// <summary>
		/// Maps to LocationPermitTransaction.SRTransactionType
		/// </summary>
		virtual public System.String SRTransactionType
		{
			get
			{
				return base.GetSystemString(LocationPermitTransactionMetadata.ColumnNames.SRTransactionType);
			}
			
			set
			{
				base.SetSystemString(LocationPermitTransactionMetadata.ColumnNames.SRTransactionType, value);
			}
		}
		
		/// <summary>
		/// Maps to LocationPermitTransaction.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(LocationPermitTransactionMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(LocationPermitTransactionMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to LocationPermitTransaction.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(LocationPermitTransactionMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(LocationPermitTransactionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to LocationPermitTransaction.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(LocationPermitTransactionMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(LocationPermitTransactionMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esLocationPermitTransaction entity)
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
				
			public System.String SRTransactionType
			{
				get
				{
					System.String data = entity.SRTransactionType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRTransactionType = null;
					else entity.SRTransactionType = Convert.ToString(value);
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
			

			private esLocationPermitTransaction entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLocationPermitTransactionQuery query)
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
				throw new Exception("esLocationPermitTransaction can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class LocationPermitTransaction : esLocationPermitTransaction
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
	abstract public class esLocationPermitTransactionQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return LocationPermitTransactionMetadata.Meta();
			}
		}	
		

		public esQueryItem PermitID
		{
			get
			{
				return new esQueryItem(this, LocationPermitTransactionMetadata.ColumnNames.PermitID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRTransactionType
		{
			get
			{
				return new esQueryItem(this, LocationPermitTransactionMetadata.ColumnNames.SRTransactionType, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, LocationPermitTransactionMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, LocationPermitTransactionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, LocationPermitTransactionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LocationPermitTransactionCollection")]
	public partial class LocationPermitTransactionCollection : esLocationPermitTransactionCollection, IEnumerable<LocationPermitTransaction>
	{
		public LocationPermitTransactionCollection()
		{

		}
		
		public static implicit operator List<LocationPermitTransaction>(LocationPermitTransactionCollection coll)
		{
			List<LocationPermitTransaction> list = new List<LocationPermitTransaction>();
			
			foreach (LocationPermitTransaction emp in coll)
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
				return  LocationPermitTransactionMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LocationPermitTransactionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LocationPermitTransaction(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LocationPermitTransaction();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public LocationPermitTransactionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LocationPermitTransactionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(LocationPermitTransactionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public LocationPermitTransaction AddNew()
		{
			LocationPermitTransaction entity = base.AddNewEntity() as LocationPermitTransaction;
			
			return entity;
		}

		public LocationPermitTransaction FindByPrimaryKey(System.String permitID, System.String sRTransactionType)
		{
			return base.FindByPrimaryKey(permitID, sRTransactionType) as LocationPermitTransaction;
		}


		#region IEnumerable<LocationPermitTransaction> Members

		IEnumerator<LocationPermitTransaction> IEnumerable<LocationPermitTransaction>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as LocationPermitTransaction;
			}
		}

		#endregion
		
		private LocationPermitTransactionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'LocationPermitTransaction' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("LocationPermitTransaction ({PermitID},{SRTransactionType})")]
	[Serializable]
	public partial class LocationPermitTransaction : esLocationPermitTransaction
	{
		public LocationPermitTransaction()
		{

		}
	
		public LocationPermitTransaction(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LocationPermitTransactionMetadata.Meta();
			}
		}
		
		
		
		override protected esLocationPermitTransactionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LocationPermitTransactionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public LocationPermitTransactionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LocationPermitTransactionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(LocationPermitTransactionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private LocationPermitTransactionQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class LocationPermitTransactionQuery : esLocationPermitTransactionQuery
	{
		public LocationPermitTransactionQuery()
		{

		}		
		
		public LocationPermitTransactionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "LocationPermitTransactionQuery";
        }
		
			
	}


	[Serializable]
	public partial class LocationPermitTransactionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LocationPermitTransactionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LocationPermitTransactionMetadata.ColumnNames.PermitID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = LocationPermitTransactionMetadata.PropertyNames.PermitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(LocationPermitTransactionMetadata.ColumnNames.SRTransactionType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = LocationPermitTransactionMetadata.PropertyNames.SRTransactionType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(LocationPermitTransactionMetadata.ColumnNames.IsActive, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LocationPermitTransactionMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c);
				
			c = new esColumnMetadata(LocationPermitTransactionMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LocationPermitTransactionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LocationPermitTransactionMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = LocationPermitTransactionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public LocationPermitTransactionMetadata Meta()
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
			 public const string SRTransactionType = "SRTransactionType";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PermitID = "PermitID";
			 public const string SRTransactionType = "SRTransactionType";
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
			lock (typeof(LocationPermitTransactionMetadata))
			{
				if(LocationPermitTransactionMetadata.mapDelegates == null)
				{
					LocationPermitTransactionMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (LocationPermitTransactionMetadata.meta == null)
				{
					LocationPermitTransactionMetadata.meta = new LocationPermitTransactionMetadata();
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
				meta.AddTypeMap("SRTransactionType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "LocationPermitTransaction";
				meta.Destination = "LocationPermitTransaction";
				
				meta.spInsert = "proc_LocationPermitTransactionInsert";				
				meta.spUpdate = "proc_LocationPermitTransactionUpdate";		
				meta.spDelete = "proc_LocationPermitTransactionDelete";
				meta.spLoadAll = "proc_LocationPermitTransactionLoadAll";
				meta.spLoadByPrimaryKey = "proc_LocationPermitTransactionLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LocationPermitTransactionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
