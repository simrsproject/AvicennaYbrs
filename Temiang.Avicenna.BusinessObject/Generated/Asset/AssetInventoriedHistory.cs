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
	abstract public class esAssetInventoriedHistoryCollection : esEntityCollectionWAuditLog
	{
		public esAssetInventoriedHistoryCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "AssetInventoriedHistoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esAssetInventoriedHistoryQuery query)
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
			this.InitQuery(query as esAssetInventoriedHistoryQuery);
		}
		#endregion
		
		virtual public AssetInventoriedHistory DetachEntity(AssetInventoriedHistory entity)
		{
			return base.DetachEntity(entity) as AssetInventoriedHistory;
		}
		
		virtual public AssetInventoriedHistory AttachEntity(AssetInventoriedHistory entity)
		{
			return base.AttachEntity(entity) as AssetInventoriedHistory;
		}
		
		virtual public void Combine(AssetInventoriedHistoryCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AssetInventoriedHistory this[int index]
		{
			get
			{
				return base[index] as AssetInventoriedHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AssetInventoriedHistory);
		}
	}



	[Serializable]
	abstract public class esAssetInventoriedHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAssetInventoriedHistoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esAssetInventoriedHistory()
		{

		}

		public esAssetInventoriedHistory(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String assetID, System.String sequenceNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(assetID, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(assetID, sequenceNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String assetID, System.String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(assetID, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(assetID, sequenceNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String assetID, System.String sequenceNo)
		{
			esAssetInventoriedHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.AssetID == assetID, query.SequenceNo == sequenceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String assetID, System.String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("AssetID",assetID);			parms.Add("SequenceNo",sequenceNo);
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
						case "SequenceNo": this.str.SequenceNo = (string)value; break;							
						case "InventoriedDate": this.str.InventoriedDate = (string)value; break;							
						case "InventoriedBy": this.str.InventoriedBy = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "InventoriedDate":
						
							if (value == null || value is System.DateTime)
								this.InventoriedDate = (System.DateTime?)value;
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
		/// Maps to AssetInventoriedHistory.AssetID
		/// </summary>
		virtual public System.String AssetID
		{
			get
			{
				return base.GetSystemString(AssetInventoriedHistoryMetadata.ColumnNames.AssetID);
			}
			
			set
			{
				base.SetSystemString(AssetInventoriedHistoryMetadata.ColumnNames.AssetID, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetInventoriedHistory.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(AssetInventoriedHistoryMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(AssetInventoriedHistoryMetadata.ColumnNames.SequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetInventoriedHistory.InventoriedDate
		/// </summary>
		virtual public System.DateTime? InventoriedDate
		{
			get
			{
				return base.GetSystemDateTime(AssetInventoriedHistoryMetadata.ColumnNames.InventoriedDate);
			}
			
			set
			{
				base.SetSystemDateTime(AssetInventoriedHistoryMetadata.ColumnNames.InventoriedDate, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetInventoriedHistory.InventoriedBy
		/// </summary>
		virtual public System.String InventoriedBy
		{
			get
			{
				return base.GetSystemString(AssetInventoriedHistoryMetadata.ColumnNames.InventoriedBy);
			}
			
			set
			{
				base.SetSystemString(AssetInventoriedHistoryMetadata.ColumnNames.InventoriedBy, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetInventoriedHistory.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(AssetInventoriedHistoryMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(AssetInventoriedHistoryMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetInventoriedHistory.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetInventoriedHistoryMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AssetInventoriedHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetInventoriedHistory.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AssetInventoriedHistoryMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AssetInventoriedHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esAssetInventoriedHistory entity)
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
				
			public System.String SequenceNo
			{
				get
				{
					System.String data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToString(value);
				}
			}
				
			public System.String InventoriedDate
			{
				get
				{
					System.DateTime? data = entity.InventoriedDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InventoriedDate = null;
					else entity.InventoriedDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String InventoriedBy
			{
				get
				{
					System.String data = entity.InventoriedBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InventoriedBy = null;
					else entity.InventoriedBy = Convert.ToString(value);
				}
			}
				
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
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
			

			private esAssetInventoriedHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAssetInventoriedHistoryQuery query)
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
				throw new Exception("esAssetInventoriedHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class AssetInventoriedHistory : esAssetInventoriedHistory
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
	abstract public class esAssetInventoriedHistoryQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AssetInventoriedHistoryMetadata.Meta();
			}
		}	
		

		public esQueryItem AssetID
		{
			get
			{
				return new esQueryItem(this, AssetInventoriedHistoryMetadata.ColumnNames.AssetID, esSystemType.String);
			}
		} 
		
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, AssetInventoriedHistoryMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem InventoriedDate
		{
			get
			{
				return new esQueryItem(this, AssetInventoriedHistoryMetadata.ColumnNames.InventoriedDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem InventoriedBy
		{
			get
			{
				return new esQueryItem(this, AssetInventoriedHistoryMetadata.ColumnNames.InventoriedBy, esSystemType.String);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, AssetInventoriedHistoryMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AssetInventoriedHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AssetInventoriedHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AssetInventoriedHistoryCollection")]
	public partial class AssetInventoriedHistoryCollection : esAssetInventoriedHistoryCollection, IEnumerable<AssetInventoriedHistory>
	{
		public AssetInventoriedHistoryCollection()
		{

		}
		
		public static implicit operator List<AssetInventoriedHistory>(AssetInventoriedHistoryCollection coll)
		{
			List<AssetInventoriedHistory> list = new List<AssetInventoriedHistory>();
			
			foreach (AssetInventoriedHistory emp in coll)
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
				return  AssetInventoriedHistoryMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetInventoriedHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AssetInventoriedHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AssetInventoriedHistory();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public AssetInventoriedHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetInventoriedHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(AssetInventoriedHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public AssetInventoriedHistory AddNew()
		{
			AssetInventoriedHistory entity = base.AddNewEntity() as AssetInventoriedHistory;
			
			return entity;
		}

		public AssetInventoriedHistory FindByPrimaryKey(System.String assetID, System.String sequenceNo)
		{
			return base.FindByPrimaryKey(assetID, sequenceNo) as AssetInventoriedHistory;
		}


		#region IEnumerable<AssetInventoriedHistory> Members

		IEnumerator<AssetInventoriedHistory> IEnumerable<AssetInventoriedHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AssetInventoriedHistory;
			}
		}

		#endregion
		
		private AssetInventoriedHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AssetInventoriedHistory' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("AssetInventoriedHistory ({AssetID},{SequenceNo})")]
	[Serializable]
	public partial class AssetInventoriedHistory : esAssetInventoriedHistory
	{
		public AssetInventoriedHistory()
		{

		}
	
		public AssetInventoriedHistory(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AssetInventoriedHistoryMetadata.Meta();
			}
		}
		
		
		
		override protected esAssetInventoriedHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetInventoriedHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public AssetInventoriedHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetInventoriedHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(AssetInventoriedHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private AssetInventoriedHistoryQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class AssetInventoriedHistoryQuery : esAssetInventoriedHistoryQuery
	{
		public AssetInventoriedHistoryQuery()
		{

		}		
		
		public AssetInventoriedHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "AssetInventoriedHistoryQuery";
        }
		
			
	}


	[Serializable]
	public partial class AssetInventoriedHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AssetInventoriedHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AssetInventoriedHistoryMetadata.ColumnNames.AssetID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetInventoriedHistoryMetadata.PropertyNames.AssetID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 30;
			c.HasDefault = true;
			c.Default = @"('00:00')";
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetInventoriedHistoryMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetInventoriedHistoryMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			c.HasDefault = true;
			c.Default = @"('000')";
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetInventoriedHistoryMetadata.ColumnNames.InventoriedDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetInventoriedHistoryMetadata.PropertyNames.InventoriedDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetInventoriedHistoryMetadata.ColumnNames.InventoriedBy, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetInventoriedHistoryMetadata.PropertyNames.InventoriedBy;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetInventoriedHistoryMetadata.ColumnNames.Notes, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetInventoriedHistoryMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetInventoriedHistoryMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetInventoriedHistoryMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetInventoriedHistoryMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetInventoriedHistoryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public AssetInventoriedHistoryMetadata Meta()
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
			 public const string SequenceNo = "SequenceNo";
			 public const string InventoriedDate = "InventoriedDate";
			 public const string InventoriedBy = "InventoriedBy";
			 public const string Notes = "Notes";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string AssetID = "AssetID";
			 public const string SequenceNo = "SequenceNo";
			 public const string InventoriedDate = "InventoriedDate";
			 public const string InventoriedBy = "InventoriedBy";
			 public const string Notes = "Notes";
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
			lock (typeof(AssetInventoriedHistoryMetadata))
			{
				if(AssetInventoriedHistoryMetadata.mapDelegates == null)
				{
					AssetInventoriedHistoryMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AssetInventoriedHistoryMetadata.meta == null)
				{
					AssetInventoriedHistoryMetadata.meta = new AssetInventoriedHistoryMetadata();
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
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InventoriedDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("InventoriedBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "AssetInventoriedHistory";
				meta.Destination = "AssetInventoriedHistory";
				
				meta.spInsert = "proc_AssetInventoriedHistoryInsert";				
				meta.spUpdate = "proc_AssetInventoriedHistoryUpdate";		
				meta.spDelete = "proc_AssetInventoriedHistoryDelete";
				meta.spLoadAll = "proc_AssetInventoriedHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_AssetInventoriedHistoryLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AssetInventoriedHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
