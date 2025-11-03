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
	abstract public class esAssetUtilizationCollection : esEntityCollectionWAuditLog
	{
		public esAssetUtilizationCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "AssetUtilizationCollection";
		}

		#region Query Logic
		protected void InitQuery(esAssetUtilizationQuery query)
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
			this.InitQuery(query as esAssetUtilizationQuery);
		}
		#endregion
		
		virtual public AssetUtilization DetachEntity(AssetUtilization entity)
		{
			return base.DetachEntity(entity) as AssetUtilization;
		}
		
		virtual public AssetUtilization AttachEntity(AssetUtilization entity)
		{
			return base.AttachEntity(entity) as AssetUtilization;
		}
		
		virtual public void Combine(AssetUtilizationCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AssetUtilization this[int index]
		{
			get
			{
				return base[index] as AssetUtilization;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AssetUtilization);
		}
	}



	[Serializable]
	abstract public class esAssetUtilization : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAssetUtilizationQuery GetDynamicQuery()
		{
			return null;
		}

		public esAssetUtilization()
		{

		}

		public esAssetUtilization(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String assetID, System.String periodNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(assetID, periodNo);
			else
				return LoadByPrimaryKeyStoredProcedure(assetID, periodNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String assetID, System.String periodNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(assetID, periodNo);
			else
				return LoadByPrimaryKeyStoredProcedure(assetID, periodNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String assetID, System.String periodNo)
		{
			esAssetUtilizationQuery query = this.GetDynamicQuery();
			query.Where(query.AssetID == assetID, query.PeriodNo == periodNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String assetID, System.String periodNo)
		{
			esParameters parms = new esParameters();
			parms.Add("AssetID",assetID);			parms.Add("PeriodNo",periodNo);
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
						case "PeriodNo": this.str.PeriodNo = (string)value; break;							
						case "UsageCounter": this.str.UsageCounter = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "UsageCounter":
						
							if (value == null || value is System.Int16)
								this.UsageCounter = (System.Int16?)value;
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
		/// Maps to AssetUtilization.AssetID
		/// </summary>
		virtual public System.String AssetID
		{
			get
			{
				return base.GetSystemString(AssetUtilizationMetadata.ColumnNames.AssetID);
			}
			
			set
			{
				base.SetSystemString(AssetUtilizationMetadata.ColumnNames.AssetID, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetUtilization.PeriodNo
		/// </summary>
		virtual public System.String PeriodNo
		{
			get
			{
				return base.GetSystemString(AssetUtilizationMetadata.ColumnNames.PeriodNo);
			}
			
			set
			{
				base.SetSystemString(AssetUtilizationMetadata.ColumnNames.PeriodNo, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetUtilization.UsageCounter
		/// </summary>
		virtual public System.Int16? UsageCounter
		{
			get
			{
				return base.GetSystemInt16(AssetUtilizationMetadata.ColumnNames.UsageCounter);
			}
			
			set
			{
				base.SetSystemInt16(AssetUtilizationMetadata.ColumnNames.UsageCounter, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetUtilization.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetUtilizationMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AssetUtilizationMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetUtilization.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AssetUtilizationMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AssetUtilizationMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esAssetUtilization entity)
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
				
			public System.String PeriodNo
			{
				get
				{
					System.String data = entity.PeriodNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodNo = null;
					else entity.PeriodNo = Convert.ToString(value);
				}
			}
				
			public System.String UsageCounter
			{
				get
				{
					System.Int16? data = entity.UsageCounter;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UsageCounter = null;
					else entity.UsageCounter = Convert.ToInt16(value);
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
			

			private esAssetUtilization entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAssetUtilizationQuery query)
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
				throw new Exception("esAssetUtilization can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class AssetUtilization : esAssetUtilization
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
	abstract public class esAssetUtilizationQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AssetUtilizationMetadata.Meta();
			}
		}	
		

		public esQueryItem AssetID
		{
			get
			{
				return new esQueryItem(this, AssetUtilizationMetadata.ColumnNames.AssetID, esSystemType.String);
			}
		} 
		
		public esQueryItem PeriodNo
		{
			get
			{
				return new esQueryItem(this, AssetUtilizationMetadata.ColumnNames.PeriodNo, esSystemType.String);
			}
		} 
		
		public esQueryItem UsageCounter
		{
			get
			{
				return new esQueryItem(this, AssetUtilizationMetadata.ColumnNames.UsageCounter, esSystemType.Int16);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AssetUtilizationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AssetUtilizationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AssetUtilizationCollection")]
	public partial class AssetUtilizationCollection : esAssetUtilizationCollection, IEnumerable<AssetUtilization>
	{
		public AssetUtilizationCollection()
		{

		}
		
		public static implicit operator List<AssetUtilization>(AssetUtilizationCollection coll)
		{
			List<AssetUtilization> list = new List<AssetUtilization>();
			
			foreach (AssetUtilization emp in coll)
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
				return  AssetUtilizationMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetUtilizationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AssetUtilization(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AssetUtilization();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public AssetUtilizationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetUtilizationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(AssetUtilizationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public AssetUtilization AddNew()
		{
			AssetUtilization entity = base.AddNewEntity() as AssetUtilization;
			
			return entity;
		}

		public AssetUtilization FindByPrimaryKey(System.String assetID, System.String periodNo)
		{
			return base.FindByPrimaryKey(assetID, periodNo) as AssetUtilization;
		}


		#region IEnumerable<AssetUtilization> Members

		IEnumerator<AssetUtilization> IEnumerable<AssetUtilization>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AssetUtilization;
			}
		}

		#endregion
		
		private AssetUtilizationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AssetUtilization' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("AssetUtilization ({AssetID},{PeriodNo})")]
	[Serializable]
	public partial class AssetUtilization : esAssetUtilization
	{
		public AssetUtilization()
		{

		}
	
		public AssetUtilization(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AssetUtilizationMetadata.Meta();
			}
		}
		
		
		
		override protected esAssetUtilizationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetUtilizationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public AssetUtilizationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetUtilizationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(AssetUtilizationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private AssetUtilizationQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class AssetUtilizationQuery : esAssetUtilizationQuery
	{
		public AssetUtilizationQuery()
		{

		}		
		
		public AssetUtilizationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "AssetUtilizationQuery";
        }
		
			
	}


	[Serializable]
	public partial class AssetUtilizationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AssetUtilizationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AssetUtilizationMetadata.ColumnNames.AssetID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetUtilizationMetadata.PropertyNames.AssetID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 30;
			c.HasDefault = true;
			c.Default = @"('00:00')";
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetUtilizationMetadata.ColumnNames.PeriodNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetUtilizationMetadata.PropertyNames.PeriodNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 6;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetUtilizationMetadata.ColumnNames.UsageCounter, 2, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = AssetUtilizationMetadata.PropertyNames.UsageCounter;
			c.NumericPrecision = 5;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetUtilizationMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetUtilizationMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetUtilizationMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetUtilizationMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public AssetUtilizationMetadata Meta()
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
			 public const string PeriodNo = "PeriodNo";
			 public const string UsageCounter = "UsageCounter";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string AssetID = "AssetID";
			 public const string PeriodNo = "PeriodNo";
			 public const string UsageCounter = "UsageCounter";
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
			lock (typeof(AssetUtilizationMetadata))
			{
				if(AssetUtilizationMetadata.mapDelegates == null)
				{
					AssetUtilizationMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AssetUtilizationMetadata.meta == null)
				{
					AssetUtilizationMetadata.meta = new AssetUtilizationMetadata();
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
				meta.AddTypeMap("PeriodNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("UsageCounter", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "AssetUtilization";
				meta.Destination = "AssetUtilization";
				
				meta.spInsert = "proc_AssetUtilizationInsert";				
				meta.spUpdate = "proc_AssetUtilizationUpdate";		
				meta.spDelete = "proc_AssetUtilizationDelete";
				meta.spLoadAll = "proc_AssetUtilizationLoadAll";
				meta.spLoadByPrimaryKey = "proc_AssetUtilizationLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AssetUtilizationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
