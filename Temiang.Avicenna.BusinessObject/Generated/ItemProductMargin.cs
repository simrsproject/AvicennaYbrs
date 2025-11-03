/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:18 PM
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
	abstract public class esItemProductMarginCollection : esEntityCollectionWAuditLog
	{
		public esItemProductMarginCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ItemProductMarginCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemProductMarginQuery query)
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
			this.InitQuery(query as esItemProductMarginQuery);
		}
		#endregion
		
		virtual public ItemProductMargin DetachEntity(ItemProductMargin entity)
		{
			return base.DetachEntity(entity) as ItemProductMargin;
		}
		
		virtual public ItemProductMargin AttachEntity(ItemProductMargin entity)
		{
			return base.AttachEntity(entity) as ItemProductMargin;
		}
		
		virtual public void Combine(ItemProductMarginCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ItemProductMargin this[int index]
		{
			get
			{
				return base[index] as ItemProductMargin;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemProductMargin);
		}
	}



	[Serializable]
	abstract public class esItemProductMargin : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemProductMarginQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemProductMargin()
		{

		}

		public esItemProductMargin(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String marginID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(marginID);
			else
				return LoadByPrimaryKeyStoredProcedure(marginID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String marginID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(marginID);
			else
				return LoadByPrimaryKeyStoredProcedure(marginID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String marginID)
		{
			esItemProductMarginQuery query = this.GetDynamicQuery();
			query.Where(query.MarginID == marginID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String marginID)
		{
			esParameters parms = new esParameters();
			parms.Add("MarginID",marginID);
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
						case "MarginID": this.str.MarginID = (string)value; break;							
						case "MarginName": this.str.MarginName = (string)value; break;							
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
		/// Maps to ItemProductMargin.MarginID
		/// </summary>
		virtual public System.String MarginID
		{
			get
			{
				return base.GetSystemString(ItemProductMarginMetadata.ColumnNames.MarginID);
			}
			
			set
			{
				base.SetSystemString(ItemProductMarginMetadata.ColumnNames.MarginID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemProductMargin.MarginName
		/// </summary>
		virtual public System.String MarginName
		{
			get
			{
				return base.GetSystemString(ItemProductMarginMetadata.ColumnNames.MarginName);
			}
			
			set
			{
				base.SetSystemString(ItemProductMarginMetadata.ColumnNames.MarginName, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemProductMargin.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMarginMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(ItemProductMarginMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemProductMargin.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemProductMarginMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ItemProductMarginMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemProductMargin.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemProductMarginMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemProductMarginMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esItemProductMargin entity)
			{
				this.entity = entity;
			}
			
	
			public System.String MarginID
			{
				get
				{
					System.String data = entity.MarginID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MarginID = null;
					else entity.MarginID = Convert.ToString(value);
				}
			}
				
			public System.String MarginName
			{
				get
				{
					System.String data = entity.MarginName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MarginName = null;
					else entity.MarginName = Convert.ToString(value);
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
			

			private esItemProductMargin entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemProductMarginQuery query)
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
				throw new Exception("esItemProductMargin can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ItemProductMargin : esItemProductMargin
	{

				
		#region ItemProductMarginValueCollectionByMarginID - Zero To Many
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - RefItemProductMarginDtToItemProductMarginHd
		/// </summary>

		[XmlIgnore]
		public ItemProductMarginValueCollection ItemProductMarginValueCollectionByMarginID
		{
			get
			{
				if(this._ItemProductMarginValueCollectionByMarginID == null)
				{
					this._ItemProductMarginValueCollectionByMarginID = new ItemProductMarginValueCollection();
					this._ItemProductMarginValueCollectionByMarginID.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("ItemProductMarginValueCollectionByMarginID", this._ItemProductMarginValueCollectionByMarginID);
				
					if(this.MarginID != null)
					{
						this._ItemProductMarginValueCollectionByMarginID.Query.Where(this._ItemProductMarginValueCollectionByMarginID.Query.MarginID == this.MarginID);
						this._ItemProductMarginValueCollectionByMarginID.Query.Load();

						// Auto-hookup Foreign Keys
						this._ItemProductMarginValueCollectionByMarginID.fks.Add(ItemProductMarginValueMetadata.ColumnNames.MarginID, this.MarginID);
					}
				}

				return this._ItemProductMarginValueCollectionByMarginID;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._ItemProductMarginValueCollectionByMarginID != null) 
				{ 
					this.RemovePostSave("ItemProductMarginValueCollectionByMarginID"); 
					this._ItemProductMarginValueCollectionByMarginID = null;
					
				} 
			} 			
		}

		private ItemProductMarginValueCollection _ItemProductMarginValueCollectionByMarginID;
		#endregion

				
		#region ItemProductNonMedicCollectionByMarginID - Zero To Many
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - RefItemProductMarginHdToItemProductNMedDt
		/// </summary>

		[XmlIgnore]
		public ItemProductNonMedicCollection ItemProductNonMedicCollectionByMarginID
		{
			get
			{
				if(this._ItemProductNonMedicCollectionByMarginID == null)
				{
					this._ItemProductNonMedicCollectionByMarginID = new ItemProductNonMedicCollection();
					this._ItemProductNonMedicCollectionByMarginID.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("ItemProductNonMedicCollectionByMarginID", this._ItemProductNonMedicCollectionByMarginID);
				
					if(this.MarginID != null)
					{
						this._ItemProductNonMedicCollectionByMarginID.Query.Where(this._ItemProductNonMedicCollectionByMarginID.Query.MarginID == this.MarginID);
						this._ItemProductNonMedicCollectionByMarginID.Query.Load();

						// Auto-hookup Foreign Keys
						this._ItemProductNonMedicCollectionByMarginID.fks.Add(ItemProductNonMedicMetadata.ColumnNames.MarginID, this.MarginID);
					}
				}

				return this._ItemProductNonMedicCollectionByMarginID;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._ItemProductNonMedicCollectionByMarginID != null) 
				{ 
					this.RemovePostSave("ItemProductNonMedicCollectionByMarginID"); 
					this._ItemProductNonMedicCollectionByMarginID = null;
					
				} 
			} 			
		}

		private ItemProductNonMedicCollection _ItemProductNonMedicCollectionByMarginID;
		#endregion

		
		/// <summary>
		/// Used internally by the entity's hierarchical properties.
		/// </summary>
		protected override List<esPropertyDescriptor> GetHierarchicalProperties()
		{
			List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();
			
			props.Add(new esPropertyDescriptor(this, "ItemProductMarginValueCollectionByMarginID", typeof(ItemProductMarginValueCollection), new ItemProductMarginValue()));
			props.Add(new esPropertyDescriptor(this, "ItemProductNonMedicCollectionByMarginID", typeof(ItemProductNonMedicCollection), new ItemProductNonMedic()));
		
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
	abstract public class esItemProductMarginQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ItemProductMarginMetadata.Meta();
			}
		}	
		

		public esQueryItem MarginID
		{
			get
			{
				return new esQueryItem(this, ItemProductMarginMetadata.ColumnNames.MarginID, esSystemType.String);
			}
		} 
		
		public esQueryItem MarginName
		{
			get
			{
				return new esQueryItem(this, ItemProductMarginMetadata.ColumnNames.MarginName, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, ItemProductMarginMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemProductMarginMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemProductMarginMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemProductMarginCollection")]
	public partial class ItemProductMarginCollection : esItemProductMarginCollection, IEnumerable<ItemProductMargin>
	{
		public ItemProductMarginCollection()
		{

		}
		
		public static implicit operator List<ItemProductMargin>(ItemProductMarginCollection coll)
		{
			List<ItemProductMargin> list = new List<ItemProductMargin>();
			
			foreach (ItemProductMargin emp in coll)
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
				return  ItemProductMarginMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemProductMarginQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemProductMargin(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemProductMargin();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ItemProductMarginQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemProductMarginQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ItemProductMarginQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ItemProductMargin AddNew()
		{
			ItemProductMargin entity = base.AddNewEntity() as ItemProductMargin;
			
			return entity;
		}

		public ItemProductMargin FindByPrimaryKey(System.String marginID)
		{
			return base.FindByPrimaryKey(marginID) as ItemProductMargin;
		}


		#region IEnumerable<ItemProductMargin> Members

		IEnumerator<ItemProductMargin> IEnumerable<ItemProductMargin>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ItemProductMargin;
			}
		}

		#endregion
		
		private ItemProductMarginQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemProductMargin' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ItemProductMargin ({MarginID})")]
	[Serializable]
	public partial class ItemProductMargin : esItemProductMargin
	{
		public ItemProductMargin()
		{

		}
	
		public ItemProductMargin(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemProductMarginMetadata.Meta();
			}
		}
		
		
		
		override protected esItemProductMarginQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemProductMarginQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ItemProductMarginQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemProductMarginQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ItemProductMarginQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ItemProductMarginQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ItemProductMarginQuery : esItemProductMarginQuery
	{
		public ItemProductMarginQuery()
		{

		}		
		
		public ItemProductMarginQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ItemProductMarginQuery";
        }
		
			
	}


	[Serializable]
	public partial class ItemProductMarginMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemProductMarginMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemProductMarginMetadata.ColumnNames.MarginID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMarginMetadata.PropertyNames.MarginID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemProductMarginMetadata.ColumnNames.MarginName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMarginMetadata.PropertyNames.MarginName;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemProductMarginMetadata.ColumnNames.IsActive, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMarginMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemProductMarginMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemProductMarginMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemProductMarginMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMarginMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ItemProductMarginMetadata Meta()
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
			 public const string MarginID = "MarginID";
			 public const string MarginName = "MarginName";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string MarginID = "MarginID";
			 public const string MarginName = "MarginName";
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
			lock (typeof(ItemProductMarginMetadata))
			{
				if(ItemProductMarginMetadata.mapDelegates == null)
				{
					ItemProductMarginMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ItemProductMarginMetadata.meta == null)
				{
					ItemProductMarginMetadata.meta = new ItemProductMarginMetadata();
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
				

				meta.AddTypeMap("MarginID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MarginName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ItemProductMargin";
				meta.Destination = "ItemProductMargin";
				
				meta.spInsert = "proc_ItemProductMarginInsert";				
				meta.spUpdate = "proc_ItemProductMarginUpdate";		
				meta.spDelete = "proc_ItemProductMarginDelete";
				meta.spLoadAll = "proc_ItemProductMarginLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemProductMarginLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemProductMarginMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
