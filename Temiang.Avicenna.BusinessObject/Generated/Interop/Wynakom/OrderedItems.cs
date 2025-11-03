/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 7/27/2020 2:30:36 PM
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



namespace Temiang.Avicenna.BusinessObject.Interop.Wynakom
{

	[Serializable]
	abstract public class esOrderedItemsCollection : esEntityCollectionWAuditLog
	{
		public esOrderedItemsCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "OrderedItemsCollection";
		}

		#region Query Logic
		protected void InitQuery(esOrderedItemsQuery query)
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
			this.InitQuery(query as esOrderedItemsQuery);
		}
		#endregion
		
		virtual public OrderedItems DetachEntity(OrderedItems entity)
		{
			return base.DetachEntity(entity) as OrderedItems;
		}
		
		virtual public OrderedItems AttachEntity(OrderedItems entity)
		{
			return base.AttachEntity(entity) as OrderedItems;
		}
		
		virtual public void Combine(OrderedItemsCollection collection)
		{
			base.Combine(collection);
		}
		
		new public OrderedItems this[int index]
		{
			get
			{
				return base[index] as OrderedItems;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(OrderedItems);
		}
	}



	[Serializable]
	abstract public class esOrderedItems : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esOrderedItemsQuery GetDynamicQuery()
		{
			return null;
		}

		public esOrderedItems()
		{

		}

		public esOrderedItems(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String orderNumber, System.DateTime orderItemDate)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderNumber, orderItemDate);
			else
				return LoadByPrimaryKeyStoredProcedure(orderNumber, orderItemDate);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String orderNumber, System.DateTime orderItemDate)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderNumber, orderItemDate);
			else
				return LoadByPrimaryKeyStoredProcedure(orderNumber, orderItemDate);
		}

		private bool LoadByPrimaryKeyDynamic(System.String orderNumber, System.DateTime orderItemDate)
		{
			esOrderedItemsQuery query = this.GetDynamicQuery();
			query.Where(query.OrderItemDate == orderItemDate, query.OrderNumber == orderNumber);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String orderNumber, System.DateTime orderItemDate)
		{
			esParameters parms = new esParameters();
			parms.Add("Order_Item_Date",orderItemDate);			parms.Add("Order_Number",orderNumber);
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
						case "OrderNumber": this.str.OrderNumber = (string)value; break;							
						case "OrderItemName": this.str.OrderItemName = (string)value; break;							
						case "OrderItemDate": this.str.OrderItemDate = (string)value; break;							
						case "OrderItemID": this.str.OrderItemID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "OrderItemDate":
						
							if (value == null || value is System.DateTime)
								this.OrderItemDate = (System.DateTime?)value;
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
		/// Maps to Ordered_Items.Order_Number
		/// </summary>
		virtual public System.String OrderNumber
		{
			get
			{
				return base.GetSystemString(OrderedItemsMetadata.ColumnNames.OrderNumber);
			}
			
			set
			{
				base.SetSystemString(OrderedItemsMetadata.ColumnNames.OrderNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to Ordered_Items.Order_Item_Name
		/// </summary>
		virtual public System.String OrderItemName
		{
			get
			{
				return base.GetSystemString(OrderedItemsMetadata.ColumnNames.OrderItemName);
			}
			
			set
			{
				base.SetSystemString(OrderedItemsMetadata.ColumnNames.OrderItemName, value);
			}
		}
		
		/// <summary>
		/// Maps to Ordered_Items.Order_Item_Date
		/// </summary>
		virtual public System.DateTime? OrderItemDate
		{
			get
			{
				return base.GetSystemDateTime(OrderedItemsMetadata.ColumnNames.OrderItemDate);
			}
			
			set
			{
				base.SetSystemDateTime(OrderedItemsMetadata.ColumnNames.OrderItemDate, value);
			}
		}
		
		/// <summary>
		/// Maps to Ordered_Items.Order_Item_ID
		/// </summary>
		virtual public System.String OrderItemID
		{
			get
			{
				return base.GetSystemString(OrderedItemsMetadata.ColumnNames.OrderItemID);
			}
			
			set
			{
				base.SetSystemString(OrderedItemsMetadata.ColumnNames.OrderItemID, value);
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
			public esStrings(esOrderedItems entity)
			{
				this.entity = entity;
			}
			
	
			public System.String OrderNumber
			{
				get
				{
					System.String data = entity.OrderNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderNumber = null;
					else entity.OrderNumber = Convert.ToString(value);
				}
			}
				
			public System.String OrderItemName
			{
				get
				{
					System.String data = entity.OrderItemName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderItemName = null;
					else entity.OrderItemName = Convert.ToString(value);
				}
			}
				
			public System.String OrderItemDate
			{
				get
				{
					System.DateTime? data = entity.OrderItemDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderItemDate = null;
					else entity.OrderItemDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String OrderItemID
			{
				get
				{
					System.String data = entity.OrderItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderItemID = null;
					else entity.OrderItemID = Convert.ToString(value);
				}
			}
			

			private esOrderedItems entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esOrderedItemsQuery query)
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
				throw new Exception("esOrderedItems can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esOrderedItemsQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return OrderedItemsMetadata.Meta();
			}
		}	
		

		public esQueryItem OrderNumber
		{
			get
			{
				return new esQueryItem(this, OrderedItemsMetadata.ColumnNames.OrderNumber, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderItemName
		{
			get
			{
				return new esQueryItem(this, OrderedItemsMetadata.ColumnNames.OrderItemName, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderItemDate
		{
			get
			{
				return new esQueryItem(this, OrderedItemsMetadata.ColumnNames.OrderItemDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem OrderItemID
		{
			get
			{
				return new esQueryItem(this, OrderedItemsMetadata.ColumnNames.OrderItemID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("OrderedItemsCollection")]
	public partial class OrderedItemsCollection : esOrderedItemsCollection, IEnumerable<OrderedItems>
	{
		public OrderedItemsCollection()
		{

		}
		
		public static implicit operator List<OrderedItems>(OrderedItemsCollection coll)
		{
			List<OrderedItems> list = new List<OrderedItems>();
			
			foreach (OrderedItems emp in coll)
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
				return  OrderedItemsMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new OrderedItemsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new OrderedItems(row);
		}

		override protected esEntity CreateEntity()
		{
			return new OrderedItems();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public OrderedItemsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new OrderedItemsQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(OrderedItemsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public OrderedItems AddNew()
		{
			OrderedItems entity = base.AddNewEntity() as OrderedItems;
			
			return entity;
		}

		public OrderedItems FindByPrimaryKey(System.DateTime orderItemDate, System.String orderNumber)
		{
			return base.FindByPrimaryKey(orderItemDate, orderNumber) as OrderedItems;
		}


		#region IEnumerable<OrderedItems> Members

		IEnumerator<OrderedItems> IEnumerable<OrderedItems>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as OrderedItems;
			}
		}

		#endregion
		
		private OrderedItemsQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Ordered_Items' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("OrderedItems ({OrderNumber},{OrderItemDate})")]
	[Serializable]
	public partial class OrderedItems : esOrderedItems
	{
		public OrderedItems()
		{

		}
	
		public OrderedItems(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return OrderedItemsMetadata.Meta();
			}
		}
		
		
		
		override protected esOrderedItemsQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new OrderedItemsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public OrderedItemsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new OrderedItemsQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(OrderedItemsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private OrderedItemsQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class OrderedItemsQuery : esOrderedItemsQuery
	{
		public OrderedItemsQuery()
		{

		}		
		
		public OrderedItemsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "OrderedItemsQuery";
        }
		
			
	}


	[Serializable]
	public partial class OrderedItemsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected OrderedItemsMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(OrderedItemsMetadata.ColumnNames.OrderNumber, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderedItemsMetadata.PropertyNames.OrderNumber;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderedItemsMetadata.ColumnNames.OrderItemName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderedItemsMetadata.PropertyNames.OrderItemName;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderedItemsMetadata.ColumnNames.OrderItemDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = OrderedItemsMetadata.PropertyNames.OrderItemDate;
			c.IsInPrimaryKey = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderedItemsMetadata.ColumnNames.OrderItemID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderedItemsMetadata.PropertyNames.OrderItemID;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public OrderedItemsMetadata Meta()
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
			 public const string OrderNumber = "Order_Number";
			 public const string OrderItemName = "Order_Item_Name";
			 public const string OrderItemDate = "Order_Item_Date";
			 public const string OrderItemID = "Order_Item_ID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string OrderNumber = "OrderNumber";
			 public const string OrderItemName = "OrderItemName";
			 public const string OrderItemDate = "OrderItemDate";
			 public const string OrderItemID = "OrderItemID";
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
			lock (typeof(OrderedItemsMetadata))
			{
				if(OrderedItemsMetadata.mapDelegates == null)
				{
					OrderedItemsMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (OrderedItemsMetadata.meta == null)
				{
					OrderedItemsMetadata.meta = new OrderedItemsMetadata();
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
				

				meta.AddTypeMap("OrderNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrderItemName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrderItemDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("OrderItemID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "Ordered_Items";
				meta.Destination = "Ordered_Items";
				
				meta.spInsert = "proc_Ordered_ItemsInsert";				
				meta.spUpdate = "proc_Ordered_ItemsUpdate";		
				meta.spDelete = "proc_Ordered_ItemsDelete";
				meta.spLoadAll = "proc_Ordered_ItemsLoadAll";
				meta.spLoadByPrimaryKey = "proc_Ordered_ItemsLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private OrderedItemsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
