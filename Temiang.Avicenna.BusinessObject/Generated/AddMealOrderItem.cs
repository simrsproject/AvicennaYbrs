/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 2/24/2015 10:51:27 AM
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
	abstract public class esAddMealOrderItemCollection : esEntityCollectionWAuditLog
	{
		public esAddMealOrderItemCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "AddMealOrderItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esAddMealOrderItemQuery query)
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
			this.InitQuery(query as esAddMealOrderItemQuery);
		}
		#endregion
		
		virtual public AddMealOrderItem DetachEntity(AddMealOrderItem entity)
		{
			return base.DetachEntity(entity) as AddMealOrderItem;
		}
		
		virtual public AddMealOrderItem AttachEntity(AddMealOrderItem entity)
		{
			return base.AttachEntity(entity) as AddMealOrderItem;
		}
		
		virtual public void Combine(AddMealOrderItemCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AddMealOrderItem this[int index]
		{
			get
			{
				return base[index] as AddMealOrderItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AddMealOrderItem);
		}
	}



	[Serializable]
	abstract public class esAddMealOrderItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAddMealOrderItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esAddMealOrderItem()
		{

		}

		public esAddMealOrderItem(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String orderNo, System.String foodID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderNo, foodID);
			else
				return LoadByPrimaryKeyStoredProcedure(orderNo, foodID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String orderNo, System.String foodID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderNo, foodID);
			else
				return LoadByPrimaryKeyStoredProcedure(orderNo, foodID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String orderNo, System.String foodID)
		{
			esAddMealOrderItemQuery query = this.GetDynamicQuery();
			query.Where(query.OrderNo == orderNo, query.FoodID == foodID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String orderNo, System.String foodID)
		{
			esParameters parms = new esParameters();
			parms.Add("OrderNo",orderNo);			parms.Add("FoodID",foodID);
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
						case "OrderNo": this.str.OrderNo = (string)value; break;							
						case "FoodID": this.str.FoodID = (string)value; break;							
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
		/// Maps to AddMealOrderItem.OrderNo
		/// </summary>
		virtual public System.String OrderNo
		{
			get
			{
				return base.GetSystemString(AddMealOrderItemMetadata.ColumnNames.OrderNo);
			}
			
			set
			{
				base.SetSystemString(AddMealOrderItemMetadata.ColumnNames.OrderNo, value);
			}
		}
		
		/// <summary>
		/// Maps to AddMealOrderItem.FoodID
		/// </summary>
		virtual public System.String FoodID
		{
			get
			{
				return base.GetSystemString(AddMealOrderItemMetadata.ColumnNames.FoodID);
			}
			
			set
			{
				base.SetSystemString(AddMealOrderItemMetadata.ColumnNames.FoodID, value);
			}
		}
		
		/// <summary>
		/// Maps to AddMealOrderItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AddMealOrderItemMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AddMealOrderItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to AddMealOrderItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AddMealOrderItemMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AddMealOrderItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esAddMealOrderItem entity)
			{
				this.entity = entity;
			}
			
	
			public System.String OrderNo
			{
				get
				{
					System.String data = entity.OrderNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderNo = null;
					else entity.OrderNo = Convert.ToString(value);
				}
			}
				
			public System.String FoodID
			{
				get
				{
					System.String data = entity.FoodID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FoodID = null;
					else entity.FoodID = Convert.ToString(value);
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
			

			private esAddMealOrderItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAddMealOrderItemQuery query)
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
				throw new Exception("esAddMealOrderItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class AddMealOrderItem : esAddMealOrderItem
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
	abstract public class esAddMealOrderItemQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AddMealOrderItemMetadata.Meta();
			}
		}	
		

		public esQueryItem OrderNo
		{
			get
			{
				return new esQueryItem(this, AddMealOrderItemMetadata.ColumnNames.OrderNo, esSystemType.String);
			}
		} 
		
		public esQueryItem FoodID
		{
			get
			{
				return new esQueryItem(this, AddMealOrderItemMetadata.ColumnNames.FoodID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AddMealOrderItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AddMealOrderItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AddMealOrderItemCollection")]
	public partial class AddMealOrderItemCollection : esAddMealOrderItemCollection, IEnumerable<AddMealOrderItem>
	{
		public AddMealOrderItemCollection()
		{

		}
		
		public static implicit operator List<AddMealOrderItem>(AddMealOrderItemCollection coll)
		{
			List<AddMealOrderItem> list = new List<AddMealOrderItem>();
			
			foreach (AddMealOrderItem emp in coll)
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
				return  AddMealOrderItemMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AddMealOrderItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AddMealOrderItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AddMealOrderItem();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public AddMealOrderItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AddMealOrderItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(AddMealOrderItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public AddMealOrderItem AddNew()
		{
			AddMealOrderItem entity = base.AddNewEntity() as AddMealOrderItem;
			
			return entity;
		}

		public AddMealOrderItem FindByPrimaryKey(System.String orderNo, System.String foodID)
		{
			return base.FindByPrimaryKey(orderNo, foodID) as AddMealOrderItem;
		}


		#region IEnumerable<AddMealOrderItem> Members

		IEnumerator<AddMealOrderItem> IEnumerable<AddMealOrderItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AddMealOrderItem;
			}
		}

		#endregion
		
		private AddMealOrderItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AddMealOrderItem' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("AddMealOrderItem ({OrderNo},{FoodID})")]
	[Serializable]
	public partial class AddMealOrderItem : esAddMealOrderItem
	{
		public AddMealOrderItem()
		{

		}
	
		public AddMealOrderItem(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AddMealOrderItemMetadata.Meta();
			}
		}
		
		
		
		override protected esAddMealOrderItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AddMealOrderItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public AddMealOrderItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AddMealOrderItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(AddMealOrderItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private AddMealOrderItemQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class AddMealOrderItemQuery : esAddMealOrderItemQuery
	{
		public AddMealOrderItemQuery()
		{

		}		
		
		public AddMealOrderItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "AddMealOrderItemQuery";
        }
		
			
	}


	[Serializable]
	public partial class AddMealOrderItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AddMealOrderItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AddMealOrderItemMetadata.ColumnNames.OrderNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AddMealOrderItemMetadata.PropertyNames.OrderNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(AddMealOrderItemMetadata.ColumnNames.FoodID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AddMealOrderItemMetadata.PropertyNames.FoodID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(AddMealOrderItemMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AddMealOrderItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AddMealOrderItemMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AddMealOrderItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public AddMealOrderItemMetadata Meta()
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
			 public const string OrderNo = "OrderNo";
			 public const string FoodID = "FoodID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string OrderNo = "OrderNo";
			 public const string FoodID = "FoodID";
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
			lock (typeof(AddMealOrderItemMetadata))
			{
				if(AddMealOrderItemMetadata.mapDelegates == null)
				{
					AddMealOrderItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AddMealOrderItemMetadata.meta == null)
				{
					AddMealOrderItemMetadata.meta = new AddMealOrderItemMetadata();
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
				

				meta.AddTypeMap("OrderNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FoodID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "AddMealOrderItem";
				meta.Destination = "AddMealOrderItem";
				
				meta.spInsert = "proc_AddMealOrderItemInsert";				
				meta.spUpdate = "proc_AddMealOrderItemUpdate";		
				meta.spDelete = "proc_AddMealOrderItemDelete";
				meta.spLoadAll = "proc_AddMealOrderItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_AddMealOrderItemLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AddMealOrderItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
