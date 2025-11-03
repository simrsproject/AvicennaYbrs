/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 7/15/2016 3:37:04 AM
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
	abstract public class esItemLaboratoryProfileCollection : esEntityCollectionWAuditLog
	{
		public esItemLaboratoryProfileCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ItemLaboratoryProfileCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemLaboratoryProfileQuery query)
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
			this.InitQuery(query as esItemLaboratoryProfileQuery);
		}
		#endregion
		
		virtual public ItemLaboratoryProfile DetachEntity(ItemLaboratoryProfile entity)
		{
			return base.DetachEntity(entity) as ItemLaboratoryProfile;
		}
		
		virtual public ItemLaboratoryProfile AttachEntity(ItemLaboratoryProfile entity)
		{
			return base.AttachEntity(entity) as ItemLaboratoryProfile;
		}
		
		virtual public void Combine(ItemLaboratoryProfileCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ItemLaboratoryProfile this[int index]
		{
			get
			{
				return base[index] as ItemLaboratoryProfile;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemLaboratoryProfile);
		}
	}



	[Serializable]
	abstract public class esItemLaboratoryProfile : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemLaboratoryProfileQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemLaboratoryProfile()
		{

		}

		public esItemLaboratoryProfile(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String parentItemID, System.String detailItemID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(parentItemID, detailItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(parentItemID, detailItemID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String parentItemID, System.String detailItemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(parentItemID, detailItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(parentItemID, detailItemID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String parentItemID, System.String detailItemID)
		{
			esItemLaboratoryProfileQuery query = this.GetDynamicQuery();
			query.Where(query.ParentItemID == parentItemID, query.DetailItemID == detailItemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String parentItemID, System.String detailItemID)
		{
			esParameters parms = new esParameters();
			parms.Add("ParentItemID",parentItemID);			parms.Add("DetailItemID",detailItemID);
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
						case "ParentItemID": this.str.ParentItemID = (string)value; break;							
						case "DetailItemID": this.str.DetailItemID = (string)value; break;							
						case "DisplaySequence": this.str.DisplaySequence = (string)value; break;							
						case "IsDisplayInResult": this.str.IsDisplayInResult = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "DisplaySequence":
						
							if (value == null || value is System.Int32)
								this.DisplaySequence = (System.Int32?)value;
							break;
						
						case "IsDisplayInResult":
						
							if (value == null || value is System.Boolean)
								this.IsDisplayInResult = (System.Boolean?)value;
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
		/// Maps to ItemLaboratoryProfile.ParentItemID
		/// </summary>
		virtual public System.String ParentItemID
		{
			get
			{
				return base.GetSystemString(ItemLaboratoryProfileMetadata.ColumnNames.ParentItemID);
			}
			
			set
			{
				base.SetSystemString(ItemLaboratoryProfileMetadata.ColumnNames.ParentItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemLaboratoryProfile.DetailItemID
		/// </summary>
		virtual public System.String DetailItemID
		{
			get
			{
				return base.GetSystemString(ItemLaboratoryProfileMetadata.ColumnNames.DetailItemID);
			}
			
			set
			{
				base.SetSystemString(ItemLaboratoryProfileMetadata.ColumnNames.DetailItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemLaboratoryProfile.DisplaySequence
		/// </summary>
		virtual public System.Int32? DisplaySequence
		{
			get
			{
				return base.GetSystemInt32(ItemLaboratoryProfileMetadata.ColumnNames.DisplaySequence);
			}
			
			set
			{
				base.SetSystemInt32(ItemLaboratoryProfileMetadata.ColumnNames.DisplaySequence, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemLaboratoryProfile.IsDisplayInResult
		/// </summary>
		virtual public System.Boolean? IsDisplayInResult
		{
			get
			{
				return base.GetSystemBoolean(ItemLaboratoryProfileMetadata.ColumnNames.IsDisplayInResult);
			}
			
			set
			{
				base.SetSystemBoolean(ItemLaboratoryProfileMetadata.ColumnNames.IsDisplayInResult, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemLaboratoryProfile.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemLaboratoryProfileMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ItemLaboratoryProfileMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemLaboratoryProfile.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemLaboratoryProfileMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemLaboratoryProfileMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esItemLaboratoryProfile entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ParentItemID
			{
				get
				{
					System.String data = entity.ParentItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParentItemID = null;
					else entity.ParentItemID = Convert.ToString(value);
				}
			}
				
			public System.String DetailItemID
			{
				get
				{
					System.String data = entity.DetailItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DetailItemID = null;
					else entity.DetailItemID = Convert.ToString(value);
				}
			}
				
			public System.String DisplaySequence
			{
				get
				{
					System.Int32? data = entity.DisplaySequence;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DisplaySequence = null;
					else entity.DisplaySequence = Convert.ToInt32(value);
				}
			}
				
			public System.String IsDisplayInResult
			{
				get
				{
					System.Boolean? data = entity.IsDisplayInResult;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDisplayInResult = null;
					else entity.IsDisplayInResult = Convert.ToBoolean(value);
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
			

			private esItemLaboratoryProfile entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemLaboratoryProfileQuery query)
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
				throw new Exception("esItemLaboratoryProfile can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ItemLaboratoryProfile : esItemLaboratoryProfile
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
	abstract public class esItemLaboratoryProfileQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ItemLaboratoryProfileMetadata.Meta();
			}
		}	
		

		public esQueryItem ParentItemID
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryProfileMetadata.ColumnNames.ParentItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem DetailItemID
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryProfileMetadata.ColumnNames.DetailItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem DisplaySequence
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryProfileMetadata.ColumnNames.DisplaySequence, esSystemType.Int32);
			}
		} 
		
		public esQueryItem IsDisplayInResult
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryProfileMetadata.ColumnNames.IsDisplayInResult, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryProfileMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryProfileMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemLaboratoryProfileCollection")]
	public partial class ItemLaboratoryProfileCollection : esItemLaboratoryProfileCollection, IEnumerable<ItemLaboratoryProfile>
	{
		public ItemLaboratoryProfileCollection()
		{

		}
		
		public static implicit operator List<ItemLaboratoryProfile>(ItemLaboratoryProfileCollection coll)
		{
			List<ItemLaboratoryProfile> list = new List<ItemLaboratoryProfile>();
			
			foreach (ItemLaboratoryProfile emp in coll)
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
				return  ItemLaboratoryProfileMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemLaboratoryProfileQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemLaboratoryProfile(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemLaboratoryProfile();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ItemLaboratoryProfileQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemLaboratoryProfileQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ItemLaboratoryProfileQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ItemLaboratoryProfile AddNew()
		{
			ItemLaboratoryProfile entity = base.AddNewEntity() as ItemLaboratoryProfile;
			
			return entity;
		}

		public ItemLaboratoryProfile FindByPrimaryKey(System.String parentItemID, System.String detailItemID)
		{
			return base.FindByPrimaryKey(parentItemID, detailItemID) as ItemLaboratoryProfile;
		}


		#region IEnumerable<ItemLaboratoryProfile> Members

		IEnumerator<ItemLaboratoryProfile> IEnumerable<ItemLaboratoryProfile>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ItemLaboratoryProfile;
			}
		}

		#endregion
		
		private ItemLaboratoryProfileQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemLaboratoryProfile' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ItemLaboratoryProfile ({ParentItemID},{DetailItemID})")]
	[Serializable]
	public partial class ItemLaboratoryProfile : esItemLaboratoryProfile
	{
		public ItemLaboratoryProfile()
		{

		}
	
		public ItemLaboratoryProfile(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemLaboratoryProfileMetadata.Meta();
			}
		}
		
		
		
		override protected esItemLaboratoryProfileQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemLaboratoryProfileQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ItemLaboratoryProfileQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemLaboratoryProfileQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ItemLaboratoryProfileQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ItemLaboratoryProfileQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ItemLaboratoryProfileQuery : esItemLaboratoryProfileQuery
	{
		public ItemLaboratoryProfileQuery()
		{

		}		
		
		public ItemLaboratoryProfileQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ItemLaboratoryProfileQuery";
        }
		
			
	}


	[Serializable]
	public partial class ItemLaboratoryProfileMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemLaboratoryProfileMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemLaboratoryProfileMetadata.ColumnNames.ParentItemID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemLaboratoryProfileMetadata.PropertyNames.ParentItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemLaboratoryProfileMetadata.ColumnNames.DetailItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemLaboratoryProfileMetadata.PropertyNames.DetailItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemLaboratoryProfileMetadata.ColumnNames.DisplaySequence, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ItemLaboratoryProfileMetadata.PropertyNames.DisplaySequence;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemLaboratoryProfileMetadata.ColumnNames.IsDisplayInResult, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemLaboratoryProfileMetadata.PropertyNames.IsDisplayInResult;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemLaboratoryProfileMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemLaboratoryProfileMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemLaboratoryProfileMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemLaboratoryProfileMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ItemLaboratoryProfileMetadata Meta()
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
			 public const string ParentItemID = "ParentItemID";
			 public const string DetailItemID = "DetailItemID";
			 public const string DisplaySequence = "DisplaySequence";
			 public const string IsDisplayInResult = "IsDisplayInResult";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ParentItemID = "ParentItemID";
			 public const string DetailItemID = "DetailItemID";
			 public const string DisplaySequence = "DisplaySequence";
			 public const string IsDisplayInResult = "IsDisplayInResult";
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
			lock (typeof(ItemLaboratoryProfileMetadata))
			{
				if(ItemLaboratoryProfileMetadata.mapDelegates == null)
				{
					ItemLaboratoryProfileMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ItemLaboratoryProfileMetadata.meta == null)
				{
					ItemLaboratoryProfileMetadata.meta = new ItemLaboratoryProfileMetadata();
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
				

				meta.AddTypeMap("ParentItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DetailItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DisplaySequence", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsDisplayInResult", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ItemLaboratoryProfile";
				meta.Destination = "ItemLaboratoryProfile";
				
				meta.spInsert = "proc_ItemLaboratoryProfileInsert";				
				meta.spUpdate = "proc_ItemLaboratoryProfileUpdate";		
				meta.spDelete = "proc_ItemLaboratoryProfileDelete";
				meta.spLoadAll = "proc_ItemLaboratoryProfileLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemLaboratoryProfileLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemLaboratoryProfileMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
