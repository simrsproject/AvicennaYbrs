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
	abstract public class esAssetPreventiveMaintenanceSchedulePeriodDateCollection : esEntityCollectionWAuditLog
	{
		public esAssetPreventiveMaintenanceSchedulePeriodDateCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "AssetPreventiveMaintenanceSchedulePeriodDateCollection";
		}

		#region Query Logic
		protected void InitQuery(esAssetPreventiveMaintenanceSchedulePeriodDateQuery query)
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
			this.InitQuery(query as esAssetPreventiveMaintenanceSchedulePeriodDateQuery);
		}
		#endregion
		
		virtual public AssetPreventiveMaintenanceSchedulePeriodDate DetachEntity(AssetPreventiveMaintenanceSchedulePeriodDate entity)
		{
			return base.DetachEntity(entity) as AssetPreventiveMaintenanceSchedulePeriodDate;
		}
		
		virtual public AssetPreventiveMaintenanceSchedulePeriodDate AttachEntity(AssetPreventiveMaintenanceSchedulePeriodDate entity)
		{
			return base.AttachEntity(entity) as AssetPreventiveMaintenanceSchedulePeriodDate;
		}
		
		virtual public void Combine(AssetPreventiveMaintenanceSchedulePeriodDateCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AssetPreventiveMaintenanceSchedulePeriodDate this[int index]
		{
			get
			{
				return base[index] as AssetPreventiveMaintenanceSchedulePeriodDate;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AssetPreventiveMaintenanceSchedulePeriodDate);
		}
	}



	[Serializable]
	abstract public class esAssetPreventiveMaintenanceSchedulePeriodDate : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAssetPreventiveMaintenanceSchedulePeriodDateQuery GetDynamicQuery()
		{
			return null;
		}

		public esAssetPreventiveMaintenanceSchedulePeriodDate()
		{

		}

		public esAssetPreventiveMaintenanceSchedulePeriodDate(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String assetID, System.String periodYear, System.DateTime periodDate)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(assetID, periodYear, periodDate);
			else
				return LoadByPrimaryKeyStoredProcedure(assetID, periodYear, periodDate);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String assetID, System.String periodYear, System.DateTime periodDate)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(assetID, periodYear, periodDate);
			else
				return LoadByPrimaryKeyStoredProcedure(assetID, periodYear, periodDate);
		}

		private bool LoadByPrimaryKeyDynamic(System.String assetID, System.String periodYear, System.DateTime periodDate)
		{
			esAssetPreventiveMaintenanceSchedulePeriodDateQuery query = this.GetDynamicQuery();
			query.Where(query.AssetID == assetID, query.PeriodYear == periodYear, query.PeriodDate == periodDate);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String assetID, System.String periodYear, System.DateTime periodDate)
		{
			esParameters parms = new esParameters();
			parms.Add("AssetID",assetID);			parms.Add("PeriodYear",periodYear);			parms.Add("PeriodDate",periodDate);
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
						case "PeriodDate": this.str.PeriodDate = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PeriodDate":
						
							if (value == null || value is System.DateTime)
								this.PeriodDate = (System.DateTime?)value;
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
		/// Maps to AssetPreventiveMaintenanceSchedulePeriodDate.AssetID
		/// </summary>
		virtual public System.String AssetID
		{
			get
			{
				return base.GetSystemString(AssetPreventiveMaintenanceSchedulePeriodDateMetadata.ColumnNames.AssetID);
			}
			
			set
			{
				base.SetSystemString(AssetPreventiveMaintenanceSchedulePeriodDateMetadata.ColumnNames.AssetID, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetPreventiveMaintenanceSchedulePeriodDate.PeriodYear
		/// </summary>
		virtual public System.String PeriodYear
		{
			get
			{
				return base.GetSystemString(AssetPreventiveMaintenanceSchedulePeriodDateMetadata.ColumnNames.PeriodYear);
			}
			
			set
			{
				base.SetSystemString(AssetPreventiveMaintenanceSchedulePeriodDateMetadata.ColumnNames.PeriodYear, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetPreventiveMaintenanceSchedulePeriodDate.PeriodDate
		/// </summary>
		virtual public System.DateTime? PeriodDate
		{
			get
			{
				return base.GetSystemDateTime(AssetPreventiveMaintenanceSchedulePeriodDateMetadata.ColumnNames.PeriodDate);
			}
			
			set
			{
				base.SetSystemDateTime(AssetPreventiveMaintenanceSchedulePeriodDateMetadata.ColumnNames.PeriodDate, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetPreventiveMaintenanceSchedulePeriodDate.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetPreventiveMaintenanceSchedulePeriodDateMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AssetPreventiveMaintenanceSchedulePeriodDateMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetPreventiveMaintenanceSchedulePeriodDate.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AssetPreventiveMaintenanceSchedulePeriodDateMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AssetPreventiveMaintenanceSchedulePeriodDateMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esAssetPreventiveMaintenanceSchedulePeriodDate entity)
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
				
			public System.String PeriodDate
			{
				get
				{
					System.DateTime? data = entity.PeriodDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodDate = null;
					else entity.PeriodDate = Convert.ToDateTime(value);
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
			

			private esAssetPreventiveMaintenanceSchedulePeriodDate entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAssetPreventiveMaintenanceSchedulePeriodDateQuery query)
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
				throw new Exception("esAssetPreventiveMaintenanceSchedulePeriodDate can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class AssetPreventiveMaintenanceSchedulePeriodDate : esAssetPreventiveMaintenanceSchedulePeriodDate
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
	abstract public class esAssetPreventiveMaintenanceSchedulePeriodDateQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AssetPreventiveMaintenanceSchedulePeriodDateMetadata.Meta();
			}
		}	
		

		public esQueryItem AssetID
		{
			get
			{
				return new esQueryItem(this, AssetPreventiveMaintenanceSchedulePeriodDateMetadata.ColumnNames.AssetID, esSystemType.String);
			}
		} 
		
		public esQueryItem PeriodYear
		{
			get
			{
				return new esQueryItem(this, AssetPreventiveMaintenanceSchedulePeriodDateMetadata.ColumnNames.PeriodYear, esSystemType.String);
			}
		} 
		
		public esQueryItem PeriodDate
		{
			get
			{
				return new esQueryItem(this, AssetPreventiveMaintenanceSchedulePeriodDateMetadata.ColumnNames.PeriodDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AssetPreventiveMaintenanceSchedulePeriodDateMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AssetPreventiveMaintenanceSchedulePeriodDateMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AssetPreventiveMaintenanceSchedulePeriodDateCollection")]
	public partial class AssetPreventiveMaintenanceSchedulePeriodDateCollection : esAssetPreventiveMaintenanceSchedulePeriodDateCollection, IEnumerable<AssetPreventiveMaintenanceSchedulePeriodDate>
	{
		public AssetPreventiveMaintenanceSchedulePeriodDateCollection()
		{

		}
		
		public static implicit operator List<AssetPreventiveMaintenanceSchedulePeriodDate>(AssetPreventiveMaintenanceSchedulePeriodDateCollection coll)
		{
			List<AssetPreventiveMaintenanceSchedulePeriodDate> list = new List<AssetPreventiveMaintenanceSchedulePeriodDate>();
			
			foreach (AssetPreventiveMaintenanceSchedulePeriodDate emp in coll)
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
				return  AssetPreventiveMaintenanceSchedulePeriodDateMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetPreventiveMaintenanceSchedulePeriodDateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AssetPreventiveMaintenanceSchedulePeriodDate(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AssetPreventiveMaintenanceSchedulePeriodDate();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public AssetPreventiveMaintenanceSchedulePeriodDateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetPreventiveMaintenanceSchedulePeriodDateQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(AssetPreventiveMaintenanceSchedulePeriodDateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public AssetPreventiveMaintenanceSchedulePeriodDate AddNew()
		{
			AssetPreventiveMaintenanceSchedulePeriodDate entity = base.AddNewEntity() as AssetPreventiveMaintenanceSchedulePeriodDate;
			
			return entity;
		}

		public AssetPreventiveMaintenanceSchedulePeriodDate FindByPrimaryKey(System.String assetID, System.String periodYear, System.DateTime periodDate)
		{
			return base.FindByPrimaryKey(assetID, periodYear, periodDate) as AssetPreventiveMaintenanceSchedulePeriodDate;
		}


		#region IEnumerable<AssetPreventiveMaintenanceSchedulePeriodDate> Members

		IEnumerator<AssetPreventiveMaintenanceSchedulePeriodDate> IEnumerable<AssetPreventiveMaintenanceSchedulePeriodDate>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AssetPreventiveMaintenanceSchedulePeriodDate;
			}
		}

		#endregion
		
		private AssetPreventiveMaintenanceSchedulePeriodDateQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AssetPreventiveMaintenanceSchedulePeriodDate' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("AssetPreventiveMaintenanceSchedulePeriodDate ({AssetID},{PeriodYear},{PeriodDate})")]
	[Serializable]
	public partial class AssetPreventiveMaintenanceSchedulePeriodDate : esAssetPreventiveMaintenanceSchedulePeriodDate
	{
		public AssetPreventiveMaintenanceSchedulePeriodDate()
		{

		}
	
		public AssetPreventiveMaintenanceSchedulePeriodDate(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AssetPreventiveMaintenanceSchedulePeriodDateMetadata.Meta();
			}
		}
		
		
		
		override protected esAssetPreventiveMaintenanceSchedulePeriodDateQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetPreventiveMaintenanceSchedulePeriodDateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public AssetPreventiveMaintenanceSchedulePeriodDateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetPreventiveMaintenanceSchedulePeriodDateQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(AssetPreventiveMaintenanceSchedulePeriodDateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private AssetPreventiveMaintenanceSchedulePeriodDateQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class AssetPreventiveMaintenanceSchedulePeriodDateQuery : esAssetPreventiveMaintenanceSchedulePeriodDateQuery
	{
		public AssetPreventiveMaintenanceSchedulePeriodDateQuery()
		{

		}		
		
		public AssetPreventiveMaintenanceSchedulePeriodDateQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "AssetPreventiveMaintenanceSchedulePeriodDateQuery";
        }
		
			
	}


	[Serializable]
	public partial class AssetPreventiveMaintenanceSchedulePeriodDateMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AssetPreventiveMaintenanceSchedulePeriodDateMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AssetPreventiveMaintenanceSchedulePeriodDateMetadata.ColumnNames.AssetID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetPreventiveMaintenanceSchedulePeriodDateMetadata.PropertyNames.AssetID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 30;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetPreventiveMaintenanceSchedulePeriodDateMetadata.ColumnNames.PeriodYear, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetPreventiveMaintenanceSchedulePeriodDateMetadata.PropertyNames.PeriodYear;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 4;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetPreventiveMaintenanceSchedulePeriodDateMetadata.ColumnNames.PeriodDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetPreventiveMaintenanceSchedulePeriodDateMetadata.PropertyNames.PeriodDate;
			c.IsInPrimaryKey = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetPreventiveMaintenanceSchedulePeriodDateMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetPreventiveMaintenanceSchedulePeriodDateMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetPreventiveMaintenanceSchedulePeriodDateMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetPreventiveMaintenanceSchedulePeriodDateMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public AssetPreventiveMaintenanceSchedulePeriodDateMetadata Meta()
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
			 public const string PeriodDate = "PeriodDate";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string AssetID = "AssetID";
			 public const string PeriodYear = "PeriodYear";
			 public const string PeriodDate = "PeriodDate";
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
			lock (typeof(AssetPreventiveMaintenanceSchedulePeriodDateMetadata))
			{
				if(AssetPreventiveMaintenanceSchedulePeriodDateMetadata.mapDelegates == null)
				{
					AssetPreventiveMaintenanceSchedulePeriodDateMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AssetPreventiveMaintenanceSchedulePeriodDateMetadata.meta == null)
				{
					AssetPreventiveMaintenanceSchedulePeriodDateMetadata.meta = new AssetPreventiveMaintenanceSchedulePeriodDateMetadata();
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
				meta.AddTypeMap("PeriodDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "AssetPreventiveMaintenanceSchedulePeriodDate";
				meta.Destination = "AssetPreventiveMaintenanceSchedulePeriodDate";
				
				meta.spInsert = "proc_AssetPreventiveMaintenanceSchedulePeriodDateInsert";				
				meta.spUpdate = "proc_AssetPreventiveMaintenanceSchedulePeriodDateUpdate";		
				meta.spDelete = "proc_AssetPreventiveMaintenanceSchedulePeriodDateDelete";
				meta.spLoadAll = "proc_AssetPreventiveMaintenanceSchedulePeriodDateLoadAll";
				meta.spLoadByPrimaryKey = "proc_AssetPreventiveMaintenanceSchedulePeriodDateLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AssetPreventiveMaintenanceSchedulePeriodDateMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
