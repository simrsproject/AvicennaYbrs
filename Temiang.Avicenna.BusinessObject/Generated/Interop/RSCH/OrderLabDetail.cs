/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 7/8/2014 10:15:48 AM
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



namespace Temiang.Avicenna.BusinessObject.Interop.RSCH
{

	[Serializable]
	abstract public class esOrderLabDetailCollection : esEntityCollectionWAuditLog
	{
		public esOrderLabDetailCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "OrderLabDetailCollection";
		}

		#region Query Logic
		protected void InitQuery(esOrderLabDetailQuery query)
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
			this.InitQuery(query as esOrderLabDetailQuery);
		}
		#endregion
		
		virtual public OrderLabDetail DetachEntity(OrderLabDetail entity)
		{
			return base.DetachEntity(entity) as OrderLabDetail;
		}
		
		virtual public OrderLabDetail AttachEntity(OrderLabDetail entity)
		{
			return base.AttachEntity(entity) as OrderLabDetail;
		}
		
		virtual public void Combine(OrderLabDetailCollection collection)
		{
			base.Combine(collection);
		}
		
		new public OrderLabDetail this[int index]
		{
			get
			{
				return base[index] as OrderLabDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(OrderLabDetail);
		}
	}



	[Serializable]
	abstract public class esOrderLabDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esOrderLabDetailQuery GetDynamicQuery()
		{
			return null;
		}

		public esOrderLabDetail()
		{

		}

		public esOrderLabDetail(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey()
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic();
			else
				return LoadByPrimaryKeyStoredProcedure();
		}

        //public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, )
        //{
        //    if (sqlAccessType == esSqlAccessType.DynamicSQL)
        //        return LoadByPrimaryKeyDynamic();
        //    else
        //        return LoadByPrimaryKeyStoredProcedure();
        //}

		private bool LoadByPrimaryKeyDynamic()
		{
			esOrderLabDetailQuery query = this.GetDynamicQuery();
			query.Where();
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure()
		{
			esParameters parms = new esParameters();

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
						case "OrderLabNo": this.str.OrderLabNo = (string)value; break;							
						case "OrderLabTglOrder": this.str.OrderLabTglOrder = (string)value; break;							
						case "CheckupResultTestCode": this.str.CheckupResultTestCode = (string)value; break;							
						case "OrderLabJamOrder": this.str.OrderLabJamOrder = (string)value; break;							
						case "OrderLabStatus": this.str.OrderLabStatus = (string)value; break;							
						case "OrderLabCito": this.str.OrderLabCito = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "OrderLabTglOrder":
						
							if (value == null || value is System.DateTime)
								this.OrderLabTglOrder = (System.DateTime?)value;
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
		/// Maps to OrderLabDetail.OrderLabNo
		/// </summary>
		virtual public System.String OrderLabNo
		{
			get
			{
				return base.GetSystemString(OrderLabDetailMetadata.ColumnNames.OrderLabNo);
			}
			
			set
			{
				base.SetSystemString(OrderLabDetailMetadata.ColumnNames.OrderLabNo, value);
			}
		}
		
		/// <summary>
		/// Maps to OrderLabDetail.OrderLabTglOrder
		/// </summary>
		virtual public System.DateTime? OrderLabTglOrder
		{
			get
			{
				return base.GetSystemDateTime(OrderLabDetailMetadata.ColumnNames.OrderLabTglOrder);
			}
			
			set
			{
				base.SetSystemDateTime(OrderLabDetailMetadata.ColumnNames.OrderLabTglOrder, value);
			}
		}
		
		/// <summary>
		/// Maps to OrderLabDetail.CheckupResultTestCode
		/// </summary>
		virtual public System.String CheckupResultTestCode
		{
			get
			{
				return base.GetSystemString(OrderLabDetailMetadata.ColumnNames.CheckupResultTestCode);
			}
			
			set
			{
				base.SetSystemString(OrderLabDetailMetadata.ColumnNames.CheckupResultTestCode, value);
			}
		}
		
		/// <summary>
		/// Maps to OrderLabDetail.OrderLabJamOrder
		/// </summary>
		virtual public System.String OrderLabJamOrder
		{
			get
			{
				return base.GetSystemString(OrderLabDetailMetadata.ColumnNames.OrderLabJamOrder);
			}
			
			set
			{
				base.SetSystemString(OrderLabDetailMetadata.ColumnNames.OrderLabJamOrder, value);
			}
		}
		
		/// <summary>
		/// Maps to OrderLabDetail.OrderLabStatus
		/// </summary>
		virtual public System.String OrderLabStatus
		{
			get
			{
				return base.GetSystemString(OrderLabDetailMetadata.ColumnNames.OrderLabStatus);
			}
			
			set
			{
				base.SetSystemString(OrderLabDetailMetadata.ColumnNames.OrderLabStatus, value);
			}
		}
		
		/// <summary>
		/// Maps to OrderLabDetail.OrderLabCito
		/// </summary>
		virtual public System.String OrderLabCito
		{
			get
			{
				return base.GetSystemString(OrderLabDetailMetadata.ColumnNames.OrderLabCito);
			}
			
			set
			{
				base.SetSystemString(OrderLabDetailMetadata.ColumnNames.OrderLabCito, value);
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
			public esStrings(esOrderLabDetail entity)
			{
				this.entity = entity;
			}
			
	
			public System.String OrderLabNo
			{
				get
				{
					System.String data = entity.OrderLabNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderLabNo = null;
					else entity.OrderLabNo = Convert.ToString(value);
				}
			}
				
			public System.String OrderLabTglOrder
			{
				get
				{
					System.DateTime? data = entity.OrderLabTglOrder;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderLabTglOrder = null;
					else entity.OrderLabTglOrder = Convert.ToDateTime(value);
				}
			}
				
			public System.String CheckupResultTestCode
			{
				get
				{
					System.String data = entity.CheckupResultTestCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CheckupResultTestCode = null;
					else entity.CheckupResultTestCode = Convert.ToString(value);
				}
			}
				
			public System.String OrderLabJamOrder
			{
				get
				{
					System.String data = entity.OrderLabJamOrder;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderLabJamOrder = null;
					else entity.OrderLabJamOrder = Convert.ToString(value);
				}
			}
				
			public System.String OrderLabStatus
			{
				get
				{
					System.String data = entity.OrderLabStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderLabStatus = null;
					else entity.OrderLabStatus = Convert.ToString(value);
				}
			}
				
			public System.String OrderLabCito
			{
				get
				{
					System.String data = entity.OrderLabCito;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderLabCito = null;
					else entity.OrderLabCito = Convert.ToString(value);
				}
			}
			

			private esOrderLabDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esOrderLabDetailQuery query)
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
				throw new Exception("esOrderLabDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esOrderLabDetailQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return OrderLabDetailMetadata.Meta();
			}
		}	
		

		public esQueryItem OrderLabNo
		{
			get
			{
				return new esQueryItem(this, OrderLabDetailMetadata.ColumnNames.OrderLabNo, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderLabTglOrder
		{
			get
			{
				return new esQueryItem(this, OrderLabDetailMetadata.ColumnNames.OrderLabTglOrder, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem CheckupResultTestCode
		{
			get
			{
				return new esQueryItem(this, OrderLabDetailMetadata.ColumnNames.CheckupResultTestCode, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderLabJamOrder
		{
			get
			{
				return new esQueryItem(this, OrderLabDetailMetadata.ColumnNames.OrderLabJamOrder, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderLabStatus
		{
			get
			{
				return new esQueryItem(this, OrderLabDetailMetadata.ColumnNames.OrderLabStatus, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderLabCito
		{
			get
			{
				return new esQueryItem(this, OrderLabDetailMetadata.ColumnNames.OrderLabCito, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("OrderLabDetailCollection")]
	public partial class OrderLabDetailCollection : esOrderLabDetailCollection, IEnumerable<OrderLabDetail>
	{
		public OrderLabDetailCollection()
		{

		}
		
		public static implicit operator List<OrderLabDetail>(OrderLabDetailCollection coll)
		{
			List<OrderLabDetail> list = new List<OrderLabDetail>();
			
			foreach (OrderLabDetail emp in coll)
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
				return  OrderLabDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new OrderLabDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new OrderLabDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new OrderLabDetail();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public OrderLabDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new OrderLabDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(OrderLabDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public OrderLabDetail AddNew()
		{
			OrderLabDetail entity = base.AddNewEntity() as OrderLabDetail;
			
			return entity;
		}

		public OrderLabDetail FindByPrimaryKey()
		{
			return base.FindByPrimaryKey() as OrderLabDetail;
		}


		#region IEnumerable<OrderLabDetail> Members

		IEnumerator<OrderLabDetail> IEnumerable<OrderLabDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as OrderLabDetail;
			}
		}

		#endregion
		
		private OrderLabDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'OrderLabDetail' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("OrderLabDetail ()")]
	[Serializable]
	public partial class OrderLabDetail : esOrderLabDetail
	{
		public OrderLabDetail()
		{

		}
	
		public OrderLabDetail(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return OrderLabDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esOrderLabDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new OrderLabDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public OrderLabDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new OrderLabDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(OrderLabDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private OrderLabDetailQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class OrderLabDetailQuery : esOrderLabDetailQuery
	{
		public OrderLabDetailQuery()
		{

		}		
		
		public OrderLabDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "OrderLabDetailQuery";
        }
		
			
	}


	[Serializable]
	public partial class OrderLabDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected OrderLabDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(OrderLabDetailMetadata.ColumnNames.OrderLabNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderLabDetailMetadata.PropertyNames.OrderLabNo;
			c.CharacterMaxLength = 25;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabDetailMetadata.ColumnNames.OrderLabTglOrder, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = OrderLabDetailMetadata.PropertyNames.OrderLabTglOrder;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabDetailMetadata.ColumnNames.CheckupResultTestCode, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderLabDetailMetadata.PropertyNames.CheckupResultTestCode;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabDetailMetadata.ColumnNames.OrderLabJamOrder, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderLabDetailMetadata.PropertyNames.OrderLabJamOrder;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabDetailMetadata.ColumnNames.OrderLabStatus, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderLabDetailMetadata.PropertyNames.OrderLabStatus;
			c.CharacterMaxLength = 1;
			c.HasDefault = true;
			c.Default = @"('F')";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(OrderLabDetailMetadata.ColumnNames.OrderLabCito, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderLabDetailMetadata.PropertyNames.OrderLabCito;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public OrderLabDetailMetadata Meta()
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
			 public const string OrderLabNo = "OrderLabNo";
			 public const string OrderLabTglOrder = "OrderLabTglOrder";
			 public const string CheckupResultTestCode = "CheckupResultTestCode";
			 public const string OrderLabJamOrder = "OrderLabJamOrder";
			 public const string OrderLabStatus = "OrderLabStatus";
			 public const string OrderLabCito = "OrderLabCito";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string OrderLabNo = "OrderLabNo";
			 public const string OrderLabTglOrder = "OrderLabTglOrder";
			 public const string CheckupResultTestCode = "CheckupResultTestCode";
			 public const string OrderLabJamOrder = "OrderLabJamOrder";
			 public const string OrderLabStatus = "OrderLabStatus";
			 public const string OrderLabCito = "OrderLabCito";
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
			lock (typeof(OrderLabDetailMetadata))
			{
				if(OrderLabDetailMetadata.mapDelegates == null)
				{
					OrderLabDetailMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (OrderLabDetailMetadata.meta == null)
				{
					OrderLabDetailMetadata.meta = new OrderLabDetailMetadata();
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
				

				meta.AddTypeMap("OrderLabNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrderLabTglOrder", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CheckupResultTestCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrderLabJamOrder", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrderLabStatus", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("OrderLabCito", new esTypeMap("char", "System.String"));			
				
				
				
				meta.Source = "OrderLabDetail";
				meta.Destination = "OrderLabDetail";
				
				meta.spInsert = "proc_OrderLabDetailInsert";				
				meta.spUpdate = "proc_OrderLabDetailUpdate";		
				meta.spDelete = "proc_OrderLabDetailDelete";
				meta.spLoadAll = "proc_OrderLabDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_OrderLabDetailLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private OrderLabDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
