/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/11/2015 3:06:52 PM
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
	abstract public class esAssetPreventiveMaintenanceSchedulePeriodCollection : esEntityCollectionWAuditLog
	{
		public esAssetPreventiveMaintenanceSchedulePeriodCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "AssetPreventiveMaintenanceSchedulePeriodCollection";
		}

		#region Query Logic
		protected void InitQuery(esAssetPreventiveMaintenanceSchedulePeriodQuery query)
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
			this.InitQuery(query as esAssetPreventiveMaintenanceSchedulePeriodQuery);
		}
		#endregion
		
		virtual public AssetPreventiveMaintenanceSchedulePeriod DetachEntity(AssetPreventiveMaintenanceSchedulePeriod entity)
		{
			return base.DetachEntity(entity) as AssetPreventiveMaintenanceSchedulePeriod;
		}
		
		virtual public AssetPreventiveMaintenanceSchedulePeriod AttachEntity(AssetPreventiveMaintenanceSchedulePeriod entity)
		{
			return base.AttachEntity(entity) as AssetPreventiveMaintenanceSchedulePeriod;
		}
		
		virtual public void Combine(AssetPreventiveMaintenanceSchedulePeriodCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AssetPreventiveMaintenanceSchedulePeriod this[int index]
		{
			get
			{
				return base[index] as AssetPreventiveMaintenanceSchedulePeriod;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AssetPreventiveMaintenanceSchedulePeriod);
		}
	}



	[Serializable]
	abstract public class esAssetPreventiveMaintenanceSchedulePeriod : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAssetPreventiveMaintenanceSchedulePeriodQuery GetDynamicQuery()
		{
			return null;
		}

		public esAssetPreventiveMaintenanceSchedulePeriod()
		{

		}

		public esAssetPreventiveMaintenanceSchedulePeriod(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String assetID, System.String periodYear)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(assetID, periodYear);
			else
				return LoadByPrimaryKeyStoredProcedure(assetID, periodYear);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String assetID, System.String periodYear)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(assetID, periodYear);
			else
				return LoadByPrimaryKeyStoredProcedure(assetID, periodYear);
		}

		private bool LoadByPrimaryKeyDynamic(System.String assetID, System.String periodYear)
		{
			esAssetPreventiveMaintenanceSchedulePeriodQuery query = this.GetDynamicQuery();
			query.Where(query.AssetID == assetID, query.PeriodYear == periodYear);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String assetID, System.String periodYear)
		{
			esParameters parms = new esParameters();
			parms.Add("AssetID",assetID);			parms.Add("PeriodYear",periodYear);
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
						case "AssetID": this.str.AssetID = (string)value; break;							
						case "PeriodYear": this.str.PeriodYear = (string)value; break;							
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
		/// Maps to AssetPreventiveMaintenanceSchedulePeriod.AssetID
		/// </summary>
		virtual public System.String AssetID
		{
			get
			{
				return base.GetSystemString(AssetPreventiveMaintenanceSchedulePeriodMetadata.ColumnNames.AssetID);
			}
			
			set
			{
				base.SetSystemString(AssetPreventiveMaintenanceSchedulePeriodMetadata.ColumnNames.AssetID, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetPreventiveMaintenanceSchedulePeriod.PeriodYear
		/// </summary>
		virtual public System.String PeriodYear
		{
			get
			{
				return base.GetSystemString(AssetPreventiveMaintenanceSchedulePeriodMetadata.ColumnNames.PeriodYear);
			}
			
			set
			{
				base.SetSystemString(AssetPreventiveMaintenanceSchedulePeriodMetadata.ColumnNames.PeriodYear, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetPreventiveMaintenanceSchedulePeriod.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetPreventiveMaintenanceSchedulePeriodMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AssetPreventiveMaintenanceSchedulePeriodMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetPreventiveMaintenanceSchedulePeriod.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AssetPreventiveMaintenanceSchedulePeriodMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AssetPreventiveMaintenanceSchedulePeriodMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esAssetPreventiveMaintenanceSchedulePeriod entity)
			{
				this.entity = entity;
			}
			
	
			public System.String AssetID
			{
				get
				{
					System.String data = entity.AssetID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetID = null;
					else entity.AssetID = Convert.ToString(value);
				}
			}
				
			public System.String PeriodYear
			{
				get
				{
					System.String data = entity.PeriodYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodYear = null;
					else entity.PeriodYear = Convert.ToString(value);
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
			

			private esAssetPreventiveMaintenanceSchedulePeriod entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAssetPreventiveMaintenanceSchedulePeriodQuery query)
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
				throw new Exception("esAssetPreventiveMaintenanceSchedulePeriod can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class AssetPreventiveMaintenanceSchedulePeriod : esAssetPreventiveMaintenanceSchedulePeriod
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
	abstract public class esAssetPreventiveMaintenanceSchedulePeriodQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AssetPreventiveMaintenanceSchedulePeriodMetadata.Meta();
			}
		}	
		

		public esQueryItem AssetID
		{
			get
			{
				return new esQueryItem(this, AssetPreventiveMaintenanceSchedulePeriodMetadata.ColumnNames.AssetID, esSystemType.String);
			}
		} 
		
		public esQueryItem PeriodYear
		{
			get
			{
				return new esQueryItem(this, AssetPreventiveMaintenanceSchedulePeriodMetadata.ColumnNames.PeriodYear, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AssetPreventiveMaintenanceSchedulePeriodMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AssetPreventiveMaintenanceSchedulePeriodMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AssetPreventiveMaintenanceSchedulePeriodCollection")]
	public partial class AssetPreventiveMaintenanceSchedulePeriodCollection : esAssetPreventiveMaintenanceSchedulePeriodCollection, IEnumerable<AssetPreventiveMaintenanceSchedulePeriod>
	{
		public AssetPreventiveMaintenanceSchedulePeriodCollection()
		{

		}
		
		public static implicit operator List<AssetPreventiveMaintenanceSchedulePeriod>(AssetPreventiveMaintenanceSchedulePeriodCollection coll)
		{
			List<AssetPreventiveMaintenanceSchedulePeriod> list = new List<AssetPreventiveMaintenanceSchedulePeriod>();
			
			foreach (AssetPreventiveMaintenanceSchedulePeriod emp in coll)
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
				return  AssetPreventiveMaintenanceSchedulePeriodMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetPreventiveMaintenanceSchedulePeriodQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AssetPreventiveMaintenanceSchedulePeriod(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AssetPreventiveMaintenanceSchedulePeriod();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public AssetPreventiveMaintenanceSchedulePeriodQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetPreventiveMaintenanceSchedulePeriodQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(AssetPreventiveMaintenanceSchedulePeriodQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public AssetPreventiveMaintenanceSchedulePeriod AddNew()
		{
			AssetPreventiveMaintenanceSchedulePeriod entity = base.AddNewEntity() as AssetPreventiveMaintenanceSchedulePeriod;
			
			return entity;
		}

		public AssetPreventiveMaintenanceSchedulePeriod FindByPrimaryKey(System.String assetID, System.String periodYear)
		{
			return base.FindByPrimaryKey(assetID, periodYear) as AssetPreventiveMaintenanceSchedulePeriod;
		}


		#region IEnumerable<AssetPreventiveMaintenanceSchedulePeriod> Members

		IEnumerator<AssetPreventiveMaintenanceSchedulePeriod> IEnumerable<AssetPreventiveMaintenanceSchedulePeriod>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AssetPreventiveMaintenanceSchedulePeriod;
			}
		}

		#endregion
		
		private AssetPreventiveMaintenanceSchedulePeriodQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AssetPreventiveMaintenanceSchedulePeriod' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("AssetPreventiveMaintenanceSchedulePeriod ({AssetID},{PeriodYear})")]
	[Serializable]
	public partial class AssetPreventiveMaintenanceSchedulePeriod : esAssetPreventiveMaintenanceSchedulePeriod
	{
		public AssetPreventiveMaintenanceSchedulePeriod()
		{

		}
	
		public AssetPreventiveMaintenanceSchedulePeriod(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AssetPreventiveMaintenanceSchedulePeriodMetadata.Meta();
			}
		}
		
		
		
		override protected esAssetPreventiveMaintenanceSchedulePeriodQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetPreventiveMaintenanceSchedulePeriodQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public AssetPreventiveMaintenanceSchedulePeriodQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetPreventiveMaintenanceSchedulePeriodQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(AssetPreventiveMaintenanceSchedulePeriodQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private AssetPreventiveMaintenanceSchedulePeriodQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class AssetPreventiveMaintenanceSchedulePeriodQuery : esAssetPreventiveMaintenanceSchedulePeriodQuery
	{
		public AssetPreventiveMaintenanceSchedulePeriodQuery()
		{

		}		
		
		public AssetPreventiveMaintenanceSchedulePeriodQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "AssetPreventiveMaintenanceSchedulePeriodQuery";
        }
		
			
	}


	[Serializable]
	public partial class AssetPreventiveMaintenanceSchedulePeriodMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AssetPreventiveMaintenanceSchedulePeriodMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AssetPreventiveMaintenanceSchedulePeriodMetadata.ColumnNames.AssetID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetPreventiveMaintenanceSchedulePeriodMetadata.PropertyNames.AssetID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 30;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetPreventiveMaintenanceSchedulePeriodMetadata.ColumnNames.PeriodYear, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetPreventiveMaintenanceSchedulePeriodMetadata.PropertyNames.PeriodYear;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 4;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetPreventiveMaintenanceSchedulePeriodMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetPreventiveMaintenanceSchedulePeriodMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetPreventiveMaintenanceSchedulePeriodMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetPreventiveMaintenanceSchedulePeriodMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public AssetPreventiveMaintenanceSchedulePeriodMetadata Meta()
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
			 public const string AssetID = "AssetID";
			 public const string PeriodYear = "PeriodYear";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string AssetID = "AssetID";
			 public const string PeriodYear = "PeriodYear";
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
			lock (typeof(AssetPreventiveMaintenanceSchedulePeriodMetadata))
			{
				if(AssetPreventiveMaintenanceSchedulePeriodMetadata.mapDelegates == null)
				{
					AssetPreventiveMaintenanceSchedulePeriodMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AssetPreventiveMaintenanceSchedulePeriodMetadata.meta == null)
				{
					AssetPreventiveMaintenanceSchedulePeriodMetadata.meta = new AssetPreventiveMaintenanceSchedulePeriodMetadata();
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
				

				meta.AddTypeMap("AssetID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PeriodYear", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "AssetPreventiveMaintenanceSchedulePeriod";
				meta.Destination = "AssetPreventiveMaintenanceSchedulePeriod";
				
				meta.spInsert = "proc_AssetPreventiveMaintenanceSchedulePeriodInsert";				
				meta.spUpdate = "proc_AssetPreventiveMaintenanceSchedulePeriodUpdate";		
				meta.spDelete = "proc_AssetPreventiveMaintenanceSchedulePeriodDelete";
				meta.spLoadAll = "proc_AssetPreventiveMaintenanceSchedulePeriodLoadAll";
				meta.spLoadByPrimaryKey = "proc_AssetPreventiveMaintenanceSchedulePeriodLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AssetPreventiveMaintenanceSchedulePeriodMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
