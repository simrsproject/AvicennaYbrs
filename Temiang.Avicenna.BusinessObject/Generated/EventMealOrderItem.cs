/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/30/2014 1:44:16 PM
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
	abstract public class esEventMealOrderItemCollection : esEntityCollectionWAuditLog
	{
		public esEventMealOrderItemCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "EventMealOrderItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esEventMealOrderItemQuery query)
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
			this.InitQuery(query as esEventMealOrderItemQuery);
		}
		#endregion
		
		virtual public EventMealOrderItem DetachEntity(EventMealOrderItem entity)
		{
			return base.DetachEntity(entity) as EventMealOrderItem;
		}
		
		virtual public EventMealOrderItem AttachEntity(EventMealOrderItem entity)
		{
			return base.AttachEntity(entity) as EventMealOrderItem;
		}
		
		virtual public void Combine(EventMealOrderItemCollection collection)
		{
			base.Combine(collection);
		}
		
		new public EventMealOrderItem this[int index]
		{
			get
			{
				return base[index] as EventMealOrderItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EventMealOrderItem);
		}
	}



	[Serializable]
	abstract public class esEventMealOrderItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEventMealOrderItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esEventMealOrderItem()
		{

		}

		public esEventMealOrderItem(DataRow row)
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
			esEventMealOrderItemQuery query = this.GetDynamicQuery();
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
						case "Qty": this.str.Qty = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Qty":
						
							if (value == null || value is System.Int16)
								this.Qty = (System.Int16?)value;
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
		/// Maps to EventMealOrderItem.OrderNo
		/// </summary>
		virtual public System.String OrderNo
		{
			get
			{
				return base.GetSystemString(EventMealOrderItemMetadata.ColumnNames.OrderNo);
			}
			
			set
			{
				base.SetSystemString(EventMealOrderItemMetadata.ColumnNames.OrderNo, value);
			}
		}
		
		/// <summary>
		/// Maps to EventMealOrderItem.FoodID
		/// </summary>
		virtual public System.String FoodID
		{
			get
			{
				return base.GetSystemString(EventMealOrderItemMetadata.ColumnNames.FoodID);
			}
			
			set
			{
				base.SetSystemString(EventMealOrderItemMetadata.ColumnNames.FoodID, value);
			}
		}
		
		/// <summary>
		/// Maps to EventMealOrderItem.Qty
		/// </summary>
		virtual public System.Int16? Qty
		{
			get
			{
				return base.GetSystemInt16(EventMealOrderItemMetadata.ColumnNames.Qty);
			}
			
			set
			{
				base.SetSystemInt16(EventMealOrderItemMetadata.ColumnNames.Qty, value);
			}
		}
		
		/// <summary>
		/// Maps to EventMealOrderItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EventMealOrderItemMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EventMealOrderItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to EventMealOrderItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EventMealOrderItemMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(EventMealOrderItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEventMealOrderItem entity)
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
				
			public System.String Qty
			{
				get
				{
					System.Int16? data = entity.Qty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Qty = null;
					else entity.Qty = Convert.ToInt16(value);
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
			

			private esEventMealOrderItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEventMealOrderItemQuery query)
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
				throw new Exception("esEventMealOrderItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class EventMealOrderItem : esEventMealOrderItem
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
	abstract public class esEventMealOrderItemQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return EventMealOrderItemMetadata.Meta();
			}
		}	
		

		public esQueryItem OrderNo
		{
			get
			{
				return new esQueryItem(this, EventMealOrderItemMetadata.ColumnNames.OrderNo, esSystemType.String);
			}
		} 
		
		public esQueryItem FoodID
		{
			get
			{
				return new esQueryItem(this, EventMealOrderItemMetadata.ColumnNames.FoodID, esSystemType.String);
			}
		} 
		
		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, EventMealOrderItemMetadata.ColumnNames.Qty, esSystemType.Int16);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EventMealOrderItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EventMealOrderItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EventMealOrderItemCollection")]
	public partial class EventMealOrderItemCollection : esEventMealOrderItemCollection, IEnumerable<EventMealOrderItem>
	{
		public EventMealOrderItemCollection()
		{

		}
		
		public static implicit operator List<EventMealOrderItem>(EventMealOrderItemCollection coll)
		{
			List<EventMealOrderItem> list = new List<EventMealOrderItem>();
			
			foreach (EventMealOrderItem emp in coll)
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
				return  EventMealOrderItemMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EventMealOrderItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EventMealOrderItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EventMealOrderItem();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public EventMealOrderItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EventMealOrderItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(EventMealOrderItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public EventMealOrderItem AddNew()
		{
			EventMealOrderItem entity = base.AddNewEntity() as EventMealOrderItem;
			
			return entity;
		}

		public EventMealOrderItem FindByPrimaryKey(System.String orderNo, System.String foodID)
		{
			return base.FindByPrimaryKey(orderNo, foodID) as EventMealOrderItem;
		}


		#region IEnumerable<EventMealOrderItem> Members

		IEnumerator<EventMealOrderItem> IEnumerable<EventMealOrderItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as EventMealOrderItem;
			}
		}

		#endregion
		
		private EventMealOrderItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EventMealOrderItem' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("EventMealOrderItem ({OrderNo},{FoodID})")]
	[Serializable]
	public partial class EventMealOrderItem : esEventMealOrderItem
	{
		public EventMealOrderItem()
		{

		}
	
		public EventMealOrderItem(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EventMealOrderItemMetadata.Meta();
			}
		}
		
		
		
		override protected esEventMealOrderItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EventMealOrderItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public EventMealOrderItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EventMealOrderItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(EventMealOrderItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private EventMealOrderItemQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class EventMealOrderItemQuery : esEventMealOrderItemQuery
	{
		public EventMealOrderItemQuery()
		{

		}		
		
		public EventMealOrderItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "EventMealOrderItemQuery";
        }
		
			
	}


	[Serializable]
	public partial class EventMealOrderItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EventMealOrderItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EventMealOrderItemMetadata.ColumnNames.OrderNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = EventMealOrderItemMetadata.PropertyNames.OrderNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(EventMealOrderItemMetadata.ColumnNames.FoodID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = EventMealOrderItemMetadata.PropertyNames.FoodID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EventMealOrderItemMetadata.ColumnNames.Qty, 2, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = EventMealOrderItemMetadata.PropertyNames.Qty;
			c.NumericPrecision = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(EventMealOrderItemMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EventMealOrderItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EventMealOrderItemMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EventMealOrderItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public EventMealOrderItemMetadata Meta()
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
			 public const string Qty = "Qty";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string OrderNo = "OrderNo";
			 public const string FoodID = "FoodID";
			 public const string Qty = "Qty";
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
			lock (typeof(EventMealOrderItemMetadata))
			{
				if(EventMealOrderItemMetadata.mapDelegates == null)
				{
					EventMealOrderItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (EventMealOrderItemMetadata.meta == null)
				{
					EventMealOrderItemMetadata.meta = new EventMealOrderItemMetadata();
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
				meta.AddTypeMap("Qty", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "EventMealOrderItem";
				meta.Destination = "EventMealOrderItem";
				
				meta.spInsert = "proc_EventMealOrderItemInsert";				
				meta.spUpdate = "proc_EventMealOrderItemUpdate";		
				meta.spDelete = "proc_EventMealOrderItemDelete";
				meta.spLoadAll = "proc_EventMealOrderItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_EventMealOrderItemLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EventMealOrderItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
