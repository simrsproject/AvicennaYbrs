/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 7/2/2014 2:03:19 PM
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
	abstract public class esItemProductMedicIndicationCollection : esEntityCollection
	{
		public esItemProductMedicIndicationCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ItemProductMedicIndicationCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemProductMedicIndicationQuery query)
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
			this.InitQuery(query as esItemProductMedicIndicationQuery);
		}
		#endregion
		
		virtual public ItemProductMedicIndication DetachEntity(ItemProductMedicIndication entity)
		{
			return base.DetachEntity(entity) as ItemProductMedicIndication;
		}
		
		virtual public ItemProductMedicIndication AttachEntity(ItemProductMedicIndication entity)
		{
			return base.AttachEntity(entity) as ItemProductMedicIndication;
		}
		
		virtual public void Combine(ItemProductMedicIndicationCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ItemProductMedicIndication this[int index]
		{
			get
			{
				return base[index] as ItemProductMedicIndication;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemProductMedicIndication);
		}
	}



	[Serializable]
	abstract public class esItemProductMedicIndication : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemProductMedicIndicationQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemProductMedicIndication()
		{

		}

		public esItemProductMedicIndication(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String itemID, System.String indicationID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, indicationID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, indicationID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String itemID, System.String indicationID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, indicationID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, indicationID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String itemID, System.String indicationID)
		{
			esItemProductMedicIndicationQuery query = this.GetDynamicQuery();
			query.Where(query.ItemID == itemID, query.IndicationID == indicationID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String itemID, System.String indicationID)
		{
			esParameters parms = new esParameters();
			parms.Add("ItemID",itemID);			parms.Add("IndicationID",indicationID);
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
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "IndicationID": this.str.IndicationID = (string)value; break;							
						case "InsertDateTime": this.str.InsertDateTime = (string)value; break;							
						case "InsertByUserID": this.str.InsertByUserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "InsertDateTime":
						
							if (value == null || value is System.DateTime)
								this.InsertDateTime = (System.DateTime?)value;
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
		/// Maps to ItemProductMedicIndication.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemProductMedicIndicationMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ItemProductMedicIndicationMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemProductMedicIndication.IndicationID
		/// </summary>
		virtual public System.String IndicationID
		{
			get
			{
				return base.GetSystemString(ItemProductMedicIndicationMetadata.ColumnNames.IndicationID);
			}
			
			set
			{
				base.SetSystemString(ItemProductMedicIndicationMetadata.ColumnNames.IndicationID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemProductMedicIndication.InsertDateTime
		/// </summary>
		virtual public System.DateTime? InsertDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemProductMedicIndicationMetadata.ColumnNames.InsertDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ItemProductMedicIndicationMetadata.ColumnNames.InsertDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemProductMedicIndication.InsertByUserID
		/// </summary>
		virtual public System.String InsertByUserID
		{
			get
			{
				return base.GetSystemString(ItemProductMedicIndicationMetadata.ColumnNames.InsertByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemProductMedicIndicationMetadata.ColumnNames.InsertByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemProductMedicIndication.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemProductMedicIndicationMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ItemProductMedicIndicationMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemProductMedicIndication.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemProductMedicIndicationMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemProductMedicIndicationMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esItemProductMedicIndication entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ItemID
			{
				get
				{
					System.String data = entity.ItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemID = null;
					else entity.ItemID = Convert.ToString(value);
				}
			}
				
			public System.String IndicationID
			{
				get
				{
					System.String data = entity.IndicationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IndicationID = null;
					else entity.IndicationID = Convert.ToString(value);
				}
			}
				
			public System.String InsertDateTime
			{
				get
				{
					System.DateTime? data = entity.InsertDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InsertDateTime = null;
					else entity.InsertDateTime = Convert.ToDateTime(value);
				}
			}
				
			public System.String InsertByUserID
			{
				get
				{
					System.String data = entity.InsertByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InsertByUserID = null;
					else entity.InsertByUserID = Convert.ToString(value);
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
			

			private esItemProductMedicIndication entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemProductMedicIndicationQuery query)
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
				throw new Exception("esItemProductMedicIndication can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esItemProductMedicIndicationQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ItemProductMedicIndicationMetadata.Meta();
			}
		}	
		

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicIndicationMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem IndicationID
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicIndicationMetadata.ColumnNames.IndicationID, esSystemType.String);
			}
		} 
		
		public esQueryItem InsertDateTime
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicIndicationMetadata.ColumnNames.InsertDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem InsertByUserID
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicIndicationMetadata.ColumnNames.InsertByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicIndicationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicIndicationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemProductMedicIndicationCollection")]
	public partial class ItemProductMedicIndicationCollection : esItemProductMedicIndicationCollection, IEnumerable<ItemProductMedicIndication>
	{
		public ItemProductMedicIndicationCollection()
		{

		}
		
		public static implicit operator List<ItemProductMedicIndication>(ItemProductMedicIndicationCollection coll)
		{
			List<ItemProductMedicIndication> list = new List<ItemProductMedicIndication>();
			
			foreach (ItemProductMedicIndication emp in coll)
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
				return  ItemProductMedicIndicationMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemProductMedicIndicationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemProductMedicIndication(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemProductMedicIndication();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ItemProductMedicIndicationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemProductMedicIndicationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ItemProductMedicIndicationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ItemProductMedicIndication AddNew()
		{
			ItemProductMedicIndication entity = base.AddNewEntity() as ItemProductMedicIndication;
			
			return entity;
		}

		public ItemProductMedicIndication FindByPrimaryKey(System.String itemID, System.String indicationID)
		{
			return base.FindByPrimaryKey(itemID, indicationID) as ItemProductMedicIndication;
		}


		#region IEnumerable<ItemProductMedicIndication> Members

		IEnumerator<ItemProductMedicIndication> IEnumerable<ItemProductMedicIndication>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ItemProductMedicIndication;
			}
		}

		#endregion
		
		private ItemProductMedicIndicationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemProductMedicIndication' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ItemProductMedicIndication ({ItemID},{IndicationID})")]
	[Serializable]
	public partial class ItemProductMedicIndication : esItemProductMedicIndication
	{
		public ItemProductMedicIndication()
		{

		}
	
		public ItemProductMedicIndication(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemProductMedicIndicationMetadata.Meta();
			}
		}
		
		
		
		override protected esItemProductMedicIndicationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemProductMedicIndicationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ItemProductMedicIndicationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemProductMedicIndicationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ItemProductMedicIndicationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ItemProductMedicIndicationQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ItemProductMedicIndicationQuery : esItemProductMedicIndicationQuery
	{
		public ItemProductMedicIndicationQuery()
		{

		}		
		
		public ItemProductMedicIndicationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ItemProductMedicIndicationQuery";
        }
		
			
	}


	[Serializable]
	public partial class ItemProductMedicIndicationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemProductMedicIndicationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemProductMedicIndicationMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicIndicationMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemProductMedicIndicationMetadata.ColumnNames.IndicationID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicIndicationMetadata.PropertyNames.IndicationID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemProductMedicIndicationMetadata.ColumnNames.InsertDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemProductMedicIndicationMetadata.PropertyNames.InsertDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemProductMedicIndicationMetadata.ColumnNames.InsertByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicIndicationMetadata.PropertyNames.InsertByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemProductMedicIndicationMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemProductMedicIndicationMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemProductMedicIndicationMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicIndicationMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ItemProductMedicIndicationMetadata Meta()
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
			 public const string ItemID = "ItemID";
			 public const string IndicationID = "IndicationID";
			 public const string InsertDateTime = "InsertDateTime";
			 public const string InsertByUserID = "InsertByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ItemID = "ItemID";
			 public const string IndicationID = "IndicationID";
			 public const string InsertDateTime = "InsertDateTime";
			 public const string InsertByUserID = "InsertByUserID";
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
			lock (typeof(ItemProductMedicIndicationMetadata))
			{
				if(ItemProductMedicIndicationMetadata.mapDelegates == null)
				{
					ItemProductMedicIndicationMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ItemProductMedicIndicationMetadata.meta == null)
				{
					ItemProductMedicIndicationMetadata.meta = new ItemProductMedicIndicationMetadata();
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
				

				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IndicationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InsertDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("InsertByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ItemProductMedicIndication";
				meta.Destination = "ItemProductMedicIndication";
				
				meta.spInsert = "proc_ItemProductMedicIndicationInsert";				
				meta.spUpdate = "proc_ItemProductMedicIndicationUpdate";		
				meta.spDelete = "proc_ItemProductMedicIndicationDelete";
				meta.spLoadAll = "proc_ItemProductMedicIndicationLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemProductMedicIndicationLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemProductMedicIndicationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
