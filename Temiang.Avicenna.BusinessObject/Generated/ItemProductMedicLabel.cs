/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 6/30/2014 2:35:25 PM
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
	abstract public class esItemProductMedicLabelCollection : esEntityCollection
	{
		public esItemProductMedicLabelCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ItemProductMedicLabelCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemProductMedicLabelQuery query)
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
			this.InitQuery(query as esItemProductMedicLabelQuery);
		}
		#endregion
		
		virtual public ItemProductMedicLabel DetachEntity(ItemProductMedicLabel entity)
		{
			return base.DetachEntity(entity) as ItemProductMedicLabel;
		}
		
		virtual public ItemProductMedicLabel AttachEntity(ItemProductMedicLabel entity)
		{
			return base.AttachEntity(entity) as ItemProductMedicLabel;
		}
		
		virtual public void Combine(ItemProductMedicLabelCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ItemProductMedicLabel this[int index]
		{
			get
			{
				return base[index] as ItemProductMedicLabel;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemProductMedicLabel);
		}
	}



	[Serializable]
	abstract public class esItemProductMedicLabel : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemProductMedicLabelQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemProductMedicLabel()
		{

		}

		public esItemProductMedicLabel(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String itemID, System.String labelID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, labelID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, labelID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String itemID, System.String labelID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, labelID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, labelID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String itemID, System.String labelID)
		{
			esItemProductMedicLabelQuery query = this.GetDynamicQuery();
			query.Where(query.ItemID == itemID, query.LabelID == labelID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String itemID, System.String labelID)
		{
			esParameters parms = new esParameters();
			parms.Add("ItemID",itemID);			parms.Add("LabelID",labelID);
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
						case "LabelID": this.str.LabelID = (string)value; break;							
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
		/// Maps to ItemProductMedicLabel.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemProductMedicLabelMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ItemProductMedicLabelMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemProductMedicLabel.LabelID
		/// </summary>
		virtual public System.String LabelID
		{
			get
			{
				return base.GetSystemString(ItemProductMedicLabelMetadata.ColumnNames.LabelID);
			}
			
			set
			{
				base.SetSystemString(ItemProductMedicLabelMetadata.ColumnNames.LabelID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemProductMedicLabel.InsertDateTime
		/// </summary>
		virtual public System.DateTime? InsertDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemProductMedicLabelMetadata.ColumnNames.InsertDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ItemProductMedicLabelMetadata.ColumnNames.InsertDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemProductMedicLabel.InsertByUserID
		/// </summary>
		virtual public System.String InsertByUserID
		{
			get
			{
				return base.GetSystemString(ItemProductMedicLabelMetadata.ColumnNames.InsertByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemProductMedicLabelMetadata.ColumnNames.InsertByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemProductMedicLabel.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemProductMedicLabelMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ItemProductMedicLabelMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemProductMedicLabel.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemProductMedicLabelMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemProductMedicLabelMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esItemProductMedicLabel entity)
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
				
			public System.String LabelID
			{
				get
				{
					System.String data = entity.LabelID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LabelID = null;
					else entity.LabelID = Convert.ToString(value);
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
			

			private esItemProductMedicLabel entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemProductMedicLabelQuery query)
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
				throw new Exception("esItemProductMedicLabel can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esItemProductMedicLabelQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ItemProductMedicLabelMetadata.Meta();
			}
		}	
		

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicLabelMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem LabelID
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicLabelMetadata.ColumnNames.LabelID, esSystemType.String);
			}
		} 
		
		public esQueryItem InsertDateTime
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicLabelMetadata.ColumnNames.InsertDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem InsertByUserID
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicLabelMetadata.ColumnNames.InsertByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicLabelMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicLabelMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemProductMedicLabelCollection")]
	public partial class ItemProductMedicLabelCollection : esItemProductMedicLabelCollection, IEnumerable<ItemProductMedicLabel>
	{
		public ItemProductMedicLabelCollection()
		{

		}
		
		public static implicit operator List<ItemProductMedicLabel>(ItemProductMedicLabelCollection coll)
		{
			List<ItemProductMedicLabel> list = new List<ItemProductMedicLabel>();
			
			foreach (ItemProductMedicLabel emp in coll)
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
				return  ItemProductMedicLabelMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemProductMedicLabelQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemProductMedicLabel(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemProductMedicLabel();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ItemProductMedicLabelQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemProductMedicLabelQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ItemProductMedicLabelQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ItemProductMedicLabel AddNew()
		{
			ItemProductMedicLabel entity = base.AddNewEntity() as ItemProductMedicLabel;
			
			return entity;
		}

		public ItemProductMedicLabel FindByPrimaryKey(System.String itemID, System.String labelID)
		{
			return base.FindByPrimaryKey(itemID, labelID) as ItemProductMedicLabel;
		}


		#region IEnumerable<ItemProductMedicLabel> Members

		IEnumerator<ItemProductMedicLabel> IEnumerable<ItemProductMedicLabel>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ItemProductMedicLabel;
			}
		}

		#endregion
		
		private ItemProductMedicLabelQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemProductMedicLabel' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ItemProductMedicLabel ({ItemID},{LabelID})")]
	[Serializable]
	public partial class ItemProductMedicLabel : esItemProductMedicLabel
	{
		public ItemProductMedicLabel()
		{

		}
	
		public ItemProductMedicLabel(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemProductMedicLabelMetadata.Meta();
			}
		}
		
		
		
		override protected esItemProductMedicLabelQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemProductMedicLabelQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ItemProductMedicLabelQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemProductMedicLabelQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ItemProductMedicLabelQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ItemProductMedicLabelQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ItemProductMedicLabelQuery : esItemProductMedicLabelQuery
	{
		public ItemProductMedicLabelQuery()
		{

		}		
		
		public ItemProductMedicLabelQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ItemProductMedicLabelQuery";
        }
		
			
	}


	[Serializable]
	public partial class ItemProductMedicLabelMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemProductMedicLabelMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemProductMedicLabelMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicLabelMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemProductMedicLabelMetadata.ColumnNames.LabelID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicLabelMetadata.PropertyNames.LabelID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemProductMedicLabelMetadata.ColumnNames.InsertDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemProductMedicLabelMetadata.PropertyNames.InsertDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemProductMedicLabelMetadata.ColumnNames.InsertByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicLabelMetadata.PropertyNames.InsertByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemProductMedicLabelMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemProductMedicLabelMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemProductMedicLabelMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicLabelMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ItemProductMedicLabelMetadata Meta()
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
			 public const string LabelID = "LabelID";
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
			 public const string LabelID = "LabelID";
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
			lock (typeof(ItemProductMedicLabelMetadata))
			{
				if(ItemProductMedicLabelMetadata.mapDelegates == null)
				{
					ItemProductMedicLabelMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ItemProductMedicLabelMetadata.meta == null)
				{
					ItemProductMedicLabelMetadata.meta = new ItemProductMedicLabelMetadata();
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
				meta.AddTypeMap("LabelID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InsertDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("InsertByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ItemProductMedicLabel";
				meta.Destination = "ItemProductMedicLabel";
				
				meta.spInsert = "proc_ItemProductMedicLabelInsert";				
				meta.spUpdate = "proc_ItemProductMedicLabelUpdate";		
				meta.spDelete = "proc_ItemProductMedicLabelDelete";
				meta.spLoadAll = "proc_ItemProductMedicLabelLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemProductMedicLabelLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemProductMedicLabelMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
