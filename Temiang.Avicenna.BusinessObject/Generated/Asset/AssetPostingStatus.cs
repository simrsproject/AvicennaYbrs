/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:11 PM
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
	abstract public class esAssetPostingStatusCollection : esEntityCollectionWAuditLog
	{
		public esAssetPostingStatusCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "AssetPostingStatusCollection";
		}

		#region Query Logic
		protected void InitQuery(esAssetPostingStatusQuery query)
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
			this.InitQuery(query as esAssetPostingStatusQuery);
		}
		#endregion
		
		virtual public AssetPostingStatus DetachEntity(AssetPostingStatus entity)
		{
			return base.DetachEntity(entity) as AssetPostingStatus;
		}
		
		virtual public AssetPostingStatus AttachEntity(AssetPostingStatus entity)
		{
			return base.AttachEntity(entity) as AssetPostingStatus;
		}
		
		virtual public void Combine(AssetPostingStatusCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AssetPostingStatus this[int index]
		{
			get
			{
				return base[index] as AssetPostingStatus;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AssetPostingStatus);
		}
	}



	[Serializable]
	abstract public class esAssetPostingStatus : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAssetPostingStatusQuery GetDynamicQuery()
		{
			return null;
		}

		public esAssetPostingStatus()
		{

		}

		public esAssetPostingStatus(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 postingId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(postingId);
			else
				return LoadByPrimaryKeyStoredProcedure(postingId);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 postingId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(postingId);
			else
				return LoadByPrimaryKeyStoredProcedure(postingId);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 postingId)
		{
			esAssetPostingStatusQuery query = this.GetDynamicQuery();
			query.Where(query.PostingId == postingId);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 postingId)
		{
			esParameters parms = new esParameters();
			parms.Add("PostingId",postingId);
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
						case "PostingId": this.str.PostingId = (string)value; break;							
						case "Month": this.str.Month = (string)value; break;							
						case "Year": this.str.Year = (string)value; break;							
						case "IsEnabled": this.str.IsEnabled = (string)value; break;							
						case "CreatedBy": this.str.CreatedBy = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "DateCreated": this.str.DateCreated = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PostingId":
						
							if (value == null || value is System.Int32)
								this.PostingId = (System.Int32?)value;
							break;
						
						case "IsEnabled":
						
							if (value == null || value is System.Boolean)
								this.IsEnabled = (System.Boolean?)value;
							break;
						
						case "DateCreated":
						
							if (value == null || value is System.DateTime)
								this.DateCreated = (System.DateTime?)value;
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
		/// Maps to AssetPostingStatus.PostingId
		/// </summary>
		virtual public System.Int32? PostingId
		{
			get
			{
				return base.GetSystemInt32(AssetPostingStatusMetadata.ColumnNames.PostingId);
			}
			
			set
			{
				base.SetSystemInt32(AssetPostingStatusMetadata.ColumnNames.PostingId, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetPostingStatus.Month
		/// </summary>
		virtual public System.String Month
		{
			get
			{
				return base.GetSystemString(AssetPostingStatusMetadata.ColumnNames.Month);
			}
			
			set
			{
				base.SetSystemString(AssetPostingStatusMetadata.ColumnNames.Month, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetPostingStatus.Year
		/// </summary>
		virtual public System.String Year
		{
			get
			{
				return base.GetSystemString(AssetPostingStatusMetadata.ColumnNames.Year);
			}
			
			set
			{
				base.SetSystemString(AssetPostingStatusMetadata.ColumnNames.Year, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetPostingStatus.IsEnabled
		/// </summary>
		virtual public System.Boolean? IsEnabled
		{
			get
			{
				return base.GetSystemBoolean(AssetPostingStatusMetadata.ColumnNames.IsEnabled);
			}
			
			set
			{
				base.SetSystemBoolean(AssetPostingStatusMetadata.ColumnNames.IsEnabled, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetPostingStatus.CreatedBy
		/// </summary>
		virtual public System.String CreatedBy
		{
			get
			{
				return base.GetSystemString(AssetPostingStatusMetadata.ColumnNames.CreatedBy);
			}
			
			set
			{
				base.SetSystemString(AssetPostingStatusMetadata.ColumnNames.CreatedBy, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetPostingStatus.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AssetPostingStatusMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AssetPostingStatusMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetPostingStatus.DateCreated
		/// </summary>
		virtual public System.DateTime? DateCreated
		{
			get
			{
				return base.GetSystemDateTime(AssetPostingStatusMetadata.ColumnNames.DateCreated);
			}
			
			set
			{
				base.SetSystemDateTime(AssetPostingStatusMetadata.ColumnNames.DateCreated, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetPostingStatus.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetPostingStatusMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AssetPostingStatusMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esAssetPostingStatus entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PostingId
			{
				get
				{
					System.Int32? data = entity.PostingId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PostingId = null;
					else entity.PostingId = Convert.ToInt32(value);
				}
			}
				
			public System.String Month
			{
				get
				{
					System.String data = entity.Month;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Month = null;
					else entity.Month = Convert.ToString(value);
				}
			}
				
			public System.String Year
			{
				get
				{
					System.String data = entity.Year;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Year = null;
					else entity.Year = Convert.ToString(value);
				}
			}
				
			public System.String IsEnabled
			{
				get
				{
					System.Boolean? data = entity.IsEnabled;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEnabled = null;
					else entity.IsEnabled = Convert.ToBoolean(value);
				}
			}
				
			public System.String CreatedBy
			{
				get
				{
					System.String data = entity.CreatedBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedBy = null;
					else entity.CreatedBy = Convert.ToString(value);
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
				
			public System.String DateCreated
			{
				get
				{
					System.DateTime? data = entity.DateCreated;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateCreated = null;
					else entity.DateCreated = Convert.ToDateTime(value);
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
			

			private esAssetPostingStatus entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAssetPostingStatusQuery query)
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
				throw new Exception("esAssetPostingStatus can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class AssetPostingStatus : esAssetPostingStatus
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
	abstract public class esAssetPostingStatusQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AssetPostingStatusMetadata.Meta();
			}
		}	
		

		public esQueryItem PostingId
		{
			get
			{
				return new esQueryItem(this, AssetPostingStatusMetadata.ColumnNames.PostingId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Month
		{
			get
			{
				return new esQueryItem(this, AssetPostingStatusMetadata.ColumnNames.Month, esSystemType.String);
			}
		} 
		
		public esQueryItem Year
		{
			get
			{
				return new esQueryItem(this, AssetPostingStatusMetadata.ColumnNames.Year, esSystemType.String);
			}
		} 
		
		public esQueryItem IsEnabled
		{
			get
			{
				return new esQueryItem(this, AssetPostingStatusMetadata.ColumnNames.IsEnabled, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem CreatedBy
		{
			get
			{
				return new esQueryItem(this, AssetPostingStatusMetadata.ColumnNames.CreatedBy, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AssetPostingStatusMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem DateCreated
		{
			get
			{
				return new esQueryItem(this, AssetPostingStatusMetadata.ColumnNames.DateCreated, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AssetPostingStatusMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AssetPostingStatusCollection")]
	public partial class AssetPostingStatusCollection : esAssetPostingStatusCollection, IEnumerable<AssetPostingStatus>
	{
		public AssetPostingStatusCollection()
		{

		}
		
		public static implicit operator List<AssetPostingStatus>(AssetPostingStatusCollection coll)
		{
			List<AssetPostingStatus> list = new List<AssetPostingStatus>();
			
			foreach (AssetPostingStatus emp in coll)
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
				return  AssetPostingStatusMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetPostingStatusQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AssetPostingStatus(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AssetPostingStatus();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public AssetPostingStatusQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetPostingStatusQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(AssetPostingStatusQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public AssetPostingStatus AddNew()
		{
			AssetPostingStatus entity = base.AddNewEntity() as AssetPostingStatus;
			
			return entity;
		}

		public AssetPostingStatus FindByPrimaryKey(System.Int32 postingId)
		{
			return base.FindByPrimaryKey(postingId) as AssetPostingStatus;
		}


		#region IEnumerable<AssetPostingStatus> Members

		IEnumerator<AssetPostingStatus> IEnumerable<AssetPostingStatus>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AssetPostingStatus;
			}
		}

		#endregion
		
		private AssetPostingStatusQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AssetPostingStatus' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("AssetPostingStatus ({PostingId})")]
	[Serializable]
	public partial class AssetPostingStatus : esAssetPostingStatus
	{
		public AssetPostingStatus()
		{

		}
	
		public AssetPostingStatus(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AssetPostingStatusMetadata.Meta();
			}
		}
		
		
		
		override protected esAssetPostingStatusQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetPostingStatusQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public AssetPostingStatusQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetPostingStatusQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(AssetPostingStatusQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private AssetPostingStatusQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class AssetPostingStatusQuery : esAssetPostingStatusQuery
	{
		public AssetPostingStatusQuery()
		{

		}		
		
		public AssetPostingStatusQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "AssetPostingStatusQuery";
        }
		
			
	}


	[Serializable]
	public partial class AssetPostingStatusMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AssetPostingStatusMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AssetPostingStatusMetadata.ColumnNames.PostingId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AssetPostingStatusMetadata.PropertyNames.PostingId;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetPostingStatusMetadata.ColumnNames.Month, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetPostingStatusMetadata.PropertyNames.Month;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetPostingStatusMetadata.ColumnNames.Year, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetPostingStatusMetadata.PropertyNames.Year;
			c.CharacterMaxLength = 4;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetPostingStatusMetadata.ColumnNames.IsEnabled, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetPostingStatusMetadata.PropertyNames.IsEnabled;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetPostingStatusMetadata.ColumnNames.CreatedBy, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetPostingStatusMetadata.PropertyNames.CreatedBy;
			c.CharacterMaxLength = 25;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetPostingStatusMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetPostingStatusMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 25;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetPostingStatusMetadata.ColumnNames.DateCreated, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetPostingStatusMetadata.PropertyNames.DateCreated;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetPostingStatusMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetPostingStatusMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public AssetPostingStatusMetadata Meta()
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
			 public const string PostingId = "PostingId";
			 public const string Month = "Month";
			 public const string Year = "Year";
			 public const string IsEnabled = "IsEnabled";
			 public const string CreatedBy = "CreatedBy";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string DateCreated = "DateCreated";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PostingId = "PostingId";
			 public const string Month = "Month";
			 public const string Year = "Year";
			 public const string IsEnabled = "IsEnabled";
			 public const string CreatedBy = "CreatedBy";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string DateCreated = "DateCreated";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
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
			lock (typeof(AssetPostingStatusMetadata))
			{
				if(AssetPostingStatusMetadata.mapDelegates == null)
				{
					AssetPostingStatusMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AssetPostingStatusMetadata.meta == null)
				{
					AssetPostingStatusMetadata.meta = new AssetPostingStatusMetadata();
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
				

				meta.AddTypeMap("PostingId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Month", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Year", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsEnabled", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreatedBy", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("DateCreated", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));			
				
				
				
				meta.Source = "AssetPostingStatus";
				meta.Destination = "AssetPostingStatus";
				
				meta.spInsert = "proc_AssetPostingStatusInsert";				
				meta.spUpdate = "proc_AssetPostingStatusUpdate";		
				meta.spDelete = "proc_AssetPostingStatusDelete";
				meta.spLoadAll = "proc_AssetPostingStatusLoadAll";
				meta.spLoadByPrimaryKey = "proc_AssetPostingStatusLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AssetPostingStatusMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
